namespace HR.UI.Forms.Settings
{
    partial class WorkShiftsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkShiftsForm));
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
            this.gridControlWorkShifts = new DevExpress.XtraGrid.GridControl();
            this.gridViewWorkShifts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkEditSaturdayEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditFridayEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditThursdayEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditWednesdayEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditTuesdayEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditMondayEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditSundayEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditAllDays = new DevExpress.XtraEditors.CheckEdit();
            this.colorPickEditShiftColor = new DevExpress.XtraEditors.ColorPickEdit();
            this.checkEditIsActive = new DevExpress.XtraEditors.CheckEdit();
            this.lookUpEditWorkHours = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.memoEditDescription = new DevExpress.XtraEditors.MemoEdit();
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
            ((System.ComponentModel.ISupportInitialize)(this.gridControlWorkShifts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWorkShifts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSaturdayEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFridayEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditThursdayEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditWednesdayEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditTuesdayEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditMondayEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSundayEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAllDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditShiftColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditIsActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditWorkHours.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).BeginInit();
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
            this.ribbonControl.Size = new System.Drawing.Size(933, 158);
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
            this.ribbonPage1.Text = "ورديات العمل";
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
            this.ribbonStatusBar.Size = new System.Drawing.Size(933, 24);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 158);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControlWorkShifts);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainerControl1.Size = new System.Drawing.Size(933, 410);
            this.splitContainerControl1.SplitterPosition = 522;
            this.splitContainerControl1.TabIndex = 2;
            // 
            // gridControlWorkShifts
            // 
            this.gridControlWorkShifts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlWorkShifts.Location = new System.Drawing.Point(0, 0);
            this.gridControlWorkShifts.MainView = this.gridViewWorkShifts;
            this.gridControlWorkShifts.MenuManager = this.ribbonControl;
            this.gridControlWorkShifts.Name = "gridControlWorkShifts";
            this.gridControlWorkShifts.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gridControlWorkShifts.Size = new System.Drawing.Size(522, 410);
            this.gridControlWorkShifts.TabIndex = 0;
            this.gridControlWorkShifts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewWorkShifts});
            // 
            // gridViewWorkShifts
            // 
            this.gridViewWorkShifts.GridControl = this.gridControlWorkShifts;
            this.gridViewWorkShifts.Name = "gridViewWorkShifts";
            this.gridViewWorkShifts.OptionsBehavior.Editable = false;
            this.gridViewWorkShifts.OptionsBehavior.ReadOnly = true;
            this.gridViewWorkShifts.OptionsView.ShowAutoFilterRow = true;
            this.gridViewWorkShifts.OptionsView.ShowGroupPanel = false;
            this.gridViewWorkShifts.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewWorkShifts_RowClick);
            this.gridViewWorkShifts.DoubleClick += new System.EventHandler(this.gridViewWorkShifts_DoubleClick);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl11);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.colorPickEditShiftColor);
            this.panelControl1.Controls.Add(this.checkEditIsActive);
            this.panelControl1.Controls.Add(this.lookUpEditWorkHours);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.memoEditDescription);
            this.panelControl1.Controls.Add(this.textEditName);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.textEditID);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(406, 410);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(354, 259);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(38, 13);
            this.labelControl11.TabIndex = 14;
            this.labelControl11.Text = "لون الوردية";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.checkEditSaturdayEnabled);
            this.groupControl1.Controls.Add(this.checkEditFridayEnabled);
            this.groupControl1.Controls.Add(this.checkEditThursdayEnabled);
            this.groupControl1.Controls.Add(this.checkEditWednesdayEnabled);
            this.groupControl1.Controls.Add(this.checkEditTuesdayEnabled);
            this.groupControl1.Controls.Add(this.checkEditMondayEnabled);
            this.groupControl1.Controls.Add(this.checkEditSundayEnabled);
            this.groupControl1.Controls.Add(this.checkEditAllDays);
            this.groupControl1.Location = new System.Drawing.Point(8, 170);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupControl1.Size = new System.Drawing.Size(384, 83);
            this.groupControl1.TabIndex = 13;
            this.groupControl1.Text = "أيام الأسبوع";
            // 
            // checkEditSaturdayEnabled
            // 
            this.checkEditSaturdayEnabled.Location = new System.Drawing.Point(62, 53);
            this.checkEditSaturdayEnabled.MenuManager = this.ribbonControl;
            this.checkEditSaturdayEnabled.Name = "checkEditSaturdayEnabled";
            this.checkEditSaturdayEnabled.Properties.Caption = "السبت";
            this.checkEditSaturdayEnabled.Size = new System.Drawing.Size(52, 20);
            this.checkEditSaturdayEnabled.TabIndex = 7;
            // 
            // checkEditFridayEnabled
            // 
            this.checkEditFridayEnabled.Location = new System.Drawing.Point(121, 53);
            this.checkEditFridayEnabled.MenuManager = this.ribbonControl;
            this.checkEditFridayEnabled.Name = "checkEditFridayEnabled";
            this.checkEditFridayEnabled.Properties.Caption = "الجمعة";
            this.checkEditFridayEnabled.Size = new System.Drawing.Size(52, 20);
            this.checkEditFridayEnabled.TabIndex = 6;
            // 
            // checkEditThursdayEnabled
            // 
            this.checkEditThursdayEnabled.Location = new System.Drawing.Point(180, 53);
            this.checkEditThursdayEnabled.MenuManager = this.ribbonControl;
            this.checkEditThursdayEnabled.Name = "checkEditThursdayEnabled";
            this.checkEditThursdayEnabled.Properties.Caption = "الخميس";
            this.checkEditThursdayEnabled.Size = new System.Drawing.Size(59, 20);
            this.checkEditThursdayEnabled.TabIndex = 5;
            // 
            // checkEditWednesdayEnabled
            // 
            this.checkEditWednesdayEnabled.Location = new System.Drawing.Point(246, 53);
            this.checkEditWednesdayEnabled.MenuManager = this.ribbonControl;
            this.checkEditWednesdayEnabled.Name = "checkEditWednesdayEnabled";
            this.checkEditWednesdayEnabled.Properties.Caption = "الأربعاء";
            this.checkEditWednesdayEnabled.Size = new System.Drawing.Size(58, 20);
            this.checkEditWednesdayEnabled.TabIndex = 4;
            // 
            // checkEditTuesdayEnabled
            // 
            this.checkEditTuesdayEnabled.Location = new System.Drawing.Point(311, 53);
            this.checkEditTuesdayEnabled.MenuManager = this.ribbonControl;
            this.checkEditTuesdayEnabled.Name = "checkEditTuesdayEnabled";
            this.checkEditTuesdayEnabled.Properties.Caption = "الثلاثاء";
            this.checkEditTuesdayEnabled.Size = new System.Drawing.Size(58, 20);
            this.checkEditTuesdayEnabled.TabIndex = 3;
            // 
            // checkEditMondayEnabled
            // 
            this.checkEditMondayEnabled.Location = new System.Drawing.Point(121, 29);
            this.checkEditMondayEnabled.MenuManager = this.ribbonControl;
            this.checkEditMondayEnabled.Name = "checkEditMondayEnabled";
            this.checkEditMondayEnabled.Properties.Caption = "الإثنين";
            this.checkEditMondayEnabled.Size = new System.Drawing.Size(52, 20);
            this.checkEditMondayEnabled.TabIndex = 2;
            // 
            // checkEditSundayEnabled
            // 
            this.checkEditSundayEnabled.Location = new System.Drawing.Point(180, 29);
            this.checkEditSundayEnabled.MenuManager = this.ribbonControl;
            this.checkEditSundayEnabled.Name = "checkEditSundayEnabled";
            this.checkEditSundayEnabled.Properties.Caption = "الأحد";
            this.checkEditSundayEnabled.Size = new System.Drawing.Size(52, 20);
            this.checkEditSundayEnabled.TabIndex = 1;
            // 
            // checkEditAllDays
            // 
            this.checkEditAllDays.Location = new System.Drawing.Point(259, 29);
            this.checkEditAllDays.MenuManager = this.ribbonControl;
            this.checkEditAllDays.Name = "checkEditAllDays";
            this.checkEditAllDays.Properties.Caption = "جميع الأيام";
            this.checkEditAllDays.Size = new System.Drawing.Size(110, 20);
            this.checkEditAllDays.TabIndex = 0;
            this.checkEditAllDays.CheckedChanged += new System.EventHandler(this.checkEditAllDays_CheckedChanged);
            // 
            // colorPickEditShiftColor
            // 
            this.colorPickEditShiftColor.EditValue = System.Drawing.Color.White;
            this.colorPickEditShiftColor.Location = new System.Drawing.Point(73, 256);
            this.colorPickEditShiftColor.MenuManager = this.ribbonControl;
            this.colorPickEditShiftColor.Name = "colorPickEditShiftColor";
            this.colorPickEditShiftColor.Properties.AutomaticColor = System.Drawing.Color.White;
            this.colorPickEditShiftColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorPickEditShiftColor.Size = new System.Drawing.Size(264, 20);
            this.colorPickEditShiftColor.TabIndex = 12;
            // 
            // checkEditIsActive
            // 
            this.checkEditIsActive.Location = new System.Drawing.Point(73, 282);
            this.checkEditIsActive.MenuManager = this.ribbonControl;
            this.checkEditIsActive.Name = "checkEditIsActive";
            this.checkEditIsActive.Properties.Caption = "نشط";
            this.checkEditIsActive.Size = new System.Drawing.Size(75, 20);
            this.checkEditIsActive.TabIndex = 11;
            // 
            // lookUpEditWorkHours
            // 
            this.lookUpEditWorkHours.Location = new System.Drawing.Point(73, 144);
            this.lookUpEditWorkHours.MenuManager = this.ribbonControl;
            this.lookUpEditWorkHours.Name = "lookUpEditWorkHours";
            this.lookUpEditWorkHours.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditWorkHours.Properties.NullText = "اختر فترة العمل";
            this.lookUpEditWorkHours.Size = new System.Drawing.Size(264, 20);
            this.lookUpEditWorkHours.TabIndex = 10;
            this.lookUpEditWorkHours.EditValueChanged += new System.EventHandler(this.lookUpEditWorkHours_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(346, 147);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(46, 13);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "فترة العمل";
            // 
            // memoEditDescription
            // 
            this.memoEditDescription.Location = new System.Drawing.Point(73, 91);
            this.memoEditDescription.MenuManager = this.ribbonControl;
            this.memoEditDescription.Name = "memoEditDescription";
            this.memoEditDescription.Size = new System.Drawing.Size(264, 47);
            this.memoEditDescription.TabIndex = 4;
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(73, 65);
            this.textEditName.MenuManager = this.ribbonControl;
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(264, 20);
            this.textEditName.TabIndex = 3;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(347, 68);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(45, 13);
            this.labelControl8.TabIndex = 2;
            this.labelControl8.Text = "اسم الوردية";
            // 
            // textEditID
            // 
            this.textEditID.Location = new System.Drawing.Point(73, 39);
            this.textEditID.MenuManager = this.ribbonControl;
            this.textEditID.Name = "textEditID";
            this.textEditID.Properties.ReadOnly = true;
            this.textEditID.Size = new System.Drawing.Size(264, 20);
            this.textEditID.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(343, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "رقم الوردية";
            // 
            // WorkShiftsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 592);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonControl);
            this.Name = "WorkShiftsForm";
            this.Ribbon = this.ribbonControl;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "إدارة ورديات العمل";
            this.Load += new System.EventHandler(this.WorkShiftsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlWorkShifts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWorkShifts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSaturdayEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditFridayEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditThursdayEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditWednesdayEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditTuesdayEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditMondayEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSundayEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAllDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditShiftColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditIsActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditWorkHours.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gridControlWorkShifts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewWorkShifts;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit textEditID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRefresh;
        private DevExpress.XtraEditors.MemoEdit memoEditDescription;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditWorkHours;
        private DevExpress.XtraEditors.CheckEdit checkEditIsActive;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEditShiftColor;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit checkEditSaturdayEnabled;
        private DevExpress.XtraEditors.CheckEdit checkEditFridayEnabled;
        private DevExpress.XtraEditors.CheckEdit checkEditThursdayEnabled;
        private DevExpress.XtraEditors.CheckEdit checkEditWednesdayEnabled;
        private DevExpress.XtraEditors.CheckEdit checkEditTuesdayEnabled;
        private DevExpress.XtraEditors.CheckEdit checkEditMondayEnabled;
        private DevExpress.XtraEditors.CheckEdit checkEditSundayEnabled;
        private DevExpress.XtraEditors.CheckEdit checkEditAllDays;
        private DevExpress.XtraEditors.LabelControl labelControl11;
    }
}