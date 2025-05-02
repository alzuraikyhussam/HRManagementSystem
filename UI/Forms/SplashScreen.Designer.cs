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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.progressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
            this.labelCopyright = new DevExpress.XtraEditors.LabelControl();
            this.labelStatus = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelCompanyName = new DevExpress.XtraEditors.LabelControl();
            this.labelApplicationName = new DevExpress.XtraEditors.LabelControl();
            this.labelApplicationVersion = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBarControl
            // 
            this.progressBarControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl.Location = new System.Drawing.Point(28, 210);
            this.progressBarControl.Name = "progressBarControl";
            this.progressBarControl.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.progressBarControl.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.progressBarControl.Size = new System.Drawing.Size(444, 12);
            this.progressBarControl.TabIndex = 5;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCopyright.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelCopyright.Appearance.Options.UseForeColor = true;
            this.labelCopyright.Appearance.Options.UseTextOptions = true;
            this.labelCopyright.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelCopyright.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelCopyright.Location = new System.Drawing.Point(28, 257);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(444, 13);
            this.labelCopyright.TabIndex = 6;
            this.labelCopyright.Text = "Copyright © 2023";
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelStatus.Appearance.Options.UseForeColor = true;
            this.labelStatus.Appearance.Options.UseTextOptions = true;
            this.labelStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelStatus.Location = new System.Drawing.Point(28, 230);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(444, 20);
            this.labelStatus.TabIndex = 7;
            this.labelStatus.Text = "جاري تهيئة النظام...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(498, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.Appearance.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Bold);
            this.labelCompanyName.Appearance.Options.UseFont = true;
            this.labelCompanyName.Appearance.Options.UseTextOptions = true;
            this.labelCompanyName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelCompanyName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelCompanyName.Location = new System.Drawing.Point(28, 127);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(444, 22);
            this.labelCompanyName.TabIndex = 10;
            this.labelCompanyName.Text = "شركتي للبرمجيات";
            // 
            // labelApplicationName
            // 
            this.labelApplicationName.Appearance.Font = new System.Drawing.Font("Cairo", 14F, System.Drawing.FontStyle.Bold);
            this.labelApplicationName.Appearance.Options.UseFont = true;
            this.labelApplicationName.Appearance.Options.UseTextOptions = true;
            this.labelApplicationName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelApplicationName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelApplicationName.Location = new System.Drawing.Point(28, 153);
            this.labelApplicationName.Name = "labelApplicationName";
            this.labelApplicationName.Size = new System.Drawing.Size(444, 30);
            this.labelApplicationName.TabIndex = 11;
            this.labelApplicationName.Text = "نظام إدارة الموارد البشرية";
            // 
            // labelApplicationVersion
            // 
            this.labelApplicationVersion.Appearance.Font = new System.Drawing.Font("Cairo", 8F);
            this.labelApplicationVersion.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelApplicationVersion.Appearance.Options.UseFont = true;
            this.labelApplicationVersion.Appearance.Options.UseForeColor = true;
            this.labelApplicationVersion.Appearance.Options.UseTextOptions = true;
            this.labelApplicationVersion.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelApplicationVersion.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelApplicationVersion.Location = new System.Drawing.Point(28, 184);
            this.labelApplicationVersion.Name = "labelApplicationVersion";
            this.labelApplicationVersion.Size = new System.Drawing.Size(444, 20);
            this.labelApplicationVersion.TabIndex = 12;
            this.labelApplicationVersion.Text = "الإصدار 1.0.0";
            // 
            // SplashScreen
            // 
            this.ActiveGlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(188)))));
            this.AllowControlsInImageMode = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 280);
            this.Controls.Add(this.labelApplicationVersion);
            this.Controls.Add(this.labelApplicationName);
            this.Controls.Add(this.labelCompanyName);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.progressBarControl);
            this.InactiveGlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(163)))), ((int)(((byte)(218)))));
            this.Name = "SplashScreen";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "SplashScreen";
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl progressBarControl;
        private DevExpress.XtraEditors.LabelControl labelCopyright;
        private DevExpress.XtraEditors.LabelControl labelStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelCompanyName;
        private DevExpress.XtraEditors.LabelControl labelApplicationName;
        private DevExpress.XtraEditors.LabelControl labelApplicationVersion;
    }
}