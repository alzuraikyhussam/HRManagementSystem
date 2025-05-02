namespace HR.UI.Forms.Employees
{
    partial class EmployeeTransferForm
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
            this.lblTransferType = new DevExpress.XtraEditors.LabelControl();
            this.cmbTransferType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblEffectiveDate = new DevExpress.XtraEditors.LabelControl();
            this.dtEffectiveDate = new DevExpress.XtraEditors.DateEdit();
            this.groupFrom = new DevExpress.XtraEditors.GroupControl();
            this.lblFromDepartment = new DevExpress.XtraEditors.LabelControl();
            this.cmbFromDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.lblFromPosition = new DevExpress.XtraEditors.LabelControl();
            this.cmbFromPosition = new DevExpress.XtraEditors.LookUpEdit();
            this.groupTo = new DevExpress.XtraEditors.GroupControl();
            this.lblToDepartment = new DevExpress.XtraEditors.LabelControl();
            this.cmbToDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.lblToPosition = new DevExpress.XtraEditors.LabelControl();
            this.cmbToPosition = new DevExpress.XtraEditors.LookUpEdit();
            this.lblReason = new DevExpress.XtraEditors.LabelControl();
            this.txtReason = new DevExpress.XtraEditors.TextEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransferType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEffectiveDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEffectiveDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupFrom)).BeginInit();
            this.groupFrom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromPosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupTo)).BeginInit();
            this.groupTo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToPosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTransferType
            // 
            this.lblTransferType.Location = new System.Drawing.Point(12, 15);
            this.lblTransferType.Name = "lblTransferType";
            this.lblTransferType.Size = new System.Drawing.Size(66, 13);
            this.lblTransferType.TabIndex = 0;
            this.lblTransferType.Text = "نوع النقل/الترقية:";
            // 
            // cmbTransferType
            // 
            this.cmbTransferType.Location = new System.Drawing.Point(84, 12);
            this.cmbTransferType.Name = "cmbTransferType";
            this.cmbTransferType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTransferType.Size = new System.Drawing.Size(305, 20);
            this.cmbTransferType.TabIndex = 1;
            // 
            // lblEffectiveDate
            // 
            this.lblEffectiveDate.Location = new System.Drawing.Point(12, 41);
            this.lblEffectiveDate.Name = "lblEffectiveDate";
            this.lblEffectiveDate.Size = new System.Drawing.Size(62, 13);
            this.lblEffectiveDate.TabIndex = 2;
            this.lblEffectiveDate.Text = "تاريخ السريان:";
            // 
            // dtEffectiveDate
            // 
            this.dtEffectiveDate.EditValue = null;
            this.dtEffectiveDate.Location = new System.Drawing.Point(84, 38);
            this.dtEffectiveDate.Name = "dtEffectiveDate";
            this.dtEffectiveDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEffectiveDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEffectiveDate.Size = new System.Drawing.Size(305, 20);
            this.dtEffectiveDate.TabIndex = 3;
            // 
            // groupFrom
            // 
            this.groupFrom.Controls.Add(this.cmbFromPosition);
            this.groupFrom.Controls.Add(this.lblFromPosition);
            this.groupFrom.Controls.Add(this.cmbFromDepartment);
            this.groupFrom.Controls.Add(this.lblFromDepartment);
            this.groupFrom.Location = new System.Drawing.Point(12, 64);
            this.groupFrom.Name = "groupFrom";
            this.groupFrom.Size = new System.Drawing.Size(377, 100);
            this.groupFrom.TabIndex = 4;
            this.groupFrom.Text = "من";
            // 
            // lblFromDepartment
            // 
            this.lblFromDepartment.Location = new System.Drawing.Point(5, 26);
            this.lblFromDepartment.Name = "lblFromDepartment";
            this.lblFromDepartment.Size = new System.Drawing.Size(32, 13);
            this.lblFromDepartment.TabIndex = 0;
            this.lblFromDepartment.Text = "القسم:";
            // 
            // cmbFromDepartment
            // 
            this.cmbFromDepartment.Location = new System.Drawing.Point(72, 23);
            this.cmbFromDepartment.Name = "cmbFromDepartment";
            this.cmbFromDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFromDepartment.Properties.NullText = "";
            this.cmbFromDepartment.Size = new System.Drawing.Size(300, 20);
            this.cmbFromDepartment.TabIndex = 1;
            this.cmbFromDepartment.EditValueChanged += new System.EventHandler(this.cmbFromDepartment_EditValueChanged);
            // 
            // lblFromPosition
            // 
            this.lblFromPosition.Location = new System.Drawing.Point(5, 52);
            this.lblFromPosition.Name = "lblFromPosition";
            this.lblFromPosition.Size = new System.Drawing.Size(72, 13);
            this.lblFromPosition.TabIndex = 2;
            this.lblFromPosition.Text = "المسمى الوظيفي:";
            // 
            // cmbFromPosition
            // 
            this.cmbFromPosition.Location = new System.Drawing.Point(72, 49);
            this.cmbFromPosition.Name = "cmbFromPosition";
            this.cmbFromPosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFromPosition.Properties.NullText = "";
            this.cmbFromPosition.Size = new System.Drawing.Size(300, 20);
            this.cmbFromPosition.TabIndex = 3;
            // 
            // groupTo
            // 
            this.groupTo.Controls.Add(this.cmbToPosition);
            this.groupTo.Controls.Add(this.lblToPosition);
            this.groupTo.Controls.Add(this.cmbToDepartment);
            this.groupTo.Controls.Add(this.lblToDepartment);
            this.groupTo.Location = new System.Drawing.Point(12, 170);
            this.groupTo.Name = "groupTo";
            this.groupTo.Size = new System.Drawing.Size(377, 100);
            this.groupTo.TabIndex = 5;
            this.groupTo.Text = "إلى";
            // 
            // lblToDepartment
            // 
            this.lblToDepartment.Location = new System.Drawing.Point(5, 26);
            this.lblToDepartment.Name = "lblToDepartment";
            this.lblToDepartment.Size = new System.Drawing.Size(32, 13);
            this.lblToDepartment.TabIndex = 0;
            this.lblToDepartment.Text = "القسم:";
            // 
            // cmbToDepartment
            // 
            this.cmbToDepartment.Location = new System.Drawing.Point(72, 23);
            this.cmbToDepartment.Name = "cmbToDepartment";
            this.cmbToDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbToDepartment.Properties.NullText = "";
            this.cmbToDepartment.Size = new System.Drawing.Size(300, 20);
            this.cmbToDepartment.TabIndex = 1;
            this.cmbToDepartment.EditValueChanged += new System.EventHandler(this.cmbToDepartment_EditValueChanged);
            // 
            // lblToPosition
            // 
            this.lblToPosition.Location = new System.Drawing.Point(5, 52);
            this.lblToPosition.Name = "lblToPosition";
            this.lblToPosition.Size = new System.Drawing.Size(72, 13);
            this.lblToPosition.TabIndex = 2;
            this.lblToPosition.Text = "المسمى الوظيفي:";
            // 
            // cmbToPosition
            // 
            this.cmbToPosition.Location = new System.Drawing.Point(72, 49);
            this.cmbToPosition.Name = "cmbToPosition";
            this.cmbToPosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbToPosition.Properties.NullText = "";
            this.cmbToPosition.Size = new System.Drawing.Size(300, 20);
            this.cmbToPosition.TabIndex = 3;
            // 
            // lblReason
            // 
            this.lblReason.Location = new System.Drawing.Point(12, 276);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(31, 13);
            this.lblReason.TabIndex = 6;
            this.lblReason.Text = "السبب:";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(49, 273);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(340, 20);
            this.txtReason.TabIndex = 7;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(12, 302);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(49, 13);
            this.lblNotes.TabIndex = 8;
            this.lblNotes.Text = "ملاحظات:";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(12, 321);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(377, 66);
            this.txtNotes.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(314, 393);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "حفظ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(233, 393);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "إلغاء";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EmployeeTransferForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(401, 428);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.groupTo);
            this.Controls.Add(this.groupFrom);
            this.Controls.Add(this.dtEffectiveDate);
            this.Controls.Add(this.lblEffectiveDate);
            this.Controls.Add(this.cmbTransferType);
            this.Controls.Add(this.lblTransferType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeTransferForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "نقل/ترقية موظف";
            this.Load += new System.EventHandler(this.EmployeeTransferForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransferType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEffectiveDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEffectiveDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupFrom)).EndInit();
            this.groupFrom.ResumeLayout(false);
            this.groupFrom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromPosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupTo)).EndInit();
            this.groupTo.ResumeLayout(false);
            this.groupTo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToPosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTransferType;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTransferType;
        private DevExpress.XtraEditors.LabelControl lblEffectiveDate;
        private DevExpress.XtraEditors.DateEdit dtEffectiveDate;
        private DevExpress.XtraEditors.GroupControl groupFrom;
        private DevExpress.XtraEditors.LookUpEdit cmbFromPosition;
        private DevExpress.XtraEditors.LabelControl lblFromPosition;
        private DevExpress.XtraEditors.LookUpEdit cmbFromDepartment;
        private DevExpress.XtraEditors.LabelControl lblFromDepartment;
        private DevExpress.XtraEditors.GroupControl groupTo;
        private DevExpress.XtraEditors.LookUpEdit cmbToPosition;
        private DevExpress.XtraEditors.LabelControl lblToPosition;
        private DevExpress.XtraEditors.LookUpEdit cmbToDepartment;
        private DevExpress.XtraEditors.LabelControl lblToDepartment;
        private DevExpress.XtraEditors.LabelControl lblReason;
        private DevExpress.XtraEditors.TextEdit txtReason;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}