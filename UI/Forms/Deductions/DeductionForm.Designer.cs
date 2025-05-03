namespace HR.UI.Forms.Deductions
{
    partial class DeductionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeductionForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lookUpEditEmployee = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditRule = new DevExpress.XtraEditors.LookUpEdit();
            this.dateEditViolation = new DevExpress.XtraEditors.DateEdit();
            this.comboBoxEditViolationType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.spinEditViolationValue = new DevExpress.XtraEditors.SpinEdit();
            this.comboBoxEditDeductionMethod = new DevExpress.XtraEditors.ComboBoxEdit();
            this.spinEditDeductionValue = new DevExpress.XtraEditors.SpinEdit();
            this.memoEditDescription = new DevExpress.XtraEditors.MemoEdit();
            this.simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemEmployee = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemRule = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemViolationDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemViolationType = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemViolationValue = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDeductionMethod = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDeductionValue = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditViolation.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditViolation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditViolationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditViolationValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDeductionMethod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditDeductionValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemViolationDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemViolationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemViolationValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDeductionMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDeductionValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lookUpEditEmployee);
            this.layoutControl1.Controls.Add(this.lookUpEditRule);
            this.layoutControl1.Controls.Add(this.dateEditViolation);
            this.layoutControl1.Controls.Add(this.comboBoxEditViolationType);
            this.layoutControl1.Controls.Add(this.spinEditViolationValue);
            this.layoutControl1.Controls.Add(this.comboBoxEditDeductionMethod);
            this.layoutControl1.Controls.Add(this.spinEditDeductionValue);
            this.layoutControl1.Controls.Add(this.memoEditDescription);
            this.layoutControl1.Controls.Add(this.simpleButtonSave);
            this.layoutControl1.Controls.Add(this.simpleButtonCancel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(795, 190, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(584, 361);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lookUpEditEmployee
            // 
            this.lookUpEditEmployee.Location = new System.Drawing.Point(12, 28);
            this.lookUpEditEmployee.Name = "lookUpEditEmployee";
            this.lookUpEditEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditEmployee.Properties.DisplayMember = "FullName";
            this.lookUpEditEmployee.Properties.NullText = "";
            this.lookUpEditEmployee.Properties.ValueMember = "ID";
            this.lookUpEditEmployee.Size = new System.Drawing.Size(472, 20);
            this.lookUpEditEmployee.StyleController = this.layoutControl1;
            this.lookUpEditEmployee.TabIndex = 4;
            this.lookUpEditEmployee.EditValueChanged += new System.EventHandler(this.lookUpEditEmployee_EditValueChanged);
            // 
            // lookUpEditRule
            // 
            this.lookUpEditRule.Location = new System.Drawing.Point(12, 68);
            this.lookUpEditRule.Name = "lookUpEditRule";
            this.lookUpEditRule.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditRule.Properties.DisplayMember = "Name";
            this.lookUpEditRule.Properties.NullText = "";
            this.lookUpEditRule.Properties.ValueMember = "ID";
            this.lookUpEditRule.Size = new System.Drawing.Size(472, 20);
            this.lookUpEditRule.StyleController = this.layoutControl1;
            this.lookUpEditRule.TabIndex = 5;
            this.lookUpEditRule.EditValueChanged += new System.EventHandler(this.lookUpEditRule_EditValueChanged);
            // 
            // dateEditViolation
            // 
            this.dateEditViolation.EditValue = null;
            this.dateEditViolation.Location = new System.Drawing.Point(12, 108);
            this.dateEditViolation.Name = "dateEditViolation";
            this.dateEditViolation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditViolation.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditViolation.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateEditViolation.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditViolation.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateEditViolation.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditViolation.Properties.MaskSettings.Set("mask", "D");
            this.dateEditViolation.Size = new System.Drawing.Size(472, 20);
            this.dateEditViolation.StyleController = this.layoutControl1;
            this.dateEditViolation.TabIndex = 6;
            // 
            // comboBoxEditViolationType
            // 
            this.comboBoxEditViolationType.Location = new System.Drawing.Point(12, 148);
            this.comboBoxEditViolationType.Name = "comboBoxEditViolationType";
            this.comboBoxEditViolationType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditViolationType.Properties.Items.AddRange(new object[] {
            "Late",
            "Absent",
            "Early",
            "Violation",
            "Other"});
            this.comboBoxEditViolationType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditViolationType.Size = new System.Drawing.Size(472, 20);
            this.comboBoxEditViolationType.StyleController = this.layoutControl1;
            this.comboBoxEditViolationType.TabIndex = 7;
            this.comboBoxEditViolationType.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditViolationType_SelectedIndexChanged);
            // 
            // spinEditViolationValue
            // 
            this.spinEditViolationValue.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditViolationValue.Location = new System.Drawing.Point(12, 188);
            this.spinEditViolationValue.Name = "spinEditViolationValue";
            this.spinEditViolationValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditViolationValue.Properties.MaskSettings.Set("mask", "n0");
            this.spinEditViolationValue.Properties.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spinEditViolationValue.Size = new System.Drawing.Size(472, 20);
            this.spinEditViolationValue.StyleController = this.layoutControl1;
            this.spinEditViolationValue.TabIndex = 8;
            // 
            // comboBoxEditDeductionMethod
            // 
            this.comboBoxEditDeductionMethod.Location = new System.Drawing.Point(12, 228);
            this.comboBoxEditDeductionMethod.Name = "comboBoxEditDeductionMethod";
            this.comboBoxEditDeductionMethod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditDeductionMethod.Properties.Items.AddRange(new object[] {
            "Fixed",
            "Percentage",
            "Days",
            "Hours"});
            this.comboBoxEditDeductionMethod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditDeductionMethod.Size = new System.Drawing.Size(472, 20);
            this.comboBoxEditDeductionMethod.StyleController = this.layoutControl1;
            this.comboBoxEditDeductionMethod.TabIndex = 9;
            this.comboBoxEditDeductionMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditDeductionMethod_SelectedIndexChanged);
            // 
            // spinEditDeductionValue
            // 
            this.spinEditDeductionValue.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditDeductionValue.Location = new System.Drawing.Point(12, 268);
            this.spinEditDeductionValue.Name = "spinEditDeductionValue";
            this.spinEditDeductionValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditDeductionValue.Properties.DisplayFormat.FormatString = "n2";
            this.spinEditDeductionValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditDeductionValue.Properties.EditFormat.FormatString = "n2";
            this.spinEditDeductionValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditDeductionValue.Properties.MaskSettings.Set("mask", "n2");
            this.spinEditDeductionValue.Properties.MaxValue = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.spinEditDeductionValue.Size = new System.Drawing.Size(472, 20);
            this.spinEditDeductionValue.StyleController = this.layoutControl1;
            this.spinEditDeductionValue.TabIndex = 10;
            // 
            // memoEditDescription
            // 
            this.memoEditDescription.Location = new System.Drawing.Point(12, 308);
            this.memoEditDescription.Name = "memoEditDescription";
            this.memoEditDescription.Properties.MaxLength = 255;
            this.memoEditDescription.Size = new System.Drawing.Size(472, 16);
            this.memoEditDescription.StyleController = this.layoutControl1;
            this.memoEditDescription.TabIndex = 11;
            // 
            // simpleButtonSave
            // 
            this.simpleButtonSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonSave.ImageOptions.Image")));
            this.simpleButtonSave.Location = new System.Drawing.Point(12, 328);
            this.simpleButtonSave.Name = "simpleButtonSave";
            this.simpleButtonSave.Size = new System.Drawing.Size(234, 22);
            this.simpleButtonSave.StyleController = this.layoutControl1;
            this.simpleButtonSave.TabIndex = 12;
            this.simpleButtonSave.Text = "حفظ";
            this.simpleButtonSave.Click += new System.EventHandler(this.simpleButtonSave_Click);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonCancel.ImageOptions.Image")));
            this.simpleButtonCancel.Location = new System.Drawing.Point(250, 328);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(234, 22);
            this.simpleButtonCancel.StyleController = this.layoutControl1;
            this.simpleButtonCancel.TabIndex = 13;
            this.simpleButtonCancel.Text = "إلغاء";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemEmployee,
            this.layoutControlItemRule,
            this.layoutControlItemViolationDate,
            this.layoutControlItemViolationType,
            this.layoutControlItemViolationValue,
            this.layoutControlItemDeductionMethod,
            this.layoutControlItemDeductionValue,
            this.layoutControlItemDescription,
            this.layoutControlItemSave,
            this.layoutControlItemCancel,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(584, 361);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemEmployee
            // 
            this.layoutControlItemEmployee.Control = this.lookUpEditEmployee;
            this.layoutControlItemEmployee.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemEmployee.Name = "layoutControlItemEmployee";
            this.layoutControlItemEmployee.Size = new System.Drawing.Size(564, 40);
            this.layoutControlItemEmployee.Text = "الموظف:";
            this.layoutControlItemEmployee.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemEmployee.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItemRule
            // 
            this.layoutControlItemRule.Control = this.lookUpEditRule;
            this.layoutControlItemRule.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItemRule.Name = "layoutControlItemRule";
            this.layoutControlItemRule.Size = new System.Drawing.Size(564, 40);
            this.layoutControlItemRule.Text = "قاعدة الخصم:";
            this.layoutControlItemRule.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemRule.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItemViolationDate
            // 
            this.layoutControlItemViolationDate.Control = this.dateEditViolation;
            this.layoutControlItemViolationDate.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItemViolationDate.Name = "layoutControlItemViolationDate";
            this.layoutControlItemViolationDate.Size = new System.Drawing.Size(564, 40);
            this.layoutControlItemViolationDate.Text = "تاريخ المخالفة:";
            this.layoutControlItemViolationDate.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemViolationDate.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItemViolationType
            // 
            this.layoutControlItemViolationType.Control = this.comboBoxEditViolationType;
            this.layoutControlItemViolationType.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItemViolationType.Name = "layoutControlItemViolationType";
            this.layoutControlItemViolationType.Size = new System.Drawing.Size(564, 40);
            this.layoutControlItemViolationType.Text = "نوع المخالفة:";
            this.layoutControlItemViolationType.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemViolationType.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItemViolationValue
            // 
            this.layoutControlItemViolationValue.Control = this.spinEditViolationValue;
            this.layoutControlItemViolationValue.Location = new System.Drawing.Point(0, 160);
            this.layoutControlItemViolationValue.Name = "layoutControlItemViolationValue";
            this.layoutControlItemViolationValue.Size = new System.Drawing.Size(564, 40);
            this.layoutControlItemViolationValue.Text = "قيمة المخالفة (دقائق):";
            this.layoutControlItemViolationValue.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemViolationValue.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItemDeductionMethod
            // 
            this.layoutControlItemDeductionMethod.Control = this.comboBoxEditDeductionMethod;
            this.layoutControlItemDeductionMethod.Location = new System.Drawing.Point(0, 200);
            this.layoutControlItemDeductionMethod.Name = "layoutControlItemDeductionMethod";
            this.layoutControlItemDeductionMethod.Size = new System.Drawing.Size(564, 40);
            this.layoutControlItemDeductionMethod.Text = "طريقة الخصم:";
            this.layoutControlItemDeductionMethod.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemDeductionMethod.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItemDeductionValue
            // 
            this.layoutControlItemDeductionValue.Control = this.spinEditDeductionValue;
            this.layoutControlItemDeductionValue.Location = new System.Drawing.Point(0, 240);
            this.layoutControlItemDeductionValue.Name = "layoutControlItemDeductionValue";
            this.layoutControlItemDeductionValue.Size = new System.Drawing.Size(564, 40);
            this.layoutControlItemDeductionValue.Text = "قيمة الخصم:";
            this.layoutControlItemDeductionValue.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemDeductionValue.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItemDescription
            // 
            this.layoutControlItemDescription.Control = this.memoEditDescription;
            this.layoutControlItemDescription.Location = new System.Drawing.Point(0, 280);
            this.layoutControlItemDescription.Name = "layoutControlItemDescription";
            this.layoutControlItemDescription.Size = new System.Drawing.Size(564, 36);
            this.layoutControlItemDescription.Text = "الوصف:";
            this.layoutControlItemDescription.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemDescription.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.simpleButtonSave;
            this.layoutControlItemSave.Location = new System.Drawing.Point(0, 316);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(238, 26);
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.simpleButtonCancel;
            this.layoutControlItemCancel.Location = new System.Drawing.Point(238, 316);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(238, 26);
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(476, 316);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(88, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // DeductionForm
            // 
            this.AcceptButton = this.simpleButtonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButtonCancel;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("DeductionForm.IconOptions.Image")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeductionForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "خصم جديد";
            this.Load += new System.EventHandler(this.DeductionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditViolation.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditViolation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditViolationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditViolationValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDeductionMethod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditDeductionValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemViolationDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemViolationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemViolationValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDeductionMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDeductionValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditEmployee;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditRule;
        private DevExpress.XtraEditors.DateEdit dateEditViolation;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditViolationType;
        private DevExpress.XtraEditors.SpinEdit spinEditViolationValue;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditDeductionMethod;
        private DevExpress.XtraEditors.SpinEdit spinEditDeductionValue;
        private DevExpress.XtraEditors.MemoEdit memoEditDescription;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSave;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemEmployee;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRule;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemViolationDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemViolationType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemViolationValue;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDeductionMethod;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDeductionValue;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDescription;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}