using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HR.Core;

namespace HR.UI.Forms.Reports
{
    /// <summary>
    /// نموذج تقرير الموظفين البسيط (بدون DevExpress)
    /// </summary>
    public partial class SimpleEmployeeReportForm : Form
    {
        private readonly ConnectionManager _connectionManager;
        
        public SimpleEmployeeReportForm()
        {
            InitializeComponent();
            
            _connectionManager = new ConnectionManager();
            
            // تحميل البيانات الأولية
            InitializeData();
        }
        
        /// <summary>
        /// تهيئة البيانات الأولية للنموذج
        /// </summary>
        private void InitializeData()
        {
            try
            {
                // تعيين القيم الافتراضية
                dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpEndDate.Value = DateTime.Now;
                
                // تحميل قائمة الإدارات
                LoadDepartments();
                
                // تحميل قائمة الموظفين
                LoadEmployees();
                
                // ربط الأحداث
                cboDepartment.SelectedIndexChanged += cboDepartment_SelectedIndexChanged;
                cboEmployee.SelectedIndexChanged += filter_ValueChanged;
                dtpStartDate.ValueChanged += filter_ValueChanged;
                dtpEndDate.ValueChanged += filter_ValueChanged;
                rdoActive.CheckedChanged += filter_ValueChanged;
                rdoInactive.CheckedChanged += filter_ValueChanged;
                
                // تهيئة GridView
                InitializeDataGridView();
                
                // تحميل البيانات الافتراضية
                LoadReportData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"حدث خطأ أثناء تهيئة البيانات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
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
                        
                        cboDepartment.DataSource = dt;
                        cboDepartment.DisplayMember = "Name";
                        cboDepartment.ValueMember = "ID";
                        cboDepartment.SelectedIndex = 0; // تعيين "كل الإدارات" كخيار افتراضي
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تحميل قائمة الإدارات: {ex.Message}");
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
                        
                        cboEmployee.DataSource = dt;
                        cboEmployee.DisplayMember = "FullName";
                        cboEmployee.ValueMember = "ID";
                        cboEmployee.SelectedIndex = 0; // تعيين "كل الموظفين" كخيار افتراضي
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تحميل قائمة الموظفين: {ex.Message}");
            }
        }
        
        /// <summary>
        /// تهيئة إعدادات جدول البيانات
        /// </summary>
        private void InitializeDataGridView()
        {
            try
            {
                // تهيئة DataGridView
                dgvEmployees.AutoGenerateColumns = false;
                dgvEmployees.AllowUserToAddRows = false;
                dgvEmployees.AllowUserToDeleteRows = false;
                dgvEmployees.ReadOnly = true;
                dgvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvEmployees.MultiSelect = false;
                dgvEmployees.RightToLeft = RightToLeft.Yes;
                dgvEmployees.BackgroundColor = Color.White;
                dgvEmployees.RowHeadersVisible = false;
                dgvEmployees.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                dgvEmployees.EnableHeadersVisualStyles = false;
                
                // تصميم رأس الجدول
                dgvEmployees.ColumnHeadersDefaultCellStyle.BackColor = Color.RoyalBlue;
                dgvEmployees.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvEmployees.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9F, FontStyle.Bold);
                dgvEmployees.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEmployees.ColumnHeadersHeight = 35;
                
                // إضافة الأعمدة
                AddColumn("EmployeeNumber", "رقم الموظف", 80);
                AddColumn("FullName", "اسم الموظف", 150);
                AddColumn("JobTitle", "المسمى الوظيفي", 120);
                AddColumn("DepartmentName", "الإدارة", 120);
                AddColumn("PositionTitle", "المنصب", 120);
                AddColumn("HireDate", "تاريخ التعيين", 100);
                AddColumn("BasicSalary", "الراتب الأساسي", 100);
                AddColumn("EmploymentType", "نوع التوظيف", 100);
                AddColumn("StatusText", "الحالة", 80);
                AddColumn("WorkShiftName", "الدوام", 100);
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تهيئة جدول البيانات: {ex.Message}");
            }
        }
        
