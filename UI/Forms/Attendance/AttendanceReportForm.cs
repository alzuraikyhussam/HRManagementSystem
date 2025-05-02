using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using HR.Core;
using HR.DataAccess;

namespace HR.UI.Forms.Attendance
{
    /// <summary>
    /// نموذج تقرير الحضور والانصراف
    /// </summary>
    public partial class AttendanceReportForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _departmentId;

        /// <summary>
        /// إنشاء نموذج جديد لتقرير الحضور
        /// </summary>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <param name="departmentId">معرف القسم (0 لكل الأقسام)</param>
        public AttendanceReportForm(DateTime startDate, DateTime endDate, int departmentId)
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
            _startDate = startDate;
            _endDate = endDate;
            _departmentId = departmentId;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void AttendanceReportForm_Load(object sender, EventArgs e)
        {
            try
            {
                // تحميل بيانات التقرير
                LoadReportData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary>
        /// تحميل بيانات التقرير
        /// </summary>
        private void LoadReportData()
        {
            // استعلام لاستخراج بيانات الحضور والانصراف
            string query = @"
            SELECT
                e.EmployeeNumber,
                e.FullName AS EmployeeName,
                d.Name AS DepartmentName,
                p.Title AS PositionTitle,
                ar.AttendanceDate,
                ar.TimeIn,
                ar.TimeOut,
                ar.LateMinutes,
                ar.EarlyDepartureMinutes,
                ar.OvertimeMinutes,
                ar.WorkedMinutes,
                ar.Status,
                CASE WHEN ar.IsManualEntry = 1 THEN 'نعم' ELSE 'لا' END AS IsManualEntry,
                ar.Notes
            FROM
                AttendanceRecords ar
            INNER JOIN
                Employees e ON ar.EmployeeID = e.ID
            LEFT JOIN
                Departments d ON e.DepartmentID = d.ID
            LEFT JOIN
                Positions p ON e.PositionID = p.ID
            WHERE
                ar.AttendanceDate BETWEEN @StartDate AND @EndDate
                AND (@DepartmentID = 0 OR e.DepartmentID = @DepartmentID)
            ORDER BY
                e.FullName, ar.AttendanceDate";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@StartDate", _startDate),
                new SqlParameter("@EndDate", _endDate),
                new SqlParameter("@DepartmentID", _departmentId)
            };

            var dataTable = _dbContext.ExecuteReader(query, parameters);

            // إنشاء التقرير وعرضه
            if (dataTable.Rows.Count > 0)
            {
                var report = new AttendanceReport();
                report.DataSource = dataTable;
                report.Parameters["StartDate"].Value = _startDate;
                report.Parameters["EndDate"].Value = _endDate;
                report.Parameters["CompanyName"].Value = _sessionManager.CompanyName;

                // عرض معاينة التقرير
                documentViewer.DocumentSource = report;
                report.CreateDocument();
            }
            else
            {
                XtraMessageBox.Show("لا توجد بيانات للعرض في التقرير. الرجاء تحديد معايير بحث مختلفة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        /// <summary>
        /// حدث ضغط زر الطباعة
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                documentViewer.PrintDirect();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء طباعة التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث ضغط زر التصدير
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // عرض حوار حفظ الملف
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "ملف PDF (*.pdf)|*.pdf|ملف Excel (*.xlsx)|*.xlsx|ملف Word (*.docx)|*.docx";
                saveDialog.Title = "تصدير التقرير";
                saveDialog.FileName = $"تقرير_الحضور_{DateTime.Now:yyyyMMdd}";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveDialog.FileName;
                    string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();

                    switch (fileExtension)
                    {
                        case ".pdf":
                            documentViewer.ExportToPdf(filePath);
                            break;
                        case ".xlsx":
                            documentViewer.ExportToXlsx(filePath);
                            break;
                        case ".docx":
                            documentViewer.ExportToDocx(filePath);
                            break;
                    }

                    if (XtraMessageBox.Show("تم تصدير التقرير بنجاح. هل تريد فتح الملف؟", "تم", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تصدير التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث ضغط زر الإغلاق
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    /// <summary>
    /// فئة التقرير
    /// </summary>
    public class AttendanceReport : XtraReport
    {
        public AttendanceReport()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // تكوين التقرير
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                CreateDetailBand(),
                CreatePageHeaderBand(),
                CreateReportHeaderBand(),
                CreatePageFooterBand()
            });

            // إضافة البارامترات
            var startDateParam = new DevExpress.XtraReports.Parameters.Parameter();
            startDateParam.Name = "StartDate";
            startDateParam.Type = typeof(DateTime);
            startDateParam.Description = "تاريخ البداية";

            var endDateParam = new DevExpress.XtraReports.Parameters.Parameter();
            endDateParam.Name = "EndDate";
            endDateParam.Type = typeof(DateTime);
            endDateParam.Description = "تاريخ النهاية";

            var companyNameParam = new DevExpress.XtraReports.Parameters.Parameter();
            companyNameParam.Name = "CompanyName";
            companyNameParam.Type = typeof(string);
            companyNameParam.Description = "اسم الشركة";

            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
                startDateParam,
                endDateParam,
                companyNameParam
            });

            this.DisplayName = "تقرير الحضور والانصراف";
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "22.1";
            this.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.RightToLeftLayout = DevExpress.XtraReports.UI.RightToLeftLayout.Yes;
        }

        private DevExpress.XtraReports.UI.DetailBand CreateDetailBand()
        {
            var detailBand = new DevExpress.XtraReports.UI.DetailBand();
            detailBand.HeightF = 25F;

            // إنشاء الخلايا للصفوف
            var xrTableRow = new DevExpress.XtraReports.UI.XRTableRow();
            xrTableRow.HeightF = 25F;
            
            var xrTable = new DevExpress.XtraReports.UI.XRTable();
            xrTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            xrTable.LocationF = new System.Drawing.PointF(0F, 0F);
            xrTable.SizeF = new System.Drawing.SizeF(1129F, 25F);
            xrTable.Rows.Add(xrTableRow);

            // إضافة الأعمدة
            AddDetailTableCell(xrTableRow, "EmployeeNumber", "رقم الموظف", 80F);
            AddDetailTableCell(xrTableRow, "EmployeeName", "اسم الموظف", 150F);
            AddDetailTableCell(xrTableRow, "DepartmentName", "القسم", 120F);
            AddDetailTableCell(xrTableRow, "PositionTitle", "المسمى الوظيفي", 120F);
            AddDetailTableCell(xrTableRow, "AttendanceDate", "التاريخ", 100F, "{0:dd/MM/yyyy}");
            AddDetailTableCell(xrTableRow, "TimeIn", "وقت الدخول", 80F, "{0:hh\\:mm}");
            AddDetailTableCell(xrTableRow, "TimeOut", "وقت الخروج", 80F, "{0:hh\\:mm}");
            AddDetailTableCell(xrTableRow, "LateMinutes", "التأخير (د)", 70F);
            AddDetailTableCell(xrTableRow, "EarlyDepartureMinutes", "المغادرة المبكرة (د)", 70F);
            AddDetailTableCell(xrTableRow, "OvertimeMinutes", "الوقت الإضافي (د)", 70F);
            AddDetailTableCell(xrTableRow, "WorkedMinutes", "وقت العمل (د)", 70F);
            AddDetailTableCell(xrTableRow, "Status", "الحالة", 80F);
            AddDetailTableCell(xrTableRow, "Notes", "ملاحظات", 150F);

            detailBand.Controls.Add(xrTable);
            return detailBand;
        }

        private void AddDetailTableCell(DevExpress.XtraReports.UI.XRTableRow row, string fieldName, string caption, float width, string formatString = "")
        {
            var cell = new DevExpress.XtraReports.UI.XRTableCell();
            cell.Name = "cell" + fieldName;
            cell.Text = fieldName;
            cell.DataBindings.Add(new DevExpress.XtraReports.UI.XRBinding("Text", null, fieldName, formatString));
            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            cell.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0);
            cell.StylePriority.UseTextAlignment = false;
            cell.WidthF = width;
            
            row.Cells.Add(cell);
        }

