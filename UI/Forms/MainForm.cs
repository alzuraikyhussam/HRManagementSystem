using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HR.Core;
using HR.Models;
using HR.UI.Forms.Company;
using HR.UI.Forms.Employee;
using HR.UI.Forms.Attendance;
using HR.UI.Forms.Leave;
using HR.UI.Forms.Payroll;
using HR.UI.Forms.Settings;

namespace HR.UI.Forms
{
    /// <summary>
    /// النموذج الرئيسي للتطبيق
    /// </summary>
    public partial class MainForm : RibbonForm
    {
        // الحالة الحالية للمستخدم
        private User _currentUser;
        
        // النافذة النشطة حاليا
        private XtraForm _activeForm;

        /// <summary>
        /// تهيئة النموذج الرئيسي
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            
            // تهيئة المكونات
            InitializeUserLookAndFeel();
            
            // ضبط الإعدادات
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += MainForm_FormClosing;
            
            // ضبط مكونات DevExpress
            this.navigationPane.SelectedPageIndex = 0;
            this.navigationPane.SelectedPageChanged += NavigationPane_SelectedPageChanged;
            
            // بدء تشغيل المؤقتات
            timerDateTime.Start();
            
            // تطبيق الصلاحيات
            _currentUser = SessionManager.GetCurrentUser();
            DisplayUserInfo();
            ApplyPermissions();
            
            // اظهار لوحة التحكم
            ShowDashboard();
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // تحديث عنوان النموذج
            UpdateFormTitle();
            
            // تحديث معلومات الإشعارات
            UpdateNotifications();
        }

