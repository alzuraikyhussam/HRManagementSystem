using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Threading;
using HR.Models;

namespace HR.Core
{
    /// <summary>
    /// مدير اتصال وتزامن أجهزة البصمة ZKTeco
    /// </summary>
    public class ZKTecoManager
    {
        // تخزين مؤقت للأجهزة المتصلة
        private static Dictionary<int, zkemkeeper.CZKEMClass> _connectedDevices = new Dictionary<int, zkemkeeper.CZKEMClass>();
        
        /// <summary>
        /// الاتصال بجهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>نجاح الاتصال</returns>
        public static bool ConnectDevice(int deviceId)
        {
            try
            {
                // التحقق مما إذا كان الجهاز متصلا بالفعل
                if (_connectedDevices.ContainsKey(deviceId) && IsDeviceConnected(deviceId))
                {
                    return true;
                }
                
                // الحصول على بيانات الجهاز من قاعدة البيانات
                BiometricDevice device = GetDeviceById(deviceId);
                
                if (device == null)
                {
                    LogManager.LogError($"جهاز البصمة غير موجود (ID: {deviceId})");
                    return false;
                }
                
                if (!device.IsActive)
                {
                    LogManager.LogError($"جهاز البصمة غير نشط (ID: {deviceId})");
                    return false;
                }
                
                // إنشاء كائن الاتصال
                zkemkeeper.CZKEMClass zk = new zkemkeeper.CZKEMClass();
                
                // محاولة الاتصال بالجهاز
                bool connected = zk.Connect_Net(device.IPAddress, device.Port);
                
                if (!connected)
                {
                    LogManager.LogError($"فشل الاتصال بجهاز البصمة (ID: {deviceId}, IP: {device.IPAddress}, Port: {device.Port})");
                    return false;
                }
                
                // التحقق من كلمة المرور إذا كانت مطلوبة
                if (!string.IsNullOrEmpty(device.CommunicationKey))
                {
                    if (!zk.SetCommPassword(Convert.ToInt32(device.CommunicationKey)))
                    {
                        zk.Disconnect();
                        LogManager.LogError($"فشل التحقق من كلمة مرور جهاز البصمة (ID: {deviceId})");
                        return false;
                    }
                }
                
                // تخزين الاتصال المنشأ
                _connectedDevices[deviceId] = zk;
                
                // تحديث حالة الجهاز في قاعدة البيانات
                UpdateDeviceStatus(deviceId, true);
                
                LogManager.LogInfo($"تم الاتصال بجهاز البصمة بنجاح (ID: {deviceId}, IP: {device.IPAddress})");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل الاتصال بجهاز البصمة (ID: {deviceId})");
                return false;
            }
        }
        
        /// <summary>
        /// قطع الاتصال بجهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>نجاح قطع الاتصال</returns>
        public static bool DisconnectDevice(int deviceId)
        {
            try
            {
                if (!_connectedDevices.ContainsKey(deviceId))
                {
                    return true;
                }
                
                zkemkeeper.CZKEMClass zk = _connectedDevices[deviceId];
                zk.Disconnect();
                _connectedDevices.Remove(deviceId);
                
                // تحديث حالة الجهاز في قاعدة البيانات
                UpdateDeviceStatus(deviceId, false);
                
                LogManager.LogInfo($"تم قطع الاتصال بجهاز البصمة (ID: {deviceId})");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل قطع الاتصال بجهاز البصمة (ID: {deviceId})");
                return false;
            }
        }
        
