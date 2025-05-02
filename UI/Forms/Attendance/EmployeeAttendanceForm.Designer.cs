namespace HR.UI.Forms.Attendance
{
    partial class EmployeeAttendanceForm
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.dateStart = new DevExpress.XtraEditors.DateEdit();
            this.dateEnd = new DevExpress.XtraEditors.DateEdit();
            this.lblEmployeeNumber = new DevExpress.XtraEditors.LabelControl();
            this.lblEmployeeName = new DevExpress.XtraEditors.LabelControl();
            this.lblDepartment = new DevExpress.XtraEditors.LabelControl();
            this.lblPosition = new DevExpress.XtraEditors.LabelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.gridAttendance = new DevExpress.XtraGrid.GridControl();
            this.gridViewAttendance = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAttendanceDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTimeIn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTimeOut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkHoursName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLateMinutes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEarlyDepartureMinutes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOvertimeMinutes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkedMinutes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkedHours = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsManualEntryText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblTotalDays = new DevExpress.XtraEditors.LabelControl();
            this.lblPresentDays = new DevExpress.XtraEditors.LabelControl();
            this.lblLateDays = new DevExpress.XtraEditors.LabelControl();
            this.lblEarlyDepartureDays = new DevExpress.XtraEditors.LabelControl();
            this.lblAbsentDays = new DevExpress.XtraEditors.LabelControl();
            this.lblLeaveDays = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalWorkedHours = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalLateHours = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalEarlyDepartureHours = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalOvertimeHours = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAttendance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAttendance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateStart
            // 
            this.dateStart.EditValue = null;
            this.dateStart.Location = new System.Drawing.Point(102, 6);
            this.dateStart.Name = "dateStart";
            this.dateStart.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateStart.Properties.Appearance.Options.UseFont = true;
            this.dateStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateStart.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateStart.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateStart.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateStart.Size = new System.Drawing.Size(120, 20);
            this.dateStart.TabIndex = 0;
            this.dateStart.EditValueChanged += new System.EventHandler(this.DateFilter_ValueChanged);
            // 
            // dateEnd
            // 
            this.dateEnd.EditValue = null;
            this.dateEnd.Location = new System.Drawing.Point(322, 6);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEnd.Properties.Appearance.Options.UseFont = true;
            this.dateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEnd.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEnd.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEnd.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEnd.Size = new System.Drawing.Size(120, 20);
            this.dateEnd.TabIndex = 1;
            this.dateEnd.EditValueChanged += new System.EventHandler(this.DateFilter_ValueChanged);
            // 
            // lblEmployeeNumber
            // 
            this.lblEmployeeNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeNumber.Appearance.Options.UseFont = true;
            this.lblEmployeeNumber.Location = new System.Drawing.Point(116, 7);
            this.lblEmployeeNumber.Name = "lblEmployeeNumber";
            this.lblEmployeeNumber.Size = new System.Drawing.Size(16, 16);
            this.lblEmployeeNumber.TabIndex = 2;
            this.lblEmployeeNumber.Text = "---";
            // 
            // lblEmployeeName
            // 
            this.lblEmployeeName.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeName.Appearance.Options.UseFont = true;
            this.lblEmployeeName.Location = new System.Drawing.Point(116, 29);
            this.lblEmployeeName.Name = "lblEmployeeName";
            this.lblEmployeeName.Size = new System.Drawing.Size(16, 16);
            this.lblEmployeeName.TabIndex = 3;
            this.lblEmployeeName.Text = "---";
            // 
            // lblDepartment
            // 
            this.lblDepartment.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.Appearance.Options.UseFont = true;
            this.lblDepartment.Location = new System.Drawing.Point(368, 7);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(16, 16);
            this.lblDepartment.TabIndex = 4;
            this.lblDepartment.Text = "---";
            // 
            // lblPosition
            // 
            this.lblPosition.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosition.Appearance.Options.UseFont = true;
            this.lblPosition.Location = new System.Drawing.Point(368, 29);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(16, 16);
            this.lblPosition.TabIndex = 5;
            this.lblPosition.Text = "---";
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.Location = new System.Drawing.Point(452, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(59, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "بحث";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Location = new System.Drawing.Point(12, 109);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "إضافة";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.Location = new System.Drawing.Point(88, 109);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 23);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "تعديل";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Location = new System.Drawing.Point(164, 109);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 23);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "حذف";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.Location = new System.Drawing.Point(545, 109);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(70, 23);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "طباعة";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExport
            // 
            this.btnExport.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.Location = new System.Drawing.Point(621, 109);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(70, 23);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "تصدير";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // gridAttendance
            // 
            this.gridAttendance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            gridLevelNode1.RelationName = "Level1";
            this.gridAttendance.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridAttendance.Location = new System.Drawing.Point(12, 138);
            this.gridAttendance.MainView = this.gridViewAttendance;
            this.gridAttendance.Name = "gridAttendance";
            this.gridAttendance.Size = new System.Drawing.Size(891, 311);
            this.gridAttendance.TabIndex = 12;
            this.gridAttendance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAttendance});
            // 
            // gridViewAttendance
            // 
            this.gridViewAttendance.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridViewAttendance.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewAttendance.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F);
            this.gridViewAttendance.Appearance.Row.Options.UseFont = true;
            this.gridViewAttendance.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colAttendanceDate,
            this.colTimeIn,
            this.colTimeOut,
            this.colWorkHoursName,
            this.colLateMinutes,
            this.colEarlyDepartureMinutes,
            this.colOvertimeMinutes,
            this.colWorkedMinutes,
            this.colWorkedHours,
            this.colStatus,
            this.colIsManualEntryText,
            this.colNotes});
            this.gridViewAttendance.GridControl = this.gridAttendance;
            this.gridViewAttendance.Name = "gridViewAttendance";
            this.gridViewAttendance.OptionsBehavior.Editable = false;
            this.gridViewAttendance.OptionsBehavior.ReadOnly = true;
            this.gridViewAttendance.OptionsFind.AlwaysVisible = true;
            this.gridViewAttendance.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewAttendance.OptionsView.ShowAutoFilterRow = true;
            this.gridViewAttendance.OptionsView.ShowFooter = true;
            this.gridViewAttendance.OptionsView.ShowGroupPanel = false;
            this.gridViewAttendance.DoubleClick += new System.EventHandler(this.gridViewAttendance_DoubleClick);
            // 
            // colID
            // 
            this.colID.Caption = "الرقم";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.OptionsColumn.AllowFocus = false;
            this.colID.OptionsColumn.ReadOnly = true;
            // 
            // colAttendanceDate
            // 
            this.colAttendanceDate.Caption = "التاريخ";
            this.colAttendanceDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colAttendanceDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colAttendanceDate.FieldName = "AttendanceDate";
            this.colAttendanceDate.Name = "colAttendanceDate";
            this.colAttendanceDate.OptionsColumn.AllowEdit = false;
            this.colAttendanceDate.OptionsColumn.AllowFocus = false;
            this.colAttendanceDate.OptionsColumn.ReadOnly = true;
            this.colAttendanceDate.Visible = true;
            this.colAttendanceDate.VisibleIndex = 0;
            this.colAttendanceDate.Width = 80;
            // 
            // colTimeIn
            // 
            this.colTimeIn.Caption = "وقت الدخول";
            this.colTimeIn.DisplayFormat.FormatString = "HH:mm";
            this.colTimeIn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTimeIn.FieldName = "TimeIn";
            this.colTimeIn.Name = "colTimeIn";
            this.colTimeIn.OptionsColumn.AllowEdit = false;
            this.colTimeIn.OptionsColumn.AllowFocus = false;
            this.colTimeIn.OptionsColumn.ReadOnly = true;
            this.colTimeIn.Visible = true;
            this.colTimeIn.VisibleIndex = 1;
            this.colTimeIn.Width = 80;
            // 
            // colTimeOut
            // 
            this.colTimeOut.Caption = "وقت الخروج";
            this.colTimeOut.DisplayFormat.FormatString = "HH:mm";
            this.colTimeOut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTimeOut.FieldName = "TimeOut";
            this.colTimeOut.Name = "colTimeOut";
            this.colTimeOut.OptionsColumn.AllowEdit = false;
            this.colTimeOut.OptionsColumn.AllowFocus = false;
            this.colTimeOut.OptionsColumn.ReadOnly = true;
            this.colTimeOut.Visible = true;
            this.colTimeOut.VisibleIndex = 2;
            this.colTimeOut.Width = 80;
            // 
            // colWorkHoursName
            // 
            this.colWorkHoursName.Caption = "ساعات العمل";
            this.colWorkHoursName.FieldName = "WorkHoursName";
            this.colWorkHoursName.Name = "colWorkHoursName";
            this.colWorkHoursName.OptionsColumn.AllowEdit = false;
            this.colWorkHoursName.OptionsColumn.AllowFocus = false;
            this.colWorkHoursName.OptionsColumn.ReadOnly = true;
            this.colWorkHoursName.Visible = true;
            this.colWorkHoursName.VisibleIndex = 3;
            this.colWorkHoursName.Width = 100;
            // 
            // colLateMinutes
            // 
            this.colLateMinutes.Caption = "دقائق التأخير";
            this.colLateMinutes.FieldName = "LateMinutes";
            this.colLateMinutes.Name = "colLateMinutes";
            this.colLateMinutes.OptionsColumn.AllowEdit = false;
            this.colLateMinutes.OptionsColumn.AllowFocus = false;
            this.colLateMinutes.OptionsColumn.ReadOnly = true;
            this.colLateMinutes.Visible = true;
            this.colLateMinutes.VisibleIndex = 4;
            this.colLateMinutes.Width = 80;
            // 
            // colEarlyDepartureMinutes
            // 
            this.colEarlyDepartureMinutes.Caption = "دقائق المغادرة المبكرة";
            this.colEarlyDepartureMinutes.FieldName = "EarlyDepartureMinutes";
            this.colEarlyDepartureMinutes.Name = "colEarlyDepartureMinutes";
            this.colEarlyDepartureMinutes.OptionsColumn.AllowEdit = false;
            this.colEarlyDepartureMinutes.OptionsColumn.AllowFocus = false;
            this.colEarlyDepartureMinutes.OptionsColumn.ReadOnly = true;
            this.colEarlyDepartureMinutes.Visible = true;
            this.colEarlyDepartureMinutes.VisibleIndex = 5;
            this.colEarlyDepartureMinutes.Width = 120;
            // 
            // colOvertimeMinutes
            // 
            this.colOvertimeMinutes.Caption = "دقائق العمل الإضافي";
            this.colOvertimeMinutes.FieldName = "OvertimeMinutes";
            this.colOvertimeMinutes.Name = "colOvertimeMinutes";
            this.colOvertimeMinutes.OptionsColumn.AllowEdit = false;
            this.colOvertimeMinutes.OptionsColumn.AllowFocus = false;
            this.colOvertimeMinutes.OptionsColumn.ReadOnly = true;
            this.colOvertimeMinutes.Visible = true;
            this.colOvertimeMinutes.VisibleIndex = 6;
            this.colOvertimeMinutes.Width = 100;
            // 
            // colWorkedMinutes
            // 
            this.colWorkedMinutes.Caption = "دقائق العمل الكلية";
            this.colWorkedMinutes.FieldName = "WorkedMinutes";
            this.colWorkedMinutes.Name = "colWorkedMinutes";
            this.colWorkedMinutes.OptionsColumn.AllowEdit = false;
            this.colWorkedMinutes.OptionsColumn.AllowFocus = false;
            this.colWorkedMinutes.OptionsColumn.ReadOnly = true;
            this.colWorkedMinutes.Width = 100;
            // 
            // colWorkedHours
            // 
            this.colWorkedHours.Caption = "ساعات العمل";
            this.colWorkedHours.DisplayFormat.FormatString = "0.00";
            this.colWorkedHours.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWorkedHours.FieldName = "WorkedHours";
            this.colWorkedHours.Name = "colWorkedHours";
            this.colWorkedHours.OptionsColumn.AllowEdit = false;
            this.colWorkedHours.OptionsColumn.AllowFocus = false;
            this.colWorkedHours.OptionsColumn.ReadOnly = true;
            this.colWorkedHours.Visible = true;
            this.colWorkedHours.VisibleIndex = 7;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "الحالة";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsColumn.AllowFocus = false;
            this.colStatus.OptionsColumn.ReadOnly = true;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 8;
            // 
            // colIsManualEntryText
            // 
            this.colIsManualEntryText.Caption = "إدخال يدوي";
            this.colIsManualEntryText.FieldName = "IsManualEntryText";
            this.colIsManualEntryText.Name = "colIsManualEntryText";
            this.colIsManualEntryText.OptionsColumn.AllowEdit = false;
            this.colIsManualEntryText.OptionsColumn.AllowFocus = false;
            this.colIsManualEntryText.OptionsColumn.ReadOnly = true;
            this.colIsManualEntryText.Visible = true;
            this.colIsManualEntryText.VisibleIndex = 9;
            // 
            // colNotes
            // 
            this.colNotes.Caption = "ملاحظات";
            this.colNotes.FieldName = "Notes";
            this.colNotes.Name = "colNotes";
            this.colNotes.OptionsColumn.AllowEdit = false;
            this.colNotes.OptionsColumn.AllowFocus = false;
            this.colNotes.OptionsColumn.ReadOnly = true;
            this.colNotes.Visible = true;
            this.colNotes.VisibleIndex = 10;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.lblEmployeeNumber);
            this.panelControl1.Controls.Add(this.lblEmployeeName);
            this.panelControl1.Controls.Add(this.lblDepartment);
            this.panelControl1.Controls.Add(this.lblPosition);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(679, 52);
            this.panelControl1.TabIndex = 13;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(17, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 14);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "من تاريخ:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(244, 9);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(49, 14);
            this.labelControl2.TabIndex = 15;
            this.labelControl2.Text = "إلى تاريخ:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(21, 7);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(66, 16);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "رقم الموظف:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(21, 29);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(71, 16);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "اسم الموظف:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(307, 7);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(40, 16);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "القسم:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(307, 29);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(41, 16);
            this.labelControl6.TabIndex = 9;
            this.labelControl6.Text = "الوظيفة:";
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.labelControl19);
            this.panelControl2.Controls.Add(this.lblTotalOvertimeHours);
            this.panelControl2.Controls.Add(this.labelControl11);
            this.panelControl2.Controls.Add(this.lblTotalEarlyDepartureHours);
            this.panelControl2.Controls.Add(this.labelControl9);
            this.panelControl2.Controls.Add(this.lblTotalLateHours);
            this.panelControl2.Controls.Add(this.labelControl7);
            this.panelControl2.Controls.Add(this.lblTotalWorkedHours);
            this.panelControl2.Controls.Add(this.labelControl18);
            this.panelControl2.Controls.Add(this.labelControl17);
            this.panelControl2.Controls.Add(this.labelControl16);
            this.panelControl2.Controls.Add(this.labelControl15);
            this.panelControl2.Controls.Add(this.labelControl14);
            this.panelControl2.Controls.Add(this.labelControl13);
            this.panelControl2.Controls.Add(this.lblLeaveDays);
            this.panelControl2.Controls.Add(this.lblAbsentDays);
            this.panelControl2.Controls.Add(this.lblEarlyDepartureDays);
            this.panelControl2.Controls.Add(this.lblLateDays);
            this.panelControl2.Controls.Add(this.lblPresentDays);
            this.panelControl2.Controls.Add(this.lblTotalDays);
            this.panelControl2.Location = new System.Drawing.Point(697, 12);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(206, 120);
            this.panelControl2.TabIndex = 16;
            // 
            // lblTotalDays
            // 
            this.lblTotalDays.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDays.Appearance.Options.UseFont = true;
            this.lblTotalDays.Location = new System.Drawing.Point(15, 7);
            this.lblTotalDays.Name = "lblTotalDays";
            this.lblTotalDays.Size = new System.Drawing.Size(7, 13);
            this.lblTotalDays.TabIndex = 0;
            this.lblTotalDays.Text = "0";
            // 
            // lblPresentDays
            // 
            this.lblPresentDays.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresentDays.Appearance.Options.UseFont = true;
            this.lblPresentDays.Location = new System.Drawing.Point(15, 26);
            this.lblPresentDays.Name = "lblPresentDays";
            this.lblPresentDays.Size = new System.Drawing.Size(7, 13);
            this.lblPresentDays.TabIndex = 1;
            this.lblPresentDays.Text = "0";
            // 
            // lblLateDays
            // 
            this.lblLateDays.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLateDays.Appearance.Options.UseFont = true;
            this.lblLateDays.Location = new System.Drawing.Point(15, 45);
            this.lblLateDays.Name = "lblLateDays";
            this.lblLateDays.Size = new System.Drawing.Size(7, 13);
            this.lblLateDays.TabIndex = 2;
            this.lblLateDays.Text = "0";
            // 
            // lblEarlyDepartureDays
            // 
            this.lblEarlyDepartureDays.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEarlyDepartureDays.Appearance.Options.UseFont = true;
            this.lblEarlyDepartureDays.Location = new System.Drawing.Point(15, 64);
            this.lblEarlyDepartureDays.Name = "lblEarlyDepartureDays";
            this.lblEarlyDepartureDays.Size = new System.Drawing.Size(7, 13);
            this.lblEarlyDepartureDays.TabIndex = 3;
            this.lblEarlyDepartureDays.Text = "0";
            // 
            // lblAbsentDays
            // 
            this.lblAbsentDays.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbsentDays.Appearance.Options.UseFont = true;
            this.lblAbsentDays.Location = new System.Drawing.Point(15, 83);
            this.lblAbsentDays.Name = "lblAbsentDays";
            this.lblAbsentDays.Size = new System.Drawing.Size(7, 13);
            this.lblAbsentDays.TabIndex = 4;
            this.lblAbsentDays.Text = "0";
            // 
            // lblLeaveDays
            // 
            this.lblLeaveDays.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeaveDays.Appearance.Options.UseFont = true;
            this.lblLeaveDays.Location = new System.Drawing.Point(15, 102);
            this.lblLeaveDays.Name = "lblLeaveDays";
            this.lblLeaveDays.Size = new System.Drawing.Size(7, 13);
            this.lblLeaveDays.TabIndex = 5;
            this.lblLeaveDays.Text = "0";
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl13.Appearance.Options.UseFont = true;
            this.labelControl13.Location = new System.Drawing.Point(42, 7);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(61, 13);
            this.labelControl13.TabIndex = 6;
            this.labelControl13.Text = "إجمالي الأيام";
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl14.Appearance.Options.UseFont = true;
            this.labelControl14.Location = new System.Drawing.Point(42, 26);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(48, 13);
            this.labelControl14.TabIndex = 7;
            this.labelControl14.Text = "أيام الحضور";
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Location = new System.Drawing.Point(42, 45);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(45, 13);
            this.labelControl15.TabIndex = 8;
            this.labelControl15.Text = "أيام التأخير";
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Location = new System.Drawing.Point(42, 64);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(82, 13);
            this.labelControl16.TabIndex = 9;
            this.labelControl16.Text = "أيام المغادرة المبكرة";
            // 
            // labelControl17
            // 
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl17.Appearance.Options.UseFont = true;
            this.labelControl17.Location = new System.Drawing.Point(42, 83);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(41, 13);
            this.labelControl17.TabIndex = 10;
            this.labelControl17.Text = "أيام الغياب";
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl18.Appearance.Options.UseFont = true;
            this.labelControl18.Location = new System.Drawing.Point(42, 102);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(49, 13);
            this.labelControl18.TabIndex = 11;
            this.labelControl18.Text = "أيام الإجازات";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(147, 7);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(52, 13);
            this.labelControl7.TabIndex = 13;
            this.labelControl7.Text = "ساعات العمل";
            // 
            // lblTotalWorkedHours
            // 
            this.lblTotalWorkedHours.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalWorkedHours.Appearance.Options.UseFont = true;
            this.lblTotalWorkedHours.Location = new System.Drawing.Point(126, 7);
            this.lblTotalWorkedHours.Name = "lblTotalWorkedHours";
            this.lblTotalWorkedHours.Size = new System.Drawing.Size(15, 13);
            this.lblTotalWorkedHours.TabIndex = 12;
            this.lblTotalWorkedHours.Text = "0.0";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(147, 26);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(56, 13);
            this.labelControl9.TabIndex = 15;
            this.labelControl9.Text = "ساعات التأخير";
            // 
            // lblTotalLateHours
            // 
            this.lblTotalLateHours.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalLateHours.Appearance.Options.UseFont = true;
            this.lblTotalLateHours.Location = new System.Drawing.Point(126, 26);
            this.lblTotalLateHours.Name = "lblTotalLateHours";
            this.lblTotalLateHours.Size = new System.Drawing.Size(15, 13);
            this.lblTotalLateHours.TabIndex = 14;
            this.lblTotalLateHours.Text = "0.0";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Location = new System.Drawing.Point(147, 45);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(56, 13);
            this.labelControl11.TabIndex = 17;
            this.labelControl11.Text = "ساعات المغادرة";
            // 
            // lblTotalEarlyDepartureHours
            // 
            this.lblTotalEarlyDepartureHours.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalEarlyDepartureHours.Appearance.Options.UseFont = true;
            this.lblTotalEarlyDepartureHours.Location = new System.Drawing.Point(126, 45);
            this.lblTotalEarlyDepartureHours.Name = "lblTotalEarlyDepartureHours";
            this.lblTotalEarlyDepartureHours.Size = new System.Drawing.Size(15, 13);
            this.lblTotalEarlyDepartureHours.TabIndex = 16;
            this.lblTotalEarlyDepartureHours.Text = "0.0";
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Location = new System.Drawing.Point(147, 64);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(55, 13);
            this.labelControl19.TabIndex = 19;
            this.labelControl19.Text = "ساعات إضافية";
            // 
            // lblTotalOvertimeHours
            // 
            this.lblTotalOvertimeHours.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOvertimeHours.Appearance.Options.UseFont = true;
            this.lblTotalOvertimeHours.Location = new System.Drawing.Point(126, 64);
            this.lblTotalOvertimeHours.Name = "lblTotalOvertimeHours";
            this.lblTotalOvertimeHours.Size = new System.Drawing.Size(15, 13);
            this.lblTotalOvertimeHours.TabIndex = 18;
            this.lblTotalOvertimeHours.Text = "0.0";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.dateStart);
            this.panelControl3.Controls.Add(this.dateEnd);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.btnSearch);
            this.panelControl3.Location = new System.Drawing.Point(12, 70);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(516, 33);
            this.panelControl3.TabIndex = 17;
            // 
            // EmployeeAttendanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 461);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.gridAttendance);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Name = "EmployeeAttendanceForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "سجل حضور الموظف";
            this.Load += new System.EventHandler(this.EmployeeAttendanceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAttendance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAttendance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dateStart;
        private DevExpress.XtraEditors.DateEdit dateEnd;
        private DevExpress.XtraEditors.LabelControl lblEmployeeNumber;
        private DevExpress.XtraEditors.LabelControl lblEmployeeName;
        private DevExpress.XtraEditors.LabelControl lblDepartment;
        private DevExpress.XtraEditors.LabelControl lblPosition;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraGrid.GridControl gridAttendance;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAttendance;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LabelControl lblTotalOvertimeHours;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl lblTotalEarlyDepartureHours;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl lblTotalLateHours;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl lblTotalWorkedHours;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl lblLeaveDays;
        private DevExpress.XtraEditors.LabelControl lblAbsentDays;
        private DevExpress.XtraEditors.LabelControl lblEarlyDepartureDays;
        private DevExpress.XtraEditors.LabelControl lblLateDays;
        private DevExpress.XtraEditors.LabelControl lblPresentDays;
        private DevExpress.XtraEditors.LabelControl lblTotalDays;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colAttendanceDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeIn;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeOut;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkHoursName;
        private DevExpress.XtraGrid.Columns.GridColumn colLateMinutes;
        private DevExpress.XtraGrid.Columns.GridColumn colEarlyDepartureMinutes;
        private DevExpress.XtraGrid.Columns.GridColumn colOvertimeMinutes;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkedMinutes;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkedHours;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colIsManualEntryText;
        private DevExpress.XtraGrid.Columns.GridColumn colNotes;
        private DevExpress.XtraEditors.PanelControl panelControl3;
    }
}