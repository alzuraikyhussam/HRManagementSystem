namespace HR.UI.Forms.Settings
{
    partial class BackupRestoreForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupRestoreForm));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItemCreateBackup = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemRestore = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSchedule = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.gridControlBackups = new DevExpress.XtraGrid.GridControl();
            this.backupInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewBackups = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreationDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFormattedSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.progressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.timerProgress = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBackups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backupInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBackups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barButtonItemCreateBackup,
            this.barButtonItemRestore,
            this.barButtonItemDelete,
            this.barButtonItemRefresh,
            this.barButtonItemSchedule});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 6;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(800, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barButtonItemCreateBackup
            // 
            this.barButtonItemCreateBackup.Caption = "إنشاء نسخة";
            this.barButtonItemCreateBackup.Id = 1;
            this.barButtonItemCreateBackup.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemCreateBackup.ImageOptions.Image")));
            this.barButtonItemCreateBackup.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemCreateBackup.ImageOptions.LargeImage")));
            this.barButtonItemCreateBackup.Name = "barButtonItemCreateBackup";
            this.barButtonItemCreateBackup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCreateBackup_ItemClick);
            // 
            // barButtonItemRestore
            // 
            this.barButtonItemRestore.Caption = "استعادة نسخة";
            this.barButtonItemRestore.Id = 2;
            this.barButtonItemRestore.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemRestore.ImageOptions.Image")));
            this.barButtonItemRestore.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemRestore.ImageOptions.LargeImage")));
            this.barButtonItemRestore.Name = "barButtonItemRestore";
            this.barButtonItemRestore.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemRestore_ItemClick);
            // 
            // barButtonItemDelete
            // 
            this.barButtonItemDelete.Caption = "حذف نسخة";
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
            // barButtonItemSchedule
            // 
            this.barButtonItemSchedule.Caption = "جدولة النسخ التلقائي";
            this.barButtonItemSchedule.Id = 5;
            this.barButtonItemSchedule.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemSchedule.ImageOptions.Image")));
            this.barButtonItemSchedule.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemSchedule.ImageOptions.LargeImage")));
            this.barButtonItemSchedule.Name = "barButtonItemSchedule";
            this.barButtonItemSchedule.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSchedule_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "الرئيسية";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemCreateBackup);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemRestore);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemDelete);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemRefresh);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "إدارة النسخ الاحتياطية";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemSchedule);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "الجدولة";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 479);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(800, 24);
            // 
            // gridControlBackups
            // 
            this.gridControlBackups.DataSource = this.backupInfoBindingSource;
            this.gridControlBackups.Location = new System.Drawing.Point(12, 12);
            this.gridControlBackups.MainView = this.gridViewBackups;
            this.gridControlBackups.MenuManager = this.ribbon;
            this.gridControlBackups.Name = "gridControlBackups";
            this.gridControlBackups.Size = new System.Drawing.Size(776, 279);
            this.gridControlBackups.TabIndex = 2;
            this.gridControlBackups.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBackups});
            // 
            // gridViewBackups
            // 
            this.gridViewBackups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFileName,
            this.colCreationDate,
            this.colFormattedSize,
            this.colDescription});
            this.gridViewBackups.GridControl = this.gridControlBackups;
            this.gridViewBackups.Name = "gridViewBackups";
            this.gridViewBackups.OptionsView.ShowGroupPanel = false;
            this.gridViewBackups.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewBackups_RowClick);
            // 
            // colFileName
            // 
            this.colFileName.Caption = "اسم الملف";
            this.colFileName.FieldName = "FileName";
            this.colFileName.Name = "colFileName";
            this.colFileName.Visible = true;
            this.colFileName.VisibleIndex = 0;
            this.colFileName.Width = 200;
            // 
            // colCreationDate
            // 
            this.colCreationDate.Caption = "تاريخ الإنشاء";
            this.colCreationDate.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colCreationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreationDate.FieldName = "CreationDate";
            this.colCreationDate.Name = "colCreationDate";
            this.colCreationDate.Visible = true;
            this.colCreationDate.VisibleIndex = 1;
            this.colCreationDate.Width = 150;
            // 
            // colFormattedSize
            // 
            this.colFormattedSize.Caption = "الحجم";
            this.colFormattedSize.FieldName = "FormattedSize";
            this.colFormattedSize.Name = "colFormattedSize";
            this.colFormattedSize.Visible = true;
            this.colFormattedSize.VisibleIndex = 2;
            this.colFormattedSize.Width = 100;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "الوصف";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 3;
            this.colDescription.Width = 300;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.progressBarControl);
            this.layoutControl1.Controls.Add(this.gridControlBackups);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(800, 321);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // progressBarControl
            // 
            this.progressBarControl.Location = new System.Drawing.Point(12, 295);
            this.progressBarControl.MenuManager = this.ribbon;
            this.progressBarControl.Name = "progressBarControl";
            this.progressBarControl.Size = new System.Drawing.Size(776, 14);
            this.progressBarControl.StyleController = this.layoutControl1;
            this.progressBarControl.TabIndex = 3;
            this.progressBarControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(800, 321);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlBackups;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(780, 283);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.progressBarControl;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 283);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(780, 18);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // timerProgress
            // 
            this.timerProgress.Interval = 200;
            this.timerProgress.Tick += new System.EventHandler(this.timerProgress_Tick);
            // 
            // BackupRestoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 503);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "BackupRestoreForm";
            this.Ribbon = this.ribbon;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "إدارة النسخ الاحتياطي والاستعادة";
            this.Load += new System.EventHandler(this.BackupRestoreForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBackups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backupInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBackups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCreateBackup;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRestore;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRefresh;
        private DevExpress.XtraGrid.GridControl gridControlBackups;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBackups;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource backupInfoBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colFileName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreationDate;
        private DevExpress.XtraGrid.Columns.GridColumn colFormattedSize;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSchedule;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.Timer timerProgress;
    }
}