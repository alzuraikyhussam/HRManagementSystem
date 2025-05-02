namespace HR.UI.Forms.Security
{
    partial class RolesListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RolesListForm));
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlRoles = new DevExpress.XtraGrid.GridControl();
            this.gridViewRoles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedAt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdatedAt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.buttonDelete = new DevExpress.XtraEditors.SimpleButton();
            this.buttonEdit = new DevExpress.XtraEditors.SimpleButton();
            this.buttonAdd = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupRoles = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.gridControlRoles);
            this.layoutControl.Controls.Add(this.panelControl1);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1108, 360, 650, 400);
            this.layoutControl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.layoutControl.Root = this.layoutControlGroup1;
            this.layoutControl.Size = new System.Drawing.Size(984, 561);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // gridControlRoles
            // 
            this.gridControlRoles.Location = new System.Drawing.Point(24, 45);
            this.gridControlRoles.MainView = this.gridViewRoles;
            this.gridControlRoles.Name = "gridControlRoles";
            this.gridControlRoles.Size = new System.Drawing.Size(936, 438);
            this.gridControlRoles.TabIndex = 4;
            this.gridControlRoles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRoles});
            // 
            // gridViewRoles
            // 
            this.gridViewRoles.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewRoles.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewRoles.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.gridViewRoles.Appearance.Row.Options.UseFont = true;
            this.gridViewRoles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colName,
            this.colDescription,
            this.colCreatedAt,
            this.colUpdatedAt});
            this.gridViewRoles.GridControl = this.gridControlRoles;
            this.gridViewRoles.Name = "gridViewRoles";
            this.gridViewRoles.OptionsFind.AlwaysVisible = true;
            this.gridViewRoles.OptionsFind.FindNullPrompt = "بحث...";
            this.gridViewRoles.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewRoles.OptionsView.ShowAutoFilterRow = true;
            this.gridViewRoles.OptionsView.ShowGroupPanel = false;
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
            // colName
            // 
            this.colName.Caption = "اسم الدور";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsColumn.AllowFocus = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 200;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "الوصف";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowEdit = false;
            this.colDescription.OptionsColumn.AllowFocus = false;
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 2;
            this.colDescription.Width = 350;
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
            this.colCreatedAt.VisibleIndex = 3;
            this.colCreatedAt.Width = 150;
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
            this.colUpdatedAt.VisibleIndex = 4;
            this.colUpdatedAt.Width = 150;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.buttonRefresh);
            this.panelControl1.Controls.Add(this.buttonDelete);
            this.panelControl1.Controls.Add(this.buttonEdit);
            this.panelControl1.Controls.Add(this.buttonAdd);
            this.panelControl1.Location = new System.Drawing.Point(24, 487);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(936, 50);
            this.panelControl1.TabIndex = 5;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonRefresh.Appearance.Options.UseFont = true;
            this.buttonRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonRefresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonRefresh.ImageOptions.SvgImage")));
            this.buttonRefresh.Location = new System.Drawing.Point(690, 2);
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
            this.buttonDelete.Location = new System.Drawing.Point(810, 2);
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
            this.layoutControlGroupRoles});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(984, 561);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroupRoles
            // 
            this.layoutControlGroupRoles.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroupRoles.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroupRoles.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroupRoles.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupRoles.Name = "layoutControlGroupRoles";
            this.layoutControlGroupRoles.Size = new System.Drawing.Size(964, 541);
            this.layoutControlGroupRoles.Text = "قائمة الأدوار والصلاحيات";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlRoles;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(940, 442);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 442);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 54);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(104, 54);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(940, 54);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // RolesListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.layoutControl);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("RolesListForm.IconOptions.SvgImage")));
            this.Name = "RolesListForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إدارة الأدوار والصلاحيات";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControlRoles;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRoles;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoles;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton buttonRefresh;
        private DevExpress.XtraEditors.SimpleButton buttonDelete;
        private DevExpress.XtraEditors.SimpleButton buttonEdit;
        private DevExpress.XtraEditors.SimpleButton buttonAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedAt;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdatedAt;
    }
}