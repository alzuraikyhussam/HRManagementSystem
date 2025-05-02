using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using HR.DataAccess;
using HR.Models;

namespace HR.Core
{
    /// <summary>
    /// مدير الجلسة والمصادقة
    /// </summary>
    public class SessionManager
    {
        private static SessionManager _instance;
        private static readonly object _lock = new object();
        private readonly DatabaseContext _dbContext;

        private User _currentUser;
        private List<RolePermission> _currentUserPermissions;

        private SessionManager()
        {
            _dbContext = new DatabaseContext();
            _currentUser = null;
            _currentUserPermissions = new List<RolePermission>();
        }

        /// <summary>
        /// الحصول على نسخة من مدير الجلسة (نمط Singleton)
        /// </summary>
        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SessionManager();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// المستخدم الحالي
        /// </summary>
        public User CurrentUser => _currentUser;

        /// <summary>
        /// تسجيل الدخول للنظام
        /// </summary>
        /// <param name="username">اسم المستخدم</param>
        /// <param name="password">كلمة المرور</param>
        /// <param name="ipAddress">عنوان IP</param>
        /// <param name="machineName">اسم الجهاز</param>
        /// <param name="userAgent">معلومات المتصفح</param>
        /// <returns>نتيجة تسجيل الدخول</returns>
        public bool Login(string username, string password, string ipAddress = "", string machineName = "", string userAgent = "")
        {
            try
            {
                // التحقق من بيانات المستخدم
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Username", username)
                };

                string query = @"
                SELECT u.*, r.* 
                FROM Users u
                INNER JOIN Roles r ON u.RoleID = r.ID
                WHERE u.Username = @Username AND u.IsActive = 1";

                var dataTable = _dbContext.ExecuteReader(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    return false;
                }

                var row = dataTable.Rows[0];
                string passwordHash = row["PasswordHash"].ToString();
                string passwordSalt = row["PasswordSalt"].ToString();

                // التحقق من كلمة المرور
                string hashedPassword = HashPassword(password, passwordSalt);
                if (passwordHash != hashedPassword)
                {
                    // زيادة عدد محاولات الدخول الفاشلة
                    IncrementFailedLoginAttempts(username);
                    
                    // تسجيل محاولة الدخول
                    LogLoginAttempt(Convert.ToInt32(row["ID"]), false, ipAddress, machineName, userAgent);
                    
                    return false;
                }

                // التحقق من قفل الحساب
                bool isLocked = Convert.ToBoolean(row["IsLocked"]);
                if (isLocked)
                {
                    DateTime? lockoutEnd = row["LockoutEnd"] as DateTime?;
                    if (lockoutEnd.HasValue && lockoutEnd.Value > DateTime.Now)
                    {
                        // تسجيل محاولة الدخول
                        LogLoginAttempt(Convert.ToInt32(row["ID"]), false, ipAddress, machineName, userAgent);
                        return false;
                    }
                    else
                    {
                        // إلغاء قفل الحساب
                        UnlockUser(Convert.ToInt32(row["ID"]));
                    }
                }

                // إعادة ضبط عدد محاولات الدخول الفاشلة
                ResetFailedLoginAttempts(Convert.ToInt32(row["ID"]));

                // تحديث آخر تسجيل دخول
                UpdateLastLogin(Convert.ToInt32(row["ID"]), ipAddress);

                // تسجيل محاولة الدخول
                LogLoginAttempt(Convert.ToInt32(row["ID"]), true, ipAddress, machineName, userAgent);

                // إنشاء كائن المستخدم
                _currentUser = new User
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Username = row["Username"].ToString(),
                    FullName = row["FullName"].ToString(),
                    Email = row["Email"].ToString(),
                    RoleID = Convert.ToInt32(row["RoleID"]),
                    Role = new Role
                    {
                        ID = Convert.ToInt32(row["RoleID"]),
                        Name = row["Name"].ToString(),
                        Description = row["Description"].ToString(),
                        IsAdmin = Convert.ToBoolean(row["IsAdmin"])
                    },
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    LastLoginDate = DateTime.Now
                };

                // تحميل صلاحيات المستخدم
                LoadUserPermissions();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// تسجيل الخروج من النظام
        /// </summary>
        public void Logout()
        {
            if (_currentUser != null)
            {
                // تسجيل وقت الخروج
                UpdateLogoutTime(_currentUser.ID);

                _currentUser = null;
                _currentUserPermissions.Clear();
            }
        }

        /// <summary>
        /// التحقق من وجود صلاحية معينة للمستخدم الحالي
        /// </summary>
        /// <param name="moduleName">اسم الوحدة</param>
        /// <param name="permissionType">نوع الصلاحية</param>
        /// <returns>نتيجة التحقق</returns>
        public bool HasPermission(string moduleName, string permissionType)
        {
            // إذا كان المستخدم مدير النظام، يتم منحه جميع الصلاحيات
            if (_currentUser?.Role?.IsAdmin == true)
            {
                return true;
            }

            // التحقق من وجود صلاحية محددة
            var permission = _currentUserPermissions.Find(p => p.ModuleName.Equals(moduleName, StringComparison.OrdinalIgnoreCase));

            if (permission == null)
            {
                return false;
            }

            switch (permissionType.ToLower())
            {
                case "view":
                    return permission.CanView;
                case "add":
                    return permission.CanAdd;
                case "edit":
                    return permission.CanEdit;
                case "delete":
                    return permission.CanDelete;
                case "print":
                    return permission.CanPrint;
                case "export":
                    return permission.CanExport;
                case "approve":
                    return permission.CanApprove;
                default:
                    return false;
            }
        }

