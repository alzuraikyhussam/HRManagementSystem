using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraCharts;
using DevExpress.Utils;
using HR.Core;

namespace HR.Core.Reports
{
    /// <summary>
    /// مولد التقارير المخصصة حسب المعايير المحددة
    /// </summary>
    public class CustomReportGenerator
    {
        private readonly ReportManager _reportManager;
        private readonly ConnectionManager _connectionManager;
        
        public CustomReportGenerator()
        {
            _reportManager = new ReportManager();
            _connectionManager = new ConnectionManager();
        }
        
        #region إعدادات وفلاتر التقرير
        
        /// <summary>
        /// خيارات تصفية التقرير
        /// </summary>
        public class ReportFilterOptions
        {
            // خيارات عامة
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public int? DepartmentId { get; set; }
            public int? PositionId { get; set; }
            public int? EmployeeId { get; set; }
            public bool? IsActive { get; set; }
            
            // خيارات الموظفين
            public DateTime? HireDateFrom { get; set; }
            public DateTime? HireDateTo { get; set; }
            public string EmploymentType { get; set; }
            public string Nationality { get; set; }
            public string Gender { get; set; }
            
            // خيارات الحضور
            public bool? IncludeAbsent { get; set; }
            public bool? IncludeLate { get; set; }
            public bool? IncludeExcused { get; set; }
            public int? MinLateMinutes { get; set; }
            public int? WorkShiftId { get; set; }
            
            // خيارات الإجازات
            public int? LeaveTypeId { get; set; }
            public string LeaveStatus { get; set; }
            public int? MinLeaveDuration { get; set; }
            public int? MaxLeaveDuration { get; set; }
            
            // خيارات الرواتب
            public int? PayrollId { get; set; }
            public string PayrollStatus { get; set; }
            public decimal? MinSalary { get; set; }
            public decimal? MaxSalary { get; set; }
            
            // خيارات التحليل
            public bool IncludeCharts { get; set; } = true;
            public bool IncludeSummary { get; set; } = true;
            public bool IncludeDetails { get; set; } = true;
            
            /// <summary>
            /// الحصول على قائمة البارامترات لاستعلام SQL
            /// </summary>
            /// <returns>قائمة البارامترات</returns>
            public Dictionary<string, object> GetSqlParameters()
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                
                if (StartDate.HasValue)
                    parameters.Add("@StartDate", StartDate.Value);
                    
                if (EndDate.HasValue)
                    parameters.Add("@EndDate", EndDate.Value);
                    
                if (DepartmentId.HasValue && DepartmentId.Value > 0)
                    parameters.Add("@DepartmentId", DepartmentId.Value);
                    
                if (PositionId.HasValue && PositionId.Value > 0)
                    parameters.Add("@PositionId", PositionId.Value);
                    
                if (EmployeeId.HasValue && EmployeeId.Value > 0)
                    parameters.Add("@EmployeeId", EmployeeId.Value);
                    
                if (IsActive.HasValue)
                    parameters.Add("@IsActive", IsActive.Value);
                    
                if (HireDateFrom.HasValue)
                    parameters.Add("@HireDateFrom", HireDateFrom.Value);
                    
                if (HireDateTo.HasValue)
                    parameters.Add("@HireDateTo", HireDateTo.Value);
                    
                if (!string.IsNullOrEmpty(EmploymentType))
                    parameters.Add("@EmploymentType", EmploymentType);
                    
                if (!string.IsNullOrEmpty(Nationality))
                    parameters.Add("@Nationality", Nationality);
                    
                if (!string.IsNullOrEmpty(Gender))
                    parameters.Add("@Gender", Gender);
                    
                if (IncludeAbsent.HasValue)
                    parameters.Add("@IncludeAbsent", IncludeAbsent.Value);
                    
                if (IncludeLate.HasValue)
                    parameters.Add("@IncludeLate", IncludeLate.Value);
                    
                if (IncludeExcused.HasValue)
                    parameters.Add("@IncludeExcused", IncludeExcused.Value);
                    
                if (MinLateMinutes.HasValue)
                    parameters.Add("@MinLateMinutes", MinLateMinutes.Value);
                    
                if (WorkShiftId.HasValue && WorkShiftId.Value > 0)
                    parameters.Add("@WorkShiftId", WorkShiftId.Value);
                    
                if (LeaveTypeId.HasValue && LeaveTypeId.Value > 0)
                    parameters.Add("@LeaveTypeId", LeaveTypeId.Value);
                    
                if (!string.IsNullOrEmpty(LeaveStatus))
                    parameters.Add("@LeaveStatus", LeaveStatus);
                    
                if (MinLeaveDuration.HasValue)
                    parameters.Add("@MinLeaveDuration", MinLeaveDuration.Value);
                    
                if (MaxLeaveDuration.HasValue)
                    parameters.Add("@MaxLeaveDuration", MaxLeaveDuration.Value);
                    
                if (PayrollId.HasValue && PayrollId.Value > 0)
                    parameters.Add("@PayrollId", PayrollId.Value);
                    
                if (!string.IsNullOrEmpty(PayrollStatus))
                    parameters.Add("@PayrollStatus", PayrollStatus);
                    
                if (MinSalary.HasValue)
                    parameters.Add("@MinSalary", MinSalary.Value);
                    
                if (MaxSalary.HasValue)
                    parameters.Add("@MaxSalary", MaxSalary.Value);
                
                return parameters;
            }
            
            /// <summary>
            /// بناء جملة WHERE لاستعلام SQL
            /// </summary>
            /// <returns>جملة WHERE</returns>
            public string BuildWhereClause(string tablePrefix = "")
            {
                List<string> conditions = new List<string>();
                string prefix = !string.IsNullOrEmpty(tablePrefix) ? tablePrefix + "." : "";
                
                // إضافة شروط التاريخ
                if (StartDate.HasValue)
                    conditions.Add($"{prefix}Date >= @StartDate");
                    
                if (EndDate.HasValue)
                    conditions.Add($"{prefix}Date <= @EndDate");
                    
                // إضافة شروط الإدارة والوظيفة والموظف
                if (DepartmentId.HasValue && DepartmentId.Value > 0)
                    conditions.Add($"E.DepartmentID = @DepartmentId");
                    
                if (PositionId.HasValue && PositionId.Value > 0)
                    conditions.Add($"E.PositionID = @PositionId");
                    
                if (EmployeeId.HasValue && EmployeeId.Value > 0)
                    conditions.Add($"E.ID = @EmployeeId");
                    
                if (IsActive.HasValue)
                    conditions.Add($"E.IsActive = @IsActive");
                    
                // إضافة شروط الموظفين
                if (HireDateFrom.HasValue)
                    conditions.Add($"E.HireDate >= @HireDateFrom");
                    
                if (HireDateTo.HasValue)
                    conditions.Add($"E.HireDate <= @HireDateTo");
                    
                if (!string.IsNullOrEmpty(EmploymentType))
                    conditions.Add($"E.EmploymentType = @EmploymentType");
                    
                if (!string.IsNullOrEmpty(Nationality))
                    conditions.Add($"E.Nationality = @Nationality");
                    
                if (!string.IsNullOrEmpty(Gender))
                    conditions.Add($"E.Gender = @Gender");
                    
                // إضافة شروط الحضور
                if (IncludeAbsent.HasValue && prefix.Contains("A"))
                    conditions.Add($"A.IsAbsent = @IncludeAbsent");
                    
                if (IncludeLate.HasValue && prefix.Contains("A"))
                    conditions.Add($"A.LateMinutes > 0");
                    
                if (IncludeExcused.HasValue && prefix.Contains("A"))
                    conditions.Add($"A.IsExcused = @IncludeExcused");
                    
                if (MinLateMinutes.HasValue && prefix.Contains("A"))
                    conditions.Add($"A.LateMinutes >= @MinLateMinutes");
                    
                if (WorkShiftId.HasValue && WorkShiftId.Value > 0)
                    conditions.Add($"E.WorkShiftID = @WorkShiftId");
                    
                // إضافة شروط الإجازات
                if (LeaveTypeId.HasValue && LeaveTypeId.Value > 0 && prefix.Contains("L"))
                    conditions.Add($"L.LeaveTypeID = @LeaveTypeId");
                    
                if (!string.IsNullOrEmpty(LeaveStatus) && prefix.Contains("L"))
                    conditions.Add($"L.Status = @LeaveStatus");
                    
                if (MinLeaveDuration.HasValue && prefix.Contains("L"))
                    conditions.Add($"L.DurationDays >= @MinLeaveDuration");
                    
                if (MaxLeaveDuration.HasValue && prefix.Contains("L"))
                    conditions.Add($"L.DurationDays <= @MaxLeaveDuration");
                    
                // إضافة شروط الرواتب
                if (PayrollId.HasValue && PayrollId.Value > 0 && prefix.Contains("P"))
                    conditions.Add($"P.ID = @PayrollId");
                    
                if (!string.IsNullOrEmpty(PayrollStatus) && prefix.Contains("P"))
                    conditions.Add($"P.Status = @PayrollStatus");
                    
                if (MinSalary.HasValue && prefix.Contains("PD"))
                    conditions.Add($"PD.NetSalary >= @MinSalary");
                    
                if (MaxSalary.HasValue && prefix.Contains("PD"))
                    conditions.Add($"PD.NetSalary <= @MaxSalary");
                
                // بناء جملة WHERE
                if (conditions.Count > 0)
                    return "WHERE " + string.Join(" AND ", conditions);
                else
                    return string.Empty;
            }
        }
        
        /// <summary>
        /// قائمة أنواع التقارير المخصصة
        /// </summary>
        public enum CustomReportType
        {
            Employees, // تقرير الموظفين
            Attendance, // تقرير الحضور والغياب
            Leave, // تقرير الإجازات
            Payroll, // تقرير الرواتب
            EmployeeDetails, // تقرير تفاصيل الموظف
            DepartmentSummary, // تقرير ملخص الإدارات
            PerformanceSummary // تقرير ملخص الأداء
        }
        
        #endregion
        
        #region توليد التقارير المخصصة
        
