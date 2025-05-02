namespace HR.UI.Forms
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.progressBarControl = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.labelCopyright = new DevExpress.XtraEditors.LabelControl();
            this.labelStatus = new DevExpress.XtraEditors.LabelControl();
            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            this.pictureEditLogo = new DevExpress.XtraEditors.PictureEdit();
            this.labelCompanyName = new DevExpress.XtraEditors.LabelControl();
            this.labelApplicationName = new DevExpress.XtraEditors.LabelControl();
            this.labelApplicationVersion = new DevExpress.XtraEditors.LabelControl();
            this.panelContent = new DevExpress.XtraEditors.PanelControl();
            this.marqueeTimer = new System.Windows.Forms.Timer(this.components);
            this.circularProgress = new DevExpress.XtraWaitForm.ProgressPanel();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).BeginInit();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBarControl
            // 
            this.progressBarControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl.EditValue = 0;
            this.progressBarControl.Location = new System.Drawing.Point(28, 280);
            this.progressBarControl.Name = "progressBarControl";
            this.progressBarControl.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.progressBarControl.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.progressBarControl.Properties.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(188)))));
            this.progressBarControl.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.progressBarControl.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.progressBarControl.Properties.MarqueeAnimationSpeed = 70;
            this.progressBarControl.Properties.ProgressAnimationMode = DevExpress.Utils.Drawing.ProgressAnimationMode.Cycle;
            this.progressBarControl.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.progressBarControl.Properties.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(188)))));
            this.progressBarControl.Size = new System.Drawing.Size(444, 8);
            this.progressBarControl.TabIndex = 5;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCopyright.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelCopyright.Appearance.Options.UseFont = true;
            this.labelCopyright.Appearance.Options.UseForeColor = true;
            this.labelCopyright.Appearance.Options.UseTextOptions = true;
            this.labelCopyright.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelCopyright.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelCopyright.Location = new System.Drawing.Point(28, 320);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(444, 13);
            this.labelCopyright.TabIndex = 6;
            this.labelCopyright.Text = "Copyright © 2023";
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelStatus.Appearance.Options.UseFont = true;
            this.labelStatus.Appearance.Options.UseForeColor = true;
            this.labelStatus.Appearance.Options.UseTextOptions = true;
            this.labelStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelStatus.Location = new System.Drawing.Point(28, 295);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(444, 20);
            this.labelStatus.TabIndex = 7;
            this.labelStatus.Text = "جاري تهيئة النظام...";
            // 
            // panelTop
            // 
            this.panelTop.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.panelTop.Appearance.Options.UseBackColor = true;
            this.panelTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelTop.Controls.Add(this.pictureEditLogo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(1, 1);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(498, 120);
            this.panelTop.TabIndex = 9;
            // 
            // pictureEditLogo
            // 
            this.pictureEditLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureEditLogo.EditValue = ((object)(resources.GetObject("pictureEditLogo.EditValue")));
            this.pictureEditLogo.Location = new System.Drawing.Point(174, 10);
            this.pictureEditLogo.Name = "pictureEditLogo";
            this.pictureEditLogo.Properties.AllowFocused = false;
            this.pictureEditLogo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEditLogo.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEditLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEditLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEditLogo.Properties.ShowMenu = false;
            this.pictureEditLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEditLogo.Size = new System.Drawing.Size(150, 100);
            this.pictureEditLogo.TabIndex = 0;
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelCompanyName.Appearance.Options.UseFont = true;
            this.labelCompanyName.Appearance.Options.UseTextOptions = true;
            this.labelCompanyName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelCompanyName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelCompanyName.Location = new System.Drawing.Point(28, 37);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(444, 22);
            this.labelCompanyName.TabIndex = 10;
            this.labelCompanyName.Text = "شركتي للبرمجيات";
            // 
            // labelApplicationName
            // 
            this.labelApplicationName.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelApplicationName.Appearance.Options.UseFont = true;
            this.labelApplicationName.Appearance.Options.UseTextOptions = true;
            this.labelApplicationName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelApplicationName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelApplicationName.Location = new System.Drawing.Point(28, 63);
            this.labelApplicationName.Name = "labelApplicationName";
            this.labelApplicationName.Size = new System.Drawing.Size(444, 30);
            this.labelApplicationName.TabIndex = 11;
            this.labelApplicationName.Text = "نظام إدارة الموارد البشرية";
            // 
            // labelApplicationVersion
            // 
            this.labelApplicationVersion.Appearance.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.labelApplicationVersion.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelApplicationVersion.Appearance.Options.UseFont = true;
            this.labelApplicationVersion.Appearance.Options.UseForeColor = true;
            this.labelApplicationVersion.Appearance.Options.UseTextOptions = true;
            this.labelApplicationVersion.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelApplicationVersion.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelApplicationVersion.Location = new System.Drawing.Point(28, 94);
            this.labelApplicationVersion.Name = "labelApplicationVersion";
            this.labelApplicationVersion.Size = new System.Drawing.Size(444, 20);
            this.labelApplicationVersion.TabIndex = 12;
            this.labelApplicationVersion.Text = "الإصدار 1.0.0";
            // 
            // panelContent
            // 
            this.panelContent.Appearance.BackColor = System.Drawing.Color.White;
            this.panelContent.Appearance.Options.UseBackColor = true;
            this.panelContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelContent.Controls.Add(this.circularProgress);
            this.panelContent.Controls.Add(this.labelCompanyName);
            this.panelContent.Controls.Add(this.labelApplicationVersion);
            this.panelContent.Controls.Add(this.labelApplicationName);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(1, 121);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(498, 228);
            this.panelContent.TabIndex = 13;
            // 
            // marqueeTimer
            // 
            this.marqueeTimer.Enabled = true;
            this.marqueeTimer.Interval = 50;
            this.marqueeTimer.Tick += new System.EventHandler(this.marqueeTimer_Tick);
            // 
            // circularProgress
            // 
            this.circularProgress.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.circularProgress.Appearance.Options.UseBackColor = true;
            this.circularProgress.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circularProgress.AppearanceCaption.Options.UseFont = true;
            this.circularProgress.AppearanceDescription.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circularProgress.AppearanceDescription.Options.UseFont = true;
            this.circularProgress.BarAnimationElementThickness = 2;
            this.circularProgress.Caption = "نظام إدارة الموارد البشرية";
            this.circularProgress.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.circularProgress.Description = "جاري التحميل...";
            this.circularProgress.Location = new System.Drawing.Point(174, 132);
            this.circularProgress.Name = "circularProgress";
            this.circularProgress.Size = new System.Drawing.Size(150, 66);
            this.circularProgress.TabIndex = 13;
            this.circularProgress.WaitAnimationType = DevExpress.Utils.Animation.WaitingAnimatorType.Ring;
            // 
            // SplashScreen
            // 
            this.ActiveGlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(188)))));
            this.AllowControlsInImageMode = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.progressBarControl);
            this.InactiveGlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(163)))), ((int)(((byte)(218)))));
            this.Name = "SplashScreen";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "نظام إدارة الموارد البشرية";
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).EndInit();
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MarqueeProgressBarControl progressBarControl;
        private DevExpress.XtraEditors.LabelControl labelCopyright;
        private DevExpress.XtraEditors.LabelControl labelStatus;
        private DevExpress.XtraEditors.PanelControl panelTop;
        private DevExpress.XtraEditors.PictureEdit pictureEditLogo;
        private DevExpress.XtraEditors.LabelControl labelCompanyName;
        private DevExpress.XtraEditors.LabelControl labelApplicationName;
        private DevExpress.XtraEditors.LabelControl labelApplicationVersion;
        private DevExpress.XtraEditors.PanelControl panelContent;
        private System.Windows.Forms.Timer marqueeTimer;
        private DevExpress.XtraWaitForm.ProgressPanel circularProgress;
    }
}