namespace HR.UI.Forms.Security
{
    partial class UsersListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersListForm));
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlUsers = new DevExpress.XtraGrid.GridControl();
            this.gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUsername = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsLocked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastLogin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedAt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdatedAt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.buttonDelete = new DevExpress.XtraEditors.SimpleButton();
            this.buttonEdit = new DevExpress.XtraEditors.SimpleButton();
            this.buttonAdd = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupUsers = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.gridControlUsers);
            this.layoutControl.Controls.Add(this.panelControl1);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1108, 360, 650, 400);
            this.layoutControl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.layoutControl.Root = this.layoutControlGroup1;
            this.layoutControl.Size = new System.Drawing.Size(1284, 661);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // gridControlUsers
            // 
            this.gridControlUsers.Location = new System.Drawing.Point(24, 45);
            this.gridControlUsers.MainView = this.gridViewUsers;
            this.gridControlUsers.Name = "gridControlUsers";
            this.gridControlUsers.Size = new System.Drawing.Size(1236, 538);
            this.gridControlUsers.TabIndex = 4;
            this.gridControlUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUsers});
            // 
            // gridViewUsers
            // 
            this.gridViewUsers.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewUsers.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewUsers.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.gridViewUsers.Appearance.Row.Options.UseFont = true;
            this.gridViewUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colUsername,
            this.colFullName,
            this.colEmail,
            this.colRoleName,
            this.colEmployeeName,
            this.colIsActive,
            this.colIsLocked,
            this.colLastLogin,
            this.colCreatedAt,
            this.colUpdatedAt});
            this.gridViewUsers.GridControl = this.gridControlUsers;
            this.gridViewUsers.Name = "gridViewUsers";
            this.gridViewUsers.OptionsFind.AlwaysVisible = true;
            this.gridViewUsers.OptionsFind.FindNullPrompt = "بحث...";
            this.gridViewUsers.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewUsers.OptionsView.ShowAutoFilterRow = true;
            this.gridViewUsers.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.Caption = "الرقم";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.OptionsColumn.AllowFocus = false;
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 60;
            // 
            // colUsername
            // 
            this.colUsername.Caption = "اسم المستخدم";
            this.colUsername.FieldName = "Username";
            this.colUsername.Name = "colUsername";
            this.colUsername.OptionsColumn.AllowEdit = false;
            this.colUsername.OptionsColumn.AllowFocus = false;
            this.colUsername.Visible = true;
            this.colUsername.VisibleIndex = 1;
            this.colUsername.Width = 120;
            // 
            // colFullName
            // 
            this.colFullName.Caption = "الاسم الكامل";
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsColumn.AllowEdit = false;
            this.colFullName.OptionsColumn.AllowFocus = false;
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 2;
            this.colFullName.Width = 180;
            // 
            // colEmail
            // 
            this.colEmail.Caption = "البريد الإلكتروني";
            this.colEmail.FieldName = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.OptionsColumn.AllowEdit = false;
            this.colEmail.OptionsColumn.AllowFocus = false;
            this.colEmail.Visible = true;
            this.colEmail.VisibleIndex = 3;
            this.colEmail.Width = 180;
            // 
            // colRoleName
            // 
            this.colRoleName.Caption = "الدور";
            this.colRoleName.FieldName = "Role.Name";
            this.colRoleName.Name = "colRoleName";
            this.colRoleName.OptionsColumn.AllowEdit = false;
            this.colRoleName.OptionsColumn.AllowFocus = false;
            this.colRoleName.Visible = true;
            this.colRoleName.VisibleIndex = 4;
            this.colRoleName.Width = 120;
            // 
            // colEmployeeName
            // 
            this.colEmployeeName.Caption = "الموظف";
            this.colEmployeeName.FieldName = "Employee.FullName";
            this.colEmployeeName.Name = "colEmployeeName";
            this.colEmployeeName.OptionsColumn.AllowEdit = false;
            this.colEmployeeName.OptionsColumn.AllowFocus = false;
            this.colEmployeeName.Visible = true;
            this.colEmployeeName.VisibleIndex = 5;
            this.colEmployeeName.Width = 150;
            // 
            // colIsActive
            // 
            this.colIsActive.Caption = "نشط";
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.OptionsColumn.AllowEdit = false;
            this.colIsActive.OptionsColumn.AllowFocus = false;
            this.colIsActive.Visible = true;
            this.colIsActive.VisibleIndex = 6;
            this.colIsActive.Width = 60;
            // 
            // colIsLocked
            // 
            this.colIsLocked.Caption = "مقفل";
            this.colIsLocked.FieldName = "IsLocked";
            this.colIsLocked.Name = "colIsLocked";
            this.colIsLocked.OptionsColumn.AllowEdit = false;
            this.colIsLocked.OptionsColumn.AllowFocus = false;
            this.colIsLocked.Visible = true;
            this.colIsLocked.VisibleIndex = 7;
            this.colIsLocked.Width = 60;
            // 
            // colLastLogin
            // 
            this.colLastLogin.Caption = "آخر تسجيل دخول";
            this.colLastLogin.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt";
            this.colLastLogin.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLastLogin.FieldName = "LastLogin";
            this.colLastLogin.Name = "colLastLogin";
            this.colLastLogin.OptionsColumn.AllowEdit = false;
            this.colLastLogin.OptionsColumn.AllowFocus = false;
            this.colLastLogin.Visible = true;
            this.colLastLogin.VisibleIndex = 8;
            this.colLastLogin.Width = 120;
            // 
            // colCreatedAt
            // 
            this.colCreatedAt.Caption = "تاريخ الإنشاء";
            this.colCreatedAt.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt";
            this.colCreatedAt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreatedAt.FieldName = "CreatedAt";
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.OptionsColumn.AllowEdit = false;
            this.colCreatedAt.OptionsColumn.AllowFocus = false;
            this.colCreatedAt.Visible = true;
            this.colCreatedAt.VisibleIndex = 9;
            this.colCreatedAt.Width = 120;
            // 
            // colUpdatedAt
            // 
            this.colUpdatedAt.Caption = "تاريخ التعديل";
            this.colUpdatedAt.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt";
            this.colUpdatedAt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colUpdatedAt.FieldName = "UpdatedAt";
            this.colUpdatedAt.Name = "colUpdatedAt";
            this.colUpdatedAt.OptionsColumn.AllowEdit = false;
            this.colUpdatedAt.OptionsColumn.AllowFocus = false;
            this.colUpdatedAt.Visible = true;
            this.colUpdatedAt.VisibleIndex = 10;
            this.colUpdatedAt.Width = 120;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.buttonRefresh);
            this.panelControl1.Controls.Add(this.buttonDelete);
            this.panelControl1.Controls.Add(this.buttonEdit);
            this.panelControl1.Controls.Add(this.buttonAdd);
            this.panelControl1.Location = new System.Drawing.Point(24, 587);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1236, 50);
            this.panelControl1.TabIndex = 5;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonRefresh.Appearance.Options.UseFont = true;
            this.buttonRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonRefresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonRefresh.ImageOptions.SvgImage")));
            this.buttonRefresh.Location = new System.Drawing.Point(990, 2);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(120, 46);
            this.buttonRefresh.TabIndex = 3;
            this.buttonRefresh.Text = "تحديث";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonDelete.Appearance.Options.UseFont = true;
            this.buttonDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonDelete.ImageOptions.SvgImage")));
            this.buttonDelete.Location = new System.Drawing.Point(1110, 2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(124, 46);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "حذف";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonEdit.Appearance.Options.UseFont = true;
            this.buttonEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonEdit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonEdit.ImageOptions.SvgImage")));
            this.buttonEdit.Location = new System.Drawing.Point(127, 2);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(120, 46);
            this.buttonEdit.TabIndex = 1;
            this.buttonEdit.Text = "تعديل";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonAdd.Appearance.Options.UseFont = true;
            this.buttonAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonAdd.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonAdd.ImageOptions.SvgImage")));
            this.buttonAdd.Location = new System.Drawing.Point(2, 2);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(125, 46);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "إضافة";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupUsers});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1284, 661);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroupUsers
            // 
            this.layoutControlGroupUsers.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroupUsers.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroupUsers.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroupUsers.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupUsers.Name = "layoutControlGroupUsers";
            this.layoutControlGroupUsers.Size = new System.Drawing.Size(1264, 641);
            this.layoutControlGroupUsers.Text = "قائمة المستخدمين";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlUsers;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1240, 542);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 542);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 54);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(104, 54);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1240, 54);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // UsersListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 661);
            this.Controls.Add(this.layoutControl);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("UsersListForm.IconOptions.SvgImage")));
            this.Name = "UsersListForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إدارة المستخدمين";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControlUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsers;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupUsers;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton buttonRefresh;
        private DevExpress.XtraEditors.SimpleButton buttonDelete;
        private DevExpress.XtraEditors.SimpleButton buttonEdit;
        private DevExpress.XtraEditors.SimpleButton buttonAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colUsername;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colRoleName;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
        private DevExpress.XtraGrid.Columns.GridColumn colIsLocked;
        private DevExpress.XtraGrid.Columns.GridColumn colLastLogin;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedAt;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdatedAt;
    }
}