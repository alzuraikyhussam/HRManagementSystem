namespace HR.UI.Forms.Company
{
    partial class DepartmentsListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepartmentsListForm));
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageTree = new DevExpress.XtraTab.XtraTabPage();
            this.treeListDepartments = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnIsActive = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.xtraTabPageGrid = new DevExpress.XtraTab.XtraTabPage();
            this.gridControlDepartments = new DevExpress.XtraGrid.GridControl();
            this.gridViewDepartments = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnParentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCreatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnModifiedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnModifiedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.buttonDelete = new DevExpress.XtraEditors.SimpleButton();
            this.buttonEdit = new DevExpress.XtraEditors.SimpleButton();
            this.buttonAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.xtraTabPageTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListDepartments)).BeginInit();
            this.xtraTabPageGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDepartments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDepartments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl.Location = new System.Drawing.Point(0, 50);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.xtraTabPageTree;
            this.xtraTabControl.Size = new System.Drawing.Size(984, 511);
            this.xtraTabControl.TabIndex = 0;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageTree,
            this.xtraTabPageGrid});
            // 
            // xtraTabPageTree
            // 
            this.xtraTabPageTree.Controls.Add(this.treeListDepartments);
            this.xtraTabPageTree.Name = "xtraTabPageTree";
            this.xtraTabPageTree.Size = new System.Drawing.Size(978, 483);
            this.xtraTabPageTree.Text = "الهيكل التنظيمي";
            // 
            // treeListDepartments
            // 
            this.treeListDepartments.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.treeListDepartments.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeListDepartments.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.treeListDepartments.Appearance.FocusedRow.Options.UseBackColor = true;
            this.treeListDepartments.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.treeListDepartments.Appearance.SelectedRow.Options.UseBackColor = true;
            this.treeListDepartments.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnID,
            this.treeListColumnName,
            this.treeListColumnCode,
            this.treeListColumnIsActive});
            this.treeListDepartments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListDepartments.Location = new System.Drawing.Point(0, 0);
            this.treeListDepartments.Name = "treeListDepartments";
            this.treeListDepartments.OptionsBehavior.Editable = false;
            this.treeListDepartments.OptionsBehavior.ReadOnly = true;
            this.treeListDepartments.OptionsView.ShowHorzLines = false;
            this.treeListDepartments.OptionsView.ShowIndicator = false;
            this.treeListDepartments.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.treeListDepartments.Size = new System.Drawing.Size(978, 483);
            this.treeListDepartments.TabIndex = 0;
            // 
            // treeListColumnID
            // 
            this.treeListColumnID.Caption = "المعرف";
            this.treeListColumnID.FieldName = "ID";
            this.treeListColumnID.Name = "treeListColumnID";
            this.treeListColumnID.OptionsColumn.AllowEdit = false;
            this.treeListColumnID.OptionsColumn.ReadOnly = true;
            this.treeListColumnID.Visible = true;
            this.treeListColumnID.VisibleIndex = 0;
            this.treeListColumnID.Width = 80;
            // 
            // treeListColumnName
            // 
            this.treeListColumnName.Caption = "اسم القسم";
            this.treeListColumnName.FieldName = "Name";
            this.treeListColumnName.Name = "treeListColumnName";
            this.treeListColumnName.OptionsColumn.AllowEdit = false;
            this.treeListColumnName.OptionsColumn.ReadOnly = true;
            this.treeListColumnName.Visible = true;
            this.treeListColumnName.VisibleIndex = 1;
            this.treeListColumnName.Width = 250;
            // 
            // treeListColumnCode
            // 
            this.treeListColumnCode.Caption = "الرمز";
            this.treeListColumnCode.FieldName = "Code";
            this.treeListColumnCode.Name = "treeListColumnCode";
            this.treeListColumnCode.OptionsColumn.AllowEdit = false;
            this.treeListColumnCode.OptionsColumn.ReadOnly = true;
            this.treeListColumnCode.Visible = true;
            this.treeListColumnCode.VisibleIndex = 2;
            this.treeListColumnCode.Width = 100;
            // 
            // treeListColumnIsActive
            // 
            this.treeListColumnIsActive.Caption = "نشط";
            this.treeListColumnIsActive.FieldName = "IsActive";
            this.treeListColumnIsActive.Name = "treeListColumnIsActive";
            this.treeListColumnIsActive.OptionsColumn.AllowEdit = false;
            this.treeListColumnIsActive.OptionsColumn.ReadOnly = true;
            this.treeListColumnIsActive.Visible = true;
            this.treeListColumnIsActive.VisibleIndex = 3;
            this.treeListColumnIsActive.Width = 80;
            // 
            // xtraTabPageGrid
            // 
            this.xtraTabPageGrid.Controls.Add(this.gridControlDepartments);
            this.xtraTabPageGrid.Name = "xtraTabPageGrid";
            this.xtraTabPageGrid.Size = new System.Drawing.Size(978, 483);
            this.xtraTabPageGrid.Text = "قائمة الأقسام";
            // 
            // gridControlDepartments
            // 
            this.gridControlDepartments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDepartments.Location = new System.Drawing.Point(0, 0);
            this.gridControlDepartments.MainView = this.gridViewDepartments;
            this.gridControlDepartments.Name = "gridControlDepartments";
            this.gridControlDepartments.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gridControlDepartments.Size = new System.Drawing.Size(978, 483);
            this.gridControlDepartments.TabIndex = 0;
            this.gridControlDepartments.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDepartments});
            // 
            // gridViewDepartments
            // 
            this.gridViewDepartments.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnID,
            this.gridColumnParentID,
            this.gridColumnName,
            this.gridColumnCode,
            this.gridColumnDescription,
            this.gridColumnIsActive,
            this.gridColumnCreatedBy,
            this.gridColumnCreatedDate,
            this.gridColumnModifiedBy,
            this.gridColumnModifiedDate});
            this.gridViewDepartments.GridControl = this.gridControlDepartments;
            this.gridViewDepartments.Name = "gridViewDepartments";
            this.gridViewDepartments.OptionsBehavior.Editable = false;
            this.gridViewDepartments.OptionsBehavior.ReadOnly = true;
            this.gridViewDepartments.OptionsFind.AlwaysVisible = true;
            this.gridViewDepartments.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnID
            // 
            this.gridColumnID.Caption = "المعرف";
            this.gridColumnID.FieldName = "ID";
            this.gridColumnID.Name = "gridColumnID";
            this.gridColumnID.OptionsColumn.AllowEdit = false;
            this.gridColumnID.OptionsColumn.ReadOnly = true;
            this.gridColumnID.Visible = true;
            this.gridColumnID.VisibleIndex = 0;
            this.gridColumnID.Width = 70;
            // 
            // gridColumnParentID
            // 
            this.gridColumnParentID.Caption = "القسم الأب";
            this.gridColumnParentID.FieldName = "ParentName";
            this.gridColumnParentID.Name = "gridColumnParentID";
            this.gridColumnParentID.OptionsColumn.AllowEdit = false;
            this.gridColumnParentID.OptionsColumn.ReadOnly = true;
            this.gridColumnParentID.Visible = true;
            this.gridColumnParentID.VisibleIndex = 1;
            this.gridColumnParentID.Width = 120;
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "اسم القسم";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.OptionsColumn.AllowEdit = false;
            this.gridColumnName.OptionsColumn.ReadOnly = true;
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 2;
            this.gridColumnName.Width = 180;
            // 
            // gridColumnCode
            // 
            this.gridColumnCode.Caption = "الرمز";
            this.gridColumnCode.FieldName = "Code";
            this.gridColumnCode.Name = "gridColumnCode";
            this.gridColumnCode.OptionsColumn.AllowEdit = false;
            this.gridColumnCode.OptionsColumn.ReadOnly = true;
            this.gridColumnCode.Visible = true;
            this.gridColumnCode.VisibleIndex = 3;
            this.gridColumnCode.Width = 80;
            // 
            // gridColumnDescription
            // 
            this.gridColumnDescription.Caption = "الوصف";
            this.gridColumnDescription.FieldName = "Description";
            this.gridColumnDescription.Name = "gridColumnDescription";
            this.gridColumnDescription.OptionsColumn.AllowEdit = false;
            this.gridColumnDescription.OptionsColumn.ReadOnly = true;
            this.gridColumnDescription.Visible = true;
            this.gridColumnDescription.VisibleIndex = 4;
            this.gridColumnDescription.Width = 200;
            // 
            // gridColumnIsActive
            // 
            this.gridColumnIsActive.Caption = "نشط";
            this.gridColumnIsActive.FieldName = "IsActive";
            this.gridColumnIsActive.Name = "gridColumnIsActive";
            this.gridColumnIsActive.OptionsColumn.AllowEdit = false;
            this.gridColumnIsActive.OptionsColumn.ReadOnly = true;
            this.gridColumnIsActive.Visible = true;
            this.gridColumnIsActive.VisibleIndex = 5;
            this.gridColumnIsActive.Width = 50;
            // 
            // gridColumnCreatedBy
            // 
            this.gridColumnCreatedBy.Caption = "المنشئ";
            this.gridColumnCreatedBy.FieldName = "CreatedByName";
            this.gridColumnCreatedBy.Name = "gridColumnCreatedBy";
            this.gridColumnCreatedBy.OptionsColumn.AllowEdit = false;
            this.gridColumnCreatedBy.OptionsColumn.ReadOnly = true;
            this.gridColumnCreatedBy.Visible = true;
            this.gridColumnCreatedBy.VisibleIndex = 6;
            this.gridColumnCreatedBy.Width = 100;
            // 
            // gridColumnCreatedDate
            // 
            this.gridColumnCreatedDate.Caption = "تاريخ الإنشاء";
            this.gridColumnCreatedDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.gridColumnCreatedDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnCreatedDate.FieldName = "CreatedDate";
            this.gridColumnCreatedDate.Name = "gridColumnCreatedDate";
            this.gridColumnCreatedDate.OptionsColumn.AllowEdit = false;
            this.gridColumnCreatedDate.OptionsColumn.ReadOnly = true;
            this.gridColumnCreatedDate.Visible = true;
            this.gridColumnCreatedDate.VisibleIndex = 7;
            this.gridColumnCreatedDate.Width = 120;
            // 
            // gridColumnModifiedBy
            // 
            this.gridColumnModifiedBy.Caption = "المعدل";
            this.gridColumnModifiedBy.FieldName = "ModifiedByName";
            this.gridColumnModifiedBy.Name = "gridColumnModifiedBy";
            this.gridColumnModifiedBy.OptionsColumn.AllowEdit = false;
            this.gridColumnModifiedBy.OptionsColumn.ReadOnly = true;
            this.gridColumnModifiedBy.Visible = true;
            this.gridColumnModifiedBy.VisibleIndex = 8;
            this.gridColumnModifiedBy.Width = 100;
            // 
            // gridColumnModifiedDate
            // 
            this.gridColumnModifiedDate.Caption = "تاريخ التعديل";
            this.gridColumnModifiedDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.gridColumnModifiedDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnModifiedDate.FieldName = "ModifiedDate";
            this.gridColumnModifiedDate.Name = "gridColumnModifiedDate";
            this.gridColumnModifiedDate.OptionsColumn.AllowEdit = false;
            this.gridColumnModifiedDate.OptionsColumn.ReadOnly = true;
            this.gridColumnModifiedDate.Visible = true;
            this.gridColumnModifiedDate.VisibleIndex = 9;
            this.gridColumnModifiedDate.Width = 120;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.buttonRefresh);
            this.panelControl1.Controls.Add(this.buttonDelete);
            this.panelControl1.Controls.Add(this.buttonEdit);
            this.panelControl1.Controls.Add(this.buttonAdd);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(984, 50);
            this.panelControl1.TabIndex = 1;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.Appearance.Options.UseTextOptions = true;
            this.buttonRefresh.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.buttonRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.ImageOptions.Image")));
            this.buttonRefresh.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.buttonRefresh.Location = new System.Drawing.Point(581, 5);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(120, 40);
            this.buttonRefresh.TabIndex = 3;
            this.buttonRefresh.Text = "تحديث";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Appearance.Options.UseTextOptions = true;
            this.buttonDelete.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.buttonDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.ImageOptions.Image")));
            this.buttonDelete.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.buttonDelete.Location = new System.Drawing.Point(707, 5);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(120, 40);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "حذف";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Appearance.Options.UseTextOptions = true;
            this.buttonEdit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.buttonEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonEdit.ImageOptions.Image")));
            this.buttonEdit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.buttonEdit.Location = new System.Drawing.Point(833, 5);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(120, 40);
            this.buttonEdit.TabIndex = 1;
            this.buttonEdit.Text = "تعديل";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Appearance.Options.UseTextOptions = true;
            this.buttonAdd.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.buttonAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.ImageOptions.Image")));
            this.buttonAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.buttonAdd.Location = new System.Drawing.Point(959, 5);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(20, 40);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "إضافة";
            // 
            // DepartmentsListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.xtraTabControl);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.Image = global::HR.Properties.Resources.logo;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "DepartmentsListForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "الهيكل التنظيمي - الأقسام";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.xtraTabPageTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListDepartments)).EndInit();
            this.xtraTabPageGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDepartments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDepartments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageTree;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageGrid;
        private DevExpress.XtraTreeList.TreeList treeListDepartments;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnIsActive;
        private DevExpress.XtraGrid.GridControl gridControlDepartments;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDepartments;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnParentID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDescription;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIsActive;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCreatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCreatedDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnModifiedBy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnModifiedDate;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton buttonRefresh;
        private DevExpress.XtraEditors.SimpleButton buttonDelete;
        private DevExpress.XtraEditors.SimpleButton buttonEdit;
        private DevExpress.XtraEditors.SimpleButton buttonAdd;
    }
}