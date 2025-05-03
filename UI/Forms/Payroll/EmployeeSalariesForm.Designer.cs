namespace HR.UI.Forms.Payroll
{
    partial class EmployeeSalariesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeSalariesForm));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControlSalaries = new DevExpress.XtraGrid.GridControl();
            this.employeeSalaryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewSalaries = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEffectiveDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControlDetails = new DevExpress.XtraEditors.GroupControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControlTotalAmount = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonDeleteComponent = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonAddComponent = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlComponents = new DevExpress.XtraGrid.GridControl();
            this.gridViewComponents = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colComponentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComponentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.dateEditEffectiveDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.searchLookUpEditEmployee = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSalaries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeSalaryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSalaries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetails)).BeginInit();
            this.groupControlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlComponents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewComponents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEffectiveDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEffectiveDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barButtonItemAdd,
            this.barButtonItemEdit,
            this.barButtonItemDelete,
            this.barButtonItemRefresh});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 5;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1024, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barButtonItemAdd
            // 
            this.barButtonItemAdd.Caption = "إضافة";
            this.barButtonItemAdd.Id = 1;
            this.barButtonItemAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemAdd.ImageOptions.Image")));
            this.barButtonItemAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemAdd.ImageOptions.LargeImage")));
            this.barButtonItemAdd.Name = "barButtonItemAdd";
            this.barButtonItemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAdd_ItemClick);
            // 
            // barButtonItemEdit
            // 
            this.barButtonItemEdit.Caption = "تعديل";
            this.barButtonItemEdit.Id = 2;
            this.barButtonItemEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemEdit.ImageOptions.Image")));
            this.barButtonItemEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemEdit.ImageOptions.LargeImage")));
            this.barButtonItemEdit.Name = "barButtonItemEdit";
            this.barButtonItemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemEdit_ItemClick);
            // 
            // barButtonItemDelete
            // 
            this.barButtonItemDelete.Caption = "حذف";
            this.barButtonItemDelete.Id = 3;
            this.barButtonItemDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemDelete.ImageOptions.Image")));
            this.barButtonItemDelete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemDelete.ImageOptions.LargeImage")));
            this.barButtonItemDelete.Name = "barButtonItemDelete";
            this.barButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDelete_ItemClick);
            // 
            // barButtonItemRefresh
            // 
            this.barButtonItemRefresh.Caption = "تحديث";
            this.barButtonItemRefresh.Id = 4;
            this.barButtonItemRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemRefresh.ImageOptions.Image")));
            this.barButtonItemRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemRefresh.ImageOptions.LargeImage")));
            this.barButtonItemRefresh.Name = "barButtonItemRefresh";
            this.barButtonItemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemRefresh_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "الرئيسية";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemAdd);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemEdit);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemDelete);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemRefresh);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "عمليات";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 673);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1024, 24);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 158);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControlSalaries);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1024, 515);
            this.splitContainerControl1.SplitterPosition = 450;
            this.splitContainerControl1.TabIndex = 2;
            // 
            // gridControlSalaries
            // 
            this.gridControlSalaries.DataSource = this.employeeSalaryBindingSource;
            this.gridControlSalaries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSalaries.Location = new System.Drawing.Point(0, 0);
            this.gridControlSalaries.MainView = this.gridViewSalaries;
            this.gridControlSalaries.MenuManager = this.ribbon;
            this.gridControlSalaries.Name = "gridControlSalaries";
            this.gridControlSalaries.Size = new System.Drawing.Size(450, 515);
            this.gridControlSalaries.TabIndex = 0;
            this.gridControlSalaries.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSalaries});
            // 
            // gridViewSalaries
            // 
            this.gridViewSalaries.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colEmployeeID,
            this.colEmployeeName,
            this.colEmployeeCode,
            this.colEffectiveDate,
            this.colIsActive});
            this.gridViewSalaries.GridControl = this.gridControlSalaries;
            this.gridViewSalaries.Name = "gridViewSalaries";
            this.gridViewSalaries.OptionsBehavior.Editable = false;
            this.gridViewSalaries.OptionsView.ShowGroupPanel = false;
            this.gridViewSalaries.DoubleClick += new System.EventHandler(this.gridViewSalaries_DoubleClick);
            // 
            // colID
            // 
            this.colID.Caption = "الرقم";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 50;
            // 
            // colEmployeeID
            // 
            this.colEmployeeID.Caption = "رقم الموظف";
            this.colEmployeeID.FieldName = "EmployeeID";
            this.colEmployeeID.Name = "colEmployeeID";
            // 
            // colEmployeeName
            // 
            this.colEmployeeName.Caption = "اسم الموظف";
            this.colEmployeeName.FieldName = "EmployeeName";
            this.colEmployeeName.Name = "colEmployeeName";
            this.colEmployeeName.Visible = true;
            this.colEmployeeName.VisibleIndex = 1;
            this.colEmployeeName.Width = 150;
            // 
            // colEmployeeCode
            // 
            this.colEmployeeCode.Caption = "رقم الموظف";
            this.colEmployeeCode.FieldName = "EmployeeCode";
            this.colEmployeeCode.Name = "colEmployeeCode";
            this.colEmployeeCode.Visible = true;
            this.colEmployeeCode.VisibleIndex = 2;
            this.colEmployeeCode.Width = 80;
            // 
            // colEffectiveDate
            // 
            this.colEffectiveDate.Caption = "تاريخ السريان";
            this.colEffectiveDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colEffectiveDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colEffectiveDate.FieldName = "EffectiveDate";
            this.colEffectiveDate.Name = "colEffectiveDate";
            this.colEffectiveDate.Visible = true;
            this.colEffectiveDate.VisibleIndex = 3;
            this.colEffectiveDate.Width = 80;
            // 
            // colIsActive
            // 
            this.colIsActive.Caption = "نشط";
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.Visible = true;
            this.colIsActive.VisibleIndex = 4;
            this.colIsActive.Width = 40;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControlDetails);
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(569, 515);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControlDetails
            // 
            this.groupControlDetails.Controls.Add(this.panelControl3);
            this.groupControlDetails.Controls.Add(this.gridControlComponents);
            this.groupControlDetails.Controls.Add(this.labelControl3);
            this.groupControlDetails.Controls.Add(this.panelControl2);
            this.groupControlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlDetails.Location = new System.Drawing.Point(2, 2);
            this.groupControlDetails.Name = "groupControlDetails";
            this.groupControlDetails.Size = new System.Drawing.Size(565, 466);
            this.groupControlDetails.TabIndex = 0;
            this.groupControlDetails.Text = "تفاصيل الراتب";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.labelControlTotalAmount);
            this.panelControl3.Controls.Add(this.simpleButtonDeleteComponent);
            this.panelControl3.Controls.Add(this.simpleButtonAddComponent);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(2, 427);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(561, 37);
            this.panelControl3.TabIndex = 3;
            // 
            // labelControlTotalAmount
            // 
            this.labelControlTotalAmount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControlTotalAmount.Appearance.Options.UseFont = true;
            this.labelControlTotalAmount.Location = new System.Drawing.Point(343, 12);
            this.labelControlTotalAmount.Name = "labelControlTotalAmount";
            this.labelControlTotalAmount.Size = new System.Drawing.Size(90, 13);
            this.labelControlTotalAmount.TabIndex = 2;
            this.labelControlTotalAmount.Text = "إجمالي الراتب: 0.00";
            // 
            // simpleButtonDeleteComponent
            // 
            this.simpleButtonDeleteComponent.Location = new System.Drawing.Point(100, 7);
            this.simpleButtonDeleteComponent.Name = "simpleButtonDeleteComponent";
            this.simpleButtonDeleteComponent.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonDeleteComponent.TabIndex = 1;
            this.simpleButtonDeleteComponent.Text = "حذف عنصر";
            this.simpleButtonDeleteComponent.Click += new System.EventHandler(this.simpleButtonDeleteComponent_Click);
            // 
            // simpleButtonAddComponent
            // 
            this.simpleButtonAddComponent.Location = new System.Drawing.Point(19, 7);
            this.simpleButtonAddComponent.Name = "simpleButtonAddComponent";
            this.simpleButtonAddComponent.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonAddComponent.TabIndex = 0;
            this.simpleButtonAddComponent.Text = "إضافة عنصر";
            this.simpleButtonAddComponent.Click += new System.EventHandler(this.simpleButtonAddComponent_Click);
            // 
            // gridControlComponents
            // 
            this.gridControlComponents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlComponents.Location = new System.Drawing.Point(5, 129);
            this.gridControlComponents.MainView = this.gridViewComponents;
            this.gridControlComponents.MenuManager = this.ribbon;
            this.gridControlComponents.Name = "gridControlComponents";
            this.gridControlComponents.Size = new System.Drawing.Size(555, 292);
            this.gridControlComponents.TabIndex = 2;
            this.gridControlComponents.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewComponents});
            // 
            // gridViewComponents
            // 
            this.gridViewComponents.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colComponentID,
            this.colComponentName,
            this.colAmount,
            this.colNotes});
            this.gridViewComponents.GridControl = this.gridControlComponents;
            this.gridViewComponents.Name = "gridViewComponents";
            this.gridViewComponents.OptionsBehavior.Editable = false;
            this.gridViewComponents.OptionsView.ShowGroupPanel = false;
            this.gridViewComponents.DoubleClick += new System.EventHandler(this.gridViewComponents_DoubleClick);
            // 
            // colComponentID
            // 
            this.colComponentID.Caption = "رقم العنصر";
            this.colComponentID.FieldName = "ComponentID";
            this.colComponentID.Name = "colComponentID";
            // 
            // colComponentName
            // 
            this.colComponentName.Caption = "اسم العنصر";
            this.colComponentName.FieldName = "ComponentName";
            this.colComponentName.Name = "colComponentName";
            this.colComponentName.Visible = true;
            this.colComponentName.VisibleIndex = 0;
            this.colComponentName.Width = 150;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "المبلغ";
            this.colAmount.DisplayFormat.FormatString = "N2";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 1;
            this.colAmount.Width = 80;
            // 
            // colNotes
            // 
            this.colNotes.Caption = "ملاحظات";
            this.colNotes.FieldName = "Notes";
            this.colNotes.Name = "colNotes";
            this.colNotes.Visible = true;
            this.colNotes.VisibleIndex = 2;
            this.colNotes.Width = 200;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(459, 110);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 13);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "عناصر الراتب:";
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.dateEditEffectiveDate);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.searchLookUpEditEmployee);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Location = new System.Drawing.Point(5, 26);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(555, 78);
            this.panelControl2.TabIndex = 0;
            // 
            // dateEditEffectiveDate
            // 
            this.dateEditEffectiveDate.EditValue = null;
            this.dateEditEffectiveDate.Location = new System.Drawing.Point(14, 42);
            this.dateEditEffectiveDate.MenuManager = this.ribbon;
            this.dateEditEffectiveDate.Name = "dateEditEffectiveDate";
            this.dateEditEffectiveDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEffectiveDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEffectiveDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateEditEffectiveDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditEffectiveDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateEditEffectiveDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditEffectiveDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateEditEffectiveDate.Size = new System.Drawing.Size(445, 20);
            this.dateEditEffectiveDate.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(465, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "تاريخ السريان:";
            // 
            // searchLookUpEditEmployee
            // 
            this.searchLookUpEditEmployee.Location = new System.Drawing.Point(14, 16);
            this.searchLookUpEditEmployee.MenuManager = this.ribbon;
            this.searchLookUpEditEmployee.Name = "searchLookUpEditEmployee";
            this.searchLookUpEditEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEditEmployee.Properties.NullText = "";
            this.searchLookUpEditEmployee.Properties.PopupView = this.searchLookUpEdit1View;
            this.searchLookUpEditEmployee.Size = new System.Drawing.Size(445, 20);
            this.searchLookUpEditEmployee.TabIndex = 1;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(465, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(42, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "الموظف:";
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.simpleButtonCancel);
            this.panelControl4.Controls.Add(this.simpleButtonSave);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(2, 468);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(565, 45);
            this.panelControl4.TabIndex = 1;
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Location = new System.Drawing.Point(100, 10);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCancel.TabIndex = 1;
            this.simpleButtonCancel.Text = "إلغاء";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // simpleButtonSave
            // 
            this.simpleButtonSave.Location = new System.Drawing.Point(19, 10);
            this.simpleButtonSave.Name = "simpleButtonSave";
            this.simpleButtonSave.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonSave.TabIndex = 0;
            this.simpleButtonSave.Text = "حفظ";
            this.simpleButtonSave.Click += new System.EventHandler(this.simpleButtonSave_Click);
            // 
            // EmployeeSalariesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 697);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "EmployeeSalariesForm";
            this.Ribbon = this.ribbon;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "إدارة رواتب الموظفين";
            this.Load += new System.EventHandler(this.EmployeeSalariesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSalaries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeSalaryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSalaries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetails)).EndInit();
            this.groupControlDetails.ResumeLayout(false);
            this.groupControlDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlComponents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewComponents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEffectiveDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEffectiveDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAdd;
        private DevExpress.XtraBars.BarButtonItem barButtonItemEdit;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRefresh;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControlSalaries;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSalaries;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControlDetails;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.DateEdit dateEditEffectiveDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEditEmployee;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl gridControlComponents;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewComponents;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelControlTotalAmount;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDeleteComponent;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAddComponent;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSave;
        private System.Windows.Forms.BindingSource employeeSalaryBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colEffectiveDate;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
        private DevExpress.XtraGrid.Columns.GridColumn colComponentID;
        private DevExpress.XtraGrid.Columns.GridColumn colComponentName;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colNotes;
    }
}