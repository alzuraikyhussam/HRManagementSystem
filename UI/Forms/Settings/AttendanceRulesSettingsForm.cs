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
    public partial class AttendanceRulesSettingsForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly SystemSettingsManager _settingsManager;
        private AttendanceRulesSettings _settings;
        
        public AttendanceRulesSettingsForm()
        {
            InitializeComponent();
            _settingsManager = new SystemSettingsManager();
        }
        
        private void AttendanceRulesSettingsForm_Load(object sender, EventArgs e)
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
            // التحقق من صلاحية تعديل إعدادات قواعد الحضور
            bool canEditSettings = SessionManager.HasPermission("Settings.EditAttendanceSettings");
            
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
                // استرجاع إعدادات قواعد الحضور
                _settings = _settingsManager.GetAttendanceRulesSettings();
                
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
            
            // قواعد الغياب
            spinEditAbsentDaysDeduction.Value = (decimal)_settings.AbsentDaysDeduction;
            spinEditMaxAllowedAbsentDays.Value = _settings.MaxAllowedAbsentDays;
            
            // قواعد التأخير
            checkEditLateArrivalPenaltyEnabled.Checked = _settings.LateArrivalPenaltyEnabled;
            spinEditLateArrivalGraceMinutes.Value = _settings.LateArrivalGraceMinutes;
            spinEditLateArrivalPenaltyPerMinute.Value = (decimal)_settings.LateArrivalPenaltyPerMinute;
            spinEditLateArrivalMaxPenaltyPerDay.Value = (decimal)_settings.LateArrivalMaxPenaltyPerDay;
            
            // قواعد المغادرة المبكرة
            checkEditEarlyDeparturePenaltyEnabled.Checked = _settings.EarlyDeparturePenaltyEnabled;
            spinEditEarlyDepartureGraceMinutes.Value = _settings.EarlyDepartureGraceMinutes;
            spinEditEarlyDeparturePenaltyPerMinute.Value = (decimal)_settings.EarlyDeparturePenaltyPerMinute;
            spinEditEarlyDepartureMaxPenaltyPerDay.Value = (decimal)_settings.EarlyDepartureMaxPenaltyPerDay;
            
            // إعدادات احتساب العمل الإضافي
            checkEditOvertimeEnabled.Checked = _settings.OvertimeEnabled;
            spinEditOvertimeStartAfterMinutes.Value = _settings.OvertimeStartAfterMinutes;
            spinEditOvertimeMultiplier.Value = (decimal)_settings.OvertimeMultiplier;
            spinEditWeekendOvertimeMultiplier.Value = (decimal)_settings.WeekendOvertimeMultiplier;
            spinEditHolidayOvertimeMultiplier.Value = (decimal)_settings.HolidayOvertimeMultiplier;
            
            // إعدادات التصاريح
            spinEditMaxPermissionsPerMonth.Value = _settings.MaxPermissionsPerMonth;
            spinEditMaxPermissionMinutesPerDay.Value = _settings.MaxPermissionMinutesPerDay;
            
            // تحديث حالة التفعيل لبعض العناصر
            UpdateControlsState();
        }
        
        /// <summary>
        /// تحديث حالة التفعيل لبعض العناصر
        /// </summary>
        private void UpdateControlsState()
        {
            // قواعد التأخير
            bool lateArrivalPenaltyEnabled = checkEditLateArrivalPenaltyEnabled.Checked;
            spinEditLateArrivalGraceMinutes.Enabled = lateArrivalPenaltyEnabled;
            spinEditLateArrivalPenaltyPerMinute.Enabled = lateArrivalPenaltyEnabled;
            spinEditLateArrivalMaxPenaltyPerDay.Enabled = lateArrivalPenaltyEnabled;
            
            // قواعد المغادرة المبكرة
            bool earlyDeparturePenaltyEnabled = checkEditEarlyDeparturePenaltyEnabled.Checked;
            spinEditEarlyDepartureGraceMinutes.Enabled = earlyDeparturePenaltyEnabled;
            spinEditEarlyDeparturePenaltyPerMinute.Enabled = earlyDeparturePenaltyEnabled;
            spinEditEarlyDepartureMaxPenaltyPerDay.Enabled = earlyDeparturePenaltyEnabled;
            
            // إعدادات احتساب العمل الإضافي
            bool overtimeEnabled = checkEditOvertimeEnabled.Checked;
            spinEditOvertimeStartAfterMinutes.Enabled = overtimeEnabled;
            spinEditOvertimeMultiplier.Enabled = overtimeEnabled;
            spinEditWeekendOvertimeMultiplier.Enabled = overtimeEnabled;
            spinEditHolidayOvertimeMultiplier.Enabled = overtimeEnabled;
        }
        
        /// <summary>
        /// تفعيل أو تعطيل عناصر التحكم
        /// </summary>
        /// <param name="enabled">حالة التفعيل</param>
        private void SetControlsEnabled(bool enabled)
        {
            // قواعد الغياب
            spinEditAbsentDaysDeduction.Enabled = enabled;
            spinEditMaxAllowedAbsentDays.Enabled = enabled;
            
            // قواعد التأخير
            checkEditLateArrivalPenaltyEnabled.Enabled = enabled;
            bool lateArrivalPenaltyEnabled = checkEditLateArrivalPenaltyEnabled.Checked;
            spinEditLateArrivalGraceMinutes.Enabled = enabled && lateArrivalPenaltyEnabled;
            spinEditLateArrivalPenaltyPerMinute.Enabled = enabled && lateArrivalPenaltyEnabled;
            spinEditLateArrivalMaxPenaltyPerDay.Enabled = enabled && lateArrivalPenaltyEnabled;
            
            // قواعد المغادرة المبكرة
            checkEditEarlyDeparturePenaltyEnabled.Enabled = enabled;
            bool earlyDeparturePenaltyEnabled = checkEditEarlyDeparturePenaltyEnabled.Checked;
            spinEditEarlyDepartureGraceMinutes.Enabled = enabled && earlyDeparturePenaltyEnabled;
            spinEditEarlyDeparturePenaltyPerMinute.Enabled = enabled && earlyDeparturePenaltyEnabled;
            spinEditEarlyDepartureMaxPenaltyPerDay.Enabled = enabled && earlyDeparturePenaltyEnabled;
            
            // إعدادات احتساب العمل الإضافي
            checkEditOvertimeEnabled.Enabled = enabled;
            bool overtimeEnabled = checkEditOvertimeEnabled.Checked;
            spinEditOvertimeStartAfterMinutes.Enabled = enabled && overtimeEnabled;
            spinEditOvertimeMultiplier.Enabled = enabled && overtimeEnabled;
            spinEditWeekendOvertimeMultiplier.Enabled = enabled && overtimeEnabled;
            spinEditHolidayOvertimeMultiplier.Enabled = enabled && overtimeEnabled;
            
            // إعدادات التصاريح
            spinEditMaxPermissionsPerMonth.Enabled = enabled;
            spinEditMaxPermissionMinutesPerDay.Enabled = enabled;
        }
        
        /// <summary>
        /// حفظ الإعدادات
        /// </summary>
        private bool SaveSettings()
        {
            try
            {
                // إنشاء كائن الإعدادات
                AttendanceRulesSettings settings = new AttendanceRulesSettings
                {
                    // قواعد الغياب
                    AbsentDaysDeduction = spinEditAbsentDaysDeduction.Value,
                    MaxAllowedAbsentDays = (int)spinEditMaxAllowedAbsentDays.Value,
                    
                    // قواعد التأخير
                    LateArrivalPenaltyEnabled = checkEditLateArrivalPenaltyEnabled.Checked,
                    LateArrivalGraceMinutes = (int)spinEditLateArrivalGraceMinutes.Value,
                    LateArrivalPenaltyPerMinute = spinEditLateArrivalPenaltyPerMinute.Value,
                    LateArrivalMaxPenaltyPerDay = spinEditLateArrivalMaxPenaltyPerDay.Value,
                    
                    // قواعد المغادرة المبكرة
                    EarlyDeparturePenaltyEnabled = checkEditEarlyDeparturePenaltyEnabled.Checked,
                    EarlyDepartureGraceMinutes = (int)spinEditEarlyDepartureGraceMinutes.Value,
                    EarlyDeparturePenaltyPerMinute = spinEditEarlyDeparturePenaltyPerMinute.Value,
                    EarlyDepartureMaxPenaltyPerDay = spinEditEarlyDepartureMaxPenaltyPerDay.Value,
                    
                    // إعدادات احتساب العمل الإضافي
                    OvertimeEnabled = checkEditOvertimeEnabled.Checked,
                    OvertimeStartAfterMinutes = (int)spinEditOvertimeStartAfterMinutes.Value,
                    OvertimeMultiplier = spinEditOvertimeMultiplier.Value,
                    WeekendOvertimeMultiplier = spinEditWeekendOvertimeMultiplier.Value,
                    HolidayOvertimeMultiplier = spinEditHolidayOvertimeMultiplier.Value,
                    
                    // إعدادات التصاريح
                    MaxPermissionsPerMonth = (int)spinEditMaxPermissionsPerMonth.Value,
                    MaxPermissionMinutesPerDay = (int)spinEditMaxPermissionMinutesPerDay.Value
                };
                
                // حفظ الإعدادات
                bool result = _settingsManager.SaveAttendanceRulesSettings(settings);
                
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
        /// التحقق من البيانات المدخلة
        /// </summary>
        private bool ValidateInput()
        {
            // قواعد الغياب
            if (spinEditAbsentDaysDeduction.Value < 0)
            {
                XtraMessageBox.Show("عدد أيام الخصم عن اليوم الغياب يجب أن يكون قيمة موجبة.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditAbsentDaysDeduction.Focus();
                return false;
            }
            
            if (spinEditMaxAllowedAbsentDays.Value < 0)
            {
                XtraMessageBox.Show("الحد الأقصى لأيام الغياب المسموح بها يجب أن يكون قيمة موجبة.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditMaxAllowedAbsentDays.Focus();
                return false;
            }
            
            // قواعد التأخير
            if (checkEditLateArrivalPenaltyEnabled.Checked)
            {
                if (spinEditLateArrivalGraceMinutes.Value < 0)
                {
                    XtraMessageBox.Show("دقائق السماح للتأخير يجب أن تكون قيمة موجبة.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    spinEditLateArrivalGraceMinutes.Focus();
                    return false;
                }
                
                if (spinEditLateArrivalPenaltyPerMinute.Value < 0)
                {
                    XtraMessageBox.Show("مقدار الخصم لكل دقيقة تأخير يجب أن يكون قيمة موجبة.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    spinEditLateArrivalPenaltyPerMinute.Focus();
                    return false;
                }
                
                if (spinEditLateArrivalMaxPenaltyPerDay.Value < 0)
                {
                    XtraMessageBox.Show("الحد الأقصى للخصم اليومي بسبب التأخير يجب أن يكون قيمة موجبة.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    spinEditLateArrivalMaxPenaltyPerDay.Focus();
                    return false;
                }
            }
            
            // قواعد المغادرة المبكرة
            if (checkEditEarlyDeparturePenaltyEnabled.Checked)
            {
                if (spinEditEarlyDepartureGraceMinutes.Value < 0)
                {
                    XtraMessageBox.Show("دقائق السماح للمغادرة المبكرة يجب أن تكون قيمة موجبة.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    spinEditEarlyDepartureGraceMinutes.Focus();
                    return false;
                }
                
                if (spinEditEarlyDeparturePenaltyPerMinute.Value < 0)
                {
                    XtraMessageBox.Show("مقدار الخصم لكل دقيقة مغادرة مبكرة يجب أن يكون قيمة موجبة.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    spinEditEarlyDeparturePenaltyPerMinute.Focus();
                    return false;
                }
                
                if (spinEditEarlyDepartureMaxPenaltyPerDay.Value < 0)
                {
                    XtraMessageBox.Show("الحد الأقصى للخصم اليومي بسبب المغادرة المبكرة يجب أن يكون قيمة موجبة.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    spinEditEarlyDepartureMaxPenaltyPerDay.Focus();
                    return false;
                }
            }
            
            // إعدادات احتساب العمل الإضافي
            if (checkEditOvertimeEnabled.Checked)
            {
                if (spinEditOvertimeStartAfterMinutes.Value < 0)
                {
                    XtraMessageBox.Show("بدء احتساب العمل الإضافي بعد عدد دقائق يجب أن يكون قيمة موجبة.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    spinEditOvertimeStartAfterMinutes.Focus();
                    return false;
                }
                
                if (spinEditOvertimeMultiplier.Value <= 0)
                {
                    XtraMessageBox.Show("مضاعف العمل الإضافي يجب أن يكون قيمة موجبة أكبر من الصفر.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    spinEditOvertimeMultiplier.Focus();
                    return false;
                }
                
                if (spinEditWeekendOvertimeMultiplier.Value <= 0)
                {
                    XtraMessageBox.Show("مضاعف العمل الإضافي في نهاية الأسبوع يجب أن يكون قيمة موجبة أكبر من الصفر.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    spinEditWeekendOvertimeMultiplier.Focus();
                    return false;
                }
                
                if (spinEditHolidayOvertimeMultiplier.Value <= 0)
                {
                    XtraMessageBox.Show("مضاعف العمل الإضافي في العطلات يجب أن يكون قيمة موجبة أكبر من الصفر.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    spinEditHolidayOvertimeMultiplier.Focus();
                    return false;
                }
            }
            
            // إعدادات التصاريح
            if (spinEditMaxPermissionsPerMonth.Value < 0)
            {
                XtraMessageBox.Show("الحد الأقصى للتصاريح في الشهر يجب أن يكون قيمة موجبة.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditMaxPermissionsPerMonth.Focus();
                return false;
            }
            
            if (spinEditMaxPermissionMinutesPerDay.Value < 0)
            {
                XtraMessageBox.Show("الحد الأقصى لدقائق التصريح في اليوم يجب أن يكون قيمة موجبة.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditMaxPermissionMinutesPerDay.Focus();
                return false;
            }
            
            return true;
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
        /// حدث تغيير حالة تفعيل خصم التأخير
        /// </summary>
        private void checkEditLateArrivalPenaltyEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // تحديث حالة التفعيل لعناصر خصم التأخير
                bool enabled = checkEditLateArrivalPenaltyEnabled.Checked;
                spinEditLateArrivalGraceMinutes.Enabled = enabled;
                spinEditLateArrivalPenaltyPerMinute.Enabled = enabled;
                spinEditLateArrivalMaxPenaltyPerDay.Enabled = enabled;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث تغيير حالة تفعيل خصم المغادرة المبكرة
        /// </summary>
        private void checkEditEarlyDeparturePenaltyEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // تحديث حالة التفعيل لعناصر خصم المغادرة المبكرة
                bool enabled = checkEditEarlyDeparturePenaltyEnabled.Checked;
                spinEditEarlyDepartureGraceMinutes.Enabled = enabled;
                spinEditEarlyDeparturePenaltyPerMinute.Enabled = enabled;
                spinEditEarlyDepartureMaxPenaltyPerDay.Enabled = enabled;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث تغيير حالة تفعيل احتساب العمل الإضافي
        /// </summary>
        private void checkEditOvertimeEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // تحديث حالة التفعيل لعناصر احتساب العمل الإضافي
                bool enabled = checkEditOvertimeEnabled.Checked;
                spinEditOvertimeStartAfterMinutes.Enabled = enabled;
                spinEditOvertimeMultiplier.Enabled = enabled;
                spinEditWeekendOvertimeMultiplier.Enabled = enabled;
                spinEditHolidayOvertimeMultiplier.Enabled = enabled;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }
    }
}