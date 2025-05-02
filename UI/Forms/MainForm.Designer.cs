namespace HR.UI.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barBtnCompanyInfo = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDepartments = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPositions = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEmployeesList = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnNewEmployee = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEmployeeDocuments = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEmployeeTransfers = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAttendanceDevices = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAttendanceManagement = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAttendanceReports = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnLeaveTypes = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnLeaveRequests = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnLeaveBalance = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnWorkHours = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnWorkShifts = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnUsers = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnRoles = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDatabaseSettings = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSystemSettings = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnBackupRestore = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnActivityLog = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItemUserInfo = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemDateTime = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemVersion = new DevExpress.XtraBars.BarStaticItem();
            this.barBtnChangePassword = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnLogout = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnExit = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDashboard = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageCompany = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupCompanyInfo = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupCompanyStructure = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageEmployees = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupEmployees = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupEmployeeHistory = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageAttendance = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupAttendanceManagement = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupAttendanceReports = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageLeaves = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupLeaves = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageWorkSchedules = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupWorkSchedules = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageSecurity = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupSecurity = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageSettings = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupSettings = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.xtraTabbedMdiManager = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.barBtnCompanyInfo,
            this.barBtnDepartments,
            this.barBtnPositions,
            this.barBtnEmployeesList,
            this.barBtnNewEmployee,
            this.barBtnEmployeeDocuments,
            this.barBtnEmployeeTransfers,
            this.barBtnAttendanceDevices,
            this.barBtnAttendanceManagement,
            this.barBtnAttendanceReports,
            this.barBtnLeaveTypes,
            this.barBtnLeaveRequests,
            this.barBtnLeaveBalance,
            this.barBtnWorkHours,
            this.barBtnWorkShifts,
            this.barBtnUsers,
            this.barBtnRoles,
            this.barBtnDatabaseSettings,
            this.barBtnSystemSettings,
            this.barBtnBackupRestore,
            this.barBtnActivityLog,
            this.barStaticItemUserInfo,
            this.barStaticItemDateTime,
            this.barStaticItemVersion,
            this.barBtnChangePassword,
            this.barBtnLogout,
            this.barBtnExit,
            this.barBtnDashboard});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 29;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageCompany,
            this.ribbonPageEmployees,
            this.ribbonPageAttendance,
            this.ribbonPageLeaves,
            this.ribbonPageWorkSchedules,
            this.ribbonPageSecurity,
            this.ribbonPageSettings});
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            this.ribbon.Size = new System.Drawing.Size(1100, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barBtnCompanyInfo
            // 
            this.barBtnCompanyInfo.Caption = "بيانات الشركة";
            this.barBtnCompanyInfo.Id = 1;
            this.barBtnCompanyInfo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnCompanyInfo.ImageOptions.Image")));
            this.barBtnCompanyInfo.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnCompanyInfo.ImageOptions.LargeImage")));
            this.barBtnCompanyInfo.Name = "barBtnCompanyInfo";
            this.barBtnCompanyInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnCompanyInfo_ItemClick);
            // 
            // barBtnDepartments
            // 
            this.barBtnDepartments.Caption = "الأقسام";
            this.barBtnDepartments.Id = 2;
            this.barBtnDepartments.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnDepartments.ImageOptions.Image")));
            this.barBtnDepartments.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnDepartments.ImageOptions.LargeImage")));
            this.barBtnDepartments.Name = "barBtnDepartments";
            this.barBtnDepartments.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDepartments_ItemClick);
            // 
            // barBtnPositions
            // 
            this.barBtnPositions.Caption = "المسميات الوظيفية";
            this.barBtnPositions.Id = 3;
            this.barBtnPositions.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnPositions.ImageOptions.Image")));
            this.barBtnPositions.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnPositions.ImageOptions.LargeImage")));
            this.barBtnPositions.Name = "barBtnPositions";
            this.barBtnPositions.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnPositions_ItemClick);
            // 
            // barBtnEmployeesList
            // 
            this.barBtnEmployeesList.Caption = "قائمة الموظفين";
            this.barBtnEmployeesList.Id = 4;
            this.barBtnEmployeesList.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnEmployeesList.ImageOptions.Image")));
            this.barBtnEmployeesList.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnEmployeesList.ImageOptions.LargeImage")));
            this.barBtnEmployeesList.Name = "barBtnEmployeesList";
            this.barBtnEmployeesList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEmployeesList_ItemClick);
            // 
            // barBtnNewEmployee
            // 
            this.barBtnNewEmployee.Caption = "موظف جديد";
            this.barBtnNewEmployee.Id = 5;
            this.barBtnNewEmployee.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnNewEmployee.ImageOptions.Image")));
            this.barBtnNewEmployee.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnNewEmployee.ImageOptions.LargeImage")));
            this.barBtnNewEmployee.Name = "barBtnNewEmployee";
            this.barBtnNewEmployee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnNewEmployee_ItemClick);
            // 
            // barBtnEmployeeDocuments
            // 
            this.barBtnEmployeeDocuments.Caption = "مستندات الموظفين";
            this.barBtnEmployeeDocuments.Id = 6;
            this.barBtnEmployeeDocuments.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnEmployeeDocuments.ImageOptions.Image")));
            this.barBtnEmployeeDocuments.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnEmployeeDocuments.ImageOptions.LargeImage")));
            this.barBtnEmployeeDocuments.Name = "barBtnEmployeeDocuments";
            this.barBtnEmployeeDocuments.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEmployeeDocuments_ItemClick);
            // 
            // barBtnEmployeeTransfers
            // 
            this.barBtnEmployeeTransfers.Caption = "النقل والترقيات";
            this.barBtnEmployeeTransfers.Id = 7;
            this.barBtnEmployeeTransfers.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnEmployeeTransfers.ImageOptions.Image")));
            this.barBtnEmployeeTransfers.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnEmployeeTransfers.ImageOptions.LargeImage")));
            this.barBtnEmployeeTransfers.Name = "barBtnEmployeeTransfers";
            this.barBtnEmployeeTransfers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEmployeeTransfers_ItemClick);
            // 
            // barBtnAttendanceDevices
            // 
            this.barBtnAttendanceDevices.Caption = "أجهزة البصمة";
            this.barBtnAttendanceDevices.Id = 8;
            this.barBtnAttendanceDevices.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnAttendanceDevices.ImageOptions.Image")));
            this.barBtnAttendanceDevices.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnAttendanceDevices.ImageOptions.LargeImage")));
            this.barBtnAttendanceDevices.Name = "barBtnAttendanceDevices";
            this.barBtnAttendanceDevices.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAttendanceDevices_ItemClick);
            // 
            // barBtnAttendanceManagement
            // 
            this.barBtnAttendanceManagement.Caption = "إدارة الحضور والانصراف";
            this.barBtnAttendanceManagement.Id = 9;
            this.barBtnAttendanceManagement.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnAttendanceManagement.ImageOptions.Image")));
            this.barBtnAttendanceManagement.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnAttendanceManagement.ImageOptions.LargeImage")));
            this.barBtnAttendanceManagement.Name = "barBtnAttendanceManagement";
            this.barBtnAttendanceManagement.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAttendanceManagement_ItemClick);
            // 
            // barBtnAttendanceReports
            // 
            this.barBtnAttendanceReports.Caption = "تقارير الحضور";
            this.barBtnAttendanceReports.Id = 10;
            this.barBtnAttendanceReports.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnAttendanceReports.ImageOptions.Image")));
            this.barBtnAttendanceReports.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnAttendanceReports.ImageOptions.LargeImage")));
            this.barBtnAttendanceReports.Name = "barBtnAttendanceReports";
            this.barBtnAttendanceReports.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAttendanceReports_ItemClick);
            // 
            // barBtnLeaveTypes
            // 
            this.barBtnLeaveTypes.Caption = "أنواع الإجازات";
            this.barBtnLeaveTypes.Id = 11;
            this.barBtnLeaveTypes.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnLeaveTypes.ImageOptions.Image")));
            this.barBtnLeaveTypes.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnLeaveTypes.ImageOptions.LargeImage")));
            this.barBtnLeaveTypes.Name = "barBtnLeaveTypes";
            this.barBtnLeaveTypes.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnLeaveTypes_ItemClick);
            // 
            // barBtnLeaveRequests
            // 
            this.barBtnLeaveRequests.Caption = "طلبات الإجازات";
            this.barBtnLeaveRequests.Id = 12;
            this.barBtnLeaveRequests.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnLeaveRequests.ImageOptions.Image")));
            this.barBtnLeaveRequests.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnLeaveRequests.ImageOptions.LargeImage")));
            this.barBtnLeaveRequests.Name = "barBtnLeaveRequests";
            this.barBtnLeaveRequests.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnLeaveRequests_ItemClick);
            // 
            // barBtnLeaveBalance
            // 
            this.barBtnLeaveBalance.Caption = "أرصدة الإجازات";
            this.barBtnLeaveBalance.Id = 13;
            this.barBtnLeaveBalance.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnLeaveBalance.ImageOptions.Image")));
            this.barBtnLeaveBalance.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnLeaveBalance.ImageOptions.LargeImage")));
            this.barBtnLeaveBalance.Name = "barBtnLeaveBalance";
            this.barBtnLeaveBalance.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnLeaveBalance_ItemClick);
            // 
            // barBtnWorkHours
            // 
            this.barBtnWorkHours.Caption = "فترات العمل";
            this.barBtnWorkHours.Id = 14;
            this.barBtnWorkHours.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnWorkHours.ImageOptions.Image")));
            this.barBtnWorkHours.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnWorkHours.ImageOptions.LargeImage")));
            this.barBtnWorkHours.Name = "barBtnWorkHours";
            this.barBtnWorkHours.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnWorkHours_ItemClick);
            // 
            // barBtnWorkShifts
            // 
            this.barBtnWorkShifts.Caption = "مناوبات العمل";
            this.barBtnWorkShifts.Id = 15;
            this.barBtnWorkShifts.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnWorkShifts.ImageOptions.Image")));
            this.barBtnWorkShifts.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnWorkShifts.ImageOptions.LargeImage")));
            this.barBtnWorkShifts.Name = "barBtnWorkShifts";
            this.barBtnWorkShifts.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnWorkShifts_ItemClick);
            // 
            // barBtnUsers
            // 
            this.barBtnUsers.Caption = "المستخدمين";
            this.barBtnUsers.Id = 16;
            this.barBtnUsers.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnUsers.ImageOptions.Image")));
            this.barBtnUsers.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnUsers.ImageOptions.LargeImage")));
            this.barBtnUsers.Name = "barBtnUsers";
            this.barBtnUsers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnUsers_ItemClick);
            // 
            // barBtnRoles
            // 
            this.barBtnRoles.Caption = "الأدوار والصلاحيات";
            this.barBtnRoles.Id = 17;
            this.barBtnRoles.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnRoles.ImageOptions.Image")));
            this.barBtnRoles.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnRoles.ImageOptions.LargeImage")));
            this.barBtnRoles.Name = "barBtnRoles";
            this.barBtnRoles.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnRoles_ItemClick);
            // 
            // barBtnDatabaseSettings
            // 
            this.barBtnDatabaseSettings.Caption = "إعدادات قاعدة البيانات";
            this.barBtnDatabaseSettings.Id = 18;
            this.barBtnDatabaseSettings.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnDatabaseSettings.ImageOptions.Image")));
            this.barBtnDatabaseSettings.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnDatabaseSettings.ImageOptions.LargeImage")));
            this.barBtnDatabaseSettings.Name = "barBtnDatabaseSettings";
            this.barBtnDatabaseSettings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDatabaseSettings_ItemClick);
            // 
            // barBtnSystemSettings
            // 
            this.barBtnSystemSettings.Caption = "إعدادات النظام";
            this.barBtnSystemSettings.Id = 19;
            this.barBtnSystemSettings.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnSystemSettings.ImageOptions.Image")));
            this.barBtnSystemSettings.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnSystemSettings.ImageOptions.LargeImage")));
            this.barBtnSystemSettings.Name = "barBtnSystemSettings";
            this.barBtnSystemSettings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSystemSettings_ItemClick);
            // 
            // barBtnBackupRestore
            // 
            this.barBtnBackupRestore.Caption = "النسخ الاحتياطي والاستعادة";
            this.barBtnBackupRestore.Id = 20;
            this.barBtnBackupRestore.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnBackupRestore.ImageOptions.Image")));
            this.barBtnBackupRestore.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnBackupRestore.ImageOptions.LargeImage")));
            this.barBtnBackupRestore.Name = "barBtnBackupRestore";
            this.barBtnBackupRestore.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnBackupRestore_ItemClick);
            // 
            // barBtnActivityLog
            // 
            this.barBtnActivityLog.Caption = "سجل النشاطات";
            this.barBtnActivityLog.Id = 21;
            this.barBtnActivityLog.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnActivityLog.ImageOptions.Image")));
            this.barBtnActivityLog.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnActivityLog.ImageOptions.LargeImage")));
            this.barBtnActivityLog.Name = "barBtnActivityLog";
            this.barBtnActivityLog.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnActivityLog_ItemClick);
            // 
            // barStaticItemUserInfo
            // 
            this.barStaticItemUserInfo.Caption = "المستخدم:";
            this.barStaticItemUserInfo.Id = 22;
            this.barStaticItemUserInfo.Name = "barStaticItemUserInfo";
            // 
            // barStaticItemDateTime
            // 
            this.barStaticItemDateTime.Caption = "التاريخ والوقت:";
            this.barStaticItemDateTime.Id = 23;
            this.barStaticItemDateTime.Name = "barStaticItemDateTime";
            // 
            // barStaticItemVersion
            // 
            this.barStaticItemVersion.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barStaticItemVersion.Caption = "الإصدار: 1.0.0";
            this.barStaticItemVersion.Id = 24;
            this.barStaticItemVersion.Name = "barStaticItemVersion";
            // 
            // barBtnChangePassword
            // 
            this.barBtnChangePassword.Caption = "تغيير كلمة المرور";
            this.barBtnChangePassword.Id = 25;
            this.barBtnChangePassword.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnChangePassword.ImageOptions.Image")));
            this.barBtnChangePassword.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnChangePassword.ImageOptions.LargeImage")));
            this.barBtnChangePassword.Name = "barBtnChangePassword";
            this.barBtnChangePassword.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnChangePassword_ItemClick);
            // 
            // barBtnLogout
            // 
            this.barBtnLogout.Caption = "تسجيل الخروج";
            this.barBtnLogout.Id = 26;
            this.barBtnLogout.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnLogout.ImageOptions.Image")));
            this.barBtnLogout.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnLogout.ImageOptions.LargeImage")));
            this.barBtnLogout.Name = "barBtnLogout";
            this.barBtnLogout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnLogout_ItemClick);
            // 
            // barBtnExit
            // 
            this.barBtnExit.Caption = "خروج";
            this.barBtnExit.Id = 27;
            this.barBtnExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnExit.ImageOptions.Image")));
            this.barBtnExit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnExit.ImageOptions.LargeImage")));
            this.barBtnExit.Name = "barBtnExit";
            this.barBtnExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnExit_ItemClick);
            // 
            // barBtnDashboard
            // 
            this.barBtnDashboard.Caption = "لوحة المعلومات";
            this.barBtnDashboard.Id = 28;
            this.barBtnDashboard.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnDashboard.ImageOptions.Image")));
            this.barBtnDashboard.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnDashboard.ImageOptions.LargeImage")));
            this.barBtnDashboard.Name = "barBtnDashboard";
            this.barBtnDashboard.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDashboard_ItemClick);
            // 
            // ribbonPageCompany
            // 
            this.ribbonPageCompany.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupCompanyInfo,
            this.ribbonPageGroupCompanyStructure});
            this.ribbonPageCompany.Name = "ribbonPageCompany";
            this.ribbonPageCompany.Text = "الشركة";
            // 
            // ribbonPageGroupCompanyInfo
            // 
            this.ribbonPageGroupCompanyInfo.ItemLinks.Add(this.barBtnCompanyInfo);
            this.ribbonPageGroupCompanyInfo.ItemLinks.Add(this.barBtnDashboard);
            this.ribbonPageGroupCompanyInfo.Name = "ribbonPageGroupCompanyInfo";
            this.ribbonPageGroupCompanyInfo.Text = "معلومات الشركة";
            // 
            // ribbonPageGroupCompanyStructure
            // 
            this.ribbonPageGroupCompanyStructure.ItemLinks.Add(this.barBtnDepartments);
            this.ribbonPageGroupCompanyStructure.ItemLinks.Add(this.barBtnPositions);
            this.ribbonPageGroupCompanyStructure.Name = "ribbonPageGroupCompanyStructure";
            this.ribbonPageGroupCompanyStructure.Text = "الهيكل التنظيمي";
            // 
            // ribbonPageEmployees
            // 
            this.ribbonPageEmployees.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupEmployees,
            this.ribbonPageGroupEmployeeHistory});
            this.ribbonPageEmployees.Name = "ribbonPageEmployees";
            this.ribbonPageEmployees.Text = "الموظفين";
            // 
            // ribbonPageGroupEmployees
            // 
            this.ribbonPageGroupEmployees.ItemLinks.Add(this.barBtnEmployeesList);
            this.ribbonPageGroupEmployees.ItemLinks.Add(this.barBtnNewEmployee);
            this.ribbonPageGroupEmployees.Name = "ribbonPageGroupEmployees";
            this.ribbonPageGroupEmployees.Text = "الموظفين";
            // 
            // ribbonPageGroupEmployeeHistory
            // 
            this.ribbonPageGroupEmployeeHistory.ItemLinks.Add(this.barBtnEmployeeDocuments);
            this.ribbonPageGroupEmployeeHistory.ItemLinks.Add(this.barBtnEmployeeTransfers);
            this.ribbonPageGroupEmployeeHistory.Name = "ribbonPageGroupEmployeeHistory";
            this.ribbonPageGroupEmployeeHistory.Text = "سجلات الموظفين";
            // 
            // ribbonPageAttendance
            // 
            this.ribbonPageAttendance.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupAttendanceManagement,
            this.ribbonPageGroupAttendanceReports});
            this.ribbonPageAttendance.Name = "ribbonPageAttendance";
            this.ribbonPageAttendance.Text = "الحضور والانصراف";
            // 
            // ribbonPageGroupAttendanceManagement
            // 
            this.ribbonPageGroupAttendanceManagement.ItemLinks.Add(this.barBtnAttendanceDevices);
            this.ribbonPageGroupAttendanceManagement.ItemLinks.Add(this.barBtnAttendanceManagement);
            this.ribbonPageGroupAttendanceManagement.Name = "ribbonPageGroupAttendanceManagement";
            this.ribbonPageGroupAttendanceManagement.Text = "إدارة الحضور";
            // 
            // ribbonPageGroupAttendanceReports
            // 
            this.ribbonPageGroupAttendanceReports.ItemLinks.Add(this.barBtnAttendanceReports);
            this.ribbonPageGroupAttendanceReports.Name = "ribbonPageGroupAttendanceReports";
            this.ribbonPageGroupAttendanceReports.Text = "التقارير";
            // 
            // ribbonPageLeaves
            // 
            this.ribbonPageLeaves.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupLeaves});
            this.ribbonPageLeaves.Name = "ribbonPageLeaves";
            this.ribbonPageLeaves.Text = "الإجازات";
            // 
            // ribbonPageGroupLeaves
            // 
            this.ribbonPageGroupLeaves.ItemLinks.Add(this.barBtnLeaveTypes);
            this.ribbonPageGroupLeaves.ItemLinks.Add(this.barBtnLeaveRequests);
            this.ribbonPageGroupLeaves.ItemLinks.Add(this.barBtnLeaveBalance);
            this.ribbonPageGroupLeaves.Name = "ribbonPageGroupLeaves";
            this.ribbonPageGroupLeaves.Text = "الإجازات";
            // 
            // ribbonPageWorkSchedules
            // 
            this.ribbonPageWorkSchedules.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupWorkSchedules});
            this.ribbonPageWorkSchedules.Name = "ribbonPageWorkSchedules";
            this.ribbonPageWorkSchedules.Text = "مواعيد العمل";
            // 
            // ribbonPageGroupWorkSchedules
            // 
            this.ribbonPageGroupWorkSchedules.ItemLinks.Add(this.barBtnWorkHours);
            this.ribbonPageGroupWorkSchedules.ItemLinks.Add(this.barBtnWorkShifts);
            this.ribbonPageGroupWorkSchedules.Name = "ribbonPageGroupWorkSchedules";
            this.ribbonPageGroupWorkSchedules.Text = "مواعيد العمل";
            // 
            // ribbonPageSecurity
            // 
            this.ribbonPageSecurity.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupSecurity});
            this.ribbonPageSecurity.Name = "ribbonPageSecurity";
            this.ribbonPageSecurity.Text = "الأمان";
            // 
            // ribbonPageGroupSecurity
            // 
            this.ribbonPageGroupSecurity.ItemLinks.Add(this.barBtnUsers);
            this.ribbonPageGroupSecurity.ItemLinks.Add(this.barBtnRoles);
            this.ribbonPageGroupSecurity.ItemLinks.Add(this.barBtnChangePassword);
            this.ribbonPageGroupSecurity.ItemLinks.Add(this.barBtnLogout);
            this.ribbonPageGroupSecurity.ItemLinks.Add(this.barBtnExit);
            this.ribbonPageGroupSecurity.Name = "ribbonPageGroupSecurity";
            this.ribbonPageGroupSecurity.Text = "الأمان";
            // 
            // ribbonPageSettings
            // 
            this.ribbonPageSettings.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupSettings});
            this.ribbonPageSettings.Name = "ribbonPageSettings";
            this.ribbonPageSettings.Text = "الإعدادات";
            // 
            // ribbonPageGroupSettings
            // 
            this.ribbonPageGroupSettings.ItemLinks.Add(this.barBtnDatabaseSettings);
            this.ribbonPageGroupSettings.ItemLinks.Add(this.barBtnSystemSettings);
            this.ribbonPageGroupSettings.ItemLinks.Add(this.barBtnBackupRestore);
            this.ribbonPageGroupSettings.ItemLinks.Add(this.barBtnActivityLog);
            this.ribbonPageGroupSettings.Name = "ribbonPageGroupSettings";
            this.ribbonPageGroupSettings.Text = "الإعدادات";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItemUserInfo);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItemDateTime);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItemVersion);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 675);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1100, 24);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2019 Colorful";
            // 
            // xtraTabbedMdiManager
            // 
            this.xtraTabbedMdiManager.MdiParent = this;
            // 
            // MainForm
            // 
            this.AllowFormGlass = DevExpress.Utils.DefaultBoolean.True;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 699);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Ribbon = this.ribbon;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "نظام إدارة الموارد البشرية";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageCompany;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupCompanyInfo;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barBtnCompanyInfo;
        private DevExpress.XtraBars.BarButtonItem barBtnDepartments;
        private DevExpress.XtraBars.BarButtonItem barBtnPositions;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupCompanyStructure;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageEmployees;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupEmployees;
        private DevExpress.XtraBars.BarButtonItem barBtnEmployeesList;
        private DevExpress.XtraBars.BarButtonItem barBtnNewEmployee;
        private DevExpress.XtraBars.BarButtonItem barBtnEmployeeDocuments;
        private DevExpress.XtraBars.BarButtonItem barBtnEmployeeTransfers;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupEmployeeHistory;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageAttendance;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupAttendanceManagement;
        private DevExpress.XtraBars.BarButtonItem barBtnAttendanceDevices;
        private DevExpress.XtraBars.BarButtonItem barBtnAttendanceManagement;
        private DevExpress.XtraBars.BarButtonItem barBtnAttendanceReports;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupAttendanceReports;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageLeaves;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupLeaves;
        private DevExpress.XtraBars.BarButtonItem barBtnLeaveTypes;
        private DevExpress.XtraBars.BarButtonItem barBtnLeaveRequests;
        private DevExpress.XtraBars.BarButtonItem barBtnLeaveBalance;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageWorkSchedules;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupWorkSchedules;
        private DevExpress.XtraBars.BarButtonItem barBtnWorkHours;
        private DevExpress.XtraBars.BarButtonItem barBtnWorkShifts;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageSecurity;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSecurity;
        private DevExpress.XtraBars.BarButtonItem barBtnUsers;
        private DevExpress.XtraBars.BarButtonItem barBtnRoles;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageSettings;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSettings;
        private DevExpress.XtraBars.BarButtonItem barBtnDatabaseSettings;
        private DevExpress.XtraBars.BarButtonItem barBtnSystemSettings;
        private DevExpress.XtraBars.BarButtonItem barBtnBackupRestore;
        private DevExpress.XtraBars.BarButtonItem barBtnActivityLog;
        private DevExpress.XtraBars.BarStaticItem barStaticItemUserInfo;
        private DevExpress.XtraBars.BarStaticItem barStaticItemDateTime;
        private DevExpress.XtraBars.BarStaticItem barStaticItemVersion;
        private System.Windows.Forms.Timer timer;
        private DevExpress.XtraBars.BarButtonItem barBtnChangePassword;
        private DevExpress.XtraBars.BarButtonItem barBtnLogout;
        private DevExpress.XtraBars.BarButtonItem barBtnExit;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager;
        private DevExpress.XtraBars.BarButtonItem barBtnDashboard;
    }
}