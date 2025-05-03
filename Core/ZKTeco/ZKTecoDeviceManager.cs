using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HR.DataAccess;
using HR.Models;

namespace HR.Core.ZKTeco
{
    /// <summary>
    /// مدير أجهزة البصمة ZKTeco
    /// </summary>
    public class ZKTecoDeviceManager
    {
        private readonly BiometricDeviceRepository _deviceRepository;
        private readonly List<ZKTecoDevice> _connectedDevices;
        
        /// <summary>
        /// إنشاء مدير أجهزة بصمة جديد
        /// </summary>
        public ZKTecoDeviceManager()
        {
            _deviceRepository = new BiometricDeviceRepository();
            _connectedDevices = new List<ZKTecoDevice>();
        }
        
        /// <summary>
        /// الحصول على قائمة أجهزة البصمة المسجلة
        /// </summary>
        /// <returns>قائمة الأجهزة</returns>
        public List<BiometricDevice> GetRegisteredDevices()
        {
            return _deviceRepository.GetAllDevices();
        }
        
        /// <summary>
        /// إضافة جهاز بصمة جديد
        /// </summary>
        /// <param name="device">بيانات الجهاز</param>
        /// <returns>رقم الجهاز</returns>
        public int AddDevice(BiometricDevice device)
        {
            return _deviceRepository.AddDevice(device);
        }
        
        /// <summary>
        /// تحديث بيانات جهاز
        /// </summary>
        /// <param name="device">بيانات الجهاز</param>
        /// <returns>نتيجة التحديث</returns>
        public bool UpdateDevice(BiometricDevice device)
        {
            return _deviceRepository.UpdateDevice(device);
        }
        
        /// <summary>
        /// حذف جهاز
        /// </summary>
        /// <param name="deviceID">رقم الجهاز</param>
        /// <returns>نتيجة الحذف</returns>
        public bool DeleteDevice(int deviceID)
        {
            return _deviceRepository.DeleteDevice(deviceID);
        }
        
