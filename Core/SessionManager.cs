using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Models;
using HR.Models.DTOs;

namespace HR.Core
{
    /// <summary>
    /// مدير جلسات المستخدمين وبيانات النظام المشتركة
    /// </summary>
    public class SessionManager
    {
        // بيانات المستخدم الحالي
        private static User _currentUser;
        private static List<string> _currentUserPermissions;
        
        // بيانات الشركة
        private static Company _company;
        
        // قائمة بالجلسات النشطة
        private static Dictionary<int, DateTime> _activeSessions = new Dictionary<int, DateTime>();
        
        // الإعدادات العامة للنظام
        private static Dictionary<string, string> _systemSettings = new Dictionary<string, string>();
        
        /// <summary>
        /// تهيئة مدير الجلسات
        /// </summary>
        public static void Initialize()
        {
            try
            {
                LoadSystemSettings();
                LoadCompanyData();
                LogManager.LogInfo("تم تهيئة مدير الجلسات بنجاح");
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تهيئة مدير الجلسات");
            }
        }

        #region Authentication

        /// <summary>
        /// تسجيل دخول المستخدم
        /// </summary>
        /// <param name="username">اسم المستخدم</param>
        /// <param name="password">كلمة المرور</param>
        /// <param name="ipAddress">عنوان IP</param>
        /// <returns>نتيجة تسجيل الدخول</returns>
        public static AuthResultDTO Login(string username, string password, string ipAddress)
        {
            try
            {
                // البحث عن المستخدم في قاعدة البيانات
                string query = @"
                    SELECT u.*, r.Name as RoleName 
                    FROM Users u 
                    LEFT JOIN Roles r ON u.RoleID = r.ID 
                    WHERE u.Username = @Username";
                
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", username)
                };
                
                DataTable userTable = ConnectionManager.ExecuteQuery(query, parameters);
                
                if (userTable.Rows.Count == 0)
                {
                    LogManager.LogLogin(username, false, ipAddress);
                    return new AuthResultDTO 
                    { 
                        IsAuthenticated = false, 
                        Message = "اسم المستخدم غير موجود" 
                    };
                }
                
                DataRow userRow = userTable.Rows[0];
                int userId = Convert.ToInt32(userRow["ID"]);
                string storedPasswordHash = userRow["PasswordHash"].ToString();
                string storedSalt = userRow["PasswordSalt"].ToString();
                bool isActive = Convert.ToBoolean(userRow["IsActive"]);
                bool isLocked = Convert.ToBoolean(userRow["IsLocked"]);
                
                if (!isActive)
                {
                    LogManager.LogLogin(username, false, ipAddress);
                    return new AuthResultDTO 
                    { 
                        IsAuthenticated = false, 
                        Message = "الحساب غير نشط" 
                    };
                }
                
                if (isLocked)
                {
                    DateTime? lockoutEnd = userRow["LockoutEnd"] == DBNull.Value ? 
                        (DateTime?)null : Convert.ToDateTime(userRow["LockoutEnd"]);
                    
                    if (lockoutEnd.HasValue && lockoutEnd.Value > DateTime.Now)
                    {
                        LogManager.LogLogin(username, false, ipAddress);
                        return new AuthResultDTO 
                        { 
                            IsAuthenticated = false, 
                            Message = $"الحساب مقفل حتى {lockoutEnd.Value:yyyy-MM-dd HH:mm}" 
                        };
                    }
                    else
                    {
                        // إذا انتهت فترة القفل، نقوم بإعادة تفعيل الحساب
                        UnlockUser(userId);
                    }
                }
                
                // التحقق من كلمة المرور
                string hashedPassword = SecurityManager.HashPassword(password, storedSalt);
                
                if (hashedPassword != storedPasswordHash)
                {
                    // زيادة عدد محاولات تسجيل الدخول الفاشلة
                    IncrementFailedLoginAttempts(userId);
                    
                    // التحقق مما إذا كان يجب قفل الحساب
                    CheckAndLockAccount(userId);
                    
                    LogManager.LogLogin(username, false, ipAddress);
                    return new AuthResultDTO 
                    { 
                        IsAuthenticated = false, 
                        Message = "كلمة المرور غير صحيحة" 
                    };
                }
                
                // إعادة تعيين محاولات تسجيل الدخول الفاشلة
                ResetFailedLoginAttempts(userId);
                
                // تحديث معلومات آخر تسجيل دخول
                UpdateLastLogin(userId);
                
                // تسجيل عملية تسجيل الدخول
                RecordLoginHistory(userId, ipAddress);
                
                // تحميل بيانات المستخدم الحالي
                _currentUser = new User
                {
                    ID = userId,
                    Username = username,
                    Email = userRow["Email"].ToString(),
                    FullName = userRow["FullName"].ToString(),
                    RoleID = userRow["RoleID"] == DBNull.Value ? (int?)null : Convert.ToInt32(userRow["RoleID"]),
                    EmployeeID = userRow["EmployeeID"] == DBNull.Value ? (int?)null : Convert.ToInt32(userRow["EmployeeID"]),
                    IsActive = isActive,
                    MustChangePassword = Convert.ToBoolean(userRow["MustChangePassword"]),
                    LastLogin = userRow["LastLogin"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(userRow["LastLogin"])
                };
                
                // تحميل صلاحيات المستخدم
                _currentUserPermissions = LoadUserPermissions(userId);
                
                // إضافة الجلسة النشطة
                _activeSessions[userId] = DateTime.Now;
                
                LogManager.LogLogin(username, true, ipAddress);
                
                return new AuthResultDTO 
                { 
                    IsAuthenticated = true, 
                    Message = "تم تسجيل الدخول بنجاح",
                    User = MapUserToDto(_currentUser),
                    Permissions = _currentUserPermissions
                };
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل تسجيل الدخول للمستخدم {username}");
                return new AuthResultDTO 
                { 
                    IsAuthenticated = false, 
                    Message = "حدث خطأ أثناء تسجيل الدخول. الرجاء المحاولة مرة أخرى." 
                };
            }
        }
        
