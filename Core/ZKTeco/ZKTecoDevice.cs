using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using HR.Models;

namespace HR.Core.ZKTeco
{
    /// <summary>
    /// فئة تمثل جهاز بصمة ZKTeco وتتعامل مع واجهة SDK
    /// </summary>
    public class ZKTecoDevice : IDisposable
    {
        #region SDK Function Imports
        
        [DllImport("zkemkeeper.dll", EntryPoint = "ZKEM_CreateSDK")]
        public static extern IntPtr ZKEM_CreateSDK();
        
        [DllImport("zkemkeeper.dll", EntryPoint = "ZKEM_Fini")]
        public static extern void ZKEM_Fini(IntPtr handle);
        
        [DllImport("zkemkeeper.dll", EntryPoint = "ZKEM_Connect")]
        public static extern bool ZKEM_Connect(IntPtr handle, string ipAddress, int port, int timeout);
        
        [DllImport("zkemkeeper.dll", EntryPoint = "ZKEM_Disconnect")]
        public static extern void ZKEM_Disconnect(IntPtr handle);
        
        [DllImport("zkemkeeper.dll", EntryPoint = "ZKEM_GetDeviceInfo")]
        public static extern bool ZKEM_GetDeviceInfo(IntPtr handle, ref int deviceType, ref int serialNumber, ref int versionInfo);
        
        [DllImport("zkemkeeper.dll", EntryPoint = "ZKEM_GetDeviceTime")]
        public static extern bool ZKEM_GetDeviceTime(IntPtr handle, ref int year, ref int month, ref int day, ref int hour, ref int minute, ref int second);
        
        [DllImport("zkemkeeper.dll", EntryPoint = "ZKEM_SetDeviceTime")]
        public static extern bool ZKEM_SetDeviceTime(IntPtr handle, int year, int month, int day, int hour, int minute, int second);
        
        [DllImport("zkemkeeper.dll", EntryPoint = "ZKEM_RefreshData")]
        public static extern bool ZKEM_RefreshData(IntPtr handle);
        
        [DllImport("zkemkeeper.dll", EntryPoint = "ZKEM_ReadAllUserID")]
        public static extern bool ZKEM_ReadAllUserID(IntPtr handle);
        
        [DllImport("zkemkeeper.dll", EntryPoint = "ZKEM_GetAllUserInfo")]
        public static extern bool ZKEM_GetAllUserInfo(IntPtr handle, ref int dwEnrollNumber, IntPtr name, IntPtr password, ref int privilege, ref bool enabled);
        
        [DllImport("zkemkeeper.dll", EntryPoint = "ZKEM_GetUserInfo")]
        public static extern bool ZKEM_GetUserInfo(IntPtr handle, int dwEnrollNumber, IntPtr name, IntPtr password, ref int privilege, ref bool enabled);
        
        [DllImport("zkemkeeper.dll", EntryPoint = "ZKEM_ReadGeneralLogData")]
        public static extern bool ZKEM_ReadGeneralLogData(IntPtr handle);
        
        [DllImport("zkemkeeper.dll", EntryPoint = "ZKEM_GetGeneralLogData")]
        public static extern bool ZKEM_GetGeneralLogData(IntPtr handle, ref int dwEnrollNumber, ref int dwVerifyMode, ref int dwInOutMode, ref int dwYear, ref int dwMonth, ref int dwDay, ref int dwHour, ref int dwMinute, ref int dwSecond, ref int dwWorkCode);
        
        #endregion
        
        private IntPtr _deviceHandle;
        private readonly string _ipAddress;
        private readonly int _port;
        private readonly int _timeout;
        private bool _isConnected;
        private bool _disposed;
        
        /// <summary>
        /// إنشاء كائن جديد لجهاز ZKTeco
        /// </summary>
        /// <param name="ipAddress">عنوان IP للجهاز</param>
        /// <param name="port">المنفذ (عادة 4370)</param>
        /// <param name="timeout">مهلة الاتصال بالثواني</param>
        public ZKTecoDevice(string ipAddress, int port = 4370, int timeout = 5)
        {
            _ipAddress = ipAddress;
            _port = port;
            _timeout = timeout * 1000; // تحويل إلى ميلي ثانية
            _deviceHandle = IntPtr.Zero;
            _isConnected = false;
        }
        