        private DevExpress.XtraReports.UI.PageHeaderBand CreatePageHeaderBand()
        {
            var pageHeaderBand = new DevExpress.XtraReports.UI.PageHeaderBand();
            pageHeaderBand.HeightF = 25F;

            // إنشاء جدول الهيدر
            var xrTableRow = new DevExpress.XtraReports.UI.XRTableRow();
            xrTableRow.HeightF = 25F;
            
            var xrTable = new DevExpress.XtraReports.UI.XRTable();
            xrTable.BackColor = System.Drawing.Color.LightGray;
            xrTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            xrTable.LocationF = new System.Drawing.PointF(0F, 0F);
            xrTable.SizeF = new System.Drawing.SizeF(1129F, 25F);
            xrTable.StylePriority.UseBackColor = true;
            xrTable.StylePriority.UseBorders = true;
            xrTable.Rows.Add(xrTableRow);

            // إضافة خلايا الهيدر
            AddHeaderTableCell(xrTableRow, "رقم الموظف", 80F);
            AddHeaderTableCell(xrTableRow, "اسم الموظف", 150F);
            AddHeaderTableCell(xrTableRow, "القسم", 120F);
            AddHeaderTableCell(xrTableRow, "المسمى الوظيفي", 120F);
            AddHeaderTableCell(xrTableRow, "التاريخ", 100F);
            AddHeaderTableCell(xrTableRow, "وقت الدخول", 80F);
            AddHeaderTableCell(xrTableRow, "وقت الخروج", 80F);
            AddHeaderTableCell(xrTableRow, "التأخير (د)", 70F);
            AddHeaderTableCell(xrTableRow, "المغادرة المبكرة (د)", 70F);
            AddHeaderTableCell(xrTableRow, "الوقت الإضافي (د)", 70F);
            AddHeaderTableCell(xrTableRow, "وقت العمل (د)", 70F);
            AddHeaderTableCell(xrTableRow, "الحالة", 80F);
            AddHeaderTableCell(xrTableRow, "ملاحظات", 150F);

            pageHeaderBand.Controls.Add(xrTable);
            return pageHeaderBand;
        }