        /// <summary>
        /// تسجيل خروج المستخدم الحالي
        /// </summary>
        public static void Logout()
        {
            if (_currentUser != null)
            {
                // تحديث سجل تسجيل الدخول بوقت الخروج
                UpdateLogoutTime(_currentUser.ID);
                
                // إزالة الجلسة النشطة
                if (_activeSessions.ContainsKey(_currentUser.ID))
                {
                    _activeSessions.Remove(_currentUser.ID);
                }
                
                // تفريغ بيانات المستخدم
                _currentUser = null;
                _currentUserPermissions = null;
                
                LogManager.LogInfo("تم تسجيل الخروج بنجاح");
            }
        }
        
        /// <summary>
        /// التحقق من صلاحيات المستخدم الحالي
        /// </summary>
        /// <param name="module">اسم الوحدة</param>
        /// <param name="permission">الصلاحية المطلوبة</param>
        /// <returns>هل يمتلك المستخدم الصلاحية؟</returns>
        public static bool HasPermission(string module, string permission)
        {
            if (_currentUser == null || _currentUserPermissions == null)
            {
                return false;
            }
            
            string permissionKey = $"{module}.{permission}";
            return _currentUserPermissions.Contains(permissionKey);
        }
        
        /// <summary>
        /// زيادة عدد محاولات تسجيل الدخول الفاشلة
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        private static void IncrementFailedLoginAttempts(int userId)
        {
            string query = @"
                UPDATE Users 
                SET FailedLoginAttempts = FailedLoginAttempts + 1 
                WHERE ID = @UserID";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };
            
            ConnectionManager.ExecuteNonQuery(query, parameters);
        }
        
        /// <summary>
        /// إعادة تعيين محاولات تسجيل الدخول الفاشلة
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        private static void ResetFailedLoginAttempts(int userId)
        {
            string query = @"
                UPDATE Users 
                SET FailedLoginAttempts = 0, IsLocked = 0, LockoutEnd = NULL 
                WHERE ID = @UserID";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };
            
            ConnectionManager.ExecuteNonQuery(query, parameters);
        }
        
        /// <summary>
        /// التحقق مما إذا كان يجب قفل الحساب
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        private static void CheckAndLockAccount(int userId)
        {
            // الحصول على عدد محاولات تسجيل الدخول الفاشلة
            string query = @"
                SELECT FailedLoginAttempts 
                FROM Users 
                WHERE ID = @UserID";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };
            
            object result = ConnectionManager.ExecuteScalar(query, parameters);
            int failedAttempts = Convert.ToInt32(result);
            
            // إذا تجاوز العدد المسموح به، يتم قفل الحساب
            int maxFailedAttempts = GetMaxFailedLoginAttempts();
            if (failedAttempts >= maxFailedAttempts)
            {
                int lockoutMinutes = GetAccountLockoutMinutes();
                DateTime lockoutEnd = DateTime.Now.AddMinutes(lockoutMinutes);
                
                query = @"
                    UPDATE Users 
                    SET IsLocked = 1, LockoutEnd = @LockoutEnd 
                    WHERE ID = @UserID";
                
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@LockoutEnd", lockoutEnd)
                };
                
                ConnectionManager.ExecuteNonQuery(query, parameters);
                
                LogManager.LogWarning($"تم قفل حساب المستخدم (ID: {userId}) بعد {failedAttempts} محاولات فاشلة حتى {lockoutEnd:yyyy-MM-dd HH:mm}");
            }
        }
        
