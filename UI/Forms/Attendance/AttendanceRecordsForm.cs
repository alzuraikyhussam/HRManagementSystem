using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;
using HR.DataAccess;

namespace HR.UI.Forms.Attendance
{
    /// <summary>
    /// نموذج سجلات الحضور والانصراف
    /// </summary>
    public partial class AttendanceRecordsForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;
        private DateTime _startDate;
        private DateTime _endDate;

        /// <summary>
        /// إنشاء نموذج جديد
        /// </summary>
        public AttendanceRecordsForm()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;

            // تعيين التواريخ الافتراضية - شهر حالي
            _startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _endDate = _startDate.AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void AttendanceRecordsForm_Load(object sender, EventArgs e)
        {
            // التحقق من صلاحيات المستخدم
            CheckUserPermissions();

            // تعيين التواريخ الافتراضية
            dateStart.DateTime = _startDate;
            dateEnd.DateTime = _endDate;

            // تحميل قائمة الأقسام
            LoadDepartments();

            // تحميل سجلات الحضور
            LoadAttendanceRecords();
        }

        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckUserPermissions()
        {
            bool canAdd = _sessionManager.HasPermission("الحضور", "add");
            bool canEdit = _sessionManager.HasPermission("الحضور", "edit");
            bool canDelete = _sessionManager.HasPermission("الحضور", "delete");
            bool canPrint = _sessionManager.HasPermission("الحضور", "print");
            bool canExport = _sessionManager.HasPermission("الحضور", "export");

            btnAdd.Enabled = canAdd;
            btnEdit.Enabled = canEdit;
            btnDelete.Enabled = canDelete;
            btnPrint.Enabled = canPrint;
            btnExport.Enabled = canExport;
        }

