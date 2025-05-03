namespace HR.UI.Forms.Leave
{
    partial class LeaveBalancesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeaveBalancesForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelTitle = new DevExpress.XtraEditors.LabelControl();
            this.buttonRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.buttonEdit = new DevExpress.XtraEditors.SimpleButton();
            this.buttonAdd = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.comboBoxYear = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxLeaveTypes = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEmployees = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlLeaveBalances = new DevExpress.XtraGrid.GridControl();
            this.gridViewLeaveBalances = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxLeaveTypes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEmployees.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLeaveBalances)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLeaveBalances)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelTitle);
            this.panelControl1.Controls.Add(this.buttonRefresh);
            this.panelControl1.Controls.Add(this.buttonEdit);
            this.panelControl1.Controls.Add(this.buttonAdd);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(884, 58);
            this.panelControl1.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Appearance.Options.UseFont = true;
            this.labelTitle.Location = new System.Drawing.Point(752, 12);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(120, 23);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "أرصدة الإجازات";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.ImageOptions.Image")));
            this.buttonRefresh.Location = new System.Drawing.Point(270, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(84, 34);
            this.buttonRefresh.TabIndex = 4;
            this.buttonRefresh.Text = "تحديث";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Enabled = false;
            this.buttonEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonEdit.ImageOptions.Image")));
            this.buttonEdit.Location = new System.Drawing.Point(370, 12);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(84, 34);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "تعديل";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.ImageOptions.Image")));
            this.buttonAdd.Location = new System.Drawing.Point(470, 12);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(84, 34);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "إضافة";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.comboBoxYear);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.comboBoxLeaveTypes);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.comboBoxEmployees);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 58);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(884, 56);
            this.panelControl2.TabIndex = 1;
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxYear.EditValue = "2025";
            this.comboBoxYear.Location = new System.Drawing.Point(142, 18);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxYear.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxYear.Size = new System.Drawing.Size(120, 20);
            this.comboBoxYear.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Location = new System.Drawing.Point(268, 21);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(29, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "السنة:";
            // 
            // comboBoxLeaveTypes
            // 
            this.comboBoxLeaveTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLeaveTypes.EditValue = "الكل";
            this.comboBoxLeaveTypes.Location = new System.Drawing.Point(318, 18);
            this.comboBoxLeaveTypes.Name = "comboBoxLeaveTypes";
            this.comboBoxLeaveTypes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxLeaveTypes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxLeaveTypes.Size = new System.Drawing.Size(150, 20);
            this.comboBoxLeaveTypes.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Location = new System.Drawing.Point(474, 21);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(57, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "نوع الإجازة:";
            // 
            // comboBoxEmployees
            // 
            this.comboBoxEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEmployees.EditValue = "الكل";
            this.comboBoxEmployees.Location = new System.Drawing.Point(550, 18);
            this.comboBoxEmployees.Name = "comboBoxEmployees";
            this.comboBoxEmployees.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEmployees.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEmployees.Size = new System.Drawing.Size(200, 20);
            this.comboBoxEmployees.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Location = new System.Drawing.Point(756, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "الموظف:";
            // 
            // gridControlLeaveBalances
            // 
            this.gridControlLeaveBalances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlLeaveBalances.Location = new System.Drawing.Point(0, 114);
            this.gridControlLeaveBalances.MainView = this.gridViewLeaveBalances;
            this.gridControlLeaveBalances.Name = "gridControlLeaveBalances";
            this.gridControlLeaveBalances.Size = new System.Drawing.Size(884, 447);
            this.gridControlLeaveBalances.TabIndex = 2;
            this.gridControlLeaveBalances.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLeaveBalances});
            // 
            // gridViewLeaveBalances
            // 
            this.gridViewLeaveBalances.GridControl = this.gridControlLeaveBalances;
            this.gridViewLeaveBalances.Name = "gridViewLeaveBalances";
            this.gridViewLeaveBalances.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewLeaveBalances.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewLeaveBalances.OptionsBehavior.Editable = false;
            this.gridViewLeaveBalances.OptionsBehavior.ReadOnly = true;
            this.gridViewLeaveBalances.OptionsFind.AlwaysVisible = true;
            this.gridViewLeaveBalances.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewLeaveBalances.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewLeaveBalances.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewLeaveBalances.OptionsView.ShowGroupPanel = false;
            // 
            // LeaveBalancesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.gridControlLeaveBalances);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("LeaveBalancesForm.IconOptions.Image")));
            this.Name = "LeaveBalancesForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "أرصدة الإجازات";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxLeaveTypes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEmployees.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLeaveBalances)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLeaveBalances)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton buttonAdd;
        private DevExpress.XtraEditors.SimpleButton buttonEdit;
        private DevExpress.XtraEditors.SimpleButton buttonRefresh;
        private DevExpress.XtraEditors.LabelControl labelTitle;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEmployees;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxLeaveTypes;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxYear;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl gridControlLeaveBalances;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLeaveBalances;
    }
}