        /// <summary>
        /// حدث إغلاق النموذج
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // سؤال المستخدم قبل الإغلاق
                DialogResult result = XtraMessageBox.Show(
                    "هل أنت متأكد من أنك تريد إغلاق البرنامج؟",
                    "تأكيد الإغلاق",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                
                // تسجيل الخروج
                SessionManager.Logout();
                
                // إغلاق جميع الاتصالات مع أجهزة البصمة
                ZKTecoManager.CloseAllConnections();
            }
        }

        /// <summary>
        /// تحديث عنوان النموذج
        /// </summary>
        private void UpdateFormTitle()
        {
            try
            {
                Company company = SessionManager.GetCompanyData();
                
                if (company != null)
                {
                    this.Text = $"{company.Name} - نظام إدارة الموارد البشرية";
                }
                else
                {
                    this.Text = "نظام إدارة الموارد البشرية";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث عنوان النموذج");
                this.Text = "نظام إدارة الموارد البشرية";
            }
        }

        /// <summary>
        /// تحديث معلومات المستخدم
        /// </summary>
        private void DisplayUserInfo()
        {
            if (_currentUser != null)
            {
                barStaticItemUsername.Caption = $"المستخدم: {_currentUser.FullName}";
                
                // تحديث معلومات الموظف إذا كان مرتبطا بالمستخدم
                if (_currentUser.EmployeeID.HasValue)
                {
                    barButtonItemUserProfile.Enabled = true;
                }
                else
                {
                    barButtonItemUserProfile.Enabled = false;
                }
            }
            else
            {
                barStaticItemUsername.Caption = "المستخدم: غير معروف";
                barButtonItemUserProfile.Enabled = false;
            }
        }

        /// <summary>
        /// تحديث حالة الإشعارات
        /// </summary>
        private void UpdateNotifications()
        {
            try
            {
                // الحصول على عدد الإشعارات
                int notificationCount = GetNotificationCount();
                
                if (notificationCount > 0)
                {
                    barButtonItemNotifications.Caption = $"الإشعارات ({notificationCount})";
                    barButtonItemNotifications.Hint = $"لديك {notificationCount} إشعار جديد";
                }
                else
                {
                    barButtonItemNotifications.Caption = "الإشعارات";
                    barButtonItemNotifications.Hint = "لا توجد إشعارات جديدة";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث حالة الإشعارات");
            }
        }

        /// <summary>
        /// الحصول على عدد الإشعارات الجديدة
        /// </summary>
        private int GetNotificationCount()
        {
            // في التطبيق الفعلي، يجب الحصول على العدد من قاعدة البيانات
            return 0;
        }

        /// <summary>
        /// تهيئة مظهر التطبيق
        /// </summary>
        private void InitializeUserLookAndFeel()
        {
            try
            {
                // تحديد السمة الافتراضية
                UserLookAndFeel.Default.SetSkinStyle(SkinStyle.Office2019Colorful);
                
                // تحديد اتجاه التطبيق
                UserLookAndFeel.Default.UseWindowsXPTheme = false;
                Application.CurrentCulture = new System.Globalization.CultureInfo("ar-SA");
                Application.CurrentUICulture = new System.Globalization.CultureInfo("ar-SA");
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تهيئة مظهر التطبيق");
            }
        }

        /// <summary>
        /// تطبيق صلاحيات المستخدم
        /// </summary>
        private void ApplyPermissions()
        {
            try
            {
                // التحقق من صلاحيات المستخدم
                bool isAdmin = SessionManager.IsCurrentUserAdmin();
                
                // يمكن الوصول إلى كافة الوحدات بواسطة المسؤول
                if (isAdmin)
                {
                    return;
                }
                
                // تطبيق الصلاحيات على وحدات التنقل
                ApplyNavigationPermissions();
                
                // تطبيق الصلاحيات على الشريط
                ApplyRibbonPermissions();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تطبيق صلاحيات المستخدم");
            }
        }

        /// <summary>
        /// تطبيق الصلاحيات على وحدات التنقل
        /// </summary>
        private void ApplyNavigationPermissions()
        {
            // لوحة التحكم متاحة للجميع
            // navigationPaneDashboard.Visible = true;
            
            // التحقق من صلاحيات الوحدات الأخرى
            navigationPaneEmployees.Visible = SessionManager.HasPermission("Employees", "View");
            navigationPaneAttendance.Visible = SessionManager.HasPermission("Attendance", "View");
            navigationPaneLeaves.Visible = SessionManager.HasPermission("Leaves", "View");
            navigationPanePayroll.Visible = SessionManager.HasPermission("Payroll", "View");
            navigationPaneReports.Visible = SessionManager.HasPermission("Reports", "View");
            navigationPaneSettings.Visible = SessionManager.HasPermission("Settings", "View");
        }

        /// <summary>
        /// تطبيق الصلاحيات على الشريط
        /// </summary>
        private void ApplyRibbonPermissions()
        {
            // تحديث صلاحيات أزرار الشريط
            // ملاحظة: في التطبيق الفعلي، يجب التحقق من صلاحيات كل زر في الشريط
        }

        /// <summary>
        /// حدث تغيير الصفحة النشطة في لوحة التنقل
        /// </summary>
        private void NavigationPane_SelectedPageChanged(object sender, NavigationPaneSelectedPageChangedEventArgs e)
        {
            // التحقق من الصفحة المحددة
            NavigationPage selectedPage = e.Page;
            
            if (selectedPage == navigationPaneDashboard)
            {
                ShowDashboard();
            }
            else if (selectedPage == navigationPaneEmployees)
            {
                ShowEmployeeManagement();
            }
            else if (selectedPage == navigationPaneAttendance)
            {
                ShowAttendanceManagement();
            }
            else if (selectedPage == navigationPaneLeaves)
            {
                ShowLeaveManagement();
            }
            else if (selectedPage == navigationPanePayroll)
            {
                ShowPayrollManagement();
            }
            else if (selectedPage == navigationPaneReports)
            {
                ShowReportsManagement();
            }
            else if (selectedPage == navigationPaneSettings)
            {
                ShowSettings();
            }
        }

        /// <summary>
        /// عرض لوحة التحكم
        /// </summary>
        private void ShowDashboard()
        {
            OpenForm(null); // ملاحظة: يجب إنشاء نموذج لوحة التحكم لاحقا
        }

        /// <summary>
        /// عرض إدارة الموظفين
        /// </summary>
        private void ShowEmployeeManagement()
        {
            OpenForm(null); // ملاحظة: يجب إنشاء نموذج إدارة الموظفين لاحقا
        }

        /// <summary>
        /// عرض إدارة الحضور
        /// </summary>
        private void ShowAttendanceManagement()
        {
            OpenForm(null); // ملاحظة: يجب إنشاء نموذج إدارة الحضور لاحقا
        }

        /// <summary>
        /// عرض إدارة الإجازات
        /// </summary>
        private void ShowLeaveManagement()
        {
            OpenForm(null); // ملاحظة: يجب إنشاء نموذج إدارة الإجازات لاحقا
        }

        /// <summary>
        /// عرض إدارة الرواتب
        /// </summary>
        private void ShowPayrollManagement()
        {
            OpenForm(null); // ملاحظة: يجب إنشاء نموذج إدارة الرواتب لاحقا
        }

        /// <summary>
        /// عرض إدارة التقارير
        /// </summary>
        private void ShowReportsManagement()
        {
            OpenForm(null); // ملاحظة: يجب إنشاء نموذج إدارة التقارير لاحقا
        }

        /// <summary>
        /// عرض الإعدادات
        /// </summary>
        private void ShowSettings()
        {
            OpenForm(null); // ملاحظة: يجب إنشاء نموذج الإعدادات لاحقا
        }

        /// <summary>
        /// فتح نموذج في منطقة العمل
        /// </summary>
        /// <param name="form">النموذج المراد فتحه</param>
        private void OpenForm(XtraForm form)
        {
            // إغلاق النموذج النشط الحالي
            if (_activeForm != null)
            {
                _activeForm.Close();
                _activeForm.Dispose();
                _activeForm = null;
            }
            
            // إذا كان النموذج الجديد فارغ، فقط قم بتنظيف المنطقة
            if (form == null)
            {
                panelContent.Controls.Clear();
                return;
            }
            
            // تهيئة النموذج الجديد
            _activeForm = form;
            _activeForm.TopLevel = false;
            _activeForm.FormBorderStyle = FormBorderStyle.None;
            _activeForm.Dock = DockStyle.Fill;
            
            // إضافة النموذج إلى منطقة العمل
            panelContent.Controls.Clear();
            panelContent.Controls.Add(_activeForm);
            
            // عرض النموذج
            _activeForm.Show();
        }

        /// <summary>
        /// حدث تحديث الوقت والتاريخ
        /// </summary>
        private void TimerDateTime_Tick(object sender, EventArgs e)
        {
            // تحديث التاريخ والوقت
            try
            {
                DateTime now = DateTime.Now;
                barStaticItemDateTime.Caption = $"{now:yyyy/MM/dd HH:mm:ss}";
            }
            catch
            {
                // تجاهل أي أخطاء في تحديث الوقت
            }
        }

        /// <summary>
        /// حدث النقر على زر الإشعارات
        /// </summary>
        private void BarButtonItemNotifications_ItemClick(object sender, ItemClickEventArgs e)
        {
            // عرض الإشعارات
            try
            {
                XtraMessageBox.Show(
                    "ستظهر قائمة الإشعارات هنا.",
                    "الإشعارات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض الإشعارات");
            }
        }

        /// <summary>
        /// حدث النقر على زر الملف الشخصي
        /// </summary>
        private void BarButtonItemUserProfile_ItemClick(object sender, ItemClickEventArgs e)
        {
            // عرض الملف الشخصي للمستخدم
            try
            {
                XtraMessageBox.Show(
                    "ستظهر معلومات الملف الشخصي هنا.",
                    "الملف الشخصي",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض الملف الشخصي");
            }
        }

        /// <summary>
        /// حدث النقر على زر تسجيل الخروج
        /// </summary>
        private void BarButtonItemLogout_ItemClick(object sender, ItemClickEventArgs e)
        {
            // تأكيد تسجيل الخروج
            try
            {
                DialogResult result = XtraMessageBox.Show(
                    "هل أنت متأكد من أنك تريد تسجيل الخروج؟",
                    "تسجيل الخروج",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // تسجيل الخروج
                    SessionManager.Logout();
                    
                    // إغلاق النموذج الرئيسي
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    
                    // عرض نموذج تسجيل الدخول مرة أخرى
                    LoginForm loginForm = new LoginForm();
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        // إعادة فتح النموذج الرئيسي
                        MainForm mainForm = new MainForm();
                        mainForm.Show();
                    }
                    else
                    {
                        // إنهاء التطبيق
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تسجيل الخروج");
                Application.Exit();
            }
        }

        /// <summary>
        /// حدث النقر على زر تغيير كلمة المرور
        /// </summary>
        private void BarButtonItemChangePassword_ItemClick(object sender, ItemClickEventArgs e)
        {
            // عرض نموذج تغيير كلمة المرور
            try
            {
                XtraMessageBox.Show(
                    "سيظهر نموذج تغيير كلمة المرور هنا.",
                    "تغيير كلمة المرور",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض نموذج تغيير كلمة المرور");
            }
        }

        /// <summary>
        /// حدث النقر على زر حول البرنامج
        /// </summary>
        private void BarButtonItemAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            // عرض معلومات حول البرنامج
            try
            {
                XtraMessageBox.Show(
                    "نظام إدارة الموارد البشرية\nالإصدار: 1.0.0\n\nجميع الحقوق محفوظة © " + DateTime.Now.Year,
                    "حول البرنامج",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض معلومات حول البرنامج");
            }
        }
    }
}