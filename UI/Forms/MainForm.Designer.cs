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
            this.accordionControl = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlDashboard = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlEmployees = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlEmployeesList = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlEmployeesVacations = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlAttendance = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlAttendanceList = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlAttendanceRules = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlAttendanceDevices = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlLeaves = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlLeavesRequests = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlLeavesTypes = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlPayroll = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlPayrollGenerate = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlPayrollSalaries = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlPayrollAllowances = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlPayrollDeductions = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlReports = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlReportsEmployees = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlReportsAttendance = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlReportsLeaves = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlReportsPayroll = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlSettings = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlCompanySettings = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlUserManagement = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlAccessControl = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlSystemSetup = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlLogout = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControlUserInfo = new DevExpress.XtraEditors.PanelControl();
            this.labelControlUserName = new DevExpress.XtraEditors.LabelControl();
            this.labelControlUserRole = new DevExpress.XtraEditors.LabelControl();
            this.pictureEditUserImage = new DevExpress.XtraEditors.PictureEdit();
            this.buttonSettings = new DevExpress.XtraEditors.SimpleButton();
            this.buttonHelp = new DevExpress.XtraEditors.SimpleButton();
            this.buttonNotifications = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEditLogo = new DevExpress.XtraEditors.PictureEdit();
            this.labelControlDateTime = new DevExpress.XtraEditors.LabelControl();
            this.panelTitle = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBoxPageIcon = new DevExpress.XtraEditors.SvgImageBox();
            this.labelControlPageTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelContent = new DevExpress.XtraEditors.PanelControl();
            this.fluentFormDefaultManager1 = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(this.components);
            this.svgImageCollection = new DevExpress.Utils.SvgImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlUserInfo)).BeginInit();
            this.panelControlUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditUserImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTitle)).BeginInit();
            this.panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBoxPageIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // accordionControl
            // 
            this.accordionControl.Appearance.AccordionControl.BackColor = System.Drawing.SystemColors.Control;
            this.accordionControl.Appearance.AccordionControl.Options.UseBackColor = true;
            this.accordionControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.accordionControl.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlDashboard,
            this.accordionControlEmployees,
            this.accordionControlAttendance,
            this.accordionControlLeaves,
            this.accordionControlPayroll,
            this.accordionControlReports,
            this.accordionControlSettings,
            this.accordionControlLogout});
            this.accordionControl.Location = new System.Drawing.Point(980, 88);
            this.accordionControl.Margin = new System.Windows.Forms.Padding(4);
            this.accordionControl.Name = "accordionControl";
            this.accordionControl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.accordionControl.RightToLeftLayout = true;
            this.accordionControl.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl.Size = new System.Drawing.Size(220, 562);
            this.accordionControl.TabIndex = 0;
            this.accordionControl.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlDashboard
            // 
            this.accordionControlDashboard.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlDashboard.Appearance.Default.Options.UseFont = true;
            this.accordionControlDashboard.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlDashboard.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accordionControlDashboard.ImageOptions.SvgImage")));
            this.accordionControlDashboard.Name = "accordionControlDashboard";
            this.accordionControlDashboard.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlDashboard.Text = "لوحة التحكم";
            // 
            // accordionControlEmployees
            // 
            this.accordionControlEmployees.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlEmployees.Appearance.Default.Options.UseFont = true;
            this.accordionControlEmployees.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlEmployeesList,
            this.accordionControlEmployeesVacations});
            this.accordionControlEmployees.Expanded = true;
            this.accordionControlEmployees.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlEmployees.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accordionControlEmployees.ImageOptions.SvgImage")));
            this.accordionControlEmployees.Name = "accordionControlEmployees";
            this.accordionControlEmployees.Text = "الموظفين";
            // 
            // accordionControlEmployeesList
            // 
            this.accordionControlEmployeesList.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlEmployeesList.Appearance.Default.Options.UseFont = true;
            this.accordionControlEmployeesList.Name = "accordionControlEmployeesList";
            this.accordionControlEmployeesList.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlEmployeesList.Text = "قائمة الموظفين";
            // 
            // accordionControlEmployeesVacations
            // 
            this.accordionControlEmployeesVacations.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlEmployeesVacations.Appearance.Default.Options.UseFont = true;
            this.accordionControlEmployeesVacations.Name = "accordionControlEmployeesVacations";
            this.accordionControlEmployeesVacations.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlEmployeesVacations.Text = "إجازات الموظفين";
            // 
            // accordionControlAttendance
            // 
            this.accordionControlAttendance.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlAttendance.Appearance.Default.Options.UseFont = true;
            this.accordionControlAttendance.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlAttendanceList,
            this.accordionControlAttendanceRules,
            this.accordionControlAttendanceDevices});
            this.accordionControlAttendance.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlAttendance.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accordionControlAttendance.ImageOptions.SvgImage")));
            this.accordionControlAttendance.Name = "accordionControlAttendance";
            this.accordionControlAttendance.Text = "الحضور والانصراف";
            // 
            // accordionControlAttendanceList
            // 
            this.accordionControlAttendanceList.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlAttendanceList.Appearance.Default.Options.UseFont = true;
            this.accordionControlAttendanceList.Name = "accordionControlAttendanceList";
            this.accordionControlAttendanceList.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlAttendanceList.Text = "سجل الحضور";
            // 
            // accordionControlAttendanceRules
            // 
            this.accordionControlAttendanceRules.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlAttendanceRules.Appearance.Default.Options.UseFont = true;
            this.accordionControlAttendanceRules.Name = "accordionControlAttendanceRules";
            this.accordionControlAttendanceRules.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlAttendanceRules.Text = "قواعد الحضور";
            // 
            // accordionControlAttendanceDevices
            // 
            this.accordionControlAttendanceDevices.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlAttendanceDevices.Appearance.Default.Options.UseFont = true;
            this.accordionControlAttendanceDevices.Name = "accordionControlAttendanceDevices";
            this.accordionControlAttendanceDevices.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlAttendanceDevices.Text = "أجهزة البصمة";
            // 
            // accordionControlLeaves
            // 
            this.accordionControlLeaves.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlLeaves.Appearance.Default.Options.UseFont = true;
            this.accordionControlLeaves.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlLeavesRequests,
            this.accordionControlLeavesTypes});
            this.accordionControlLeaves.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlLeaves.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accordionControlLeaves.ImageOptions.SvgImage")));
            this.accordionControlLeaves.Name = "accordionControlLeaves";
            this.accordionControlLeaves.Text = "الإجازات";
            // 
            // accordionControlLeavesRequests
            // 
            this.accordionControlLeavesRequests.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlLeavesRequests.Appearance.Default.Options.UseFont = true;
            this.accordionControlLeavesRequests.Name = "accordionControlLeavesRequests";
            this.accordionControlLeavesRequests.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlLeavesRequests.Text = "طلبات الإجازات";
            // 
            // accordionControlLeavesTypes
            // 
            this.accordionControlLeavesTypes.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlLeavesTypes.Appearance.Default.Options.UseFont = true;
            this.accordionControlLeavesTypes.Name = "accordionControlLeavesTypes";
            this.accordionControlLeavesTypes.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlLeavesTypes.Text = "أنواع الإجازات";
            // 
            // accordionControlPayroll
            // 
            this.accordionControlPayroll.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlPayroll.Appearance.Default.Options.UseFont = true;
            this.accordionControlPayroll.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlPayrollGenerate,
            this.accordionControlPayrollSalaries,
            this.accordionControlPayrollAllowances,
            this.accordionControlPayrollDeductions});
            this.accordionControlPayroll.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlPayroll.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accordionControlPayroll.ImageOptions.SvgImage")));
            this.accordionControlPayroll.Name = "accordionControlPayroll";
            this.accordionControlPayroll.Text = "الرواتب";
            // 
            // accordionControlPayrollGenerate
            // 
            this.accordionControlPayrollGenerate.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlPayrollGenerate.Appearance.Default.Options.UseFont = true;
            this.accordionControlPayrollGenerate.Name = "accordionControlPayrollGenerate";
            this.accordionControlPayrollGenerate.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlPayrollGenerate.Text = "إصدار الرواتب";
            // 
            // accordionControlPayrollSalaries
            // 
            this.accordionControlPayrollSalaries.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlPayrollSalaries.Appearance.Default.Options.UseFont = true;
            this.accordionControlPayrollSalaries.Name = "accordionControlPayrollSalaries";
            this.accordionControlPayrollSalaries.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlPayrollSalaries.Text = "سجل الرواتب";
            // 
            // accordionControlPayrollAllowances
            // 
            this.accordionControlPayrollAllowances.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlPayrollAllowances.Appearance.Default.Options.UseFont = true;
            this.accordionControlPayrollAllowances.Name = "accordionControlPayrollAllowances";
            this.accordionControlPayrollAllowances.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlPayrollAllowances.Text = "البدلات";
            // 
            // accordionControlPayrollDeductions
            // 
            this.accordionControlPayrollDeductions.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlPayrollDeductions.Appearance.Default.Options.UseFont = true;
            this.accordionControlPayrollDeductions.Name = "accordionControlPayrollDeductions";
            this.accordionControlPayrollDeductions.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlPayrollDeductions.Text = "الاستقطاعات";
            // 
            // accordionControlReports
            // 
            this.accordionControlReports.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlReports.Appearance.Default.Options.UseFont = true;
            this.accordionControlReports.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlReportsEmployees,
            this.accordionControlReportsAttendance,
            this.accordionControlReportsLeaves,
            this.accordionControlReportsPayroll});
            this.accordionControlReports.Expanded = true;
            this.accordionControlReports.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlReports.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accordionControlReports.ImageOptions.SvgImage")));
            this.accordionControlReports.Name = "accordionControlReports";
            this.accordionControlReports.Text = "التقارير";
            // 
            // accordionControlReportsEmployees
            // 
            this.accordionControlReportsEmployees.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlReportsEmployees.Appearance.Default.Options.UseFont = true;
            this.accordionControlReportsEmployees.Name = "accordionControlReportsEmployees";
            this.accordionControlReportsEmployees.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlReportsEmployees.Text = "تقارير الموظفين";
            // 
            // accordionControlReportsAttendance
            // 
            this.accordionControlReportsAttendance.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlReportsAttendance.Appearance.Default.Options.UseFont = true;
            this.accordionControlReportsAttendance.Name = "accordionControlReportsAttendance";
            this.accordionControlReportsAttendance.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlReportsAttendance.Text = "تقارير الحضور";
            // 
            // accordionControlReportsLeaves
            // 
            this.accordionControlReportsLeaves.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlReportsLeaves.Appearance.Default.Options.UseFont = true;
            this.accordionControlReportsLeaves.Name = "accordionControlReportsLeaves";
            this.accordionControlReportsLeaves.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlReportsLeaves.Text = "تقارير الإجازات";
            // 
            // accordionControlReportsPayroll
            // 
            this.accordionControlReportsPayroll.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlReportsPayroll.Appearance.Default.Options.UseFont = true;
            this.accordionControlReportsPayroll.Name = "accordionControlReportsPayroll";
            this.accordionControlReportsPayroll.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlReportsPayroll.Text = "تقارير الرواتب";
            // 
            // accordionControlSettings
            // 
            this.accordionControlSettings.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlSettings.Appearance.Default.Options.UseFont = true;
            this.accordionControlSettings.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlCompanySettings,
            this.accordionControlUserManagement,
            this.accordionControlAccessControl,
            this.accordionControlSystemSetup});
            this.accordionControlSettings.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlSettings.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accordionControlSettings.ImageOptions.SvgImage")));
            this.accordionControlSettings.Name = "accordionControlSettings";
            this.accordionControlSettings.Text = "الإعدادات";
            // 
            // accordionControlCompanySettings
            // 
            this.accordionControlCompanySettings.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlCompanySettings.Appearance.Default.Options.UseFont = true;
            this.accordionControlCompanySettings.Name = "accordionControlCompanySettings";
            this.accordionControlCompanySettings.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlCompanySettings.Text = "بيانات الشركة";
            // 
            // accordionControlUserManagement
            // 
            this.accordionControlUserManagement.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlUserManagement.Appearance.Default.Options.UseFont = true;
            this.accordionControlUserManagement.Name = "accordionControlUserManagement";
            this.accordionControlUserManagement.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlUserManagement.Text = "إدارة المستخدمين";
            // 
            // accordionControlAccessControl
            // 
            this.accordionControlAccessControl.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlAccessControl.Appearance.Default.Options.UseFont = true;
            this.accordionControlAccessControl.Name = "accordionControlAccessControl";
            this.accordionControlAccessControl.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlAccessControl.Text = "الصلاحيات";
            // 
            // accordionControlSystemSetup
            // 
            this.accordionControlSystemSetup.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlSystemSetup.Appearance.Default.Options.UseFont = true;
            this.accordionControlSystemSetup.Name = "accordionControlSystemSetup";
            this.accordionControlSystemSetup.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlSystemSetup.Text = "إعداد النظام";
            // 
            // accordionControlLogout
            // 
            this.accordionControlLogout.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlLogout.Appearance.Default.Options.UseFont = true;
            this.accordionControlLogout.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.accordionControlLogout.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accordionControlLogout.ImageOptions.SvgImage")));
            this.accordionControlLogout.Name = "accordionControlLogout";
            this.accordionControlLogout.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlLogout.Text = "تسجيل الخروج";
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Manager = this.fluentFormDefaultManager1;
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(1200, 31);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            this.fluentDesignFormControl1.Visible = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.panelControlUserInfo);
            this.panelControl1.Controls.Add(this.buttonSettings);
            this.panelControl1.Controls.Add(this.buttonHelp);
            this.panelControl1.Controls.Add(this.buttonNotifications);
            this.panelControl1.Controls.Add(this.pictureEditLogo);
            this.panelControl1.Controls.Add(this.labelControlDateTime);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 31);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1200, 44);
            this.panelControl1.TabIndex = 3;
            // 
            // panelControlUserInfo
            // 
            this.panelControlUserInfo.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControlUserInfo.Appearance.Options.UseBackColor = true;
            this.panelControlUserInfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlUserInfo.Controls.Add(this.labelControlUserName);
            this.panelControlUserInfo.Controls.Add(this.labelControlUserRole);
            this.panelControlUserInfo.Controls.Add(this.pictureEditUserImage);
            this.panelControlUserInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControlUserInfo.Location = new System.Drawing.Point(151, 0);
            this.panelControlUserInfo.Name = "panelControlUserInfo";
            this.panelControlUserInfo.Size = new System.Drawing.Size(247, 44);
            this.panelControlUserInfo.TabIndex = 6;
            // 
            // labelControlUserName
            // 
            this.labelControlUserName.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlUserName.Appearance.Options.UseFont = true;
            this.labelControlUserName.Location = new System.Drawing.Point(43, 6);
            this.labelControlUserName.Name = "labelControlUserName";
            this.labelControlUserName.Size = new System.Drawing.Size(92, 17);
            this.labelControlUserName.TabIndex = 5;
            this.labelControlUserName.Text = "مرحباً، المستخدم";
            // 
            // labelControlUserRole
            // 
            this.labelControlUserRole.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlUserRole.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControlUserRole.Appearance.Options.UseFont = true;
            this.labelControlUserRole.Appearance.Options.UseForeColor = true;
            this.labelControlUserRole.Location = new System.Drawing.Point(45, 24);
            this.labelControlUserRole.Name = "labelControlUserRole";
            this.labelControlUserRole.Size = new System.Drawing.Size(28, 13);
            this.labelControlUserRole.TabIndex = 4;
            this.labelControlUserRole.Text = "المدير";
            // 
            // pictureEditUserImage
            // 
            this.pictureEditUserImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEditUserImage.EditValue = ((object)(resources.GetObject("pictureEditUserImage.EditValue")));
            this.pictureEditUserImage.Location = new System.Drawing.Point(0, 0);
            this.pictureEditUserImage.Name = "pictureEditUserImage";
            this.pictureEditUserImage.Properties.AllowFocused = false;
            this.pictureEditUserImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEditUserImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEditUserImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEditUserImage.Size = new System.Drawing.Size(40, 44);
            this.pictureEditUserImage.TabIndex = 2;
            // 
            // buttonSettings
            // 
            this.buttonSettings.AllowFocus = false;
            this.buttonSettings.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.buttonSettings.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.buttonSettings.Appearance.Options.UseBackColor = true;
            this.buttonSettings.Appearance.Options.UseBorderColor = true;
            this.buttonSettings.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSettings.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.buttonSettings.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonSettings.ImageOptions.SvgImage")));
            this.buttonSettings.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.buttonSettings.Location = new System.Drawing.Point(1020, 0);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.buttonSettings.Size = new System.Drawing.Size(60, 44);
            this.buttonSettings.TabIndex = 5;
            this.buttonSettings.Text = "إعدادات";
            this.buttonSettings.Click += new System.EventHandler(this.ButtonSettings_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.AllowFocus = false;
            this.buttonHelp.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.buttonHelp.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.buttonHelp.Appearance.Options.UseBackColor = true;
            this.buttonHelp.Appearance.Options.UseBorderColor = true;
            this.buttonHelp.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonHelp.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.buttonHelp.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonHelp.ImageOptions.SvgImage")));
            this.buttonHelp.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.buttonHelp.Location = new System.Drawing.Point(1080, 0);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.buttonHelp.Size = new System.Drawing.Size(60, 44);
            this.buttonHelp.TabIndex = 4;
            this.buttonHelp.Text = "مساعدة";
            this.buttonHelp.Click += new System.EventHandler(this.ButtonHelp_Click);
            // 
            // buttonNotifications
            // 
            this.buttonNotifications.AllowFocus = false;
            this.buttonNotifications.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.buttonNotifications.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.buttonNotifications.Appearance.Options.UseBackColor = true;
            this.buttonNotifications.Appearance.Options.UseBorderColor = true;
            this.buttonNotifications.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonNotifications.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.buttonNotifications.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonNotifications.ImageOptions.SvgImage")));
            this.buttonNotifications.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.buttonNotifications.Location = new System.Drawing.Point(1140, 0);
            this.buttonNotifications.Name = "buttonNotifications";
            this.buttonNotifications.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.buttonNotifications.Size = new System.Drawing.Size(60, 44);
            this.buttonNotifications.TabIndex = 3;
            this.buttonNotifications.Text = "إشعارات";
            this.buttonNotifications.Click += new System.EventHandler(this.ButtonNotifications_Click);
            // 
            // pictureEditLogo
            // 
            this.pictureEditLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEditLogo.EditValue = ((object)(resources.GetObject("pictureEditLogo.EditValue")));
            this.pictureEditLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureEditLogo.Name = "pictureEditLogo";
            this.pictureEditLogo.Properties.AllowFocused = false;
            this.pictureEditLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEditLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEditLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEditLogo.Size = new System.Drawing.Size(151, 44);
            this.pictureEditLogo.TabIndex = 1;
            // 
            // labelControlDateTime
            // 
            this.labelControlDateTime.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlDateTime.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControlDateTime.Appearance.Options.UseFont = true;
            this.labelControlDateTime.Appearance.Options.UseForeColor = true;
            this.labelControlDateTime.Location = new System.Drawing.Point(462, 15);
            this.labelControlDateTime.Name = "labelControlDateTime";
            this.labelControlDateTime.Size = new System.Drawing.Size(136, 13);
            this.labelControlDateTime.TabIndex = 0;
            this.labelControlDateTime.Text = "2025/05/02 12:00:00";
            // 
            // panelTitle
            // 
            this.panelTitle.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.panelTitle.Appearance.Options.UseBackColor = true;
            this.panelTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelTitle.Controls.Add(this.svgImageBoxPageIcon);
            this.panelTitle.Controls.Add(this.labelControlPageTitle);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 75);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(980, 43);
            this.panelTitle.TabIndex = 4;
            // 
            // svgImageBoxPageIcon
            // 
            this.svgImageBoxPageIcon.Dock = System.Windows.Forms.DockStyle.Right;
            this.svgImageBoxPageIcon.Location = new System.Drawing.Point(936, 0);
            this.svgImageBoxPageIcon.Name = "svgImageBoxPageIcon";
            this.svgImageBoxPageIcon.Size = new System.Drawing.Size(44, 43);
            this.svgImageBoxPageIcon.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageBoxPageIcon.SvgImage")));
            this.svgImageBoxPageIcon.TabIndex = 1;
            this.svgImageBoxPageIcon.Text = "svgImageBox1";
            // 
            // labelControlPageTitle
            // 
            this.labelControlPageTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlPageTitle.Appearance.Options.UseFont = true;
            this.labelControlPageTitle.Location = new System.Drawing.Point(848, 11);
            this.labelControlPageTitle.Name = "labelControlPageTitle";
            this.labelControlPageTitle.Size = new System.Drawing.Size(82, 21);
            this.labelControlPageTitle.TabIndex = 0;
            this.labelControlPageTitle.Text = "لوحة التحكم";
            // 
            // panelContent
            // 
            this.panelContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 118);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(980, 532);
            this.panelContent.TabIndex = 5;
            // 
            // fluentFormDefaultManager1
            // 
            this.fluentFormDefaultManager1.Form = this;
            // 
            // svgImageCollection
            // 
            this.svgImageCollection.Add("dashboard", "image://svgimages/dashboards/dashboard.svg");
            this.svgImageCollection.Add("employees", "image://svgimages/people/employee.svg");
            this.svgImageCollection.Add("attendance", "image://svgimages/scheduling/time.svg");
            this.svgImageCollection.Add("leaves", "image://svgimages/outlook inspired/calendar.svg");
            this.svgImageCollection.Add("payroll", "image://svgimages/business objects/bo_price.svg");
            this.svgImageCollection.Add("reports", "image://svgimages/dashboards/chart.svg");
            this.svgImageCollection.Add("settings", "image://svgimages/icon builder/actions_settings.svg");
            this.svgImageCollection.Add("logout", "image://svgimages/dashboards/logout.svg");
            this.svgImageCollection.Add("info", "image://svgimages/icon builder/actions_info.svg");
            this.svgImageCollection.Add("warning", "image://svgimages/icon builder/actions_question.svg");
            this.svgImageCollection.Add("error", "image://svgimages/icon builder/actions_deletecircled.svg");
            this.svgImageCollection.Add("success", "image://svgimages/icon builder/actions_check.svg");
            // 
            // MainForm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 650);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTitle);
            this.Controls.Add(this.accordionControl);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.fluentDesignFormControl1);
            this.FluentDesignFormControl = this.fluentDesignFormControl1;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("MainForm.IconOptions.SvgImage")));
            this.Name = "MainForm";
            this.NavigationControl = this.accordionControl;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "نظام إدارة الموارد البشرية";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlUserInfo)).EndInit();
            this.panelControlUserInfo.ResumeLayout(false);
            this.panelControlUserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditUserImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTitle)).EndInit();
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBoxPageIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControlDateTime;
        private DevExpress.XtraEditors.PictureEdit pictureEditLogo;
        private DevExpress.XtraEditors.SimpleButton buttonNotifications;
        private DevExpress.XtraEditors.SimpleButton buttonHelp;
        private DevExpress.XtraEditors.SimpleButton buttonSettings;
        private DevExpress.XtraEditors.PanelControl panelControlUserInfo;
        private DevExpress.XtraEditors.LabelControl labelControlUserName;
        private DevExpress.XtraEditors.LabelControl labelControlUserRole;
        private DevExpress.XtraEditors.PictureEdit pictureEditUserImage;
        private DevExpress.XtraEditors.PanelControl panelTitle;
        private DevExpress.XtraEditors.PanelControl panelContent;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlDashboard;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlEmployees;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlEmployeesList;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlEmployeesVacations;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlAttendance;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlAttendanceList;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlAttendanceRules;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlAttendanceDevices;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlLeaves;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlLeavesRequests;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlLeavesTypes;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlPayroll;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlPayrollGenerate;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlPayrollSalaries;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlPayrollAllowances;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlPayrollDeductions;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlReports;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlReportsEmployees;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlReportsAttendance;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlReportsLeaves;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlReportsPayroll;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlSettings;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlCompanySettings;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlUserManagement;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlAccessControl;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlSystemSetup;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlLogout;
        private DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager fluentFormDefaultManager1;
        private DevExpress.XtraEditors.LabelControl labelControlPageTitle;
        private DevExpress.XtraEditors.SvgImageBox svgImageBoxPageIcon;
        private DevExpress.Utils.SvgImageCollection svgImageCollection;
    }
}