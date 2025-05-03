using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraCharts;
using HR.Core;

namespace HR.Core.Reports
{
    /// <summary>
    /// تقرير كشف الرواتب
    /// </summary>
    public class PayrollReport
    {
        private readonly ReportManager _reportManager;
        private readonly ConnectionManager _connectionManager;

        public PayrollReport()
        {
            _reportManager = new ReportManager();
            _connectionManager = new ConnectionManager();
        }

        /// <summary>
        /// إنشاء تقرير كشف الرواتب
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        /// <returns>تقرير كشف الرواتب</returns>
        public XtraReport GeneratePayrollReport(int payrollId)
        {
            try
            {
                // إنشاء تقرير جديد باستخدام القالب الموحد
                XtraReport report = _reportManager.CreateBaseReport("كشف الرواتب");

                // جلب البيانات
                DataSet dataSet = GetPayrollData(payrollId);

                if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
                {
                    XRLabel noDataLabel = new XRLabel();
                    noDataLabel.Text = "لا توجد بيانات متاحة لهذا الكشف";
                    noDataLabel.Font = new Font("Arial", 12, FontStyle.Bold);
                    noDataLabel.TextAlignment = TextAlignment.MiddleCenter;
                    noDataLabel.SizeF = new SizeF(report.PageWidth - 40, 40);
                    noDataLabel.LocationF = new PointF(20, 10);

                    DetailBand detailBand = new DetailBand();
                    detailBand.Controls.Add(noDataLabel);
                    report.Bands.Add(detailBand);

                    return report;
                }

                // الحصول على بيانات الكشف الرئيسية
                DataTable payrollTable = dataSet.Tables[0];
                DataRow payrollRow = payrollTable.Rows[0];

                // إضافة معلومات الكشف إلى ترويسة التقرير
                AddPayrollInfoToReport(report, payrollRow);

                // إنشاء قسم Detail للتقرير
                DetailBand detailBand = new DetailBand();
                report.Bands.Add(detailBand);

                // إنشاء جدول الرواتب (XRTable)
                XRTable salaryTable = CreateSalaryTable(dataSet.Tables[1], report.PageWidth);
                detailBand.Controls.Add(salaryTable);

                // إضافة رسم بياني للرواتب
                AddPayrollChart(report, dataSet.Tables[1]);

                // إضافة ملخص الكشف
                AddPayrollSummary(report, payrollRow);

                return report;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في إنشاء تقرير كشف الرواتب");
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
        /// جلب بيانات كشف الرواتب
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        /// <returns>مجموعة بيانات تحتوي على كشف الرواتب وتفاصيله</returns>
        private DataSet GetPayrollData(int payrollId)
        {
            DataSet dataSet = new DataSet();

            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    connection.Open();

                    // استعلام جلب معلومات الكشف الرئيسية
                    string payrollQuery = @"
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
                            P.Notes,
                            P.Status,
                            P.GeneratedDate,
                            P.GeneratedBy,
                            P.ApprovedDate,
                            P.ApprovedBy,
                            U1.FullName AS GeneratedByName,
                            U2.FullName AS ApprovedByName
                        FROM Payrolls P
                        LEFT JOIN Users U1 ON P.GeneratedBy = U1.ID
                        LEFT JOIN Users U2 ON P.ApprovedBy = U2.ID
                        WHERE P.ID = @PayrollId";

                    using (SqlCommand command = new SqlCommand(payrollQuery, connection))
                    {
                        command.Parameters.AddWithValue("@PayrollId", payrollId);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "PayrollInfo");
                    }

                    // استعلام جلب تفاصيل رواتب الموظفين
                    string detailsQuery = @"
                        SELECT 
                            PD.ID,
                            PD.EmployeeID,
                            E.EmployeeNumber,
                            E.FullName AS EmployeeName,
                            D.Name AS DepartmentName,
                            P.Title AS PositionTitle,
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
                        INNER JOIN Employees E ON PD.EmployeeID = E.ID
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        LEFT JOIN Positions P ON E.PositionID = P.ID
                        WHERE PD.PayrollID = @PayrollId
                        ORDER BY E.FullName";

                    using (SqlCommand command = new SqlCommand(detailsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@PayrollId", payrollId);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "PayrollDetails");
                    }

                    // استعلام جلب ملخص حسب الإدارات
                    string departmentSummaryQuery = @"
                        SELECT 
                            D.Name AS DepartmentName,
                            COUNT(DISTINCT PD.EmployeeID) AS EmployeeCount,
                            SUM(PD.BaseSalary) AS TotalBaseSalary,
                            SUM(PD.Allowances) AS TotalAllowances,
                            SUM(PD.Overtime) AS TotalOvertime,
                            SUM(PD.Bonus) AS TotalBonus,
                            SUM(PD.Deductions) AS TotalDeductions,
                            SUM(PD.NetSalary) AS TotalNetSalary
                        FROM PayrollDetails PD
                        INNER JOIN Employees E ON PD.EmployeeID = E.ID
                        LEFT JOIN Departments D ON E.DepartmentID = D.ID
                        WHERE PD.PayrollID = @PayrollId
                        GROUP BY D.Name
                        ORDER BY D.Name";

                    using (SqlCommand command = new SqlCommand(departmentSummaryQuery, connection))
                    {
                        command.Parameters.AddWithValue("@PayrollId", payrollId);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet, "DepartmentSummary");
                    }
                }

                return dataSet;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في جلب بيانات كشف الرواتب");
                return null;
            }
        }

