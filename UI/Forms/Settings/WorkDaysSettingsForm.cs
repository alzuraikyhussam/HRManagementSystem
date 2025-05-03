using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using HR.Core;

namespace HR.UI.Forms.Settings
{
    public partial class WorkDaysSettingsForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly SystemSettingsManager _settingsManager;
        private WorkDaysSettings _settings;
        
        public WorkDaysSettingsForm()
        {
            InitializeComponent();
            _settingsManager = new SystemSettingsManager();
        }
        
        private void WorkDaysSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                // استرجاع الإعدادات
                LoadSettings();
                
                // التحقق من الصلاحيات
                CheckPermissions();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckPermissions()
        {
            // التحقق من صلاحية تعديل الإعدادات
            bool canEditSettings = SessionManager.HasPermission("Settings.EditWorkDaysSettings");
            
            // تطبيق الصلاحيات على عناصر التحكم
            SetControlsEnabled(canEditSettings);
            
            // تعطيل أو تفعيل زر الحفظ
            barButtonItemSave.Enabled = canEditSettings;
        }
        
        /// <summary>
        /// تحميل الإعدادات
        /// </summary>
        private void LoadSettings()
        {
            this.Cursor = Cursors.WaitCursor;
            
            try
            {
                // استرجاع إعدادات أيام العمل
                _settings = _settingsManager.GetWorkDaysSettings();
                
                // عرض البيانات
                DisplaySettings();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل الإعدادات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// عرض الإعدادات
        /// </summary>
        private void DisplaySettings()
        {
            if (_settings == null)
                return;
            
            // إعدادات أيام العمل
            checkEditSaturday.Checked = _settings.Saturday;
            checkEditSunday.Checked = _settings.Sunday;
            checkEditMonday.Checked = _settings.Monday;
            checkEditTuesday.Checked = _settings.Tuesday;
            checkEditWednesday.Checked = _settings.Wednesday;
            checkEditThursday.Checked = _settings.Thursday;
            checkEditFriday.Checked = _settings.Friday;
            
            // مواعيد الدوام
            SetWorkTimeValues(timeEditSaturdayStart, timeEditSaturdayEnd, _settings.SaturdayStartTime, _settings.SaturdayEndTime);
            SetWorkTimeValues(timeEditSundayStart, timeEditSundayEnd, _settings.SundayStartTime, _settings.SundayEndTime);
            SetWorkTimeValues(timeEditMondayStart, timeEditMondayEnd, _settings.MondayStartTime, _settings.MondayEndTime);
            SetWorkTimeValues(timeEditTuesdayStart, timeEditTuesdayEnd, _settings.TuesdayStartTime, _settings.TuesdayEndTime);
            SetWorkTimeValues(timeEditWednesdayStart, timeEditWednesdayEnd, _settings.WednesdayStartTime, _settings.WednesdayEndTime);
            SetWorkTimeValues(timeEditThursdayStart, timeEditThursdayEnd, _settings.ThursdayStartTime, _settings.ThursdayEndTime);
            SetWorkTimeValues(timeEditFridayStart, timeEditFridayEnd, _settings.FridayStartTime, _settings.FridayEndTime);
            
            // خيارات المرونة
            checkEditFlexibleStartTime.Checked = _settings.FlexibleStartTime;
            checkEditFlexibleEndTime.Checked = _settings.FlexibleEndTime;
            spinEditGraceMinutes.Value = _settings.GraceMinutes;
            checkEditShiftWorkEnabled.Checked = _settings.ShiftWorkEnabled;
            
            // تحديث حالة التفعيل لأيام العمل
            UpdateDayControlsState();
        }
        
        /// <summary>
        /// ضبط قيم مواعيد الدوام
        /// </summary>
        private void SetWorkTimeValues(TimeEdit startTimeEdit, TimeEdit endTimeEdit, TimeSpan? startTime, TimeSpan? endTime)
        {
            if (startTime.HasValue)
            {
                startTimeEdit.Time = DateTime.Today.Add(startTime.Value);
            }
            else
            {
                startTimeEdit.Time = DateTime.Today.AddHours(8);
            }
            
            if (endTime.HasValue)
            {
                endTimeEdit.Time = DateTime.Today.Add(endTime.Value);
            }
            else
            {
                endTimeEdit.Time = DateTime.Today.AddHours(16);
            }
        }
        
        /// <summary>
        /// تحديث حالة التفعيل لعناصر أيام العمل
        /// </summary>
        private void UpdateDayControlsState()
        {
            // السبت
            timeEditSaturdayStart.Enabled = checkEditSaturday.Checked;
            timeEditSaturdayEnd.Enabled = checkEditSaturday.Checked;
            
            // الأحد
            timeEditSundayStart.Enabled = checkEditSunday.Checked;
            timeEditSundayEnd.Enabled = checkEditSunday.Checked;
            
            // الإثنين
            timeEditMondayStart.Enabled = checkEditMonday.Checked;
            timeEditMondayEnd.Enabled = checkEditMonday.Checked;
            
            // الثلاثاء
            timeEditTuesdayStart.Enabled = checkEditTuesday.Checked;
            timeEditTuesdayEnd.Enabled = checkEditTuesday.Checked;
            
            // الأربعاء
            timeEditWednesdayStart.Enabled = checkEditWednesday.Checked;
            timeEditWednesdayEnd.Enabled = checkEditWednesday.Checked;
            
            // الخميس
            timeEditThursdayStart.Enabled = checkEditThursday.Checked;
            timeEditThursdayEnd.Enabled = checkEditThursday.Checked;
            
            // الجمعة
            timeEditFridayStart.Enabled = checkEditFriday.Checked;
            timeEditFridayEnd.Enabled = checkEditFriday.Checked;
        }
        
        /// <summary>
        /// تفعيل أو تعطيل عناصر التحكم
        /// </summary>
        /// <param name="enabled">حالة التفعيل</param>
        private void SetControlsEnabled(bool enabled)
        {
            // إعدادات أيام العمل
            checkEditSaturday.Enabled = enabled;
            checkEditSunday.Enabled = enabled;
            checkEditMonday.Enabled = enabled;
            checkEditTuesday.Enabled = enabled;
            checkEditWednesday.Enabled = enabled;
            checkEditThursday.Enabled = enabled;
            checkEditFriday.Enabled = enabled;
            
            // مواعيد الدوام
            timeEditSaturdayStart.Enabled = enabled && checkEditSaturday.Checked;
            timeEditSaturdayEnd.Enabled = enabled && checkEditSaturday.Checked;
            timeEditSundayStart.Enabled = enabled && checkEditSunday.Checked;
            timeEditSundayEnd.Enabled = enabled && checkEditSunday.Checked;
            timeEditMondayStart.Enabled = enabled && checkEditMonday.Checked;
            timeEditMondayEnd.Enabled = enabled && checkEditMonday.Checked;
            timeEditTuesdayStart.Enabled = enabled && checkEditTuesday.Checked;
            timeEditTuesdayEnd.Enabled = enabled && checkEditTuesday.Checked;
            timeEditWednesdayStart.Enabled = enabled && checkEditWednesday.Checked;
            timeEditWednesdayEnd.Enabled = enabled && checkEditWednesday.Checked;
            timeEditThursdayStart.Enabled = enabled && checkEditThursday.Checked;
            timeEditThursdayEnd.Enabled = enabled && checkEditThursday.Checked;
            timeEditFridayStart.Enabled = enabled && checkEditFriday.Checked;
            timeEditFridayEnd.Enabled = enabled && checkEditFriday.Checked;
            
            // خيارات المرونة
            checkEditFlexibleStartTime.Enabled = enabled;
            checkEditFlexibleEndTime.Enabled = enabled;
            spinEditGraceMinutes.Enabled = enabled;
            checkEditShiftWorkEnabled.Enabled = enabled;
        }
        
        /// <summary>
        /// حفظ الإعدادات
        /// </summary>
        private bool SaveSettings()
        {
            try
            {
                // إنشاء كائن الإعدادات
                WorkDaysSettings settings = new WorkDaysSettings
                {
                    // إعدادات أيام العمل
                    Saturday = checkEditSaturday.Checked,
                    Sunday = checkEditSunday.Checked,
                    Monday = checkEditMonday.Checked,
                    Tuesday = checkEditTuesday.Checked,
                    Wednesday = checkEditWednesday.Checked,
                    Thursday = checkEditThursday.Checked,
                    Friday = checkEditFriday.Checked,
                    
                    // مواعيد الدوام
                    SaturdayStartTime = GetTimeSpanFromTimeEdit(timeEditSaturdayStart),
                    SaturdayEndTime = GetTimeSpanFromTimeEdit(timeEditSaturdayEnd),
                    SundayStartTime = GetTimeSpanFromTimeEdit(timeEditSundayStart),
                    SundayEndTime = GetTimeSpanFromTimeEdit(timeEditSundayEnd),
                    MondayStartTime = GetTimeSpanFromTimeEdit(timeEditMondayStart),
                    MondayEndTime = GetTimeSpanFromTimeEdit(timeEditMondayEnd),
                    TuesdayStartTime = GetTimeSpanFromTimeEdit(timeEditTuesdayStart),
                    TuesdayEndTime = GetTimeSpanFromTimeEdit(timeEditTuesdayEnd),
                    WednesdayStartTime = GetTimeSpanFromTimeEdit(timeEditWednesdayStart),
                    WednesdayEndTime = GetTimeSpanFromTimeEdit(timeEditWednesdayEnd),
                    ThursdayStartTime = GetTimeSpanFromTimeEdit(timeEditThursdayStart),
                    ThursdayEndTime = GetTimeSpanFromTimeEdit(timeEditThursdayEnd),
                    FridayStartTime = GetTimeSpanFromTimeEdit(timeEditFridayStart),
                    FridayEndTime = GetTimeSpanFromTimeEdit(timeEditFridayEnd),
                    
                    // خيارات المرونة
                    FlexibleStartTime = checkEditFlexibleStartTime.Checked,
                    FlexibleEndTime = checkEditFlexibleEndTime.Checked,
                    GraceMinutes = (int)spinEditGraceMinutes.Value,
                    ShiftWorkEnabled = checkEditShiftWorkEnabled.Checked
                };
                
                // حفظ الإعدادات
                bool result = _settingsManager.SaveWorkDaysSettings(settings);
                
                if (result)
                {
                    // تحديث الإعدادات المحملة
                    _settings = settings;
                }
                
                return result;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حفظ الإعدادات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
                return false;
            }
        }
        
        /// <summary>
        /// الحصول على فاصل زمني من عنصر تحرير الوقت
        /// </summary>
        private TimeSpan? GetTimeSpanFromTimeEdit(TimeEdit timeEdit)
        {
            return timeEdit.Time.TimeOfDay;
        }
        
        /// <summary>
        /// حدث الضغط على زر التحديث
        /// </summary>
        private void barButtonItemRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // إعادة تحميل الإعدادات
                LoadSettings();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحديث البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث الضغط على زر الحفظ
        /// </summary>
        private void barButtonItemSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // التحقق من البيانات
                if (!ValidateInput())
                    return;
                
                this.Cursor = Cursors.WaitCursor;
                
                // حفظ الإعدادات
                bool result = SaveSettings();
                
                if (result)
                {
                    XtraMessageBox.Show("تم حفظ الإعدادات بنجاح.", "نجاح", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("فشل حفظ الإعدادات.", "خطأ", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حفظ البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// التحقق من البيانات المدخلة
        /// </summary>
        private bool ValidateInput()
        {
            // التحقق من وجود يوم عمل واحد على الأقل
            if (!checkEditSaturday.Checked && !checkEditSunday.Checked && 
                !checkEditMonday.Checked && !checkEditTuesday.Checked && 
                !checkEditWednesday.Checked && !checkEditThursday.Checked && 
                !checkEditFriday.Checked)
            {
                XtraMessageBox.Show("يجب تحديد يوم عمل واحد على الأقل.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            // التحقق من قيم مواعيد الدوام
            if (checkEditSaturday.Checked && !ValidateWorkTimes(timeEditSaturdayStart, timeEditSaturdayEnd, "السبت"))
                return false;
            
            if (checkEditSunday.Checked && !ValidateWorkTimes(timeEditSundayStart, timeEditSundayEnd, "الأحد"))
                return false;
            
            if (checkEditMonday.Checked && !ValidateWorkTimes(timeEditMondayStart, timeEditMondayEnd, "الإثنين"))
                return false;
            
            if (checkEditTuesday.Checked && !ValidateWorkTimes(timeEditTuesdayStart, timeEditTuesdayEnd, "الثلاثاء"))
                return false;
            
            if (checkEditWednesday.Checked && !ValidateWorkTimes(timeEditWednesdayStart, timeEditWednesdayEnd, "الأربعاء"))
                return false;
            
            if (checkEditThursday.Checked && !ValidateWorkTimes(timeEditThursdayStart, timeEditThursdayEnd, "الخميس"))
                return false;
            
            if (checkEditFriday.Checked && !ValidateWorkTimes(timeEditFridayStart, timeEditFridayEnd, "الجمعة"))
                return false;
            
            return true;
        }
        
        /// <summary>
        /// التحقق من قيم مواعيد الدوام
        /// </summary>
        private bool ValidateWorkTimes(TimeEdit startTimeEdit, TimeEdit endTimeEdit, string dayName)
        {
            // التحقق من أن وقت الانتهاء بعد وقت البداية
            if (startTimeEdit.Time >= endTimeEdit.Time)
            {
                XtraMessageBox.Show($"وقت انتهاء الدوام ليوم {dayName} يجب أن يكون بعد وقت البداية.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                endTimeEdit.Focus();
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// حدث تغيير حالة تفعيل أيام العمل
        /// </summary>
        private void checkEditWorkDay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // تحديث حالة التفعيل لأيام العمل
                UpdateDayControlsState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// ضبط نفس أوقات الدوام لجميع أيام العمل
        /// </summary>
        private void barButtonItemCopyTimes_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // عرض نموذج لاختيار اليوم المصدر
                using (CopyWorkTimesForm copyForm = new CopyWorkTimesForm())
                {
                    if (copyForm.ShowDialog() == DialogResult.OK)
                    {
                        // نسخ أوقات الدوام
                        string sourceDay = copyForm.SourceDay;
                        bool copyToAll = copyForm.CopyToAll;
                        
                        // الحصول على أوقات اليوم المصدر
                        TimeSpan startTime = TimeSpan.Zero;
                        TimeSpan endTime = TimeSpan.Zero;
                        
                        switch (sourceDay)
                        {
                            case "السبت":
                                startTime = timeEditSaturdayStart.Time.TimeOfDay;
                                endTime = timeEditSaturdayEnd.Time.TimeOfDay;
                                break;
                            
                            case "الأحد":
                                startTime = timeEditSundayStart.Time.TimeOfDay;
                                endTime = timeEditSundayEnd.Time.TimeOfDay;
                                break;
                            
                            case "الإثنين":
                                startTime = timeEditMondayStart.Time.TimeOfDay;
                                endTime = timeEditMondayEnd.Time.TimeOfDay;
                                break;
                            
                            case "الثلاثاء":
                                startTime = timeEditTuesdayStart.Time.TimeOfDay;
                                endTime = timeEditTuesdayEnd.Time.TimeOfDay;
                                break;
                            
                            case "الأربعاء":
                                startTime = timeEditWednesdayStart.Time.TimeOfDay;
                                endTime = timeEditWednesdayEnd.Time.TimeOfDay;
                                break;
                            
                            case "الخميس":
                                startTime = timeEditThursdayStart.Time.TimeOfDay;
                                endTime = timeEditThursdayEnd.Time.TimeOfDay;
                                break;
                            
                            case "الجمعة":
                                startTime = timeEditFridayStart.Time.TimeOfDay;
                                endTime = timeEditFridayEnd.Time.TimeOfDay;
                                break;
                        }
                        
                        DateTime startDateTime = DateTime.Today.Add(startTime);
                        DateTime endDateTime = DateTime.Today.Add(endTime);
                        
                        // نسخ الأوقات إلى الأيام المحددة
                        if (copyToAll || copyForm.CopyToSaturday)
                        {
                            timeEditSaturdayStart.Time = startDateTime;
                            timeEditSaturdayEnd.Time = endDateTime;
                        }
                        
                        if (copyToAll || copyForm.CopyToSunday)
                        {
                            timeEditSundayStart.Time = startDateTime;
                            timeEditSundayEnd.Time = endDateTime;
                        }
                        
                        if (copyToAll || copyForm.CopyToMonday)
                        {
                            timeEditMondayStart.Time = startDateTime;
                            timeEditMondayEnd.Time = endDateTime;
                        }
                        
                        if (copyToAll || copyForm.CopyToTuesday)
                        {
                            timeEditTuesdayStart.Time = startDateTime;
                            timeEditTuesdayEnd.Time = endDateTime;
                        }
                        
                        if (copyToAll || copyForm.CopyToWednesday)
                        {
                            timeEditWednesdayStart.Time = startDateTime;
                            timeEditWednesdayEnd.Time = endDateTime;
                        }
                        
                        if (copyToAll || copyForm.CopyToThursday)
                        {
                            timeEditThursdayStart.Time = startDateTime;
                            timeEditThursdayEnd.Time = endDateTime;
                        }
                        
                        if (copyToAll || copyForm.CopyToFriday)
                        {
                            timeEditFridayStart.Time = startDateTime;
                            timeEditFridayEnd.Time = endDateTime;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// استيراد إعدادات أيام العمل من جدول فترات العمل
        /// </summary>
        private void barButtonItemImportFromWorkHours_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // عرض نموذج لاختيار فترة العمل
                using (SelectWorkHoursForm selectForm = new SelectWorkHoursForm())
                {
                    if (selectForm.ShowDialog() == DialogResult.OK)
                    {
                        // استيراد الإعدادات من فترة العمل المحددة
                        int workHoursID = selectForm.SelectedWorkHoursID;
                        
                        // استرجاع بيانات فترة العمل
                        if (workHoursID > 0)
                        {
                            // تنفيذ استيراد الإعدادات
                            if (ImportWorkHoursSettings(workHoursID))
                            {
                                XtraMessageBox.Show("تم استيراد إعدادات فترة العمل بنجاح.", "نجاح", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                XtraMessageBox.Show("فشل استيراد إعدادات فترة العمل.", "خطأ", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// استيراد إعدادات من فترة عمل
        /// </summary>
        private bool ImportWorkHoursSettings(int workHoursID)
        {
            try
            {
                // استرجاع بيانات فترة العمل من قاعدة البيانات
                using (SqlConnection connection = new ConnectionManager().GetConnection())
                {
                    string query = @"
                        SELECT 
                            Name, 
                            StartTime, 
                            EndTime, 
                            FlexibleMinutes, 
                            LateThresholdMinutes, 
                            ShortDayThresholdMinutes,
                            OverTimeStartMinutes
                        FROM WorkHours
                        WHERE ID = @ID";
                    
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", workHoursID);
                    
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    
                    if (!reader.Read())
                    {
                        // لم يتم العثور على فترة العمل
                        return false;
                    }
                    
                    // استخراج البيانات من القارئ
                    string name = reader["Name"].ToString();
                    TimeSpan startTime = (TimeSpan)reader["StartTime"];
                    TimeSpan endTime = (TimeSpan)reader["EndTime"];
                    int flexibleMinutes = Convert.ToInt32(reader["FlexibleMinutes"]);
                    
                    // إغلاق القارئ
                    reader.Close();
                    
                    // استرجاع أيام العمل من إعدادات الورديات (إن وجدت)
                    query = @"
                        SELECT 
                            SundayEnabled, 
                            MondayEnabled, 
                            TuesdayEnabled, 
                            WednesdayEnabled, 
                            ThursdayEnabled,
                            FridayEnabled,
                            SaturdayEnabled
                        FROM WorkShifts
                        WHERE WorkHoursID = @WorkHoursID AND IsActive = 1";
                    
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@WorkHoursID", workHoursID);
                    
                    reader = command.ExecuteReader();
                    
                    // إعداد الأيام بناءً على قيم المناوبات، إذا وجدت
                    if (reader.Read())
                    {
                        checkEditSunday.Checked = Convert.ToBoolean(reader["SundayEnabled"]);
                        checkEditMonday.Checked = Convert.ToBoolean(reader["MondayEnabled"]);
                        checkEditTuesday.Checked = Convert.ToBoolean(reader["TuesdayEnabled"]);
                        checkEditWednesday.Checked = Convert.ToBoolean(reader["WednesdayEnabled"]);
                        checkEditThursday.Checked = Convert.ToBoolean(reader["ThursdayEnabled"]);
                        checkEditFriday.Checked = Convert.ToBoolean(reader["FridayEnabled"]);
                        checkEditSaturday.Checked = Convert.ToBoolean(reader["SaturdayEnabled"]);
                    }
                    else
                    {
                        // إذا لم يتم العثور على أي مناوبة، نفترض أن هذه الفترة مطبقة على كل الأيام
                        checkEditSunday.Checked = true;
                        checkEditMonday.Checked = true;
                        checkEditTuesday.Checked = true;
                        checkEditWednesday.Checked = true;
                        checkEditThursday.Checked = true;
                        checkEditFriday.Checked = false; // نفترض أن الجمعة عطلة
                        checkEditSaturday.Checked = true;
                    }
                    
                    // تحديث أوقات الدوام
                    DateTime startDateTime = DateTime.Today.Add(startTime);
                    DateTime endDateTime = DateTime.Today.Add(endTime);
                    
                    // تحديث أوقات الدوام لكل يوم
                    timeEditSaturdayStart.Time = startDateTime;
                    timeEditSaturdayEnd.Time = endDateTime;
                    timeEditSundayStart.Time = startDateTime;
                    timeEditSundayEnd.Time = endDateTime;
                    timeEditMondayStart.Time = startDateTime;
                    timeEditMondayEnd.Time = endDateTime;
                    timeEditTuesdayStart.Time = startDateTime;
                    timeEditTuesdayEnd.Time = endDateTime;
                    timeEditWednesdayStart.Time = startDateTime;
                    timeEditWednesdayEnd.Time = endDateTime;
                    timeEditThursdayStart.Time = startDateTime;
                    timeEditThursdayEnd.Time = endDateTime;
                    timeEditFridayStart.Time = startDateTime;
                    timeEditFridayEnd.Time = endDateTime;
                    
                    // تحديث دقائق السماح
                    spinEditGraceMinutes.Value = flexibleMinutes;
                    
                    // تحديث خيارات المرونة
                    checkEditFlexibleStartTime.Checked = flexibleMinutes > 0;
                    
                    // تحديث حالة عناصر التحكم
                    UpdateDayControlsState();
                    
                    // إضافة رسالة للمستخدم
                    XtraMessageBox.Show($"تم استيراد إعدادات فترة العمل '{name}' بنجاح.", "استيراد الإعدادات",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء استيراد إعدادات فترة العمل رقم: {workHoursID}");
                return false;
            }
        }
    }
    
    /// <summary>
    /// نموذج نسخ أوقات الدوام
    /// </summary>
    public class CopyWorkTimesForm : XtraForm
    {
        private ComboBoxEdit comboBoxEditSourceDay;
        private CheckEdit checkEditCopyToAll;
        private CheckEdit checkEditCopyToSaturday;
        private CheckEdit checkEditCopyToSunday;
        private CheckEdit checkEditCopyToMonday;
        private CheckEdit checkEditCopyToTuesday;
        private CheckEdit checkEditCopyToWednesday;
        private CheckEdit checkEditCopyToThursday;
        private CheckEdit checkEditCopyToFriday;
        private SimpleButton simpleButtonOK;
        private SimpleButton simpleButtonCancel;
        private GroupControl groupControlCopyTo;
        private LabelControl labelControl1;
        
        /// <summary>
        /// اليوم المصدر
        /// </summary>
        public string SourceDay { get; private set; }
        
        /// <summary>
        /// نسخ إلى جميع الأيام
        /// </summary>
        public bool CopyToAll { get; private set; }
        
        /// <summary>
        /// نسخ إلى السبت
        /// </summary>
        public bool CopyToSaturday { get; private set; }
        
        /// <summary>
        /// نسخ إلى الأحد
        /// </summary>
        public bool CopyToSunday { get; private set; }
        
        /// <summary>
        /// نسخ إلى الإثنين
        /// </summary>
        public bool CopyToMonday { get; private set; }
        
        /// <summary>
        /// نسخ إلى الثلاثاء
        /// </summary>
        public bool CopyToTuesday { get; private set; }
        
        /// <summary>
        /// نسخ إلى الأربعاء
        /// </summary>
        public bool CopyToWednesday { get; private set; }
        
        /// <summary>
        /// نسخ إلى الخميس
        /// </summary>
        public bool CopyToThursday { get; private set; }
        
        /// <summary>
        /// نسخ إلى الجمعة
        /// </summary>
        public bool CopyToFriday { get; private set; }
        
        public CopyWorkTimesForm()
        {
            InitializeComponent();
            
            // تعبئة قائمة الأيام
            comboBoxEditSourceDay.Properties.Items.AddRange(new string[] {
                "السبت", "الأحد", "الإثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة"
            });
            
            comboBoxEditSourceDay.SelectedIndex = 0;
        }
        
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditSourceDay = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControlCopyTo = new DevExpress.XtraEditors.GroupControl();
            this.checkEditCopyToFriday = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditCopyToThursday = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditCopyToWednesday = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditCopyToTuesday = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditCopyToMonday = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditCopyToSunday = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditCopyToSaturday = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditCopyToAll = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSourceDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlCopyTo)).BeginInit();
            this.groupControlCopyTo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToFriday.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToThursday.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToWednesday.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToTuesday.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToMonday.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToSunday.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToSaturday.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(300, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(59, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "نسخ من يوم:";
            // 
            // comboBoxEditSourceDay
            // 
            this.comboBoxEditSourceDay.Location = new System.Drawing.Point(12, 17);
            this.comboBoxEditSourceDay.Name = "comboBoxEditSourceDay";
            this.comboBoxEditSourceDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditSourceDay.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditSourceDay.Size = new System.Drawing.Size(282, 20);
            this.comboBoxEditSourceDay.TabIndex = 1;
            // 
            // groupControlCopyTo
            // 
            this.groupControlCopyTo.Controls.Add(this.checkEditCopyToFriday);
            this.groupControlCopyTo.Controls.Add(this.checkEditCopyToThursday);
            this.groupControlCopyTo.Controls.Add(this.checkEditCopyToWednesday);
            this.groupControlCopyTo.Controls.Add(this.checkEditCopyToTuesday);
            this.groupControlCopyTo.Controls.Add(this.checkEditCopyToMonday);
            this.groupControlCopyTo.Controls.Add(this.checkEditCopyToSunday);
            this.groupControlCopyTo.Controls.Add(this.checkEditCopyToSaturday);
            this.groupControlCopyTo.Controls.Add(this.checkEditCopyToAll);
            this.groupControlCopyTo.Location = new System.Drawing.Point(12, 43);
            this.groupControlCopyTo.Name = "groupControlCopyTo";
            this.groupControlCopyTo.Size = new System.Drawing.Size(367, 158);
            this.groupControlCopyTo.TabIndex = 2;
            this.groupControlCopyTo.Text = "نسخ إلى";
            // 
            // checkEditCopyToFriday
            // 
            this.checkEditCopyToFriday.Location = new System.Drawing.Point(5, 130);
            this.checkEditCopyToFriday.Name = "checkEditCopyToFriday";
            this.checkEditCopyToFriday.Properties.Caption = "الجمعة";
            this.checkEditCopyToFriday.Size = new System.Drawing.Size(75, 19);
            this.checkEditCopyToFriday.TabIndex = 7;
            // 
            // checkEditCopyToThursday
            // 
            this.checkEditCopyToThursday.Location = new System.Drawing.Point(5, 110);
            this.checkEditCopyToThursday.Name = "checkEditCopyToThursday";
            this.checkEditCopyToThursday.Properties.Caption = "الخميس";
            this.checkEditCopyToThursday.Size = new System.Drawing.Size(75, 19);
            this.checkEditCopyToThursday.TabIndex = 6;
            // 
            // checkEditCopyToWednesday
            // 
            this.checkEditCopyToWednesday.Location = new System.Drawing.Point(5, 90);
            this.checkEditCopyToWednesday.Name = "checkEditCopyToWednesday";
            this.checkEditCopyToWednesday.Properties.Caption = "الأربعاء";
            this.checkEditCopyToWednesday.Size = new System.Drawing.Size(75, 19);
            this.checkEditCopyToWednesday.TabIndex = 5;
            // 
            // checkEditCopyToTuesday
            // 
            this.checkEditCopyToTuesday.Location = new System.Drawing.Point(5, 70);
            this.checkEditCopyToTuesday.Name = "checkEditCopyToTuesday";
            this.checkEditCopyToTuesday.Properties.Caption = "الثلاثاء";
            this.checkEditCopyToTuesday.Size = new System.Drawing.Size(75, 19);
            this.checkEditCopyToTuesday.TabIndex = 4;
            // 
            // checkEditCopyToMonday
            // 
            this.checkEditCopyToMonday.Location = new System.Drawing.Point(5, 50);
            this.checkEditCopyToMonday.Name = "checkEditCopyToMonday";
            this.checkEditCopyToMonday.Properties.Caption = "الإثنين";
            this.checkEditCopyToMonday.Size = new System.Drawing.Size(75, 19);
            this.checkEditCopyToMonday.TabIndex = 3;
            // 
            // checkEditCopyToSunday
            // 
            this.checkEditCopyToSunday.Location = new System.Drawing.Point(86, 24);
            this.checkEditCopyToSunday.Name = "checkEditCopyToSunday";
            this.checkEditCopyToSunday.Properties.Caption = "الأحد";
            this.checkEditCopyToSunday.Size = new System.Drawing.Size(75, 19);
            this.checkEditCopyToSunday.TabIndex = 2;
            // 
            // checkEditCopyToSaturday
            // 
            this.checkEditCopyToSaturday.Location = new System.Drawing.Point(5, 24);
            this.checkEditCopyToSaturday.Name = "checkEditCopyToSaturday";
            this.checkEditCopyToSaturday.Properties.Caption = "السبت";
            this.checkEditCopyToSaturday.Size = new System.Drawing.Size(75, 19);
            this.checkEditCopyToSaturday.TabIndex = 1;
            // 
            // checkEditCopyToAll
            // 
            this.checkEditCopyToAll.Location = new System.Drawing.Point(286, 24);
            this.checkEditCopyToAll.Name = "checkEditCopyToAll";
            this.checkEditCopyToAll.Properties.Caption = "نسخ للكل";
            this.checkEditCopyToAll.Size = new System.Drawing.Size(75, 19);
            this.checkEditCopyToAll.TabIndex = 0;
            this.checkEditCopyToAll.CheckedChanged += new System.EventHandler(this.checkEditCopyToAll_CheckedChanged);
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(12, 207);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 3;
            this.simpleButtonOK.Text = "موافق";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonCancel.Location = new System.Drawing.Point(93, 207);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCancel.TabIndex = 4;
            this.simpleButtonCancel.Text = "إلغاء";
            // 
            // CopyWorkTimesForm
            // 
            this.AcceptButton = this.simpleButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButtonCancel;
            this.ClientSize = new System.Drawing.Size(391, 237);
            this.Controls.Add(this.simpleButtonCancel);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.groupControlCopyTo);
            this.Controls.Add(this.comboBoxEditSourceDay);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CopyWorkTimesForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "نسخ أوقات الدوام";
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSourceDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlCopyTo)).EndInit();
            this.groupControlCopyTo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToFriday.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToThursday.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToWednesday.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToTuesday.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToMonday.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToSunday.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToSaturday.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyToAll.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من اختيار يوم مصدر
                if (comboBoxEditSourceDay.SelectedIndex < 0)
                {
                    XtraMessageBox.Show("يرجى اختيار اليوم المصدر.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBoxEditSourceDay.Focus();
                    return;
                }
                
                // التحقق من اختيار يوم هدف على الأقل
                if (!checkEditCopyToAll.Checked && 
                    !checkEditCopyToSaturday.Checked && !checkEditCopyToSunday.Checked && 
                    !checkEditCopyToMonday.Checked && !checkEditCopyToTuesday.Checked && 
                    !checkEditCopyToWednesday.Checked && !checkEditCopyToThursday.Checked && 
                    !checkEditCopyToFriday.Checked)
                {
                    XtraMessageBox.Show("يرجى اختيار يوم هدف واحد على الأقل.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // حفظ البيانات
                SourceDay = comboBoxEditSourceDay.Text;
                CopyToAll = checkEditCopyToAll.Checked;
                CopyToSaturday = checkEditCopyToSaturday.Checked;
                CopyToSunday = checkEditCopyToSunday.Checked;
                CopyToMonday = checkEditCopyToMonday.Checked;
                CopyToTuesday = checkEditCopyToTuesday.Checked;
                CopyToWednesday = checkEditCopyToWednesday.Checked;
                CopyToThursday = checkEditCopyToThursday.Checked;
                CopyToFriday = checkEditCopyToFriday.Checked;
                
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        private void checkEditCopyToAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // تحديث حالة الخيارات الأخرى
                bool enabled = !checkEditCopyToAll.Checked;
                
                checkEditCopyToSaturday.Enabled = enabled;
                checkEditCopyToSunday.Enabled = enabled;
                checkEditCopyToMonday.Enabled = enabled;
                checkEditCopyToTuesday.Enabled = enabled;
                checkEditCopyToWednesday.Enabled = enabled;
                checkEditCopyToThursday.Enabled = enabled;
                checkEditCopyToFriday.Enabled = enabled;
                
                // إلغاء تحديد الخيارات الأخرى
                if (!enabled)
                {
                    checkEditCopyToSaturday.Checked = false;
                    checkEditCopyToSunday.Checked = false;
                    checkEditCopyToMonday.Checked = false;
                    checkEditCopyToTuesday.Checked = false;
                    checkEditCopyToWednesday.Checked = false;
                    checkEditCopyToThursday.Checked = false;
                    checkEditCopyToFriday.Checked = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }
    }
    
    /// <summary>
    /// نموذج اختيار فترة عمل
    /// </summary>
    public class SelectWorkHoursForm : XtraForm
    {
        private LookUpEdit lookUpEditWorkHours;
        private SimpleButton simpleButtonOK;
        private SimpleButton simpleButtonCancel;
        private LabelControl labelControl1;
        
        /// <summary>
        /// رقم فترة العمل المحددة
        /// </summary>
        public int SelectedWorkHoursID { get; private set; }
        
        public SelectWorkHoursForm()
        {
            InitializeComponent();
            
            // تحميل قائمة فترات العمل
            LoadWorkHours();
        }
        
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditWorkHours = new DevExpress.XtraEditors.LookUpEdit();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditWorkHours.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(279, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "فترة العمل:";
            // 
            // lookUpEditWorkHours
            // 
            this.lookUpEditWorkHours.Location = new System.Drawing.Point(12, 22);
            this.lookUpEditWorkHours.Name = "lookUpEditWorkHours";
            this.lookUpEditWorkHours.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditWorkHours.Properties.NullText = "";
            this.lookUpEditWorkHours.Size = new System.Drawing.Size(261, 20);
            this.lookUpEditWorkHours.TabIndex = 1;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(12, 48);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 2;
            this.simpleButtonOK.Text = "موافق";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonCancel.Location = new System.Drawing.Point(93, 48);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCancel.TabIndex = 3;
            this.simpleButtonCancel.Text = "إلغاء";
            // 
            // SelectWorkHoursForm
            // 
            this.AcceptButton = this.simpleButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButtonCancel;
            this.ClientSize = new System.Drawing.Size(344, 83);
            this.Controls.Add(this.simpleButtonCancel);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.lookUpEditWorkHours);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectWorkHoursForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "اختيار فترة عمل";
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditWorkHours.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        /// <summary>
        /// تحميل قائمة فترات العمل
        /// </summary>
        private void LoadWorkHours()
        {
            try
            {
                // تحميل بيانات فترات العمل
                // في السيناريو الحقيقي، سيتم استرجاع البيانات من قاعدة البيانات
                
                // إنشاء قائمة فترات عمل للاختبار
                List<WorkHourItem> workHours = new List<WorkHourItem>
                {
                    new WorkHourItem { ID = 1, Name = "الدوام الصباحي (8:00 - 16:00)" },
                    new WorkHourItem { ID = 2, Name = "الدوام المسائي (16:00 - 00:00)" },
                    new WorkHourItem { ID = 3, Name = "الدوام الليلي (00:00 - 8:00)" },
                    new WorkHourItem { ID = 4, Name = "دوام مرن (9:00 - 17:00)" }
                };
                
                // ربط البيانات بعنصر الاختيار
                lookUpEditWorkHours.Properties.DataSource = workHours;
                lookUpEditWorkHours.Properties.DisplayMember = "Name";
                lookUpEditWorkHours.Properties.ValueMember = "ID";
                
                // تحديد العناصر المراد عرضها
                lookUpEditWorkHours.Properties.Columns.Clear();
                lookUpEditWorkHours.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "فترة العمل"));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل فترات العمل: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من اختيار فترة عمل
                if (lookUpEditWorkHours.EditValue == null)
                {
                    XtraMessageBox.Show("يرجى اختيار فترة عمل.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lookUpEditWorkHours.Focus();
                    return;
                }
                
                // حفظ البيانات
                SelectedWorkHoursID = Convert.ToInt32(lookUpEditWorkHours.EditValue);
                
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
    }
    
    /// <summary>
    /// فترة عمل للعرض
    /// </summary>
    public class WorkHourItem
    {
        /// <summary>
        /// الرقم
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// الاسم
        /// </summary>
        public string Name { get; set; }
    }
}