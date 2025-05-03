using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// مخزن الإعدادات
    /// </summary>
    public class SettingsRepository
    {
        private readonly ConnectionManager _connectionManager;
        
        /// <summary>
        /// إنشاء مخزن إعدادات جديد
        /// </summary>
        public SettingsRepository()
        {
            _connectionManager = new ConnectionManager();
        }
        
        #region System Settings
        
        /// <summary>
        /// التحقق من وجود إعداد
        /// </summary>
        /// <param name="key">مفتاح الإعداد</param>
        /// <returns>نتيجة التحقق</returns>
        public bool SettingExists(string key)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT COUNT(*) FROM SystemSettings WHERE SettingKey = @Key";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Key", key);
                        
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء التحقق من وجود الإعداد: {key}");
                return false;
            }
        }
        
        /// <summary>
        /// الحصول على قيمة إعداد
        /// </summary>
        /// <param name="key">مفتاح الإعداد</param>
        /// <returns>قيمة الإعداد</returns>
        public string GetSettingValue(string key)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT SettingValue FROM SystemSettings WHERE SettingKey = @Key";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Key", key);
                        
                        object result = command.ExecuteScalar();
                        return result != null ? result.ToString() : null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء الحصول على قيمة الإعداد: {key}");
                return null;
            }
        }
        
        /// <summary>
        /// إضافة إعداد جديد
        /// </summary>
        /// <param name="key">مفتاح الإعداد</param>
        /// <param name="value">قيمة الإعداد</param>
        /// <returns>نتيجة الإضافة</returns>
        public bool AddSetting(string key, string value)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        INSERT INTO SystemSettings (SettingKey, SettingValue, SettingGroup, Description, LastUpdated, UpdatedBy)
                        VALUES (@Key, @Value, @Group, @Description, @LastUpdated, @UpdatedBy)";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Key", key);
                        command.Parameters.AddWithValue("@Value", value);
                        
                        // استنتاج مجموعة الإعداد من المفتاح
                        string group = "General";
                        if (key.Contains("."))
                        {
                            group = key.Split('.')[0];
                        }
                        command.Parameters.AddWithValue("@Group", group);
                        
                        command.Parameters.AddWithValue("@Description", "");
                        command.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
                        command.Parameters.AddWithValue("@UpdatedBy", 1); // System
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء إضافة الإعداد: {key}");
                return false;
            }
        }
        
        /// <summary>
        /// تحديث إعداد
        /// </summary>
        /// <param name="key">مفتاح الإعداد</param>
        /// <param name="value">قيمة الإعداد</param>
        /// <returns>نتيجة التحديث</returns>
        public bool UpdateSetting(string key, string value)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        UPDATE SystemSettings
                        SET SettingValue = @Value, LastUpdated = @LastUpdated, UpdatedBy = @UpdatedBy
                        WHERE SettingKey = @Key";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Key", key);
                        command.Parameters.AddWithValue("@Value", value);
                        command.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
                        command.Parameters.AddWithValue("@UpdatedBy", 1); // System
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء تحديث الإعداد: {key}");
                return false;
            }
        }
        
        /// <summary>
        /// حذف إعداد
        /// </summary>
        /// <param name="key">مفتاح الإعداد</param>
        /// <returns>نتيجة الحذف</returns>
        public bool DeleteSetting(string key)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "DELETE FROM SystemSettings WHERE SettingKey = @Key";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Key", key);
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء حذف الإعداد: {key}");
                return false;
            }
        }
        
        /// <summary>
        /// الحصول على قائمة الإعدادات حسب المجموعة
        /// </summary>
        /// <param name="group">مجموعة الإعدادات</param>
        /// <returns>قائمة الإعدادات</returns>
        public List<Setting> GetSettingsByGroup(string group)
        {
            try
            {
                List<Setting> settings = new List<Setting>();
                
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT ID, SettingKey, SettingValue, SettingGroup, Description, LastUpdated, UpdatedBy
                        FROM SystemSettings
                        WHERE SettingGroup = @Group
                        ORDER BY SettingKey";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Group", group);
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Setting setting = new Setting
                                {
                                    ID = reader.GetInt32(0),
                                    SettingKey = reader.GetString(1),
                                    SettingValue = reader.GetString(2),
                                    SettingGroup = reader.GetString(3),
                                    Description = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    LastUpdated = reader.GetDateTime(5),
                                    UpdatedBy = reader.GetInt32(6)
                                };
                                
                                settings.Add(setting);
                            }
                        }
                    }
                }
                
                return settings;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء الحصول على قائمة الإعدادات لمجموعة: {group}");
                return null;
            }
        }
        
        /// <summary>
        /// الحصول على قائمة جميع مجموعات الإعدادات
        /// </summary>
        /// <returns>قائمة مجموعات الإعدادات</returns>
        public List<string> GetAllSettingGroups()
        {
            try
            {
                List<string> groups = new List<string>();
                
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT DISTINCT SettingGroup FROM SystemSettings ORDER BY SettingGroup";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                groups.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                
                return groups;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء الحصول على قائمة مجموعات الإعدادات");
                return null;
            }
        }
        
        #endregion
        
        #region Work Periods
        
        /// <summary>
        /// الحصول على قائمة فترات العمل
        /// </summary>
        /// <returns>قائمة فترات العمل</returns>
        public List<WorkPeriod> GetAllWorkPeriods()
        {
            try
            {
                List<WorkPeriod> periods = new List<WorkPeriod>();
                
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT ID, PeriodName, StartTime, EndTime, BreakDurationMinutes, 
                               BreakStartTime, BreakEndTime, WorkHours, GraceMinutes, 
                               PeriodColor, Description, IsActive
                        FROM WorkPeriods
                        ORDER BY PeriodName";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                WorkPeriod period = new WorkPeriod
                                {
                                    ID = reader.GetInt32(0),
                                    PeriodName = reader.GetString(1),
                                    StartTime = reader.GetTimeSpan(2),
                                    EndTime = reader.GetTimeSpan(3),
                                    BreakDurationMinutes = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                    BreakStartTime = reader.IsDBNull(5) ? (TimeSpan?)null : reader.GetTimeSpan(5),
                                    BreakEndTime = reader.IsDBNull(6) ? (TimeSpan?)null : reader.GetTimeSpan(6),
                                    WorkHours = reader.GetDecimal(7),
                                    GraceMinutes = reader.GetInt32(8),
                                    PeriodColor = reader.IsDBNull(9) ? null : reader.GetString(9),
                                    Description = reader.IsDBNull(10) ? null : reader.GetString(10),
                                    IsActive = reader.GetBoolean(11)
                                };
                                
                                periods.Add(period);
                            }
                        }
                    }
                }
                
                return periods;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء الحصول على قائمة فترات العمل");
                return null;
            }
        }
        
        /// <summary>
        /// الحصول على فترة عمل حسب الرقم
        /// </summary>
        /// <param name="periodID">رقم الفترة</param>
        /// <returns>فترة العمل</returns>
        public WorkPeriod GetWorkPeriodByID(int periodID)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT ID, PeriodName, StartTime, EndTime, BreakDurationMinutes, 
                               BreakStartTime, BreakEndTime, WorkHours, GraceMinutes, 
                               PeriodColor, Description, IsActive
                        FROM WorkPeriods
                        WHERE ID = @ID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", periodID);
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                WorkPeriod period = new WorkPeriod
                                {
                                    ID = reader.GetInt32(0),
                                    PeriodName = reader.GetString(1),
                                    StartTime = reader.GetTimeSpan(2),
                                    EndTime = reader.GetTimeSpan(3),
                                    BreakDurationMinutes = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                    BreakStartTime = reader.IsDBNull(5) ? (TimeSpan?)null : reader.GetTimeSpan(5),
                                    BreakEndTime = reader.IsDBNull(6) ? (TimeSpan?)null : reader.GetTimeSpan(6),
                                    WorkHours = reader.GetDecimal(7),
                                    GraceMinutes = reader.GetInt32(8),
                                    PeriodColor = reader.IsDBNull(9) ? null : reader.GetString(9),
                                    Description = reader.IsDBNull(10) ? null : reader.GetString(10),
                                    IsActive = reader.GetBoolean(11)
                                };
                                
                                return period;
                            }
                        }
                    }
                }
                
                return null;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء الحصول على فترة العمل رقم: {periodID}");
                return null;
            }
        }
        
        /// <summary>
        /// إضافة فترة عمل جديدة
        /// </summary>
        /// <param name="period">فترة العمل</param>
        /// <returns>رقم فترة العمل الجديدة</returns>
        public int AddWorkPeriod(WorkPeriod period)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        INSERT INTO WorkPeriods (PeriodName, StartTime, EndTime, BreakDurationMinutes, 
                                               BreakStartTime, BreakEndTime, WorkHours, GraceMinutes, 
                                               PeriodColor, Description, IsActive)
                        VALUES (@PeriodName, @StartTime, @EndTime, @BreakDurationMinutes, 
                               @BreakStartTime, @BreakEndTime, @WorkHours, @GraceMinutes, 
                               @PeriodColor, @Description, @IsActive);
                        SELECT SCOPE_IDENTITY();";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PeriodName", period.PeriodName);
                        command.Parameters.AddWithValue("@StartTime", period.StartTime);
                        command.Parameters.AddWithValue("@EndTime", period.EndTime);
                        command.Parameters.AddWithValue("@BreakDurationMinutes", (object)period.BreakDurationMinutes ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BreakStartTime", (object)period.BreakStartTime ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BreakEndTime", (object)period.BreakEndTime ?? DBNull.Value);
                        command.Parameters.AddWithValue("@WorkHours", period.WorkHours);
                        command.Parameters.AddWithValue("@GraceMinutes", period.GraceMinutes);
                        command.Parameters.AddWithValue("@PeriodColor", (object)period.PeriodColor ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Description", (object)period.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", period.IsActive);
                        
                        decimal result = (decimal)command.ExecuteScalar();
                        return (int)result;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء إضافة فترة العمل: {period.PeriodName}");
                return 0;
            }
        }
        
        /// <summary>
        /// تحديث فترة عمل
        /// </summary>
        /// <param name="period">فترة العمل</param>
        /// <returns>نتيجة التحديث</returns>
        public bool UpdateWorkPeriod(WorkPeriod period)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        UPDATE WorkPeriods
                        SET PeriodName = @PeriodName,
                            StartTime = @StartTime,
                            EndTime = @EndTime,
                            BreakDurationMinutes = @BreakDurationMinutes,
                            BreakStartTime = @BreakStartTime,
                            BreakEndTime = @BreakEndTime,
                            WorkHours = @WorkHours,
                            GraceMinutes = @GraceMinutes,
                            PeriodColor = @PeriodColor,
                            Description = @Description,
                            IsActive = @IsActive
                        WHERE ID = @ID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", period.ID);
                        command.Parameters.AddWithValue("@PeriodName", period.PeriodName);
                        command.Parameters.AddWithValue("@StartTime", period.StartTime);
                        command.Parameters.AddWithValue("@EndTime", period.EndTime);
                        command.Parameters.AddWithValue("@BreakDurationMinutes", (object)period.BreakDurationMinutes ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BreakStartTime", (object)period.BreakStartTime ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BreakEndTime", (object)period.BreakEndTime ?? DBNull.Value);
                        command.Parameters.AddWithValue("@WorkHours", period.WorkHours);
                        command.Parameters.AddWithValue("@GraceMinutes", period.GraceMinutes);
                        command.Parameters.AddWithValue("@PeriodColor", (object)period.PeriodColor ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Description", (object)period.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", period.IsActive);
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء تحديث فترة العمل: {period.PeriodName}");
                return false;
            }
        }
        
        /// <summary>
        /// حذف فترة عمل
        /// </summary>
        /// <param name="periodID">رقم الفترة</param>
        /// <returns>نتيجة الحذف</returns>
        public bool DeleteWorkPeriod(int periodID)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "DELETE FROM WorkPeriods WHERE ID = @ID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", periodID);
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء حذف فترة العمل رقم: {periodID}");
                return false;
            }
        }
        
        #endregion
        
        #region Work Shifts
        
        /// <summary>
        /// الحصول على قائمة ورديات العمل
        /// </summary>
        /// <returns>قائمة ورديات العمل</returns>
        public List<WorkShift> GetAllWorkShifts()
        {
            try
            {
                List<WorkShift> shifts = new List<WorkShift>();
                
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT ID, ShiftName, ShiftType, StartTime, EndTime, CrossDay, 
                               BreakDurationMinutes, BreakStartTime, BreakEndTime, 
                               WorkHours, GraceMinutes, OvertimeMultiplier, 
                               ShiftColor, Description, IsActive
                        FROM WorkShifts
                        ORDER BY ShiftName";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                WorkShift shift = new WorkShift
                                {
                                    ID = reader.GetInt32(0),
                                    ShiftName = reader.GetString(1),
                                    ShiftType = reader.GetString(2),
                                    StartTime = reader.GetTimeSpan(3),
                                    EndTime = reader.GetTimeSpan(4),
                                    CrossDay = reader.GetBoolean(5),
                                    BreakDurationMinutes = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                                    BreakStartTime = reader.IsDBNull(7) ? (TimeSpan?)null : reader.GetTimeSpan(7),
                                    BreakEndTime = reader.IsDBNull(8) ? (TimeSpan?)null : reader.GetTimeSpan(8),
                                    WorkHours = reader.GetDecimal(9),
                                    GraceMinutes = reader.GetInt32(10),
                                    OvertimeMultiplier = reader.IsDBNull(11) ? (decimal?)null : reader.GetDecimal(11),
                                    ShiftColor = reader.IsDBNull(12) ? null : reader.GetString(12),
                                    Description = reader.IsDBNull(13) ? null : reader.GetString(13),
                                    IsActive = reader.GetBoolean(14)
                                };
                                
                                shifts.Add(shift);
                            }
                        }
                    }
                }
                
                return shifts;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء الحصول على قائمة ورديات العمل");
                return null;
            }
        }
        
        /// <summary>
        /// الحصول على وردية عمل حسب الرقم
        /// </summary>
        /// <param name="shiftID">رقم الوردية</param>
        /// <returns>وردية العمل</returns>
        public WorkShift GetWorkShiftByID(int shiftID)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT ID, ShiftName, ShiftType, StartTime, EndTime, CrossDay, 
                               BreakDurationMinutes, BreakStartTime, BreakEndTime, 
                               WorkHours, GraceMinutes, OvertimeMultiplier, 
                               ShiftColor, Description, IsActive
                        FROM WorkShifts
                        WHERE ID = @ID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", shiftID);
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                WorkShift shift = new WorkShift
                                {
                                    ID = reader.GetInt32(0),
                                    ShiftName = reader.GetString(1),
                                    ShiftType = reader.GetString(2),
                                    StartTime = reader.GetTimeSpan(3),
                                    EndTime = reader.GetTimeSpan(4),
                                    CrossDay = reader.GetBoolean(5),
                                    BreakDurationMinutes = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                                    BreakStartTime = reader.IsDBNull(7) ? (TimeSpan?)null : reader.GetTimeSpan(7),
                                    BreakEndTime = reader.IsDBNull(8) ? (TimeSpan?)null : reader.GetTimeSpan(8),
                                    WorkHours = reader.GetDecimal(9),
                                    GraceMinutes = reader.GetInt32(10),
                                    OvertimeMultiplier = reader.IsDBNull(11) ? (decimal?)null : reader.GetDecimal(11),
                                    ShiftColor = reader.IsDBNull(12) ? null : reader.GetString(12),
                                    Description = reader.IsDBNull(13) ? null : reader.GetString(13),
                                    IsActive = reader.GetBoolean(14)
                                };
                                
                                return shift;
                            }
                        }
                    }
                }
                
                return null;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء الحصول على وردية العمل رقم: {shiftID}");
                return null;
            }
        }
        
        /// <summary>
        /// إضافة وردية عمل جديدة
        /// </summary>
        /// <param name="shift">وردية العمل</param>
        /// <returns>رقم وردية العمل الجديدة</returns>
        public int AddWorkShift(WorkShift shift)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        INSERT INTO WorkShifts (ShiftName, ShiftType, StartTime, EndTime, CrossDay, 
                                             BreakDurationMinutes, BreakStartTime, BreakEndTime, 
                                             WorkHours, GraceMinutes, OvertimeMultiplier, 
                                             ShiftColor, Description, IsActive)
                        VALUES (@ShiftName, @ShiftType, @StartTime, @EndTime, @CrossDay, 
                              @BreakDurationMinutes, @BreakStartTime, @BreakEndTime, 
                              @WorkHours, @GraceMinutes, @OvertimeMultiplier, 
                              @ShiftColor, @Description, @IsActive);
                        SELECT SCOPE_IDENTITY();";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ShiftName", shift.ShiftName);
                        command.Parameters.AddWithValue("@ShiftType", shift.ShiftType);
                        command.Parameters.AddWithValue("@StartTime", shift.StartTime);
                        command.Parameters.AddWithValue("@EndTime", shift.EndTime);
                        command.Parameters.AddWithValue("@CrossDay", shift.CrossDay);
                        command.Parameters.AddWithValue("@BreakDurationMinutes", (object)shift.BreakDurationMinutes ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BreakStartTime", (object)shift.BreakStartTime ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BreakEndTime", (object)shift.BreakEndTime ?? DBNull.Value);
                        command.Parameters.AddWithValue("@WorkHours", shift.WorkHours);
                        command.Parameters.AddWithValue("@GraceMinutes", shift.GraceMinutes);
                        command.Parameters.AddWithValue("@OvertimeMultiplier", (object)shift.OvertimeMultiplier ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ShiftColor", (object)shift.ShiftColor ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Description", (object)shift.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", shift.IsActive);
                        
                        decimal result = (decimal)command.ExecuteScalar();
                        return (int)result;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء إضافة وردية العمل: {shift.ShiftName}");
                return 0;
            }
        }
        
        /// <summary>
        /// تحديث وردية عمل
        /// </summary>
        /// <param name="shift">وردية العمل</param>
        /// <returns>نتيجة التحديث</returns>
        public bool UpdateWorkShift(WorkShift shift)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        UPDATE WorkShifts
                        SET ShiftName = @ShiftName,
                            ShiftType = @ShiftType,
                            StartTime = @StartTime,
                            EndTime = @EndTime,
                            CrossDay = @CrossDay,
                            BreakDurationMinutes = @BreakDurationMinutes,
                            BreakStartTime = @BreakStartTime,
                            BreakEndTime = @BreakEndTime,
                            WorkHours = @WorkHours,
                            GraceMinutes = @GraceMinutes,
                            OvertimeMultiplier = @OvertimeMultiplier,
                            ShiftColor = @ShiftColor,
                            Description = @Description,
                            IsActive = @IsActive
                        WHERE ID = @ID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", shift.ID);
                        command.Parameters.AddWithValue("@ShiftName", shift.ShiftName);
                        command.Parameters.AddWithValue("@ShiftType", shift.ShiftType);
                        command.Parameters.AddWithValue("@StartTime", shift.StartTime);
                        command.Parameters.AddWithValue("@EndTime", shift.EndTime);
                        command.Parameters.AddWithValue("@CrossDay", shift.CrossDay);
                        command.Parameters.AddWithValue("@BreakDurationMinutes", (object)shift.BreakDurationMinutes ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BreakStartTime", (object)shift.BreakStartTime ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BreakEndTime", (object)shift.BreakEndTime ?? DBNull.Value);
                        command.Parameters.AddWithValue("@WorkHours", shift.WorkHours);
                        command.Parameters.AddWithValue("@GraceMinutes", shift.GraceMinutes);
                        command.Parameters.AddWithValue("@OvertimeMultiplier", (object)shift.OvertimeMultiplier ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ShiftColor", (object)shift.ShiftColor ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Description", (object)shift.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", shift.IsActive);
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء تحديث وردية العمل: {shift.ShiftName}");
                return false;
            }
        }
        
        /// <summary>
        /// حذف وردية عمل
        /// </summary>
        /// <param name="shiftID">رقم الوردية</param>
        /// <returns>نتيجة الحذف</returns>
        public bool DeleteWorkShift(int shiftID)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "DELETE FROM WorkShifts WHERE ID = @ID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", shiftID);
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء حذف وردية العمل رقم: {shiftID}");
                return false;
            }
        }
        
        #endregion
    }
    
    /// <summary>
    /// إعداد النظام
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// الرقم
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// مفتاح الإعداد
        /// </summary>
        public string SettingKey { get; set; }
        
        /// <summary>
        /// قيمة الإعداد
        /// </summary>
        public string SettingValue { get; set; }
        
        /// <summary>
        /// مجموعة الإعداد
        /// </summary>
        public string SettingGroup { get; set; }
        
        /// <summary>
        /// وصف الإعداد
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// تاريخ آخر تحديث
        /// </summary>
        public DateTime LastUpdated { get; set; }
        
        /// <summary>
        /// بواسطة
        /// </summary>
        public int UpdatedBy { get; set; }
    }
}