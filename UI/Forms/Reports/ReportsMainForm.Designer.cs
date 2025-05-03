namespace HR.UI.Forms.Reports
{
    partial class ReportsMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsMainForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnDepartmentSummary = new DevExpress.XtraEditors.SimpleButton();
            this.btnEmployeeDetails = new DevExpress.XtraEditors.SimpleButton();
            this.btnEmployeesList = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnLeaveBalance = new DevExpress.XtraEditors.SimpleButton();
            this.btnLeaveReport = new DevExpress.XtraEditors.SimpleButton();
            this.btnAttendanceDetails = new DevExpress.XtraEditors.SimpleButton();
            this.btnAttendanceSummary = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnDeductionReport = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalaryAnalysis = new DevExpress.XtraEditors.SimpleButton();
            this.btnPayrollReport = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.btnPerformanceReport = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.btnCustomReport = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lblTitle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(800, 60);
            this.panelControl1.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(800, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "إدارة التقارير";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.btnDepartmentSummary);
            this.groupControl1.Controls.Add(this.btnEmployeeDetails);
            this.groupControl1.Controls.Add(this.btnEmployeesList);
            this.groupControl1.Location = new System.Drawing.Point(12, 77);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(776, 80);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "تقارير القوى العاملة";
            // 
            // btnDepartmentSummary
            // 
            this.btnDepartmentSummary.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnDepartmentSummary.Appearance.Options.UseFont = true;
            this.btnDepartmentSummary.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDepartmentSummary.ImageOptions.Image")));
            this.btnDepartmentSummary.Location = new System.Drawing.Point(151, 30);
            this.btnDepartmentSummary.Name = "btnDepartmentSummary";
            this.btnDepartmentSummary.Size = new System.Drawing.Size(155, 36);
            this.btnDepartmentSummary.TabIndex = 2;
            this.btnDepartmentSummary.Text = "ملخص الإدارات";
            // 
            // btnEmployeeDetails
            // 
            this.btnEmployeeDetails.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnEmployeeDetails.Appearance.Options.UseFont = true;
            this.btnEmployeeDetails.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEmployeeDetails.ImageOptions.Image")));
            this.btnEmployeeDetails.Location = new System.Drawing.Point(312, 30);
            this.btnEmployeeDetails.Name = "btnEmployeeDetails";
            this.btnEmployeeDetails.Size = new System.Drawing.Size(155, 36);
            this.btnEmployeeDetails.TabIndex = 1;
            this.btnEmployeeDetails.Text = "تفاصيل موظف";
            // 
            // btnEmployeesList
            // 
            this.btnEmployeesList.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnEmployeesList.Appearance.Options.UseFont = true;
            this.btnEmployeesList.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEmployeesList.ImageOptions.Image")));
            this.btnEmployeesList.Location = new System.Drawing.Point(473, 30);
            this.btnEmployeesList.Name = "btnEmployeesList";
            this.btnEmployeesList.Size = new System.Drawing.Size(155, 36);
            this.btnEmployeesList.TabIndex = 0;
            this.btnEmployeesList.Text = "قائمة الموظفين";
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.btnLeaveBalance);
            this.groupControl2.Controls.Add(this.btnLeaveReport);
            this.groupControl2.Controls.Add(this.btnAttendanceDetails);
            this.groupControl2.Controls.Add(this.btnAttendanceSummary);
            this.groupControl2.Location = new System.Drawing.Point(12, 163);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(776, 80);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "تقارير الحضور والإجازات";
            // 
            // btnLeaveBalance
            // 
            this.btnLeaveBalance.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnLeaveBalance.Appearance.Options.UseFont = true;
            this.btnLeaveBalance.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLeaveBalance.ImageOptions.Image")));
            this.btnLeaveBalance.Location = new System.Drawing.Point(151, 30);
            this.btnLeaveBalance.Name = "btnLeaveBalance";
            this.btnLeaveBalance.Size = new System.Drawing.Size(155, 36);
            this.btnLeaveBalance.TabIndex = 3;
            this.btnLeaveBalance.Text = "أرصدة الإجازات";
            // 
            // btnLeaveReport
            // 
            this.btnLeaveReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnLeaveReport.Appearance.Options.UseFont = true;
            this.btnLeaveReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLeaveReport.ImageOptions.Image")));
            this.btnLeaveReport.Location = new System.Drawing.Point(312, 30);
            this.btnLeaveReport.Name = "btnLeaveReport";
            this.btnLeaveReport.Size = new System.Drawing.Size(155, 36);
            this.btnLeaveReport.TabIndex = 2;
            this.btnLeaveReport.Text = "تقرير الإجازات";
            // 
            // btnAttendanceDetails
            // 
            this.btnAttendanceDetails.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnAttendanceDetails.Appearance.Options.UseFont = true;
            this.btnAttendanceDetails.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAttendanceDetails.ImageOptions.Image")));
            this.btnAttendanceDetails.Location = new System.Drawing.Point(473, 30);
            this.btnAttendanceDetails.Name = "btnAttendanceDetails";
            this.btnAttendanceDetails.Size = new System.Drawing.Size(155, 36);
            this.btnAttendanceDetails.TabIndex = 1;
            this.btnAttendanceDetails.Text = "تفاصيل الحضور";
            // 
            // btnAttendanceSummary
            // 
            this.btnAttendanceSummary.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnAttendanceSummary.Appearance.Options.UseFont = true;
            this.btnAttendanceSummary.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAttendanceSummary.ImageOptions.Image")));
            this.btnAttendanceSummary.Location = new System.Drawing.Point(634, 30);
            this.btnAttendanceSummary.Name = "btnAttendanceSummary";
            this.btnAttendanceSummary.Size = new System.Drawing.Size(137, 36);
            this.btnAttendanceSummary.TabIndex = 0;
            this.btnAttendanceSummary.Text = "ملخص الحضور";
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.Controls.Add(this.btnDeductionReport);
            this.groupControl3.Controls.Add(this.btnSalaryAnalysis);
            this.groupControl3.Controls.Add(this.btnPayrollReport);
            this.groupControl3.Location = new System.Drawing.Point(12, 249);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(776, 80);
            this.groupControl3.TabIndex = 2;
            this.groupControl3.Text = "تقارير الرواتب";
            // 
            // btnDeductionReport
            // 
            this.btnDeductionReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnDeductionReport.Appearance.Options.UseFont = true;
            this.btnDeductionReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDeductionReport.ImageOptions.Image")));
            this.btnDeductionReport.Location = new System.Drawing.Point(312, 30);
            this.btnDeductionReport.Name = "btnDeductionReport";
            this.btnDeductionReport.Size = new System.Drawing.Size(155, 36);
            this.btnDeductionReport.TabIndex = 2;
            this.btnDeductionReport.Text = "تقرير الخصومات";
            // 
            // btnSalaryAnalysis
            // 
            this.btnSalaryAnalysis.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnSalaryAnalysis.Appearance.Options.UseFont = true;
            this.btnSalaryAnalysis.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSalaryAnalysis.ImageOptions.Image")));
            this.btnSalaryAnalysis.Location = new System.Drawing.Point(473, 30);
            this.btnSalaryAnalysis.Name = "btnSalaryAnalysis";
            this.btnSalaryAnalysis.Size = new System.Drawing.Size(155, 36);
            this.btnSalaryAnalysis.TabIndex = 1;
            this.btnSalaryAnalysis.Text = "تحليل الرواتب";
            // 
            // btnPayrollReport
            // 
            this.btnPayrollReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnPayrollReport.Appearance.Options.UseFont = true;
            this.btnPayrollReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPayrollReport.ImageOptions.Image")));
            this.btnPayrollReport.Location = new System.Drawing.Point(634, 30);
            this.btnPayrollReport.Name = "btnPayrollReport";
            this.btnPayrollReport.Size = new System.Drawing.Size(137, 36);
            this.btnPayrollReport.TabIndex = 0;
            this.btnPayrollReport.Text = "كشف الرواتب";
            // 
            // groupControl4
            // 
            this.groupControl4.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.groupControl4.AppearanceCaption.Options.UseFont = true;
            this.groupControl4.Controls.Add(this.btnPerformanceReport);
            this.groupControl4.Location = new System.Drawing.Point(12, 335);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(776, 80);
            this.groupControl4.TabIndex = 3;
            this.groupControl4.Text = "تقارير الأداء";
            // 
            // btnPerformanceReport
            // 
            this.btnPerformanceReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnPerformanceReport.Appearance.Options.UseFont = true;
            this.btnPerformanceReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPerformanceReport.ImageOptions.Image")));
            this.btnPerformanceReport.Location = new System.Drawing.Point(473, 30);
            this.btnPerformanceReport.Name = "btnPerformanceReport";
            this.btnPerformanceReport.Size = new System.Drawing.Size(155, 36);
            this.btnPerformanceReport.TabIndex = 0;
            this.btnPerformanceReport.Text = "تقرير الأداء";
            // 
            // groupControl5
            // 
            this.groupControl5.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.groupControl5.AppearanceCaption.Options.UseFont = true;
            this.groupControl5.Controls.Add(this.btnCustomReport);
            this.groupControl5.Location = new System.Drawing.Point(12, 421);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(776, 80);
            this.groupControl5.TabIndex = 4;
            this.groupControl5.Text = "التقارير المخصصة";
            // 
            // btnCustomReport
            // 
            this.btnCustomReport.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnCustomReport.Appearance.Options.UseFont = true;
            this.btnCustomReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCustomReport.ImageOptions.Image")));
            this.btnCustomReport.Location = new System.Drawing.Point(473, 30);
            this.btnCustomReport.Name = "btnCustomReport";
            this.btnCustomReport.Size = new System.Drawing.Size(155, 36);
            this.btnCustomReport.TabIndex = 0;
            this.btnCustomReport.Text = "مولد التقارير المخصصة";
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.Image")));
            this.btnClose.Location = new System.Drawing.Point(344, 516);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(137, 36);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "إغلاق";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ReportsMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(800, 564);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupControl5);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("ReportsMainForm.IconOptions.Image")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportsMainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إدارة التقارير";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnDepartmentSummary;
        private DevExpress.XtraEditors.SimpleButton btnEmployeeDetails;
        private DevExpress.XtraEditors.SimpleButton btnEmployeesList;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnLeaveBalance;
        private DevExpress.XtraEditors.SimpleButton btnLeaveReport;
        private DevExpress.XtraEditors.SimpleButton btnAttendanceDetails;
        private DevExpress.XtraEditors.SimpleButton btnAttendanceSummary;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton btnDeductionReport;
        private DevExpress.XtraEditors.SimpleButton btnSalaryAnalysis;
        private DevExpress.XtraEditors.SimpleButton btnPayrollReport;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnPerformanceReport;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraEditors.SimpleButton btnCustomReport;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}