namespace HR.UI.Forms.Attendance
{
    partial class ManualAttendanceForm
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
            this.lookupEmployee = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateAttendance = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.timeIn = new DevExpress.XtraEditors.TimeEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.timeOut = new DevExpress.XtraEditors.TimeEdit();
            this.lookupStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.memoNotes = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lookupEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAttendance.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAttendance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeOut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoNotes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lookupEmployee
            // 
            this.lookupEmployee.Location = new System.Drawing.Point(114, 12);
            this.lookupEmployee.Name = "lookupEmployee";
            this.lookupEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupEmployee.Properties.NullText = "";
            this.lookupEmployee.Size = new System.Drawing.Size(246, 20);
            this.lookupEmployee.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "الموظف:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(37, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "التاريخ:";
            // 
            // dateAttendance
            // 
            this.dateAttendance.EditValue = null;
            this.dateAttendance.Location = new System.Drawing.Point(114, 38);
            this.dateAttendance.Name = "dateAttendance";
            this.dateAttendance.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateAttendance.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateAttendance.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateAttendance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateAttendance.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateAttendance.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateAttendance.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateAttendance.Size = new System.Drawing.Size(100, 20);
            this.dateAttendance.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "وقت الدخول:";
            // 
            // timeIn
            // 
            this.timeIn.EditValue = new System.DateTime(2025, 1, 1, 0, 0, 0, 0);
            this.timeIn.Location = new System.Drawing.Point(114, 64);
            this.timeIn.Name = "timeIn";
            this.timeIn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeIn.Properties.DisplayFormat.FormatString = "HH:mm";
            this.timeIn.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeIn.Properties.EditFormat.FormatString = "HH:mm";
            this.timeIn.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeIn.Properties.Mask.EditMask = "HH:mm";
            this.timeIn.Size = new System.Drawing.Size(100, 20);
            this.timeIn.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 93);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "وقت الخروج:";
            // 
            // timeOut
            // 
            this.timeOut.EditValue = new System.DateTime(2025, 1, 1, 0, 0, 0, 0);
            this.timeOut.Location = new System.Drawing.Point(114, 90);
            this.timeOut.Name = "timeOut";
            this.timeOut.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeOut.Properties.DisplayFormat.FormatString = "HH:mm";
            this.timeOut.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeOut.Properties.EditFormat.FormatString = "HH:mm";
            this.timeOut.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeOut.Properties.Mask.EditMask = "HH:mm";
            this.timeOut.Size = new System.Drawing.Size(100, 20);
            this.timeOut.TabIndex = 3;
            // 
            // lookupStatus
            // 
            this.lookupStatus.Location = new System.Drawing.Point(114, 116);
            this.lookupStatus.Name = "lookupStatus";
            this.lookupStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupStatus.Properties.NullText = "";
            this.lookupStatus.Size = new System.Drawing.Size(100, 20);
            this.lookupStatus.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 119);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(33, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "الحالة:";
            // 
            // memoNotes
            // 
            this.memoNotes.Location = new System.Drawing.Point(114, 142);
            this.memoNotes.Name = "memoNotes";
            this.memoNotes.Size = new System.Drawing.Size(246, 60);
            this.memoNotes.TabIndex = 5;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 142);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(46, 13);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "ملاحظات:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(114, 208);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "حفظ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(195, 208);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "إلغاء";
            // 
            // ManualAttendanceForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(372, 243);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.memoNotes);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.lookupStatus);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.timeOut);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.timeIn);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.dateAttendance);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lookupEmployee);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManualAttendanceForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "إدخال سجل حضور يدوي";
            this.Load += new System.EventHandler(this.ManualAttendanceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookupEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAttendance.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAttendance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeOut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoNotes.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit lookupEmployee;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateAttendance;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TimeEdit timeIn;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TimeEdit timeOut;
        private DevExpress.XtraEditors.LookUpEdit lookupStatus;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.MemoEdit memoNotes;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}