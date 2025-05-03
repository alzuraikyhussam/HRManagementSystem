using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.Models;

namespace HR.UI.Forms.Leave
{
    /// <summary>
    /// نموذج إدارة رصيد إجازة
    /// </summary>
    public partial class LeaveBalanceForm : XtraForm
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly int _leaveBalanceId;
        private readonly int _employeeId;
        private readonly bool _isEditMode;
        private LeaveBalance _leaveBalance;

        /// <summary>
        /// إنشاء نموذج جديد لرصيد إجازة (وضع الإضافة)
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        public LeaveBalanceForm(int employeeId = 0)
        {
            InitializeComponent();
            
            // تهيئة وحدة العمل
            _unitOfWork = new UnitOfWork();
            _leaveBalanceId = 0;
            _employeeId = employeeId;
            _isEditMode = false;
            
            // تهيئة النموذج
            this.Text = "إضافة رصيد إجازة جديد";
            
            // تسجيل الأحداث
            this.Load += LeaveBalanceForm_Load;
            buttonSave.Click += ButtonSave_Click;
            buttonCancel.Click += ButtonCancel_Click;
            lookUpEditLeaveType.EditValueChanged += LookUpEditLeaveType_EditValueChanged;
            spinEditBaseBalance.EditValueChanged += Balance_EditValueChanged;
            spinEditAdditionalBalance.EditValueChanged += Balance_EditValueChanged;
            spinEditUsedBalance.EditValueChanged += Balance_EditValueChanged;
        }

        /// <summary>
        /// إنشاء نموذج لتعديل رصيد إجازة موجود
        /// </summary>
        /// <param name="leaveBalanceId">معرف رصيد الإجازة</param>
        public LeaveBalanceForm(int leaveBalanceId)
        {
            InitializeComponent();
            
            // تهيئة وحدة العمل
            _unitOfWork = new UnitOfWork();
            _leaveBalanceId = leaveBalanceId;
            _employeeId = 0;
            _isEditMode = true;
            
            // تهيئة النموذج
            this.Text = "تعديل رصيد إجازة";
            
            // تسجيل الأحداث
            this.Load += LeaveBalanceForm_Load;
            buttonSave.Click += ButtonSave_Click;
            buttonCancel.Click += ButtonCancel_Click;
            lookUpEditLeaveType.EditValueChanged += LookUpEditLeaveType_EditValueChanged;
            spinEditBaseBalance.EditValueChanged += Balance_EditValueChanged;
            spinEditAdditionalBalance.EditValueChanged += Balance_EditValueChanged;
            spinEditUsedBalance.EditValueChanged += Balance_EditValueChanged;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void LeaveBalanceForm_Load(object sender, EventArgs e)
        {
            try
            {
                // إعداد عناصر التحكم
                InitializeControls();
                
                // تحميل بيانات رصيد الإجازة في وضع التعديل
                if (_isEditMode)
                {
                    LoadLeaveBalanceData();
                }
                else
                {
                    // وضع الإضافة
                    SetupAddMode();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تهيئة نموذج رصيد الإجازة");
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
            // تحميل قائمة الموظفين
            LoadEmployees();
            
            // تحميل قائمة أنواع الإجازات
            LoadLeaveTypes();
            
            // إعداد قائمة السنوات
            comboBoxYear.Properties.Items.Clear();
            
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear - 5; year <= currentYear + 1; year++)
            {
                comboBoxYear.Properties.Items.Add(year);
            }
            
            comboBoxYear.SelectedItem = currentYear;
            
            // تحديث الرصيد المتبقي
            UpdateRemainingBalance();
        }

        /// <summary>
        /// تحميل قائمة الموظفين
        /// </summary>
        private void LoadEmployees()
        {
            try
            {
                // الحصول على قائمة الموظفين النشطين
                var employees = _unitOfWork.EmployeeRepository.GetAllEmployees();
                
                // تصفية القائمة للموظفين النشطين فقط
                var activeEmployees = employees.FindAll(e => e.IsActive);
                
                // تعيين مصدر البيانات
                lookUpEditEmployee.Properties.DataSource = activeEmployees;
                lookUpEditEmployee.Properties.ValueMember = "ID";
                lookUpEditEmployee.Properties.DisplayMember = "FullName";
                
                // إعداد العرض
                lookUpEditEmployee.Properties.Columns.Clear();
                lookUpEditEmployee.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FullName", "اسم الموظف"));
                lookUpEditEmployee.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EmployeeCode", "الرقم الوظيفي"));
                lookUpEditEmployee.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Department.Name", "القسم"));
                
                lookUpEditEmployee.Properties.PopupWidth = 400;
                lookUpEditEmployee.Properties.NullText = "اختر الموظف...";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل قائمة الموظفين");
                throw;
            }
        }

        /// <summary>
        /// تحميل قائمة أنواع الإجازات
        /// </summary>
        private void LoadLeaveTypes()
        {
            try
            {
                // الحصول على قائمة أنواع الإجازات النشطة
                var leaveTypes = _unitOfWork.LeaveRepository.GetAllLeaveTypes();
                
                // تصفية القائمة للأنواع النشطة فقط
                var activeLeaveTypes = leaveTypes.FindAll(lt => lt.IsActive);
                
                // تعيين مصدر البيانات
                lookUpEditLeaveType.Properties.DataSource = activeLeaveTypes;
                lookUpEditLeaveType.Properties.ValueMember = "ID";
                lookUpEditLeaveType.Properties.DisplayMember = "Name";
                
                // إعداد العرض
                lookUpEditLeaveType.Properties.Columns.Clear();
                lookUpEditLeaveType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "نوع الإجازة"));
                lookUpEditLeaveType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DefaultDays", "الأيام الافتراضية"));
                
                lookUpEditLeaveType.Properties.PopupWidth = 300;
                lookUpEditLeaveType.Properties.NullText = "اختر نوع الإجازة...";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل أنواع الإجازات");
                throw;
            }
        }

