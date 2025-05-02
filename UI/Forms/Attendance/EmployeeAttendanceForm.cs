using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.DataAccess;

namespace HR.UI.Forms.Attendance
{
    /// <summary>
    /// نموذج عرض سجلات حضور موظف
    /// </summary>
    public partial class EmployeeAttendanceForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;
        private readonly int _employeeId;
        private DateTime _startDate;
        private DateTime _endDate;

        /// <summary>
        /// إنشاء نموذج جديد
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        public EmployeeAttendanceForm(int employeeId)
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
            _employeeId = employeeId;

            // تعيين التواريخ الافتراضية - شهر حالي
            _startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _endDate = _startDate.AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void EmployeeAttendanceForm_Load(object sender, EventArgs e)
        {
            try
            {
                // تحميل بيانات الموظف
                LoadEmployeeInfo();

                // تعيين التواريخ الافتراضية
                dateStart.DateTime = _startDate;
                dateEnd.DateTime = _endDate;

                // تحميل سجلات الحضور
                LoadAttendanceRecords();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات الحضور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل بيانات الموظف
        /// </summary>
        private void LoadEmployeeInfo()
        {
            try
            {
                string query = @"
                SELECT 
                    e.ID, 
                    e.EmployeeNumber, 
                    e.FullName,
                    d.Name AS DepartmentName,
                    p.Title AS PositionTitle
                FROM 
                    Employees e
                LEFT JOIN 
                    Departments d ON e.DepartmentID = d.ID
                LEFT JOIN 
                    Positions p ON e.PositionID = p.ID
                WHERE 
                    e.ID = @EmployeeID";

                var parameters = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@EmployeeID", _employeeId)
                };

                var dataTable = _dbContext.ExecuteReader(query, parameters);
                
                if (dataTable.Rows.Count > 0)
                {
                    var row = dataTable.Rows[0];
                    lblEmployeeNumber.Text = row["EmployeeNumber"].ToString();
                    lblEmployeeName.Text = row["FullName"].ToString();
                    lblDepartment.Text = row["DepartmentName"] != DBNull.Value ? row["DepartmentName"].ToString() : "";
                    lblPosition.Text = row["PositionTitle"] != DBNull.Value ? row["PositionTitle"].ToString() : "";

                    this.Text = $"سجل حضور الموظف: {row["FullName"]}";
                }
                else
                {
                    XtraMessageBox.Show("لم يتم العثور على بيانات الموظف المطلوب!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء تحميل بيانات الموظف: " + ex.Message);
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
                    ar.AttendanceDate,
                    ar.TimeIn,
                    ar.TimeOut,
                    CASE 
                        WHEN ar.WorkHoursID IS NOT NULL THEN wh.Name 
                        ELSE '' 
                    END AS WorkHoursName,
                    ar.LateMinutes,
                    ar.EarlyDepartureMinutes,
                    ar.OvertimeMinutes,
                    ar.WorkedMinutes,
                    CAST(ar.WorkedMinutes / 60.0 AS DECIMAL(10,2)) AS WorkedHours,
                    ar.Status,
                    ar.IsManualEntry,
                    ar.Notes,
                    CASE 
                        WHEN ar.IsManualEntry = 1 THEN 'نعم'
                        ELSE 'لا'
                    END AS IsManualEntryText
                FROM
                    AttendanceRecords ar
                LEFT JOIN
                    WorkHours wh ON ar.WorkHoursID = wh.ID
                WHERE
                    ar.EmployeeID = @EmployeeID
                    AND ar.AttendanceDate BETWEEN @StartDate AND @EndDate
                ORDER BY
                    ar.AttendanceDate DESC";

                var parameters = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@EmployeeID", _employeeId),
                    new System.Data.SqlClient.SqlParameter("@StartDate", _startDate),
                    new System.Data.SqlClient.SqlParameter("@EndDate", _endDate)
                };

                var dataTable = _dbContext.ExecuteReader(query, parameters);
                gridAttendance.DataSource = dataTable;
                gridViewAttendance.BestFitColumns();

                // حساب الإحصائيات
                CalculateStatistics(dataTable);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء تحميل سجلات الحضور: " + ex.Message);
            }
        }

        /// <summary>
        /// حساب إحصائيات الحضور
        /// </summary>
        private void CalculateStatistics(DataTable dataTable)
        {
            try
            {
                int totalPresent = 0;
                int totalLate = 0;
                int totalEarlyDeparture = 0;
                int totalAbsent = 0;
                int totalLeave = 0;
                int totalWorkedMinutes = 0;
                int totalLateMinutes = 0;
                int totalEarlyDepartureMinutes = 0;
                int totalOvertimeMinutes = 0;

                foreach (DataRow row in dataTable.Rows)
                {
                    string status = row["Status"].ToString();
                    
                    // عدد أيام الحضور والغياب
                    switch (status)
                    {
                        case "Present":
                            totalPresent++;
                            break;
                        case "Late":
                            totalLate++;
                            break;
                        case "EarlyDeparture":
                            totalEarlyDeparture++;
                            break;
                        case "Absent":
                            totalAbsent++;
                            break;
                        case "Leave":
                            totalLeave++;
                            break;
                    }

                    // إجمالي الدقائق
                    if (row["WorkedMinutes"] != DBNull.Value)
                        totalWorkedMinutes += Convert.ToInt32(row["WorkedMinutes"]);
                    
                    if (row["LateMinutes"] != DBNull.Value)
                        totalLateMinutes += Convert.ToInt32(row["LateMinutes"]);
                    
                    if (row["EarlyDepartureMinutes"] != DBNull.Value)
                        totalEarlyDepartureMinutes += Convert.ToInt32(row["EarlyDepartureMinutes"]);
                    
                    if (row["OvertimeMinutes"] != DBNull.Value)
                        totalOvertimeMinutes += Convert.ToInt32(row["OvertimeMinutes"]);
                }

                // عرض الإحصائيات
                lblTotalDays.Text = dataTable.Rows.Count.ToString();
                lblPresentDays.Text = (totalPresent + totalLate + totalEarlyDeparture).ToString();
                lblLateDays.Text = totalLate.ToString();
                lblEarlyDepartureDays.Text = totalEarlyDeparture.ToString();
                lblAbsentDays.Text = totalAbsent.ToString();
                lblLeaveDays.Text = totalLeave.ToString();
                
                // تحويل الدقائق إلى ساعات
                decimal totalWorkedHours = Math.Round((decimal)totalWorkedMinutes / 60, 2);
                decimal totalLateHours = Math.Round((decimal)totalLateMinutes / 60, 2);
                decimal totalEarlyDepartureHours = Math.Round((decimal)totalEarlyDepartureMinutes / 60, 2);
                decimal totalOvertimeHours = Math.Round((decimal)totalOvertimeMinutes / 60, 2);
                
                lblTotalWorkedHours.Text = totalWorkedHours.ToString("0.00");
                lblTotalLateHours.Text = totalLateHours.ToString("0.00");
                lblTotalEarlyDepartureHours.Text = totalEarlyDepartureHours.ToString("0.00");
                lblTotalOvertimeHours.Text = totalOvertimeHours.ToString("0.00");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حساب الإحصائيات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    // تحديد القيم المبدئية للنموذج
                    // سيتم تطوير هذا لاحقاً

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
                DateTime attendanceDate = Convert.ToDateTime(gridViewAttendance.GetRowCellValue(gridViewAttendance.FocusedRowHandle, "AttendanceDate"));

                if (XtraMessageBox.Show($"هل أنت متأكد من حذف سجل حضور بتاريخ {attendanceDate:dd/MM/yyyy}؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string query = "DELETE FROM AttendanceRecords WHERE ID = @ID";
                    var parameters = new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@ID", recordId) };
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
                // سيتم تطوير هذه الوظيفة لاحقاً مع إضافة نماذج التقارير
                XtraMessageBox.Show("لم يتم تطوير وظيفة طباعة التقارير بعد", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تشغيل عملية الطباعة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                saveDialog.FileName = $"تقرير_حضور_{lblEmployeeName.Text}_{DateTime.Now:yyyyMMdd}";

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
        /// حدث تغيير التواريخ
        /// </summary>
        private void DateFilter_ValueChanged(object sender, EventArgs e)
        {
            _startDate = dateStart.DateTime.Date;
            _endDate = dateEnd.DateTime.Date;
            LoadAttendanceRecords();
        }

        /// <summary>
        /// حدث النقر على زر بحث
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
            if (btnEdit.Enabled)
            {
                EditRecord();
            }
        }
    }
}