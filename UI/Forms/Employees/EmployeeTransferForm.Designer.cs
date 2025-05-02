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
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.labelEmployee = new DevExpress.XtraEditors.LabelControl();
            this.lookUpFromDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpFromPosition = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpToDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpToPosition = new DevExpress.XtraEditors.LookUpEdit();
            this.dateTransferDate = new DevExpress.XtraEditors.DateEdit();
            this.spinEditNewSalary = new DevExpress.XtraEditors.SpinEdit();
            this.memoNotes = new DevExpress.XtraEditors.MemoEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutItemEmployee = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemFromDepartment = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemFromPosition = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemToDepartment = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemToPosition = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemTransferDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemNewSalary = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemNotes = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpFromDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpFromPosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToPosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTransferDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTransferDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditNewSalary.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemFromDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemFromPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemToDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemToPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTransferDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemNewSalary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.labelEmployee);
            this.layoutControl.Controls.Add(this.lookUpFromDepartment);
            this.layoutControl.Controls.Add(this.lookUpFromPosition);
            this.layoutControl.Controls.Add(this.lookUpToDepartment);
            this.layoutControl.Controls.Add(this.lookUpToPosition);
            this.layoutControl.Controls.Add(this.dateTransferDate);
            this.layoutControl.Controls.Add(this.spinEditNewSalary);
            this.layoutControl.Controls.Add(this.memoNotes);
            this.layoutControl.Controls.Add(this.btnSave);
            this.layoutControl.Controls.Add(this.btnCancel);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(800, 334, 650, 400);
            this.layoutControl.Root = this.layoutControlGroup;
            this.layoutControl.Size = new System.Drawing.Size(550, 400);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup.GroupBordersVisible = false;
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemEmployee,
            this.layoutItemFromDepartment,
            this.layoutItemFromPosition,
            this.layoutItemToDepartment,
            this.layoutItemToPosition,
            this.layoutItemTransferDate,
            this.layoutItemNewSalary,
            this.layoutItemNotes,
            this.emptySpaceItem1,
            this.layoutItemSave,
            this.layoutItemCancel});
            this.layoutControlGroup.Name = "Root";
            this.layoutControlGroup.Size = new System.Drawing.Size(550, 400);
            this.layoutControlGroup.TextVisible = false;
            // 
            // labelEmployee
            // 
            this.labelEmployee.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelEmployee.Appearance.Options.UseFont = true;
            this.labelEmployee.Appearance.Options.UseTextOptions = true;
            this.labelEmployee.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelEmployee.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelEmployee.Location = new System.Drawing.Point(112, 12);
            this.labelEmployee.Name = "labelEmployee";
            this.labelEmployee.Size = new System.Drawing.Size(426, 20);
            this.labelEmployee.StyleController = this.layoutControl;
            this.labelEmployee.TabIndex = 4;
            this.labelEmployee.Text = "اسم الموظف";
            // 
            // lookUpFromDepartment
            // 
            this.lookUpFromDepartment.Location = new System.Drawing.Point(112, 36);
            this.lookUpFromDepartment.Name = "lookUpFromDepartment";
            this.lookUpFromDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpFromDepartment.Properties.NullText = "";
            this.lookUpFromDepartment.Properties.ReadOnly = true;
            this.lookUpFromDepartment.Size = new System.Drawing.Size(426, 20);
            this.lookUpFromDepartment.StyleController = this.layoutControl;
            this.lookUpFromDepartment.TabIndex = 5;
            // 
            // lookUpFromPosition
            // 
            this.lookUpFromPosition.Location = new System.Drawing.Point(112, 60);
            this.lookUpFromPosition.Name = "lookUpFromPosition";
            this.lookUpFromPosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpFromPosition.Properties.NullText = "";
            this.lookUpFromPosition.Properties.ReadOnly = true;
            this.lookUpFromPosition.Size = new System.Drawing.Size(426, 20);
            this.lookUpFromPosition.StyleController = this.layoutControl;
            this.lookUpFromPosition.TabIndex = 6;
            // 
            // lookUpToDepartment
            // 
            this.lookUpToDepartment.Location = new System.Drawing.Point(112, 84);
            this.lookUpToDepartment.Name = "lookUpToDepartment";
            this.lookUpToDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpToDepartment.Properties.NullText = "";
            this.lookUpToDepartment.Size = new System.Drawing.Size(426, 20);
            this.lookUpToDepartment.StyleController = this.layoutControl;
            this.lookUpToDepartment.TabIndex = 7;
            // 
            // lookUpToPosition
            // 
            this.lookUpToPosition.Location = new System.Drawing.Point(112, 108);
            this.lookUpToPosition.Name = "lookUpToPosition";
            this.lookUpToPosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpToPosition.Properties.NullText = "";
            this.lookUpToPosition.Size = new System.Drawing.Size(426, 20);
            this.lookUpToPosition.StyleController = this.layoutControl;
            this.lookUpToPosition.TabIndex = 8;
            // 
            // dateTransferDate
            // 
            this.dateTransferDate.EditValue = null;
            this.dateTransferDate.Location = new System.Drawing.Point(112, 132);
            this.dateTransferDate.Name = "dateTransferDate";
            this.dateTransferDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTransferDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTransferDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateTransferDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTransferDate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateTransferDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTransferDate.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateTransferDate.Size = new System.Drawing.Size(426, 20);
            this.dateTransferDate.StyleController = this.layoutControl;
            this.dateTransferDate.TabIndex = 9;
            // 
            // spinEditNewSalary
            // 
            this.spinEditNewSalary.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditNewSalary.Location = new System.Drawing.Point(112, 156);
            this.spinEditNewSalary.Name = "spinEditNewSalary";
            this.spinEditNewSalary.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditNewSalary.Properties.DisplayFormat.FormatString = "N2";
            this.spinEditNewSalary.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditNewSalary.Properties.EditFormat.FormatString = "N2";
            this.spinEditNewSalary.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditNewSalary.Properties.Mask.EditMask = "N2";
            this.spinEditNewSalary.Size = new System.Drawing.Size(426, 20);
            this.spinEditNewSalary.StyleController = this.layoutControl;
            this.spinEditNewSalary.TabIndex = 10;
            // 
            // memoNotes
            // 
            this.memoNotes.Location = new System.Drawing.Point(112, 180);
            this.memoNotes.Name = "memoNotes";
            this.memoNotes.Size = new System.Drawing.Size(426, 74);
            this.memoNotes.StyleController = this.layoutControl;
            this.memoNotes.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 356);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(260, 32);
            this.btnSave.StyleController = this.layoutControl;
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "حفظ";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(276, 356);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(262, 32);
            this.btnCancel.StyleController = this.layoutControl;
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "إلغاء";
            // 
            // layoutItemEmployee
            // 
            this.layoutItemEmployee.Control = this.labelEmployee;
            this.layoutItemEmployee.Location = new System.Drawing.Point(0, 0);
            this.layoutItemEmployee.Name = "layoutItemEmployee";
            this.layoutItemEmployee.Size = new System.Drawing.Size(530, 24);
            this.layoutItemEmployee.Text = "الموظف:";
            this.layoutItemEmployee.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemFromDepartment
            // 
            this.layoutItemFromDepartment.Control = this.lookUpFromDepartment;
            this.layoutItemFromDepartment.Location = new System.Drawing.Point(0, 24);
            this.layoutItemFromDepartment.Name = "layoutItemFromDepartment";
            this.layoutItemFromDepartment.Size = new System.Drawing.Size(530, 24);
            this.layoutItemFromDepartment.Text = "القسم الحالي:";
            this.layoutItemFromDepartment.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemFromPosition
            // 
            this.layoutItemFromPosition.Control = this.lookUpFromPosition;
            this.layoutItemFromPosition.Location = new System.Drawing.Point(0, 48);
            this.layoutItemFromPosition.Name = "layoutItemFromPosition";
            this.layoutItemFromPosition.Size = new System.Drawing.Size(530, 24);
            this.layoutItemFromPosition.Text = "المسمى الحالي:";
            this.layoutItemFromPosition.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemToDepartment
            // 
            this.layoutItemToDepartment.Control = this.lookUpToDepartment;
            this.layoutItemToDepartment.Location = new System.Drawing.Point(0, 72);
            this.layoutItemToDepartment.Name = "layoutItemToDepartment";
            this.layoutItemToDepartment.Size = new System.Drawing.Size(530, 24);
            this.layoutItemToDepartment.Text = "القسم الجديد:";
            this.layoutItemToDepartment.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemToPosition
            // 
            this.layoutItemToPosition.Control = this.lookUpToPosition;
            this.layoutItemToPosition.Location = new System.Drawing.Point(0, 96);
            this.layoutItemToPosition.Name = "layoutItemToPosition";
            this.layoutItemToPosition.Size = new System.Drawing.Size(530, 24);
            this.layoutItemToPosition.Text = "المسمى الجديد:";
            this.layoutItemToPosition.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemTransferDate
            // 
            this.layoutItemTransferDate.Control = this.dateTransferDate;
            this.layoutItemTransferDate.Location = new System.Drawing.Point(0, 120);
            this.layoutItemTransferDate.Name = "layoutItemTransferDate";
            this.layoutItemTransferDate.Size = new System.Drawing.Size(530, 24);
            this.layoutItemTransferDate.Text = "تاريخ النقل:";
            this.layoutItemTransferDate.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemNewSalary
            // 
            this.layoutItemNewSalary.Control = this.spinEditNewSalary;
            this.layoutItemNewSalary.Location = new System.Drawing.Point(0, 144);
            this.layoutItemNewSalary.Name = "layoutItemNewSalary";
            this.layoutItemNewSalary.Size = new System.Drawing.Size(530, 24);
            this.layoutItemNewSalary.Text = "الراتب الجديد:";
            this.layoutItemNewSalary.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutItemNotes
            // 
            this.layoutItemNotes.Control = this.memoNotes;
            this.layoutItemNotes.Location = new System.Drawing.Point(0, 168);
            this.layoutItemNotes.Name = "layoutItemNotes";
            this.layoutItemNotes.Size = new System.Drawing.Size(530, 78);
            this.layoutItemNotes.Text = "ملاحظات:";
            this.layoutItemNotes.TextSize = new System.Drawing.Size(97, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 246);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(530, 98);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutItemSave
            // 
            this.layoutItemSave.Control = this.btnSave;
            this.layoutItemSave.Location = new System.Drawing.Point(0, 344);
            this.layoutItemSave.Name = "layoutItemSave";
            this.layoutItemSave.Size = new System.Drawing.Size(264, 36);
            this.layoutItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemSave.TextVisible = false;
            // 
            // layoutItemCancel
            // 
            this.layoutItemCancel.Control = this.btnCancel;
            this.layoutItemCancel.Location = new System.Drawing.Point(264, 344);
            this.layoutItemCancel.Name = "layoutItemCancel";
            this.layoutItemCancel.Size = new System.Drawing.Size(266, 36);
            this.layoutItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemCancel.TextVisible = false;
            // 
            // EmployeeTransferForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(550, 400);
            this.Controls.Add(this.layoutControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeTransferForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "نقل/ترقية موظف";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpFromDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpFromPosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToPosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTransferDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTransferDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditNewSalary.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemFromDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemFromPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemToDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemToPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTransferDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemNewSalary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        private DevExpress.XtraEditors.LabelControl labelEmployee;
        private DevExpress.XtraEditors.LookUpEdit lookUpFromDepartment;
        private DevExpress.XtraEditors.LookUpEdit lookUpFromPosition;
        private DevExpress.XtraEditors.LookUpEdit lookUpToDepartment;
        private DevExpress.XtraEditors.LookUpEdit lookUpToPosition;
        private DevExpress.XtraEditors.DateEdit dateTransferDate;
        private DevExpress.XtraEditors.SpinEdit spinEditNewSalary;
        private DevExpress.XtraEditors.MemoEdit memoNotes;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemEmployee;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemFromDepartment;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemFromPosition;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemToDepartment;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemToPosition;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemTransferDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemNewSalary;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemNotes;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemCancel;
    }
}