        /// <summary>
        /// تحميل بيانات رصيد الإجازة للتعديل
        /// </summary>
        private void LoadLeaveBalanceData()
        {
            try
            {
                // الحصول على بيانات رصيد الإجازة
                _leaveBalance = _unitOfWork.LeaveRepository.GetLeaveBalanceById(_leaveBalanceId);
                
                if (_leaveBalance == null)
                {
                    XtraMessageBox.Show(
                        "لم يتم العثور على رصيد الإجازة المطلوب.",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                
                // تعيين معرف الموظف
                _employeeId = _leaveBalance.EmployeeID;
                
                // ملئ البيانات في النموذج
                lookUpEditEmployee.EditValue = _leaveBalance.EmployeeID;
                lookUpEditLeaveType.EditValue = _leaveBalance.LeaveTypeID;
                comboBoxYear.SelectedItem = _leaveBalance.Year;
                spinEditBaseBalance.Value = Convert.ToDecimal(_leaveBalance.BaseBalance);
                spinEditAdditionalBalance.Value = Convert.ToDecimal(_leaveBalance.AdditionalBalance);
                spinEditUsedBalance.Value = Convert.ToDecimal(_leaveBalance.UsedBalance);
                memoEditNotes.Text = _leaveBalance.Notes;
                
                // تحديث الرصيد المتبقي
                UpdateRemainingBalance();
                
                // تعطيل تغيير الموظف ونوع الإجازة والسنة في وضع التعديل
                lookUpEditEmployee.ReadOnly = true;
                lookUpEditLeaveType.ReadOnly = true;
                comboBoxYear.ReadOnly = true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل بيانات رصيد الإجازة");
                throw;
            }
        }

        /// <summary>
        /// إعداد وضع الإضافة
        /// </summary>
        private void SetupAddMode()
        {
            // إنشاء كائن جديد
            _leaveBalance = new LeaveBalance
            {
                EmployeeID = _employeeId,
                Year = DateTime.Now.Year,
                BaseBalance = 0,
                AdditionalBalance = 0,
                UsedBalance = 0
            };
            
            // تعيين القيم الافتراضية
            if (_employeeId > 0)
            {
                lookUpEditEmployee.EditValue = _employeeId;
                
                // تعطيل تغيير الموظف إذا كان محدداً مسبقاً
                lookUpEditEmployee.ReadOnly = true;
            }
            
            comboBoxYear.SelectedItem = DateTime.Now.Year;
            
            // تحديث الرصيد المتبقي
            UpdateRemainingBalance();
        }

        /// <summary>
        /// حدث تغيير نوع الإجازة
        /// </summary>
        private void LookUpEditLeaveType_EditValueChanged(object sender, EventArgs e)
        {
            // تحديث الرصيد الأساسي بناءً على نوع الإجازة المحدد (إذا كان فارغاً)
            if (lookUpEditLeaveType.EditValue != null && spinEditBaseBalance.Value == 0)
            {
                int leaveTypeId = Convert.ToInt32(lookUpEditLeaveType.EditValue);
                var leaveType = _unitOfWork.LeaveRepository.GetLeaveTypeById(leaveTypeId);
                
                if (leaveType != null)
                {
                    spinEditBaseBalance.Value = leaveType.DefaultDays;
                }
            }
            
            // التحقق من عدم وجود رصيد مسجل بالفعل للموظف ونوع الإجازة والسنة المحددة
            if (!_isEditMode && lookUpEditEmployee.EditValue != null && lookUpEditLeaveType.EditValue != null && comboBoxYear.SelectedItem != null)
            {
                int employeeId = Convert.ToInt32(lookUpEditEmployee.EditValue);
                int leaveTypeId = Convert.ToInt32(lookUpEditLeaveType.EditValue);
                int year = Convert.ToInt32(comboBoxYear.SelectedItem);
                
                bool exists = _unitOfWork.LeaveRepository.IsLeaveBalanceExists(employeeId, leaveTypeId, year);
                
                if (exists)
                {
                    XtraMessageBox.Show(
                        "يوجد رصيد مسجل بالفعل للموظف ونوع الإجازة والسنة المحددة.",
                        "رصيد موجود",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                        
                    lookUpEditLeaveType.EditValue = null;
                }
            }
        }

        /// <summary>
        /// حدث تغيير قيم الأرصدة
        /// </summary>
        private void Balance_EditValueChanged(object sender, EventArgs e)
        {
            // تحديث الرصيد المتبقي
            UpdateRemainingBalance();
        }

        /// <summary>
        /// تحديث الرصيد المتبقي
        /// </summary>
        private void UpdateRemainingBalance()
        {
            decimal baseBalance = spinEditBaseBalance.Value;
            decimal additionalBalance = spinEditAdditionalBalance.Value;
            decimal usedBalance = spinEditUsedBalance.Value;
            
            decimal remainingBalance = baseBalance + additionalBalance - usedBalance;
            
            labelControlRemainingBalance.Text = $"الرصيد المتبقي: {remainingBalance:0.#}";
            
            // تغيير لون الرصيد المتبقي حسب القيمة
            if (remainingBalance <= 0)
            {
                labelControlRemainingBalance.ForeColor = System.Drawing.Color.Red;
            }
            else if (remainingBalance < 3)
            {
                labelControlRemainingBalance.ForeColor = System.Drawing.Color.DarkOrange;
            }
            else
            {
                labelControlRemainingBalance.ForeColor = System.Drawing.Color.Green;
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
                
                // تحديث بيانات رصيد الإجازة
                _leaveBalance.EmployeeID = Convert.ToInt32(lookUpEditEmployee.EditValue);
                _leaveBalance.LeaveTypeID = Convert.ToInt32(lookUpEditLeaveType.EditValue);
                _leaveBalance.Year = Convert.ToInt32(comboBoxYear.SelectedItem);
                _leaveBalance.BaseBalance = Convert.ToDecimal(spinEditBaseBalance.Value);
                _leaveBalance.AdditionalBalance = Convert.ToDecimal(spinEditAdditionalBalance.Value);
                _leaveBalance.UsedBalance = Convert.ToDecimal(spinEditUsedBalance.Value);
                _leaveBalance.Notes = memoEditNotes.Text.Trim();
                
                // حفظ البيانات
                bool success;
                if (_isEditMode)
                {
                    // تحديث رصيد الإجازة الحالي
                    success = _unitOfWork.LeaveRepository.UpdateLeaveBalance(_leaveBalance);
                }
                else
                {
                    // إضافة رصيد إجازة جديد
                    success = _unitOfWork.LeaveRepository.AddLeaveBalance(_leaveBalance);
                }
                
                if (success)
                {
                    // إغلاق النموذج بنجاح
                    XtraMessageBox.Show(
                        _isEditMode ? "تم تحديث رصيد الإجازة بنجاح." : "تم إضافة رصيد الإجازة بنجاح.",
                        "تم بنجاح",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show(
                        "فشل في حفظ بيانات رصيد الإجازة.",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في حفظ بيانات رصيد الإجازة");
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
            // التحقق من اختيار الموظف
            if (lookUpEditEmployee.EditValue == null)
            {
                XtraMessageBox.Show(
                    "الرجاء اختيار الموظف.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                lookUpEditEmployee.Focus();
                return false;
            }
            
            // التحقق من اختيار نوع الإجازة
            if (lookUpEditLeaveType.EditValue == null)
            {
                XtraMessageBox.Show(
                    "الرجاء اختيار نوع الإجازة.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                lookUpEditLeaveType.Focus();
                return false;
            }
            
            // التحقق من اختيار السنة
            if (comboBoxYear.SelectedItem == null)
            {
                XtraMessageBox.Show(
                    "الرجاء اختيار السنة.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                comboBoxYear.Focus();
                return false;
            }
            
            // التحقق من صحة الأرصدة
            if (spinEditBaseBalance.Value < 0)
            {
                XtraMessageBox.Show(
                    "يجب أن يكون الرصيد الأساسي قيمة موجبة أو صفر.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                spinEditBaseBalance.Focus();
                return false;
            }
            
            if (spinEditAdditionalBalance.Value < 0)
            {
                XtraMessageBox.Show(
                    "يجب أن يكون الرصيد الإضافي قيمة موجبة أو صفر.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                spinEditAdditionalBalance.Focus();
                return false;
            }
            
            if (spinEditUsedBalance.Value < 0)
            {
                XtraMessageBox.Show(
                    "يجب أن يكون الرصيد المستخدم قيمة موجبة أو صفر.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                spinEditUsedBalance.Focus();
                return false;
            }
            
            // التحقق من عدم وجود رصيد مسجل بالفعل للموظف ونوع الإجازة والسنة المحددة (في حالة الإضافة فقط)
            if (!_isEditMode)
            {
                int employeeId = Convert.ToInt32(lookUpEditEmployee.EditValue);
                int leaveTypeId = Convert.ToInt32(lookUpEditLeaveType.EditValue);
                int year = Convert.ToInt32(comboBoxYear.SelectedItem);
                
                bool exists = _unitOfWork.LeaveRepository.IsLeaveBalanceExists(employeeId, leaveTypeId, year);
                
                if (exists)
                {
                    XtraMessageBox.Show(
                        "يوجد رصيد مسجل بالفعل للموظف ونوع الإجازة والسنة المحددة.",
                        "رصيد موجود",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
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