using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// مستودع بيانات فترات العمل
    /// </summary>
    public class WorkHoursRepository
    {
        private readonly ConnectionManager _connectionManager;
        
        public WorkHoursRepository()
        {
            _connectionManager = new ConnectionManager();
        }
        
        /// <summary>
        /// الحصول على جميع فترات العمل
        /// </summary>
        /// <returns>جدول بيانات يحتوي على فترات العمل</returns>
        public DataTable GetWorkHours()
        {
            DataTable workHours = new DataTable();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = @"
                        SELECT 
                            ID,
                            Name,
                            Description,
                            StartTime,
                            EndTime,
                            FlexibleMinutes,
                            LateThresholdMinutes,
                            ShortDayThresholdMinutes,
                            OverTimeStartMinutes,
                            TotalHours,
                            CreatedAt,
                            CreatedBy,
                            UpdatedAt,
                            UpdatedBy
                        FROM WorkHours
                        ORDER BY Name";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(workHours);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء استرجاع فترات العمل");
            }
            
            return workHours;
        }
        
        /// <summary>
        /// الحصول على فترة عمل محددة
        /// </summary>
        /// <param name="id">معرف فترة العمل</param>
        /// <returns>فترة العمل</returns>
        public WorkHoursModel GetWorkHoursById(int id)
        {
            WorkHoursModel workHours = null;
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = @"
                        SELECT 
                            ID,
                            Name,
                            Description,
                            StartTime,
                            EndTime,
                            FlexibleMinutes,
                            LateThresholdMinutes,
                            ShortDayThresholdMinutes,
                            OverTimeStartMinutes,
                            TotalHours
                        FROM WorkHours
                        WHERE ID = @ID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        
                        connection.Open();
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                workHours = new WorkHoursModel
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty,
                                    StartTime = (TimeSpan)reader["StartTime"],
                                    EndTime = (TimeSpan)reader["EndTime"],
                                    FlexibleMinutes = Convert.ToInt32(reader["FlexibleMinutes"]),
                                    LateThresholdMinutes = Convert.ToInt32(reader["LateThresholdMinutes"]),
                                    ShortDayThresholdMinutes = Convert.ToInt32(reader["ShortDayThresholdMinutes"]),
                                    OverTimeStartMinutes = Convert.ToInt32(reader["OverTimeStartMinutes"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء استرجاع فترة العمل رقم: {id}");
            }
            
            return workHours;
        }
        
        /// <summary>
        /// إضافة فترة عمل جديدة
        /// </summary>
        /// <param name="workHours">بيانات فترة العمل</param>
        /// <returns>نجاح العملية</returns>
        public bool AddWorkHours(WorkHoursModel workHours)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = @"
                        INSERT INTO WorkHours (
                            Name,
                            Description,
                            StartTime,
                            EndTime,
                            FlexibleMinutes,
                            LateThresholdMinutes,
                            ShortDayThresholdMinutes,
                            OverTimeStartMinutes,
                            CreatedAt,
                            CreatedBy)
                        VALUES (
                            @Name,
                            @Description,
                            @StartTime,
                            @EndTime,
                            @FlexibleMinutes,
                            @LateThresholdMinutes,
                            @ShortDayThresholdMinutes,
                            @OverTimeStartMinutes,
                            GETDATE(),
                            @CreatedBy);
                        
                        SELECT SCOPE_IDENTITY()";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", workHours.Name);
                        command.Parameters.AddWithValue("@Description", !string.IsNullOrEmpty(workHours.Description) ? (object)workHours.Description : DBNull.Value);
                        command.Parameters.AddWithValue("@StartTime", workHours.StartTime);
                        command.Parameters.AddWithValue("@EndTime", workHours.EndTime);
                        command.Parameters.AddWithValue("@FlexibleMinutes", workHours.FlexibleMinutes);
                        command.Parameters.AddWithValue("@LateThresholdMinutes", workHours.LateThresholdMinutes);
                        command.Parameters.AddWithValue("@ShortDayThresholdMinutes", workHours.ShortDayThresholdMinutes);
                        command.Parameters.AddWithValue("@OverTimeStartMinutes", workHours.OverTimeStartMinutes);
                        command.Parameters.AddWithValue("@CreatedBy", SessionManager.CurrentUser.ID);
                        
                        connection.Open();
                        
                        object result = command.ExecuteScalar();
                        
                        if (result != null && result != DBNull.Value)
                        {
                            workHours.ID = Convert.ToInt32(result);
                            return true;
                        }
                    }
                }
                
                return false;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء إضافة فترة عمل جديدة");
                return false;
            }
        }
        
        /// <summary>
        /// تعديل فترة عمل
        /// </summary>
        /// <param name="workHours">بيانات فترة العمل</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateWorkHours(WorkHoursModel workHours)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = @"
                        UPDATE WorkHours
                        SET 
                            Name = @Name,
                            Description = @Description,
                            StartTime = @StartTime,
                            EndTime = @EndTime,
                            FlexibleMinutes = @FlexibleMinutes,
                            LateThresholdMinutes = @LateThresholdMinutes,
                            ShortDayThresholdMinutes = @ShortDayThresholdMinutes,
                            OverTimeStartMinutes = @OverTimeStartMinutes,
                            UpdatedAt = GETDATE(),
                            UpdatedBy = @UpdatedBy
                        WHERE ID = @ID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", workHours.ID);
                        command.Parameters.AddWithValue("@Name", workHours.Name);
                        command.Parameters.AddWithValue("@Description", !string.IsNullOrEmpty(workHours.Description) ? (object)workHours.Description : DBNull.Value);
                        command.Parameters.AddWithValue("@StartTime", workHours.StartTime);
                        command.Parameters.AddWithValue("@EndTime", workHours.EndTime);
                        command.Parameters.AddWithValue("@FlexibleMinutes", workHours.FlexibleMinutes);
                        command.Parameters.AddWithValue("@LateThresholdMinutes", workHours.LateThresholdMinutes);
                        command.Parameters.AddWithValue("@ShortDayThresholdMinutes", workHours.ShortDayThresholdMinutes);
                        command.Parameters.AddWithValue("@OverTimeStartMinutes", workHours.OverTimeStartMinutes);
                        command.Parameters.AddWithValue("@UpdatedBy", SessionManager.CurrentUser.ID);
                        
                        connection.Open();
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء تعديل فترة العمل رقم: {workHours.ID}");
                return false;
            }
        }
        
        /// <summary>
        /// حذف فترة عمل
        /// </summary>
        /// <param name="id">معرف فترة العمل</param>
        /// <returns>نجاح العملية</returns>
        public bool DeleteWorkHours(int id)
        {
            try
            {
                // التحقق من عدم وجود مناوبات مرتبطة بفترة العمل
                if (HasDependentWorkShifts(id))
                {
                    return false;
                }
                
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = "DELETE FROM WorkHours WHERE ID = @ID";
                    
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
                LogManager.LogException(ex, $"حدث خطأ أثناء حذف فترة العمل رقم: {id}");
                return false;
            }
        }
        
        /// <summary>
        /// التحقق من وجود مناوبات مرتبطة بفترة العمل
        /// </summary>
        /// <param name="id">معرف فترة العمل</param>
        /// <returns>وجود مناوبات مرتبطة</returns>
        private bool HasDependentWorkShifts(int id)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = "SELECT COUNT(*) FROM WorkShifts WHERE WorkHoursID = @WorkHoursID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@WorkHoursID", id);
                        
                        connection.Open();
                        
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء التحقق من ارتباط فترة العمل رقم: {id}");
                return false;
            }
        }
        
        /// <summary>
        /// الحصول على قائمة فترات العمل للاختيار
        /// </summary>
        /// <returns>قائمة فترات العمل</returns>
        public List<WorkHoursListItem> GetWorkHoursDropDownList()
        {
            List<WorkHoursListItem> list = new List<WorkHoursListItem>();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = "SELECT ID, Name FROM WorkHours ORDER BY Name";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new WorkHoursListItem
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
                LogManager.LogException(ex, "حدث خطأ أثناء استرجاع قائمة فترات العمل");
            }
            
            return list;
        }
    }
    
    /// <summary>
    /// عنصر قائمة فترات العمل
    /// </summary>
    public class WorkHoursListItem
    {
        /// <summary>
        /// معرف فترة العمل
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// اسم فترة العمل
        /// </summary>
        public string Name { get; set; }
    }
}