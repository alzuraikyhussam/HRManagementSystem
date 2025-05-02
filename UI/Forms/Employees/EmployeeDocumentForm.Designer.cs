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
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.textDocumentName = new DevExpress.XtraEditors.TextEdit();
            this.lookUpDocumentType = new DevExpress.XtraEditors.LookUpEdit();
            this.dateIssueDate = new DevExpress.XtraEditors.DateEdit();
            this.dateExpiryDate = new DevExpress.XtraEditors.DateEdit();
            this.checkHasExpiry = new DevExpress.XtraEditors.CheckEdit();
            this.memoNotes = new DevExpress.XtraEditors.MemoEdit();
            this.btnUploadFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnViewFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutItemDocumentName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemDocumentType = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemIssueDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemHasExpiry = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemExpiryDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemNotes = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemUploadFile = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemViewFile = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDocumentName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpDocumentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateIssueDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateIssueDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExpiryDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExpiryDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkHasExpiry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemDocumentName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemDocumentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemIssueDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHasExpiry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemExpiryDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemUploadFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemViewFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.textDocumentName);
            this.layoutControl.Controls.Add(this.lookUpDocumentType);
            this.layoutControl.Controls.Add(this.dateIssueDate);
            this.layoutControl.Controls.Add(this.dateExpiryDate);
            this.layoutControl.Controls.Add(this.checkHasExpiry);
            this.layoutControl.Controls.Add(this.memoNotes);
            this.layoutControl.Controls.Add(this.btnUploadFile);
            this.layoutControl.Controls.Add(this.btnViewFile);
            this.layoutControl.Controls.Add(this.btnSave);
            this.layoutControl.Controls.Add(this.btnCancel);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(800, 334, 650, 400);
            this.layoutControl.Root = this.layoutControlGroup;
            this.layoutControl.Size = new System.Drawing.Size(500, 350);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup.GroupBordersVisible = false;
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemDocumentName,
            this.layoutItemDocumentType,
            this.layoutItemIssueDate,
            this.layoutItemHasExpiry,
            this.layoutItemExpiryDate,
            this.layoutItemNotes,
            this.layoutItemUploadFile,
            this.layoutItemViewFile,
            this.emptySpaceItem1,
            this.layoutItemSave,
            this.layoutItemCancel});
            this.layoutControlGroup.Name = "Root";
            this.layoutControlGroup.Size = new System.Drawing.Size(500, 350);
            this.layoutControlGroup.TextVisible = false;
            // 
            // textDocumentName
            // 
            this.textDocumentName.Location = new System.Drawing.Point(112, 12);
            this.textDocumentName.Name = "textDocumentName";
            this.textDocumentName.Size = new System.Drawing.Size(376, 20);
            this.textDocumentName.StyleController = this.layoutControl;
            this.textDocumentName.TabIndex = 4;
            // 
            // lookUpDocumentType
            // 
            this.lookUpDocumentType.Location = new System.Drawing.Point(112, 36);
            this.lookUpDocumentType.Name = "lookUpDocumentType";
            this.lookUpDocumentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpDocumentType.Properties.NullText = "";
            this.lookUpDocumentType.Size = new System.Drawing.Size(376, 20);
            this.lookUpDocumentType.StyleController = this.layoutControl;
            this.lookUpDocumentType.TabIndex = 5;
            // 
            // dateIssueDate
            // 
            this.dateIssueDate.EditValue = null;
            this.dateIssueDate.Location = new System.Drawing.Point(112, 60);
            this.dateIssueDate.Name = "dateIssueDate";
            this.dateIssueDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateIssueDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateIssueDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateIssueDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateIssueDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateIssueDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateIssueDate.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateIssueDate.Size = new System.Drawing.Size(376, 20);
            this.dateIssueDate.StyleController = this.layoutControl;
            this.dateIssueDate.TabIndex = 6;
            // 
            // dateExpiryDate
            // 
            this.dateExpiryDate.EditValue = null;
            this.dateExpiryDate.Location = new System.Drawing.Point(112, 108);
            this.dateExpiryDate.Name = "dateExpiryDate";
            this.dateExpiryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateExpiryDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateExpiryDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateExpiryDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateExpiryDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateExpiryDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateExpiryDate.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateExpiryDate.Size = new System.Drawing.Size(376, 20);
            this.dateExpiryDate.StyleController = this.layoutControl;
            this.dateExpiryDate.TabIndex = 8;
            // 
            // checkHasExpiry
            // 
            this.checkHasExpiry.Location = new System.Drawing.Point(112, 84);
            this.checkHasExpiry.Name = "checkHasExpiry";
            this.checkHasExpiry.Properties.Caption = "";
            this.checkHasExpiry.Size = new System.Drawing.Size(376, 20);
            this.checkHasExpiry.StyleController = this.layoutControl;
            this.checkHasExpiry.TabIndex = 7;
            // 
            // memoNotes
            // 
            this.memoNotes.Location = new System.Drawing.Point(112, 132);
            this.memoNotes.Name = "memoNotes";
            this.memoNotes.Size = new System.Drawing.Size(376, 82);
            this.memoNotes.StyleController = this.layoutControl;
            this.memoNotes.TabIndex = 9;
            // 
            // btnUploadFile
            // 
            this.btnUploadFile.Location = new System.Drawing.Point(112, 218);
            this.btnUploadFile.Name = "btnUploadFile";
            this.btnUploadFile.Size = new System.Drawing.Size(182, 22);
            this.btnUploadFile.StyleController = this.layoutControl;
            this.btnUploadFile.TabIndex = 10;
            this.btnUploadFile.Text = "تحميل ملف";
            // 
            // btnViewFile
            // 
            this.btnViewFile.Location = new System.Drawing.Point(298, 218);
            this.btnViewFile.Name = "btnViewFile";
            this.btnViewFile.Size = new System.Drawing.Size(190, 22);
            this.btnViewFile.StyleController = this.layoutControl;
            this.btnViewFile.TabIndex = 11;
            this.btnViewFile.Text = "عرض الملف";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 306);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(235, 32);
            this.btnSave.StyleController = this.layoutControl;
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "حفظ";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(251, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(237, 32);
            this.btnCancel.StyleController = this.layoutControl;
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "إلغاء";
            // 
            // layoutItemDocumentName
            // 
            this.layoutItemDocumentName.Control = this.textDocumentName;
            this.layoutItemDocumentName.Location = new System.Drawing.Point(0, 0);
            this.layoutItemDocumentName.Name = "layoutItemDocumentName";
            this.layoutItemDocumentName.Size = new System.Drawing.Size(480, 24);
            this.layoutItemDocumentName.Text = "اسم المستند:";
            this.layoutItemDocumentName.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemDocumentType
            // 
            this.layoutItemDocumentType.Control = this.lookUpDocumentType;
            this.layoutItemDocumentType.Location = new System.Drawing.Point(0, 24);
            this.layoutItemDocumentType.Name = "layoutItemDocumentType";
            this.layoutItemDocumentType.Size = new System.Drawing.Size(480, 24);
            this.layoutItemDocumentType.Text = "نوع المستند:";
            this.layoutItemDocumentType.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemIssueDate
            // 
            this.layoutItemIssueDate.Control = this.dateIssueDate;
            this.layoutItemIssueDate.Location = new System.Drawing.Point(0, 48);
            this.layoutItemIssueDate.Name = "layoutItemIssueDate";
            this.layoutItemIssueDate.Size = new System.Drawing.Size(480, 24);
            this.layoutItemIssueDate.Text = "تاريخ الإصدار:";
            this.layoutItemIssueDate.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemHasExpiry
            // 
            this.layoutItemHasExpiry.Control = this.checkHasExpiry;
            this.layoutItemHasExpiry.Location = new System.Drawing.Point(0, 72);
            this.layoutItemHasExpiry.Name = "layoutItemHasExpiry";
            this.layoutItemHasExpiry.Size = new System.Drawing.Size(480, 24);
            this.layoutItemHasExpiry.Text = "له تاريخ انتهاء:";
            this.layoutItemHasExpiry.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemExpiryDate
            // 
            this.layoutItemExpiryDate.Control = this.dateExpiryDate;
            this.layoutItemExpiryDate.Location = new System.Drawing.Point(0, 96);
            this.layoutItemExpiryDate.Name = "layoutItemExpiryDate";
            this.layoutItemExpiryDate.Size = new System.Drawing.Size(480, 24);
            this.layoutItemExpiryDate.Text = "تاريخ الانتهاء:";
            this.layoutItemExpiryDate.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemNotes
            // 
            this.layoutItemNotes.Control = this.memoNotes;
            this.layoutItemNotes.Location = new System.Drawing.Point(0, 120);
            this.layoutItemNotes.Name = "layoutItemNotes";
            this.layoutItemNotes.Size = new System.Drawing.Size(480, 86);
            this.layoutItemNotes.Text = "ملاحظات:";
            this.layoutItemNotes.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemUploadFile
            // 
            this.layoutItemUploadFile.Control = this.btnUploadFile;
            this.layoutItemUploadFile.Location = new System.Drawing.Point(0, 206);
            this.layoutItemUploadFile.Name = "layoutItemUploadFile";
            this.layoutItemUploadFile.Size = new System.Drawing.Size(286, 26);
            this.layoutItemUploadFile.Text = "الملف:";
            this.layoutItemUploadFile.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemViewFile
            // 
            this.layoutItemViewFile.Control = this.btnViewFile;
            this.layoutItemViewFile.Location = new System.Drawing.Point(286, 206);
            this.layoutItemViewFile.Name = "layoutItemViewFile";
            this.layoutItemViewFile.Size = new System.Drawing.Size(194, 26);
            this.layoutItemViewFile.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemViewFile.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 232);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(480, 62);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutItemSave
            // 
            this.layoutItemSave.Control = this.btnSave;
            this.layoutItemSave.Location = new System.Drawing.Point(0, 294);
            this.layoutItemSave.Name = "layoutItemSave";
            this.layoutItemSave.Size = new System.Drawing.Size(239, 36);
            this.layoutItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemSave.TextVisible = false;
            // 
            // layoutItemCancel
            // 
            this.layoutItemCancel.Control = this.btnCancel;
            this.layoutItemCancel.Location = new System.Drawing.Point(239, 294);
            this.layoutItemCancel.Name = "layoutItemCancel";
            this.layoutItemCancel.Size = new System.Drawing.Size(241, 36);
            this.layoutItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemCancel.TextVisible = false;
            // 
            // EmployeeDocumentForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this.layoutControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeDocumentForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "وثيقة الموظف";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDocumentName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpDocumentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateIssueDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateIssueDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExpiryDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExpiryDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkHasExpiry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemDocumentName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemDocumentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemIssueDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHasExpiry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemExpiryDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemUploadFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemViewFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        private DevExpress.XtraEditors.TextEdit textDocumentName;
        private DevExpress.XtraEditors.LookUpEdit lookUpDocumentType;
        private DevExpress.XtraEditors.DateEdit dateIssueDate;
        private DevExpress.XtraEditors.DateEdit dateExpiryDate;
        private DevExpress.XtraEditors.CheckEdit checkHasExpiry;
        private DevExpress.XtraEditors.MemoEdit memoNotes;
        private DevExpress.XtraEditors.SimpleButton btnUploadFile;
        private DevExpress.XtraEditors.SimpleButton btnViewFile;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemDocumentName;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemDocumentType;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemIssueDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemHasExpiry;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemExpiryDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemNotes;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemUploadFile;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemViewFile;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemCancel;
    }
}