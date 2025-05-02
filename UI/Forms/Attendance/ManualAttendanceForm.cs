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
    /// نموذج إدخال سجلات الحضور يدويًا
    /// </summary>
    public partial class ManualAttendanceForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;
        private int _attendanceRecordId;
        private bool _isEditMode;

        /// <summary>
        /// إنشاء نموذج جديد للإدخال اليدوي
        /// </summary>
        public ManualAttendanceForm()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
            _attendanceRecordId = 0;
            _isEditMode = false;
        }

        /// <summary>
        /// إنشاء نموذج لتعديل سجل موجود
        /// </summary>
        /// <param name="attendanceRecordId">معرف سجل الحضور</param>
        public ManualAttendanceForm(int attendanceRecordId)
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
            _attendanceRecordId = attendanceRecordId;
            _isEditMode = true;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void ManualAttendanceForm_Load(object sender, EventArgs e)
        {
            LoadStatusOptions();
            LoadEmployees();

            if (_isEditMode)
            {
                LoadAttendanceRecord();
                this.Text = "تعديل سجل حضور";
            }
            else
            {
                dateAttendance.DateTime = DateTime.Now.Date;
                timeIn.Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                timeOut.Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0);
                lookupStatus.EditValue = "حاضر";
            }
        }

        /// <summary>
        /// تحميل خيارات حالة الحضور
        /// </summary>
        private void LoadStatusOptions()
        {
            try
            {
                DataTable statusTable = new DataTable();
                statusTable.Columns.Add("Value", typeof(string));
                statusTable.Columns.Add("Display", typeof(string));

                // إضافة حالات الحضور
                statusTable.Rows.Add("حاضر", "حاضر");
                statusTable.Rows.Add("متأخر", "متأخر");
                statusTable.Rows.Add("مغادرة مبكرة", "مغادرة مبكرة");
                statusTable.Rows.Add("غياب", "غياب");
                statusTable.Rows.Add("إجازة", "إجازة");
                statusTable.Rows.Add("مهمة عمل", "مهمة عمل");

                lookupStatus.Properties.DataSource = statusTable;
                lookupStatus.Properties.DisplayMember = "Display";
                lookupStatus.Properties.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل خيارات الحالة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ID, 
                    EmployeeNumber,
                    FullName,
                    DepartmentID
                FROM 
                    Employees
                WHERE 
                    IsActive = 1
                ORDER BY 
                    FullName";

                var dataTable = _dbContext.ExecuteReader(query);

                lookupEmployee.Properties.DataSource = dataTable;
                lookupEmployee.Properties.DisplayMember = "FullName";
                lookupEmployee.Properties.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل قائمة الموظفين: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل بيانات سجل الحضور للتعديل
        /// </summary>
        private void LoadAttendanceRecord()
        {
            try
            {
                string query = @"
                SELECT 
                    ar.EmployeeID,
                    ar.AttendanceDate,
                    ar.TimeIn,
                    ar.TimeOut,
                    ar.Status,
                    ar.Notes
                FROM 
                    AttendanceRecords ar
                WHERE 
                    ar.ID = @ID";

                var parameters = new SqlParameter[] { new SqlParameter("@ID", _attendanceRecordId) };
                var dataTable = _dbContext.ExecuteReader(query, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    var row = dataTable.Rows[0];
                    lookupEmployee.EditValue = row["EmployeeID"];
                    dateAttendance.DateTime = Convert.ToDateTime(row["AttendanceDate"]);

                    if (row["TimeIn"] != DBNull.Value)
                    {
                        TimeSpan timeInValue = (TimeSpan)row["TimeIn"];
                        timeIn.Time = new DateTime(
                            dateAttendance.DateTime.Year,
                            dateAttendance.DateTime.Month,
                            dateAttendance.DateTime.Day,
                            timeInValue.Hours,
                            timeInValue.Minutes,
                            0);
                    }

                    if (row["TimeOut"] != DBNull.Value)
                    {
                        TimeSpan timeOutValue = (TimeSpan)row["TimeOut"];
                        timeOut.Time = new DateTime(
                            dateAttendance.DateTime.Year,
                            dateAttendance.DateTime.Month,
                            dateAttendance.DateTime.Day,
                            timeOutValue.Hours,
                            timeOutValue.Minutes,
                            0);
                    }

                    lookupStatus.EditValue = row["Status"].ToString();
                    memoNotes.Text = row["Notes"].ToString();
                }
                else
                {
                    XtraMessageBox.Show("لم يتم العثور على سجل الحضور المطلوب.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات سجل الحضور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر الحفظ
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من اكتمال البيانات المطلوبة
                if (!ValidateInputs())
                    return;

                // حساب الوقت المستغرق
                int workedMinutes = 0;
                int lateMinutes = 0;
                int earlyDepartureMinutes = 0;
                int overtimeMinutes = 0;

                // الحصول على الوقت المطلوب للدوام
                int requiredMinutes = 480; // 8 ساعات × 60 دقيقة = 480 دقيقة (افتراضياً)

                // حساب الوقت الفعلي للعمل
                if (lookupStatus.EditValue.ToString() != "غياب" && lookupStatus.EditValue.ToString() != "إجازة")
                {
                    TimeSpan timeInValue = new TimeSpan(timeIn.Time.Hour, timeIn.Time.Minute, 0);
                    TimeSpan timeOutValue = new TimeSpan(timeOut.Time.Hour, timeOut.Time.Minute, 0);

                    // معالجة حالة إذا كان وقت الخروج في اليوم التالي
                    if (timeOutValue < timeInValue)
                    {
                        timeOutValue = timeOutValue.Add(new TimeSpan(24, 0, 0));
                    }

                    // حساب الوقت المستغرق بالدقائق
                    workedMinutes = (int)(timeOutValue - timeInValue).TotalMinutes;

                    // حساب التأخير والمغادرة المبكرة (استناداً إلى ساعات العمل الافتراضية 8:00 - 16:00)
                    TimeSpan expectedTimeIn = new TimeSpan(8, 0, 0);
                    TimeSpan expectedTimeOut = new TimeSpan(16, 0, 0);

                    if (timeInValue > expectedTimeIn)
                    {
                        lateMinutes = (int)(timeInValue - expectedTimeIn).TotalMinutes;
                    }

                    if (timeOutValue < expectedTimeOut)
                    {
                        earlyDepartureMinutes = (int)(expectedTimeOut - timeOutValue).TotalMinutes;
                    }
                    else if (timeOutValue > expectedTimeOut)
                    {
                        overtimeMinutes = (int)(timeOutValue - expectedTimeOut).TotalMinutes;
                    }
                }

                // حفظ البيانات
                string query;
                SqlParameter[] parameters;

                if (_isEditMode)
                {
                    query = @"
                    UPDATE AttendanceRecords SET
                        EmployeeID = @EmployeeID,
                        AttendanceDate = @AttendanceDate,
                        TimeIn = @TimeIn,
                        TimeOut = @TimeOut,
                        LateMinutes = @LateMinutes,
                        EarlyDepartureMinutes = @EarlyDepartureMinutes,
                        OvertimeMinutes = @OvertimeMinutes,
                        WorkedMinutes = @WorkedMinutes,
                        Status = @Status,
                        IsManualEntry = 1,
                        Notes = @Notes,
                        ModifiedBy = @ModifiedBy,
                        ModifiedDate = GETDATE()
                    WHERE ID = @ID";

                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@ID", _attendanceRecordId),
                        new SqlParameter("@EmployeeID", lookupEmployee.EditValue),
                        new SqlParameter("@AttendanceDate", dateAttendance.DateTime),
                        new SqlParameter("@TimeIn", TimeSpan.FromHours(timeIn.Time.Hour).Add(TimeSpan.FromMinutes(timeIn.Time.Minute))),
                        new SqlParameter("@TimeOut", TimeSpan.FromHours(timeOut.Time.Hour).Add(TimeSpan.FromMinutes(timeOut.Time.Minute))),
                        new SqlParameter("@LateMinutes", lateMinutes),
                        new SqlParameter("@EarlyDepartureMinutes", earlyDepartureMinutes),
                        new SqlParameter("@OvertimeMinutes", overtimeMinutes),
                        new SqlParameter("@WorkedMinutes", workedMinutes),
                        new SqlParameter("@Status", lookupStatus.EditValue.ToString()),
                        new SqlParameter("@Notes", memoNotes.Text),
                        new SqlParameter("@ModifiedBy", _sessionManager.CurrentUser.ID)
                    };
                }
                else
                {
                    query = @"
                    INSERT INTO AttendanceRecords (
                        EmployeeID, AttendanceDate, TimeIn, TimeOut, 
                        LateMinutes, EarlyDepartureMinutes, OvertimeMinutes, WorkedMinutes,
                        Status, IsManualEntry, Notes, CreatedBy, CreatedDate
                    ) VALUES (
                        @EmployeeID, @AttendanceDate, @TimeIn, @TimeOut,
                        @LateMinutes, @EarlyDepartureMinutes, @OvertimeMinutes, @WorkedMinutes,
                        @Status, 1, @Notes, @CreatedBy, GETDATE()
                    )";

                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@EmployeeID", lookupEmployee.EditValue),
                        new SqlParameter("@AttendanceDate", dateAttendance.DateTime),
                        new SqlParameter("@TimeIn", TimeSpan.FromHours(timeIn.Time.Hour).Add(TimeSpan.FromMinutes(timeIn.Time.Minute))),
                        new SqlParameter("@TimeOut", TimeSpan.FromHours(timeOut.Time.Hour).Add(TimeSpan.FromMinutes(timeOut.Time.Minute))),
                        new SqlParameter("@LateMinutes", lateMinutes),
                        new SqlParameter("@EarlyDepartureMinutes", earlyDepartureMinutes),
                        new SqlParameter("@OvertimeMinutes", overtimeMinutes),
                        new SqlParameter("@WorkedMinutes", workedMinutes),
                        new SqlParameter("@Status", lookupStatus.EditValue.ToString()),
                        new SqlParameter("@Notes", memoNotes.Text),
                        new SqlParameter("@CreatedBy", _sessionManager.CurrentUser.ID)
                    };
                }

                _dbContext.ExecuteNonQuery(query, parameters);

                XtraMessageBox.Show("تم حفظ بيانات الحضور بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حفظ البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// التحقق من اكتمال البيانات المطلوبة
        /// </summary>
        private bool ValidateInputs()
        {
            if (lookupEmployee.EditValue == null)
            {
                XtraMessageBox.Show("الرجاء اختيار الموظف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lookupEmployee.Focus();
                return false;
            }

            if (dateAttendance.EditValue == null)
            {
                XtraMessageBox.Show("الرجاء تحديد تاريخ الحضور", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateAttendance.Focus();
                return false;
            }

            if (lookupStatus.EditValue == null)
            {
                XtraMessageBox.Show("الرجاء تحديد حالة الحضور", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lookupStatus.Focus();
                return false;
            }

            // التحقق من سجلات الحضور المتكررة
            if (!_isEditMode)
            {
                string query = @"
                SELECT COUNT(*) FROM AttendanceRecords 
                WHERE EmployeeID = @EmployeeID AND AttendanceDate = @AttendanceDate";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", lookupEmployee.EditValue),
                    new SqlParameter("@AttendanceDate", dateAttendance.DateTime.Date)
                };

                int count = Convert.ToInt32(_dbContext.ExecuteScalar(query, parameters));
                if (count > 0)
                {
                    if (XtraMessageBox.Show(
                        "يوجد سجل حضور لهذا الموظف في نفس التاريخ. هل تريد المتابعة؟",
                        "تحذير",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}