        /// <summary>
        /// الاتصال بجهاز
        /// </summary>
        /// <param name="deviceID">رقم الجهاز</param>
        /// <returns>نتيجة الاتصال</returns>
        public bool ConnectDevice(int deviceID)
        {
            try
            {
                // الحصول على بيانات الجهاز
                BiometricDevice device = _deviceRepository.GetDeviceByID(deviceID);
                if (device == null)
                {
                    LogManager.LogError($"لم يتم العثور على جهاز البصمة رقم {deviceID}");
                    return false;
                }
                
                // التحقق مما إذا كان الجهاز متصل بالفعل
                if (_connectedDevices.Any(d => d.DeviceID == deviceID))
                {
                    LogManager.LogInfo($"جهاز البصمة {device.DeviceName} متصل بالفعل");
                    return true;
                }
                
                // إنشاء اتصال جديد بالجهاز
                ZKTecoDevice zkDevice = new ZKTecoDevice(device);
                bool result = zkDevice.Connect();
                
                if (result)
                {
                    // إضافة الجهاز إلى قائمة الأجهزة المتصلة
                    _connectedDevices.Add(zkDevice);
                    
                    // تحديث حالة الجهاز في قاعدة البيانات
                    device.LastConnection = DateTime.Now;
                    device.ConnectionStatus = "Connected";
                    _deviceRepository.UpdateDevice(device);
                    
                    LogManager.LogInfo($"تم الاتصال بجهاز البصمة {device.DeviceName} بنجاح");
                    return true;
                }
                else
                {
                    // تحديث حالة الجهاز في قاعدة البيانات
                    device.ConnectionStatus = "Disconnected";
                    device.LastError = "فشل الاتصال بالجهاز";
                    _deviceRepository.UpdateDevice(device);
                    
                    LogManager.LogError($"فشل الاتصال بجهاز البصمة {device.DeviceName}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء الاتصال بجهاز البصمة رقم {deviceID}");
                return false;
            }
        }
        
        /// <summary>
        /// قطع الاتصال بجهاز
        /// </summary>
        /// <param name="deviceID">رقم الجهاز</param>
        /// <returns>نتيجة قطع الاتصال</returns>
        public bool DisconnectDevice(int deviceID)
        {
            try
            {
                // البحث عن الجهاز في قائمة الأجهزة المتصلة
                ZKTecoDevice zkDevice = _connectedDevices.FirstOrDefault(d => d.DeviceID == deviceID);
                if (zkDevice == null)
                {
                    LogManager.LogWarning($"جهاز البصمة رقم {deviceID} غير متصل حالياً");
                    return true;
                }
                
                // قطع الاتصال بالجهاز
                bool result = zkDevice.Disconnect();
                
                if (result)
                {
                    // إزالة الجهاز من قائمة الأجهزة المتصلة
                    _connectedDevices.Remove(zkDevice);
                    
                    // تحديث حالة الجهاز في قاعدة البيانات
                    BiometricDevice device = _deviceRepository.GetDeviceByID(deviceID);
                    if (device != null)
                    {
                        device.ConnectionStatus = "Disconnected";
                        _deviceRepository.UpdateDevice(device);
                    }
                    
                    LogManager.LogInfo($"تم قطع الاتصال بجهاز البصمة رقم {deviceID} بنجاح");
                    return true;
                }
                else
                {
                    LogManager.LogError($"فشل قطع الاتصال بجهاز البصمة رقم {deviceID}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء قطع الاتصال بجهاز البصمة رقم {deviceID}");
                return false;
            }
        }
        
        /// <summary>
        /// استيراد سجلات الحضور من جهاز
        /// </summary>
        /// <param name="deviceID">رقم الجهاز</param>
        /// <returns>عدد السجلات المستوردة</returns>
        public int ImportAttendanceRecords(int deviceID)
        {
            try
            {
                // التحقق من اتصال الجهاز
                ZKTecoDevice zkDevice = _connectedDevices.FirstOrDefault(d => d.DeviceID == deviceID);
                if (zkDevice == null)
                {
                    // محاولة الاتصال بالجهاز
                    bool connected = ConnectDevice(deviceID);
                    if (!connected)
                    {
                        LogManager.LogError($"فشل الاتصال بجهاز البصمة رقم {deviceID}");
                        return 0;
                    }
                    
                    zkDevice = _connectedDevices.FirstOrDefault(d => d.DeviceID == deviceID);
                }
                
                // استيراد سجلات الحضور
                List<RawAttendanceLog> logs = zkDevice.GetAttendanceLogs();
                
                if (logs == null || logs.Count == 0)
                {
                    LogManager.LogInfo($"لم يتم العثور على سجلات حضور جديدة في جهاز البصمة رقم {deviceID}");
                    return 0;
                }
                
                // حفظ السجلات في قاعدة البيانات
                int count = 0;
                foreach (RawAttendanceLog log in logs)
                {
                    // التحقق من عدم وجود السجل مسبقاً
                    if (!_deviceRepository.AttendanceLogExists(log.DeviceID, log.UserID, log.LogTime))
                    {
                        int id = _deviceRepository.AddAttendanceLog(log);
                        if (id > 0)
                        {
                            count++;
                        }
                    }
                }
                
                // تحديث وقت آخر استيراد في بيانات الجهاز
                BiometricDevice device = _deviceRepository.GetDeviceByID(deviceID);
                if (device != null)
                {
                    device.LastDownload = DateTime.Now;
                    _deviceRepository.UpdateDevice(device);
                }
                
                LogManager.LogInfo($"تم استيراد {count} سجل حضور من جهاز البصمة رقم {deviceID}");
                return count;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء استيراد سجلات الحضور من جهاز البصمة رقم {deviceID}");
                return 0;
            }
        }
        
        /// <summary>
        /// مزامنة بيانات المستخدمين مع الجهاز
        /// </summary>
        /// <param name="deviceID">رقم الجهاز</param>
        /// <returns>نتيجة المزامنة</returns>
        public bool SynchronizeUsers(int deviceID)
        {
            try
            {
                // التحقق من اتصال الجهاز
                ZKTecoDevice zkDevice = _connectedDevices.FirstOrDefault(d => d.DeviceID == deviceID);
                if (zkDevice == null)
                {
                    // محاولة الاتصال بالجهاز
                    bool connected = ConnectDevice(deviceID);
                    if (!connected)
                    {
                        LogManager.LogError($"فشل الاتصال بجهاز البصمة رقم {deviceID}");
                        return false;
                    }
                    
                    zkDevice = _connectedDevices.FirstOrDefault(d => d.DeviceID == deviceID);
                }
                
                // الحصول على الموظفين النشطين
                EmployeeRepository employeeRepo = new EmployeeRepository();
                List<Employee> employees = employeeRepo.GetActiveEmployees();
                
                if (employees == null || employees.Count == 0)
                {
                    LogManager.LogWarning("لم يتم العثور على موظفين نشطين للمزامنة مع جهاز البصمة");
                    return false;
                }
                
                // مزامنة كل موظف مع الجهاز
                int successCount = 0;
                foreach (Employee employee in employees)
                {
                    // التحقق من وجود رقم بصمة للموظف
                    if (string.IsNullOrEmpty(employee.BiometricID))
                    {
                        // إنشاء رقم بصمة جديد للموظف
                        employee.BiometricID = employee.ID.ToString().PadLeft(8, '0');
                        employeeRepo.UpdateEmployee(employee);
                    }
                    
                    // مزامنة بيانات الموظف مع الجهاز
                    bool result = zkDevice.AddOrUpdateUser(employee.BiometricID, employee.FullName);
                    if (result)
                    {
                        successCount++;
                    }
                }
                
                // تحديث وقت آخر مزامنة في بيانات الجهاز
                BiometricDevice device = _deviceRepository.GetDeviceByID(deviceID);
                if (device != null)
                {
                    device.LastSync = DateTime.Now;
                    _deviceRepository.UpdateDevice(device);
                }
                
                LogManager.LogInfo($"تمت مزامنة {successCount} موظف من أصل {employees.Count} مع جهاز البصمة رقم {deviceID}");
                return successCount > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء مزامنة المستخدمين مع جهاز البصمة رقم {deviceID}");
                return false;
            }
        }
        
        /// <summary>
        /// تسجيل بصمة موظف
        /// </summary>
        /// <param name="deviceID">رقم الجهاز</param>
        /// <param name="employeeID">رقم الموظف</param>
        /// <returns>نتيجة التسجيل</returns>
        public bool EnrollFingerprint(int deviceID, int employeeID)
        {
            try
            {
                // التحقق من اتصال الجهاز
                ZKTecoDevice zkDevice = _connectedDevices.FirstOrDefault(d => d.DeviceID == deviceID);
                if (zkDevice == null)
                {
                    // محاولة الاتصال بالجهاز
                    bool connected = ConnectDevice(deviceID);
                    if (!connected)
                    {
                        LogManager.LogError($"فشل الاتصال بجهاز البصمة رقم {deviceID}");
                        return false;
                    }
                    
                    zkDevice = _connectedDevices.FirstOrDefault(d => d.DeviceID == deviceID);
                }
                
                // الحصول على بيانات الموظف
                EmployeeRepository employeeRepo = new EmployeeRepository();
                Employee employee = employeeRepo.GetEmployeeByID(employeeID);
                
                if (employee == null)
                {
                    LogManager.LogError($"لم يتم العثور على الموظف رقم {employeeID}");
                    return false;
                }
                
                // التحقق من وجود رقم بصمة للموظف
                if (string.IsNullOrEmpty(employee.BiometricID))
                {
                    // إنشاء رقم بصمة جديد للموظف
                    employee.BiometricID = employee.ID.ToString().PadLeft(8, '0');
                    employeeRepo.UpdateEmployee(employee);
                }
                
                // بدء عملية تسجيل البصمة
                bool result = zkDevice.EnrollFingerprint(employee.BiometricID, employee.FullName);
                
                if (result)
                {
                    LogManager.LogInfo($"تم تسجيل بصمة الموظف {employee.FullName} بنجاح على جهاز البصمة رقم {deviceID}");
                    return true;
                }
                else
                {
                    LogManager.LogError($"فشل تسجيل بصمة الموظف {employee.FullName} على جهاز البصمة رقم {deviceID}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء تسجيل بصمة الموظف رقم {employeeID} على جهاز البصمة رقم {deviceID}");
                return false;
            }
        }
        
        /// <summary>
        /// تحديث وقت الجهاز
        /// </summary>
        /// <param name="deviceID">رقم الجهاز</param>
        /// <returns>نتيجة التحديث</returns>
        public bool SynchronizeTime(int deviceID)
        {
            try
            {
                // التحقق من اتصال الجهاز
                ZKTecoDevice zkDevice = _connectedDevices.FirstOrDefault(d => d.DeviceID == deviceID);
                if (zkDevice == null)
                {
                    // محاولة الاتصال بالجهاز
                    bool connected = ConnectDevice(deviceID);
                    if (!connected)
                    {
                        LogManager.LogError($"فشل الاتصال بجهاز البصمة رقم {deviceID}");
                        return false;
                    }
                    
                    zkDevice = _connectedDevices.FirstOrDefault(d => d.DeviceID == deviceID);
                }
                
                // تحديث وقت الجهاز
                bool result = zkDevice.SetDeviceTime(DateTime.Now);
                
                if (result)
                {
                    LogManager.LogInfo($"تم تحديث وقت جهاز البصمة رقم {deviceID} بنجاح");
                    return true;
                }
                else
                {
                    LogManager.LogError($"فشل تحديث وقت جهاز البصمة رقم {deviceID}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء تحديث وقت جهاز البصمة رقم {deviceID}");
                return false;
            }
        }
        
        /// <summary>
        /// استيراد سجلات الحضور من جميع الأجهزة
        /// </summary>
        /// <returns>عدد السجلات المستوردة</returns>
        public int ImportAttendanceRecordsFromAllDevices()
        {
            try
            {
                // الحصول على قائمة الأجهزة النشطة
                List<BiometricDevice> devices = _deviceRepository.GetActiveDevices();
                
                if (devices == null || devices.Count == 0)
                {
                    LogManager.LogWarning("لم يتم العثور على أجهزة بصمة نشطة");
                    return 0;
                }
                
                int totalCount = 0;
                
                // استيراد السجلات من كل جهاز
                foreach (BiometricDevice device in devices)
                {
                    int count = ImportAttendanceRecords(device.ID);
                    totalCount += count;
                    
                    // إضافة فترة انتظار بين كل جهاز وآخر
                    Thread.Sleep(500);
                }
                
                LogManager.LogInfo($"تم استيراد {totalCount} سجل حضور من {devices.Count} جهاز بصمة");
                return totalCount;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء استيراد سجلات الحضور من جميع الأجهزة");
                return 0;
            }
        }
        
        /// <summary>
        /// معالجة سجلات الحضور الخام
        /// </summary>
        /// <returns>عدد السجلات المعالجة</returns>
        public int ProcessRawAttendanceLogs()
        {
            try
            {
                // الحصول على السجلات الخام غير المعالجة
                List<RawAttendanceLog> rawLogs = _deviceRepository.GetUnprocessedAttendanceLogs();
                
                if (rawLogs == null || rawLogs.Count == 0)
                {
                    LogManager.LogInfo("لا توجد سجلات حضور خام للمعالجة");
                    return 0;
                }
                
                int processedCount = 0;
                
                // معالجة كل سجل
                foreach (RawAttendanceLog rawLog in rawLogs)
                {
                    try
                    {
                        // البحث عن الموظف بواسطة رقم البصمة
                        EmployeeRepository employeeRepo = new EmployeeRepository();
                        Employee employee = employeeRepo.GetEmployeeByBiometricID(rawLog.UserID);
                        
                        if (employee == null)
                        {
                            // تحديث حالة السجل إلى "خطأ"
                            rawLog.ProcessingStatus = "Error";
                            rawLog.ProcessingNotes = "لم يتم العثور على الموظف برقم البصمة " + rawLog.UserID;
                            _deviceRepository.UpdateAttendanceLog(rawLog);
                            continue;
                        }
                        
                        // إنشاء سجل حضور جديد
                        AttendanceRecord record = new AttendanceRecord
                        {
                            EmployeeID = employee.ID,
                            LogDateTime = rawLog.LogTime,
                            LogType = DetermineLogType(rawLog.LogTime),
                            DeviceID = rawLog.DeviceID,
                            Status = "Auto",
                            Notes = $"تم إنشاؤه تلقائياً من سجل البصمة رقم {rawLog.ID}"
                        };
                        
                        // حفظ سجل الحضور
                        AttendanceRepository attendanceRepo = new AttendanceRepository();
                        int recordID = attendanceRepo.AddAttendanceRecord(record);
                        
                        if (recordID > 0)
                        {
                            // تحديث حالة السجل الخام
                            rawLog.ProcessingStatus = "Processed";
                            rawLog.ProcessingNotes = $"تمت معالجته وإنشاء سجل الحضور رقم {recordID}";
                            _deviceRepository.UpdateAttendanceLog(rawLog);
                            
                            processedCount++;
                        }
                        else
                        {
                            // تحديث حالة السجل إلى "خطأ"
                            rawLog.ProcessingStatus = "Error";
                            rawLog.ProcessingNotes = "فشل إنشاء سجل الحضور";
                            _deviceRepository.UpdateAttendanceLog(rawLog);
                        }
                    }
                    catch (Exception ex)
                    {
                        // تحديث حالة السجل إلى "خطأ"
                        rawLog.ProcessingStatus = "Error";
                        rawLog.ProcessingNotes = $"حدث خطأ أثناء المعالجة: {ex.Message}";
                        _deviceRepository.UpdateAttendanceLog(rawLog);
                        
                        LogManager.LogException(ex, $"حدث خطأ أثناء معالجة سجل الحضور الخام رقم {rawLog.ID}");
                    }
                }
                
                LogManager.LogInfo($"تمت معالجة {processedCount} سجل حضور من أصل {rawLogs.Count}");
                return processedCount;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء معالجة سجلات الحضور الخام");
                return 0;
            }
        }
        
        /// <summary>
        /// تحديد نوع السجل بناءً على الوقت
        /// </summary>
        /// <param name="logTime">وقت التسجيل</param>
        /// <returns>نوع السجل (حضور/انصراف)</returns>
        private string DetermineLogType(DateTime logTime)
        {
            // افتراضياً، الأوقات قبل الظهر تعتبر حضور، والأوقات بعد الظهر تعتبر انصراف
            if (logTime.Hour < 12)
            {
                return "CheckIn";
            }
            else
            {
                return "CheckOut";
            }
        }
    }
}