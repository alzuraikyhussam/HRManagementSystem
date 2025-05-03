namespace HR.UI.Forms.Attendance
{
    partial class AttendanceRecordsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttendanceRecordsForm));
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
            this.barButtonItemPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemExport = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDevices = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemImport = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupActions = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
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
            this.checkEditManualOnly = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButtonApplyFilter = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonClearFilter = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroupFilters = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemEmployee = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDepartment = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemStatus = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemManualOnly = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemApplyFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemClearFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemFilters = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControlAttendance = new DevExpress.XtraGrid.GridControl();
            this.attendanceRecordBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewAttendance = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAttendanceDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTimeIn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTimeOut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLateMinutes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEarlyDepartureMinutes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkedMinutes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOvertimeMinutes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsManualEntry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEdit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
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
            ((System.ComponentModel.ISupportInitialize)(this.checkEditManualOnly.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFilters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemManualOnly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemApplyFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClearFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemFilters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAttendance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceRecordBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAttendance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
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
            this.barButtonItemPrint,
            this.barButtonItemExport,
            this.barButtonItemAdd,
            this.barButtonItemEdit,
            this.barButtonItemDelete,
            this.barButtonItemDevices,
            this.barButtonItemImport});
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
            // barButtonItemPrint
            // 
            this.barButtonItemPrint.Caption = "طباعة";
            this.barButtonItemPrint.Id = 2;
            this.barButtonItemPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemPrint.ImageOptions.Image")));
            this.barButtonItemPrint.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemPrint.ImageOptions.LargeImage")));
            this.barButtonItemPrint.Name = "barButtonItemPrint";
            this.barButtonItemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemPrint_ItemClick);
            // 
            // barButtonItemExport
            // 
            this.barButtonItemExport.Caption = "تصدير";
            this.barButtonItemExport.Id = 3;
            this.barButtonItemExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemExport.ImageOptions.Image")));
            this.barButtonItemExport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemExport.ImageOptions.LargeImage")));
            this.barButtonItemExport.Name = "barButtonItemExport";
            this.barButtonItemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemExport_ItemClick);
            // 
            // barButtonItemAdd
            // 
            this.barButtonItemAdd.Caption = "إضافة سجل";
            this.barButtonItemAdd.Id = 4;
            this.barButtonItemAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemAdd.ImageOptions.Image")));
            this.barButtonItemAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemAdd.ImageOptions.LargeImage")));
            this.barButtonItemAdd.Name = "barButtonItemAdd";
            this.barButtonItemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAdd_ItemClick);
            // 
            // barButtonItemEdit
            // 
            this.barButtonItemEdit.Caption = "تعديل";
            this.barButtonItemEdit.Id = 5;
            this.barButtonItemEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemEdit.ImageOptions.Image")));
            this.barButtonItemEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemEdit.ImageOptions.LargeImage")));
            this.barButtonItemEdit.Name = "barButtonItemEdit";
            this.barButtonItemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemEdit_ItemClick);
            // 
            // barButtonItemDelete
            // 
            this.barButtonItemDelete.Caption = "حذف";
            this.barButtonItemDelete.Id = 6;
            this.barButtonItemDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemDelete.ImageOptions.Image")));
            this.barButtonItemDelete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemDelete.ImageOptions.LargeImage")));
            this.barButtonItemDelete.Name = "barButtonItemDelete";
            this.barButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDelete_ItemClick);
            // 
            // barButtonItemDevices
            // 
            this.barButtonItemDevices.Caption = "أجهزة البصمة";
            this.barButtonItemDevices.Id = 7;
            this.barButtonItemDevices.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemDevices.ImageOptions.Image")));
            this.barButtonItemDevices.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemDevices.ImageOptions.LargeImage")));
            this.barButtonItemDevices.Name = "barButtonItemDevices";
            this.barButtonItemDevices.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDevices_ItemClick);
            // 
            // barButtonItemImport
            // 
            this.barButtonItemImport.Caption = "استيراد";
            this.barButtonItemImport.Id = 8;
            this.barButtonItemImport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemImport.ImageOptions.Image")));
            this.barButtonItemImport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemImport.ImageOptions.LargeImage")));
            this.barButtonItemImport.Name = "barButtonItemImport";
            this.barButtonItemImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemImport_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupActions,
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
            // ribbonPageGroupTools
            // 
            this.ribbonPageGroupTools.ItemLinks.Add(this.barButtonItemPrint);
            this.ribbonPageGroupTools.ItemLinks.Add(this.barButtonItemExport);
            this.ribbonPageGroupTools.ItemLinks.Add(this.barButtonItemImport);
            this.ribbonPageGroupTools.ItemLinks.Add(this.barButtonItemDevices);
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
            this.layoutControl1.Root = this.Root;
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
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControlAttendance);
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
            this.layoutControl2.Root = this.layoutControlGroup3;
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
            this.layoutControlFilters.Controls.Add(this.checkEditManualOnly);
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
            this.dateEditFrom.Location = new System.Drawing.Point(12, 12);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Size = new System.Drawing.Size(164, 20);
            this.dateEditFrom.StyleController = this.layoutControlFilters;
            this.dateEditFrom.TabIndex = 4;
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dateEditTo.Location = new System.Drawing.Point(12, 36);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Size = new System.Drawing.Size(164, 20);
            this.dateEditTo.StyleController = this.layoutControlFilters;
            this.dateEditTo.TabIndex = 5;
            // 
            // lookUpEditEmployee
            // 
            this.lookUpEditEmployee.Location = new System.Drawing.Point(12, 60);
            this.lookUpEditEmployee.Name = "lookUpEditEmployee";
            this.lookUpEditEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditEmployee.Properties.NullText = "كل الموظفين";
            this.lookUpEditEmployee.Size = new System.Drawing.Size(164, 20);
            this.lookUpEditEmployee.StyleController = this.layoutControlFilters;
            this.lookUpEditEmployee.TabIndex = 6;
            // 
            // lookUpEditDepartment
            // 
            this.lookUpEditDepartment.Location = new System.Drawing.Point(12, 84);
            this.lookUpEditDepartment.Name = "lookUpEditDepartment";
            this.lookUpEditDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditDepartment.Properties.NullText = "كل الأقسام";
            this.lookUpEditDepartment.Size = new System.Drawing.Size(164, 20);
            this.lookUpEditDepartment.StyleController = this.layoutControlFilters;
            this.lookUpEditDepartment.TabIndex = 7;
            // 
            // comboBoxEditStatus
            // 
            this.comboBoxEditStatus.Location = new System.Drawing.Point(12, 108);
            this.comboBoxEditStatus.Name = "comboBoxEditStatus";
            this.comboBoxEditStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditStatus.Properties.Items.AddRange(new object[] {
            "الكل",
            "حاضر",
            "متأخر",
            "مغادرة مبكرة",
            "غائب",
            "إجازة"});
            this.comboBoxEditStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditStatus.Size = new System.Drawing.Size(164, 20);
            this.comboBoxEditStatus.StyleController = this.layoutControlFilters;
            this.comboBoxEditStatus.TabIndex = 8;
            // 
            // checkEditManualOnly
            // 
            this.checkEditManualOnly.Location = new System.Drawing.Point(12, 132);
            this.checkEditManualOnly.Name = "checkEditManualOnly";
            this.checkEditManualOnly.Properties.Caption = "الإدخالات اليدوية فقط";
            this.checkEditManualOnly.Size = new System.Drawing.Size(219, 20);
            this.checkEditManualOnly.StyleController = this.layoutControlFilters;
            this.checkEditManualOnly.TabIndex = 9;
            // 
            // simpleButtonApplyFilter
            // 
            this.simpleButtonApplyFilter.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonApplyFilter.ImageOptions.Image")));
            this.simpleButtonApplyFilter.Location = new System.Drawing.Point(12, 156);
            this.simpleButtonApplyFilter.Name = "simpleButtonApplyFilter";
            this.simpleButtonApplyFilter.Size = new System.Drawing.Size(107, 22);
            this.simpleButtonApplyFilter.StyleController = this.layoutControlFilters;
            this.simpleButtonApplyFilter.TabIndex = 10;
            this.simpleButtonApplyFilter.Text = "تطبيق";
            this.simpleButtonApplyFilter.Click += new System.EventHandler(this.simpleButtonApplyFilter_Click);
            // 
            // simpleButtonClearFilter
            // 
            this.simpleButtonClearFilter.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonClearFilter.ImageOptions.Image")));
            this.simpleButtonClearFilter.Location = new System.Drawing.Point(123, 156);
            this.simpleButtonClearFilter.Name = "simpleButtonClearFilter";
            this.simpleButtonClearFilter.Size = new System.Drawing.Size(108, 22);
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
            this.layoutControlItemFrom,
            this.layoutControlItemTo,
            this.layoutControlItemEmployee,
            this.layoutControlItemDepartment,
            this.layoutControlItemStatus,
            this.layoutControlItemManualOnly,
            this.layoutControlItemApplyFilter,
            this.layoutControlItemClearFilter,
            this.emptySpaceItemFilters});
            this.layoutControlGroupFilters.Name = "layoutControlGroupFilters";
            this.layoutControlGroupFilters.Size = new System.Drawing.Size(243, 351);
            this.layoutControlGroupFilters.TextVisible = false;
            // 
            // layoutControlItemFrom
            // 
            this.layoutControlItemFrom.Control = this.dateEditFrom;
            this.layoutControlItemFrom.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemFrom.Name = "layoutControlItemFrom";
            this.layoutControlItemFrom.Size = new System.Drawing.Size(223, 24);
            this.layoutControlItemFrom.Text = "من:";
            this.layoutControlItemFrom.TextSize = new System.Drawing.Size(43, 13);
            // 
            // layoutControlItemTo
            // 
            this.layoutControlItemTo.Control = this.dateEditTo;
            this.layoutControlItemTo.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemTo.Name = "layoutControlItemTo";
            this.layoutControlItemTo.Size = new System.Drawing.Size(223, 24);
            this.layoutControlItemTo.Text = "إلى:";
            this.layoutControlItemTo.TextSize = new System.Drawing.Size(43, 13);
            // 
            // layoutControlItemEmployee
            // 
            this.layoutControlItemEmployee.Control = this.lookUpEditEmployee;
            this.layoutControlItemEmployee.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemEmployee.Name = "layoutControlItemEmployee";
            this.layoutControlItemEmployee.Size = new System.Drawing.Size(223, 24);
            this.layoutControlItemEmployee.Text = "الموظف:";
            this.layoutControlItemEmployee.TextSize = new System.Drawing.Size(43, 13);
            // 
            // layoutControlItemDepartment
            // 
            this.layoutControlItemDepartment.Control = this.lookUpEditDepartment;
            this.layoutControlItemDepartment.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemDepartment.Name = "layoutControlItemDepartment";
            this.layoutControlItemDepartment.Size = new System.Drawing.Size(223, 24);
            this.layoutControlItemDepartment.Text = "القسم:";
            this.layoutControlItemDepartment.TextSize = new System.Drawing.Size(43, 13);
            // 
            // layoutControlItemStatus
            // 
            this.layoutControlItemStatus.Control = this.comboBoxEditStatus;
            this.layoutControlItemStatus.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemStatus.Name = "layoutControlItemStatus";
            this.layoutControlItemStatus.Size = new System.Drawing.Size(223, 24);
            this.layoutControlItemStatus.Text = "الحالة:";
            this.layoutControlItemStatus.TextSize = new System.Drawing.Size(43, 13);
            // 
            // layoutControlItemManualOnly
            // 
            this.layoutControlItemManualOnly.Control = this.checkEditManualOnly;
            this.layoutControlItemManualOnly.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItemManualOnly.Name = "layoutControlItemManualOnly";
            this.layoutControlItemManualOnly.Size = new System.Drawing.Size(223, 24);
            this.layoutControlItemManualOnly.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemManualOnly.TextVisible = false;
            // 
            // layoutControlItemApplyFilter
            // 
            this.layoutControlItemApplyFilter.Control = this.simpleButtonApplyFilter;
            this.layoutControlItemApplyFilter.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItemApplyFilter.Name = "layoutControlItemApplyFilter";
            this.layoutControlItemApplyFilter.Size = new System.Drawing.Size(111, 26);
            this.layoutControlItemApplyFilter.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemApplyFilter.TextVisible = false;
            // 
            // layoutControlItemClearFilter
            // 
            this.layoutControlItemClearFilter.Control = this.simpleButtonClearFilter;
            this.layoutControlItemClearFilter.Location = new System.Drawing.Point(111, 144);
            this.layoutControlItemClearFilter.Name = "layoutControlItemClearFilter";
            this.layoutControlItemClearFilter.Size = new System.Drawing.Size(112, 26);
            this.layoutControlItemClearFilter.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemClearFilter.TextVisible = false;
            // 
            // emptySpaceItemFilters
            // 
            this.emptySpaceItemFilters.AllowHotTrack = false;
            this.emptySpaceItemFilters.Location = new System.Drawing.Point(0, 170);
            this.emptySpaceItemFilters.Name = "emptySpaceItemFilters";
            this.emptySpaceItemFilters.Size = new System.Drawing.Size(223, 161);
            this.emptySpaceItemFilters.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(271, 400);
            this.layoutControlGroup3.TextVisible = false;
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
            // gridControlAttendance
            // 
            this.gridControlAttendance.DataSource = this.attendanceRecordBindingSource;
            this.gridControlAttendance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlAttendance.Location = new System.Drawing.Point(0, 0);
            this.gridControlAttendance.MainView = this.gridViewAttendance;
            this.gridControlAttendance.MenuManager = this.ribbon;
            this.gridControlAttendance.Name = "gridControlAttendance";
            this.gridControlAttendance.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.repositoryItemButtonEdit2});
            this.gridControlAttendance.Size = new System.Drawing.Size(898, 400);
            this.gridControlAttendance.TabIndex = 0;
            this.gridControlAttendance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAttendance});
            // 
            // attendanceRecordBindingSource
            // 
            this.attendanceRecordBindingSource.DataSource = typeof(HR.Models.AttendanceRecord);
            // 
            // gridViewAttendance
            // 
            this.gridViewAttendance.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colEmployeeID,
            this.colEmployeeName,
            this.colDepartmentName,
            this.colAttendanceDate,
            this.colTimeIn,
            this.colTimeOut,
            this.colLateMinutes,
            this.colEarlyDepartureMinutes,
            this.colWorkedMinutes,
            this.colOvertimeMinutes,
            this.colStatus,
            this.colIsManualEntry,
            this.colNotes,
            this.colEdit,
            this.colDelete});
            this.gridViewAttendance.GridControl = this.gridControlAttendance;
            this.gridViewAttendance.GroupCount = 1;
            this.gridViewAttendance.Name = "gridViewAttendance";
            this.gridViewAttendance.OptionsBehavior.ReadOnly = true;
            this.gridViewAttendance.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewAttendance.OptionsView.ShowAutoFilterRow = true;
            this.gridViewAttendance.OptionsView.ShowFooter = true;
            this.gridViewAttendance.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colAttendanceDate, DevExpress.Data.ColumnSortOrder.Descending)});
            // 
            // colID
            // 
            this.colID.Caption = "المعرف";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 54;
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
            this.colEmployeeName.Width = 109;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "القسم";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 2;
            this.colDepartmentName.Width = 109;
            // 
            // colAttendanceDate
            // 
            this.colAttendanceDate.Caption = "التاريخ";
            this.colAttendanceDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colAttendanceDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colAttendanceDate.FieldName = "AttendanceDate";
            this.colAttendanceDate.Name = "colAttendanceDate";
            this.colAttendanceDate.Visible = true;
            this.colAttendanceDate.VisibleIndex = 3;
            this.colAttendanceDate.Width = 109;
            // 
            // colTimeIn
            // 
            this.colTimeIn.Caption = "الدخول";
            this.colTimeIn.DisplayFormat.FormatString = "HH:mm:ss";
            this.colTimeIn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTimeIn.FieldName = "TimeIn";
            this.colTimeIn.Name = "colTimeIn";
            this.colTimeIn.Visible = true;
            this.colTimeIn.VisibleIndex = 3;
            this.colTimeIn.Width = 90;
            // 
            // colTimeOut
            // 
            this.colTimeOut.Caption = "الخروج";
            this.colTimeOut.DisplayFormat.FormatString = "HH:mm:ss";
            this.colTimeOut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTimeOut.FieldName = "TimeOut";
            this.colTimeOut.Name = "colTimeOut";
            this.colTimeOut.Visible = true;
            this.colTimeOut.VisibleIndex = 4;
            this.colTimeOut.Width = 90;
            // 
            // colLateMinutes
            // 
            this.colLateMinutes.Caption = "دقائق التأخير";
            this.colLateMinutes.FieldName = "LateMinutes";
            this.colLateMinutes.Name = "colLateMinutes";
            this.colLateMinutes.Visible = true;
            this.colLateMinutes.VisibleIndex = 5;
            this.colLateMinutes.Width = 73;
            // 
            // colEarlyDepartureMinutes
            // 
            this.colEarlyDepartureMinutes.Caption = "دقائق المغادرة المبكرة";
            this.colEarlyDepartureMinutes.FieldName = "EarlyDepartureMinutes";
            this.colEarlyDepartureMinutes.Name = "colEarlyDepartureMinutes";
            this.colEarlyDepartureMinutes.Visible = true;
            this.colEarlyDepartureMinutes.VisibleIndex = 6;
            this.colEarlyDepartureMinutes.Width = 83;
            // 
            // colWorkedMinutes
            // 
            this.colWorkedMinutes.Caption = "دقائق العمل";
            this.colWorkedMinutes.DisplayFormat.FormatString = "0.00 ساعة";
            this.colWorkedMinutes.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colWorkedMinutes.FieldName = "WorkedMinutes";
            this.colWorkedMinutes.Name = "colWorkedMinutes";
            this.colWorkedMinutes.Visible = true;
            this.colWorkedMinutes.VisibleIndex = 7;
            this.colWorkedMinutes.Width = 63;
            // 
            // colOvertimeMinutes
            // 
            this.colOvertimeMinutes.Caption = "دقائق العمل الإضافي";
            this.colOvertimeMinutes.FieldName = "OvertimeMinutes";
            this.colOvertimeMinutes.Name = "colOvertimeMinutes";
            this.colOvertimeMinutes.Visible = true;
            this.colOvertimeMinutes.VisibleIndex = 8;
            this.colOvertimeMinutes.Width = 60;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "الحالة";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 9;
            this.colStatus.Width = 50;
            // 
            // colIsManualEntry
            // 
            this.colIsManualEntry.Caption = "إدخال يدوي";
            this.colIsManualEntry.FieldName = "IsManualEntry";
            this.colIsManualEntry.Name = "colIsManualEntry";
            this.colIsManualEntry.Visible = true;
            this.colIsManualEntry.VisibleIndex = 10;
            this.colIsManualEntry.Width = 50;
            // 
            // colNotes
            // 
            this.colNotes.Caption = "ملاحظات";
            this.colNotes.FieldName = "Notes";
            this.colNotes.Name = "colNotes";
            this.colNotes.Visible = true;
            this.colNotes.VisibleIndex = 11;
            this.colNotes.Width = 149;
            // 
            // colEdit
            // 
            this.colEdit.Caption = "تعديل";
            this.colEdit.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colEdit.Name = "colEdit";
            this.colEdit.Visible = true;
            this.colEdit.VisibleIndex = 12;
            this.colEdit.Width = 50;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // colDelete
            // 
            this.colDelete.Caption = "حذف";
            this.colDelete.ColumnEdit = this.repositoryItemButtonEdit2;
            this.colDelete.Name = "colDelete";
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 13;
            this.colDelete.Width = 50;
            // 
            // repositoryItemButtonEdit2
            // 
            this.repositoryItemButtonEdit2.AutoHeight = false;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit2_ButtonClick);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1198, 424);
            this.Root.TextVisible = false;
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
            // AttendanceRecordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 606);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("AttendanceRecordsForm.IconOptions.Image")));
            this.Name = "AttendanceRecordsForm";
            this.Ribbon = this.ribbon;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "سجلات الحضور والانصراف";
            this.Load += new System.EventHandler(this.AttendanceRecordsForm_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.checkEditManualOnly.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFilters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemManualOnly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemApplyFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClearFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemFilters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAttendance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceRecordBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAttendance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupActions;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRefresh;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPrint;
        private DevExpress.XtraBars.BarButtonItem barButtonItemExport;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAdd;
        private DevExpress.XtraBars.BarButtonItem barButtonItemEdit;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDevices;
        private DevExpress.XtraBars.BarButtonItem barButtonItemImport;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupTools;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraGrid.GridControl gridControlAttendance;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAttendance;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControl layoutControlFilters;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraEditors.DateEdit dateEditTo;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditEmployee;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditDepartment;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditStatus;
        private DevExpress.XtraEditors.CheckEdit checkEditManualOnly;
        private DevExpress.XtraEditors.SimpleButton simpleButtonApplyFilter;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClearFilter;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupFilters;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFrom;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemEmployee;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDepartment;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemManualOnly;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemApplyFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemClearFilter;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemFilters;
        private System.Windows.Forms.BindingSource attendanceRecordBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colAttendanceDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeIn;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeOut;
        private DevExpress.XtraGrid.Columns.GridColumn colLateMinutes;
        private DevExpress.XtraGrid.Columns.GridColumn colEarlyDepartureMinutes;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkedMinutes;
        private DevExpress.XtraGrid.Columns.GridColumn colOvertimeMinutes;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colIsManualEntry;
        private DevExpress.XtraGrid.Columns.GridColumn colNotes;
        private DevExpress.XtraGrid.Columns.GridColumn colEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
    }
}