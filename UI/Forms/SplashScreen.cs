using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;
using DevExpress.Utils;
using DevExpress.LookAndFeel;
using HR.Core;

namespace HR.UI.Forms
{
    /// <summary>
    /// نموذج شاشة البداية للنظام
    /// </summary>
    public partial class SplashScreen : SplashScreen
    {
        private int _progressValue = 0;
        private bool _isFirstLoad = true;
        
        /// <summary>
        /// تهيئة نموذج شاشة البداية
        /// </summary>
        public SplashScreen()
        {
            InitializeComponent();
            
            // إعداد النص
            this.labelCopyright.Text = $"Copyright © {DateTime.Now.Year}";
            this.labelApplicationVersion.Text = $"الإصدار {Application.ProductVersion}";
            
            // إعداد الصورة
            SetupLogo();
        }

        #region Overrides

        /// <summary>
        /// تحديث حالة تقدم التهيئة
        /// </summary>
        /// <param name="operationDescription">وصف العملية الحالية</param>
        /// <param name="progress">نسبة التقدم</param>
        public override void SetupStatusAndProgress(string operationDescription, int progress)
        {
            if (!string.IsNullOrEmpty(operationDescription))
            {
                this.labelStatus.Text = operationDescription;
                this.circularProgress.Description = operationDescription;
            }
            
            _progressValue = progress;
        }

        /// <summary>
        /// الإغلاق الآمن بعد ظهور النافذة الرئيسية
        /// </summary>
        public void CloseWhenAppReady()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion

        /// <summary>
        /// إعداد شعار الشركة
        /// </summary>
        private void SetupLogo()
        {
            try
            {
                // محاولة تحميل الشعار من ملف
                string logoPath = Path.Combine(Application.StartupPath, "Logo.png");
                if (File.Exists(logoPath))
                {
                    using (var stream = new FileStream(logoPath, FileMode.Open, FileAccess.Read))
                    {
                        this.pictureEditLogo.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    // استخدام الشعار الافتراضي
                    // الشعار مضمن في الموارد
                }
            }
            catch (Exception ex)
            {
                // تجاهل استثناءات قراءة الشعار واستخدام الشعار الافتراضي
                Console.WriteLine($"Failed to load logo: {ex.Message}");
            }
        }

        /// <summary>
        /// عرض شاشة البداية وتهيئة النظام
        /// </summary>
        private void SplashScreen_Load(object sender, EventArgs e)
        {
            if (_isFirstLoad)
            {
                _isFirstLoad = false;
                // تشغيل مهمة تهيئة النظام في خلفية
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(InitializeSystem));
            }
        }

        /// <summary>
        /// تحديث شريط التقدم
        /// </summary>
        private void marqueeTimer_Tick(object sender, EventArgs e)
        {
            // تحديث القيمة الحالية للتقدم بشكل تدريجي
            this.progressBarControl.EditValue = _progressValue;
        }

