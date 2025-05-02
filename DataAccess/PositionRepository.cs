using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// Repository for position operations
    /// </summary>
    public class PositionRepository
    {
        /// <summary>
        /// Gets all positions
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive positions</param>
        /// <returns>List of PositionDTO objects</returns>
        public List<PositionDTO> GetAllPositions(bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT p.ID, p.Title, p.Description, p.DepartmentID, 
                           d.Name AS DepartmentName, p.GradeLevel, 
                           p.MinSalary, p.MaxSalary, p.IsManagerPosition, 
                           p.IsActive, p.CreatedAt, p.UpdatedAt
                    FROM Positions p
                    LEFT JOIN Departments d ON p.DepartmentID = d.ID";

                if (!includeInactive)
                {
                    query += " WHERE p.IsActive = 1";
                }

                query += " ORDER BY d.Name, p.Title";

                DataTable dataTable = ConnectionManager.ExecuteQuery(query);
                List<PositionDTO> positions = new List<PositionDTO>();

                foreach (DataRow row in dataTable.Rows)
                {
                    PositionDTO position = new PositionDTO
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Title = row["Title"].ToString(),
                        Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                        DepartmentID = row["DepartmentID"] != DBNull.Value ? Convert.ToInt32(row["DepartmentID"]) : (int?)null,
                        DepartmentName = row["DepartmentName"] != DBNull.Value ? row["DepartmentName"].ToString() : null,
                        GradeLevel = row["GradeLevel"] != DBNull.Value ? Convert.ToInt32(row["GradeLevel"]) : (int?)null,
                        MinSalary = row["MinSalary"] != DBNull.Value ? Convert.ToDecimal(row["MinSalary"]) : (decimal?)null,
                        MaxSalary = row["MaxSalary"] != DBNull.Value ? Convert.ToDecimal(row["MaxSalary"]) : (decimal?)null,
                        IsManagerPosition = Convert.ToBoolean(row["IsManagerPosition"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                        UpdatedAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : (DateTime?)null
                    };

                    positions.Add(position);
                }

                return positions;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<PositionDTO>();
            }
        }

        /// <summary>
        /// Gets positions for a specific department
        /// </summary>
        /// <param name="departmentId">Department ID</param>
        /// <param name="includeInactive">Whether to include inactive positions</param>
        /// <returns>List of PositionDTO objects</returns>
        public List<PositionDTO> GetPositionsByDepartment(int departmentId, bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT p.ID, p.Title, p.Description, p.DepartmentID, 
                           d.Name AS DepartmentName, p.GradeLevel, 
                           p.MinSalary, p.MaxSalary, p.IsManagerPosition, 
                           p.IsActive, p.CreatedAt, p.UpdatedAt
                    FROM Positions p
                    LEFT JOIN Departments d ON p.DepartmentID = d.ID
                    WHERE p.DepartmentID = @DepartmentID";

                if (!includeInactive)
                {
                    query += " AND p.IsActive = 1";
                }

                query += " ORDER BY p.Title";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentID", departmentId)
                };

                DataTable dataTable = ConnectionManager.ExecuteQuery(query, parameters);
                List<PositionDTO> positions = new List<PositionDTO>();

                foreach (DataRow row in dataTable.Rows)
                {
                    PositionDTO position = new PositionDTO
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Title = row["Title"].ToString(),
                        Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                        DepartmentID = row["DepartmentID"] != DBNull.Value ? Convert.ToInt32(row["DepartmentID"]) : (int?)null,
                        DepartmentName = row["DepartmentName"] != DBNull.Value ? row["DepartmentName"].ToString() : null,
                        GradeLevel = row["GradeLevel"] != DBNull.Value ? Convert.ToInt32(row["GradeLevel"]) : (int?)null,
                        MinSalary = row["MinSalary"] != DBNull.Value ? Convert.ToDecimal(row["MinSalary"]) : (decimal?)null,
                        MaxSalary = row["MaxSalary"] != DBNull.Value ? Convert.ToDecimal(row["MaxSalary"]) : (decimal?)null,
                        IsManagerPosition = Convert.ToBoolean(row["IsManagerPosition"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                        UpdatedAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : (DateTime?)null
                    };

                    positions.Add(position);
                }

                return positions;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<PositionDTO>();
            }
        }

        /// <summary>
        /// Gets managerial positions
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive positions</param>
        /// <returns>List of PositionDTO objects</returns>
        public List<PositionDTO> GetManagerPositions(bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT p.ID, p.Title, p.Description, p.DepartmentID, 
                           d.Name AS DepartmentName, p.GradeLevel, 
                           p.MinSalary, p.MaxSalary, p.IsManagerPosition, 
                           p.IsActive, p.CreatedAt, p.UpdatedAt
                    FROM Positions p
                    LEFT JOIN Departments d ON p.DepartmentID = d.ID
                    WHERE p.IsManagerPosition = 1";

                if (!includeInactive)
                {
                    query += " AND p.IsActive = 1";
                }

                query += " ORDER BY d.Name, p.Title";

                DataTable dataTable = ConnectionManager.ExecuteQuery(query);
                List<PositionDTO> positions = new List<PositionDTO>();

                foreach (DataRow row in dataTable.Rows)
                {
                    PositionDTO position = new PositionDTO
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Title = row["Title"].ToString(),
                        Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                        DepartmentID = row["DepartmentID"] != DBNull.Value ? Convert.ToInt32(row["DepartmentID"]) : (int?)null,
                        DepartmentName = row["DepartmentName"] != DBNull.Value ? row["DepartmentName"].ToString() : null,
                        GradeLevel = row["GradeLevel"] != DBNull.Value ? Convert.ToInt32(row["GradeLevel"]) : (int?)null,
                        MinSalary = row["MinSalary"] != DBNull.Value ? Convert.ToDecimal(row["MinSalary"]) : (decimal?)null,
                        MaxSalary = row["MaxSalary"] != DBNull.Value ? Convert.ToDecimal(row["MaxSalary"]) : (decimal?)null,
                        IsManagerPosition = Convert.ToBoolean(row["IsManagerPosition"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                        UpdatedAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : (DateTime?)null
                    };

                    positions.Add(position);
                }

                return positions;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<PositionDTO>();
            }
        }

        /// <summary>
        /// Gets a position by ID
        /// </summary>
        /// <param name="positionId">Position ID</param>
        /// <returns>PositionDTO object</returns>
        public PositionDTO GetPositionById(int positionId)
        {
            try
            {
                string query = @"
                    SELECT p.ID, p.Title, p.Description, p.DepartmentID, 
                           d.Name AS DepartmentName, p.GradeLevel, 
                           p.MinSalary, p.MaxSalary, p.IsManagerPosition, 
                           p.IsActive, p.CreatedAt, p.UpdatedAt
                    FROM Positions p
                    LEFT JOIN Departments d ON p.DepartmentID = d.ID
                    WHERE p.ID = @PositionID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@PositionID", positionId)
                };

                DataTable dataTable = ConnectionManager.ExecuteQuery(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                DataRow row = dataTable.Rows[0];

                PositionDTO position = new PositionDTO
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Title = row["Title"].ToString(),
                    Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                    DepartmentID = row["DepartmentID"] != DBNull.Value ? Convert.ToInt32(row["DepartmentID"]) : (int?)null,
                    DepartmentName = row["DepartmentName"] != DBNull.Value ? row["DepartmentName"].ToString() : null,
                    GradeLevel = row["GradeLevel"] != DBNull.Value ? Convert.ToInt32(row["GradeLevel"]) : (int?)null,
                    MinSalary = row["MinSalary"] != DBNull.Value ? Convert.ToDecimal(row["MinSalary"]) : (decimal?)null,
                    MaxSalary = row["MaxSalary"] != DBNull.Value ? Convert.ToDecimal(row["MaxSalary"]) : (decimal?)null,
                    IsManagerPosition = Convert.ToBoolean(row["IsManagerPosition"]),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    UpdatedAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : (DateTime?)null
                };

                return position;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Saves a position (inserts if ID is 0, updates otherwise)
        /// </summary>
        /// <param name="position">PositionDTO object</param>
        /// <param name="userId">User ID performing the operation</param>
        /// <returns>ID of the saved position</returns>
        public int SavePosition(PositionDTO position, int userId)
        {
            try
            {
                string query;
                SqlParameter[] parameters;

                if (position.ID == 0)
                {
                    // Insert new position
                    query = @"
                        INSERT INTO Positions (
                            Title, Description, DepartmentID, GradeLevel, 
                            MinSalary, MaxSalary, IsManagerPosition, IsActive, CreatedAt)
                        VALUES (
                            @Title, @Description, @DepartmentID, @GradeLevel, 
                            @MinSalary, @MaxSalary, @IsManagerPosition, @IsActive, @CreatedAt);
                        SELECT SCOPE_IDENTITY();";

                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Title", position.Title),
                        new SqlParameter("@Description", (object)position.Description ?? DBNull.Value),
                        new SqlParameter("@DepartmentID", (object)position.DepartmentID ?? DBNull.Value),
                        new SqlParameter("@GradeLevel", (object)position.GradeLevel ?? DBNull.Value),
                        new SqlParameter("@MinSalary", (object)position.MinSalary ?? DBNull.Value),
                        new SqlParameter("@MaxSalary", (object)position.MaxSalary ?? DBNull.Value),
                        new SqlParameter("@IsManagerPosition", position.IsManagerPosition),
                        new SqlParameter("@IsActive", position.IsActive),
                        new SqlParameter("@CreatedAt", DateTime.Now)
                    };

                    object result = ConnectionManager.ExecuteScalar(query, parameters);
                    position.ID = Convert.ToInt32(result);

                    // Log activity
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    activityRepo.LogActivity(userId, "Add", "Positions", 
                        $"Added new position: {position.Title}", position.ID, null, null);

                    return position.ID;
                }
                else
                {
                    // Get existing position for activity log
                    PositionDTO existingPosition = GetPositionById(position.ID);
                    string oldValues = Newtonsoft.Json.JsonConvert.SerializeObject(existingPosition);

                    // Update existing position
                    query = @"
                        UPDATE Positions
                        SET Title = @Title,
                            Description = @Description,
                            DepartmentID = @DepartmentID,
                            GradeLevel = @GradeLevel,
                            MinSalary = @MinSalary,
                            MaxSalary = @MaxSalary,
                            IsManagerPosition = @IsManagerPosition,
                            IsActive = @IsActive,
                            UpdatedAt = @UpdatedAt
                        WHERE ID = @ID";

                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@ID", position.ID),
                        new SqlParameter("@Title", position.Title),
                        new SqlParameter("@Description", (object)position.Description ?? DBNull.Value),
                        new SqlParameter("@DepartmentID", (object)position.DepartmentID ?? DBNull.Value),
                        new SqlParameter("@GradeLevel", (object)position.GradeLevel ?? DBNull.Value),
                        new SqlParameter("@MinSalary", (object)position.MinSalary ?? DBNull.Value),
                        new SqlParameter("@MaxSalary", (object)position.MaxSalary ?? DBNull.Value),
                        new SqlParameter("@IsManagerPosition", position.IsManagerPosition),
                        new SqlParameter("@IsActive", position.IsActive),
                        new SqlParameter("@UpdatedAt", DateTime.Now)
                    };

                    int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                    // Log activity
                    if (result > 0)
                    {
                        ActivityLogRepository activityRepo = new ActivityLogRepository();
                        string newValues = Newtonsoft.Json.JsonConvert.SerializeObject(position);
                        activityRepo.LogActivity(userId, "Edit", "Positions", 
                            $"Updated position: {position.Title}", position.ID, oldValues, newValues);
                    }

                    return position.ID;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Deletes a position
        /// </summary>
        /// <param name="positionId">Position ID to delete</param>
        /// <param name="userId">User ID performing the operation</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool DeletePosition(int positionId, int userId)
        {
            try
            {
                // Check for employees in the position
                string checkEmployeesQuery = "SELECT COUNT(*) FROM Employees WHERE PositionID = @PositionID";
                SqlParameter[] checkParams = new SqlParameter[]
                {
                    new SqlParameter("@PositionID", positionId)
                };
                int employeeCount = Convert.ToInt32(ConnectionManager.ExecuteScalar(checkEmployeesQuery, checkParams));

                if (employeeCount > 0)
                {
                    return false; // Can't delete position with employees
                }

                // Check if position is used as a manager position for any department
                string checkDepartmentsQuery = "SELECT COUNT(*) FROM Departments WHERE ManagerPositionID = @PositionID";
                SqlParameter[] checkDeptParams = new SqlParameter[]
                {
                    new SqlParameter("@PositionID", positionId)
                };
                int deptCount = Convert.ToInt32(ConnectionManager.ExecuteScalar(checkDepartmentsQuery, checkDeptParams));

                if (deptCount > 0)
                {
                    return false; // Can't delete position if it's used as a manager position
                }

                // Get position details for activity log
                PositionDTO position = GetPositionById(positionId);
                if (position == null)
                {
                    return false;
                }

                // Delete the position
                string query = "DELETE FROM Positions WHERE ID = @PositionID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@PositionID", positionId)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                // Log activity
                if (result > 0)
                {
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    string oldValues = Newtonsoft.Json.JsonConvert.SerializeObject(position);
                    activityRepo.LogActivity(userId, "Delete", "Positions", 
                        $"Deleted position: {position.Title}", positionId, oldValues, null);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Gets positions for a dropdown list
        /// </summary>
        /// <param name="departmentId">Optional department ID filter</param>
        /// <param name="includeInactive">Whether to include inactive positions</param>
        /// <returns>DataTable with position ID and title</returns>
        public DataTable GetPositionsForDropDown(int? departmentId = null, bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT p.ID, 
                           CASE WHEN d.Name IS NOT NULL THEN d.Name + ' - ' + p.Title ELSE p.Title END AS Title
                    FROM Positions p
                    LEFT JOIN Departments d ON p.DepartmentID = d.ID
                    WHERE 1=1";

                if (departmentId.HasValue)
                {
                    query += " AND p.DepartmentID = @DepartmentID";
                }

                if (!includeInactive)
                {
                    query += " AND p.IsActive = 1";
                }

                query += " ORDER BY d.Name, p.Title";

                SqlParameter[] parameters = new SqlParameter[] { };
                if (departmentId.HasValue)
                {
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@DepartmentID", departmentId.Value)
                    };
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
        /// Gets manager positions for a dropdown list
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive positions</param>
        /// <returns>DataTable with manager position ID and title</returns>
        public DataTable GetManagerPositionsForDropDown(bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT p.ID, 
                           CASE WHEN d.Name IS NOT NULL THEN d.Name + ' - ' + p.Title ELSE p.Title END AS Title
                    FROM Positions p
                    LEFT JOIN Departments d ON p.DepartmentID = d.ID
                    WHERE p.IsManagerPosition = 1";

                if (!includeInactive)
                {
                    query += " AND p.IsActive = 1";
                }

                query += " ORDER BY d.Name, p.Title";

                return ConnectionManager.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Gets positions count
        /// </summary>
        /// <returns>Number of positions</returns>
        public int GetPositionsCount()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Positions";
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
