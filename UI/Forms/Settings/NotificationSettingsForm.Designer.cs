namespace HR.UI.Forms.Settings
{
    partial class NotificationSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationSettingsForm));
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItemSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControlNotifications = new DevExpress.XtraEditors.GroupControl();
            this.checkEditEnableSystemNotifications = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditEnableSMSNotifications = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditEnableEmailNotifications = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditEnableNotifications = new DevExpress.XtraEditors.CheckEdit();
            this.groupControlNotificationTypes = new DevExpress.XtraEditors.GroupControl();
            this.checkEditSalaryIssueNotification = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditAttendanceIssueNotification = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditEmployeeTerminationNotification = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditNewEmployeeNotification = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditLeaveRequestRejectedNotification = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditLeaveRequestApprovedNotification = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditNewLeaveRequestNotification = new DevExpress.XtraEditors.CheckEdit();
            this.groupControlNotificationLevel = new DevExpress.XtraEditors.GroupControl();
            this.radioGroupNotificationLevel = new DevExpress.XtraEditors.RadioGroup();
            this.groupControlNotificationTiming = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.spinEditDailyNotificationHour = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlNotifications)).BeginInit();
            this.groupControlNotifications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditEnableSystemNotifications.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditEnableSMSNotifications.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditEnableEmailNotifications.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditEnableNotifications.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlNotificationTypes)).BeginInit();
            this.groupControlNotificationTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSalaryIssueNotification.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAttendanceIssueNotification.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditEmployeeTerminationNotification.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditNewEmployeeNotification.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditLeaveRequestRejectedNotification.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditLeaveRequestApprovedNotification.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditNewLeaveRequestNotification.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlNotificationLevel)).BeginInit();
            this.groupControlNotificationLevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupNotificationLevel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlNotificationTiming)).BeginInit();
            this.groupControlNotificationTiming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditDailyNotificationHour.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.ribbonControl.SearchEditItem,
            this.barButtonItemSave,
            this.barButtonItemRefresh});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 3;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ribbonControl.Size = new System.Drawing.Size(798, 158);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
            // 
            // barButtonItemSave
            // 
            this.barButtonItemSave.Caption = "حفظ";
            this.barButtonItemSave.Id = 1;
            this.barButtonItemSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemSave.ImageOptions.Image")));
            this.barButtonItemSave.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemSave.ImageOptions.LargeImage")));
            this.barButtonItemSave.Name = "barButtonItemSave";
            this.barButtonItemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSave_ItemClick);
            // 
            // barButtonItemRefresh
            // 
            this.barButtonItemRefresh.Caption = "تحديث";
            this.barButtonItemRefresh.Id = 2;
            this.barButtonItemRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemRefresh.ImageOptions.Image")));
            this.barButtonItemRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemRefresh.ImageOptions.LargeImage")));
            this.barButtonItemRefresh.Name = "barButtonItemRefresh";
            this.barButtonItemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemRefresh_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "إعدادات الإشعارات";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemSave);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemRefresh);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "عمليات";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 578);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ribbonStatusBar.Size = new System.Drawing.Size(798, 24);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControlNotifications);
            this.panelControl1.Controls.Add(this.groupControlNotificationTypes);
            this.panelControl1.Controls.Add(this.groupControlNotificationLevel);
            this.panelControl1.Controls.Add(this.groupControlNotificationTiming);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 158);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(798, 420);
            this.panelControl1.TabIndex = 2;
            // 
            // groupControlNotifications
            // 
            this.groupControlNotifications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControlNotifications.Controls.Add(this.checkEditEnableSystemNotifications);
            this.groupControlNotifications.Controls.Add(this.checkEditEnableSMSNotifications);
            this.groupControlNotifications.Controls.Add(this.checkEditEnableEmailNotifications);
            this.groupControlNotifications.Controls.Add(this.checkEditEnableNotifications);
            this.groupControlNotifications.Location = new System.Drawing.Point(12, 12);
            this.groupControlNotifications.Name = "groupControlNotifications";
            this.groupControlNotifications.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupControlNotifications.Size = new System.Drawing.Size(774, 80);
            this.groupControlNotifications.TabIndex = 0;
            this.groupControlNotifications.Text = "إعدادات الإشعارات العامة";
            // 
            // checkEditEnableSystemNotifications
            // 
            this.checkEditEnableSystemNotifications.Location = new System.Drawing.Point(159, 47);
            this.checkEditEnableSystemNotifications.Name = "checkEditEnableSystemNotifications";
            this.checkEditEnableSystemNotifications.Properties.Caption = "تفعيل إشعارات النظام";
            this.checkEditEnableSystemNotifications.Size = new System.Drawing.Size(134, 20);
            this.checkEditEnableSystemNotifications.TabIndex = 3;
            // 
            // checkEditEnableSMSNotifications
            // 
            this.checkEditEnableSMSNotifications.Location = new System.Drawing.Point(346, 47);
            this.checkEditEnableSMSNotifications.Name = "checkEditEnableSMSNotifications";
            this.checkEditEnableSMSNotifications.Properties.Caption = "تفعيل إشعارات الرسائل النصية";
            this.checkEditEnableSMSNotifications.Size = new System.Drawing.Size(162, 20);
            this.checkEditEnableSMSNotifications.TabIndex = 2;
            // 
            // checkEditEnableEmailNotifications
            // 
            this.checkEditEnableEmailNotifications.Location = new System.Drawing.Point(553, 47);
            this.checkEditEnableEmailNotifications.Name = "checkEditEnableEmailNotifications";
            this.checkEditEnableEmailNotifications.Properties.Caption = "تفعيل إشعارات البريد الإلكتروني";
            this.checkEditEnableEmailNotifications.Size = new System.Drawing.Size(162, 20);
            this.checkEditEnableEmailNotifications.TabIndex = 1;
            // 
            // checkEditEnableNotifications
            // 
            this.checkEditEnableNotifications.Location = new System.Drawing.Point(594, 26);
            this.checkEditEnableNotifications.Name = "checkEditEnableNotifications";
            this.checkEditEnableNotifications.Properties.Caption = "تفعيل الإشعارات";
            this.checkEditEnableNotifications.Size = new System.Drawing.Size(121, 20);
            this.checkEditEnableNotifications.TabIndex = 0;
            this.checkEditEnableNotifications.CheckedChanged += new System.EventHandler(this.checkEditEnableNotifications_CheckedChanged);
            // 
            // groupControlNotificationTypes
            // 
            this.groupControlNotificationTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlNotificationTypes.Controls.Add(this.checkEditSalaryIssueNotification);
            this.groupControlNotificationTypes.Controls.Add(this.checkEditAttendanceIssueNotification);
            this.groupControlNotificationTypes.Controls.Add(this.checkEditEmployeeTerminationNotification);
            this.groupControlNotificationTypes.Controls.Add(this.checkEditNewEmployeeNotification);
            this.groupControlNotificationTypes.Controls.Add(this.checkEditLeaveRequestRejectedNotification);
            this.groupControlNotificationTypes.Controls.Add(this.checkEditLeaveRequestApprovedNotification);
            this.groupControlNotificationTypes.Controls.Add(this.checkEditNewLeaveRequestNotification);
            this.groupControlNotificationTypes.Location = new System.Drawing.Point(12, 98);
            this.groupControlNotificationTypes.Name = "groupControlNotificationTypes";
            this.groupControlNotificationTypes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupControlNotificationTypes.Size = new System.Drawing.Size(480, 310);
            this.groupControlNotificationTypes.TabIndex = 1;
            this.groupControlNotificationTypes.Text = "أنواع الإشعارات";
            // 
            // checkEditSalaryIssueNotification
            // 
            this.checkEditSalaryIssueNotification.Location = new System.Drawing.Point(272, 138);
            this.checkEditSalaryIssueNotification.Name = "checkEditSalaryIssueNotification";
            this.checkEditSalaryIssueNotification.Properties.Caption = "إشعار عند مشكلة في الراتب";
            this.checkEditSalaryIssueNotification.Size = new System.Drawing.Size(179, 20);
            this.checkEditSalaryIssueNotification.TabIndex = 6;
            // 
            // checkEditAttendanceIssueNotification
            // 
            this.checkEditAttendanceIssueNotification.Location = new System.Drawing.Point(272, 112);
            this.checkEditAttendanceIssueNotification.Name = "checkEditAttendanceIssueNotification";
            this.checkEditAttendanceIssueNotification.Properties.Caption = "إشعار عند مشكلة في الحضور";
            this.checkEditAttendanceIssueNotification.Size = new System.Drawing.Size(179, 20);
            this.checkEditAttendanceIssueNotification.TabIndex = 5;
            // 
            // checkEditEmployeeTerminationNotification
            // 
            this.checkEditEmployeeTerminationNotification.Location = new System.Drawing.Point(252, 86);
            this.checkEditEmployeeTerminationNotification.Name = "checkEditEmployeeTerminationNotification";
            this.checkEditEmployeeTerminationNotification.Properties.Caption = "إشعار عند إنهاء خدمة موظف";
            this.checkEditEmployeeTerminationNotification.Size = new System.Drawing.Size(199, 20);
            this.checkEditEmployeeTerminationNotification.TabIndex = 4;
            // 
            // checkEditNewEmployeeNotification
            // 
            this.checkEditNewEmployeeNotification.Location = new System.Drawing.Point(268, 60);
            this.checkEditNewEmployeeNotification.Name = "checkEditNewEmployeeNotification";
            this.checkEditNewEmployeeNotification.Properties.Caption = "إشعار عند إضافة موظف جديد";
            this.checkEditNewEmployeeNotification.Size = new System.Drawing.Size(183, 20);
            this.checkEditNewEmployeeNotification.TabIndex = 3;
            // 
            // checkEditLeaveRequestRejectedNotification
            // 
            this.checkEditLeaveRequestRejectedNotification.Location = new System.Drawing.Point(30, 86);
            this.checkEditLeaveRequestRejectedNotification.Name = "checkEditLeaveRequestRejectedNotification";
            this.checkEditLeaveRequestRejectedNotification.Properties.Caption = "إشعار عند رفض طلب الإجازة";
            this.checkEditLeaveRequestRejectedNotification.Size = new System.Drawing.Size(183, 20);
            this.checkEditLeaveRequestRejectedNotification.TabIndex = 2;
            // 
            // checkEditLeaveRequestApprovedNotification
            // 
            this.checkEditLeaveRequestApprovedNotification.Location = new System.Drawing.Point(28, 60);
            this.checkEditLeaveRequestApprovedNotification.Name = "checkEditLeaveRequestApprovedNotification";
            this.checkEditLeaveRequestApprovedNotification.Properties.Caption = "إشعار عند قبول طلب الإجازة";
            this.checkEditLeaveRequestApprovedNotification.Size = new System.Drawing.Size(185, 20);
            this.checkEditLeaveRequestApprovedNotification.TabIndex = 1;
            // 
            // checkEditNewLeaveRequestNotification
            // 
            this.checkEditNewLeaveRequestNotification.Location = new System.Drawing.Point(31, 34);
            this.checkEditNewLeaveRequestNotification.Name = "checkEditNewLeaveRequestNotification";
            this.checkEditNewLeaveRequestNotification.Properties.Caption = "إشعار عند طلب إجازة جديد";
            this.checkEditNewLeaveRequestNotification.Size = new System.Drawing.Size(182, 20);
            this.checkEditNewLeaveRequestNotification.TabIndex = 0;
            // 
            // groupControlNotificationLevel
            // 
            this.groupControlNotificationLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlNotificationLevel.Controls.Add(this.radioGroupNotificationLevel);
            this.groupControlNotificationLevel.Location = new System.Drawing.Point(498, 98);
            this.groupControlNotificationLevel.Name = "groupControlNotificationLevel";
            this.groupControlNotificationLevel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupControlNotificationLevel.Size = new System.Drawing.Size(288, 153);
            this.groupControlNotificationLevel.TabIndex = 2;
            this.groupControlNotificationLevel.Text = "مستوى الإشعارات";
            // 
            // radioGroupNotificationLevel
            // 
            this.radioGroupNotificationLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioGroupNotificationLevel.Location = new System.Drawing.Point(2, 23);
            this.radioGroupNotificationLevel.Name = "radioGroupNotificationLevel";
            this.radioGroupNotificationLevel.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "منخفض - الإشعارات الهامة فقط"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "متوسط - الإشعارات المهمة والمتوسطة الأهمية"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "عالي - كل الإشعارات")});
            this.radioGroupNotificationLevel.Size = new System.Drawing.Size(284, 128);
            this.radioGroupNotificationLevel.TabIndex = 0;
            // 
            // groupControlNotificationTiming
            // 
            this.groupControlNotificationTiming.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlNotificationTiming.Controls.Add(this.labelControl1);
            this.groupControlNotificationTiming.Controls.Add(this.spinEditDailyNotificationHour);
            this.groupControlNotificationTiming.Location = new System.Drawing.Point(498, 257);
            this.groupControlNotificationTiming.Name = "groupControlNotificationTiming";
            this.groupControlNotificationTiming.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupControlNotificationTiming.Size = new System.Drawing.Size(288, 151);
            this.groupControlNotificationTiming.TabIndex = 3;
            this.groupControlNotificationTiming.Text = "توقيت الإشعارات";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(173, 45);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(109, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "ساعة الإشعارات اليومية";
            // 
            // spinEditDailyNotificationHour
            // 
            this.spinEditDailyNotificationHour.EditValue = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.spinEditDailyNotificationHour.Location = new System.Drawing.Point(85, 43);
            this.spinEditDailyNotificationHour.Name = "spinEditDailyNotificationHour";
            this.spinEditDailyNotificationHour.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditDailyNotificationHour.Properties.IsFloatValue = false;
            this.spinEditDailyNotificationHour.Properties.MaskSettings.Set("mask", "N00");
            this.spinEditDailyNotificationHour.Properties.MaxValue = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.spinEditDailyNotificationHour.Size = new System.Drawing.Size(70, 20);
            this.spinEditDailyNotificationHour.TabIndex = 0;
            // 
            // NotificationSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 602);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonControl);
            this.Name = "NotificationSettingsForm";
            this.Ribbon = this.ribbonControl;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "إعدادات الإشعارات";
            this.Load += new System.EventHandler(this.NotificationSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlNotifications)).EndInit();
            this.groupControlNotifications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditEnableSystemNotifications.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditEnableSMSNotifications.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditEnableEmailNotifications.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditEnableNotifications.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlNotificationTypes)).EndInit();
            this.groupControlNotificationTypes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSalaryIssueNotification.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAttendanceIssueNotification.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditEmployeeTerminationNotification.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditNewEmployeeNotification.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditLeaveRequestRejectedNotification.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditLeaveRequestApprovedNotification.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditNewLeaveRequestNotification.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlNotificationLevel)).EndInit();
            this.groupControlNotificationLevel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupNotificationLevel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlNotificationTiming)).EndInit();
            this.groupControlNotificationTiming.ResumeLayout(false);
            this.groupControlNotificationTiming.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditDailyNotificationHour.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSave;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRefresh;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControlNotifications;
        private DevExpress.XtraEditors.CheckEdit checkEditEnableSystemNotifications;
        private DevExpress.XtraEditors.CheckEdit checkEditEnableSMSNotifications;
        private DevExpress.XtraEditors.CheckEdit checkEditEnableEmailNotifications;
        private DevExpress.XtraEditors.CheckEdit checkEditEnableNotifications;
        private DevExpress.XtraEditors.GroupControl groupControlNotificationTypes;
        private DevExpress.XtraEditors.CheckEdit checkEditSalaryIssueNotification;
        private DevExpress.XtraEditors.CheckEdit checkEditAttendanceIssueNotification;
        private DevExpress.XtraEditors.CheckEdit checkEditEmployeeTerminationNotification;
        private DevExpress.XtraEditors.CheckEdit checkEditNewEmployeeNotification;
        private DevExpress.XtraEditors.CheckEdit checkEditLeaveRequestRejectedNotification;
        private DevExpress.XtraEditors.CheckEdit checkEditLeaveRequestApprovedNotification;
        private DevExpress.XtraEditors.CheckEdit checkEditNewLeaveRequestNotification;
        private DevExpress.XtraEditors.GroupControl groupControlNotificationLevel;
        private DevExpress.XtraEditors.RadioGroup radioGroupNotificationLevel;
        private DevExpress.XtraEditors.GroupControl groupControlNotificationTiming;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit spinEditDailyNotificationHour;
    }
}