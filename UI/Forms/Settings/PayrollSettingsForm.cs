using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using HR.Core;

namespace HR.UI.Forms.Settings
{
    public partial class PayrollSettingsForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly SystemSettingsManager _settingsManager;
        private PayrollSettings _settings;
        
        public PayrollSettingsForm()
        {
            InitializeComponent();
            _settingsManager = new SystemSettingsManager();
        }
        
        private void PayrollSettingsForm_Load(object sender, EventArgs e)
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
            // التحقق من صلاحية تعديل إعدادات الراتب
            bool canEditSettings = SessionManager.HasPermission("Settings.EditPayrollSettings");
            
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
                // استرجاع إعدادات الراتب
                _settings = _settingsManager.GetPayrollSettings();
                
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
            
            // إعدادات دورة الراتب
            dateEditPayrollStartDate.DateTime = _settings.PayrollStartDate;
            spinEditPayrollDays.Value = _settings.PayrollDays;
            comboBoxEditPayrollCalculationMethod.SelectedItem = _settings.PayrollCalculationMethod;
            checkEditAutoApprovePayroll.Checked = _settings.AutoApprovePayroll;
            
            // إعدادات الراتب الأساسي
            spinEditOvertimeMultiplier.Value = (decimal)_settings.OvertimeMultiplier;
            spinEditWeekendOvertimeMultiplier.Value = (decimal)_settings.WeekendOvertimeMultiplier;
            spinEditHolidayOvertimeMultiplier.Value = (decimal)_settings.HolidayOvertimeMultiplier;
            checkEditIncludeAllowancesInOvertime.Checked = _settings.IncludeAllowancesInOvertime;
            
            // إعدادات الضرائب والتأمينات
            spinEditTaxPercentage.Value = (decimal)_settings.TaxPercentage;
            checkEditTaxEnabled.Checked = _settings.TaxEnabled;
            spinEditInsurancePercentage.Value = (decimal)_settings.InsurancePercentage;
            checkEditInsuranceEnabled.Checked = _settings.InsuranceEnabled;
            spinEditPensionPercentage.Value = (decimal)_settings.PensionPercentage;
            checkEditPensionEnabled.Checked = _settings.PensionEnabled;
            
