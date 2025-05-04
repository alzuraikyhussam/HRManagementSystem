namespace HR.UI.Forms.Attendance
{
    partial class ZKTecoOperationsForm
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZKTecoOperationsForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControlDevice = new DevExpress.XtraEditors.GroupControl();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDeviceName = new DevExpress.XtraEditors.LabelControl();
            this.labelControlIPAddress = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlConnectionStatus = new DevExpress.XtraEditors.LabelControl();
            this.groupControlOperations = new DevExpress.XtraEditors.GroupControl();
            this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            this.simpleButtonConnect = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonDisconnect = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonImport = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonSyncUsers = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonEnroll = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonSyncTime = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.progressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
            this.labelControlStatus = new DevExpress.XtraEditors.LabelControl();
            this.groupControlEmployees = new DevExpress.XtraEditors.GroupControl();
            this.gridControlEmployees = new DevExpress.XtraGrid.GridControl();
            this.employeeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewEmployees = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colJobTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBiometricID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDevice)).BeginInit();
            this.groupControlDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlOperations)).BeginInit();
            this.groupControlOperations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
            this.tablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlEmployees)).BeginInit();
            this.groupControlEmployees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControlDevice);
            this.panelControl1.Controls.Add(this.groupControlOperations);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(984, 220);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControlDevice
            // 
            this.groupControlDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlDevice.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.groupControlDevice.AppearanceCaption.Options.UseFont = true;
            this.groupControlDevice.Controls.Add(this.tablePanel1);
            this.groupControlDevice.Location = new System.Drawing.Point(603, 5);
            this.groupControlDevice.Name = "groupControlDevice";
            this.groupControlDevice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupControlDevice.Size = new System.Drawing.Size(376, 156);
            this.groupControlDevice.TabIndex = 1;
            this.groupControlDevice.Text = "بيانات الجهاز";
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 40F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 60F)});
            this.tablePanel1.Controls.Add(this.labelControlTitle);
            this.tablePanel1.Controls.Add(this.labelControl1);
            this.tablePanel1.Controls.Add(this.labelControl2);
            this.tablePanel1.Controls.Add(this.labelControlDeviceName);
            this.tablePanel1.Controls.Add(this.labelControlIPAddress);
            this.tablePanel1.Controls.Add(this.labelControl5);
            this.tablePanel1.Controls.Add(this.labelControlConnectionStatus);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(2, 24);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F)});
            this.tablePanel1.Size = new System.Drawing.Size(372, 130);
            this.tablePanel1.TabIndex = 0;
            // 
            // labelControlTitle
            // 
            this.tablePanel1.SetColumn(this.labelControlTitle, 0);
            this.tablePanel1.SetColumnSpan(this.labelControlTitle, 2);
            this.labelControlTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelControlTitle.Appearance.Options.UseFont = true;
            this.labelControlTitle.Appearance.Options.UseTextOptions = true;
            this.labelControlTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlTitle.Location = new System.Drawing.Point(3, 3);
            this.labelControlTitle.Name = "labelControlTitle";
            this.tablePanel1.SetRow(this.labelControlTitle, 0);
            this.labelControlTitle.Size = new System.Drawing.Size(366, 34);
            this.labelControlTitle.TabIndex = 0;
            this.labelControlTitle.Text = "عمليات جهاز البصمة";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.tablePanel1.SetColumn(this.labelControl1, 0);
            this.labelControl1.Location = new System.Drawing.Point(3, 43);
            this.labelControl1.Name = "labelControl1";
            this.tablePanel1.SetRow(this.labelControl1, 1);
            this.labelControl1.Size = new System.Drawing.Size(142, 24);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "اسم الجهاز:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.tablePanel1.SetColumn(this.labelControl2, 0);
            this.labelControl2.Location = new System.Drawing.Point(3, 73);
            this.labelControl2.Name = "labelControl2";
            this.tablePanel1.SetRow(this.labelControl2, 2);
            this.labelControl2.Size = new System.Drawing.Size(142, 24);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "عنوان IP:";
            // 
            // labelControlDeviceName
            // 
            this.labelControlDeviceName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControlDeviceName.Appearance.Options.UseFont = true;
            this.labelControlDeviceName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.tablePanel1.SetColumn(this.labelControlDeviceName, 1);
            this.labelControlDeviceName.Location = new System.Drawing.Point(151, 43);
            this.labelControlDeviceName.Name = "labelControlDeviceName";
            this.tablePanel1.SetRow(this.labelControlDeviceName, 1);
            this.labelControlDeviceName.Size = new System.Drawing.Size(218, 24);
            this.labelControlDeviceName.TabIndex = 3;
            this.labelControlDeviceName.Text = "جهاز البصمة الرئيسي";
            // 
            // labelControlIPAddress
            // 
            this.labelControlIPAddress.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControlIPAddress.Appearance.Options.UseFont = true;
            this.labelControlIPAddress.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.tablePanel1.SetColumn(this.labelControlIPAddress, 1);
            this.labelControlIPAddress.Location = new System.Drawing.Point(151, 73);
            this.labelControlIPAddress.Name = "labelControlIPAddress";
            this.tablePanel1.SetRow(this.labelControlIPAddress, 2);
            this.labelControlIPAddress.Size = new System.Drawing.Size(218, 24);
            this.labelControlIPAddress.TabIndex = 4;
            this.labelControlIPAddress.Text = "192.168.1.201";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.tablePanel1.SetColumn(this.labelControl5, 0);
            this.labelControl5.Location = new System.Drawing.Point(3, 103);
            this.labelControl5.Name = "labelControl5";
            this.tablePanel1.SetRow(this.labelControl5, 3);
            this.labelControl5.Size = new System.Drawing.Size(142, 24);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "حالة الاتصال:";
            // 
            // labelControlConnectionStatus
            // 
            this.labelControlConnectionStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControlConnectionStatus.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControlConnectionStatus.Appearance.Options.UseFont = true;
            this.labelControlConnectionStatus.Appearance.Options.UseForeColor = true;
            this.labelControlConnectionStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.tablePanel1.SetColumn(this.labelControlConnectionStatus, 1);
            this.labelControlConnectionStatus.Location = new System.Drawing.Point(151, 103);
            this.labelControlConnectionStatus.Name = "labelControlConnectionStatus";
            this.tablePanel1.SetRow(this.labelControlConnectionStatus, 3);
            this.labelControlConnectionStatus.Size = new System.Drawing.Size(218, 24);
            this.labelControlConnectionStatus.TabIndex = 6;
            this.labelControlConnectionStatus.Text = "غير متصل";
            // 
            // groupControlOperations
            // 
            this.groupControlOperations.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.groupControlOperations.AppearanceCaption.Options.UseFont = true;
            this.groupControlOperations.Controls.Add(this.tablePanel2);
            this.groupControlOperations.Location = new System.Drawing.Point(5, 5);
            this.groupControlOperations.Name = "groupControlOperations";
            this.groupControlOperations.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupControlOperations.Size = new System.Drawing.Size(592, 156);
            this.groupControlOperations.TabIndex = 0;
            this.groupControlOperations.Text = "العمليات المتاحة";
            // 
            // tablePanel2
            // 
            this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.33F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.33F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.33F)});
            this.tablePanel2.Controls.Add(this.simpleButtonConnect);
            this.tablePanel2.Controls.Add(this.simpleButtonDisconnect);
            this.tablePanel2.Controls.Add(this.simpleButtonImport);
            this.tablePanel2.Controls.Add(this.simpleButtonSyncUsers);
            this.tablePanel2.Controls.Add(this.simpleButtonEnroll);
            this.tablePanel2.Controls.Add(this.simpleButtonSyncTime);
            this.tablePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel2.Location = new System.Drawing.Point(2, 24);
            this.tablePanel2.Name = "tablePanel2";
            this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 65F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 65F)});
            this.tablePanel2.Size = new System.Drawing.Size(588, 130);
            this.tablePanel2.TabIndex = 0;
            // 
            // simpleButtonConnect
            // 
            this.simpleButtonConnect.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonConnect.Appearance.Options.UseFont = true;
            this.tablePanel2.SetColumn(this.simpleButtonConnect, 2);
            this.simpleButtonConnect.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.simpleButtonConnect.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButtonConnect.ImageOptions.SvgImage")));
            this.simpleButtonConnect.Location = new System.Drawing.Point(3, 3);
            this.simpleButtonConnect.Name = "simpleButtonConnect";
            this.tablePanel2.SetRow(this.simpleButtonConnect, 0);
            this.simpleButtonConnect.Size = new System.Drawing.Size(190, 59);
            this.simpleButtonConnect.TabIndex = 0;
            this.simpleButtonConnect.Text = "اتصال بالجهاز";
            this.simpleButtonConnect.Click += new System.EventHandler(this.simpleButtonConnect_Click);
            // 
            // simpleButtonDisconnect
            // 
            this.simpleButtonDisconnect.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.simpleButtonDisconnect.Appearance.Options.UseFont = true;
            this.tablePanel2.SetColumn(this.simpleButtonDisconnect, 1);
            this.simpleButtonDisconnect.Enabled = false;
            this.simpleButtonDisconnect.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.simpleButtonDisconnect.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButtonDisconnect.ImageOptions.SvgImage")));
            this.simpleButtonDisconnect.Location = new System.Drawing.Point(199, 3);
            this.simpleButtonDisconnect.Name = "simpleButtonDisconnect";
            this.tablePanel2.SetRow(this.simpleButtonDisconnect, 0);
            this.simpleButtonDisconnect.Size = new System.Drawing.Size(190, 59);
            this.simpleButtonDisconnect.TabIndex = 1;
            this.simpleButtonDisconnect.Text = "قطع الاتصال";
            this.simpleButtonDisconnect.Click += new System.EventHandler(this.simpleButtonDisconnect_Click);
            // 
            // simpleButtonImport
            // 
            this.simpleButtonImport.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.simpleButtonImport.Appearance.Options.UseFont = true;
            this.tablePanel2.SetColumn(this.simpleButtonImport, 0);
            this.simpleButtonImport.Enabled = false;
            this.simpleButtonImport.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.simpleButtonImport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButtonImport.ImageOptions.SvgImage")));
            this.simpleButtonImport.Location = new System.Drawing.Point(395, 3);
            this.simpleButtonImport.Name = "simpleButtonImport";
            this.tablePanel2.SetRow(this.simpleButtonImport, 0);
            this.simpleButtonImport.Size = new System.Drawing.Size(190, 59);
            this.simpleButtonImport.TabIndex = 2;
            this.simpleButtonImport.Text = "استيراد سجلات الحضور";
            this.simpleButtonImport.Click += new System.EventHandler(this.simpleButtonImport_Click);
            // 
            // simpleButtonSyncUsers
            // 
            this.simpleButtonSyncUsers.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.simpleButtonSyncUsers.Appearance.Options.UseFont = true;
            this.tablePanel2.SetColumn(this.simpleButtonSyncUsers, 2);
            this.simpleButtonSyncUsers.Enabled = false;
            this.simpleButtonSyncUsers.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.simpleButtonSyncUsers.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButtonSyncUsers.ImageOptions.SvgImage")));
            this.simpleButtonSyncUsers.Location = new System.Drawing.Point(3, 68);
            this.simpleButtonSyncUsers.Name = "simpleButtonSyncUsers";
            this.tablePanel2.SetRow(this.simpleButtonSyncUsers, 1);
            this.simpleButtonSyncUsers.Size = new System.Drawing.Size(190, 59);
            this.simpleButtonSyncUsers.TabIndex = 3;
            this.simpleButtonSyncUsers.Text = "مزامنة المستخدمين";
            this.simpleButtonSyncUsers.Click += new System.EventHandler(this.simpleButtonSyncUsers_Click);
            // 
            // simpleButtonEnroll
            // 
            this.simpleButtonEnroll.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.simpleButtonEnroll.Appearance.Options.UseFont = true;
            this.tablePanel2.SetColumn(this.simpleButtonEnroll, 1);
            this.simpleButtonEnroll.Enabled = false;
            this.simpleButtonEnroll.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.simpleButtonEnroll.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButtonEnroll.ImageOptions.SvgImage")));
            this.simpleButtonEnroll.Location = new System.Drawing.Point(199, 68);
            this.simpleButtonEnroll.Name = "simpleButtonEnroll";
            this.tablePanel2.SetRow(this.simpleButtonEnroll, 1);
            this.simpleButtonEnroll.Size = new System.Drawing.Size(190, 59);
            this.simpleButtonEnroll.TabIndex = 4;
            this.simpleButtonEnroll.Text = "تسجيل بصمة موظف";
            this.simpleButtonEnroll.Click += new System.EventHandler(this.simpleButtonEnroll_Click);
            // 
            // simpleButtonSyncTime
            // 
            this.simpleButtonSyncTime.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.simpleButtonSyncTime.Appearance.Options.UseFont = true;
            this.tablePanel2.SetColumn(this.simpleButtonSyncTime, 0);
            this.simpleButtonSyncTime.Enabled = false;
            this.simpleButtonSyncTime.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.simpleButtonSyncTime.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButtonSyncTime.ImageOptions.SvgImage")));
            this.simpleButtonSyncTime.Location = new System.Drawing.Point(395, 68);
            this.simpleButtonSyncTime.Name = "simpleButtonSyncTime";
            this.tablePanel2.SetRow(this.simpleButtonSyncTime, 1);
            this.simpleButtonSyncTime.Size = new System.Drawing.Size(190, 59);
            this.simpleButtonSyncTime.TabIndex = 5;
            this.simpleButtonSyncTime.Text = "مزامنة وقت الجهاز";
            this.simpleButtonSyncTime.Click += new System.EventHandler(this.simpleButtonSyncTime_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.progressBarControl);
            this.panelControl2.Controls.Add(this.labelControlStatus);
            this.panelControl2.Location = new System.Drawing.Point(5, 167);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(974, 42);
            this.panelControl2.TabIndex = 2;
            // 
            // progressBarControl
            // 
            this.progressBarControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl.Location = new System.Drawing.Point(5, 25);
            this.progressBarControl.Name = "progressBarControl";
            this.progressBarControl.Size = new System.Drawing.Size(964, 12);
            this.progressBarControl.TabIndex = 1;
            this.progressBarControl.Visible = false;
            // 
            // labelControlStatus
            // 
            this.labelControlStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControlStatus.Appearance.Options.UseFont = true;
            this.labelControlStatus.Appearance.Options.UseTextOptions = true;
            this.labelControlStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlStatus.Location = new System.Drawing.Point(5, 5);
            this.labelControlStatus.Name = "labelControlStatus";
            this.labelControlStatus.Size = new System.Drawing.Size(964, 14);
            this.labelControlStatus.TabIndex = 0;
            this.labelControlStatus.Text = "جاهز";
            // 
            // groupControlEmployees
            // 
            this.groupControlEmployees.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.groupControlEmployees.AppearanceCaption.Options.UseFont = true;
            this.groupControlEmployees.Controls.Add(this.gridControlEmployees);
            this.groupControlEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlEmployees.Location = new System.Drawing.Point(0, 220);
            this.groupControlEmployees.Name = "groupControlEmployees";
            this.groupControlEmployees.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupControlEmployees.Size = new System.Drawing.Size(984, 341);
            this.groupControlEmployees.TabIndex = 1;
            this.groupControlEmployees.Text = "الموظفون";
            // 
            // gridControlEmployees
            // 
            this.gridControlEmployees.DataSource = this.employeeBindingSource;
            this.gridControlEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlEmployees.Location = new System.Drawing.Point(2, 24);
            this.gridControlEmployees.MainView = this.gridViewEmployees;
            this.gridControlEmployees.Name = "gridControlEmployees";
            this.gridControlEmployees.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControlEmployees.Size = new System.Drawing.Size(980, 315);
            this.gridControlEmployees.TabIndex = 0;
            this.gridControlEmployees.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewEmployees});
            // 
            // employeeBindingSource
            // 
            this.employeeBindingSource.DataSource = typeof(HR.Models.Employee);
            // 
            // gridViewEmployees
            // 
            this.gridViewEmployees.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colEmployeeCode,
            this.colFullName,
            this.colDepartmentName,
            this.colJobTitle,
            this.colBiometricID});
            this.gridViewEmployees.GridControl = this.gridControlEmployees;
            this.gridViewEmployees.Name = "gridViewEmployees";
            this.gridViewEmployees.OptionsBehavior.Editable = false;
            this.gridViewEmployees.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.Caption = "الرقم";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 52;
            // 
            // colEmployeeCode
            // 
            this.colEmployeeCode.Caption = "الرقم الوظيفي";
            this.colEmployeeCode.FieldName = "EmployeeCode";
            this.colEmployeeCode.Name = "colEmployeeCode";
            this.colEmployeeCode.OptionsColumn.AllowEdit = false;
            this.colEmployeeCode.Visible = true;
            this.colEmployeeCode.VisibleIndex = 1;
            this.colEmployeeCode.Width = 100;
            // 
            // colFullName
            // 
            this.colFullName.Caption = "اسم الموظف";
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsColumn.AllowEdit = false;
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 2;
            this.colFullName.Width = 180;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "القسم";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.OptionsColumn.AllowEdit = false;
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 3;
            this.colDepartmentName.Width = 130;
            // 
            // colJobTitle
            // 
            this.colJobTitle.Caption = "المسمى الوظيفي";
            this.colJobTitle.FieldName = "JobTitle";
            this.colJobTitle.Name = "colJobTitle";
            this.colJobTitle.OptionsColumn.AllowEdit = false;
            this.colJobTitle.Visible = true;
            this.colJobTitle.VisibleIndex = 4;
            this.colJobTitle.Width = 120;
            // 
            // colBiometricID
            // 
            this.colBiometricID.Caption = "رقم البصمة";
            this.colBiometricID.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colBiometricID.FieldName = "BiometricID";
            this.colBiometricID.Name = "colBiometricID";
            this.colBiometricID.OptionsColumn.AllowEdit = false;
            this.colBiometricID.Visible = true;
            this.colBiometricID.VisibleIndex = 5;
            this.colBiometricID.Width = 96;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            editorButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("editorButtonImageOptions1.SvgImage")));
            editorButtonImageOptions1.SvgImageSize = new System.Drawing.Size(16, 16);
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // ZKTecoOperationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.groupControlEmployees);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ZKTecoOperationsForm.IconOptions.SvgImage")));
            this.Name = "ZKTecoOperationsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "عمليات جهاز البصمة";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZKTecoOperationsForm_FormClosing);
            this.Load += new System.EventHandler(this.ZKTecoOperationsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDevice)).EndInit();
            this.groupControlDevice.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlOperations)).EndInit();
            this.groupControlOperations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
            this.tablePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlEmployees)).EndInit();
            this.groupControlEmployees.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControlOperations;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonConnect;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDisconnect;
        private DevExpress.XtraEditors.SimpleButton simpleButtonImport;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSyncUsers;
        private DevExpress.XtraEditors.SimpleButton simpleButtonEnroll;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSyncTime;
        private DevExpress.XtraEditors.GroupControl groupControlDevice;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.LabelControl labelControlTitle;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControlDeviceName;
        private DevExpress.XtraEditors.LabelControl labelControlIPAddress;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControlConnectionStatus;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl;
        private DevExpress.XtraEditors.LabelControl labelControlStatus;
        private DevExpress.XtraEditors.GroupControl groupControlEmployees;
        private DevExpress.XtraGrid.GridControl gridControlEmployees;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewEmployees;
        private System.Windows.Forms.BindingSource employeeBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colJobTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colBiometricID;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
    }
}