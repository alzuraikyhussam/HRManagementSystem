namespace HR.UI.Forms.Security
{
    partial class RoleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoleForm));
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.memoEditDescription = new DevExpress.XtraEditors.MemoEdit();
            this.gridControlPermissions = new DevExpress.XtraGrid.GridControl();
            this.gridViewPermissions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colModuleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCanView = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCanAdd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCanEdit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCanDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCanPrint = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCanExport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCanImport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCanApprove = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit8 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.buttonSave = new DevExpress.XtraEditors.SimpleButton();
            this.buttonDeselectAll = new DevExpress.XtraEditors.SimpleButton();
            this.buttonSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupRoleInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupPermissions = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPermissions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPermissions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoleInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupPermissions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.textEditName);
            this.layoutControl.Controls.Add(this.memoEditDescription);
            this.layoutControl.Controls.Add(this.gridControlPermissions);
            this.layoutControl.Controls.Add(this.panelControl1);
            this.layoutControl.Controls.Add(this.buttonDeselectAll);
            this.layoutControl.Controls.Add(this.buttonSelectAll);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1108, 360, 650, 400);
            this.layoutControl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.layoutControl.Root = this.layoutControlGroup1;
            this.layoutControl.Size = new System.Drawing.Size(1024, 768);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(121, 45);
            this.textEditName.Name = "textEditName";
            this.textEditName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.textEditName.Properties.Appearance.Options.UseFont = true;
            this.textEditName.Size = new System.Drawing.Size(803, 24);
            this.textEditName.StyleController = this.layoutControl;
            this.textEditName.TabIndex = 4;
            // 
            // memoEditDescription
            // 
            this.memoEditDescription.Location = new System.Drawing.Point(121, 73);
            this.memoEditDescription.Name = "memoEditDescription";
            this.memoEditDescription.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.memoEditDescription.Properties.Appearance.Options.UseFont = true;
            this.memoEditDescription.Size = new System.Drawing.Size(803, 65);
            this.memoEditDescription.StyleController = this.layoutControl;
            this.memoEditDescription.TabIndex = 5;
            // 
            // gridControlPermissions
            // 
            this.gridControlPermissions.Location = new System.Drawing.Point(24, 187);
            this.gridControlPermissions.MainView = this.gridViewPermissions;
            this.gridControlPermissions.Name = "gridControlPermissions";
            this.gridControlPermissions.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2,
            this.repositoryItemCheckEdit3,
            this.repositoryItemCheckEdit4,
            this.repositoryItemCheckEdit5,
            this.repositoryItemCheckEdit6,
            this.repositoryItemCheckEdit7,
            this.repositoryItemCheckEdit8});
            this.gridControlPermissions.Size = new System.Drawing.Size(976, 476);
            this.gridControlPermissions.TabIndex = 6;
            this.gridControlPermissions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPermissions});
            // 
            // gridViewPermissions
            // 
            this.gridViewPermissions.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewPermissions.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewPermissions.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.gridViewPermissions.Appearance.Row.Options.UseFont = true;
            this.gridViewPermissions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colModuleName,
            this.colCanView,
            this.colCanAdd,
            this.colCanEdit,
            this.colCanDelete,
            this.colCanPrint,
            this.colCanExport,
            this.colCanImport,
            this.colCanApprove});
            this.gridViewPermissions.GridControl = this.gridControlPermissions;
            this.gridViewPermissions.Name = "gridViewPermissions";
            this.gridViewPermissions.OptionsView.ShowGroupPanel = false;
            // 
            // colModuleName
            // 
            this.colModuleName.Caption = "وحدة النظام";
            this.colModuleName.FieldName = "ModuleName";
            this.colModuleName.Name = "colModuleName";
            this.colModuleName.OptionsColumn.AllowEdit = false;
            this.colModuleName.OptionsColumn.ReadOnly = true;
            this.colModuleName.Visible = true;
            this.colModuleName.VisibleIndex = 0;
            this.colModuleName.Width = 200;
            // 
            // colCanView
            // 
            this.colCanView.Caption = "عرض";
            this.colCanView.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colCanView.FieldName = "CanView";
            this.colCanView.Name = "colCanView";
            this.colCanView.Visible = true;
            this.colCanView.VisibleIndex = 1;
            this.colCanView.Width = 88;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ValueChecked = 1;
            this.repositoryItemCheckEdit1.ValueUnchecked = 0;
            // 
            // colCanAdd
            // 
            this.colCanAdd.Caption = "إضافة";
            this.colCanAdd.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colCanAdd.FieldName = "CanAdd";
            this.colCanAdd.Name = "colCanAdd";
            this.colCanAdd.Visible = true;
            this.colCanAdd.VisibleIndex = 2;
            this.colCanAdd.Width = 90;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.ValueChecked = 1;
            this.repositoryItemCheckEdit2.ValueUnchecked = 0;
            // 
            // colCanEdit
            // 
            this.colCanEdit.Caption = "تعديل";
            this.colCanEdit.ColumnEdit = this.repositoryItemCheckEdit3;
            this.colCanEdit.FieldName = "CanEdit";
            this.colCanEdit.Name = "colCanEdit";
            this.colCanEdit.Visible = true;
            this.colCanEdit.VisibleIndex = 3;
            this.colCanEdit.Width = 90;
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            this.repositoryItemCheckEdit3.ValueChecked = 1;
            this.repositoryItemCheckEdit3.ValueUnchecked = 0;
            // 
            // colCanDelete
            // 
            this.colCanDelete.Caption = "حذف";
            this.colCanDelete.ColumnEdit = this.repositoryItemCheckEdit4;
            this.colCanDelete.FieldName = "CanDelete";
            this.colCanDelete.Name = "colCanDelete";
            this.colCanDelete.Visible = true;
            this.colCanDelete.VisibleIndex = 4;
            this.colCanDelete.Width = 90;
            // 
            // repositoryItemCheckEdit4
            // 
            this.repositoryItemCheckEdit4.AutoHeight = false;
            this.repositoryItemCheckEdit4.Name = "repositoryItemCheckEdit4";
            this.repositoryItemCheckEdit4.ValueChecked = 1;
            this.repositoryItemCheckEdit4.ValueUnchecked = 0;
            // 
            // colCanPrint
            // 
            this.colCanPrint.Caption = "طباعة";
            this.colCanPrint.ColumnEdit = this.repositoryItemCheckEdit5;
            this.colCanPrint.FieldName = "CanPrint";
            this.colCanPrint.Name = "colCanPrint";
            this.colCanPrint.Visible = true;
            this.colCanPrint.VisibleIndex = 5;
            this.colCanPrint.Width = 90;
            // 
            // repositoryItemCheckEdit5
            // 
            this.repositoryItemCheckEdit5.AutoHeight = false;
            this.repositoryItemCheckEdit5.Name = "repositoryItemCheckEdit5";
            this.repositoryItemCheckEdit5.ValueChecked = 1;
            this.repositoryItemCheckEdit5.ValueUnchecked = 0;
            // 
            // colCanExport
            // 
            this.colCanExport.Caption = "تصدير";
            this.colCanExport.ColumnEdit = this.repositoryItemCheckEdit6;
            this.colCanExport.FieldName = "CanExport";
            this.colCanExport.Name = "colCanExport";
            this.colCanExport.Visible = true;
            this.colCanExport.VisibleIndex = 6;
            this.colCanExport.Width = 90;
            // 
            // repositoryItemCheckEdit6
            // 
            this.repositoryItemCheckEdit6.AutoHeight = false;
            this.repositoryItemCheckEdit6.Name = "repositoryItemCheckEdit6";
            this.repositoryItemCheckEdit6.ValueChecked = 1;
            this.repositoryItemCheckEdit6.ValueUnchecked = 0;
            // 
            // colCanImport
            // 
            this.colCanImport.Caption = "استيراد";
            this.colCanImport.ColumnEdit = this.repositoryItemCheckEdit7;
            this.colCanImport.FieldName = "CanImport";
            this.colCanImport.Name = "colCanImport";
            this.colCanImport.Visible = true;
            this.colCanImport.VisibleIndex = 7;
            this.colCanImport.Width = 90;
            // 
            // repositoryItemCheckEdit7
            // 
            this.repositoryItemCheckEdit7.AutoHeight = false;
            this.repositoryItemCheckEdit7.Name = "repositoryItemCheckEdit7";
            this.repositoryItemCheckEdit7.ValueChecked = 1;
            this.repositoryItemCheckEdit7.ValueUnchecked = 0;
            // 
            // colCanApprove
            // 
            this.colCanApprove.Caption = "اعتماد";
            this.colCanApprove.ColumnEdit = this.repositoryItemCheckEdit8;
            this.colCanApprove.FieldName = "CanApprove";
            this.colCanApprove.Name = "colCanApprove";
            this.colCanApprove.Visible = true;
            this.colCanApprove.VisibleIndex = 8;
            this.colCanApprove.Width = 90;
            // 
            // repositoryItemCheckEdit8
            // 
            this.repositoryItemCheckEdit8.AutoHeight = false;
            this.repositoryItemCheckEdit8.Name = "repositoryItemCheckEdit8";
            this.repositoryItemCheckEdit8.ValueChecked = 1;
            this.repositoryItemCheckEdit8.ValueUnchecked = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.buttonCancel);
            this.panelControl1.Controls.Add(this.buttonSave);
            this.panelControl1.Location = new System.Drawing.Point(12, 701);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1000, 55);
            this.panelControl1.TabIndex = 7;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonCancel.Appearance.Options.UseFont = true;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonCancel.ImageOptions.SvgImage")));
            this.buttonCancel.Location = new System.Drawing.Point(0, 0);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(120, 55);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "إلغاء";
            // 
            // buttonSave
            // 
            this.buttonSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonSave.Appearance.Options.UseFont = true;
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonSave.ImageOptions.SvgImage")));
            this.buttonSave.Location = new System.Drawing.Point(880, 0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(120, 55);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "حفظ";
            // 
            // buttonDeselectAll
            // 
            this.buttonDeselectAll.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonDeselectAll.Appearance.Options.UseFont = true;
            this.buttonDeselectAll.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonDeselectAll.ImageOptions.SvgImage")));
            this.buttonDeselectAll.Location = new System.Drawing.Point(514, 667);
            this.buttonDeselectAll.Name = "buttonDeselectAll";
            this.buttonDeselectAll.Size = new System.Drawing.Size(486, 30);
            this.buttonDeselectAll.StyleController = this.layoutControl;
            this.buttonDeselectAll.TabIndex = 9;
            this.buttonDeselectAll.Text = "إلغاء تحديد الكل";
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonSelectAll.Appearance.Options.UseFont = true;
            this.buttonSelectAll.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonSelectAll.ImageOptions.SvgImage")));
            this.buttonSelectAll.Location = new System.Drawing.Point(24, 667);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(486, 30);
            this.buttonSelectAll.StyleController = this.layoutControl;
            this.buttonSelectAll.TabIndex = 10;
            this.buttonSelectAll.Text = "تحديد الكل";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupRoleInfo,
            this.layoutControlGroupPermissions,
            this.layoutControlItem4});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1024, 768);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroupRoleInfo
            // 
            this.layoutControlGroupRoleInfo.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroupRoleInfo.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroupRoleInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroupRoleInfo.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupRoleInfo.Name = "layoutControlGroupRoleInfo";
            this.layoutControlGroupRoleInfo.Size = new System.Drawing.Size(1004, 142);
            this.layoutControlGroupRoleInfo.Text = "معلومات الدور";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.textEditName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(980, 28);
            this.layoutControlItem1.Text = "اسم الدور";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(85, 17);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.memoEditDescription;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(980, 69);
            this.layoutControlItem2.Text = "الوصف";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(85, 17);
            // 
            // layoutControlGroupPermissions
            // 
            this.layoutControlGroupPermissions.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroupPermissions.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroupPermissions.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.layoutControlItem5});
            this.layoutControlGroupPermissions.Location = new System.Drawing.Point(0, 142);
            this.layoutControlGroupPermissions.Name = "layoutControlGroupPermissions";
            this.layoutControlGroupPermissions.Size = new System.Drawing.Size(1004, 559);
            this.layoutControlGroupPermissions.Text = "الصلاحيات والأذونات";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridControlPermissions;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(980, 480);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.buttonSelectAll;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 480);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(490, 34);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.buttonDeselectAll;
            this.layoutControlItem5.Location = new System.Drawing.Point(490, 480);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(490, 34);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.buttonSave;
            this.layoutControlItem7.Location = new System.Drawing.Point(890, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(110, 55);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.panelControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 701);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(0, 59);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(5, 59);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1004, 59);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // RoleForm
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.layoutControl);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("RoleForm.IconOptions.SvgImage")));
            this.Name = "RoleForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إدارة الدور";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPermissions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPermissions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoleInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupPermissions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraEditors.MemoEdit memoEditDescription;
        private DevExpress.XtraGrid.GridControl gridControlPermissions;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPermissions;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoleInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupPermissions;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton buttonCancel;
        private DevExpress.XtraEditors.SimpleButton buttonSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton buttonDeselectAll;
        private DevExpress.XtraEditors.SimpleButton buttonSelectAll;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn colModuleName;
        private DevExpress.XtraGrid.Columns.GridColumn colCanView;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colCanAdd;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colCanEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn colCanDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit4;
        private DevExpress.XtraGrid.Columns.GridColumn colCanPrint;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit5;
        private DevExpress.XtraGrid.Columns.GridColumn colCanExport;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit6;
        private DevExpress.XtraGrid.Columns.GridColumn colCanImport;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit7;
        private DevExpress.XtraGrid.Columns.GridColumn colCanApprove;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit8;
    }
}