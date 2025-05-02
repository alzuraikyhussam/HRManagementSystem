using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.Models;

namespace HR.UI.Forms.Security
{
    /// <summary>
    /// نموذج إدارة المستخدمين
    /// </summary>
    public partial class UserForm : XtraForm
    {
        // كائن المستخدم الحالي
        private User _user;
        
        // قائمة الأدوار
        private List<Role> _roles;
        
        // قائمة الموظفين
        private List<Employee> _employees;
        
        // تحديد ما إذا كان هناك تغييرات
        private bool _hasChanges = false;
        
        // تحديد ما إذا كانت عملية إضافة جديدة
        private bool _isNewUser = false;
        
        // تحديد ما إذا كان يتم تغيير كلمة المرور
        private bool _isChangingPassword = false;
        
        /// <summary>
        /// تهيئة نموذج إدارة المستخدم
        /// </summary>
        public UserForm()
        {
            InitializeComponent();
            
            // إنشاء مستخدم جديد
            _user = new User();
            _isNewUser = true;
            _isChangingPassword = true;
            
            // ضبط خصائص النموذج
            this.Text = "إضافة مستخدم جديد";
            
            // تهيئة عناصر التحكم
            InitializeControls();
            
            // تسجيل الأحداث
            this.Load += UserForm_Load;
        }
        
        /// <summary>
        /// تهيئة نموذج إدارة المستخدم (تعديل مستخدم موجود)
        /// </summary>
        public UserForm(int userId)
        {
            InitializeComponent();
            
            // جلب المستخدم من قاعدة البيانات
            using (var unitOfWork = new UnitOfWork())
            {
                _user = unitOfWork.UserRepository.GetById(userId);
                
                if (_user == null)
                {
                    // إذا لم يتم العثور على المستخدم، إنشاء مستخدم جديد
                    _user = new User();
                    _isNewUser = true;
                    _isChangingPassword = true;
                    this.Text = "إضافة مستخدم جديد";
                }
                else
                {
                    _isNewUser = false;
                    _isChangingPassword = false;
                    this.Text = $"تعديل المستخدم: {_user.Username}";
                }
            }
            
            // تهيئة عناصر التحكم
            InitializeControls();
            
            // تسجيل الأحداث
            this.Load += UserForm_Load;
        }

        /// <summary>
        /// تهيئة عناصر التحكم
        /// </summary>
        private void InitializeControls()
        {
            try
            {
                // تسجيل أحداث التغيير للحقول
                RegisterChangeEvents();
                
                // تسجيل أحداث الأزرار
                buttonSave.Click += ButtonSave_Click;
                buttonCancel.Click += ButtonCancel_Click;
                checkEditChangePassword.CheckedChanged += CheckEditChangePassword_CheckedChanged;
                
                // إظهار/إخفاء حقول كلمة المرور بناءً على حالة الإضافة/التعديل
                layoutControlGroupPassword.Visibility = _isChangingPassword ? 
                    DevExpress.XtraLayout.Utils.LayoutVisibility.Always : 
                    DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                
                checkEditChangePassword.Visibility = _isNewUser ? 
                    DevExpress.XtraEditors.Controls.ControlVisibility.Never : 
                    DevExpress.XtraEditors.Controls.ControlVisibility.Always;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تهيئة عناصر التحكم في نموذج إدارة المستخدم");
            }
        }