        /// <summary>
        /// تحميل صلاحيات المستخدم الحالي
        /// </summary>
        private void LoadUserPermissions()
        {
            _currentUserPermissions.Clear();

            if (_currentUser == null || _currentUser.RoleID <= 0)
            {
                return;
            }

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@RoleID", _currentUser.RoleID)
            };

            string query = @"
            SELECT * FROM RolePermissions
            WHERE RoleID = @RoleID";

            var dataTable = _dbContext.ExecuteReader(query, parameters);

            foreach (var row in dataTable.AsEnumerable())
            {
                var permission = new RolePermission
                {
                    ID = Convert.ToInt32(row["ID"]),
                    RoleID = Convert.ToInt32(row["RoleID"]),
                    ModuleName = row["ModuleName"].ToString(),
                    PermissionName = row["PermissionName"].ToString(),
                    Description = row["Description"].ToString(),
                    CanView = Convert.ToBoolean(row["CanView"]),
                    CanAdd = Convert.ToBoolean(row["CanAdd"]),
                    CanEdit = Convert.ToBoolean(row["CanEdit"]),
                    CanDelete = Convert.ToBoolean(row["CanDelete"]),
                    CanPrint = Convert.ToBoolean(row["CanPrint"]),
                    CanExport = Convert.ToBoolean(row["CanExport"]),
                    CanApprove = Convert.ToBoolean(row["CanApprove"])
                };

                _currentUserPermissions.Add(permission);
            }
        }

        /// <summary>
        /// زيادة عدد محاولات الدخول الفاشلة
        /// </summary>
        /// <param name="username">اسم المستخدم</param>
        private void IncrementFailedLoginAttempts(string username)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Username", username)
            };

            string query = @"
            UPDATE Users 
            SET FailedLoginAttempts = FailedLoginAttempts + 1,
                IsLocked = CASE WHEN FailedLoginAttempts + 1 >= 5 THEN 1 ELSE IsLocked END,
                LockoutEnd = CASE WHEN FailedLoginAttempts + 1 >= 5 THEN DATEADD(MINUTE, 30, GETDATE()) ELSE LockoutEnd END,
                UpdatedAt = GETDATE()
            WHERE Username = @Username";

            _dbContext.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// إعادة ضبط عدد محاولات الدخول الفاشلة
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        private void ResetFailedLoginAttempts(int userId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserID", userId)
            };

            string query = @"
            UPDATE Users 
            SET FailedLoginAttempts = 0,
                UpdatedAt = GETDATE()
            WHERE ID = @UserID";

            _dbContext.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// فتح قفل حساب المستخدم
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        private void UnlockUser(int userId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserID", userId)
            };

            string query = @"
            UPDATE Users 
            SET IsLocked = 0,
                LockoutEnd = NULL,
                FailedLoginAttempts = 0,
                UpdatedAt = GETDATE()
            WHERE ID = @UserID";

            _dbContext.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// تحديث تاريخ آخر تسجيل دخول
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        /// <param name="ipAddress">عنوان IP</param>
        private void UpdateLastLogin(int userId, string ipAddress)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@IPAddress", ipAddress ?? "")
            };

            string query = @"
            UPDATE Users 
            SET LastLogin = GETDATE(),
                LastLoginIP = @IPAddress,
                UpdatedAt = GETDATE()
            WHERE ID = @UserID";

            _dbContext.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// تسجيل محاولة الدخول
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        /// <param name="success">نجاح العملية</param>
        /// <param name="ipAddress">عنوان IP</param>
        /// <param name="machineName">اسم الجهاز</param>
        /// <param name="userAgent">معلومات المتصفح</param>
        private void LogLoginAttempt(int userId, bool success, string ipAddress, string machineName, string userAgent)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@LoginTime", DateTime.Now),
                new SqlParameter("@IPAddress", ipAddress ?? ""),
                new SqlParameter("@MachineName", machineName ?? ""),
                new SqlParameter("@LoginStatus", success ? "Success" : "Failed"),
                new SqlParameter("@UserAgent", userAgent ?? "")
            };

            string query = @"
            INSERT INTO LoginHistory (UserID, LoginTime, IPAddress, MachineName, LoginStatus, UserAgent)
            VALUES (@UserID, @LoginTime, @IPAddress, @MachineName, @LoginStatus, @UserAgent)";

            _dbContext.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// تحديث وقت تسجيل الخروج
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        private void UpdateLogoutTime(int userId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@LogoutTime", DateTime.Now)
            };

            string query = @"
            UPDATE LoginHistory 
            SET LogoutTime = @LogoutTime
            WHERE UserID = @UserID AND LogoutTime IS NULL
            AND LoginTime = (
                SELECT MAX(LoginTime) 
                FROM LoginHistory 
                WHERE UserID = @UserID AND LogoutTime IS NULL
            )";

            _dbContext.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// تشفير كلمة المرور
        /// </summary>
        /// <param name="password">كلمة المرور الأصلية</param>
        /// <param name="salt">ملح التشفير</param>
        /// <returns>كلمة المرور المشفرة</returns>
        public string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string saltedPassword = password + salt;
                byte[] bytes = Encoding.UTF8.GetBytes(saltedPassword);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// إنشاء ملح تشفير جديد
        /// </summary>
        /// <returns>ملح التشفير</returns>
        public string GenerateSalt()
        {
            byte[] salt = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
    }
}