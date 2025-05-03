using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using HR.Core;
using HR.Core.ZKTeco;
using HR.Models;

namespace HR.UI.Forms.Attendance
{
    /// <summary>
    /// نموذج إدارة أجهزة البصمة
    /// </summary>
    public partial class BiometricDevicesForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly ZKTecoDeviceManager _deviceManager;
        private bool _isSyncing = false;
        
        /// <summary>
        /// منشئ النموذج
        /// </summary>
        public BiometricDevicesForm()
        {
            InitializeComponent();
            _deviceManager = ZKTecoDeviceManager.Instance;
        }
        
        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void BiometricDevicesForm_Load(object sender, EventArgs e)
        {
            LoadDevices();
        }
        
        /// <summary>
        /// تحميل أجهزة البصمة
        /// </summary>
        private void LoadDevices()
        {
            try
            {
                var devices = _deviceManager.GetDevices();
                biometricDeviceBindingSource.DataSource = devices;
                gridViewDevices.RefreshData();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل أجهزة البصمة");
                XtraMessageBox.Show("حدث خطأ أثناء تحميل أجهزة البصمة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر التحديث
        /// </summary>
        private void barButtonItemRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadDevices();
        }
        
        /// <summary>
        /// حدث النقر على زر إضافة جهاز
        /// </summary>
        private void barButtonItemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var form = new BiometricDeviceForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadDevices();
                }
            }
        }
        
        /// <summary>
        /// حدث النقر على زر تعديل
        /// </summary>
        private void barButtonItemEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditSelectedDevice();
        }
        
        /// <summary>
        /// حدث النقر على زر حذف
        /// </summary>
        private void barButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteSelectedDevice();
        }
        
        /// <summary>
        /// حدث النقر على زر مزامنة الكل
        /// </summary>
        private async void barButtonItemSyncAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            await SyncAllDevicesAsync();
        }
        
        /// <summary>
        /// حدث النقر على زر مزامنة المستخدمين
        /// </summary>
        private void barButtonItemSyncUsers_ItemClick(object sender, ItemClickEventArgs e)
        {
            SyncSelectedDeviceUsers();
        }
        
        /// <summary>
        /// حدث النقر على زر مزامنة السجلات
        /// </summary>
        private void barButtonItemSyncLogs_ItemClick(object sender, ItemClickEventArgs e)
        {
            SyncSelectedDeviceLogs();
        }
        
        /// <summary>
        /// حدث النقر على زر معالجة السجلات
        /// </summary>
        private void barButtonItemProcessLogs_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProcessAttendanceLogs();
        }
        
        /// <summary>
        /// حدث النقر على زر اختبار الاتصال
        /// </summary>
        private void barButtonItemTestConnection_ItemClick(object sender, ItemClickEventArgs e)
        {
            TestSelectedDeviceConnection();
        }
        
        /// <summary>
        /// تعديل الجهاز المحدد
        /// </summary>
        private void EditSelectedDevice()
        {
            var selectedDevice = biometricDeviceBindingSource.Current as BiometricDevice;
            if (selectedDevice == null)
            {
                XtraMessageBox.Show("الرجاء تحديد جهاز أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            using (var form = new BiometricDeviceForm(selectedDevice))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadDevices();
                }
            }
        }
        
        /// <summary>
        /// حذف الجهاز المحدد
        /// </summary>
        private void DeleteSelectedDevice()
        {
            var selectedDevice = biometricDeviceBindingSource.Current as BiometricDevice;
            if (selectedDevice == null)
            {
                XtraMessageBox.Show("الرجاء تحديد جهاز أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (XtraMessageBox.Show($"هل أنت متأكد من حذف الجهاز '{selectedDevice.DeviceName}'؟", "تأكيد الحذف", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool result = _deviceManager.DeleteDevice(selectedDevice.ID);
                    
                    if (result)
                    {
                        LoadDevices();
                        XtraMessageBox.Show("تم حذف الجهاز بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("فشل في حذف الجهاز", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"فشل في حذف الجهاز {selectedDevice.ID}");
                    XtraMessageBox.Show("حدث خطأ أثناء حذف الجهاز", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        /// <summary>
        /// اختبار الاتصال بالجهاز المحدد
        /// </summary>
        private void TestSelectedDeviceConnection()
        {
            var selectedDevice = biometricDeviceBindingSource.Current as BiometricDevice;
            if (selectedDevice == null)
            {
                XtraMessageBox.Show("الرجاء تحديد جهاز أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                Cursor = Cursors.WaitCursor;
                
                var result = _deviceManager.TestConnection(selectedDevice.IPAddress, selectedDevice.Port ?? 4370);
                
                if (result.IsSuccess)
                {
                    string deviceInfo = "";
                    if (result.DeviceInfo != null)
                    {
                        deviceInfo = $"\nنوع الجهاز: {result.DeviceInfo.DeviceType}" +
                                     $"\nالرقم التسلسلي: {result.DeviceInfo.SerialNumber}" +
                                     $"\nالإصدار: {result.DeviceInfo.FirmwareVersion}" +
                                     $"\nوقت الجهاز: {result.DeviceInfo.DeviceTime}";
                    }
                    
                    XtraMessageBox.Show($"تم الاتصال بالجهاز بنجاح{deviceInfo}", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show($"فشل في الاتصال بالجهاز\n{result.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في اختبار الاتصال بالجهاز {selectedDevice.DeviceName}");
                XtraMessageBox.Show("حدث خطأ أثناء اختبار الاتصال بالجهاز", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// مزامنة مستخدمي الجهاز المحدد
        /// </summary>
        private void SyncSelectedDeviceUsers()
        {
            var selectedDevice = biometricDeviceBindingSource.Current as BiometricDevice;
            if (selectedDevice == null)
            {
                XtraMessageBox.Show("الرجاء تحديد جهاز أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                Cursor = Cursors.WaitCursor;
                
                var result = _deviceManager.SyncUsers(selectedDevice.ID);
                
                if (result.IsSuccess)
                {
                    LoadDevices();
                    XtraMessageBox.Show($"تمت مزامنة المستخدمين بنجاح\nإجمالي السجلات: {result.TotalRecords}\nالسجلات الجديدة: {result.NewRecords}\nالأخطاء: {result.ErrorRecords}", 
                        "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show($"فشل في مزامنة المستخدمين\n{result.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في مزامنة مستخدمي الجهاز {selectedDevice.DeviceName}");
                XtraMessageBox.Show("حدث خطأ أثناء مزامنة المستخدمين", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// مزامنة سجلات الجهاز المحدد
        /// </summary>
        private void SyncSelectedDeviceLogs()
        {
            var selectedDevice = biometricDeviceBindingSource.Current as BiometricDevice;
            if (selectedDevice == null)
            {
                XtraMessageBox.Show("الرجاء تحديد جهاز أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                Cursor = Cursors.WaitCursor;
                
                var result = _deviceManager.SyncAttendanceLogs(selectedDevice.ID);
                
                if (result.IsSuccess)
                {
                    LoadDevices();
                    XtraMessageBox.Show($"تمت مزامنة سجلات الحضور بنجاح\nإجمالي السجلات: {result.TotalRecords}\nالسجلات الجديدة: {result.NewRecords}\nالأخطاء: {result.ErrorRecords}", 
                        "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show($"فشل في مزامنة سجلات الحضور\n{result.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في مزامنة سجلات الجهاز {selectedDevice.DeviceName}");
                XtraMessageBox.Show("حدث خطأ أثناء مزامنة سجلات الحضور", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// معالجة سجلات الحضور
        /// </summary>
        private void ProcessAttendanceLogs()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                var result = _deviceManager.ProcessAttendanceLogs();
                
                if (result.IsSuccess)
                {
                    XtraMessageBox.Show($"تمت معالجة سجلات الحضور بنجاح\nإجمالي السجلات: {result.TotalRecords}\nالسجلات المعالجة: {result.ProcessedRecords}\nالأخطاء: {result.ErrorRecords}", 
                        "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show($"فشل في معالجة سجلات الحضور\n{result.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في معالجة سجلات الحضور");
                XtraMessageBox.Show("حدث خطأ أثناء معالجة سجلات الحضور", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// مزامنة جميع الأجهزة
        /// </summary>
        private async Task SyncAllDevicesAsync()
        {
            if (_isSyncing)
            {
                XtraMessageBox.Show("توجد عملية مزامنة جارية بالفعل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                _isSyncing = true;
                Cursor = Cursors.WaitCursor;
                
                // تعطيل الأزرار أثناء المزامنة
                SetSyncButtonsEnabled(false);
                
                // إظهار نافذة التقدم
                using (var progressForm = new DevExpress.XtraWaitForm.ProgressPanel())
                {
                    progressForm.Caption = "جاري المزامنة";
                    progressForm.Description = "يرجى الانتظار...";
                    progressForm.Show();
                    progressForm.Refresh();
                    
                    var result = await _deviceManager.SyncAllDevicesAsync();
                    
                    progressForm.Close();
                    
                    if (result.IsSuccess)
                    {
                        LoadDevices();
                        
                        int totalDevices = result.DeviceResults.Count;
                        int successDevices = result.DeviceResults.Count(d => d.IsSuccess);
                        
                        string details = "تفاصيل المزامنة:\n";
                        foreach (var deviceResult in result.DeviceResults)
                        {
                            details += $"- {deviceResult.DeviceName} ({deviceResult.IPAddress}): {(deviceResult.IsSuccess ? "نجاح" : "فشل")}";
                            if (!deviceResult.IsSuccess && !string.IsNullOrEmpty(deviceResult.ErrorMessage))
                            {
                                details += $" - {deviceResult.ErrorMessage}";
                            }
                            details += "\n";
                        }
                        
                        XtraMessageBox.Show($"تمت مزامنة الأجهزة\nالأجهزة الناجحة: {successDevices}/{totalDevices}\n\n{details}", 
                            "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show($"فشل في مزامنة الأجهزة\n{result.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في مزامنة جميع الأجهزة");
                XtraMessageBox.Show("حدث خطأ أثناء مزامنة الأجهزة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isSyncing = false;
                Cursor = Cursors.Default;
                
                // إعادة تفعيل الأزرار
                SetSyncButtonsEnabled(true);
            }
        }
        
        /// <summary>
        /// تفعيل/تعطيل أزرار المزامنة
        /// </summary>
        private void SetSyncButtonsEnabled(bool enabled)
        {
            barButtonItemSyncAll.Enabled = enabled;
            barButtonItemSyncUsers.Enabled = enabled;
            barButtonItemSyncLogs.Enabled = enabled;
            barButtonItemProcessLogs.Enabled = enabled;
        }
        
        /// <summary>
        /// حدث النقر على زر التعديل في الجدول
        /// </summary>
        private void repositoryItemButtonEditEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            EditSelectedDevice();
        }
        
        /// <summary>
        /// حدث النقر على زر اختبار الاتصال في الجدول
        /// </summary>
        private void repositoryItemButtonEditTest_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            TestSelectedDeviceConnection();
        }
        
        /// <summary>
        /// حدث النقر على زر الحذف في الجدول
        /// </summary>
        private void repositoryItemButtonEditDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DeleteSelectedDevice();
        }
    }
}