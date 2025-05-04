using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using HR.Core;
using HR.Core.ZKTeco;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Attendance
{
    /// <summary>
    /// نموذج عمليات أجهزة البصمة ZKTeco
    /// </summary>
    public partial class ZKTecoOperationsForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly ZKTecoDeviceManager _deviceManager;
        private readonly EmployeeRepository _employeeRepository;
        private readonly BiometricDevice _device;
        private BackgroundWorker _worker;
        private bool _isBusy = false;
        
        /// <summary>
        /// خاصية لإظهار النموذج من أجل استيراد السجلات
        /// </summary>
        public bool ShowForImport { get; set; }
        
        /// <summary>
        /// خاصية لإظهار النموذج من أجل مزامنة المستخدمين
        /// </summary>
        public bool ShowForSyncUsers { get; set; }
        
        /// <summary>
        /// خاصية لإظهار النموذج من أجل تسجيل البصمة
        /// </summary>
        public bool ShowForEnrollFingerprint { get; set; }

        /// <summary>
        /// منشئ النموذج
        /// </summary>
        /// <param name="device">جهاز البصمة</param>
        public ZKTecoOperationsForm(BiometricDevice device)
        {
            InitializeComponent();
            
            _deviceManager = new ZKTecoDeviceManager();
            _employeeRepository = new EmployeeRepository();
            _device = device;
            
            // تهيئة العامل الخلفي
            InitializeBackgroundWorker();
            
            // تهيئة أحداث النموذج
            this.Load += ZKTecoOperationsForm_Load;
            this.FormClosing += ZKTecoOperationsForm_FormClosing;
            
            // تهيئة أحداث الأزرار
            simpleButtonConnect.Click += simpleButtonConnect_Click;
            simpleButtonDisconnect.Click += simpleButtonDisconnect_Click;
            simpleButtonImport.Click += simpleButtonImport_Click;
            simpleButtonSyncUsers.Click += simpleButtonSyncUsers_Click;
            simpleButtonEnroll.Click += simpleButtonEnroll_Click;
            simpleButtonSyncTime.Click += simpleButtonSyncTime_Click;
        }

        /// <summary>
        /// تهيئة العامل الخلفي
        /// </summary>
        private void InitializeBackgroundWorker()
        {
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += Worker_DoWork;
            _worker.ProgressChanged += Worker_ProgressChanged;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void ZKTecoOperationsForm_Load(object sender, EventArgs e)
        {
            try
            {
                // عرض بيانات الجهاز
                labelControlDeviceName.Text = _device.DeviceName;
                labelControlIPAddress.Text = _device.IPAddress;
                
                // تحميل بيانات الموظفين
                LoadEmployees();
                
                // التحقق من الصلاحيات
                CheckPermissions();
                
                // التحقق من حالة الاتصال
                RefreshConnectionStatus();
                
                // التحقق من الخصائص المحددة للعمليات
                if (ShowForImport)
                {
                    // إذا كان الجهاز متصل، بدء عملية استيراد السجلات مباشرة
                    if (_deviceManager.IsDeviceConnected(_device.ID))
                    {
                        simpleButtonImport_Click(null, null);
                    }
                    else
                    {
                        // عرض رسالة للمستخدم بضرورة الاتصال أولاً
                        XtraMessageBox.Show("يجب الاتصال بالجهاز أولاً قبل استيراد السجلات.", "تنبيه", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // التركيز على زر الاتصال
                        simpleButtonConnect.Focus();
                    }
                }
                else if (ShowForSyncUsers)
                {
                    // إذا كان الجهاز متصل، بدء عملية مزامنة المستخدمين مباشرة
                    if (_deviceManager.IsDeviceConnected(_device.ID))
                    {
                        simpleButtonSyncUsers_Click(null, null);
                    }
                    else
                    {
                        // عرض رسالة للمستخدم بضرورة الاتصال أولاً
                        XtraMessageBox.Show("يجب الاتصال بالجهاز أولاً قبل مزامنة المستخدمين.", "تنبيه", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // التركيز على زر الاتصال
                        simpleButtonConnect.Focus();
                    }
                }
                else if (ShowForEnrollFingerprint)
                {
                    // إذا كان الجهاز متصل، تجهيز واجهة تسجيل البصمة
                    if (_deviceManager.IsDeviceConnected(_device.ID))
                    {
                        // التركيز على اختيار الموظف
                        gridControlEmployees.Focus();
                        
                        // عرض رسالة إرشادية
                        XtraMessageBox.Show("يرجى اختيار الموظف المراد تسجيل بصمته ثم الضغط على زر تسجيل البصمة.", "تسجيل البصمة", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // عرض رسالة للمستخدم بضرورة الاتصال أولاً
                        XtraMessageBox.Show("يجب الاتصال بالجهاز أولاً قبل تسجيل البصمة.", "تنبيه", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // التركيز على زر الاتصال
                        simpleButtonConnect.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل النموذج: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }

        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckPermissions()
        {
            simpleButtonConnect.Enabled = SessionManager.HasPermission("Attendance.ConnectDevice");
            simpleButtonImport.Enabled = SessionManager.HasPermission("Attendance.ImportLogs");
            simpleButtonSyncUsers.Enabled = SessionManager.HasPermission("Attendance.SyncUsers");
            simpleButtonEnroll.Enabled = SessionManager.HasPermission("Attendance.EnrollFingerprint");
            simpleButtonSyncTime.Enabled = SessionManager.HasPermission("Attendance.SyncTime");
        }

        /// <summary>
        /// تحميل بيانات الموظفين
        /// </summary>
        private void LoadEmployees()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // الحصول على الموظفين النشطين
                List<Employee> employees = _employeeRepository.GetActiveEmployees();
                
                // عرض البيانات
                employeeBindingSource.DataSource = employees;
                
                // ضبط إعدادات العرض
                gridViewEmployees.OptionsBehavior.Editable = false;
                gridViewEmployees.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridViewEmployees.OptionsSelection.MultiSelect = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل بيانات الموظفين: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// الحصول على الموظف المحدد
        /// </summary>
        /// <returns>الموظف المحدد</returns>
        private Employee GetSelectedEmployee()
        {
            if (gridViewEmployees.GetSelectedRows().Length > 0)
            {
                int rowHandle = gridViewEmployees.GetSelectedRows()[0];
                if (rowHandle >= 0)
                {
                    return gridViewEmployees.GetRow(rowHandle) as Employee;
                }
            }
            
            return null;
        }

        /// <summary>
        /// حدث النقر على زر الاتصال
        /// </summary>
        private void simpleButtonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isBusy)
                {
                    XtraMessageBox.Show("يوجد عملية قيد التنفيذ حالياً. يرجى الانتظار.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // بدء عملية الاتصال
                _isBusy = true;
                UpdateUIState(false);
                
                // عرض رسالة
                labelControlStatus.Text = "جاري الاتصال بالجهاز...";
                progressBarControl.Visible = true;
                progressBarControl.Properties.Maximum = 100;
                progressBarControl.Position = 50;
                
                // بدء العملية في خلفية
                _worker.RunWorkerAsync(new WorkerArgs { Operation = DeviceOperation.Connect });
            }
            catch (Exception ex)
            {
                _isBusy = false;
                UpdateUIState(true);
                progressBarControl.Visible = false;
                
                XtraMessageBox.Show($"حدث خطأ أثناء الاتصال بالجهاز: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }

        /// <summary>
        /// حدث النقر على زر قطع الاتصال
        /// </summary>
        private void simpleButtonDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isBusy)
                {
                    XtraMessageBox.Show("يوجد عملية قيد التنفيذ حالياً. يرجى الانتظار.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // بدء عملية قطع الاتصال
                _isBusy = true;
                UpdateUIState(false);
                
                // عرض رسالة
                labelControlStatus.Text = "جاري قطع الاتصال بالجهاز...";
                progressBarControl.Visible = true;
                progressBarControl.Properties.Maximum = 100;
                progressBarControl.Position = 50;
                
                // بدء العملية في خلفية
                _worker.RunWorkerAsync(new WorkerArgs { Operation = DeviceOperation.Disconnect });
            }
            catch (Exception ex)
            {
                _isBusy = false;
                UpdateUIState(true);
                progressBarControl.Visible = false;
                
                XtraMessageBox.Show($"حدث خطأ أثناء قطع الاتصال بالجهاز: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }

        /// <summary>
        /// حدث النقر على زر استيراد السجلات
        /// </summary>
        private void simpleButtonImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isBusy)
                {
                    XtraMessageBox.Show("يوجد عملية قيد التنفيذ حالياً. يرجى الانتظار.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // التأكد من الاتصال بالجهاز قبل استيراد السجلات
                if (!_deviceManager.IsDeviceConnected(_device.ID))
                {
                    XtraMessageBox.Show("يجب الاتصال بالجهاز أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // بدء عملية استيراد السجلات
                _isBusy = true;
                UpdateUIState(false);
                
                // عرض رسالة
                labelControlStatus.Text = "جاري استيراد سجلات الحضور...";
                progressBarControl.Visible = true;
                progressBarControl.Properties.Maximum = 100;
                progressBarControl.Position = 10;
                
                // بدء العملية في خلفية
                _worker.RunWorkerAsync(new WorkerArgs { Operation = DeviceOperation.ImportLogs });
            }
            catch (Exception ex)
            {
                _isBusy = false;
                UpdateUIState(true);
                progressBarControl.Visible = false;
                
                XtraMessageBox.Show($"حدث خطأ أثناء استيراد السجلات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }

        /// <summary>
        /// حدث النقر على زر مزامنة المستخدمين
        /// </summary>
        private void simpleButtonSyncUsers_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isBusy)
                {
                    XtraMessageBox.Show("يوجد عملية قيد التنفيذ حالياً. يرجى الانتظار.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // التأكد من الاتصال بالجهاز قبل مزامنة المستخدمين
                if (!_deviceManager.IsDeviceConnected(_device.ID))
                {
                    if (XtraMessageBox.Show("الجهاز غير متصل حالياً. هل ترغب في الاتصال به أولاً؟", "تنبيه", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        simpleButtonConnect_Click(sender, e);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                
                // تأكيد المزامنة
                if (XtraMessageBox.Show("سيتم مزامنة بيانات الموظفين مع جهاز البصمة. هل تريد المتابعة؟", "تأكيد", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                
                // بدء عملية مزامنة المستخدمين
                _isBusy = true;
                UpdateUIState(false);
                
                // عرض رسالة
                labelControlStatus.Text = "جاري مزامنة بيانات المستخدمين...";
                progressBarControl.Visible = true;
                progressBarControl.Properties.Maximum = 100;
                progressBarControl.Position = 10;
                
                // بدء العملية في خلفية
                _worker.RunWorkerAsync(new WorkerArgs { Operation = DeviceOperation.SyncUsers });
            }
            catch (Exception ex)
            {
                _isBusy = false;
                UpdateUIState(true);
                progressBarControl.Visible = false;
                
                XtraMessageBox.Show($"حدث خطأ أثناء مزامنة المستخدمين: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }

        /// <summary>
        /// حدث النقر على زر تسجيل بصمة
        /// </summary>
        private void simpleButtonEnroll_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isBusy)
                {
                    XtraMessageBox.Show("يوجد عملية قيد التنفيذ حالياً. يرجى الانتظار.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // التأكد من الاتصال بالجهاز قبل تسجيل البصمة
                if (!_deviceManager.IsDeviceConnected(_device.ID))
                {
                    if (XtraMessageBox.Show("الجهاز غير متصل حالياً. هل ترغب في الاتصال به أولاً؟", "تنبيه", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        simpleButtonConnect_Click(sender, e);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                
                // الحصول على الموظف المحدد
                Employee employee = GetSelectedEmployee();
                if (employee == null)
                {
                    XtraMessageBox.Show("يرجى تحديد موظف أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // تأكيد تسجيل البصمة
                if (XtraMessageBox.Show($"سيتم تسجيل بصمة الموظف {employee.FullName}. يجب أن يكون الموظف متواجداً للمتابعة. هل تريد المتابعة؟", 
                    "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                
                // بدء عملية تسجيل البصمة
                _isBusy = true;
                UpdateUIState(false);
                
                // عرض رسالة
                labelControlStatus.Text = $"جاري تسجيل بصمة الموظف {employee.FullName}...";
                progressBarControl.Visible = true;
                progressBarControl.Properties.Maximum = 100;
                progressBarControl.Position = 10;
                
                // بدء العملية في خلفية
                _worker.RunWorkerAsync(new WorkerArgs { 
                    Operation = DeviceOperation.EnrollFingerprint,
                    EmployeeID = employee.ID
                });
            }
            catch (Exception ex)
            {
                _isBusy = false;
                UpdateUIState(true);
                progressBarControl.Visible = false;
                
                XtraMessageBox.Show($"حدث خطأ أثناء تسجيل البصمة: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }

        /// <summary>
        /// حدث النقر على زر مزامنة الوقت
        /// </summary>
        private void simpleButtonSyncTime_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isBusy)
                {
                    XtraMessageBox.Show("يوجد عملية قيد التنفيذ حالياً. يرجى الانتظار.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // التأكد من الاتصال بالجهاز قبل مزامنة الوقت
                if (!_deviceManager.IsDeviceConnected(_device.ID))
                {
                    if (XtraMessageBox.Show("الجهاز غير متصل حالياً. هل ترغب في الاتصال به أولاً؟", "تنبيه", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        simpleButtonConnect_Click(sender, e);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                
                // تأكيد مزامنة الوقت
                if (XtraMessageBox.Show($"سيتم مزامنة وقت جهاز البصمة مع وقت النظام الحالي ({DateTime.Now:yyyy-MM-dd HH:mm:ss}). هل تريد المتابعة؟", 
                    "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                
                // بدء عملية مزامنة الوقت
                _isBusy = true;
                UpdateUIState(false);
                
                // عرض رسالة
                labelControlStatus.Text = "جاري مزامنة وقت الجهاز...";
                progressBarControl.Visible = true;
                progressBarControl.Properties.Maximum = 100;
                progressBarControl.Position = 30;
                
                // بدء العملية في خلفية
                _worker.RunWorkerAsync(new WorkerArgs { Operation = DeviceOperation.SyncTime });
            }
            catch (Exception ex)
            {
                _isBusy = false;
                UpdateUIState(true);
                progressBarControl.Visible = false;
                
                XtraMessageBox.Show($"حدث خطأ أثناء مزامنة الوقت: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }

        /// <summary>
        /// حدث معالجة العمل في الخلفية
        /// </summary>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WorkerArgs args = e.Argument as WorkerArgs;
                if (args == null)
                {
                    return;
                }
                
                // تنفيذ العملية المطلوبة
                switch (args.Operation)
                {
                    case DeviceOperation.Connect:
                        _worker.ReportProgress(10, "جاري الاتصال بالجهاز...");
                        Thread.Sleep(500);
                        
                        bool connected = _deviceManager.ConnectDevice(_device.ID);
                        
                        _worker.ReportProgress(100, connected ? 
                            "تم الاتصال بالجهاز بنجاح." : 
                            "فشل الاتصال بالجهاز.");
                        
                        e.Result = new WorkerResult { 
                            Success = connected,
                            Message = connected ? 
                                "تم الاتصال بالجهاز بنجاح." : 
                                "فشل الاتصال بالجهاز. يرجى التأكد من إعدادات الاتصال وأن الجهاز قيد التشغيل."
                        };
                        break;
                        
                    case DeviceOperation.Disconnect:
                        _worker.ReportProgress(10, "جاري قطع الاتصال بالجهاز...");
                        Thread.Sleep(500);
                        
                        bool disconnected = _deviceManager.DisconnectDevice(_device.ID);
                        
                        _worker.ReportProgress(100, disconnected ? 
                            "تم قطع الاتصال بالجهاز بنجاح." : 
                            "فشل قطع الاتصال بالجهاز.");
                        
                        e.Result = new WorkerResult { 
                            Success = disconnected,
                            Message = disconnected ? 
                                "تم قطع الاتصال بالجهاز بنجاح." : 
                                "فشل قطع الاتصال بالجهاز."
                        };
                        break;
                        
                    case DeviceOperation.ImportLogs:
                        _worker.ReportProgress(10, "جاري التحضير لاستيراد السجلات...");
                        Thread.Sleep(500);
                        
                        _worker.ReportProgress(30, "جاري استيراد سجلات الحضور من الجهاز...");
                        int importedCount = _deviceManager.ImportAttendanceRecords(_device.ID);
                        
                        _worker.ReportProgress(100, $"تم استيراد {importedCount} سجل حضور بنجاح.");
                        
                        e.Result = new WorkerResult { 
                            Success = true,
                            Message = $"تم استيراد {importedCount} سجل حضور بنجاح.",
                            Data = importedCount
                        };
                        break;
                        
                    case DeviceOperation.SyncUsers:
                        _worker.ReportProgress(10, "جاري التحضير لمزامنة المستخدمين...");
                        Thread.Sleep(500);
                        
                        _worker.ReportProgress(30, "جاري مزامنة بيانات المستخدمين مع الجهاز...");
                        bool syncSuccess = _deviceManager.SynchronizeUsers(_device.ID);
                        
                        _worker.ReportProgress(100, syncSuccess ? 
                            "تمت مزامنة بيانات المستخدمين بنجاح." : 
                            "فشلت مزامنة بيانات المستخدمين.");
                        
                        e.Result = new WorkerResult { 
                            Success = syncSuccess,
                            Message = syncSuccess ? 
                                "تمت مزامنة بيانات المستخدمين بنجاح." : 
                                "فشلت مزامنة بيانات المستخدمين. يرجى التأكد من الاتصال بالجهاز."
                        };
                        break;
                        
                    case DeviceOperation.EnrollFingerprint:
                        _worker.ReportProgress(10, "جاري التحضير لتسجيل البصمة...");
                        Thread.Sleep(500);
                        
                        Employee employee = _employeeRepository.GetEmployeeByID(args.EmployeeID);
                        if (employee == null)
                        {
                            e.Result = new WorkerResult { 
                                Success = false,
                                Message = "لم يتم العثور على بيانات الموظف."
                            };
                            return;
                        }
                        
                        _worker.ReportProgress(30, $"جاري تسجيل بصمة الموظف {employee.FullName}...");
                        _worker.ReportProgress(40, "يرجى الطلب من الموظف وضع إصبعه على الجهاز...");
                        
                        // تسجيل البصمة
                        bool enrollSuccess = _deviceManager.EnrollFingerprint(_device.ID, employee.ID);
                        
                        _worker.ReportProgress(100, enrollSuccess ? 
                            $"تم تسجيل بصمة الموظف {employee.FullName} بنجاح." : 
                            $"فشل تسجيل بصمة الموظف {employee.FullName}.");
                        
                        e.Result = new WorkerResult { 
                            Success = enrollSuccess,
                            Message = enrollSuccess ? 
                                $"تم تسجيل بصمة الموظف {employee.FullName} بنجاح." : 
                                $"فشل تسجيل بصمة الموظف {employee.FullName}. يرجى المحاولة مرة أخرى."
                        };
                        break;
                        
                    case DeviceOperation.SyncTime:
                        _worker.ReportProgress(10, "جاري التحضير لمزامنة الوقت...");
                        Thread.Sleep(500);
                        
                        _worker.ReportProgress(50, "جاري مزامنة وقت الجهاز...");
                        bool timeSuccess = _deviceManager.SynchronizeTime(_device.ID);
                        
                        _worker.ReportProgress(100, timeSuccess ? 
                            "تمت مزامنة وقت الجهاز بنجاح." : 
                            "فشلت مزامنة وقت الجهاز.");
                        
                        e.Result = new WorkerResult { 
                            Success = timeSuccess,
                            Message = timeSuccess ? 
                                "تمت مزامنة وقت الجهاز بنجاح." : 
                                "فشلت مزامنة وقت الجهاز. يرجى التأكد من الاتصال بالجهاز."
                        };
                        break;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                e.Result = new WorkerResult { 
                    Success = false,
                    Message = $"حدث خطأ أثناء تنفيذ العملية: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// حدث تغيير تقدم العمل
        /// </summary>
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                // تحديث شريط التقدم
                progressBarControl.Position = e.ProgressPercentage;
                
                // تحديث رسالة الحالة
                if (e.UserState != null)
                {
                    labelControlStatus.Text = e.UserState.ToString();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }

        /// <summary>
        /// حدث اكتمال العمل
        /// </summary>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                _isBusy = false;
                UpdateUIState(true);
                progressBarControl.Visible = false;
                
                if (e.Error != null)
                {
                    XtraMessageBox.Show($"حدث خطأ أثناء تنفيذ العملية: {e.Error.Message}", "خطأ", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (e.Cancelled)
                {
                    labelControlStatus.Text = "تم إلغاء العملية.";
                    return;
                }
                
                WorkerResult result = e.Result as WorkerResult;
                if (result != null)
                {
                    // عرض نتيجة العملية
                    labelControlStatus.Text = result.Message;
                    
                    if (result.Success)
                    {
                        // تحديث حالة الأزرار بعد العملية الناجحة
                        UpdateConnectedState(_deviceManager.IsDeviceConnected(_device.ID));
                    }
                    else
                    {
                        XtraMessageBox.Show(result.Message, "تنبيه", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                
                // تحديث حالة الاتصال
                RefreshConnectionStatus();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحديث حالة واجهة المستخدم
        /// </summary>
        /// <param name="enabled">حالة التفعيل</param>
        private void UpdateUIState(bool enabled)
        {
            simpleButtonConnect.Enabled = enabled && SessionManager.HasPermission("Attendance.ConnectDevice");
            simpleButtonDisconnect.Enabled = enabled && _deviceManager.IsDeviceConnected(_device.ID) && 
                SessionManager.HasPermission("Attendance.ConnectDevice");
            simpleButtonImport.Enabled = enabled && _deviceManager.IsDeviceConnected(_device.ID) && 
                SessionManager.HasPermission("Attendance.ImportLogs");
            simpleButtonSyncUsers.Enabled = enabled && 
                SessionManager.HasPermission("Attendance.SyncUsers");
            simpleButtonEnroll.Enabled = enabled && 
                SessionManager.HasPermission("Attendance.EnrollFingerprint");
            simpleButtonSyncTime.Enabled = enabled && 
                SessionManager.HasPermission("Attendance.SyncTime");
            gridControlEmployees.Enabled = enabled;
        }
        
        /// <summary>
        /// تحديث حالة الاتصال
        /// </summary>
        /// <param name="connected">حالة الاتصال</param>
        private void UpdateConnectedState(bool connected)
        {
            simpleButtonConnect.Enabled = !connected && SessionManager.HasPermission("Attendance.ConnectDevice");
            simpleButtonDisconnect.Enabled = connected && SessionManager.HasPermission("Attendance.ConnectDevice");
            simpleButtonImport.Enabled = connected && SessionManager.HasPermission("Attendance.ImportLogs");
            simpleButtonSyncUsers.Enabled = connected && SessionManager.HasPermission("Attendance.SyncUsers");
            simpleButtonEnroll.Enabled = connected && SessionManager.HasPermission("Attendance.EnrollFingerprint");
            simpleButtonSyncTime.Enabled = connected && SessionManager.HasPermission("Attendance.SyncTime");
            
            // تحديث نص الحالة
            if (connected)
            {
                labelControlConnectionStatus.Text = "متصل";
                labelControlConnectionStatus.ForeColor = Color.Green;
            }
            else
            {
                labelControlConnectionStatus.Text = "غير متصل";
                labelControlConnectionStatus.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// تحديث حالة الاتصال
        /// </summary>
        private void RefreshConnectionStatus()
        {
            try
            {
                // التحقق من حالة الاتصال
                bool connected = _deviceManager.IsDeviceConnected(_device.ID);
                
                // تحديث حالة الأزرار
                UpdateConnectedState(connected);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }

        /// <summary>
        /// حدث إغلاق النموذج
        /// </summary>
        private void ZKTecoOperationsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // إلغاء العملية إذا كانت قيد التنفيذ
                if (_isBusy && _worker.IsBusy)
                {
                    if (XtraMessageBox.Show("هناك عملية قيد التنفيذ. هل تريد إلغاؤها وإغلاق النموذج؟", 
                        "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        e.Cancel = true;
                        return;
                    }
                    
                    _worker.CancelAsync();
                }
                
                // قطع الاتصال بالجهاز عند الإغلاق
                if (_deviceManager.IsDeviceConnected(_device.ID))
                {
                    _deviceManager.DisconnectDevice(_device.ID);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
        }
    }

    /// <summary>
    /// أنواع عمليات جهاز البصمة
    /// </summary>
    public enum DeviceOperation
    {
        Connect,
        Disconnect,
        ImportLogs,
        SyncUsers,
        EnrollFingerprint,
        SyncTime
    }

    /// <summary>
    /// معاملات العامل الخلفي
    /// </summary>
    public class WorkerArgs
    {
        public DeviceOperation Operation { get; set; }
        public int EmployeeID { get; set; }
    }

    /// <summary>
    /// نتيجة العامل الخلفي
    /// </summary>
    public class WorkerResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}