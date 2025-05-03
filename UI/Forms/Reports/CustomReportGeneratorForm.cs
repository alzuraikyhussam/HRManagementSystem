using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;
using HR.Core;
using HR.Core.Reports;
using HR.DataAccess;

namespace HR.UI.Forms.Reports
{
    public partial class CustomReportGeneratorForm : XtraForm
    {
        private readonly CustomReportGenerator _reportGenerator;
        private readonly ConnectionManager _connectionManager;
        
        public CustomReportGeneratorForm()
        {
            InitializeComponent();
            _reportGenerator = new CustomReportGenerator();
            _connectionManager = new ConnectionManager();
            
            // تهيئة الضوابط
            InitControls();
        }
        
        /// <summary>
        /// تهيئة عناصر التحكم
        /// </summary>
        private void InitControls()
        {
            // تهيئة قائمة أنواع التقارير
            cboReportType.Properties.Items.AddRange(new object[]
            {
                "تقرير الموظفين",
                "تقرير الحضور والغياب",
                "تقرير الإجازات",
                "تقرير الرواتب",
                "تقرير تفاصيل موظف",
                "تقرير ملخص الإدارات",
                "تقرير ملخص الأداء"
            });
            
            // تحديد النوع الافتراضي
            cboReportType.SelectedIndex = 0;
            
            // ضبط فترة التاريخ
            DateTime today = DateTime.Today;
            DateTime firstDay = new DateTime(today.Year, today.Month, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);
            
            dtStartDate.DateTime = firstDay;
            dtEndDate.DateTime = lastDay;
            
            // تحميل قوائم البيانات
            LoadDepartments();
            LoadEmployees();
            LoadLeaveTypes();
            LoadPayrolls();
            
            // ربط الأحداث
            cboReportType.SelectedIndexChanged += cboReportType_SelectedIndexChanged;
            cboDepartment.SelectedIndexChanged += cboDepartment_SelectedIndexChanged;
            
            // تهيئة أدوات التحكم وفق النوع الحالي
            UpdateControlsForSelectedReportType();
        }
        
