using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.DataAccess;

namespace HR.UI.Forms.Attendance
{
    /// <summary>
    /// نموذج اختيار موظف
    /// </summary>
    public partial class EmployeeSelectorForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;

        /// <summary>
        /// معرف الموظف المختار
        /// </summary>
        public int SelectedEmployeeId { get; private set; }

        /// <summary>
        /// إنشاء نموذج جديد
        /// </summary>
        public EmployeeSelectorForm()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
            SelectedEmployeeId = 0;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void EmployeeSelectorForm_Load(object sender, EventArgs e)
        {
            try
            {
                // تحميل قائمة الموظفين
                LoadEmployees();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات الموظفين: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل قائمة الموظفين
        /// </summary>
        private void LoadEmployees()
        {
            try
            {
                string query = @"
                SELECT 
                    e.ID, 
                    e.EmployeeNumber, 
                    e.FullName,
                    d.Name AS DepartmentName,
                    p.Title AS PositionTitle
                FROM 
                    Employees e
                LEFT JOIN 
                    Departments d ON e.DepartmentID = d.ID
                LEFT JOIN 
                    Positions p ON e.PositionID = p.ID
                WHERE 
                    e.Status = 'Active'
                ORDER BY 
                    e.FullName";

                var dataTable = _dbContext.ExecuteReader(query);
                gridEmployees.DataSource = dataTable;
                gridViewEmployees.BestFitColumns();
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء تحميل قائمة الموظفين: " + ex.Message);
            }
        }

        /// <summary>
        /// البحث عن موظف
        /// </summary>
        private void SearchEmployee()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    LoadEmployees();
                    return;
                }

                string query = @"
                SELECT 
                    e.ID, 
                    e.EmployeeNumber, 
                    e.FullName,
                    d.Name AS DepartmentName,
                    p.Title AS PositionTitle
                FROM 
                    Employees e
                LEFT JOIN 
                    Departments d ON e.DepartmentID = d.ID
                LEFT JOIN 
                    Positions p ON e.PositionID = p.ID
                WHERE 
                    e.Status = 'Active'
                    AND (
                        e.EmployeeNumber LIKE @SearchTerm
                        OR e.FullName LIKE @SearchTerm
                        OR d.Name LIKE @SearchTerm
                        OR p.Title LIKE @SearchTerm
                    )
                ORDER BY 
                    e.FullName";

                var parameters = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@SearchTerm", "%" + txtSearch.Text + "%")
                };

                var dataTable = _dbContext.ExecuteReader(query, parameters);
                gridEmployees.DataSource = dataTable;
                gridViewEmployees.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء البحث: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// اختيار موظف
        /// </summary>
        private void SelectEmployee()
        {
            try
            {
                if (gridViewEmployees.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("الرجاء اختيار موظف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SelectedEmployeeId = Convert.ToInt32(gridViewEmployees.GetRowCellValue(gridViewEmployees.FocusedRowHandle, "ID"));
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء اختيار الموظف: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر بحث
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchEmployee();
        }

        /// <summary>
        /// حدث النقر على زر اختيار
        /// </summary>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectEmployee();
        }

        /// <summary>
        /// حدث النقر على زر إلغاء
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// حدث النقر المزدوج على الجدول
        /// </summary>
        private void gridViewEmployees_DoubleClick(object sender, EventArgs e)
        {
            SelectEmployee();
        }

        /// <summary>
        /// حدث الضغط على زر Enter في حقل البحث
        /// </summary>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchEmployee();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}