        /// <summary>
        /// توليد تقرير مخصص
        /// </summary>
        /// <param name="reportType">نوع التقرير</param>
        /// <param name="reportTitle">عنوان التقرير</param>
        /// <param name="filters">فلاتر التقرير</param>
        /// <returns>التقرير المطلوب</returns>
        public XtraReport GenerateCustomReport(CustomReportType reportType, string reportTitle, ReportFilterOptions filters)
        {
            try
            {
                XtraReport report = _reportManager.CreateBaseReport(reportTitle);
                
                DataSet dataSet = null;
                
                // جلب البيانات حسب نوع التقرير
                switch (reportType)
                {
                    case CustomReportType.Employees:
                        dataSet = GetEmployeesData(filters);
                        break;
                        
                    case CustomReportType.Attendance:
                        dataSet = GetAttendanceData(filters);
                        break;
                        
                    case CustomReportType.Leave:
                        dataSet = GetLeaveData(filters);
                        break;
                        
                    case CustomReportType.Payroll:
                        dataSet = GetPayrollData(filters);
                        break;
                        
                    case CustomReportType.EmployeeDetails:
                        dataSet = GetEmployeeDetailsData(filters);
                        break;
                        
                    case CustomReportType.DepartmentSummary:
                        dataSet = GetDepartmentSummaryData(filters);
                        break;
                        
                    case CustomReportType.PerformanceSummary:
                        dataSet = GetPerformanceSummaryData(filters);
                        break;
                }
                
                if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
                {
                    XRLabel noDataLabel = new XRLabel();
                    noDataLabel.Text = "لا توجد بيانات متاحة وفق المعايير المحددة";
                    noDataLabel.Font = new Font("Arial", 12, FontStyle.Bold);
                    noDataLabel.TextAlignment = TextAlignment.MiddleCenter;
                    noDataLabel.SizeF = new SizeF(report.PageWidth - 40, 40);
                    noDataLabel.LocationF = new PointF(20, 10);
                    
                    DetailBand detailBand = new DetailBand();
                    detailBand.Controls.Add(noDataLabel);
                    report.Bands.Add(detailBand);
                    
                    return report;
                }
                
                // إضافة معلومات الفلاتر المستخدمة
                AddFiltersSummaryToReport(report, filters);
                
                // بناء التقرير حسب نوعه
                switch (reportType)
                {
                    case CustomReportType.Employees:
                        BuildEmployeesReport(report, dataSet, filters);
                        break;
                        
                    case CustomReportType.Attendance:
                        BuildAttendanceReport(report, dataSet, filters);
                        break;
                        
                    case CustomReportType.Leave:
                        BuildLeaveReport(report, dataSet, filters);
                        break;
                        
                    case CustomReportType.Payroll:
                        BuildPayrollReport(report, dataSet, filters);
                        break;
                        
                    case CustomReportType.EmployeeDetails:
                        BuildEmployeeDetailsReport(report, dataSet, filters);
                        break;
                        
                    case CustomReportType.DepartmentSummary:
                        BuildDepartmentSummaryReport(report, dataSet, filters);
                        break;
                        
                    case CustomReportType.PerformanceSummary:
                        BuildPerformanceSummaryReport(report, dataSet, filters);
                        break;
                }
                
                return report;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في إنشاء التقرير المخصص");
                XtraReport report = _reportManager.CreateBaseReport("خطأ في إنشاء التقرير");
                
                DetailBand detailBand = new DetailBand();
                XRLabel errorLabel = new XRLabel();
                errorLabel.Text = "حدث خطأ أثناء إنشاء التقرير: " + ex.Message;
                errorLabel.Font = new Font("Arial", 12, FontStyle.Bold);
                errorLabel.TextAlignment = TextAlignment.MiddleCenter;
                errorLabel.SizeF = new SizeF(report.PageWidth - 40, 40);
                errorLabel.LocationF = new PointF(20, 10);
                detailBand.Controls.Add(errorLabel);
                report.Bands.Add(detailBand);
                
                return report;
            }
        }
        
        /// <summary>
        /// إضافة ملخص الفلاتر المستخدمة إلى التقرير
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="filters">الفلاتر المستخدمة</param>
        private void AddFiltersSummaryToReport(XtraReport report, ReportFilterOptions filters)
        {
            // إنشاء قسم لعرض الفلاتر المستخدمة
            GroupHeaderBand filtersBand = new GroupHeaderBand();
            filtersBand.HeightF = 40;
            report.Bands.Add(filtersBand);
            
            // إنشاء جدول للفلاتر
            XRTable filtersTable = new XRTable();
            filtersTable.LocationF = new PointF(10, 5);
            filtersTable.SizeF = new SizeF(report.PageWidth - 20, 30);
            filtersTable.BackColor = Color.LightGray;
            filtersTable.Font = new Font("Arial", 9);
            filtersBand.Controls.Add(filtersTable);
            
            XRTableRow filtersRow = new XRTableRow();
            filtersRow.HeightF = 30;
            filtersTable.Rows.Add(filtersRow);
            
            // إضافة معلومات الفلاتر الأساسية
            List<string> filtersInfo = new List<string>();
            
            if (filters.StartDate.HasValue && filters.EndDate.HasValue)
                filtersInfo.Add($"الفترة: من {filters.StartDate.Value.ToString("yyyy/MM/dd")} إلى {filters.EndDate.Value.ToString("yyyy/MM/dd")}");
                
            if (filters.DepartmentId.HasValue && filters.DepartmentId.Value > 0)
                filtersInfo.Add($"الإدارة: {GetDepartmentName(filters.DepartmentId.Value)}");
                
            if (filters.EmployeeId.HasValue && filters.EmployeeId.Value > 0)
                filtersInfo.Add($"الموظف: {GetEmployeeName(filters.EmployeeId.Value)}");
                
            if (filters.LeaveTypeId.HasValue && filters.LeaveTypeId.Value > 0)
                filtersInfo.Add($"نوع الإجازة: {GetLeaveTypeName(filters.LeaveTypeId.Value)}");
                
            if (filters.PayrollId.HasValue && filters.PayrollId.Value > 0)
                filtersInfo.Add($"كشف الرواتب: {GetPayrollName(filters.PayrollId.Value)}");
                
            // إضافة معلومات الفلاتر الإضافية
            string additionalFilters = string.Empty;
            
            if (filters.IsActive.HasValue)
                additionalFilters += (additionalFilters.Length > 0 ? "، " : "") + "الموظفين " + (filters.IsActive.Value ? "النشطين" : "غير النشطين");
                
            if (filters.IncludeAbsent.HasValue)
                additionalFilters += (additionalFilters.Length > 0 ? "، " : "") + (filters.IncludeAbsent.Value ? "الغيابات" : "الحضور فقط");
                
            if (filters.IncludeLate.HasValue)
                additionalFilters += (additionalFilters.Length > 0 ? "، " : "") + (filters.IncludeLate.Value ? "التأخير" : "الوقت المحدد فقط");
                
            if (!string.IsNullOrEmpty(additionalFilters))
                filtersInfo.Add($"فلاتر إضافية: {additionalFilters}");
                
            // إضافة خلية الفلاتر
            XRTableCell filtersCell = new XRTableCell();
            filtersCell.Text = string.Join(" | ", filtersInfo);
            filtersCell.TextAlignment = TextAlignment.MiddleRight;
            filtersCell.Borders = BorderSide.All;
            filtersCell.BorderWidth = 1;
            filtersCell.Padding = new PaddingInfo(5, 5, 0, 0);
            filtersRow.Cells.Add(filtersCell);
            
            // إضافة خط فاصل
            XRLine xrLine = new XRLine();
            xrLine.LocationF = new PointF(0, 38);
            xrLine.SizeF = new SizeF(report.PageWidth - 20, 2);
            xrLine.LineWidth = 1;
            filtersBand.Controls.Add(xrLine);
        }
        
        #endregion
        
        #region جلب البيانات
        
        /// <summary>
        /// الحصول على بيانات الموظفين
        /// </summary>
        /// <param name="filters">فلاتر التقرير</param>
        /// <returns>مجموعة بيانات الموظفين</returns>
        private DataSet GetEmployeesData(ReportFilterOptions filters)
        {
            DataSet dataSet = new DataSet();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    // بناء استعلام الموظفين
                    string whereClause = filters.BuildWhereClause("E");
                    
                    // استعلام الموظفين الرئيسي
                    string employeesQuery = $@"
                        SELECT 
                            E.ID,
                            E.EmployeeNumber,
                            E.FullName,
                            E.JobTitle,
                            D.Name AS DepartmentName,
                            P.Title AS PositionTitle,
                            E.HireDate,
                            E.BasicSalary,
                            E.EmploymentType,
                            E.Email,
                            E.Phone,
                            E.Gender,
                            E.Nationality,
                            E.BirthDate,
                            E.Address,
                            E.IsActive,
                            CASE WHEN E.IsActive = 1 THEN 'نشط' ELSE 'غير نشط' END AS StatusText,
                            WS.Name AS WorkShiftName
                        FROM Employees E
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        LEFT JOIN Positions P ON E.PositionID = P.ID
                        LEFT JOIN WorkShifts WS ON E.WorkShiftID = WS.ID
                        {whereClause}
                        ORDER BY D.Name, E.FullName";
                    
                    using (SqlCommand command = new SqlCommand(employeesQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "Employees");
                    }
                    
                    // استعلام ملخص الإدارات
                    string departmentSummaryQuery = $@"
                        SELECT 
                            D.Name AS DepartmentName,
                            COUNT(E.ID) AS EmployeeCount,
                            SUM(CASE WHEN E.IsActive = 1 THEN 1 ELSE 0 END) AS ActiveCount,
                            SUM(CASE WHEN E.IsActive = 0 THEN 1 ELSE 0 END) AS InactiveCount,
                            SUM(CASE WHEN E.Gender = 'M' THEN 1 ELSE 0 END) AS MaleCount,
                            SUM(CASE WHEN E.Gender = 'F' THEN 1 ELSE 0 END) AS FemaleCount,
                            AVG(E.BasicSalary) AS AvgSalary,
                            SUM(E.BasicSalary) AS TotalSalary
                        FROM Employees E
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        {whereClause}
                        GROUP BY D.Name
                        ORDER BY D.Name";
                    
                    using (SqlCommand command = new SqlCommand(departmentSummaryQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "DepartmentSummary");
                    }
                    
                    // استعلام ملخص أنواع التوظيف
                    string employmentTypeSummaryQuery = $@"
                        SELECT 
                            E.EmploymentType,
                            COUNT(E.ID) AS EmployeeCount,
                            SUM(CASE WHEN E.IsActive = 1 THEN 1 ELSE 0 END) AS ActiveCount,
                            AVG(E.BasicSalary) AS AvgSalary
                        FROM Employees E
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        {whereClause}
                        GROUP BY E.EmploymentType
                        ORDER BY COUNT(E.ID) DESC";
                    
                    using (SqlCommand command = new SqlCommand(employmentTypeSummaryQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "EmploymentTypeSummary");
                    }
                }
                