        /// <summary>
        /// تحميل قائمة الإدارات
        /// </summary>
        private void LoadDepartments()
        {
            try
            {
                using (var connection = _connectionManager.GetConnection())
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
        
        /// <summary>
        /// تحميل قائمة الموظفين
        /// </summary>
        private void LoadEmployees(int departmentId = 0)
        {
            try
            {
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT ID, FullName FROM Employees";
                    
                    if (departmentId > 0)
                    {
                        query += " WHERE DepartmentID = @DepartmentId";
                    }
                    
                    query += " ORDER BY FullName";
                    
                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        if (departmentId > 0)
                        {
                            command.Parameters.AddWithValue("@DepartmentId", departmentId);
                        }
                        
                        DataTable dt = new DataTable();
                        var adapter = new System.Data.SqlClient.SqlDataAdapter(command);
                        adapter.Fill(dt);
                        
                        // إضافة خيار "كل الموظفين"
                        DataRow allRow = dt.NewRow();
                        allRow["ID"] = 0;
                        allRow["FullName"] = "كل الموظفين";
                        dt.Rows.InsertAt(allRow, 0);
                        
                        cboEmployee.Properties.DataSource = dt;
                        cboEmployee.Properties.DisplayMember = "FullName";
                        cboEmployee.Properties.ValueMember = "ID";
                        cboEmployee.Properties.PopulateColumns();
                        cboEmployee.Properties.Columns["ID"].Visible = false;
                        cboEmployee.EditValue = 0; // تعيين "كل الموظفين" كخيار افتراضي
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل قائمة الموظفين: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تحميل قائمة أنواع الإجازات
        /// </summary>
        private void LoadLeaveTypes()
        {
            try
            {
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT ID, Name FROM LeaveTypes ORDER BY Name";
                    
                    using (var adapter = new System.Data.SqlClient.SqlDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        // إضافة خيار "كل أنواع الإجازات"
                        DataRow allRow = dt.NewRow();
                        allRow["ID"] = 0;
                        allRow["Name"] = "كل أنواع الإجازات";
                        dt.Rows.InsertAt(allRow, 0);
                        
                        cboLeaveType.Properties.DataSource = dt;
                        cboLeaveType.Properties.DisplayMember = "Name";
                        cboLeaveType.Properties.ValueMember = "ID";
                        cboLeaveType.Properties.PopulateColumns();
                        cboLeaveType.Properties.Columns["ID"].Visible = false;
                        cboLeaveType.EditValue = 0; // تعيين "كل أنواع الإجازات" كخيار افتراضي
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل قائمة أنواع الإجازات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تحميل قائمة كشوفات الرواتب
        /// </summary>
        private void LoadPayrolls()
        {
            try
            {
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT ID, PayrollName, 
                            CONCAT(PayrollName, ' (', PayrollMonth, '/', PayrollYear, ')') AS DisplayName
                        FROM Payrolls 
                        ORDER BY PayrollYear DESC, PayrollMonth DESC";
                    
                    using (var adapter = new System.Data.SqlClient.SqlDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        // إضافة خيار "كل كشوفات الرواتب"
                        DataRow allRow = dt.NewRow();
                        allRow["ID"] = 0;
                        allRow["PayrollName"] = "كل كشوفات الرواتب";
                        allRow["DisplayName"] = "كل كشوفات الرواتب";
                        dt.Rows.InsertAt(allRow, 0);
                        
                        cboPayroll.Properties.DataSource = dt;
                        cboPayroll.Properties.DisplayMember = "DisplayName";
                        cboPayroll.Properties.ValueMember = "ID";
                        cboPayroll.Properties.PopulateColumns();
                        cboPayroll.Properties.Columns["ID"].Visible = false;
                        cboPayroll.Properties.Columns["PayrollName"].Visible = false;
                        cboPayroll.EditValue = 0; // تعيين "كل كشوفات الرواتب" كخيار افتراضي
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل قائمة كشوفات الرواتب: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تحديث عناصر التحكم وفق نوع التقرير المحدد
        /// </summary>
        private void UpdateControlsForSelectedReportType()
        {
            // إخفاء جميع لوحات الخيارات
            panelEmployee.Visible = false;
            panelAttendance.Visible = false;
            panelLeave.Visible = false;
            panelPayroll.Visible = false;
            
            // إظهار اللوحات المناسبة وفق نوع التقرير
            switch (cboReportType.SelectedIndex)
            {
                case 0: // تقرير الموظفين
                    panelEmployee.Visible = true;
                    break;
                    
                case 1: // تقرير الحضور والغياب
                    panelEmployee.Visible = true;
                    panelAttendance.Visible = true;
                    break;
                    
                case 2: // تقرير الإجازات
                    panelEmployee.Visible = true;
                    panelLeave.Visible = true;
                    break;
                    
                case 3: // تقرير الرواتب
                    panelEmployee.Visible = true;
                    panelPayroll.Visible = true;
                    break;
                    
                case 4: // تقرير تفاصيل موظف
                    panelEmployee.Visible = true;
                    dtStartDate.Enabled = true;
                    dtEndDate.Enabled = true;
                    cboEmployee.Enabled = true;
                    // لتقرير تفاصيل موظف، يجب تحديد موظف
                    if (Convert.ToInt32(cboEmployee.EditValue) == 0)
                    {
                        // تحديد أول موظف في القائمة إذا كانت تحتوي على موظفين
                        if (cboEmployee.Properties.Items.Count > 1)
                        {
                            cboEmployee.SelectedIndex = 1;
                        }
                    }
                    break;
                    
                case 5: // تقرير ملخص الإدارات
                    panelEmployee.Visible = true;
                    break;
                    
                case 6: // تقرير ملخص الأداء
                    panelEmployee.Visible = true;
                    panelAttendance.Visible = true;
                    break;
            }
        }
        
        /// <summary>
        /// حدث تغيير نوع التقرير
        /// </summary>
        private void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControlsForSelectedReportType();
        }
        
        /// <summary>
        /// حدث تغيير الإدارة
        /// </summary>
        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            int departmentId = Convert.ToInt32(cboDepartment.EditValue);
            LoadEmployees(departmentId);
        }
        
        /// <summary>
        /// زر إنشاء التقرير
        /// </summary>
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                // إنشاء كائن فلاتر التقرير
                CustomReportGenerator.ReportFilterOptions filters = new CustomReportGenerator.ReportFilterOptions();
                
                // تعيين خيارات التاريخ
                if (chkDateRange.Checked)
                {
                    filters.StartDate = dtStartDate.DateTime;
                    filters.EndDate = dtEndDate.DateTime;
                }
                
                // تعيين خيارات الإدارة والموظف
                if (Convert.ToInt32(cboDepartment.EditValue) > 0)
                {
                    filters.DepartmentId = Convert.ToInt32(cboDepartment.EditValue);
                }
                
                if (Convert.ToInt32(cboEmployee.EditValue) > 0)
                {
                    filters.EmployeeId = Convert.ToInt32(cboEmployee.EditValue);
                }
                
                // تعيين خيارات الحالة
                if (rgEmployeeStatus.SelectedIndex >= 0)
                {
                    filters.IsActive = rgEmployeeStatus.SelectedIndex == 0;
                }
                
                // تعيين خيارات الحضور
                if (chkIncludeAbsent.Checked)
                {
                    filters.IncludeAbsent = true;
                }
                
                if (chkIncludeLate.Checked)
                {
                    filters.IncludeLate = true;
                }
                
                if (chkIncludeExcused.Checked)
                {
                    filters.IncludeExcused = true;
                }
                
                if (!string.IsNullOrEmpty(txtMinLateMinutes.Text))
                {
                    filters.MinLateMinutes = Convert.ToInt32(txtMinLateMinutes.Text);
                }
                
                // تعيين خيارات الإجازات
                if (Convert.ToInt32(cboLeaveType.EditValue) > 0)
                {
                    filters.LeaveTypeId = Convert.ToInt32(cboLeaveType.EditValue);
                }
                
                if (cboLeaveStatus.SelectedIndex >= 0)
                {
                    switch (cboLeaveStatus.SelectedIndex)
                    {
                        case 0: // الكل
                            filters.LeaveStatus = null;
                            break;
                        case 1: // معتمدة
                            filters.LeaveStatus = "Approved";
                            break;
                        case 2: // قيد الانتظار
                            filters.LeaveStatus = "Pending";
                            break;
                        case 3: // مرفوضة
                            filters.LeaveStatus = "Rejected";
                            break;
                        case 4: // ملغاة
                            filters.LeaveStatus = "Cancelled";
                            break;
                    }
                }
                
                // تعيين خيارات الرواتب
                if (Convert.ToInt32(cboPayroll.EditValue) > 0)
                {
                    filters.PayrollId = Convert.ToInt32(cboPayroll.EditValue);
                }
                
                // تعيين خيارات العرض
                filters.IncludeCharts = chkIncludeCharts.Checked;
                filters.IncludeSummary = chkIncludeSummary.Checked;
                filters.IncludeDetails = chkIncludeDetails.Checked;
                
                // تحديد نوع التقرير وعنوانه
                CustomReportGenerator.CustomReportType reportType;
                string reportTitle = string.Empty;
                
                switch (cboReportType.SelectedIndex)
                {
                    case 0: // تقرير الموظفين
                        reportType = CustomReportGenerator.CustomReportType.Employees;
                        reportTitle = "تقرير الموظفين";
                        break;
                        
                    case 1: // تقرير الحضور والغياب
                        reportType = CustomReportGenerator.CustomReportType.Attendance;
                        reportTitle = "تقرير الحضور والغياب";
                        break;
                        
                    case 2: // تقرير الإجازات
                        reportType = CustomReportGenerator.CustomReportType.Leave;
                        reportTitle = "تقرير الإجازات";
                        break;
                        
                    case 3: // تقرير الرواتب
                        reportType = CustomReportGenerator.CustomReportType.Payroll;
                        reportTitle = "تقرير الرواتب";
                        break;
                        
                    case 4: // تقرير تفاصيل موظف
                        reportType = CustomReportGenerator.CustomReportType.EmployeeDetails;
                        string employeeName = cboEmployee.Text;
                        reportTitle = "تقرير تفاصيل الموظف: " + employeeName;
                        break;
                        
                    case 5: // تقرير ملخص الإدارات
                        reportType = CustomReportGenerator.CustomReportType.DepartmentSummary;
                        reportTitle = "تقرير ملخص الإدارات";
                        break;
                        
                    case 6: // تقرير ملخص الأداء
                        reportType = CustomReportGenerator.CustomReportType.PerformanceSummary;
                        reportTitle = "تقرير ملخص الأداء";
                        break;
                        
                    default:
                        throw new ArgumentException("نوع التقرير غير صالح");
                }
                
                // إضافة معلومات الإدارة للعنوان إذا تم تحديدها
                if (filters.DepartmentId.HasValue && filters.DepartmentId.Value > 0)
                {
                    reportTitle += $" - {cboDepartment.Text}";
                }
                
                // إضافة معلومات الفترة للعنوان
                if (filters.StartDate.HasValue && filters.EndDate.HasValue)
                {
                    reportTitle += $" ({filters.StartDate.Value.ToString("yyyy/MM/dd")} - {filters.EndDate.Value.ToString("yyyy/MM/dd")})";
                }
                
                // إنشاء التقرير وعرضه
                _reportGenerator.ShowCustomReport(reportType, reportTitle, filters);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إنشاء التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// زر إغلاق النموذج
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}