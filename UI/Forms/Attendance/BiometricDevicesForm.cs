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
using HR.Core.ZKTeco;
using HR.Models;

namespace HR.UI.Forms.Attendance
{
    public partial class BiometricDevicesForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly ZKTecoDeviceManager _deviceManager;
        private List<BiometricDevice> _devices;
        private BiometricDevice _currentDevice;
        
        public BiometricDevicesForm()
        {
            InitializeComponent();
            _deviceManager = new ZKTecoDeviceManager();
        }
        
        private void BiometricDevicesForm_Load(object sender, EventArgs e)
        {
            try
            {
                // تهيئة عناصر التحكم
                InitializeControls();
                
                // تحميل البيانات
                LoadData();
                
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
        /// تهيئة عناصر التحكم
        /// </summary>
        private void InitializeControls()
        {
            // ضبط إعدادات الجدول
            gridViewDevices.OptionsBehavior.Editable = false;
            gridViewDevices.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewDevices.OptionsView.ShowGroupPanel = false;
            gridViewDevices.OptionsView.ShowIndicator = false;
            
            // ضبط حالة عناصر التحكم
            ClearEditors();
            SetEditorsEnabled(false);
        }
        
        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckPermissions()
        {
            // التحقق من صلاحية إضافة جهاز
            barButtonItemAdd.Enabled = SessionManager.HasPermission("Attendance.AddDevice");
            
            // التحقق من صلاحية تعديل جهاز
            barButtonItemEdit.Enabled = SessionManager.HasPermission("Attendance.EditDevice");
            
            // التحقق من صلاحية حذف جهاز
            barButtonItemDelete.Enabled = SessionManager.HasPermission("Attendance.DeleteDevice");
            
            // التحقق من صلاحية الاتصال بالجهاز
            barButtonItemConnect.Enabled = SessionManager.HasPermission("Attendance.ConnectDevice");
            
            // التحقق من صلاحية استيراد السجلات
            barButtonItemImport.Enabled = SessionManager.HasPermission("Attendance.ImportLogs");
            
            // التحقق من صلاحية مزامنة المستخدمين
            barButtonItemSyncUsers.Enabled = SessionManager.HasPermission("Attendance.SyncUsers");
            
            // التحقق من صلاحية تسجيل البصمة
            barButtonItemEnroll.Enabled = SessionManager.HasPermission("Attendance.EnrollFingerprint");
        }
        
        /// <summary>
        /// تحميل البيانات
        /// </summary>
        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // الحصول على أجهزة البصمة
                _devices = _deviceManager.GetRegisteredDevices();
                
                // عرض البيانات
                biometricDeviceBindingSource.DataSource = _devices;
                gridViewDevices.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل أجهزة البصمة: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// تنظيف عناصر التحرير
        /// </summary>
        private void ClearEditors()
        {
            textEditDeviceName.Text = string.Empty;
            textEditIPAddress.Text = string.Empty;
            spinEditPort.Value = 4370;
            textEditSerialNumber.Text = string.Empty;
            textEditModel.Text = string.Empty;
            checkEditIsActive.Checked = true;
            memoEditNotes.Text = string.Empty;
            
            _currentDevice = null;
        }
        
        /// <summary>
        /// ضبط حالة عناصر التحرير
        /// </summary>
        /// <param name="enabled">حالة التفعيل</param>
        private void SetEditorsEnabled(bool enabled)
        {
            groupControlDetails.Enabled = enabled;
            simpleButtonSave.Enabled = enabled;
            simpleButtonCancel.Enabled = enabled;
        }
        
        /// <summary>
        /// عرض بيانات الجهاز
        /// </summary>
        /// <param name="device">الجهاز</param>
        private void DisplayDeviceData(BiometricDevice device)
        {
            if (device == null)
            {
                ClearEditors();
                return;
            }
            
            _currentDevice = device;
            
            textEditDeviceName.Text = device.DeviceName;
            textEditIPAddress.Text = device.IPAddress;
            spinEditPort.Value = device.Port;
            textEditSerialNumber.Text = device.SerialNumber;
            textEditModel.Text = device.Model;
            checkEditIsActive.Checked = device.IsActive;
            memoEditNotes.Text = device.Notes;
            
            UpdateButtonsState();
        }
        
        /// <summary>
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonsState()
        {
            BiometricDevice selectedDevice = GetSelectedDevice();
            
            if (selectedDevice != null)
            {
                barButtonItemEdit.Enabled = SessionManager.HasPermission("Attendance.EditDevice");
                barButtonItemDelete.Enabled = SessionManager.HasPermission("Attendance.DeleteDevice");
                barButtonItemConnect.Enabled = SessionManager.HasPermission("Attendance.ConnectDevice") &&
                    selectedDevice.IsActive;
                barButtonItemImport.Enabled = SessionManager.HasPermission("Attendance.ImportLogs") &&
                    selectedDevice.IsActive;
                barButtonItemSyncUsers.Enabled = SessionManager.HasPermission("Attendance.SyncUsers") &&
                    selectedDevice.IsActive;
                barButtonItemEnroll.Enabled = SessionManager.HasPermission("Attendance.EnrollFingerprint") &&
                    selectedDevice.IsActive;
            }
            else
            {
                barButtonItemEdit.Enabled = false;
                barButtonItemDelete.Enabled = false;
                barButtonItemConnect.Enabled = false;
                barButtonItemImport.Enabled = false;
                barButtonItemSyncUsers.Enabled = false;
                barButtonItemEnroll.Enabled = false;
            }
        }
        
        /// <summary>
        /// الحصول على الجهاز المحدد
        /// </summary>
        /// <returns>الجهاز المحدد</returns>
        private BiometricDevice GetSelectedDevice()
        {
            if (gridViewDevices.GetSelectedRows().Length > 0)
            {
                int rowHandle = gridViewDevices.GetSelectedRows()[0];
                if (rowHandle >= 0)
                {
                    return gridViewDevices.GetRow(rowHandle) as BiometricDevice;
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// حفظ بيانات الجهاز
        /// </summary>
        /// <returns>نتيجة الحفظ</returns>
        private bool SaveDeviceData()
        {
            try
            {
                // التحقق من إدخال البيانات
                if (!ValidateInput())
                    return false;
                
                // إنشاء أو تحديث جهاز
                bool isNew = _currentDevice == null;
                
                if (isNew)
                {
                    _currentDevice = new BiometricDevice();
                }
                
                // تعبئة بيانات الجهاز
                _currentDevice.DeviceName = textEditDeviceName.Text;
                _currentDevice.IPAddress = textEditIPAddress.Text;
                _currentDevice.Port = Convert.ToInt32(spinEditPort.Value);
                _currentDevice.SerialNumber = textEditSerialNumber.Text;
                _currentDevice.Model = textEditModel.Text;
                _currentDevice.IsActive = checkEditIsActive.Checked;
                _currentDevice.Notes = memoEditNotes.Text;
                
                // حفظ الجهاز
                if (isNew)
                {
                    int id = _deviceManager.AddDevice(_currentDevice);
                    if (id > 0)
                    {
                        _currentDevice.ID = id;
                        _devices.Add(_currentDevice);
                        biometricDeviceBindingSource.ResetBindings(false);
                        
                        XtraMessageBox.Show("تمت إضافة جهاز البصمة بنجاح.", "نجاح", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("فشلت عملية إضافة جهاز البصمة.", "خطأ", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    bool result = _deviceManager.UpdateDevice(_currentDevice);
                    if (result)
                    {
                        gridViewDevices.RefreshData();
                        
                        XtraMessageBox.Show("تم تحديث جهاز البصمة بنجاح.", "نجاح", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        XtraMessageBox.Show("فشلت عملية تحديث جهاز البصمة.", "خطأ", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حفظ جهاز البصمة: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
                return false;
            }
        }
        
        /// <summary>
        /// التحقق من صحة البيانات المدخلة
        /// </summary>
        /// <returns>نتيجة التحقق</returns>
        private bool ValidateInput()
        {
            // التحقق من اسم الجهاز
            if (string.IsNullOrWhiteSpace(textEditDeviceName.Text))
            {
                XtraMessageBox.Show("يرجى إدخال اسم الجهاز.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditDeviceName.Focus();
                return false;
            }
            
            // التحقق من عنوان IP
            if (string.IsNullOrWhiteSpace(textEditIPAddress.Text))
            {
                XtraMessageBox.Show("يرجى إدخال عنوان IP للجهاز.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditIPAddress.Focus();
                return false;
            }
            
            // التحقق من المنفذ
            if (spinEditPort.Value <= 0)
            {
                XtraMessageBox.Show("يرجى إدخال رقم منفذ صحيح للجهاز.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spinEditPort.Focus();
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// حدث النقر على زر التحديث
        /// </summary>
        private void barButtonItemRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحديث البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر الإضافة
        /// </summary>
        private void barButtonItemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // تنظيف عناصر التحرير
                ClearEditors();
                
                // تفعيل عناصر التحرير
                SetEditorsEnabled(true);
                
                // التركيز على اسم الجهاز
                textEditDeviceName.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر التعديل
        /// </summary>
        private void barButtonItemEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على الجهاز المحدد
                BiometricDevice selectedDevice = GetSelectedDevice();
                if (selectedDevice == null)
                {
                    XtraMessageBox.Show("يرجى تحديد جهاز أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // عرض بيانات الجهاز
                DisplayDeviceData(selectedDevice);
                
                // تفعيل عناصر التحرير
                SetEditorsEnabled(true);
                
                // التركيز على اسم الجهاز
                textEditDeviceName.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر الحذف
        /// </summary>
        private void barButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على الجهاز المحدد
                BiometricDevice selectedDevice = GetSelectedDevice();
                if (selectedDevice == null)
                {
                    XtraMessageBox.Show("يرجى تحديد جهاز أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // تأكيد الحذف
                if (XtraMessageBox.Show($"هل أنت متأكد من حذف جهاز البصمة '{selectedDevice.DeviceName}'؟", 
                    "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                
                // حذف الجهاز
                bool result = _deviceManager.DeleteDevice(selectedDevice.ID);
                if (result)
                {
                    _devices.Remove(selectedDevice);
                    biometricDeviceBindingSource.ResetBindings(false);
                    
                    XtraMessageBox.Show("تم حذف جهاز البصمة بنجاح.", "نجاح", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // تنظيف وتعطيل عناصر التحرير
                    ClearEditors();
                    SetEditorsEnabled(false);
                }
                else
                {
                    XtraMessageBox.Show("فشلت عملية حذف جهاز البصمة.", "خطأ", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// حدث النقر على زر الاتصال بالجهاز
        /// </summary>
        private void barButtonItemConnect_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على الجهاز المحدد
                BiometricDevice selectedDevice = GetSelectedDevice();
                if (selectedDevice == null)
                {
                    XtraMessageBox.Show("يرجى تحديد جهاز أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // فتح نموذج عمليات الجهاز
                using (ZKTecoOperationsForm operationsForm = new ZKTecoOperationsForm(selectedDevice))
                {
                    operationsForm.ShowDialog();
                    
                    // تحديث البيانات بعد إغلاق النموذج
                    LoadData();
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
        /// حدث النقر على زر استيراد سجلات الحضور
        /// </summary>
        private void barButtonItemImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على الجهاز المحدد
                BiometricDevice selectedDevice = GetSelectedDevice();
                if (selectedDevice == null)
                {
                    XtraMessageBox.Show("يرجى تحديد جهاز أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                this.Cursor = Cursors.WaitCursor;
                
                // استيراد سجلات الحضور
                XtraMessageBox.Show("جاري استيراد سجلات الحضور. قد تستغرق العملية بعض الوقت...", "استيراد سجلات الحضور", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                int count = _deviceManager.ImportAttendanceRecords(selectedDevice.ID);
                
                if (count > 0)
                {
                    XtraMessageBox.Show($"تم استيراد {count} سجل حضور من جهاز البصمة '{selectedDevice.DeviceName}' بنجاح.", "نجاح", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // السؤال عن معالجة السجلات
                    if (XtraMessageBox.Show("هل ترغب في معالجة سجلات الحضور الآن؟", "معالجة السجلات", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ProcessAttendanceLogs();
                    }
                    
                    LoadData();
                }
                else
                {
                    XtraMessageBox.Show($"لم يتم العثور على سجلات حضور جديدة في جهاز البصمة '{selectedDevice.DeviceName}'.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// حدث النقر على زر مزامنة المستخدمين
        /// </summary>
        private void barButtonItemSyncUsers_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على الجهاز المحدد
                BiometricDevice selectedDevice = GetSelectedDevice();
                if (selectedDevice == null)
                {
                    XtraMessageBox.Show("يرجى تحديد جهاز أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                this.Cursor = Cursors.WaitCursor;
                
                // مزامنة المستخدمين
                XtraMessageBox.Show("جاري مزامنة المستخدمين. قد تستغرق العملية بعض الوقت...", "مزامنة المستخدمين", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                bool result = _deviceManager.SynchronizeUsers(selectedDevice.ID);
                
                if (result)
                {
                    XtraMessageBox.Show($"تمت مزامنة المستخدمين مع جهاز البصمة '{selectedDevice.DeviceName}' بنجاح.", "نجاح", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoadData();
                }
                else
                {
                    XtraMessageBox.Show($"فشلت مزامنة المستخدمين مع جهاز البصمة '{selectedDevice.DeviceName}'.", "خطأ", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// حدث النقر على زر تسجيل البصمة
        /// </summary>
        private void barButtonItemEnroll_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على الجهاز المحدد
                BiometricDevice selectedDevice = GetSelectedDevice();
                if (selectedDevice == null)
                {
                    XtraMessageBox.Show("يرجى تحديد جهاز أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // فتح نموذج تسجيل البصمة
                EnrollFingerprintForm enrollForm = new EnrollFingerprintForm(selectedDevice.ID);
                enrollForm.ShowDialog();
                
                // تحديث البيانات بعد الانتهاء
                LoadData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر حفظ
        /// </summary>
        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // حفظ بيانات الجهاز
                if (SaveDeviceData())
                {
                    // تنظيف وتعطيل عناصر التحرير
                    ClearEditors();
                    SetEditorsEnabled(false);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء الحفظ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر إلغاء
        /// </summary>
        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // تنظيف وتعطيل عناصر التحرير
                ClearEditors();
                SetEditorsEnabled(false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على صف في الجدول
        /// </summary>
        private void gridViewDevices_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                // تحديث حالة الأزرار
                UpdateButtonsState();
                
                // عرض بيانات الجهاز إذا كان النقر مزدوجاً
                if (e.Clicks == 2 && SessionManager.HasPermission("Attendance.EditDevice"))
                {
                    barButtonItemEdit_ItemClick(null, null);
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
        /// معالجة سجلات الحضور
        /// </summary>
        private void ProcessAttendanceLogs()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // معالجة سجلات الحضور
                XtraMessageBox.Show("جاري معالجة سجلات الحضور. قد تستغرق العملية بعض الوقت...", "معالجة سجلات الحضور", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                int count = _deviceManager.ProcessRawAttendanceLogs();
                
                if (count > 0)
                {
                    XtraMessageBox.Show($"تمت معالجة {count} سجل حضور بنجاح.", "نجاح", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("لم يتم العثور على سجلات حضور للمعالجة.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء معالجة سجلات الحضور: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
    
    /// <summary>
    /// نموذج تسجيل البصمة
    /// </summary>
    public class EnrollFingerprintForm : XtraForm
    {
        private readonly ZKTecoDeviceManager _deviceManager;
        private readonly int _deviceID;
        private List<Employee> _employees;
        
        private LabelControl labelControl1;
        private SearchLookUpEdit searchLookUpEditEmployee;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private LabelControl labelControl2;
        private SimpleButton simpleButtonEnroll;
        private SimpleButton simpleButtonClose;
        private ProgressBarControl progressBarControl;
        
        public EnrollFingerprintForm(int deviceID)
        {
            InitializeComponent();
            
            _deviceManager = new ZKTecoDeviceManager();
            _deviceID = deviceID;
            
            LoadEmployees();
        }
        
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.searchLookUpEditEmployee = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonEnroll = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonClose = new DevExpress.XtraEditors.SimpleButton();
            this.progressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(413, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(42, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "الموظف:";
            // 
            // searchLookUpEditEmployee
            // 
            this.searchLookUpEditEmployee.Location = new System.Drawing.Point(12, 12);
            this.searchLookUpEditEmployee.Name = "searchLookUpEditEmployee";
            this.searchLookUpEditEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEditEmployee.Properties.NullText = "";
            this.searchLookUpEditEmployee.Properties.PopupView = this.searchLookUpEdit1View;
            this.searchLookUpEditEmployee.Size = new System.Drawing.Size(395, 20);
            this.searchLookUpEditEmployee.TabIndex = 1;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(432, 39);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "ملاحظة:\r\nسيتم فتح نافذة لتسجيل البصمة. يرجى اتباع التعليمات لتسجيل بصمة الموظف.\r\nقد تستغرق العملية بعض الوقت.";
            // 
            // simpleButtonEnroll
            // 
            this.simpleButtonEnroll.Location = new System.Drawing.Point(12, 115);
            this.simpleButtonEnroll.Name = "simpleButtonEnroll";
            this.simpleButtonEnroll.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonEnroll.TabIndex = 3;
            this.simpleButtonEnroll.Text = "تسجيل البصمة";
            this.simpleButtonEnroll.Click += new System.EventHandler(this.simpleButtonEnroll_Click);
            // 
            // simpleButtonClose
            // 
            this.simpleButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonClose.Location = new System.Drawing.Point(93, 115);
            this.simpleButtonClose.Name = "simpleButtonClose";
            this.simpleButtonClose.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonClose.TabIndex = 4;
            this.simpleButtonClose.Text = "إغلاق";
            // 
            // progressBarControl
            // 
            this.progressBarControl.Location = new System.Drawing.Point(12, 83);
            this.progressBarControl.Name = "progressBarControl";
            this.progressBarControl.Size = new System.Drawing.Size(443, 18);
            this.progressBarControl.TabIndex = 5;
            this.progressBarControl.Visible = false;
            // 
            // EnrollFingerprintForm
            // 
            this.AcceptButton = this.simpleButtonEnroll;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButtonClose;
            this.ClientSize = new System.Drawing.Size(467, 150);
            this.Controls.Add(this.progressBarControl);
            this.Controls.Add(this.simpleButtonClose);
            this.Controls.Add(this.simpleButtonEnroll);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.searchLookUpEditEmployee);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnrollFingerprintForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "تسجيل بصمة موظف";
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        /// <summary>
        /// تحميل قائمة الموظفين
        /// </summary>
        private void LoadEmployees()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // الحصول على قائمة الموظفين النشطين
                EmployeeRepository employeeRepo = new EmployeeRepository();
                _employees = employeeRepo.GetActiveEmployees();
                
                // عرض البيانات
                searchLookUpEditEmployee.Properties.DataSource = _employees;
                searchLookUpEditEmployee.Properties.DisplayMember = "FullName";
                searchLookUpEditEmployee.Properties.ValueMember = "ID";
                
                // ضبط أعمدة العرض
                searchLookUpEdit1View.Columns.Clear();
                searchLookUpEdit1View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    Caption = "رقم الموظف",
                    FieldName = "EmployeeCode",
                    Visible = true,
                    VisibleIndex = 0,
                    Width = 80
                });
                searchLookUpEdit1View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    Caption = "اسم الموظف",
                    FieldName = "FullName",
                    Visible = true,
                    VisibleIndex = 1,
                    Width = 200
                });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل قائمة الموظفين: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// حدث النقر على زر تسجيل البصمة
        /// </summary>
        private void simpleButtonEnroll_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من اختيار موظف
                if (searchLookUpEditEmployee.EditValue == null)
                {
                    XtraMessageBox.Show("يرجى اختيار موظف.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    searchLookUpEditEmployee.Focus();
                    return;
                }
                
                // الحصول على الموظف المحدد
                int employeeID = Convert.ToInt32(searchLookUpEditEmployee.EditValue);
                Employee employee = _employees.FirstOrDefault(e => e.ID == employeeID);
                
                if (employee == null)
                {
                    XtraMessageBox.Show("لم يتم العثور على الموظف المحدد.", "خطأ", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // عرض رسالة تأكيد
                if (XtraMessageBox.Show($"هل أنت متأكد من تسجيل بصمة الموظف '{employee.FullName}'؟", "تأكيد", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                
                // تعطيل عناصر التحكم وعرض شريط التقدم
                searchLookUpEditEmployee.Enabled = false;
                simpleButtonEnroll.Enabled = false;
                progressBarControl.Visible = true;
                progressBarControl.Properties.Minimum = 0;
                progressBarControl.Properties.Maximum = 100;
                progressBarControl.Properties.Step = 10;
                
                // بدء عملية تسجيل البصمة في مهمة منفصلة
                System.Threading.ThreadPool.QueueUserWorkItem(state =>
                {
                    try
                    {
                        // تحديث شريط التقدم
                        UpdateProgress(10);
                        
                        // تسجيل البصمة
                        bool result = _deviceManager.EnrollFingerprint(_deviceID, employeeID);
                        
                        // تحديث شريط التقدم
                        UpdateProgress(100);
                        
                        // عرض نتيجة العملية
                        this.Invoke(new Action(() =>
                        {
                            if (result)
                            {
                                XtraMessageBox.Show($"تم تسجيل بصمة الموظف '{employee.FullName}' بنجاح.", "نجاح", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                // إغلاق النموذج
                                DialogResult = DialogResult.OK;
                                Close();
                            }
                            else
                            {
                                XtraMessageBox.Show($"فشل تسجيل بصمة الموظف '{employee.FullName}'.", "خطأ", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                
                                // إعادة تفعيل عناصر التحكم وإخفاء شريط التقدم
                                searchLookUpEditEmployee.Enabled = true;
                                simpleButtonEnroll.Enabled = true;
                                progressBarControl.Visible = false;
                            }
                        }));
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            XtraMessageBox.Show($"حدث خطأ أثناء تسجيل البصمة: {ex.Message}", "خطأ", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LogManager.LogException(ex);
                            
                            // إعادة تفعيل عناصر التحكم وإخفاء شريط التقدم
                            searchLookUpEditEmployee.Enabled = true;
                            simpleButtonEnroll.Enabled = true;
                            progressBarControl.Visible = false;
                        }));
                    }
                });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
                
                // إعادة تفعيل عناصر التحكم وإخفاء شريط التقدم
                searchLookUpEditEmployee.Enabled = true;
                simpleButtonEnroll.Enabled = true;
                progressBarControl.Visible = false;
            }
        }
        
        /// <summary>
        /// تحديث شريط التقدم
        /// </summary>
        /// <param name="value">القيمة</param>
        private void UpdateProgress(int value)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    progressBarControl.EditValue = value;
                }));
            }
            catch
            {
                // تجاهل أي خطأ هنا
            }
        }
    }
}