        /// <summary>
        /// إلغاء قفل حساب المستخدم
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        private static void UnlockUser(int userId)
        {
            string query = @"
                UPDATE Users 
                SET IsLocked = 0, LockoutEnd = NULL, FailedLoginAttempts = 0 
                WHERE ID = @UserID";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };
            
            ConnectionManager.ExecuteNonQuery(query, parameters);
            
            LogManager.LogInfo($"تم إلغاء قفل حساب المستخدم (ID: {userId})");
        }
        
        /// <summary>
        /// تحديث وقت آخر تسجيل دخول
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        private static void UpdateLastLogin(int userId)
        {
            string query = @"
                UPDATE Users 
                SET LastLogin = @LastLogin 
                WHERE ID = @UserID";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@LastLogin", DateTime.Now)
            };
            
            ConnectionManager.ExecuteNonQuery(query, parameters);
        }
        
        /// <summary>
        /// تسجيل عملية تسجيل الدخول
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        /// <param name="ipAddress">عنوان IP</param>
        private static void RecordLoginHistory(int userId, string ipAddress)
        {
            string query = @"
                INSERT INTO LoginHistory (UserID, LoginTime, IPAddress, MachineName, LoginStatus) 
                VALUES (@UserID, @LoginTime, @IPAddress, @MachineName, @LoginStatus);
                SELECT SCOPE_IDENTITY();";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@LoginTime", DateTime.Now),
                new SqlParameter("@IPAddress", ipAddress),
                new SqlParameter("@MachineName", Environment.MachineName),
                new SqlParameter("@LoginStatus", "Success")
            };
            
            ConnectionManager.ExecuteScalar(query, parameters);
        }
        
        /// <summary>
        /// تحديث وقت تسجيل الخروج
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        private static void UpdateLogoutTime(int userId)
        {
            string query = @"
                UPDATE LoginHistory 
                SET LogoutTime = @LogoutTime 
                WHERE UserID = @UserID AND LogoutTime IS NULL";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@LogoutTime", DateTime.Now)
            };
            
            ConnectionManager.ExecuteNonQuery(query, parameters);
        }
        
        /// <summary>
        /// تحميل صلاحيات المستخدم
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        /// <returns>قائمة الصلاحيات</returns>
        private static List<string> LoadUserPermissions(int userId)
        {
            List<string> permissions = new List<string>();
            
            string query = @"
                SELECT rp.ModuleName, rp.CanView, rp.CanAdd, rp.CanEdit, rp.CanDelete, 
                       rp.CanPrint, rp.CanExport, rp.CanImport, rp.CanApprove
                FROM Users u
                JOIN Roles r ON u.RoleID = r.ID
                JOIN RolePermissions rp ON r.ID = rp.RoleID
                WHERE u.ID = @UserID";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };
            
            DataTable permissionsTable = ConnectionManager.ExecuteQuery(query, parameters);
            
            foreach (DataRow row in permissionsTable.Rows)
            {
                string module = row["ModuleName"].ToString();
                
                if (Convert.ToBoolean(row["CanView"]))
                    permissions.Add($"{module}.View");
                
                if (Convert.ToBoolean(row["CanAdd"]))
                    permissions.Add($"{module}.Add");
                
                if (Convert.ToBoolean(row["CanEdit"]))
                    permissions.Add($"{module}.Edit");
                
                if (Convert.ToBoolean(row["CanDelete"]))
                    permissions.Add($"{module}.Delete");
                
                if (Convert.ToBoolean(row["CanPrint"]))
                    permissions.Add($"{module}.Print");
                
                if (Convert.ToBoolean(row["CanExport"]))
                    permissions.Add($"{module}.Export");
                
                if (Convert.ToBoolean(row["CanImport"]))
                    permissions.Add($"{module}.Import");
                
                if (Convert.ToBoolean(row["CanApprove"]))
                    permissions.Add($"{module}.Approve");
            }
            
            return permissions;
        }
        
        /// <summary>
        /// تحويل كائن المستخدم إلى DTO
        /// </summary>
        /// <param name="user">كائن المستخدم</param>
        /// <returns>DTO المستخدم</returns>
        private static UserDTO MapUserToDto(User user)
        {
            if (user == null)
            {
                return null;
            }
            
            return new UserDTO
            {
                ID = user.ID,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                RoleID = user.RoleID,
                RoleName = GetRoleName(user.RoleID),
                EmployeeID = user.EmployeeID,
                EmployeeName = GetEmployeeName(user.EmployeeID),
                IsActive = user.IsActive,
                MustChangePassword = user.MustChangePassword,
                LastLogin = user.LastLogin,
                IsLocked = user.IsLocked,
                LockoutEnd = user.LockoutEnd
            };
        }
        
        /// <summary>
        /// الحصول على اسم الدور
        /// </summary>
        /// <param name="roleId">معرف الدور</param>
        /// <returns>اسم الدور</returns>
        private static string GetRoleName(int? roleId)
        {
            if (roleId == null)
            {
                return null;
            }
            
            string query = "SELECT Name FROM Roles WHERE ID = @RoleID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RoleID", roleId)
            };
            
            object result = ConnectionManager.ExecuteScalar(query, parameters);
            return result?.ToString();
        }
        
        /// <summary>
        /// الحصول على اسم الموظف
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <returns>اسم الموظف</returns>
        private static string GetEmployeeName(int? employeeId)
        {
            if (employeeId == null)
            {
                return null;
            }
            
            string query = "SELECT FullName FROM Employees WHERE ID = @EmployeeID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", employeeId)
            };
            
            object result = ConnectionManager.ExecuteScalar(query, parameters);
            return result?.ToString();
        }
        
        #endregion
        
        #region User Properties
        
        /// <summary>
        /// الحصول على المستخدم الحالي
        /// </summary>
        /// <returns>المستخدم الحالي</returns>
        public static User GetCurrentUser()
        {
            return _currentUser;
        }
        
        /// <summary>
        /// الحصول على معرف المستخدم الحالي
        /// </summary>
        /// <returns>معرف المستخدم الحالي</returns>
        public static int? GetCurrentUserId()
        {
            return _currentUser?.ID;
        }
        
        /// <summary>
        /// التحقق مما إذا كان المستخدم قد قام بتسجيل الدخول
        /// </summary>
        /// <returns>هل قام المستخدم بتسجيل الدخول؟</returns>
        public static bool IsAuthenticated()
        {
            return _currentUser != null;
        }
        
        /// <summary>
        /// التحقق مما إذا كان المستخدم الحالي مسؤول النظام
        /// </summary>
        /// <returns>هل المستخدم الحالي مسؤول النظام؟</returns>
        public static bool IsCurrentUserAdmin()
        {
            if (_currentUser == null)
            {
                return false;
            }
            
            // التحقق من اسم الدور
            string roleName = GetRoleName(_currentUser.RoleID);
            return roleName?.ToLower() == "admin" || roleName?.ToLower() == "administrator";
        }
        
        #endregion
        
        #region Company Data
        
        /// <summary>
        /// تحميل بيانات الشركة
        /// </summary>
        private static void LoadCompanyData()
        {
            try
            {
                string query = "SELECT * FROM Company";
                DataTable companyTable = ConnectionManager.ExecuteQuery(query);
                
                if (companyTable.Rows.Count > 0)
                {
                    DataRow row = companyTable.Rows[0];
                    
                    _company = new Company
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Name = row["Name"].ToString(),
                        LegalName = row["LegalName"].ToString(),
                        CommercialRecord = row["CommercialRecord"].ToString(),
                        TaxNumber = row["TaxNumber"].ToString(),
                        Address = row["Address"].ToString(),
                        Phone = row["Phone"].ToString(),
                        Email = row["Email"].ToString(),
                        Website = row["Website"].ToString(),
                        Logo = row["Logo"] != DBNull.Value ? (byte[])row["Logo"] : null,
                        EstablishmentDate = row["EstablishmentDate"] != DBNull.Value ? 
                            (DateTime?)Convert.ToDateTime(row["EstablishmentDate"]) : null,
                        Notes = row["Notes"].ToString(),
                        CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                        UpdatedAt = row["UpdatedAt"] != DBNull.Value ? 
                            (DateTime?)Convert.ToDateTime(row["UpdatedAt"]) : null
                    };
                    
                    LogManager.LogInfo("تم تحميل بيانات الشركة بنجاح");
                }
                else
                {
                    LogManager.LogWarning("لم يتم العثور على بيانات الشركة");
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل بيانات الشركة");
            }
        }
        
        /// <summary>
        /// الحصول على بيانات الشركة
        /// </summary>
        /// <returns>بيانات الشركة</returns>
        public static Company GetCompanyData()
        {
            return _company;
        }
        
        /// <summary>
        /// الحصول على ملخص بيانات الشركة
        /// </summary>
        /// <returns>ملخص بيانات الشركة</returns>
        public static CompanySummaryDTO GetCompanySummary()
        {
            if (_company == null)
            {
                return null;
            }
            
            return new CompanySummaryDTO
            {
                ID = _company.ID,
                Name = _company.Name,
                LegalName = _company.LegalName,
                Logo = _company.Logo,
                Address = _company.Address,
                Phone = _company.Phone,
                Email = _company.Email
            };
        }
        
        #endregion
        
        #region System Settings
        
        /// <summary>
        /// تحميل إعدادات النظام
        /// </summary>
        private static void LoadSystemSettings()
        {
            try
            {
                string query = "SELECT SettingKey, SettingValue FROM SystemSettings";
                DataTable settingsTable = ConnectionManager.ExecuteQuery(query);
                
                _systemSettings.Clear();
                
                foreach (DataRow row in settingsTable.Rows)
                {
                    string key = row["SettingKey"].ToString();
                    string value = row["SettingValue"].ToString();
                    
                    _systemSettings[key] = value;
                }
                
                LogManager.LogInfo("تم تحميل إعدادات النظام بنجاح");
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل إعدادات النظام");
            }
        }
        
        /// <summary>
        /// الحصول على قيمة إعداد النظام
        /// </summary>
        /// <param name="key">مفتاح الإعداد</param>
        /// <param name="defaultValue">القيمة الافتراضية</param>
        /// <returns>قيمة الإعداد</returns>
        public static string GetSetting(string key, string defaultValue = "")
        {
            if (_systemSettings.ContainsKey(key))
            {
                return _systemSettings[key];
            }
            
            return defaultValue;
        }
        
        /// <summary>
        /// الحصول على قيمة إعداد النظام كرقم صحيح
        /// </summary>
        /// <param name="key">مفتاح الإعداد</param>
        /// <param name="defaultValue">القيمة الافتراضية</param>
        /// <returns>قيمة الإعداد</returns>
        public static int GetSettingInt(string key, int defaultValue = 0)
        {
            if (_systemSettings.ContainsKey(key))
            {
                if (int.TryParse(_systemSettings[key], out int value))
                {
                    return value;
                }
            }
            
            return defaultValue;
        }
        
        /// <summary>
        /// الحصول على قيمة إعداد النظام كقيمة منطقية
        /// </summary>
        /// <param name="key">مفتاح الإعداد</param>
        /// <param name="defaultValue">القيمة الافتراضية</param>
        /// <returns>قيمة الإعداد</returns>
        public static bool GetSettingBool(string key, bool defaultValue = false)
        {
            if (_systemSettings.ContainsKey(key))
            {
                string value = _systemSettings[key].ToLower();
                return value == "true" || value == "1" || value == "yes" || value == "y";
            }
            
            return defaultValue;
        }
        
        /// <summary>
        /// الحصول على قيمة إعداد النظام كقيمة عشرية
        /// </summary>
        /// <param name="key">مفتاح الإعداد</param>
        /// <param name="defaultValue">القيمة الافتراضية</param>
        /// <returns>قيمة الإعداد</returns>
        public static decimal GetSettingDecimal(string key, decimal defaultValue = 0)
        {
            if (_systemSettings.ContainsKey(key))
            {
                if (decimal.TryParse(_systemSettings[key], out decimal value))
                {
                    return value;
                }
            }
            
            return defaultValue;
        }
        
        /// <summary>
        /// الحصول على الحد الأقصى لمحاولات تسجيل الدخول الفاشلة
        /// </summary>
        /// <returns>الحد الأقصى لمحاولات تسجيل الدخول الفاشلة</returns>
        private static int GetMaxFailedLoginAttempts()
        {
            return GetSettingInt("MaxFailedLoginAttempts", 5);
        }
        
        /// <summary>
        /// الحصول على مدة قفل الحساب بالدقائق
        /// </summary>
        /// <returns>مدة قفل الحساب بالدقائق</returns>
        private static int GetAccountLockoutMinutes()
        {
            return GetSettingInt("AccountLockoutMinutes", 30);
        }
        
        #endregion
    }
}