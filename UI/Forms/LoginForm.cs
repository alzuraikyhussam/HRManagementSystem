using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HR.Core;
using HR.Models.DTOs;

namespace HR.UI.Forms
{
    /// <summary>
    /// نموذج تسجيل الدخول للنظام
    /// </summary>
    public partial class LoginForm : XtraForm
    {
        private int _failedAttempts = 0;
        private const int MaxFailedAttempts = 5;
        
        // مسار ملف حفظ بيانات الدخول
        private readonly string _credentialsFilePath;

        /// <summary>
        /// تهيئة نموذج تسجيل الدخول
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            
            // تهيئة مسار ملف حفظ بيانات الدخول
            _credentialsFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "HRSystem", 
                "credentials.dat");
            
            // تعيين خصائص الواجهة
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // إعداد مكونات DevExpress 
            this.pictureEditLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEditLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            
            // إضافة الأحداث
            this.Load += LoginForm_Load;
            this.textEditUsername.KeyDown += TextEdit_KeyDown;
            this.textEditPassword.KeyDown += TextEdit_KeyDown;
            this.buttonLogin.Click += ButtonLogin_Click;
            this.buttonCancel.Click += ButtonCancel_Click;
            this.linkLabelForgotPassword.Click += LinkLabelForgotPassword_Click;
            this.toggleSwitchShowPassword.Toggled += ToggleSwitchShowPassword_Toggled;
            this.checkRememberMe.CheckedChanged += CheckRememberMe_CheckedChanged;
            
            // إعدادات الواجهة
            SetCompanyInfo();
            LoadSavedCredentials();
            textEditUsername.Select();
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // تحديد لون النموذج ومكوناته
            this.BackColor = Color.FromArgb(240, 240, 240);
            
            // عرض إصدار النظام
            this.labelVersion.Text = $"الإصدار {Application.ProductVersion}";
            
            // عرض اسم جهاز المستخدم
            this.labelComputer.Text = $"اسم الجهاز: {Environment.MachineName}";
            
