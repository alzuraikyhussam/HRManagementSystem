namespace HR.UI.Forms.Reports
{
    partial class SimpleEmployeeReportForm
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
            this.components = new System.ComponentModel.Container();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDepartment = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboEmployee = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.rdoInactive = new System.Windows.Forms.RadioButton();
            this.rdoActive = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.chkDateRange = new System.Windows.Forms.CheckBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelHeader.Controls.Add(this.lblReportTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 50);
            this.panelHeader.TabIndex = 0;
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReportTitle.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblReportTitle.ForeColor = System.Drawing.Color.White;
            this.lblReportTitle.Location = new System.Drawing.Point(0, 0);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Size = new System.Drawing.Size(1000, 50);
            this.lblReportTitle.TabIndex = 0;
            this.lblReportTitle.Text = "تقرير الموظفين";
            this.lblReportTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelFilters.Controls.Add(this.groupBox3);
            this.panelFilters.Controls.Add(this.groupBox2);
            this.panelFilters.Controls.Add(this.groupBox1);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 50);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Padding = new System.Windows.Forms.Padding(10);
            this.panelFilters.Size = new System.Drawing.Size(1000, 150);
            this.panelFilters.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboEmployee);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboDepartment);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(630, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "معلومات الموظف";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "الإدارة:";
            // 
            // cboDepartment
            // 
            this.cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.Location = new System.Drawing.Point(20, 27);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(288, 21);
            this.cboDepartment.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "الموظف:";
            // 
            // cboEmployee
            // 
            this.cboEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmployee.FormattingEnabled = true;
            this.cboEmployee.Location = new System.Drawing.Point(20, 67);
            this.cboEmployee.Name = "cboEmployee";
            this.cboEmployee.Size = new System.Drawing.Size(288, 21);
            this.cboEmployee.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoActive);
            this.groupBox2.Controls.Add(this.rdoInactive);
            this.groupBox2.Controls.Add(this.rdoAll);
            this.groupBox2.Location = new System.Drawing.Point(420, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 130);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "الحالة";
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Checked = true;
            this.rdoAll.Location = new System.Drawing.Point(150, 30);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(44, 17);
            this.rdoAll.TabIndex = 0;
            this.rdoAll.TabStop = true;
            this.rdoAll.Text = "الكل";
            this.rdoAll.UseVisualStyleBackColor = true;
            // 
            // rdoInactive
            // 
            this.rdoInactive.AutoSize = true;
            this.rdoInactive.Location = new System.Drawing.Point(128, 90);
            this.rdoInactive.Name = "rdoInactive";
            this.rdoInactive.Size = new System.Drawing.Size(66, 17);
            this.rdoInactive.TabIndex = 1;
            this.rdoInactive.Text = "غير نشط";
            this.rdoInactive.UseVisualStyleBackColor = true;
            // 
            // rdoActive
            // 
            this.rdoActive.AutoSize = true;
            this.rdoActive.Location = new System.Drawing.Point(154, 60);
            this.rdoActive.Name = "rdoActive";
            this.rdoActive.Size = new System.Drawing.Size(40, 17);
            this.rdoActive.TabIndex = 2;
            this.rdoActive.Text = "نشط";
            this.rdoActive.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkDateRange);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.dtpStartDate);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dtpEndDate);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(10, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 130);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "الفترة الزمنية";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(20, 87);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(289, 20);
            this.dtpEndDate.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(315, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "إلى تاريخ:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(20, 57);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(289, 20);
            this.dtpStartDate.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(315, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "من تاريخ:";
            // 
            // chkDateRange
            // 
            this.chkDateRange.AutoSize = true;
            this.chkDateRange.Checked = true;
            this.chkDateRange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDateRange.Location = new System.Drawing.Point(287, 30);
            this.chkDateRange.Name = "chkDateRange";
            this.chkDateRange.Size = new System.Drawing.Size(107, 17);
            this.chkDateRange.TabIndex = 4;
            this.chkDateRange.Text = "تحديد فترة زمنية";
            this.chkDateRange.UseVisualStyleBackColor = true;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelButtons.Controls.Add(this.btnGenerateReport);
            this.panelButtons.Controls.Add(this.btnExportExcel);
            this.panelButtons.Controls.Add(this.btnPrint);
            this.panelButtons.Controls.Add(this.btnRefresh);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(0, 200);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1000, 50);
            this.panelButtons.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = null;
            this.btnRefresh.Location = new System.Drawing.Point(250, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(175, 35);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "تحديث البيانات";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = null;
            this.btnPrint.Location = new System.Drawing.Point(431, 8);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(175, 35);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "طباعة";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = null;
            this.btnExportExcel.Location = new System.Drawing.Point(612, 8);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(175, 35);
            this.btnExportExcel.TabIndex = 2;
            this.btnExportExcel.Text = "تصدير إلى Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnGenerateReport.Image = null;
            this.btnGenerateReport.Location = new System.Drawing.Point(793, 8);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(175, 35);
            this.btnGenerateReport.TabIndex = 3;
            this.btnGenerateReport.Text = "إنشاء التقرير";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvEmployees);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 250);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new System.Windows.Forms.Padding(10);
            this.panelGrid.Size = new System.Drawing.Size(1000, 518);
            this.panelGrid.TabIndex = 3;
            // 
            // dgvEmployees
            // 
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployees.Location = new System.Drawing.Point(10, 10);
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.Size = new System.Drawing.Size(980, 498);
            this.dgvEmployees.TabIndex = 0;
            // 
            // SimpleEmployeeReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 768);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SimpleEmployeeReportForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تقرير الموظفين";
            this.panelHeader.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblReportTitle;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkDateRange;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoActive;
        private System.Windows.Forms.RadioButton rdoInactive;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboEmployee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboDepartment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvEmployees;
    }
}