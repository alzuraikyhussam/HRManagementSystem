using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using HR.Core;
using HR.Models;
using HR.Models.DTOs;
using HR.UI.Forms.Company;
using HR.UI.Forms.Employee;
using HR.UI.Forms.Attendance;
using HR.UI.Forms.Leave;
using HR.UI.Forms.Payroll;
using HR.UI.Forms.Settings;
using HR.UI.Forms.Dashboard;

namespace HR.UI.Forms
{
    /// <summary>
    /// النموذج الرئيسي للتطبيق
    /// </summary>
    public partial class MainForm : XtraForm
    {
        // الحالة الحالية للمستخدم
        private User _currentUser;
        
        // النافذة النشطة حاليا
        private XtraForm _activeForm;
        
        // مؤقت لتحديث الوقت والتاريخ
        private Timer _dateTimeTimer;

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
            this.accordionControl.SelectedElement = accordionControlDashboard;
            this.accordionControl.ElementClick += AccordionControl_ElementClick;
            
            // إنشاء مؤقت لتحديث الوقت والتاريخ
            _dateTimeTimer = new Timer();
            _dateTimeTimer.Interval = 1000;
            _dateTimeTimer.Tick += DateTimeTimer_Tick;
            _dateTimeTimer.Start();
            
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
                
                // إيقاف المؤقت
                if (_dateTimeTimer != null)
                {
                    _dateTimeTimer.Stop();
                    _dateTimeTimer.Dispose();
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
                    
                    // تحديث الشعار إذا كان متوفراً
                    if (company.Logo != null && company.Logo.Length > 0)
                    {
                        using (var ms = new System.IO.MemoryStream(company.Logo))
                        {
                            this.pictureEditLogo.Image = Image.FromStream(ms);
                        }
                    }
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
                // عرض معلومات المستخدم في شريط العنوان
                labelControlUserName.Text = $"مرحباً، {_currentUser.FullName}";
                
                // تحديث صورة المستخدم إذا كانت متوفرة
                if (_currentUser.PhotoData != null && _currentUser.PhotoData.Length > 0)
                {
                    using (var ms = new System.IO.MemoryStream(_currentUser.PhotoData))
                    {
                        pictureEditUserImage.Image = Image.FromStream(ms);
                    }
                }
                
                // تحديث معلومات الموظف إذا كان مرتبطا بالمستخدم
                labelControlUserRole.Text = _currentUser.Role?.Name ?? "مستخدم";
            }
            else
            {
                labelControlUserName.Text = "مرحباً، الزائر";
                labelControlUserRole.Text = "زائر";
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
                    buttonNotifications.Text = $"الإشعارات ({notificationCount})";
                    buttonNotifications.ToolTip = $"لديك {notificationCount} إشعار جديد";
                    buttonNotifications.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    buttonNotifications.Text = "الإشعارات";
                    buttonNotifications.ToolTip = "لا توجد إشعارات جديدة";
                    buttonNotifications.Appearance.ForeColor = Color.Black;
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
            return 3; // قيمة تجريبية
        }

        /// <summary>
        /// حدث تيك تحديث الوقت والتاريخ
        /// </summary>
        private void DateTimeTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // تحديث الوقت والتاريخ
                DateTime now = DateTime.Now;
                labelControlDateTime.Text = $"{now:yyyy/MM/dd HH:mm:ss}";
            }
            catch
            {
                // تجاهل أي أخطاء في تحديث الوقت
            }
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
                
                // لوحة التحكم متاحة للجميع
                // accordionControlDashboard.Visible = true;
                