        /// <summary>
        /// تهيئة النظام في خلفية
        /// </summary>
        private void InitializeSystem(object state)
        {
            try
            {
                // تأخير بسيط لعرض الشاشة
                System.Threading.Thread.Sleep(500);
                
                // تهيئة مدير السجلات
                SetStatusText("تهيئة سجلات النظام...");
                LogManager.Initialize();
                SetProgress(10);
                System.Threading.Thread.Sleep(300);

                // تطبيق السمة والإعدادات
                SetStatusText("تحميل مظهر النظام...");
                InitializeAppearance();
                SetProgress(20);
                System.Threading.Thread.Sleep(300);

                // التحقق من الاتصال بقاعدة البيانات
                SetStatusText("الاتصال بقاعدة البيانات...");
                if (!ConnectionManager.Initialize())
                {
                    ShowDbConnectionError();
                    return;
                }
                SetProgress(40);
                System.Threading.Thread.Sleep(300);

                // التحقق من تكوين قاعدة البيانات
                SetStatusText("التحقق من تكوين قاعدة البيانات...");
                if (!ConnectionManager.IsSystemConfigured())
                {
                    // التحقق مما إذا كان يجب عرض معالج الإعداد
                    if (ShouldShowSetupWizard())
                    {
                        ShowSetupWizard();
                        return;
                    }
                    
                    // تهيئة قاعدة البيانات إذا لم تكن موجودة
                    SetStatusText("تهيئة قاعدة البيانات...");
                    if (!ConnectionManager.InitializeDatabase())
                    {
                        ShowDbInitializationError();
                        return;
                    }
                }
                SetProgress(60);
                System.Threading.Thread.Sleep(300);

                // تهيئة مدير الجلسات
                SetStatusText("تهيئة بيانات النظام...");
                SessionManager.Initialize();
                SetProgress(70);
                System.Threading.Thread.Sleep(300);

                // تحميل اللغة والإعدادات
                SetStatusText("تحميل إعدادات النظام...");
                LoadSystemSettings();
                SetProgress(80);
                System.Threading.Thread.Sleep(300);

                // تهيئة أجهزة البصمة
                SetStatusText("تهيئة أجهزة البصمة...");
                InitializeFingerPrintDevices();
                SetProgress(90);
                System.Threading.Thread.Sleep(300);

                // اكتمال التهيئة
                SetStatusText("اكتملت تهيئة النظام بنجاح");
                SetProgress(100);
                System.Threading.Thread.Sleep(500);

                // عرض شاشة تسجيل الدخول
                this.Invoke(new Action(() =>
                {
                    var loginForm = new LoginForm();
                    this.Hide();
                    
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        // عرض النافذة الرئيسية بعد تسجيل الدخول
                        var mainForm = new MainForm();
                        mainForm.Show();
                    }
                    else
                    {
                        // إنهاء التطبيق إذا لم يتم تسجيل الدخول
                        Application.Exit();
                    }

                    // إغلاق شاشة البداية
                    CloseWhenAppReady();
                }));
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء تهيئة النظام");
                this.Invoke(new Action(() =>
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        $"حدث خطأ أثناء تهيئة النظام: {ex.Message}",
                        "خطأ في النظام",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Application.Exit();
                }));
            }
        }

        /// <summary>
        /// تهيئة مظهر التطبيق
        /// </summary>
        private void InitializeAppearance()
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    // تطبيق سمة المظهر
                    UserLookAndFeel.Default.SkinName = "Office 2019 Colorful";
                    
                    // إعداد إتجاه النص للغة العربية
                    Application.CurrentCulture = new System.Globalization.CultureInfo("ar-SA");
                    Application.CurrentUICulture = new System.Globalization.CultureInfo("ar-SA");
                }));
            }
            catch (Exception ex)
            {
                LogManager.LogWarning($"فشل تهيئة مظهر التطبيق: {ex.Message}");
                // تجاهل الخطأ والإستمرار بالإعدادات الإفتراضية
            }
        }

        /// <summary>
        /// تهيئة أجهزة البصمة
        /// </summary>
        private void InitializeFingerPrintDevices()
        {
            try
            {
                // في التطبيق الفعلي، يمكن الاتصال بأجهزة البصمة هنا أو ترك ذلك للنافذة المختصة
            }
            catch (Exception ex)
            {
                LogManager.LogWarning($"فشل تهيئة أجهزة البصمة: {ex.Message}");
                // تجاهل الخطأ والإستمرار
            }
        }

        /// <summary>
        /// التحقق مما إذا كان يجب عرض معالج الإعداد
        /// </summary>
        private bool ShouldShowSetupWizard()
        {
            // في التطبيق الفعلي، يمكن فحص وجود جداول قاعدة البيانات أو تكوينات معينة
            return !ConnectionManager.IsSystemConfigured();
        }

        /// <summary>
        /// عرض معالج الإعداد الأولي
        /// </summary>
        private void ShowSetupWizard()
        {
            this.Invoke(new Action(() =>
            {
                // في الإصدار الكامل، سيتم استبدال هذا بمعالج الإعداد الحقيقي
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "يجب إعداد النظام للمرة الأولى. سيتم عرض معالج الإعداد الآن.",
                    "معالج الإعداد",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    
                // من المفترض أن يعرض معالج الإعداد هنا
                // SetupWizardForm setupWizard = new SetupWizardForm();
                // setupWizard.ShowDialog();
                
                Application.Exit();
            }));
        }

        /// <summary>
        /// تحديث نص حالة التقدم
        /// </summary>
        private void SetStatusText(string text)
        {
            this.Invoke(new Action(() =>
            {
                this.labelStatus.Text = text;
                this.circularProgress.Description = text;
            }));
        }

        /// <summary>
        /// تحديث قيمة التقدم
        /// </summary>
        private void SetProgress(int progress)
        {
            _progressValue = progress;
        }

        /// <summary>
        /// تحميل إعدادات النظام
        /// </summary>
        private void LoadSystemSettings()
        {
            try
            {
                var company = SessionManager.GetCompanyData();
                
                this.Invoke(new Action(() =>
                {
                    if (company != null)
                    {
                        this.labelCompanyName.Text = company.Name;
                        
                        // تحميل شعار الشركة إذا كان متوفرا
                        if (company.Logo != null && company.Logo.Length > 0)
                        {
                            using (var ms = new MemoryStream(company.Logo))
                            {
                                this.pictureEditLogo.Image = Image.FromStream(ms);
                            }
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل إعدادات النظام");
                // استخدام الإعدادات الافتراضية
            }
        }

        /// <summary>
        /// عرض خطأ الاتصال بقاعدة البيانات
        /// </summary>
        private void ShowDbConnectionError()
        {
            this.Invoke(new Action(() =>
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "فشل الاتصال بقاعدة البيانات. يرجى التحقق من إعدادات الاتصال والمحاولة مرة أخرى.",
                    "خطأ الاتصال بقاعدة البيانات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.Exit();
            }));
        }

        /// <summary>
        /// عرض خطأ تهيئة قاعدة البيانات
        /// </summary>
        private void ShowDbInitializationError()
        {
            this.Invoke(new Action(() =>
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "فشل في تهيئة قاعدة البيانات. يرجى التحقق من صلاحيات المستخدم والمحاولة مرة أخرى.",
                    "خطأ تهيئة قاعدة البيانات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.Exit();
            }));
        }
    }
}