using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Attendance
{
    /// <summary>
    /// نموذج عرض سجلات الحضور والانصراف
    /// </summary>
    public partial class AttendanceRecordsForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly AttendanceRepository _attendanceRepository;
        private readonly EmployeeRepository _employeeRepository;
        private DateTime _fromDate;
        private DateTime _toDate;
        private int? _employeeId;
        private int? _departmentId;
        private string _status;
        private bool _manualOnly;
        
        /// <summary>
        /// منشئ النموذج
        /// </summary>
        public AttendanceRecordsForm()
        {
            InitializeComponent();
            
            _attendanceRepository = new AttendanceRepository();
            _employeeRepository = new EmployeeRepository();
            
            // تهيئة الفلاتر الافتراضية
            var today = DateTime.Today;
            _fromDate = new DateTime(today.Year, today.Month, 1);
            _toDate = today;
            _employeeId = null;
            _departmentId = null;
            _status = "الكل";
            _manualOnly = false;
        }
        
        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void AttendanceRecordsForm_Load(object sender, EventArgs e)
        {
            LoadFilters();
            ApplyFilters();
        }
        
        /// <summary>
        /// تحميل عناصر الفلاتر
        /// </summary>
        private void LoadFilters()
        {
            try
            {
                // تهيئة فلتر التاريخ
                dateEditFrom.DateTime = _fromDate;
                dateEditTo.DateTime = _toDate;
                
                // تهيئة فلتر الموظفين
                var employees = _employeeRepository.GetAllEmployees();
                lookUpEditEmployee.Properties.DataSource = employees;
                lookUpEditEmployee.Properties.DisplayMember = "FullName";
                lookUpEditEmployee.Properties.ValueMember = "ID";
                
                // تهيئة فلتر الأقسام
                var departments = _employeeRepository.GetAllDepartments();
                lookUpEditDepartment.Properties.DataSource = departments;
                lookUpEditDepartment.Properties.DisplayMember = "Name";
                lookUpEditDepartment.Properties.ValueMember = "ID";
                
                // تهيئة فلتر الحالة
                comboBoxEditStatus.SelectedIndex = 0; // الكل
                
                // تهيئة فلتر الإدخال اليدوي
                checkEditManualOnly.Checked = _manualOnly;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل الفلاتر");
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات الفلاتر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر تطبيق الفلتر
        /// </summary>
        private void simpleButtonApplyFilter_Click(object sender, EventArgs e)
        {
            CollectFilterValues();
            ApplyFilters();
        }
        
        /// <summary>
        /// حدث النقر على زر مسح الفلتر
        /// </summary>
        private void simpleButtonClearFilter_Click(object sender, EventArgs e)
        {
            // إعادة تهيئة الفلاتر الافتراضية
            var today = DateTime.Today;
            _fromDate = new DateTime(today.Year, today.Month, 1);
            _toDate = today;
            _employeeId = null;
            _departmentId = null;
            _status = "الكل";
            _manualOnly = false;
            
            // إعادة تهيئة عناصر التحكم
            dateEditFrom.DateTime = _fromDate;
            dateEditTo.DateTime = _toDate;
            lookUpEditEmployee.EditValue = null;
            lookUpEditDepartment.EditValue = null;
            comboBoxEditStatus.SelectedIndex = 0;
            checkEditManualOnly.Checked = false;
            
            ApplyFilters();
        }
        
        /// <summary>
        /// جمع قيم الفلاتر من عناصر التحكم
        /// </summary>
        private void CollectFilterValues()
        {
            _fromDate = dateEditFrom.DateTime.Date;
            _toDate = dateEditTo.DateTime.Date;
            _employeeId = (int?)lookUpEditEmployee.EditValue;
            _departmentId = (int?)lookUpEditDepartment.EditValue;
            _status = comboBoxEditStatus.Text == "الكل" ? null : comboBoxEditStatus.Text;
            _manualOnly = checkEditManualOnly.Checked;
        }
        
        /// <summary>
        /// تطبيق الفلاتر واستعلام البيانات
        /// </summary>
        private void ApplyFilters()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // الحصول على سجلات الحضور حسب الفلاتر
                var records = _attendanceRepository.GetAttendanceRecords(_fromDate, _toDate);
                
                // تطبيق الفلاتر الإضافية
                if (_employeeId.HasValue)
                {
                    records = records.Where(r => r.EmployeeID == _employeeId.Value).ToList();
                }
                
                if (_departmentId.HasValue)
                {
                    records = records.Where(r => r.DepartmentName != null && 
                        _employeeRepository.GetEmployeeById(r.EmployeeID)?.DepartmentID == _departmentId.Value).ToList();
                }
                
                if (!string.IsNullOrEmpty(_status))
                {
                    records = records.Where(r => r.Status == _status).ToList();
                }
                
                if (_manualOnly)
                {
                    records = records.Where(r => r.IsManualEntry).ToList();
                }
                
                // عرض البيانات
                attendanceRecordBindingSource.DataSource = records;
                
                // تحديث حالة الشريط
                ribbonStatusBar.Description = $"عدد السجلات: {records.Count}";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تطبيق الفلاتر");
                XtraMessageBox.Show("حدث خطأ أثناء تحميل البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// حدث النقر على زر التحديث
        /// </summary>
        private void barButtonItemRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            ApplyFilters();
        }
        
        /// <summary>
        /// حدث النقر على زر إضافة سجل
        /// </summary>
        private void barButtonItemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var form = new AttendanceRecordForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ApplyFilters();
                }
            }
        }
        
        /// <summary>
        /// حدث النقر على زر تعديل
        /// </summary>
        private void barButtonItemEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditSelectedRecord();
        }
        
        /// <summary>
        /// حدث النقر على زر حذف
        /// </summary>
        private void barButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteSelectedRecord();
        }
        
        /// <summary>
        /// حدث النقر على زر الطباعة
        /// </summary>
        private void barButtonItemPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControlAttendance.ShowPrintPreview();
        }
        
        /// <summary>
        /// حدث النقر على زر التصدير
        /// </summary>
        private void barButtonItemExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "ملف إكسل (*.xlsx)|*.xlsx|ملف CSV (*.csv)|*.csv";
                saveDialog.FileName = "سجلات_الحضور_والانصراف";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string extension = System.IO.Path.GetExtension(saveDialog.FileName).ToLower();
                    
                    if (extension == ".xlsx")
                    {
                        gridControlAttendance.ExportToXlsx(saveDialog.FileName);
                    }
                    else if (extension == ".csv")
                    {
                        gridControlAttendance.ExportToCsv(saveDialog.FileName);
                    }
                    
                    if (XtraMessageBox.Show("تم تصدير البيانات بنجاح. هل تريد فتح الملف؟", "تصدير", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
            }
        }
        
        /// <summary>
        /// حدث النقر على زر الاستيراد
        /// </summary>
        private void barButtonItemImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMessageBox.Show("سيتم تنفيذ هذه الخاصية لاحقاً", "قيد التطوير", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        /// <summary>
        /// حدث النقر على زر أجهزة البصمة
        /// </summary>
        private void barButtonItemDevices_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var form = new BiometricDevicesForm())
            {
                form.ShowDialog();
                // بعد إغلاق نموذج أجهزة البصمة، قد تكون هناك سجلات جديدة، لذا نحدث البيانات
                ApplyFilters();
            }
        }
        
        /// <summary>
        /// تعديل السجل المحدد
        /// </summary>
        private void EditSelectedRecord()
        {
            var focusedRow = gridViewAttendance.GetFocusedRow() as AttendanceRecord;
            if (focusedRow == null)
            {
                XtraMessageBox.Show("الرجاء تحديد سجل أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            using (var form = new AttendanceRecordForm(focusedRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ApplyFilters();
                }
            }
        }
        
        /// <summary>
        /// حذف السجل المحدد
        /// </summary>
        private void DeleteSelectedRecord()
        {
            var focusedRow = gridViewAttendance.GetFocusedRow() as AttendanceRecord;
            if (focusedRow == null)
            {
                XtraMessageBox.Show("الرجاء تحديد سجل أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (XtraMessageBox.Show($"هل أنت متأكد من حذف سجل الحضور للموظف {focusedRow.EmployeeName} بتاريخ {focusedRow.AttendanceDate:yyyy-MM-dd}؟", 
                "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool result = _attendanceRepository.DeleteAttendanceRecord(focusedRow.ID);
                    
                    if (result)
                    {
                        ApplyFilters();
                        XtraMessageBox.Show("تم حذف السجل بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("فشل في حذف السجل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"فشل في حذف سجل الحضور {focusedRow.ID}");
                    XtraMessageBox.Show("حدث خطأ أثناء حذف السجل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        /// <summary>
        /// حدث النقر على زر التعديل في الجدول
        /// </summary>
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            EditSelectedRecord();
        }
        
        /// <summary>
        /// حدث النقر على زر الحذف في الجدول
        /// </summary>
        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DeleteSelectedRecord();
        }
    }
}