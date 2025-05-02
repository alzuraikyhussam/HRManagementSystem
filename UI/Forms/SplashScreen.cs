using System;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;
using HR.Core;

namespace HR.UI.Forms
{
    /// <summary>
    /// نموذج شاشة البداية للنظام
    /// </summary>
    public partial class SplashScreen : SplashScreen
    {
        /// <summary>
        /// تهيئة نموذج شاشة البداية
        /// </summary>
        public SplashScreen()
        {
            InitializeComponent();
            this.labelCopyright.Text = $"Copyright © {DateTime.Now.Year}";
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
                this.labelStatus.Text = operationDescription;
            
            this.progressBarControl.EditValue = progress;
            this.progressBarControl.Update();
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
        /// عرض شاشة البداية وتهيئة النظام
        /// </summary>
        private void SplashScreen_Load(object sender, EventArgs e)
        {
            // تشغيل مهمة تهيئة النظام في خلفية
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(InitializeSystem));
        }

        /// <summary>
        /// تهيئة النظام في خلفية
        /// </summary>
        private void InitializeSystem(object state)
        {
            try
            {
                // تهيئة مدير السجلات
                SetStatusText("تهيئة سجلات النظام...");
                LogManager.Initialize();
                SetProgress(15);

                // التحقق من الاتصال بقاعدة البيانات
                SetStatusText("الاتصال بقاعدة البيانات...");
                if (!ConnectionManager.Initialize())
                {
                    ShowDbConnectionError();
                    return;
                }
                SetProgress(30);

                // التحقق من تكوين قاعدة البيانات
                SetStatusText("التحقق من تكوين قاعدة البيانات...");
                if (!ConnectionManager.IsSystemConfigured())
                {
                    // تهيئة قاعدة البيانات إذا لم تكن موجودة
                    SetStatusText("تهيئة قاعدة البيانات...");
                    if (!ConnectionManager.InitializeDatabase())
                    {
                        ShowDbInitializationError();
                        return;
                    }
                }
                SetProgress(60);

                // تهيئة مدير الجلسات
                SetStatusText("تهيئة بيانات النظام...");
                SessionManager.Initialize();
                SetProgress(80);

                // تحميل اللغة والإعدادات
                SetStatusText("تحميل إعدادات النظام...");
                LoadSystemSettings();
                SetProgress(100);

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
        /// تحديث نص حالة التقدم
        /// </summary>
        private void SetStatusText(string text)
        {
            this.Invoke(new Action(() =>
            {
                this.labelStatus.Text = text;
            }));
        }

        /// <summary>
        /// تحديث قيمة التقدم
        /// </summary>
        private void SetProgress(int progress)
        {
            this.Invoke(new Action(() =>
            {
                this.progressBarControl.EditValue = progress;
            }));
        }

        /// <summary>
        /// تحميل إعدادات النظام
        /// </summary>
        private void LoadSystemSettings()
        {
            // تطبيق سمة النظام والإعدادات الأخرى
            try
            {
                var settingsRepository = new DataAccess.SystemSettingsRepository();
                // تحميل الإعدادات من قاعدة البيانات
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