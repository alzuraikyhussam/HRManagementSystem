using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// مستودع بيانات أجهزة البصمة
    /// </summary>
    public class BiometricDeviceRepository
    {
        private readonly ConnectionManager _connectionManager;
        
        /// <summary>
        /// إنشاء مستودع بيانات أجهزة البصمة
        /// </summary>
        public BiometricDeviceRepository()
        {
            _connectionManager = ConnectionManager.Instance;
        }
        
        /// <summary>
        /// الحصول على جميع أجهزة البصمة
        /// </summary>
        /// <returns>قائمة أجهزة البصمة</returns>
        public List<BiometricDevice> GetAllDevices()
        {
            List<BiometricDevice> devices = new List<BiometricDevice>();
            
            string query = @"
                SELECT 
                    ID, DeviceName, DeviceModel, SerialNumber, IPAddress, Port, 
                    CommunicationKey, Description, Location, IsActive, 
                    LastSyncTime, LastSyncStatus, LastSyncErrors, CreatedAt, CreatedBy
                FROM BiometricDevices
                ORDER BY DeviceName";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            devices.Add(MapDeviceFromReader(reader));
                        }
                    }
                }
            }
            
            return devices;
        }
        
        /// <summary>
        /// الحصول على الأجهزة النشطة فقط
        /// </summary>
        /// <returns>قائمة الأجهزة النشطة</returns>
        public List<BiometricDevice> GetActiveDevices()
        {
            List<BiometricDevice> devices = new List<BiometricDevice>();
            
            string query = @"
                SELECT 
                    ID, DeviceName, DeviceModel, SerialNumber, IPAddress, Port, 
                    CommunicationKey, Description, Location, IsActive, 
                    LastSyncTime, LastSyncStatus, LastSyncErrors, CreatedAt, CreatedBy
                FROM BiometricDevices
                WHERE IsActive = 1
                ORDER BY DeviceName";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            devices.Add(MapDeviceFromReader(reader));
                        }
                    }
                }
            }
            
            return devices;
        }
        
        /// <summary>
        /// الحصول على جهاز بصمة بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف الجهاز</param>
        /// <returns>جهاز البصمة</returns>
        public BiometricDevice GetDeviceById(int id)
        {
            string query = @"
                SELECT 
                    ID, DeviceName, DeviceModel, SerialNumber, IPAddress, Port, 
                    CommunicationKey, Description, Location, IsActive, 
                    LastSyncTime, LastSyncStatus, LastSyncErrors, CreatedAt, CreatedBy
                FROM BiometricDevices
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapDeviceFromReader(reader);
                        }
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// إضافة جهاز بصمة جديد
        /// </summary>
        /// <param name="device">بيانات الجهاز</param>
        /// <returns>معرف الجهاز الجديد</returns>
        public int AddDevice(BiometricDevice device)
        {
            string query = @"
                INSERT INTO BiometricDevices (
                    DeviceName, DeviceModel, SerialNumber, IPAddress, Port, 
                    CommunicationKey, Description, Location, IsActive, 
                    LastSyncTime, LastSyncStatus, LastSyncErrors, CreatedAt, CreatedBy
                ) 
                VALUES (
                    @DeviceName, @DeviceModel, @SerialNumber, @IPAddress, @Port, 
                    @CommunicationKey, @Description, @Location, @IsActive, 
                    @LastSyncTime, @LastSyncStatus, @LastSyncErrors, @CreatedAt, @CreatedBy
                );
                SELECT SCOPE_IDENTITY();";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddDeviceParameters(command, device);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// تحديث بيانات جهاز بصمة
        /// </summary>
        /// <param name="device">بيانات الجهاز</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateDevice(BiometricDevice device)
        {
            string query = @"
                UPDATE BiometricDevices
                SET 
                    DeviceName = @DeviceName,
                    DeviceModel = @DeviceModel,
                    SerialNumber = @SerialNumber,
                    IPAddress = @IPAddress,
                    Port = @Port,
                    CommunicationKey = @CommunicationKey,
                    Description = @Description,
                    Location = @Location,
                    IsActive = @IsActive,
                    LastSyncTime = @LastSyncTime,
                    LastSyncStatus = @LastSyncStatus,
                    LastSyncErrors = @LastSyncErrors
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", device.ID);
                    AddDeviceParameters(command, device);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// حذف جهاز بصمة
        /// </summary>
        /// <param name="id">معرف الجهاز</param>
        /// <returns>نجاح العملية</returns>
        public bool DeleteDevice(int id)
        {
            string query = "DELETE FROM BiometricDevices WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// تحديث أو إضافة مستخدم جهاز بصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <param name="biometricUserId">معرف المستخدم في الجهاز</param>
        /// <param name="name">اسم المستخدم</param>
        /// <param name="isActive">حالة التفعيل</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateOrAddDeviceUser(int deviceId, int biometricUserId, string name, bool isActive)
        {
            // التحقق من وجود المستخدم في الجهاز
            string checkQuery = @"
                SELECT COUNT(*)
                FROM BiometricDeviceUsers
                WHERE DeviceID = @DeviceID AND BiometricUserID = @BiometricUserID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                connection.Open();
                
                bool userExists = false;
                
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@DeviceID", deviceId);
                    checkCommand.Parameters.AddWithValue("@BiometricUserID", biometricUserId);
                    
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    userExists = count > 0;
                }
                
                if (userExists)
                {
                    // تحديث بيانات المستخدم الموجود
                    string updateQuery = @"
                        UPDATE BiometricDeviceUsers
                        SET 
                            UserName = @UserName,
                            IsActive = @IsActive,
                            LastUpdate = @LastUpdate
                        WHERE DeviceID = @DeviceID AND BiometricUserID = @BiometricUserID";
                    
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@DeviceID", deviceId);
                        updateCommand.Parameters.AddWithValue("@BiometricUserID", biometricUserId);
                        updateCommand.Parameters.AddWithValue("@UserName", name);
                        updateCommand.Parameters.AddWithValue("@IsActive", isActive);
                        updateCommand.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                        
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                else
                {
                    // إضافة مستخدم جديد
                    string insertQuery = @"
                        INSERT INTO BiometricDeviceUsers (
                            DeviceID, BiometricUserID, UserName, IsActive, CreatedAt, LastUpdate
                        )
                        VALUES (
                            @DeviceID, @BiometricUserID, @UserName, @IsActive, @CreatedAt, @LastUpdate
                        )";
                    
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@DeviceID", deviceId);
                        insertCommand.Parameters.AddWithValue("@BiometricUserID", biometricUserId);
                        insertCommand.Parameters.AddWithValue("@UserName", name);
                        insertCommand.Parameters.AddWithValue("@IsActive", isActive);
                        insertCommand.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        insertCommand.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                        
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
        }
        
        /// <summary>
        /// إضافة سجل حضور خام من جهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <param name="biometricUserId">معرف المستخدم في الجهاز</param>
        /// <param name="logDateTime">وقت التسجيل</param>
        /// <param name="verifyMode">طريقة التحقق</param>
        /// <param name="inOutMode">نوع التسجيل (دخول/خروج)</param>
        /// <param name="workCode">رمز العمل</param>
        /// <returns>نجاح العملية</returns>
        public bool AddAttendanceLog(int deviceId, int biometricUserId, DateTime logDateTime, int verifyMode, int inOutMode, int workCode)
        {
            // التحقق من وجود السجل مسبقاً (لتجنب التكرار)
            string checkQuery = @"
                SELECT COUNT(*)
                FROM RawAttendanceLogs
                WHERE 
                    DeviceID = @DeviceID AND 
                    BiometricUserID = @BiometricUserID AND 
                    LogDateTime = @LogDateTime";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                connection.Open();
                
                bool logExists = false;
                
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@DeviceID", deviceId);
                    checkCommand.Parameters.AddWithValue("@BiometricUserID", biometricUserId);
                    checkCommand.Parameters.AddWithValue("@LogDateTime", logDateTime);
                    
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    logExists = count > 0;
                }
                
                if (logExists)
                {
                    // السجل موجود بالفعل، لا نضيفه مرة أخرى
                    return false;
                }
                
                // البحث عن معرف الموظف المرتبط بمعرف المستخدم في جهاز البصمة
                int? employeeId = null;
                
                string findEmployeeQuery = @"
                    SELECT EmployeeID
                    FROM EmployeeBiometrics
                    WHERE BiometricUserID = @BiometricUserID";
                
                using (SqlCommand findEmployeeCommand = new SqlCommand(findEmployeeQuery, connection))
                {
                    findEmployeeCommand.Parameters.AddWithValue("@BiometricUserID", biometricUserId);
                    
                    object result = findEmployeeCommand.ExecuteScalar();
                    
                    if (result != null && result != DBNull.Value)
                    {
                        employeeId = Convert.ToInt32(result);
                    }
                }
                
                // إضافة سجل حضور جديد
                string insertQuery = @"
                    INSERT INTO RawAttendanceLogs (
                        DeviceID, BiometricUserID, LogDateTime, LogType, VerifyMode, 
                        WorkCode, IsProcessed, IsMatched, EmployeeID, SyncTime
                    )
                    VALUES (
                        @DeviceID, @BiometricUserID, @LogDateTime, @LogType, @VerifyMode, 
                        @WorkCode, @IsProcessed, @IsMatched, @EmployeeID, @SyncTime
                    )";
                
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@DeviceID", deviceId);
                    insertCommand.Parameters.AddWithValue("@BiometricUserID", biometricUserId);
                    insertCommand.Parameters.AddWithValue("@LogDateTime", logDateTime);
                    insertCommand.Parameters.AddWithValue("@LogType", inOutMode);
                    insertCommand.Parameters.AddWithValue("@VerifyMode", verifyMode);
                    insertCommand.Parameters.AddWithValue("@WorkCode", workCode);
                    insertCommand.Parameters.AddWithValue("@IsProcessed", false);
                    insertCommand.Parameters.AddWithValue("@IsMatched", employeeId.HasValue);
                    
                    if (employeeId.HasValue)
                    {
                        insertCommand.Parameters.AddWithValue("@EmployeeID", employeeId.Value);
                    }
                    else
                    {
                        insertCommand.Parameters.AddWithValue("@EmployeeID", DBNull.Value);
                    }
                    
                    insertCommand.Parameters.AddWithValue("@SyncTime", DateTime.Now);
                    
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// الحصول على سجلات الحضور الخام غير المعالجة
        /// </summary>
        /// <returns>قائمة سجلات الحضور الخام</returns>
        public List<RawAttendanceLog> GetUnprocessedAttendanceLogs()
        {
            List<RawAttendanceLog> logs = new List<RawAttendanceLog>();
            
            string query = @"
                SELECT 
                    ID, DeviceID, BiometricUserID, LogDateTime, LogType, VerifyMode, 
                    WorkCode, IsProcessed, IsMatched, EmployeeID, SyncTime
                FROM RawAttendanceLogs
                WHERE IsProcessed = 0 AND IsMatched = 1
                ORDER BY LogDateTime";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(MapRawLogFromReader(reader));
                        }
                    }
                }
            }
            
            return logs;
        }
        
        /// <summary>
        /// تحديث حالة سجل الحضور الخام ليصبح معالجاً
        /// </summary>
        /// <param name="logId">معرف السجل</param>
        /// <returns>نجاح العملية</returns>
        public bool MarkAttendanceLogAsProcessed(int logId)
        {
            string query = @"
                UPDATE RawAttendanceLogs
                SET IsProcessed = 1
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", logId);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// إضافة بارامترات الجهاز إلى الكوماند
        /// </summary>
        /// <param name="command">الكوماند</param>
        /// <param name="device">بيانات الجهاز</param>
        private void AddDeviceParameters(SqlCommand command, BiometricDevice device)
        {
            command.Parameters.AddWithValue("@DeviceName", device.DeviceName);
            command.Parameters.AddWithValue("@DeviceModel", (object)device.DeviceModel ?? DBNull.Value);
            command.Parameters.AddWithValue("@SerialNumber", (object)device.SerialNumber ?? DBNull.Value);
            command.Parameters.AddWithValue("@IPAddress", device.IPAddress);
            command.Parameters.AddWithValue("@Port", (object)device.Port ?? DBNull.Value);
            command.Parameters.AddWithValue("@CommunicationKey", (object)device.CommunicationKey ?? DBNull.Value);
            command.Parameters.AddWithValue("@Description", (object)device.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@Location", (object)device.Location ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", device.IsActive);
            command.Parameters.AddWithValue("@LastSyncTime", (object)device.LastSyncTime ?? DBNull.Value);
            command.Parameters.AddWithValue("@LastSyncStatus", (object)device.LastSyncStatus ?? DBNull.Value);
            command.Parameters.AddWithValue("@LastSyncErrors", (object)device.LastSyncErrors ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreatedAt", device.CreatedAt);
            command.Parameters.AddWithValue("@CreatedBy", (object)device.CreatedBy ?? DBNull.Value);
        }
        
        /// <summary>
        /// تحويل من قارئ البيانات إلى كائن جهاز بصمة
        /// </summary>
        /// <param name="reader">قارئ البيانات</param>
        /// <returns>كائن جهاز بصمة</returns>
        private BiometricDevice MapDeviceFromReader(SqlDataReader reader)
        {
            return new BiometricDevice
            {
                ID = Convert.ToInt32(reader["ID"]),
                DeviceName = reader["DeviceName"].ToString(),
                DeviceModel = reader["DeviceModel"] != DBNull.Value ? reader["DeviceModel"].ToString() : null,
                SerialNumber = reader["SerialNumber"] != DBNull.Value ? reader["SerialNumber"].ToString() : null,
                IPAddress = reader["IPAddress"].ToString(),
                Port = reader["Port"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Port"]) : null,
                CommunicationKey = reader["CommunicationKey"] != DBNull.Value ? reader["CommunicationKey"].ToString() : null,
                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                Location = reader["Location"] != DBNull.Value ? reader["Location"].ToString() : null,
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                LastSyncTime = reader["LastSyncTime"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["LastSyncTime"]) : null,
                LastSyncStatus = reader["LastSyncStatus"] != DBNull.Value ? reader["LastSyncStatus"].ToString() : null,
                LastSyncErrors = reader["LastSyncErrors"] != DBNull.Value ? reader["LastSyncErrors"].ToString() : null,
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                CreatedBy = reader["CreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CreatedBy"]) : null
            };
        }
        
        /// <summary>
        /// تحويل من قارئ البيانات إلى كائن سجل حضور خام
        /// </summary>
        /// <param name="reader">قارئ البيانات</param>
        /// <returns>كائن سجل حضور خام</returns>
        private RawAttendanceLog MapRawLogFromReader(SqlDataReader reader)
        {
            return new RawAttendanceLog
            {
                ID = Convert.ToInt32(reader["ID"]),
                DeviceID = Convert.ToInt32(reader["DeviceID"]),
                BiometricUserID = Convert.ToInt32(reader["BiometricUserID"]),
                LogDateTime = Convert.ToDateTime(reader["LogDateTime"]),
                LogType = reader["LogType"] != DBNull.Value ? (int?)Convert.ToInt32(reader["LogType"]) : null,
                VerifyMode = reader["VerifyMode"] != DBNull.Value ? (int?)Convert.ToInt32(reader["VerifyMode"]) : null,
                WorkCode = reader["WorkCode"] != DBNull.Value ? (int?)Convert.ToInt32(reader["WorkCode"]) : null,
                IsProcessed = Convert.ToBoolean(reader["IsProcessed"]),
                IsMatched = Convert.ToBoolean(reader["IsMatched"]),
                EmployeeID = reader["EmployeeID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["EmployeeID"]) : null,
                SyncTime = Convert.ToDateTime(reader["SyncTime"])
            };
        }
    }
}