        /// <summary>
        /// إضافة عمود إلى DataGridView
        /// </summary>
        private void AddColumn(string dataPropertyName, string headerText, int width)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = dataPropertyName;
            column.HeaderText = headerText;
            column.Width = width;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            // تنسيق خاص لبعض الأعمدة
            if (dataPropertyName == "HireDate" || dataPropertyName == "BirthDate")
            {
                column.DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            else if (dataPropertyName == "BasicSalary")
            {
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            
            dgvEmployees.Columns.Add(column);
        }
        
        /// <summary>
        /// تحميل بيانات التقرير
        /// </summary>
        private void LoadReportData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // إنشاء معايير التقرير
                var filter = CreateFilterOptions();
                
                // الحصول على البيانات
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    // بناء استعلام الموظفين
                    string whereClause = BuildWhereClause(filter);
                    
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
                    
                    using (var command = new System.Data.SqlClient.SqlCommand(employeesQuery, connection))
                    {
                        // إضافة البارامترات
                        AddFilterParameters(command, filter);
                        
                        // تنفيذ الاستعلام
                        DataTable resultTable = new DataTable();
                        var adapter = new System.Data.SqlClient.SqlDataAdapter(command);
                        adapter.Fill(resultTable);
                        
                        // تعيين البيانات للجدول
                        dgvEmployees.DataSource = resultTable;
                        
                        // تحديث عنوان التقرير بعدد النتائج
                        lblReportTitle.Text = $"تقرير الموظفين - عدد النتائج: {resultTable.Rows.Count}";
                        
                        // تطبيق تنسيق خاص للخلايا
                        ApplyCellFormatting();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"حدث خطأ أثناء تحميل بيانات التقرير: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// تطبيق تنسيق خاص للخلايا
        /// </summary>
        private void ApplyCellFormatting()
        {
            // تلوين خلايا الحالة
            foreach (DataGridViewRow row in dgvEmployees.Rows)
            {
                if (row.Cells["StatusText"].Value != null)
                {
                    string status = row.Cells["StatusText"].Value.ToString();
                    if (status == "نشط")
                    {
                        row.Cells["StatusText"].Style.ForeColor = Color.Green;
                        row.Cells["StatusText"].Style.Font = new Font(dgvEmployees.Font, FontStyle.Bold);
                    }
                    else if (status == "غير نشط")
                    {
                        row.Cells["StatusText"].Style.ForeColor = Color.Red;
                        row.Cells["StatusText"].Style.Font = new Font(dgvEmployees.Font, FontStyle.Bold);
                    }
                }
            }
        }
        
        /// <summary>
        /// إنشاء معايير التصفية بناءً على اختيارات المستخدم
        /// </summary>
        private ReportFilterOptions CreateFilterOptions()
        {
            var filter = new ReportFilterOptions();
            
            // تعيين الفترة الزمنية
            if (chkDateRange.Checked)
            {
                filter.StartDate = dtpStartDate.Value;
                filter.EndDate = dtpEndDate.Value;
            }
            
            // تعيين الإدارة والموظف
            if (cboDepartment.SelectedValue != null && Convert.ToInt32(cboDepartment.SelectedValue) > 0)
            {
                filter.DepartmentId = Convert.ToInt32(cboDepartment.SelectedValue);
            }
            
            if (cboEmployee.SelectedValue != null && Convert.ToInt32(cboEmployee.SelectedValue) > 0)
            {
                filter.EmployeeId = Convert.ToInt32(cboEmployee.SelectedValue);
            }
            
            // تعيين حالة الموظف
            if (rdoActive.Checked)
            {
                filter.IsActive = true;
            }
            else if (rdoInactive.Checked)
            {
                filter.IsActive = false;
            }
            
            return filter;
        }
        
        /// <summary>
        /// بناء شرط WHERE لاستعلام الموظفين
        /// </summary>
        private string BuildWhereClause(ReportFilterOptions filter)
        {
            List<string> conditions = new List<string>();
            
            // شرط الفترة الزمنية (تاريخ التعيين)
            if (chkDateRange.Checked)
            {
                conditions.Add("E.HireDate BETWEEN @StartDate AND @EndDate");
            }
            
            // شرط الإدارة
            if (filter.DepartmentId.HasValue && filter.DepartmentId.Value > 0)
            {
                conditions.Add("E.DepartmentID = @DepartmentId");
            }
            
            // شرط الموظف
            if (filter.EmployeeId.HasValue && filter.EmployeeId.Value > 0)
            {
                conditions.Add("E.ID = @EmployeeId");
            }
            
            // شرط الحالة
            if (filter.IsActive.HasValue)
            {
                conditions.Add("E.IsActive = @IsActive");
            }
            
            // بناء جملة WHERE
            if (conditions.Count > 0)
            {
                return "WHERE " + string.Join(" AND ", conditions);
            }
            
            return string.Empty;
        }
        
        /// <summary>
        /// إضافة بارامترات الاستعلام
        /// </summary>
        private void AddFilterParameters(System.Data.SqlClient.SqlCommand command, ReportFilterOptions filter)
        {
            // بارامترات الفترة الزمنية
            if (chkDateRange.Checked)
            {
                command.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                command.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);
            }
            
            // بارامترات الإدارة والموظف
            if (filter.DepartmentId.HasValue && filter.DepartmentId.Value > 0)
            {
                command.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId.Value);
            }
            
            if (filter.EmployeeId.HasValue && filter.EmployeeId.Value > 0)
            {
                command.Parameters.AddWithValue("@EmployeeId", filter.EmployeeId.Value);
            }
            
            // بارامتر الحالة
            if (filter.IsActive.HasValue)
            {
                command.Parameters.AddWithValue("@IsActive", filter.IsActive.Value);
            }
        }
        
