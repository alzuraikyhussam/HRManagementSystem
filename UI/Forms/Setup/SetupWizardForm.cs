using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraWizard;
using HR.Core;
using HR.Models.DTOs;

namespace HR.UI.Forms.Setup
{
    /// <summary>
    /// نموذج معالج الإعداد الأولي للنظام
    /// </summary>
    public partial class SetupWizardForm : XtraForm
    {
        // بيانات الشركة
        private CompanyDTO _company = new CompanyDTO();
        
        // بيانات مدير النظام
        private UserDTO _admin = new UserDTO();
        
        // مسار الصورة المؤقت
        private string _tempLogoPath;
        
        /// <summary>
        /// تهيئة نموذج معالج الإعداد
        /// </summary>
        public SetupWizardForm()
        {
            InitializeComponent();
            
            // تعيين خصائص النموذج
            this.Text = "معالج إعداد نظام إدارة الموارد البشرية";
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // ضبط الإعدادات الافتراضية
            InitializeDefaultValues();
            
            // تسجيل الأحداث
            this.wizardControl.FinishClick += WizardControl_FinishClick;
            this.wizardControl.CancelClick += WizardControl_CancelClick;
            this.wizardControl.NextClick += WizardControl_NextClick;
            this.buttonSelectLogo.Click += ButtonSelectLogo_Click;
            this.buttonClearLogo.Click += ButtonClearLogo_Click;
            this.dateEditCompanyEstablished.Properties.Mask.Culture = new System.Globalization.CultureInfo("ar-SA");
            
            // تحديد حقول كلمة المرور
            textEditAdminPassword.Properties.UseSystemPasswordChar = true;
            textEditAdminConfirmPassword.Properties.UseSystemPasswordChar = true;
            
            // توليد رمز التحقق
            GenerateVerificationCode();
        }

        /// <summary>
        /// تهيئة القيم الافتراضية
        /// </summary>
        private void InitializeDefaultValues()
        {
            // بيانات الشركة الافتراضية
            _company.EstablishmentDate = DateTime.Now;
            dateEditCompanyEstablished.DateTime = _company.EstablishmentDate;
            
            // إعدادات قاعدة البيانات الافتراضية
            radioGroupDatabaseType.SelectedIndex = 0; // SQL Server
            
            // تهيئة بعض العناصر
            comboBoxEditCountry.Properties.Items.AddRange(new object[] { 
                "المملكة العربية السعودية", "الإمارات العربية المتحدة", "قطر", "الكويت", "البحرين", "عمان", "مصر", "الأردن", "لبنان", "العراق", "المغرب", "تونس", "الجزائر", "ليبيا", "السودان", "اليمن", "سوريا", "فلسطين" 
            });
            comboBoxEditCountry.SelectedIndex = 0;
            
            // إعدادات النظام الافتراضية
            checkEditEnableFingerprint.Checked = true;
            checkEditEnableSMS.Checked = false;
            checkEditEnableEmail.Checked = true;
        }

        /// <summary>
        /// حدث النقر على زر الإنهاء
        /// </summary>
        private void WizardControl_FinishClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // منع الإغلاق التلقائي
            
