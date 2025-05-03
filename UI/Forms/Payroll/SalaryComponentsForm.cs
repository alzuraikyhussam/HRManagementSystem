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
    public partial class SalaryComponentsForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly SalaryRepository _salaryRepository;
        private List<SalaryComponent> _components;
        private SalaryComponent _currentComponent;
        
        public SalaryComponentsForm()
        {
            InitializeComponent();
            _salaryRepository = new SalaryRepository();
        }
        
        private void SalaryComponentsForm_Load(object sender, EventArgs e)
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
            gridViewComponents.OptionsBehavior.Editable = false;
            gridViewComponents.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewComponents.OptionsView.ShowGroupPanel = false;
            gridViewComponents.OptionsView.ShowIndicator = false;
            
            // تعبئة قائمة أنواع العناصر
            lookUpEditComponentType.Properties.DataSource = new List<ComponentTypeItem>
            {
                new ComponentTypeItem { Type = "Basic", Name = "أساسي" },
                new ComponentTypeItem { Type = "Allowance", Name = "بدل" },
                new ComponentTypeItem { Type = "Deduction", Name = "خصم" },
                new ComponentTypeItem { Type = "Bonus", Name = "حافز" }
            };
            lookUpEditComponentType.Properties.DisplayMember = "Name";
            lookUpEditComponentType.Properties.ValueMember = "Type";
            
            // تعبئة قائمة أنواع الحساب
            lookUpEditCalculationType.Properties.DataSource = new List<CalculationTypeItem>
            {
                new CalculationTypeItem { Type = "Fixed", Name = "ثابت" },
                new CalculationTypeItem { Type = "Percentage", Name = "نسبة" },
                new CalculationTypeItem { Type = "Formula", Name = "معادلة" }
            };
            lookUpEditCalculationType.Properties.DisplayMember = "Name";
            lookUpEditCalculationType.Properties.ValueMember = "Type";
            
            // ضبط حالة عناصر التحكم
            ClearEditors();
            SetEditorsEnabled(false);
        }
        
        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckPermissions()
        {
            // التحقق من صلاحية إضافة عنصر
            barButtonItemAdd.Enabled = SessionManager.HasPermission("Payroll.AddSalaryComponent");
            
            // التحقق من صلاحية تعديل عنصر
            barButtonItemEdit.Enabled = SessionManager.HasPermission("Payroll.EditSalaryComponent");
            
            // التحقق من صلاحية حذف عنصر
            barButtonItemDelete.Enabled = SessionManager.HasPermission("Payroll.DeleteSalaryComponent");
        }
        
        /// <summary>
        /// تحميل البيانات
        /// </summary>
        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // الحصول على عناصر الرواتب
                _components = _salaryRepository.GetAllSalaryComponents();
                
                // عرض البيانات
                salaryComponentBindingSource.DataSource = _components;
                gridViewComponents.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل عناصر الرواتب: {ex.Message}", "خطأ", 
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
            textEditComponentCode.Text = string.Empty;
            textEditComponentName.Text = string.Empty;
            lookUpEditComponentType.EditValue = null;
            lookUpEditCalculationType.EditValue = null;
            textEditAmount.Text = "0";
            textEditFormula.Text = string.Empty;
            checkEditIsActive.Checked = true;
            checkEditIsDefault.Checked = false;
            checkEditIsTaxable.Checked = false;
            checkEditAffectsOvertime.Checked = false;
            memoEditDescription.Text = string.Empty;
            
            _currentComponent = null;
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
        /// عرض بيانات العنصر
        /// </summary>
        /// <param name="component">عنصر الراتب</param>
        private void DisplayComponentData(SalaryComponent component)
        {
            if (component == null)
            {
                ClearEditors();
                return;
            }
            
            _currentComponent = component;
            
            textEditComponentCode.Text = component.ComponentCode;
            textEditComponentName.Text = component.ComponentName;
            lookUpEditComponentType.EditValue = component.ComponentType;
            lookUpEditCalculationType.EditValue = component.CalculationType;
            textEditAmount.Text = component.Amount.ToString("0.00");
            textEditFormula.Text = component.Formula;
            checkEditIsActive.Checked = component.IsActive;
            checkEditIsDefault.Checked = component.IsDefault;
            checkEditIsTaxable.Checked = component.IsTaxable;
            checkEditAffectsOvertime.Checked = component.AffectsOvertime;
            memoEditDescription.Text = component.Description;
        }
        
        /// <summary>
        /// حفظ بيانات العنصر
        /// </summary>
        /// <returns>نتيجة الحفظ</returns>
        private bool SaveComponentData()
        {
            try
            {
                // التحقق من إدخال البيانات
                if (!ValidateInput())
                    return false;
                
                // إنشاء أو تحديث عنصر
                bool isNew = _currentComponent == null;
                
                if (isNew)
                {
                    _currentComponent = new SalaryComponent();
                }
                
                // تعبئة بيانات العنصر
                _currentComponent.ComponentCode = textEditComponentCode.Text;
                _currentComponent.ComponentName = textEditComponentName.Text;
                _currentComponent.ComponentType = lookUpEditComponentType.EditValue.ToString();
                _currentComponent.CalculationType = lookUpEditCalculationType.EditValue.ToString();
                _currentComponent.Amount = Convert.ToDecimal(textEditAmount.Text);
                _currentComponent.Formula = textEditFormula.Text;
                _currentComponent.IsActive = checkEditIsActive.Checked;
                _currentComponent.IsDefault = checkEditIsDefault.Checked;
                _currentComponent.IsTaxable = checkEditIsTaxable.Checked;
                _currentComponent.AffectsOvertime = checkEditAffectsOvertime.Checked;
                _currentComponent.Description = memoEditDescription.Text;
                
                // حفظ العنصر
                if (isNew)
                {
                    int id = _salaryRepository.AddSalaryComponent(_currentComponent);
                    if (id > 0)
                    {
                        _currentComponent.ID = id;
                        _components.Add(_currentComponent);
                        salaryComponentBindingSource.ResetBindings(false);
                        
                        XtraMessageBox.Show("تمت إضافة عنصر الراتب بنجاح.", "نجاح", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("فشلت عملية إضافة عنصر الراتب.", "خطأ", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    bool result = _salaryRepository.UpdateSalaryComponent(_currentComponent);
                    if (result)
                    {
                        gridViewComponents.RefreshData();
                        
                        XtraMessageBox.Show("تم تحديث عنصر الراتب بنجاح.", "نجاح", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("فشلت عملية تحديث عنصر الراتب.", "خطأ", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حفظ عنصر الراتب: {ex.Message}", "خطأ", 
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
            // التحقق من رمز العنصر
            if (string.IsNullOrWhiteSpace(textEditComponentCode.Text))
            {
                XtraMessageBox.Show("يرجى إدخال رمز العنصر.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditComponentCode.Focus();
                return false;
            }
            
            // التحقق من اسم العنصر
            if (string.IsNullOrWhiteSpace(textEditComponentName.Text))
            {
                XtraMessageBox.Show("يرجى إدخال اسم العنصر.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditComponentName.Focus();
                return false;
            }
            
            // التحقق من نوع العنصر
            if (lookUpEditComponentType.EditValue == null)
            {
                XtraMessageBox.Show("يرجى اختيار نوع العنصر.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lookUpEditComponentType.Focus();
                return false;
            }
            
            // التحقق من نوع الحساب
            if (lookUpEditCalculationType.EditValue == null)
            {
                XtraMessageBox.Show("يرجى اختيار نوع الحساب.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lookUpEditCalculationType.Focus();
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
            
            // التحقق من المعادلة إذا كان نوع الحساب معادلة
            if (lookUpEditCalculationType.EditValue.ToString() == "Formula" &&
                string.IsNullOrWhiteSpace(textEditFormula.Text))
            {
                XtraMessageBox.Show("يرجى إدخال المعادلة.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditFormula.Focus();
                return false;
            }
            
            // التحقق من عدم تكرار رمز العنصر
            if (_currentComponent == null || 
                _currentComponent.ComponentCode != textEditComponentCode.Text)
            {
                if (_components.Any(c => c.ComponentCode == textEditComponentCode.Text))
                {
                    XtraMessageBox.Show("رمز العنصر موجود مسبقاً، يرجى اختيار رمز آخر.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textEditComponentCode.Focus();
                    return false;
                }
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
                
                // التركيز على رمز العنصر
                textEditComponentCode.Focus();
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
                // الحصول على العنصر المحدد
                if (gridViewComponents.GetSelectedRows().Length > 0)
                {
                    int rowHandle = gridViewComponents.GetSelectedRows()[0];
                    if (rowHandle >= 0)
                    {
                        SalaryComponent component = gridViewComponents.GetRow(rowHandle) as SalaryComponent;
                        if (component != null)
                        {
                            // عرض بيانات العنصر
                            DisplayComponentData(component);
                            
                            // تفعيل عناصر التحرير
                            SetEditorsEnabled(true);
                            
                            // التركيز على اسم العنصر
                            textEditComponentName.Focus();
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
        /// حدث النقر على زر الحذف
        /// </summary>
        private void barButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على العنصر المحدد
                if (gridViewComponents.GetSelectedRows().Length > 0)
                {
                    int rowHandle = gridViewComponents.GetSelectedRows()[0];
                    if (rowHandle >= 0)
                    {
                        SalaryComponent component = gridViewComponents.GetRow(rowHandle) as SalaryComponent;
                        if (component != null)
                        {
                            // تأكيد الحذف
                            if (XtraMessageBox.Show($"هل أنت متأكد من حذف عنصر الراتب '{component.ComponentName}'؟", 
                                "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                // حذف العنصر
                                bool result = _salaryRepository.DeleteSalaryComponent(component.ID);
                                if (result)
                                {
                                    _components.Remove(component);
                                    salaryComponentBindingSource.ResetBindings(false);
                                    
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
        /// حدث النقر على زر الحفظ
        /// </summary>
        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // حفظ بيانات العنصر
                if (SaveComponentData())
                {
                    // تنظيف وتعطيل عناصر التحرير
                    ClearEditors();
                    SetEditorsEnabled(false);
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
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث تغيير نوع الحساب
        /// </summary>
        private void lookUpEditCalculationType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                // التحقق من القيمة المحددة
                if (lookUpEditCalculationType.EditValue == null)
                    return;
                
                string calculationType = lookUpEditCalculationType.EditValue.ToString();
                
                // تفعيل/تعطيل المعادلة حسب نوع الحساب
                textEditFormula.Enabled = calculationType == "Formula";
                
                // تفعيل/تعطيل المبلغ حسب نوع الحساب
                textEditAmount.Enabled = calculationType != "Formula";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر المزدوج على الجدول
        /// </summary>
        private void gridViewComponents_DoubleClick(object sender, EventArgs e)
        {
            if (SessionManager.HasPermission("Payroll.EditSalaryComponent"))
            {
                barButtonItemEdit_ItemClick(null, null);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر المساعدة
        /// </summary>
        private void barButtonItemHelp_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                XtraMessageBox.Show(
                    "عناصر الرواتب:\n\n" +
                    "- العنصر الأساسي: هو الراتب الأساسي للموظف.\n" +
                    "- البدل: مبلغ إضافي يضاف إلى الراتب الأساسي (مثل بدل السكن، بدل النقل).\n" +
                    "- الخصم: مبلغ يخصم من الراتب (مثل تأمينات اجتماعية، ضرائب).\n" +
                    "- الحافز: مبلغ إضافي يضاف حسب شروط معينة (مثل مكافأة أداء).\n\n" +
                    "أنواع الحساب:\n\n" +
                    "- ثابت: مبلغ ثابت لا يتغير.\n" +
                    "- نسبة: نسبة مئوية من قيمة أخرى (مثل 10% من الراتب الأساسي).\n" +
                    "- معادلة: صيغة رياضية تستخدم لحساب القيمة.\n\n" +
                    "المعادلات:\n\n" +
                    "- استخدم [Basic] للإشارة إلى الراتب الأساسي.\n" +
                    "- استخدم [ComponentName] للإشارة إلى عناصر الراتب الأخرى.\n" +
                    "- مثال: [Basic] * 0.1 + 500",
                    "مساعدة", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    /// عنصر نوع المكون
    /// </summary>
    public class ComponentTypeItem
    {
        /// <summary>
        /// النوع
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// الاسم
        /// </summary>
        public string Name { get; set; }
    }
    
    /// <summary>
    /// عنصر نوع الحساب
    /// </summary>
    public class CalculationTypeItem
    {
        /// <summary>
        /// النوع
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// الاسم
        /// </summary>
        public string Name { get; set; }
    }
}