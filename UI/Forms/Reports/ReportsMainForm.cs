using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.Core.Reports;

namespace HR.UI.Forms.Reports
{
    public partial class ReportsMainForm : XtraForm
    {
        private readonly ReportManager _reportManager;
        private readonly PayrollReport _payrollReport;
        private readonly AttendanceReport _attendanceReport;
        private readonly LeaveReport _leaveReport;
        private readonly CustomReportGenerator _customReportGenerator;
        
        // تتبع حالة التقرير المعروض حاليًا
        private string _currentReportType;
        
        /// <summary>
        /// إنشاء نموذج التقارير الرئيسي
        /// </summary>
        public ReportsMainForm()
        {
            InitializeComponent();
            
            _reportManager = new ReportManager();
            _payrollReport = new PayrollReport();
            _attendanceReport = new AttendanceReport();
            _leaveReport = new LeaveReport();
            _customReportGenerator = new CustomReportGenerator();
            
            // ربط أحداث الأزرار
            InitializeButtons();
        }
        
        /// <summary>
        /// إنشاء نموذج التقارير وعرض تقرير محدد
        /// </summary>
        /// <param name="reportType">نوع التقرير المراد عرضه</param>
        public ReportsMainForm(string reportType)
        {
            InitializeComponent();
            
            _reportManager = new ReportManager();
            _payrollReport = new PayrollReport();
            _attendanceReport = new AttendanceReport();
            _leaveReport = new LeaveReport();
            _customReportGenerator = new CustomReportGenerator();
            
            // ربط أحداث الأزرار
            InitializeButtons();
            
            // عرض التقرير المطلوب
            switch (reportType)
            {
                case "Employees":
                    ShowEmployeeReport();
                    break;
                case "Attendance":
                    ShowAttendanceReport();
                    break;
                case "Leave":
                    ShowLeaveReport();
                    break;
                case "Payroll":
                    ShowPayrollReport();
                    break;
                case "Custom":
                    ShowCustomReport();
                    break;
            }
        }
        
        /// <summary>
        /// عرض تقرير الموظفين
        /// </summary>
        public void ShowEmployeeReport()
        {
            _currentReportType = "Employees";
            btnEmployeesList.PerformClick();
        }
        
        /// <summary>
        /// عرض تقرير الحضور
        /// </summary>
        public void ShowAttendanceReport()
        {
            _currentReportType = "Attendance";
            btnAttendanceSummary.PerformClick();
        }
        
        /// <summary>
        /// عرض تقرير الإجازات
        /// </summary>
        public void ShowLeaveReport()
        {
            _currentReportType = "Leave";
            btnLeaveReport.PerformClick();
        }
        
        /// <summary>
        /// عرض تقرير الرواتب
        /// </summary>
        public void ShowPayrollReport()
        {
            _currentReportType = "Payroll";
            btnPayrollReport.PerformClick();
        }
        
        /// <summary>
        /// عرض التقارير المخصصة
        /// </summary>
        public void ShowCustomReport()
        {
            _currentReportType = "Custom";
            btnCustomReport.PerformClick();
        }
        
        /// <summary>
        /// تهيئة أحداث الأزرار
        /// </summary>
        private void InitializeButtons()
        {
            // تقارير القوى العاملة
            btnEmployeesList.Click += BtnEmployeesList_Click;
            btnEmployeeDetails.Click += BtnEmployeeDetails_Click;
            btnDepartmentSummary.Click += BtnDepartmentSummary_Click;
            
            // تقارير الحضور والإجازات
            btnAttendanceSummary.Click += BtnAttendanceSummary_Click;
            btnAttendanceDetails.Click += BtnAttendanceDetails_Click;
            btnLeaveReport.Click += BtnLeaveReport_Click;
            btnLeaveBalance.Click += BtnLeaveBalance_Click;
            
            // تقارير الرواتب
            btnPayrollReport.Click += BtnPayrollReport_Click;
            btnSalaryAnalysis.Click += BtnSalaryAnalysis_Click;
            btnDeductionReport.Click += BtnDeductionReport_Click;
            
            // تقارير الأداء
            btnPerformanceReport.Click += BtnPerformanceReport_Click;
            
            // التقارير المخصصة
            btnCustomReport.Click += BtnCustomReport_Click;
        }
        
