using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// مستودع بيانات المناوبات
    /// </summary>
    public class WorkShiftRepository
    {
        private readonly ConnectionManager _connectionManager;
        
        public WorkShiftRepository()
        {
            _connectionManager = new ConnectionManager();
        }
        
        /// <summary>
        /// الحصول على جميع المناوبات
        /// </summary>
        /// <returns>جدول بيانات يحتوي على المناوبات</returns>
        public DataTable GetWorkShifts()
        {
            DataTable workShifts = new DataTable();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = @"
                        SELECT 
                            WS.ID,
                            WS.Name,
                            WS.Description,
                            WS.WorkHoursID,
                            WH.Name AS WorkHoursName,
                            WH.StartTime,
                            WH.EndTime,
                            WS.SundayEnabled,
                            WS.MondayEnabled,
                            WS.TuesdayEnabled,
                            WS.WednesdayEnabled,
                            WS.ThursdayEnabled,
                            WS.FridayEnabled,
                            WS.SaturdayEnabled,
                            WS.ColorCode,
                            WS.IsActive,
                            WS.CreatedAt,
                            WS.CreatedBy,
                            WS.UpdatedAt,
                            WS.UpdatedBy
                        FROM WorkShifts WS
                        LEFT JOIN WorkHours WH ON WS.WorkHoursID = WH.ID
                        ORDER BY WS.Name";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(workShifts);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء استرجاع المناوبات");
            }
            
            return workShifts;
        }
        
        /// <summary>
        /// الحصول على جميع المناوبات النشطة
        /// </summary>
        /// <returns>جدول بيانات يحتوي على المناوبات النشطة</returns>
        public DataTable GetActiveWorkShifts()
        {
            DataTable workShifts = new DataTable();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = @"
                        SELECT 
                            WS.ID,
                            WS.Name,
                            WS.Description,
                            WS.WorkHoursID,
                            WH.Name AS WorkHoursName,
                            WH.StartTime,
                            WH.EndTime,
                            WS.SundayEnabled,
                            WS.MondayEnabled,
                            WS.TuesdayEnabled,
                            WS.WednesdayEnabled,
                            WS.ThursdayEnabled,
                            WS.FridayEnabled,
                            WS.SaturdayEnabled,
                            WS.ColorCode,
                            WS.IsActive,
                            WS.CreatedAt,
                            WS.CreatedBy,
                            WS.UpdatedAt,
                            WS.UpdatedBy
                        FROM WorkShifts WS
                        LEFT JOIN WorkHours WH ON WS.WorkHoursID = WH.ID
                        WHERE WS.IsActive = 1
                        ORDER BY WS.Name";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(workShifts);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء استرجاع المناوبات النشطة");
            }
            
            return workShifts;
        }
        
        /// <summary>
        /// الحصول على مناوبة محددة
        /// </summary>
        /// <param name="id">معرف المناوبة</param>
        /// <returns>مناوبة</returns>
        public WorkShiftModel GetWorkShiftById(int id)
        {
            WorkShiftModel workShift = null;
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = @"
                        SELECT 
                            WS.ID,
                            WS.Name,
                            WS.Description,
                            WS.WorkHoursID,
                            WH.Name AS WorkHoursName,
                            WH.StartTime,
                            WH.EndTime,
                            WS.SundayEnabled,
                            WS.MondayEnabled,
                            WS.TuesdayEnabled,
                            WS.WednesdayEnabled,
                            WS.ThursdayEnabled,
                            WS.FridayEnabled,
                            WS.SaturdayEnabled,
                            WS.ColorCode,
                            WS.IsActive
                        FROM WorkShifts WS
                        LEFT JOIN WorkHours WH ON WS.WorkHoursID = WH.ID
                        WHERE WS.ID = @ID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        
                        connection.Open();
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                workShift = new WorkShiftModel
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty,
                                    WorkHoursID = Convert.ToInt32(reader["WorkHoursID"]),
                                    WorkHoursName = reader["WorkHoursName"] != DBNull.Value ? reader["WorkHoursName"].ToString() : string.Empty,
                                    StartTime = reader["StartTime"] != DBNull.Value ? (TimeSpan?)reader["StartTime"] : null,
                                    EndTime = reader["EndTime"] != DBNull.Value ? (TimeSpan?)reader["EndTime"] : null,
                                    SundayEnabled = Convert.ToBoolean(reader["SundayEnabled"]),
                                    MondayEnabled = Convert.ToBoolean(reader["MondayEnabled"]),
                                    TuesdayEnabled = Convert.ToBoolean(reader["TuesdayEnabled"]),
                                    WednesdayEnabled = Convert.ToBoolean(reader["WednesdayEnabled"]),
                                    ThursdayEnabled = Convert.ToBoolean(reader["ThursdayEnabled"]),
                                    FridayEnabled = Convert.ToBoolean(reader["FridayEnabled"]),
                                    SaturdayEnabled = Convert.ToBoolean(reader["SaturdayEnabled"]),
                                    ColorCode = reader["ColorCode"] != DBNull.Value ? reader["ColorCode"].ToString() : string.Empty,
                                    IsActive = Convert.ToBoolean(reader["IsActive"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء استرجاع المناوبة رقم: {id}");
            }
            
            return workShift;
        }
        
        /// <summary>
        /// إضافة مناوبة جديدة
        /// </summary>
        /// <param name="workShift">بيانات المناوبة</param>
        /// <returns>نجاح العملية</returns>
        public bool AddWorkShift(WorkShiftModel workShift)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = @"
                        INSERT INTO WorkShifts (
                            Name,
                            Description,
                            WorkHoursID,
                            SundayEnabled,
                            MondayEnabled,
                            TuesdayEnabled,
                            WednesdayEnabled,
                            ThursdayEnabled,
                            FridayEnabled,
                            SaturdayEnabled,
                            ColorCode,
                            IsActive,
                            CreatedAt,
                            CreatedBy)
                        VALUES (
                            @Name,
                            @Description,
                            @WorkHoursID,
                            @SundayEnabled,
                            @MondayEnabled,
                            @TuesdayEnabled,
                            @WednesdayEnabled,
                            @ThursdayEnabled,
                            @FridayEnabled,
                            @SaturdayEnabled,
                            @ColorCode,
                            @IsActive,
                            GETDATE(),
                            @CreatedBy);
                        
                        SELECT SCOPE_IDENTITY()";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", workShift.Name);
                        command.Parameters.AddWithValue("@Description", !string.IsNullOrEmpty(workShift.Description) ? (object)workShift.Description : DBNull.Value);
                        command.Parameters.AddWithValue("@WorkHoursID", workShift.WorkHoursID);
                        command.Parameters.AddWithValue("@SundayEnabled", workShift.SundayEnabled);
                        command.Parameters.AddWithValue("@MondayEnabled", workShift.MondayEnabled);
                        command.Parameters.AddWithValue("@TuesdayEnabled", workShift.TuesdayEnabled);
                        command.Parameters.AddWithValue("@WednesdayEnabled", workShift.WednesdayEnabled);
                        command.Parameters.AddWithValue("@ThursdayEnabled", workShift.ThursdayEnabled);
                        command.Parameters.AddWithValue("@FridayEnabled", workShift.FridayEnabled);
                        command.Parameters.AddWithValue("@SaturdayEnabled", workShift.SaturdayEnabled);
                        command.Parameters.AddWithValue("@ColorCode", !string.IsNullOrEmpty(workShift.ColorCode) ? (object)workShift.ColorCode : DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", workShift.IsActive);
                        command.Parameters.AddWithValue("@CreatedBy", SessionManager.CurrentUser.ID);
                        
                        connection.Open();
                        
                        object result = command.ExecuteScalar();
                        
                        if (result != null && result != DBNull.Value)
                        {
                            workShift.ID = Convert.ToInt32(result);
                            return true;
                        }
                    }
                }
                
                return false;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء إضافة مناوبة جديدة");
                return false;
            }
        }
        
        /// <summary>
        /// تعديل مناوبة
        /// </summary>
        /// <param name="workShift">بيانات المناوبة</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateWorkShift(WorkShiftModel workShift)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = @"
                        UPDATE WorkShifts
                        SET 
                            Name = @Name,
                            Description = @Description,
                            WorkHoursID = @WorkHoursID,
                            SundayEnabled = @SundayEnabled,
                            MondayEnabled = @MondayEnabled,
                            TuesdayEnabled = @TuesdayEnabled,
                            WednesdayEnabled = @WednesdayEnabled,
                            ThursdayEnabled = @ThursdayEnabled,
                            FridayEnabled = @FridayEnabled,
                            SaturdayEnabled = @SaturdayEnabled,
                            ColorCode = @ColorCode,
                            IsActive = @IsActive,
                            UpdatedAt = GETDATE(),
                            UpdatedBy = @UpdatedBy
                        WHERE ID = @ID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", workShift.ID);
                        command.Parameters.AddWithValue("@Name", workShift.Name);
                        command.Parameters.AddWithValue("@Description", !string.IsNullOrEmpty(workShift.Description) ? (object)workShift.Description : DBNull.Value);
                        command.Parameters.AddWithValue("@WorkHoursID", workShift.WorkHoursID);
                        command.Parameters.AddWithValue("@SundayEnabled", workShift.SundayEnabled);
                        command.Parameters.AddWithValue("@MondayEnabled", workShift.MondayEnabled);
                        command.Parameters.AddWithValue("@TuesdayEnabled", workShift.TuesdayEnabled);
                        command.Parameters.AddWithValue("@WednesdayEnabled", workShift.WednesdayEnabled);
                        command.Parameters.AddWithValue("@ThursdayEnabled", workShift.ThursdayEnabled);
                        command.Parameters.AddWithValue("@FridayEnabled", workShift.FridayEnabled);
                        command.Parameters.AddWithValue("@SaturdayEnabled", workShift.SaturdayEnabled);
                        command.Parameters.AddWithValue("@ColorCode", !string.IsNullOrEmpty(workShift.ColorCode) ? (object)workShift.ColorCode : DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", workShift.IsActive);
                        command.Parameters.AddWithValue("@UpdatedBy", SessionManager.CurrentUser.ID);
                        
                        connection.Open();
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء تعديل المناوبة رقم: {workShift.ID}");
                return false;
            }
        }
        
        /// <summary>
        /// حذف مناوبة
        /// </summary>
        /// <param name="id">معرف المناوبة</param>
        /// <returns>نجاح العملية</returns>
        public bool DeleteWorkShift(int id)
        {
            try
            {
                // التحقق من عدم وجود موظفين مرتبطين بالمناوبة
                if (HasDependentEmployees(id))
                {
                    return false;
                }
                
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = "DELETE FROM WorkShifts WHERE ID = @ID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        
                        connection.Open();
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء حذف المناوبة رقم: {id}");
                return false;
            }
        }
        
        /// <summary>
        /// التحقق من وجود موظفين مرتبطين بالمناوبة
        /// </summary>
        /// <param name="id">معرف المناوبة</param>
        /// <returns>وجود موظفين مرتبطين</returns>
        private bool HasDependentEmployees(int id)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = "SELECT COUNT(*) FROM Employees WHERE WorkShiftID = @WorkShiftID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@WorkShiftID", id);
                        
                        connection.Open();
                        
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء التحقق من ارتباط المناوبة رقم: {id}");
                return false;
            }
        }
        
        /// <summary>
        /// الحصول على قائمة المناوبات للاختيار
        /// </summary>
        /// <returns>قائمة المناوبات</returns>
        public List<WorkShiftListItem> GetWorkShiftsDropDownList()
        {
            List<WorkShiftListItem> list = new List<WorkShiftListItem>();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = "SELECT ID, Name FROM WorkShifts WHERE IsActive = 1 ORDER BY Name";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new WorkShiftListItem
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Name = reader["Name"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء استرجاع قائمة المناوبات");
            }
            
            return list;
        }
    }
    
    /// <summary>
    /// عنصر قائمة المناوبات
    /// </summary>
    public class WorkShiftListItem
    {
        /// <summary>
        /// معرف المناوبة
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// اسم المناوبة
        /// </summary>
        public string Name { get; set; }
    }
}