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
                // عناصر التقارير الفرعية
                else if (element == accordionControlReportsEmployees)
                {
                    ShowSpecificReport("Employees");
                }
                else if (element == accordionControlReportsAttendance)
                {
                    ShowSpecificReport("Attendance");
                }
                else if (element == accordionControlReportsLeaves)
                {
                    ShowSpecificReport("Leave");
                }
                else if (element == accordionControlReportsPayroll)
                {
                    ShowSpecificReport("Payroll");
                }
                else if (element == accordionControlAttendanceDevices)
                {
                    // عرض واجهة أجهزة البصمة
                    ShowBiometricDevicesForm();
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
            try
            {
                // تحديد العنصر المحدد في الأكورديون
                accordionControl.SelectedElement = accordionControlEmployees;
                
                // تحديث العنوان
                labelControlPageTitle.Text = "إدارة الموظفين";
                
                // تحديث الأيقونة
                svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("employees");
                
                // إنشاء نموذج قائمة الموظفين
                var employeesListForm = new Employees.EmployeesListForm();
                
                // فتح النموذج في منطقة العمل
                OpenForm(employeesListForm);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض إدارة الموظفين");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء عرض إدارة الموظفين: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// عرض إدارة الحضور
        /// </summary>
        private void ShowAttendanceManagement()
        {
            try
            {
                // تحديد العنصر المحدد في الأكورديون
                accordionControl.SelectedElement = accordionControlAttendance;
                
                // تحديث العنوان
                labelControlPageTitle.Text = "إدارة الحضور والانصراف";
                
                // تحديث الأيقونة
                svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("attendance");
                
                // إضافة أزرار الإجراءات السريعة
                ClearActionButtons();
                
                // إضافة زر لإدارة أجهزة البصمة
                var btnBiometricDevices = CreateActionButton("أجهزة البصمة", "biometric");
                btnBiometricDevices.Click += (s, e) => ShowBiometricDevicesForm();
                panelControlActions.Controls.Add(btnBiometricDevices);
                
                // إنشاء نموذج إدارة الحضور
                var attendanceRecordsForm = new Attendance.AttendanceRecordsForm();
                
                // فتح النموذج في منطقة العمل
                OpenForm(attendanceRecordsForm);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض إدارة الحضور");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء عرض إدارة الحضور: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// عرض إدارة الإجازات
        /// </summary>
        private void ShowLeaveManagement()
        {
            try
            {
                // تحديد العنصر المحدد في الأكورديون
                accordionControl.SelectedElement = accordionControlLeaves;
                
                // تحديث العنوان
                labelControlPageTitle.Text = "إدارة الإجازات";
                
                // تحديث الأيقونة
                svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("leaves");
                
                // هنا يمكن إضافة أو إنشاء نموذج إدارة الإجازات
                // مؤقتاً، نعرض رسالة عن عدم اكتمال هذه الميزة
                XtraMessageBox.Show(
                    "نموذج إدارة الإجازات قيد التطوير",
                    "إدارة الإجازات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                // تنظيف منطقة المحتوى
                OpenForm(null);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض إدارة الإجازات");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء عرض إدارة الإجازات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// عرض إدارة الرواتب
        /// </summary>
        private void ShowPayrollManagement()
        {
            try
            {
                // تحديد العنصر المحدد في الأكورديون
                accordionControl.SelectedElement = accordionControlPayroll;
                
                // تحديث العنوان
                labelControlPageTitle.Text = "إدارة الرواتب";
                
                // تحديث الأيقونة
                svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("payroll");
                
                // هنا يمكن إضافة أو إنشاء نموذج إدارة الرواتب
                // مؤقتاً، نعرض رسالة عن عدم اكتمال هذه الميزة
                XtraMessageBox.Show(
                    "نموذج إدارة الرواتب قيد التطوير",
                    "إدارة الرواتب",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                // تنظيف منطقة المحتوى
                OpenForm(null);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض إدارة الرواتب");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء عرض إدارة الرواتب: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// عرض إدارة التقارير (لا تقوم بفتح أي نموذج)
        /// </summary>
        private void ShowReportsManagement()
        {
            try
            {
                // تحديد العنصر المحدد في الأكورديون
                accordionControl.SelectedElement = accordionControlReports;
                
                // تحديث العنوان
                labelControlPageTitle.Text = "التقارير";
                
                // تحديث الأيقونة
                svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("reports");
                
                // إعداد وحدة التقارير في القائمة
                SetupReportsModule();
                
                // لا نفتح أي نموذج هنا حتى يختار المستخدم التقرير المطلوب من القائمة الجانبية
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض إدارة التقارير");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء عرض إدارة التقارير: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// عرض نموذج أجهزة البصمة
        /// </summary>
        private void ShowBiometricDevicesForm()
        {
            try
            {
                // تحديث العنوان
                labelControlPageTitle.Text = "إدارة أجهزة البصمة";
                
                // تحديث الأيقونة
                svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("biometric");
                
                // إنشاء نموذج أجهزة البصمة
                var biometricDevicesForm = new UI.Forms.Attendance.BiometricDevicesForm();
                
                // فتح النموذج في منطقة العمل
                OpenForm(biometricDevicesForm);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض نموذج أجهزة البصمة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء عرض نموذج أجهزة البصمة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// إنشاء زر إجراء سريع
        /// </summary>
        /// <param name="text">نص الزر</param>
        /// <param name="iconName">اسم الأيقونة</param>
        /// <returns>الزر الجديد</returns>
        private SimpleButton CreateActionButton(string text, string iconName)
        {
            SimpleButton button = new SimpleButton();
            button.Text = text;
            button.ImageOptions.SvgImage = svgImageCollection.GetImageByName(iconName);
            button.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Regular);
            button.Size = new Size(120, 40);
            button.Padding = new Padding(5);
            button.Margin = new Padding(5);
            
            return button;
        }
        
        /// <summary>
        /// إزالة كافة أزرار الإجراءات السريعة
        /// </summary>
        private void ClearActionButtons()
        {
            // التحقق من وجود لوحة الإجراءات قبل محاولة مسح محتواها
            if (panelControlActions != null)
            {
                panelControlActions.Controls.Clear();
            }
            else
            {
                // إنشاء لوحة الإجراءات إذا لم تكن موجودة
                panelControlActions = new DevExpress.XtraEditors.PanelControl();
                panelControlActions.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
                panelControlActions.Appearance.Options.UseBackColor = true;
                panelControlActions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                panelControlActions.Dock = System.Windows.Forms.DockStyle.Top;
                panelControlActions.Location = new System.Drawing.Point(0, 50);
                panelControlActions.Name = "panelControlActions";
                panelControlActions.Size = new System.Drawing.Size(980, 68);
                
                // إضافة اللوحة إلى النموذج
                this.Controls.Add(panelControlActions);
                panelTitle.BringToFront();
                panelControlActions.BringToFront();
                panelContent.BringToFront();
            }
        }
        
        /// <summary>
        /// عرض تقرير محدد من قائمة التقارير
        /// </summary>
        /// <param name="reportType">نوع التقرير</param>
        private void ShowSpecificReport(string reportType)
        {
            try
            {
                // تحديث العنوان حسب نوع التقرير
                switch (reportType)
                {
                    case "Employees":
                        labelControlPageTitle.Text = "تقارير الموظفين";
                        break;
                    case "Attendance":
                        labelControlPageTitle.Text = "تقارير الحضور والانصراف";
                        break;
                    case "Leave":
                        labelControlPageTitle.Text = "تقارير الإجازات";
                        break;
                    case "Payroll":
                        labelControlPageTitle.Text = "تقارير الرواتب";
                        break;
                    default:
                        labelControlPageTitle.Text = "التقارير";
                        break;
                }
                
                // تحديث الأيقونة
                svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("reports");
                
                // إنشاء النموذج المناسب حسب نوع التقرير
                Form reportForm = null;
                
                switch (reportType)
                {
                    case "Employees":
                        reportForm = new UI.Forms.Reports.EmployeeReportForm();
                        break;
                    case "Attendance":
                        reportForm = new UI.Forms.Reports.AttendanceReportForm();
                        break;
                    case "Leave":
                        reportForm = new UI.Forms.Reports.LeaveReportForm();
                        break;
                    case "Payroll":
                        reportForm = new UI.Forms.Reports.PayrollReportForm();
                        break;
                    case "Operations":
                        reportForm = new UI.Forms.Reports.OperationsReportForm();
                        break;
                    case "CustomGenerator":
                        reportForm = new UI.Forms.Reports.CustomReportGeneratorForm();
                        break;
                    default:
                        // حالة غير معروفة
                        XtraMessageBox.Show(
                            "نوع التقرير غير مدعوم بعد.",
                            "تنبيه",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                }
                
                // فتح النموذج في منطقة العمل
                OpenForm(reportForm as XtraForm);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض التقرير المحدد");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء عرض التقرير: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// عرض الإعدادات
        /// </summary>
        private void ShowSettings()
        {
            try
            {
                // تحديد العنصر المحدد في الأكورديون
                accordionControl.SelectedElement = accordionControlSettings;
                
                // تحديث العنوان
                labelControlPageTitle.Text = "إعدادات النظام";
                
                // تحديث الأيقونة
                svgImageBoxPageIcon.SvgImage = svgImageCollection.GetImageByName("settings");
                
                // هنا يمكن إضافة أو إنشاء نموذج إعدادات النظام
                // مؤقتاً، نعرض رسالة عن عدم اكتمال هذه الميزة
                XtraMessageBox.Show(
                    "نموذج إعدادات النظام قيد التطوير",
                    "إعدادات النظام",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                // تنظيف منطقة المحتوى
                OpenForm(null);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض إعدادات النظام");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء عرض إعدادات النظام: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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