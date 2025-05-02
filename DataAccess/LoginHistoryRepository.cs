using System;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// Repository for login history operations
    /// </summary>
    public class LoginHistoryRepository
    {
        /// <summary>
        /// Records a user login
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Login history ID</returns>
        public int RecordLogin(int userId)
        {
            try
            {
                string ipAddress = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0].ToString();
                string machineName = Environment.MachineName;
                string userAgent = "HR Management System";

                string query = @"
                    INSERT INTO LoginHistory (UserID, LoginTime, IPAddress, MachineName, LoginStatus, UserAgent)
                    VALUES (@UserID, @LoginTime, @IPAddress, @MachineName, @LoginStatus, @UserAgent);
                    SELECT SCOPE_IDENTITY();";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@LoginTime", DateTime.Now),
                    new SqlParameter("@IPAddress", ipAddress),
                    new SqlParameter("@MachineName", machineName),
                    new SqlParameter("@LoginStatus", "Success"),
                    new SqlParameter("@UserAgent", userAgent)
                };

                object result = ConnectionManager.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Records a failed login attempt
        /// </summary>
        /// <param name="username">Username that was attempted</param>
        /// <returns>Login history ID</returns>
        public int RecordFailedLogin(string username)
        {
            try
            {
                string ipAddress = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0].ToString();
                string machineName = Environment.MachineName;
                string userAgent = "HR Management System";

                // Find user ID if username exists
                string userQuery = "SELECT ID FROM Users WHERE Username = @Username";
                SqlParameter[] userParams = new SqlParameter[]
                {
                    new SqlParameter("@Username", username)
                };
                object userId = ConnectionManager.ExecuteScalar(userQuery, userParams);

                string query = @"
                    INSERT INTO LoginHistory (UserID, LoginTime, IPAddress, MachineName, LoginStatus, UserAgent)
                    VALUES (@UserID, @LoginTime, @IPAddress, @MachineName, @LoginStatus, @UserAgent);
                    SELECT SCOPE_IDENTITY();";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId ?? DBNull.Value),
                    new SqlParameter("@LoginTime", DateTime.Now),
                    new SqlParameter("@IPAddress", ipAddress),
                    new SqlParameter("@MachineName", machineName),
                    new SqlParameter("@LoginStatus", "Failed"),
                    new SqlParameter("@UserAgent", userAgent)
                };

                object result = ConnectionManager.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Records a user logout
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool RecordLogout(int userId)
        {
            try
            {
                // Find the last login record that doesn't have a logout time
                string query = @"
                    UPDATE LoginHistory
                    SET LogoutTime = @LogoutTime
                    WHERE UserID = @UserID 
                      AND LogoutTime IS NULL
                      AND LoginStatus = 'Success'
                    ORDER BY LoginTime DESC";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@LogoutTime", DateTime.Now)
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
        /// Gets login history for a specific user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <returns>DataTable with login history</returns>
        public DataTable GetUserLoginHistory(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                string dateFilter = "";
                if (startDate.HasValue)
                {
                    dateFilter += " AND LoginTime >= @StartDate";
                }
                if (endDate.HasValue)
                {
                    dateFilter += " AND LoginTime <= @EndDate";
                }

                string query = $@"
                    SELECT lh.ID, lh.LoginTime, lh.LogoutTime, lh.IPAddress, 
                        lh.MachineName, lh.LoginStatus, lh.UserAgent,
                        u.Username, u.FullName
                    FROM LoginHistory lh
                    LEFT JOIN Users u ON lh.UserID = u.ID
                    WHERE lh.UserID = @UserID {dateFilter}
                    ORDER BY lh.LoginTime DESC";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId)
                };

                if (startDate.HasValue)
                {
                    Array.Resize(ref parameters, parameters.Length + 1);
                    parameters[parameters.Length - 1] = new SqlParameter("@StartDate", startDate.Value.Date);
                }

                if (endDate.HasValue)
                {
                    Array.Resize(ref parameters, parameters.Length + 1);
                    parameters[parameters.Length - 1] = new SqlParameter("@EndDate", endDate.Value.Date.AddDays(1).AddSeconds(-1));
                }

                return ConnectionManager.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Gets all login history with filters
        /// </summary>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <param name="loginStatus">Login status filter</param>
        /// <param name="userId">User ID filter</param>
        /// <returns>DataTable with login history</returns>
        public DataTable GetLoginHistory(DateTime? startDate = null, DateTime? endDate = null, 
            string loginStatus = null, int? userId = null)
        {
            try
            {
                string whereClause = "WHERE 1=1";
                
                if (startDate.HasValue)
                {
                    whereClause += " AND LoginTime >= @StartDate";
                }
                if (endDate.HasValue)
                {
                    whereClause += " AND LoginTime <= @EndDate";
                }
                if (!string.IsNullOrEmpty(loginStatus))
                {
                    whereClause += " AND LoginStatus = @LoginStatus";
                }
                if (userId.HasValue)
                {
                    whereClause += " AND UserID = @UserID";
                }

                string query = $@"
                    SELECT lh.ID, lh.LoginTime, lh.LogoutTime, lh.IPAddress, 
                        lh.MachineName, lh.LoginStatus, lh.UserAgent,
                        u.Username, u.FullName
                    FROM LoginHistory lh
                    LEFT JOIN Users u ON lh.UserID = u.ID
                    {whereClause}
                    ORDER BY lh.LoginTime DESC";

                SqlParameter[] parameters = new SqlParameter[] { };
                int paramIndex = 0;

                if (startDate.HasValue)
                {
                    Array.Resize(ref parameters, paramIndex + 1);
                    parameters[paramIndex++] = new SqlParameter("@StartDate", startDate.Value.Date);
                }

                if (endDate.HasValue)
                {
                    Array.Resize(ref parameters, paramIndex + 1);
                    parameters[paramIndex++] = new SqlParameter("@EndDate", endDate.Value.Date.AddDays(1).AddSeconds(-1));
                }

                if (!string.IsNullOrEmpty(loginStatus))
                {
                    Array.Resize(ref parameters, paramIndex + 1);
                    parameters[paramIndex++] = new SqlParameter("@LoginStatus", loginStatus);
                }

                if (userId.HasValue)
                {
                    Array.Resize(ref parameters, paramIndex + 1);
                    parameters[paramIndex++] = new SqlParameter("@UserID", userId.Value);
                }

                return ConnectionManager.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Gets active login sessions (logins without logouts)
        /// </summary>
        /// <returns>DataTable with active sessions</returns>
        public DataTable GetActiveSessions()
        {
            try
            {
                string query = @"
                    SELECT lh.ID, lh.UserID, lh.LoginTime, lh.IPAddress, 
                        lh.MachineName, lh.UserAgent,
                        u.Username, u.FullName
                    FROM LoginHistory lh
                    JOIN Users u ON lh.UserID = u.ID
                    WHERE lh.LogoutTime IS NULL
                      AND lh.LoginStatus = 'Success'
                    ORDER BY lh.LoginTime DESC";

                return ConnectionManager.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Clears old login history before a specified date
        /// </summary>
        /// <param name="beforeDate">The date before which to clear history</param>
        /// <returns>Number of rows affected</returns>
        public int ClearLoginHistory(DateTime beforeDate)
        {
            try
            {
                string query = @"
                    DELETE FROM LoginHistory
                    WHERE LoginTime < @BeforeDate";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@BeforeDate", beforeDate)
                };

                return ConnectionManager.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }
    }
}