                // التحقق من صلاحيات الوحدات الأخرى
                accordionControlEmployees.Visible = SessionManager.HasPermission("Employees", "View");
                accordionControlAttendance.Visible = SessionManager.HasPermission("Attendance", "View");
                accordionControlLeaves.Visible = SessionManager.HasPermission("Leaves", "View");
                accordionControlPayroll.Visible = SessionManager.HasPermission("Payroll", "View");
                accordionControlReports.Visible = SessionManager.HasPermission("Reports", "View");
                accordionControlSettings.Visible = SessionManager.HasPermission("Settings", "View");
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تطبيق صلاحيات المستخدم");
            }
        }

        /// <summary>
        /// حدث النقر على عنصر في الأكورديون
        /// </summary>
        private void AccordionControl_ElementClick(object sender, ElementClickEventArgs e)
        {
            try
            {
                // التحقق من العنصر المحدد
                AccordionControlElement element = e.Element;
                
                if (element == accordionControlDashboard)
                {
                    ShowDashboard();
                }
                else if (element == accordionControlEmployees)
                {
                    ShowEmployeeManagement();
                }
                else if (element == accordionControlAttendance)
                {
                    ShowAttendanceManagement();
                }
                else if (element == accordionControlLeaves)
                {
                    ShowLeaveManagement();
                }
                else if (element == accordionControlPayroll)
                {
                    ShowPayrollManagement();
                }
                else if (element == accordionControlReports)
                {
                    ShowReportsManagement();
                }
                else if (element == accordionControlSettings)
                {
                    ShowSettings();
                }
                else if (element == accordionControlLogout)
                {
                    LogoutUser();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في معالجة حدث النقر على عنصر الأكورديون");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تنفيذ العملية: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// عرض لوحة التحكم
        /// </summary>
        private void ShowDashboard()
        {
            try
            {
                accordionControl.SelectedElement = accordionControlDashboard;
                
                // إنشاء نموذج لوحة المعلومات
                var dashboardForm = new Dashboard.DashboardForm();
                
                // فتح النموذج في منطقة العمل
                OpenForm(dashboardForm);
                
                // تحديث العنوان
                labelControlPageTitle.Text = "لوحة التحكم";
                
                // تحديث الأيقونة
                svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("dashboard");
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض لوحة التحكم");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء عرض لوحة التحكم: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// عرض إدارة الموظفين
        /// </summary>
        private void ShowEmployeeManagement()
        {
            // تحديد العنصر المحدد في الأكورديون
            accordionControl.SelectedElement = accordionControlEmployees;
            
            // تحديث العنوان
            labelControlPageTitle.Text = "إدارة الموظفين";
            
            // تحديث الأيقونة
            svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("employees");
            
            // ملاحظة: يجب إنشاء نموذج إدارة الموظفين لاحقا
            OpenForm(null);
        }

        /// <summary>
        /// عرض إدارة الحضور
        /// </summary>
        private void ShowAttendanceManagement()
        {
            // تحديد العنصر المحدد في الأكورديون
            accordionControl.SelectedElement = accordionControlAttendance;
            
            // تحديث العنوان
            labelControlPageTitle.Text = "إدارة الحضور والانصراف";
            
            // تحديث الأيقونة
            svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("attendance");
            
            // ملاحظة: يجب إنشاء نموذج إدارة الحضور لاحقا
            OpenForm(null);
        }

        /// <summary>
        /// عرض إدارة الإجازات
        /// </summary>
        private void ShowLeaveManagement()
        {
            // تحديد العنصر المحدد في الأكورديون
            accordionControl.SelectedElement = accordionControlLeaves;
            
            // تحديث العنوان
            labelControlPageTitle.Text = "إدارة الإجازات";
            
            // تحديث الأيقونة
            svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("leaves");
            
            // ملاحظة: يجب إنشاء نموذج إدارة الإجازات لاحقا
            OpenForm(null);
        }

        /// <summary>
        /// عرض إدارة الرواتب
        /// </summary>
        private void ShowPayrollManagement()
        {
            // تحديد العنصر المحدد في الأكورديون
            accordionControl.SelectedElement = accordionControlPayroll;
            
            // تحديث العنوان
            labelControlPageTitle.Text = "إدارة الرواتب";
            
            // تحديث الأيقونة
            svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("payroll");
            
            // ملاحظة: يجب إنشاء نموذج إدارة الرواتب لاحقا
            OpenForm(null);
        }

        /// <summary>
        /// عرض إدارة التقارير
        /// </summary>
        private void ShowReportsManagement()
        {
            // تحديد العنصر المحدد في الأكورديون
            accordionControl.SelectedElement = accordionControlReports;
            
            // تحديث العنوان
            labelControlPageTitle.Text = "التقارير";
            
            // تحديث الأيقونة
            svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("reports");
            
            // ملاحظة: يجب إنشاء نموذج إدارة التقارير لاحقا
            OpenForm(null);
        }

        /// <summary>
        /// عرض الإعدادات
        /// </summary>
        private void ShowSettings()
        {
            // تحديد العنصر المحدد في الأكورديون
            accordionControl.SelectedElement = accordionControlSettings;
            
            // تحديث العنوان
            labelControlPageTitle.Text = "إعدادات النظام";
            
            // تحديث الأيقونة
            svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("settings");
            
            // ملاحظة: يجب إنشاء نموذج الإعدادات لاحقا
            OpenForm(null);
        }

        /// <summary>
        /// تسجيل الخروج من النظام
        /// </summary>
        private void LogoutUser()
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
                    // إيقاف المؤقت
                    if (_dateTimeTimer != null)
                    {
                        _dateTimeTimer.Stop();
                        _dateTimeTimer.Dispose();
                    }
                    
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
        /// حدث النقر على زر الإشعارات
        /// </summary>
        private void ButtonNotifications_Click(object sender, EventArgs e)
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
        private void ButtonUserProfile_Click(object sender, EventArgs e)
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
        /// حدث النقر على زر المساعدة
        /// </summary>
        private void ButtonHelp_Click(object sender, EventArgs e)
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

        /// <summary>
        /// حدث النقر على زر الإعدادات السريعة
        /// </summary>
        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            // عرض إعدادات سريعة
            try
            {
                XtraMessageBox.Show(
                    "ستظهر قائمة الإعدادات السريعة هنا.",
                    "الإعدادات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض الإعدادات السريعة");
            }
        }
    }
}