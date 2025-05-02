using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.Models;

namespace HR.UI.Forms.Leave
{
    /// <summary>
    /// نموذج إدارة نوع إجازة
    /// </summary>
    public partial class LeaveTypeForm : XtraForm
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly int _leaveTypeId;
        private readonly bool _isEditMode;
        private LeaveType _leaveType;

        /// <summary>
        /// إنشاء نموذج جديد لنوع إجازة (وضع الإضافة)
        /// </summary>
        public LeaveTypeForm()
        {
            InitializeComponent();
            
            // تهيئة وحدة العمل
            _unitOfWork = new UnitOfWork();
            _leaveTypeId = 0;
            _isEditMode = false;
            
            // تهيئة النموذج
            this.Text = "إضافة نوع إجازة جديد";
            
            // تسجيل الأحداث
            this.Load += LeaveTypeForm_Load;
            buttonSave.Click += ButtonSave_Click;
            buttonCancel.Click += ButtonCancel_Click;
        }

        /// <summary>
        /// إنشاء نموذج لتعديل نوع إجازة موجود (وضع التعديل)
        /// </summary>
        /// <param name="leaveTypeId">معرف نوع الإجازة</param>
        public LeaveTypeForm(int leaveTypeId)
        {
            InitializeComponent();
            
            // تهيئة وحدة العمل
            _unitOfWork = new UnitOfWork();
            _leaveTypeId = leaveTypeId;
            _isEditMode = true;
            
            // تهيئة النموذج
            this.Text = "تعديل نوع إجازة";
            
            // تسجيل الأحداث
            this.Load += LeaveTypeForm_Load;
            buttonSave.Click += ButtonSave_Click;
            buttonCancel.Click += ButtonCancel_Click;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void LeaveTypeForm_Load(object sender, EventArgs e)
        {
            try
            {
                // إعداد عناصر التحكم
                InitializeControls();
                
                // تحميل بيانات نوع الإجازة في وضع التعديل
                if (_isEditMode)
                {
                    LoadLeaveTypeData();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تهيئة نموذج نوع الإجازة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تهيئة النموذج: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary>
        /// إعداد عناصر التحكم
        /// </summary>
        private void InitializeControls()
        {
            // إعداد قائمة منسدلة للونية (رموز الألوان)
            colorComboBoxColor.Properties.Items.Clear();
            
            // إضافة الألوان الأساسية
            colorComboBoxColor.Properties.Items.Add(System.Drawing.Color.Red);
            colorComboBoxColor.Properties.Items.Add(System.Drawing.Color.Green);
            colorComboBoxColor.Properties.Items.Add(System.Drawing.Color.Blue);
            colorComboBoxColor.Properties.Items.Add(System.Drawing.Color.Yellow);
            colorComboBoxColor.Properties.Items.Add(System.Drawing.Color.Purple);
            colorComboBoxColor.Properties.Items.Add(System.Drawing.Color.Orange);
            colorComboBoxColor.Properties.Items.Add(System.Drawing.Color.Teal);
            colorComboBoxColor.Properties.Items.Add(System.Drawing.Color.Pink);
            colorComboBoxColor.Properties.Items.Add(System.Drawing.Color.Brown);
            colorComboBoxColor.Properties.Items.Add(System.Drawing.Color.Gray);
            
            // تعيين اللون الافتراضي
            colorComboBoxColor.SelectedIndex = 0;
            
            // إعداد قائمة منسدلة للأولوية
            comboBoxPriority.Properties.Items.Clear();
            comboBoxPriority.Properties.Items.Add("منخفضة");
            comboBoxPriority.Properties.Items.Add("متوسطة");
            comboBoxPriority.Properties.Items.Add("عالية");
            comboBoxPriority.SelectedIndex = 1; // متوسطة افتراضياً
            
            // تعيين القيم الافتراضية
            checkBoxRequiresApproval.Checked = true;
            checkBoxIsPaid.Checked = true;
            checkBoxIsActive.Checked = true;
            spinEditDefaultDays.Value = 1;
            spinEditMaxDays.Value = 30;
            
            // في وضع الإضافة، نقوم بتهيئة كائن جديد
            if (!_isEditMode)
            {
                _leaveType = new LeaveType
                {
                    IsActive = true,
                    IsPaid = true,
                    RequiresApproval = true,
                    DefaultDays = 1,
                    MaximumDays = 30,
                    Priority = "متوسطة",
                    ColorCode = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.Blue)
                };
            }
        }

        /// <summary>
        /// تحميل بيانات نوع الإجازة للتعديل
        /// </summary>
        private void LoadLeaveTypeData()
        {
            try
            {
                // الحصول على بيانات نوع الإجازة
                _leaveType = _unitOfWork.LeaveRepository.GetLeaveTypeById(_leaveTypeId);
                
                if (_leaveType == null)
                {
                    XtraMessageBox.Show(
                        "لم يتم العثور على نوع الإجازة المطلوب.",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                
                // ملئ البيانات في النموذج
                textEditName.Text = _leaveType.Name;
                textEditDescription.Text = _leaveType.Description;
                spinEditDefaultDays.Value = _leaveType.DefaultDays;
                spinEditMaxDays.Value = _leaveType.MaximumDays;
                checkBoxRequiresApproval.Checked = _leaveType.RequiresApproval;
                checkBoxIsPaid.Checked = _leaveType.IsPaid;
                checkBoxIsActive.Checked = _leaveType.IsActive;
                
                // تعيين الأولوية
                if (!string.IsNullOrEmpty(_leaveType.Priority))
                {
                    int priorityIndex = comboBoxPriority.Properties.Items.IndexOf(_leaveType.Priority);
                    if (priorityIndex >= 0)
                    {
                        comboBoxPriority.SelectedIndex = priorityIndex;
                    }
                }
                
                // تعيين اللون
                if (!string.IsNullOrEmpty(_leaveType.ColorCode))
                {
                    try
                    {
                        System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(_leaveType.ColorCode);
                        colorComboBoxColor.SelectedColor = color;
                    }
                    catch
                    {
                        // في حال وجود خطأ في تحويل اللون، نستخدم اللون الافتراضي
                        colorComboBoxColor.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل بيانات نوع الإجازة");
                throw;
            }
        }

        /// <summary>
        /// حدث نقر زر الحفظ
        /// </summary>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من صحة الإدخال
                if (!ValidateInput())
                    return;
                
                // تحديث بيانات نوع الإجازة
                _leaveType.Name = textEditName.Text.Trim();
                _leaveType.Description = textEditDescription.Text.Trim();
                _leaveType.DefaultDays = (int)spinEditDefaultDays.Value;
                _leaveType.MaximumDays = (int)spinEditMaxDays.Value;
                _leaveType.RequiresApproval = checkBoxRequiresApproval.Checked;
                _leaveType.IsPaid = checkBoxIsPaid.Checked;
                _leaveType.IsActive = checkBoxIsActive.Checked;
                
                // تعيين الأولوية واللون
                if (comboBoxPriority.SelectedItem != null)
                {
                    _leaveType.Priority = comboBoxPriority.SelectedItem.ToString();
                }
                
                _leaveType.ColorCode = System.Drawing.ColorTranslator.ToHtml(colorComboBoxColor.SelectedColor);
                
                // حفظ البيانات
                bool success;
                if (_isEditMode)
                {
                    // تحديث نوع الإجازة الحالي
                    success = _unitOfWork.LeaveRepository.UpdateLeaveType(_leaveType);
                }
                else
                {
                    // إضافة نوع إجازة جديد
                    success = _unitOfWork.LeaveRepository.AddLeaveType(_leaveType);
                }
                
                if (success)
                {
                    // إغلاق النموذج بنجاح
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show(
                        "فشل في حفظ بيانات نوع الإجازة.",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في حفظ بيانات نوع الإجازة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء حفظ البيانات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// التحقق من صحة البيانات المدخلة
        /// </summary>
        private bool ValidateInput()
        {
            // التحقق من إدخال الاسم
            if (string.IsNullOrWhiteSpace(textEditName.Text))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال اسم نوع الإجازة.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditName.Focus();
                return false;
            }
            
            // التحقق من وجود اسم مكرر
            if (_unitOfWork.LeaveRepository.IsLeaveTypeNameExists(textEditName.Text.Trim(), _leaveTypeId))
            {
                XtraMessageBox.Show(
                    "اسم نوع الإجازة موجود بالفعل. الرجاء اختيار اسم آخر.",
                    "اسم مكرر",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditName.Focus();
                return false;
            }
            
            // التحقق من صحة عدد الأيام
            if (spinEditDefaultDays.Value <= 0)
            {
                XtraMessageBox.Show(
                    "يجب أن يكون عدد الأيام الافتراضية أكبر من صفر.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                spinEditDefaultDays.Focus();
                return false;
            }
            
            if (spinEditMaxDays.Value < spinEditDefaultDays.Value)
            {
                XtraMessageBox.Show(
                    "يجب أن يكون الحد الأقصى للأيام أكبر من أو يساوي عدد الأيام الافتراضية.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                spinEditMaxDays.Focus();
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// حدث نقر زر الإلغاء
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// التنظيف عند إغلاق النموذج
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            
            // التخلص من الموارد
            _unitOfWork.Dispose();
        }
    }
}