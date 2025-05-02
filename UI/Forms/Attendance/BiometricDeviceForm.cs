using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.Core.ZKTeco;
using HR.Models;

namespace HR.UI.Forms.Attendance
{
    /// <summary>
    /// نموذج إضافة/تعديل بيانات جهاز البصمة
    /// </summary>
    public partial class BiometricDeviceForm : XtraForm
    {
        private readonly ZKTecoDeviceManager _deviceManager;
        private readonly SessionManager _sessionManager;
        private readonly int _deviceId;
        private BiometricDevice _device;
        private bool _isEditMode;

        /// <summary>
        /// إنشاء نموذج جديد لإضافة جهاز
        /// </summary>
        public BiometricDeviceForm()
        {
            InitializeComponent();
            _deviceManager = new ZKTecoDeviceManager();
            _sessionManager = SessionManager.Instance;
            _deviceId = 0;
            _isEditMode = false;
        }

        /// <summary>
        /// إنشاء نموذج جديد لتعديل جهاز موجود
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        public BiometricDeviceForm(int deviceId)
        {
            InitializeComponent();
            _deviceManager = new ZKTecoDeviceManager();
            _sessionManager = SessionManager.Instance;
            _deviceId = deviceId;
            _isEditMode = true;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void BiometricDeviceForm_Load(object sender, EventArgs e)
        {
            try
            {
                // ضبط عنوان النموذج
                this.Text = _isEditMode ? "تعديل بيانات جهاز البصمة" : "إضافة جهاز بصمة جديد";

                // تعيين القيم الافتراضية
                if (!_isEditMode)
                {
                    checkIsActive.Checked = true;
                    spinPort.Value = 4370; // المنفذ الافتراضي لأجهزة ZK
                }
                else
                {
                    // تحميل بيانات الجهاز
                    LoadDeviceData();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات الجهاز: " + ex.Message,
                    "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل بيانات الجهاز في وضع التعديل
        /// </summary>
        private void LoadDeviceData()
        {
            _device = _deviceManager.GetDeviceById(_deviceId);
            if (_device == null)
            {
                XtraMessageBox.Show("لم يتم العثور على بيانات الجهاز المطلوب!",
                    "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            // ملء البيانات في النموذج
            txtDeviceName.Text = _device.DeviceName;
            txtModel.Text = _device.DeviceModel;
            txtSerial.Text = _device.SerialNumber;
            txtIPAddress.Text = _device.IPAddress;
            spinPort.Value = _device.Port;
            txtCommunicationKey.Text = _device.CommunicationKey;
            txtLocation.Text = _device.Location;
            memoDescription.Text = _device.Description;
            checkIsActive.Checked = _device.IsActive;
        }

        /// <summary>
        /// حفظ بيانات الجهاز
        /// </summary>
        private void SaveDevice()
        {
            try
            {
                // التحقق من صحة البيانات
                if (string.IsNullOrWhiteSpace(txtDeviceName.Text))
                {
                    XtraMessageBox.Show("الرجاء إدخال اسم الجهاز", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDeviceName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtIPAddress.Text))
                {
                    XtraMessageBox.Show("الرجاء إدخال عنوان IP للجهاز", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIPAddress.Focus();
                    return;
                }

                System.Net.IPAddress ipAddress;
                if (!System.Net.IPAddress.TryParse(txtIPAddress.Text, out ipAddress))
                {
                    XtraMessageBox.Show("عنوان IP غير صالح. الرجاء إدخال عنوان IP صحيح.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIPAddress.Focus();
                    return;
                }

                // إنشاء أو تحديث كائن الجهاز
                BiometricDevice device = _isEditMode ? _device : new BiometricDevice();
                device.DeviceName = txtDeviceName.Text.Trim();
                device.DeviceModel = string.IsNullOrWhiteSpace(txtModel.Text) ? null : txtModel.Text.Trim();
                device.SerialNumber = string.IsNullOrWhiteSpace(txtSerial.Text) ? null : txtSerial.Text.Trim();
                device.IPAddress = txtIPAddress.Text.Trim();
                device.Port = Convert.ToInt32(spinPort.Value);
                device.CommunicationKey = string.IsNullOrWhiteSpace(txtCommunicationKey.Text) ? null : txtCommunicationKey.Text.Trim();
                device.Location = string.IsNullOrWhiteSpace(txtLocation.Text) ? null : txtLocation.Text.Trim();
                device.Description = string.IsNullOrWhiteSpace(memoDescription.Text) ? null : memoDescription.Text.Trim();
                device.IsActive = checkIsActive.Checked;

                if (_isEditMode)
                {
                    // تحديث الجهاز
                    _deviceManager.UpdateDevice(device);
                    XtraMessageBox.Show("تم تحديث بيانات الجهاز بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // إضافة جهاز جديد
                    _deviceManager.AddDevice(device);
                    XtraMessageBox.Show("تمت إضافة الجهاز بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حفظ بيانات الجهاز: " + ex.Message,
                    "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// اختبار الاتصال بالجهاز
        /// </summary>
        private void TestConnection()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtIPAddress.Text))
                {
                    XtraMessageBox.Show("الرجاء إدخال عنوان IP للجهاز", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIPAddress.Focus();
                    return;
                }

                System.Net.IPAddress ipAddress;
                if (!System.Net.IPAddress.TryParse(txtIPAddress.Text, out ipAddress))
                {
                    XtraMessageBox.Show("عنوان IP غير صالح. الرجاء إدخال عنوان IP صحيح.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIPAddress.Focus();
                    return;
                }

                // عرض مؤشر التقدم
                using (var waitForm = new WaitForm1("جاري اختبار الاتصال..."))
                {
                    waitForm.Show(this);
                    waitForm.SetDescription("جاري محاولة الاتصال بالجهاز. يرجى الانتظار...");

                    bool result = _deviceManager.TestConnection(txtIPAddress.Text, Convert.ToInt32(spinPort.Value));

                    waitForm.Close();

                    if (result)
                    {
                        XtraMessageBox.Show("تم الاتصال بالجهاز بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("فشل الاتصال بالجهاز. يرجى التحقق من عنوان IP والمنفذ والتأكد من أن الجهاز متصل بالشبكة.", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء اختبار الاتصال: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر حفظ
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDevice();
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
        /// حدث النقر على زر اختبار الاتصال
        /// </summary>
        private void btnTest_Click(object sender, EventArgs e)
        {
            TestConnection();
        }
    }
}