using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;
using HR.DataAccess;

namespace HR.UI.Forms.Attendance
{
    /// <summary>
    /// نموذج سجلات الحضور والانصراف
    /// </summary>
    public partial class AttendanceRecordsForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;
        private DateTime _startDate;
        private DateTime _endDate;

        /// <summary>
        /// إنشاء نموذج جديد
        /// </summary>
        public AttendanceRecordsForm()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;

            // تعيين التواريخ الافتراضية - شهر حالي
            _startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _endDate = _startDate.AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void AttendanceRecordsForm_Load(object sender, EventArgs e)
        {
            // التحقق من صلاحيات المستخدم
            CheckUserPermissions();

            // تعيين التواريخ الافتراضية
            dateStart.DateTime = _startDate;
            dateEnd.DateTime = _endDate;

            // تحميل قائمة الأقسام
            LoadDepartments();

            // تحميل سجلات الحضور
            LoadAttendanceRecords();
        }

        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckUserPermissions()
        {
            bool canAdd = _sessionManager.HasPermission("الحضور", "add");
            bool canEdit = _sessionManager.HasPermission("الحضور", "edit");
            bool canDelete = _sessionManager.HasPermission("الحضور", "delete");
            bool canPrint = _sessionManager.HasPermission("الحضور", "print");
            bool canExport = _sessionManager.HasPermission("الحضور", "export");

            btnAdd.Enabled = canAdd;
            btnEdit.Enabled = canEdit;
            btnDelete.Enabled = canDelete;
            btnPrint.Enabled = canPrint;
            btnExport.Enabled = canExport;
        }

