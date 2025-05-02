using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// Repository for user operations
    /// </summary>
    public class UserRepository
    {
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive users</param>
        /// <returns>List of UserDTO objects</returns>
        public List<UserDTO> GetAllUsers(bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT u.ID, u.Username, u.Email, u.FullName, u.RoleID, 
                           r.Name AS RoleName, u.EmployeeID, 
                           CASE WHEN e.FullName IS NOT NULL THEN e.FullName ELSE '' END AS EmployeeName,
                           u.IsActive, u.MustChangePassword, u.LastLogin, 
                           u.LastPasswordChange, u.FailedLoginAttempts, u.IsLocked, 
                           u.LockoutEnd, u.CreatedAt, u.CreatedBy, 
                           creator.Username AS CreatedByName,
                           u.UpdatedAt, u.UpdatedBy,
                           updater.Username AS UpdatedByName
                    FROM Users u
                    LEFT JOIN Roles r ON u.RoleID = r.ID
                    LEFT JOIN Employees e ON u.EmployeeID = e.ID
                    LEFT JOIN Users creator ON u.CreatedBy = creator.ID
                    LEFT JOIN Users updater ON u.UpdatedBy = updater.ID";

                if (!includeInactive)
                {
                    query += " WHERE u.IsActive = 1";
                }

                query += " ORDER BY u.Username";

                DataTable dataTable = ConnectionManager.ExecuteQuery(query);
                List<UserDTO> users = new List<UserDTO>();

                foreach (DataRow row in dataTable.Rows)
                {
                    UserDTO user = new UserDTO
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Username = row["Username"].ToString(),
                        Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                        FullName = row["FullName"].ToString(),
                        RoleID = row["RoleID"] != DBNull.Value ? Convert.ToInt32(row["RoleID"]) : (int?)null,
                        RoleName = row["RoleName"] != DBNull.Value ? row["RoleName"].ToString() : null,
                        EmployeeID = row["EmployeeID"] != DBNull.Value ? Convert.ToInt32(row["EmployeeID"]) : (int?)null,
                        EmployeeName = row["EmployeeName"] != DBNull.Value ? row["EmployeeName"].ToString() : null,
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        MustChangePassword = Convert.ToBoolean(row["MustChangePassword"]),
                        LastLogin = row["LastLogin"] != DBNull.Value ? Convert.ToDateTime(row["LastLogin"]) : (DateTime?)null,
                        LastPasswordChange = row["LastPasswordChange"] != DBNull.Value ? Convert.ToDateTime(row["LastPasswordChange"]) : (DateTime?)null,
                        FailedLoginAttempts = Convert.ToInt32(row["FailedLoginAttempts"]),
                        IsLocked = Convert.ToBoolean(row["IsLocked"]),
                        LockoutEnd = row["LockoutEnd"] != DBNull.Value ? Convert.ToDateTime(row["LockoutEnd"]) : (DateTime?)null,
                        CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : (int?)null,
                        CreatedByName = row["CreatedByName"] != DBNull.Value ? row["CreatedByName"].ToString() : null,
                        UpdatedAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : (DateTime?)null,
                        UpdatedBy = row["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(row["UpdatedBy"]) : (int?)null,
                        UpdatedByName = row["UpdatedByName"] != DBNull.Value ? row["UpdatedByName"].ToString() : null
                    };

                    users.Add(user);
                }

                return users;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<UserDTO>();
            }
        }

        /// <summary>
        /// Gets a user by ID
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>UserDTO object</returns>
        public UserDTO GetUserById(int userId)
        {
            try
            {
                string query = @"
                    SELECT u.ID, u.Username, u.PasswordHash, u.PasswordSalt, u.Email, u.FullName, u.RoleID, 
                           r.Name AS RoleName, u.EmployeeID, 
                           CASE WHEN e.FullName IS NOT NULL THEN e.FullName ELSE '' END AS EmployeeName,
                           u.IsActive, u.MustChangePassword, u.LastLogin, 
                           u.LastPasswordChange, u.FailedLoginAttempts, u.IsLocked, 
                           u.LockoutEnd, u.CreatedAt, u.CreatedBy, 
                           creator.Username AS CreatedByName,
                           u.UpdatedAt, u.UpdatedBy,
                           updater.Username AS UpdatedByName
                    FROM Users u
                    LEFT JOIN Roles r ON u.RoleID = r.ID
                    LEFT JOIN Employees e ON u.EmployeeID = e.ID
                    LEFT JOIN Users creator ON u.CreatedBy = creator.ID
                    LEFT JOIN Users updater ON u.UpdatedBy = updater.ID
                    WHERE u.ID = @UserID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId)
                };

                DataTable dataTable = ConnectionManager.ExecuteQuery(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                DataRow row = dataTable.Rows[0];

                UserDTO user = new UserDTO
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Username = row["Username"].ToString(),
                    PasswordHash = row["PasswordHash"].ToString(),
                    PasswordSalt = row["PasswordSalt"].ToString(),
                    Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                    FullName = row["FullName"].ToString(),
                    RoleID = row["RoleID"] != DBNull.Value ? Convert.ToInt32(row["RoleID"]) : (int?)null,
                    RoleName = row["RoleName"] != DBNull.Value ? row["RoleName"].ToString() : null,
                    EmployeeID = row["EmployeeID"] != DBNull.Value ? Convert.ToInt32(row["EmployeeID"]) : (int?)null,
                    EmployeeName = row["EmployeeName"] != DBNull.Value ? row["EmployeeName"].ToString() : null,
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    MustChangePassword = Convert.ToBoolean(row["MustChangePassword"]),
                    LastLogin = row["LastLogin"] != DBNull.Value ? Convert.ToDateTime(row["LastLogin"]) : (DateTime?)null,
                    LastPasswordChange = row["LastPasswordChange"] != DBNull.Value ? Convert.ToDateTime(row["LastPasswordChange"]) : (DateTime?)null,
                    FailedLoginAttempts = Convert.ToInt32(row["FailedLoginAttempts"]),
                    IsLocked = Convert.ToBoolean(row["IsLocked"]),
                    LockoutEnd = row["LockoutEnd"] != DBNull.Value ? Convert.ToDateTime(row["LockoutEnd"]) : (DateTime?)null,
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : (int?)null,
                    CreatedByName = row["CreatedByName"] != DBNull.Value ? row["CreatedByName"].ToString() : null,
                    UpdatedAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : (DateTime?)null,
                    UpdatedBy = row["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(row["UpdatedBy"]) : (int?)null,
                    UpdatedByName = row["UpdatedByName"] != DBNull.Value ? row["UpdatedByName"].ToString() : null
                };

                return user;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets a user by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>UserDTO object</returns>
        public UserDTO GetUserByUsername(string username)
        {
            try
            {
                string query = @"
                    SELECT u.ID, u.Username, u.PasswordHash, u.PasswordSalt, u.Email, u.FullName, u.RoleID, 
                           r.Name AS RoleName, u.EmployeeID, 
                           CASE WHEN e.FullName IS NOT NULL THEN e.FullName ELSE '' END AS EmployeeName,
                           u.IsActive, u.MustChangePassword, u.LastLogin, 
                           u.LastPasswordChange, u.FailedLoginAttempts, u.IsLocked, 
                           u.LockoutEnd, u.CreatedAt, u.CreatedBy, 
                           creator.Username AS CreatedByName,
                           u.UpdatedAt, u.UpdatedBy,
                           updater.Username AS UpdatedByName
                    FROM Users u
                    LEFT JOIN Roles r ON u.RoleID = r.ID
                    LEFT JOIN Employees e ON u.EmployeeID = e.ID
                    LEFT JOIN Users creator ON u.CreatedBy = creator.ID
                    LEFT JOIN Users updater ON u.UpdatedBy = updater.ID
                    WHERE u.Username = @Username";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", username)
                };

                DataTable dataTable = ConnectionManager.ExecuteQuery(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                DataRow row = dataTable.Rows[0];

                UserDTO user = new UserDTO
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Username = row["Username"].ToString(),
                    PasswordHash = row["PasswordHash"].ToString(),
                    PasswordSalt = row["PasswordSalt"].ToString(),
                    Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                    FullName = row["FullName"].ToString(),
                    RoleID = row["RoleID"] != DBNull.Value ? Convert.ToInt32(row["RoleID"]) : (int?)null,
                    RoleName = row["RoleName"] != DBNull.Value ? row["RoleName"].ToString() : null,
                    EmployeeID = row["EmployeeID"] != DBNull.Value ? Convert.ToInt32(row["EmployeeID"]) : (int?)null,
                    EmployeeName = row["EmployeeName"] != DBNull.Value ? row["EmployeeName"].ToString() : null,
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    MustChangePassword = Convert.ToBoolean(row["MustChangePassword"]),
                    LastLogin = row["LastLogin"] != DBNull.Value ? Convert.ToDateTime(row["LastLogin"]) : (DateTime?)null,
                    LastPasswordChange = row["LastPasswordChange"] != DBNull.Value ? Convert.ToDateTime(row["LastPasswordChange"]) : (DateTime?)null,
                    FailedLoginAttempts = Convert.ToInt32(row["FailedLoginAttempts"]),
                    IsLocked = Convert.ToBoolean(row["IsLocked"]),
                    LockoutEnd = row["LockoutEnd"] != DBNull.Value ? Convert.ToDateTime(row["LockoutEnd"]) : (DateTime?)null,
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : (int?)null,
                    CreatedByName = row["CreatedByName"] != DBNull.Value ? row["CreatedByName"].ToString() : null,
                    UpdatedAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : (DateTime?)null,
                    UpdatedBy = row["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(row["UpdatedBy"]) : (int?)null,
                    UpdatedByName = row["UpdatedByName"] != DBNull.Value ? row["UpdatedByName"].ToString() : null
                };

                return user;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">UserDTO object</param>
        /// <param name="password">Plain text password</param>
        /// <param name="createdByUserId">User ID who created this user</param>
        /// <returns>ID of the new user</returns>
        public int CreateUser(UserDTO user, string password, int? createdByUserId)
        {
            try
            {
                // Check if username already exists
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                SqlParameter[] checkParams = new SqlParameter[]
                {
                    new SqlParameter("@Username", user.Username)
                };
                int count = Convert.ToInt32(ConnectionManager.ExecuteScalar(checkQuery, checkParams));

                if (count > 0)
                {
                    return -1; // Username already exists
                }

                // Hash the password
                string passwordHash;
                string passwordSalt;
                passwordHash = Encryption.HashPassword(password, out passwordSalt);

                string query = @"
                    INSERT INTO Users (
                        Username, PasswordHash, PasswordSalt, Email, FullName, 
                        RoleID, EmployeeID, IsActive, MustChangePassword, 
                        LastPasswordChange, FailedLoginAttempts, IsLocked, 
                        CreatedAt, CreatedBy)
                    VALUES (
                        @Username, @PasswordHash, @PasswordSalt, @Email, @FullName, 
                        @RoleID, @EmployeeID, @IsActive, @MustChangePassword, 
                        @LastPasswordChange, @FailedLoginAttempts, @IsLocked, 
                        @CreatedAt, @CreatedBy);
                    SELECT SCOPE_IDENTITY();";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", user.Username),
                    new SqlParameter("@PasswordHash", passwordHash),
                    new SqlParameter("@PasswordSalt", passwordSalt),
                    new SqlParameter("@Email", (object)user.Email ?? DBNull.Value),
                    new SqlParameter("@FullName", user.FullName),
                    new SqlParameter("@RoleID", (object)user.RoleID ?? DBNull.Value),
                    new SqlParameter("@EmployeeID", (object)user.EmployeeID ?? DBNull.Value),
                    new SqlParameter("@IsActive", user.IsActive),
                    new SqlParameter("@MustChangePassword", user.MustChangePassword),
                    new SqlParameter("@LastPasswordChange", DateTime.Now),
                    new SqlParameter("@FailedLoginAttempts", 0),
                    new SqlParameter("@IsLocked", false),
                    new SqlParameter("@CreatedAt", DateTime.Now),
                    new SqlParameter("@CreatedBy", (object)createdByUserId ?? DBNull.Value)
                };

                object result = ConnectionManager.ExecuteScalar(query, parameters);
                int userId = Convert.ToInt32(result);

                // Log activity
                ActivityLogRepository activityRepo = new ActivityLogRepository();
                activityRepo.LogActivity(createdByUserId, "Add", "Users", 
                    $"Added new user: {user.Username}", userId, null, null);

                return userId;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Updates an existing user
        /// </summary>
        /// <param name="user">UserDTO object</param>
        /// <param name="updatedByUserId">User ID who updated this user</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool UpdateUser(UserDTO user, int? updatedByUserId)
        {
            try
            {
                // Get existing user for activity log
                UserDTO existingUser = GetUserById(user.ID);
                if (existingUser == null)
                {
                    return false;
                }

                string oldValues = Newtonsoft.Json.JsonConvert.SerializeObject(existingUser);

                string query = @"
                    UPDATE Users
                    SET Username = @Username,
                        Email = @Email,
                        FullName = @FullName,
                        RoleID = @RoleID,
                        EmployeeID = @EmployeeID,
                        IsActive = @IsActive,
                        MustChangePassword = @MustChangePassword,
                        UpdatedAt = @UpdatedAt,
                        UpdatedBy = @UpdatedBy
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", user.ID),
                    new SqlParameter("@Username", user.Username),
                    new SqlParameter("@Email", (object)user.Email ?? DBNull.Value),
                    new SqlParameter("@FullName", user.FullName),
                    new SqlParameter("@RoleID", (object)user.RoleID ?? DBNull.Value),
                    new SqlParameter("@EmployeeID", (object)user.EmployeeID ?? DBNull.Value),
                    new SqlParameter("@IsActive", user.IsActive),
                    new SqlParameter("@MustChangePassword", user.MustChangePassword),
                    new SqlParameter("@UpdatedAt", DateTime.Now),
                    new SqlParameter("@UpdatedBy", (object)updatedByUserId ?? DBNull.Value)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                // Log activity
                if (result > 0)
                {
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    string newValues = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                    activityRepo.LogActivity(updatedByUserId, "Edit", "Users", 
                        $"Updated user: {user.Username}", user.ID, oldValues, newValues);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Changes a user's password
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="newPassword">New password</param>
        /// <param name="updatedByUserId">User ID who changed the password</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool ChangePassword(int userId, string newPassword, int? updatedByUserId)
        {
            try
            {
                // Hash the new password
                string passwordHash;
                string passwordSalt;
                passwordHash = Encryption.HashPassword(newPassword, out passwordSalt);

                string query = @"
                    UPDATE Users
                    SET PasswordHash = @PasswordHash,
                        PasswordSalt = @PasswordSalt,
                        LastPasswordChange = @LastPasswordChange,
                        MustChangePassword = 0,
                        UpdatedAt = @UpdatedAt,
                        UpdatedBy = @UpdatedBy
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", userId),
                    new SqlParameter("@PasswordHash", passwordHash),
                    new SqlParameter("@PasswordSalt", passwordSalt),
                    new SqlParameter("@LastPasswordChange", DateTime.Now),
                    new SqlParameter("@UpdatedAt", DateTime.Now),
                    new SqlParameter("@UpdatedBy", (object)updatedByUserId ?? DBNull.Value)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                // Log activity
                if (result > 0)
                {
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    activityRepo.LogActivity(updatedByUserId, "Security", "Users", 
                        $"Changed password for user ID: {userId}", userId, null, null);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Resets a user's password
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="newPassword">New password</param>
        /// <param name="updatedByUserId">User ID who reset the password</param>
        /// <param name="mustChangePassword">Whether the user must change password at next login</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool ResetPassword(int userId, string newPassword, int? updatedByUserId, bool mustChangePassword = true)
        {
            try
            {
                // Hash the new password
                string passwordHash;
                string passwordSalt;
                passwordHash = Encryption.HashPassword(newPassword, out passwordSalt);

                string query = @"
                    UPDATE Users
                    SET PasswordHash = @PasswordHash,
                        PasswordSalt = @PasswordSalt,
                        LastPasswordChange = @LastPasswordChange,
                        MustChangePassword = @MustChangePassword,
                        UpdatedAt = @UpdatedAt,
                        UpdatedBy = @UpdatedBy
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", userId),
                    new SqlParameter("@PasswordHash", passwordHash),
                    new SqlParameter("@PasswordSalt", passwordSalt),
                    new SqlParameter("@LastPasswordChange", DateTime.Now),
                    new SqlParameter("@MustChangePassword", mustChangePassword),
                    new SqlParameter("@UpdatedAt", DateTime.Now),
                    new SqlParameter("@UpdatedBy", (object)updatedByUserId ?? DBNull.Value)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                // Log activity
                if (result > 0)
                {
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    activityRepo.LogActivity(updatedByUserId, "Security", "Users", 
                        $"Reset password for user ID: {userId}", userId, null, null);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Locks a user account
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="lockoutEnd">When the lockout should end</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool LockUserAccount(int userId, DateTime lockoutEnd)
        {
            try
            {
                string query = @"
                    UPDATE Users
                    SET IsLocked = 1,
                        LockoutEnd = @LockoutEnd,
                        UpdatedAt = @UpdatedAt
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", userId),
                    new SqlParameter("@LockoutEnd", lockoutEnd),
                    new SqlParameter("@UpdatedAt", DateTime.Now)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                // Log activity
                if (result > 0)
                {
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    activityRepo.LogActivity(null, "Security", "Users", 
                        $"Locked user account ID: {userId} until {lockoutEnd}", userId, null, null);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Unlocks a user account
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="updatedByUserId">User ID who unlocked the account</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool UnlockUserAccount(int userId, int? updatedByUserId)
        {
            try
            {
                string query = @"
                    UPDATE Users
                    SET IsLocked = 0,
                        LockoutEnd = NULL,
                        FailedLoginAttempts = 0,
                        UpdatedAt = @UpdatedAt,
                        UpdatedBy = @UpdatedBy
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", userId),
                    new SqlParameter("@UpdatedAt", DateTime.Now),
                    new SqlParameter("@UpdatedBy", (object)updatedByUserId ?? DBNull.Value)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                // Log activity
                if (result > 0)
                {
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    activityRepo.LogActivity(updatedByUserId, "Security", "Users", 
                        $"Unlocked user account ID: {userId}", userId, null, null);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Increments the failed login attempts for a user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool IncrementFailedLoginAttempts(int userId)
        {
            try
            {
                string query = @"
                    UPDATE Users
                    SET FailedLoginAttempts = FailedLoginAttempts + 1,
                        UpdatedAt = @UpdatedAt
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", userId),
                    new SqlParameter("@UpdatedAt", DateTime.Now)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Resets the failed login attempts for a user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool ResetFailedLoginAttempts(int userId)
        {
            try
            {
                string query = @"
                    UPDATE Users
                    SET FailedLoginAttempts = 0,
                        UpdatedAt = @UpdatedAt
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", userId),
                    new SqlParameter("@UpdatedAt", DateTime.Now)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Updates the last login time for a user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool UpdateLastLogin(int userId)
        {
            try
            {
                string query = @"
                    UPDATE Users
                    SET LastLogin = @LastLogin,
                        UpdatedAt = @UpdatedAt
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", userId),
                    new SqlParameter("@LastLogin", DateTime.Now),
                    new SqlParameter("@UpdatedAt", DateTime.Now)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="userId">User ID to delete</param>
        /// <param name="deletedByUserId">User ID who deleted the user</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool DeleteUser(int userId, int deletedByUserId)
        {
            try
            {
                // Get existing user for activity log
                UserDTO existingUser = GetUserById(userId);
                if (existingUser == null)
                {
                    return false;
                }

                string oldValues = Newtonsoft.Json.JsonConvert.SerializeObject(existingUser);

                // Check if this is the last admin user
                string checkQuery = @"
                    SELECT COUNT(*) 
                    FROM Users u
                    JOIN Roles r ON u.RoleID = r.ID
                    JOIN RolePermissions rp ON r.ID = rp.RoleID
                    WHERE u.ID <> @UserID
                      AND u.IsActive = 1
                      AND rp.ModuleName = 'Users'
                      AND rp.CanAdd = 1
                      AND rp.CanEdit = 1
                      AND rp.CanDelete = 1";

                SqlParameter[] checkParams = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId)
                };

                int adminCount = Convert.ToInt32(ConnectionManager.ExecuteScalar(checkQuery, checkParams));

                if (adminCount == 0)
                {
                    // This is the last admin user, don't allow deletion
                    return false;
                }

                // Delete the user
                string query = "DELETE FROM Users WHERE ID = @UserID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                // Log activity
                if (result > 0)
                {
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    activityRepo.LogActivity(deletedByUserId, "Delete", "Users", 
                        $"Deleted user: {existingUser.Username}", userId, oldValues, null);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Gets users for a dropdown list
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive users</param>
        /// <returns>DataTable with user ID and name</returns>
        public DataTable GetUsersForDropDown(bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT ID, Username + ' (' + FullName + ')' AS Name
                    FROM Users";

                if (!includeInactive)
                {
                    query += " WHERE IsActive = 1";
                }

                query += " ORDER BY Username";

                return ConnectionManager.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Gets the count of users
        /// </summary>
        /// <returns>Number of users</returns>
        public int GetUsersCount()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Users";
                return Convert.ToInt32(ConnectionManager.ExecuteScalar(query));
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }
    }
}
