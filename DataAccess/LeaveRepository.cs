using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using HR.Core;
using HR.Models;
using HR.Models.DTOs;

namespace HR.DataAccess
{
    /// <summary>
    /// مستودع بيانات الإجازات
    /// </summary>
    public class LeaveRepository
    {
        /// <summary>
        /// الحصول على قائمة أنواع الإجازات
        /// </summary>
        /// <returns>قائمة أنواع الإجازات</returns>
        public List<LeaveType> GetAllLeaveTypes()
        {
            List<LeaveType> leaveTypes = new List<LeaveType>();

            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM LeaveTypes ORDER BY Name", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LeaveType leaveType = new LeaveType
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                                    DefaultDays = Convert.ToInt32(reader["DefaultDays"]),
                                    MaximumDays = reader["MaximumDays"] != DBNull.Value ? Convert.ToInt32(reader["MaximumDays"]) : 0,
                                    IsPaid = Convert.ToBoolean(reader["IsPaid"]),
                                    RequiresApproval = Convert.ToBoolean(reader["RequiresApproval"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    ColorCode = reader["ColorCode"] != DBNull.Value ? reader["ColorCode"].ToString() : null,
                                    Priority = reader["Priority"] != DBNull.Value ? reader["Priority"].ToString() : null,
                                    CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : 0,
                                    CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue,
                                    ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : 0,
                                    ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : DateTime.MinValue
                                };

                                leaveTypes.Add(leaveType);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على قائمة أنواع الإجازات");
                throw;
            }

            return leaveTypes;
        }

