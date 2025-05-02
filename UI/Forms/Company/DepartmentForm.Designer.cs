namespace HR.UI.Forms.Company
{
    partial class DepartmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepartmentForm));
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.lookUpEditParentDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditManagerPosition = new DevExpress.XtraEditors.LookUpEdit();
            this.textEditLocation = new DevExpress.XtraEditors.TextEdit();
            this.checkEditIsActive = new DevExpress.XtraEditors.CheckEdit();
            this.memoEditDescription = new DevExpress.XtraEditors.MemoEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.buttonSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupDepartmentInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditParentDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditIsActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditOrder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupDepartmentInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.textEditName);
            this.layoutControl.Controls.Add(this.textEditCode);
            this.layoutControl.Controls.Add(this.lookUpEditParentDepartment);
            this.layoutControl.Controls.Add(this.checkEditIsActive);
            this.layoutControl.Controls.Add(this.memoEditDescription);
            this.layoutControl.Controls.Add(this.spinEditOrder);
            this.layoutControl.Controls.Add(this.panelControl1);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1108, 360, 650, 400);
            this.layoutControl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.layoutControl.Root = this.layoutControlGroup1;
            this.layoutControl.Size = new System.Drawing.Size(600, 400);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(118, 45);
            this.textEditName.Name = "textEditName";
            this.textEditName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.textEditName.Properties.Appearance.Options.UseFont = true;
            this.textEditName.Size = new System.Drawing.Size(370, 24);
            this.textEditName.StyleController = this.layoutControl;
            this.textEditName.TabIndex = 4;
            // 
            // textEditCode
            // 
            this.textEditCode.Location = new System.Drawing.Point(118, 73);
            this.textEditCode.Name = "textEditCode";
            this.textEditCode.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.textEditCode.Properties.Appearance.Options.UseFont = true;
            this.textEditCode.Size = new System.Drawing.Size(370, 24);
            this.textEditCode.StyleController = this.layoutControl;
            this.textEditCode.TabIndex = 5;
            // 
            // lookUpEditParentDepartment
            // 
            this.lookUpEditParentDepartment.Location = new System.Drawing.Point(118, 101);
            this.lookUpEditParentDepartment.Name = "lookUpEditParentDepartment";
            this.lookUpEditParentDepartment.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lookUpEditParentDepartment.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditParentDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditParentDepartment.Properties.NullText = "";
            this.lookUpEditParentDepartment.Size = new System.Drawing.Size(370, 24);
            this.lookUpEditParentDepartment.StyleController = this.layoutControl;
            this.lookUpEditParentDepartment.TabIndex = 6;
            // 
            // checkEditIsActive
            // 
            this.checkEditIsActive.EditValue = true;
            this.checkEditIsActive.Location = new System.Drawing.Point(24, 129);
            this.checkEditIsActive.Name = "checkEditIsActive";
            this.checkEditIsActive.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.checkEditIsActive.Properties.Appearance.Options.UseFont = true;
            this.checkEditIsActive.Properties.Caption = "نشط";
            this.checkEditIsActive.Size = new System.Drawing.Size(464, 21);
            this.checkEditIsActive.StyleController = this.layoutControl;
            this.checkEditIsActive.TabIndex = 7;
            // 
            // memoEditDescription
            // 
            this.memoEditDescription.Location = new System.Drawing.Point(118, 154);
            this.memoEditDescription.Name = "memoEditDescription";
            this.memoEditDescription.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.memoEditDescription.Properties.Appearance.Options.UseFont = true;
            this.memoEditDescription.Size = new System.Drawing.Size(370, 128);
            this.memoEditDescription.StyleController = this.layoutControl;
            this.memoEditDescription.TabIndex = 8;
            // 
            // spinEditOrder
            // 
            this.spinEditOrder.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditOrder.Location = new System.Drawing.Point(118, 286);
            this.spinEditOrder.Name = "spinEditOrder";
            this.spinEditOrder.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.spinEditOrder.Properties.Appearance.Options.UseFont = true;
            this.spinEditOrder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditOrder.Properties.IsFloatValue = false;
            this.spinEditOrder.Properties.Mask.EditMask = "N00";
            this.spinEditOrder.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinEditOrder.Size = new System.Drawing.Size(370, 24);
            this.spinEditOrder.StyleController = this.layoutControl;
            this.spinEditOrder.TabIndex = 9;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.buttonCancel);
            this.panelControl1.Controls.Add(this.buttonSave);
            this.panelControl1.Location = new System.Drawing.Point(12, 326);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(576, 62);
            this.panelControl1.TabIndex = 10;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonCancel.Appearance.Options.UseFont = true;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonCancel.ImageOptions.SvgImage")));
            this.buttonCancel.Location = new System.Drawing.Point(0, 0);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(120, 62);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "إلغاء";
            // 
            // buttonSave
            // 
            this.buttonSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.buttonSave.Appearance.Options.UseFont = true;
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonSave.ImageOptions.SvgImage")));
            this.buttonSave.Location = new System.Drawing.Point(456, 0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(120, 62);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "حفظ";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupDepartmentInfo,
            this.layoutControlItem7});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(600, 400);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroupDepartmentInfo
            // 
            this.layoutControlGroupDepartmentInfo.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroupDepartmentInfo.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroupDepartmentInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroupDepartmentInfo.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupDepartmentInfo.Name = "layoutControlGroupDepartmentInfo";
            this.layoutControlGroupDepartmentInfo.Size = new System.Drawing.Size(580, 314);
            this.layoutControlGroupDepartmentInfo.Text = "بيانات القسم";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.textEditName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(556, 28);
            this.layoutControlItem1.Text = "اسم القسم";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(93, 17);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.textEditCode;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(556, 28);
            this.layoutControlItem2.Text = "كود القسم";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(93, 17);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.lookUpEditParentDepartment;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 56);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(556, 28);
            this.layoutControlItem3.Text = "القسم الأعلى";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(93, 17);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.checkEditIsActive;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 84);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(556, 25);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.Control = this.memoEditDescription;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 109);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(556, 132);
            this.layoutControlItem5.Text = "الوصف";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(93, 17);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem6.Control = this.spinEditOrder;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 241);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(556, 28);
            this.layoutControlItem6.Text = "ترتيب العرض";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(93, 17);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.panelControl1;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 314);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(580, 66);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // DepartmentForm
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.layoutControl);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("DepartmentForm.IconOptions.SvgImage")));
            this.Name = "DepartmentForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إدارة القسم";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditParentDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditIsActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditOrder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupDepartmentInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraEditors.TextEdit textEditCode;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditParentDepartment;
        private DevExpress.XtraEditors.CheckEdit checkEditIsActive;
        private DevExpress.XtraEditors.MemoEdit memoEditDescription;
        private DevExpress.XtraEditors.SpinEdit spinEditOrder;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupDepartmentInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton buttonCancel;
        private DevExpress.XtraEditors.SimpleButton buttonSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}