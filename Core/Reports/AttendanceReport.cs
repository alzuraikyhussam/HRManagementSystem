using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraCharts;
using HR.Core;

namespace HR.Core.Reports
{
    /// <summary>
    /// تقرير الحضور والغياب
    /// </summary>
    public class AttendanceReport
    {
        private readonly ReportManager _reportManager;
        private readonly ConnectionManager _connectionManager;
        
        public AttendanceReport()
        {
            _reportManager = new ReportManager();
            _connectionManager = new ConnectionManager();
        }
        
        /// <summary>
        /// إنشاء تقرير حضور وغياب للموظفين
        /// </summary>
        /// <param name="departmentId">معرف الإدارة (0 لكل الإدارات)</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <returns>تقرير الحضور والغياب</returns>
        public XtraReport GenerateAttendanceReport(int departmentId, DateTime startDate, DateTime endDate)
        {
            try
            {
                // إنشاء تقرير جديد باستخدام القالب الموحد
                XtraReport report = _reportManager.CreateBaseReport("تقرير الحضور والغياب");
                
                // جلب البيانات
                DataSet dataSet = GetAttendanceData(departmentId, startDate, endDate);
                
                if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
                {
                    XRLabel noDataLabel = new XRLabel();
                    noDataLabel.Text = "لا توجد بيانات متاحة لهذه الفترة";
                    noDataLabel.Font = new Font("Arial", 12, FontStyle.Bold);
                    noDataLabel.TextAlignment = TextAlignment.MiddleCenter;
                    noDataLabel.SizeF = new SizeF(report.PageWidth - 40, 40);
                    noDataLabel.LocationF = new PointF(20, 10);
                    
                    DetailBand detailBand = new DetailBand();
                    detailBand.Controls.Add(noDataLabel);
                    report.Bands.Add(detailBand);
                    
                    return report;
                }
                
                // إضافة معلومات التقرير
                AddReportInfoToHeader(report, departmentId, startDate, endDate, dataSet.Tables[0]);
                
                // إنشاء تقرير حضور الموظفين
                CreateAttendanceSummaryReport(report, dataSet.Tables[0]);
                
                // إضافة ملخص إحصائي
                AddStatisticalSummary(report, dataSet.Tables[1]);
                
                // إضافة رسم بياني
                AddAttendanceChart(report, dataSet.Tables[1]);
                
                return report;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في إنشاء تقرير الحضور والغياب");
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
        /// جلب بيانات الحضور والغياب
        /// </summary>
        /// <param name="departmentId">معرف الإدارة</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <returns>مجموعة بيانات تحتوي على بيانات الحضور والغياب</returns>
        private DataSet GetAttendanceData(int departmentId, DateTime startDate, DateTime endDate)
        {
            DataSet dataSet = new DataSet();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    // استعلام جلب بيانات الحضور والغياب التفصيلية
                    string attendanceQuery = @"
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
                        WHERE A.AttendanceDate BETWEEN @StartDate AND @EndDate
                        " + (departmentId > 0 ? " AND E.DepartmentID = @DepartmentId" : "") + @"
                        ORDER BY A.AttendanceDate DESC, E.FullName";
                    
                    using (SqlCommand command = new SqlCommand(attendanceQuery, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.Date);
                        command.Parameters.AddWithValue("@EndDate", endDate.Date);
                        
                        if (departmentId > 0)
                        {
                            command.Parameters.AddWithValue("@DepartmentId", departmentId);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "AttendanceDetail");
                    }
                    
                    // استعلام جلب ملخص إحصائي للحضور حسب الإدارة
                    string summaryQuery = @"
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
                        WHERE A.AttendanceDate BETWEEN @StartDate AND @EndDate
                        " + (departmentId > 0 ? " AND E.DepartmentID = @DepartmentId" : "") + @"
                        GROUP BY D.Name
                        ORDER BY D.Name";
                    
                    using (SqlCommand command = new SqlCommand(summaryQuery, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.Date);
                        command.Parameters.AddWithValue("@EndDate", endDate.Date);
                        
                        if (departmentId > 0)
                        {
                            command.Parameters.AddWithValue("@DepartmentId", departmentId);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "AttendanceSummary");
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
        /// إضافة معلومات التقرير إلى الترويسة
        /// </summary>
        private void AddReportInfoToHeader(XtraReport report, int departmentId, DateTime startDate, DateTime endDate, DataTable attendanceTable)
        {
            GroupHeaderBand infoHeaderBand = new GroupHeaderBand();
            infoHeaderBand.HeightF = 80;
            report.Bands.Add(infoHeaderBand);
            
            // الإدارة
            XRLabel departmentLabel = new XRLabel();
            string departmentName = "كل الإدارات";
            
            if (departmentId > 0 && attendanceTable.Rows.Count > 0)
            {
                departmentName = attendanceTable.Rows[0]["DepartmentName"].ToString();
            }
            
            departmentLabel.Text = "الإدارة: " + departmentName;
            departmentLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            departmentLabel.LocationF = new PointF(report.PageWidth - 280, 10);
            departmentLabel.SizeF = new SizeF(260, 20);
            departmentLabel.TextAlignment = TextAlignment.MiddleRight;
            infoHeaderBand.Controls.Add(departmentLabel);
            
            // الفترة
            XRLabel periodLabel = new XRLabel();
            periodLabel.Text = $"الفترة: من {startDate.ToString("yyyy/MM/dd")} إلى {endDate.ToString("yyyy/MM/dd")}";
            periodLabel.Font = new Font("Arial", 10);
            periodLabel.LocationF = new PointF(report.PageWidth - 280, 30);
            periodLabel.SizeF = new SizeF(260, 20);
            periodLabel.TextAlignment = TextAlignment.MiddleRight;
            infoHeaderBand.Controls.Add(periodLabel);
            
            // عدد أيام الدوام
            int workingDays = (int)(endDate - startDate).TotalDays + 1;
            XRLabel workingDaysLabel = new XRLabel();
            workingDaysLabel.Text = $"عدد أيام الدوام: {workingDays} يوم";
            workingDaysLabel.Font = new Font("Arial", 10);
            workingDaysLabel.LocationF = new PointF(report.PageWidth - 280, 50);
            workingDaysLabel.SizeF = new SizeF(260, 20);
            workingDaysLabel.TextAlignment = TextAlignment.MiddleRight;
            infoHeaderBand.Controls.Add(workingDaysLabel);
            
            // عدد الموظفين
            int employeeCount = GetDistinctEmployeeCount(attendanceTable);
            XRLabel employeeCountLabel = new XRLabel();
            employeeCountLabel.Text = $"عدد الموظفين: {employeeCount} موظف";
            employeeCountLabel.Font = new Font("Arial", 10);
            employeeCountLabel.LocationF = new PointF(10, 10);
            employeeCountLabel.SizeF = new SizeF(200, 20);
            employeeCountLabel.TextAlignment = TextAlignment.MiddleLeft;
            infoHeaderBand.Controls.Add(employeeCountLabel);
            
            // خط أفقي
            XRLine xrLine = new XRLine();
            xrLine.LocationF = new PointF(0, 75);
            xrLine.SizeF = new SizeF(report.PageWidth - 20, 2);
            xrLine.LineWidth = 1;
            infoHeaderBand.Controls.Add(xrLine);
        }
        
        /// <summary>
        /// الحصول على عدد الموظفين المتميزين
        /// </summary>
        private int GetDistinctEmployeeCount(DataTable attendanceTable)
        {
            HashSet<int> distinctEmployees = new HashSet<int>();
            
            foreach (DataRow row in attendanceTable.Rows)
            {
                int employeeId = Convert.ToInt32(row["EmployeeID"]);
                distinctEmployees.Add(employeeId);
            }
            
            return distinctEmployees.Count;
        }
        
        /// <summary>
        /// إنشاء تقرير ملخص الحضور
        /// </summary>
        private void CreateAttendanceSummaryReport(XtraReport report, DataTable attendanceTable)
        {
            // إنشاء عنوان القسم
            GroupHeaderBand tableTitleBand = new GroupHeaderBand();
            tableTitleBand.HeightF = 35;
            report.Bands.Add(tableTitleBand);
            
            // عنوان الجدول
            XRLabel tableTitle = new XRLabel();
            tableTitle.Text = "ملخص سجلات الحضور والغياب";
            tableTitle.Font = new Font("Arial", 12, FontStyle.Bold);
            tableTitle.TextAlignment = TextAlignment.MiddleCenter;
            tableTitle.LocationF = new PointF(0, 5);
            tableTitle.SizeF = new SizeF(report.PageWidth - 20, 25);
            tableTitleBand.Controls.Add(tableTitle);
            
            // إنشاء قسم Detail للتقرير
            DetailBand detailBand = new DetailBand();
            report.Bands.Add(detailBand);
            
            // إنشاء جدول الحضور
            XRTable attendanceTable1 = CreateAttendanceTable(attendanceTable, report.PageWidth);
            detailBand.Controls.Add(attendanceTable1);
        }
        
        /// <summary>
        /// إنشاء جدول الحضور
        /// </summary>
        private XRTable CreateAttendanceTable(DataTable dataTable, float pageWidth)
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
            AddTableCell(headerRow, "م", 40);
            AddTableCell(headerRow, "الرقم الوظيفي", 80);
            AddTableCell(headerRow, "اسم الموظف", 150);
            AddTableCell(headerRow, "الإدارة", 100);
            AddTableCell(headerRow, "التاريخ", 80);
            AddTableCell(headerRow, "وقت الحضور", 80);
            AddTableCell(headerRow, "وقت الانصراف", 80);
            AddTableCell(headerRow, "ساعات العمل", 80);
            AddTableCell(headerRow, "التأخير (دقيقة)", 80);
            AddTableCell(headerRow, "الحالة", 80);
            
            // إضافة صفوف البيانات
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                XRTableRow dataRow = new XRTableRow();
                dataRow.HeightF = 25;
                dataRow.BackColor = i % 2 == 0 ? Color.White : Color.WhiteSmoke;
                table.Rows.Add(dataRow);
                
                // إضافة خلايا البيانات
                AddTableCell(dataRow, (i + 1).ToString(), 40);
                AddTableCell(dataRow, row["EmployeeNumber"].ToString(), 80);
                AddTableCell(dataRow, row["EmployeeName"].ToString(), 150);
                AddTableCell(dataRow, row["DepartmentName"].ToString(), 100);
                
                DateTime attendanceDate = Convert.ToDateTime(row["AttendanceDate"]);
                AddTableCell(dataRow, attendanceDate.ToString("yyyy/MM/dd"), 80);
                
                string checkInTime = row["CheckInTime"] != DBNull.Value ? ((TimeSpan)row["CheckInTime"]).ToString(@"hh\:mm") : "-";
                AddTableCell(dataRow, checkInTime, 80);
                
                string checkOutTime = row["CheckOutTime"] != DBNull.Value ? ((TimeSpan)row["CheckOutTime"]).ToString(@"hh\:mm") : "-";
                AddTableCell(dataRow, checkOutTime, 80);
                
                decimal workHours = row["WorkHours"] != DBNull.Value ? Convert.ToDecimal(row["WorkHours"]) : 0;
                AddTableCell(dataRow, workHours.ToString("N2"), 80);
                
                int lateMinutes = row["LateMinutes"] != DBNull.Value ? Convert.ToInt32(row["LateMinutes"]) : 0;
                AddTableCell(dataRow, lateMinutes.ToString(), 80);
                
                bool isAbsent = Convert.ToBoolean(row["IsAbsent"]);
                bool isExcused = Convert.ToBoolean(row["IsExcused"]);
                string status = isAbsent ? (isExcused ? "غياب بعذر" : "غياب") : "حضور";
                
                Color statusColor = isAbsent ? (isExcused ? Color.Orange : Color.Red) : Color.Green;
                AddTableCell(dataRow, status, 80, TextAlignment.MiddleCenter, statusColor);
            }
            
            return table;
        }
        
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
        /// إضافة ملخص إحصائي
        /// </summary>
        private void AddStatisticalSummary(XtraReport report, DataTable summaryTable)
        {
            // إنشاء عنوان القسم
            GroupHeaderBand summaryHeaderBand = new GroupHeaderBand();
            summaryHeaderBand.HeightF = 35;
            report.Bands.Add(summaryHeaderBand);
            
            // عنوان الملخص
            XRLabel summaryTitle = new XRLabel();
            summaryTitle.Text = "ملخص إحصائي للحضور والغياب حسب الإدارات";
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
            XRTable summaryTable1 = new XRTable();
            summaryTable1.LocationF = new PointF(10, 0);
            summaryTable1.SizeF = new SizeF(report.PageWidth - 20, summaryTable.Rows.Count * 30 + 30);
            summaryTable1.Borders = BorderSide.All;
            summaryTable1.BorderWidth = 1;
            summaryBand.Controls.Add(summaryTable1);
            
            // إنشاء صف العناوين
            XRTableRow headerRow = new XRTableRow();
            headerRow.HeightF = 30;
            headerRow.BackColor = Color.LightBlue;
            headerRow.Font = new Font("Arial", 9, FontStyle.Bold);
            summaryTable1.Rows.Add(headerRow);
            
            // إضافة خلايا العناوين
            AddTableCell(headerRow, "الإدارة", 150);
            AddTableCell(headerRow, "عدد الموظفين", 80);
            AddTableCell(headerRow, "الغياب", 60);
            AddTableCell(headerRow, "الغياب بعذر", 80);
            AddTableCell(headerRow, "التأخير", 60);
            AddTableCell(headerRow, "الانصراف المبكر", 100);
            AddTableCell(headerRow, "العمل الإضافي", 80);
            AddTableCell(headerRow, "متوسط ساعات العمل", 100);
            
            // إضافة صفوف البيانات
            for (int i = 0; i < summaryTable.Rows.Count; i++)
            {
                DataRow row = summaryTable.Rows[i];
                XRTableRow dataRow = new XRTableRow();
                dataRow.HeightF = 30;
                dataRow.BackColor = i % 2 == 0 ? Color.White : Color.WhiteSmoke;
                summaryTable1.Rows.Add(dataRow);
                
                // إضافة خلايا البيانات
                AddTableCell(dataRow, row["DepartmentName"].ToString(), 150);
                AddTableCell(dataRow, row["EmployeeCount"].ToString(), 80);
                AddTableCell(dataRow, row["AbsentCount"].ToString(), 60);
                AddTableCell(dataRow, row["ExcusedCount"].ToString(), 80);
                AddTableCell(dataRow, row["LateCount"].ToString(), 60);
                AddTableCell(dataRow, row["EarlyDepartureCount"].ToString(), 100);
                AddTableCell(dataRow, row["OverTimeCount"].ToString(), 80);
                
                decimal avgWorkHours = Convert.ToDecimal(row["AvgWorkHours"]);
                AddTableCell(dataRow, avgWorkHours.ToString("N2"), 100);
            }
        }
        
        /// <summary>
        /// إضافة رسم بياني للحضور والغياب
        /// </summary>
        private void AddAttendanceChart(XtraReport report, DataTable summaryTable)
        {
            // إنشاء عنوان القسم
            GroupHeaderBand chartGroupBand = new GroupHeaderBand();
            chartGroupBand.HeightF = 350;
            report.Bands.Add(chartGroupBand);
            
            // عنوان الرسم البياني
            XRLabel chartTitle = new XRLabel();
            chartTitle.Text = "توزيع الحضور والغياب حسب الإدارات";
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
            
            // إنشاء سلسلة بيانات الحضور
            Series presentSeries = new Series("الحضور", ViewType.Bar);
            Series absentSeries = new Series("الغياب", ViewType.Bar);
            Series lateSeries = new Series("التأخير", ViewType.Bar);
            
            chart.Series.Add(presentSeries);
            chart.Series.Add(absentSeries);
            chart.Series.Add(lateSeries);
            
            // تعيين أنماط العرض
            ((BarSeriesView)presentSeries.View).Color = Color.Green;
            ((BarSeriesView)absentSeries.View).Color = Color.Red;
            ((BarSeriesView)lateSeries.View).Color = Color.Orange;
            
            // إضافة نقاط البيانات
            foreach (DataRow row in summaryTable.Rows)
            {
                string departmentName = row["DepartmentName"].ToString();
                int employeeCount = Convert.ToInt32(row["EmployeeCount"]);
                int absentCount = Convert.ToInt32(row["AbsentCount"]);
                int lateCount = Convert.ToInt32(row["LateCount"]);
                
                // عدد الموظفين الحاضرين = إجمالي الموظفين - الغياب
                int presentCount = employeeCount - absentCount;
                
                presentSeries.Points.Add(new SeriesPoint(departmentName, presentCount));
                absentSeries.Points.Add(new SeriesPoint(departmentName, absentCount));
                lateSeries.Points.Add(new SeriesPoint(departmentName, lateCount));
            }
            
            // تعيين نمط المحاور
            chart.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            
            // تفعيل التكديس للأعمدة
            ((XYDiagram)chart.Diagram).EnableAxisXScrolling = true;
            ((XYDiagram)chart.Diagram).EnableAxisXZooming = true;
        }
        
        /// <summary>
        /// عرض تقرير الحضور والغياب
        /// </summary>
        /// <param name="departmentId">معرف الإدارة</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        public void ShowAttendanceReport(int departmentId, DateTime startDate, DateTime endDate)
        {
            XtraReport report = GenerateAttendanceReport(departmentId, startDate, endDate);
            _reportManager.ShowPreview(report);
        }
        
        /// <summary>
        /// طباعة تقرير الحضور والغياب
        /// </summary>
        /// <param name="departmentId">معرف الإدارة</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        public void PrintAttendanceReport(int departmentId, DateTime startDate, DateTime endDate)
        {
            XtraReport report = GenerateAttendanceReport(departmentId, startDate, endDate);
            _reportManager.Print(report);
        }
        
        /// <summary>
        /// تصدير تقرير الحضور والغياب إلى PDF
        /// </summary>
        /// <param name="departmentId">معرف الإدارة</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <param name="filePath">مسار الملف</param>
        public void ExportAttendanceReportToPdf(int departmentId, DateTime startDate, DateTime endDate, string filePath)
        {
            XtraReport report = GenerateAttendanceReport(departmentId, startDate, endDate);
            _reportManager.ExportToPdf(report, filePath);
        }
    }
}