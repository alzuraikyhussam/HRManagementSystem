namespace HR.UI.Forms.Deductions
{
    partial class DeductionRulesListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeductionRulesListForm));
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
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupActions = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupExport = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlRules = new DevExpress.XtraGrid.GridControl();
            this.deductionRuleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewRules = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeductionMethod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeductionValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAppliesTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPositionName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMinViolation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxViolation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActivationDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEdit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deductionRuleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRules)).BeginInit();
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
            this.barButtonItemAdd,
            this.barButtonItemEdit,
            this.barButtonItemDelete,
            this.barButtonItemPrint,
            this.barButtonItemExport});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 7;
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
            this.barButtonItemAdd.Caption = "إضافة قاعدة";
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
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupActions,
            this.ribbonPageGroupExport});
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
            // ribbonPageGroupExport
            // 
            this.ribbonPageGroupExport.ItemLinks.Add(this.barButtonItemPrint);
            this.ribbonPageGroupExport.ItemLinks.Add(this.barButtonItemExport);
            this.ribbonPageGroupExport.Name = "ribbonPageGroupExport";
            this.ribbonPageGroupExport.Text = "طباعة وتصدير";
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
            this.layoutControl1.Controls.Add(this.gridControlRules);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1198, 424);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControlRules
            // 
            this.gridControlRules.DataSource = this.deductionRuleBindingSource;
            this.gridControlRules.Location = new System.Drawing.Point(12, 12);
            this.gridControlRules.MainView = this.gridViewRules;
            this.gridControlRules.MenuManager = this.ribbon;
            this.gridControlRules.Name = "gridControlRules";
            this.gridControlRules.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.repositoryItemButtonEdit2});
            this.gridControlRules.Size = new System.Drawing.Size(1174, 400);
            this.gridControlRules.TabIndex = 4;
            this.gridControlRules.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRules});
            // 
            // deductionRuleBindingSource
            // 
            this.deductionRuleBindingSource.DataSource = typeof(HR.Models.DeductionRule);
            // 
            // gridViewRules
            // 
            this.gridViewRules.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colName,
            this.colDescription,
            this.colType,
            this.colDeductionMethod,
            this.colDeductionValue,
            this.colAppliesTo,
            this.colDepartmentName,
            this.colPositionName,
            this.colMinViolation,
            this.colMaxViolation,
            this.colActivationDate,
            this.colIsActive,
            this.colEdit,
            this.colDelete});
            this.gridViewRules.GridControl = this.gridControlRules;
            this.gridViewRules.Name = "gridViewRules";
            this.gridViewRules.OptionsBehavior.ReadOnly = true;
            this.gridViewRules.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewRules.OptionsView.ShowAutoFilterRow = true;
            this.gridViewRules.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.Caption = "المعرف";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 25;
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 94;
            // 
            // colName
            // 
            this.colName.Caption = "اسم القاعدة";
            this.colName.FieldName = "Name";
            this.colName.MinWidth = 25;
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 94;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "الوصف";
            this.colDescription.FieldName = "Description";
            this.colDescription.MinWidth = 25;
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 2;
            this.colDescription.Width = 94;
            // 
            // colType
            // 
            this.colType.Caption = "نوع المخالفة";
            this.colType.FieldName = "Type";
            this.colType.MinWidth = 25;
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 3;
            this.colType.Width = 94;
            // 
            // colDeductionMethod
            // 
            this.colDeductionMethod.Caption = "طريقة الخصم";
            this.colDeductionMethod.FieldName = "DeductionMethod";
            this.colDeductionMethod.MinWidth = 25;
            this.colDeductionMethod.Name = "colDeductionMethod";
            this.colDeductionMethod.Visible = true;
            this.colDeductionMethod.VisibleIndex = 4;
            this.colDeductionMethod.Width = 94;
            // 
            // colDeductionValue
            // 
            this.colDeductionValue.Caption = "قيمة الخصم";
            this.colDeductionValue.FieldName = "DeductionValue";
            this.colDeductionValue.MinWidth = 25;
            this.colDeductionValue.Name = "colDeductionValue";
            this.colDeductionValue.Visible = true;
            this.colDeductionValue.VisibleIndex = 5;
            this.colDeductionValue.Width = 94;
            // 
            // colAppliesTo
            // 
            this.colAppliesTo.Caption = "تطبق على";
            this.colAppliesTo.FieldName = "AppliesTo";
            this.colAppliesTo.MinWidth = 25;
            this.colAppliesTo.Name = "colAppliesTo";
            this.colAppliesTo.Visible = true;
            this.colAppliesTo.VisibleIndex = 6;
            this.colAppliesTo.Width = 94;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "القسم";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.MinWidth = 25;
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 7;
            this.colDepartmentName.Width = 94;
            // 
            // colPositionName
            // 
            this.colPositionName.Caption = "المنصب";
            this.colPositionName.FieldName = "PositionName";
            this.colPositionName.MinWidth = 25;
            this.colPositionName.Name = "colPositionName";
            this.colPositionName.Visible = true;
            this.colPositionName.VisibleIndex = 8;
            this.colPositionName.Width = 94;
            // 
            // colMinViolation
            // 
            this.colMinViolation.Caption = "الحد الأدنى للمخالفة";
            this.colMinViolation.FieldName = "MinViolation";
            this.colMinViolation.MinWidth = 25;
            this.colMinViolation.Name = "colMinViolation";
            this.colMinViolation.Visible = true;
            this.colMinViolation.VisibleIndex = 9;
            this.colMinViolation.Width = 94;
            // 
            // colMaxViolation
            // 
            this.colMaxViolation.Caption = "الحد الأقصى للمخالفة";
            this.colMaxViolation.FieldName = "MaxViolation";
            this.colMaxViolation.MinWidth = 25;
            this.colMaxViolation.Name = "colMaxViolation";
            this.colMaxViolation.Visible = true;
            this.colMaxViolation.VisibleIndex = 10;
            this.colMaxViolation.Width = 94;
            // 
            // colActivationDate
            // 
            this.colActivationDate.Caption = "تاريخ التفعيل";
            this.colActivationDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colActivationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colActivationDate.FieldName = "ActivationDate";
            this.colActivationDate.MinWidth = 25;
            this.colActivationDate.Name = "colActivationDate";
            this.colActivationDate.Visible = true;
            this.colActivationDate.VisibleIndex = 11;
            this.colActivationDate.Width = 94;
            // 
            // colIsActive
            // 
            this.colIsActive.Caption = "نشط";
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.MinWidth = 25;
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.Visible = true;
            this.colIsActive.VisibleIndex = 12;
            this.colIsActive.Width = 94;
            // 
            // colEdit
            // 
            this.colEdit.Caption = "تعديل";
            this.colEdit.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colEdit.MinWidth = 25;
            this.colEdit.Name = "colEdit";
            this.colEdit.Visible = true;
            this.colEdit.VisibleIndex = 13;
            this.colEdit.Width = 94;
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
            this.colDelete.MinWidth = 25;
            this.colDelete.Name = "colDelete";
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 14;
            this.colDelete.Width = 94;
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
            this.layoutControlItem1.Control = this.gridControlRules;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1178, 404);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // DeductionRulesListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 606);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("DeductionRulesListForm.IconOptions.Image")));
            this.Name = "DeductionRulesListForm";
            this.Ribbon = this.ribbon;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "قواعد الخصم";
            this.Load += new System.EventHandler(this.DeductionRulesListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deductionRuleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRules)).EndInit();
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
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gridControlRules;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRules;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRefresh;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAdd;
        private DevExpress.XtraBars.BarButtonItem barButtonItemEdit;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPrint;
        private DevExpress.XtraBars.BarButtonItem barButtonItemExport;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupExport;
        private System.Windows.Forms.BindingSource deductionRuleBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colDeductionMethod;
        private DevExpress.XtraGrid.Columns.GridColumn colDeductionValue;
        private DevExpress.XtraGrid.Columns.GridColumn colAppliesTo;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colPositionName;
        private DevExpress.XtraGrid.Columns.GridColumn colMinViolation;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxViolation;
        private DevExpress.XtraGrid.Columns.GridColumn colActivationDate;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
        private DevExpress.XtraGrid.Columns.GridColumn colEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
    }
}