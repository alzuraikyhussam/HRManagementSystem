using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// مستودع بيانات المسميات الوظيفية
    /// </summary>
    public class PositionRepository
    {
        /// <summary>
        /// الحصول على كافة المسميات الوظيفية
        /// </summary>
        /// <param name="includeInactive">تضمين المسميات غير النشطة</param>
        /// <returns>قائمة بالمسميات الوظيفية</returns>
        public List<PositionDTO> GetAllPositions(bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT p.ID, p.Title, p.Description, p.DepartmentID, d.Name AS DepartmentName,
                           p.GradeLevel, p.MinSalary, p.MaxSalary, p.IsManagerPosition, p.IsActive
                    FROM Positions p
                    LEFT JOIN Departments d ON p.DepartmentID = d.ID";

                if (!includeInactive)
                {
                    query += " WHERE p.IsActive = 1";
                }

                query += " ORDER BY p.Title";

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query))
                {
                    List<PositionDTO> positions = new List<PositionDTO>();

                    while (reader.Read())
                    {
                        PositionDTO position = new PositionDTO
                        {
                            ID = reader.GetInt32(0),
                            Title = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            DepartmentID = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                            DepartmentName = reader.IsDBNull(4) ? null : reader.GetString(4),
                            GradeLevel = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                            MinSalary = reader.IsDBNull(6) ? (decimal?)null : reader.GetDecimal(6),
                            MaxSalary = reader.IsDBNull(7) ? (decimal?)null : reader.GetDecimal(7),
                            IsManagerPosition = reader.GetBoolean(8),
                            IsActive = reader.GetBoolean(9)
                        };

                        positions.Add(position);
                    }

                    return positions;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على كافة المسميات الوظيفية");
                throw;
            }
        }

        /// <summary>
        /// الحصول على المسميات الوظيفية لإدارة معينة
        /// </summary>
        /// <param name="departmentId">معرف الإدارة</param>
        /// <param name="includeInactive">تضمين المسميات غير النشطة</param>
        /// <returns>قائمة بالمسميات الوظيفية للإدارة</returns>
        public List<PositionDTO> GetPositionsByDepartment(int departmentId, bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT p.ID, p.Title, p.Description, p.DepartmentID, d.Name AS DepartmentName,
                           p.GradeLevel, p.MinSalary, p.MaxSalary, p.IsManagerPosition, p.IsActive
                    FROM Positions p
                    LEFT JOIN Departments d ON p.DepartmentID = d.ID
                    WHERE p.DepartmentID = @DepartmentID";

                if (!includeInactive)
                {
                    query += " AND p.IsActive = 1";
                }

                query += " ORDER BY p.Title";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@DepartmentID", departmentId)
                };

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query, parameters))
                {
                    List<PositionDTO> positions = new List<PositionDTO>();

                    while (reader.Read())
                    {
                        PositionDTO position = new PositionDTO
                        {
                            ID = reader.GetInt32(0),
                            Title = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            DepartmentID = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                            DepartmentName = reader.IsDBNull(4) ? null : reader.GetString(4),
                            GradeLevel = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                            MinSalary = reader.IsDBNull(6) ? (decimal?)null : reader.GetDecimal(6),
                            MaxSalary = reader.IsDBNull(7) ? (decimal?)null : reader.GetDecimal(7),
                            IsManagerPosition = reader.GetBoolean(8),
                            IsActive = reader.GetBoolean(9)
                        };

                        positions.Add(position);
                    }

                    return positions;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في الحصول على المسميات الوظيفية للإدارة رقم {departmentId}");
                throw;
            }
        }

        /// <summary>
        /// الحصول على المسميات الوظيفية الإدارية (مدير)
        /// </summary>
        /// <param name="includeInactive">تضمين المسميات غير النشطة</param>
        /// <returns>قائمة بالمسميات الوظيفية الإدارية</returns>
        public List<PositionDTO> GetManagerPositions(bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT p.ID, p.Title, p.Description, p.DepartmentID, d.Name AS DepartmentName,
                           p.GradeLevel, p.MinSalary, p.MaxSalary, p.IsManagerPosition, p.IsActive
                    FROM Positions p
                    LEFT JOIN Departments d ON p.DepartmentID = d.ID
                    WHERE p.IsManagerPosition = 1";

                if (!includeInactive)
                {
                    query += " AND p.IsActive = 1";
                }

                query += " ORDER BY p.Title";

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query))
                {
                    List<PositionDTO> positions = new List<PositionDTO>();

                    while (reader.Read())
                    {
                        PositionDTO position = new PositionDTO
                        {
                            ID = reader.GetInt32(0),
                            Title = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            DepartmentID = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                            DepartmentName = reader.IsDBNull(4) ? null : reader.GetString(4),
                            GradeLevel = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                            MinSalary = reader.IsDBNull(6) ? (decimal?)null : reader.GetDecimal(6),
                            MaxSalary = reader.IsDBNull(7) ? (decimal?)null : reader.GetDecimal(7),
                            IsManagerPosition = reader.GetBoolean(8),
                            IsActive = reader.GetBoolean(9)
                        };

                        positions.Add(position);
                    }

                    return positions;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على المسميات الوظيفية الإدارية");
                throw;
            }
        }

        /// <summary>
        /// الحصول على المسمى الوظيفي بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف المسمى الوظيفي</param>
        /// <returns>بيانات المسمى الوظيفي</returns>
        public PositionDTO GetPositionById(int id)
        {
            try
            {
                string query = @"
                    SELECT p.ID, p.Title, p.Description, p.DepartmentID, d.Name AS DepartmentName,
                           p.GradeLevel, p.MinSalary, p.MaxSalary, p.IsManagerPosition, p.IsActive
                    FROM Positions p
                    LEFT JOIN Departments d ON p.DepartmentID = d.ID
                    WHERE p.ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", id)
                };

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        PositionDTO position = new PositionDTO
                        {
                            ID = reader.GetInt32(0),
                            Title = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            DepartmentID = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                            DepartmentName = reader.IsDBNull(4) ? null : reader.GetString(4),
                            GradeLevel = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                            MinSalary = reader.IsDBNull(6) ? (decimal?)null : reader.GetDecimal(6),
                            MaxSalary = reader.IsDBNull(7) ? (decimal?)null : reader.GetDecimal(7),
                            IsManagerPosition = reader.GetBoolean(8),
                            IsActive = reader.GetBoolean(9)
                        };

                        return position;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في الحصول على المسمى الوظيفي رقم {id}");
                throw;
            }
        }

        /// <summary>
        /// إنشاء مسمى وظيفي جديد
        /// </summary>
        /// <param name="position">بيانات المسمى الوظيفي</param>
        /// <returns>معرف المسمى الوظيفي الجديد</returns>
        public int CreatePosition(PositionDTO position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }

            try
            {
                string query = @"
                    INSERT INTO Positions (
                        Title, Description, DepartmentID, GradeLevel,
                        MinSalary, MaxSalary, IsManagerPosition, IsActive,
                        CreatedAt
                    )
                    VALUES (
                        @Title, @Description, @DepartmentID, @GradeLevel,
                        @MinSalary, @MaxSalary, @IsManagerPosition, @IsActive,
                        GETDATE()
                    );
                    SELECT SCOPE_IDENTITY();";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Title", (object)position.Title ?? DBNull.Value),
                    new SqlParameter("@Description", (object)position.Description ?? DBNull.Value),
                    new SqlParameter("@DepartmentID", (object)position.DepartmentID ?? DBNull.Value),
                    new SqlParameter("@GradeLevel", (object)position.GradeLevel ?? DBNull.Value),
                    new SqlParameter("@MinSalary", (object)position.MinSalary ?? DBNull.Value),
                    new SqlParameter("@MaxSalary", (object)position.MaxSalary ?? DBNull.Value),
                    new SqlParameter("@IsManagerPosition", position.IsManagerPosition),
                    new SqlParameter("@IsActive", position.IsActive)
                };

                object result = ConnectionManager.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في إنشاء مسمى وظيفي جديد");
                throw;
            }
        }

        /// <summary>
        /// تحديث مسمى وظيفي
        /// </summary>
        /// <param name="position">بيانات المسمى الوظيفي</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdatePosition(PositionDTO position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }

            try
            {
                string query = @"
                    UPDATE Positions
                    SET Title = @Title,
                        Description = @Description,
                        DepartmentID = @DepartmentID,
                        GradeLevel = @GradeLevel,
                        MinSalary = @MinSalary,
                        MaxSalary = @MaxSalary,
                        IsManagerPosition = @IsManagerPosition,
                        IsActive = @IsActive,
                        UpdatedAt = GETDATE()
                    WHERE ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", position.ID),
                    new SqlParameter("@Title", (object)position.Title ?? DBNull.Value),
                    new SqlParameter("@Description", (object)position.Description ?? DBNull.Value),
                    new SqlParameter("@DepartmentID", (object)position.DepartmentID ?? DBNull.Value),
                    new SqlParameter("@GradeLevel", (object)position.GradeLevel ?? DBNull.Value),
                    new SqlParameter("@MinSalary", (object)position.MinSalary ?? DBNull.Value),
                    new SqlParameter("@MaxSalary", (object)position.MaxSalary ?? DBNull.Value),
                    new SqlParameter("@IsManagerPosition", position.IsManagerPosition),
                    new SqlParameter("@IsActive", position.IsActive)
                };

                int rowsAffected = ConnectionManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في تحديث المسمى الوظيفي رقم {position.ID}");
                throw;
            }
        }

        /// <summary>
        /// حذف مسمى وظيفي
        /// </summary>
        /// <param name="id">معرف المسمى الوظيفي</param>
        /// <returns>نجاح العملية</returns>
        public bool DeletePosition(int id)
        {
            try
            {
                // التحقق من عدم وجود موظفين مرتبطين بالمسمى الوظيفي
                string checkQuery = "SELECT COUNT(*) FROM Employees WHERE PositionID = @ID";
                
                SqlParameter[] checkParameters =
                {
                    new SqlParameter("@ID", id)
                };

                object result = ConnectionManager.ExecuteScalar(checkQuery, checkParameters);
                int count = Convert.ToInt32(result);

                if (count > 0)
                {
                    // لا يمكن حذف المسمى الوظيفي لوجود موظفين مرتبطين به
                    return false;
                }

                // التحقق من عدم وجود إدارات تستخدم هذا المسمى الوظيفي كمدير
                string checkManagerQuery = "SELECT COUNT(*) FROM Departments WHERE ManagerPositionID = @ID";
                
                object managerResult = ConnectionManager.ExecuteScalar(checkManagerQuery, checkParameters);
                int managerCount = Convert.ToInt32(managerResult);

                if (managerCount > 0)
                {
                    // لا يمكن حذف المسمى الوظيفي لاستخدامه كمسمى إداري
                    return false;
                }

                string query = "DELETE FROM Positions WHERE ID = @ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID", id)
                };

                int rowsAffected = ConnectionManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في حذف المسمى الوظيفي رقم {id}");
                throw;
            }
        }

        /// <summary>
        /// الحصول على عدد الموظفين في كل مسمى وظيفي
        /// </summary>
        /// <returns>قاموس بمعرفات المسميات الوظيفية وعدد الموظفين</returns>
        public Dictionary<int, int> GetPositionEmployeeCounts()
        {
            try
            {
                string query = @"
                    SELECT PositionID, COUNT(*) AS EmployeeCount
                    FROM Employees
                    WHERE PositionID IS NOT NULL
                    GROUP BY PositionID";

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query))
                {
                    Dictionary<int, int> counts = new Dictionary<int, int>();

                    while (reader.Read())
                    {
                        int positionId = reader.GetInt32(0);
                        int employeeCount = reader.GetInt32(1);
                        counts[positionId] = employeeCount;
                    }

                    return counts;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على عدد الموظفين في المسميات الوظيفية");
                throw;
            }
        }
    }
}