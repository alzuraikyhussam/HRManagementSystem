namespace HR.UI.Forms.Leave
{
    partial class LeaveTypesListForm
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeaveTypesListForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.buttonDelete = new DevExpress.XtraEditors.SimpleButton();
            this.buttonEdit = new DevExpress.XtraEditors.SimpleButton();
            this.buttonAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlLeaveTypes = new DevExpress.XtraGrid.GridControl();
            this.gridViewLeaveTypes = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLeaveTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLeaveTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.buttonRefresh);
            this.panelControl1.Controls.Add(this.buttonDelete);
            this.panelControl1.Controls.Add(this.buttonEdit);
            this.panelControl1.Controls.Add(this.buttonAdd);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(884, 58);
            this.panelControl1.TabIndex = 0;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.ImageOptions.Image")));
            this.buttonRefresh.Location = new System.Drawing.Point(350, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(84, 34);
            this.buttonRefresh.TabIndex = 4;
            this.buttonRefresh.Text = "تحديث";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Enabled = false;
            this.buttonDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.ImageOptions.Image")));
            this.buttonDelete.Location = new System.Drawing.Point(450, 12);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(84, 34);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "حذف";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Enabled = false;
            this.buttonEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonEdit.ImageOptions.Image")));
            this.buttonEdit.Location = new System.Drawing.Point(550, 12);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(84, 34);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "تعديل";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.ImageOptions.Image")));
            this.buttonAdd.Location = new System.Drawing.Point(650, 12);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(84, 34);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "إضافة";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(752, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(120, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "أنواع الإجازات";
            // 
            // gridControlLeaveTypes
            // 
            this.gridControlLeaveTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlLeaveTypes.Location = new System.Drawing.Point(0, 58);
            this.gridControlLeaveTypes.MainView = this.gridViewLeaveTypes;
            this.gridControlLeaveTypes.Name = "gridControlLeaveTypes";
            this.gridControlLeaveTypes.Size = new System.Drawing.Size(884, 503);
            this.gridControlLeaveTypes.TabIndex = 1;
            this.gridControlLeaveTypes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLeaveTypes});
            // 
            // gridViewLeaveTypes
            // 
            this.gridViewLeaveTypes.GridControl = this.gridControlLeaveTypes;
            this.gridViewLeaveTypes.Name = "gridViewLeaveTypes";
            this.gridViewLeaveTypes.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewLeaveTypes.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewLeaveTypes.OptionsBehavior.Editable = false;
            this.gridViewLeaveTypes.OptionsBehavior.ReadOnly = true;
            this.gridViewLeaveTypes.OptionsFind.AlwaysVisible = true;
            this.gridViewLeaveTypes.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewLeaveTypes.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewLeaveTypes.OptionsView.ShowGroupPanel = false;
            // 
            // LeaveTypesListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.gridControlLeaveTypes);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("LeaveTypesListForm.IconOptions.Image")));
            this.Name = "LeaveTypesListForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إدارة أنواع الإجازات";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLeaveTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLeaveTypes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton buttonRefresh;
        private DevExpress.XtraEditors.SimpleButton buttonDelete;
        private DevExpress.XtraEditors.SimpleButton buttonEdit;
        private DevExpress.XtraEditors.SimpleButton buttonAdd;
        private DevExpress.XtraGrid.GridControl gridControlLeaveTypes;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLeaveTypes;
    }
}