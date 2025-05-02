namespace HR.UI.Forms.Company
{
    partial class PositionsListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PositionsListForm));
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.lookUpEditFilterDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.checkEditShowInactivePositions = new DevExpress.XtraEditors.CheckEdit();
            this.gridControlPositions = new DevExpress.XtraGrid.GridControl();
            this.gridViewPositions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGradeLevel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMinSalary = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxSalary = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsManagerPosition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.buttonDelete = new DevExpress.XtraEditors.SimpleButton();
            this.buttonEdit = new DevExpress.XtraEditors.SimpleButton();
            this.buttonAdd = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupFilter = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupPositions = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditFilterDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditShowInactivePositions.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPositions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPositions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupPositions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.lookUpEditFilterDepartment);
            this.layoutControl.Controls.Add(this.checkEditShowInactivePositions);
            this.layoutControl.Controls.Add(this.gridControlPositions);
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
            // lookUpEditFilterDepartment
            // 
            this.lookUpEditFilterDepartment.Location = new System.Drawing.Point(126, 45);
            this.lookUpEditFilterDepartment.Name = "lookUpEditFilterDepartment";
            this.lookUpEditFilterDepartment.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lookUpEditFilterDepartment.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditFilterDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditFilterDepartment.Properties.NullText = "";
            this.lookUpEditFilterDepartment.Size = new System.Drawing.Size(373, 24);
            this.lookUpEditFilterDepartment.StyleController = this.layoutControl;
            this.lookUpEditFilterDepartment.TabIndex = 4;
            // 
            // checkEditShowInactivePositions
            // 
            this.checkEditShowInactivePositions.Location = new System.Drawing.Point(24, 73);
            this.checkEditShowInactivePositions.Name = "checkEditShowInactivePositions";
            this.checkEditShowInactivePositions.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.checkEditShowInactivePositions.Properties.Appearance.Options.UseFont = true;
            this.checkEditShowInactivePositions.Properties.Caption = "عرض المسميات الوظيفية غير النشطة";
            this.checkEditShowInactivePositions.Size = new System.Drawing.Size(936, 21);
            this.checkEditShowInactivePositions.StyleController = this.layoutControl;
            this.checkEditShowInactivePositions.TabIndex = 5;
            // 
            // gridControlPositions
            // 
            this.gridControlPositions.Location = new System.Drawing.Point(24, 143);
            this.gridControlPositions.MainView = this.gridViewPositions;
            this.gridControlPositions.Name = "gridControlPositions";
            this.gridControlPositions.Size = new System.Drawing.Size(936, 340);
            this.gridControlPositions.TabIndex = 6;
            this.gridControlPositions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPositions});
            // 
            // gridViewPositions
            // 
            this.gridViewPositions.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewPositions.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewPositions.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.gridViewPositions.Appearance.Row.Options.UseFont = true;
            this.gridViewPositions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colTitle,
            this.colDepartmentName,
            this.colGradeLevel,
            this.colMinSalary,
            this.colMaxSalary,
            this.colIsManagerPosition,
            this.colIsActive});
            this.gridViewPositions.GridControl = this.gridControlPositions;
            this.gridViewPositions.Name = "gridViewPositions";
            this.gridViewPositions.OptionsFind.AlwaysVisible = true;
            this.gridViewPositions.OptionsFind.FindNullPrompt = "بحث...";
            this.gridViewPositions.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewPositions.OptionsView.ShowAutoFilterRow = true;
            this.gridViewPositions.OptionsView.ShowGroupPanel = false;
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
            // colTitle
            // 
            this.colTitle.Caption = "المسمى الوظيفي";
            this.colTitle.FieldName = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.OptionsColumn.AllowEdit = false;
            this.colTitle.OptionsColumn.AllowFocus = false;
            this.colTitle.Visible = true;
            this.colTitle.VisibleIndex = 1;
            this.colTitle.Width = 200;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "القسم";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.OptionsColumn.AllowEdit = false;
            this.colDepartmentName.OptionsColumn.AllowFocus = false;
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 2;
            this.colDepartmentName.Width = 150;
            // 
            // colGradeLevel
            // 
            this.colGradeLevel.Caption = "المستوى";
            this.colGradeLevel.FieldName = "GradeLevel";
            this.colGradeLevel.Name = "colGradeLevel";
            this.colGradeLevel.OptionsColumn.AllowEdit = false;
            this.colGradeLevel.OptionsColumn.AllowFocus = false;
            this.colGradeLevel.Visible = true;
            this.colGradeLevel.VisibleIndex = 3;
            this.colGradeLevel.Width = 60;
            // 
            // colMinSalary
            // 
            this.colMinSalary.Caption = "الحد الأدنى للراتب";
            this.colMinSalary.DisplayFormat.FormatString = "n2";
            this.colMinSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMinSalary.FieldName = "MinSalary";
            this.colMinSalary.Name = "colMinSalary";
            this.colMinSalary.OptionsColumn.AllowEdit = false;
            this.colMinSalary.OptionsColumn.AllowFocus = false;
            this.colMinSalary.Visible = true;
            this.colMinSalary.VisibleIndex = 4;
            this.colMinSalary.Width = 100;
            // 
            // colMaxSalary
            // 
            this.colMaxSalary.Caption = "الحد الأقصى للراتب";
            this.colMaxSalary.DisplayFormat.FormatString = "n2";
            this.colMaxSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMaxSalary.FieldName = "MaxSalary";
            this.colMaxSalary.Name = "colMaxSalary";
            this.colMaxSalary.OptionsColumn.AllowEdit = false;
            this.colMaxSalary.OptionsColumn.AllowFocus = false;
            this.colMaxSalary.Visible = true;
            this.colMaxSalary.VisibleIndex = 5;
            this.colMaxSalary.Width = 100;
            // 
            // colIsManagerPosition
            // 
            this.colIsManagerPosition.Caption = "منصب إداري";
            this.colIsManagerPosition.FieldName = "IsManagerPosition";
            this.colIsManagerPosition.Name = "colIsManagerPosition";
            this.colIsManagerPosition.OptionsColumn.AllowEdit = false;
            this.colIsManagerPosition.OptionsColumn.AllowFocus = false;
            this.colIsManagerPosition.Visible = true;
            this.colIsManagerPosition.VisibleIndex = 6;
            this.colIsManagerPosition.Width = 80;
            // 
            // colIsActive
            // 
            this.colIsActive.Caption = "نشط";
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.OptionsColumn.AllowEdit = false;
            this.colIsActive.OptionsColumn.AllowFocus = false;
            this.colIsActive.Visible = true;
            this.colIsActive.VisibleIndex = 7;
            this.colIsActive.Width = 60;
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
            this.panelControl1.TabIndex = 7;
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
            this.layoutControlGroupFilter,
            this.layoutControlGroupPositions});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(984, 561);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroupFilter
            // 
            this.layoutControlGroupFilter.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroupFilter.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroupFilter.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroupFilter.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupFilter.Name = "layoutControlGroupFilter";
            this.layoutControlGroupFilter.Size = new System.Drawing.Size(964, 98);
            this.layoutControlGroupFilter.Text = "خيارات البحث والترشيح";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.lookUpEditFilterDepartment;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(940, 28);
            this.layoutControlItem1.Text = "القسم";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(99, 17);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.checkEditShowInactivePositions;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(940, 25);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroupPositions
            // 
            this.layoutControlGroupPositions.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroupPositions.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroupPositions.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroupPositions.Location = new System.Drawing.Point(0, 98);
            this.layoutControlGroupPositions.Name = "layoutControlGroupPositions";
            this.layoutControlGroupPositions.Size = new System.Drawing.Size(964, 443);
            this.layoutControlGroupPositions.Text = "قائمة المسميات الوظيفية";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridControlPositions;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(940, 344);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.panelControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 344);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(0, 54);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(104, 54);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(940, 54);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // PositionsListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.layoutControl);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("PositionsListForm.IconOptions.SvgImage")));
            this.Name = "PositionsListForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إدارة المسميات الوظيفية";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditFilterDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditShowInactivePositions.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPositions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPositions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupPositions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditFilterDepartment;
        private DevExpress.XtraEditors.CheckEdit checkEditShowInactivePositions;
        private DevExpress.XtraGrid.GridControl gridControlPositions;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPositions;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupPositions;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton buttonRefresh;
        private DevExpress.XtraEditors.SimpleButton buttonDelete;
        private DevExpress.XtraEditors.SimpleButton buttonEdit;
        private DevExpress.XtraEditors.SimpleButton buttonAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colGradeLevel;
        private DevExpress.XtraGrid.Columns.GridColumn colMinSalary;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxSalary;
        private DevExpress.XtraGrid.Columns.GridColumn colIsManagerPosition;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
    }
}