        /// <summary>
        /// تحميل قائمة الأقسام
        /// </summary>
        private void LoadDepartments()
        {
            try
            {
                string query = @"
                SELECT ID, Name
                FROM Departments
                WHERE IsActive = 1
                ORDER BY Name";

                var dataTable = _dbContext.ExecuteReader(query);

                // إضافة عنصر "الكل"
                DataRow allRow = dataTable.NewRow();
                allRow["ID"] = 0;
                allRow["Name"] = "جميع الأقسام";
                dataTable.Rows.InsertAt(allRow, 0);

                lookupDepartment.Properties.DataSource = dataTable;
                lookupDepartment.Properties.DisplayMember = "Name";
                lookupDepartment.Properties.ValueMember = "ID";
                lookupDepartment.EditValue = 0; // تحديد "الكل" كقيمة افتراضية
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل قائمة الأقسام: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل سجلات الحضور
        /// </summary>
        private void LoadAttendanceRecords()
        {
            try
            {
                string query = @"
                SELECT
                    ar.ID,
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
                    ar.IsManualEntry,
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
                    AND (@Status = 'All' OR ar.Status = @Status)
                ORDER BY
                    ar.AttendanceDate DESC, e.FullName";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@StartDate", _startDate),
                    new SqlParameter("@EndDate", _endDate),
                    new SqlParameter("@DepartmentID", Convert.ToInt32(lookupDepartment.EditValue)),
                    new SqlParameter("@Status", lookupStatus.EditValue.ToString())
                };

                var dataTable = _dbContext.ExecuteReader(query, parameters);
                gridAttendance.DataSource = dataTable;
                gridViewAttendance.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل سجلات الحضور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// إضافة سجل حضور يدوي
        /// </summary>
        private void AddManualRecord()
        {
            try
            {
                using (var manualForm = new ManualAttendanceForm())
                {
                    if (manualForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadAttendanceRecords();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إضافة سجل حضور يدوي: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تعديل سجل حضور
        /// </summary>
        private void EditRecord()
        {
            try
            {
                if (gridViewAttendance.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("الرجاء اختيار سجل للتعديل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int recordId = Convert.ToInt32(gridViewAttendance.GetRowCellValue(gridViewAttendance.FocusedRowHandle, "ID"));

                using (var manualForm = new ManualAttendanceForm(recordId))
                {
                    if (manualForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadAttendanceRecords();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تعديل سجل الحضور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حذف سجل حضور
        /// </summary>
        private void DeleteRecord()
        {
            try
            {
                if (gridViewAttendance.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("الرجاء اختيار سجل للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int recordId = Convert.ToInt32(gridViewAttendance.GetRowCellValue(gridViewAttendance.FocusedRowHandle, "ID"));
                string employeeName = gridViewAttendance.GetRowCellValue(gridViewAttendance.FocusedRowHandle, "EmployeeName").ToString();
                DateTime attendanceDate = Convert.ToDateTime(gridViewAttendance.GetRowCellValue(gridViewAttendance.FocusedRowHandle, "AttendanceDate"));

                if (XtraMessageBox.Show($"هل أنت متأكد من حذف سجل حضور الموظف '{employeeName}' بتاريخ {attendanceDate:dd/MM/yyyy}؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string query = "DELETE FROM AttendanceRecords WHERE ID = @ID";
                    var parameters = new SqlParameter[] { new SqlParameter("@ID", recordId) };
                    _dbContext.ExecuteNonQuery(query, parameters);

                    XtraMessageBox.Show("تم حذف السجل بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAttendanceRecords();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حذف سجل الحضور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// طباعة تقرير الحضور
        /// </summary>
        private void PrintReport()
        {
            try
            {
                // التحقق من وجود بيانات للطباعة
                if (gridViewAttendance.RowCount == 0)
                {
                    XtraMessageBox.Show("لا توجد بيانات للطباعة. الرجاء تحديد معايير بحث مختلفة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // إنشاء تقرير الحضور والانصراف
                using (var reportForm = new AttendanceReportForm(dateStart.DateTime, dateEnd.DateTime, Convert.ToInt32(lookupDepartment.EditValue)))
                {
                    reportForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء طباعة التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تصدير بيانات الحضور
        /// </summary>
        private void ExportData()
        {
            try
            {
                // التحقق من وجود بيانات للتصدير
                if (gridViewAttendance.RowCount == 0)
                {
                    XtraMessageBox.Show("لا توجد بيانات للتصدير. الرجاء تحديد معايير بحث مختلفة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // عرض حوار حفظ الملف
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "ملف إكسل (*.xlsx)|*.xlsx|ملف CSV (*.csv)|*.csv";
                saveDialog.Title = "تصدير البيانات";
                saveDialog.FileName = $"تقرير_الحضور_{DateTime.Now:yyyyMMdd}";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveDialog.FileName;
                    string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();

                    if (fileExtension == ".xlsx")
                    {
                        gridViewAttendance.ExportToXlsx(filePath);
                    }
                    else if (fileExtension == ".csv")
                    {
                        gridViewAttendance.ExportToCsv(filePath);
                    }

                    if (XtraMessageBox.Show("تم تصدير البيانات بنجاح. هل تريد فتح الملف؟", "تم", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تصدير البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث تغيير معايير البحث
        /// </summary>
        private void SearchCriteria_Changed(object sender, EventArgs e)
        {
            _startDate = dateStart.DateTime.Date;
            _endDate = dateEnd.DateTime.Date;
            LoadAttendanceRecords();
        }

        /// <summary>
        /// حدث النقر على زر البحث
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAttendanceRecords();
        }

        /// <summary>
        /// حدث النقر على زر إضافة
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddManualRecord();
        }

        /// <summary>
        /// حدث النقر على زر تعديل
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditRecord();
        }

        /// <summary>
        /// حدث النقر على زر حذف
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteRecord();
        }

        /// <summary>
        /// حدث النقر على زر طباعة
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintReport();
        }

        /// <summary>
        /// حدث النقر على زر تصدير
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportData();
        }

        /// <summary>
        /// حدث النقر المزدوج على الجدول
        /// </summary>
        private void gridViewAttendance_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridHitInfo info = gridViewAttendance.CalcHitInfo(ea.Location);
            if (info.InRow && btnEdit.Enabled)
            {
                EditRecord();
            }
        }
    }
}