        /// <summary>
        /// الاتصال بالجهاز
        /// </summary>
        /// <returns>نجاح العملية</returns>
        public bool Connect()
        {
            if (_isConnected)
                return true;
            
            try
            {
                // التحقق من إمكانية الوصول للجهاز
                using (var ping = new System.Net.NetworkInformation.Ping())
                {
                    var reply = ping.Send(_ipAddress, 1000);
                    if (reply == null || reply.Status != System.Net.NetworkInformation.IPStatus.Success)
                    {
                        LogManager.LogWarning($"فشل في الوصول للجهاز (ZKTeco) على العنوان {_ipAddress}");
                        return false;
                    }
                }
                
                // إنشاء مقبض SDK
                _deviceHandle = ZKEM_CreateSDK();
                
                if (_deviceHandle == IntPtr.Zero)
                {
                    LogManager.LogError("فشل في إنشاء مقبض SDK لجهاز البصمة");
                    return false;
                }
                
                // الاتصال بالجهاز
                _isConnected = ZKEM_Connect(_deviceHandle, _ipAddress, _port, _timeout);
                
                if (!_isConnected)
                {
                    LogManager.LogError($"فشل في الاتصال بجهاز البصمة على العنوان {_ipAddress}:{_port}");
                    ZKEM_Fini(_deviceHandle);
                    _deviceHandle = IntPtr.Zero;
                    return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء الاتصال بجهاز البصمة {_ipAddress}");
                
                if (_deviceHandle != IntPtr.Zero)
                {
                    ZKEM_Fini(_deviceHandle);
                    _deviceHandle = IntPtr.Zero;
                }
                
                _isConnected = false;
                return false;
            }
        }
        
        /// <summary>
        /// إغلاق الاتصال بالجهاز
        /// </summary>
        public void Disconnect()
        {
            if (_isConnected && _deviceHandle != IntPtr.Zero)
            {
                ZKEM_Disconnect(_deviceHandle);
                ZKEM_Fini(_deviceHandle);
                _deviceHandle = IntPtr.Zero;
                _isConnected = false;
            }
        }
        
        /// <summary>
        /// الحصول على معلومات الجهاز
        /// </summary>
        /// <returns>معلومات الجهاز</returns>
        public DeviceInfo GetDeviceInfo()
        {
            if (!_isConnected)
            {
                if (!Connect())
                    return null;
            }
            
            try
            {
                int deviceType = 0;
                int serialNumber = 0;
                int versionInfo = 0;
                
                bool result = ZKEM_GetDeviceInfo(_deviceHandle, ref deviceType, ref serialNumber, ref versionInfo);
                
                if (!result)
                {
                    LogManager.LogWarning($"فشل في الحصول على معلومات الجهاز {_ipAddress}");
                    return null;
                }
                
                var deviceInfo = new DeviceInfo
                {
                    DeviceType = deviceType,
                    SerialNumber = serialNumber.ToString(),
                    FirmwareVersion = versionInfo.ToString()
                };
                
                // الحصول على وقت الجهاز
                int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0;
                if (ZKEM_GetDeviceTime(_deviceHandle, ref year, ref month, ref day, ref hour, ref minute, ref second))
                {
                    deviceInfo.DeviceTime = new DateTime(year, month, day, hour, minute, second);
                }
                
                return deviceInfo;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء الحصول على معلومات الجهاز {_ipAddress}");
                return null;
            }
        }
        
        /// <summary>
        /// تحديث وقت الجهاز ليتطابق مع وقت الخادم
        /// </summary>
        /// <returns>نجاح العملية</returns>
        public bool SyncDeviceTime()
        {
            if (!_isConnected)
            {
                if (!Connect())
                    return false;
            }
            
            try
            {
                DateTime now = DateTime.Now;
                bool result = ZKEM_SetDeviceTime(
                    _deviceHandle,
                    now.Year,
                    now.Month,
                    now.Day,
                    now.Hour,
                    now.Minute,
                    now.Second);
                
                if (!result)
                {
                    LogManager.LogWarning($"فشل في تحديث وقت الجهاز {_ipAddress}");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء تحديث وقت الجهاز {_ipAddress}");
                return false;
            }
        }
        
        /// <summary>
        /// الحصول على قائمة المستخدمين في الجهاز
        /// </summary>
        /// <returns>قائمة المستخدمين</returns>
        public List<BiometricUser> GetUsers()
        {
            if (!_isConnected)
            {
                if (!Connect())
                    return null;
            }
            
            try
            {
                // تحديث بيانات المستخدمين
                if (!ZKEM_ReadAllUserID(_deviceHandle))
                {
                    LogManager.LogWarning($"فشل في قراءة معرفات المستخدمين من الجهاز {_ipAddress}");
                    return null;
                }
                
                List<BiometricUser> users = new List<BiometricUser>();
                bool hasMoreUsers = true;
                
                // قراءة معلومات المستخدمين
                while (hasMoreUsers)
                {
                    int enrollNumber = 0;
                    IntPtr name = Marshal.AllocHGlobal(256);
                    IntPtr password = Marshal.AllocHGlobal(64);
                    int privilege = 0;
                    bool enabled = false;
                    
                    try
                    {
                        hasMoreUsers = ZKEM_GetAllUserInfo(_deviceHandle, ref enrollNumber, name, password, ref privilege, ref enabled);
                        
                        if (hasMoreUsers && enrollNumber > 0)
                        {
                            string userName = Marshal.PtrToStringAnsi(name);
                            string userPassword = Marshal.PtrToStringAnsi(password);
                            
                            users.Add(new BiometricUser
                            {
                                EnrollNumber = enrollNumber,
                                Name = userName,
                                Password = userPassword,
                                Privilege = privilege,
                                Enabled = enabled
                            });
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(name);
                        Marshal.FreeHGlobal(password);
                    }
                }
                
                return users;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء الحصول على قائمة المستخدمين من الجهاز {_ipAddress}");
                return null;
            }
        }
        
        /// <summary>
        /// الحصول على معلومات مستخدم محدد
        /// </summary>
        /// <param name="enrollNumber">معرف المستخدم</param>
        /// <returns>معلومات المستخدم</returns>
        public BiometricUser GetUser(int enrollNumber)
        {
            if (!_isConnected)
            {
                if (!Connect())
                    return null;
            }
            
            try
            {
                IntPtr name = Marshal.AllocHGlobal(256);
                IntPtr password = Marshal.AllocHGlobal(64);
                int privilege = 0;
                bool enabled = false;
                
                try
                {
                    bool success = ZKEM_GetUserInfo(_deviceHandle, enrollNumber, name, password, ref privilege, ref enabled);
                    
                    if (!success)
                    {
                        LogManager.LogWarning($"فشل في الحصول على معلومات المستخدم {enrollNumber} من الجهاز {_ipAddress}");
                        return null;
                    }
                    
                    string userName = Marshal.PtrToStringAnsi(name);
                    string userPassword = Marshal.PtrToStringAnsi(password);
                    
                    return new BiometricUser
                    {
                        EnrollNumber = enrollNumber,
                        Name = userName,
                        Password = userPassword,
                        Privilege = privilege,
                        Enabled = enabled
                    };
                }
                finally
                {
                    Marshal.FreeHGlobal(name);
                    Marshal.FreeHGlobal(password);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء الحصول على معلومات المستخدم {enrollNumber} من الجهاز {_ipAddress}");
                return null;
            }
        }
        
        /// <summary>
        /// الحصول على سجلات الحضور من الجهاز
        /// </summary>
        /// <returns>قائمة سجلات الحضور</returns>
        public List<BiometricLog> GetAttendanceLogs()
        {
            if (!_isConnected)
            {
                if (!Connect())
                    return null;
            }
            
            try
            {
                // تحديث بيانات السجلات
                if (!ZKEM_ReadGeneralLogData(_deviceHandle))
                {
                    LogManager.LogWarning($"فشل في قراءة سجلات الحضور من الجهاز {_ipAddress}");
                    return null;
                }
                
                List<BiometricLog> logs = new List<BiometricLog>();
                bool hasMoreLogs = true;
                
                // قراءة السجلات
                while (hasMoreLogs)
                {
                    int enrollNumber = 0;
                    int verifyMode = 0;
                    int inOutMode = 0;
                    int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0;
                    int workCode = 0;
                    
                    hasMoreLogs = ZKEM_GetGeneralLogData(
                        _deviceHandle,
                        ref enrollNumber,
                        ref verifyMode,
                        ref inOutMode,
                        ref year,
                        ref month,
                        ref day,
                        ref hour,
                        ref minute,
                        ref second,
                        ref workCode);
                    
                    if (hasMoreLogs && enrollNumber > 0)
                    {
                        DateTime logDateTime;
                        try
                        {
                            logDateTime = new DateTime(year, month, day, hour, minute, second);
                        }
                        catch
                        {
                            // في حالة عدم صحة التاريخ
                            logDateTime = DateTime.Now;
                        }
                        
                        logs.Add(new BiometricLog
                        {
                            EnrollNumber = enrollNumber,
                            LogTime = logDateTime,
                            VerifyMode = verifyMode,
                            InOutMode = inOutMode,
                            WorkCode = workCode
                        });
                    }
                }
                
                return logs;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء الحصول على سجلات الحضور من الجهاز {_ipAddress}");
                return null;
            }
        }
        
        /// <summary>
        /// التخلص من الموارد
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        /// <summary>
        /// التخلص من الموارد
        /// </summary>
        /// <param name="disposing">هل يتم التخلص من الموارد المدارة</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // تنظيف الموارد المدارة
                }
                
                // تنظيف الموارد غير المدارة
                Disconnect();
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// المنظف
        /// </summary>
        ~ZKTecoDevice()
        {
            Dispose(false);
        }
    }
    
    /// <summary>
    /// كائن يمثل معلومات جهاز البصمة
    /// </summary>
    public class DeviceInfo
    {
        /// <summary>
        /// نوع الجهاز
        /// </summary>
        public int DeviceType { get; set; }
        
        /// <summary>
        /// الرقم التسلسلي
        /// </summary>
        public string SerialNumber { get; set; }
        
        /// <summary>
        /// إصدار البرنامج الثابت
        /// </summary>
        public string FirmwareVersion { get; set; }
        
        /// <summary>
        /// وقت الجهاز
        /// </summary>
        public DateTime DeviceTime { get; set; }
    }
    
    /// <summary>
    /// كائن يمثل مستخدم جهاز البصمة
    /// </summary>
    public class BiometricUser
    {
        /// <summary>
        /// معرف المستخدم
        /// </summary>
        public int EnrollNumber { get; set; }
        
        /// <summary>
        /// اسم المستخدم
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// كلمة المرور
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// مستوى الصلاحية
        /// </summary>
        public int Privilege { get; set; }
        
        /// <summary>
        /// هل المستخدم مفعل
        /// </summary>
        public bool Enabled { get; set; }
    }
    
    /// <summary>
    /// كائن يمثل سجل حضور من جهاز البصمة
    /// </summary>
    public class BiometricLog
    {
        /// <summary>
        /// معرف المستخدم
        /// </summary>
        public int EnrollNumber { get; set; }
        
        /// <summary>
        /// وقت تسجيل البصمة
        /// </summary>
        public DateTime LogTime { get; set; }
        
        /// <summary>
        /// طريقة التحقق
        /// </summary>
        public int VerifyMode { get; set; }
        
        /// <summary>
        /// نوع السجل (دخول/خروج)
        /// </summary>
        public int InOutMode { get; set; }
        
        /// <summary>
        /// رمز العمل
        /// </summary>
        public int WorkCode { get; set; }
    }
}