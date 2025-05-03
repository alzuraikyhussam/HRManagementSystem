namespace HR.UI.Forms.Deductions
{
    partial class DeductionsListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeductionsListForm));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemExport = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemRules = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemApprove = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemReject = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupActions = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupApproval = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupTools = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControlFilters = new DevExpress.XtraLayout.LayoutControl();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.lookUpEditEmployee = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.comboBoxEditStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButtonApplyFilter = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonClearFilter = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroupFilters = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemFromFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemToFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemEmployeeFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDepartmentFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemStatusFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTypeFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemApplyFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemClearFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControlDeductions = new DevExpress.XtraGrid.GridControl();
            this.deductionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewDeductions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeductionRuleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeductionDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colViolationDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colViolationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colViolationValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeductionMethod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeductionValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApprovedByUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApprovalDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEdit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEditItem = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEditDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlFilters)).BeginInit();
            this.layoutControlFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFilters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFromFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemToFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEmployeeFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDepartmentFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStatusFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTypeFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemApplyFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClearFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDeductions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deductionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDeductions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barButtonItemRefresh,
            this.barButtonItemAdd,
            this.barButtonItemEdit,
            this.barButtonItemDelete,
            this.barButtonItemPrint,
            this.barButtonItemExport,
            this.barButtonItemRules,
            this.barButtonItemApprove,
            this.barButtonItemReject});
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
            // barButtonItemAdd
            // 
            this.barButtonItemAdd.Caption = "إضافة خصم";
            this.barButtonItemAdd.Id = 2;
            this.barButtonItemAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemAdd.ImageOptions.Image")));
            this.barButtonItemAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemAdd.ImageOptions.LargeImage")));
            this.barButtonItemAdd.Name = "barButtonItemAdd";
            this.barButtonItemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAdd_ItemClick);
            // 
            // barButtonItemEdit
            // 
            this.barButtonItemEdit.Caption = "تعديل";
            this.barButtonItemEdit.Id = 3;
            this.barButtonItemEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemEdit.ImageOptions.Image")));
            this.barButtonItemEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemEdit.ImageOptions.LargeImage")));
            this.barButtonItemEdit.Name = "barButtonItemEdit";
            this.barButtonItemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemEdit_ItemClick);
            // 
            // barButtonItemDelete
            // 
            this.barButtonItemDelete.Caption = "حذف";
            this.barButtonItemDelete.Id = 4;
            this.barButtonItemDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemDelete.ImageOptions.Image")));
            this.barButtonItemDelete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemDelete.ImageOptions.LargeImage")));
            this.barButtonItemDelete.Name = "barButtonItemDelete";
            this.barButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDelete_ItemClick);
            // 
            // barButtonItemPrint
            // 
            this.barButtonItemPrint.Caption = "طباعة";
            this.barButtonItemPrint.Id = 5;
            this.barButtonItemPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemPrint.ImageOptions.Image")));
            this.barButtonItemPrint.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemPrint.ImageOptions.LargeImage")));
            this.barButtonItemPrint.Name = "barButtonItemPrint";
            this.barButtonItemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemPrint_ItemClick);
            // 
            // barButtonItemExport
            // 
            this.barButtonItemExport.Caption = "تصدير";
            this.barButtonItemExport.Id = 6;
            this.barButtonItemExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemExport.ImageOptions.Image")));
            this.barButtonItemExport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemExport.ImageOptions.LargeImage")));
            this.barButtonItemExport.Name = "barButtonItemExport";
            this.barButtonItemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemExport_ItemClick);
            // 
            // barButtonItemRules
            // 
            this.barButtonItemRules.Caption = "قواعد الخصم";
            this.barButtonItemRules.Id = 7;
            this.barButtonItemRules.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemRules.ImageOptions.Image")));
            this.barButtonItemRules.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemRules.ImageOptions.LargeImage")));
            this.barButtonItemRules.Name = "barButtonItemRules";
            this.barButtonItemRules.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemRules_ItemClick);
            // 
            // barButtonItemApprove
            // 
            this.barButtonItemApprove.Caption = "اعتماد";
            this.barButtonItemApprove.Id = 8;
            this.barButtonItemApprove.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemApprove.ImageOptions.Image")));
            this.barButtonItemApprove.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemApprove.ImageOptions.LargeImage")));
            this.barButtonItemApprove.Name = "barButtonItemApprove";
            this.barButtonItemApprove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemApprove_ItemClick);
            // 
            // barButtonItemReject
            // 
            this.barButtonItemReject.Caption = "رفض";
            this.barButtonItemReject.Id = 9;
            this.barButtonItemReject.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemReject.ImageOptions.Image")));
            this.barButtonItemReject.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemReject.ImageOptions.LargeImage")));
            this.barButtonItemReject.Name = "barButtonItemReject";
            this.barButtonItemReject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemReject_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupActions,
            this.ribbonPageGroupApproval,
            this.ribbonPageGroupTools});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "الرئيسية";
            // 
            // ribbonPageGroupActions
            // 
            this.ribbonPageGroupActions.ItemLinks.Add(this.barButtonItemRefresh);
            this.ribbonPageGroupActions.ItemLinks.Add(this.barButtonItemAdd);
            this.ribbonPageGroupActions.ItemLinks.Add(this.barButtonItemEdit);
            this.ribbonPageGroupActions.ItemLinks.Add(this.barButtonItemDelete);
            this.ribbonPageGroupActions.Name = "ribbonPageGroupActions";
            this.ribbonPageGroupActions.Text = "العمليات";
            // 
            // ribbonPageGroupApproval
            // 
            this.ribbonPageGroupApproval.ItemLinks.Add(this.barButtonItemApprove);
            this.ribbonPageGroupApproval.ItemLinks.Add(this.barButtonItemReject);
            this.ribbonPageGroupApproval.Name = "ribbonPageGroupApproval";
            this.ribbonPageGroupApproval.Text = "الاعتماد";
            // 
            // ribbonPageGroupTools
            // 
            this.ribbonPageGroupTools.ItemLinks.Add(this.barButtonItemPrint);
            this.ribbonPageGroupTools.ItemLinks.Add(this.barButtonItemExport);
            this.ribbonPageGroupTools.ItemLinks.Add(this.barButtonItemRules);
            this.ribbonPageGroupTools.Name = "ribbonPageGroupTools";
            this.ribbonPageGroupTools.Text = "الأدوات";
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
            this.layoutControl1.Controls.Add(this.splitContainerControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1198, 424);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Location = new System.Drawing.Point(12, 12);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.layoutControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControlDeductions);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1174, 400);
            this.splitContainerControl1.SplitterPosition = 271;
            this.splitContainerControl1.TabIndex = 4;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.groupControl1);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.Root;
            this.layoutControl2.Size = new System.Drawing.Size(271, 400);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.layoutControlFilters);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(247, 376);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "الفلاتر";
            // 
            // layoutControlFilters
            // 
            this.layoutControlFilters.Controls.Add(this.dateEditFrom);
            this.layoutControlFilters.Controls.Add(this.dateEditTo);
            this.layoutControlFilters.Controls.Add(this.lookUpEditEmployee);
            this.layoutControlFilters.Controls.Add(this.lookUpEditDepartment);
            this.layoutControlFilters.Controls.Add(this.comboBoxEditStatus);
            this.layoutControlFilters.Controls.Add(this.comboBoxEditType);
            this.layoutControlFilters.Controls.Add(this.simpleButtonApplyFilter);
            this.layoutControlFilters.Controls.Add(this.simpleButtonClearFilter);
            this.layoutControlFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlFilters.Location = new System.Drawing.Point(2, 23);
            this.layoutControlFilters.Name = "layoutControlFilters";
            this.layoutControlFilters.Root = this.layoutControlGroupFilters;
            this.layoutControlFilters.Size = new System.Drawing.Size(243, 351);
            this.layoutControlFilters.TabIndex = 0;
            this.layoutControlFilters.Text = "layoutControl3";
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dateEditFrom.Location = new System.Drawing.Point(12, 28);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateEditFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditFrom.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateEditFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditFrom.Properties.MaskSettings.Set("mask", "D");
            this.dateEditFrom.Size = new System.Drawing.Size(159, 20);
            this.dateEditFrom.StyleController = this.layoutControlFilters;
            this.dateEditFrom.TabIndex = 4;
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dateEditTo.Location = new System.Drawing.Point(12, 68);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateEditTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditTo.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateEditTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditTo.Properties.MaskSettings.Set("mask", "D");
            this.dateEditTo.Size = new System.Drawing.Size(159, 20);
            this.dateEditTo.StyleController = this.layoutControlFilters;
            this.dateEditTo.TabIndex = 5;
            // 
            // lookUpEditEmployee
            // 
            this.lookUpEditEmployee.Location = new System.Drawing.Point(12, 108);
            this.lookUpEditEmployee.Name = "lookUpEditEmployee";
            this.lookUpEditEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditEmployee.Properties.DisplayMember = "FullName";
            this.lookUpEditEmployee.Properties.NullText = "كل الموظفين";
            this.lookUpEditEmployee.Properties.ValueMember = "ID";
            this.lookUpEditEmployee.Size = new System.Drawing.Size(159, 20);
            this.lookUpEditEmployee.StyleController = this.layoutControlFilters;
            this.lookUpEditEmployee.TabIndex = 6;
            // 
            // lookUpEditDepartment
            // 
            this.lookUpEditDepartment.Location = new System.Drawing.Point(12, 148);
            this.lookUpEditDepartment.Name = "lookUpEditDepartment";
            this.lookUpEditDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditDepartment.Properties.DisplayMember = "Name";
            this.lookUpEditDepartment.Properties.NullText = "كل الأقسام";
            this.lookUpEditDepartment.Properties.ValueMember = "ID";
            this.lookUpEditDepartment.Size = new System.Drawing.Size(159, 20);
            this.lookUpEditDepartment.StyleController = this.layoutControlFilters;
            this.lookUpEditDepartment.TabIndex = 7;
            // 
            // comboBoxEditStatus
            // 
            this.comboBoxEditStatus.Location = new System.Drawing.Point(12, 188);
            this.comboBoxEditStatus.Name = "comboBoxEditStatus";
            this.comboBoxEditStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditStatus.Properties.Items.AddRange(new object[] {
            "الكل",
            "Pending",
            "Approved",
            "Rejected",
            "Cancelled"});
            this.comboBoxEditStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditStatus.Size = new System.Drawing.Size(159, 20);
            this.comboBoxEditStatus.StyleController = this.layoutControlFilters;
            this.comboBoxEditStatus.TabIndex = 8;
            // 
            // comboBoxEditType
            // 
            this.comboBoxEditType.Location = new System.Drawing.Point(12, 228);
            this.comboBoxEditType.Name = "comboBoxEditType";
            this.comboBoxEditType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditType.Properties.Items.AddRange(new object[] {
            "الكل",
            "Late",
            "Absent",
            "Early",
            "Violation",
            "Other"});
            this.comboBoxEditType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditType.Size = new System.Drawing.Size(159, 20);
            this.comboBoxEditType.StyleController = this.layoutControlFilters;
            this.comboBoxEditType.TabIndex = 9;
            // 
            // simpleButtonApplyFilter
            // 
            this.simpleButtonApplyFilter.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonApplyFilter.ImageOptions.Image")));
            this.simpleButtonApplyFilter.Location = new System.Drawing.Point(12, 252);
            this.simpleButtonApplyFilter.Name = "simpleButtonApplyFilter";
            this.simpleButtonApplyFilter.Size = new System.Drawing.Size(108, 22);
            this.simpleButtonApplyFilter.StyleController = this.layoutControlFilters;
            this.simpleButtonApplyFilter.TabIndex = 10;
            this.simpleButtonApplyFilter.Text = "تطبيق";
            this.simpleButtonApplyFilter.Click += new System.EventHandler(this.simpleButtonApplyFilter_Click);
            // 
            // simpleButtonClearFilter
            // 
            this.simpleButtonClearFilter.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonClearFilter.ImageOptions.Image")));
            this.simpleButtonClearFilter.Location = new System.Drawing.Point(124, 252);
            this.simpleButtonClearFilter.Name = "simpleButtonClearFilter";
            this.simpleButtonClearFilter.Size = new System.Drawing.Size(107, 22);
            this.simpleButtonClearFilter.StyleController = this.layoutControlFilters;
            this.simpleButtonClearFilter.TabIndex = 11;
            this.simpleButtonClearFilter.Text = "مسح";
            this.simpleButtonClearFilter.Click += new System.EventHandler(this.simpleButtonClearFilter_Click);
            // 
            // layoutControlGroupFilters
            // 
            this.layoutControlGroupFilters.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupFilters.GroupBordersVisible = false;
            this.layoutControlGroupFilters.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemFromFilter,
            this.layoutControlItemToFilter,
            this.layoutControlItemEmployeeFilter,
            this.layoutControlItemDepartmentFilter,
            this.layoutControlItemStatusFilter,
            this.layoutControlItemTypeFilter,
            this.layoutControlItemApplyFilter,
            this.layoutControlItemClearFilter,
            this.emptySpaceItem1});
            this.layoutControlGroupFilters.Name = "layoutControlGroupFilters";
            this.layoutControlGroupFilters.Size = new System.Drawing.Size(243, 351);
            this.layoutControlGroupFilters.TextVisible = false;
            // 
            // layoutControlItemFromFilter
            // 
            this.layoutControlItemFromFilter.Control = this.dateEditFrom;
            this.layoutControlItemFromFilter.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemFromFilter.Name = "layoutControlItemFromFilter";
            this.layoutControlItemFromFilter.Size = new System.Drawing.Size(223, 40);
            this.layoutControlItemFromFilter.Text = "من تاريخ:";
            this.layoutControlItemFromFilter.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemFromFilter.TextSize = new System.Drawing.Size(56, 13);
            // 
            // layoutControlItemToFilter
            // 
            this.layoutControlItemToFilter.Control = this.dateEditTo;
            this.layoutControlItemToFilter.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItemToFilter.Name = "layoutControlItemToFilter";
            this.layoutControlItemToFilter.Size = new System.Drawing.Size(223, 40);
            this.layoutControlItemToFilter.Text = "إلى تاريخ:";
            this.layoutControlItemToFilter.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemToFilter.TextSize = new System.Drawing.Size(56, 13);
            // 
            // layoutControlItemEmployeeFilter
            // 
            this.layoutControlItemEmployeeFilter.Control = this.lookUpEditEmployee;
            this.layoutControlItemEmployeeFilter.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItemEmployeeFilter.Name = "layoutControlItemEmployeeFilter";
            this.layoutControlItemEmployeeFilter.Size = new System.Drawing.Size(223, 40);
            this.layoutControlItemEmployeeFilter.Text = "الموظف:";
            this.layoutControlItemEmployeeFilter.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemEmployeeFilter.TextSize = new System.Drawing.Size(56, 13);
            // 
            // layoutControlItemDepartmentFilter
            // 
            this.layoutControlItemDepartmentFilter.Control = this.lookUpEditDepartment;
            this.layoutControlItemDepartmentFilter.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItemDepartmentFilter.Name = "layoutControlItemDepartmentFilter";
            this.layoutControlItemDepartmentFilter.Size = new System.Drawing.Size(223, 40);
            this.layoutControlItemDepartmentFilter.Text = "القسم:";
            this.layoutControlItemDepartmentFilter.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemDepartmentFilter.TextSize = new System.Drawing.Size(56, 13);
            // 
            // layoutControlItemStatusFilter
            // 
            this.layoutControlItemStatusFilter.Control = this.comboBoxEditStatus;
            this.layoutControlItemStatusFilter.Location = new System.Drawing.Point(0, 160);
            this.layoutControlItemStatusFilter.Name = "layoutControlItemStatusFilter";
            this.layoutControlItemStatusFilter.Size = new System.Drawing.Size(223, 40);
            this.layoutControlItemStatusFilter.Text = "الحالة:";
            this.layoutControlItemStatusFilter.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemStatusFilter.TextSize = new System.Drawing.Size(56, 13);
            // 
            // layoutControlItemTypeFilter
            // 
            this.layoutControlItemTypeFilter.Control = this.comboBoxEditType;
            this.layoutControlItemTypeFilter.Location = new System.Drawing.Point(0, 200);
            this.layoutControlItemTypeFilter.Name = "layoutControlItemTypeFilter";
            this.layoutControlItemTypeFilter.Size = new System.Drawing.Size(223, 40);
            this.layoutControlItemTypeFilter.Text = "نوع المخالفة:";
            this.layoutControlItemTypeFilter.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemTypeFilter.TextSize = new System.Drawing.Size(56, 13);
            // 
            // layoutControlItemApplyFilter
            // 
            this.layoutControlItemApplyFilter.Control = this.simpleButtonApplyFilter;
            this.layoutControlItemApplyFilter.Location = new System.Drawing.Point(0, 240);
            this.layoutControlItemApplyFilter.Name = "layoutControlItemApplyFilter";
            this.layoutControlItemApplyFilter.Size = new System.Drawing.Size(112, 26);
            this.layoutControlItemApplyFilter.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemApplyFilter.TextVisible = false;
            // 
            // layoutControlItemClearFilter
            // 
            this.layoutControlItemClearFilter.Control = this.simpleButtonClearFilter;
            this.layoutControlItemClearFilter.Location = new System.Drawing.Point(112, 240);
            this.layoutControlItemClearFilter.Name = "layoutControlItemClearFilter";
            this.layoutControlItemClearFilter.Size = new System.Drawing.Size(111, 26);
            this.layoutControlItemClearFilter.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemClearFilter.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 266);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(223, 65);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(271, 400);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.groupControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(251, 380);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // gridControlDeductions
            // 
            this.gridControlDeductions.DataSource = this.deductionBindingSource;
            this.gridControlDeductions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDeductions.Location = new System.Drawing.Point(0, 0);
            this.gridControlDeductions.MainView = this.gridViewDeductions;
            this.gridControlDeductions.MenuManager = this.ribbon;
            this.gridControlDeductions.Name = "gridControlDeductions";
            this.gridControlDeductions.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditItem,
            this.repositoryItemButtonEditDelete});
            this.gridControlDeductions.Size = new System.Drawing.Size(898, 400);
            this.gridControlDeductions.TabIndex = 0;
            this.gridControlDeductions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDeductions});
            // 
            // deductionBindingSource
            // 
            this.deductionBindingSource.DataSource = typeof(HR.Models.Deduction);
            // 
            // gridViewDeductions
            // 
            this.gridViewDeductions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colEmployeeID,
            this.colEmployeeName,
            this.colDepartmentName,
            this.colDeductionRuleName,
            this.colDeductionDate,
            this.colViolationDate,
            this.colViolationType,
            this.colViolationValue,
            this.colDeductionMethod,
            this.colDeductionValue,
            this.colDescription,
            this.colStatus,
            this.colApprovedByUser,
            this.colApprovalDate,
            this.colEdit,
            this.colDelete});
            this.gridViewDeductions.GridControl = this.gridControlDeductions;
            this.gridViewDeductions.Name = "gridViewDeductions";
            this.gridViewDeductions.OptionsBehavior.ReadOnly = true;
            this.gridViewDeductions.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewDeductions.OptionsView.ShowAutoFilterRow = true;
            this.gridViewDeductions.OptionsView.ShowFooter = true;
            // 
            // colID
            // 
            this.colID.Caption = "المعرف";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            // 
            // colEmployeeID
            // 
            this.colEmployeeID.Caption = "معرف الموظف";
            this.colEmployeeID.FieldName = "EmployeeID";
            this.colEmployeeID.Name = "colEmployeeID";
            // 
            // colEmployeeName
            // 
            this.colEmployeeName.Caption = "الموظف";
            this.colEmployeeName.FieldName = "EmployeeName";
            this.colEmployeeName.Name = "colEmployeeName";
            this.colEmployeeName.Visible = true;
            this.colEmployeeName.VisibleIndex = 1;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "القسم";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 2;
            // 
            // colDeductionRuleName
            // 
            this.colDeductionRuleName.Caption = "قاعدة الخصم";
            this.colDeductionRuleName.FieldName = "DeductionRuleName";
            this.colDeductionRuleName.Name = "colDeductionRuleName";
            this.colDeductionRuleName.Visible = true;
            this.colDeductionRuleName.VisibleIndex = 3;
            // 
            // colDeductionDate
            // 
            this.colDeductionDate.Caption = "تاريخ الخصم";
            this.colDeductionDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colDeductionDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDeductionDate.FieldName = "DeductionDate";
            this.colDeductionDate.Name = "colDeductionDate";
            this.colDeductionDate.Visible = true;
            this.colDeductionDate.VisibleIndex = 4;
            // 
            // colViolationDate
            // 
            this.colViolationDate.Caption = "تاريخ المخالفة";
            this.colViolationDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colViolationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colViolationDate.FieldName = "ViolationDate";
            this.colViolationDate.Name = "colViolationDate";
            this.colViolationDate.Visible = true;
            this.colViolationDate.VisibleIndex = 5;
            // 
            // colViolationType
            // 
            this.colViolationType.Caption = "نوع المخالفة";
            this.colViolationType.FieldName = "ViolationType";
            this.colViolationType.Name = "colViolationType";
            this.colViolationType.Visible = true;
            this.colViolationType.VisibleIndex = 6;
            // 
            // colViolationValue
            // 
            this.colViolationValue.Caption = "قيمة المخالفة";
            this.colViolationValue.FieldName = "ViolationValue";
            this.colViolationValue.Name = "colViolationValue";
            this.colViolationValue.Visible = true;
            this.colViolationValue.VisibleIndex = 7;
            // 
            // colDeductionMethod
            // 
            this.colDeductionMethod.Caption = "طريقة الخصم";
            this.colDeductionMethod.FieldName = "DeductionMethod";
            this.colDeductionMethod.Name = "colDeductionMethod";
            this.colDeductionMethod.Visible = true;
            this.colDeductionMethod.VisibleIndex = 8;
            // 
            // colDeductionValue
            // 
            this.colDeductionValue.Caption = "قيمة الخصم";
            this.colDeductionValue.FieldName = "DeductionValue";
            this.colDeductionValue.Name = "colDeductionValue";
            this.colDeductionValue.Visible = true;
            this.colDeductionValue.VisibleIndex = 9;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "الوصف";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 10;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "الحالة";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 11;
            // 
            // colApprovedByUser
            // 
            this.colApprovedByUser.Caption = "معتمد من";
            this.colApprovedByUser.FieldName = "ApprovedByUser";
            this.colApprovedByUser.Name = "colApprovedByUser";
            this.colApprovedByUser.Visible = true;
            this.colApprovedByUser.VisibleIndex = 12;
            // 
            // colApprovalDate
            // 
            this.colApprovalDate.Caption = "تاريخ الاعتماد";
            this.colApprovalDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colApprovalDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colApprovalDate.FieldName = "ApprovalDate";
            this.colApprovalDate.Name = "colApprovalDate";
            this.colApprovalDate.Visible = true;
            this.colApprovalDate.VisibleIndex = 13;
            // 
            // colEdit
            // 
            this.colEdit.Caption = "تعديل";
            this.colEdit.ColumnEdit = this.repositoryItemButtonEditItem;
            this.colEdit.Name = "colEdit";
            this.colEdit.Visible = true;
            this.colEdit.VisibleIndex = 14;
            // 
            // repositoryItemButtonEditItem
            // 
            this.repositoryItemButtonEditItem.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.repositoryItemButtonEditItem.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEditItem.Name = "repositoryItemButtonEditItem";
            this.repositoryItemButtonEditItem.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEditItem.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditItem_ButtonClick);
            // 
            // colDelete
            // 
            this.colDelete.Caption = "حذف";
            this.colDelete.ColumnEdit = this.repositoryItemButtonEditDelete;
            this.colDelete.Name = "colDelete";
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 15;
            // 
            // repositoryItemButtonEditDelete
            // 
            this.repositoryItemButtonEditDelete.AutoHeight = false;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.repositoryItemButtonEditDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEditDelete.Name = "repositoryItemButtonEditDelete";
            this.repositoryItemButtonEditDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEditDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditDelete_ButtonClick);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1198, 424);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.splitContainerControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1178, 404);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // DeductionsListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 606);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("DeductionsListForm.IconOptions.Image")));
            this.Name = "DeductionsListForm";
            this.Ribbon = this.ribbon;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "سجلات الخصم";
            this.Load += new System.EventHandler(this.DeductionsListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlFilters)).EndInit();
            this.layoutControlFilters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFilters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFromFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemToFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEmployeeFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDepartmentFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStatusFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTypeFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemApplyFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClearFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDeductions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deductionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDeductions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupActions;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRefresh;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAdd;
        private DevExpress.XtraBars.BarButtonItem barButtonItemEdit;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPrint;
        private DevExpress.XtraBars.BarButtonItem barButtonItemExport;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupTools;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRules;
        private DevExpress.XtraBars.BarButtonItem barButtonItemApprove;
        private DevExpress.XtraBars.BarButtonItem barButtonItemReject;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupApproval;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gridControlDeductions;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDeductions;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControl layoutControlFilters;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraEditors.DateEdit dateEditTo;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditEmployee;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditDepartment;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditStatus;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditType;
        private DevExpress.XtraEditors.SimpleButton simpleButtonApplyFilter;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClearFilter;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupFilters;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFromFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemToFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemEmployeeFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDepartmentFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemStatusFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTypeFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemApplyFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemClearFilter;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private System.Windows.Forms.BindingSource deductionBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colDeductionRuleName;
        private DevExpress.XtraGrid.Columns.GridColumn colDeductionDate;
        private DevExpress.XtraGrid.Columns.GridColumn colViolationDate;
        private DevExpress.XtraGrid.Columns.GridColumn colViolationType;
        private DevExpress.XtraGrid.Columns.GridColumn colViolationValue;
        private DevExpress.XtraGrid.Columns.GridColumn colDeductionMethod;
        private DevExpress.XtraGrid.Columns.GridColumn colDeductionValue;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colApprovedByUser;
        private DevExpress.XtraGrid.Columns.GridColumn colApprovalDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditItem;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditDelete;
    }
}