namespace HR.UI.Forms
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.labelCompanyName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEditUsername = new DevExpress.XtraEditors.TextEdit();
            this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
            this.buttonLogin = new DevExpress.XtraEditors.SimpleButton();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.linkLabelForgotPassword = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.labelVersion = new DevExpress.XtraEditors.LabelControl();
            this.labelComputer = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(157, 12);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.Size = new System.Drawing.Size(150, 100);
            this.pictureEdit1.TabIndex = 0;
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelCompanyName.Appearance.Options.UseFont = true;
            this.labelCompanyName.Appearance.Options.UseTextOptions = true;
            this.labelCompanyName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelCompanyName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelCompanyName.Location = new System.Drawing.Point(12, 118);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(440, 22);
            this.labelCompanyName.TabIndex = 1;
            this.labelCompanyName.Text = "نظام إدارة الموارد البشرية";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(18, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 17);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "اسم المستخدم:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(18, 52);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 17);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "كلمة المرور:";
            // 
            // textEditUsername
            // 
            this.textEditUsername.Location = new System.Drawing.Point(95, 15);
            this.textEditUsername.Name = "textEditUsername";
            this.textEditUsername.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.textEditUsername.Properties.Appearance.Options.UseFont = true;
            this.textEditUsername.Size = new System.Drawing.Size(327, 24);
            this.textEditUsername.TabIndex = 0;
            // 
            // textEditPassword
            // 
            this.textEditPassword.Location = new System.Drawing.Point(95, 49);
            this.textEditPassword.Name = "textEditPassword";
            this.textEditPassword.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.textEditPassword.Properties.Appearance.Options.UseFont = true;
            this.textEditPassword.Properties.UseSystemPasswordChar = true;
            this.textEditPassword.Size = new System.Drawing.Size(327, 24);
            this.textEditPassword.TabIndex = 1;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonLogin.Appearance.Options.UseFont = true;
            this.buttonLogin.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.buttonLogin.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonLogin.ImageOptions.SvgImage")));
            this.buttonLogin.Location = new System.Drawing.Point(222, 84);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(200, 32);
            this.buttonLogin.TabIndex = 2;
            this.buttonLogin.Text = "تسجيل الدخول";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonCancel.Appearance.Options.UseFont = true;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.buttonCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonCancel.ImageOptions.SvgImage")));
            this.buttonCancel.Location = new System.Drawing.Point(18, 84);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(198, 32);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "إلغاء";
            // 
            // linkLabelForgotPassword
            // 
            this.linkLabelForgotPassword.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.linkLabelForgotPassword.Appearance.Options.UseFont = true;
            this.linkLabelForgotPassword.Location = new System.Drawing.Point(18, 122);
            this.linkLabelForgotPassword.Name = "linkLabelForgotPassword";
            this.linkLabelForgotPassword.Size = new System.Drawing.Size(92, 15);
            this.linkLabelForgotPassword.TabIndex = 4;
            this.linkLabelForgotPassword.Text = "نسيت كلمة المرور؟";
            // 
            // labelVersion
            // 
            this.labelVersion.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelVersion.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelVersion.Appearance.Options.UseFont = true;
            this.labelVersion.Appearance.Options.UseForeColor = true;
            this.labelVersion.Location = new System.Drawing.Point(18, 373);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(50, 13);
            this.labelVersion.TabIndex = 9;
            this.labelVersion.Text = "الإصدار 1.0";
            // 
            // labelComputer
            // 
            this.labelComputer.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelComputer.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelComputer.Appearance.Options.UseFont = true;
            this.labelComputer.Appearance.Options.UseForeColor = true;
            this.labelComputer.Appearance.Options.UseTextOptions = true;
            this.labelComputer.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelComputer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelComputer.Location = new System.Drawing.Point(278, 373);
            this.labelComputer.Name = "labelComputer";
            this.labelComputer.Size = new System.Drawing.Size(174, 13);
            this.labelComputer.TabIndex = 10;
            this.labelComputer.Text = "اسم الجهاز: COMPUTER";
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Controls.Add(this.labelCompanyName);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(464, 160);
            this.panelControl1.TabIndex = 11;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.textEditUsername);
            this.panelControl2.Controls.Add(this.textEditPassword);
            this.panelControl2.Controls.Add(this.buttonLogin);
            this.panelControl2.Controls.Add(this.buttonCancel);
            this.panelControl2.Controls.Add(this.linkLabelForgotPassword);
            this.panelControl2.Location = new System.Drawing.Point(12, 186);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(440, 152);
            this.panelControl2.TabIndex = 12;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.buttonLogin;
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(464, 399);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.labelComputer);
            this.Controls.Add(this.labelVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("LoginForm.IconOptions.SvgImage")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تسجيل الدخول";
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl labelCompanyName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEditUsername;
        private DevExpress.XtraEditors.TextEdit textEditPassword;
        private DevExpress.XtraEditors.SimpleButton buttonLogin;
        private DevExpress.XtraEditors.SimpleButton buttonCancel;
        private DevExpress.XtraEditors.HyperlinkLabelControl linkLabelForgotPassword;
        private DevExpress.XtraEditors.LabelControl labelVersion;
        private DevExpress.XtraEditors.LabelControl labelComputer;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
    }
}