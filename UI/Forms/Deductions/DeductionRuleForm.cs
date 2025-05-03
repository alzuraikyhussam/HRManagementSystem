using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Deductions
{
    /// <summary>
    /// نموذج إضافة/تعديل قاعدة خصم
    /// </summary>
    public partial class DeductionRuleForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly DeductionRepository _deductionRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly DeductionRule _rule;
        private readonly bool _isEditMode;
        
        /// <summary>
        /// منشئ النموذج لإضافة قاعدة جديدة
        /// </summary>
        public DeductionRuleForm()
        {
            InitializeComponent();
            
            _deductionRepository = new DeductionRepository();
            _employeeRepository = new EmployeeRepository();
            
            _rule = new DeductionRule
            {
                IsActive = true,
                ActivationDate = DateTime.Today,
                AppliesTo = "All",
                CreatedAt = DateTime.Now
            };
            
            _isEditMode = false;
        }
        
        /// <summary>
        /// منشئ النموذج لتعديل قاعدة موجودة
        /// </summary>
        /// <param name="rule">قاعدة الخصم المراد تعديلها</param>
        public DeductionRuleForm(DeductionRule rule)
        {
            InitializeComponent();
            
            _deductionRepository = new DeductionRepository();
            _employeeRepository = new EmployeeRepository();
            
            _rule = rule;
            _isEditMode = true;
        }
        
        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void DeductionRuleForm_Load(object sender, EventArgs e)
        {
            LoadLookupData();
            
            if (_isEditMode)
            {
                Text = "تعديل قاعدة خصم";
                LoadRuleData();
            }
            else
            {
                Text = "إضافة قاعدة خصم";
                InitializeNewRule();
            }
            
            UpdateControlsVisibility();
        }
        
        /// <summary>
        /// تحميل بيانات القوائم المنسدلة
        /// </summary>
        private void LoadLookupData()
        {
            try
            {
                // تحميل الأقسام
                var departments = _employeeRepository.GetAllDepartments();
                lookUpEditDepartment.Properties.DataSource = departments;
                
                // تحميل المناصب
                var positions = _employeeRepository.GetAllPositions();
                lookUpEditPosition.Properties.DataSource = positions;
                
                // ضبط قيم القوائم المنسدلة
                comboBoxEditType.SelectedIndex = 0;
                comboBoxEditMethod.SelectedIndex = 0;
                radioGroupAppliesTo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل بيانات القوائم المنسدلة");
                XtraMessageBox.Show("حدث خطأ أثناء تحميل البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تهيئة قاعدة جديدة
        /// </summary>
        private void InitializeNewRule()
        {
            dateEditActivation.DateTime = DateTime.Today;
            checkEditActive.Checked = true;
        }
        
        /// <summary>
        /// تحميل بيانات القاعدة للتعديل
        /// </summary>
        private void LoadRuleData()
        {
            textEditName.Text = _rule.Name;
            memoEditDescription.Text = _rule.Description;
            comboBoxEditType.Text = _rule.Type;
            comboBoxEditMethod.Text = _rule.DeductionMethod;
            spinEditValue.Value = _rule.DeductionValue;
            radioGroupAppliesTo.EditValue = _rule.AppliesTo;
            lookUpEditDepartment.EditValue = _rule.DepartmentID;
            lookUpEditPosition.EditValue = _rule.PositionID;
            spinEditMinViolation.Value = _rule.MinViolation ?? 0;
            spinEditMaxViolation.Value = _rule.MaxViolation ?? 0;
            dateEditActivation.DateTime = _rule.ActivationDate ?? DateTime.Today;
            checkEditActive.Checked = _rule.IsActive;
            memoEditNotes.Text = _rule.Notes;
        }
        
        /// <summary>
        /// تحديث ظهور عناصر التحكم حسب الاختيارات
        /// </summary>
        private void UpdateControlsVisibility()
        {
            // تحديث ظهور القسم والمنصب حسب اختيار التطبيق على
            var appliesTo = radioGroupAppliesTo.EditValue?.ToString();
            
            layoutControlItemDepartment.Visibility = (appliesTo == "Department") 
                ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always 
                : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            
            layoutControlItemPosition.Visibility = (appliesTo == "Position")
                ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            
            // تحديث ظهور حدود المخالفة حسب نوع المخالفة
            var violationType = comboBoxEditType.Text;
            bool showViolationLimits = (violationType == "Late" || violationType == "Early");
            
            layoutControlItemMinViolation.Visibility = showViolationLimits
                ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            
            layoutControlItemMaxViolation.Visibility = showViolationLimits
                ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            
            // تحديث عنوان حقل القيمة حسب طريقة الخصم
            var deductionMethod = comboBoxEditMethod.Text;
            
            switch (deductionMethod)
            {
                case "Percentage":
                    layoutControlItemValue.Text = "قيمة الخصم (نسبة مئوية):";
                    break;
                case "Days":
                    layoutControlItemValue.Text = "قيمة الخصم (عدد الأيام):";
                    break;
                case "Hours":
                    layoutControlItemValue.Text = "قيمة الخصم (عدد الساعات):";
                    break;
                default:
                    layoutControlItemValue.Text = "قيمة الخصم:";
                    break;
            }
        }
        
        /// <summary>
        /// حدث تغيير نوع المخالفة
        /// </summary>
        private void comboBoxEditType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControlsVisibility();
        }
        
        /// <summary>
        /// حدث تغيير طريقة الخصم
        /// </summary>
        private void comboBoxEditMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControlsVisibility();
        }
        
        /// <summary>
        /// حدث تغيير التطبيق على
        /// </summary>
        private void radioGroupAppliesTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControlsVisibility();
        }
        
        /// <summary>
        /// حدث النقر على زر الحفظ
        /// </summary>
        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;
            
            try
            {
                CollectData();
                
                bool success;
                
                if (_isEditMode)
                {
                    success = _deductionRepository.UpdateDeductionRule(_rule);
                }
                else
                {
                    int id = _deductionRepository.AddDeductionRule(_rule);
                    success = id > 0;
                }
                
                if (success)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show("فشل في حفظ البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في حفظ قاعدة الخصم");
                XtraMessageBox.Show("حدث خطأ أثناء حفظ البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// التحقق من صحة البيانات المدخلة
        /// </summary>
        /// <returns>صحة البيانات</returns>
        private bool ValidateInput()
        {
            // التحقق من إدخال اسم القاعدة
            if (string.IsNullOrWhiteSpace(textEditName.Text))
            {
                XtraMessageBox.Show("الرجاء إدخال اسم القاعدة", "بيانات غير مكتملة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditName.Focus();
                return false;
            }
            
            // التحقق من اختيار نوع المخالفة
            if (string.IsNullOrWhiteSpace(comboBoxEditType.Text))
            {
                XtraMessageBox.Show("الرجاء اختيار نوع المخالفة", "بيانات غير مكتملة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxEditType.Focus();
                return false;
            }
            
            // التحقق من اختيار طريقة الخصم
            if (string.IsNullOrWhiteSpace(comboBoxEditMethod.Text))
            {
                XtraMessageBox.Show("الرجاء اختيار طريقة الخصم", "بيانات غير مكتملة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxEditMethod.Focus();
                return false;
            }
            
            // التحقق من إدخال قيمة الخصم
            if (spinEditValue.Value <= 0)
            {
                XtraMessageBox.Show("الرجاء إدخال قيمة صحيحة للخصم", "بيانات غير مكتملة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditValue.Focus();
                return false;
            }
            
            // التحقق من اختيار القسم إذا كان التطبيق على قسم محدد
            if (radioGroupAppliesTo.EditValue?.ToString() == "Department" && lookUpEditDepartment.EditValue == null)
            {
                XtraMessageBox.Show("الرجاء اختيار القسم", "بيانات غير مكتملة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lookUpEditDepartment.Focus();
                return false;
            }
            
            // التحقق من اختيار المنصب إذا كان التطبيق على منصب محدد
            if (radioGroupAppliesTo.EditValue?.ToString() == "Position" && lookUpEditPosition.EditValue == null)
            {
                XtraMessageBox.Show("الرجاء اختيار المنصب", "بيانات غير مكتملة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lookUpEditPosition.Focus();
                return false;
            }
            
            // التحقق من حدود المخالفة
            if ((comboBoxEditType.Text == "Late" || comboBoxEditType.Text == "Early") && 
                spinEditMinViolation.Value > spinEditMaxViolation.Value && spinEditMaxViolation.Value > 0)
            {
                XtraMessageBox.Show("الحد الأدنى للمخالفة يجب أن يكون أقل من الحد الأقصى", "بيانات غير صحيحة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditMinViolation.Focus();
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// جمع البيانات من النموذج إلى الكائن
        /// </summary>
        private void CollectData()
        {
            _rule.Name = textEditName.Text;
            _rule.Description = memoEditDescription.Text;
            _rule.Type = comboBoxEditType.Text;
            _rule.DeductionMethod = comboBoxEditMethod.Text;
            _rule.DeductionValue = spinEditValue.Value;
            _rule.AppliesTo = radioGroupAppliesTo.EditValue?.ToString();
            
            // إعادة ضبط القيم حسب الاختيارات
            _rule.DepartmentID = (_rule.AppliesTo == "Department") ? (int?)lookUpEditDepartment.EditValue : null;
            _rule.PositionID = (_rule.AppliesTo == "Position") ? (int?)lookUpEditPosition.EditValue : null;
            
            // حدود المخالفة
            bool useViolationLimits = (_rule.Type == "Late" || _rule.Type == "Early");
            _rule.MinViolation = useViolationLimits ? (decimal?)spinEditMinViolation.Value : null;
            _rule.MaxViolation = useViolationLimits ? (decimal?)spinEditMaxViolation.Value : null;
            
            _rule.ActivationDate = dateEditActivation.DateTime;
            _rule.IsActive = checkEditActive.Checked;
            _rule.Notes = memoEditNotes.Text;
            
            // في حالة الإضافة
            if (!_isEditMode)
            {
                _rule.CreatedAt = DateTime.Now;
                _rule.CreatedBy = SessionManager.CurrentUser?.ID;
            }
        }
    }
}