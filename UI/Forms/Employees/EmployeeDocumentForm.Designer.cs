namespace HR.UI.Forms.Employees
{
    partial class EmployeeDocumentForm
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
            this.lblDocumentType = new DevExpress.XtraEditors.LabelControl();
            this.cmbDocumentType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblDocumentTitle = new DevExpress.XtraEditors.LabelControl();
            this.txtDocumentTitle = new DevExpress.XtraEditors.TextEdit();
            this.lblDocumentNumber = new DevExpress.XtraEditors.LabelControl();
            this.txtDocumentNumber = new DevExpress.XtraEditors.TextEdit();
            this.lblIssueDate = new DevExpress.XtraEditors.LabelControl();
            this.dtIssueDate = new DevExpress.XtraEditors.DateEdit();
            this.lblExpiryDate = new DevExpress.XtraEditors.LabelControl();
            this.dtExpiryDate = new DevExpress.XtraEditors.DateEdit();
            this.lblIssuedBy = new DevExpress.XtraEditors.LabelControl();
            this.txtIssuedBy = new DevExpress.XtraEditors.TextEdit();
            this.lblDocumentPath = new DevExpress.XtraEditors.LabelControl();
            this.txtDocumentPath = new DevExpress.XtraEditors.TextEdit();
            this.btnBrowseDocumentPath = new DevExpress.XtraEditors.SimpleButton();
            this.groupFile = new DevExpress.XtraEditors.GroupControl();
            this.txtFilePath = new DevExpress.XtraEditors.TextEdit();
            this.btnSelectFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownloadFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearFile = new DevExpress.XtraEditors.SimpleButton();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDocumentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocumentTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocumentNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtIssueDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtIssueDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExpiryDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExpiryDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssuedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocumentPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupFile)).BeginInit();
            this.groupFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDocumentType
            // 
            this.lblDocumentType.Location = new System.Drawing.Point(12, 12);
            this.lblDocumentType.Name = "lblDocumentType";
            this.lblDocumentType.Size = new System.Drawing.Size(57, 13);
            this.lblDocumentType.TabIndex = 0;
            this.lblDocumentType.Text = "نوع الوثيقة:";
            // 
            // cmbDocumentType
            // 
            this.cmbDocumentType.Location = new System.Drawing.Point(113, 9);
            this.cmbDocumentType.Name = "cmbDocumentType";
            this.cmbDocumentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDocumentType.Size = new System.Drawing.Size(276, 20);
            this.cmbDocumentType.TabIndex = 1;
            // 
            // lblDocumentTitle
            // 
            this.lblDocumentTitle.Location = new System.Drawing.Point(12, 38);
            this.lblDocumentTitle.Name = "lblDocumentTitle";
            this.lblDocumentTitle.Size = new System.Drawing.Size(64, 13);
            this.lblDocumentTitle.TabIndex = 2;
            this.lblDocumentTitle.Text = "عنوان الوثيقة:";
            // 
            // txtDocumentTitle
            // 
            this.txtDocumentTitle.Location = new System.Drawing.Point(113, 35);
            this.txtDocumentTitle.Name = "txtDocumentTitle";
            this.txtDocumentTitle.Size = new System.Drawing.Size(276, 20);
            this.txtDocumentTitle.TabIndex = 3;
            // 
            // lblDocumentNumber
            // 
            this.lblDocumentNumber.Location = new System.Drawing.Point(12, 64);
            this.lblDocumentNumber.Name = "lblDocumentNumber";
            this.lblDocumentNumber.Size = new System.Drawing.Size(56, 13);
            this.lblDocumentNumber.TabIndex = 4;
            this.lblDocumentNumber.Text = "رقم الوثيقة:";
            // 
            // txtDocumentNumber
            // 
            this.txtDocumentNumber.Location = new System.Drawing.Point(113, 61);
            this.txtDocumentNumber.Name = "txtDocumentNumber";
            this.txtDocumentNumber.Size = new System.Drawing.Size(276, 20);
            this.txtDocumentNumber.TabIndex = 5;
            // 
            // lblIssueDate
            // 
            this.lblIssueDate.Location = new System.Drawing.Point(12, 90);
            this.lblIssueDate.Name = "lblIssueDate";
            this.lblIssueDate.Size = new System.Drawing.Size(64, 13);
            this.lblIssueDate.TabIndex = 6;
            this.lblIssueDate.Text = "تاريخ الإصدار:";
            // 
            // dtIssueDate
            // 
            this.dtIssueDate.EditValue = null;
            this.dtIssueDate.Location = new System.Drawing.Point(113, 87);
            this.dtIssueDate.Name = "dtIssueDate";
            this.dtIssueDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtIssueDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtIssueDate.Size = new System.Drawing.Size(276, 20);
            this.dtIssueDate.TabIndex = 7;
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.Location = new System.Drawing.Point(12, 116);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(67, 13);
            this.lblExpiryDate.TabIndex = 8;
            this.lblExpiryDate.Text = "تاريخ الانتهاء:";
            // 
            // dtExpiryDate
            // 
            this.dtExpiryDate.EditValue = null;
            this.dtExpiryDate.Location = new System.Drawing.Point(113, 113);
            this.dtExpiryDate.Name = "dtExpiryDate";
            this.dtExpiryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtExpiryDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtExpiryDate.Size = new System.Drawing.Size(276, 20);
            this.dtExpiryDate.TabIndex = 9;
            // 
            // lblIssuedBy
            // 
            this.lblIssuedBy.Location = new System.Drawing.Point(12, 142);
            this.lblIssuedBy.Name = "lblIssuedBy";
            this.lblIssuedBy.Size = new System.Drawing.Size(70, 13);
            this.lblIssuedBy.TabIndex = 10;
            this.lblIssuedBy.Text = "الجهة المصدرة:";
            // 
            // txtIssuedBy
            // 
            this.txtIssuedBy.Location = new System.Drawing.Point(113, 139);
            this.txtIssuedBy.Name = "txtIssuedBy";
            this.txtIssuedBy.Size = new System.Drawing.Size(276, 20);
            this.txtIssuedBy.TabIndex = 11;
            // 
            // lblDocumentPath
            // 
            this.lblDocumentPath.Location = new System.Drawing.Point(12, 168);
            this.lblDocumentPath.Name = "lblDocumentPath";
            this.lblDocumentPath.Size = new System.Drawing.Size(64, 13);
            this.lblDocumentPath.TabIndex = 12;
            this.lblDocumentPath.Text = "مسار الوثيقة:";
            // 
            // txtDocumentPath
            // 
            this.txtDocumentPath.Location = new System.Drawing.Point(113, 165);
            this.txtDocumentPath.Name = "txtDocumentPath";
            this.txtDocumentPath.Size = new System.Drawing.Size(227, 20);
            this.txtDocumentPath.TabIndex = 13;
            // 
            // btnBrowseDocumentPath
            // 
            this.btnBrowseDocumentPath.Location = new System.Drawing.Point(346, 164);
            this.btnBrowseDocumentPath.Name = "btnBrowseDocumentPath";
            this.btnBrowseDocumentPath.Size = new System.Drawing.Size(43, 23);
            this.btnBrowseDocumentPath.TabIndex = 14;
            this.btnBrowseDocumentPath.Text = "...";
            // 
            // groupFile
            // 
            this.groupFile.Controls.Add(this.txtFilePath);
            this.groupFile.Controls.Add(this.btnSelectFile);
            this.groupFile.Controls.Add(this.btnDownloadFile);
            this.groupFile.Controls.Add(this.btnClearFile);
            this.groupFile.Location = new System.Drawing.Point(12, 191);
            this.groupFile.Name = "groupFile";
            this.groupFile.Size = new System.Drawing.Size(377, 82);
            this.groupFile.TabIndex = 15;
            this.groupFile.Text = "ملف الوثيقة";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(5, 24);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Properties.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(367, 20);
            this.txtFilePath.TabIndex = 0;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(289, 50);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(83, 23);
            this.btnSelectFile.TabIndex = 1;
            this.btnSelectFile.Text = "اختيار ملف";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnDownloadFile
            // 
            this.btnDownloadFile.Enabled = false;
            this.btnDownloadFile.Location = new System.Drawing.Point(200, 50);
            this.btnDownloadFile.Name = "btnDownloadFile";
            this.btnDownloadFile.Size = new System.Drawing.Size(83, 23);
            this.btnDownloadFile.TabIndex = 2;
            this.btnDownloadFile.Text = "تنزيل الملف";
            this.btnDownloadFile.Click += new System.EventHandler(this.btnDownloadFile_Click);
            // 
            // btnClearFile
            // 
            this.btnClearFile.Location = new System.Drawing.Point(5, 50);
            this.btnClearFile.Name = "btnClearFile";
            this.btnClearFile.Size = new System.Drawing.Size(83, 23);
            this.btnClearFile.TabIndex = 3;
            this.btnClearFile.Text = "حذف الملف";
            this.btnClearFile.Click += new System.EventHandler(this.btnClearFile_Click);
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(12, 279);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(49, 13);
            this.lblNotes.TabIndex = 16;
            this.lblNotes.Text = "ملاحظات:";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(12, 298);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(377, 96);
            this.txtNotes.TabIndex = 17;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(314, 400);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "حفظ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(233, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "إلغاء";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EmployeeDocumentForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(401, 435);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.groupFile);
            this.Controls.Add(this.btnBrowseDocumentPath);
            this.Controls.Add(this.txtDocumentPath);
            this.Controls.Add(this.lblDocumentPath);
            this.Controls.Add(this.txtIssuedBy);
            this.Controls.Add(this.lblIssuedBy);
            this.Controls.Add(this.dtExpiryDate);
            this.Controls.Add(this.lblExpiryDate);
            this.Controls.Add(this.dtIssueDate);
            this.Controls.Add(this.lblIssueDate);
            this.Controls.Add(this.txtDocumentNumber);
            this.Controls.Add(this.lblDocumentNumber);
            this.Controls.Add(this.txtDocumentTitle);
            this.Controls.Add(this.lblDocumentTitle);
            this.Controls.Add(this.cmbDocumentType);
            this.Controls.Add(this.lblDocumentType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeDocumentForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "وثيقة موظف";
            this.Load += new System.EventHandler(this.EmployeeDocumentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbDocumentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocumentTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocumentNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtIssueDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtIssueDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExpiryDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExpiryDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssuedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocumentPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupFile)).EndInit();
            this.groupFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblDocumentType;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDocumentType;
        private DevExpress.XtraEditors.LabelControl lblDocumentTitle;
        private DevExpress.XtraEditors.TextEdit txtDocumentTitle;
        private DevExpress.XtraEditors.LabelControl lblDocumentNumber;
        private DevExpress.XtraEditors.TextEdit txtDocumentNumber;
        private DevExpress.XtraEditors.LabelControl lblIssueDate;
        private DevExpress.XtraEditors.DateEdit dtIssueDate;
        private DevExpress.XtraEditors.LabelControl lblExpiryDate;
        private DevExpress.XtraEditors.DateEdit dtExpiryDate;
        private DevExpress.XtraEditors.LabelControl lblIssuedBy;
        private DevExpress.XtraEditors.TextEdit txtIssuedBy;
        private DevExpress.XtraEditors.LabelControl lblDocumentPath;
        private DevExpress.XtraEditors.TextEdit txtDocumentPath;
        private DevExpress.XtraEditors.SimpleButton btnBrowseDocumentPath;
        private DevExpress.XtraEditors.GroupControl groupFile;
        private DevExpress.XtraEditors.TextEdit txtFilePath;
        private DevExpress.XtraEditors.SimpleButton btnSelectFile;
        private DevExpress.XtraEditors.SimpleButton btnDownloadFile;
        private DevExpress.XtraEditors.SimpleButton btnClearFile;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}