        /// <summary>
        /// تسجيل أحداث التغيير للحقول
        /// </summary>
        private void RegisterChangeEvents()
        {
            textEditUsername.EditValueChanged += Control_ValueChanged;
            textEditEmail.EditValueChanged += Control_ValueChanged;
            textEditFullName.EditValueChanged += Control_ValueChanged;
            lookUpEditRole.EditValueChanged += Control_ValueChanged;
            lookUpEditEmployee.EditValueChanged += Control_ValueChanged;
            checkEditIsActive.CheckedChanged += Control_ValueChanged;
            checkEditMustChangePassword.CheckedChanged += Control_ValueChanged;
            textEditPassword.EditValueChanged += Control_ValueChanged;
            textEditConfirmPassword.EditValueChanged += Control_ValueChanged;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void UserForm_Load(object sender, EventArgs e)
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // جلب الأدوار والموظفين
                LoadRoles();
                LoadEmployees();
                
                // عرض بيانات المستخدم
                DisplayUserData();
                
                // تحديث حالة الأزرار
                UpdateButtonState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل بيانات المستخدم");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل بيانات المستخدم: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// جلب الأدوار
        /// </summary>
        private void LoadRoles()
        {
            try
            {
                // جلب الأدوار من قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    _roles = unitOfWork.RoleRepository.GetAll();
                    
                    // تعيين مصدر البيانات للقائمة المنسدلة
                    lookUpEditRole.Properties.DataSource = _roles;
                    lookUpEditRole.Properties.DisplayMember = "Name";
                    lookUpEditRole.Properties.ValueMember = "ID";
                    
                    // إضافة خيار "لا يوجد" للدور
                    lookUpEditRole.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
                    lookUpEditRole.Properties.NullText = "اختر دور المستخدم";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل جلب الأدوار");
                throw;
            }
        }

        /// <summary>
        /// جلب الموظفين
        /// </summary>
        private void LoadEmployees()
        {
            try
            {
                // جلب الموظفين من قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    // جلب الموظفين الذين ليس لديهم حسابات مستخدمين (باستثناء المستخدم الحالي في حالة التعديل)
                    if (_isNewUser)
                    {
                        _employees = unitOfWork.EmployeeRepository.GetEmployeesWithoutUsers();
                    }
                    else
                    {
                        _employees = unitOfWork.EmployeeRepository.GetEmployeesWithoutUsers(_user.ID);
                        
                        // إضافة الموظف المرتبط بالمستخدم الحالي (إن وجد)
                        if (_user.EmployeeID.HasValue)
                        {
                            var currentEmployee = unitOfWork.EmployeeRepository.GetById(_user.EmployeeID.Value);
                            if (currentEmployee != null && !_employees.Exists(e => e.ID == currentEmployee.ID))
                            {
                                _employees.Add(currentEmployee);
                            }
                        }
                    }
                    
                    // تعيين مصدر البيانات للقائمة المنسدلة
                    lookUpEditEmployee.Properties.DataSource = _employees;
                    lookUpEditEmployee.Properties.DisplayMember = "FullName";
                    lookUpEditEmployee.Properties.ValueMember = "ID";
                    
                    // إضافة خيار "لا يوجد" للموظف
                    lookUpEditEmployee.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
                    lookUpEditEmployee.Properties.NullText = "اختر الموظف المرتبط (اختياري)";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل جلب الموظفين");
                throw;
            }
        }

        /// <summary>
        /// عرض بيانات المستخدم في النموذج
        /// </summary>
        private void DisplayUserData()
        {
            if (_user == null)
                return;
            
            // عرض البيانات في الحقول
            textEditUsername.Text = _user.Username;
            textEditEmail.Text = _user.Email;
            textEditFullName.Text = _user.FullName;
            lookUpEditRole.EditValue = _user.RoleID;
            lookUpEditEmployee.EditValue = _user.EmployeeID;
            checkEditIsActive.Checked = _user.IsActive;
            checkEditMustChangePassword.Checked = _user.MustChangePassword;
            
            // إعادة تعيين حالة التغييرات
            _hasChanges = false;
        }

        /// <summary>
        /// حدث تغيير قيمة أي عنصر تحكم
        /// </summary>
        private void Control_ValueChanged(object sender, EventArgs e)
        {
            _hasChanges = true;
            UpdateButtonState();
        }

        /// <summary>
        /// حدث تغيير حالة مربع "تغيير كلمة المرور"
        /// </summary>
        private void CheckEditChangePassword_CheckedChanged(object sender, EventArgs e)
        {
            _isChangingPassword = checkEditChangePassword.Checked;
            
            // إظهار/إخفاء حقول كلمة المرور
            layoutControlGroupPassword.Visibility = _isChangingPassword ? 
                DevExpress.XtraLayout.Utils.LayoutVisibility.Always : 
                DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            
            // مسح كلمة المرور إذا تم إخفاء الحقول
            if (!_isChangingPassword)
            {
                textEditPassword.Text = "";
                textEditConfirmPassword.Text = "";
            }
            
            UpdateButtonState();
        }

        /// <summary>
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonState()
        {
            bool isValid = !string.IsNullOrWhiteSpace(textEditUsername.Text) && 
                          !string.IsNullOrWhiteSpace(textEditFullName.Text) && 
                          lookUpEditRole.EditValue != null;
            
            // التحقق من كلمة المرور إذا كان يتم تغييرها
            if (_isChangingPassword)
            {
                isValid = isValid && 
                         !string.IsNullOrWhiteSpace(textEditPassword.Text) && 
                         !string.IsNullOrWhiteSpace(textEditConfirmPassword.Text) && 
                         textEditPassword.Text == textEditConfirmPassword.Text;
            }
            
            buttonSave.Enabled = _hasChanges && isValid;
        }

        /// <summary>
        /// حدث النقر على زر الحفظ
        /// </summary>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من صحة البيانات
                if (!ValidateData())
                    return;
                
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // تحديث كائن المستخدم
                UpdateUserObject();
                
                // حفظ البيانات
                SaveUserData();
                
                // عرض رسالة النجاح
                XtraMessageBox.Show(
                    _isNewUser ? "تم إضافة المستخدم بنجاح" : "تم تعديل المستخدم بنجاح",
                    "تم الحفظ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                // إغلاق النموذج
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حفظ بيانات المستخدم");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء حفظ بيانات المستخدم: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// التحقق من صحة البيانات
        /// </summary>
        private bool ValidateData()
        {
            // التحقق من اسم المستخدم
            if (string.IsNullOrWhiteSpace(textEditUsername.Text))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال اسم المستخدم",
                    "خطأ في البيانات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditUsername.Focus();
                return false;
            }
            
            // التحقق من الاسم الكامل
            if (string.IsNullOrWhiteSpace(textEditFullName.Text))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال الاسم الكامل",
                    "خطأ في البيانات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditFullName.Focus();
                return false;
            }
            
            // التحقق من الدور
            if (lookUpEditRole.EditValue == null)
            {
                XtraMessageBox.Show(
                    "الرجاء اختيار دور المستخدم",
                    "خطأ في البيانات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                lookUpEditRole.Focus();
                return false;
            }
            
            // التحقق من صحة البريد الإلكتروني إذا كان مدخلاً
            if (!string.IsNullOrWhiteSpace(textEditEmail.Text))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(textEditEmail.Text);
                }
                catch
                {
                    XtraMessageBox.Show(
                        "الرجاء إدخال عنوان بريد إلكتروني صحيح",
                        "خطأ في البيانات",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    textEditEmail.Focus();
                    return false;
                }
            }
            
            // التحقق من كلمة المرور إذا كان يتم تغييرها
            if (_isChangingPassword)
            {
                if (string.IsNullOrWhiteSpace(textEditPassword.Text))
                {
                    XtraMessageBox.Show(
                        "الرجاء إدخال كلمة المرور",
                        "خطأ في البيانات",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    textEditPassword.Focus();
                    return false;
                }
                
                if (textEditPassword.Text.Length < 8)
                {
                    XtraMessageBox.Show(
                        "يجب أن تكون كلمة المرور 8 أحرف على الأقل",
                        "خطأ في البيانات",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    textEditPassword.Focus();
                    return false;
                }
                
                if (string.IsNullOrWhiteSpace(textEditConfirmPassword.Text))
                {
                    XtraMessageBox.Show(
                        "الرجاء تأكيد كلمة المرور",
                        "خطأ في البيانات",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    textEditConfirmPassword.Focus();
                    return false;
                }
                
                if (textEditPassword.Text != textEditConfirmPassword.Text)
                {
                    XtraMessageBox.Show(
                        "كلمة المرور وتأكيدها غير متطابقين",
                        "خطأ في البيانات",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    textEditConfirmPassword.Focus();
                    return false;
                }
            }
            
            // التحقق من عدم وجود مستخدم بنفس اسم المستخدم
            using (var unitOfWork = new UnitOfWork())
            {
                if (_isNewUser)
                {
                    // في حالة الإضافة
                    if (unitOfWork.UserRepository.Exists(u => u.Username == textEditUsername.Text))
                    {
                        XtraMessageBox.Show(
                            "يوجد مستخدم آخر بنفس اسم المستخدم. الرجاء اختيار اسم مستخدم مختلف",
                            "خطأ في البيانات",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        textEditUsername.Focus();
                        return false;
                    }
                }
                else
                {
                    // في حالة التعديل
                    if (unitOfWork.UserRepository.Exists(u => u.Username == textEditUsername.Text && u.ID != _user.ID))
                    {
                        XtraMessageBox.Show(
                            "يوجد مستخدم آخر بنفس اسم المستخدم. الرجاء اختيار اسم مستخدم مختلف",
                            "خطأ في البيانات",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        textEditUsername.Focus();
                        return false;
                    }
                }
            }
            
            return true;
        }

        /// <summary>
        /// تحديث كائن المستخدم
        /// </summary>
        private void UpdateUserObject()
        {
            if (_user == null)
                _user = new User();
            
            // تحديث البيانات من الحقول
            _user.Username = textEditUsername.Text;
            _user.Email = textEditEmail.Text;
            _user.FullName = textEditFullName.Text;
            _user.RoleID = Convert.ToInt32(lookUpEditRole.EditValue);
            _user.EmployeeID = lookUpEditEmployee.EditValue != null ? 
                Convert.ToInt32(lookUpEditEmployee.EditValue) : (int?)null;
            _user.IsActive = checkEditIsActive.Checked;
            _user.MustChangePassword = checkEditMustChangePassword.Checked;
            
            // تحديث كلمة المرور إذا كان يتم تغييرها
            if (_isChangingPassword)
            {
                // إنشاء ملح عشوائي
                byte[] salt = new byte[16];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(salt);
                }
                
                // تشفير كلمة المرور باستخدام الملح
                byte[] passwordHash;
                using (var pbkdf2 = new Rfc2898DeriveBytes(textEditPassword.Text, salt, 10000))
                {
                    passwordHash = pbkdf2.GetBytes(20);
                }
                
                // تخزين الملح وكلمة المرور المشفرة
                _user.PasswordSalt = Convert.ToBase64String(salt);
                _user.PasswordHash = Convert.ToBase64String(passwordHash);
                _user.LastPasswordChange = DateTime.Now;
            }
            
            // تحديث تواريخ الإنشاء والتعديل
            if (_isNewUser)
            {
                _user.CreatedAt = DateTime.Now;
                _user.CreatedBy = SessionManager.CurrentUser?.ID;
                _user.FailedLoginAttempts = 0;
                _user.IsLocked = false;
            }
            
            _user.UpdatedAt = DateTime.Now;
            _user.UpdatedBy = SessionManager.CurrentUser?.ID;
        }

        /// <summary>
        /// حفظ بيانات المستخدم
        /// </summary>
        private void SaveUserData()
        {
            try
            {
                // حفظ بيانات المستخدم في قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    if (_isNewUser)
                    {
                        // إضافة مستخدم جديد
                        unitOfWork.UserRepository.Add(_user);
                    }
                    else
                    {
                        // تحديث مستخدم موجود
                        unitOfWork.UserRepository.Update(_user);
                    }
                    
                    unitOfWork.Complete();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حفظ بيانات المستخدم في قاعدة البيانات");
                throw;
            }
        }

        /// <summary>
        /// حدث النقر على زر الإلغاء
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود تغييرات
                if (_hasChanges)
                {
                    DialogResult result = XtraMessageBox.Show(
                        "هل تريد تجاهل التغييرات؟",
                        "تأكيد",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    
                    if (result == DialogResult.No)
                        return;
                }
                
                // إغلاق النموذج
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل إلغاء التغييرات");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء إلغاء التغييرات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}