        #region أحداث أدوات التحكم
        
        /// <summary>
        /// حدث تغيير الإدارة
        /// </summary>
        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int departmentId = 0;
                if (cboDepartment.SelectedValue != null)
                {
                    departmentId = Convert.ToInt32(cboDepartment.SelectedValue);
                }
                
                // تحديث قائمة الموظفين
                LoadEmployees(departmentId);
                
                // تحديث البيانات
                LoadReportData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"حدث خطأ: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// حدث تغيير أي من أدوات التصفية
        /// </summary>
        private void filter_ValueChanged(object sender, EventArgs e)
        {
            LoadReportData();
        }
        
        /// <summary>
        /// زر إنشاء التقرير
        /// </summary>
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // إعادة تحميل البيانات
                LoadReportData();
                
                // في هذه النسخة البسيطة، لا نقوم بإنشاء تقرير منفصل
                MessageBox.Show(
                    "تم تحديث بيانات التقرير بنجاح.",
                    "تم",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"حدث خطأ أثناء إنشاء التقرير: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// زر تصدير البيانات إلى Excel
        /// </summary>
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "ملفات Excel|*.xlsx";
                saveDialog.Title = "تصدير إلى Excel";
                saveDialog.FileName = "تقرير الموظفين.xlsx";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // في هذه النسخة البسيطة، سنعرض رسالة فقط
                    MessageBox.Show(
                        $"سيتم تصدير البيانات إلى: {saveDialog.FileName}",
                        "تصدير",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"حدث خطأ أثناء تصدير البيانات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// زر الطباعة
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // في هذه النسخة البسيطة، سنعرض رسالة فقط
                MessageBox.Show(
                    "سيتم طباعة التقرير",
                    "طباعة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"حدث خطأ أثناء الطباعة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// زر تحديث البيانات
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReportData();
        }
        
        #endregion
        
        /// <summary>
        /// فئة لتخزين معايير التصفية
        /// </summary>
        public class ReportFilterOptions
        {
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public int? DepartmentId { get; set; }
            public int? EmployeeId { get; set; }
            public bool? IsActive { get; set; }
        }
    }
}