            // تركيز حقل الإدخال المناسب
            if (string.IsNullOrWhiteSpace(textEditUsername.Text))
            {
                textEditUsername.Focus();
            }
            else if (string.IsNullOrWhiteSpace(textEditPassword.Text))
            {
                textEditPassword.Focus();
            }
            else
            {
                buttonLogin.Focus();
            }
        }

        /// <summary>
        /// إعداد معلومات الشركة
        /// </summary>
        private void SetCompanyInfo()
        {
            try
            {
                // الحصول على ملخص بيانات الشركة
                var companySummary = SessionManager.GetCompanySummary();
                
                if (companySummary != null)
                {
                    // عرض شعار الشركة
                    if (companySummary.Logo != null)
                    {
                        using (var stream = new System.IO.MemoryStream(companySummary.Logo))
                        {
                            this.pictureEditLogo.Image = Image.FromStream(stream);
                        }
                    }
                    
                    // عرض اسم الشركة
                    this.labelCompanyName.Text = companySummary.Name;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل معلومات الشركة في نموذج تسجيل الدخول");
                this.labelCompanyName.Text = "نظام إدارة الموارد البشرية";
            }
        }

        /// <summary>
        /// حدث ضغط المفاتيح لحقول النص
        /// </summary>
        private void TextEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                
                if (sender == textEditUsername)
                {
                    textEditPassword.Focus();
                }
                else if (sender == textEditPassword)
                {
                    AttemptLogin();
                }
            }
        }

        /// <summary>
        /// حدث النقر على زر تسجيل الدخول
        /// </summary>
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            AttemptLogin();
        }

        /// <summary>
        /// حدث النقر على زر الإلغاء
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// حدث النقر على رابط نسيت كلمة المرور
        /// </summary>
        private void LinkLabelForgotPassword_Click(object sender, EventArgs e)
        {
            string username = textEditUsername.Text.Trim();
            
            if (string.IsNullOrEmpty(username))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال اسم المستخدم أولاً.",
                    "نسيت كلمة المرور",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                
                textEditUsername.Focus();
                return;
            }
            
            XtraMessageBox.Show(
                "سيتم إرسال رسالة إلى بريدك الإلكتروني المسجل لإعادة تعيين كلمة المرور.\n\nيرجى مراجعة مسؤول النظام.",
                "نسيت كلمة المرور",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        
        /// <summary>
        /// حدث تبديل حالة إظهار كلمة المرور
        /// </summary>
        private void ToggleSwitchShowPassword_Toggled(object sender, EventArgs e)
        {
            // تغيير حالة إظهار كلمة المرور
            textEditPassword.Properties.UseSystemPasswordChar = !toggleSwitchShowPassword.IsOn;
        }
        
        /// <summary>
        /// حدث تغيير حالة تذكر بيانات الدخول
        /// </summary>
        private void CheckRememberMe_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkRememberMe.Checked)
            {
                // إذا تم إلغاء تحديد الخيار، نقوم بحذف أي بيانات محفوظة
                ClearSavedCredentials();
            }
        }

        /// <summary>
        /// محاولة تسجيل الدخول
        /// </summary>
        private void AttemptLogin()
        {
            // التحقق من الإدخال
            string username = textEditUsername.Text.Trim();
            string password = textEditPassword.Text;
            
            if (string.IsNullOrEmpty(username))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال اسم المستخدم.",
                    "تسجيل الدخول",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                
                textEditUsername.Focus();
                return;
            }
            
            if (string.IsNullOrEmpty(password))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال كلمة المرور.",
                    "تسجيل الدخول",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                
                textEditPassword.Focus();
                return;
            }
            
            // تعطيل عناصر التحكم أثناء عملية تسجيل الدخول
            EnableControls(false);
            
            try
            {
                // محاولة تسجيل الدخول
                string ipAddress = GetLocalIPAddress();
                AuthResultDTO result = SessionManager.Login(username, password, ipAddress);
                
                if (result.IsAuthenticated)
                {
                    // إعادة تعيين عدد المحاولات الفاشلة
                    _failedAttempts = 0;
                    
                    // حفظ بيانات المستخدم إذا تم اختيار تذكرها
                    if (checkRememberMe.Checked)
                    {
                        SaveCredentials(username, password);
                    }
                    else
                    {
                        ClearSavedCredentials();
                    }
                    
                    // التحقق مما إذا كان يجب على المستخدم تغيير كلمة المرور
                    if (result.User.MustChangePassword)
                    {
                        XtraMessageBox.Show(
                            "يجب عليك تغيير كلمة المرور الخاصة بك للمتابعة.",
                            "تغيير كلمة المرور مطلوب",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        
                        // فتح نموذج تغيير كلمة المرور
                        if (ShowChangePasswordDialog(result.User.ID))
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            // إذا ألغى المستخدم تغيير كلمة المرور، نقوم بتسجيل الخروج
                            SessionManager.Logout();
                            EnableControls(true);
                        }
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    // زيادة عدد المحاولات الفاشلة
                    _failedAttempts++;
                    
                    // التحقق مما إذا كان يجب قفل الحساب
                    if (_failedAttempts >= MaxFailedAttempts)
                    {
                        XtraMessageBox.Show(
                            "لقد تجاوزت الحد الأقصى المسموح به من محاولات تسجيل الدخول الفاشلة. سيتم إغلاق التطبيق لأسباب أمنية.",
                            "تسجيل الدخول",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        
                        Application.Exit();
                    }
                    else
                    {
                        XtraMessageBox.Show(
                            $"{result.Message}\n\nعدد المحاولات المتبقية: {MaxFailedAttempts - _failedAttempts}",
                            "تسجيل الدخول",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        
                        textEditPassword.Clear();
                        textEditPassword.Focus();
                        EnableControls(true);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تسجيل الدخول");
                
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تسجيل الدخول: {ex.Message}",
                    "تسجيل الدخول",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                
                EnableControls(true);
            }
        }

        /// <summary>
        /// تمكين أو تعطيل عناصر التحكم
        /// </summary>
        private void EnableControls(bool enable)
        {
            this.textEditUsername.Enabled = enable;
            this.textEditPassword.Enabled = enable;
            this.buttonLogin.Enabled = enable;
            this.linkLabelForgotPassword.Enabled = enable;
            this.checkRememberMe.Enabled = enable;
            this.toggleSwitchShowPassword.Enabled = enable;
            
            if (!enable)
            {
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
            
            Application.DoEvents();
        }

        /// <summary>
        /// الحصول على عنوان IP المحلي
        /// </summary>
        private string GetLocalIPAddress()
        {
            try
            {
                string hostName = System.Net.Dns.GetHostName();
                System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(hostName);
                
                foreach (System.Net.IPAddress address in hostEntry.AddressList)
                {
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return address.ToString();
                    }
                }
                
                return "127.0.0.1"; // Fallback to localhost
            }
            catch
            {
                return "127.0.0.1"; // Fallback to localhost
            }
        }

        /// <summary>
        /// عرض نموذج تغيير كلمة المرور
        /// </summary>
        private bool ShowChangePasswordDialog(int userId)
        {
            // ملاحظة: يجب إنشاء نموذج تغيير كلمة المرور لاحقاً
            // لكن حالياً نستخدم نمط بديل مؤقت
            
            using (var form = new XtraForm())
            {
                form.Text = "تغيير كلمة المرور";
                form.StartPosition = FormStartPosition.CenterParent;
                form.Size = new Size(400, 250);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                
                var labelInfo = new LabelControl
                {
                    Text = "يجب عليك تغيير كلمة المرور الخاصة بك للمتابعة.",
                    AutoSizeMode = LabelAutoSizeMode.None,
                    Size = new Size(360, 40),
                    Location = new Point(20, 20)
                };
                
                var labelCurrent = new LabelControl
                {
                    Text = "كلمة المرور الحالية:",
                    Location = new Point(20, 70)
                };
                
                var textCurrent = new TextEdit
                {
                    Location = new Point(150, 70),
                    Size = new Size(200, 20),
                    Properties = { UseSystemPasswordChar = true }
                };
                
                var labelNew = new LabelControl
                {
                    Text = "كلمة المرور الجديدة:",
                    Location = new Point(20, 100)
                };
                
                var textNew = new TextEdit
                {
                    Location = new Point(150, 100),
                    Size = new Size(200, 20),
                    Properties = { UseSystemPasswordChar = true }
                };
                
                var labelConfirm = new LabelControl
                {
                    Text = "تأكيد كلمة المرور:",
                    Location = new Point(20, 130)
                };
                
                var textConfirm = new TextEdit
                {
                    Location = new Point(150, 130),
                    Size = new Size(200, 20),
                    Properties = { UseSystemPasswordChar = true }
                };
                
                var buttonOK = new SimpleButton
                {
                    Text = "موافق",
                    Location = new Point(150, 170),
                    Size = new Size(100, 30)
                };
                
                var buttonCancel = new SimpleButton
                {
                    Text = "إلغاء",
                    Location = new Point(260, 170),
                    Size = new Size(100, 30)
                };
                
                form.Controls.AddRange(new Control[] { labelInfo, labelCurrent, textCurrent, labelNew, textNew, labelConfirm, textConfirm, buttonOK, buttonCancel });
                
                buttonOK.Click += (s, e) =>
                {
                    // التحقق من الإدخال
                    if (string.IsNullOrEmpty(textCurrent.Text))
                    {
                        XtraMessageBox.Show("الرجاء إدخال كلمة المرور الحالية.", "تغيير كلمة المرور", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textCurrent.Focus();
                        return;
                    }
                    
                    if (string.IsNullOrEmpty(textNew.Text))
                    {
                        XtraMessageBox.Show("الرجاء إدخال كلمة المرور الجديدة.", "تغيير كلمة المرور", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textNew.Focus();
                        return;
                    }
                    
                    if (textNew.Text.Length < 6)
                    {
                        XtraMessageBox.Show("يجب أن تتكون كلمة المرور الجديدة من 6 أحرف على الأقل.", "تغيير كلمة المرور", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textNew.Focus();
                        return;
                    }
                    
                    if (textNew.Text != textConfirm.Text)
                    {
                        XtraMessageBox.Show("كلمة المرور الجديدة وتأكيدها غير متطابقين.", "تغيير كلمة المرور", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textConfirm.Focus();
                        return;
                    }
                    
                    try
                    {
                        // تحديث كلمة المرور
                        bool success = SecurityManager.ChangePassword(userId, textCurrent.Text, textNew.Text);
                        
                        if (success)
                        {
                            XtraMessageBox.Show("تم تغيير كلمة المرور بنجاح.", "تغيير كلمة المرور", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            form.DialogResult = DialogResult.OK;
                            form.Close();
                        }
                        else
                        {
                            XtraMessageBox.Show("فشل تغيير كلمة المرور. الرجاء التحقق من كلمة المرور الحالية.", "تغيير كلمة المرور", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textCurrent.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"حدث خطأ أثناء تغيير كلمة المرور: {ex.Message}", "تغيير كلمة المرور", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
                
                buttonCancel.Click += (s, e) =>
                {
                    form.DialogResult = DialogResult.Cancel;
                    form.Close();
                };
                
                return form.ShowDialog() == DialogResult.OK;
            }
        }
        
        /// <summary>
        /// حفظ بيانات الدخول
        /// </summary>
        private void SaveCredentials(string username, string password)
        {
            try
            {
                // إنشاء دليل التخزين إذا لم يكن موجوداً
                string directory = Path.GetDirectoryName(_credentialsFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                // تشفير البيانات
                string encryptedData = EncryptCredentials(username, password);
                
                // حفظ البيانات المشفرة
                File.WriteAllText(_credentialsFilePath, encryptedData);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في حفظ بيانات الدخول");
                // تجاهل الخطأ واستمر بدون تخزين البيانات
            }
        }
        
        /// <summary>
        /// حذف بيانات الدخول المحفوظة
        /// </summary>
        private void ClearSavedCredentials()
        {
            try
            {
                if (File.Exists(_credentialsFilePath))
                {
                    File.Delete(_credentialsFilePath);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في حذف بيانات الدخول المحفوظة");
                // تجاهل الخطأ
            }
        }
        
        /// <summary>
        /// تحميل بيانات الدخول المحفوظة
        /// </summary>
        private void LoadSavedCredentials()
        {
            try
            {
                if (File.Exists(_credentialsFilePath))
                {
                    // قراءة البيانات المشفرة
                    string encryptedData = File.ReadAllText(_credentialsFilePath);
                    
                    // فك تشفير البيانات
                    var credentials = DecryptCredentials(encryptedData);
                    
                    if (credentials != null)
                    {
                        // ملء حقول النموذج
                        textEditUsername.Text = credentials.Item1;
                        textEditPassword.Text = credentials.Item2;
                        checkRememberMe.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل بيانات الدخول المحفوظة");
                // تجاهل الخطأ واستمر بدون تحميل البيانات
                
                // حذف الملف المعطوب
                ClearSavedCredentials();
            }
        }
        
        /// <summary>
        /// تشفير بيانات الدخول
        /// </summary>
        private string EncryptCredentials(string username, string password)
        {
            try
            {
                // استخدام مفتاح خاص بالجهاز لتشفير البيانات
                byte[] key = GetMachineKey();
                string data = $"{username}|{password}";
                
                return SecurityManager.Encrypt(data, key);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تشفير بيانات الدخول");
                throw;
            }
        }
        
        /// <summary>
        /// فك تشفير بيانات الدخول
        /// </summary>
        private Tuple<string, string> DecryptCredentials(string encryptedData)
        {
            try
            {
                // استخدام مفتاح خاص بالجهاز لفك تشفير البيانات
                byte[] key = GetMachineKey();
                string data = SecurityManager.Decrypt(encryptedData, key);
                
                if (string.IsNullOrEmpty(data) || !data.Contains("|"))
                {
                    return null;
                }
                
                string[] parts = data.Split('|');
                if (parts.Length != 2)
                {
                    return null;
                }
                
                return new Tuple<string, string>(parts[0], parts[1]);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في فك تشفير بيانات الدخول");
                return null;
            }
        }
        
        /// <summary>
        /// الحصول على مفتاح مشتق من معلومات الجهاز
        /// </summary>
        private byte[] GetMachineKey()
        {
            // استخدام اسم الجهاز ومعرف المستخدم لإنشاء مفتاح فريد لهذا الجهاز
            string machineSpecificInfo = Environment.MachineName + Environment.UserName;
            return SecurityManager.DeriveKeyFromString(machineSpecificInfo);
        }
    }
}