            // تحديث حالة التفعيل لبعض العناصر
            UpdateControlsState();
        }
        
        /// <summary>
        /// تحديث حالة التفعيل لبعض العناصر
        /// </summary>
        private void UpdateControlsState()
        {
            // الضرائب
            spinEditTaxPercentage.Enabled = checkEditTaxEnabled.Checked;
            
            // التأمينات
            spinEditInsurancePercentage.Enabled = checkEditInsuranceEnabled.Checked;
            
            // المعاش
            spinEditPensionPercentage.Enabled = checkEditPensionEnabled.Checked;
        }
        
        /// <summary>
        /// تفعيل أو تعطيل عناصر التحكم
        /// </summary>
        /// <param name="enabled">حالة التفعيل</param>
        private void SetControlsEnabled(bool enabled)
        {
            // إعدادات دورة الراتب
            dateEditPayrollStartDate.Enabled = enabled;
            spinEditPayrollDays.Enabled = enabled;
            comboBoxEditPayrollCalculationMethod.Enabled = enabled;
            checkEditAutoApprovePayroll.Enabled = enabled;
            
            // إعدادات الراتب الأساسي
            spinEditOvertimeMultiplier.Enabled = enabled;
            spinEditWeekendOvertimeMultiplier.Enabled = enabled;
            spinEditHolidayOvertimeMultiplier.Enabled = enabled;
            checkEditIncludeAllowancesInOvertime.Enabled = enabled;
            
            // إعدادات الضرائب والتأمينات
            checkEditTaxEnabled.Enabled = enabled;
            spinEditTaxPercentage.Enabled = enabled && checkEditTaxEnabled.Checked;
            checkEditInsuranceEnabled.Enabled = enabled;
            spinEditInsurancePercentage.Enabled = enabled && checkEditInsuranceEnabled.Checked;
            checkEditPensionEnabled.Enabled = enabled;
            spinEditPensionPercentage.Enabled = enabled && checkEditPensionEnabled.Checked;
        }
        
        /// <summary>
        /// حفظ الإعدادات
        /// </summary>
        private bool SaveSettings()
        {
            try
            {
                // إنشاء كائن الإعدادات
                PayrollSettings settings = new PayrollSettings
                {
                    // إعدادات دورة الراتب
                    PayrollStartDate = dateEditPayrollStartDate.DateTime,
                    PayrollDays = (int)spinEditPayrollDays.Value,
                    PayrollCalculationMethod = comboBoxEditPayrollCalculationMethod.SelectedItem?.ToString() ?? "MonthlyRate",
                    AutoApprovePayroll = checkEditAutoApprovePayroll.Checked,
                    
                    // إعدادات الراتب الأساسي
                    OvertimeMultiplier = (double)spinEditOvertimeMultiplier.Value,
                    WeekendOvertimeMultiplier = (double)spinEditWeekendOvertimeMultiplier.Value,
                    HolidayOvertimeMultiplier = (double)spinEditHolidayOvertimeMultiplier.Value,
                    IncludeAllowancesInOvertime = checkEditIncludeAllowancesInOvertime.Checked,
                    
                    // إعدادات الضرائب والتأمينات
                    TaxEnabled = checkEditTaxEnabled.Checked,
                    TaxPercentage = (double)spinEditTaxPercentage.Value,
                    InsuranceEnabled = checkEditInsuranceEnabled.Checked,
                    InsurancePercentage = (double)spinEditInsurancePercentage.Value,
                    PensionEnabled = checkEditPensionEnabled.Checked,
                    PensionPercentage = (double)spinEditPensionPercentage.Value
                };
                
                // حفظ الإعدادات
                bool result = _settingsManager.SavePayrollSettings(settings);
                
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
            // التحقق من ملء الحقول الضرورية
            if (comboBoxEditPayrollCalculationMethod.SelectedItem == null)
            {
                XtraMessageBox.Show("يرجى اختيار طريقة حساب الراتب.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxEditPayrollCalculationMethod.Focus();
                return false;
            }
            
            // التحقق من القيم الرقمية
            if (spinEditPayrollDays.Value <= 0)
            {
                XtraMessageBox.Show("يجب أن تكون أيام دورة الراتب أكبر من صفر.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditPayrollDays.Focus();
                return false;
            }
            
            if (spinEditOvertimeMultiplier.Value <= 0)
            {
                XtraMessageBox.Show("يجب أن يكون مُضاعف العمل الإضافي أكبر من صفر.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditOvertimeMultiplier.Focus();
                return false;
            }
            
            if (spinEditWeekendOvertimeMultiplier.Value <= 0)
            {
                XtraMessageBox.Show("يجب أن يكون مُضاعف العمل الإضافي في نهاية الأسبوع أكبر من صفر.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditWeekendOvertimeMultiplier.Focus();
                return false;
            }
            
            if (spinEditHolidayOvertimeMultiplier.Value <= 0)
            {
                XtraMessageBox.Show("يجب أن يكون مُضاعف العمل الإضافي في العطلات أكبر من صفر.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditHolidayOvertimeMultiplier.Focus();
                return false;
            }
            
            // التحقق من الضرائب والتأمينات إذا كانت مفعلة
            if (checkEditTaxEnabled.Checked && spinEditTaxPercentage.Value <= 0)
            {
                XtraMessageBox.Show("يجب أن تكون نسبة الضريبة أكبر من صفر.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditTaxPercentage.Focus();
                return false;
            }
            
            if (checkEditInsuranceEnabled.Checked && spinEditInsurancePercentage.Value <= 0)
            {
                XtraMessageBox.Show("يجب أن تكون نسبة التأمينات أكبر من صفر.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditInsurancePercentage.Focus();
                return false;
            }
            
            if (checkEditPensionEnabled.Checked && spinEditPensionPercentage.Value <= 0)
            {
                XtraMessageBox.Show("يجب أن تكون نسبة المعاش أكبر من صفر.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditPensionPercentage.Focus();
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// حدث تغيير حالة تفعيل الضرائب
        /// </summary>
        private void checkEditTaxEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // تحديث حالة عناصر الضرائب
                spinEditTaxPercentage.Enabled = checkEditTaxEnabled.Checked;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث تغيير حالة تفعيل التأمينات
        /// </summary>
        private void checkEditInsuranceEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // تحديث حالة عناصر التأمينات
                spinEditInsurancePercentage.Enabled = checkEditInsuranceEnabled.Checked;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث تغيير حالة تفعيل المعاش
        /// </summary>
        private void checkEditPensionEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // تحديث حالة عناصر المعاش
                spinEditPensionPercentage.Enabled = checkEditPensionEnabled.Checked;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }
    }
}