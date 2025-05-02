namespace HR.UI.Forms.Leave
{
    partial class LeaveTypeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeaveTypeForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.buttonSave = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkBoxIsActive = new DevExpress.XtraEditors.CheckEdit();
            this.checkBoxIsPaid = new DevExpress.XtraEditors.CheckEdit();
            this.checkBoxRequiresApproval = new DevExpress.XtraEditors.CheckEdit();
            this.colorComboBoxColor = new DevExpress.XtraEditors.ColorEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxPriority = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.spinEditMaxDays = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.spinEditDefaultDays = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEditDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxIsActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxIsPaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxRequiresApproval.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorComboBoxColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPriority.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMaxDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditDefaultDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.buttonCancel);
            this.panelControl1.Controls.Add(this.buttonSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 363);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(484, 48);
            this.panelControl1.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.ImageOptions.Image")));
            this.buttonCancel.Location = new System.Drawing.Point(310, 7);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 32);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "إلغاء";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.ImageOptions.Image")));
            this.buttonSave.Location = new System.Drawing.Point(397, 7);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 32);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "حفظ";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.checkBoxIsActive);
            this.groupControl1.Controls.Add(this.checkBoxIsPaid);
            this.groupControl1.Controls.Add(this.checkBoxRequiresApproval);
            this.groupControl1.Controls.Add(this.colorComboBoxColor);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.comboBoxPriority);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.spinEditMaxDays);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.spinEditDefaultDays);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.textEditDescription);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.textEditName);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(484, 363);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "بيانات نوع الإجازة";
            // 
            // checkBoxIsActive
            // 
            this.checkBoxIsActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIsActive.Location = new System.Drawing.Point(135, 332);
            this.checkBoxIsActive.Name = "checkBoxIsActive";
            this.checkBoxIsActive.Properties.Caption = "نشط";
            this.checkBoxIsActive.Size = new System.Drawing.Size(75, 20);
            this.checkBoxIsActive.TabIndex = 14;
            // 
            // checkBoxIsPaid
            // 
            this.checkBoxIsPaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIsPaid.Location = new System.Drawing.Point(222, 332);
            this.checkBoxIsPaid.Name = "checkBoxIsPaid";
            this.checkBoxIsPaid.Properties.Caption = "مدفوعة الأجر";
            this.checkBoxIsPaid.Size = new System.Drawing.Size(100, 20);
            this.checkBoxIsPaid.TabIndex = 13;
            // 
            // checkBoxRequiresApproval
            // 
            this.checkBoxRequiresApproval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxRequiresApproval.Location = new System.Drawing.Point(338, 332);
            this.checkBoxRequiresApproval.Name = "checkBoxRequiresApproval";
            this.checkBoxRequiresApproval.Properties.Caption = "تتطلب موافقة";
            this.checkBoxRequiresApproval.Size = new System.Drawing.Size(100, 20);
            this.checkBoxRequiresApproval.TabIndex = 12;
            // 
            // colorComboBoxColor
            // 
            this.colorComboBoxColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorComboBoxColor.EditValue = System.Drawing.Color.Empty;
            this.colorComboBoxColor.Location = new System.Drawing.Point(135, 290);
            this.colorComboBoxColor.Name = "colorComboBoxColor";
            this.colorComboBoxColor.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.colorComboBoxColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorComboBoxColor.Size = new System.Drawing.Size(248, 20);
            this.colorComboBoxColor.TabIndex = 11;
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl6.Location = new System.Drawing.Point(403, 293);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(23, 13);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "اللون:";
            // 
            // comboBoxPriority
            // 
            this.comboBoxPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPriority.Location = new System.Drawing.Point(135, 256);
            this.comboBoxPriority.Name = "comboBoxPriority";
            this.comboBoxPriority.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxPriority.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxPriority.Size = new System.Drawing.Size(248, 20);
            this.comboBoxPriority.TabIndex = 9;
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl5.Location = new System.Drawing.Point(400, 259);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(35, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "الأولوية:";
            // 
            // spinEditMaxDays
            // 
            this.spinEditMaxDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.spinEditMaxDays.EditValue = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.spinEditMaxDays.Location = new System.Drawing.Point(135, 223);
            this.spinEditMaxDays.Name = "spinEditMaxDays";
            this.spinEditMaxDays.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditMaxDays.Properties.MaxValue = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.spinEditMaxDays.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditMaxDays.Size = new System.Drawing.Size(248, 20);
            this.spinEditMaxDays.TabIndex = 7;
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Location = new System.Drawing.Point(385, 226);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(86, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "الحد الأقصى للأيام:";
            // 
            // spinEditDefaultDays
            // 
            this.spinEditDefaultDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.spinEditDefaultDays.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditDefaultDays.Location = new System.Drawing.Point(135, 190);
            this.spinEditDefaultDays.Name = "spinEditDefaultDays";
            this.spinEditDefaultDays.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditDefaultDays.Properties.MaxValue = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.spinEditDefaultDays.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditDefaultDays.Size = new System.Drawing.Size(248, 20);
            this.spinEditDefaultDays.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Location = new System.Drawing.Point(385, 193);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(78, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "الأيام الافتراضية:";
            // 
            // textEditDescription
            // 
            this.textEditDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditDescription.Location = new System.Drawing.Point(135, 88);
            this.textEditDescription.Name = "textEditDescription";
            this.textEditDescription.Size = new System.Drawing.Size(248, 96);
            this.textEditDescription.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Location = new System.Drawing.Point(401, 90);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "الوصف:";
            // 
            // textEditName
            // 
            this.textEditName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditName.Location = new System.Drawing.Point(135, 56);
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(248, 20);
            this.textEditName.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Location = new System.Drawing.Point(404, 59);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(31, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "الاسم:";
            // 
            // LeaveTypeForm
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(484, 411);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("LeaveTypeForm.IconOptions.Image")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LeaveTypeForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "نوع الإجازة";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxIsActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxIsPaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxRequiresApproval.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorComboBoxColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPriority.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMaxDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditDefaultDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton buttonCancel;
        private DevExpress.XtraEditors.SimpleButton buttonSave;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraEditors.MemoEdit textEditDescription;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit spinEditDefaultDays;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit spinEditMaxDays;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxPriority;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ColorEdit colorComboBoxColor;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.CheckEdit checkBoxRequiresApproval;
        private DevExpress.XtraEditors.CheckEdit checkBoxIsPaid;
        private DevExpress.XtraEditors.CheckEdit checkBoxIsActive;
    }
}