        /// <summary>
        /// الحصول على نوع إجازة بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف نوع الإجازة</param>
        /// <returns>نوع الإجازة</returns>
        public LeaveType GetLeaveTypeById(int id)
        {
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM LeaveTypes WHERE ID = @ID", connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new LeaveType
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                                    DefaultDays = Convert.ToInt32(reader["DefaultDays"]),
                                    MaximumDays = reader["MaximumDays"] != DBNull.Value ? Convert.ToInt32(reader["MaximumDays"]) : 0,
                                    IsPaid = Convert.ToBoolean(reader["IsPaid"]),
                                    RequiresApproval = Convert.ToBoolean(reader["RequiresApproval"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    ColorCode = reader["ColorCode"] != DBNull.Value ? reader["ColorCode"].ToString() : null,
                                    Priority = reader["Priority"] != DBNull.Value ? reader["Priority"].ToString() : null,
                                    CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : 0,
                                    CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue,
                                    ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : 0,
                                    ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : DateTime.MinValue
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في الحصول على نوع الإجازة بالمعرف {id}");
                throw;
            }

            return null;
        }

        /// <summary>
        /// إضافة نوع إجازة جديد
        /// </summary>
        /// <param name="leaveType">نوع الإجازة الجديد</param>
        /// <returns>نجاح العملية</returns>
        public bool AddLeaveType(LeaveType leaveType)
        {
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        @"INSERT INTO LeaveTypes 
                          (Name, Description, DefaultDays, MaximumDays, IsPaid, RequiresApproval, 
                           IsActive, ColorCode, Priority, CreatedBy, CreatedDate) 
                          VALUES 
                          (@Name, @Description, @DefaultDays, @MaximumDays, @IsPaid, @RequiresApproval, 
                           @IsActive, @ColorCode, @Priority, @CreatedBy, @CreatedDate);
                          SELECT SCOPE_IDENTITY();", connection))
                    {
                        command.Parameters.AddWithValue("@Name", leaveType.Name);
                        command.Parameters.AddWithValue("@Description", (object)leaveType.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DefaultDays", leaveType.DefaultDays);
                        command.Parameters.AddWithValue("@MaximumDays", (object)leaveType.MaximumDays ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IsPaid", leaveType.IsPaid);
                        command.Parameters.AddWithValue("@RequiresApproval", leaveType.RequiresApproval);
                        command.Parameters.AddWithValue("@IsActive", leaveType.IsActive);
                        command.Parameters.AddWithValue("@ColorCode", (object)leaveType.ColorCode ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Priority", (object)leaveType.Priority ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", SessionManager.CurrentUser.ID);
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                        int newId = Convert.ToInt32(command.ExecuteScalar());
                        leaveType.ID = newId;
                        return newId > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في إضافة نوع إجازة جديد");
                throw;
            }
        }

        /// <summary>
        /// تحديث نوع إجازة
        /// </summary>
        /// <param name="leaveType">نوع الإجازة المحدث</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateLeaveType(LeaveType leaveType)
        {
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        @"UPDATE LeaveTypes 
                          SET Name = @Name, 
                              Description = @Description, 
                              DefaultDays = @DefaultDays, 
                              MaximumDays = @MaximumDays, 
                              IsPaid = @IsPaid, 
                              RequiresApproval = @RequiresApproval, 
                              IsActive = @IsActive, 
                              ColorCode = @ColorCode, 
                              Priority = @Priority, 
                              ModifiedBy = @ModifiedBy, 
                              ModifiedDate = @ModifiedDate 
                          WHERE ID = @ID", connection))
                    {
                        command.Parameters.AddWithValue("@ID", leaveType.ID);
                        command.Parameters.AddWithValue("@Name", leaveType.Name);
                        command.Parameters.AddWithValue("@Description", (object)leaveType.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DefaultDays", leaveType.DefaultDays);
                        command.Parameters.AddWithValue("@MaximumDays", (object)leaveType.MaximumDays ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IsPaid", leaveType.IsPaid);
                        command.Parameters.AddWithValue("@RequiresApproval", leaveType.RequiresApproval);
                        command.Parameters.AddWithValue("@IsActive", leaveType.IsActive);
                        command.Parameters.AddWithValue("@ColorCode", (object)leaveType.ColorCode ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Priority", (object)leaveType.Priority ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedBy", SessionManager.CurrentUser.ID);
                        command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في تحديث نوع الإجازة بالمعرف {leaveType.ID}");
                throw;
            }
        }

        /// <summary>
        /// حذف نوع إجازة
        /// </summary>
        /// <param name="id">معرف نوع الإجازة</param>
        /// <returns>نجاح العملية</returns>
        public bool DeleteLeaveType(int id)
        {
            try
            {
                // التحقق أولاً إذا كان هناك طلبات إجازة تستخدم هذا النوع
                bool isUsed = false;
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        "SELECT COUNT(*) FROM LeaveRequests WHERE LeaveTypeID = @LeaveTypeID", connection))
                    {
                        command.Parameters.AddWithValue("@LeaveTypeID", id);
                        int count = (int)command.ExecuteScalar();
                        isUsed = count > 0;
                    }
                }

                if (isUsed)
                {
                    // لا يمكن حذف نوع الإجازة لأنه مستخدم
                    return false;
                }

                // حذف نوع الإجازة إذا لم يكن مستخدماً
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        "DELETE FROM LeaveTypes WHERE ID = @ID", connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في حذف نوع الإجازة بالمعرف {id}");
                throw;
            }
        }

        /// <summary>
        /// التحقق ما إذا كان اسم نوع الإجازة موجوداً بالفعل
        /// </summary>
        /// <param name="name">اسم نوع الإجازة</param>
        /// <param name="idToExclude">معرف نوع الإجازة للاستثناء (للتحديث)</param>
        /// <returns>وجود الاسم</returns>
        public bool IsLeaveTypeNameExists(string name, int idToExclude = 0)
        {
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    string sql = "SELECT COUNT(*) FROM LeaveTypes WHERE Name = @Name";
                    
                    if (idToExclude > 0)
                    {
                        sql += " AND ID <> @ID";
                    }
                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        
                        if (idToExclude > 0)
                        {
                            command.Parameters.AddWithValue("@ID", idToExclude);
                        }
                        
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في التحقق من وجود اسم نوع الإجازة '{name}'");
                throw;
            }
        }

        /// <summary>
        /// الحصول على الإجازات النشطة في تاريخ معين
        /// </summary>
        /// <param name="date">التاريخ</param>
        /// <returns>قائمة الإجازات</returns>
        public List<LeaveRequest> GetActiveLeaves(DateTime date)
        {
            List<LeaveRequest> leaves = new List<LeaveRequest>();

            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT lr.*, 
                            lt.Name AS LeaveTypeName, 
                            e.FirstName, e.LastName, e.ID as EmployeeID
                          FROM LeaveRequests lr
                          INNER JOIN LeaveTypes lt ON lr.LeaveTypeID = lt.ID
                          INNER JOIN Employees e ON lr.EmployeeID = e.ID
                          WHERE lr.StartDate <= @Date AND lr.EndDate >= @Date
                                AND lr.Status = 'Approved'
                          ORDER BY lr.StartDate", connection))
                    {
                        command.Parameters.AddWithValue("@Date", date.Date);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LeaveRequest leave = new LeaveRequest
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                    LeaveTypeID = Convert.ToInt32(reader["LeaveTypeID"]),
                                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                                    EndDate = Convert.ToDateTime(reader["EndDate"]),
                                    Status = reader["Status"].ToString(),
                                    Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                                    SubmissionDate = reader["SubmissionDate"] != DBNull.Value ? Convert.ToDateTime(reader["SubmissionDate"]) : DateTime.MinValue,
                                    ApprovalDate = reader["ApprovalDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ApprovalDate"]) : null,
                                    ApprovedBy = reader["ApprovedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ApprovedBy"]) : null,
                                    ReasonForRejection = reader["ReasonForRejection"] != DBNull.Value ? reader["ReasonForRejection"].ToString() : null,
                                    CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : 0,
                                    CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue,
                                    ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : 0,
                                    ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : DateTime.MinValue,
                                    Employee = new Employee
                                    {
                                        ID = Convert.ToInt32(reader["EmployeeID"]),
                                        FirstName = reader["FirstName"].ToString(),
                                        LastName = reader["LastName"].ToString()
                                    },
                                    LeaveType = new LeaveType
                                    {
                                        ID = Convert.ToInt32(reader["LeaveTypeID"]),
                                        Name = reader["LeaveTypeName"].ToString()
                                    }
                                };

