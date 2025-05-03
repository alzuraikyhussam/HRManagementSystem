using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Deductions
{
    /// <summary>
    /// نموذج عرض سجلات الخصم
    /// </summary>
    public partial class DeductionsListForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly DeductionRepository _deductionRepository;
        private readonly EmployeeRepository _employeeRepository;
        private DateTime? _fromDate;
        private DateTime? _toDate;
        private int? _employeeId;
        private int? _departmentId;
        private string _status;
        private string _type;
        
        /// <summary>
        /// منشئ النموذج
        /// </summary>
        public DeductionsListForm()
        {
            InitializeComponent();
            
            _deductionRepository = new DeductionRepository();
            _employeeRepository = new EmployeeRepository();
            
            // تهيئة الفلاتر الافتراضية
            var today = DateTime.Today;
            _fromDate = new DateTime(today.Year, today.Month, 1);
            _toDate = today;
            _employeeId = null;
            _departmentId = null;
            _status = null;
            _type = null;
        }
        
        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void DeductionsListForm_Load(object sender, EventArgs e)
        {
            LoadFilters();
            ApplyFilters();
            UpdateUIByPermissions();
        }
        
        /// <summary>
        /// تحميل عناصر الفلاتر
        /// </summary>
        private void LoadFilters()
        {
            try
            {
                // تهيئة فلتر التاريخ
                dateEditFrom.DateTime = _fromDate ?? DateTime.Today.AddMonths(-1);
                dateEditTo.DateTime = _toDate ?? DateTime.Today;
                
                // تحميل الموظفين
                var employees = _employeeRepository.GetAllEmployees();
                lookUpEditEmployee.Properties.DataSource = employees;
                lookUpEditEmployee.Properties.DisplayMember = "FullName";
                lookUpEditEmployee.Properties.ValueMember = "ID";
                
                // تحميل الأقسام
                var departments = _employeeRepository.GetAllDepartments();
                lookUpEditDepartment.Properties.DataSource = departments;
                lookUpEditDepartment.Properties.DisplayMember = "Name";
                lookUpEditDepartment.Properties.ValueMember = "ID";
                
                // تهيئة الحالة
                comboBoxEditStatus.SelectedIndex = 0; // الكل
                
                // تهيئة نوع المخالفة
                comboBoxEditType.SelectedIndex = 0; // الكل
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل الفلاتر");
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات الفلاتر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تحديث حالة واجهة المستخدم حسب الصلاحيات
        /// </summary>
        private void UpdateUIByPermissions()
        {
            bool canApprove = SessionManager.HasPermission("Deductions.Approve");
            
            ribbonPageGroupApproval.Visible = canApprove;
            barButtonItemApprove.Visibility = canApprove ? BarItemVisibility.Always : BarItemVisibility.Never;
            barButtonItemReject.Visibility = canApprove ? BarItemVisibility.Always : BarItemVisibility.Never;
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
            _status = null;
            _type = null;
            
            // إعادة تهيئة عناصر التحكم
            dateEditFrom.DateTime = _fromDate ?? DateTime.Today.AddMonths(-1);
            dateEditTo.DateTime = _toDate ?? DateTime.Today;
            lookUpEditEmployee.EditValue = null;
            lookUpEditDepartment.EditValue = null;
            comboBoxEditStatus.SelectedIndex = 0;
            comboBoxEditType.SelectedIndex = 0;
            
            ApplyFilters();
        }
        
        /// <summary>
        /// جمع قيم الفلاتر من عناصر التحكم
        /// </summary>
        private void CollectFilterValues()
        {
            _fromDate = dateEditFrom.EditValue != null ? (DateTime?)dateEditFrom.DateTime.Date : null;
            _toDate = dateEditTo.EditValue != null ? (DateTime?)dateEditTo.DateTime.Date : null;
            _employeeId = (int?)lookUpEditEmployee.EditValue;
            _departmentId = (int?)lookUpEditDepartment.EditValue;
            _status = comboBoxEditStatus.Text == "الكل" ? null : comboBoxEditStatus.Text;
            _type = comboBoxEditType.Text == "الكل" ? null : comboBoxEditType.Text;
        }
        
        /// <summary>
        /// تطبيق الفلاتر واستعلام البيانات
        /// </summary>
        private void ApplyFilters()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                List<Deduction> deductions;
                
                // الحصول على الخصومات حسب الفلاتر
                if (_fromDate.HasValue && _toDate.HasValue)
                {
                    if (_employeeId.HasValue)
                    {
                        deductions = _deductionRepository.GetEmployeeDeductionsInPeriod(
                            _employeeId.Value, _fromDate.Value, _toDate.Value);
                    }
                    else
                    {
                        deductions = _deductionRepository.GetDeductionsInPeriod(_fromDate.Value, _toDate.Value);
                    }
                }
                else if (_employeeId.HasValue)
                {
                    deductions = _deductionRepository.GetEmployeeDeductions(_employeeId.Value);
                }
                else
                {
                    deductions = _deductionRepository.GetAllDeductions();
                }
                
                // تطبيق الفلاتر الإضافية
                if (_departmentId.HasValue)
                {
                    deductions = deductions.FindAll(d => 
                        d.DepartmentName != null && 
                        _employeeRepository.GetEmployeeById(d.EmployeeID)?.DepartmentID == _departmentId.Value);
                }
                
                if (!string.IsNullOrEmpty(_status))
                {
                    deductions = deductions.FindAll(d => d.Status == _status);
                }
                
                if (!string.IsNullOrEmpty(_type))
                {
                    deductions = deductions.FindAll(d => d.ViolationType == _type);
                }
                
                // عرض البيانات
                deductionBindingSource.DataSource = deductions;
                
                // تحديث حالة الشريط
                ribbonStatusBar.Description = $"عدد السجلات: {deductions.Count}";
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
        /// حدث النقر على زر إضافة خصم
        /// </summary>
        private void barButtonItemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var form = new DeductionForm())
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
            EditSelectedDeduction();
        }
        
        /// <summary>
        /// حدث النقر على زر حذف
        /// </summary>
        private void barButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteSelectedDeduction();
        }
        
        /// <summary>
        /// حدث النقر على زر الطباعة
        /// </summary>
        private void barButtonItemPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControlDeductions.ShowPrintPreview();
        }
        
        /// <summary>
        /// حدث النقر على زر التصدير
        /// </summary>
        private void barButtonItemExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "ملف إكسل (*.xlsx)|*.xlsx|ملف CSV (*.csv)|*.csv";
                saveDialog.FileName = "سجلات_الخصم";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string extension = System.IO.Path.GetExtension(saveDialog.FileName).ToLower();
                    
                    if (extension == ".xlsx")
                    {
                        gridControlDeductions.ExportToXlsx(saveDialog.FileName);
                    }
                    else if (extension == ".csv")
                    {
                        gridControlDeductions.ExportToCsv(saveDialog.FileName);
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
        /// حدث النقر على زر قواعد الخصم
        /// </summary>
        private void barButtonItemRules_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var form = new DeductionRulesListForm())
            {
                form.ShowDialog();
            }
        }
        
        /// <summary>
        /// حدث النقر على زر الاعتماد
        /// </summary>
        private void barButtonItemApprove_ItemClick(object sender, ItemClickEventArgs e)
        {
            ApproveSelectedDeduction();
        }
        
        /// <summary>
        /// حدث النقر على زر الرفض
        /// </summary>
        private void barButtonItemReject_ItemClick(object sender, ItemClickEventArgs e)
        {
            RejectSelectedDeduction();
        }
        
        /// <summary>
        /// تعديل الخصم المحدد
        /// </summary>
        private void EditSelectedDeduction()
        {
            var focusedRow = gridViewDeductions.GetFocusedRow() as Deduction;
            if (focusedRow == null)
            {
                XtraMessageBox.Show("الرجاء تحديد سجل أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // لا يمكن تعديل الخصومات المعتمدة أو المرفوضة
            if (focusedRow.Status != "Pending")
            {
                XtraMessageBox.Show("لا يمكن تعديل الخصومات المعتمدة أو المرفوضة أو الملغية", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            using (var form = new DeductionForm(focusedRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ApplyFilters();
                }
            }
        }
        
        /// <summary>
        /// حذف الخصم المحدد
        /// </summary>
        private void DeleteSelectedDeduction()
        {
            var focusedRow = gridViewDeductions.GetFocusedRow() as Deduction;
            if (focusedRow == null)
            {
                XtraMessageBox.Show("الرجاء تحديد سجل أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // لا يمكن حذف الخصومات المعتمدة
            if (focusedRow.Status == "Approved" && focusedRow.IsPayrollProcessed)
            {
                XtraMessageBox.Show("لا يمكن حذف الخصومات المعتمدة والمعالجة في الرواتب", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (XtraMessageBox.Show($"هل أنت متأكد من حذف خصم الموظف {focusedRow.EmployeeName} بتاريخ {focusedRow.DeductionDate:yyyy-MM-dd}؟", 
                "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool result = _deductionRepository.DeleteDeduction(focusedRow.ID);
                    
                    if (result)
                    {
                        ApplyFilters();
                        XtraMessageBox.Show("تم حذف الخصم بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("فشل في حذف الخصم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"فشل في حذف الخصم {focusedRow.ID}");
                    XtraMessageBox.Show("حدث خطأ أثناء حذف الخصم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        /// <summary>
        /// اعتماد الخصم المحدد
        /// </summary>
        private void ApproveSelectedDeduction()
        {
            var focusedRow = gridViewDeductions.GetFocusedRow() as Deduction;
            if (focusedRow == null)
            {
                XtraMessageBox.Show("الرجاء تحديد سجل أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // تحقق من حالة الخصم
            if (focusedRow.Status != "Pending")
            {
                XtraMessageBox.Show("لا يمكن اعتماد خصم تمت معالجته من قبل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (XtraMessageBox.Show($"هل أنت متأكد من اعتماد خصم الموظف {focusedRow.EmployeeName} بتاريخ {focusedRow.DeductionDate:yyyy-MM-dd}؟", 
                "تأكيد الاعتماد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool result = _deductionRepository.ApproveDeduction(focusedRow.ID, SessionManager.CurrentUser.ID);
                    
                    if (result)
                    {
                        ApplyFilters();
                        XtraMessageBox.Show("تم اعتماد الخصم بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("فشل في اعتماد الخصم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"فشل في اعتماد الخصم {focusedRow.ID}");
                    XtraMessageBox.Show("حدث خطأ أثناء اعتماد الخصم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        /// <summary>
        /// رفض الخصم المحدد
        /// </summary>
        private void RejectSelectedDeduction()
        {
            var focusedRow = gridViewDeductions.GetFocusedRow() as Deduction;
            if (focusedRow == null)
            {
                XtraMessageBox.Show("الرجاء تحديد سجل أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // تحقق من حالة الخصم
            if (focusedRow.Status != "Pending")
            {
                XtraMessageBox.Show("لا يمكن رفض خصم تمت معالجته من قبل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (XtraMessageBox.Show($"هل أنت متأكد من رفض خصم الموظف {focusedRow.EmployeeName} بتاريخ {focusedRow.DeductionDate:yyyy-MM-dd}؟", 
                "تأكيد الرفض", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool result = _deductionRepository.RejectDeduction(focusedRow.ID, SessionManager.CurrentUser.ID);
                    
                    if (result)
                    {
                        ApplyFilters();
                        XtraMessageBox.Show("تم رفض الخصم بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("فشل في رفض الخصم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"فشل في رفض الخصم {focusedRow.ID}");
                    XtraMessageBox.Show("حدث خطأ أثناء رفض الخصم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        /// <summary>
        /// حدث النقر على زر التعديل في الجدول
        /// </summary>
        private void repositoryItemButtonEditItem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            EditSelectedDeduction();
        }
        
        /// <summary>
        /// حدث النقر على زر الحذف في الجدول
        /// </summary>
        private void repositoryItemButtonEditDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DeleteSelectedDeduction();
        }
    }
}