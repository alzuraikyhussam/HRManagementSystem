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
    /// تقرير الإجازات
    /// </summary>
    public class LeaveReport
    {
        private readonly ReportManager _reportManager;
        private readonly ConnectionManager _connectionManager;
        
        public LeaveReport()
        {
            _reportManager = new ReportManager();
            _connectionManager = new ConnectionManager();
        }
        
        /// <summary>
        /// إنشاء تقرير الإجازات
        /// </summary>
        /// <param name="departmentId">معرف الإدارة (0 لكل الإدارات)</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <param name="leaveTypeId">معرف نوع الإجازة (0 لكل الأنواع)</param>
        /// <returns>تقرير الإجازات</returns>
        public XtraReport GenerateLeaveReport(int departmentId, DateTime startDate, DateTime endDate, int leaveTypeId = 0)
        {
            try
            {
                // إنشاء تقرير جديد باستخدام القالب الموحد
                XtraReport report = _reportManager.CreateBaseReport("تقرير الإجازات");
                
                // جلب البيانات
                DataSet dataSet = GetLeaveData(departmentId, startDate, endDate, leaveTypeId);
                
                if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
                {
                    XRLabel noDataLabel = new XRLabel();
                    noDataLabel.Text = "لا توجد بيانات إجازات متاحة لهذه الفترة";
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
                AddReportInfoToHeader(report, departmentId, startDate, endDate, dataSet.Tables[0], leaveTypeId);
                
                // إنشاء تقرير تفصيلي للإجازات
                CreateLeaveDetailsReport(report, dataSet.Tables[0]);
                
                // إضافة ملخص الإجازات حسب الإدارة
                AddDepartmentSummary(report, dataSet.Tables[1]);
                
                // إضافة ملخص الإجازات حسب النوع
                AddLeaveTypeSummary(report, dataSet.Tables[2]);
                
                // إضافة رسم بياني
                AddLeaveChart(report, dataSet.Tables[2]);
                
                return report;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في إنشاء تقرير الإجازات");
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
        /// جلب بيانات الإجازات
        /// </summary>
        /// <param name="departmentId">معرف الإدارة</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <param name="leaveTypeId">معرف نوع الإجازة</param>
        /// <returns>مجموعة بيانات تحتوي على بيانات الإجازات</returns>
        private DataSet GetLeaveData(int departmentId, DateTime startDate, DateTime endDate, int leaveTypeId)
        {
            DataSet dataSet = new DataSet();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    // استعلام جلب بيانات الإجازات التفصيلية
                    string leaveQuery = @"
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
                        WHERE L.StartDate <= @EndDate AND L.EndDate >= @StartDate
                        " + (departmentId > 0 ? " AND E.DepartmentID = @DepartmentId" : "") + @"
                        " + (leaveTypeId > 0 ? " AND L.LeaveTypeID = @LeaveTypeId" : "") + @"
                        ORDER BY L.StartDate DESC, E.FullName";
                    
                    using (SqlCommand command = new SqlCommand(leaveQuery, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.Date);
                        command.Parameters.AddWithValue("@EndDate", endDate.Date);
                        
                        if (departmentId > 0)
                        {
                            command.Parameters.AddWithValue("@DepartmentId", departmentId);
                        }
                        
                        if (leaveTypeId > 0)
                        {
                            command.Parameters.AddWithValue("@LeaveTypeId", leaveTypeId);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "LeaveDetail");
                    }
                    
                    // استعلام جلب ملخص الإجازات حسب الإدارة
                    string departmentSummaryQuery = @"
                        SELECT 
                            D.Name AS DepartmentName,
                            COUNT(DISTINCT L.EmployeeID) AS EmployeeCount,
                            COUNT(L.ID) AS LeaveCount,
                            SUM(L.DurationDays) AS TotalDays,
                            AVG(L.DurationDays) AS AvgDuration
                        FROM Leave L
                        INNER JOIN Employees E ON L.EmployeeID = E.ID
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        WHERE L.StartDate <= @EndDate AND L.EndDate >= @StartDate
                        " + (departmentId > 0 ? " AND E.DepartmentID = @DepartmentId" : "") + @"
                        " + (leaveTypeId > 0 ? " AND L.LeaveTypeID = @LeaveTypeId" : "") + @"
                        GROUP BY D.Name
                        ORDER BY D.Name";
                    
                    using (SqlCommand command = new SqlCommand(departmentSummaryQuery, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.Date);
                        command.Parameters.AddWithValue("@EndDate", endDate.Date);
                        
                        if (departmentId > 0)
                        {
                            command.Parameters.AddWithValue("@DepartmentId", departmentId);
                        }
                        
                        if (leaveTypeId > 0)
                        {
                            command.Parameters.AddWithValue("@LeaveTypeId", leaveTypeId);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "DepartmentSummary");
                    }
                    
                    // استعلام جلب ملخص الإجازات حسب النوع
                    string leaveTypeSummaryQuery = @"
                        SELECT 
                            LT.Name AS LeaveTypeName,
                            COUNT(L.ID) AS LeaveCount,
                            SUM(L.DurationDays) AS TotalDays,
                            AVG(L.DurationDays) AS AvgDuration,
                            COUNT(DISTINCT L.EmployeeID) AS EmployeeCount
                        FROM Leave L
                        INNER JOIN Employees E ON L.EmployeeID = E.ID
                        LEFT JOIN LeaveTypes LT ON L.LeaveTypeID = LT.ID
                        WHERE L.StartDate <= @EndDate AND L.EndDate >= @StartDate
                        " + (departmentId > 0 ? " AND E.DepartmentID = @DepartmentId" : "") + @"
                        " + (leaveTypeId > 0 ? " AND L.LeaveTypeID = @LeaveTypeId" : "") + @"
                        GROUP BY LT.Name
                        ORDER BY COUNT(L.ID) DESC";
                    
                    using (SqlCommand command = new SqlCommand(leaveTypeSummaryQuery, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.Date);
                        command.Parameters.AddWithValue("@EndDate", endDate.Date);
                        
                        if (departmentId > 0)
                        {
                            command.Parameters.AddWithValue("@DepartmentId", departmentId);
                        }
                        
                        if (leaveTypeId > 0)
                        {
                            command.Parameters.AddWithValue("@LeaveTypeId", leaveTypeId);
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
        /// إضافة معلومات التقرير إلى الترويسة
        /// </summary>
        private void AddReportInfoToHeader(XtraReport report, int departmentId, DateTime startDate, DateTime endDate, DataTable leaveTable, int leaveTypeId)
        {
            GroupHeaderBand infoHeaderBand = new GroupHeaderBand();
            infoHeaderBand.HeightF = 100;
            report.Bands.Add(infoHeaderBand);
            
            // الإدارة
            XRLabel departmentLabel = new XRLabel();
            string departmentName = "كل الإدارات";
            
            if (departmentId > 0 && leaveTable.Rows.Count > 0)
            {
                departmentName = leaveTable.Rows[0]["DepartmentName"].ToString();
            }
            
            departmentLabel.Text = "الإدارة: " + departmentName;
            departmentLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            departmentLabel.LocationF = new PointF(report.PageWidth - 280, 10);
            departmentLabel.SizeF = new SizeF(260, 20);
            departmentLabel.TextAlignment = TextAlignment.MiddleRight;
            infoHeaderBand.Controls.Add(departmentLabel);
            
            // نوع الإجازة
            XRLabel leaveTypeLabel = new XRLabel();
            string leaveTypeName = "كل أنواع الإجازات";
            
            if (leaveTypeId > 0 && leaveTable.Rows.Count > 0)
            {
                leaveTypeName = leaveTable.Rows[0]["LeaveTypeName"].ToString();
            }
            
            leaveTypeLabel.Text = "نوع الإجازة: " + leaveTypeName;
            leaveTypeLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            leaveTypeLabel.LocationF = new PointF(report.PageWidth - 280, 30);
            leaveTypeLabel.SizeF = new SizeF(260, 20);
            leaveTypeLabel.TextAlignment = TextAlignment.MiddleRight;
            infoHeaderBand.Controls.Add(leaveTypeLabel);
            
            // الفترة
            XRLabel periodLabel = new XRLabel();
            periodLabel.Text = $"الفترة: من {startDate.ToString("yyyy/MM/dd")} إلى {endDate.ToString("yyyy/MM/dd")}";
            periodLabel.Font = new Font("Arial", 10);
            periodLabel.LocationF = new PointF(report.PageWidth - 280, 50);
            periodLabel.SizeF = new SizeF(260, 20);
            periodLabel.TextAlignment = TextAlignment.MiddleRight;
            infoHeaderBand.Controls.Add(periodLabel);
            
            // عدد الموظفين
            int employeeCount = GetDistinctEmployeeCount(leaveTable);
            XRLabel employeeCountLabel = new XRLabel();
            employeeCountLabel.Text = $"عدد الموظفين: {employeeCount} موظف";
            employeeCountLabel.Font = new Font("Arial", 10);
            employeeCountLabel.LocationF = new PointF(10, 10);
            employeeCountLabel.SizeF = new SizeF(200, 20);
            employeeCountLabel.TextAlignment = TextAlignment.MiddleLeft;
            infoHeaderBand.Controls.Add(employeeCountLabel);
            
            // إجمالي الإجازات
            int leaveCount = leaveTable.Rows.Count;
            XRLabel leaveCountLabel = new XRLabel();
            leaveCountLabel.Text = $"عدد الإجازات: {leaveCount} إجازة";
            leaveCountLabel.Font = new Font("Arial", 10);
            leaveCountLabel.LocationF = new PointF(10, 30);
            leaveCountLabel.SizeF = new SizeF(200, 20);
            leaveCountLabel.TextAlignment = TextAlignment.MiddleLeft;
            infoHeaderBand.Controls.Add(leaveCountLabel);
            
            // إجمالي أيام الإجازات
            int totalDays = CalculateTotalLeaveDays(leaveTable);
            XRLabel totalDaysLabel = new XRLabel();
            totalDaysLabel.Text = $"إجمالي أيام الإجازات: {totalDays} يوم";
            totalDaysLabel.Font = new Font("Arial", 10);
            totalDaysLabel.LocationF = new PointF(10, 50);
            totalDaysLabel.SizeF = new SizeF(200, 20);
            totalDaysLabel.TextAlignment = TextAlignment.MiddleLeft;
            infoHeaderBand.Controls.Add(totalDaysLabel);
            
            // خط أفقي
            XRLine xrLine = new XRLine();
            xrLine.LocationF = new PointF(0, 80);
            xrLine.SizeF = new SizeF(report.PageWidth - 20, 2);
            xrLine.LineWidth = 1;
            infoHeaderBand.Controls.Add(xrLine);
        }
        
        /// <summary>
        /// الحصول على عدد الموظفين المتميزين
        /// </summary>
        private int GetDistinctEmployeeCount(DataTable leaveTable)
        {
            HashSet<int> distinctEmployees = new HashSet<int>();
            
            foreach (DataRow row in leaveTable.Rows)
            {
                int employeeId = Convert.ToInt32(row["EmployeeID"]);
                distinctEmployees.Add(employeeId);
            }
            
            return distinctEmployees.Count;
        }
        
        /// <summary>
        /// حساب إجمالي أيام الإجازات
        /// </summary>
        private int CalculateTotalLeaveDays(DataTable leaveTable)
        {
            int totalDays = 0;
            
            foreach (DataRow row in leaveTable.Rows)
            {
                int days = Convert.ToInt32(row["DurationDays"]);
                totalDays += days;
            }
            
            return totalDays;
        }
        
        /// <summary>
        /// إنشاء تقرير تفصيلي للإجازات
        /// </summary>
        private void CreateLeaveDetailsReport(XtraReport report, DataTable leaveTable)
        {
            // إنشاء عنوان القسم
            GroupHeaderBand tableTitleBand = new GroupHeaderBand();
            tableTitleBand.HeightF = 35;
            report.Bands.Add(tableTitleBand);
            
            // عنوان الجدول
            XRLabel tableTitle = new XRLabel();
            tableTitle.Text = "بيانات الإجازات التفصيلية";
            tableTitle.Font = new Font("Arial", 12, FontStyle.Bold);
            tableTitle.TextAlignment = TextAlignment.MiddleCenter;
            tableTitle.LocationF = new PointF(0, 5);
            tableTitle.SizeF = new SizeF(report.PageWidth - 20, 25);
            tableTitleBand.Controls.Add(tableTitle);
            
            // إنشاء قسم Detail للتقرير
            DetailBand detailBand = new DetailBand();
            report.Bands.Add(detailBand);
            
            // إنشاء جدول الإجازات
            XRTable leaveTable1 = CreateLeaveTable(leaveTable, report.PageWidth);
            detailBand.Controls.Add(leaveTable1);
        }
        
        /// <summary>
        /// إنشاء جدول الإجازات
        /// </summary>
        private XRTable CreateLeaveTable(DataTable dataTable, float pageWidth)
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
            AddTableCell(headerRow, "الموظف", 120);
            AddTableCell(headerRow, "الإدارة", 100);
            AddTableCell(headerRow, "نوع الإجازة", 100);
            AddTableCell(headerRow, "تاريخ البداية", 70);
            AddTableCell(headerRow, "تاريخ النهاية", 70);
            AddTableCell(headerRow, "المدة (أيام)", 60);
            AddTableCell(headerRow, "الحالة", 60);
            AddTableCell(headerRow, "تاريخ الموافقة", 70);
            AddTableCell(headerRow, "معتمد من", 100);
            
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
                AddTableCell(dataRow, row["EmployeeName"].ToString(), 120);
                AddTableCell(dataRow, row["DepartmentName"].ToString(), 100);
                AddTableCell(dataRow, row["LeaveTypeName"].ToString(), 100);
                
                DateTime startDate = Convert.ToDateTime(row["StartDate"]);
                AddTableCell(dataRow, startDate.ToString("yyyy/MM/dd"), 70);
                
                DateTime endDate = Convert.ToDateTime(row["EndDate"]);
                AddTableCell(dataRow, endDate.ToString("yyyy/MM/dd"), 70);
                
                int durationDays = Convert.ToInt32(row["DurationDays"]);
                AddTableCell(dataRow, durationDays.ToString(), 60);
                
                string status = row["Status"].ToString();
                Color statusColor = Color.Black;
                
                if (status == "Approved")
                {
                    status = "معتمدة";
                    statusColor = Color.Green;
                }
                else if (status == "Pending")
                {
                    status = "قيد الانتظار";
                    statusColor = Color.Orange;
                }
                else if (status == "Rejected")
                {
                    status = "مرفوضة";
                    statusColor = Color.Red;
                }
                else if (status == "Cancelled")
                {
                    status = "ملغاة";
                    statusColor = Color.Gray;
                }
                
                AddTableCell(dataRow, status, 60, TextAlignment.MiddleCenter, statusColor);
                
                string approvedDate = row["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(row["ApprovedDate"]).ToString("yyyy/MM/dd") : "-";
                AddTableCell(dataRow, approvedDate, 70);
                
                string approvedBy = row["ApprovedByName"] != DBNull.Value ? row["ApprovedByName"].ToString() : "-";
                AddTableCell(dataRow, approvedBy, 100);
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
        /// إضافة ملخص الإجازات حسب الإدارة
        /// </summary>
        private void AddDepartmentSummary(XtraReport report, DataTable departmentSummaryTable)
        {
            // التحقق من وجود بيانات
            if (departmentSummaryTable == null || departmentSummaryTable.Rows.Count == 0)
                return;
                
            // إنشاء عنوان القسم
            GroupHeaderBand summaryHeaderBand = new GroupHeaderBand();
            summaryHeaderBand.HeightF = 35;
            report.Bands.Add(summaryHeaderBand);
            
            // عنوان الملخص
            XRLabel summaryTitle = new XRLabel();
            summaryTitle.Text = "ملخص الإجازات حسب الإدارات";
            summaryTitle.Font = new Font("Arial", 12, FontStyle.Bold);
            summaryTitle.TextAlignment = TextAlignment.MiddleCenter;
            summaryTitle.LocationF = new PointF(0, 5);
            summaryTitle.SizeF = new SizeF(report.PageWidth - 20, 25);
            summaryHeaderBand.Controls.Add(summaryTitle);
            
            // إنشاء قسم آخر للتقرير
            GroupHeaderBand summaryBand = new GroupHeaderBand();
            summaryBand.HeightF = departmentSummaryTable.Rows.Count * 30 + 30;
            report.Bands.Add(summaryBand);
            
            // إنشاء جدول الملخص
            XRTable summaryTable = new XRTable();
            summaryTable.LocationF = new PointF(10, 0);
            summaryTable.SizeF = new SizeF(report.PageWidth - 30, departmentSummaryTable.Rows.Count * 30 + 30);
            summaryTable.Borders = BorderSide.All;
            summaryTable.BorderWidth = 1;
            summaryBand.Controls.Add(summaryTable);
            
            // إنشاء صف العناوين
            XRTableRow headerRow = new XRTableRow();
            headerRow.HeightF = 30;
            headerRow.BackColor = Color.LightBlue;
            headerRow.Font = new Font("Arial", 9, FontStyle.Bold);
            summaryTable.Rows.Add(headerRow);
            
            // إضافة خلايا العناوين
            AddTableCell(headerRow, "الإدارة", 150);
            AddTableCell(headerRow, "عدد الموظفين", 90);
            AddTableCell(headerRow, "عدد الإجازات", 90);
            AddTableCell(headerRow, "إجمالي الأيام", 90);
            AddTableCell(headerRow, "متوسط مدة الإجازة", 120);
            
            // إضافة صفوف البيانات
            for (int i = 0; i < departmentSummaryTable.Rows.Count; i++)
            {
                DataRow row = departmentSummaryTable.Rows[i];
                XRTableRow dataRow = new XRTableRow();
                dataRow.HeightF = 30;
                dataRow.BackColor = i % 2 == 0 ? Color.White : Color.WhiteSmoke;
                summaryTable.Rows.Add(dataRow);
                
                // إضافة خلايا البيانات
                AddTableCell(dataRow, row["DepartmentName"].ToString(), 150);
                AddTableCell(dataRow, row["EmployeeCount"].ToString(), 90);
                AddTableCell(dataRow, row["LeaveCount"].ToString(), 90);
                AddTableCell(dataRow, row["TotalDays"].ToString(), 90);
                
                decimal avgDuration = Convert.ToDecimal(row["AvgDuration"]);
                AddTableCell(dataRow, avgDuration.ToString("N2") + " يوم", 120);
            }
        }
        
        /// <summary>
        /// إضافة ملخص الإجازات حسب النوع
        /// </summary>
        private void AddLeaveTypeSummary(XtraReport report, DataTable leaveTypeSummaryTable)
        {
            // التحقق من وجود بيانات
            if (leaveTypeSummaryTable == null || leaveTypeSummaryTable.Rows.Count == 0)
                return;
                
            // إنشاء عنوان القسم
            GroupHeaderBand summaryHeaderBand = new GroupHeaderBand();
            summaryHeaderBand.HeightF = 35;
            report.Bands.Add(summaryHeaderBand);
            
            // عنوان الملخص
            XRLabel summaryTitle = new XRLabel();
            summaryTitle.Text = "ملخص الإجازات حسب النوع";
            summaryTitle.Font = new Font("Arial", 12, FontStyle.Bold);
            summaryTitle.TextAlignment = TextAlignment.MiddleCenter;
            summaryTitle.LocationF = new PointF(0, 5);
            summaryTitle.SizeF = new SizeF(report.PageWidth - 20, 25);
            summaryHeaderBand.Controls.Add(summaryTitle);
            
            // إنشاء قسم آخر للتقرير
            GroupHeaderBand summaryBand = new GroupHeaderBand();
            summaryBand.HeightF = leaveTypeSummaryTable.Rows.Count * 30 + 30;
            report.Bands.Add(summaryBand);
            
            // إنشاء جدول الملخص
            XRTable summaryTable = new XRTable();
            summaryTable.LocationF = new PointF(10, 0);
            summaryTable.SizeF = new SizeF(report.PageWidth - 30, leaveTypeSummaryTable.Rows.Count * 30 + 30);
            summaryTable.Borders = BorderSide.All;
            summaryTable.BorderWidth = 1;
            summaryBand.Controls.Add(summaryTable);
            
            // إنشاء صف العناوين
            XRTableRow headerRow = new XRTableRow();
            headerRow.HeightF = 30;
            headerRow.BackColor = Color.LightBlue;
            headerRow.Font = new Font("Arial", 9, FontStyle.Bold);
            summaryTable.Rows.Add(headerRow);
            
            // إضافة خلايا العناوين
            AddTableCell(headerRow, "نوع الإجازة", 150);
            AddTableCell(headerRow, "عدد الإجازات", 90);
            AddTableCell(headerRow, "إجمالي الأيام", 90);
            AddTableCell(headerRow, "متوسط المدة", 90);
            AddTableCell(headerRow, "عدد الموظفين", 90);
            AddTableCell(headerRow, "النسبة", 90);
            
            // حساب إجمالي الإجازات
            int totalLeaves = 0;
            foreach (DataRow row in leaveTypeSummaryTable.Rows)
            {
                totalLeaves += Convert.ToInt32(row["LeaveCount"]);
            }
            
            // إضافة صفوف البيانات
            for (int i = 0; i < leaveTypeSummaryTable.Rows.Count; i++)
            {
                DataRow row = leaveTypeSummaryTable.Rows[i];
                XRTableRow dataRow = new XRTableRow();
                dataRow.HeightF = 30;
                dataRow.BackColor = i % 2 == 0 ? Color.White : Color.WhiteSmoke;
                summaryTable.Rows.Add(dataRow);
                
                // إضافة خلايا البيانات
                AddTableCell(dataRow, row["LeaveTypeName"].ToString(), 150);
                AddTableCell(dataRow, row["LeaveCount"].ToString(), 90);
                AddTableCell(dataRow, row["TotalDays"].ToString(), 90);
                
                decimal avgDuration = Convert.ToDecimal(row["AvgDuration"]);
                AddTableCell(dataRow, avgDuration.ToString("N2") + " يوم", 90);
                
                AddTableCell(dataRow, row["EmployeeCount"].ToString(), 90);
                
                int leaveCount = Convert.ToInt32(row["LeaveCount"]);
                double percentage = totalLeaves > 0 ? (double)leaveCount / totalLeaves * 100 : 0;
                AddTableCell(dataRow, percentage.ToString("N2") + "%", 90);
            }
        }
        
        /// <summary>
        /// إضافة رسم بياني للإجازات
        /// </summary>
        private void AddLeaveChart(XtraReport report, DataTable leaveTypeSummaryTable)
        {
            // التحقق من وجود بيانات
            if (leaveTypeSummaryTable == null || leaveTypeSummaryTable.Rows.Count == 0)
                return;
                
            // إنشاء عنوان القسم
            GroupHeaderBand chartGroupBand = new GroupHeaderBand();
            chartGroupBand.HeightF = 350;
            report.Bands.Add(chartGroupBand);
            
            // عنوان الرسم البياني
            XRLabel chartTitle = new XRLabel();
            chartTitle.Text = "توزيع الإجازات حسب النوع";
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
            chart.Legend.Font = new Font("Arial", 10);
            
            // إنشاء سلسلة بيانات الإجازات (رسم دائري)
            Series leaveTypeSeries = new Series("أنواع الإجازات", ViewType.Pie);
            chart.Series.Add(leaveTypeSeries);
            
            // تعيين نمط العرض للرسم الدائري
            ((PieSeriesView)leaveTypeSeries.View).ExplodeMode = PieExplodeMode.All;
            ((PieSeriesView)leaveTypeSeries.View).ExplodedDistancePercentage = 5;
            ((PieSeriesView)leaveTypeSeries.View).SweepDirection = PieSweepDirection.Clockwise;
            
            // إضافة نقاط البيانات
            foreach (DataRow row in leaveTypeSummaryTable.Rows)
            {
                string leaveTypeName = row["LeaveTypeName"].ToString();
                int leaveCount = Convert.ToInt32(row["LeaveCount"]);
                
                SeriesPoint point = new SeriesPoint(leaveTypeName, leaveCount);
                leaveTypeSeries.Points.Add(point);
            }
            
            // تفعيل عرض بيانات القيم
            leaveTypeSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            leaveTypeSeries.Label.TextPattern = "{A}: {VP:P1}";
            leaveTypeSeries.Label.Font = new Font("Arial", 9);
            
            // إضافة عنوان فرعي للرسم البياني
            XRLabel chartSubtitle = new XRLabel();
            chartSubtitle.Text = $"إجمالي عدد الإجازات: {leaveTypeSummaryTable.Compute("SUM(LeaveCount)", "")}";
            chartSubtitle.Font = new Font("Arial", 10, FontStyle.Italic);
            chartSubtitle.TextAlignment = TextAlignment.MiddleCenter;
            chartSubtitle.LocationF = new PointF(0, 310);
            chartSubtitle.SizeF = new SizeF(report.PageWidth - 20, 25);
            chartGroupBand.Controls.Add(chartSubtitle);
            
            // إضافة رسم بياني شريطي للمقارنة بين عدد الإجازات وإجمالي الأيام
            XRChart barChart = new XRChart();
            barChart.LocationF = new PointF(0, 340);
            barChart.SizeF = new SizeF(report.PageWidth - 20, 250);
            
            // إنشاء قسم جديد للرسم الشريطي
            GroupHeaderBand barChartBand = new GroupHeaderBand();
            barChartBand.HeightF = 290;
            report.Bands.Add(barChartBand);
            
            // عنوان الرسم البياني الشريطي
            XRLabel barChartTitle = new XRLabel();
            barChartTitle.Text = "مقارنة بين عدد الإجازات وإجمالي الأيام لكل نوع";
            barChartTitle.Font = new Font("Arial", 12, FontStyle.Bold);
            barChartTitle.TextAlignment = TextAlignment.MiddleCenter;
            barChartTitle.LocationF = new PointF(0, 10);
            barChartTitle.SizeF = new SizeF(report.PageWidth - 20, 25);
            barChartBand.Controls.Add(barChartTitle);
            
            barChartBand.Controls.Add(barChart);
            
            // تهيئة الرسم البياني الشريطي
            XYDiagram diagram = new XYDiagram();
            barChart.Diagram = diagram;
            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisXZooming = true;
            
            // إنشاء سلسلة بيانات لعدد الإجازات
            Series countSeries = new Series("عدد الإجازات", ViewType.Bar);
            barChart.Series.Add(countSeries);
            
            // إنشاء سلسلة بيانات لإجمالي الأيام
            Series daysSeries = new Series("إجمالي الأيام", ViewType.Bar);
            barChart.Series.Add(daysSeries);
            
            // تعيين ألوان السلاسل
            ((BarSeriesView)countSeries.View).Color = Color.SteelBlue;
            ((BarSeriesView)daysSeries.View).Color = Color.MediumSeaGreen;
            
            // إضافة نقاط البيانات
            foreach (DataRow row in leaveTypeSummaryTable.Rows)
            {
                string leaveTypeName = row["LeaveTypeName"].ToString();
                int leaveCount = Convert.ToInt32(row["LeaveCount"]);
                int totalDays = Convert.ToInt32(row["TotalDays"]);
                
                countSeries.Points.Add(new SeriesPoint(leaveTypeName, leaveCount));
                daysSeries.Points.Add(new SeriesPoint(leaveTypeName, totalDays));
            }
            
            // تفعيل عرض القيم
            countSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            daysSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            
            // تعيين إعدادات الخطوط
            barChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            barChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            barChart.Legend.AlignmentVertical = LegendAlignmentVertical.Bottom;
            barChart.Legend.Direction = LegendDirection.LeftToRight;
            barChart.Legend.Font = new Font("Arial", 10);
        }
        
        /// <summary>
        /// عرض تقرير الإجازات
        /// </summary>
        /// <param name="departmentId">معرف الإدارة</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <param name="leaveTypeId">معرف نوع الإجازة</param>
        public void ShowLeaveReport(int departmentId, DateTime startDate, DateTime endDate, int leaveTypeId = 0)
        {
            XtraReport report = GenerateLeaveReport(departmentId, startDate, endDate, leaveTypeId);
            _reportManager.ShowPreview(report);
        }
        
        /// <summary>
        /// طباعة تقرير الإجازات
        /// </summary>
        /// <param name="departmentId">معرف الإدارة</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <param name="leaveTypeId">معرف نوع الإجازة</param>
        public void PrintLeaveReport(int departmentId, DateTime startDate, DateTime endDate, int leaveTypeId = 0)
        {
            XtraReport report = GenerateLeaveReport(departmentId, startDate, endDate, leaveTypeId);
            _reportManager.Print(report);
        }
        
        /// <summary>
        /// تصدير تقرير الإجازات إلى PDF
        /// </summary>
        /// <param name="departmentId">معرف الإدارة</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <param name="leaveTypeId">معرف نوع الإجازة</param>
        /// <param name="filePath">مسار الملف</param>
        public void ExportLeaveReportToPdf(int departmentId, DateTime startDate, DateTime endDate, int leaveTypeId, string filePath)
        {
            XtraReport report = GenerateLeaveReport(departmentId, startDate, endDate, leaveTypeId);
            _reportManager.ExportToPdf(report, filePath);
        }
    }
}