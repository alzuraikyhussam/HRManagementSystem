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
            this.barButtonItemUserProfile = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemNotifications = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemChangePassword = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemLogout = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItemUsername = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemDateTime = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItemAbout = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageFile = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupUser = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupHelp = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.timerDateTime = new System.Windows.Forms.Timer(this.components);
            this.navigationPane = new DevExpress.XtraBars.Navigation.NavigationPane();
            this.navigationPaneDashboard = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navigationPaneEmployees = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navigationPaneAttendance = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navigationPaneLeaves = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navigationPanePayroll = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navigationPaneReports = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navigationPaneSettings = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.panelContent = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationPane)).BeginInit();
            this.navigationPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.barButtonItemUserProfile,
            this.barButtonItemNotifications,
            this.barButtonItemChangePassword,
            this.barButtonItemLogout,
            this.barStaticItemUsername,
            this.barStaticItemDateTime,
            this.barButtonItemAbout});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 9;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageFile});
            this.ribbon.QuickToolbarItemLinks.Add(this.barButtonItemNotifications);
            this.ribbon.QuickToolbarItemLinks.Add(this.barButtonItemUserProfile);
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            this.ribbon.Size = new System.Drawing.Size(1224, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barButtonItemUserProfile
            // 
            this.barButtonItemUserProfile.Caption = "الملف الشخصي";
            this.barButtonItemUserProfile.Id = 1;
            this.barButtonItemUserProfile.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemUserProfile.ImageOptions.SvgImage")));
            this.barButtonItemUserProfile.Name = "barButtonItemUserProfile";
            this.barButtonItemUserProfile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemUserProfile_ItemClick);
            // 
            // barButtonItemNotifications
            // 
            this.barButtonItemNotifications.Caption = "الإشعارات";
            this.barButtonItemNotifications.Id = 2;
            this.barButtonItemNotifications.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemNotifications.ImageOptions.SvgImage")));
            this.barButtonItemNotifications.Name = "barButtonItemNotifications";
            this.barButtonItemNotifications.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemNotifications_ItemClick);
            // 
            // barButtonItemChangePassword
            // 
            this.barButtonItemChangePassword.Caption = "تغيير كلمة المرور";
            this.barButtonItemChangePassword.Id = 3;
            this.barButtonItemChangePassword.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemChangePassword.ImageOptions.SvgImage")));
            this.barButtonItemChangePassword.Name = "barButtonItemChangePassword";
            this.barButtonItemChangePassword.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemChangePassword_ItemClick);
            // 
            // barButtonItemLogout
            // 
            this.barButtonItemLogout.Caption = "تسجيل الخروج";
            this.barButtonItemLogout.Id = 4;
            this.barButtonItemLogout.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemLogout.ImageOptions.SvgImage")));
            this.barButtonItemLogout.Name = "barButtonItemLogout";
            this.barButtonItemLogout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemLogout_ItemClick);
            // 
            // barStaticItemUsername
            // 
            this.barStaticItemUsername.Caption = "المستخدم: ";
            this.barStaticItemUsername.Id = 5;
            this.barStaticItemUsername.Name = "barStaticItemUsername";
            // 
            // barStaticItemDateTime
            // 
            this.barStaticItemDateTime.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barStaticItemDateTime.Caption = "التاريخ والوقت";
            this.barStaticItemDateTime.Id = 6;
            this.barStaticItemDateTime.Name = "barStaticItemDateTime";
            // 
            // barButtonItemAbout
            // 
            this.barButtonItemAbout.Caption = "حول البرنامج";
            this.barButtonItemAbout.Id = 7;
            this.barButtonItemAbout.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemAbout.ImageOptions.SvgImage")));
            this.barButtonItemAbout.Name = "barButtonItemAbout";
            this.barButtonItemAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemAbout_ItemClick);
            // 
            // ribbonPageFile
            // 
            this.ribbonPageFile.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupUser,
            this.ribbonPageGroupHelp});
            this.ribbonPageFile.Name = "ribbonPageFile";
            this.ribbonPageFile.Text = "ملف";
            // 
            // ribbonPageGroupUser
            // 
            this.ribbonPageGroupUser.ItemLinks.Add(this.barButtonItemUserProfile);
            this.ribbonPageGroupUser.ItemLinks.Add(this.barButtonItemChangePassword);
            this.ribbonPageGroupUser.ItemLinks.Add(this.barButtonItemLogout);
            this.ribbonPageGroupUser.Name = "ribbonPageGroupUser";
            this.ribbonPageGroupUser.Text = "المستخدم";
            // 
            // ribbonPageGroupHelp
            // 
            this.ribbonPageGroupHelp.ItemLinks.Add(this.barButtonItemAbout);
            this.ribbonPageGroupHelp.Name = "ribbonPageGroupHelp";
            this.ribbonPageGroupHelp.Text = "مساعدة";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItemUsername);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItemDateTime);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 719);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1224, 24);
            // 
            // timerDateTime
            // 
            this.timerDateTime.Interval = 1000;
            this.timerDateTime.Tick += new System.EventHandler(this.TimerDateTime_Tick);
            // 
            // navigationPane
            // 
            this.navigationPane.AppearanceButton.Hovered.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.navigationPane.AppearanceButton.Hovered.Options.UseFont = true;
            this.navigationPane.AppearanceButton.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navigationPane.AppearanceButton.Normal.Options.UseFont = true;
            this.navigationPane.AppearanceButton.Pressed.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.navigationPane.AppearanceButton.Pressed.Options.UseFont = true;
            this.navigationPane.Controls.Add(this.navigationPaneDashboard);
            this.navigationPane.Controls.Add(this.navigationPaneEmployees);
            this.navigationPane.Controls.Add(this.navigationPaneAttendance);
            this.navigationPane.Controls.Add(this.navigationPaneLeaves);
            this.navigationPane.Controls.Add(this.navigationPanePayroll);
            this.navigationPane.Controls.Add(this.navigationPaneReports);
            this.navigationPane.Controls.Add(this.navigationPaneSettings);
            this.navigationPane.Dock = System.Windows.Forms.DockStyle.Left;
            this.navigationPane.Location = new System.Drawing.Point(0, 158);
            this.navigationPane.Name = "navigationPane";
            this.navigationPane.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPage[] {
            this.navigationPaneDashboard,
            this.navigationPaneEmployees,
            this.navigationPaneAttendance,
            this.navigationPaneLeaves,
            this.navigationPanePayroll,
            this.navigationPaneReports,
            this.navigationPaneSettings});
            this.navigationPane.RegularSize = new System.Drawing.Size(231, 561);
            this.navigationPane.SelectedPage = this.navigationPaneDashboard;
            this.navigationPane.Size = new System.Drawing.Size(231, 561);
            this.navigationPane.TabIndex = 2;
            this.navigationPane.Text = "الرئيسية";
            // 
            // navigationPaneDashboard
            // 
            this.navigationPaneDashboard.Caption = "لوحة التحكم";
            this.navigationPaneDashboard.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("navigationPaneDashboard.ImageOptions.SvgImage")));
            this.navigationPaneDashboard.Name = "navigationPaneDashboard";
            this.navigationPaneDashboard.Size = new System.Drawing.Size(134, 515);
            // 
            // navigationPaneEmployees
            // 
            this.navigationPaneEmployees.Caption = "الموظفين";
            this.navigationPaneEmployees.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("navigationPaneEmployees.ImageOptions.SvgImage")));
            this.navigationPaneEmployees.Name = "navigationPaneEmployees";
            this.navigationPaneEmployees.Size = new System.Drawing.Size(134, 515);
            // 
            // navigationPaneAttendance
            // 
            this.navigationPaneAttendance.Caption = "الحضور والانصراف";
            this.navigationPaneAttendance.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("navigationPaneAttendance.ImageOptions.SvgImage")));
            this.navigationPaneAttendance.Name = "navigationPaneAttendance";
            this.navigationPaneAttendance.Size = new System.Drawing.Size(134, 515);
            // 
            // navigationPaneLeaves
            // 
            this.navigationPaneLeaves.Caption = "الإجازات";
            this.navigationPaneLeaves.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("navigationPaneLeaves.ImageOptions.SvgImage")));
            this.navigationPaneLeaves.Name = "navigationPaneLeaves";
            this.navigationPaneLeaves.Size = new System.Drawing.Size(134, 515);
            // 
            // navigationPanePayroll
            // 
            this.navigationPanePayroll.Caption = "الرواتب";
            this.navigationPanePayroll.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("navigationPanePayroll.ImageOptions.SvgImage")));
            this.navigationPanePayroll.Name = "navigationPanePayroll";
            this.navigationPanePayroll.Size = new System.Drawing.Size(134, 515);
            // 
            // navigationPaneReports
            // 
            this.navigationPaneReports.Caption = "التقارير";
            this.navigationPaneReports.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("navigationPaneReports.ImageOptions.SvgImage")));
            this.navigationPaneReports.Name = "navigationPaneReports";
            this.navigationPaneReports.Size = new System.Drawing.Size(134, 515);
            // 
            // navigationPaneSettings
            // 
            this.navigationPaneSettings.Caption = "الإعدادات";
            this.navigationPaneSettings.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("navigationPaneSettings.ImageOptions.SvgImage")));
            this.navigationPaneSettings.Name = "navigationPaneSettings";
            this.navigationPaneSettings.Size = new System.Drawing.Size(134, 515);
            // 
            // panelContent
            // 
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(231, 158);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(993, 561);
            this.panelContent.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 743);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.navigationPane);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("MainForm.IconOptions.SvgImage")));
            this.Name = "MainForm";
            this.Ribbon = this.ribbon;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "نظام إدارة الموارد البشرية";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationPane)).EndInit();
            this.navigationPane.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageFile;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupUser;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemUserProfile;
        private DevExpress.XtraBars.BarButtonItem barButtonItemNotifications;
        private DevExpress.XtraBars.BarButtonItem barButtonItemChangePassword;
        private DevExpress.XtraBars.BarButtonItem barButtonItemLogout;
        private DevExpress.XtraBars.BarStaticItem barStaticItemUsername;
        private DevExpress.XtraBars.BarStaticItem barStaticItemDateTime;
        private System.Windows.Forms.Timer timerDateTime;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAbout;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupHelp;
        private DevExpress.XtraBars.Navigation.NavigationPane navigationPane;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPaneDashboard;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPaneEmployees;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPaneAttendance;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPaneLeaves;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPanePayroll;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPaneReports;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPaneSettings;
        private DevExpress.XtraEditors.PanelControl panelContent;
    }
}