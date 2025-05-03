using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Payroll
{
    public partial class EmployeeSalariesForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly SalaryRepository _salaryRepository;
        private readonly EmployeeRepository _employeeRepository;
        private List<EmployeeSalary> _salaries;
        private List<SalaryComponent> _components;
        private List<Employee> _employees;
        private EmployeeSalary _currentSalary;
        
        public EmployeeSalariesForm()
        {
            InitializeComponent();
            _salaryRepository = new SalaryRepository();
            _employeeRepository = new EmployeeRepository();
        }
        
        private void EmployeeSalariesForm_Load(object sender, EventArgs e)
        {
            try
            {
                // تهيئة عناصر التحكم
                InitializeControls();
                
                // تحميل البيانات
                LoadData();
                
                // التحقق من الصلاحيات
                CheckPermissions();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// تهيئة عناصر التحكم
        /// </summary>
        private void InitializeControls()
        {
            // ضبط إعدادات الجدول
            gridViewSalaries.OptionsBehavior.Editable = false;
            gridViewSalaries.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewSalaries.OptionsView.ShowGroupPanel = false;
            gridViewSalaries.OptionsView.ShowIndicator = false;
            
            // ضبط حالة عناصر التحكم
            ClearEditors();
            SetEditorsEnabled(false);
        }
        
        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckPermissions()
        {
            // التحقق من صلاحية إضافة راتب
            barButtonItemAdd.Enabled = SessionManager.HasPermission("Payroll.AddEmployeeSalary");
            
            // التحقق من صلاحية تعديل راتب
            barButtonItemEdit.Enabled = SessionManager.HasPermission("Payroll.EditEmployeeSalary");
            
            // التحقق من صلاحية حذف راتب
            barButtonItemDelete.Enabled = SessionManager.HasPermission("Payroll.DeleteEmployeeSalary");
        }
        
        /// <summary>
        /// تحميل البيانات
        /// </summary>
        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // الحصول على الموظفين
                _employees = _employeeRepository.GetAllEmployees();
                
                // الحصول على عناصر الرواتب
                _components = _salaryRepository.GetAllSalaryComponents();
                
                // الحصول على رواتب الموظفين
                _salaries = _salaryRepository.GetAllEmployeeSalaries();
                
                // عرض بيانات رواتب الموظفين
                employeeSalaryBindingSource.DataSource = _salaries;
                gridViewSalaries.RefreshData();
                
                // تحديث قائمة الموظفين
                searchLookUpEditEmployee.Properties.DataSource = _employees;
                searchLookUpEditEmployee.Properties.DisplayMember = "FullName";
                searchLookUpEditEmployee.Properties.ValueMember = "ID";
                
                // تحديث قائمة عناصر الرواتب
                gridControlComponents.DataSource = null;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// تنظيف عناصر التحرير
        /// </summary>
        private void ClearEditors()
        {
            searchLookUpEditEmployee.EditValue = null;
            dateEditEffectiveDate.DateTime = DateTime.Now.Date;
            
            _currentSalary = null;
        }
        
        /// <summary>
        /// ضبط حالة عناصر التحرير
        /// </summary>
        /// <param name="enabled">حالة التفعيل</param>
        private void SetEditorsEnabled(bool enabled)
        {
            groupControlDetails.Enabled = enabled;
            simpleButtonSave.Enabled = enabled;
            simpleButtonCancel.Enabled = enabled;
        }
        
        /// <summary>
        /// عرض بيانات راتب الموظف
        /// </summary>
        /// <param name="salary">راتب الموظف</param>
        private void DisplaySalaryData(EmployeeSalary salary)
        {
            if (salary == null)
            {
                ClearEditors();
                return;
            }
            
            _currentSalary = salary;
            
            // الموظف
            searchLookUpEditEmployee.EditValue = salary.EmployeeID;
            
            // تاريخ السريان
            dateEditEffectiveDate.DateTime = salary.EffectiveDate;
            
            // عناصر الراتب
            LoadEmployeeSalaryComponents(salary);
        }
        
        /// <summary>
        /// تحميل عناصر راتب الموظف
        /// </summary>
        /// <param name="salary">راتب الموظف</param>
        private void LoadEmployeeSalaryComponents(EmployeeSalary salary)
        {
            try
            {
                // الحصول على عناصر راتب الموظف
                List<EmployeeSalaryComponent> components = _salaryRepository.GetEmployeeSalaryComponents(salary.ID);
                
                // عرض البيانات
                gridControlComponents.DataSource = components;
                
                // حساب الإجمالي
                decimal totalSalary = components.Sum(c => c.Amount);
                labelControlTotalAmount.Text = $"إجمالي الراتب: {totalSalary.ToString("N2")}";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل عناصر الراتب: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// إضافة أو تعديل عنصر راتب
        /// </summary>
        private void AddOrEditSalaryComponent()
        {
            try
            {
                // التأكد من وجود راتب موظف
                if (_currentSalary == null || _currentSalary.ID <= 0)
                {
                    XtraMessageBox.Show("يجب حفظ راتب الموظف أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // فتح نموذج إضافة/تعديل عنصر راتب
                EmployeeSalaryComponentForm componentForm = new EmployeeSalaryComponentForm(_currentSalary, _components);
                if (componentForm.ShowDialog() == DialogResult.OK)
                {
                    // تحديث عناصر الراتب
                    LoadEmployeeSalaryComponents(_currentSalary);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حذف عنصر راتب
        /// </summary>
        private void DeleteSalaryComponent()
        {
            try
            {
                // التأكد من وجود راتب موظف
                if (_currentSalary == null || _currentSalary.ID <= 0)
                {
                    XtraMessageBox.Show("يجب حفظ راتب الموظف أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // الحصول على العنصر المحدد
                if (gridViewComponents.GetSelectedRows().Length > 0)
                {
                    int rowHandle = gridViewComponents.GetSelectedRows()[0];
                    if (rowHandle >= 0)
                    {
                        EmployeeSalaryComponent component = gridViewComponents.GetRow(rowHandle) as EmployeeSalaryComponent;
                        if (component != null)
                        {
                            // تأكيد الحذف
                            if (XtraMessageBox.Show($"هل أنت متأكد من حذف عنصر الراتب '{component.ComponentName}'؟", 
                                "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                // حذف العنصر
                                bool result = _salaryRepository.DeleteEmployeeSalaryComponent(component.ID);
                                if (result)
                                {
                                    // تحديث العناصر
                                    LoadEmployeeSalaryComponents(_currentSalary);
                                    
                                    XtraMessageBox.Show("تم حذف عنصر الراتب بنجاح.", "نجاح", 
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    XtraMessageBox.Show("فشلت عملية حذف عنصر الراتب.", "خطأ", 
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("يرجى تحديد عنصر أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حفظ بيانات الراتب
        /// </summary>
        /// <returns>نتيجة الحفظ</returns>
        private bool SaveSalaryData()
        {
            try
            {
                // التحقق من إدخال البيانات
                if (!ValidateInput())
                    return false;
                
                // إنشاء أو تحديث راتب
                bool isNew = _currentSalary == null || _currentSalary.ID <= 0;
                
                if (isNew)
                {
                    _currentSalary = new EmployeeSalary();
                }
                
                // تعبئة بيانات الراتب
                _currentSalary.EmployeeID = Convert.ToInt32(searchLookUpEditEmployee.EditValue);
                _currentSalary.EffectiveDate = dateEditEffectiveDate.DateTime;
                _currentSalary.IsActive = true;
                
                // حفظ الراتب
                if (isNew)
                {
                    int id = _salaryRepository.AddEmployeeSalary(_currentSalary);
                    if (id > 0)
                    {
                        _currentSalary.ID = id;
                        _salaries.Add(_currentSalary);
                        employeeSalaryBindingSource.ResetBindings(false);
                        
                        XtraMessageBox.Show("تمت إضافة راتب الموظف بنجاح.", "نجاح", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("فشلت عملية إضافة راتب الموظف.", "خطأ", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    bool result = _salaryRepository.UpdateEmployeeSalary(_currentSalary);
                    if (result)
                    {
                        gridViewSalaries.RefreshData();
                        
                        XtraMessageBox.Show("تم تحديث راتب الموظف بنجاح.", "نجاح", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("فشلت عملية تحديث راتب الموظف.", "خطأ", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حفظ راتب الموظف: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
                return false;
            }
        }
        
        /// <summary>
        /// التحقق من صحة البيانات المدخلة
        /// </summary>
        /// <returns>نتيجة التحقق</returns>
        private bool ValidateInput()
        {
            // التحقق من الموظف
            if (searchLookUpEditEmployee.EditValue == null)
            {
                XtraMessageBox.Show("يرجى اختيار الموظف.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                searchLookUpEditEmployee.Focus();
                return false;
            }
            
            // التحقق من التاريخ
            if (dateEditEffectiveDate.EditValue == null)
            {
                XtraMessageBox.Show("يرجى إدخال تاريخ السريان.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateEditEffectiveDate.Focus();
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// حدث النقر على زر التحديث
        /// </summary>
        private void barButtonItemRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحديث البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر الإضافة
        /// </summary>
        private void barButtonItemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // تنظيف عناصر التحرير
                ClearEditors();
                
                // تفعيل عناصر التحرير
                SetEditorsEnabled(true);
                
                // التركيز على الموظف
                searchLookUpEditEmployee.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر التعديل
        /// </summary>
        private void barButtonItemEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على الراتب المحدد
                if (gridViewSalaries.GetSelectedRows().Length > 0)
                {
                    int rowHandle = gridViewSalaries.GetSelectedRows()[0];
                    if (rowHandle >= 0)
                    {
                        EmployeeSalary salary = gridViewSalaries.GetRow(rowHandle) as EmployeeSalary;
                        if (salary != null)
                        {
                            // عرض بيانات الراتب
                            DisplaySalaryData(salary);
                            
                            // تفعيل عناصر التحرير
                            SetEditorsEnabled(true);
                            
                            // التركيز على الموظف
                            searchLookUpEditEmployee.Focus();
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("يرجى تحديد راتب أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر الحذف
        /// </summary>
        private void barButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على الراتب المحدد
                if (gridViewSalaries.GetSelectedRows().Length > 0)
                {
                    int rowHandle = gridViewSalaries.GetSelectedRows()[0];
                    if (rowHandle >= 0)
                    {
                        EmployeeSalary salary = gridViewSalaries.GetRow(rowHandle) as EmployeeSalary;
                        if (salary != null)
                        {
                            // تأكيد الحذف
                            if (XtraMessageBox.Show($"هل أنت متأكد من حذف راتب الموظف '{salary.EmployeeName}'؟", 
                                "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                // حذف الراتب
                                bool result = _salaryRepository.DeleteEmployeeSalary(salary.ID);
                                if (result)
                                {
                                    _salaries.Remove(salary);
                                    employeeSalaryBindingSource.ResetBindings(false);
                                    
                                    XtraMessageBox.Show("تم حذف راتب الموظف بنجاح.", "نجاح", 
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    XtraMessageBox.Show("فشلت عملية حذف راتب الموظف.", "خطأ", 
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("يرجى تحديد راتب أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر الحفظ
        /// </summary>
        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // حفظ بيانات الراتب
                if (SaveSalaryData())
                {
                    // لا نقوم بتنظيف وتعطيل عناصر التحرير لكي يتمكن المستخدم من إضافة عناصر الراتب
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء الحفظ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر الإلغاء
        /// </summary>
        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // تنظيف وتعطيل عناصر التحرير
                ClearEditors();
                SetEditorsEnabled(false);
                
                // مسح عناصر الراتب
                gridControlComponents.DataSource = null;
                labelControlTotalAmount.Text = "إجمالي الراتب: 0.00";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر إضافة عنصر
        /// </summary>
        private void simpleButtonAddComponent_Click(object sender, EventArgs e)
        {
            try
            {
                AddOrEditSalaryComponent();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر حذف عنصر
        /// </summary>
        private void simpleButtonDeleteComponent_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteSalaryComponent();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر المزدوج على جدول الرواتب
        /// </summary>
        private void gridViewSalaries_DoubleClick(object sender, EventArgs e)
        {
            if (SessionManager.HasPermission("Payroll.EditEmployeeSalary"))
            {
                barButtonItemEdit_ItemClick(null, null);
            }
        }
        
        /// <summary>
        /// حدث النقر المزدوج على جدول عناصر الراتب
        /// </summary>
        private void gridViewComponents_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // الحصول على العنصر المحدد
                if (gridViewComponents.GetSelectedRows().Length > 0)
                {
                    int rowHandle = gridViewComponents.GetSelectedRows()[0];
                    if (rowHandle >= 0)
                    {
                        EmployeeSalaryComponent component = gridViewComponents.GetRow(rowHandle) as EmployeeSalaryComponent;
                        if (component != null)
                        {
                            // فتح نموذج تعديل عنصر الراتب
                            EmployeeSalaryComponentForm componentForm = new EmployeeSalaryComponentForm(_currentSalary, _components, component);
                            if (componentForm.ShowDialog() == DialogResult.OK)
                            {
                                // تحديث عناصر الراتب
                                LoadEmployeeSalaryComponents(_currentSalary);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
    }
    
    /// <summary>
    /// نموذج إضافة/تعديل عنصر راتب موظف
    /// </summary>
    public class EmployeeSalaryComponentForm : XtraForm
    {
        private EmployeeSalary _salary;
        private List<SalaryComponent> _components;
        private EmployeeSalaryComponent _component;
        private bool _isEdit;
        
        private LabelControl labelControl1;
        private LookUpEdit lookUpEditComponent;
        private LabelControl labelControl2;
        private TextEdit textEditAmount;
        private LabelControl labelControl3;
        private MemoEdit memoEditNotes;
        private SimpleButton simpleButtonOK;
        private SimpleButton simpleButtonCancel;
        
        /// <summary>
        /// إنشاء نموذج جديد
        /// </summary>
        /// <param name="salary">راتب الموظف</param>
        /// <param name="components">عناصر الرواتب</param>
        public EmployeeSalaryComponentForm(EmployeeSalary salary, List<SalaryComponent> components)
            : this(salary, components, null)
        {
        }
        
        /// <summary>
        /// إنشاء نموذج تعديل
        /// </summary>
        /// <param name="salary">راتب الموظف</param>
        /// <param name="components">عناصر الرواتب</param>
        /// <param name="component">عنصر الراتب</param>
        public EmployeeSalaryComponentForm(EmployeeSalary salary, List<SalaryComponent> components, EmployeeSalaryComponent component)
        {
            InitializeComponent();
            
            _salary = salary;
            _components = components;
            _component = component;
            _isEdit = component != null;
            
            // إعداد النموذج
            this.Text = _isEdit ? "تعديل عنصر راتب" : "إضافة عنصر راتب";
            
            // تعبئة قائمة العناصر
            lookUpEditComponent.Properties.DataSource = _components;
            lookUpEditComponent.Properties.DisplayMember = "ComponentName";
            lookUpEditComponent.Properties.ValueMember = "ID";
            
            // عرض بيانات العنصر إذا كان تعديل
            if (_isEdit)
            {
                lookUpEditComponent.EditValue = _component.ComponentID;
                lookUpEditComponent.Enabled = false;
                textEditAmount.Text = _component.Amount.ToString("0.00");
                memoEditNotes.Text = _component.Notes;
            }
            else
            {
                textEditAmount.Text = "0.00";
            }
        }
        
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditComponent = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEditAmount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.memoEditNotes = new DevExpress.XtraEditors.MemoEdit();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditComponent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditNotes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(341, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "عنصر الراتب:";
            // 
            // lookUpEditComponent
            // 
            this.lookUpEditComponent.Location = new System.Drawing.Point(12, 12);
            this.lookUpEditComponent.Name = "lookUpEditComponent";
            this.lookUpEditComponent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditComponent.Properties.NullText = "";
            this.lookUpEditComponent.Size = new System.Drawing.Size(323, 20);
            this.lookUpEditComponent.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(341, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(33, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "المبلغ:";
            // 
            // textEditAmount
            // 
            this.textEditAmount.Location = new System.Drawing.Point(12, 38);
            this.textEditAmount.Name = "textEditAmount";
            this.textEditAmount.Properties.DisplayFormat.FormatString = "N2";
            this.textEditAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEditAmount.Properties.EditFormat.FormatString = "N2";
            this.textEditAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEditAmount.Properties.Mask.EditMask = "N2";
            this.textEditAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEditAmount.Size = new System.Drawing.Size(323, 20);
            this.textEditAmount.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(341, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "ملاحظات:";
            // 
            // memoEditNotes
            // 
            this.memoEditNotes.Location = new System.Drawing.Point(12, 64);
            this.memoEditNotes.Name = "memoEditNotes";
            this.memoEditNotes.Size = new System.Drawing.Size(323, 60);
            this.memoEditNotes.TabIndex = 5;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(12, 130);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 6;
            this.simpleButtonOK.Text = "موافق";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonCancel.Location = new System.Drawing.Point(93, 130);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCancel.TabIndex = 7;
            this.simpleButtonCancel.Text = "إلغاء";
            // 
            // EmployeeSalaryComponentForm
            // 
            this.AcceptButton = this.simpleButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButtonCancel;
            this.ClientSize = new System.Drawing.Size(414, 165);
            this.Controls.Add(this.simpleButtonCancel);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.memoEditNotes);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.textEditAmount);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lookUpEditComponent);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeSalaryComponentForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "عنصر راتب";
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditComponent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditNotes.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من إدخال البيانات
                if (!ValidateInput())
                    return;
                
                // إنشاء عنصر راتب
                if (_component == null)
                {
                    _component = new EmployeeSalaryComponent();
                }
                
                // تعبئة بيانات العنصر
                _component.SalaryID = _salary.ID;
                _component.ComponentID = Convert.ToInt32(lookUpEditComponent.EditValue);
                _component.ComponentName = lookUpEditComponent.Text;
                _component.Amount = Convert.ToDecimal(textEditAmount.Text);
                _component.Notes = memoEditNotes.Text;
                
                // حفظ العنصر
                SalaryRepository repo = new SalaryRepository();
                bool result = false;
                
                if (_isEdit)
                {
                    result = repo.UpdateEmployeeSalaryComponent(_component);
                }
                else
                {
                    int id = repo.AddEmployeeSalaryComponent(_component);
                    result = id > 0;
                    if (result)
                    {
                        _component.ID = id;
                    }
                }
                
                if (result)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show("فشلت عملية حفظ عنصر الراتب.", "خطأ", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حفظ عنصر الراتب: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        private bool ValidateInput()
        {
            // التحقق من العنصر
            if (lookUpEditComponent.EditValue == null)
            {
                XtraMessageBox.Show("يرجى اختيار عنصر الراتب.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lookUpEditComponent.Focus();
                return false;
            }
            
            // التحقق من المبلغ
            if (string.IsNullOrWhiteSpace(textEditAmount.Text))
            {
                XtraMessageBox.Show("يرجى إدخال المبلغ.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditAmount.Focus();
                return false;
            }
            
            // التحقق من عدم تكرار العنصر
            if (!_isEdit)
            {
                int componentID = Convert.ToInt32(lookUpEditComponent.EditValue);
                SalaryRepository repo = new SalaryRepository();
                List<EmployeeSalaryComponent> components = repo.GetEmployeeSalaryComponents(_salary.ID);
                
                if (components.Any(c => c.ComponentID == componentID))
                {
                    XtraMessageBox.Show("عنصر الراتب موجود مسبقاً لهذا الموظف.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lookUpEditComponent.Focus();
                    return false;
                }
            }
            
            return true;
        }
    }
}