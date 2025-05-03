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
    public partial class NotificationSettingsForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly SystemSettingsManager _settingsManager;
        private NotificationSettings _settings;
        
        public NotificationSettingsForm()
        {
            InitializeComponent();
            _settingsManager = new SystemSettingsManager();
        }
        
        private void NotificationSettingsForm_Load(object sender, EventArgs e)
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
            // التحقق من صلاحية تعديل إعدادات الإشعارات
            bool canEditSettings = SessionManager.HasPermission("Settings.EditNotificationSettings");
            
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
                // استرجاع إعدادات الإشعارات
                _settings = _settingsManager.GetNotificationSettings();
                
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
            
            // إعدادات تفعيل الإشعارات
            checkEditEnableNotifications.Checked = _settings.EnableNotifications;
            checkEditEnableEmailNotifications.Checked = _settings.EnableEmailNotifications;
            checkEditEnableSMSNotifications.Checked = _settings.EnableSMSNotifications;
            checkEditEnableSystemNotifications.Checked = _settings.EnableSystemNotifications;
            
            // إعدادات الإشعارات حسب النوع
            checkEditNewLeaveRequestNotification.Checked = _settings.NotifyOnNewLeaveRequest;
            checkEditLeaveRequestApprovedNotification.Checked = _settings.NotifyOnLeaveRequestApproved;
            checkEditLeaveRequestRejectedNotification.Checked = _settings.NotifyOnLeaveRequestRejected;
            checkEditNewEmployeeNotification.Checked = _settings.NotifyOnNewEmployee;
            checkEditEmployeeTerminationNotification.Checked = _settings.NotifyOnEmployeeTermination;
            checkEditAttendanceIssueNotification.Checked = _settings.NotifyOnAttendanceIssue;
            checkEditSalaryIssueNotification.Checked = _settings.NotifyOnSalaryIssue;
            
            // إعدادات مستوى الإشعارات
            radioGroupNotificationLevel.SelectedIndex = (int)_settings.NotificationLevel;
            
            // إعدادات توقيت الإشعارات
            spinEditDailyNotificationHour.Value = _settings.DailyNotificationHour;
            
            // تحديث حالة التفعيل لبعض العناصر
            UpdateControlsState();
        }
        
        /// <summary>
        /// تحديث حالة التفعيل لبعض العناصر
        /// </summary>
        private void UpdateControlsState()
        {
            bool notificationsEnabled = checkEditEnableNotifications.Checked;
            
            // إعدادات وسائل الإشعارات
            checkEditEnableEmailNotifications.Enabled = notificationsEnabled;
            checkEditEnableSMSNotifications.Enabled = notificationsEnabled;
            checkEditEnableSystemNotifications.Enabled = notificationsEnabled;
            
            // إعدادات نوع الإشعارات
            groupControlNotificationTypes.Enabled = notificationsEnabled;
            
            // إعدادات مستوى الإشعارات
            groupControlNotificationLevel.Enabled = notificationsEnabled;
            
            // إعدادات توقيت الإشعارات
            groupControlNotificationTiming.Enabled = notificationsEnabled;
        }
        
        /// <summary>
        /// تفعيل أو تعطيل عناصر التحكم
        /// </summary>
        /// <param name="enabled">حالة التفعيل</param>
        private void SetControlsEnabled(bool enabled)
        {
            // إعدادات تفعيل الإشعارات
            checkEditEnableNotifications.Enabled = enabled;
            
            // إعدادات وسائل الإشعارات (تعتمد أيضًا على تفعيل الإشعارات)
            bool notificationsEnabled = checkEditEnableNotifications.Checked;
            checkEditEnableEmailNotifications.Enabled = enabled && notificationsEnabled;
            checkEditEnableSMSNotifications.Enabled = enabled && notificationsEnabled;
            checkEditEnableSystemNotifications.Enabled = enabled && notificationsEnabled;
            
            // إعدادات نوع الإشعارات
            groupControlNotificationTypes.Enabled = enabled && notificationsEnabled;
            
            // إعدادات مستوى الإشعارات
            groupControlNotificationLevel.Enabled = enabled && notificationsEnabled;
            
            // إعدادات توقيت الإشعارات
            groupControlNotificationTiming.Enabled = enabled && notificationsEnabled;
        }
        
        /// <summary>
        /// حفظ الإعدادات
        /// </summary>
        private bool SaveSettings()
        {
            try
            {
                // إنشاء كائن الإعدادات
                NotificationSettings settings = new NotificationSettings
                {
                    // إعدادات تفعيل الإشعارات
                    EnableNotifications = checkEditEnableNotifications.Checked,
                    EnableEmailNotifications = checkEditEnableEmailNotifications.Checked,
                    EnableSMSNotifications = checkEditEnableSMSNotifications.Checked,
                    EnableSystemNotifications = checkEditEnableSystemNotifications.Checked,
                    
                    // إعدادات الإشعارات حسب النوع
                    NotifyOnNewLeaveRequest = checkEditNewLeaveRequestNotification.Checked,
                    NotifyOnLeaveRequestApproved = checkEditLeaveRequestApprovedNotification.Checked,
                    NotifyOnLeaveRequestRejected = checkEditLeaveRequestRejectedNotification.Checked,
                    NotifyOnNewEmployee = checkEditNewEmployeeNotification.Checked,
                    NotifyOnEmployeeTermination = checkEditEmployeeTerminationNotification.Checked,
                    NotifyOnAttendanceIssue = checkEditAttendanceIssueNotification.Checked,
                    NotifyOnSalaryIssue = checkEditSalaryIssueNotification.Checked,
                    
                    // إعدادات مستوى الإشعارات
                    NotificationLevel = (NotificationLevel)radioGroupNotificationLevel.SelectedIndex,
                    
                    // إعدادات توقيت الإشعارات
                    DailyNotificationHour = (int)spinEditDailyNotificationHour.Value
                };
                
                // حفظ الإعدادات
                bool result = _settingsManager.SaveNotificationSettings(settings);
                
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
            // التحقق من وجود وسيلة إشعار مفعلة إذا كانت الإشعارات مفعلة
            if (checkEditEnableNotifications.Checked &&
                !checkEditEnableEmailNotifications.Checked &&
                !checkEditEnableSMSNotifications.Checked &&
                !checkEditEnableSystemNotifications.Checked)
            {
                XtraMessageBox.Show("يجب تفعيل وسيلة إشعار واحدة على الأقل عند تفعيل الإشعارات.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                checkEditEnableEmailNotifications.Focus();
                return false;
            }
            
            // التحقق من وجود نوع إشعار مفعل إذا كانت الإشعارات مفعلة
            if (checkEditEnableNotifications.Checked &&
                !checkEditNewLeaveRequestNotification.Checked &&
                !checkEditLeaveRequestApprovedNotification.Checked &&
                !checkEditLeaveRequestRejectedNotification.Checked &&
                !checkEditNewEmployeeNotification.Checked &&
                !checkEditEmployeeTerminationNotification.Checked &&
                !checkEditAttendanceIssueNotification.Checked &&
                !checkEditSalaryIssueNotification.Checked)
            {
                XtraMessageBox.Show("يجب تفعيل نوع إشعار واحد على الأقل عند تفعيل الإشعارات.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                checkEditNewLeaveRequestNotification.Focus();
                return false;
            }
            
            // التحقق من ساعة الإشعار اليومي
            if (spinEditDailyNotificationHour.Value < 0 || spinEditDailyNotificationHour.Value > 23)
            {
                XtraMessageBox.Show("يجب أن تكون ساعة الإشعار اليومي بين 0 و 23.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditDailyNotificationHour.Focus();
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// حدث تغيير حالة تفعيل الإشعارات
        /// </summary>
        private void checkEditEnableNotifications_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // تحديث حالة التفعيل لبعض العناصر
                UpdateControlsState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }
    }
    
    /// <summary>
    /// مستوى الإشعارات
    /// </summary>
    public enum NotificationLevel
    {
        /// <summary>
        /// منخفض (الأحداث المهمة فقط)
        /// </summary>
        Low = 0,
        
        /// <summary>
        /// متوسط (الأحداث المهمة والمتوسطة)
        /// </summary>
        Medium = 1,
        
        /// <summary>
        /// عالي (جميع الأحداث)
        /// </summary>
        High = 2
    }
}