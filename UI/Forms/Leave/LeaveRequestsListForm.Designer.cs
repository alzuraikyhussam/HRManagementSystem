namespace HR.UI.Forms.Leave
{
    partial class LeaveRequestsListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeaveRequestsListForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelTitle = new DevExpress.XtraEditors.LabelControl();
            this.buttonRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.buttonReject = new DevExpress.XtraEditors.SimpleButton();
            this.buttonApprove = new DevExpress.XtraEditors.SimpleButton();
            this.buttonView = new DevExpress.XtraEditors.SimpleButton();
            this.buttonAdd = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.radioGroupLeaveStatus = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlLeaveRequests = new DevExpress.XtraGrid.GridControl();
            this.gridViewLeaveRequests = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupLeaveStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLeaveRequests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLeaveRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelTitle);
            this.panelControl1.Controls.Add(this.buttonRefresh);
            this.panelControl1.Controls.Add(this.buttonReject);
            this.panelControl1.Controls.Add(this.buttonApprove);
            this.panelControl1.Controls.Add(this.buttonView);
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
            this.labelTitle.Text = "طلبات الإجازات";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.ImageOptions.Image")));
            this.buttonRefresh.Location = new System.Drawing.Point(170, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(84, 34);
            this.buttonRefresh.TabIndex = 4;
            this.buttonRefresh.Text = "تحديث";
            // 
            // buttonReject
            // 
            this.buttonReject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReject.Enabled = false;
            this.buttonReject.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonReject.ImageOptions.Image")));
            this.buttonReject.Location = new System.Drawing.Point(270, 12);
            this.buttonReject.Name = "buttonReject";
            this.buttonReject.Size = new System.Drawing.Size(84, 34);
            this.buttonReject.TabIndex = 3;
            this.buttonReject.Text = "رفض";
            // 
            // buttonApprove
            // 
            this.buttonApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApprove.Enabled = false;
            this.buttonApprove.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonApprove.ImageOptions.Image")));
            this.buttonApprove.Location = new System.Drawing.Point(370, 12);
            this.buttonApprove.Name = "buttonApprove";
            this.buttonApprove.Size = new System.Drawing.Size(84, 34);
            this.buttonApprove.TabIndex = 2;
            this.buttonApprove.Text = "موافقة";
            // 
            // buttonView
            // 
            this.buttonView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonView.Enabled = false;
            this.buttonView.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonView.ImageOptions.Image")));
            this.buttonView.Location = new System.Drawing.Point(470, 12);
            this.buttonView.Name = "buttonView";
            this.buttonView.Size = new System.Drawing.Size(84, 34);
            this.buttonView.TabIndex = 1;
            this.buttonView.Text = "عرض";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.ImageOptions.Image")));
            this.buttonAdd.Location = new System.Drawing.Point(570, 12);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(84, 34);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "إضافة";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.radioGroupLeaveStatus);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.dateEditTo);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.dateEditFrom);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 58);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(884, 56);
            this.panelControl2.TabIndex = 1;
            // 
            // radioGroupLeaveStatus
            // 
            this.radioGroupLeaveStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioGroupLeaveStatus.EditValue = 0;
            this.radioGroupLeaveStatus.Location = new System.Drawing.Point(14, 8);
            this.radioGroupLeaveStatus.Name = "radioGroupLeaveStatus";
            this.radioGroupLeaveStatus.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroupLeaveStatus.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupLeaveStatus.Properties.Columns = 5;
            this.radioGroupLeaveStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "الكل"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "قيد الانتظار"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "تمت الموافقة"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "مرفوض"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(4, "ملغي")});
            this.radioGroupLeaveStatus.Size = new System.Drawing.Size(471, 34);
            this.radioGroupLeaveStatus.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Location = new System.Drawing.Point(509, 18);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "الحالة:";
            // 
            // dateEditTo
            // 
            this.dateEditTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEditTo.EditValue = null;
            this.dateEditTo.Location = new System.Drawing.Point(568, 26);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateEditTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditTo.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateEditTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditTo.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dateEditTo.Size = new System.Drawing.Size(100, 20);
            this.dateEditTo.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Location = new System.Drawing.Point(674, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(22, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "إلى:";
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEditFrom.EditValue = null;
            this.dateEditFrom.Location = new System.Drawing.Point(708, 26);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateEditFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditFrom.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateEditFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditFrom.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dateEditFrom.Size = new System.Drawing.Size(100, 20);
            this.dateEditFrom.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Location = new System.Drawing.Point(821, 29);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(51, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "الفترة - من:";
            // 
            // gridControlLeaveRequests
            // 
            this.gridControlLeaveRequests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlLeaveRequests.Location = new System.Drawing.Point(0, 114);
            this.gridControlLeaveRequests.MainView = this.gridViewLeaveRequests;
            this.gridControlLeaveRequests.Name = "gridControlLeaveRequests";
            this.gridControlLeaveRequests.Size = new System.Drawing.Size(884, 447);
            this.gridControlLeaveRequests.TabIndex = 2;
            this.gridControlLeaveRequests.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLeaveRequests});
            // 
            // gridViewLeaveRequests
            // 
            this.gridViewLeaveRequests.GridControl = this.gridControlLeaveRequests;
            this.gridViewLeaveRequests.Name = "gridViewLeaveRequests";
            this.gridViewLeaveRequests.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewLeaveRequests.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewLeaveRequests.OptionsBehavior.Editable = false;
            this.gridViewLeaveRequests.OptionsBehavior.ReadOnly = true;
            this.gridViewLeaveRequests.OptionsFind.AlwaysVisible = true;
            this.gridViewLeaveRequests.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewLeaveRequests.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewLeaveRequests.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewLeaveRequests.OptionsView.ShowGroupPanel = false;
            // 
            // LeaveRequestsListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.gridControlLeaveRequests);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("LeaveRequestsListForm.IconOptions.Image")));
            this.Name = "LeaveRequestsListForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "طلبات الإجازات";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupLeaveStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLeaveRequests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLeaveRequests)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton buttonAdd;
        private DevExpress.XtraEditors.SimpleButton buttonView;
        private DevExpress.XtraEditors.SimpleButton buttonRefresh;
        private DevExpress.XtraEditors.SimpleButton buttonReject;
        private DevExpress.XtraEditors.SimpleButton buttonApprove;
        private DevExpress.XtraEditors.LabelControl labelTitle;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateEditTo;
        private DevExpress.XtraEditors.RadioGroup radioGroupLeaveStatus;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl gridControlLeaveRequests;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLeaveRequests;
    }
}