        /// <summary>
        /// إضافة معلومات الكشف إلى ترويسة التقرير
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="payrollRow">بيانات الكشف الرئيسية</param>
        private void AddPayrollInfoToReport(XtraReport report, DataRow payrollRow)
        {
            GroupHeaderBand infoHeaderBand = new GroupHeaderBand();
            infoHeaderBand.HeightF = 120;
            report.Bands.Add(infoHeaderBand);

            // إنشاء جدول للمعلومات
            XRTable infoTable = new XRTable();
            infoTable.LocationF = new PointF(10, 10);
            infoTable.SizeF = new SizeF(report.PageWidth - 20, 100);
            infoTable.Borders = BorderSide.All;
            infoTable.BorderWidth = 1;
            infoTable.BackColor = Color.LightGray;
            infoHeaderBand.Controls.Add(infoTable);

            XRTableRow row1 = new XRTableRow();
            row1.HeightF = 25;
            infoTable.Rows.Add(row1);

            // اسم الكشف
            XRTableCell cell1_1 = new XRTableCell();
            cell1_1.Text = "اسم الكشف: " + payrollRow["PayrollName"].ToString();
            cell1_1.TextAlignment = TextAlignment.MiddleRight;
            cell1_1.Font = new Font("Arial", 10, FontStyle.Bold);
            cell1_1.BackColor = Color.LightBlue;
            cell1_1.Borders = BorderSide.All;
            cell1_1.BorderWidth = 1;
            cell1_1.WidthF = 250;
            row1.Cells.Add(cell1_1);

            // الشهر والسنة
            XRTableCell cell1_2 = new XRTableCell();
            cell1_2.Text = $"الفترة: {payrollRow["PayrollMonth"]} / {payrollRow["PayrollYear"]}";
            cell1_2.TextAlignment = TextAlignment.MiddleRight;
            cell1_2.Font = new Font("Arial", 10, FontStyle.Bold);
            cell1_2.BackColor = Color.LightBlue;
            cell1_2.Borders = BorderSide.All;
            cell1_2.BorderWidth = 1;
            row1.Cells.Add(cell1_2);

            XRTableRow row2 = new XRTableRow();
            row2.HeightF = 25;
            infoTable.Rows.Add(row2);

            // تاريخ الإنشاء
            XRTableCell cell2_1 = new XRTableCell();
            cell2_1.Text = $"تاريخ الإنشاء: {Convert.ToDateTime(payrollRow["GeneratedDate"]).ToString("yyyy/MM/dd")}";
            cell2_1.TextAlignment = TextAlignment.MiddleRight;
            cell2_1.Font = new Font("Arial", 10);
            cell2_1.Borders = BorderSide.All;
            cell2_1.BorderWidth = 1;
            row2.Cells.Add(cell2_1);

            // بواسطة
            XRTableCell cell2_2 = new XRTableCell();
            cell2_2.Text = $"بواسطة: {payrollRow["GeneratedByName"]}";
            cell2_2.TextAlignment = TextAlignment.MiddleRight;
            cell2_2.Font = new Font("Arial", 10);
            cell2_2.Borders = BorderSide.All;
            cell2_2.BorderWidth = 1;
            row2.Cells.Add(cell2_2);

            XRTableRow row3 = new XRTableRow();
            row3.HeightF = 25;
            infoTable.Rows.Add(row3);

            // الحالة
            XRTableCell cell3_1 = new XRTableCell();
            cell3_1.Text = $"الحالة: {payrollRow["Status"]}";
            cell3_1.TextAlignment = TextAlignment.MiddleRight;
            cell3_1.Font = new Font("Arial", 10);
            cell3_1.Borders = BorderSide.All;
            cell3_1.BorderWidth = 1;
            row3.Cells.Add(cell3_1);

            // تاريخ الدفع
            XRTableCell cell3_2 = new XRTableCell();
            string paymentDate = payrollRow["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(payrollRow["PaymentDate"]).ToString("yyyy/MM/dd") : "غير محدد";
            cell3_2.Text = $"تاريخ الدفع: {paymentDate}";
            cell3_2.TextAlignment = TextAlignment.MiddleRight;
            cell3_2.Font = new Font("Arial", 10);
            cell3_2.Borders = BorderSide.All;
            cell3_2.BorderWidth = 1;
            row3.Cells.Add(cell3_2);

            XRTableRow row4 = new XRTableRow();
            row4.HeightF = 25;
            infoTable.Rows.Add(row4);

            // إجمالي الرواتب
            XRTableCell cell4_1 = new XRTableCell();
            cell4_1.Text = $"إجمالي الرواتب: {Convert.ToDecimal(payrollRow["TotalBaseSalary"]):N2}";
            cell4_1.TextAlignment = TextAlignment.MiddleRight;
            cell4_1.Font = new Font("Arial", 10, FontStyle.Bold);
            cell4_1.BackColor = Color.LightGreen;
            cell4_1.Borders = BorderSide.All;
            cell4_1.BorderWidth = 1;
            row4.Cells.Add(cell4_1);

            // إجمالي المستحق
            XRTableCell cell4_2 = new XRTableCell();
            cell4_2.Text = $"إجمالي المستحق: {Convert.ToDecimal(payrollRow["TotalNetSalary"]):N2}";
            cell4_2.TextAlignment = TextAlignment.MiddleRight;
            cell4_2.Font = new Font("Arial", 10, FontStyle.Bold);
            cell4_2.BackColor = Color.LightGreen;
            cell4_2.Borders = BorderSide.All;
            cell4_2.BorderWidth = 1;
            row4.Cells.Add(cell4_2);
        }

        /// <summary>
        /// إنشاء جدول الرواتب
        /// </summary>
        /// <param name="detailsTable">جدول بيانات تفاصيل الرواتب</param>
        /// <param name="pageWidth">عرض الصفحة</param>
        /// <returns>جدول XRTable</returns>
        private XRTable CreateSalaryTable(DataTable detailsTable, float pageWidth)
        {
            XRTable table = new XRTable();
            table.LocationF = new PointF(0, 10);
            table.SizeF = new SizeF(pageWidth - 20, detailsTable.Rows.Count * 25 + 30);
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
            AddTableCell(headerRow, "رقم الموظف", 80);
            AddTableCell(headerRow, "اسم الموظف", 150);
            AddTableCell(headerRow, "الإدارة", 100);
            AddTableCell(headerRow, "الوظيفة", 100);
            AddTableCell(headerRow, "الراتب الأساسي", 80);
            AddTableCell(headerRow, "البدلات", 70);
            AddTableCell(headerRow, "العمل الإضافي", 70);
            AddTableCell(headerRow, "المكافآت", 70);
            AddTableCell(headerRow, "الخصومات", 70);
            AddTableCell(headerRow, "الصافي", 80);
            
            // إضافة صفوف البيانات
            for (int i = 0; i < detailsTable.Rows.Count; i++)
            {
                DataRow row = detailsTable.Rows[i];
                XRTableRow dataRow = new XRTableRow();
                dataRow.HeightF = 25;
                dataRow.BackColor = i % 2 == 0 ? Color.White : Color.WhiteSmoke;
                table.Rows.Add(dataRow);
                
                // إضافة خلايا البيانات
                AddTableCell(dataRow, (i + 1).ToString(), 40);
                AddTableCell(dataRow, row["EmployeeNumber"].ToString(), 80);
                AddTableCell(dataRow, row["EmployeeName"].ToString(), 150);
                AddTableCell(dataRow, row["DepartmentName"].ToString(), 100);
                AddTableCell(dataRow, row["PositionTitle"].ToString(), 100);
                AddTableCell(dataRow, Convert.ToDecimal(row["BaseSalary"]).ToString("N2"), 80, TextAlignment.MiddleLeft);
                AddTableCell(dataRow, Convert.ToDecimal(row["Allowances"]).ToString("N2"), 70, TextAlignment.MiddleLeft);
                AddTableCell(dataRow, Convert.ToDecimal(row["Overtime"]).ToString("N2"), 70, TextAlignment.MiddleLeft);
                AddTableCell(dataRow, Convert.ToDecimal(row["Bonus"]).ToString("N2"), 70, TextAlignment.MiddleLeft);
                AddTableCell(dataRow, Convert.ToDecimal(row["Deductions"]).ToString("N2"), 70, TextAlignment.MiddleLeft);
                
                decimal netSalary = Convert.ToDecimal(row["NetSalary"]);
                AddTableCell(dataRow, netSalary.ToString("N2"), 80, TextAlignment.MiddleLeft, netSalary < 0 ? Color.Red : Color.Black);
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
        /// إضافة رسم بياني للرواتب
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="detailsTable">جدول بيانات تفاصيل الرواتب</param>
        private void AddPayrollChart(XtraReport report, DataTable detailsTable)
        {
            // إنشاء عنوان القسم
            GroupHeaderBand chartGroupBand = new GroupHeaderBand();
            chartGroupBand.HeightF = 300;
            report.Bands.Add(chartGroupBand);
            
            // عنوان الرسم البياني
            XRLabel chartTitle = new XRLabel();
            chartTitle.Text = "توزيع الرواتب حسب الإدارات";
            chartTitle.Font = new Font("Arial", 12, FontStyle.Bold);
            chartTitle.TextAlignment = TextAlignment.MiddleCenter;
            chartTitle.LocationF = new PointF(0, 0);
            chartTitle.SizeF = new SizeF(report.PageWidth - 20, 25);
            chartGroupBand.Controls.Add(chartTitle);
            
            // إنشاء الرسم البياني
            XRChart chart = new XRChart();
            chart.LocationF = new PointF(0, 30);
            chart.SizeF = new SizeF(report.PageWidth - 20, 250);
            chartGroupBand.Controls.Add(chart);
            
            // تهيئة الرسم البياني
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.Bottom;
            chart.Legend.Direction = LegendDirection.LeftToRight;
            
            // تجميع البيانات حسب الإدارات
            var departmentData = detailsTable.AsEnumerable()
                .GroupBy(row => row.Field<string>("DepartmentName"))
                .Select(g => new
                {
                    Department = g.Key,
                    TotalSalary = g.Sum(row => Convert.ToDecimal(row["NetSalary"]))
                })
                .OrderByDescending(x => x.TotalSalary)
                .ToList();
            
            // إنشاء سلسلة بيانات الرسم البياني
            Series series = new Series("الرواتب حسب الإدارات", ViewType.Pie);
            chart.Series.Add(series);
            
            // إضافة نقاط البيانات
            foreach (var item in departmentData)
            {
                series.Points.Add(new SeriesPoint(item.Department, item.TotalSalary));
            }
            
            // تنسيق سلسلة البيانات
            ((PieSeriesView)series.View).ExplodeMode = PieExplodeMode.All;
            ((PieSeriesView)series.View).ExplodedDistancePercentage = 10;
            ((PieSeriesView)series.View).RuntimeExploding = true;
            
            // إضافة قيم البيانات
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series.Label.TextPattern = "{A}: {VP:P2}";
        }

        /// <summary>
        /// إضافة ملخص الكشف
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="payrollRow">بيانات الكشف الرئيسية</param>
        private void AddPayrollSummary(XtraReport report, DataRow payrollRow)
        {
            // إنشاء تذييل التقرير
            ReportFooterBand reportFooter = new ReportFooterBand();
            reportFooter.HeightF = 150;
            report.Bands.Add(reportFooter);
            
            // إنشاء جدول للملخص
            XRTable table = new XRTable();
            table.LocationF = new PointF(report.PageWidth / 2 - 200, 20);
            table.SizeF = new SizeF(400, 100);
            table.Borders = BorderSide.All;
            table.BorderWidth = 1;
            reportFooter.Controls.Add(table);
            
            // إنشاء صفوف الملخص
            AddSummaryRow(table, "إجمالي الرواتب الأساسية", Convert.ToDecimal(payrollRow["TotalBaseSalary"]).ToString("N2"));
            AddSummaryRow(table, "إجمالي البدلات والإضافات", Convert.ToDecimal(payrollRow["TotalAdditions"]).ToString("N2"));
            AddSummaryRow(table, "إجمالي الخصومات", Convert.ToDecimal(payrollRow["TotalDeductions"]).ToString("N2"));
            AddSummaryRow(table, "إجمالي المستحق", Convert.ToDecimal(payrollRow["TotalNetSalary"]).ToString("N2"), true);
            
            // إضافة ملاحظات
            string notes = payrollRow["Notes"] != DBNull.Value ? payrollRow["Notes"].ToString() : string.Empty;
            if (!string.IsNullOrEmpty(notes))
            {
                XRLabel notesLabel = new XRLabel();
                notesLabel.Text = "ملاحظات: " + notes;
                notesLabel.Font = new Font("Arial", 10);
                notesLabel.LocationF = new PointF(10, 130);
                notesLabel.SizeF = new SizeF(report.PageWidth - 20, 20);
                notesLabel.TextAlignment = TextAlignment.MiddleRight;
                reportFooter.Controls.Add(notesLabel);
            }
        }

        /// <summary>
        /// إضافة صف ملخص
        /// </summary>
        private void AddSummaryRow(XRTable table, string title, string value, bool isTotal = false)
        {
            XRTableRow row = new XRTableRow();
            row.HeightF = 25;
            table.Rows.Add(row);
            
            // خلية العنوان
            XRTableCell titleCell = new XRTableCell();
            titleCell.Text = title;
            titleCell.TextAlignment = TextAlignment.MiddleRight;
            titleCell.BackColor = isTotal ? Color.LightGreen : Color.LightGray;
            titleCell.Font = new Font("Arial", isTotal ? 10 : 9, isTotal ? FontStyle.Bold : FontStyle.Regular);
            titleCell.Borders = BorderSide.All;
            titleCell.BorderWidth = 1;
            titleCell.WidthF = 200;
            row.Cells.Add(titleCell);
            
            // خلية القيمة
            XRTableCell valueCell = new XRTableCell();
            valueCell.Text = value;
            valueCell.TextAlignment = TextAlignment.MiddleLeft;
            valueCell.BackColor = isTotal ? Color.LightGreen : Color.White;
            valueCell.Font = new Font("Arial", isTotal ? 10 : 9, isTotal ? FontStyle.Bold : FontStyle.Regular);
            valueCell.Borders = BorderSide.All;
            valueCell.BorderWidth = 1;
            valueCell.WidthF = 200;
            row.Cells.Add(valueCell);
        }

        /// <summary>
        /// عرض تقرير كشف الرواتب
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        public void ShowPayrollReport(int payrollId)
        {
            XtraReport report = GeneratePayrollReport(payrollId);
            _reportManager.ShowPreview(report);
        }

        /// <summary>
        /// طباعة تقرير كشف الرواتب
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        public void PrintPayrollReport(int payrollId)
        {
            XtraReport report = GeneratePayrollReport(payrollId);
            _reportManager.Print(report);
        }

        /// <summary>
        /// تصدير تقرير كشف الرواتب إلى PDF
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        /// <param name="filePath">مسار الملف</param>
        public void ExportPayrollReportToPdf(int payrollId, string filePath)
        {
            XtraReport report = GeneratePayrollReport(payrollId);
            _reportManager.ExportToPdf(report, filePath);
        }
    }
}