                return dataSet;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في جلب بيانات الموظفين");
                return null;
            }
        }
        
        /// <summary>
        /// الحصول على بيانات الحضور والغياب
        /// </summary>
        /// <param name="filters">فلاتر التقرير</param>
        /// <returns>مجموعة بيانات الحضور والغياب</returns>
        private DataSet GetAttendanceData(ReportFilterOptions filters)
        {
            DataSet dataSet = new DataSet();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    // بناء استعلام الحضور
                    string whereClause = filters.BuildWhereClause("A");
                    
                    // استعلام الحضور الرئيسي
                    string attendanceQuery = $@"
                        SELECT 
                            A.ID,
                            A.EmployeeID,
                            E.EmployeeNumber,
                            E.FullName AS EmployeeName,
                            D.Name AS DepartmentName,
                            P.Title AS PositionTitle,
                            A.AttendanceDate,
                            A.CheckInTime,
                            A.CheckOutTime,
                            A.WorkHours,
                            A.LateMinutes,
                            A.EarlyDepartureMinutes,
                            A.OverTimeMinutes,
                            A.IsAbsent,
                            A.IsExcused,
                            A.AttendanceStatus,
                            A.Notes
                        FROM Attendance A
                        INNER JOIN Employees E ON A.EmployeeID = E.ID
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        LEFT JOIN Positions P ON E.PositionID = P.ID
                        {whereClause}
                        ORDER BY A.AttendanceDate DESC, E.FullName";
                    
                    using (SqlCommand command = new SqlCommand(attendanceQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "Attendance");
                    }
                    
                    // استعلام ملخص الإدارات
                    string departmentSummaryQuery = $@"
                        SELECT 
                            D.Name AS DepartmentName,
                            COUNT(DISTINCT A.EmployeeID) AS EmployeeCount,
                            SUM(CASE WHEN A.IsAbsent = 1 THEN 1 ELSE 0 END) AS AbsentCount,
                            SUM(CASE WHEN A.IsExcused = 1 THEN 1 ELSE 0 END) AS ExcusedCount,
                            SUM(CASE WHEN A.LateMinutes > 0 THEN 1 ELSE 0 END) AS LateCount,
                            SUM(CASE WHEN A.EarlyDepartureMinutes > 0 THEN 1 ELSE 0 END) AS EarlyDepartureCount,
                            SUM(CASE WHEN A.OverTimeMinutes > 0 THEN 1 ELSE 0 END) AS OverTimeCount,
                            AVG(A.WorkHours) AS AvgWorkHours,
                            AVG(A.LateMinutes) AS AvgLateMinutes
                        FROM Attendance A
                        INNER JOIN Employees E ON A.EmployeeID = E.ID
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        {whereClause}
                        GROUP BY D.Name
                        ORDER BY D.Name";
                    
                    using (SqlCommand command = new SqlCommand(departmentSummaryQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "DepartmentSummary");
                    }
                    
                    // استعلام ملخص الموظفين
                    string employeeSummaryQuery = $@"
                        SELECT 
                            E.ID AS EmployeeID,
                            E.FullName AS EmployeeName,
                            D.Name AS DepartmentName,
                            COUNT(A.ID) AS AttendanceCount,
                            SUM(CASE WHEN A.IsAbsent = 1 THEN 1 ELSE 0 END) AS AbsentCount,
                            SUM(CASE WHEN A.IsExcused = 1 THEN 1 ELSE 0 END) AS ExcusedCount,
                            SUM(CASE WHEN A.LateMinutes > 0 THEN 1 ELSE 0 END) AS LateCount,
                            AVG(A.WorkHours) AS AvgWorkHours,
                            AVG(A.LateMinutes) AS AvgLateMinutes,
                            SUM(A.WorkHours) AS TotalWorkHours
                        FROM Attendance A
                        INNER JOIN Employees E ON A.EmployeeID = E.ID
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        {whereClause}
                        GROUP BY E.ID, E.FullName, D.Name
                        ORDER BY D.Name, E.FullName";
                    
                    using (SqlCommand command = new SqlCommand(employeeSummaryQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "EmployeeSummary");
                    }
                }
                
                return dataSet;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في جلب بيانات الحضور والغياب");
                return null;
            }
        }
        
        /// <summary>
        /// الحصول على بيانات الإجازات
        /// </summary>
        /// <param name="filters">فلاتر التقرير</param>
        /// <returns>مجموعة بيانات الإجازات</returns>
        private DataSet GetLeaveData(ReportFilterOptions filters)
        {
            DataSet dataSet = new DataSet();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    // بناء استعلام الإجازات
                    string whereClause = filters.BuildWhereClause("L");
                    
                    // استعلام الإجازات الرئيسي
                    string leaveQuery = $@"
                        SELECT 
                            L.ID,
                            L.EmployeeID,
                            E.EmployeeNumber,
                            E.FullName AS EmployeeName,
                            D.Name AS DepartmentName,
                            P.Title AS PositionTitle,
                            LT.Name AS LeaveTypeName,
                            L.StartDate,
                            L.EndDate,
                            L.DurationDays,
                            L.Status,
                            L.ApprovedBy,
                            L.ApprovedDate,
                            L.Notes,
                            U.FullName AS ApprovedByName
                        FROM Leave L
                        INNER JOIN Employees E ON L.EmployeeID = E.ID
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        LEFT JOIN Positions P ON E.PositionID = P.ID
                        LEFT JOIN LeaveTypes LT ON L.LeaveTypeID = LT.ID
                        LEFT JOIN Users U ON L.ApprovedBy = U.ID
                        {whereClause}
                        ORDER BY L.StartDate DESC, E.FullName";
                    
                    using (SqlCommand command = new SqlCommand(leaveQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "Leave");
                    }
                    
                    // استعلام ملخص الإدارات
                    string departmentSummaryQuery = $@"
                        SELECT 
                            D.Name AS DepartmentName,
                            COUNT(DISTINCT L.EmployeeID) AS EmployeeCount,
                            COUNT(L.ID) AS LeaveCount,
                            SUM(L.DurationDays) AS TotalDays,
                            AVG(L.DurationDays) AS AvgDuration
                        FROM Leave L
                        INNER JOIN Employees E ON L.EmployeeID = E.ID
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        {whereClause}
                        GROUP BY D.Name
                        ORDER BY D.Name";
                    
                    using (SqlCommand command = new SqlCommand(departmentSummaryQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "DepartmentSummary");
                    }
                    
                    // استعلام ملخص أنواع الإجازات
                    string leaveTypeSummaryQuery = $@"
                        SELECT 
                            LT.Name AS LeaveTypeName,
                            COUNT(L.ID) AS LeaveCount,
                            SUM(L.DurationDays) AS TotalDays,
                            AVG(L.DurationDays) AS AvgDuration,
                            COUNT(DISTINCT L.EmployeeID) AS EmployeeCount
                        FROM Leave L
                        INNER JOIN Employees E ON L.EmployeeID = E.ID
                        LEFT JOIN LeaveTypes LT ON L.LeaveTypeID = LT.ID
                        {whereClause}
                        GROUP BY LT.Name
                        ORDER BY COUNT(L.ID) DESC";
                    
                    using (SqlCommand command = new SqlCommand(leaveTypeSummaryQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "LeaveTypeSummary");
                    }
                }
                
                return dataSet;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في جلب بيانات الإجازات");
                return null;
            }
        }
        
        /// <summary>
        /// الحصول على بيانات الرواتب
        /// </summary>
        /// <param name="filters">فلاتر التقرير</param>
        /// <returns>مجموعة بيانات الرواتب</returns>
        private DataSet GetPayrollData(ReportFilterOptions filters)
        {
            DataSet dataSet = new DataSet();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    // بناء استعلام الرواتب
                    string whereClause = filters.BuildWhereClause("PD");
                    
                    // تخصيص الاستعلام بناءً على وجود PayrollId
                    string payrollDetailsQuery;
                    
                    if (filters.PayrollId.HasValue && filters.PayrollId.Value > 0)
                    {
                        // استعلام تفاصيل كشف رواتب محدد
                        payrollDetailsQuery = $@"
                            SELECT 
                                PD.ID,
                                PD.PayrollID,
                                P.PayrollName,
                                P.PayrollMonth,
                                P.PayrollYear,
                                PD.EmployeeID,
                                E.EmployeeNumber,
                                E.FullName AS EmployeeName,
                                D.Name AS DepartmentName,
                                Pos.Title AS PositionTitle,
                                PD.BaseSalary,
                                PD.Allowances,
                                PD.Overtime,
                                PD.Bonus,
                                PD.Deductions,
                                PD.AbsenceDeductions,
                                PD.LateDeductions,
                                PD.LoanDeductions,
                                PD.NetSalary,
                                PD.Notes
                            FROM PayrollDetails PD
                            INNER JOIN Payrolls P ON PD.PayrollID = P.ID
                            INNER JOIN Employees E ON PD.EmployeeID = E.ID
                            LEFT JOIN Departments D ON E.DepartmentID = D.ID
                            LEFT JOIN Positions Pos ON E.PositionID = Pos.ID
                            WHERE PD.PayrollID = @PayrollId
                            ORDER BY D.Name, E.FullName";
                            
                        using (SqlCommand command = new SqlCommand(payrollDetailsQuery, connection))
                        {
                            command.Parameters.AddWithValue("@PayrollId", filters.PayrollId.Value);
                            
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            adapter.Fill(dataSet, "PayrollDetails");
                        }
                        
                        // استعلام معلومات كشف الرواتب
                        string payrollInfoQuery = @"
                            SELECT 
                                P.ID,
                                P.PayrollName,
                                P.PayrollMonth,
                                P.PayrollYear,
                                P.TotalBaseSalary,
                                P.TotalAdditions,
                                P.TotalDeductions,
                                P.TotalNetSalary,
                                P.PaymentDate,
                                P.Status,
                                P.GeneratedDate,
                                P.GeneratedBy,
                                P.ApprovedDate,
                                P.ApprovedBy,
                                P.Notes,
                                U1.FullName AS GeneratedByName,
                                U2.FullName AS ApprovedByName
                            FROM Payrolls P
                            LEFT JOIN Users U1 ON P.GeneratedBy = U1.ID
                            LEFT JOIN Users U2 ON P.ApprovedBy = U2.ID
                            WHERE P.ID = @PayrollId";
                            
                        using (SqlCommand command = new SqlCommand(payrollInfoQuery, connection))
                        {
                            command.Parameters.AddWithValue("@PayrollId", filters.PayrollId.Value);
                            
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            adapter.Fill(dataSet, "PayrollInfo");
                        }
                    }
                    else
                    {
                        // استعلام عام للرواتب
                        payrollDetailsQuery = $@"
                            SELECT 
                                PD.ID,
                                PD.PayrollID,
                                P.PayrollName,
                                P.PayrollMonth,
                                P.PayrollYear,
                                PD.EmployeeID,
                                E.EmployeeNumber,
                                E.FullName AS EmployeeName,
                                D.Name AS DepartmentName,
                                Pos.Title AS PositionTitle,
                                PD.BaseSalary,
                                PD.Allowances,
                                PD.Overtime,
                                PD.Bonus,
                                PD.Deductions,
                                PD.AbsenceDeductions,
                                PD.LateDeductions,
                                PD.LoanDeductions,
                                PD.NetSalary,
                                PD.Notes
                            FROM PayrollDetails PD
                            INNER JOIN Payrolls P ON PD.PayrollID = P.ID
                            INNER JOIN Employees E ON PD.EmployeeID = E.ID
                            LEFT JOIN Departments D ON E.DepartmentID = D.ID
                            LEFT JOIN Positions Pos ON E.PositionID = Pos.ID
                            {whereClause}
                            ORDER BY P.PayrollYear DESC, P.PayrollMonth DESC, D.Name, E.FullName";
                            
                        using (SqlCommand command = new SqlCommand(payrollDetailsQuery, connection))
                        {
                            // إضافة البارامترات
                            foreach (var param in filters.GetSqlParameters())
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                            
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            adapter.Fill(dataSet, "PayrollDetails");
                        }
                        
                        // استعلام ملخص كشوفات الرواتب
                        string payrollSummaryQuery = $@"
                            SELECT 
                                P.ID,
                                P.PayrollName,
                                P.PayrollMonth,
                                P.PayrollYear,
                                P.TotalBaseSalary,
                                P.TotalAdditions,
                                P.TotalDeductions,
                                P.TotalNetSalary,
                                P.PaymentDate,
                                P.Status,
                                P.GeneratedDate,
                                COUNT(PD.ID) AS EmployeeCount
                            FROM Payrolls P
                            INNER JOIN PayrollDetails PD ON P.ID = PD.PayrollID
                            INNER JOIN Employees E ON PD.EmployeeID = E.ID
                            LEFT JOIN Departments D ON E.DepartmentID = D.ID
                            {(whereClause.Replace("PD.", "P."))}
                            GROUP BY P.ID, P.PayrollName, P.PayrollMonth, P.PayrollYear, P.TotalBaseSalary, 
                                P.TotalAdditions, P.TotalDeductions, P.TotalNetSalary, P.PaymentDate, 
                                P.Status, P.GeneratedDate
                            ORDER BY P.PayrollYear DESC, P.PayrollMonth DESC";
                            
                        using (SqlCommand command = new SqlCommand(payrollSummaryQuery, connection))
                        {
                            // إضافة البارامترات
                            foreach (var param in filters.GetSqlParameters())
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                            
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            adapter.Fill(dataSet, "PayrollSummary");
                        }
                    }
                    
                    // استعلام ملخص الإدارات
                    string departmentSummaryQuery = $@"
                        SELECT 
                            D.Name AS DepartmentName,
                            COUNT(DISTINCT PD.EmployeeID) AS EmployeeCount,
                            SUM(PD.BaseSalary) AS TotalBaseSalary,
                            SUM(PD.Allowances) AS TotalAllowances,
                            SUM(PD.Overtime) AS TotalOvertime,
                            SUM(PD.Bonus) AS TotalBonus,
                            SUM(PD.Deductions) AS TotalDeductions,
                            SUM(PD.NetSalary) AS TotalNetSalary,
                            AVG(PD.NetSalary) AS AvgNetSalary
                        FROM PayrollDetails PD
                        INNER JOIN Payrolls P ON PD.PayrollID = P.ID
                        INNER JOIN Employees E ON PD.EmployeeID = E.ID
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        {whereClause}
                        GROUP BY D.Name
                        ORDER BY D.Name";
                    
                    using (SqlCommand command = new SqlCommand(departmentSummaryQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "DepartmentSummary");
                    }
                }
                
                return dataSet;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في جلب بيانات الرواتب");
                return null;
            }
        }
        
        /// <summary>
        /// الحصول على بيانات تفاصيل الموظف
        /// </summary>
        /// <param name="filters">فلاتر التقرير</param>
        /// <returns>مجموعة بيانات تفاصيل الموظف</returns>
        private DataSet GetEmployeeDetailsData(ReportFilterOptions filters)
        {
            DataSet dataSet = new DataSet();
            
            // التأكد من وجود معرف الموظف
            if (!filters.EmployeeId.HasValue || filters.EmployeeId.Value <= 0)
            {
                return null;
            }
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    // استعلام معلومات الموظف
                    string employeeQuery = @"
                        SELECT 
                            E.ID,
                            E.EmployeeNumber,
                            E.FullName,
                            E.JobTitle,
                            D.Name AS DepartmentName,
                            P.Title AS PositionTitle,
                            E.HireDate,
                            E.BasicSalary,
                            E.EmploymentType,
                            E.Email,
                            E.Phone,
                            E.Gender,
                            E.Nationality,
                            E.BirthDate,
                            E.Address,
                            E.IsActive,
                            CASE WHEN E.IsActive = 1 THEN 'نشط' ELSE 'غير نشط' END AS StatusText,
                            WS.Name AS WorkShiftName,
                            WH.StartTime,
                            WH.EndTime,
                            CONCAT(U.FirstName, ' ', U.LastName) AS ManagerName
                        FROM Employees E
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        LEFT JOIN Positions P ON E.PositionID = P.ID
                        LEFT JOIN WorkShifts WS ON E.WorkShiftID = WS.ID
                        LEFT JOIN WorkHours WH ON WS.WorkHoursID = WH.ID
                        LEFT JOIN Employees M ON E.ManagerID = M.ID
                        LEFT JOIN Users U ON M.UserID = U.ID
                        WHERE E.ID = @EmployeeId";
                    
                    using (SqlCommand command = new SqlCommand(employeeQuery, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", filters.EmployeeId.Value);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "Employee");
                    }
                    
                    // استعلام سجل الحضور
                    string attendanceQuery = @"
                        SELECT 
                            A.ID,
                            A.AttendanceDate,
                            A.CheckInTime,
                            A.CheckOutTime,
                            A.WorkHours,
                            A.LateMinutes,
                            A.EarlyDepartureMinutes,
                            A.OverTimeMinutes,
                            A.IsAbsent,
                            A.IsExcused,
                            A.AttendanceStatus,
                            A.Notes
                        FROM Attendance A
                        WHERE A.EmployeeID = @EmployeeId
                        ORDER BY A.AttendanceDate DESC";
                    
                    using (SqlCommand command = new SqlCommand(attendanceQuery, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", filters.EmployeeId.Value);
                        
                        if (filters.StartDate.HasValue)
                        {
                            command.CommandText += " AND A.AttendanceDate >= @StartDate";
                            command.Parameters.AddWithValue("@StartDate", filters.StartDate.Value);
                        }
                        
                        if (filters.EndDate.HasValue)
                        {
                            command.CommandText += " AND A.AttendanceDate <= @EndDate";
                            command.Parameters.AddWithValue("@EndDate", filters.EndDate.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "Attendance");
                    }
                    
                    // استعلام سجل الإجازات
                    string leaveQuery = @"
                        SELECT 
                            L.ID,
                            LT.Name AS LeaveTypeName,
                            L.StartDate,
                            L.EndDate,
                            L.DurationDays,
                            L.Status,
                            L.ApprovedBy,
                            L.ApprovedDate,
                            L.Notes,
                            U.FullName AS ApprovedByName
                        FROM Leave L
                        LEFT JOIN LeaveTypes LT ON L.LeaveTypeID = LT.ID
                        LEFT JOIN Users U ON L.ApprovedBy = U.ID
                        WHERE L.EmployeeID = @EmployeeId
                        ORDER BY L.StartDate DESC";
                    
                    using (SqlCommand command = new SqlCommand(leaveQuery, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", filters.EmployeeId.Value);
                        
                        if (filters.StartDate.HasValue)
                        {
                            command.CommandText += " AND L.StartDate >= @StartDate";
                            command.Parameters.AddWithValue("@StartDate", filters.StartDate.Value);
                        }
                        
                        if (filters.EndDate.HasValue)
                        {
                            command.CommandText += " AND L.EndDate <= @EndDate";
                            command.Parameters.AddWithValue("@EndDate", filters.EndDate.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "Leave");
                    }
                    
                    // استعلام سجل الرواتب
                    string payrollQuery = @"
                        SELECT 
                            PD.ID,
                            PD.PayrollID,
                            P.PayrollName,
                            P.PayrollMonth,
                            P.PayrollYear,
                            PD.BaseSalary,
                            PD.Allowances,
                            PD.Overtime,
                            PD.Bonus,
                            PD.Deductions,
                            PD.AbsenceDeductions,
                            PD.LateDeductions,
                            PD.LoanDeductions,
                            PD.NetSalary,
                            P.PaymentDate,
                            P.Status,
                            PD.Notes
                        FROM PayrollDetails PD
                        INNER JOIN Payrolls P ON PD.PayrollID = P.ID
                        WHERE PD.EmployeeID = @EmployeeId
                        ORDER BY P.PayrollYear DESC, P.PayrollMonth DESC";
                    
                    using (SqlCommand command = new SqlCommand(payrollQuery, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", filters.EmployeeId.Value);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "Payroll");
                    }
                    
                    // استعلام ملخص الحضور
                    string attendanceSummaryQuery = @"
                        SELECT 
                            COUNT(A.ID) AS AttendanceCount,
                            SUM(CASE WHEN A.IsAbsent = 1 THEN 1 ELSE 0 END) AS AbsentCount,
                            SUM(CASE WHEN A.IsExcused = 1 THEN 1 ELSE 0 END) AS ExcusedCount,
                            SUM(CASE WHEN A.LateMinutes > 0 THEN 1 ELSE 0 END) AS LateCount,
                            SUM(CASE WHEN A.EarlyDepartureMinutes > 0 THEN 1 ELSE 0 END) AS EarlyDepartureCount,
                            SUM(CASE WHEN A.OverTimeMinutes > 0 THEN 1 ELSE 0 END) AS OverTimeCount,
                            AVG(A.WorkHours) AS AvgWorkHours,
                            SUM(A.WorkHours) AS TotalWorkHours,
                            AVG(A.LateMinutes) AS AvgLateMinutes,
                            SUM(A.LateMinutes) AS TotalLateMinutes,
                            MAX(A.AttendanceDate) AS LastAttendanceDate
                        FROM Attendance A
                        WHERE A.EmployeeID = @EmployeeId";
                    
                    using (SqlCommand command = new SqlCommand(attendanceSummaryQuery, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", filters.EmployeeId.Value);
                        
                        if (filters.StartDate.HasValue)
                        {
                            command.CommandText += " AND A.AttendanceDate >= @StartDate";
                            command.Parameters.AddWithValue("@StartDate", filters.StartDate.Value);
                        }
                        
                        if (filters.EndDate.HasValue)
                        {
                            command.CommandText += " AND A.AttendanceDate <= @EndDate";
                            command.Parameters.AddWithValue("@EndDate", filters.EndDate.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "AttendanceSummary");
                    }
                    
                    // استعلام ملخص الإجازات
                    string leaveSummaryQuery = @"
                        SELECT 
                            LT.Name AS LeaveTypeName,
                            COUNT(L.ID) AS LeaveCount,
                            SUM(L.DurationDays) AS TotalDays,
                            AVG(L.DurationDays) AS AvgDuration
                        FROM Leave L
                        LEFT JOIN LeaveTypes LT ON L.LeaveTypeID = LT.ID
                        WHERE L.EmployeeID = @EmployeeId
                        GROUP BY LT.Name";
                    
                    using (SqlCommand command = new SqlCommand(leaveSummaryQuery, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", filters.EmployeeId.Value);
                        
                        if (filters.StartDate.HasValue)
                        {
                            command.CommandText += " AND L.StartDate >= @StartDate";
                            command.Parameters.AddWithValue("@StartDate", filters.StartDate.Value);
                        }
                        
                        if (filters.EndDate.HasValue)
                        {
                            command.CommandText += " AND L.EndDate <= @EndDate";
                            command.Parameters.AddWithValue("@EndDate", filters.EndDate.Value);
                        }
                        
                        command.CommandText += " ORDER BY COUNT(L.ID) DESC";
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "LeaveSummary");
                    }
                }
                
                return dataSet;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في جلب بيانات تفاصيل الموظف");
                return null;
            }
        }
        
        /// <summary>
        /// الحصول على بيانات ملخص الإدارات
        /// </summary>
        /// <param name="filters">فلاتر التقرير</param>
        /// <returns>مجموعة بيانات ملخص الإدارات</returns>
        private DataSet GetDepartmentSummaryData(ReportFilterOptions filters)
        {
            DataSet dataSet = new DataSet();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    // بناء استعلام الإدارات
                    string whereClause = filters.BuildWhereClause("E");
                    
                    // استعلام بيانات الإدارات الرئيسية
                    string departmentsQuery = $@"
                        SELECT 
                            D.ID,
                            D.Name AS DepartmentName,
                            D.Description,
                            D.ManagerID,
                            EM.FullName AS ManagerName,
                            D.ParentDepartmentID,
                            PD.Name AS ParentDepartmentName,
                            COUNT(E.ID) AS EmployeeCount,
                            SUM(CASE WHEN E.IsActive = 1 THEN 1 ELSE 0 END) AS ActiveEmployeeCount,
                            SUM(CASE WHEN E.Gender = 'M' THEN 1 ELSE 0 END) AS MaleCount,
                            SUM(CASE WHEN E.Gender = 'F' THEN 1 ELSE 0 END) AS FemaleCount,
                            AVG(E.BasicSalary) AS AvgSalary,
                            SUM(E.BasicSalary) AS TotalSalary
                        FROM Departments D
                        LEFT JOIN Employees E ON E.DepartmentID = D.ID
                        LEFT JOIN Employees EM ON D.ManagerID = EM.ID
                        LEFT JOIN Departments PD ON D.ParentDepartmentID = PD.ID
                        {whereClause.Replace("E.", "E.")}
                        GROUP BY D.ID, D.Name, D.Description, D.ManagerID, EM.FullName, D.ParentDepartmentID, PD.Name
                        ORDER BY D.ParentDepartmentID, D.Name";
                    
                    using (SqlCommand command = new SqlCommand(departmentsQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "Departments");
                    }
                    
                    // استعلام ملخص الحضور حسب الإدارات
                    string attendanceSummaryQuery = $@"
                        SELECT 
                            D.ID AS DepartmentID,
                            D.Name AS DepartmentName,
                            COUNT(DISTINCT A.EmployeeID) AS EmployeeCount,
                            SUM(CASE WHEN A.IsAbsent = 1 THEN 1 ELSE 0 END) AS AbsentCount,
                            SUM(CASE WHEN A.IsExcused = 1 THEN 1 ELSE 0 END) AS ExcusedCount,
                            SUM(CASE WHEN A.LateMinutes > 0 THEN 1 ELSE 0 END) AS LateCount,
                            AVG(A.WorkHours) AS AvgWorkHours,
                            AVG(A.LateMinutes) AS AvgLateMinutes
                        FROM Attendance A
                        INNER JOIN Employees E ON A.EmployeeID = E.ID
                        INNER JOIN Departments D ON E.DepartmentID = D.ID
                        {whereClause.Replace("E.", "E.")}";
                    
                    // إضافة شروط التاريخ
                    if (filters.StartDate.HasValue)
                    {
                        attendanceSummaryQuery += " AND A.AttendanceDate >= @StartDate";
                    }
                    
                    if (filters.EndDate.HasValue)
                    {
                        attendanceSummaryQuery += " AND A.AttendanceDate <= @EndDate";
                    }
                    
                    attendanceSummaryQuery += @" GROUP BY D.ID, D.Name
                        ORDER BY D.Name";
                    
                    using (SqlCommand command = new SqlCommand(attendanceSummaryQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "DepartmentAttendance");
                    }
                    
                    // استعلام ملخص الإجازات حسب الإدارات
                    string leaveSummaryQuery = $@"
                        SELECT 
                            D.ID AS DepartmentID,
                            D.Name AS DepartmentName,
                            COUNT(DISTINCT L.EmployeeID) AS EmployeeCount,
                            COUNT(L.ID) AS LeaveCount,
                            SUM(L.DurationDays) AS TotalDays,
                            AVG(L.DurationDays) AS AvgDuration
                        FROM Leave L
                        INNER JOIN Employees E ON L.EmployeeID = E.ID
                        INNER JOIN Departments D ON E.DepartmentID = D.ID
                        {whereClause.Replace("E.", "E.")}";
                    
                    // إضافة شروط التاريخ
                    if (filters.StartDate.HasValue)
                    {
                        leaveSummaryQuery += " AND L.StartDate <= @EndDate AND L.EndDate >= @StartDate";
                    }
                    
                    if (filters.EndDate.HasValue)
                    {
                        if (!filters.StartDate.HasValue)
                        {
                            leaveSummaryQuery += " AND L.StartDate <= @EndDate";
                        }
                    }
                    
                    leaveSummaryQuery += @" GROUP BY D.ID, D.Name
                        ORDER BY D.Name";
                    
                    using (SqlCommand command = new SqlCommand(leaveSummaryQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "DepartmentLeave");
                    }
                    
                    // استعلام ملخص الرواتب حسب الإدارات
                    string payrollSummaryQuery = $@"
                        SELECT 
                            D.ID AS DepartmentID,
                            D.Name AS DepartmentName,
                            COUNT(DISTINCT PD.EmployeeID) AS EmployeeCount,
                            AVG(PD.BaseSalary) AS AvgBaseSalary,
                            SUM(PD.BaseSalary) AS TotalBaseSalary,
                            AVG(PD.Allowances) AS AvgAllowances,
                            SUM(PD.Allowances) AS TotalAllowances,
                            AVG(PD.Deductions) AS AvgDeductions,
                            SUM(PD.Deductions) AS TotalDeductions,
                            AVG(PD.NetSalary) AS AvgNetSalary,
                            SUM(PD.NetSalary) AS TotalNetSalary
                        FROM PayrollDetails PD
                        INNER JOIN Payrolls P ON PD.PayrollID = P.ID
                        INNER JOIN Employees E ON PD.EmployeeID = E.ID
                        INNER JOIN Departments D ON E.DepartmentID = D.ID
                        {whereClause.Replace("E.", "E.")}";
                    
                    // إضافة شروط كشف الرواتب
                    if (filters.PayrollId.HasValue && filters.PayrollId.Value > 0)
                    {
                        payrollSummaryQuery += " AND PD.PayrollID = @PayrollId";
                    }
                    else if (filters.StartDate.HasValue || filters.EndDate.HasValue)
                    {
                        // استخدام التاريخ للبحث عن كشوفات الرواتب المناسبة
                        if (filters.StartDate.HasValue)
                        {
                            int year = filters.StartDate.Value.Year;
                            int month = filters.StartDate.Value.Month;
                            payrollSummaryQuery += $" AND (P.PayrollYear > {year} OR (P.PayrollYear = {year} AND P.PayrollMonth >= {month}))";
                        }
                        
                        if (filters.EndDate.HasValue)
                        {
                            int year = filters.EndDate.Value.Year;
                            int month = filters.EndDate.Value.Month;
                            payrollSummaryQuery += $" AND (P.PayrollYear < {year} OR (P.PayrollYear = {year} AND P.PayrollMonth <= {month}))";
                        }
                    }
                    
                    payrollSummaryQuery += @" GROUP BY D.ID, D.Name
                        ORDER BY D.Name";
                    
                    using (SqlCommand command = new SqlCommand(payrollSummaryQuery, connection))
                    {
                        // إضافة البارامترات
                        foreach (var param in filters.GetSqlParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "DepartmentPayroll");
                    }
                }
                
                return dataSet;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في جلب بيانات ملخص الإدارات");
                return null;
            }
        }
        
        /// <summary>
        /// الحصول على بيانات ملخص الأداء
        /// </summary>
        /// <param name="filters">فلاتر التقرير</param>
        /// <returns>مجموعة بيانات ملخص الأداء</returns>
        private DataSet GetPerformanceSummaryData(ReportFilterOptions filters)
        {
            DataSet dataSet = new DataSet();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    // استعلام ملخص الأداء للموظفين
                    string performanceQuery = @"
                        SELECT 
                            E.ID AS EmployeeID,
                            E.EmployeeNumber,
                            E.FullName AS EmployeeName,
                            D.Name AS DepartmentName,
                            P.Title AS PositionTitle,
                            
                            -- بيانات الحضور
                            COUNT(A.ID) AS AttendanceRecords,
                            SUM(CASE WHEN A.IsAbsent = 1 THEN 1 ELSE 0 END) AS AbsentCount,
                            SUM(CASE WHEN A.LateMinutes > 0 THEN 1 ELSE 0 END) AS LateCount,
                            AVG(A.LateMinutes) AS AvgLateMinutes,
                            SUM(A.LateMinutes) AS TotalLateMinutes,
                            
                            -- بيانات الإجازات
                            (SELECT COUNT(L.ID) FROM Leave L WHERE L.EmployeeID = E.ID AND L.Status = 'Approved') AS LeaveCount,
                            (SELECT SUM(L.DurationDays) FROM Leave L WHERE L.EmployeeID = E.ID AND L.Status = 'Approved') AS LeaveDays,
                            
                            -- معدل الأداء
                            CASE 
                                WHEN COUNT(A.ID) = 0 THEN 0
                                ELSE (COUNT(A.ID) - SUM(CASE WHEN A.IsAbsent = 1 THEN 1 ELSE 0 END) - SUM(CASE WHEN A.LateMinutes > 30 THEN 0.5 WHEN A.LateMinutes > 0 THEN 0.25 ELSE 0 END)) * 100.0 / COUNT(A.ID)
                            END AS PerformanceRate
                        FROM Employees E
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        LEFT JOIN Positions P ON E.PositionID = P.ID
                        LEFT JOIN Attendance A ON E.ID = A.EmployeeID";
                    
                    // إضافة شروط الفلترة
                    List<string> conditions = new List<string>();
                    
                    // شروط التاريخ
                    if (filters.StartDate.HasValue)
                    {
                        conditions.Add("A.AttendanceDate >= @StartDate");
                    }
                    
                    if (filters.EndDate.HasValue)
                    {
                        conditions.Add("A.AttendanceDate <= @EndDate");
                    }
                    
                    // شروط الإدارة والموظف
                    if (filters.DepartmentId.HasValue && filters.DepartmentId.Value > 0)
                    {
                        conditions.Add("E.DepartmentID = @DepartmentId");
                    }
                    
                    if (filters.EmployeeId.HasValue && filters.EmployeeId.Value > 0)
                    {
                        conditions.Add("E.ID = @EmployeeId");
                    }
                    
                    if (filters.IsActive.HasValue)
                    {
                        conditions.Add("E.IsActive = @IsActive");
                    }
                    
                    // إضافة شروط الفلترة للاستعلام
                    if (conditions.Count > 0)
                    {
                        performanceQuery += " WHERE " + string.Join(" AND ", conditions);
                    }
                    
                    // إضافة التجميع
                    performanceQuery += @"
                        GROUP BY E.ID, E.EmployeeNumber, E.FullName, D.Name, P.Title
                        ORDER BY D.Name, E.FullName";
                    
                    using (SqlCommand command = new SqlCommand(performanceQuery, connection))
                    {
                        // إضافة البارامترات
                        if (filters.StartDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@StartDate", filters.StartDate.Value);
                        }
                        
                        if (filters.EndDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@EndDate", filters.EndDate.Value);
                        }
                        
                        if (filters.DepartmentId.HasValue && filters.DepartmentId.Value > 0)
                        {
                            command.Parameters.AddWithValue("@DepartmentId", filters.DepartmentId.Value);
                        }
                        
                        if (filters.EmployeeId.HasValue && filters.EmployeeId.Value > 0)
                        {
                            command.Parameters.AddWithValue("@EmployeeId", filters.EmployeeId.Value);
                        }
                        
                        if (filters.IsActive.HasValue)
                        {
                            command.Parameters.AddWithValue("@IsActive", filters.IsActive.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "EmployeePerformance");
                    }
                    
                    // استعلام ملخص الأداء حسب الإدارات
                    string departmentPerformanceQuery = @"
                        SELECT 
                            D.ID AS DepartmentID,
                            D.Name AS DepartmentName,
                            COUNT(DISTINCT E.ID) AS EmployeeCount,
                            
                            -- بيانات الحضور
                            COUNT(A.ID) AS AttendanceRecords,
                            SUM(CASE WHEN A.IsAbsent = 1 THEN 1 ELSE 0 END) AS AbsentCount,
                            SUM(CASE WHEN A.LateMinutes > 0 THEN 1 ELSE 0 END) AS LateCount,
                            AVG(A.LateMinutes) AS AvgLateMinutes,
                            
                            -- معدل الأداء
                            CASE 
                                WHEN COUNT(A.ID) = 0 THEN 0
                                ELSE (COUNT(A.ID) - SUM(CASE WHEN A.IsAbsent = 1 THEN 1 ELSE 0 END) - SUM(CASE WHEN A.LateMinutes > 30 THEN 0.5 WHEN A.LateMinutes > 0 THEN 0.25 ELSE 0 END)) * 100.0 / COUNT(A.ID)
                            END AS DepartmentPerformanceRate
                        FROM Departments D
                        LEFT JOIN Employees E ON D.ID = E.DepartmentID
                        LEFT JOIN Attendance A ON E.ID = A.EmployeeID";
                    
                    // إضافة شروط الفلترة
                    if (conditions.Count > 0)
                    {
                        departmentPerformanceQuery += " WHERE " + string.Join(" AND ", conditions);
                    }
                    
                    // إضافة التجميع
                    departmentPerformanceQuery += @"
                        GROUP BY D.ID, D.Name
                        ORDER BY D.Name";
                    
                    using (SqlCommand command = new SqlCommand(departmentPerformanceQuery, connection))
                    {
                        // إضافة البارامترات
                        if (filters.StartDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@StartDate", filters.StartDate.Value);
                        }
                        
                        if (filters.EndDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@EndDate", filters.EndDate.Value);
                        }
                        
                        if (filters.DepartmentId.HasValue && filters.DepartmentId.Value > 0)
                        {
                            command.Parameters.AddWithValue("@DepartmentId", filters.DepartmentId.Value);
                        }
                        
                        if (filters.EmployeeId.HasValue && filters.EmployeeId.Value > 0)
                        {
                            command.Parameters.AddWithValue("@EmployeeId", filters.EmployeeId.Value);
                        }
                        
                        if (filters.IsActive.HasValue)
                        {
                            command.Parameters.AddWithValue("@IsActive", filters.IsActive.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "DepartmentPerformance");
                    }
                }
                
                return dataSet;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في جلب بيانات ملخص الأداء");
                return null;
            }
        }
        
        #endregion
        
        #region بناء التقارير
        
        /// <summary>
        /// بناء تقرير الموظفين
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="dataSet">مجموعة البيانات</param>
        /// <param name="filters">الفلاتر المستخدمة</param>
        private void BuildEmployeesReport(XtraReport report, DataSet dataSet, ReportFilterOptions filters)
        {
            // إضافة عنوان القسم
            GroupHeaderBand tableTitleBand = new GroupHeaderBand();
            tableTitleBand.HeightF = 35;
            report.Bands.Add(tableTitleBand);
            
            // عنوان الجدول
            XRLabel tableTitle = new XRLabel();
            tableTitle.Text = "قائمة الموظفين";
            tableTitle.Font = new Font("Arial", 12, FontStyle.Bold);
            tableTitle.TextAlignment = TextAlignment.MiddleCenter;
            tableTitle.LocationF = new PointF(0, 5);
            tableTitle.SizeF = new SizeF(report.PageWidth - 20, 25);
            tableTitleBand.Controls.Add(tableTitle);
            
            // إضافة قسم التفاصيل
            DetailBand detailBand = new DetailBand();
            report.Bands.Add(detailBand);
            
            // إنشاء جدول الموظفين
            if (dataSet.Tables["Employees"] != null && dataSet.Tables["Employees"].Rows.Count > 0)
            {
                XRTable employeesTable = CreateEmployeesTable(dataSet.Tables["Employees"], report.PageWidth);
                detailBand.Controls.Add(employeesTable);
            }
            
            // إضافة ملخص الإدارات
            if (dataSet.Tables["DepartmentSummary"] != null && dataSet.Tables["DepartmentSummary"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddDepartmentSummaryTable(report, dataSet.Tables["DepartmentSummary"]);
            }
            
            // إضافة رسم بياني للموظفين حسب الإدارات
            if (dataSet.Tables["DepartmentSummary"] != null && dataSet.Tables["DepartmentSummary"].Rows.Count > 0 && filters.IncludeCharts)
            {
                AddEmployeesChart(report, dataSet.Tables["DepartmentSummary"]);
            }
            
            // إضافة ملخص أنواع التوظيف
            if (dataSet.Tables["EmploymentTypeSummary"] != null && dataSet.Tables["EmploymentTypeSummary"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddEmploymentTypeSummaryTable(report, dataSet.Tables["EmploymentTypeSummary"]);
            }
        }
        
        /// <summary>
        /// إنشاء جدول الموظفين
        /// </summary>
        private XRTable CreateEmployeesTable(DataTable dataTable, float pageWidth)
        {
            XRTable table = new XRTable();
            table.LocationF = new PointF(0, 0);
            table.SizeF = new SizeF(pageWidth - 20, dataTable.Rows.Count * 25 + 30);
            table.Borders = BorderSide.All;
            table.BorderWidth = 1;
            
            // إنشاء صف العناوين
            XRTableRow headerRow = new XRTableRow();
            headerRow.HeightF = 30;
            headerRow.BackColor = Color.LightBlue;
            headerRow.Font = new Font("Arial", 9, FontStyle.Bold);
            table.Rows.Add(headerRow);
            
            // إضافة خلايا العناوين
            AddTableCell(headerRow, "م", 30);
            AddTableCell(headerRow, "رقم الموظف", 70);
            AddTableCell(headerRow, "الاسم", 120);
            AddTableCell(headerRow, "الإدارة", 100);
            AddTableCell(headerRow, "المسمى الوظيفي", 100);
            AddTableCell(headerRow, "تاريخ التعيين", 70);
            AddTableCell(headerRow, "الراتب الأساسي", 70);
            AddTableCell(headerRow, "البريد الإلكتروني", 120);
            AddTableCell(headerRow, "الحالة", 50);
            
            // إضافة صفوف البيانات
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                XRTableRow dataRow = new XRTableRow();
                dataRow.HeightF = 25;
                dataRow.BackColor = i % 2 == 0 ? Color.White : Color.WhiteSmoke;
                table.Rows.Add(dataRow);
                
                // إضافة خلايا البيانات
                AddTableCell(dataRow, (i + 1).ToString(), 30);
                AddTableCell(dataRow, row["EmployeeNumber"].ToString(), 70);
                AddTableCell(dataRow, row["FullName"].ToString(), 120);
                AddTableCell(dataRow, row["DepartmentName"].ToString(), 100);
                AddTableCell(dataRow, row["JobTitle"].ToString(), 100);
                
                DateTime hireDate = Convert.ToDateTime(row["HireDate"]);
                AddTableCell(dataRow, hireDate.ToString("yyyy/MM/dd"), 70);
                
                decimal salary = Convert.ToDecimal(row["BasicSalary"]);
                AddTableCell(dataRow, salary.ToString("N2"), 70, TextAlignment.MiddleLeft);
                
                AddTableCell(dataRow, row["Email"].ToString(), 120);
                
                bool isActive = Convert.ToBoolean(row["IsActive"]);
                string status = isActive ? "نشط" : "غير نشط";
                Color statusColor = isActive ? Color.Green : Color.Red;
                AddTableCell(dataRow, status, 50, TextAlignment.MiddleCenter, statusColor);
            }
            
            return table;
        }
        
        /// <summary>
        /// إضافة ملخص الإدارات
        /// </summary>
        private void AddDepartmentSummaryTable(XtraReport report, DataTable summaryTable)
        {
            // إنشاء عنوان القسم
            GroupHeaderBand summaryHeaderBand = new GroupHeaderBand();
            summaryHeaderBand.HeightF = 35;
            report.Bands.Add(summaryHeaderBand);
            
            // عنوان الملخص
            XRLabel summaryTitle = new XRLabel();
            summaryTitle.Text = "ملخص الموظفين حسب الإدارات";
            summaryTitle.Font = new Font("Arial", 12, FontStyle.Bold);
            summaryTitle.TextAlignment = TextAlignment.MiddleCenter;
            summaryTitle.LocationF = new PointF(0, 5);
            summaryTitle.SizeF = new SizeF(report.PageWidth - 20, 25);
            summaryHeaderBand.Controls.Add(summaryTitle);
            
            // إنشاء قسم آخر للتقرير
            GroupHeaderBand summaryBand = new GroupHeaderBand();
            summaryBand.HeightF = summaryTable.Rows.Count * 30 + 30;
            report.Bands.Add(summaryBand);
            
            // إنشاء جدول الملخص
            XRTable departmentTable = new XRTable();
            departmentTable.LocationF = new PointF(10, 0);
            departmentTable.SizeF = new SizeF(report.PageWidth - 20, summaryTable.Rows.Count * 30 + 30);
            departmentTable.Borders = BorderSide.All;
            departmentTable.BorderWidth = 1;
            summaryBand.Controls.Add(departmentTable);
            
            // إنشاء صف العناوين
            XRTableRow headerRow = new XRTableRow();
            headerRow.HeightF = 30;
            headerRow.BackColor = Color.LightBlue;
            headerRow.Font = new Font("Arial", 9, FontStyle.Bold);
            departmentTable.Rows.Add(headerRow);
            
            // إضافة خلايا العناوين
            AddTableCell(headerRow, "الإدارة", 120);
            AddTableCell(headerRow, "عدد الموظفين", 80);
            AddTableCell(headerRow, "نشط", 60);
            AddTableCell(headerRow, "غير نشط", 60);
            AddTableCell(headerRow, "ذكور", 60);
            AddTableCell(headerRow, "إناث", 60);
            AddTableCell(headerRow, "متوسط الراتب", 80);
            AddTableCell(headerRow, "إجمالي الرواتب", 100);
            
            // إضافة صفوف البيانات
            for (int i = 0; i < summaryTable.Rows.Count; i++)
            {
                DataRow row = summaryTable.Rows[i];
                XRTableRow dataRow = new XRTableRow();
                dataRow.HeightF = 30;
                dataRow.BackColor = i % 2 == 0 ? Color.White : Color.WhiteSmoke;
                departmentTable.Rows.Add(dataRow);
                
                // إضافة خلايا البيانات
                AddTableCell(dataRow, row["DepartmentName"].ToString(), 120);
                AddTableCell(dataRow, row["EmployeeCount"].ToString(), 80);
                AddTableCell(dataRow, row["ActiveCount"].ToString(), 60);
                AddTableCell(dataRow, row["InactiveCount"].ToString(), 60);
                AddTableCell(dataRow, row["MaleCount"].ToString(), 60);
                AddTableCell(dataRow, row["FemaleCount"].ToString(), 60);
                
                decimal avgSalary = Convert.ToDecimal(row["AvgSalary"]);
                AddTableCell(dataRow, avgSalary.ToString("N2"), 80);
                
                decimal totalSalary = Convert.ToDecimal(row["TotalSalary"]);
                AddTableCell(dataRow, totalSalary.ToString("N2"), 100);
            }
        }
        
        /// <summary>
        /// إضافة ملخص أنواع التوظيف
        /// </summary>
        private void AddEmploymentTypeSummaryTable(XtraReport report, DataTable summaryTable)
        {
            // إنشاء عنوان القسم
            GroupHeaderBand summaryHeaderBand = new GroupHeaderBand();
            summaryHeaderBand.HeightF = 35;
            report.Bands.Add(summaryHeaderBand);
            
            // عنوان الملخص
            XRLabel summaryTitle = new XRLabel();
            summaryTitle.Text = "ملخص الموظفين حسب نوع التوظيف";
            summaryTitle.Font = new Font("Arial", 12, FontStyle.Bold);
            summaryTitle.TextAlignment = TextAlignment.MiddleCenter;
            summaryTitle.LocationF = new PointF(0, 5);
            summaryTitle.SizeF = new SizeF(report.PageWidth - 20, 25);
            summaryHeaderBand.Controls.Add(summaryTitle);
            
            // إنشاء قسم آخر للتقرير
            GroupHeaderBand summaryBand = new GroupHeaderBand();
            summaryBand.HeightF = summaryTable.Rows.Count * 30 + 30;
            report.Bands.Add(summaryBand);
            
            // إنشاء جدول الملخص
            XRTable typeTable = new XRTable();
            typeTable.LocationF = new PointF(10, 0);
            typeTable.SizeF = new SizeF(report.PageWidth - 20, summaryTable.Rows.Count * 30 + 30);
            typeTable.Borders = BorderSide.All;
            typeTable.BorderWidth = 1;
            summaryBand.Controls.Add(typeTable);
            
            // إنشاء صف العناوين
            XRTableRow headerRow = new XRTableRow();
            headerRow.HeightF = 30;
            headerRow.BackColor = Color.LightBlue;
            headerRow.Font = new Font("Arial", 9, FontStyle.Bold);
            typeTable.Rows.Add(headerRow);
            
            // إضافة خلايا العناوين
            AddTableCell(headerRow, "نوع التوظيف", 150);
            AddTableCell(headerRow, "عدد الموظفين", 100);
            AddTableCell(headerRow, "نشط", 80);
            AddTableCell(headerRow, "متوسط الراتب", 100);
            
            // إضافة صفوف البيانات
            for (int i = 0; i < summaryTable.Rows.Count; i++)
            {
                DataRow row = summaryTable.Rows[i];
                XRTableRow dataRow = new XRTableRow();
                dataRow.HeightF = 30;
                dataRow.BackColor = i % 2 == 0 ? Color.White : Color.WhiteSmoke;
                typeTable.Rows.Add(dataRow);
                
                // إضافة خلايا البيانات
                string employmentType = row["EmploymentType"].ToString();
                switch (employmentType)
                {
                    case "FullTime":
                        employmentType = "دوام كامل";
                        break;
                    case "PartTime":
                        employmentType = "دوام جزئي";
                        break;
                    case "Contract":
                        employmentType = "عقد";
                        break;
                    case "Temporary":
                        employmentType = "مؤقت";
                        break;
                }
                
                AddTableCell(dataRow, employmentType, 150);
                AddTableCell(dataRow, row["EmployeeCount"].ToString(), 100);
                AddTableCell(dataRow, row["ActiveCount"].ToString(), 80);
                
                decimal avgSalary = Convert.ToDecimal(row["AvgSalary"]);
                AddTableCell(dataRow, avgSalary.ToString("N2"), 100);
            }
        }
        
        /// <summary>
        /// إضافة رسم بياني للموظفين
        /// </summary>
        private void AddEmployeesChart(XtraReport report, DataTable summaryTable)
        {
            // إنشاء عنوان القسم
            GroupHeaderBand chartGroupBand = new GroupHeaderBand();
            chartGroupBand.HeightF = 350;
            report.Bands.Add(chartGroupBand);
            
            // عنوان الرسم البياني
            XRLabel chartTitle = new XRLabel();
            chartTitle.Text = "توزيع الموظفين حسب الإدارات";
            chartTitle.Font = new Font("Arial", 12, FontStyle.Bold);
            chartTitle.TextAlignment = TextAlignment.MiddleCenter;
            chartTitle.LocationF = new PointF(0, 10);
            chartTitle.SizeF = new SizeF(report.PageWidth - 20, 25);
            chartGroupBand.Controls.Add(chartTitle);
            
            // إنشاء الرسم البياني
            XRChart chart = new XRChart();
            chart.LocationF = new PointF(0, 40);
            chart.SizeF = new SizeF(report.PageWidth - 20, 300);
            chartGroupBand.Controls.Add(chart);
            
            // تهيئة الرسم البياني
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.Bottom;
            chart.Legend.Direction = LegendDirection.LeftToRight;
            
            // إنشاء سلسلة بيانات الموظفين الكلي
            Series employeesSeries = new Series("عدد الموظفين", ViewType.Bar);
            chart.Series.Add(employeesSeries);
            
            // إنشاء سلسلة بيانات الموظفين النشطين
            Series activeSeries = new Series("نشط", ViewType.Bar);
            chart.Series.Add(activeSeries);
            
            // إنشاء سلسلة بيانات الموظفين غير النشطين
            Series inactiveSeries = new Series("غير نشط", ViewType.Bar);
            chart.Series.Add(inactiveSeries);
            
            // تعيين أنماط العرض
            ((BarSeriesView)employeesSeries.View).Color = Color.SteelBlue;
            ((BarSeriesView)activeSeries.View).Color = Color.Green;
            ((BarSeriesView)inactiveSeries.View).Color = Color.Red;
            
            // إضافة نقاط البيانات
            foreach (DataRow row in summaryTable.Rows)
            {
                string departmentName = row["DepartmentName"].ToString();
                int employeeCount = Convert.ToInt32(row["EmployeeCount"]);
                int activeCount = Convert.ToInt32(row["ActiveCount"]);
                int inactiveCount = Convert.ToInt32(row["InactiveCount"]);
                
                employeesSeries.Points.Add(new SeriesPoint(departmentName, employeeCount));
                activeSeries.Points.Add(new SeriesPoint(departmentName, activeCount));
                inactiveSeries.Points.Add(new SeriesPoint(departmentName, inactiveCount));
            }
            
            // تعيين نمط المحاور
            chart.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            
            // تفعيل التكديس للأعمدة
            XYDiagram diagram = (XYDiagram)chart.Diagram;
            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisXZooming = true;
        }
        
        /// <summary>
        /// بناء تقرير الحضور والغياب
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="dataSet">مجموعة البيانات</param>
        /// <param name="filters">الفلاتر المستخدمة</param>
        private void BuildAttendanceReport(XtraReport report, DataSet dataSet, ReportFilterOptions filters)
        {
            // سيتم إضافة التنفيذ هنا بشكل مشابه لتقرير الموظفين
            // جداول الحضور والملخصات والرسوم البيانية
            
            // إضافة ملخص الحضور حسب الموظفين إذا تم تفعيله
            if (dataSet.Tables["EmployeeSummary"] != null && dataSet.Tables["EmployeeSummary"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddEmployeeAttendanceSummaryTable(report, dataSet.Tables["EmployeeSummary"]);
            }
            
            // إضافة تفاصيل الحضور إذا تم تفعيله
            if (dataSet.Tables["Attendance"] != null && dataSet.Tables["Attendance"].Rows.Count > 0 && filters.IncludeDetails)
            {
                AddAttendanceDetailsTable(report, dataSet.Tables["Attendance"]);
            }
            
            // إضافة ملخص الحضور حسب الإدارات إذا تم تفعيله
            if (dataSet.Tables["DepartmentSummary"] != null && dataSet.Tables["DepartmentSummary"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddDepartmentAttendanceSummaryTable(report, dataSet.Tables["DepartmentSummary"]);
            }
            
            // إضافة رسم بياني للحضور إذا تم تفعيله
            if (dataSet.Tables["DepartmentSummary"] != null && dataSet.Tables["DepartmentSummary"].Rows.Count > 0 && filters.IncludeCharts)
            {
                AddAttendanceChart(report, dataSet.Tables["DepartmentSummary"]);
            }
        }
        
        /// <summary>
        /// بناء تقرير الإجازات
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="dataSet">مجموعة البيانات</param>
        /// <param name="filters">الفلاتر المستخدمة</param>
        private void BuildLeaveReport(XtraReport report, DataSet dataSet, ReportFilterOptions filters)
        {
            // سيتم إضافة التنفيذ هنا بشكل مشابه لتقارير الموظفين والحضور
            // جداول الإجازات والملخصات والرسوم البيانية
            
            // إضافة تفاصيل الإجازات إذا تم تفعيله
            if (dataSet.Tables["Leave"] != null && dataSet.Tables["Leave"].Rows.Count > 0 && filters.IncludeDetails)
            {
                AddLeaveDetailsTable(report, dataSet.Tables["Leave"]);
            }
            
            // إضافة ملخص الإجازات حسب الإدارات إذا تم تفعيله
            if (dataSet.Tables["DepartmentSummary"] != null && dataSet.Tables["DepartmentSummary"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddDepartmentLeaveSummaryTable(report, dataSet.Tables["DepartmentSummary"]);
            }
            
            // إضافة ملخص الإجازات حسب النوع إذا تم تفعيله
            if (dataSet.Tables["LeaveTypeSummary"] != null && dataSet.Tables["LeaveTypeSummary"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddLeaveTypeSummaryTable(report, dataSet.Tables["LeaveTypeSummary"]);
            }
            
            // إضافة رسم بياني للإجازات إذا تم تفعيله
            if (dataSet.Tables["LeaveTypeSummary"] != null && dataSet.Tables["LeaveTypeSummary"].Rows.Count > 0 && filters.IncludeCharts)
            {
                AddLeaveChart(report, dataSet.Tables["LeaveTypeSummary"]);
            }
        }
        
        /// <summary>
        /// بناء تقرير الرواتب
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="dataSet">مجموعة البيانات</param>
        /// <param name="filters">الفلاتر المستخدمة</param>
        private void BuildPayrollReport(XtraReport report, DataSet dataSet, ReportFilterOptions filters)
        {
            // سيتم إضافة التنفيذ هنا بشكل مشابه للتقارير السابقة
            // جداول الرواتب والملخصات والرسوم البيانية
            
            // إضافة معلومات كشف الرواتب إذا تم تحديد كشف محدد
            if (dataSet.Tables["PayrollInfo"] != null && dataSet.Tables["PayrollInfo"].Rows.Count > 0)
            {
                AddPayrollInfoToReport(report, dataSet.Tables["PayrollInfo"].Rows[0]);
            }
            
            // إضافة تفاصيل الرواتب إذا تم تفعيله
            if (dataSet.Tables["PayrollDetails"] != null && dataSet.Tables["PayrollDetails"].Rows.Count > 0 && filters.IncludeDetails)
            {
                AddPayrollDetailsTable(report, dataSet.Tables["PayrollDetails"]);
            }
            
            // إضافة ملخص الرواتب حسب الإدارات إذا تم تفعيله
            if (dataSet.Tables["DepartmentSummary"] != null && dataSet.Tables["DepartmentSummary"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddDepartmentPayrollSummaryTable(report, dataSet.Tables["DepartmentSummary"]);
            }
            
            // إضافة قائمة كشوفات الرواتب إذا لم يتم تحديد كشف محدد
            if (dataSet.Tables["PayrollSummary"] != null && dataSet.Tables["PayrollSummary"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddPayrollSummaryTable(report, dataSet.Tables["PayrollSummary"]);
            }
            
            // إضافة رسم بياني للرواتب إذا تم تفعيله
            if (dataSet.Tables["DepartmentSummary"] != null && dataSet.Tables["DepartmentSummary"].Rows.Count > 0 && filters.IncludeCharts)
            {
                AddPayrollChart(report, dataSet.Tables["DepartmentSummary"]);
            }
        }
        
        /// <summary>
        /// بناء تقرير تفاصيل الموظف
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="dataSet">مجموعة البيانات</param>
        /// <param name="filters">الفلاتر المستخدمة</param>
        private void BuildEmployeeDetailsReport(XtraReport report, DataSet dataSet, ReportFilterOptions filters)
        {
            // سيتم إضافة التنفيذ هنا بشكل مشابه للتقارير السابقة
            // معلومات الموظف وسجلات الحضور والإجازات والرواتب
            
            // إضافة معلومات الموظف
            if (dataSet.Tables["Employee"] != null && dataSet.Tables["Employee"].Rows.Count > 0)
            {
                AddEmployeeInfoToReport(report, dataSet.Tables["Employee"].Rows[0]);
            }
            
            // إضافة ملخص الحضور
            if (dataSet.Tables["AttendanceSummary"] != null && dataSet.Tables["AttendanceSummary"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddEmployeeAttendanceSummary(report, dataSet.Tables["AttendanceSummary"].Rows[0]);
            }
            
            // إضافة سجل الحضور إذا تم تفعيله
            if (dataSet.Tables["Attendance"] != null && dataSet.Tables["Attendance"].Rows.Count > 0 && filters.IncludeDetails)
            {
                AddEmployeeAttendanceTable(report, dataSet.Tables["Attendance"]);
            }
            
            // إضافة ملخص الإجازات إذا تم تفعيله
            if (dataSet.Tables["LeaveSummary"] != null && dataSet.Tables["LeaveSummary"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddEmployeeLeaveSummary(report, dataSet.Tables["LeaveSummary"]);
            }
            
            // إضافة سجل الإجازات إذا تم تفعيله
            if (dataSet.Tables["Leave"] != null && dataSet.Tables["Leave"].Rows.Count > 0 && filters.IncludeDetails)
            {
                AddEmployeeLeaveTable(report, dataSet.Tables["Leave"]);
            }
            
            // إضافة سجل الرواتب إذا تم تفعيله
            if (dataSet.Tables["Payroll"] != null && dataSet.Tables["Payroll"].Rows.Count > 0 && filters.IncludeDetails)
            {
                AddEmployeePayrollTable(report, dataSet.Tables["Payroll"]);
            }
        }
        
        /// <summary>
        /// بناء تقرير ملخص الإدارات
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="dataSet">مجموعة البيانات</param>
        /// <param name="filters">الفلاتر المستخدمة</param>
        private void BuildDepartmentSummaryReport(XtraReport report, DataSet dataSet, ReportFilterOptions filters)
        {
            // سيتم إضافة التنفيذ هنا بشكل مشابه للتقارير السابقة
            // جداول الإدارات والملخصات والرسوم البيانية
            
            // إضافة قائمة الإدارات
            if (dataSet.Tables["Departments"] != null && dataSet.Tables["Departments"].Rows.Count > 0)
            {
                AddDepartmentsTable(report, dataSet.Tables["Departments"]);
            }
            
            // إضافة ملخص الحضور حسب الإدارات إذا تم تفعيله
            if (dataSet.Tables["DepartmentAttendance"] != null && dataSet.Tables["DepartmentAttendance"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddDepartmentAttendanceTable(report, dataSet.Tables["DepartmentAttendance"]);
            }
            
            // إضافة ملخص الإجازات حسب الإدارات إذا تم تفعيله
            if (dataSet.Tables["DepartmentLeave"] != null && dataSet.Tables["DepartmentLeave"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddDepartmentLeaveTable(report, dataSet.Tables["DepartmentLeave"]);
            }
            
            // إضافة ملخص الرواتب حسب الإدارات إذا تم تفعيله
            if (dataSet.Tables["DepartmentPayroll"] != null && dataSet.Tables["DepartmentPayroll"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddDepartmentPayrollTable(report, dataSet.Tables["DepartmentPayroll"]);
            }
            
            // إضافة رسم بياني للإدارات إذا تم تفعيله
            if (dataSet.Tables["Departments"] != null && dataSet.Tables["Departments"].Rows.Count > 0 && filters.IncludeCharts)
            {
                AddDepartmentsChart(report, dataSet.Tables["Departments"]);
            }
        }
        
        /// <summary>
        /// بناء تقرير ملخص الأداء
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="dataSet">مجموعة البيانات</param>
        /// <param name="filters">الفلاتر المستخدمة</param>
        private void BuildPerformanceSummaryReport(XtraReport report, DataSet dataSet, ReportFilterOptions filters)
        {
            // سيتم إضافة التنفيذ هنا بشكل مشابه للتقارير السابقة
            // جداول الأداء والملخصات والرسوم البيانية
            
            // إضافة أداء الموظفين
            if (dataSet.Tables["EmployeePerformance"] != null && dataSet.Tables["EmployeePerformance"].Rows.Count > 0)
            {
                AddEmployeePerformanceTable(report, dataSet.Tables["EmployeePerformance"]);
            }
            
            // إضافة ملخص أداء الإدارات إذا تم تفعيله
            if (dataSet.Tables["DepartmentPerformance"] != null && dataSet.Tables["DepartmentPerformance"].Rows.Count > 0 && filters.IncludeSummary)
            {
                AddDepartmentPerformanceTable(report, dataSet.Tables["DepartmentPerformance"]);
            }
            
            // إضافة رسم بياني للأداء إذا تم تفعيله
            if (dataSet.Tables["DepartmentPerformance"] != null && dataSet.Tables["DepartmentPerformance"].Rows.Count > 0 && filters.IncludeCharts)
            {
                AddPerformanceChart(report, dataSet.Tables["DepartmentPerformance"]);
            }
        }
        
        #endregion
        
        #region طرق مساعدة
        
        /// <summary>
        /// إضافة خلية إلى صف في الجدول
        /// </summary>
        private void AddTableCell(XRTableRow row, string text, float width, TextAlignment textAlignment = TextAlignment.MiddleCenter, Color? textColor = null)
        {
            XRTableCell cell = new XRTableCell();
            cell.Text = text;
            cell.TextAlignment = textAlignment;
            cell.Borders = BorderSide.All;
            cell.BorderWidth = 1;
            cell.Font = new Font("Arial", 9);
            cell.WidthF = width;
            
            if (textColor.HasValue)
            {
                cell.ForeColor = textColor.Value;
            }
            
            row.Cells.Add(cell);
        }
        
        /// <summary>
        /// الحصول على اسم الإدارة
        /// </summary>
        private string GetDepartmentName(int departmentId)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT Name FROM Departments WHERE ID = @DepartmentId";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DepartmentId", departmentId);
                        
                        object result = command.ExecuteScalar();
                        return result != null ? result.ToString() : "غير محدد";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في الحصول على اسم الإدارة");
                return "غير محدد";
            }
        }
        
        /// <summary>
        /// الحصول على اسم الموظف
        /// </summary>
        private string GetEmployeeName(int employeeId)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT FullName FROM Employees WHERE ID = @EmployeeId";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);
                        
                        object result = command.ExecuteScalar();
                        return result != null ? result.ToString() : "غير محدد";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في الحصول على اسم الموظف");
                return "غير محدد";
            }
        }
        
        /// <summary>
        /// الحصول على اسم نوع الإجازة
        /// </summary>
        private string GetLeaveTypeName(int leaveTypeId)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT Name FROM LeaveTypes WHERE ID = @LeaveTypeId";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LeaveTypeId", leaveTypeId);
                        
                        object result = command.ExecuteScalar();
                        return result != null ? result.ToString() : "غير محدد";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في الحصول على اسم نوع الإجازة");
                return "غير محدد";
            }
        }
        
        /// <summary>
        /// الحصول على اسم كشف الرواتب
        /// </summary>
        private string GetPayrollName(int payrollId)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT PayrollName FROM Payrolls WHERE ID = @PayrollId";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PayrollId", payrollId);
                        
                        object result = command.ExecuteScalar();
                        return result != null ? result.ToString() : "غير محدد";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في الحصول على اسم كشف الرواتب");
                return "غير محدد";
            }
        }
        
        // هنا يمكن إضافة طرق AddXXX المختلفة لإضافة الجداول والرسوم البيانية المختلفة
        // سيتم تنفيذها بشكل مشابه للطرق السابقة في التقارير المحددة
        
        #endregion
        
        #region طرق عرض وطباعة التقارير
        
        /// <summary>
        /// عرض التقرير المخصص
        /// </summary>
        /// <param name="reportType">نوع التقرير</param>
        /// <param name="reportTitle">عنوان التقرير</param>
        /// <param name="filters">فلاتر التقرير</param>
        public void ShowCustomReport(CustomReportType reportType, string reportTitle, ReportFilterOptions filters)
        {
            XtraReport report = GenerateCustomReport(reportType, reportTitle, filters);
            _reportManager.ShowPreview(report);
        }
        
        /// <summary>
        /// طباعة التقرير المخصص
        /// </summary>
        /// <param name="reportType">نوع التقرير</param>
        /// <param name="reportTitle">عنوان التقرير</param>
        /// <param name="filters">فلاتر التقرير</param>
        public void PrintCustomReport(CustomReportType reportType, string reportTitle, ReportFilterOptions filters)
        {
            XtraReport report = GenerateCustomReport(reportType, reportTitle, filters);
            _reportManager.Print(report);
        }
        
        /// <summary>
        /// تصدير التقرير المخصص إلى PDF
        /// </summary>
        /// <param name="reportType">نوع التقرير</param>
        /// <param name="reportTitle">عنوان التقرير</param>
        /// <param name="filters">فلاتر التقرير</param>
        /// <param name="filePath">مسار الملف</param>
        public void ExportCustomReportToPdf(CustomReportType reportType, string reportTitle, ReportFilterOptions filters, string filePath)
        {
            XtraReport report = GenerateCustomReport(reportType, reportTitle, filters);
            _reportManager.ExportToPdf(report, filePath);
        }
        
        #endregion
    }
}