        /// <summary>
        /// التحقق من حالة اتصال الجهاز
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>هل الجهاز متصل؟</returns>
        public static bool IsDeviceConnected(int deviceId)
        {
            try
            {
                if (!_connectedDevices.ContainsKey(deviceId))
                {
                    return false;
                }
                
                zkemkeeper.CZKEMClass zk = _connectedDevices[deviceId];
                return zk.GetLastError() == 0;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// مزامنة سجلات البصمة من الجهاز
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>عدد السجلات المزامنة</returns>
        public static int SyncAttendanceLogs(int deviceId)
        {
            try
            {
                if (!ConnectDevice(deviceId))
                {
                    return -1;
                }
                
                zkemkeeper.CZKEMClass zk = _connectedDevices[deviceId];
                
                // التحضير لقراءة سجلات الحضور
                string enrollNumber = string.Empty;
                int verifyMode = 0;
                int inOutMode = 0;
                int year = 0;
                int month = 0;
                int day = 0;
                int hour = 0;
                int minute = 0;
                int second = 0;
                int workCode = 0;
                
                // تحديد وقت آخر مزامنة
                DateTime lastSyncTime = GetLastSyncTime(deviceId);
                DateTime newSyncTime = DateTime.Now;
                int syncedRecords = 0;
                
                // قراءة سجلات البصمة
                bool readSuccess = zk.ReadGeneralLogData(1);
                
                if (!readSuccess)
                {
                    LogManager.LogError($"فشل قراءة سجلات البصمة (ID: {deviceId})");
                    return -1;
                }
                
                // معالجة البيانات
                while (zk.SSR_GetGeneralLogData(1, out enrollNumber, out verifyMode, out inOutMode, 
                    out year, out month, out day, out hour, out minute, out second, ref workCode))
                {
                    // إنشاء كائن DateTime من البيانات
                    DateTime logDateTime = new DateTime(year, month, day, hour, minute, second);
                    
                    // التحقق مما إذا كان السجل جديدًا منذ آخر مزامنة
                    if (logDateTime > lastSyncTime)
                    {
                        // حفظ السجل في قاعدة البيانات
                        SaveAttendanceLog(deviceId, enrollNumber, logDateTime, verifyMode, inOutMode, workCode);
                        syncedRecords++;
                    }
                }
                
                // تحديث وقت آخر مزامنة
                UpdateLastSyncTime(deviceId, newSyncTime, syncedRecords);
                
                LogManager.LogInfo($"تمت مزامنة سجلات البصمة (ID: {deviceId}, Records: {syncedRecords})");
                return syncedRecords;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل مزامنة سجلات البصمة (ID: {deviceId})");
                
                // تحديث حالة المزامنة في قاعدة البيانات
                UpdateSyncStatus(deviceId, "فشل المزامنة", ex.Message);
                return -1;
            }
        }
        
        /// <summary>
        /// مزامنة معلومات المستخدمين من جهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>عدد المستخدمين المزامنين</returns>
        public static int SyncUsers(int deviceId)
        {
            try
            {
                if (!ConnectDevice(deviceId))
                {
                    return -1;
                }
                
                zkemkeeper.CZKEMClass zk = _connectedDevices[deviceId];
                
                // التحضير لقراءة معلومات المستخدمين
                string enrollNumber = string.Empty;
                string name = string.Empty;
                string password = string.Empty;
                int privilege = 0;
                bool enabled = false;
                
                // عداد المستخدمين المزامنين
                int syncedUsers = 0;
                
                // قراءة معلومات المستخدمين
                zk.ReadAllUserID(1);
                
                // معالجة البيانات
                while (zk.SSR_GetAllUserInfo(1, out enrollNumber, out name, out password, out privilege, out enabled))
                {
                    // تحديث بيانات المستخدم
                    UpdateUserBiometricInfo(enrollNumber, name, deviceId);
                    syncedUsers++;
                }
                
                LogManager.LogInfo($"تمت مزامنة معلومات المستخدمين (ID: {deviceId}, Users: {syncedUsers})");
                return syncedUsers;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل مزامنة معلومات المستخدمين (ID: {deviceId})");
                return -1;
            }
        }
        
        /// <summary>
        /// الحصول على معلومات جهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>معلومات جهاز البصمة</returns>
        public static BiometricDeviceInfo GetDeviceInfo(int deviceId)
        {
            try
            {
                if (!ConnectDevice(deviceId))
                {
                    return null;
                }
                
                zkemkeeper.CZKEMClass zk = _connectedDevices[deviceId];
                
                // معلومات الجهاز
                string firmwareVersion = string.Empty;
                string productCode = string.Empty;
                string manufacturerCode = string.Empty;
                
                // الحصول على معلومات الجهاز
                zk.GetFirmwareVersion(1, ref firmwareVersion);
                zk.GetProductCode(1, ref productCode);
                zk.GetDeviceInfo(1, 1, ref manufacturerCode);
                
                // إحصائيات الجهاز
                int userCount = 0;
                int fpCount = 0;
                int recordCount = 0;
                
                zk.GetDeviceStatus(1, 2, ref userCount);
                zk.GetDeviceStatus(1, 3, ref fpCount);
                zk.GetDeviceStatus(1, 4, ref recordCount);
                
                // إنشاء كائن معلومات الجهاز
                BiometricDeviceInfo info = new BiometricDeviceInfo
                {
                    DeviceID = deviceId,
                    FirmwareVersion = firmwareVersion,
                    ProductCode = productCode,
                    ManufacturerCode = manufacturerCode,
                    UserCount = userCount,
                    FingerprintCount = fpCount,
                    RecordCount = recordCount,
                    IsConnected = true
                };
                
                return info;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل الحصول على معلومات جهاز البصمة (ID: {deviceId})");
                return null;
            }
        }
        
        /// <summary>
        /// تفريغ سجلات الحضور من الجهاز
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>نجاح عملية التفريغ</returns>
        public static bool ClearAttendanceLogs(int deviceId)
        {
            try
            {
                if (!ConnectDevice(deviceId))
                {
                    return false;
                }
                
                zkemkeeper.CZKEMClass zk = _connectedDevices[deviceId];
                
                // تفريغ سجلات الحضور
                bool success = zk.ClearGLog(1);
                
                if (!success)
                {
                    LogManager.LogError($"فشل تفريغ سجلات الحضور (ID: {deviceId})");
                    return false;
                }
                
                LogManager.LogInfo($"تم تفريغ سجلات الحضور (ID: {deviceId})");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل تفريغ سجلات الحضور (ID: {deviceId})");
                return false;
            }
        }
        
        /// <summary>
        /// إعادة تشغيل جهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>نجاح عملية إعادة التشغيل</returns>
        public static bool RestartDevice(int deviceId)
        {
            try
            {
                if (!ConnectDevice(deviceId))
                {
                    return false;
                }
                
                zkemkeeper.CZKEMClass zk = _connectedDevices[deviceId];
                
                // إعادة تشغيل الجهاز
                bool success = zk.RestartDevice(1);
                
                if (!success)
                {
                    LogManager.LogError($"فشل إعادة تشغيل جهاز البصمة (ID: {deviceId})");
                    return false;
                }
                
                // إزالة الجهاز من القائمة
                _connectedDevices.Remove(deviceId);
                
                LogManager.LogInfo($"تم إعادة تشغيل جهاز البصمة (ID: {deviceId})");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل إعادة تشغيل جهاز البصمة (ID: {deviceId})");
                return false;
            }
        }
        
        /// <summary>
        /// تعيين التاريخ والوقت في جهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <param name="dateTime">التاريخ والوقت</param>
        /// <returns>نجاح عملية التعيين</returns>
        public static bool SetDeviceTime(int deviceId, DateTime dateTime)
        {
            try
            {
                if (!ConnectDevice(deviceId))
                {
                    return false;
                }
                
                zkemkeeper.CZKEMClass zk = _connectedDevices[deviceId];
                
                // تعيين التاريخ والوقت
                bool success = zk.SetDeviceTime2(1, dateTime.Year, dateTime.Month, dateTime.Day, 
                    dateTime.Hour, dateTime.Minute, dateTime.Second);
                
                if (!success)
                {
                    LogManager.LogError($"فشل تعيين التاريخ والوقت في جهاز البصمة (ID: {deviceId})");
                    return false;
                }
                
                LogManager.LogInfo($"تم تعيين التاريخ والوقت في جهاز البصمة (ID: {deviceId})");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل تعيين التاريخ والوقت في جهاز البصمة (ID: {deviceId})");
                return false;
            }
        }
        
        /// <summary>
        /// الحصول على التاريخ والوقت من جهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>التاريخ والوقت</returns>
        public static DateTime GetDeviceTime(int deviceId)
        {
            try
            {
                if (!ConnectDevice(deviceId))
                {
                    return DateTime.MinValue;
                }
                
                zkemkeeper.CZKEMClass zk = _connectedDevices[deviceId];
                
                // الحصول على التاريخ والوقت
                int year = 0;
                int month = 0;
                int day = 0;
                int hour = 0;
                int minute = 0;
                int second = 0;
                
                bool success = zk.GetDeviceTime(1, ref year, ref month, ref day, ref hour, ref minute, ref second);
                
                if (!success)
                {
                    LogManager.LogError($"فشل الحصول على التاريخ والوقت من جهاز البصمة (ID: {deviceId})");
                    return DateTime.MinValue;
                }
                
                DateTime deviceTime = new DateTime(year, month, day, hour, minute, second);
                return deviceTime;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل الحصول على التاريخ والوقت من جهاز البصمة (ID: {deviceId})");
                return DateTime.MinValue;
            }
        }
        
        /// <summary>
        /// مزامنة معلومات المستخدم مع جهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="biometricId">معرف البصمة</param>
        /// <param name="name">اسم الموظف</param>
        /// <returns>نجاح عملية المزامنة</returns>
        public static bool SyncUserToDevice(int deviceId, int employeeId, int biometricId, string name)
        {
            try
            {
                if (!ConnectDevice(deviceId))
                {
                    return false;
                }
                
                zkemkeeper.CZKEMClass zk = _connectedDevices[deviceId];
                
                // مزامنة معلومات المستخدم
                string enrollNumber = biometricId.ToString();
                string password = string.Empty;
                int privilege = 0;
                bool enabled = true;
                
                bool success = zk.SSR_SetUserInfo(1, enrollNumber, name, password, privilege, enabled);
                
                if (!success)
                {
                    LogManager.LogError($"فشل مزامنة معلومات المستخدم مع جهاز البصمة (ID: {deviceId}, EmployeeID: {employeeId})");
                    return false;
                }
                
                LogManager.LogInfo($"تم مزامنة معلومات المستخدم مع جهاز البصمة (ID: {deviceId}, EmployeeID: {employeeId})");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل مزامنة معلومات المستخدم مع جهاز البصمة (ID: {deviceId}, EmployeeID: {employeeId})");
                return false;
            }
        }
        
        /// <summary>
        /// حذف مستخدم من جهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <param name="biometricId">معرف البصمة</param>
        /// <returns>نجاح عملية الحذف</returns>
        public static bool DeleteUserFromDevice(int deviceId, int biometricId)
        {
            try
            {
                if (!ConnectDevice(deviceId))
                {
                    return false;
                }
                
                zkemkeeper.CZKEMClass zk = _connectedDevices[deviceId];
                
                // حذف المستخدم
                string enrollNumber = biometricId.ToString();
                bool success = zk.SSR_DeleteEnrollData(1, enrollNumber, 12);
                
                if (!success)
                {
                    LogManager.LogError($"فشل حذف المستخدم من جهاز البصمة (ID: {deviceId}, BiometricID: {biometricId})");
                    return false;
                }
                
                LogManager.LogInfo($"تم حذف المستخدم من جهاز البصمة (ID: {deviceId}, BiometricID: {biometricId})");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل حذف المستخدم من جهاز البصمة (ID: {deviceId}, BiometricID: {biometricId})");
                return false;
            }
        }
        
        /// <summary>
        /// إغلاق جميع الاتصالات بأجهزة البصمة
        /// </summary>
        public static void CloseAllConnections()
        {
            foreach (int deviceId in new List<int>(_connectedDevices.Keys))
            {
                DisconnectDevice(deviceId);
            }
            
            _connectedDevices.Clear();
            LogManager.LogInfo("تم إغلاق جميع الاتصالات بأجهزة البصمة");
        }
        
        #region Database Operations
        
        /// <summary>
        /// الحصول على بيانات جهاز البصمة من قاعدة البيانات
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>بيانات جهاز البصمة</returns>
        private static BiometricDevice GetDeviceById(int deviceId)
        {
            string query = @"
                SELECT * FROM BiometricDevices 
                WHERE ID = @DeviceID";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DeviceID", deviceId)
            };
            
            DataTable deviceTable = ConnectionManager.ExecuteQuery(query, parameters);
            
            if (deviceTable.Rows.Count == 0)
            {
                return null;
            }
            
            DataRow row = deviceTable.Rows[0];
            
            BiometricDevice device = new BiometricDevice
            {
                ID = Convert.ToInt32(row["ID"]),
                DeviceName = row["DeviceName"].ToString(),
                DeviceModel = row["DeviceModel"].ToString(),
                SerialNumber = row["SerialNumber"].ToString(),
                IPAddress = row["IPAddress"].ToString(),
                Port = Convert.ToInt32(row["Port"]),
                CommunicationKey = row["CommunicationKey"].ToString(),
                Description = row["Description"].ToString(),
                Location = row["Location"].ToString(),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                LastSyncTime = row["LastSyncTime"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["LastSyncTime"]) : null,
                LastSyncStatus = row["LastSyncStatus"].ToString(),
                LastSyncErrors = row["LastSyncErrors"].ToString(),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                CreatedBy = row["CreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(row["CreatedBy"]) : null,
                UpdatedAt = row["UpdatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["UpdatedAt"]) : null,
                UpdatedBy = row["UpdatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(row["UpdatedBy"]) : null
            };
            
            return device;
        }
        
        /// <summary>
        /// تحديث حالة جهاز البصمة في قاعدة البيانات
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <param name="isConnected">حالة الاتصال</param>
        private static void UpdateDeviceStatus(int deviceId, bool isConnected)
        {
            string query = @"
                UPDATE BiometricDevices 
                SET LastSyncStatus = @Status, 
                    UpdatedAt = @UpdatedAt 
                WHERE ID = @DeviceID";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DeviceID", deviceId),
                new SqlParameter("@Status", isConnected ? "متصل" : "غير متصل"),
                new SqlParameter("@UpdatedAt", DateTime.Now)
            };
            
            ConnectionManager.ExecuteNonQuery(query, parameters);
        }
        
        /// <summary>
        /// الحصول على وقت آخر مزامنة لجهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>وقت آخر مزامنة</returns>
        private static DateTime GetLastSyncTime(int deviceId)
        {
            string query = @"
                SELECT LastSyncTime 
                FROM BiometricDevices 
                WHERE ID = @DeviceID";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DeviceID", deviceId)
            };
            
