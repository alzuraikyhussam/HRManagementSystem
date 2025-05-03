using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using HR.Core;

namespace HR.UI.Forms.Settings
{
    public partial class GeneralSettingsForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly SystemSettingsManager _settingsManager;
        private GeneralSettings _settings;
        
        public GeneralSettingsForm()
        {
            InitializeComponent();
            _settingsManager = new SystemSettingsManager();
        }
        
        private void GeneralSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                // استرجاع الإعدادات
                LoadSettings();
                
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
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckPermissions()
        {
            // التحقق من صلاحية تعديل الإعدادات
            bool canEditSettings = SessionManager.HasPermission("Settings.EditGeneralSettings");
            
            // تطبيق الصلاحيات على عناصر التحكم
            SetControlsEnabled(canEditSettings);
            
            // تعطيل أو تفعيل زر الحفظ
            barButtonItemSave.Enabled = canEditSettings;
        }
        
        /// <summary>
        /// تحميل الإعدادات
        /// </summary>
        private void LoadSettings()
        {
            this.Cursor = Cursors.WaitCursor;
            
            try
            {
                // استرجاع الإعدادات العامة
                _settings = _settingsManager.GetGeneralSettings();
                
                // عرض البيانات
                DisplaySettings();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل الإعدادات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// عرض الإعدادات
        /// </summary>
        private void DisplaySettings()
        {
            if (_settings == null)
                return;
            
            // الإعدادات العامة
            textEditCompanyName.Text = _settings.CompanyName;
            pictureEditCompanyLogo.EditValue = string.IsNullOrEmpty(_settings.CompanyLogo) ? null : _settings.CompanyLogo;
            textEditSystemTitle.Text = _settings.SystemTitle;
            
            // إعدادات اللغة والتنسيق
            comboBoxEditLanguage.SelectedItem = _settings.DefaultLanguage == "ar" ? "العربية" : "English";
            textEditDateFormat.Text = _settings.DefaultDateFormat;
            textEditTimeFormat.Text = _settings.DefaultTimeFormat;
            textEditCurrency.Text = _settings.DefaultCurrency;
            
            // إعدادات الإشعارات
            checkEditNotificationsEnabled.Checked = _settings.EnableNotifications;
            checkEditEmailsEnabled.Checked = _settings.EnableEmails;
            checkEditSMSEnabled.Checked = _settings.EnableSMS;
            
            // إعدادات البريد الإلكتروني
            textEditSystemEmail.Text = _settings.SystemEmail;
            textEditSMTPServer.Text = _settings.SMTPServer;
            spinEditSMTPPort.Value = _settings.SMTPPort;
            textEditSMTPUsername.Text = _settings.SMTPUsername;
            textEditSMTPPassword.Text = _settings.SMTPPassword;
            checkEditSSLEnabled.Checked = _settings.EnableSSL;
            
            // إعدادات الرسائل النصية
            textEditSMSGatewayURL.Text = _settings.SMSGatewayURL;
            textEditSMSUsername.Text = _settings.SMSUsername;
            textEditSMSPassword.Text = _settings.SMSPassword;
            
            // تحديث حالة التفعيل لبعض العناصر
            UpdateControlsState();
        }
        
        /// <summary>
        /// تحديث حالة التفعيل لبعض العناصر
        /// </summary>
        private void UpdateControlsState()
        {
            // إعدادات الإشعارات
            groupControlEmails.Enabled = checkEditEmailsEnabled.Checked;
            groupControlSMS.Enabled = checkEditSMSEnabled.Checked;
        }
        
        /// <summary>
        /// تفعيل أو تعطيل عناصر التحكم
        /// </summary>
        /// <param name="enabled">حالة التفعيل</param>
        private void SetControlsEnabled(bool enabled)
        {
            // الإعدادات العامة
            textEditCompanyName.Enabled = enabled;
            pictureEditCompanyLogo.Enabled = enabled;
            simpleButtonBrowseLogo.Enabled = enabled;
            textEditSystemTitle.Enabled = enabled;
            
            // إعدادات اللغة والتنسيق
            comboBoxEditLanguage.Enabled = enabled;
            textEditDateFormat.Enabled = enabled;
            textEditTimeFormat.Enabled = enabled;
            textEditCurrency.Enabled = enabled;
            
            // إعدادات الإشعارات
            checkEditNotificationsEnabled.Enabled = enabled;
            checkEditEmailsEnabled.Enabled = enabled && checkEditNotificationsEnabled.Checked;
            checkEditSMSEnabled.Enabled = enabled && checkEditNotificationsEnabled.Checked;
            
            // إعدادات البريد الإلكتروني
            textEditSystemEmail.Enabled = enabled;
            textEditSMTPServer.Enabled = enabled;
            spinEditSMTPPort.Enabled = enabled;
            textEditSMTPUsername.Enabled = enabled;
            textEditSMTPPassword.Enabled = enabled;
            checkEditSSLEnabled.Enabled = enabled;
            
            // إعدادات الرسائل النصية
            textEditSMSGatewayURL.Enabled = enabled;
            textEditSMSUsername.Enabled = enabled;
            textEditSMSPassword.Enabled = enabled;
            
            // تحديث حالة التفعيل لبعض العناصر
            UpdateControlsState();
        }
        
        /// <summary>
        /// حفظ الإعدادات
        /// </summary>
        private bool SaveSettings()
        {
            try
            {
                // إنشاء كائن الإعدادات
                GeneralSettings settings = new GeneralSettings
                {
                    // الإعدادات العامة
                    CompanyName = textEditCompanyName.Text,
                    CompanyLogo = pictureEditCompanyLogo.EditValue?.ToString() ?? string.Empty,
                    SystemTitle = textEditSystemTitle.Text,
                    
                    // إعدادات اللغة والتنسيق
                    DefaultLanguage = comboBoxEditLanguage.SelectedItem.ToString() == "العربية" ? "ar" : "en",
                    DefaultDateFormat = textEditDateFormat.Text,
                    DefaultTimeFormat = textEditTimeFormat.Text,
                    DefaultCurrency = textEditCurrency.Text,
                    
                    // إعدادات الإشعارات
                    EnableNotifications = checkEditNotificationsEnabled.Checked,
                    EnableEmails = checkEditEmailsEnabled.Checked,
                    EnableSMS = checkEditSMSEnabled.Checked,
                    
                    // إعدادات البريد الإلكتروني
                    SystemEmail = textEditSystemEmail.Text,
                    SMTPServer = textEditSMTPServer.Text,
                    SMTPPort = Convert.ToInt32(spinEditSMTPPort.Value),
                    SMTPUsername = textEditSMTPUsername.Text,
                    SMTPPassword = textEditSMTPPassword.Text,
                    EnableSSL = checkEditSSLEnabled.Checked,
                    
                    // إعدادات الرسائل النصية
                    SMSGatewayURL = textEditSMSGatewayURL.Text,
                    SMSUsername = textEditSMSUsername.Text,
                    SMSPassword = textEditSMSPassword.Text
                };
                
                // حفظ الإعدادات
                bool result = _settingsManager.SaveGeneralSettings(settings);
                
                if (result)
                {
                    // تحديث الإعدادات المحملة
                    _settings = settings;
                }
                
                return result;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حفظ الإعدادات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
                return false;
            }
        }
        
        /// <summary>
        /// حدث الضغط على زر التحديث
        /// </summary>
        private void barButtonItemRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // إعادة تحميل الإعدادات
                LoadSettings();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحديث البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث الضغط على زر الحفظ
        /// </summary>
        private void barButtonItemSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // التحقق من البيانات
                if (!ValidateInput())
                    return;
                
                this.Cursor = Cursors.WaitCursor;
                
                // حفظ الإعدادات
                bool result = SaveSettings();
                
                if (result)
                {
                    XtraMessageBox.Show("تم حفظ الإعدادات بنجاح.", "نجاح", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("فشل حفظ الإعدادات.", "خطأ", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حفظ البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// التحقق من البيانات المدخلة
        /// </summary>
        private bool ValidateInput()
        {
            // التحقق من اسم الشركة
            if (string.IsNullOrWhiteSpace(textEditCompanyName.Text))
            {
                XtraMessageBox.Show("يرجى إدخال اسم الشركة.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditCompanyName.Focus();
                return false;
            }
            
            // التحقق من عنوان النظام
            if (string.IsNullOrWhiteSpace(textEditSystemTitle.Text))
            {
                XtraMessageBox.Show("يرجى إدخال عنوان النظام.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditSystemTitle.Focus();
                return false;
            }
            
            // التحقق من تنسيق التاريخ
            if (string.IsNullOrWhiteSpace(textEditDateFormat.Text))
            {
                XtraMessageBox.Show("يرجى إدخال تنسيق التاريخ.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditDateFormat.Focus();
                return false;
            }
            
            // التحقق من تنسيق الوقت
            if (string.IsNullOrWhiteSpace(textEditTimeFormat.Text))
            {
                XtraMessageBox.Show("يرجى إدخال تنسيق الوقت.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditTimeFormat.Focus();
                return false;
            }
            
            // التحقق من إعدادات البريد الإلكتروني
            if (checkEditEmailsEnabled.Checked)
            {
                // التحقق من البريد الإلكتروني للنظام
                if (string.IsNullOrWhiteSpace(textEditSystemEmail.Text))
                {
                    XtraMessageBox.Show("يرجى إدخال البريد الإلكتروني للنظام.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textEditSystemEmail.Focus();
                    return false;
                }
                
                // التحقق من خادم SMTP
                if (string.IsNullOrWhiteSpace(textEditSMTPServer.Text))
                {
                    XtraMessageBox.Show("يرجى إدخال خادم SMTP.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textEditSMTPServer.Focus();
                    return false;
                }
                
                // التحقق من منفذ SMTP
                if (spinEditSMTPPort.Value <= 0)
                {
                    XtraMessageBox.Show("يرجى إدخال منفذ SMTP صحيح.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    spinEditSMTPPort.Focus();
                    return false;
                }
            }
            
            // التحقق من إعدادات الرسائل النصية
            if (checkEditSMSEnabled.Checked)
            {
                // التحقق من رابط بوابة الرسائل النصية
                if (string.IsNullOrWhiteSpace(textEditSMSGatewayURL.Text))
                {
                    XtraMessageBox.Show("يرجى إدخال رابط بوابة الرسائل النصية.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textEditSMSGatewayURL.Focus();
                    return false;
                }
            }
            
            return true;
        }
        
        /// <summary>
        /// حدث تغيير حالة تفعيل الإشعارات
        /// </summary>
        private void checkEditNotificationsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // تحديث حالة عناصر الإشعارات
                checkEditEmailsEnabled.Enabled = checkEditNotificationsEnabled.Checked;
                checkEditSMSEnabled.Enabled = checkEditNotificationsEnabled.Checked;
                
                // إذا تم تعطيل الإشعارات، يتم تعطيل خيارات البريد والرسائل
                if (!checkEditNotificationsEnabled.Checked)
                {
                    checkEditEmailsEnabled.Checked = false;
                    checkEditSMSEnabled.Checked = false;
                }
                
                // تحديث حالة التفعيل لبعض العناصر
                UpdateControlsState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث تغيير حالة تفعيل البريد الإلكتروني
        /// </summary>
        private void checkEditEmailsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // تحديث حالة عناصر البريد الإلكتروني
                groupControlEmails.Enabled = checkEditEmailsEnabled.Checked;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث تغيير حالة تفعيل الرسائل النصية
        /// </summary>
        private void checkEditSMSEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // تحديث حالة عناصر الرسائل النصية
                groupControlSMS.Enabled = checkEditSMSEnabled.Checked;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث الضغط على زر استعراض الشعار
        /// </summary>
        private void simpleButtonBrowseLogo_Click(object sender, EventArgs e)
        {
            try
            {
                // إنشاء مربع حوار اختيار ملف
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "ملفات الصور|*.jpg;*.jpeg;*.png;*.bmp|كل الملفات|*.*";
                    openFileDialog.Title = "اختيار شعار الشركة";
                    
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // تحميل الصورة
                        pictureEditCompanyLogo.LoadImage(openFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل الصورة: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث الضغط على زر اختبار الاتصال بالبريد الإلكتروني
        /// </summary>
        private void simpleButtonTestEmail_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من البيانات
                if (string.IsNullOrWhiteSpace(textEditSystemEmail.Text))
                {
                    XtraMessageBox.Show("يرجى إدخال البريد الإلكتروني للنظام.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textEditSystemEmail.Focus();
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(textEditSMTPServer.Text))
                {
                    XtraMessageBox.Show("يرجى إدخال خادم SMTP.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textEditSMTPServer.Focus();
                    return;
                }
                
                if (spinEditSMTPPort.Value <= 0)
                {
                    XtraMessageBox.Show("يرجى إدخال منفذ SMTP صحيح.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    spinEditSMTPPort.Focus();
                    return;
                }
                
                this.Cursor = Cursors.WaitCursor;
                
                // محاولة الاتصال بخادم البريد الإلكتروني
                // هذا مجرد محاكاة للاختبار، في التطبيق الفعلي سيتم استخدام خدمة البريد الإلكتروني
                
                // تأخير مؤقت لمحاكاة عملية الاتصال
                System.Threading.Thread.Sleep(1500);
                
                // عرض رسالة النجاح
                XtraMessageBox.Show("تم الاتصال بخادم البريد الإلكتروني بنجاح.", "نجاح", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"فشل الاتصال بخادم البريد الإلكتروني: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// حدث الضغط على زر اختبار الاتصال ببوابة الرسائل النصية
        /// </summary>
        private void simpleButtonTestSMS_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من البيانات
                if (string.IsNullOrWhiteSpace(textEditSMSGatewayURL.Text))
                {
                    XtraMessageBox.Show("يرجى إدخال رابط بوابة الرسائل النصية.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textEditSMSGatewayURL.Focus();
                    return;
                }
                
                this.Cursor = Cursors.WaitCursor;
                
                // محاولة الاتصال ببوابة الرسائل النصية
                // هذا مجرد محاكاة للاختبار، في التطبيق الفعلي سيتم استخدام خدمة الرسائل النصية
                
                // تأخير مؤقت لمحاكاة عملية الاتصال
                System.Threading.Thread.Sleep(1500);
                
                // عرض رسالة النجاح
                XtraMessageBox.Show("تم الاتصال ببوابة الرسائل النصية بنجاح.", "نجاح", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"فشل الاتصال ببوابة الرسائل النصية: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}