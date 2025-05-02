namespace HR.UI.Forms.Employees
{
    partial class EmployeesListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeesListForm));
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.labelItemCount = new DevExpress.XtraEditors.LabelControl();
            this.gridControlEmployees = new DevExpress.XtraGrid.GridControl();
            this.gridViewEmployees = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPositionTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGender = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMobile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHireDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDirectManagerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonPrint = new DevExpress.XtraEditors.SimpleButton();
            this.buttonExport = new DevExpress.XtraEditors.SimpleButton();
            this.buttonViewDetails = new DevExpress.XtraEditors.SimpleButton();
            this.buttonRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.buttonDelete = new DevExpress.XtraEditors.SimpleButton();
            this.buttonEdit = new DevExpress.XtraEditors.SimpleButton();
            this.buttonAdd = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxEditDepartment = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditPosition = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupEmployees = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupFilters = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFilters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.labelItemCount);
            this.layoutControl.Controls.Add(this.gridControlEmployees);
            this.layoutControl.Controls.Add(this.panelControl1);
            this.layoutControl.Controls.Add(this.comboBoxEditDepartment);
            this.layoutControl.Controls.Add(this.comboBoxEditPosition);
            this.layoutControl.Controls.Add(this.comboBoxEditStatus);
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
            // labelItemCount
            // 
            this.labelItemCount.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelItemCount.Appearance.Options.UseFont = true;
            this.labelItemCount.Location = new System.Drawing.Point(12, 599);
            this.labelItemCount.Name = "labelItemCount";
            this.labelItemCount.Size = new System.Drawing.Size(110, 17);
            this.labelItemCount.StyleController = this.layoutControl;
            this.labelItemCount.TabIndex = 10;
            this.labelItemCount.Text = "إجمالي عدد الموظفين: 0";
            // 
            // gridControlEmployees
            // 
            this.gridControlEmployees.Location = new System.Drawing.Point(24, 125);
            this.gridControlEmployees.MainView = this.gridViewEmployees;
            this.gridControlEmployees.Name = "gridControlEmployees";
            this.gridControlEmployees.Size = new System.Drawing.Size(1236, 467);
            this.gridControlEmployees.TabIndex = 4;
            this.gridControlEmployees.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewEmployees});
            // 
            // gridViewEmployees
            // 
            this.gridViewEmployees.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewEmployees.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewEmployees.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.gridViewEmployees.Appearance.Row.Options.UseFont = true;
            this.gridViewEmployees.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colEmployeeNumber,
            this.colFullName,
            this.colDepartmentName,
            this.colPositionTitle,
            this.colGender,
            this.colMobile,
            this.colEmail,
            this.colHireDate,
            this.colStatus,
            this.colDirectManagerName});
            this.gridViewEmployees.GridControl = this.gridControlEmployees;
            this.gridViewEmployees.Name = "gridViewEmployees";
            this.gridViewEmployees.OptionsFind.AlwaysVisible = true;
            this.gridViewEmployees.OptionsFind.FindNullPrompt = "بحث...";
            this.gridViewEmployees.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewEmployees.OptionsView.ShowAutoFilterRow = true;
            this.gridViewEmployees.OptionsView.ShowGroupPanel = false;
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
            this.colID.Width = 50;
            // 
            // colEmployeeNumber
            // 
            this.colEmployeeNumber.Caption = "الرقم الوظيفي";
            this.colEmployeeNumber.FieldName = "EmployeeNumber";
            this.colEmployeeNumber.Name = "colEmployeeNumber";
            this.colEmployeeNumber.OptionsColumn.AllowEdit = false;
            this.colEmployeeNumber.OptionsColumn.AllowFocus = false;
            this.colEmployeeNumber.Visible = true;
            this.colEmployeeNumber.VisibleIndex = 1;
            this.colEmployeeNumber.Width = 100;
            // 
            // colFullName
            // 
            this.colFullName.Caption = "اسم الموظف";
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsColumn.AllowEdit = false;
            this.colFullName.OptionsColumn.AllowFocus = false;
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 2;
            this.colFullName.Width = 200;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "القسم";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.OptionsColumn.AllowEdit = false;
            this.colDepartmentName.OptionsColumn.AllowFocus = false;
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 3;
            this.colDepartmentName.Width = 120;
            // 
            // colPositionTitle
            // 
            this.colPositionTitle.Caption = "المسمى الوظيفي";
            this.colPositionTitle.FieldName = "PositionTitle";
            this.colPositionTitle.Name = "colPositionTitle";
            this.colPositionTitle.OptionsColumn.AllowEdit = false;
            this.colPositionTitle.OptionsColumn.AllowFocus = false;
            this.colPositionTitle.Visible = true;
            this.colPositionTitle.VisibleIndex = 4;
            this.colPositionTitle.Width = 120;
            // 
            // colGender
            // 
            this.colGender.Caption = "الجنس";
            this.colGender.FieldName = "Gender";
            this.colGender.Name = "colGender";
            this.colGender.OptionsColumn.AllowEdit = false;
            this.colGender.OptionsColumn.AllowFocus = false;
            this.colGender.Visible = true;
            this.colGender.VisibleIndex = 5;
            this.colGender.Width = 60;
            // 
            // colMobile
            // 
            this.colMobile.Caption = "رقم الجوال";
            this.colMobile.FieldName = "Mobile";
            this.colMobile.Name = "colMobile";
            this.colMobile.OptionsColumn.AllowEdit = false;
            this.colMobile.OptionsColumn.AllowFocus = false;
            this.colMobile.Visible = true;
            this.colMobile.VisibleIndex = 6;
            this.colMobile.Width = 100;
            // 
            // colEmail
            // 
            this.colEmail.Caption = "البريد الإلكتروني";
            this.colEmail.FieldName = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.OptionsColumn.AllowEdit = false;
            this.colEmail.OptionsColumn.AllowFocus = false;
            this.colEmail.Visible = true;
            this.colEmail.VisibleIndex = 7;
            this.colEmail.Width = 150;
            // 
            // colHireDate
            // 
            this.colHireDate.Caption = "تاريخ التوظيف";
            this.colHireDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colHireDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colHireDate.FieldName = "HireDate";
            this.colHireDate.Name = "colHireDate";
            this.colHireDate.OptionsColumn.AllowEdit = false;
            this.colHireDate.OptionsColumn.AllowFocus = false;
            this.colHireDate.Visible = true;
            this.colHireDate.VisibleIndex = 8;
            this.colHireDate.Width = 100;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "الحالة";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsColumn.AllowFocus = false;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 9;
            this.colStatus.Width = 80;
            // 
            // colDirectManagerName
            // 
            this.colDirectManagerName.Caption = "المدير المباشر";
            this.colDirectManagerName.FieldName = "DirectManagerName";
            this.colDirectManagerName.Name = "colDirectManagerName";
            this.colDirectManagerName.OptionsColumn.AllowEdit = false;
            this.colDirectManagerName.OptionsColumn.AllowFocus = false;
            this.colDirectManagerName.Visible = true;
            this.colDirectManagerName.VisibleIndex = 10;
            this.colDirectManagerName.Width = 150;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.buttonPrint);
            this.panelControl1.Controls.Add(this.buttonExport);
            this.panelControl1.Controls.Add(this.buttonViewDetails);
            this.panelControl1.Controls.Add(this.buttonRefresh);
            this.panelControl1.Controls.Add(this.buttonDelete);
            this.panelControl1.Controls.Add(this.buttonEdit);
            this.panelControl1.Controls.Add(this.buttonAdd);
            this.panelControl1.Location = new System.Drawing.Point(24, 620);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1236, 50);
            this.panelControl1.TabIndex = 5;
            // 
            // buttonPrint
            // 
            this.buttonPrint.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonPrint.Appearance.Options.UseFont = true;
            this.buttonPrint.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonPrint.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonPrint.ImageOptions.SvgImage")));
            this.buttonPrint.Location = new System.Drawing.Point(764, 2);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(110, 46);
            this.buttonPrint.TabIndex = 6;
            this.buttonPrint.Text = "طباعة";
            // 
            // buttonExport
            // 
            this.buttonExport.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonExport.Appearance.Options.UseFont = true;
            this.buttonExport.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonExport.ImageOptions.SvgImage")));
            this.buttonExport.Location = new System.Drawing.Point(874, 2);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(110, 46);
            this.buttonExport.TabIndex = 5;
            this.buttonExport.Text = "تصدير";
            // 
            // buttonViewDetails
            // 
            this.buttonViewDetails.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonViewDetails.Appearance.Options.UseFont = true;
            this.buttonViewDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonViewDetails.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonViewDetails.ImageOptions.SvgImage")));
            this.buttonViewDetails.Location = new System.Drawing.Point(984, 2);
            this.buttonViewDetails.Name = "buttonViewDetails";
            this.buttonViewDetails.Size = new System.Drawing.Size(110, 46);
            this.buttonViewDetails.TabIndex = 4;
            this.buttonViewDetails.Text = "التفاصيل";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonRefresh.Appearance.Options.UseFont = true;
            this.buttonRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonRefresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonRefresh.ImageOptions.SvgImage")));
            this.buttonRefresh.Location = new System.Drawing.Point(1094, 2);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(140, 46);
            this.buttonRefresh.TabIndex = 3;
            this.buttonRefresh.Text = "تحديث";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonDelete.Appearance.Options.UseFont = true;
            this.buttonDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonDelete.ImageOptions.SvgImage")));
            this.buttonDelete.Location = new System.Drawing.Point(252, 2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(125, 46);
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
            this.buttonEdit.Size = new System.Drawing.Size(125, 46);
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
            // comboBoxEditDepartment
            // 
            this.comboBoxEditDepartment.Location = new System.Drawing.Point(886, 76);
            this.comboBoxEditDepartment.Name = "comboBoxEditDepartment";
            this.comboBoxEditDepartment.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.comboBoxEditDepartment.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditDepartment.Size = new System.Drawing.Size(292, 24);
            this.comboBoxEditDepartment.StyleController = this.layoutControl;
            this.comboBoxEditDepartment.TabIndex = 6;
            // 
            // comboBoxEditPosition
            // 
            this.comboBoxEditPosition.Location = new System.Drawing.Point(513, 76);
            this.comboBoxEditPosition.Name = "comboBoxEditPosition";
            this.comboBoxEditPosition.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.comboBoxEditPosition.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditPosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditPosition.Size = new System.Drawing.Size(292, 24);
            this.comboBoxEditPosition.StyleController = this.layoutControl;
            this.comboBoxEditPosition.TabIndex = 7;
            // 
            // comboBoxEditStatus
            // 
            this.comboBoxEditStatus.Location = new System.Drawing.Point(82, 76);
            this.comboBoxEditStatus.Name = "comboBoxEditStatus";
            this.comboBoxEditStatus.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.comboBoxEditStatus.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditStatus.Size = new System.Drawing.Size(350, 24);
            this.comboBoxEditStatus.StyleController = this.layoutControl;
            this.comboBoxEditStatus.TabIndex = 8;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupEmployees,
            this.layoutControlItem6,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1284, 661);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroupEmployees
            // 
            this.layoutControlGroupEmployees.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroupEmployees.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroupEmployees.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlGroupFilters});
            this.layoutControlGroupEmployees.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupEmployees.Name = "layoutControlGroupEmployees";
            this.layoutControlGroupEmployees.Size = new System.Drawing.Size(1264, 587);
            this.layoutControlGroupEmployees.Text = "قائمة الموظفين";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlEmployees;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1240, 471);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 551);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 54);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(104, 54);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1240, 54);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroupFilters
            // 
            this.layoutControlGroupFilters.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroupFilters.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroupFilters.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroupFilters.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupFilters.Name = "layoutControlGroupFilters";
            this.layoutControlGroupFilters.Size = new System.Drawing.Size(1240, 80);
            this.layoutControlGroupFilters.Text = "خيارات التصفية";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.comboBoxEditDepartment;
            this.layoutControlItem3.Location = new System.Drawing.Point(784, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(398, 28);
            this.layoutControlItem3.Text = "القسم";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(90, 17);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.comboBoxEditPosition;
            this.layoutControlItem4.Location = new System.Drawing.Point(411, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(373, 28);
            this.layoutControlItem4.Text = "المسمى الوظيفي";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(90, 17);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.Control = this.comboBoxEditStatus;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(411, 28);
            this.layoutControlItem5.Text = "الحالة";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(90, 17);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.labelItemCount;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 587);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(114, 21);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(114, 587);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1150, 21);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // EmployeesListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 661);
            this.Controls.Add(this.layoutControl);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("EmployeesListForm.IconOptions.SvgImage")));
            this.Name = "EmployeesListForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إدارة الموظفين";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFilters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControlEmployees;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewEmployees;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupEmployees;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton buttonRefresh;
        private DevExpress.XtraEditors.SimpleButton buttonDelete;
        private DevExpress.XtraEditors.SimpleButton buttonEdit;
        private DevExpress.XtraEditors.SimpleButton buttonAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colPositionTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colGender;
        private DevExpress.XtraGrid.Columns.GridColumn colMobile;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colHireDate;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditDepartment;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditPosition;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditStatus;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupFilters;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton buttonPrint;
        private DevExpress.XtraEditors.SimpleButton buttonExport;
        private DevExpress.XtraEditors.SimpleButton buttonViewDetails;
        private DevExpress.XtraEditors.LabelControl labelItemCount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colDirectManagerName;
    }
}