        /// <summary>
        /// تحميل قائمة الأقسام
        /// </summary>
        private void LoadDepartments()
        {
            try
            {
                string query = @"
                SELECT ID, Name
                FROM Departments
                WHERE IsActive = 1
                ORDER BY Name";

                var dataTable = _dbContext.ExecuteReader(query);

                // إضافة عنصر "الكل"
                DataRow allRow = dataTable.NewRow();
                allRow["ID"] = 0;
                allRow["Name"] = "جميع الأقسام";
                dataTable.Rows.InsertAt(allRow, 0);

                lookupDepartment.Properties.DataSource = dataTable;
                lookupDepartment.Properties.DisplayMember = "Name";
                lookupDepartment.Properties.ValueMember = "ID";
                lookupDepartment.EditValue = 0; // تحديد "الكل" كقيمة افتراضية
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل قائمة الأقسام: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل سجلات الحضور
        /// </summary>
        private void LoadAttendanceRecords()
        {
            try
            {
                string query = @"
                SELECT
                    ar.ID,
                    e.EmployeeNumber,
                    e.FullName AS EmployeeName,
                    d.Name AS DepartmentName,
                    p.Title AS PositionTitle,
                    ar.AttendanceDate,
                    ar.TimeIn,
                    ar.TimeOut,
                    ar.LateMinutes,
                    ar.EarlyDepartureMinutes,
                    ar.OvertimeMinutes,
                    ar.WorkedMinutes,
                    ar.Status,
                    ar.IsManualEntry,
                    ar.Notes
                FROM
                    AttendanceRecords ar
                INNER JOIN
                    Employees e ON ar.EmployeeID = e.ID
                LEFT JOIN
                    Departments d ON e.DepartmentID = d.ID
                LEFT JOIN
                    Positions p ON e.PositionID = p.ID
                WHERE
                    ar.AttendanceDate BETWEEN @StartDate AND @EndDate
                    AND (@DepartmentID = 0 OR e.DepartmentID = @DepartmentID)
                    AND (@Status = 'All' OR ar.Status = @Status)
                ORDER BY
                    ar.AttendanceDate DESC, e.FullName";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@StartDate", _startDate),
                    new SqlParameter("@EndDate", _endDate),
                    new SqlParameter("@DepartmentID", Convert.ToInt32(lookupDepartment.EditValue)),
                    new SqlParameter("@Status", lookupStatus.EditValue.ToString())
                };

                var dataTable = _dbContext.ExecuteReader(query, parameters);
                gridAttendance.DataSource = dataTable;
                gridViewAttendance.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل سجلات الحضور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// إضافة سجل حضور يدوي
        /// </summary>
        private void AddManualRecord()
        {
            try
            {
                using (var manualForm = new ManualAttendanceForm())
                {
                    if (manualForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadAttendanceRecords();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إضافة سجل حضور يدوي: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تعديل سجل حضور
        /// </summary>
        private void EditRecord()
        {
            try
            {
                if (gridViewAttendance.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("الرجاء اختيار سجل للتعديل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int recordId = Convert.ToInt32(gridViewAttendance.GetRowCellValue(gridViewAttendance.FocusedRowHandle, "ID"));

                using (var manualForm = new ManualAttendanceForm(recordId))
                {
                    if (manualForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadAttendanceRecords();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تعديل سجل الحضور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حذف سجل حضور
        /// </summary>
        private void DeleteRecord()
        {
            try
            {
                if (gridViewAttendance.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("الرجاء اختيار سجل للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int recordId = Convert.ToInt32(gridViewAttendance.GetRowCellValue(gridViewAttendance.FocusedRowHandle, "ID"));
                string employeeName = gridViewAttendance.GetRowCellValue(gridViewAttendance.FocusedRowHandle, "EmployeeName").ToString();
                DateTime attendanceDate = Convert.ToDateTime(gridViewAttendance.GetRowCellValue(gridViewAttendance.FocusedRowHandle, "AttendanceDate"));

                if (XtraMessageBox.Show($"هل أنت متأكد من حذف سجل حضور الموظف '{employeeName}' بتاريخ {attendanceDate:dd/MM/yyyy}؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string query = "DELETE FROM AttendanceRecords WHERE ID = @ID";
                    var parameters = new SqlParameter[] { new SqlParameter("@ID", recordId) };
                    _dbContext.ExecuteNonQuery(query, parameters);

                    XtraMessageBox.Show("تم حذف السجل بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAttendanceRecords();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حذف سجل الحضور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// طباعة تقرير الحضور
        /// </summary>
        private void PrintReport()
        {
            try
            {
                // التحقق من وجود بيانات للطباعة
                if (gridViewAttendance.RowCount == 0)
                {
                    XtraMessageBox.Show("لا توجد بيانات للطباعة. الرجاء تحديد معايير بحث مختلفة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // إنشاء تقرير الحضور والانصراف
                using (var reportForm = new AttendanceReportForm(dateStart.DateTime, dateEnd.DateTime, Convert.ToInt32(lookupDepartment.EditValue)))
                {
                    reportForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء طباعة التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تصدير بيانات الحضور
        /// </summary>
        private void ExportData()
        {
            try
            {
                // التحقق من وجود بيانات للتصدير
                if (gridViewAttendance.RowCount == 0)
                {
                    XtraMessageBox.Show("لا توجد بيانات للتصدير. الرجاء تحديد معايير بحث مختلفة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // عرض حوار حفظ الملف
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "ملف إكسل (*.xlsx)|*.xlsx|ملف CSV (*.csv)|*.csv";
                saveDialog.Title = "تصدير البيانات";
                saveDialog.FileName = $"تقرير_الحضور_{DateTime.Now:yyyyMMdd}";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveDialog.FileName;
                    string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();

                    if (fileExtension == ".xlsx")
                    {
                        gridViewAttendance.ExportToXlsx(filePath);
                    }
                    else if (fileExtension == ".csv")
                    {
                        gridViewAttendance.ExportToCsv(filePath);
                    }

                    if (XtraMessageBox.Show("تم تصدير البيانات بنجاح. هل تريد فتح الملف؟", "تم", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تصدير البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث تغيير معايير البحث
        /// </summary>
        private void SearchCriteria_Changed(object sender, EventArgs e)
        {
            _startDate = dateStart.DateTime.Date;
            _endDate = dateEnd.DateTime.Date;
            LoadAttendanceRecords();
        }

        /// <summary>
        /// حدث النقر على زر البحث
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAttendanceRecords();
        }

        /// <summary>
        /// حدث النقر على زر إضافة
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddManualRecord();
        }

        /// <summary>
        /// حدث النقر على زر تعديل
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditRecord();
        }

        /// <summary>
        /// حدث النقر على زر حذف
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteRecord();
        }

        /// <summary>
        /// حدث النقر على زر طباعة
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintReport();
        }

        /// <summary>
        /// حدث النقر على زر تصدير
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportData();
        }

        /// <summary>
        /// حدث النقر المزدوج على الجدول
        /// </summary>
        private void gridViewAttendance_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridHitInfo info = gridViewAttendance.CalcHitInfo(ea.Location);
            if (info.InRow && btnEdit.Enabled)
            {
                EditRecord();
            }
        }

        #region Designer Generated Code

        private DevExpress.XtraEditors.DateEdit dateStart;
        private DevExpress.XtraEditors.DateEdit dateEnd;
        private DevExpress.XtraEditors.LookUpEdit lookupDepartment;
        private DevExpress.XtraEditors.LookUpEdit lookupStatus;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraGrid.GridControl gridAttendance;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAttendance;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;

        private void InitializeComponent()
        {
            this.dateStart = new DevExpress.XtraEditors.DateEdit();
            this.dateEnd = new DevExpress.XtraEditors.DateEdit();
            this.lookupDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.lookupStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.gridAttendance = new DevExpress.XtraGrid.GridControl();
            this.gridViewAttendance = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAttendance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAttendance)).BeginInit();
            this.SuspendLayout();
            // 
            // dateStart
            // 
            this.dateStart.EditValue = null;
            this.dateStart.Location = new System.Drawing.Point(98, 12);
            this.dateStart.Name = "dateStart";
            this.dateStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateStart.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateStart.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateStart.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateStart.Size = new System.Drawing.Size(120, 20);
            this.dateStart.TabIndex = 0;
            this.dateStart.EditValueChanged += new System.EventHandler(this.SearchCriteria_Changed);
            // 
            // dateEnd
            // 
            this.dateEnd.EditValue = null;
            this.dateEnd.Location = new System.Drawing.Point(327, 12);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEnd.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEnd.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEnd.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateEnd.Size = new System.Drawing.Size(120, 20);
            this.dateEnd.TabIndex = 1;
            this.dateEnd.EditValueChanged += new System.EventHandler(this.SearchCriteria_Changed);
            // 
            // lookupDepartment
            // 
            this.lookupDepartment.Location = new System.Drawing.Point(98, 38);
            this.lookupDepartment.Name = "lookupDepartment";
            this.lookupDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupDepartment.Properties.NullText = "";
            this.lookupDepartment.Size = new System.Drawing.Size(152, 20);
            this.lookupDepartment.TabIndex = 2;
            this.lookupDepartment.EditValueChanged += new System.EventHandler(this.SearchCriteria_Changed);
            // 
            // lookupStatus
            // 
            this.lookupStatus.Location = new System.Drawing.Point(327, 38);
            this.lookupStatus.Name = "lookupStatus";
            this.lookupStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupStatus.Properties.NullText = "";
            this.lookupStatus.Size = new System.Drawing.Size(152, 20);
            this.lookupStatus.TabIndex = 3;
            this.lookupStatus.EditValueChanged += new System.EventHandler(this.SearchCriteria_Changed);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(485, 36);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "بحث";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 72);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "إضافة";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(88, 72);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 23);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "تعديل";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(164, 72);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "حذف";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(404, 72);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "طباعة";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(485, 72);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "تصدير";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // gridAttendance
            // 
            this.gridAttendance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridAttendance.Location = new System.Drawing.Point(12, 101);
            this.gridAttendance.MainView = this.gridViewAttendance;
            this.gridAttendance.Name = "gridAttendance";
            this.gridAttendance.Size = new System.Drawing.Size(776, 337);
            this.gridAttendance.TabIndex = 10;
            this.gridAttendance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAttendance});
            // 
            // gridViewAttendance
            // 
            this.gridViewAttendance.GridControl = this.gridAttendance;
            this.gridViewAttendance.Name = "gridViewAttendance";
            this.gridViewAttendance.OptionsBehavior.Editable = false;
            this.gridViewAttendance.OptionsBehavior.ReadOnly = true;
            this.gridViewAttendance.OptionsFind.AlwaysVisible = true;
            this.gridViewAttendance.OptionsView.ShowAutoFilterRow = true;
            this.gridViewAttendance.OptionsView.ShowFooter = true;
            this.gridViewAttendance.DoubleClick += new System.EventHandler(this.gridViewAttendance_DoubleClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 13);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "من تاريخ:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(237, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(49, 13);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "إلى تاريخ:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(23, 41);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 13);
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "القسم:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(237, 41);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(33, 13);
            this.labelControl4.TabIndex = 14;
            this.labelControl4.Text = "الحالة:";
            // 
            // AttendanceRecordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.gridAttendance);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lookupStatus);
            this.Controls.Add(this.lookupDepartment);
            this.Controls.Add(this.dateEnd);
            this.Controls.Add(this.dateStart);
            this.Name = "AttendanceRecordsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "سجلات الحضور والانصراف";
            this.Load += new System.EventHandler(this.AttendanceRecordsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAttendance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAttendance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }

    /// <summary>
    /// نموذج إدخال سجل حضور يدوي
    /// </summary>
    public class ManualAttendanceForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;
        private readonly int _recordId;
        private bool _isEditMode;

        /// <summary>
        /// إنشاء نموذج جديد لإدخال سجل حضور يدوي
        /// </summary>
        public ManualAttendanceForm()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
            _recordId = 0;
            _isEditMode = false;
        }

        /// <summary>
        /// إنشاء نموذج لتعديل سجل حضور موجود
        /// </summary>
        public ManualAttendanceForm(int recordId)
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
            _recordId = recordId;
            _isEditMode = true;
        }

        // ستتم إضافة المزيد من التفاصيل عند تطوير نموذج الإدخال اليدوي

        #region Designer Generated Code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ManualAttendanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManualAttendanceForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "إدخال سجل حضور يدوي";
            this.ResumeLayout(false);

        }

        #endregion
    }

    /// <summary>
    /// نموذج تقرير الحضور والانصراف
    /// </summary>
    public class AttendanceReportForm : XtraForm
    {
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;
        private readonly int _departmentId;

        /// <summary>
        /// إنشاء نموذج تقرير جديد
        /// </summary>
        public AttendanceReportForm(DateTime startDate, DateTime endDate, int departmentId)
        {
            InitializeComponent();
            _startDate = startDate;
            _endDate = endDate;
            _departmentId = departmentId;
        }

        // ستتم إضافة المزيد من التفاصيل عند تطوير نماذج التقارير

        #region Designer Generated Code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AttendanceReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Name = "AttendanceReportForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تقرير الحضور والانصراف";
            this.ResumeLayout(false);

        }

        #endregion
    }
}