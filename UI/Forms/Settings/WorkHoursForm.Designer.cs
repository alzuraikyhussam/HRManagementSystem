namespace HR.UI.Forms.Settings
{
    partial class WorkHoursForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkHoursForm));
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControlWorkHours = new DevExpress.XtraGrid.GridControl();
            this.gridViewWorkHours = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.spinEditOverTimeStartMinutes = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.spinEditShortDayThresholdMinutes = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.spinEditLateThresholdMinutes = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.spinEditFlexibleMinutes = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.memoEditDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.textEditTotalHours = new DevExpress.XtraEditors.TextEdit();
            this.timeEditEndTime = new DevExpress.XtraEditors.TimeEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.timeEditStartTime = new DevExpress.XtraEditors.TimeEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.textEditID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlWorkHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWorkHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditOverTimeStartMinutes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditShortDayThresholdMinutes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditLateThresholdMinutes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditFlexibleMinutes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTotalHours.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.ribbonControl.SearchEditItem,
            this.barButtonItemAdd,
            this.barButtonItemEdit,
            this.barButtonItemDelete,
            this.barButtonItemSave,
            this.barButtonItemCancel,
            this.barButtonItemRefresh});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 7;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ribbonControl.Size = new System.Drawing.Size(864, 158);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
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
            // barButtonItemSave
            // 
            this.barButtonItemSave.Caption = "حفظ";
            this.barButtonItemSave.Id = 4;
            this.barButtonItemSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemSave.ImageOptions.Image")));
            this.barButtonItemSave.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemSave.ImageOptions.LargeImage")));
            this.barButtonItemSave.Name = "barButtonItemSave";
            this.barButtonItemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSave_ItemClick);
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
            // barButtonItemRefresh
            // 
            this.barButtonItemRefresh.Caption = "تحديث";
            this.barButtonItemRefresh.Id = 6;
            this.barButtonItemRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemRefresh.ImageOptions.Image")));
            this.barButtonItemRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemRefresh.ImageOptions.LargeImage")));
            this.barButtonItemRefresh.Name = "barButtonItemRefresh";
            this.barButtonItemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemRefresh_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "فترات العمل";
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
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemSave);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemCancel);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "حفظ";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 568);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ribbonStatusBar.Size = new System.Drawing.Size(864, 24);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 158);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControlWorkHours);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainerControl1.Size = new System.Drawing.Size(864, 410);
            this.splitContainerControl1.SplitterPosition = 471;
            this.splitContainerControl1.TabIndex = 2;
            // 
            // gridControlWorkHours
            // 
            this.gridControlWorkHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlWorkHours.Location = new System.Drawing.Point(0, 0);
            this.gridControlWorkHours.MainView = this.gridViewWorkHours;
            this.gridControlWorkHours.MenuManager = this.ribbonControl;
            this.gridControlWorkHours.Name = "gridControlWorkHours";
            this.gridControlWorkHours.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gridControlWorkHours.Size = new System.Drawing.Size(471, 410);
            this.gridControlWorkHours.TabIndex = 0;
            this.gridControlWorkHours.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewWorkHours});
            // 
            // gridViewWorkHours
            // 
            this.gridViewWorkHours.GridControl = this.gridControlWorkHours;
            this.gridViewWorkHours.Name = "gridViewWorkHours";
            this.gridViewWorkHours.OptionsBehavior.Editable = false;
            this.gridViewWorkHours.OptionsBehavior.ReadOnly = true;
            this.gridViewWorkHours.OptionsView.ShowAutoFilterRow = true;
            this.gridViewWorkHours.OptionsView.ShowGroupPanel = false;
            this.gridViewWorkHours.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewWorkHours_RowClick);
            this.gridViewWorkHours.DoubleClick += new System.EventHandler(this.gridViewWorkHours_DoubleClick);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.spinEditOverTimeStartMinutes);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.spinEditShortDayThresholdMinutes);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.spinEditLateThresholdMinutes);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.spinEditFlexibleMinutes);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.memoEditDescription);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.textEditTotalHours);
            this.panelControl1.Controls.Add(this.timeEditEndTime);
            this.panelControl1.Controls.Add(this.labelControl10);
            this.panelControl1.Controls.Add(this.timeEditStartTime);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.textEditName);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.textEditID);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(388, 410);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(293, 273);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(83, 13);
            this.labelControl5.TabIndex = 19;
            this.labelControl5.Text = "بدء العمل الإضافي";
            // 
            // spinEditOverTimeStartMinutes
            // 
            this.spinEditOverTimeStartMinutes.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditOverTimeStartMinutes.Location = new System.Drawing.Point(65, 270);
            this.spinEditOverTimeStartMinutes.MenuManager = this.ribbonControl;
            this.spinEditOverTimeStartMinutes.Name = "spinEditOverTimeStartMinutes";
            this.spinEditOverTimeStartMinutes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditOverTimeStartMinutes.Properties.MaskSettings.Set("mask", "d");
            this.spinEditOverTimeStartMinutes.Size = new System.Drawing.Size(222, 20);
            this.spinEditOverTimeStartMinutes.TabIndex = 18;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(272, 247);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(104, 13);
            this.labelControl6.TabIndex = 17;
            this.labelControl6.Text = "الحد الأدنى للمغادرة المبكرة";
            // 
            // spinEditShortDayThresholdMinutes
            // 
            this.spinEditShortDayThresholdMinutes.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditShortDayThresholdMinutes.Location = new System.Drawing.Point(65, 244);
            this.spinEditShortDayThresholdMinutes.MenuManager = this.ribbonControl;
            this.spinEditShortDayThresholdMinutes.Name = "spinEditShortDayThresholdMinutes";
            this.spinEditShortDayThresholdMinutes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditShortDayThresholdMinutes.Properties.MaskSettings.Set("mask", "d");
            this.spinEditShortDayThresholdMinutes.Size = new System.Drawing.Size(222, 20);
            this.spinEditShortDayThresholdMinutes.TabIndex = 16;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(293, 221);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(83, 13);
            this.labelControl4.TabIndex = 15;
            this.labelControl4.Text = "الحد الأدنى للتأخير";
            // 
            // spinEditLateThresholdMinutes
            // 
            this.spinEditLateThresholdMinutes.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditLateThresholdMinutes.Location = new System.Drawing.Point(65, 218);
            this.spinEditLateThresholdMinutes.MenuManager = this.ribbonControl;
            this.spinEditLateThresholdMinutes.Name = "spinEditLateThresholdMinutes";
            this.spinEditLateThresholdMinutes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditLateThresholdMinutes.Properties.MaskSettings.Set("mask", "d");
            this.spinEditLateThresholdMinutes.Size = new System.Drawing.Size(222, 20);
            this.spinEditLateThresholdMinutes.TabIndex = 14;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(294, 195);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(82, 13);
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "دقائق السماح للتأخير";
            // 
            // spinEditFlexibleMinutes
            // 
            this.spinEditFlexibleMinutes.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditFlexibleMinutes.Location = new System.Drawing.Point(65, 192);
            this.spinEditFlexibleMinutes.MenuManager = this.ribbonControl;
            this.spinEditFlexibleMinutes.Name = "spinEditFlexibleMinutes";
            this.spinEditFlexibleMinutes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditFlexibleMinutes.Properties.MaskSettings.Set("mask", "d");
            this.spinEditFlexibleMinutes.Size = new System.Drawing.Size(222, 20);
            this.spinEditFlexibleMinutes.TabIndex = 12;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(322, 91);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 13);
            this.labelControl2.TabIndex = 11;
            this.labelControl2.Text = "وصف الفترة";
            // 
            // memoEditDescription
            // 
            this.memoEditDescription.Location = new System.Drawing.Point(65, 91);
            this.memoEditDescription.MenuManager = this.ribbonControl;
            this.memoEditDescription.Name = "memoEditDescription";
            this.memoEditDescription.Size = new System.Drawing.Size(222, 69);
            this.memoEditDescription.TabIndex = 10;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(319, 298);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(57, 13);
            this.labelControl7.TabIndex = 9;
            this.labelControl7.Text = "عدد الساعات";
            // 
            // textEditTotalHours
            // 
            this.textEditTotalHours.Location = new System.Drawing.Point(65, 295);
            this.textEditTotalHours.MenuManager = this.ribbonControl;
            this.textEditTotalHours.Name = "textEditTotalHours";
            this.textEditTotalHours.Properties.ReadOnly = true;
            this.textEditTotalHours.Size = new System.Drawing.Size(222, 20);
            this.textEditTotalHours.TabIndex = 8;
            // 
            // timeEditEndTime
            // 
            this.timeEditEndTime.EditValue = new System.DateTime(2023, 5, 3, 16, 0, 0, 0);
            this.timeEditEndTime.Location = new System.Drawing.Point(65, 166);
            this.timeEditEndTime.MenuManager = this.ribbonControl;
            this.timeEditEndTime.Name = "timeEditEndTime";
            this.timeEditEndTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEditEndTime.Size = new System.Drawing.Size(222, 20);
            this.timeEditEndTime.TabIndex = 7;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(316, 169);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(60, 13);
            this.labelControl10.TabIndex = 6;
            this.labelControl10.Text = "ساعة الانتهاء";
            // 
            // timeEditStartTime
            // 
            this.timeEditStartTime.EditValue = new System.DateTime(2023, 5, 3, 8, 0, 0, 0);
            this.timeEditStartTime.Location = new System.Drawing.Point(65, 166);
            this.timeEditStartTime.MenuManager = this.ribbonControl;
            this.timeEditStartTime.Name = "timeEditStartTime";
            this.timeEditStartTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEditStartTime.Size = new System.Drawing.Size(222, 20);
            this.timeEditStartTime.TabIndex = 5;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(326, 169);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(50, 13);
            this.labelControl9.TabIndex = 4;
            this.labelControl9.Text = "ساعة البدء";
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(65, 65);
            this.textEditName.MenuManager = this.ribbonControl;
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(222, 20);
            this.textEditName.TabIndex = 3;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(328, 68);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(48, 13);
            this.labelControl8.TabIndex = 2;
            this.labelControl8.Text = "اسم الفترة";
            // 
            // textEditID
            // 
            this.textEditID.Location = new System.Drawing.Point(65, 39);
            this.textEditID.MenuManager = this.ribbonControl;
            this.textEditID.Name = "textEditID";
            this.textEditID.Properties.ReadOnly = true;
            this.textEditID.Size = new System.Drawing.Size(222, 20);
            this.textEditID.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(336, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "رقم الفترة";
            // 
            // WorkHoursForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 592);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonControl);
            this.Name = "WorkHoursForm";
            this.Ribbon = this.ribbonControl;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "إدارة فترات العمل";
            this.Load += new System.EventHandler(this.WorkHoursForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlWorkHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWorkHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditOverTimeStartMinutes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditShortDayThresholdMinutes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditLateThresholdMinutes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditFlexibleMinutes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTotalHours.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditID.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAdd;
        private DevExpress.XtraBars.BarButtonItem barButtonItemEdit;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSave;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancel;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControlWorkHours;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewWorkHours;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit textEditID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TimeEdit timeEditEndTime;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TimeEdit timeEditStartTime;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRefresh;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit textEditTotalHours;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit memoEditDescription;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SpinEdit spinEditOverTimeStartMinutes;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SpinEdit spinEditShortDayThresholdMinutes;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit spinEditLateThresholdMinutes;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit spinEditFlexibleMinutes;
    }
}