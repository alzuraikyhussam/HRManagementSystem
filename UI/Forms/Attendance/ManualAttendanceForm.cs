using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.DataAccess;

namespace HR.UI.Forms.Attendance
{
    /// <summary>
    /// نموذج إدخال سجل حضور يدوي
    /// </summary>
    public partial class ManualAttendanceForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;
        private readonly int _recordId;
        private bool _isEditMode;
        private int _employeeId;
        private DateTime _attendanceDate;
        private int? _workHoursId;

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

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void ManualAttendanceForm_Load(object sender, EventArgs e)
        {
            try
            {
                // تعيين عنوان النموذج
                this.Text = _isEditMode ? "تعديل سجل حضور" : "إدخال سجل حضور يدوي";

                // تحميل قائمة الموظفين
                LoadEmployees();

                // تحميل قائمة ساعات العمل
                LoadWorkHours();

                // ضبط القيم الافتراضية
                if (!_isEditMode)
                {
                    // القيم الافتراضية لإدخال جديد
                    dateAttendance.DateTime = DateTime.Today;
                    timeIn.EditValue = DateTime.Today.AddHours(8);
                    timeOut.EditValue = DateTime.Today.AddHours(16);
                    lookupStatus.EditValue = "Present";
                    checkManualEntry.Checked = true;
                }
                else
                {
                    // تحميل بيانات السجل للتعديل
                    LoadRecordData();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل قائمة الموظفين
        /// </summary>
        private void LoadEmployees()
        {
            try
            {
                string query = @"
                SELECT 
                    e.ID, 
                    e.EmployeeNumber, 
                    e.FullName,
                    d.Name AS DepartmentName
                FROM 
                    Employees e
                LEFT JOIN 
                    Departments d ON e.DepartmentID = d.ID
                WHERE 
                    e.Status = 'Active'
                ORDER BY 
                    e.FullName";

                var dataTable = _dbContext.ExecuteReader(query);
                lookupEmployee.Properties.DataSource = dataTable;
                lookupEmployee.Properties.DisplayMember = "FullName";
                lookupEmployee.Properties.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء تحميل قائمة الموظفين: " + ex.Message);
            }
        }

        /// <summary>
        /// تحميل قائمة ساعات العمل
        /// </summary>
        private void LoadWorkHours()
        {
            try
            {
                string query = @"
                SELECT 
                    ID, 
                    Name,
                    StartTime,
                    EndTime,
                    TotalHours
                FROM 
                    WorkHours
                ORDER BY 
                    Name";

                var dataTable = _dbContext.ExecuteReader(query);

                // إضافة عنصر "بدون"
                DataRow noneRow = dataTable.NewRow();
                noneRow["ID"] = DBNull.Value;
                noneRow["Name"] = "بدون";
                noneRow["StartTime"] = DBNull.Value;
                noneRow["EndTime"] = DBNull.Value;
                noneRow["TotalHours"] = DBNull.Value;
                dataTable.Rows.InsertAt(noneRow, 0);

                lookupWorkHours.Properties.DataSource = dataTable;
                lookupWorkHours.Properties.DisplayMember = "Name";
                lookupWorkHours.Properties.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء تحميل قائمة ساعات العمل: " + ex.Message);
            }
        }

        /// <summary>
        /// تحميل بيانات السجل للتعديل
        /// </summary>
        private void LoadRecordData()
        {
            try
            {
                string query = @"
                SELECT 
                    ar.EmployeeID,
                    ar.AttendanceDate,
                    ar.TimeIn,
                    ar.TimeOut,
                    ar.WorkHoursID,
                    ar.LateMinutes,
                    ar.EarlyDepartureMinutes,
                    ar.OvertimeMinutes,
                    ar.WorkedMinutes,
                    ar.Status,
                    ar.IsManualEntry,
                    ar.Notes
                FROM 
                    AttendanceRecords ar
                WHERE 
                    ar.ID = @ID";

                var parameters = new SqlParameter[] { new SqlParameter("@ID", _recordId) };
                var dataTable = _dbContext.ExecuteReader(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show("لم يتم العثور على سجل الحضور المطلوب!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                var row = dataTable.Rows[0];

                _employeeId = Convert.ToInt32(row["EmployeeID"]);
                lookupEmployee.EditValue = _employeeId;
                lookupEmployee.Enabled = false;  // منع تغيير الموظف في وضع التعديل

                _attendanceDate = Convert.ToDateTime(row["AttendanceDate"]);
                dateAttendance.DateTime = _attendanceDate;

                if (row["TimeIn"] != DBNull.Value)
                    timeIn.EditValue = Convert.ToDateTime(row["TimeIn"]);
                else
                    timeIn.EditValue = null;

                if (row["TimeOut"] != DBNull.Value)
                    timeOut.EditValue = Convert.ToDateTime(row["TimeOut"]);
                else
                    timeOut.EditValue = null;

                _workHoursId = row["WorkHoursID"] != DBNull.Value ? (int?)Convert.ToInt32(row["WorkHoursID"]) : null;
                lookupWorkHours.EditValue = _workHoursId;

                spinLateMinutes.Value = row["LateMinutes"] != DBNull.Value ? Convert.ToDecimal(row["LateMinutes"]) : 0;
                spinEarlyMinutes.Value = row["EarlyDepartureMinutes"] != DBNull.Value ? Convert.ToDecimal(row["EarlyDepartureMinutes"]) : 0;
                spinOvertimeMinutes.Value = row["OvertimeMinutes"] != DBNull.Value ? Convert.ToDecimal(row["OvertimeMinutes"]) : 0;
                spinWorkedMinutes.Value = row["WorkedMinutes"] != DBNull.Value ? Convert.ToDecimal(row["WorkedMinutes"]) : 0;
                
                lookupStatus.EditValue = row["Status"].ToString();
                checkManualEntry.Checked = Convert.ToBoolean(row["IsManualEntry"]);
                txtNotes.Text = row["Notes"] != DBNull.Value ? row["Notes"].ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء تحميل بيانات السجل: " + ex.Message);
            }
        }

        /// <summary>
        /// حساب القيم التلقائية لدقائق العمل والتأخير عند تغيير الأوقات
        /// </summary>
        private void CalculateMinutes()
        {
            try
            {
                // التحقق من إدخال وقت الدخول والخروج
                if (timeIn.EditValue == null || timeOut.EditValue == null)
                    return;

                DateTime inTime = Convert.ToDateTime(timeIn.EditValue);
                DateTime outTime = Convert.ToDateTime(timeOut.EditValue);

                // حساب دقائق العمل
                if (outTime > inTime)
                {
                    // حساب دقائق العمل
                    spinWorkedMinutes.Value = (decimal)(outTime - inTime).TotalMinutes;

                    // حساب التأخير والمغادرة المبكرة والعمل الإضافي إذا تم تحديد ساعات العمل
                    if (lookupWorkHours.EditValue != null && lookupWorkHours.EditValue != DBNull.Value)
                    {
                        int workHoursId = Convert.ToInt32(lookupWorkHours.EditValue);
                        DataRowView workHoursRow = lookupWorkHours.GetSelectedDataRow() as DataRowView;
                        
                        if (workHoursRow != null && workHoursRow["StartTime"] != DBNull.Value && workHoursRow["EndTime"] != DBNull.Value)
                        {
                            TimeSpan startTime = (TimeSpan)workHoursRow["StartTime"];
                            TimeSpan endTime = (TimeSpan)workHoursRow["EndTime"];

                            // حساب دقائق التأخير
                            if (inTime.TimeOfDay > startTime)
                            {
                                spinLateMinutes.Value = (decimal)(inTime.TimeOfDay - startTime).TotalMinutes;
                            }
                            else
                            {
                                spinLateMinutes.Value = 0;
                            }

                            // حساب دقائق المغادرة المبكرة
                            if (outTime.TimeOfDay < endTime)
                            {
                                spinEarlyMinutes.Value = (decimal)(endTime - outTime.TimeOfDay).TotalMinutes;
                            }
                            else
                            {
                                spinEarlyMinutes.Value = 0;
                            }

                            // حساب دقائق العمل الإضافي
                            if (outTime.TimeOfDay > endTime)
                            {
                                spinOvertimeMinutes.Value = (decimal)(outTime.TimeOfDay - endTime).TotalMinutes;
                            }
                            else
                            {
                                spinOvertimeMinutes.Value = 0;
                            }
                        }
                    }

                    // تحديث الحالة بناءً على القيم المحسوبة
                    if (spinLateMinutes.Value > 0 && spinEarlyMinutes.Value > 0)
                    {
                        lookupStatus.EditValue = "LateAndEarlyDeparture";
                    }
                    else if (spinLateMinutes.Value > 0)
                    {
                        lookupStatus.EditValue = "Late";
                    }
                    else if (spinEarlyMinutes.Value > 0)
                    {
                        lookupStatus.EditValue = "EarlyDeparture";
                    }
                    else
                    {
                        lookupStatus.EditValue = "Present";
                    }
                }
            }
            catch
            {
                // تجاهل أي خطأ في الحسابات التلقائية
            }
        }

        /// <summary>
        /// حفظ سجل الحضور
        /// </summary>
        private void SaveRecord()
        {
            try
            {
                // التحقق من اختيار موظف
                if (lookupEmployee.EditValue == null)
                {
                    XtraMessageBox.Show("الرجاء اختيار موظف", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lookupEmployee.Focus();
                    return;
                }

                // التحقق من إدخال تاريخ الحضور
                if (dateAttendance.EditValue == null)
                {
                    XtraMessageBox.Show("الرجاء إدخال تاريخ الحضور", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateAttendance.Focus();
                    return;
                }

                // التحقق من عدم تكرار السجل لنفس الموظف في نفس اليوم
                if (!_isEditMode)
                {
                    string checkQuery = @"
                    SELECT COUNT(*) FROM AttendanceRecords 
                    WHERE EmployeeID = @EmployeeID AND AttendanceDate = @AttendanceDate";

                    var checkParams = new SqlParameter[]
                    {
                        new SqlParameter("@EmployeeID", Convert.ToInt32(lookupEmployee.EditValue)),
                        new SqlParameter("@AttendanceDate", Convert.ToDateTime(dateAttendance.EditValue).Date)
                    };

                    int existingCount = Convert.ToInt32(_dbContext.ExecuteScalar(checkQuery, checkParams));
                    if (existingCount > 0)
                    {
                        XtraMessageBox.Show("يوجد بالفعل سجل حضور لهذا الموظف في هذا التاريخ. الرجاء تعديل السجل الموجود.", 
                            "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // تجهيز البيانات للحفظ
                int employeeId = Convert.ToInt32(lookupEmployee.EditValue);
                DateTime attendanceDate = Convert.ToDateTime(dateAttendance.EditValue).Date;
                DateTime? timeInValue = timeIn.EditValue != null ? (DateTime?)Convert.ToDateTime(timeIn.EditValue) : null;
                DateTime? timeOutValue = timeOut.EditValue != null ? (DateTime?)Convert.ToDateTime(timeOut.EditValue) : null;
                int? workHoursId = lookupWorkHours.EditValue != null && lookupWorkHours.EditValue != DBNull.Value ? 
                    (int?)Convert.ToInt32(lookupWorkHours.EditValue) : null;
                int lateMinutes = Convert.ToInt32(spinLateMinutes.Value);
                int earlyMinutes = Convert.ToInt32(spinEarlyMinutes.Value);
                int overtimeMinutes = Convert.ToInt32(spinOvertimeMinutes.Value);
                int workedMinutes = Convert.ToInt32(spinWorkedMinutes.Value);
                string status = lookupStatus.EditValue.ToString();
                bool isManualEntry = checkManualEntry.Checked;
                string notes = txtNotes.Text.Trim();

                if (_isEditMode)
                {
                    // تحديث سجل موجود
                    string updateQuery = @"
                    UPDATE AttendanceRecords SET
                        AttendanceDate = @AttendanceDate,
                        TimeIn = @TimeIn,
                        TimeOut = @TimeOut,
                        WorkHoursID = @WorkHoursID,
                        LateMinutes = @LateMinutes,
                        EarlyDepartureMinutes = @EarlyDepartureMinutes,
                        OvertimeMinutes = @OvertimeMinutes,
                        WorkedMinutes = @WorkedMinutes,
                        Status = @Status,
                        IsManualEntry = @IsManualEntry,
                        Notes = @Notes,
                        UpdatedAt = @UpdatedAt,
                        UpdatedBy = @UpdatedBy
                    WHERE ID = @ID";

                    var updateParams = new SqlParameter[]
                    {
                        new SqlParameter("@ID", _recordId),
                        new SqlParameter("@AttendanceDate", attendanceDate),
                        new SqlParameter("@TimeIn", timeInValue ?? (object)DBNull.Value),
                        new SqlParameter("@TimeOut", timeOutValue ?? (object)DBNull.Value),
                        new SqlParameter("@WorkHoursID", workHoursId ?? (object)DBNull.Value),
                        new SqlParameter("@LateMinutes", lateMinutes),
                        new SqlParameter("@EarlyDepartureMinutes", earlyMinutes),
                        new SqlParameter("@OvertimeMinutes", overtimeMinutes),
                        new SqlParameter("@WorkedMinutes", workedMinutes),
                        new SqlParameter("@Status", status),
                        new SqlParameter("@IsManualEntry", isManualEntry),
                        new SqlParameter("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes),
                        new SqlParameter("@UpdatedAt", DateTime.Now),
                        new SqlParameter("@UpdatedBy", _sessionManager.CurrentUser.ID)
                    };

                    _dbContext.ExecuteNonQuery(updateQuery, updateParams);
                    XtraMessageBox.Show("تم تحديث سجل الحضور بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // إضافة سجل جديد
                    string insertQuery = @"
                    INSERT INTO AttendanceRecords (
                        EmployeeID, AttendanceDate, TimeIn, TimeOut, WorkHoursID,
                        LateMinutes, EarlyDepartureMinutes, OvertimeMinutes, WorkedMinutes,
                        Status, IsManualEntry, Notes, CreatedAt, CreatedBy
                    ) VALUES (
                        @EmployeeID, @AttendanceDate, @TimeIn, @TimeOut, @WorkHoursID,
                        @LateMinutes, @EarlyDepartureMinutes, @OvertimeMinutes, @WorkedMinutes,
                        @Status, @IsManualEntry, @Notes, @CreatedAt, @CreatedBy
                    )";

                    var insertParams = new SqlParameter[]
                    {
                        new SqlParameter("@EmployeeID", employeeId),
                        new SqlParameter("@AttendanceDate", attendanceDate),
                        new SqlParameter("@TimeIn", timeInValue ?? (object)DBNull.Value),
                        new SqlParameter("@TimeOut", timeOutValue ?? (object)DBNull.Value),
                        new SqlParameter("@WorkHoursID", workHoursId ?? (object)DBNull.Value),
                        new SqlParameter("@LateMinutes", lateMinutes),
                        new SqlParameter("@EarlyDepartureMinutes", earlyMinutes),
                        new SqlParameter("@OvertimeMinutes", overtimeMinutes),
                        new SqlParameter("@WorkedMinutes", workedMinutes),
                        new SqlParameter("@Status", status),
                        new SqlParameter("@IsManualEntry", isManualEntry),
                        new SqlParameter("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes),
                        new SqlParameter("@CreatedAt", DateTime.Now),
                        new SqlParameter("@CreatedBy", _sessionManager.CurrentUser.ID)
                    };

                    _dbContext.ExecuteNonQuery(insertQuery, insertParams);
                    XtraMessageBox.Show("تمت إضافة سجل الحضور بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حفظ سجل الحضور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث تغيير حالة الحضور
        /// </summary>
        private void lookupStatus_EditValueChanged(object sender, EventArgs e)
        {
            // تغيير لون خلفية حقل الحالة حسب القيمة المختارة
            switch (lookupStatus.EditValue.ToString())
            {
                case "Present":
                    lookupStatus.Properties.Appearance.BackColor = System.Drawing.Color.LightGreen;
                    break;
                case "Late":
                    lookupStatus.Properties.Appearance.BackColor = System.Drawing.Color.Yellow;
                    break;
                case "EarlyDeparture":
                    lookupStatus.Properties.Appearance.BackColor = System.Drawing.Color.LightSalmon;
                    break;
                case "LateAndEarlyDeparture":
                    lookupStatus.Properties.Appearance.BackColor = System.Drawing.Color.Orange;
                    break;
                case "Absent":
                    lookupStatus.Properties.Appearance.BackColor = System.Drawing.Color.Red;
                    break;
                case "Leave":
                    lookupStatus.Properties.Appearance.BackColor = System.Drawing.Color.LightBlue;
                    break;
                default:
                    lookupStatus.Properties.Appearance.BackColor = System.Drawing.Color.White;
                    break;
            }
        }

        /// <summary>
        /// حدث النقر على زر حفظ
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        /// <summary>
        /// حدث النقر على زر إلغاء
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// حدث تغيير وقت الدخول أو الخروج أو ساعات العمل
        /// </summary>
        private void Time_ValueChanged(object sender, EventArgs e)
        {
            CalculateMinutes();
        }

        #region Designer Generated Code

        private DevExpress.XtraEditors.LookUpEdit lookupEmployee;
        private DevExpress.XtraEditors.DateEdit dateAttendance;
        private DevExpress.XtraEditors.TimeEdit timeIn;
        private DevExpress.XtraEditors.TimeEdit timeOut;
        private DevExpress.XtraEditors.LookUpEdit lookupWorkHours;
        private DevExpress.XtraEditors.SpinEdit spinLateMinutes;
        private DevExpress.XtraEditors.SpinEdit spinEarlyMinutes;
        private DevExpress.XtraEditors.SpinEdit spinOvertimeMinutes;
        private DevExpress.XtraEditors.SpinEdit spinWorkedMinutes;
        private DevExpress.XtraEditors.LookUpEdit lookupStatus;
        private DevExpress.XtraEditors.CheckEdit checkManualEntry;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl11;

        private void InitializeComponent()
        {
            this.lookupEmployee = new DevExpress.XtraEditors.LookUpEdit();
            this.dateAttendance = new DevExpress.XtraEditors.DateEdit();
            this.timeIn = new DevExpress.XtraEditors.TimeEdit();
            this.timeOut = new DevExpress.XtraEditors.TimeEdit();
            this.lookupWorkHours = new DevExpress.XtraEditors.LookUpEdit();
            this.spinLateMinutes = new DevExpress.XtraEditors.SpinEdit();
            this.spinEarlyMinutes = new DevExpress.XtraEditors.SpinEdit();
            this.spinOvertimeMinutes = new DevExpress.XtraEditors.SpinEdit();
            this.spinWorkedMinutes = new DevExpress.XtraEditors.SpinEdit();
            this.lookupStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.checkManualEntry = new DevExpress.XtraEditors.CheckEdit();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.lookupEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAttendance.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAttendance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeOut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupWorkHours.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLateMinutes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEarlyMinutes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinOvertimeMinutes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinWorkedMinutes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkManualEntry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lookupEmployee
            // 
            this.lookupEmployee.Location = new System.Drawing.Point(140, 12);
            this.lookupEmployee.Name = "lookupEmployee";
            this.lookupEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupEmployee.Properties.NullText = "";
            this.lookupEmployee.Size = new System.Drawing.Size(325, 20);
            this.lookupEmployee.TabIndex = 0;
            // 
            // dateAttendance
            // 
            this.dateAttendance.EditValue = null;
            this.dateAttendance.Location = new System.Drawing.Point(140, 38);
            this.dateAttendance.Name = "dateAttendance";
            this.dateAttendance.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateAttendance.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateAttendance.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateAttendance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateAttendance.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateAttendance.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateAttendance.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateAttendance.Size = new System.Drawing.Size(120, 20);
            this.dateAttendance.TabIndex = 1;
            // 
            // timeIn
            // 
            this.timeIn.EditValue = null;
            this.timeIn.Location = new System.Drawing.Point(140, 64);
            this.timeIn.Name = "timeIn";
            this.timeIn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeIn.Properties.DisplayFormat.FormatString = "HH:mm";
            this.timeIn.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeIn.Properties.EditFormat.FormatString = "HH:mm";
            this.timeIn.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeIn.Properties.Mask.EditMask = "HH:mm";
            this.timeIn.Size = new System.Drawing.Size(100, 20);
            this.timeIn.TabIndex = 2;
            this.timeIn.EditValueChanged += new System.EventHandler(this.Time_ValueChanged);
            // 
            // timeOut
            // 
            this.timeOut.EditValue = null;
            this.timeOut.Location = new System.Drawing.Point(140, 90);
            this.timeOut.Name = "timeOut";
            this.timeOut.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeOut.Properties.DisplayFormat.FormatString = "HH:mm";
            this.timeOut.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeOut.Properties.EditFormat.FormatString = "HH:mm";
            this.timeOut.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeOut.Properties.Mask.EditMask = "HH:mm";
            this.timeOut.Size = new System.Drawing.Size(100, 20);
            this.timeOut.TabIndex = 3;
            this.timeOut.EditValueChanged += new System.EventHandler(this.Time_ValueChanged);
            // 
            // lookupWorkHours
            // 
            this.lookupWorkHours.Location = new System.Drawing.Point(140, 116);
            this.lookupWorkHours.Name = "lookupWorkHours";
            this.lookupWorkHours.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupWorkHours.Properties.NullText = "";
            this.lookupWorkHours.Size = new System.Drawing.Size(200, 20);
            this.lookupWorkHours.TabIndex = 4;
            this.lookupWorkHours.EditValueChanged += new System.EventHandler(this.Time_ValueChanged);
            // 
            // spinLateMinutes
            // 
            this.spinLateMinutes.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinLateMinutes.Location = new System.Drawing.Point(140, 142);
            this.spinLateMinutes.Name = "spinLateMinutes";
            this.spinLateMinutes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinLateMinutes.Properties.IsFloatValue = false;
            this.spinLateMinutes.Properties.Mask.EditMask = "N00";
            this.spinLateMinutes.Size = new System.Drawing.Size(100, 20);
            this.spinLateMinutes.TabIndex = 5;
            // 
            // spinEarlyMinutes
            // 
            this.spinEarlyMinutes.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEarlyMinutes.Location = new System.Drawing.Point(140, 168);
            this.spinEarlyMinutes.Name = "spinEarlyMinutes";
            this.spinEarlyMinutes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEarlyMinutes.Properties.IsFloatValue = false;
            this.spinEarlyMinutes.Properties.Mask.EditMask = "N00";
            this.spinEarlyMinutes.Size = new System.Drawing.Size(100, 20);
            this.spinEarlyMinutes.TabIndex = 6;
            // 
            // spinOvertimeMinutes
            // 
            this.spinOvertimeMinutes.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinOvertimeMinutes.Location = new System.Drawing.Point(140, 194);
            this.spinOvertimeMinutes.Name = "spinOvertimeMinutes";
            this.spinOvertimeMinutes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinOvertimeMinutes.Properties.IsFloatValue = false;
            this.spinOvertimeMinutes.Properties.Mask.EditMask = "N00";
            this.spinOvertimeMinutes.Size = new System.Drawing.Size(100, 20);
            this.spinOvertimeMinutes.TabIndex = 7;
            // 
            // spinWorkedMinutes
            // 
            this.spinWorkedMinutes.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinWorkedMinutes.Location = new System.Drawing.Point(140, 220);
            this.spinWorkedMinutes.Name = "spinWorkedMinutes";
            this.spinWorkedMinutes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinWorkedMinutes.Properties.IsFloatValue = false;
            this.spinWorkedMinutes.Properties.Mask.EditMask = "N00";
            this.spinWorkedMinutes.Size = new System.Drawing.Size(100, 20);
            this.spinWorkedMinutes.TabIndex = 8;
            // 
            // lookupStatus
            // 
            this.lookupStatus.Location = new System.Drawing.Point(140, 246);
            this.lookupStatus.Name = "lookupStatus";
            this.lookupStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupStatus.Properties.DropDownRows = 7;
            this.lookupStatus.Properties.Items.AddRange(new object[] {
            "Present",
            "Late",
            "EarlyDeparture",
            "LateAndEarlyDeparture",
            "Absent",
            "Leave",
            "InOnly"});
            this.lookupStatus.Properties.NullText = "";
            this.lookupStatus.Size = new System.Drawing.Size(150, 20);
            this.lookupStatus.TabIndex = 9;
            this.lookupStatus.EditValueChanged += new System.EventHandler(this.lookupStatus_EditValueChanged);
            // 
            // checkManualEntry
            // 
            this.checkManualEntry.Location = new System.Drawing.Point(140, 272);
            this.checkManualEntry.Name = "checkManualEntry";
            this.checkManualEntry.Properties.Caption = "إدخال يدوي";
            this.checkManualEntry.Size = new System.Drawing.Size(75, 20);
            this.checkManualEntry.TabIndex = 10;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(140, 298);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(325, 60);
            this.txtNotes.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(304, 371);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "حفظ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(385, 371);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "إلغاء";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(29, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "الموظف:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(29, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(31, 13);
            this.labelControl2.TabIndex = 15;
            this.labelControl2.Text = "التاريخ:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(29, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(59, 13);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "وقت الدخول:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(29, 93);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(57, 13);
            this.labelControl4.TabIndex = 17;
            this.labelControl4.Text = "وقت الخروج:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(29, 119);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(66, 13);
            this.labelControl5.TabIndex = 18;
            this.labelControl5.Text = "ساعات العمل:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(29, 145);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(66, 13);
            this.labelControl6.TabIndex = 19;
            this.labelControl6.Text = "دقائق التأخير:";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(29, 171);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(105, 13);
            this.labelControl7.TabIndex = 20;
            this.labelControl7.Text = "دقائق المغادرة المبكرة:";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(29, 197);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(95, 13);
            this.labelControl8.TabIndex = 21;
            this.labelControl8.Text = "دقائق العمل الإضافي:";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(29, 223);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(65, 13);
            this.labelControl9.TabIndex = 22;
            this.labelControl9.Text = "دقائق العمل:";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(29, 249);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(33, 13);
            this.labelControl10.TabIndex = 23;
            this.labelControl10.Text = "الحالة:";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(29, 298);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(46, 13);
            this.labelControl11.TabIndex = 24;
            this.labelControl11.Text = "ملاحظات:";
            // 
            // ManualAttendanceForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 406);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.checkManualEntry);
            this.Controls.Add(this.lookupStatus);
            this.Controls.Add(this.spinWorkedMinutes);
            this.Controls.Add(this.spinOvertimeMinutes);
            this.Controls.Add(this.spinEarlyMinutes);
            this.Controls.Add(this.spinLateMinutes);
            this.Controls.Add(this.lookupWorkHours);
            this.Controls.Add(this.timeOut);
            this.Controls.Add(this.timeIn);
            this.Controls.Add(this.dateAttendance);
            this.Controls.Add(this.lookupEmployee);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManualAttendanceForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "إدخال سجل حضور يدوي";
            this.Load += new System.EventHandler(this.ManualAttendanceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookupEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAttendance.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAttendance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeOut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupWorkHours.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLateMinutes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEarlyMinutes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinOvertimeMinutes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinWorkedMinutes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkManualEntry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}