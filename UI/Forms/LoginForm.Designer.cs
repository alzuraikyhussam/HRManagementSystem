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
            this.pictureEditLogo = new DevExpress.XtraEditors.PictureEdit();
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
            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            this.panelContent = new DevExpress.XtraEditors.PanelControl();
            this.checkRememberMe = new DevExpress.XtraEditors.CheckEdit();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.toggleSwitchShowPassword = new DevExpress.XtraEditors.ToggleSwitch();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).BeginInit();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkRememberMe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitchShowPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureEditLogo
            // 
            this.pictureEditLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureEditLogo.EditValue = ((object)(resources.GetObject("pictureEditLogo.EditValue")));
            this.pictureEditLogo.Location = new System.Drawing.Point(157, 12);
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
            this.labelCompanyName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelCompanyName.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelCompanyName.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelCompanyName.Appearance.Options.UseFont = true;
            this.labelCompanyName.Appearance.Options.UseForeColor = true;
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
            this.textEditUsername.Properties.NullText = "أدخل اسم المستخدم";
            this.textEditUsername.Size = new System.Drawing.Size(327, 24);
            this.textEditUsername.TabIndex = 0;
            // 
            // textEditPassword
            // 
            this.textEditPassword.Location = new System.Drawing.Point(95, 49);
            this.textEditPassword.Name = "textEditPassword";
            this.textEditPassword.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.textEditPassword.Properties.Appearance.Options.UseFont = true;
            this.textEditPassword.Properties.NullText = "أدخل كلمة المرور";
            this.textEditPassword.Properties.UseSystemPasswordChar = true;
            this.textEditPassword.Size = new System.Drawing.Size(293, 24);
            this.textEditPassword.TabIndex = 1;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonLogin.Appearance.Options.UseFont = true;
            this.buttonLogin.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.buttonLogin.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonLogin.ImageOptions.SvgImage")));
            this.buttonLogin.Location = new System.Drawing.Point(222, 130);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(200, 32);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "تسجيل الدخول";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonCancel.Appearance.Options.UseFont = true;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.buttonCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonCancel.ImageOptions.SvgImage")));
            this.buttonCancel.Location = new System.Drawing.Point(18, 130);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(198, 32);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "إلغاء";
            // 
            // linkLabelForgotPassword
            // 
            this.linkLabelForgotPassword.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.linkLabelForgotPassword.Appearance.Options.UseFont = true;
            this.linkLabelForgotPassword.Location = new System.Drawing.Point(18, 168);
            this.linkLabelForgotPassword.Name = "linkLabelForgotPassword";
            this.linkLabelForgotPassword.Size = new System.Drawing.Size(92, 15);
            this.linkLabelForgotPassword.TabIndex = 5;
            this.linkLabelForgotPassword.Text = "نسيت كلمة المرور؟";
            // 
            // labelVersion
            // 
            this.labelVersion.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelVersion.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelVersion.Appearance.Options.UseFont = true;
            this.labelVersion.Appearance.Options.UseForeColor = true;
            this.labelVersion.Location = new System.Drawing.Point(18, 409);
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
            this.labelComputer.Location = new System.Drawing.Point(278, 409);
            this.labelComputer.Name = "labelComputer";
            this.labelComputer.Size = new System.Drawing.Size(174, 13);
            this.labelComputer.TabIndex = 10;
            this.labelComputer.Text = "اسم الجهاز: COMPUTER";
            // 
            // panelTop
            // 
            this.panelTop.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.panelTop.Appearance.Options.UseBackColor = true;
            this.panelTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelTop.Controls.Add(this.pictureEditLogo);
            this.panelTop.Controls.Add(this.labelCompanyName);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(464, 160);
            this.panelTop.TabIndex = 11;
            // 
            // panelContent
            // 
            this.panelContent.Appearance.BackColor = System.Drawing.Color.White;
            this.panelContent.Appearance.Options.UseBackColor = true;
            this.panelContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelContent.Controls.Add(this.checkRememberMe);
            this.panelContent.Controls.Add(this.separatorControl1);
            this.panelContent.Controls.Add(this.toggleSwitchShowPassword);
            this.panelContent.Controls.Add(this.labelControl1);
            this.panelContent.Controls.Add(this.labelControl2);
            this.panelContent.Controls.Add(this.textEditUsername);
            this.panelContent.Controls.Add(this.textEditPassword);
            this.panelContent.Controls.Add(this.buttonLogin);
            this.panelContent.Controls.Add(this.buttonCancel);
            this.panelContent.Controls.Add(this.linkLabelForgotPassword);
            this.panelContent.Location = new System.Drawing.Point(12, 186);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(440, 200);
            this.panelContent.TabIndex = 12;
            // 
            // checkRememberMe
            // 
            this.checkRememberMe.Location = new System.Drawing.Point(95, 79);
            this.checkRememberMe.Name = "checkRememberMe";
            this.checkRememberMe.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkRememberMe.Properties.Appearance.Options.UseFont = true;
            this.checkRememberMe.Properties.Caption = "تذكر بيانات الدخول";
            this.checkRememberMe.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.checkRememberMe.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkRememberMe.Size = new System.Drawing.Size(129, 19);
            this.checkRememberMe.TabIndex = 2;
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(18, 104);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(404, 20);
            this.separatorControl1.TabIndex = 13;
            // 
            // toggleSwitchShowPassword
            // 
            this.toggleSwitchShowPassword.Location = new System.Drawing.Point(394, 51);
            this.toggleSwitchShowPassword.Name = "toggleSwitchShowPassword";
            this.toggleSwitchShowPassword.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitchShowPassword.Properties.Appearance.Options.UseFont = true;
            this.toggleSwitchShowPassword.Properties.AutoWidth = true;
            this.toggleSwitchShowPassword.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.toggleSwitchShowPassword.Properties.OffText = "إظهار";
            this.toggleSwitchShowPassword.Properties.OnText = "إخفاء";
            this.toggleSwitchShowPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toggleSwitchShowPassword.Size = new System.Drawing.Size(82, 20);
            this.toggleSwitchShowPassword.TabIndex = 6;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.buttonLogin;
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(464, 434);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTop);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).EndInit();
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkRememberMe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitchShowPassword.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEditLogo;
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
        private DevExpress.XtraEditors.PanelControl panelTop;
        private DevExpress.XtraEditors.PanelControl panelContent;
        private DevExpress.XtraEditors.CheckEdit checkRememberMe;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitchShowPassword;
    }
}