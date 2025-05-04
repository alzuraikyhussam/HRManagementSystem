using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// مستودع بيانات الحضور والانصراف
    /// </summary>
    public class AttendanceRepository
    {
        private readonly ConnectionManager _connectionManager;
        
        /// <summary>
        /// إنشاء مستودع بيانات الحضور
        /// </summary>
        public AttendanceRepository()
        {
            _connectionManager = ConnectionManager.Instance;
        }
        
        #region Attendance Records
        
        /// <summary>
        /// الحصول على سجلات الحضور في فترة زمنية محددة
        /// </summary>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <returns>قائمة سجلات الحضور</returns>
        public List<AttendanceRecord> GetAttendanceRecords(DateTime startDate, DateTime endDate)
        {
            List<AttendanceRecord> records = new List<AttendanceRecord>();
            
            string query = @"
                SELECT 
                    a.ID, a.EmployeeID, a.AttendanceDate, a.TimeIn, a.TimeOut, 
                    a.WorkHoursID, a.LateMinutes, a.EarlyDepartureMinutes, 
                    a.OvertimeMinutes, a.WorkedMinutes, a.Status, a.IsManualEntry, 
                    a.Notes, a.CreatedAt, a.CreatedBy,
                    e.FullName AS EmployeeName,
                    d.Name AS DepartmentName,
                    u.Username AS CreatedByUser
                FROM AttendanceRecords a
                LEFT JOIN Employees e ON a.EmployeeID = e.ID
                LEFT JOIN Departments d ON e.DepartmentID = d.ID
                LEFT JOIN Users u ON a.CreatedBy = u.ID
                WHERE a.AttendanceDate BETWEEN @StartDate AND @EndDate
                ORDER BY a.AttendanceDate DESC, e.FullName";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.Date);
                    command.Parameters.AddWithValue("@EndDate", endDate.Date);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(MapAttendanceRecordFromReader(reader));
                        }
                    }
                }
            }
            
            return records;
        }
        
        /// <summary>
        /// الحصول على سجلات الحضور لموظف في فترة زمنية محددة
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <returns>قائمة سجلات الحضور</returns>
        public List<AttendanceRecord> GetEmployeeAttendanceRecords(int employeeId, DateTime startDate, DateTime endDate)
        {
            List<AttendanceRecord> records = new List<AttendanceRecord>();
            
            string query = @"
                SELECT 
                    a.ID, a.EmployeeID, a.AttendanceDate, a.TimeIn, a.TimeOut, 
                    a.WorkHoursID, a.LateMinutes, a.EarlyDepartureMinutes, 
                    a.OvertimeMinutes, a.WorkedMinutes, a.Status, a.IsManualEntry, 
                    a.Notes, a.CreatedAt, a.CreatedBy,
                    e.FullName AS EmployeeName,
                    d.Name AS DepartmentName,
                    u.Username AS CreatedByUser
                FROM AttendanceRecords a
                LEFT JOIN Employees e ON a.EmployeeID = e.ID
                LEFT JOIN Departments d ON e.DepartmentID = d.ID
                LEFT JOIN Users u ON a.CreatedBy = u.ID
                WHERE a.EmployeeID = @EmployeeID AND a.AttendanceDate BETWEEN @StartDate AND @EndDate
                ORDER BY a.AttendanceDate DESC";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@StartDate", startDate.Date);
                    command.Parameters.AddWithValue("@EndDate", endDate.Date);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(MapAttendanceRecordFromReader(reader));
                        }
                    }
                }
            }
            
            return records;
        }
        
        /// <summary>
        /// الحصول على سجل حضور لموظف في تاريخ محدد
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="date">التاريخ</param>
        /// <returns>سجل الحضور</returns>
        public AttendanceRecord GetAttendanceRecordByEmployeeAndDate(int employeeId, DateTime date)
        {
            string query = @"
                SELECT 
                    a.ID, a.EmployeeID, a.AttendanceDate, a.TimeIn, a.TimeOut, 
                    a.WorkHoursID, a.LateMinutes, a.EarlyDepartureMinutes, 
                    a.OvertimeMinutes, a.WorkedMinutes, a.Status, a.IsManualEntry, 
                    a.Notes, a.CreatedAt, a.CreatedBy,
                    e.FullName AS EmployeeName,
                    d.Name AS DepartmentName,
                    u.Username AS CreatedByUser
                FROM AttendanceRecords a
                LEFT JOIN Employees e ON a.EmployeeID = e.ID
                LEFT JOIN Departments d ON e.DepartmentID = d.ID
                LEFT JOIN Users u ON a.CreatedBy = u.ID
                WHERE a.EmployeeID = @EmployeeID AND a.AttendanceDate = @Date";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@Date", date.Date);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapAttendanceRecordFromReader(reader);
                        }
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// الحصول على تقرير الحضور لموظف في شهر محدد
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="year">السنة</param>
        /// <param name="month">الشهر</param>
        /// <returns>ملخص الحضور</returns>
        public AttendanceSummary GetEmployeeMonthlyAttendanceSummary(int employeeId, int year, int month)
        {
            var summary = new AttendanceSummary
            {
                EmployeeID = employeeId,
                Year = year,
                Month = month
            };
            
            // تحديد الفترة الزمنية للشهر
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                connection.Open();
                
                // استعلام لاستخراج ملخص الحضور
                string query = @"
                    SELECT 
                        COUNT(*) AS TotalDays,
                        SUM(CASE WHEN Status LIKE N'%حاضر%' THEN 1 ELSE 0 END) AS PresentDays,
                        SUM(CASE WHEN Status LIKE N'%غائب%' THEN 1 ELSE 0 END) AS AbsentDays,
                        SUM(CASE WHEN Status LIKE N'%متأخر%' THEN 1 ELSE 0 END) AS LateDays,
                        SUM(CASE WHEN Status LIKE N'%مغادرة مبكرة%' THEN 1 ELSE 0 END) AS EarlyDepartureDays,
                        SUM(LateMinutes) AS TotalLateMinutes,
                        SUM(EarlyDepartureMinutes) AS TotalEarlyDepartureMinutes,
                        SUM(OvertimeMinutes) AS TotalOvertimeMinutes,
                        SUM(WorkedMinutes) AS TotalWorkedMinutes
                    FROM AttendanceRecords 
                    WHERE EmployeeID = @EmployeeID 
                    AND AttendanceDate BETWEEN @StartDate AND @EndDate";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            summary.TotalDays = Convert.ToInt32(reader["TotalDays"]);
                            summary.PresentDays = Convert.ToInt32(reader["PresentDays"]);
                            summary.AbsentDays = Convert.ToInt32(reader["AbsentDays"]);
                            summary.LateDays = Convert.ToInt32(reader["LateDays"]);
                            summary.EarlyDepartureDays = Convert.ToInt32(reader["EarlyDepartureDays"]);
                            summary.TotalLateMinutes = Convert.ToInt32(reader["TotalLateMinutes"]);
                            summary.TotalEarlyDepartureMinutes = Convert.ToInt32(reader["TotalEarlyDepartureMinutes"]);
                            summary.TotalOvertimeMinutes = Convert.ToInt32(reader["TotalOvertimeMinutes"]);
                            summary.TotalWorkedMinutes = Convert.ToInt32(reader["TotalWorkedMinutes"]);
                        }
                    }
                }
                
                // الحصول على عدد أيام الإجازات في الشهر
                string leaveQuery = @"
                    SELECT COUNT(*) AS LeaveCount
                    FROM LeaveRequests
                    WHERE EmployeeID = @EmployeeID 
                    AND Status = 'Approved'
                    AND (
                        (StartDate BETWEEN @StartDate AND @EndDate) OR 
                        (EndDate BETWEEN @StartDate AND @EndDate) OR 
                        (StartDate <= @StartDate AND EndDate >= @EndDate)
                    )";
                
                using (SqlCommand leaveCommand = new SqlCommand(leaveQuery, connection))
                {
                    leaveCommand.Parameters.AddWithValue("@EmployeeID", employeeId);
                    leaveCommand.Parameters.AddWithValue("@StartDate", startDate);
                    leaveCommand.Parameters.AddWithValue("@EndDate", endDate);
                    
                    var result = leaveCommand.ExecuteScalar();
                    summary.LeaveDays = Convert.ToInt32(result);
                }
                
                // الحصول على عدد أيام التصاريح في الشهر
                string permissionQuery = @"
                    SELECT COUNT(*) AS PermissionCount
                    FROM AttendancePermissions
                    WHERE EmployeeID = @EmployeeID 
                    AND Status = 'Approved'
                    AND PermissionDate BETWEEN @StartDate AND @EndDate";
                
                using (SqlCommand permissionCommand = new SqlCommand(permissionQuery, connection))
                {
                    permissionCommand.Parameters.AddWithValue("@EmployeeID", employeeId);
                    permissionCommand.Parameters.AddWithValue("@StartDate", startDate);
                    permissionCommand.Parameters.AddWithValue("@EndDate", endDate);
                    
                    var result = permissionCommand.ExecuteScalar();
                    summary.PermissionDays = Convert.ToInt32(result);
                }
                
                // إضافة: الحصول على ملخص لساعات العمل حسب الأسبوع
                string weeklyQuery = @"
                    SELECT 
                        DATEPART(wk, AttendanceDate) AS WeekNumber,
                        SUM(WorkedMinutes) AS TotalMinutes
                    FROM AttendanceRecords 
                    WHERE EmployeeID = @EmployeeID 
                    AND AttendanceDate BETWEEN @StartDate AND @EndDate
                    GROUP BY DATEPART(wk, AttendanceDate)
                    ORDER BY WeekNumber";
                
                using (SqlCommand weeklyCommand = new SqlCommand(weeklyQuery, connection))
                {
                    weeklyCommand.Parameters.AddWithValue("@EmployeeID", employeeId);
                    weeklyCommand.Parameters.AddWithValue("@StartDate", startDate);
                    weeklyCommand.Parameters.AddWithValue("@EndDate", endDate);
                    
                    summary.WeeklyHours = new Dictionary<int, decimal>();
                    
                    using (SqlDataReader reader = weeklyCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int weekNumber = Convert.ToInt32(reader["WeekNumber"]);
                            int minutes = Convert.ToInt32(reader["TotalMinutes"]);
                            summary.WeeklyHours[weekNumber] = Math.Round((decimal)minutes / 60, 2);
                        }
                    }
                }
                
                // إضافة: الحصول على ملخص لأنماط الحضور حسب أيام الأسبوع
                string dayOfWeekQuery = @"
                    SELECT 
                        DATEPART(dw, AttendanceDate) AS DayOfWeek,
                        AVG(CASE WHEN TimeIn IS NOT NULL THEN 
                            DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), CAST(TimeIn AS TIME)) 
                            ELSE NULL END) AS AvgTimeIn,
                        AVG(CASE WHEN TimeOut IS NOT NULL THEN 
                            DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), CAST(TimeOut AS TIME)) 
                            ELSE NULL END) AS AvgTimeOut,
                        COUNT(*) AS DayCount,
                        SUM(CASE WHEN Status LIKE N'%متأخر%' THEN 1 ELSE 0 END) AS LateCount
                    FROM AttendanceRecords 
                    WHERE EmployeeID = @EmployeeID 
                    AND AttendanceDate BETWEEN @StartDate AND @EndDate
                    GROUP BY DATEPART(dw, AttendanceDate)
                    ORDER BY DayOfWeek";
                
                using (SqlCommand dayOfWeekCommand = new SqlCommand(dayOfWeekQuery, connection))
                {
                    dayOfWeekCommand.Parameters.AddWithValue("@EmployeeID", employeeId);
                    dayOfWeekCommand.Parameters.AddWithValue("@StartDate", startDate);
                    dayOfWeekCommand.Parameters.AddWithValue("@EndDate", endDate);
                    
                    summary.DayOfWeekPatterns = new List<DayOfWeekPattern>();
                    
                    using (SqlDataReader reader = dayOfWeekCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int dayOfWeek = Convert.ToInt32(reader["DayOfWeek"]);
                            object avgTimeInObj = reader["AvgTimeIn"];
                            object avgTimeOutObj = reader["AvgTimeOut"];
                            int dayCount = Convert.ToInt32(reader["DayCount"]);
                            int lateCount = Convert.ToInt32(reader["LateCount"]);
                            
                            var pattern = new DayOfWeekPattern
                            {
                                DayOfWeek = dayOfWeek,
                                DayCount = dayCount
                            };
                            
                            if (avgTimeInObj != DBNull.Value)
                            {
                                int minutes = Convert.ToInt32(avgTimeInObj);
                                pattern.AverageTimeIn = TimeSpan.FromMinutes(minutes);
                            }
                            
                            if (avgTimeOutObj != DBNull.Value)
                            {
                                int minutes = Convert.ToInt32(avgTimeOutObj);
                                pattern.AverageTimeOut = TimeSpan.FromMinutes(minutes);
                            }
                            
                            pattern.LateFrequency = dayCount > 0 ? (decimal)lateCount / dayCount : 0;
                            
                            summary.DayOfWeekPatterns.Add(pattern);
                        }
                    }
                }
            }
            
            // الحصول على اسم الموظف والقسم
            var employee = new EmployeeRepository().GetEmployeeById(employeeId);
            if (employee != null)
            {
                summary.EmployeeName = employee.FullName;
                summary.DepartmentName = employee.DepartmentName;
            }
            
            // إضافة: حساب الإحصائيات المتقدمة
            CalculateAdvancedStatistics(summary);
            
            return summary;
        }
        
        /// <summary>
        /// حساب الإحصائيات المتقدمة لملخص الحضور
        /// </summary>
        private void CalculateAdvancedStatistics(AttendanceSummary summary)
        {
            // حساب متوسط ساعات العمل اليومية
            if (summary.PresentDays > 0)
            {
                summary.AverageDailyWorkHours = Math.Round((decimal)summary.TotalWorkedMinutes / (summary.PresentDays * 60), 2);
            }
            
            // حساب معدل الالتزام بوقت الحضور
            int totalDaysWithAttendance = summary.PresentDays + summary.LateDays;
            if (totalDaysWithAttendance > 0)
            {
                summary.PunctualityRate = Math.Round(((decimal)(totalDaysWithAttendance - summary.LateDays) / totalDaysWithAttendance) * 100, 2);
            }
            
            // حساب معدل الالتزام بوقت الانصراف
            if (totalDaysWithAttendance > 0)
            {
                summary.DepartureComplianceRate = Math.Round(((decimal)(totalDaysWithAttendance - summary.EarlyDepartureDays) / totalDaysWithAttendance) * 100, 2);
            }
            
            // حساب معدل الالتزام العام
            summary.OverallComplianceRate = Math.Round(((decimal)summary.PresentDays / (summary.TotalDays)) * 100, 2);
            
            // حساب نسبة الساعات الإضافية
            int regularMinutes = summary.PresentDays * 8 * 60; // 8 ساعات لكل يوم عمل
            if (regularMinutes > 0)
            {
                summary.OvertimeRate = Math.Round(((decimal)summary.TotalOvertimeMinutes / regularMinutes) * 100, 2);
            }
            
            // تحديد اليوم الأكثر تأخيراً في الأسبوع
            if (summary.DayOfWeekPatterns != null && summary.DayOfWeekPatterns.Count > 0)
            {
                var mostLateDayPattern = summary.DayOfWeekPatterns
                    .OrderByDescending(p => p.LateFrequency)
                    .FirstOrDefault();
                    
                if (mostLateDayPattern != null)
                {
                    summary.MostLateDayOfWeek = mostLateDayPattern.DayOfWeek;
                    summary.MostLateDayFrequency = mostLateDayPattern.LateFrequency;
                }
            }
        }
        
        /// <summary>
        /// الحصول على عدد الموظفين الحاضرين اليوم
        /// </summary>
        public int GetEmployeesPresentCount(DateTime date)
        {
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM AttendanceRecords 
                    WHERE AttendanceDate = @Date 
                    AND TimeIn IS NOT NULL 
                    AND Status NOT LIKE N'%غائب%' 
                    AND Status NOT LIKE N'%إجازة%'";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", date.Date);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// الحصول على عدد الموظفين المتأخرين اليوم
        /// </summary>
        public int GetEmployeesLateCount(DateTime date)
        {
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM AttendanceRecords 
                    WHERE AttendanceDate = @Date 
                    AND (Status LIKE N'%متأخر%' OR LateMinutes > 0)";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", date.Date);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// الحصول على عدد الموظفين المغادرين مبكراً اليوم
        /// </summary>
        public int GetEmployeesEarlyDepartureCount(DateTime date)
        {
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM AttendanceRecords 
                    WHERE AttendanceDate = @Date 
                    AND (Status LIKE N'%مغادرة مبكرة%' OR EarlyDepartureMinutes > 0)";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", date.Date);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// الحصول على إجمالي أيام الحضور في الشهر الماضي
        /// </summary>
        public int GetTotalAttendanceDaysLastMonth()
        {
            DateTime firstDayLastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            DateTime lastDayLastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM AttendanceRecords 
                    WHERE AttendanceDate BETWEEN @StartDate AND @EndDate 
                    AND TimeIn IS NOT NULL 
                    AND Status NOT LIKE N'%غائب%'";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", firstDayLastMonth);
                    command.Parameters.AddWithValue("@EndDate", lastDayLastMonth);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// الحصول على متوسط دقائق التأخير للفترة المحددة
        /// </summary>
        public decimal GetAverageLateMinutes(DateTime fromDate, DateTime toDate)
        {
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                string query = @"
                    SELECT AVG(CAST(LateMinutes AS FLOAT)) 
                    FROM AttendanceRecords 
                    WHERE AttendanceDate BETWEEN @StartDate AND @EndDate 
                    AND LateMinutes > 0";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", fromDate.Date);
                    command.Parameters.AddWithValue("@EndDate", toDate.Date);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToDecimal(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// الحصول على متوسط ساعات العمل اليومية للفترة المحددة
        /// </summary>
        public decimal GetAverageDailyWorkHours(DateTime fromDate, DateTime toDate)
        {
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                string query = @"
                    SELECT AVG(CAST(WorkedMinutes AS FLOAT) / 60.0) 
                    FROM AttendanceRecords 
                    WHERE AttendanceDate BETWEEN @StartDate AND @EndDate 
                    AND WorkedMinutes > 0";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", fromDate.Date);
                    command.Parameters.AddWithValue("@EndDate", toDate.Date);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToDecimal(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// إضافة سجل حضور جديد
        /// </summary>
        /// <param name="record">بيانات سجل الحضور</param>
        /// <returns>معرف السجل الجديد</returns>
        public int AddAttendanceRecord(AttendanceRecord record)
        {
            string query = @"
                INSERT INTO AttendanceRecords (
                    EmployeeID, AttendanceDate, TimeIn, TimeOut, WorkHoursID, 
                    LateMinutes, EarlyDepartureMinutes, OvertimeMinutes, WorkedMinutes, 
                    Status, IsManualEntry, Notes, CreatedAt, CreatedBy
                ) 
                VALUES (
                    @EmployeeID, @AttendanceDate, @TimeIn, @TimeOut, @WorkHoursID, 
                    @LateMinutes, @EarlyDepartureMinutes, @OvertimeMinutes, @WorkedMinutes, 
                    @Status, @IsManualEntry, @Notes, @CreatedAt, @CreatedBy
                );
                SELECT SCOPE_IDENTITY();";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddAttendanceRecordParameters(command, record);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// تحديث سجل حضور
        /// </summary>
        /// <param name="record">بيانات سجل الحضور</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateAttendanceRecord(AttendanceRecord record)
        {
            string query = @"
                UPDATE AttendanceRecords
                SET 
                    EmployeeID = @EmployeeID,
                    AttendanceDate = @AttendanceDate,
                    TimeIn = @TimeIn,
                    TimeOut = @TimeOut,
                    WorkHoursID = @WorkHoursID,
                    LateMinutes = @LateMinutes,
                    EarlyDepartureMinutes = @EarlyDepartureMinutes,
                    OvertimeMinutes = @OvertimeMinutes,
                    WorkedMinutes = @WorkedMinutes,
                    Status = @Status,
                    IsManualEntry = @IsManualEntry,
                    Notes = @Notes,
                    CreatedAt = @CreatedAt,
                    CreatedBy = @CreatedBy
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", record.ID);
                    AddAttendanceRecordParameters(command, record);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// حذف سجل حضور
        /// </summary>
        /// <param name="id">معرف السجل</param>
        /// <returns>نجاح العملية</returns>
        public bool DeleteAttendanceRecord(int id)
        {
            string query = "DELETE FROM AttendanceRecords WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        #endregion
        
        #region Attendance Permissions
        
        /// <summary>
        /// الحصول على جميع تصاريح الحضور
        /// </summary>
        /// <returns>قائمة تصاريح الحضور</returns>
        public List<AttendancePermission> GetAttendancePermissions()
        {
            List<AttendancePermission> permissions = new List<AttendancePermission>();
            
            string query = @"
                SELECT 
                    p.ID, p.EmployeeID, p.PermissionDate, p.PermissionType, 
                    p.StartTime, p.EndTime, p.TotalMinutes, p.Reason, 
                    p.Status, p.ApprovedBy, p.ApprovalDate, p.Notes, 
                    p.CreatedAt, p.CreatedBy, p.UpdatedAt,
                    e.FullName AS EmployeeName,
                    d.Name AS DepartmentName,
                    u1.Username AS CreatedByUser,
                    u2.Username AS ApprovedByUser
                FROM AttendancePermissions p
                LEFT JOIN Employees e ON p.EmployeeID = e.ID
                LEFT JOIN Departments d ON e.DepartmentID = d.ID
                LEFT JOIN Users u1 ON p.CreatedBy = u1.ID
                LEFT JOIN Users u2 ON p.ApprovedBy = u2.ID
                ORDER BY p.PermissionDate DESC, e.FullName";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            permissions.Add(MapAttendancePermissionFromReader(reader));
                        }
                    }
                }
            }
            
            return permissions;
        }
        
        /// <summary>
        /// الحصول على تصاريح الحضور لموظف
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <returns>قائمة تصاريح الحضور</returns>
        public List<AttendancePermission> GetEmployeeAttendancePermissions(int employeeId)
        {
            List<AttendancePermission> permissions = new List<AttendancePermission>();
            
            string query = @"
                SELECT 
                    p.ID, p.EmployeeID, p.PermissionDate, p.PermissionType, 
                    p.StartTime, p.EndTime, p.TotalMinutes, p.Reason, 
                    p.Status, p.ApprovedBy, p.ApprovalDate, p.Notes, 
                    p.CreatedAt, p.CreatedBy, p.UpdatedAt,
                    e.FullName AS EmployeeName,
                    d.Name AS DepartmentName,
                    u1.Username AS CreatedByUser,
                    u2.Username AS ApprovedByUser
                FROM AttendancePermissions p
                LEFT JOIN Employees e ON p.EmployeeID = e.ID
                LEFT JOIN Departments d ON e.DepartmentID = d.ID
                LEFT JOIN Users u1 ON p.CreatedBy = u1.ID
                LEFT JOIN Users u2 ON p.ApprovedBy = u2.ID
                WHERE p.EmployeeID = @EmployeeID
                ORDER BY p.PermissionDate DESC";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            permissions.Add(MapAttendancePermissionFromReader(reader));
                        }
                    }
                }
            }
            
            return permissions;
        }
        
        /// <summary>
        /// الحصول على تصريح حضور بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف التصريح</param>
        /// <returns>تصريح الحضور</returns>
        public AttendancePermission GetAttendancePermissionById(int id)
        {
            string query = @"
                SELECT 
                    p.ID, p.EmployeeID, p.PermissionDate, p.PermissionType, 
                    p.StartTime, p.EndTime, p.TotalMinutes, p.Reason, 
                    p.Status, p.ApprovedBy, p.ApprovalDate, p.Notes, 
                    p.CreatedAt, p.CreatedBy, p.UpdatedAt,
                    e.FullName AS EmployeeName,
                    d.Name AS DepartmentName,
                    u1.Username AS CreatedByUser,
                    u2.Username AS ApprovedByUser
                FROM AttendancePermissions p
                LEFT JOIN Employees e ON p.EmployeeID = e.ID
                LEFT JOIN Departments d ON e.DepartmentID = d.ID
                LEFT JOIN Users u1 ON p.CreatedBy = u1.ID
                LEFT JOIN Users u2 ON p.ApprovedBy = u2.ID
                WHERE p.ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapAttendancePermissionFromReader(reader);
                        }
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// إضافة تصريح حضور جديد
        /// </summary>
        /// <param name="permission">بيانات التصريح</param>
        /// <returns>معرف التصريح الجديد</returns>
        public int AddAttendancePermission(AttendancePermission permission)
        {
            string query = @"
                INSERT INTO AttendancePermissions (
                    EmployeeID, PermissionDate, PermissionType, StartTime, EndTime, 
                    TotalMinutes, Reason, Status, ApprovedBy, ApprovalDate, 
                    Notes, CreatedAt, CreatedBy, UpdatedAt
                ) 
                VALUES (
                    @EmployeeID, @PermissionDate, @PermissionType, @StartTime, @EndTime, 
                    @TotalMinutes, @Reason, @Status, @ApprovedBy, @ApprovalDate, 
                    @Notes, @CreatedAt, @CreatedBy, @UpdatedAt
                );
                SELECT SCOPE_IDENTITY();";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddAttendancePermissionParameters(command, permission);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// تحديث تصريح حضور
        /// </summary>
        /// <param name="permission">بيانات التصريح</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateAttendancePermission(AttendancePermission permission)
        {
            string query = @"
                UPDATE AttendancePermissions
                SET 
                    EmployeeID = @EmployeeID,
                    PermissionDate = @PermissionDate,
                    PermissionType = @PermissionType,
                    StartTime = @StartTime,
                    EndTime = @EndTime,
                    TotalMinutes = @TotalMinutes,
                    Reason = @Reason,
                    Status = @Status,
                    ApprovedBy = @ApprovedBy,
                    ApprovalDate = @ApprovalDate,
                    Notes = @Notes,
                    UpdatedAt = @UpdatedAt
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", permission.ID);
                    AddAttendancePermissionParameters(command, permission);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// حذف تصريح حضور
        /// </summary>
        /// <param name="id">معرف التصريح</param>
        /// <returns>نجاح العملية</returns>
        public bool DeleteAttendancePermission(int id)
        {
            string query = "DELETE FROM AttendancePermissions WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// الموافقة على تصريح حضور
        /// </summary>
        /// <param name="id">معرف التصريح</param>
        /// <param name="approvedBy">معرف المستخدم الموافق</param>
        /// <param name="notes">ملاحظات (اختياري)</param>
        /// <returns>نجاح العملية</returns>
        public bool ApproveAttendancePermission(int id, int approvedBy, string notes = null)
        {
            string query = @"
                UPDATE AttendancePermissions
                SET 
                    Status = 'Approved',
                    ApprovedBy = @ApprovedBy,
                    ApprovalDate = @ApprovalDate,
                    Notes = CASE WHEN @Notes IS NULL THEN Notes ELSE @Notes END,
                    UpdatedAt = @UpdatedAt
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@ApprovedBy", approvedBy);
                    command.Parameters.AddWithValue("@ApprovalDate", DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// رفض تصريح حضور
        /// </summary>
        /// <param name="id">معرف التصريح</param>
        /// <param name="approvedBy">معرف المستخدم الرافض</param>
        /// <param name="notes">ملاحظات (اختياري)</param>
        /// <returns>نجاح العملية</returns>
        public bool RejectAttendancePermission(int id, int approvedBy, string notes = null)
        {
            string query = @"
                UPDATE AttendancePermissions
                SET 
                    Status = 'Rejected',
                    ApprovedBy = @ApprovedBy,
                    ApprovalDate = @ApprovalDate,
                    Notes = CASE WHEN @Notes IS NULL THEN Notes ELSE @Notes END,
                    UpdatedAt = @UpdatedAt
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@ApprovedBy", approvedBy);
                    command.Parameters.AddWithValue("@ApprovalDate", DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// إضافة بارامترات سجل الحضور إلى الكوماند
        /// </summary>
        /// <param name="command">الكوماند</param>
        /// <param name="record">بيانات سجل الحضور</param>
        private void AddAttendanceRecordParameters(SqlCommand command, AttendanceRecord record)
        {
            command.Parameters.AddWithValue("@EmployeeID", record.EmployeeID);
            command.Parameters.AddWithValue("@AttendanceDate", record.AttendanceDate);
            command.Parameters.AddWithValue("@TimeIn", (object)record.TimeIn ?? DBNull.Value);
            command.Parameters.AddWithValue("@TimeOut", (object)record.TimeOut ?? DBNull.Value);
            command.Parameters.AddWithValue("@WorkHoursID", (object)record.WorkHoursID ?? DBNull.Value);
            command.Parameters.AddWithValue("@LateMinutes", record.LateMinutes);
            command.Parameters.AddWithValue("@EarlyDepartureMinutes", record.EarlyDepartureMinutes);
            command.Parameters.AddWithValue("@OvertimeMinutes", record.OvertimeMinutes);
            command.Parameters.AddWithValue("@WorkedMinutes", record.WorkedMinutes);
            command.Parameters.AddWithValue("@Status", (object)record.Status ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsManualEntry", record.IsManualEntry);
            command.Parameters.AddWithValue("@Notes", (object)record.Notes ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreatedAt", record.CreatedAt);
            command.Parameters.AddWithValue("@CreatedBy", (object)record.CreatedBy ?? DBNull.Value);
        }
        
        /// <summary>
        /// إضافة بارامترات تصريح الحضور إلى الكوماند
        /// </summary>
        /// <param name="command">الكوماند</param>
        /// <param name="permission">بيانات تصريح الحضور</param>
        private void AddAttendancePermissionParameters(SqlCommand command, AttendancePermission permission)
        {
            command.Parameters.AddWithValue("@EmployeeID", permission.EmployeeID);
            command.Parameters.AddWithValue("@PermissionDate", permission.PermissionDate);
            command.Parameters.AddWithValue("@PermissionType", permission.PermissionType);
            command.Parameters.AddWithValue("@StartTime", (object)permission.StartTime ?? DBNull.Value);
            command.Parameters.AddWithValue("@EndTime", (object)permission.EndTime ?? DBNull.Value);
            command.Parameters.AddWithValue("@TotalMinutes", (object)permission.TotalMinutes ?? DBNull.Value);
            command.Parameters.AddWithValue("@Reason", (object)permission.Reason ?? DBNull.Value);
            command.Parameters.AddWithValue("@Status", permission.Status);
            command.Parameters.AddWithValue("@ApprovedBy", (object)permission.ApprovedBy ?? DBNull.Value);
            command.Parameters.AddWithValue("@ApprovalDate", (object)permission.ApprovalDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@Notes", (object)permission.Notes ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreatedAt", permission.CreatedAt);
            command.Parameters.AddWithValue("@CreatedBy", (object)permission.CreatedBy ?? DBNull.Value);
            command.Parameters.AddWithValue("@UpdatedAt", (object)permission.UpdatedAt ?? DBNull.Value);
        }
        
        /// <summary>
        /// تحويل من قارئ البيانات إلى كائن سجل حضور
        /// </summary>
        /// <param name="reader">قارئ البيانات</param>
        /// <returns>كائن سجل حضور</returns>
        private AttendanceRecord MapAttendanceRecordFromReader(SqlDataReader reader)
        {
            return new AttendanceRecord
            {
                ID = Convert.ToInt32(reader["ID"]),
                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                EmployeeName = reader["EmployeeName"] != DBNull.Value ? reader["EmployeeName"].ToString() : null,
                DepartmentName = reader["DepartmentName"] != DBNull.Value ? reader["DepartmentName"].ToString() : null,
                AttendanceDate = Convert.ToDateTime(reader["AttendanceDate"]),
                TimeIn = reader["TimeIn"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["TimeIn"]) : null,
                TimeOut = reader["TimeOut"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["TimeOut"]) : null,
                WorkHoursID = reader["WorkHoursID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["WorkHoursID"]) : null,
                LateMinutes = Convert.ToInt32(reader["LateMinutes"]),
                EarlyDepartureMinutes = Convert.ToInt32(reader["EarlyDepartureMinutes"]),
                OvertimeMinutes = Convert.ToInt32(reader["OvertimeMinutes"]),
                WorkedMinutes = Convert.ToInt32(reader["WorkedMinutes"]),
                Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : null,
                IsManualEntry = Convert.ToBoolean(reader["IsManualEntry"]),
                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                CreatedBy = reader["CreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CreatedBy"]) : null,
                CreatedByUser = reader["CreatedByUser"] != DBNull.Value ? reader["CreatedByUser"].ToString() : null
            };
        }
        
        /// <summary>
        /// تحويل من قارئ البيانات إلى كائن تصريح حضور
        /// </summary>
        /// <param name="reader">قارئ البيانات</param>
        /// <returns>كائن تصريح حضور</returns>
        private AttendancePermission MapAttendancePermissionFromReader(SqlDataReader reader)
        {
            return new AttendancePermission
            {
                ID = Convert.ToInt32(reader["ID"]),
                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                EmployeeName = reader["EmployeeName"] != DBNull.Value ? reader["EmployeeName"].ToString() : null,
                DepartmentName = reader["DepartmentName"] != DBNull.Value ? reader["DepartmentName"].ToString() : null,
                PermissionDate = Convert.ToDateTime(reader["PermissionDate"]),
                PermissionType = reader["PermissionType"].ToString(),
                StartTime = reader["StartTime"] != DBNull.Value ? (TimeSpan?)TimeSpan.Parse(reader["StartTime"].ToString()) : null,
                EndTime = reader["EndTime"] != DBNull.Value ? (TimeSpan?)TimeSpan.Parse(reader["EndTime"].ToString()) : null,
                TotalMinutes = reader["TotalMinutes"] != DBNull.Value ? (int?)Convert.ToInt32(reader["TotalMinutes"]) : null,
                Reason = reader["Reason"] != DBNull.Value ? reader["Reason"].ToString() : null,
                Status = reader["Status"].ToString(),
                ApprovedBy = reader["ApprovedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ApprovedBy"]) : null,
                ApprovedByUser = reader["ApprovedByUser"] != DBNull.Value ? reader["ApprovedByUser"].ToString() : null,
                ApprovalDate = reader["ApprovalDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ApprovalDate"]) : null,
                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                CreatedBy = reader["CreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CreatedBy"]) : null,
                CreatedByUser = reader["CreatedByUser"] != DBNull.Value ? reader["CreatedByUser"].ToString() : null,
                UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["UpdatedAt"]) : null
            };
        }
        
        #endregion
    }
    
    /// <summary>
    /// كائن يمثل ملخص الحضور الشهري لموظف
    /// </summary>
    public class AttendanceSummary
    {
        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// اسم الموظف
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// اسم القسم
        /// </summary>
        public string DepartmentName { get; set; }
        
        /// <summary>
        /// السنة
        /// </summary>
        public int Year { get; set; }
        
        /// <summary>
        /// الشهر
        /// </summary>
        public int Month { get; set; }
        
        /// <summary>
        /// إجمالي عدد الأيام
        /// </summary>
        public int TotalDays { get; set; }
        
        /// <summary>
        /// عدد أيام الحضور
        /// </summary>
        public int PresentDays { get; set; }
        
        /// <summary>
        /// عدد أيام الغياب
        /// </summary>
        public int AbsentDays { get; set; }
        
        /// <summary>
        /// عدد أيام التأخير
        /// </summary>
        public int LateDays { get; set; }
        
        /// <summary>
        /// عدد أيام المغادرة المبكرة
        /// </summary>
        public int EarlyDepartureDays { get; set; }
        
        /// <summary>
        /// عدد أيام الإجازات
        /// </summary>
        public int LeaveDays { get; set; }
        
        /// <summary>
        /// عدد أيام التصاريح
        /// </summary>
        public int PermissionDays { get; set; }
        
        /// <summary>
        /// إجمالي دقائق التأخير
        /// </summary>
        public int TotalLateMinutes { get; set; }
        
        /// <summary>
        /// إجمالي دقائق المغادرة المبكرة
        /// </summary>
        public int TotalEarlyDepartureMinutes { get; set; }
        
        /// <summary>
        /// إجمالي دقائق العمل الإضافي
        /// </summary>
        public int TotalOvertimeMinutes { get; set; }
        
        /// <summary>
        /// إجمالي دقائق العمل
        /// </summary>
        public int TotalWorkedMinutes { get; set; }
    }
}