                                leaves.Add(leave);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على الإجازات النشطة");
                throw;
            }

            return leaves;
        }

        /// <summary>
        /// الحصول على الإجازات في فترة زمنية محددة
        /// </summary>
        /// <param name="fromDate">من تاريخ</param>
        /// <param name="toDate">إلى تاريخ</param>
        /// <returns>قائمة الإجازات</returns>
        public List<LeaveRequest> GetLeavesByDateRange(DateTime fromDate, DateTime toDate)
        {
            List<LeaveRequest> leaves = new List<LeaveRequest>();

            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT lr.*, 
                            lt.Name AS LeaveTypeName, 
                            e.FirstName, e.LastName, e.ID as EmployeeID
                          FROM LeaveRequests lr
                          INNER JOIN LeaveTypes lt ON lr.LeaveTypeID = lt.ID
                          INNER JOIN Employees e ON lr.EmployeeID = e.ID
                          WHERE 
                            (lr.StartDate BETWEEN @FromDate AND @ToDate OR
                             lr.EndDate BETWEEN @FromDate AND @ToDate OR
                             @FromDate BETWEEN lr.StartDate AND lr.EndDate)
                          ORDER BY lr.StartDate", connection))
                    {
                        command.Parameters.AddWithValue("@FromDate", fromDate.Date);
                        command.Parameters.AddWithValue("@ToDate", toDate.Date);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LeaveRequest leave = new LeaveRequest
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                    LeaveTypeID = Convert.ToInt32(reader["LeaveTypeID"]),
                                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                                    EndDate = Convert.ToDateTime(reader["EndDate"]),
                                    Status = reader["Status"].ToString(),
                                    Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                                    SubmissionDate = reader["SubmissionDate"] != DBNull.Value ? Convert.ToDateTime(reader["SubmissionDate"]) : DateTime.MinValue,
                                    ApprovalDate = reader["ApprovalDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ApprovalDate"]) : null,
                                    ApprovedBy = reader["ApprovedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ApprovedBy"]) : null,
                                    ReasonForRejection = reader["ReasonForRejection"] != DBNull.Value ? reader["ReasonForRejection"].ToString() : null,
                                    CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : 0,
                                    CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue,
                                    ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : 0,
                                    ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : DateTime.MinValue,
                                    Employee = new Employee
                                    {
                                        ID = Convert.ToInt32(reader["EmployeeID"]),
                                        FirstName = reader["FirstName"].ToString(),
                                        LastName = reader["LastName"].ToString()
                                    },
                                    LeaveType = new LeaveType
                                    {
                                        ID = Convert.ToInt32(reader["LeaveTypeID"]),
                                        Name = reader["LeaveTypeName"].ToString()
                                    }
                                };

                                leaves.Add(leave);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على الإجازات في الفترة الزمنية المحددة");
                throw;
            }

            return leaves;
        }

        /// <summary>
        /// الحصول على طلبات الإجازات بناءً على معايير البحث
        /// </summary>
        /// <param name="employeeId">معرف الموظف (0 لعرض الكل)</param>
        /// <param name="fromDate">من تاريخ</param>
        /// <param name="toDate">إلى تاريخ</param>
        /// <param name="status">حالة الطلب (null لعرض الكل)</param>
        /// <returns>قائمة طلبات الإجازات</returns>
        public List<LeaveRequestDTO> GetLeaveRequests(int employeeId, DateTime fromDate, DateTime toDate, string status = null)
        {
            List<LeaveRequestDTO> requests = new List<LeaveRequestDTO>();

            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();

                    string sql = @"
                        SELECT 
                            lr.ID,
                            lr.EmployeeID,
                            e.FirstName + ' ' + e.LastName AS EmployeeName,
                            lr.LeaveTypeID,
                            lt.Name AS LeaveType,
                            lr.StartDate,
                            lr.EndDate,
                            DATEDIFF(day, lr.StartDate, lr.EndDate) + 1 AS Days,
                            lr.SubmissionDate,
                            lr.Status,
                            lr.Notes,
                            lr.ApprovalDate,
                            lr.ApprovedBy,
                            a.FirstName + ' ' + a.LastName AS ApprovedByName,
                            lr.ReasonForRejection
                        FROM 
                            LeaveRequests lr
                        INNER JOIN 
                            Employees e ON lr.EmployeeID = e.ID
                        INNER JOIN 
                            LeaveTypes lt ON lr.LeaveTypeID = lt.ID
                        LEFT JOIN 
                            Employees a ON lr.ApprovedBy = a.ID
                        WHERE 
                            (lr.StartDate BETWEEN @FromDate AND @ToDate OR
                             lr.EndDate BETWEEN @FromDate AND @ToDate)";

                    if (employeeId > 0)
                    {
                        sql += " AND lr.EmployeeID = @EmployeeID";
                    }

                    if (!string.IsNullOrEmpty(status))
                    {
                        sql += " AND lr.Status = @Status";
                    }

                    sql += " ORDER BY lr.SubmissionDate DESC";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FromDate", fromDate.Date);
                        command.Parameters.AddWithValue("@ToDate", toDate.Date);

                        if (employeeId > 0)
                        {
                            command.Parameters.AddWithValue("@EmployeeID", employeeId);
                        }

                        if (!string.IsNullOrEmpty(status))
                        {
                            command.Parameters.AddWithValue("@Status", status);
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LeaveRequestDTO request = new LeaveRequestDTO
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    EmployeeId = Convert.ToInt32(reader["EmployeeID"]),
                                    EmployeeName = reader["EmployeeName"].ToString(),
                                    LeaveTypeId = Convert.ToInt32(reader["LeaveTypeID"]),
                                    LeaveType = reader["LeaveType"].ToString(),
                                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                                    EndDate = Convert.ToDateTime(reader["EndDate"]),
                                    Days = Convert.ToInt32(reader["Days"]),
                                    SubmissionDate = Convert.ToDateTime(reader["SubmissionDate"]),
                                    Status = reader["Status"].ToString(),
                                    Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                                    ApprovalDate = reader["ApprovalDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ApprovalDate"]) : null,
                                    ApprovedById = reader["ApprovedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ApprovedBy"]) : null,
                                    ApprovedByName = reader["ApprovedByName"] != DBNull.Value ? reader["ApprovedByName"].ToString() : null,
                                    ReasonForRejection = reader["ReasonForRejection"] != DBNull.Value ? reader["ReasonForRejection"].ToString() : null
                                };

                                requests.Add(request);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على طلبات الإجازات");
                throw;
            }

            return requests;
        }

        /// <summary>
        /// الحصول على طلب إجازة بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف طلب الإجازة</param>
        /// <returns>طلب الإجازة</returns>
        public LeaveRequest GetLeaveRequestById(int id)
        {
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT lr.*, 
                            lt.Name AS LeaveTypeName, 
                            e.FirstName, e.LastName, e.ID as EmployeeID
                          FROM LeaveRequests lr
                          INNER JOIN LeaveTypes lt ON lr.LeaveTypeID = lt.ID
                          INNER JOIN Employees e ON lr.EmployeeID = e.ID
                          WHERE lr.ID = @ID", connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new LeaveRequest
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                    LeaveTypeID = Convert.ToInt32(reader["LeaveTypeID"]),
                                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                                    EndDate = Convert.ToDateTime(reader["EndDate"]),
                                    Status = reader["Status"].ToString(),
                                    Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                                    SubmissionDate = reader["SubmissionDate"] != DBNull.Value ? Convert.ToDateTime(reader["SubmissionDate"]) : DateTime.MinValue,
                                    ApprovalDate = reader["ApprovalDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ApprovalDate"]) : null,
                                    ApprovedBy = reader["ApprovedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ApprovedBy"]) : null,
                                    ReasonForRejection = reader["ReasonForRejection"] != DBNull.Value ? reader["ReasonForRejection"].ToString() : null,
                                    CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : 0,
                                    CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue,
                                    ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : 0,
                                    ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : DateTime.MinValue,
                                    Employee = new Employee
                                    {
                                        ID = Convert.ToInt32(reader["EmployeeID"]),
                                        FirstName = reader["FirstName"].ToString(),
                                        LastName = reader["LastName"].ToString()
                                    },
                                    LeaveType = new LeaveType
                                    {
                                        ID = Convert.ToInt32(reader["LeaveTypeID"]),
                                        Name = reader["LeaveTypeName"].ToString()
                                    }
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في الحصول على طلب الإجازة بالمعرف {id}");
                throw;
            }

            return null;
        }

        /// <summary>
        /// إضافة طلب إجازة جديد
        /// </summary>
        /// <param name="leaveRequest">طلب الإجازة الجديد</param>
        /// <returns>نجاح العملية</returns>
        public bool AddLeaveRequest(LeaveRequest leaveRequest)
        {
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    
                    // بدء المعاملة
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // إضافة طلب الإجازة
                            using (SqlCommand command = new SqlCommand(
                                @"INSERT INTO LeaveRequests 
                                  (EmployeeID, LeaveTypeID, StartDate, EndDate, Status, Notes, 
                                   SubmissionDate, CreatedBy, CreatedDate) 
                                  VALUES 
                                  (@EmployeeID, @LeaveTypeID, @StartDate, @EndDate, @Status, @Notes, 
                                   @SubmissionDate, @CreatedBy, @CreatedDate);
                                  SELECT SCOPE_IDENTITY();", connection, transaction))
                            {
                                command.Parameters.AddWithValue("@EmployeeID", leaveRequest.EmployeeID);
                                command.Parameters.AddWithValue("@LeaveTypeID", leaveRequest.LeaveTypeID);
                                command.Parameters.AddWithValue("@StartDate", leaveRequest.StartDate);
                                command.Parameters.AddWithValue("@EndDate", leaveRequest.EndDate);
                                command.Parameters.AddWithValue("@Status", "Pending"); // حالة افتراضية للطلبات الجديدة
                                command.Parameters.AddWithValue("@Notes", (object)leaveRequest.Notes ?? DBNull.Value);
                                command.Parameters.AddWithValue("@SubmissionDate", DateTime.Now);
                                command.Parameters.AddWithValue("@CreatedBy", SessionManager.CurrentUser.ID);
                                command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                                int newId = Convert.ToInt32(command.ExecuteScalar());
                                leaveRequest.ID = newId;
                                
                                // تعديل رصيد الإجازات (سيتم تنفيذه عند الموافقة على الطلب)
                                
                                // إتمام المعاملة
                                transaction.Commit();
                                return newId > 0;
                            }
                        }
                        catch (Exception)
                        {
                            // التراجع عن المعاملة في حالة حدوث خطأ
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في إضافة طلب إجازة جديد");
                throw;
            }
        }

        /// <summary>
        /// الموافقة على طلب إجازة
        /// </summary>
        /// <param name="leaveRequestId">معرف طلب الإجازة</param>
        /// <param name="approvedBy">معرف المستخدم الذي وافق على الطلب</param>
        /// <param name="approvalDate">تاريخ الموافقة</param>
        /// <param name="notes">ملاحظات الموافقة</param>
        /// <returns>نجاح العملية</returns>
        public bool ApproveLeaveRequest(int leaveRequestId, int approvedBy, DateTime approvalDate, string notes)
        {
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    
                    // بدء المعاملة
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // الحصول على معلومات طلب الإجازة
                            LeaveRequest leaveRequest;
                            using (SqlCommand command = new SqlCommand(
                                "SELECT * FROM LeaveRequests WHERE ID = @ID", connection, transaction))
                            {
                                command.Parameters.AddWithValue("@ID", leaveRequestId);
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (!reader.Read())
                                    {
                                        // طلب الإجازة غير موجود
                                        return false;
                                    }
                                    
                                    leaveRequest = new LeaveRequest
                                    {
                                        ID = Convert.ToInt32(reader["ID"]),
                                        EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                        LeaveTypeID = Convert.ToInt32(reader["LeaveTypeID"]),
                                        StartDate = Convert.ToDateTime(reader["StartDate"]),
                                        EndDate = Convert.ToDateTime(reader["EndDate"]),
                                        Status = reader["Status"].ToString()
                                    };
                                }
                            }
                            
                            // التحقق من أن الطلب في حالة انتظار
                            if (leaveRequest.Status != "Pending")
                            {
                                // لا يمكن الموافقة على طلب ليس في حالة انتظار
                                return false;
                            }
                            
                            // حساب عدد أيام الإجازة
                            int days = (leaveRequest.EndDate - leaveRequest.StartDate).Days + 1;
                            
                            // التحقق من رصيد الإجازات
                            // في التطبيق الفعلي، نحتاج للتحقق من رصيد الإجازات المتاح للموظف قبل الموافقة
                            
                            // تحديث حالة طلب الإجازة
                            using (SqlCommand command = new SqlCommand(
                                @"UPDATE LeaveRequests 
                                  SET Status = @Status, 
                                      ApprovalDate = @ApprovalDate, 
                                      ApprovedBy = @ApprovedBy, 
                                      Notes = @Notes,
                                      ModifiedBy = @ModifiedBy, 
                                      ModifiedDate = @ModifiedDate 
                                  WHERE ID = @ID", connection, transaction))
                            {
                                command.Parameters.AddWithValue("@ID", leaveRequestId);
                                command.Parameters.AddWithValue("@Status", "Approved");
                                command.Parameters.AddWithValue("@ApprovalDate", approvalDate);
                                command.Parameters.AddWithValue("@ApprovedBy", approvedBy);
                                command.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
                                command.Parameters.AddWithValue("@ModifiedBy", SessionManager.CurrentUser.ID);
                                command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                                int rowsAffected = command.ExecuteNonQuery();
                                
                                if (rowsAffected <= 0)
                                {
                                    // فشل في تحديث الطلب
                                    transaction.Rollback();
                                    return false;
                                }
                            }
                            
                            // تحديث رصيد الإجازات (LeaveBalances)
                            // في التطبيق الفعلي، نحتاج لخصم عدد أيام الإجازة من رصيد الموظف
                            
                            // إتمام المعاملة
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            // التراجع عن المعاملة في حالة حدوث خطأ
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في الموافقة على طلب الإجازة بالمعرف {leaveRequestId}");
                throw;
            }
        }

        /// <summary>
        /// رفض طلب إجازة
        /// </summary>
        /// <param name="leaveRequestId">معرف طلب الإجازة</param>
        /// <param name="rejectedBy">معرف المستخدم الذي رفض الطلب</param>
        /// <param name="rejectionDate">تاريخ الرفض</param>
        /// <param name="reasonForRejection">سبب الرفض</param>
        /// <returns>نجاح العملية</returns>
        public bool RejectLeaveRequest(int leaveRequestId, int rejectedBy, DateTime rejectionDate, string reasonForRejection)
        {
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    
                    // التحقق من أن الطلب في حالة انتظار
                    using (SqlCommand checkCommand = new SqlCommand(
                        "SELECT Status FROM LeaveRequests WHERE ID = @ID", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@ID", leaveRequestId);
                        string status = (string)checkCommand.ExecuteScalar();
                        
                        if (status != "Pending")
                        {
                            // لا يمكن رفض طلب ليس في حالة انتظار
                            return false;
                        }
                    }
                    
                    // تحديث حالة طلب الإجازة
                    using (SqlCommand command = new SqlCommand(
                        @"UPDATE LeaveRequests 
                          SET Status = @Status, 
                              ApprovalDate = @RejectionDate, 
                              ApprovedBy = @RejectedBy, 
                              ReasonForRejection = @ReasonForRejection,
                              ModifiedBy = @ModifiedBy, 
                              ModifiedDate = @ModifiedDate 
                          WHERE ID = @ID", connection))
                    {
                        command.Parameters.AddWithValue("@ID", leaveRequestId);
                        command.Parameters.AddWithValue("@Status", "Rejected");
                        command.Parameters.AddWithValue("@RejectionDate", rejectionDate);
                        command.Parameters.AddWithValue("@RejectedBy", rejectedBy);
                        command.Parameters.AddWithValue("@ReasonForRejection", reasonForRejection);
                        command.Parameters.AddWithValue("@ModifiedBy", SessionManager.CurrentUser.ID);
                        command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في رفض طلب الإجازة بالمعرف {leaveRequestId}");
                throw;
            }
        }

        /// <summary>
        /// إلغاء طلب إجازة
        /// </summary>
        /// <param name="leaveRequestId">معرف طلب الإجازة</param>
        /// <returns>نجاح العملية</returns>
        public bool CancelLeaveRequest(int leaveRequestId)
        {
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    
                    // بدء المعاملة
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // التحقق من أن الطلب في حالة انتظار أو تمت الموافقة
                            string status;
                            using (SqlCommand checkCommand = new SqlCommand(
                                "SELECT Status FROM LeaveRequests WHERE ID = @ID", connection, transaction))
                            {
                                checkCommand.Parameters.AddWithValue("@ID", leaveRequestId);
                                status = (string)checkCommand.ExecuteScalar();
                                
                                if (status != "Pending" && status != "Approved")
                                {
                                    // لا يمكن إلغاء طلب ليس في حالة انتظار أو تمت الموافقة
                                    return false;
                                }
                            }
                            
                            // تحديث حالة طلب الإجازة
                            using (SqlCommand command = new SqlCommand(
                                @"UPDATE LeaveRequests 
                                  SET Status = @Status, 
                                      ModifiedBy = @ModifiedBy, 
                                      ModifiedDate = @ModifiedDate 
                                  WHERE ID = @ID", connection, transaction))
                            {
                                command.Parameters.AddWithValue("@ID", leaveRequestId);
                                command.Parameters.AddWithValue("@Status", "Cancelled");
                                command.Parameters.AddWithValue("@ModifiedBy", SessionManager.CurrentUser.ID);
                                command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                                int rowsAffected = command.ExecuteNonQuery();
                                
                                if (rowsAffected <= 0)
                                {
                                    // فشل في تحديث الطلب
                                    transaction.Rollback();
                                    return false;
                                }
                            }
                            
                            // إذا كان الطلب قد تمت الموافقة عليه، نحتاج لإعادة أيام الإجازة إلى رصيد الموظف
                            if (status == "Approved")
                            {
                                // في التطبيق الفعلي، نحتاج لإعادة أيام الإجازة إلى رصيد الموظف
                            }
                            
                            // إتمام المعاملة
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            // التراجع عن المعاملة في حالة حدوث خطأ
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في إلغاء طلب الإجازة بالمعرف {leaveRequestId}");
                throw;
            }
        }

        /// <summary>
        /// التحقق من تداخل الإجازات للموظف
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <param name="leaveRequestIdToExclude">معرف طلب الإجازة للاستثناء (للتحديث)</param>
        /// <returns>وجود تداخل</returns>
        public bool CheckLeaveOverlap(int employeeId, DateTime startDate, DateTime endDate, int leaveRequestIdToExclude = 0)
        {
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    string sql = @"
                        SELECT COUNT(*) FROM LeaveRequests 
                        WHERE EmployeeID = @EmployeeID 
                          AND Status IN ('Pending', 'Approved')
                          AND (
                              (StartDate BETWEEN @StartDate AND @EndDate) OR
                              (EndDate BETWEEN @StartDate AND @EndDate) OR
                              (@StartDate BETWEEN StartDate AND EndDate)
                          )";
                    
                    if (leaveRequestIdToExclude > 0)
                    {
                        sql += " AND ID <> @LeaveRequestID";
                    }
                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeID", employeeId);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        
                        if (leaveRequestIdToExclude > 0)
                        {
                            command.Parameters.AddWithValue("@LeaveRequestID", leaveRequestIdToExclude);
                        }
                        
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في التحقق من تداخل الإجازات");
                throw;
            }
        }

        /// <summary>
        /// الحصول على عدد الطلبات المعلقة
        /// </summary>
        /// <returns>عدد الطلبات المعلقة</returns>
        public int GetPendingRequestsCount()
        {
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        "SELECT COUNT(*) FROM LeaveRequests WHERE Status = 'Pending'", connection))
                    {
                        return (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على عدد طلبات الإجازات المعلقة");
                throw;
            }
        }
    }
}