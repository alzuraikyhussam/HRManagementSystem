using System;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// Repository for activity log operations
    /// </summary>
    public class ActivityLogRepository
    {
        /// <summary>
        /// Logs an activity
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="activityType">Activity type</param>
        /// <param name="moduleName">Module name</param>
        /// <param name="description">Description</param>
        /// <param name="recordId">Record ID</param>
        /// <param name="oldValues">Old values</param>
        /// <param name="newValues">New values</param>
        /// <returns>Activity log ID</returns>
        public int LogActivity(int? userId, string activityType, string moduleName, string description, 
            int? recordId = null, string oldValues = null, string newValues = null)
        {
            try
            {
                string ipAddress = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0].ToString();
                
                string query = @"
                    INSERT INTO ActivityLog (UserID, ActivityDate, ActivityType, ModuleName, Description, 
                        IPAddress, RecordID, OldValues, NewValues)
                    VALUES (@UserID, @ActivityDate, @ActivityType, @ModuleName, @Description, 
                        @IPAddress, @RecordID, @OldValues, @NewValues);
                    SELECT SCOPE_IDENTITY();";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", (object)userId ?? DBNull.Value),
                    new SqlParameter("@ActivityDate", DateTime.Now),
                    new SqlParameter("@ActivityType", activityType),
                    new SqlParameter("@ModuleName", moduleName),
                    new SqlParameter("@Description", description),
                    new SqlParameter("@IPAddress", ipAddress),
                    new SqlParameter("@RecordID", (object)recordId ?? DBNull.Value),
                    new SqlParameter("@OldValues", (object)oldValues ?? DBNull.Value),
                    new SqlParameter("@NewValues", (object)newValues ?? DBNull.Value)
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
        /// Gets activity logs for a specific user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <returns>DataTable with activity logs</returns>
        public DataTable GetUserActivities(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                string dateFilter = "";
                if (startDate.HasValue)
                {
                    dateFilter += " AND ActivityDate >= @StartDate";
                }
                if (endDate.HasValue)
                {
                    dateFilter += " AND ActivityDate <= @EndDate";
                }

                string query = $@"
                    SELECT a.ID, a.ActivityDate, a.ActivityType, a.ModuleName, a.Description, 
                        a.IPAddress, a.RecordID, a.OldValues, a.NewValues,
                        u.Username, u.FullName
                    FROM ActivityLog a
                    LEFT JOIN Users u ON a.UserID = u.ID
                    WHERE a.UserID = @UserID {dateFilter}
                    ORDER BY a.ActivityDate DESC";

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
        /// Gets all activity logs with filters
        /// </summary>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <param name="activityType">Activity type filter</param>
        /// <param name="moduleName">Module name filter</param>
        /// <param name="userId">User ID filter</param>
        /// <returns>DataTable with activity logs</returns>
        public DataTable GetActivityLogs(DateTime? startDate = null, DateTime? endDate = null, 
            string activityType = null, string moduleName = null, int? userId = null)
        {
            try
            {
                string whereClause = "WHERE 1=1";
                
                if (startDate.HasValue)
                {
                    whereClause += " AND ActivityDate >= @StartDate";
                }
                if (endDate.HasValue)
                {
                    whereClause += " AND ActivityDate <= @EndDate";
                }
                if (!string.IsNullOrEmpty(activityType))
                {
                    whereClause += " AND ActivityType = @ActivityType";
                }
                if (!string.IsNullOrEmpty(moduleName))
                {
                    whereClause += " AND ModuleName = @ModuleName";
                }
                if (userId.HasValue)
                {
                    whereClause += " AND UserID = @UserID";
                }

                string query = $@"
                    SELECT a.ID, a.ActivityDate, a.ActivityType, a.ModuleName, a.Description, 
                        a.IPAddress, a.RecordID, 
                        u.Username, u.FullName
                    FROM ActivityLog a
                    LEFT JOIN Users u ON a.UserID = u.ID
                    {whereClause}
                    ORDER BY a.ActivityDate DESC";

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

                if (!string.IsNullOrEmpty(activityType))
                {
                    Array.Resize(ref parameters, paramIndex + 1);
                    parameters[paramIndex++] = new SqlParameter("@ActivityType", activityType);
                }

                if (!string.IsNullOrEmpty(moduleName))
                {
                    Array.Resize(ref parameters, paramIndex + 1);
                    parameters[paramIndex++] = new SqlParameter("@ModuleName", moduleName);
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
        /// Gets the list of unique activity types
        /// </summary>
        /// <returns>DataTable with activity types</returns>
        public DataTable GetActivityTypes()
        {
            try
            {
                string query = @"
                    SELECT DISTINCT ActivityType
                    FROM ActivityLog
                    ORDER BY ActivityType";

                return ConnectionManager.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Clears old activity logs before a specified date
        /// </summary>
        /// <param name="beforeDate">The date before which to clear logs</param>
        /// <returns>Number of rows affected</returns>
        public int ClearActivityLogs(DateTime beforeDate)
        {
            try
            {
                string query = @"
                    DELETE FROM ActivityLog
                    WHERE ActivityDate < @BeforeDate";

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