            object result = ConnectionManager.ExecuteScalar(query, parameters);
            
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToDateTime(result);
            }
            
            return DateTime.MinValue;
        }
        
        /// <summary>
        /// تحديث وقت آخر مزامنة لجهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <param name="syncTime">وقت المزامنة</param>
        /// <param name="syncedRecords">عدد السجلات المزامنة</param>
        private static void UpdateLastSyncTime(int deviceId, DateTime syncTime, int syncedRecords)
        {
            string query = @"
                UPDATE BiometricDevices 
                SET LastSyncTime = @SyncTime, 
                    LastSyncStatus = @Status, 
                    LastSyncErrors = NULL, 
                    UpdatedAt = @UpdatedAt 
                WHERE ID = @DeviceID";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DeviceID", deviceId),
                new SqlParameter("@SyncTime", syncTime),
                new SqlParameter("@Status", $"تمت المزامنة ({syncedRecords} سجل)"),
                new SqlParameter("@UpdatedAt", DateTime.Now)
            };
            
            ConnectionManager.ExecuteNonQuery(query, parameters);
        }
        
        /// <summary>
        /// تحديث حالة المزامنة لجهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <param name="status">حالة المزامنة</param>
        /// <param name="errors">رسالة الخطأ</param>
        private static void UpdateSyncStatus(int deviceId, string status, string errors)
        {
            string query = @"
                UPDATE BiometricDevices 
                SET LastSyncStatus = @Status, 
                    LastSyncErrors = @Errors, 
                    UpdatedAt = @UpdatedAt 
                WHERE ID = @DeviceID";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DeviceID", deviceId),
                new SqlParameter("@Status", status),
                new SqlParameter("@Errors", errors),
                new SqlParameter("@UpdatedAt", DateTime.Now)
            };
            
            ConnectionManager.ExecuteNonQuery(query, parameters);
        }
        
        /// <summary>
        /// حفظ سجل حضور في قاعدة البيانات
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <param name="enrollNumber">رقم المستخدم في الجهاز</param>
        /// <param name="logDateTime">وقت تسجيل البصمة</param>
        /// <param name="verifyMode">طريقة التحقق</param>
        /// <param name="inOutMode">نوع التسجيل</param>
        /// <param name="workCode">رمز العمل</param>
        private static void SaveAttendanceLog(int deviceId, string enrollNumber, DateTime logDateTime, int verifyMode, int inOutMode, int workCode)
        {
            try
            {
                // التحقق من وجود السجل مسبقا
                string checkQuery = @"
                    SELECT COUNT(*) 
                    FROM RawAttendanceLogs 
                    WHERE DeviceID = @DeviceID 
                    AND BiometricUserID = @BiometricUserID 
                    AND LogDateTime = @LogDateTime";
                
                SqlParameter[] checkParameters = new SqlParameter[]
                {
                    new SqlParameter("@DeviceID", deviceId),
                    new SqlParameter("@BiometricUserID", enrollNumber),
                    new SqlParameter("@LogDateTime", logDateTime)
                };
                
                int existingCount = Convert.ToInt32(ConnectionManager.ExecuteScalar(checkQuery, checkParameters));
                
                if (existingCount > 0)
                {
                    return; // تجاوز السجلات المكررة
                }
                
                // الحصول على معرف الموظف المرتبط
                int? employeeId = GetEmployeeIdByBiometricId(enrollNumber);
                
                // حفظ السجل
                string insertQuery = @"
                    INSERT INTO RawAttendanceLogs 
                    (DeviceID, BiometricUserID, LogDateTime, LogType, VerifyMode, WorkCode, IsProcessed, IsMatched, EmployeeID, SyncTime) 
                    VALUES 
                    (@DeviceID, @BiometricUserID, @LogDateTime, @LogType, @VerifyMode, @WorkCode, @IsProcessed, @IsMatched, @EmployeeID, @SyncTime)";
                
                SqlParameter[] insertParameters = new SqlParameter[]
                {
                    new SqlParameter("@DeviceID", deviceId),
                    new SqlParameter("@BiometricUserID", enrollNumber),
                    new SqlParameter("@LogDateTime", logDateTime),
                    new SqlParameter("@LogType", inOutMode),
                    new SqlParameter("@VerifyMode", verifyMode),
                    new SqlParameter("@WorkCode", workCode),
                    new SqlParameter("@IsProcessed", false),
                    new SqlParameter("@IsMatched", employeeId != null),
                    new SqlParameter("@EmployeeID", (object)employeeId ?? DBNull.Value),
                    new SqlParameter("@SyncTime", DateTime.Now)
                };
                
                ConnectionManager.ExecuteNonQuery(insertQuery, insertParameters);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل حفظ سجل الحضور (DeviceID: {deviceId}, BiometricID: {enrollNumber})");
            }
        }
        
        /// <summary>
        /// الحصول على معرف الموظف باستخدام معرف البصمة
        /// </summary>
        /// <param name="biometricId">معرف البصمة</param>
        /// <returns>معرف الموظف</returns>
        private static int? GetEmployeeIdByBiometricId(string biometricId)
        {
            string query = @"
                SELECT ID 
                FROM Employees 
                WHERE BiometricID = @BiometricID";
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@BiometricID", biometricId)
            };
            
            object result = ConnectionManager.ExecuteScalar(query, parameters);
            
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            
            return null;
        }
        
        /// <summary>
        /// تحديث معلومات البصمة للموظف
        /// </summary>
        /// <param name="biometricId">معرف البصمة</param>
        /// <param name="name">اسم المستخدم في الجهاز</param>
        /// <param name="deviceId">معرف الجهاز</param>
        private static void UpdateUserBiometricInfo(string biometricId, string name, int deviceId)
        {
            try
            {
                // البحث عن الموظف بمعرف البصمة
                int? employeeId = GetEmployeeIdByBiometricId(biometricId);
                
                if (employeeId == null)
                {
                    // تسجيل وجود مستخدم في الجهاز غير مرتبط بموظف
                    LogManager.LogWarning($"مستخدم غير مرتبط بموظف (BiometricID: {biometricId}, Name: {name}, DeviceID: {deviceId})");
                    return;
                }
                
                // تحديث سجلات البصمة غير المطابقة
                string updateQuery = @"
                    UPDATE RawAttendanceLogs 
                    SET IsMatched = 1, 
                        EmployeeID = @EmployeeID 
                    WHERE BiometricUserID = @BiometricUserID 
                    AND (IsMatched = 0 OR EmployeeID IS NULL)";
                
                SqlParameter[] updateParameters = new SqlParameter[]
                {
                    new SqlParameter("@BiometricUserID", biometricId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                
                ConnectionManager.ExecuteNonQuery(updateQuery, updateParameters);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل تحديث معلومات البصمة للموظف (BiometricID: {biometricId})");
            }
        }
        
        #endregion
    }
    
    /// <summary>
    /// معلومات عن جهاز البصمة
    /// </summary>
    public class BiometricDeviceInfo
    {
        public int DeviceID { get; set; }
        public string FirmwareVersion { get; set; }
        public string ProductCode { get; set; }
        public string ManufacturerCode { get; set; }
        public int UserCount { get; set; }
        public int FingerprintCount { get; set; }
        public int RecordCount { get; set; }
        public bool IsConnected { get; set; }
    }
}