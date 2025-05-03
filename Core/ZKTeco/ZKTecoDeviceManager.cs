using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HR.Models;

namespace HR.Core.ZKTeco
{
    /// <summary>
    /// مدير أجهزة البصمة ZKTeco
    /// مسؤول عن إدارة أجهزة البصمة ومزامنة البيانات
    /// </summary>
    public class ZKTecoDeviceManager
    {
        private readonly UnitOfWork _unitOfWork;
        private static ZKTecoDeviceManager _instance;
        private static readonly object _lockObject = new object();
        private bool _isSyncing;
        private CancellationTokenSource _syncCancellationTokenSource;
        
        /// <summary>
        /// الحصول على نسخة من مدير الأجهزة (Singleton)
        /// </summary>
        public static ZKTecoDeviceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new ZKTecoDeviceManager();
                        }
                    }
                }
                
                return _instance;
            }
        }
        
        /// <summary>
        /// منشئ خاص للفئة
        /// </summary>
        private ZKTecoDeviceManager()
        {
            _unitOfWork = new UnitOfWork();
            _isSyncing = false;
            _syncCancellationTokenSource = new CancellationTokenSource();
        }
        
        /// <summary>
        /// الحصول على قائمة الأجهزة المسجلة في النظام
        /// </summary>
        /// <returns>قائمة الأجهزة</returns>
        public List<BiometricDevice> GetDevices()
        {
            try
            {
                return _unitOfWork.BiometricDeviceRepository.GetAllDevices();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على قائمة أجهزة البصمة");
                return new List<BiometricDevice>();
            }
        }
        
        /// <summary>
        /// الحصول على بيانات جهاز محدد
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>بيانات الجهاز</returns>
        public BiometricDevice GetDevice(int deviceId)
        {
            try
            {
                return _unitOfWork.BiometricDeviceRepository.GetDeviceById(deviceId);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في الحصول على بيانات جهاز البصمة {deviceId}");
                return null;
            }
        }
        
        /// <summary>
        /// إضافة جهاز جديد
        /// </summary>
        /// <param name="device">بيانات الجهاز</param>
        /// <returns>نجاح العملية</returns>
        public bool AddDevice(BiometricDevice device)
        {
            try
            {
                device.CreatedAt = DateTime.Now;
                device.CreatedBy = SessionManager.CurrentUser.ID;
                
                // اختبار الاتصال بالجهاز
                var testResult = TestConnection(device.IPAddress, device.Port);
                
                if (!testResult.IsSuccess)
                {
                    LogManager.LogWarning($"فشل اختبار الاتصال بالجهاز: {testResult.Message}");
                    // نضيف الجهاز حتى لو فشل الاتصال، ولكن نعلم المستخدم
                }
                
                // إضافة الجهاز إلى قاعدة البيانات
                int id = _unitOfWork.BiometricDeviceRepository.AddDevice(device);
                
                return id > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في إضافة جهاز بصمة جديد");
                return false;
            }
        }
        
        /// <summary>
        /// تحديث بيانات جهاز
        /// </summary>
        /// <param name="device">بيانات الجهاز</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateDevice(BiometricDevice device)
        {
            try
            {
                var existingDevice = _unitOfWork.BiometricDeviceRepository.GetDeviceById(device.ID);
                
                if (existingDevice == null)
                {
                    LogManager.LogWarning($"لم يتم العثور على جهاز البصمة {device.ID}");
                    return false;
                }
                
                // هل تم تغيير بيانات الاتصال؟
                bool connectionInfoChanged = existingDevice.IPAddress != device.IPAddress || existingDevice.Port != device.Port;
                
                if (connectionInfoChanged)
                {
                    // اختبار الاتصال بالجهاز بالبيانات الجديدة
                    var testResult = TestConnection(device.IPAddress, device.Port);
                    
                    if (!testResult.IsSuccess)
                    {
                        LogManager.LogWarning($"فشل اختبار الاتصال بالجهاز بالبيانات الجديدة: {testResult.Message}");
                        // نحدث الجهاز حتى لو فشل الاتصال، ولكن نعلم المستخدم
                    }
                }
                
                return _unitOfWork.BiometricDeviceRepository.UpdateDevice(device);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في تحديث بيانات جهاز البصمة {device.ID}");
                return false;
            }
        }
        
        /// <summary>
        /// حذف جهاز
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>نجاح العملية</returns>
        public bool DeleteDevice(int deviceId)
        {
            try
            {
                return _unitOfWork.BiometricDeviceRepository.DeleteDevice(deviceId);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في حذف جهاز البصمة {deviceId}");
                return false;
            }
        }
        
        /// <summary>
        /// اختبار الاتصال بجهاز
        /// </summary>
        /// <param name="ipAddress">عنوان IP</param>
        /// <param name="port">المنفذ</param>
        /// <returns>نتيجة الاختبار</returns>
        public ConnectionTestResult TestConnection(string ipAddress, int port = 4370)
        {
            try
            {
                using (var device = new ZKTecoDevice(ipAddress, port))
                {
                    if (!device.Connect())
                    {
                        return new ConnectionTestResult
                        {
                            IsSuccess = false,
                            Message = "فشل الاتصال بالجهاز",
                            DeviceInfo = null
                        };
                    }
                    
                    var info = device.GetDeviceInfo();
                    
                    if (info == null)
                    {
                        return new ConnectionTestResult
                        {
                            IsSuccess = true,
                            Message = "تم الاتصال بالجهاز، ولكن فشل في الحصول على معلومات الجهاز",
                            DeviceInfo = null
                        };
                    }
                    
                    return new ConnectionTestResult
                    {
                        IsSuccess = true,
                        Message = "تم الاتصال بالجهاز بنجاح",
                        DeviceInfo = info
                    };
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء اختبار الاتصال بالجهاز {ipAddress}:{port}");
                
                return new ConnectionTestResult
                {
                    IsSuccess = false,
                    Message = $"حدث خطأ: {ex.Message}",
                    DeviceInfo = null
                };
            }
        }
        
        /// <summary>
        /// مزامنة بيانات المستخدمين من جهاز محدد
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>نتيجة المزامنة</returns>
        public SyncResult SyncUsers(int deviceId)
        {
            BiometricDevice device = _unitOfWork.BiometricDeviceRepository.GetDeviceById(deviceId);
            
            if (device == null)
            {
                return new SyncResult
                {
                    IsSuccess = false,
                    Message = $"لم يتم العثور على الجهاز {deviceId}",
                    TotalRecords = 0,
                    NewRecords = 0,
                    ErrorRecords = 0
                };
            }
            
            try
            {
                using (var zkDevice = new ZKTecoDevice(device.IPAddress, device.Port ?? 4370))
                {
                    if (!zkDevice.Connect())
                    {
                        return new SyncResult
                        {
                            IsSuccess = false,
                            Message = $"فشل الاتصال بالجهاز {device.DeviceName} ({device.IPAddress})",
                            TotalRecords = 0,
                            NewRecords = 0,
                            ErrorRecords = 0
                        };
                    }
                    
                    // الحصول على قائمة المستخدمين من الجهاز
                    var biometricUsers = zkDevice.GetUsers();
                    
                    if (biometricUsers == null || biometricUsers.Count == 0)
                    {
                        return new SyncResult
                        {
                            IsSuccess = false,
                            Message = "لم يتم العثور على مستخدمين في الجهاز",
                            TotalRecords = 0,
                            NewRecords = 0,
                            ErrorRecords = 0
                        };
                    }
                    
                    // تحديث بيانات المستخدمين في قاعدة البيانات
                    int newUsers = 0;
                    int errorUsers = 0;
                    
                    foreach (var biometricUser in biometricUsers)
                    {
                        try
                        {
                            bool result = _unitOfWork.BiometricDeviceRepository.UpdateOrAddDeviceUser(
                                deviceId,
                                biometricUser.EnrollNumber,
                                biometricUser.Name,
                                biometricUser.Enabled);
                                
                            if (result)
                            {
                                newUsers++;
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.LogException(ex, $"خطأ في تحديث بيانات المستخدم {biometricUser.EnrollNumber} في الجهاز {device.DeviceName}");
                            errorUsers++;
                        }
                    }
                    
                    // تحديث وقت آخر مزامنة
                    device.LastSyncTime = DateTime.Now;
                    device.LastSyncStatus = "Users Sync Completed";
                    _unitOfWork.BiometricDeviceRepository.UpdateDevice(device);
                    
                    return new SyncResult
                    {
                        IsSuccess = true,
                        Message = $"تمت مزامنة بيانات المستخدمين من الجهاز {device.DeviceName} بنجاح",
                        TotalRecords = biometricUsers.Count,
                        NewRecords = newUsers,
                        ErrorRecords = errorUsers
                    };
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء مزامنة بيانات المستخدمين من الجهاز {device.DeviceName}");
                
                // تحديث حالة المزامنة
                try
                {
                    device.LastSyncStatus = "Error";
                    device.LastSyncErrors = ex.Message;
                    _unitOfWork.BiometricDeviceRepository.UpdateDevice(device);
                }
                catch { /* تجاهل أي خطأ */ }
                
                return new SyncResult
                {
                    IsSuccess = false,
                    Message = $"حدث خطأ: {ex.Message}",
                    TotalRecords = 0,
                    NewRecords = 0,
                    ErrorRecords = 0
                };
            }
        }
        
        /// <summary>
        /// مزامنة سجلات الحضور من جهاز محدد
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>نتيجة المزامنة</returns>
        public SyncResult SyncAttendanceLogs(int deviceId)
        {
            BiometricDevice device = _unitOfWork.BiometricDeviceRepository.GetDeviceById(deviceId);
            
            if (device == null)
            {
                return new SyncResult
                {
                    IsSuccess = false,
                    Message = $"لم يتم العثور على الجهاز {deviceId}",
                    TotalRecords = 0,
                    NewRecords = 0,
                    ErrorRecords = 0
                };
            }
            
            try
            {
                using (var zkDevice = new ZKTecoDevice(device.IPAddress, device.Port ?? 4370))
                {
                    if (!zkDevice.Connect())
                    {
                        return new SyncResult
                        {
                            IsSuccess = false,
                            Message = $"فشل الاتصال بالجهاز {device.DeviceName} ({device.IPAddress})",
                            TotalRecords = 0,
                            NewRecords = 0,
                            ErrorRecords = 0
                        };
                    }
                    
                    // الحصول على سجلات الحضور من الجهاز
                    var logs = zkDevice.GetAttendanceLogs();
                    
                    if (logs == null || logs.Count == 0)
                    {
                        return new SyncResult
                        {
                            IsSuccess = false,
                            Message = "لم يتم العثور على سجلات حضور في الجهاز",
                            TotalRecords = 0,
                            NewRecords = 0,
                            ErrorRecords = 0
                        };
                    }
                    
                    // تحديث سجلات الحضور في قاعدة البيانات
                    int newLogs = 0;
                    int errorLogs = 0;
                    
                    foreach (var log in logs)
                    {
                        try
                        {
                            bool result = _unitOfWork.BiometricDeviceRepository.AddAttendanceLog(
                                deviceId,
                                log.EnrollNumber,
                                log.LogTime,
                                log.VerifyMode,
                                log.InOutMode,
                                log.WorkCode);
                                
                            if (result)
                            {
                                newLogs++;
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.LogException(ex, $"خطأ في إضافة سجل الحضور للمستخدم {log.EnrollNumber} في الجهاز {device.DeviceName}");
                            errorLogs++;
                        }
                    }
                    
                    // تحديث وقت آخر مزامنة
                    device.LastSyncTime = DateTime.Now;
                    device.LastSyncStatus = "Attendance Sync Completed";
                    _unitOfWork.BiometricDeviceRepository.UpdateDevice(device);
                    
                    return new SyncResult
                    {
                        IsSuccess = true,
                        Message = $"تمت مزامنة سجلات الحضور من الجهاز {device.DeviceName} بنجاح",
                        TotalRecords = logs.Count,
                        NewRecords = newLogs,
                        ErrorRecords = errorLogs
                    };
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء مزامنة سجلات الحضور من الجهاز {device.DeviceName}");
                
                // تحديث حالة المزامنة
                try
                {
                    device.LastSyncStatus = "Error";
                    device.LastSyncErrors = ex.Message;
                    _unitOfWork.BiometricDeviceRepository.UpdateDevice(device);
                }
                catch { /* تجاهل أي خطأ */ }
                
                return new SyncResult
                {
                    IsSuccess = false,
                    Message = $"حدث خطأ: {ex.Message}",
                    TotalRecords = 0,
                    NewRecords = 0,
                    ErrorRecords = 0
                };
            }
        }
        
        /// <summary>
        /// معالجة سجلات الحضور الخام وتحويلها إلى سجلات حضور
        /// </summary>
        /// <returns>نتيجة المعالجة</returns>
        public ProcessResult ProcessAttendanceLogs()
        {
            try
            {
                // الحصول على السجلات الخام غير المعالجة
                var rawLogs = _unitOfWork.BiometricDeviceRepository.GetUnprocessedAttendanceLogs();
                
                if (rawLogs == null || rawLogs.Count == 0)
                {
                    return new ProcessResult
                    {
                        IsSuccess = true,
                        Message = "لا توجد سجلات خام جديدة للمعالجة",
                        TotalRecords = 0,
                        ProcessedRecords = 0,
                        ErrorRecords = 0
                    };
                }
                
                // تنظيم السجلات حسب الموظف والتاريخ
                var groupedLogs = rawLogs
                    .Where(log => log.EmployeeID.HasValue)
                    .GroupBy(log => new { EmployeeID = log.EmployeeID.Value, Date = log.LogDateTime.Date })
                    .ToDictionary(
                        g => g.Key,
                        g => g.OrderBy(log => log.LogDateTime).ToList()
                    );
                
                int processedRecords = 0;
                int errorRecords = 0;
                
                // معالجة سجلات كل موظف في كل يوم
                foreach (var entry in groupedLogs)
                {
                    try
                    {
                        var employeeID = entry.Key.EmployeeID;
                        var date = entry.Key.Date;
                        var logs = entry.Value;
                        
                        // الحصول على جدول العمل للموظف
                        var workHours = _unitOfWork.EmployeeRepository.GetEmployeeWorkHoursForDate(employeeID, date);
                        
                        if (workHours == null)
                        {
                            LogManager.LogWarning($"لم يتم العثور على جدول عمل للموظف {employeeID} في التاريخ {date}");
                            continue;
                        }
                        
                        // تحديد وقت الدخول (أول بصمة في اليوم)
                        var firstLog = logs.FirstOrDefault();
                        var timeIn = firstLog?.LogDateTime;
                        
                        // تحديد وقت الخروج (آخر بصمة في اليوم)
                        var lastLog = logs.LastOrDefault();
                        var timeOut = (logs.Count > 1) ? lastLog?.LogDateTime : (DateTime?)null;
                        
                        // حساب التأخير والمغادرة المبكرة
                        int lateMinutes = 0;
                        int earlyDepartureMinutes = 0;
                        int workedMinutes = 0;
                        int overtimeMinutes = 0;
                        string status = "حاضر";
                        
                        if (timeIn.HasValue && workHours.StartTime.HasValue)
                        {
                            var expectedTimeIn = date.Add(workHours.StartTime.Value.TimeOfDay);
                            if (timeIn.Value > expectedTimeIn)
                            {
                                lateMinutes = (int)(timeIn.Value - expectedTimeIn).TotalMinutes;
                                status = "متأخر";
                            }
                        }
                        
                        if (timeOut.HasValue && workHours.EndTime.HasValue)
                        {
                            var expectedTimeOut = date.Add(workHours.EndTime.Value.TimeOfDay);
                            if (timeOut.Value < expectedTimeOut)
                            {
                                earlyDepartureMinutes = (int)(expectedTimeOut - timeOut.Value).TotalMinutes;
                                status = (lateMinutes > 0) ? "متأخر ومغادرة مبكرة" : "مغادرة مبكرة";
                            }
                        }
                        
                        if (timeIn.HasValue && timeOut.HasValue)
                        {
                            workedMinutes = (int)(timeOut.Value - timeIn.Value).TotalMinutes;
                            
                            if (workHours.TotalHours.HasValue)
                            {
                                var expectedMinutes = workHours.TotalHours.Value * 60;
                                if (workedMinutes > expectedMinutes)
                                {
                                    overtimeMinutes = workedMinutes - expectedMinutes;
                                }
                            }
                        }
                        else if (timeIn.HasValue && !timeOut.HasValue)
                        {
                            status = "بدون تسجيل خروج";
                        }
                        
                        // إنشاء سجل الحضور
                        var attendanceRecord = new AttendanceRecord
                        {
                            EmployeeID = employeeID,
                            AttendanceDate = date,
                            TimeIn = timeIn,
                            TimeOut = timeOut,
                            WorkHoursID = workHours.ID,
                            LateMinutes = lateMinutes,
                            EarlyDepartureMinutes = earlyDepartureMinutes,
                            OvertimeMinutes = overtimeMinutes,
                            WorkedMinutes = workedMinutes,
                            Status = status,
                            IsManualEntry = false,
                            CreatedAt = DateTime.Now,
                            CreatedBy = SessionManager.CurrentUser?.ID ?? 1
                        };
                        
                        // التحقق من عدم وجود سجل سابق لنفس الموظف ونفس اليوم
                        var existingRecord = _unitOfWork.AttendanceRepository.GetAttendanceRecordByEmployeeAndDate(employeeID, date);
                        
                        if (existingRecord != null)
                        {
                            // تحديث السجل الموجود
                            existingRecord.TimeIn = timeIn;
                            existingRecord.TimeOut = timeOut;
                            existingRecord.LateMinutes = lateMinutes;
                            existingRecord.EarlyDepartureMinutes = earlyDepartureMinutes;
                            existingRecord.OvertimeMinutes = overtimeMinutes;
                            existingRecord.WorkedMinutes = workedMinutes;
                            existingRecord.Status = status;
                            
                            _unitOfWork.AttendanceRepository.UpdateAttendanceRecord(existingRecord);
                        }
                        else
                        {
                            // إضافة سجل جديد
                            _unitOfWork.AttendanceRepository.AddAttendanceRecord(attendanceRecord);
                        }
                        
                        // تحديث حالة السجلات الخام
                        foreach (var log in logs)
                        {
                            _unitOfWork.BiometricDeviceRepository.MarkAttendanceLogAsProcessed(log.ID);
                        }
                        
                        processedRecords++;
                    }
                    catch (Exception ex)
                    {
                        LogManager.LogException(ex, $"خطأ في معالجة سجلات الحضور للموظف {entry.Key.EmployeeID} في التاريخ {entry.Key.Date}");
                        errorRecords++;
                    }
                }
                
                return new ProcessResult
                {
                    IsSuccess = true,
                    Message = "تمت معالجة سجلات الحضور الخام بنجاح",
                    TotalRecords = groupedLogs.Count,
                    ProcessedRecords = processedRecords,
                    ErrorRecords = errorRecords
                };
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء معالجة سجلات الحضور الخام");
                
                return new ProcessResult
                {
                    IsSuccess = false,
                    Message = $"حدث خطأ: {ex.Message}",
                    TotalRecords = 0,
                    ProcessedRecords = 0,
                    ErrorRecords = 0
                };
            }
        }
        
        /// <summary>
        /// مزامنة جميع أجهزة البصمة
        /// </summary>
        /// <returns>نتيجة المزامنة</returns>
        public async Task<SyncAllResult> SyncAllDevicesAsync()
        {
            if (_isSyncing)
            {
                return new SyncAllResult
                {
                    IsSuccess = false,
                    Message = "عملية المزامنة جارية بالفعل",
                    DeviceResults = new List<DeviceSyncResult>()
                };
            }
            
            _isSyncing = true;
            
            try
            {
                // إلغاء أي عملية مزامنة سابقة
                if (_syncCancellationTokenSource != null)
                {
                    _syncCancellationTokenSource.Cancel();
                }
                
                _syncCancellationTokenSource = new CancellationTokenSource();
                var token = _syncCancellationTokenSource.Token;
                
                // الحصول على قائمة الأجهزة النشطة
                var devices = _unitOfWork.BiometricDeviceRepository.GetActiveDevices();
                
                if (devices == null || devices.Count == 0)
                {
                    _isSyncing = false;
                    return new SyncAllResult
                    {
                        IsSuccess = true,
                        Message = "لا توجد أجهزة نشطة للمزامنة",
                        DeviceResults = new List<DeviceSyncResult>()
                    };
                }
                
                List<DeviceSyncResult> results = new List<DeviceSyncResult>();
                
                // مزامنة كل جهاز على حدة
                foreach (var device in devices)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    
                    var deviceResult = new DeviceSyncResult
                    {
                        DeviceId = device.ID,
                        DeviceName = device.DeviceName,
                        IPAddress = device.IPAddress
                    };
                    
                    try
                    {
                        // مزامنة المستخدمين
                        var usersResult = SyncUsers(device.ID);
                        deviceResult.UsersSyncResult = usersResult;
                        
                        // مزامنة سجلات الحضور
                        var logsResult = SyncAttendanceLogs(device.ID);
                        deviceResult.LogsSyncResult = logsResult;
                        
                        deviceResult.IsSuccess = usersResult.IsSuccess || logsResult.IsSuccess;
                        
                        results.Add(deviceResult);
                        
                        // انتظار قليلاً قبل مزامنة الجهاز التالي
                        await Task.Delay(1000, token);
                    }
                    catch (Exception ex)
                    {
                        LogManager.LogException(ex, $"خطأ في مزامنة الجهاز {device.DeviceName}");
                        
                        deviceResult.IsSuccess = false;
                        deviceResult.ErrorMessage = ex.Message;
                        
                        results.Add(deviceResult);
                    }
                }
                
                // معالجة سجلات الحضور
                if (!token.IsCancellationRequested)
                {
                    var processResult = ProcessAttendanceLogs();
                }
                
                return new SyncAllResult
                {
                    IsSuccess = true,
                    Message = "تمت مزامنة الأجهزة بنجاح",
                    DeviceResults = results
                };
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء مزامنة أجهزة البصمة");
                
                return new SyncAllResult
                {
                    IsSuccess = false,
                    Message = $"حدث خطأ: {ex.Message}",
                    DeviceResults = new List<DeviceSyncResult>()
                };
            }
            finally
            {
                _isSyncing = false;
            }
        }
        
        /// <summary>
        /// إلغاء عملية المزامنة الحالية
        /// </summary>
        public void CancelSync()
        {
            if (_isSyncing && _syncCancellationTokenSource != null)
            {
                _syncCancellationTokenSource.Cancel();
            }
        }
    }
    
    /// <summary>
    /// نتيجة اختبار الاتصال بجهاز البصمة
    /// </summary>
    public class ConnectionTestResult
    {
        /// <summary>
        /// هل نجح الاختبار
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// رسالة التوضيح
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// معلومات الجهاز
        /// </summary>
        public DeviceInfo DeviceInfo { get; set; }
    }
    
    /// <summary>
    /// نتيجة عملية المزامنة
    /// </summary>
    public class SyncResult
    {
        /// <summary>
        /// هل نجحت المزامنة
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// رسالة التوضيح
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// إجمالي عدد السجلات
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// عدد السجلات الجديدة
        /// </summary>
        public int NewRecords { get; set; }
        
        /// <summary>
        /// عدد السجلات التي حدث فيها خطأ
        /// </summary>
        public int ErrorRecords { get; set; }
    }
    
    /// <summary>
    /// نتيجة معالجة سجلات الحضور
    /// </summary>
    public class ProcessResult
    {
        /// <summary>
        /// هل نجحت المعالجة
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// رسالة التوضيح
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// إجمالي عدد السجلات
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// عدد السجلات التي تمت معالجتها
        /// </summary>
        public int ProcessedRecords { get; set; }
        
        /// <summary>
        /// عدد السجلات التي حدث فيها خطأ
        /// </summary>
        public int ErrorRecords { get; set; }
    }
    
    /// <summary>
    /// نتيجة مزامنة جهاز واحد
    /// </summary>
    public class DeviceSyncResult
    {
        /// <summary>
        /// معرف الجهاز
        /// </summary>
        public int DeviceId { get; set; }
        
        /// <summary>
        /// اسم الجهاز
        /// </summary>
        public string DeviceName { get; set; }
        
        /// <summary>
        /// عنوان IP للجهاز
        /// </summary>
        public string IPAddress { get; set; }
        
        /// <summary>
        /// هل نجحت المزامنة
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// رسالة الخطأ
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// نتيجة مزامنة المستخدمين
        /// </summary>
        public SyncResult UsersSyncResult { get; set; }
        
        /// <summary>
        /// نتيجة مزامنة سجلات الحضور
        /// </summary>
        public SyncResult LogsSyncResult { get; set; }
    }
    
    /// <summary>
    /// نتيجة مزامنة جميع الأجهزة
    /// </summary>
    public class SyncAllResult
    {
        /// <summary>
        /// هل نجحت المزامنة
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// رسالة التوضيح
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// نتائج مزامنة الأجهزة
        /// </summary>
        public List<DeviceSyncResult> DeviceResults { get; set; }
    }
}