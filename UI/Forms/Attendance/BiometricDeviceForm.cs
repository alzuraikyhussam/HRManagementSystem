using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.Core.ZKTeco;
using HR.Models;

namespace HR.UI.Forms.Attendance
{
    /// <summary>
    /// نموذج إضافة/تعديل جهاز بصمة
    /// </summary>
    public partial class BiometricDeviceForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly ZKTecoDeviceManager _deviceManager;
        private readonly BiometricDevice _device;
        private readonly bool _isNewDevice;
        private bool _dataChanged = false;
        
        /// <summary>
        /// منشئ النموذج لإضافة جهاز جديد
        /// </summary>
        public BiometricDeviceForm()
        {
            InitializeComponent();
            
            _deviceManager = ZKTecoDeviceManager.Instance;
            _device = new BiometricDevice();
            _isNewDevice = true;
            
            // تهيئة افتراضية
            _device.IsActive = true;
            _device.Port = 4370;
        }
        
        /// <summary>
        /// منشئ النموذج لتعديل جهاز موجود
        /// </summary>
        /// <param name="device">بيانات الجهاز</param>
        public BiometricDeviceForm(BiometricDevice device)
        {
            InitializeComponent();
            
            _deviceManager = ZKTecoDeviceManager.Instance;
            _device = device;
            _isNewDevice = false;
        }
        
        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void BiometricDeviceForm_Load(object sender, EventArgs e)
        {
            LoadDeviceData();
        }
        
        /// <summary>
        /// تحميل بيانات الجهاز إلى النموذج
        /// </summary>
        private void LoadDeviceData()
        {
            textEditDeviceName.Text = _device.DeviceName;
            textEditDeviceModel.Text = _device.DeviceModel;
            textEditSerialNumber.Text = _device.SerialNumber;
            textEditIPAddress.Text = _device.IPAddress;
            spinEditPort.Value = _device.Port ?? 4370;
            textEditCommunicationKey.Text = _device.CommunicationKey;
            textEditLocation.Text = _device.Location;
            memoEditDescription.Text = _device.Description;
            checkEditIsActive.Checked = _device.IsActive;
            
            // تحديث عنوان النموذج
            this.Text = _isNewDevice ? "إضافة جهاز بصمة جديد" : $"تعديل بيانات الجهاز: {_device.DeviceName}";
            
            // تمكين التحقق من تغيير البيانات
            _dataChanged = false;
        }
        
        /// <summary>
        /// حدث النقر على زر اختبار الاتصال
        /// </summary>
        private void simpleButtonTestConnection_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textEditIPAddress.Text))
            {
                XtraMessageBox.Show("الرجاء إدخال عنوان IP للجهاز", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textEditIPAddress.Focus();
                return;
            }
            
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // اختبار الاتصال
                var result = _deviceManager.TestConnection(textEditIPAddress.Text, (int)spinEditPort.Value);
                
                if (result.IsSuccess)
                {
                    string deviceInfo = "";
                    if (result.DeviceInfo != null)
                    {
                        deviceInfo = $"\nنوع الجهاز: {result.DeviceInfo.DeviceType}" +
                                     $"\nالرقم التسلسلي: {result.DeviceInfo.SerialNumber}" +
                                     $"\nالإصدار: {result.DeviceInfo.FirmwareVersion}" +
                                     $"\nوقت الجهاز: {result.DeviceInfo.DeviceTime}";
                                     
                        // تعبئة بعض البيانات تلقائياً
                        if (string.IsNullOrEmpty(textEditSerialNumber.Text))
                        {
                            textEditSerialNumber.Text = result.DeviceInfo.SerialNumber;
                            _dataChanged = true;
                        }
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
                LogManager.LogException(ex, "فشل في اختبار الاتصال بالجهاز");
                XtraMessageBox.Show("حدث خطأ أثناء اختبار الاتصال بالجهاز", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// حدث النقر على زر الحفظ
        /// </summary>
        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;
            
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // جمع البيانات من النموذج
                _device.DeviceName = textEditDeviceName.Text;
                _device.DeviceModel = textEditDeviceModel.Text;
                _device.SerialNumber = textEditSerialNumber.Text;
                _device.IPAddress = textEditIPAddress.Text;
                _device.Port = (int)spinEditPort.Value;
                _device.CommunicationKey = textEditCommunicationKey.Text;
                _device.Location = textEditLocation.Text;
                _device.Description = memoEditDescription.Text;
                _device.IsActive = checkEditIsActive.Checked;
                
                bool result;
                
                if (_isNewDevice)
                {
                    // إضافة جهاز جديد
                    result = _deviceManager.AddDevice(_device);
                    
                    if (result)
                    {
                        XtraMessageBox.Show("تم إضافة الجهاز بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("فشل في إضافة الجهاز", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // تعديل جهاز موجود
                    result = _deviceManager.UpdateDevice(_device);
                    
                    if (result)
                    {
                        XtraMessageBox.Show("تم تحديث بيانات الجهاز بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _dataChanged = false;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("فشل في تحديث بيانات الجهاز", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في حفظ بيانات الجهاز");
                XtraMessageBox.Show("حدث خطأ أثناء حفظ بيانات الجهاز", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// التحقق من صحة البيانات
        /// </summary>
        private bool ValidateData()
        {
            // التحقق من اسم الجهاز
            if (string.IsNullOrWhiteSpace(textEditDeviceName.Text))
            {
                XtraMessageBox.Show("الرجاء إدخال اسم الجهاز", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textEditDeviceName.Focus();
                return false;
            }
            
            // التحقق من عنوان IP
            if (string.IsNullOrWhiteSpace(textEditIPAddress.Text))
            {
                XtraMessageBox.Show("الرجاء إدخال عنوان IP للجهاز", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textEditIPAddress.Focus();
                return false;
            }
            
            // التحقق من صحة عنوان IP
            System.Net.IPAddress ipAddress;
            if (!System.Net.IPAddress.TryParse(textEditIPAddress.Text, out ipAddress))
            {
                XtraMessageBox.Show("عنوان IP غير صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textEditIPAddress.Focus();
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// حدث تغيير البيانات في أي حقل
        /// </summary>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            _dataChanged = true;
        }
        
        /// <summary>
        /// حدث تغيير حالة التفعيل
        /// </summary>
        private void checkEditIsActive_CheckedChanged(object sender, EventArgs e)
        {
            _dataChanged = true;
        }
        
        /// <summary>
        /// حدث قبل إغلاق النموذج
        /// </summary>
        private void BiometricDeviceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_dataChanged && this.DialogResult != DialogResult.OK)
            {
                var result = XtraMessageBox.Show("هل تريد إلغاء التغييرات؟", "تنبيه", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}