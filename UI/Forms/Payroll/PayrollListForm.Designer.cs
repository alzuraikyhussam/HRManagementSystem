namespace HR.UI.Forms.Payroll
{
    partial class PayrollListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PayrollListForm));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCreate = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemApprove = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemClose = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemExport = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSalaryComponents = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemEmployeeSalaries = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupData = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupActions = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupExport = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupSettings = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlPayrolls = new DevExpress.XtraGrid.GridControl();
            this.payrollBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewPayrolls = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayrollName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayrollPeriod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayrollMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayrollYear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalEmployees = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalPayrollAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaymentDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApprovedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApprovedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colView = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.comboBoxEditYear = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonFilter = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPayrolls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.payrollBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPayrolls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barButtonItemRefresh,
            this.barButtonItemCreate,
            this.barButtonItemApprove,
            this.barButtonItemClose,
            this.barButtonItemCancel,
            this.barButtonItemPrint,
            this.barButtonItemExport,
            this.barButtonItemSalaryComponents,
            this.barButtonItemEmployeeSalaries});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 10;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1198, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barButtonItemRefresh
            // 
            this.barButtonItemRefresh.Caption = "تحديث";
            this.barButtonItemRefresh.Id = 1;
            this.barButtonItemRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemRefresh.ImageOptions.Image")));
            this.barButtonItemRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemRefresh.ImageOptions.LargeImage")));
            this.barButtonItemRefresh.Name = "barButtonItemRefresh";
            this.barButtonItemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemRefresh_ItemClick);
            // 
            // barButtonItemCreate
            // 
            this.barButtonItemCreate.Caption = "إنشاء كشف رواتب";
            this.barButtonItemCreate.Id = 2;
            this.barButtonItemCreate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemCreate.ImageOptions.Image")));
            this.barButtonItemCreate.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemCreate.ImageOptions.LargeImage")));
            this.barButtonItemCreate.Name = "barButtonItemCreate";
            this.barButtonItemCreate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCreate_ItemClick);
            // 
            // barButtonItemApprove
            // 
            this.barButtonItemApprove.Caption = "اعتماد";
            this.barButtonItemApprove.Id = 3;
            this.barButtonItemApprove.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemApprove.ImageOptions.Image")));
            this.barButtonItemApprove.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemApprove.ImageOptions.LargeImage")));
            this.barButtonItemApprove.Name = "barButtonItemApprove";
            this.barButtonItemApprove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemApprove_ItemClick);
            // 
            // barButtonItemClose
            // 
            this.barButtonItemClose.Caption = "إقفال";
            this.barButtonItemClose.Id = 4;
            this.barButtonItemClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemClose.ImageOptions.Image")));
            this.barButtonItemClose.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemClose.ImageOptions.LargeImage")));
            this.barButtonItemClose.Name = "barButtonItemClose";
            this.barButtonItemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemClose_ItemClick);
            // 
            // barButtonItemCancel
            // 
            this.barButtonItemCancel.Caption = "إلغاء";
            this.barButtonItemCancel.Id = 5;
            this.barButtonItemCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemCancel.ImageOptions.Image")));
            this.barButtonItemCancel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemCancel.ImageOptions.LargeImage")));
            this.barButtonItemCancel.Name = "barButtonItemCancel";
            this.barButtonItemCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCancel_ItemClick);
            // 
            // barButtonItemPrint
            // 
            this.barButtonItemPrint.Caption = "طباعة";
            this.barButtonItemPrint.Id = 6;
            this.barButtonItemPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemPrint.ImageOptions.Image")));
            this.barButtonItemPrint.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemPrint.ImageOptions.LargeImage")));
            this.barButtonItemPrint.Name = "barButtonItemPrint";
            this.barButtonItemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemPrint_ItemClick);
            // 
            // barButtonItemExport
            // 
            this.barButtonItemExport.Caption = "تصدير";
            this.barButtonItemExport.Id = 7;
            this.barButtonItemExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemExport.ImageOptions.Image")));
            this.barButtonItemExport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemExport.ImageOptions.LargeImage")));
            this.barButtonItemExport.Name = "barButtonItemExport";
            this.barButtonItemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemExport_ItemClick);
            // 
            // barButtonItemSalaryComponents
            // 
            this.barButtonItemSalaryComponents.Caption = "عناصر الرواتب";
            this.barButtonItemSalaryComponents.Id = 8;
            this.barButtonItemSalaryComponents.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemSalaryComponents.ImageOptions.Image")));
            this.barButtonItemSalaryComponents.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemSalaryComponents.ImageOptions.LargeImage")));
            this.barButtonItemSalaryComponents.Name = "barButtonItemSalaryComponents";
            this.barButtonItemSalaryComponents.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSalaryComponents_ItemClick);
            // 
            // barButtonItemEmployeeSalaries
            // 
            this.barButtonItemEmployeeSalaries.Caption = "رواتب الموظفين";
            this.barButtonItemEmployeeSalaries.Id = 9;
            this.barButtonItemEmployeeSalaries.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemEmployeeSalaries.ImageOptions.Image")));
            this.barButtonItemEmployeeSalaries.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemEmployeeSalaries.ImageOptions.LargeImage")));
            this.barButtonItemEmployeeSalaries.Name = "barButtonItemEmployeeSalaries";
            this.barButtonItemEmployeeSalaries.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemEmployeeSalaries_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupData,
            this.ribbonPageGroupActions,
            this.ribbonPageGroupExport,
            this.ribbonPageGroupSettings});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "الرئيسية";
            // 
            // ribbonPageGroupData
            // 
            this.ribbonPageGroupData.ItemLinks.Add(this.barButtonItemRefresh);
            this.ribbonPageGroupData.ItemLinks.Add(this.barButtonItemCreate);
            this.ribbonPageGroupData.Name = "ribbonPageGroupData";
            this.ribbonPageGroupData.Text = "البيانات";
            // 
            // ribbonPageGroupActions
            // 
            this.ribbonPageGroupActions.ItemLinks.Add(this.barButtonItemApprove);
            this.ribbonPageGroupActions.ItemLinks.Add(this.barButtonItemClose);
            this.ribbonPageGroupActions.ItemLinks.Add(this.barButtonItemCancel);
            this.ribbonPageGroupActions.Name = "ribbonPageGroupActions";
            this.ribbonPageGroupActions.Text = "الإجراءات";
            // 
            // ribbonPageGroupExport
            // 
            this.ribbonPageGroupExport.ItemLinks.Add(this.barButtonItemPrint);
            this.ribbonPageGroupExport.ItemLinks.Add(this.barButtonItemExport);
            this.ribbonPageGroupExport.Name = "ribbonPageGroupExport";
            this.ribbonPageGroupExport.Text = "طباعة وتصدير";
            // 
            // ribbonPageGroupSettings
            // 
            this.ribbonPageGroupSettings.ItemLinks.Add(this.barButtonItemSalaryComponents);
            this.ribbonPageGroupSettings.ItemLinks.Add(this.barButtonItemEmployeeSalaries);
            this.ribbonPageGroupSettings.Name = "ribbonPageGroupSettings";
            this.ribbonPageGroupSettings.Text = "الإعدادات";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 582);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1198, 24);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControlPayrolls);
            this.layoutControl1.Controls.Add(this.comboBoxEditYear);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.simpleButtonFilter);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1198, 424);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControlPayrolls
            // 
            this.gridControlPayrolls.DataSource = this.payrollBindingSource;
            this.gridControlPayrolls.Location = new System.Drawing.Point(12, 96);
            this.gridControlPayrolls.MainView = this.gridViewPayrolls;
            this.gridControlPayrolls.MenuManager = this.ribbon;
            this.gridControlPayrolls.Name = "gridControlPayrolls";
            this.gridControlPayrolls.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControlPayrolls.Size = new System.Drawing.Size(1174, 316);
            this.gridControlPayrolls.TabIndex = 3;
            this.gridControlPayrolls.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPayrolls});
            // 
            // payrollBindingSource
            // 
            this.payrollBindingSource.DataSource = typeof(HR.Models.Payroll);
            // 
            // gridViewPayrolls
            // 
            this.gridViewPayrolls.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colPayrollName,
            this.colPayrollPeriod,
            this.colPayrollMonth,
            this.colPayrollYear,
            this.colStartDate,
            this.colEndDate,
            this.colTotalEmployees,
            this.colTotalPayrollAmount,
            this.colStatus,
            this.colPaymentDate,
            this.colCreatedBy,
            this.colCreatedDate,
            this.colApprovedBy,
            this.colApprovedDate,
            this.colView});
            this.gridViewPayrolls.GridControl = this.gridControlPayrolls;
            this.gridViewPayrolls.Name = "gridViewPayrolls";
            this.gridViewPayrolls.OptionsView.ShowGroupPanel = false;
            this.gridViewPayrolls.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewPayrolls_RowClick);
            // 
            // colID
            // 
            this.colID.Caption = "المعرف";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 25;
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 60;
            // 
            // colPayrollName
            // 
            this.colPayrollName.Caption = "اسم كشف الرواتب";
            this.colPayrollName.FieldName = "PayrollName";
            this.colPayrollName.MinWidth = 25;
            this.colPayrollName.Name = "colPayrollName";
            this.colPayrollName.Visible = true;
            this.colPayrollName.VisibleIndex = 1;
            this.colPayrollName.Width = 150;
            // 
            // colPayrollPeriod
            // 
            this.colPayrollPeriod.Caption = "الفترة";
            this.colPayrollPeriod.FieldName = "PayrollPeriod";
            this.colPayrollPeriod.MinWidth = 25;
            this.colPayrollPeriod.Name = "colPayrollPeriod";
            this.colPayrollPeriod.Visible = true;
            this.colPayrollPeriod.VisibleIndex = 2;
            this.colPayrollPeriod.Width = 80;
            // 
            // colPayrollMonth
            // 
            this.colPayrollMonth.Caption = "الشهر";
            this.colPayrollMonth.FieldName = "PayrollMonth";
            this.colPayrollMonth.MinWidth = 25;
            this.colPayrollMonth.Name = "colPayrollMonth";
            this.colPayrollMonth.Visible = true;
            this.colPayrollMonth.VisibleIndex = 3;
            this.colPayrollMonth.Width = 50;
            // 
            // colPayrollYear
            // 
            this.colPayrollYear.Caption = "السنة";
            this.colPayrollYear.FieldName = "PayrollYear";
            this.colPayrollYear.MinWidth = 25;
            this.colPayrollYear.Name = "colPayrollYear";
            this.colPayrollYear.Visible = true;
            this.colPayrollYear.VisibleIndex = 4;
            this.colPayrollYear.Width = 60;
            // 
            // colStartDate
            // 
            this.colStartDate.Caption = "تاريخ البداية";
            this.colStartDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colStartDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colStartDate.FieldName = "StartDate";
            this.colStartDate.MinWidth = 25;
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.Visible = true;
            this.colStartDate.VisibleIndex = 5;
            this.colStartDate.Width = 80;
            // 
            // colEndDate
            // 
            this.colEndDate.Caption = "تاريخ النهاية";
            this.colEndDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colEndDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colEndDate.FieldName = "EndDate";
            this.colEndDate.MinWidth = 25;
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.Visible = true;
            this.colEndDate.VisibleIndex = 6;
            this.colEndDate.Width = 80;
            // 
            // colTotalEmployees
            // 
            this.colTotalEmployees.Caption = "عدد الموظفين";
            this.colTotalEmployees.FieldName = "TotalEmployees";
            this.colTotalEmployees.MinWidth = 25;
            this.colTotalEmployees.Name = "colTotalEmployees";
            this.colTotalEmployees.Visible = true;
            this.colTotalEmployees.VisibleIndex = 7;
            this.colTotalEmployees.Width = 80;
            // 
            // colTotalPayrollAmount
            // 
            this.colTotalPayrollAmount.Caption = "إجمالي المبلغ";
            this.colTotalPayrollAmount.DisplayFormat.FormatString = "N2";
            this.colTotalPayrollAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalPayrollAmount.FieldName = "TotalPayrollAmount";
            this.colTotalPayrollAmount.MinWidth = 25;
            this.colTotalPayrollAmount.Name = "colTotalPayrollAmount";
            this.colTotalPayrollAmount.Visible = true;
            this.colTotalPayrollAmount.VisibleIndex = 8;
            this.colTotalPayrollAmount.Width = 100;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "الحالة";
            this.colStatus.FieldName = "Status";
            this.colStatus.MinWidth = 25;
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 9;
            this.colStatus.Width = 80;
            // 
            // colPaymentDate
            // 
            this.colPaymentDate.Caption = "تاريخ الدفع";
            this.colPaymentDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colPaymentDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colPaymentDate.FieldName = "PaymentDate";
            this.colPaymentDate.MinWidth = 25;
            this.colPaymentDate.Name = "colPaymentDate";
            this.colPaymentDate.Visible = true;
            this.colPaymentDate.VisibleIndex = 10;
            this.colPaymentDate.Width = 80;
            // 
            // colCreatedBy
            // 
            this.colCreatedBy.Caption = "المنشئ";
            this.colCreatedBy.FieldName = "CreatedByName";
            this.colCreatedBy.MinWidth = 25;
            this.colCreatedBy.Name = "colCreatedBy";
            this.colCreatedBy.Visible = true;
            this.colCreatedBy.VisibleIndex = 11;
            this.colCreatedBy.Width = 80;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.Caption = "تاريخ الإنشاء";
            this.colCreatedDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colCreatedDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreatedDate.FieldName = "CreatedDate";
            this.colCreatedDate.MinWidth = 25;
            this.colCreatedDate.Name = "colCreatedDate";
            this.colCreatedDate.Visible = true;
            this.colCreatedDate.VisibleIndex = 12;
            this.colCreatedDate.Width = 80;
            // 
            // colApprovedBy
            // 
            this.colApprovedBy.Caption = "المعتمد";
            this.colApprovedBy.FieldName = "ApprovedByName";
            this.colApprovedBy.MinWidth = 25;
            this.colApprovedBy.Name = "colApprovedBy";
            this.colApprovedBy.Visible = true;
            this.colApprovedBy.VisibleIndex = 13;
            this.colApprovedBy.Width = 80;
            // 
            // colApprovedDate
            // 
            this.colApprovedDate.Caption = "تاريخ الاعتماد";
            this.colApprovedDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colApprovedDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colApprovedDate.FieldName = "ApprovedDate";
            this.colApprovedDate.MinWidth = 25;
            this.colApprovedDate.Name = "colApprovedDate";
            this.colApprovedDate.Visible = true;
            this.colApprovedDate.VisibleIndex = 14;
            this.colApprovedDate.Width = 80;
            // 
            // colView
            // 
            this.colView.Caption = "عرض";
            this.colView.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colView.MinWidth = 25;
            this.colView.Name = "colView";
            this.colView.Visible = true;
            this.colView.VisibleIndex = 15;
            this.colView.Width = 60;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // comboBoxEditYear
            // 
            this.comboBoxEditYear.Location = new System.Drawing.Point(840, 43);
            this.comboBoxEditYear.MenuManager = this.ribbon;
            this.comboBoxEditYear.Name = "comboBoxEditYear";
            this.comboBoxEditYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditYear.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditYear.Size = new System.Drawing.Size(176, 20);
            this.comboBoxEditYear.StyleController = this.layoutControl1;
            this.comboBoxEditYear.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(1020, 43);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "السنة:";
            // 
            // simpleButtonFilter
            // 
            this.simpleButtonFilter.Location = new System.Drawing.Point(734, 43);
            this.simpleButtonFilter.Name = "simpleButtonFilter";
            this.simpleButtonFilter.Size = new System.Drawing.Size(102, 22);
            this.simpleButtonFilter.StyleController = this.layoutControl1;
            this.simpleButtonFilter.TabIndex = 1;
            this.simpleButtonFilter.Text = "عرض";
            this.simpleButtonFilter.Click += new System.EventHandler(this.simpleButtonFilter_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1198, 424);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlPayrolls;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 84);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1178, 320);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1178, 84);
            this.layoutControlGroup1.Text = "فلترة البيانات";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.comboBoxEditYear;
            this.layoutControlItem2.Location = new System.Drawing.Point(716, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(180, 43);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.labelControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(896, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(258, 43);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonFilter;
            this.layoutControlItem4.Location = new System.Drawing.Point(610, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(106, 43);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(610, 43);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // PayrollListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 606);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("PayrollListForm.IconOptions.Image")));
            this.Name = "PayrollListForm";
            this.Ribbon = this.ribbon;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "كشوف الرواتب";
            this.Load += new System.EventHandler(this.PayrollListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPayrolls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.payrollBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPayrolls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupData;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gridControlPayrolls;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPayrolls;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRefresh;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCreate;
        private DevExpress.XtraBars.BarButtonItem barButtonItemApprove;
        private DevExpress.XtraBars.BarButtonItem barButtonItemClose;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancel;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPrint;
        private DevExpress.XtraBars.BarButtonItem barButtonItemExport;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSalaryComponents;
        private DevExpress.XtraBars.BarButtonItem barButtonItemEmployeeSalaries;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupActions;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupExport;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSettings;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditYear;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonFilter;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private System.Windows.Forms.BindingSource payrollBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollName;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollPeriod;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollMonth;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollYear;
        private DevExpress.XtraGrid.Columns.GridColumn colStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalEmployees;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalPayrollAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colPaymentDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colApprovedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colApprovedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colView;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
    }
}