        #region أحداث تقارير القوى العاملة
        
        /// <summary>
        /// تقرير قائمة الموظفين
        /// </summary>
        private void BtnEmployeesList_Click(object sender, EventArgs e)
        {
            try
            {
                var filterOptions = new CustomReportGenerator.ReportFilterOptions();
                
                // عرض تقرير الموظفين
                _customReportGenerator.ShowCustomReport(
                    CustomReportGenerator.CustomReportType.Employees,
                    "تقرير قائمة الموظفين", 
                    filterOptions);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إنشاء التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تقرير تفاصيل موظف
        /// </summary>
        private void BtnEmployeeDetails_Click(object sender, EventArgs e)
        {
            try
            {
                // فتح نموذج اختيار الموظف ثم عرض التقرير
                using (var form = new EmployeeSelectionForm())
                {
                    if (form.ShowDialog() == DialogResult.OK && form.SelectedEmployeeId > 0)
                    {
                        var filterOptions = new CustomReportGenerator.ReportFilterOptions
                        {
                            EmployeeId = form.SelectedEmployeeId
                        };
                        
                        // عرض تقرير تفاصيل الموظف
                        _customReportGenerator.ShowCustomReport(
                            CustomReportGenerator.CustomReportType.EmployeeDetails,
                            "تقرير تفاصيل الموظف: " + form.SelectedEmployeeName, 
                            filterOptions);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إنشاء التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تقرير ملخص الإدارات
        /// </summary>
        private void BtnDepartmentSummary_Click(object sender, EventArgs e)
        {
            try
            {
                var filterOptions = new CustomReportGenerator.ReportFilterOptions();
                
                // عرض تقرير ملخص الإدارات
                _customReportGenerator.ShowCustomReport(
                    CustomReportGenerator.CustomReportType.DepartmentSummary,
                    "تقرير ملخص الإدارات", 
                    filterOptions);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إنشاء التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion
        
        #region أحداث تقارير الحضور والإجازات
        
        /// <summary>
        /// تقرير ملخص الحضور
        /// </summary>
        private void BtnAttendanceSummary_Click(object sender, EventArgs e)
        {
            try
            {
                // فتح نموذج اختيار الفترة والإدارة
                using (var form = new DateRangeSelectionForm("تقرير ملخص الحضور والغياب"))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var filterOptions = new CustomReportGenerator.ReportFilterOptions
                        {
                            StartDate = form.StartDate,
                            EndDate = form.EndDate,
                            DepartmentId = form.DepartmentId,
                            IncludeSummary = true,
                            IncludeCharts = true,
                            IncludeDetails = false
                        };
                        
                        // عرض تقرير ملخص الحضور
                        _customReportGenerator.ShowCustomReport(
                            CustomReportGenerator.CustomReportType.Attendance,
                            "تقرير ملخص الحضور والغياب", 
                            filterOptions);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إنشاء التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تقرير تفاصيل الحضور
        /// </summary>
        private void BtnAttendanceDetails_Click(object sender, EventArgs e)
        {
            try
            {
                // استخدام تقرير الحضور المفصل
                using (var form = new DateRangeSelectionForm("تقرير تفاصيل الحضور والغياب"))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        int departmentId = form.DepartmentId;
                        _attendanceReport.ShowAttendanceReport(departmentId, form.StartDate, form.EndDate);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إنشاء التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تقرير الإجازات
        /// </summary>
        private void BtnLeaveReport_Click(object sender, EventArgs e)
        {
            try
            {
                // استخدام تقرير الإجازات
                using (var form = new DateRangeSelectionForm("تقرير الإجازات"))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        int departmentId = form.DepartmentId;
                        _leaveReport.ShowLeaveReport(departmentId, form.StartDate, form.EndDate);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إنشاء التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تقرير رصيد الإجازات
        /// </summary>
        private void BtnLeaveBalance_Click(object sender, EventArgs e)
        {
            try
            {
                var filterOptions = new CustomReportGenerator.ReportFilterOptions
                {
                    // تعيين الإعدادات كما هو مطلوب لتقرير رصيد الإجازات
                    IncludeSummary = true,
                    IncludeCharts = true
                };
                
                // عرض تقرير رصيد الإجازات
                _customReportGenerator.ShowCustomReport(
                    CustomReportGenerator.CustomReportType.Leave,
                    "تقرير رصيد الإجازات", 
                    filterOptions);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إنشاء التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion
        
        #region أحداث تقارير الرواتب
        
        /// <summary>
        /// تقرير كشف الرواتب
        /// </summary>
        private void BtnPayrollReport_Click(object sender, EventArgs e)
        {
            try
            {
                // استخدام تقرير الرواتب
                using (var form = new PayrollSelectionForm())
                {
                    if (form.ShowDialog() == DialogResult.OK && form.SelectedPayrollId > 0)
                    {
                        _payrollReport.ShowPayrollReport(form.SelectedPayrollId);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إنشاء التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تقرير تحليل الرواتب
        /// </summary>
        private void BtnSalaryAnalysis_Click(object sender, EventArgs e)
        {
            try
            {
                var filterOptions = new CustomReportGenerator.ReportFilterOptions
                {
                    // تعيين الإعدادات كما هو مطلوب لتقرير تحليل الرواتب
                    IncludeSummary = true,
                    IncludeCharts = true
                };
                
                // عرض تقرير تحليل الرواتب
                _customReportGenerator.ShowCustomReport(
                    CustomReportGenerator.CustomReportType.Payroll,
                    "تقرير تحليل الرواتب", 
                    filterOptions);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إنشاء التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تقرير الخصومات
        /// </summary>
        private void BtnDeductionReport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new DateRangeSelectionForm("تقرير الخصومات"))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var filterOptions = new CustomReportGenerator.ReportFilterOptions
                        {
                            StartDate = form.StartDate,
                            EndDate = form.EndDate,
                            DepartmentId = form.DepartmentId,
                            IncludeSummary = true,
                            IncludeCharts = true
                        };
                        
                        // عرض تقرير الخصومات (باستخدام تقرير الرواتب المخصص)
                        _customReportGenerator.ShowCustomReport(
                            CustomReportGenerator.CustomReportType.Payroll,
                            "تقرير الخصومات", 
                            filterOptions);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إنشاء التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion
        
        #region أحداث تقارير الأداء
        
        /// <summary>
        /// تقرير الأداء
        /// </summary>
        private void BtnPerformanceReport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new DateRangeSelectionForm("تقرير الأداء"))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var filterOptions = new CustomReportGenerator.ReportFilterOptions
                        {
                            StartDate = form.StartDate,
                            EndDate = form.EndDate,
                            DepartmentId = form.DepartmentId,
                            IncludeSummary = true,
                            IncludeCharts = true
                        };
                        
                        // عرض تقرير الأداء
                        _customReportGenerator.ShowCustomReport(
                            CustomReportGenerator.CustomReportType.PerformanceSummary,
                            "تقرير ملخص الأداء", 
                            filterOptions);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إنشاء التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion
        
        #region أحداث التقارير المخصصة
        
        /// <summary>
        /// فتح مولد التقارير المخصصة
        /// </summary>
        private void BtnCustomReport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new CustomReportGeneratorForm())
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء فتح مولد التقارير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion
        
        /// <summary>
        /// زر إغلاق النموذج
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
    #region نماذج مساعدة
    
    /// <summary>
    /// نموذج اختيار الفترة والإدارة
    /// </summary>
    public class DateRangeSelectionForm : XtraForm
    {
        private LabelControl lblTitle;
        private LookUpEdit cboDepartment;
        private DateEdit dtStartDate;
        private DateEdit dtEndDate;
        private SimpleButton btnOK;
        private SimpleButton btnCancel;
        
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int DepartmentId { get; private set; }
        
        public DateRangeSelectionForm(string title)
        {
            InitializeComponent();
            lblTitle.Text = title;
            
            // تعيين القيم الافتراضية
            DateTime today = DateTime.Today;
            DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            
            dtStartDate.DateTime = firstDayOfMonth;
            dtEndDate.DateTime = lastDayOfMonth;
            
            // تحميل قائمة الإدارات
            LoadDepartments();
        }
        
        private void InitializeComponent()
        {
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.cboDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.dtStartDate = new DevExpress.XtraEditors.DateEdit();
            this.dtEndDate = new DevExpress.XtraEditors.DateEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.lblTitle.Size = new System.Drawing.Size(400, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "اختيار الفترة";
            // 
            // cboDepartment
            // 
            this.cboDepartment.Location = new System.Drawing.Point(50, 60);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDepartment.Properties.NullText = "";
            this.cboDepartment.Size = new System.Drawing.Size(250, 20);
            this.cboDepartment.TabIndex = 1;
            // 
            // dtStartDate
            // 
            this.dtStartDate.EditValue = null;
            this.dtStartDate.Location = new System.Drawing.Point(50, 100);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStartDate.Size = new System.Drawing.Size(250, 20);
            this.dtStartDate.TabIndex = 2;
            // 
            // dtEndDate
            // 
            this.dtEndDate.EditValue = null;
            this.dtEndDate.Location = new System.Drawing.Point(50, 140);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEndDate.Size = new System.Drawing.Size(250, 20);
            this.dtEndDate.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(225, 180);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "موافق";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(100, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "إلغاء";
            // 
            // DateRangeSelectionForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(400, 220);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dtEndDate);
            this.Controls.Add(this.dtStartDate);
            this.Controls.Add(this.cboDepartment);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DateRangeSelectionForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "اختيار الفترة والإدارة";
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        
        private void LoadDepartments()
        {
            try
            {
                var connectionManager = new ConnectionManager();
                
                using (var connection = connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT ID, Name FROM Departments ORDER BY Name";
                    
                    using (var adapter = new System.Data.SqlClient.SqlDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        // إضافة خيار "كل الإدارات"
                        DataRow allRow = dt.NewRow();
                        allRow["ID"] = 0;
                        allRow["Name"] = "كل الإدارات";
                        dt.Rows.InsertAt(allRow, 0);
                        
                        cboDepartment.Properties.DataSource = dt;
                        cboDepartment.Properties.DisplayMember = "Name";
                        cboDepartment.Properties.ValueMember = "ID";
                        cboDepartment.Properties.PopulateColumns();
                        cboDepartment.Properties.Columns["ID"].Visible = false;
                        cboDepartment.EditValue = 0; // تعيين "كل الإدارات" كخيار افتراضي
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل قائمة الإدارات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من صحة البيانات
                if (dtStartDate.DateTime > dtEndDate.DateTime)
                {
                    XtraMessageBox.Show("تاريخ البداية يجب أن يكون قبل تاريخ النهاية", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // تعيين القيم
                StartDate = dtStartDate.DateTime;
                EndDate = dtEndDate.DateTime;
                DepartmentId = Convert.ToInt32(cboDepartment.EditValue);
                
                // إغلاق النموذج بنجاح
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
    /// <summary>
    /// نموذج اختيار الموظف
    /// </summary>
    public class EmployeeSelectionForm : XtraForm
    {
        private LabelControl lblTitle;
        private LookUpEdit cboEmployee;
        private SimpleButton btnOK;
        private SimpleButton btnCancel;
        
        public int SelectedEmployeeId { get; private set; }
        public string SelectedEmployeeName { get; private set; }
        
        public EmployeeSelectionForm()
        {
            InitializeComponent();
            
            // تحميل قائمة الموظفين
            LoadEmployees();
        }
        
        private void InitializeComponent()
        {
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.cboEmployee = new DevExpress.XtraEditors.LookUpEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployee.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.lblTitle.Size = new System.Drawing.Size(400, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "اختيار موظف";
            // 
            // cboEmployee
            // 
            this.cboEmployee.Location = new System.Drawing.Point(50, 70);
            this.cboEmployee.Name = "cboEmployee";
            this.cboEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEmployee.Properties.NullText = "";
            this.cboEmployee.Size = new System.Drawing.Size(300, 20);
            this.cboEmployee.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(225, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "موافق";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(100, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "إلغاء";
            // 
            // EmployeeSelectionForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(400, 160);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cboEmployee);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeSelectionForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "اختيار موظف";
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployee.Properties)).EndInit();
            this.ResumeLayout(false);
        }
        
        private void LoadEmployees()
        {
            try
            {
                var connectionManager = new ConnectionManager();
                
                using (var connection = connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT ID, FullName FROM Employees WHERE IsActive = 1 ORDER BY FullName";
                    
                    using (var adapter = new System.Data.SqlClient.SqlDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        cboEmployee.Properties.DataSource = dt;
                        cboEmployee.Properties.DisplayMember = "FullName";
                        cboEmployee.Properties.ValueMember = "ID";
                        cboEmployee.Properties.PopulateColumns();
                        cboEmployee.Properties.Columns["ID"].Visible = false;
                        
                        if (dt.Rows.Count > 0)
                        {
                            cboEmployee.EditValue = dt.Rows[0]["ID"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل قائمة الموظفين: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من اختيار موظف
                if (cboEmployee.EditValue == null)
                {
                    XtraMessageBox.Show("الرجاء اختيار موظف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // تعيين القيم
                SelectedEmployeeId = Convert.ToInt32(cboEmployee.EditValue);
                SelectedEmployeeName = cboEmployee.Text;
                
                // إغلاق النموذج بنجاح
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
    /// <summary>
    /// نموذج اختيار كشف الرواتب
    /// </summary>
    public class PayrollSelectionForm : XtraForm
    {
        private LabelControl lblTitle;
        private LookUpEdit cboPayroll;
        private SimpleButton btnOK;
        private SimpleButton btnCancel;
        
        public int SelectedPayrollId { get; private set; }
        
        public PayrollSelectionForm()
        {
            InitializeComponent();
            
            // تحميل قائمة كشوفات الرواتب
            LoadPayrolls();
        }
        
        private void InitializeComponent()
        {
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.cboPayroll = new DevExpress.XtraEditors.LookUpEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboPayroll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.lblTitle.Size = new System.Drawing.Size(400, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "اختيار كشف الرواتب";
            // 
            // cboPayroll
            // 
            this.cboPayroll.Location = new System.Drawing.Point(50, 70);
            this.cboPayroll.Name = "cboPayroll";
            this.cboPayroll.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPayroll.Properties.NullText = "";
            this.cboPayroll.Size = new System.Drawing.Size(300, 20);
            this.cboPayroll.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(225, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "موافق";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(100, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "إلغاء";
            // 
            // PayrollSelectionForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(400, 160);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cboPayroll);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PayrollSelectionForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "اختيار كشف الرواتب";
            ((System.ComponentModel.ISupportInitialize)(this.cboPayroll.Properties)).EndInit();
            this.ResumeLayout(false);
        }
        
        private void LoadPayrolls()
        {
            try
            {
                var connectionManager = new ConnectionManager();
                
                using (var connection = connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT ID, 
                            CONCAT(PayrollName, ' (', PayrollMonth, '/', PayrollYear, ')') AS DisplayName
                        FROM Payrolls 
                        ORDER BY PayrollYear DESC, PayrollMonth DESC";
                    
                    using (var adapter = new System.Data.SqlClient.SqlDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        cboPayroll.Properties.DataSource = dt;
                        cboPayroll.Properties.DisplayMember = "DisplayName";
                        cboPayroll.Properties.ValueMember = "ID";
                        cboPayroll.Properties.PopulateColumns();
                        cboPayroll.Properties.Columns["ID"].Visible = false;
                        
                        if (dt.Rows.Count > 0)
                        {
                            cboPayroll.EditValue = dt.Rows[0]["ID"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل قائمة كشوفات الرواتب: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من اختيار كشف رواتب
                if (cboPayroll.EditValue == null)
                {
                    XtraMessageBox.Show("الرجاء اختيار كشف رواتب", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // تعيين القيم
                SelectedPayrollId = Convert.ToInt32(cboPayroll.EditValue);
                
                // إغلاق النموذج بنجاح
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
    #endregion
}