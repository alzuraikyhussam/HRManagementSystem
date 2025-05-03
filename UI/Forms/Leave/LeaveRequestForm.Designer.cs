namespace HR.UI.Forms.Leave
{
    partial class LeaveRequestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeaveRequestForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.buttonSave = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControlApprovalInfo = new DevExpress.XtraEditors.LabelControl();
            this.labelControlSubmissionDate = new DevExpress.XtraEditors.LabelControl();
            this.labelControlStatus = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDaysCount = new DevExpress.XtraEditors.LabelControl();
            this.memoEditNotes = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditEndDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditStartDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditLeaveType = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditEmployee = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLeaveType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditEmployee.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.buttonCancel);
            this.panelControl1.Controls.Add(this.buttonSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 362);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(584, 48);
            this.panelControl1.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.ImageOptions.Image")));
            this.buttonCancel.Location = new System.Drawing.Point(410, 7);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 32);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "إلغاء";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.ImageOptions.Image")));
            this.buttonSave.Location = new System.Drawing.Point(497, 7);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 32);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "حفظ";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControlApprovalInfo);
            this.groupControl1.Controls.Add(this.labelControlSubmissionDate);
            this.groupControl1.Controls.Add(this.labelControlStatus);
            this.groupControl1.Controls.Add(this.labelControlDaysCount);
            this.groupControl1.Controls.Add(this.memoEditNotes);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.dateEditEndDate);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.dateEditStartDate);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.lookUpEditLeaveType);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.lookUpEditEmployee);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(584, 362);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "بيانات طلب الإجازة";
            // 
            // labelControlApprovalInfo
            // 
            this.labelControlApprovalInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlApprovalInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlApprovalInfo.Location = new System.Drawing.Point(12, 322);
            this.labelControlApprovalInfo.Name = "labelControlApprovalInfo";
            this.labelControlApprovalInfo.Size = new System.Drawing.Size(560, 23);
            this.labelControlApprovalInfo.TabIndex = 13;
            this.labelControlApprovalInfo.Text = "معلومات الموافقة";
            this.labelControlApprovalInfo.Visible = false;
            // 
            // labelControlSubmissionDate
            // 
            this.labelControlSubmissionDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlSubmissionDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlSubmissionDate.Location = new System.Drawing.Point(12, 293);
            this.labelControlSubmissionDate.Name = "labelControlSubmissionDate";
            this.labelControlSubmissionDate.Size = new System.Drawing.Size(260, 23);
            this.labelControlSubmissionDate.TabIndex = 12;
            this.labelControlSubmissionDate.Text = "تاريخ التقديم:";
            this.labelControlSubmissionDate.Visible = false;
            // 
            // labelControlStatus
            // 
            this.labelControlStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlStatus.Appearance.Options.UseFont = true;
            this.labelControlStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlStatus.Location = new System.Drawing.Point(278, 293);
            this.labelControlStatus.Name = "labelControlStatus";
            this.labelControlStatus.Size = new System.Drawing.Size(294, 23);
            this.labelControlStatus.TabIndex = 11;
            this.labelControlStatus.Text = "حالة الطلب: قيد الانتظار";
            this.labelControlStatus.Visible = false;
            // 
            // labelControlDaysCount
            // 
            this.labelControlDaysCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlDaysCount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlDaysCount.Appearance.Options.UseFont = true;
            this.labelControlDaysCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlDaysCount.Location = new System.Drawing.Point(243, 136);
            this.labelControlDaysCount.Name = "labelControlDaysCount";
            this.labelControlDaysCount.Size = new System.Drawing.Size(227, 23);
            this.labelControlDaysCount.TabIndex = 10;
            this.labelControlDaysCount.Text = "عدد الأيام: 1";
            // 
            // memoEditNotes
            // 
            this.memoEditNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.memoEditNotes.Location = new System.Drawing.Point(243, 164);
            this.memoEditNotes.Name = "memoEditNotes";
            this.memoEditNotes.Size = new System.Drawing.Size(249, 120);
            this.memoEditNotes.TabIndex = 9;
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl5.Location = new System.Drawing.Point(510, 166);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "ملاحظات:";
            // 
            // dateEditEndDate
            // 
            this.dateEditEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEditEndDate.EditValue = null;
            this.dateEditEndDate.Location = new System.Drawing.Point(243, 110);
            this.dateEditEndDate.Name = "dateEditEndDate";
            this.dateEditEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEndDate.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateEditEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditEndDate.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateEditEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditEndDate.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dateEditEndDate.Size = new System.Drawing.Size(249, 20);
            this.dateEditEndDate.TabIndex = 7;
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Location = new System.Drawing.Point(502, 113);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(62, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "تاريخ النهاية:";
            // 
            // dateEditStartDate
            // 
            this.dateEditStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEditStartDate.EditValue = null;
            this.dateEditStartDate.Location = new System.Drawing.Point(243, 84);
            this.dateEditStartDate.Name = "dateEditStartDate";
            this.dateEditStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditStartDate.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateEditStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditStartDate.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateEditStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditStartDate.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dateEditStartDate.Size = new System.Drawing.Size(249, 20);
            this.dateEditStartDate.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Location = new System.Drawing.Point(508, 87);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "تاريخ البداية:";
            // 
            // lookUpEditLeaveType
            // 
            this.lookUpEditLeaveType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lookUpEditLeaveType.Location = new System.Drawing.Point(243, 58);
            this.lookUpEditLeaveType.Name = "lookUpEditLeaveType";
            this.lookUpEditLeaveType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditLeaveType.Size = new System.Drawing.Size(249, 20);
            this.lookUpEditLeaveType.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Location = new System.Drawing.Point(508, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(57, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "نوع الإجازة:";
            // 
            // lookUpEditEmployee
            // 
            this.lookUpEditEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lookUpEditEmployee.Location = new System.Drawing.Point(243, 32);
            this.lookUpEditEmployee.Name = "lookUpEditEmployee";
            this.lookUpEditEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditEmployee.Properties.NullText = "اختر الموظف...";
            this.lookUpEditEmployee.Size = new System.Drawing.Size(249, 20);
            this.lookUpEditEmployee.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Location = new System.Drawing.Point(514, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "الموظف:";
            // 
            // LeaveRequestForm
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(584, 410);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("LeaveRequestForm.IconOptions.Image")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LeaveRequestForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "طلب إجازة";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLeaveType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditEmployee.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton buttonCancel;
        private DevExpress.XtraEditors.SimpleButton buttonSave;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditEmployee;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditLeaveType;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateEditStartDate;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dateEditEndDate;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.MemoEdit memoEditNotes;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControlDaysCount;
        private DevExpress.XtraEditors.LabelControl labelControlApprovalInfo;
        private DevExpress.XtraEditors.LabelControl labelControlSubmissionDate;
        private DevExpress.XtraEditors.LabelControl labelControlStatus;
    }
}