            try
            {
                // عرض رسالة تأكيد للمستخدم
                DialogResult result = XtraMessageBox.Show(
                    "هل أنت متأكد من تطبيق إعدادات النظام؟",
                    "تأكيد الإعداد",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.No)
                {
                    return;
                }
                
                // إظهار مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // تجميع بيانات الإعداد
                if (!CollectSetupData())
                {
                    return;
                }
                
                // تطبيق إعدادات النظام
                ApplySystemSetup();
                
                // عرض رسالة نجاح
                XtraMessageBox.Show(
                    "تم إعداد النظام بنجاح. يمكنك الآن تسجيل الدخول باستخدام حساب المدير الذي أنشأته.",
                    "اكتمال الإعداد",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                // إغلاق النموذج
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء إعداد النظام: {ex.Message}",
                    "خطأ في الإعداد",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// حدث النقر على زر الإلغاء
        /// </summary>
        private void WizardControl_CancelClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // عرض رسالة تأكيد للمستخدم
            DialogResult result = XtraMessageBox.Show(
                "هل أنت متأكد من إلغاء إعداد النظام؟",
                "تأكيد الإلغاء",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            
            e.Cancel = (result == DialogResult.No);
            
            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        /// حدث النقر على زر التالي
        /// </summary>
        private void WizardControl_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            // التحقق من البيانات قبل الانتقال للصفحة التالية
            if (e.Page == wizardPageCompany)
            {
                if (!ValidateCompanyPage())
                {
                    e.Handled = true;
                }
            }
            else if (e.Page == wizardPageAdmin)
            {
                if (!ValidateAdminPage())
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// حدث النقر على زر اختيار الشعار
        /// </summary>
        private void ButtonSelectLogo_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "ملفات الصور|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                    dialog.Title = "اختر شعار الشركة";
                    
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        // حفظ مسار الصورة المؤقت
                        _tempLogoPath = dialog.FileName;
                        
                        // عرض الصورة
                        using (var stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            pictureEditLogo.Image = Image.FromStream(stream);
                        }
                        
                        // تفعيل زر حذف الشعار
                        buttonClearLogo.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل الصورة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر حذف الشعار
        /// </summary>
        private void ButtonClearLogo_Click(object sender, EventArgs e)
        {
            // حذف الصورة المحددة
            pictureEditLogo.Image = null;
            _tempLogoPath = null;
            buttonClearLogo.Enabled = false;
        }

        /// <summary>
        /// التحقق من صحة بيانات الشركة
        /// </summary>
        private bool ValidateCompanyPage()
        {
            // التحقق من اسم الشركة
            if (string.IsNullOrWhiteSpace(textEditCompanyName.Text))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال اسم الشركة",
                    "بيانات غير مكتملة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditCompanyName.Focus();
                return false;
            }
            
            // التحقق من سنة التأسيس
            if (dateEditCompanyEstablished.DateTime > DateTime.Now)
            {
                XtraMessageBox.Show(
                    "تاريخ التأسيس يجب أن يكون في الماضي",
                    "تاريخ غير صالح",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                dateEditCompanyEstablished.Focus();
                return false;
            }
            
            // حفظ بيانات الشركة
            _company.Name = textEditCompanyName.Text;
            _company.CommercialRegistrationNo = textEditCompanyRegNo.Text;
            _company.TaxNumber = textEditCompanyTaxNo.Text;
            _company.EstablishmentDate = dateEditCompanyEstablished.DateTime;
            _company.Phone = textEditCompanyPhone.Text;
            _company.Email = textEditCompanyEmail.Text;
            _company.Website = textEditCompanyWebsite.Text;
            _company.Address = memoEditCompanyAddress.Text;
            _company.Country = comboBoxEditCountry.Text;
            
            return true;
        }

        /// <summary>
        /// التحقق من صحة بيانات المدير
        /// </summary>
        private bool ValidateAdminPage()
        {
            // التحقق من اسم المستخدم
            if (string.IsNullOrWhiteSpace(textEditAdminUsername.Text))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال اسم المستخدم للمدير",
                    "بيانات غير مكتملة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditAdminUsername.Focus();
                return false;
            }
            
            // التحقق من الاسم الكامل
            if (string.IsNullOrWhiteSpace(textEditAdminFullName.Text))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال الاسم الكامل للمدير",
                    "بيانات غير مكتملة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditAdminFullName.Focus();
                return false;
            }
            
            // التحقق من البريد الإلكتروني
            if (string.IsNullOrWhiteSpace(textEditAdminEmail.Text) || !IsValidEmail(textEditAdminEmail.Text))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال بريد إلكتروني صالح للمدير",
                    "بيانات غير صالحة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditAdminEmail.Focus();
                return false;
            }
            
            // التحقق من كلمة المرور
            if (string.IsNullOrWhiteSpace(textEditAdminPassword.Text) || textEditAdminPassword.Text.Length < 6)
            {
                XtraMessageBox.Show(
                    "كلمة المرور يجب أن تكون 6 أحرف على الأقل",
                    "كلمة مرور ضعيفة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditAdminPassword.Focus();
                return false;
            }
            
            // التحقق من تطابق كلمة المرور
            if (textEditAdminPassword.Text != textEditAdminConfirmPassword.Text)
            {
                XtraMessageBox.Show(
                    "كلمة المرور وتأكيدها غير متطابقين",
                    "خطأ في كلمة المرور",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditAdminConfirmPassword.Focus();
                return false;
            }
            
            // التحقق من رمز التحقق
            if (textEditVerificationCode.Text != labelVerificationCode.Text)
            {
                XtraMessageBox.Show(
                    "رمز التحقق غير صحيح، يرجى إدخاله بشكل صحيح",
                    "خطأ في التحقق",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditVerificationCode.Focus();
                
                // توليد رمز تحقق جديد
                GenerateVerificationCode();
                return false;
            }
            
            // حفظ بيانات المدير
            _admin.Username = textEditAdminUsername.Text;
            _admin.FullName = textEditAdminFullName.Text;
            _admin.Email = textEditAdminEmail.Text;
            _admin.Phone = textEditAdminPhone.Text;
            _admin.Password = textEditAdminPassword.Text; // سيتم تشفيرها لاحقاً
            
            return true;
        }

        /// <summary>
        /// تطبيق إعدادات النظام
        /// </summary>
        private void ApplySystemSetup()
        {
            try
            {
                // تحويل شعار الشركة إلى مصفوفة بايتات
                if (!string.IsNullOrEmpty(_tempLogoPath) && File.Exists(_tempLogoPath))
                {
                    _company.Logo = File.ReadAllBytes(_tempLogoPath);
                }
                
                // تحديد نوع قاعدة البيانات
                string databaseType = radioGroupDatabaseType.Properties.Items[radioGroupDatabaseType.SelectedIndex].ToString();
                
                // تهيئة إعدادات الاتصال بقاعدة البيانات
                SetupResultDTO setupResult = new SetupResultDTO
                {
                    DatabaseType = databaseType,
                    Company = _company,
                    AdminUser = _admin,
                    Settings = new SystemSettingsDTO
                    {
                        EnableFingerprint = checkEditEnableFingerprint.Checked,
                        EnableSMSNotifications = checkEditEnableSMS.Checked,
                        EnableEmailNotifications = checkEditEnableEmail.Checked,
                        DefaultLanguage = "ar-SA"
                    }
                };
                
                // إعداد نوع قاعدة البيانات
                switch (databaseType)
                {
                    case "SQL Server":
                        setupResult.ConnectionString = textEditConnectionString.Text;
                        break;
                    case "PostgreSQL":
                        setupResult.ConnectionString = textEditConnectionString.Text;
                        break;
                    case "SQLite":
                        string dbFile = Path.Combine(Application.StartupPath, "HRSystem.db");
                        setupResult.ConnectionString = $"Data Source={dbFile};Version=3;";
                        break;
                }
                
                // تطبيق الإعداد
                bool success = SystemSetupManager.ApplySystemSetup(setupResult);
                
                if (!success)
                {
                    throw new Exception("فشل تطبيق الإعدادات");
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تطبيق إعدادات النظام");
                throw;
            }
        }

        /// <summary>
        /// تجميع بيانات الإعداد
        /// </summary>
        private bool CollectSetupData()
        {
            try
            {
                // التحقق من بيانات قاعدة البيانات
                if (radioGroupDatabaseType.SelectedIndex != 2) // إذا لم تكن SQLite
                {
                    if (string.IsNullOrWhiteSpace(textEditConnectionString.Text))
                    {
                        XtraMessageBox.Show(
                            "الرجاء إدخال سلسلة الاتصال بقاعدة البيانات",
                            "بيانات غير مكتملة",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        wizardControl.SelectedPage = wizardPageDatabase;
                        textEditConnectionString.Focus();
                        return false;
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تجميع بيانات الإعداد");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تجميع بيانات الإعداد: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// التحقق من صحة البريد الإلكتروني
        /// </summary>
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// توليد رمز التحقق
        /// </summary>
        private void GenerateVerificationCode()
        {
            // توليد رمز عشوائي من 6 أرقام
            Random random = new Random();
            int code = random.Next(100000, 999999);
            labelVerificationCode.Text = code.ToString();
        }
    }
}