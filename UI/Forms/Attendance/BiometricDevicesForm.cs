using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;
using HR.Core.ZKTeco;
using HR.Models;

namespace HR.UI.Forms.Attendance
{
    /// <summary>
    /// نموذج إدارة أجهزة البصمة
    /// </summary>
    public partial class BiometricDevicesForm : XtraForm
    {
        private readonly SessionManager _sessionManager;
        private readonly ZKTecoDeviceManager _deviceManager;

        /// <summary>
        /// إنشاء نموذج جديد
        /// </summary>
        public BiometricDevicesForm()
        {
            InitializeComponent();
            _sessionManager = SessionManager.Instance;
            _deviceManager = new ZKTecoDeviceManager();
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void BiometricDevicesForm_Load(object sender, EventArgs e)
        {
            // التحقق من صلاحيات المستخدم
            CheckUserPermissions();

            // تحميل بيانات الأجهزة
            LoadDevices();
        }

        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckUserPermissions()
        {
            bool canAdd = _sessionManager.HasPermission("الحضور", "add");
            bool canEdit = _sessionManager.HasPermission("الحضور", "edit");
            bool canDelete = _sessionManager.HasPermission("الحضور", "delete");

            btnAdd.Enabled = canAdd;
            btnEdit.Enabled = canEdit;
            btnDelete.Enabled = canDelete;
            btnTest.Enabled = canEdit || canAdd;
            btnSync.Enabled = canEdit;
        }

        /// <summary>
        /// تحميل بيانات أجهزة البصمة
        /// </summary>
        private void LoadDevices()
        {
            try
            {
                var devices = _deviceManager.GetAllDevices();
                gridDevices.DataSource = devices;
                gridViewDevices.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات أجهزة البصمة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// إضافة جهاز بصمة جديد
        /// </summary>
        private void AddDevice()
        {
            try
            {
                using (var deviceForm = new BiometricDeviceForm())
                {
                    if (deviceForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadDevices();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إضافة جهاز بصمة جديد: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تعديل جهاز بصمة موجود
        /// </summary>
        private void EditDevice()
        {
            try
            {
                if (gridViewDevices.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("الرجاء اختيار جهاز للتعديل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var device = gridViewDevices.GetFocusedRow() as BiometricDevice;
                if (device == null)
                {
                    return;
                }

                using (var deviceForm = new BiometricDeviceForm(device.ID))
                {
                    if (deviceForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadDevices();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تعديل جهاز البصمة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حذف جهاز بصمة
        /// </summary>
        private void DeleteDevice()
        {
            try
            {
                if (gridViewDevices.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("الرجاء اختيار جهاز للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var device = gridViewDevices.GetFocusedRow() as BiometricDevice;
                if (device == null)
                {
                    return;
                }

                if (XtraMessageBox.Show($"هل أنت متأكد من حذف الجهاز '{device.DeviceName}'؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _deviceManager.DeleteDevice(device.ID);
                    XtraMessageBox.Show("تم حذف الجهاز بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDevices();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حذف جهاز البصمة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// اختبار الاتصال بجهاز البصمة
        /// </summary>
        private void TestConnection()
        {
            try
            {
                if (gridViewDevices.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("الرجاء اختيار جهاز لاختبار الاتصال", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var device = gridViewDevices.GetFocusedRow() as BiometricDevice;
                if (device == null)
                {
                    return;
                }

                // عرض مؤشر التقدم
                using (var progressForm = new WaitForm1("جاري اختبار الاتصال..."))
                {
                    progressForm.Show();
                    progressForm.SetDescription("جاري محاولة الاتصال بالجهاز. يرجى الانتظار...");

                    bool result = _deviceManager.TestConnection(device.IPAddress, device.Port);

                    progressForm.Close();

                    if (result)
                    {
                        XtraMessageBox.Show($"تم الاتصال بالجهاز '{device.DeviceName}' بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show($"فشل الاتصال بالجهاز '{device.DeviceName}'. يرجى التحقق من عنوان IP والمنفذ والتأكد من أن الجهاز متصل بالشبكة.", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء اختبار الاتصال: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// مزامنة بيانات جهاز البصمة
        /// </summary>
        private void SyncDevice()
        {
            try
            {
                if (gridViewDevices.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("الرجاء اختيار جهاز للمزامنة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var device = gridViewDevices.GetFocusedRow() as BiometricDevice;
                if (device == null)
                {
                    return;
                }

                // التأكيد قبل المزامنة
                if (XtraMessageBox.Show($"هل تريد مزامنة بيانات الجهاز '{device.DeviceName}'؟\r\n\r\nستتم مزامنة جميع سجلات الحضور من الجهاز إلى النظام.", "تأكيد المزامنة", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // عرض مؤشر التقدم
                    using (var progressForm = new WaitForm1("جاري مزامنة البيانات..."))
                    {
                        progressForm.Show();
                        progressForm.SetDescription("جاري مزامنة البيانات من الجهاز. قد تستغرق هذه العملية بعض الوقت...");

                        try
                        {
                            // تنفيذ المزامنة
                            string result = _deviceManager.SynchronizeDevice(device.ID);

                            progressForm.Close();

                            // عرض نتيجة المزامنة
                            XtraMessageBox.Show(result, "نتيجة المزامنة", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // إعادة تحميل البيانات
                            LoadDevices();
                        }
                        catch (Exception ex)
                        {
                            progressForm.Close();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء مزامنة البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// معالجة سجلات الحضور غير المعالجة
        /// </summary>
        private void ProcessAttendanceRecords()
        {
            try
            {
                // التأكيد قبل المعالجة
                if (XtraMessageBox.Show("هل تريد معالجة سجلات الحضور غير المعالجة؟\r\n\r\nسيتم معالجة جميع سجلات البصمة الخام وتحويلها إلى سجلات حضور.", "تأكيد المعالجة", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // عرض مؤشر التقدم
                    using (var progressForm = new WaitForm1("جاري معالجة السجلات..."))
                    {
                        progressForm.Show();
                        progressForm.SetDescription("جاري معالجة سجلات الحضور. قد تستغرق هذه العملية بعض الوقت...");

                        try
                        {
                            // تنفيذ المعالجة
                            int processedCount = _deviceManager.ProcessAttendanceRecords();

                            progressForm.Close();

                            // عرض نتيجة المعالجة
                            XtraMessageBox.Show($"تمت معالجة {processedCount} سجل بنجاح", "نتيجة المعالجة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            progressForm.Close();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء معالجة سجلات الحضور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// عرض سجلات حضور موظف معين
        /// </summary>
        private void ViewEmployeeAttendance()
        {
            try
            {
                using (var employeeSelector = new EmployeeSelectorForm())
                {
                    if (employeeSelector.ShowDialog() == DialogResult.OK && employeeSelector.SelectedEmployeeId > 0)
                    {
                        int employeeId = employeeSelector.SelectedEmployeeId;
                        using (var attendanceForm = new EmployeeAttendanceForm(employeeId))
                        {
                            attendanceForm.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء عرض سجلات الحضور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر إضافة جهاز
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddDevice();
        }

        /// <summary>
        /// حدث النقر على زر تعديل جهاز
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditDevice();
        }

        /// <summary>
        /// حدث النقر على زر حذف جهاز
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteDevice();
        }

        /// <summary>
        /// حدث النقر على زر اختبار الاتصال
        /// </summary>
        private void btnTest_Click(object sender, EventArgs e)
        {
            TestConnection();
        }

        /// <summary>
        /// حدث النقر على زر المزامنة
        /// </summary>
        private void btnSync_Click(object sender, EventArgs e)
        {
            SyncDevice();
        }

        /// <summary>
        /// حدث النقر على زر معالجة السجلات
        /// </summary>
        private void btnProcessRecords_Click(object sender, EventArgs e)
        {
            ProcessAttendanceRecords();
        }

        /// <summary>
        /// حدث النقر على زر عرض سجلات الحضور
        /// </summary>
        private void btnViewAttendance_Click(object sender, EventArgs e)
        {
            ViewEmployeeAttendance();
        }

        /// <summary>
        /// حدث النقر على زر التحديث
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDevices();
        }

        /// <summary>
        /// حدث النقر المزدوج على صف في جدول الأجهزة
        /// </summary>
        private void gridViewDevices_DoubleClick(object sender, EventArgs e)
        {
            if (btnEdit.Enabled)
            {
                EditDevice();
            }
        }
    }
}