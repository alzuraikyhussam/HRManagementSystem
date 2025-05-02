using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// مستودع بيانات المستخدمين وعمليات المصادقة
    /// </summary>
    public class UserRepository
    {
        // ثابت ملح التشفير للنظام
        private const string SYSTEM_SALT = "HR_SYSTEM_SALT_2022";

        /// <summary>
        /// التحقق من صحة بيانات تسجيل الدخول
        /// </summary>
        /// <param name="username">اسم المستخدم</param>
        /// <param name="password">كلمة المرور</param>
        /// <returns>بيانات المستخدم إذا كانت المصادقة ناجحة، وإلا قيمة فارغة</returns>
        public UserDTO ValidateLogin(string username, string password)
        {
            try
            {
                string query = @"
                    SELECT u.ID, u.Username, u.PasswordHash, u.PasswordSalt, u.Email, u.FullName,
                           u.RoleID, r.Name AS RoleName, u.EmployeeID, 
                           CASE WHEN e.ID IS NOT NULL THEN e.FullName ELSE NULL END AS EmployeeFullName,
                           u.IsActive, u.MustChangePassword, u.LastLogin, u.LastPasswordChange,
                           u.FailedLoginAttempts, u.IsLocked, u.LockoutEnd
                    FROM Users u
                    LEFT JOIN Roles r ON u.RoleID = r.ID
                    LEFT JOIN Employees e ON u.EmployeeID = e.ID
                    WHERE u.Username = @Username";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Username", username)
                };

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query, parameters))
                {
                    if (!reader.Read())
                    {
                        // المستخدم غير موجود
                        return null;
                    }

                    int id = reader.GetInt32(0);
                    string storedPasswordHash = reader.IsDBNull(2) ? null : reader.GetString(2);
                    string storedPasswordSalt = reader.IsDBNull(3) ? null : reader.GetString(3);
                    bool isActive = reader.GetBoolean(10);
                    bool isLocked = reader.GetBoolean(15);
                    DateTime? lockoutEnd = reader.IsDBNull(16) ? (DateTime?)null : reader.GetDateTime(16);

                    // التحقق من حالة الحساب
                    if (!isActive)
                    {
                        // الحساب غير نشط
                        return null;
                    }

                    if (isLocked && lockoutEnd.HasValue && lockoutEnd.Value > DateTime.Now)
                    {
                        // الحساب مقفل
                        return null;
                    }

                    // التحقق من كلمة المرور
                    string saltedPassword = password + (storedPasswordSalt ?? SYSTEM_SALT);
                    string passwordHash = ComputeSHA256Hash(saltedPassword);

                    if (passwordHash != storedPasswordHash)
                    {
                        // زيادة عدد محاولات الدخول الفاشلة
                        IncrementFailedLoginAttempts(id);
                        return null;
                    }

                    // إنشاء كائن المستخدم
                    UserDTO user = new UserDTO
                    {
                        ID = id,
                        Username = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                        FullName = reader.IsDBNull(5) ? null : reader.GetString(5),
                        RoleID = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                        RoleName = reader.IsDBNull(7) ? null : reader.GetString(7),
                        EmployeeID = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                        EmployeeFullName = reader.IsDBNull(9) ? null : reader.GetString(9),
                        IsActive = isActive,
                        MustChangePassword = reader.GetBoolean(11),
                        LastLogin = reader.IsDBNull(12) ? (DateTime?)null : reader.GetDateTime(12),
                        LastPasswordChange = reader.IsDBNull(13) ? (DateTime?)null : reader.GetDateTime(13),
                        FailedLoginAttempts = reader.IsDBNull(14) ? 0 : reader.GetInt32(14),
                        IsLocked = isLocked,
                        LockoutEnd = lockoutEnd
                    };

                    // تحديث بيانات آخر تسجيل دخول
                    UpdateLastLogin(id);

                    // تسجيل عملية الدخول
                    LogLogin(id, true);

                    return user;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في التحقق من بيانات تسجيل الدخول للمستخدم {username}");
                throw;
            }
        }

        /// <summary>
        /// الحصول على كافة المستخدمين
        /// </summary>
        /// <param name="includeInactive">تضمين المستخدمين غير النشطين</param>
        /// <returns>قائمة بالمستخدمين</returns>
        public List<UserDTO> GetAllUsers(bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT u.ID, u.Username, u.Email, u.FullName, u.RoleID, r.Name AS RoleName,
                           u.EmployeeID, e.FullName AS EmployeeFullName, u.IsActive, 
                           u.MustChangePassword, u.LastLogin, u.LastPasswordChange,
                           u.FailedLoginAttempts, u.IsLocked, u.LockoutEnd
                    FROM Users u
                    LEFT JOIN Roles r ON u.RoleID = r.ID
                    LEFT JOIN Employees e ON u.EmployeeID = e.ID";

                if (!includeInactive)
                {
                    query += " WHERE u.IsActive = 1";
                }

                query += " ORDER BY u.Username";

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query))
                {
                    List<UserDTO> users = new List<UserDTO>();

                    while (reader.Read())
                    {
                        UserDTO user = new UserDTO
                        {
                            ID = reader.GetInt32(0),
                            Username = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Email = reader.IsDBNull(2) ? null : reader.GetString(2),
                            FullName = reader.IsDBNull(3) ? null : reader.GetString(3),
                            RoleID = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                            RoleName = reader.IsDBNull(5) ? null : reader.GetString(5),
                            EmployeeID = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                            EmployeeFullName = reader.IsDBNull(7) ? null : reader.GetString(7),
                            IsActive = reader.GetBoolean(8),
                            MustChangePassword = reader.GetBoolean(9),
                            LastLogin = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10),
                            LastPasswordChange = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11),
                            FailedLoginAttempts = reader.IsDBNull(12) ? 0 : reader.GetInt32(12),
                            IsLocked = reader.GetBoolean(13),
                            LockoutEnd = reader.IsDBNull(14) ? (DateTime?)null : reader.GetDateTime(14)
                        };

                        users.Add(user);
                    }

                    return users;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على كافة المستخدمين");
                throw;
            }
        }

        /// <summary>
        /// الحصول على المستخدم بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف المستخدم</param>
        /// <returns>بيانات المستخدم</returns>
        public UserDTO GetUserById(int id)
        {
            try
            {
                string query = @"
                    SELECT u.ID, u.Username, u.Email, u.FullName, u.RoleID, r.Name AS RoleName,
                           u.EmployeeID, e.FullName AS EmployeeFullName, u.IsActive, 
                           u.MustChangePassword, u.LastLogin, u.LastPasswordChange,
                           u.FailedLoginAttempts, u.IsLocked, u.LockoutEnd
                    FROM Users u
                    LEFT JOIN Roles r ON u.RoleID = r.ID
                    LEFT JOIN Employees e ON u.EmployeeID = e.ID
                    WHERE u.ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", id)
                };

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        UserDTO user = new UserDTO
                        {
                            ID = reader.GetInt32(0),
                            Username = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Email = reader.IsDBNull(2) ? null : reader.GetString(2),
                            FullName = reader.IsDBNull(3) ? null : reader.GetString(3),
                            RoleID = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                            RoleName = reader.IsDBNull(5) ? null : reader.GetString(5),
                            EmployeeID = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                            EmployeeFullName = reader.IsDBNull(7) ? null : reader.GetString(7),
                            IsActive = reader.GetBoolean(8),
                            MustChangePassword = reader.GetBoolean(9),
                            LastLogin = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10),
                            LastPasswordChange = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11),
                            FailedLoginAttempts = reader.IsDBNull(12) ? 0 : reader.GetInt32(12),
                            IsLocked = reader.GetBoolean(13),
                            LockoutEnd = reader.IsDBNull(14) ? (DateTime?)null : reader.GetDateTime(14)
                        };

                        return user;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في الحصول على المستخدم رقم {id}");
                throw;
            }
        }

        /// <summary>
        /// الحصول على المستخدم بواسطة اسم المستخدم
        /// </summary>
        /// <param name="username">اسم المستخدم</param>
        /// <returns>بيانات المستخدم</returns>
        public UserDTO GetUserByUsername(string username)
        {
            try
            {
                string query = @"
                    SELECT u.ID, u.Username, u.Email, u.FullName, u.RoleID, r.Name AS RoleName,
                           u.EmployeeID, e.FullName AS EmployeeFullName, u.IsActive, 
                           u.MustChangePassword, u.LastLogin, u.LastPasswordChange,
                           u.FailedLoginAttempts, u.IsLocked, u.LockoutEnd
                    FROM Users u
                    LEFT JOIN Roles r ON u.RoleID = r.ID
                    LEFT JOIN Employees e ON u.EmployeeID = e.ID
                    WHERE u.Username = @Username";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Username", username)
                };

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        UserDTO user = new UserDTO
                        {
                            ID = reader.GetInt32(0),
                            Username = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Email = reader.IsDBNull(2) ? null : reader.GetString(2),
                            FullName = reader.IsDBNull(3) ? null : reader.GetString(3),
                            RoleID = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                            RoleName = reader.IsDBNull(5) ? null : reader.GetString(5),
                            EmployeeID = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                            EmployeeFullName = reader.IsDBNull(7) ? null : reader.GetString(7),
                            IsActive = reader.GetBoolean(8),
                            MustChangePassword = reader.GetBoolean(9),
                            LastLogin = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10),
                            LastPasswordChange = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11),
                            FailedLoginAttempts = reader.IsDBNull(12) ? 0 : reader.GetInt32(12),
                            IsLocked = reader.GetBoolean(13),
                            LockoutEnd = reader.IsDBNull(14) ? (DateTime?)null : reader.GetDateTime(14)
                        };

                        return user;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في الحصول على المستخدم باسم {username}");
                throw;
            }
        }

        /// <summary>
        /// إنشاء مستخدم جديد
        /// </summary>
        /// <param name="user">بيانات المستخدم</param>
        /// <param name="password">كلمة المرور</param>
        /// <param name="createdBy">معرف المستخدم المنشئ</param>
        /// <returns>معرف المستخدم الجديد</returns>
        public int CreateUser(UserCreateDTO user, string password, int? createdBy)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                // التحقق من عدم وجود مستخدم بنفس اسم المستخدم
                if (IsUsernameExists(user.Username))
                {
                    throw new InvalidOperationException($"يوجد مستخدم آخر باسم {user.Username}");
                }

                // إنشاء ملح عشوائي
                string salt = GenerateRandomSalt();

                // حساب قيمة التشفير
                string saltedPassword = password + salt;
                string passwordHash = ComputeSHA256Hash(saltedPassword);

                string query = @"
                    INSERT INTO Users (
                        Username, PasswordHash, PasswordSalt, Email, FullName,
                        RoleID, EmployeeID, IsActive, MustChangePassword,
                        LastPasswordChange, CreatedAt, CreatedBy
                    )
                    VALUES (
                        @Username, @PasswordHash, @PasswordSalt, @Email, @FullName,
                        @RoleID, @EmployeeID, @IsActive, @MustChangePassword,
                        GETDATE(), GETDATE(), @CreatedBy
                    );
                    SELECT SCOPE_IDENTITY();";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Username", user.Username),
                    new SqlParameter("@PasswordHash", passwordHash),
                    new SqlParameter("@PasswordSalt", salt),
                    new SqlParameter("@Email", (object)user.Email ?? DBNull.Value),
                    new SqlParameter("@FullName", (object)user.FullName ?? DBNull.Value),
                    new SqlParameter("@RoleID", (object)user.RoleID ?? DBNull.Value),
                    new SqlParameter("@EmployeeID", (object)user.EmployeeID ?? DBNull.Value),
                    new SqlParameter("@IsActive", user.IsActive),
                    new SqlParameter("@MustChangePassword", user.MustChangePassword),
                    new SqlParameter("@CreatedBy", (object)createdBy ?? DBNull.Value)
                };

                object result = ConnectionManager.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في إنشاء مستخدم جديد");
                throw;
            }
        }

        /// <summary>
        /// تحديث بيانات المستخدم
        /// </summary>
        /// <param name="user">بيانات المستخدم</param>
        /// <param name="updatedBy">معرف المستخدم المحدث</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateUser(UserUpdateDTO user, int? updatedBy)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                string query = @"
                    UPDATE Users
                    SET Email = @Email,
                        FullName = @FullName,
                        RoleID = @RoleID,
                        EmployeeID = @EmployeeID,
                        IsActive = @IsActive,
                        MustChangePassword = @MustChangePassword,
                        UpdatedAt = GETDATE(),
                        UpdatedBy = @UpdatedBy
                    WHERE ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", user.ID),
                    new SqlParameter("@Email", (object)user.Email ?? DBNull.Value),
                    new SqlParameter("@FullName", (object)user.FullName ?? DBNull.Value),
                    new SqlParameter("@RoleID", (object)user.RoleID ?? DBNull.Value),
                    new SqlParameter("@EmployeeID", (object)user.EmployeeID ?? DBNull.Value),
                    new SqlParameter("@IsActive", user.IsActive),
                    new SqlParameter("@MustChangePassword", user.MustChangePassword),
                    new SqlParameter("@UpdatedBy", (object)updatedBy ?? DBNull.Value)
                };

                int rowsAffected = ConnectionManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في تحديث المستخدم رقم {user.ID}");
                throw;
            }
        }

        /// <summary>
        /// تغيير كلمة المرور
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        /// <param name="newPassword">كلمة المرور الجديدة</param>
        /// <returns>نجاح العملية</returns>
        public bool ChangePassword(int userId, string newPassword)
        {
            try
            {
                // إنشاء ملح عشوائي
                string salt = GenerateRandomSalt();

                // حساب قيمة التشفير
                string saltedPassword = newPassword + salt;
                string passwordHash = ComputeSHA256Hash(saltedPassword);

                string query = @"
                    UPDATE Users
                    SET PasswordHash = @PasswordHash,
                        PasswordSalt = @PasswordSalt,
                        LastPasswordChange = GETDATE(),
                        MustChangePassword = 0,
                        UpdatedAt = GETDATE()
                    WHERE ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", userId),
                    new SqlParameter("@PasswordHash", passwordHash),
                    new SqlParameter("@PasswordSalt", salt)
                };

                int rowsAffected = ConnectionManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في تغيير كلمة المرور للمستخدم رقم {userId}");
                throw;
            }
        }

        /// <summary>
        /// التحقق من صحة كلمة المرور الحالية
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        /// <param name="password">كلمة المرور</param>
        /// <returns>صحة كلمة المرور</returns>
        public bool ValidatePassword(int userId, string password)
        {
            try
            {
                string query = "SELECT PasswordHash, PasswordSalt FROM Users WHERE ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", userId)
                };

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        string storedPasswordHash = reader.IsDBNull(0) ? null : reader.GetString(0);
                        string storedPasswordSalt = reader.IsDBNull(1) ? null : reader.GetString(1);

                        // التحقق من كلمة المرور
                        string saltedPassword = password + (storedPasswordSalt ?? SYSTEM_SALT);
                        string passwordHash = ComputeSHA256Hash(saltedPassword);

                        return passwordHash == storedPasswordHash;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في التحقق من كلمة المرور للمستخدم رقم {userId}");
                throw;
            }
        }

        /// <summary>
        /// إعادة تعيين كلمة المرور
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        /// <param name="newPassword">كلمة المرور الجديدة</param>
        /// <param name="mustChangePassword">يجب تغيير كلمة المرور عند تسجيل الدخول التالي</param>
        /// <returns>نجاح العملية</returns>
        public bool ResetPassword(int userId, string newPassword, bool mustChangePassword)
        {
            try
            {
                // إنشاء ملح عشوائي
                string salt = GenerateRandomSalt();

                // حساب قيمة التشفير
                string saltedPassword = newPassword + salt;
                string passwordHash = ComputeSHA256Hash(saltedPassword);

                string query = @"
                    UPDATE Users
                    SET PasswordHash = @PasswordHash,
                        PasswordSalt = @PasswordSalt,
                        LastPasswordChange = GETDATE(),
                        MustChangePassword = @MustChangePassword,
                        UpdatedAt = GETDATE()
                    WHERE ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", userId),
                    new SqlParameter("@PasswordHash", passwordHash),
                    new SqlParameter("@PasswordSalt", salt),
                    new SqlParameter("@MustChangePassword", mustChangePassword)
                };

                int rowsAffected = ConnectionManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في إعادة تعيين كلمة المرور للمستخدم رقم {userId}");
                throw;
            }
        }

        /// <summary>
        /// حذف مستخدم
        /// </summary>
        /// <param name="id">معرف المستخدم</param>
        /// <returns>نجاح العملية</returns>
        public bool DeleteUser(int id)
        {
            try
            {
                // التحقق من عدم وجود سجلات مرتبطة بالمستخدم
                string checkQuery = @"
                    SELECT 
                        (SELECT COUNT(*) FROM LoginHistory WHERE UserID = @ID) +
                        (SELECT COUNT(*) FROM ActivityLog WHERE UserID = @ID)";

                SqlParameter[] checkParameters =
                {
                    new SqlParameter("@ID", id)
                };

                object result = ConnectionManager.ExecuteScalar(checkQuery, checkParameters);
                int count = Convert.ToInt32(result);

                if (count > 0)
                {
                    // تعطيل المستخدم بدلاً من حذفه
                    string updateQuery = "UPDATE Users SET IsActive = 0, UpdatedAt = GETDATE() WHERE ID = @ID";
                    
                    SqlParameter[] updateParameters =
                    {
                        new SqlParameter("@ID", id)
                    };

                    int updateRowsAffected = ConnectionManager.ExecuteNonQuery(updateQuery, updateParameters);
                    return updateRowsAffected > 0;
                }

                string query = "DELETE FROM Users WHERE ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", id)
                };

                int rowsAffected = ConnectionManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في حذف المستخدم رقم {id}");
                throw;
            }
        }

        /// <summary>
        /// تعطيل أو تفعيل حساب مستخدم
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        /// <param name="isActive">حالة التفعيل</param>
        /// <returns>نجاح العملية</returns>
        public bool SetUserActive(int userId, bool isActive)
        {
            try
            {
                string query = "UPDATE Users SET IsActive = @IsActive, UpdatedAt = GETDATE() WHERE ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", userId),
                    new SqlParameter("@IsActive", isActive)
                };

                int rowsAffected = ConnectionManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في {(isActive ? "تفعيل" : "تعطيل")} المستخدم رقم {userId}");
                throw;
            }
        }

        /// <summary>
        /// إلغاء قفل حساب مستخدم
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        /// <returns>نجاح العملية</returns>
        public bool UnlockUser(int userId)
        {
            try
            {
                string query = @"
                    UPDATE Users 
                    SET IsLocked = 0, 
                        LockoutEnd = NULL, 
                        FailedLoginAttempts = 0, 
                        UpdatedAt = GETDATE() 
                    WHERE ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", userId)
                };

                int rowsAffected = ConnectionManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في إلغاء قفل المستخدم رقم {userId}");
                throw;
            }
        }

        /// <summary>
        /// تحديث وقت آخر تسجيل دخول
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        public void UpdateLastLogin(int userId)
        {
            try
            {
                string query = @"
                    UPDATE Users 
                    SET LastLogin = GETDATE(), 
                        FailedLoginAttempts = 0, 
                        IsLocked = 0, 
                        LockoutEnd = NULL 
                    WHERE ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", userId)
                };

                ConnectionManager.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في تحديث وقت آخر تسجيل دخول للمستخدم رقم {userId}");
                // لا نعيد رمي الاستثناء لتجنب توقف عملية تسجيل الدخول
            }
        }

        /// <summary>
        /// زيادة عدد محاولات تسجيل الدخول الفاشلة
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        private void IncrementFailedLoginAttempts(int userId)
        {
            try
            {
                // الحصول على عدد المحاولات الفاشلة الحالي
                string getQuery = "SELECT FailedLoginAttempts FROM Users WHERE ID = @ID";

                SqlParameter[] getParameters =
                {
                    new SqlParameter("@ID", userId)
                };

                object result = ConnectionManager.ExecuteScalar(getQuery, getParameters);
                int failedAttempts = Convert.ToInt32(result) + 1;

                // تحديث عدد المحاولات الفاشلة
                string updateQuery = @"
                    UPDATE Users 
                    SET FailedLoginAttempts = @FailedAttempts,
                        IsLocked = CASE WHEN @FailedAttempts >= 5 THEN 1 ELSE 0 END,
                        LockoutEnd = CASE WHEN @FailedAttempts >= 5 THEN DATEADD(MINUTE, 30, GETDATE()) ELSE NULL END
                    WHERE ID = @ID";

                SqlParameter[] updateParameters =
                {
                    new SqlParameter("@ID", userId),
                    new SqlParameter("@FailedAttempts", failedAttempts)
                };

                ConnectionManager.ExecuteNonQuery(updateQuery, updateParameters);

                // تسجيل عملية الدخول الفاشلة
                LogLogin(userId, false);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في زيادة عدد محاولات تسجيل الدخول الفاشلة للمستخدم رقم {userId}");
                // لا نعيد رمي الاستثناء لتجنب توقف عملية تسجيل الدخول
            }
        }

        /// <summary>
        /// تسجيل عملية تسجيل الدخول
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        /// <param name="success">نجاح العملية</param>
        private void LogLogin(int userId, bool success)
        {
            try
            {
                string query = @"
                    INSERT INTO LoginHistory (UserID, LoginTime, IPAddress, LoginStatus)
                    VALUES (@UserID, GETDATE(), 'Unknown', @LoginStatus)";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@LoginStatus", success ? "Success" : "Failed")
                };

                ConnectionManager.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في تسجيل عملية تسجيل الدخول للمستخدم رقم {userId}");
                // لا نعيد رمي الاستثناء لتجنب توقف عملية تسجيل الدخول
            }
        }

        /// <summary>
        /// التحقق من وجود مستخدم باسم المستخدم المحدد
        /// </summary>
        /// <param name="username">اسم المستخدم</param>
        /// <returns>وجود المستخدم</returns>
        private bool IsUsernameExists(string username)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Username", username)
                };

                object result = ConnectionManager.ExecuteScalar(query, parameters);
                int count = Convert.ToInt32(result);

                return count > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في التحقق من وجود مستخدم باسم {username}");
                throw;
            }
        }

        /// <summary>
        /// إنشاء قيمة تشفير SHA-256
        /// </summary>
        /// <param name="input">النص المدخل</param>
        /// <returns>قيمة التشفير</returns>
        private string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                
                return builder.ToString();
            }
        }

        /// <summary>
        /// إنشاء ملح عشوائي
        /// </summary>
        /// <returns>الملح العشوائي</returns>
        private string GenerateRandomSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            
            return Convert.ToBase64String(saltBytes);
        }
    }
}