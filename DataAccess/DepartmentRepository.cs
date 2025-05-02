using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// Repository for department operations
    /// </summary>
    public class DepartmentRepository
    {
        /// <summary>
        /// Gets all departments
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive departments</param>
        /// <returns>List of departments</returns>
        public List<DepartmentDTO> GetAllDepartments(bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT d.ID, d.Name, d.Description, d.ParentID, pd.Name AS ParentName, 
                           d.ManagerPositionID, p.Title AS ManagerPositionTitle, d.Location, d.IsActive
                    FROM Departments d
                    LEFT JOIN Departments pd ON d.ParentID = pd.ID
                    LEFT JOIN Positions p ON d.ManagerPositionID = p.ID";

                if (!includeInactive)
                {
                    query += " WHERE d.IsActive = 1";
                }

                query += " ORDER BY ISNULL(d.ParentID, 0), d.Name";

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query))
                {
                    List<DepartmentDTO> departments = new List<DepartmentDTO>();

                    while (reader.Read())
                    {
                        DepartmentDTO department = new DepartmentDTO
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            ParentID = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                            ParentName = reader.IsDBNull(4) ? null : reader.GetString(4),
                            ManagerPositionID = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                            ManagerPositionTitle = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Location = reader.IsDBNull(7) ? null : reader.GetString(7),
                            IsActive = reader.GetBoolean(8),
                            ChildDepartments = new List<DepartmentDTO>()
                        };

                        departments.Add(department);
                    }

                    // Build department hierarchy
                    BuildDepartmentHierarchy(departments);

                    return departments;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to get all departments");
                throw;
            }
        }

        /// <summary>
        /// Gets departments for a tree view
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive departments</param>
        /// <returns>List of department tree nodes</returns>
        public List<DepartmentTreeNodeDTO> GetDepartmentsForTree(bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT d.ID, d.Name, d.ParentID, d.IsActive,
                           CASE WHEN EXISTS (SELECT 1 FROM Departments WHERE ParentID = d.ID) THEN 1 ELSE 0 END AS HasChildren
                    FROM Departments d";

                if (!includeInactive)
                {
                    query += " WHERE d.IsActive = 1";
                }

                query += " ORDER BY ISNULL(d.ParentID, 0), d.Name";

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query))
                {
                    List<DepartmentTreeNodeDTO> departments = new List<DepartmentTreeNodeDTO>();

                    while (reader.Read())
                    {
                        DepartmentTreeNodeDTO department = new DepartmentTreeNodeDTO
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            ParentID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                            IsActive = reader.GetBoolean(3),
                            HasChildren = reader.GetBoolean(4),
                            Children = new List<DepartmentTreeNodeDTO>()
                        };

                        departments.Add(department);
                    }

                    // Build department hierarchy
                    BuildDepartmentTreeHierarchy(departments);

                    return departments;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to get departments for tree");
                throw;
            }
        }

        /// <summary>
        /// Gets a department by ID
        /// </summary>
        /// <param name="id">Department ID</param>
        /// <returns>Department information or null if not found</returns>
        public DepartmentDTO GetDepartmentById(int id)
        {
            try
            {
                string query = @"
                    SELECT d.ID, d.Name, d.Description, d.ParentID, pd.Name AS ParentName, 
                           d.ManagerPositionID, p.Title AS ManagerPositionTitle, d.Location, d.IsActive
                    FROM Departments d
                    LEFT JOIN Departments pd ON d.ParentID = pd.ID
                    LEFT JOIN Positions p ON d.ManagerPositionID = p.ID
                    WHERE d.ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", id)
                };

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        DepartmentDTO department = new DepartmentDTO
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            ParentID = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                            ParentName = reader.IsDBNull(4) ? null : reader.GetString(4),
                            ManagerPositionID = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                            ManagerPositionTitle = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Location = reader.IsDBNull(7) ? null : reader.GetString(7),
                            IsActive = reader.GetBoolean(8),
                            ChildDepartments = new List<DepartmentDTO>()
                        };

                        return department;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"Failed to get department with ID {id}");
                throw;
            }
        }

        /// <summary>
        /// Gets child departments for a parent department
        /// </summary>
        /// <param name="parentId">Parent department ID or null for root departments</param>
        /// <param name="includeInactive">Whether to include inactive departments</param>
        /// <returns>List of child departments</returns>
        public List<DepartmentDTO> GetChildDepartments(int? parentId, bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT d.ID, d.Name, d.Description, d.ParentID, pd.Name AS ParentName, 
                           d.ManagerPositionID, p.Title AS ManagerPositionTitle, d.Location, d.IsActive
                    FROM Departments d
                    LEFT JOIN Departments pd ON d.ParentID = pd.ID
                    LEFT JOIN Positions p ON d.ManagerPositionID = p.ID
                    WHERE ";

                if (parentId.HasValue)
                {
                    query += "d.ParentID = @ParentID";
                }
                else
                {
                    query += "d.ParentID IS NULL";
                }

                if (!includeInactive)
                {
                    query += " AND d.IsActive = 1";
                }

                query += " ORDER BY d.Name";

                SqlParameter[] parameters = parentId.HasValue
                    ? new[] { new SqlParameter("@ParentID", parentId.Value) }
                    : Array.Empty<SqlParameter>();

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query, parameters))
                {
                    List<DepartmentDTO> departments = new List<DepartmentDTO>();

                    while (reader.Read())
                    {
                        DepartmentDTO department = new DepartmentDTO
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            ParentID = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                            ParentName = reader.IsDBNull(4) ? null : reader.GetString(4),
                            ManagerPositionID = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                            ManagerPositionTitle = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Location = reader.IsDBNull(7) ? null : reader.GetString(7),
                            IsActive = reader.GetBoolean(8),
                            ChildDepartments = new List<DepartmentDTO>()
                        };

                        departments.Add(department);
                    }

                    return departments;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"Failed to get child departments for parent ID {parentId}");
                throw;
            }
        }

        /// <summary>
        /// Creates a new department
        /// </summary>
        /// <param name="department">Department information</param>
        /// <returns>ID of the created department</returns>
        public int CreateDepartment(DepartmentDTO department)
        {
            if (department == null)
            {
                throw new ArgumentNullException(nameof(department));
            }

            try
            {
                string query = @"
                    INSERT INTO Departments (Name, Description, ParentID, ManagerPositionID, Location, IsActive, CreatedAt)
                    VALUES (@Name, @Description, @ParentID, @ManagerPositionID, @Location, @IsActive, GETDATE());
                    SELECT SCOPE_IDENTITY();";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Name", (object)department.Name ?? DBNull.Value),
                    new SqlParameter("@Description", (object)department.Description ?? DBNull.Value),
                    new SqlParameter("@ParentID", (object)department.ParentID ?? DBNull.Value),
                    new SqlParameter("@ManagerPositionID", (object)department.ManagerPositionID ?? DBNull.Value),
                    new SqlParameter("@Location", (object)department.Location ?? DBNull.Value),
                    new SqlParameter("@IsActive", department.IsActive)
                };

                object result = ConnectionManager.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to create department");
                throw;
            }
        }

        /// <summary>
        /// Updates a department
        /// </summary>
        /// <param name="department">Department information</param>
        /// <returns>True if the operation was successful</returns>
        public bool UpdateDepartment(DepartmentDTO department)
        {
            if (department == null)
            {
                throw new ArgumentNullException(nameof(department));
            }

            try
            {
                string query = @"
                    UPDATE Departments
                    SET Name = @Name,
                        Description = @Description,
                        ParentID = @ParentID,
                        ManagerPositionID = @ManagerPositionID,
                        Location = @Location,
                        IsActive = @IsActive,
                        UpdatedAt = GETDATE()
                    WHERE ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", department.ID),
                    new SqlParameter("@Name", (object)department.Name ?? DBNull.Value),
                    new SqlParameter("@Description", (object)department.Description ?? DBNull.Value),
                    new SqlParameter("@ParentID", (object)department.ParentID ?? DBNull.Value),
                    new SqlParameter("@ManagerPositionID", (object)department.ManagerPositionID ?? DBNull.Value),
                    new SqlParameter("@Location", (object)department.Location ?? DBNull.Value),
                    new SqlParameter("@IsActive", department.IsActive)
                };

                int rowsAffected = ConnectionManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"Failed to update department with ID {department.ID}");
                throw;
            }
        }

        /// <summary>
        /// Deletes a department
        /// </summary>
        /// <param name="id">Department ID</param>
        /// <returns>True if the operation was successful</returns>
        public bool DeleteDepartment(int id)
        {
            try
            {
                // First, check if there are any employees or positions in this department
                string checkQuery = @"
                    SELECT 
                        (SELECT COUNT(*) FROM Employees WHERE DepartmentID = @ID) +
                        (SELECT COUNT(*) FROM Positions WHERE DepartmentID = @ID) +
                        (SELECT COUNT(*) FROM Departments WHERE ParentID = @ID)";

                SqlParameter[] checkParameters =
                {
                    new SqlParameter("@ID", id)
                };

                object result = ConnectionManager.ExecuteScalar(checkQuery, checkParameters);
                int count = Convert.ToInt32(result);

                if (count > 0)
                {
                    // Cannot delete a department with employees, positions, or child departments
                    return false;
                }

                string query = "DELETE FROM Departments WHERE ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", id)
                };

                int rowsAffected = ConnectionManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"Failed to delete department with ID {id}");
                throw;
            }
        }

        /// <summary>
        /// Gets the employee count for each department
        /// </summary>
        /// <returns>Dictionary of department IDs and employee counts</returns>
        public Dictionary<int, int> GetDepartmentEmployeeCounts()
        {
            try
            {
                string query = @"
                    SELECT DepartmentID, COUNT(*) AS EmployeeCount
                    FROM Employees
                    WHERE DepartmentID IS NOT NULL
                    GROUP BY DepartmentID";

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query))
                {
                    Dictionary<int, int> counts = new Dictionary<int, int>();

                    while (reader.Read())
                    {
                        int departmentId = reader.GetInt32(0);
                        int employeeCount = reader.GetInt32(1);
                        counts[departmentId] = employeeCount;
                    }

                    return counts;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to get department employee counts");
                throw;
            }
        }

        #region Helper Methods

        /// <summary>
        /// Builds the department hierarchy
        /// </summary>
        /// <param name="departments">List of all departments</param>
        private void BuildDepartmentHierarchy(List<DepartmentDTO> departments)
        {
            Dictionary<int, DepartmentDTO> departmentMap = new Dictionary<int, DepartmentDTO>();
            foreach (DepartmentDTO department in departments)
            {
                departmentMap[department.ID] = department;
            }

            foreach (DepartmentDTO department in departments)
            {
                if (department.ParentID.HasValue && departmentMap.ContainsKey(department.ParentID.Value))
                {
                    departmentMap[department.ParentID.Value].ChildDepartments.Add(department);
                }
            }
        }

        /// <summary>
        /// Builds the department tree hierarchy
        /// </summary>
        /// <param name="departments">List of all department tree nodes</param>
        private void BuildDepartmentTreeHierarchy(List<DepartmentTreeNodeDTO> departments)
        {
            Dictionary<int, DepartmentTreeNodeDTO> departmentMap = new Dictionary<int, DepartmentTreeNodeDTO>();
            foreach (DepartmentTreeNodeDTO department in departments)
            {
                departmentMap[department.ID] = department;
            }

            foreach (DepartmentTreeNodeDTO department in departments.ToArray())
            {
                if (department.ParentID.HasValue && departmentMap.ContainsKey(department.ParentID.Value))
                {
                    departmentMap[department.ParentID.Value].Children.Add(department);
                    departments.Remove(department);
                }
            }
        }

        #endregion
    }
}