        private void AddHeaderTableCell(DevExpress.XtraReports.UI.XRTableRow row, string caption, float width)
        {
            var cell = new DevExpress.XtraReports.UI.XRTableCell();
            cell.Name = "header" + caption;
            cell.Text = caption;
            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            cell.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0);
            cell.StylePriority.UseTextAlignment = false;
            cell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            cell.StylePriority.UseFont = true;
            cell.WidthF = width;
            
            row.Cells.Add(cell);
        }

        private DevExpress.XtraReports.UI.ReportHeaderBand CreateReportHeaderBand()
        {
            var reportHeaderBand = new DevExpress.XtraReports.UI.ReportHeaderBand();
            reportHeaderBand.HeightF = 70F;

            // عنوان التقرير
            var titleLabel = new DevExpress.XtraReports.UI.XRLabel();
            titleLabel.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            titleLabel.LocationF = new System.Drawing.PointF(0F, 0F);
            titleLabel.SizeF = new System.Drawing.SizeF(1129F, 30F);
            titleLabel.StylePriority.UseFont = true;
            titleLabel.StylePriority.UseTextAlignment = true;
            titleLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            titleLabel.Text = "تقرير الحضور والانصراف";

            // اسم الشركة
            var companyLabel = new DevExpress.XtraReports.UI.XRLabel();
            companyLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            companyLabel.LocationF = new System.Drawing.PointF(0F, 30F);
            companyLabel.SizeF = new System.Drawing.SizeF(1129F, 20F);
            companyLabel.StylePriority.UseFont = true;
            companyLabel.StylePriority.UseTextAlignment = true;
            companyLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            companyLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0);
            companyLabel.DataBindings.Add(new DevExpress.XtraReports.UI.XRBinding("Text", null, "CompanyName"));

            // فترة التقرير
            var periodLabel = new DevExpress.XtraReports.UI.XRLabel();
            periodLabel.Font = new System.Drawing.Font("Arial", 9.75F);
            periodLabel.LocationF = new System.Drawing.PointF(0F, 50F);
            periodLabel.SizeF = new System.Drawing.SizeF(1129F, 20F);
            periodLabel.StylePriority.UseFont = true;
            periodLabel.StylePriority.UseTextAlignment = true;
            periodLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            periodLabel.Text = "الفترة من: [Parameters.StartDate!dd/MM/yyyy] إلى: [Parameters.EndDate!dd/MM/yyyy]";

            reportHeaderBand.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                titleLabel,
                companyLabel,
                periodLabel
            });

            return reportHeaderBand;
        }

        private DevExpress.XtraReports.UI.PageFooterBand CreatePageFooterBand()
        {
            var pageFooterBand = new DevExpress.XtraReports.UI.PageFooterBand();
            pageFooterBand.HeightF = 30F;

            // إضافة رقم الصفحة
            var pageInfoLabel = new DevExpress.XtraReports.UI.XRPageInfo();
            pageInfoLabel.LocationF = new System.Drawing.PointF(0F, 5F);
            pageInfoLabel.SizeF = new System.Drawing.SizeF(1129F, 20F);
            pageInfoLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            pageInfoLabel.TextFormatString = "الصفحة {0} من {1}";
            pageInfoLabel.StylePriority.UseTextAlignment = true;

            pageFooterBand.Controls.Add(pageInfoLabel);
            return pageFooterBand;
        }
    }
}