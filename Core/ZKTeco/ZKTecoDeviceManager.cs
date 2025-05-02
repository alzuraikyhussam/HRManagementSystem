using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;
using HR.DataAccess;
using HR.Models;

namespace HR.Core.ZKTeco
{
    /// <summary>
    /// مدير أجهزة البصمة ZKTeco
    /// </summary>
    public class ZKTecoDeviceManager
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;

        /// <summary>
        /// إنشاء مدير أجهزة البصمة
        /// </summary>
        public ZKTecoDeviceManager()
        {
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
        }

        /// <summary>
        /// اختبار الاتصال بجهاز البصمة
        /// </summary>
        /// <param name="ipAddress">عنوان IP للجهاز</param>
        /// <param name="port">المنفذ</param>
        /// <returns>نتيجة الاتصال</returns>
        public bool TestConnection(string ipAddress, int port)
        {
            try
            {
                // اختبار اتصال TCP
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(ipAddress, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5));

                    if (!success)
                    {
                        return false;
                    }

                    client.EndConnect(result);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// إضافة جهاز بصمة جديد
        /// </summary>
        /// <param name="device">بيانات الجهاز</param>
        /// <returns>معرف الجهاز الجديد</returns>
        public int AddDevice(BiometricDevice device)
        {
            try
            {
                string query = @"
                INSERT INTO BiometricDevices (
                    DeviceName, DeviceModel, SerialNumber, IPAddress, Port, CommunicationKey,
                    Description, Location, IsActive, CreatedAt, CreatedBy
                ) VALUES (
                    @DeviceName, @DeviceModel, @SerialNumber, @IPAddress, @Port, @CommunicationKey,
                    @Description, @Location, @IsActive, @CreatedAt, @CreatedBy
                );
                SELECT SCOPE_IDENTITY();";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@DeviceName", device.DeviceName),
                    new SqlParameter("@DeviceModel", device.DeviceModel ?? (object)DBNull.Value),
                    new SqlParameter("@SerialNumber", device.SerialNumber ?? (object)DBNull.Value),
                    new SqlParameter("@IPAddress", device.IPAddress),
                    new SqlParameter("@Port", device.Port),
                    new SqlParameter("@CommunicationKey", device.CommunicationKey ?? (object)DBNull.Value),
                    new SqlParameter("@Description", device.Description ?? (object)DBNull.Value),
                    new SqlParameter("@Location", device.Location ?? (object)DBNull.Value),
                    new SqlParameter("@IsActive", device.IsActive),
                    new SqlParameter("@CreatedAt", DateTime.Now),
                    new SqlParameter("@CreatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                };

                object result = _dbContext.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء إضافة جهاز بصمة جديد: " + ex.Message);
            }
        }

        /// <summary>
        /// تحديث بيانات جهاز بصمة
        /// </summary>
        /// <param name="device">بيانات الجهاز</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateDevice(BiometricDevice device)
        {
            try
            {
                string query = @"
                UPDATE BiometricDevices SET
                    DeviceName = @DeviceName,
                    DeviceModel = @DeviceModel,
                    SerialNumber = @SerialNumber,
                    IPAddress = @IPAddress,
                    Port = @Port,
                    CommunicationKey = @CommunicationKey,
                    Description = @Description,
                    Location = @Location,
                    IsActive = @IsActive,
                    UpdatedAt = @UpdatedAt,
                    UpdatedBy = @UpdatedBy
                WHERE ID = @ID";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@ID", device.ID),
                    new SqlParameter("@DeviceName", device.DeviceName),
                    new SqlParameter("@DeviceModel", device.DeviceModel ?? (object)DBNull.Value),
                    new SqlParameter("@SerialNumber", device.SerialNumber ?? (object)DBNull.Value),
                    new SqlParameter("@IPAddress", device.IPAddress),
                    new SqlParameter("@Port", device.Port),
                    new SqlParameter("@CommunicationKey", device.CommunicationKey ?? (object)DBNull.Value),
                    new SqlParameter("@Description", device.Description ?? (object)DBNull.Value),
                    new SqlParameter("@Location", device.Location ?? (object)DBNull.Value),
                    new SqlParameter("@IsActive", device.IsActive),
                    new SqlParameter("@UpdatedAt", DateTime.Now),
                    new SqlParameter("@UpdatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                };

                int affectedRows = _dbContext.ExecuteNonQuery(query, parameters);
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء تحديث بيانات جهاز البصمة: " + ex.Message);
            }
        }

        /// <summary>
        /// حذف جهاز بصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>نجاح العملية</returns>
        public bool DeleteDevice(int deviceId)
        {
            try
            {
                string query = "DELETE FROM BiometricDevices WHERE ID = @ID";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@ID", deviceId)
                };

                int affectedRows = _dbContext.ExecuteNonQuery(query, parameters);
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء حذف جهاز البصمة: " + ex.Message);
            }
        }

        /// <summary>
        /// الحصول على جميع أجهزة البصمة
        /// </summary>
        /// <returns>قائمة أجهزة البصمة</returns>
        public List<BiometricDevice> GetAllDevices()
        {
            try
            {
                string query = @"
                SELECT 
                    ID, DeviceName, DeviceModel, SerialNumber, IPAddress, Port, CommunicationKey,
                    Description, Location, IsActive, LastSyncTime, LastSyncStatus, LastSyncErrors,
                    CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
                FROM 
                    BiometricDevices
                ORDER BY 
                    DeviceName";

                var dataTable = _dbContext.ExecuteReader(query);
                List<BiometricDevice> devices = new List<BiometricDevice>();

                foreach (DataRow row in dataTable.Rows)
                {
                    devices.Add(new BiometricDevice
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        DeviceName = row["DeviceName"].ToString(),
                        DeviceModel = row["DeviceModel"] != DBNull.Value ? row["DeviceModel"].ToString() : null,
                        SerialNumber = row["SerialNumber"] != DBNull.Value ? row["SerialNumber"].ToString() : null,
                        IPAddress = row["IPAddress"].ToString(),
                        Port = Convert.ToInt32(row["Port"]),
                        CommunicationKey = row["CommunicationKey"] != DBNull.Value ? row["CommunicationKey"].ToString() : null,
                        Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                        Location = row["Location"] != DBNull.Value ? row["Location"].ToString() : null,
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        LastSyncTime = row["LastSyncTime"] != DBNull.Value ? (DateTime?)row["LastSyncTime"] : null,
                        LastSyncStatus = row["LastSyncStatus"] != DBNull.Value ? row["LastSyncStatus"].ToString() : null,
                        LastSyncErrors = row["LastSyncErrors"] != DBNull.Value ? row["LastSyncErrors"].ToString() : null,
                        CreatedAt = row["CreatedAt"] != DBNull.Value ? (DateTime?)row["CreatedAt"] : null,
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? (int?)row["CreatedBy"] : null,
                        UpdatedAt = row["UpdatedAt"] != DBNull.Value ? (DateTime?)row["UpdatedAt"] : null,
                        UpdatedBy = row["UpdatedBy"] != DBNull.Value ? (int?)row["UpdatedBy"] : null
                    });
                }

                return devices;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء جلب أجهزة البصمة: " + ex.Message);
            }
        }

        /// <summary>
        /// الحصول على جهاز بصمة محدد
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>بيانات الجهاز</returns>
        public BiometricDevice GetDeviceById(int deviceId)
        {
            try
            {
                string query = @"
                SELECT 
                    ID, DeviceName, DeviceModel, SerialNumber, IPAddress, Port, CommunicationKey,
                    Description, Location, IsActive, LastSyncTime, LastSyncStatus, LastSyncErrors,
                    CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
                FROM 
                    BiometricDevices
                WHERE 
                    ID = @ID";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@ID", deviceId)
                };

                var dataTable = _dbContext.ExecuteReader(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                var row = dataTable.Rows[0];
                return new BiometricDevice
                {
                    ID = Convert.ToInt32(row["ID"]),
                    DeviceName = row["DeviceName"].ToString(),
                    DeviceModel = row["DeviceModel"] != DBNull.Value ? row["DeviceModel"].ToString() : null,
                    SerialNumber = row["SerialNumber"] != DBNull.Value ? row["SerialNumber"].ToString() : null,
                    IPAddress = row["IPAddress"].ToString(),
                    Port = Convert.ToInt32(row["Port"]),
                    CommunicationKey = row["CommunicationKey"] != DBNull.Value ? row["CommunicationKey"].ToString() : null,
                    Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                    Location = row["Location"] != DBNull.Value ? row["Location"].ToString() : null,
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    LastSyncTime = row["LastSyncTime"] != DBNull.Value ? (DateTime?)row["LastSyncTime"] : null,
                    LastSyncStatus = row["LastSyncStatus"] != DBNull.Value ? row["LastSyncStatus"].ToString() : null,
                    LastSyncErrors = row["LastSyncErrors"] != DBNull.Value ? row["LastSyncErrors"].ToString() : null,
                    CreatedAt = row["CreatedAt"] != DBNull.Value ? (DateTime?)row["CreatedAt"] : null,
                    CreatedBy = row["CreatedBy"] != DBNull.Value ? (int?)row["CreatedBy"] : null,
                    UpdatedAt = row["UpdatedAt"] != DBNull.Value ? (DateTime?)row["UpdatedAt"] : null,
                    UpdatedBy = row["UpdatedBy"] != DBNull.Value ? (int?)row["UpdatedBy"] : null
                };
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء جلب بيانات جهاز البصمة: " + ex.Message);
            }
        }

        /// <summary>
        /// تحديث حالة مزامنة جهاز البصمة
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <param name="status">حالة المزامنة</param>
        /// <param name="errors">أخطاء المزامنة (إن وجدت)</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateSyncStatus(int deviceId, string status, string errors = null)
        {
            try
            {
                string query = @"
                UPDATE BiometricDevices SET
                    LastSyncTime = @LastSyncTime,
                    LastSyncStatus = @LastSyncStatus,
                    LastSyncErrors = @LastSyncErrors
                WHERE ID = @ID";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@ID", deviceId),
                    new SqlParameter("@LastSyncTime", DateTime.Now),
                    new SqlParameter("@LastSyncStatus", status),
                    new SqlParameter("@LastSyncErrors", errors ?? (object)DBNull.Value)
                };

                int affectedRows = _dbContext.ExecuteNonQuery(query, parameters);
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء تحديث حالة مزامنة جهاز البصمة: " + ex.Message);
            }
        }

        /// <summary>
        /// حفظ سجلات البصمة الخام
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <param name="logs">سجلات البصمة</param>
        /// <returns>عدد السجلات المضافة</returns>
        public int SaveRawAttendanceLogs(int deviceId, List<RawAttendanceLog> logs)
        {
            try
            {
                int addedCount = 0;

                _dbContext.ExecuteTransaction((connection, transaction) =>
                {
                    foreach (var log in logs)
                    {
                        // التحقق من عدم وجود السجل مسبقاً
                        string checkQuery = @"
                        SELECT COUNT(*) FROM RawAttendanceLogs 
                        WHERE DeviceID = @DeviceID AND BiometricUserID = @BiometricUserID AND LogDateTime = @LogDateTime";

                        SqlCommand checkCmd = new SqlCommand(checkQuery, connection, transaction);
                        checkCmd.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@DeviceID", deviceId),
                            new SqlParameter("@BiometricUserID", log.BiometricUserID),
                            new SqlParameter("@LogDateTime", log.LogDateTime)
                        });

                        int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (existingCount > 0)
                        {
                            continue; // تخطي السجل الموجود مسبقاً
                        }

                        // إضافة السجل الجديد
                        string insertQuery = @"
                        INSERT INTO RawAttendanceLogs (
                            DeviceID, BiometricUserID, LogDateTime, LogType, VerifyMode, WorkCode, 
                            IsProcessed, IsMatched, EmployeeID, SyncTime
                        ) VALUES (
                            @DeviceID, @BiometricUserID, @LogDateTime, @LogType, @VerifyMode, @WorkCode, 
                            @IsProcessed, @IsMatched, @EmployeeID, @SyncTime
                        )";

                        SqlCommand insertCmd = new SqlCommand(insertQuery, connection, transaction);
                        insertCmd.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@DeviceID", deviceId),
                            new SqlParameter("@BiometricUserID", log.BiometricUserID),
                            new SqlParameter("@LogDateTime", log.LogDateTime),
                            new SqlParameter("@LogType", log.LogType ?? (object)DBNull.Value),
                            new SqlParameter("@VerifyMode", log.VerifyMode ?? (object)DBNull.Value),
                            new SqlParameter("@WorkCode", log.WorkCode ?? (object)DBNull.Value),
                            new SqlParameter("@IsProcessed", false),
                            new SqlParameter("@IsMatched", false),
                            new SqlParameter("@EmployeeID", log.EmployeeID ?? (object)DBNull.Value),
                            new SqlParameter("@SyncTime", DateTime.Now)
                        });

                        insertCmd.ExecuteNonQuery();
                        addedCount++;
                    }

                    // محاولة مطابقة السجلات الجديدة مع الموظفين
                    MatchAttendanceLogsWithEmployees(connection, transaction, deviceId);
                });

                return addedCount;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء حفظ سجلات البصمة: " + ex.Message);
            }
        }

        /// <summary>
        /// مطابقة سجلات البصمة مع الموظفين
        /// </summary>
        private void MatchAttendanceLogsWithEmployees(SqlConnection connection, SqlTransaction transaction, int deviceId)
        {
            string query = @"
            UPDATE r
            SET r.EmployeeID = e.ID, r.IsMatched = 1
            FROM RawAttendanceLogs r
            INNER JOIN Employees e ON r.BiometricUserID = e.BiometricID
            WHERE r.DeviceID = @DeviceID AND r.IsMatched = 0";

            SqlCommand cmd = new SqlCommand(query, connection, transaction);
            cmd.Parameters.Add(new SqlParameter("@DeviceID", deviceId));
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// معالجة سجلات البصمة غير المعالجة
        /// </summary>
        /// <returns>عدد السجلات المعالجة</returns>
        public int ProcessAttendanceRecords()
        {
            try
            {
                int processedCount = 0;

                _dbContext.ExecuteTransaction((connection, transaction) =>
                {
                    // الحصول على السجلات غير المعالجة المطابقة للموظفين
                    string selectQuery = @"
                    SELECT 
                        r.ID, r.EmployeeID, r.BiometricUserID, r.LogDateTime, r.LogType, 
                        e.WorkShiftID, ws.WorkHoursID, wh.StartTime, wh.EndTime
                    FROM 
                        RawAttendanceLogs r
                    INNER JOIN 
                        Employees e ON r.EmployeeID = e.ID
                    LEFT JOIN 
                        WorkShifts ws ON e.WorkShiftID = ws.ID
                    LEFT JOIN 
                        WorkHours wh ON ws.WorkHoursID = wh.ID
                    WHERE 
                        r.IsProcessed = 0 AND r.IsMatched = 1
                    ORDER BY 
                        r.EmployeeID, r.LogDateTime";

                    SqlCommand selectCmd = new SqlCommand(selectQuery, connection, transaction);
                    
                    Dictionary<int, List<DateTime>> employeeLogs = new Dictionary<int, List<DateTime>>();
                    Dictionary<int, int> employeeWorkHours = new Dictionary<int, int>();
                    Dictionary<int, TimeSpan> employeeStartTime = new Dictionary<int, TimeSpan>();
                    Dictionary<int, TimeSpan> employeeEndTime = new Dictionary<int, TimeSpan>();

                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int rawLogId = reader.GetInt32(reader.GetOrdinal("ID"));
                            int employeeId = reader.GetInt32(reader.GetOrdinal("EmployeeID"));
                            DateTime logDateTime = reader.GetDateTime(reader.GetOrdinal("LogDateTime"));
                            
                            // جمع سجلات كل موظف حسب التاريخ
                            if (!employeeLogs.ContainsKey(employeeId))
                            {
                                employeeLogs.Add(employeeId, new List<DateTime>());
                                
                                // حفظ معلومات المناوبة لكل موظف
                                if (!reader.IsDBNull(reader.GetOrdinal("WorkHoursID")))
                                {
                                    int workHoursId = reader.GetInt32(reader.GetOrdinal("WorkHoursID"));
                                    TimeSpan startTime = reader.GetTimeSpan(reader.GetOrdinal("StartTime"));
                                    TimeSpan endTime = reader.GetTimeSpan(reader.GetOrdinal("EndTime"));
                                    
                                    employeeWorkHours.Add(employeeId, workHoursId);
                                    employeeStartTime.Add(employeeId, startTime);
                                    employeeEndTime.Add(employeeId, endTime);
                                }
                            }
                            
                            employeeLogs[employeeId].Add(logDateTime);
                        }
                    }

                    // معالجة سجلات الحضور لكل موظف
                    foreach (var entry in employeeLogs)
                    {
                        int employeeId = entry.Key;
                        var logs = entry.Value;
                        
                        // تجميع السجلات حسب التاريخ
                        var logsByDate = new Dictionary<DateTime, List<DateTime>>();
                        
                        foreach (var log in logs)
                        {
                            DateTime dateOnly = log.Date;
                            if (!logsByDate.ContainsKey(dateOnly))
                            {
                                logsByDate.Add(dateOnly, new List<DateTime>());
                            }
                            
                            logsByDate[dateOnly].Add(log);
                        }
                        
                        // معالجة سجلات كل يوم
                        foreach (var dailyLogs in logsByDate)
                        {
                            DateTime dateOnly = dailyLogs.Key;
                            var dateLogs = dailyLogs.Value.OrderBy(l => l).ToList();
                            
                            // الحصول على أول سجل (تسجيل الدخول) وآخر سجل (تسجيل الخروج)
                            DateTime? timeIn = dateLogs.Count > 0 ? dateLogs[0] : (DateTime?)null;
                            DateTime? timeOut = dateLogs.Count > 1 ? dateLogs[dateLogs.Count - 1] : (DateTime?)null;
                            
                            int workHoursId = 0;
                            int lateMinutes = 0;
                            int earlyDepartureMinutes = 0;
                            int overtimeMinutes = 0;
                            int workedMinutes = 0;
                            string status = "Present";
                            
                            // حساب مدة العمل والتأخير
                            if (timeIn.HasValue && timeOut.HasValue)
                            {
                                // حساب مدة العمل
                                workedMinutes = (int)(timeOut.Value - timeIn.Value).TotalMinutes;
                                
                                // حساب التأخير والمغادرة المبكرة إذا كانت المناوبة معروفة
                                if (employeeWorkHours.ContainsKey(employeeId))
                                {
                                    workHoursId = employeeWorkHours[employeeId];
                                    TimeSpan startTime = employeeStartTime[employeeId];
                                    TimeSpan endTime = employeeEndTime[employeeId];
                                    
                                    // حساب التأخير
                                    TimeSpan timeInTime = timeIn.Value.TimeOfDay;
                                    if (timeInTime > startTime)
                                    {
                                        lateMinutes = (int)(timeInTime - startTime).TotalMinutes;
                                        if (lateMinutes > 0)
                                        {
                                            status = "Late";
                                        }
                                    }
                                    
                                    // حساب المغادرة المبكرة
                                    TimeSpan timeOutTime = timeOut.Value.TimeOfDay;
                                    if (timeOutTime < endTime)
                                    {
                                        earlyDepartureMinutes = (int)(endTime - timeOutTime).TotalMinutes;
                                        if (earlyDepartureMinutes > 0 && status != "Late")
                                        {
                                            status = "EarlyDeparture";
                                        }
                                    }
                                    
                                    // حساب العمل الإضافي
                                    if (timeOutTime > endTime)
                                    {
                                        overtimeMinutes = (int)(timeOutTime - endTime).TotalMinutes;
                                    }
                                }
                            }
                            else if (timeIn.HasValue && !timeOut.HasValue)
                            {
                                status = "InOnly"; // تسجيل دخول فقط
                            }
                            
                            // التحقق من وجود سجل للتاريخ نفسه للموظف
                            string checkAttendanceQuery = @"
                            SELECT ID FROM AttendanceRecords 
                            WHERE EmployeeID = @EmployeeID AND AttendanceDate = @AttendanceDate";
                            
                            SqlCommand checkAttendanceCmd = new SqlCommand(checkAttendanceQuery, connection, transaction);
                            checkAttendanceCmd.Parameters.AddRange(new SqlParameter[]
                            {
                                new SqlParameter("@EmployeeID", employeeId),
                                new SqlParameter("@AttendanceDate", dateOnly)
                            });
                            
                            object existingId = checkAttendanceCmd.ExecuteScalar();
                            
                            if (existingId != null) // تحديث السجل الموجود
                            {
                                string updateAttendanceQuery = @"
                                UPDATE AttendanceRecords SET
                                    TimeIn = @TimeIn,
                                    TimeOut = @TimeOut,
                                    WorkHoursID = @WorkHoursID,
                                    LateMinutes = @LateMinutes,
                                    EarlyDepartureMinutes = @EarlyDepartureMinutes,
                                    OvertimeMinutes = @OvertimeMinutes,
                                    WorkedMinutes = @WorkedMinutes,
                                    Status = @Status,
                                    UpdatedAt = @UpdatedAt,
                                    UpdatedBy = @UpdatedBy
                                WHERE ID = @ID";
                                
                                SqlCommand updateAttendanceCmd = new SqlCommand(updateAttendanceQuery, connection, transaction);
                                updateAttendanceCmd.Parameters.AddRange(new SqlParameter[]
                                {
                                    new SqlParameter("@ID", existingId),
                                    new SqlParameter("@TimeIn", timeIn ?? (object)DBNull.Value),
                                    new SqlParameter("@TimeOut", timeOut ?? (object)DBNull.Value),
                                    new SqlParameter("@WorkHoursID", workHoursId > 0 ? (object)workHoursId : DBNull.Value),
                                    new SqlParameter("@LateMinutes", lateMinutes),
                                    new SqlParameter("@EarlyDepartureMinutes", earlyDepartureMinutes),
                                    new SqlParameter("@OvertimeMinutes", overtimeMinutes),
                                    new SqlParameter("@WorkedMinutes", workedMinutes),
                                    new SqlParameter("@Status", status),
                                    new SqlParameter("@UpdatedAt", DateTime.Now),
                                    new SqlParameter("@UpdatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                                });
                                
                                updateAttendanceCmd.ExecuteNonQuery();
                            }
                            else // إضافة سجل جديد
                            {
                                string insertAttendanceQuery = @"
                                INSERT INTO AttendanceRecords (
                                    EmployeeID, AttendanceDate, TimeIn, TimeOut, WorkHoursID,
                                    LateMinutes, EarlyDepartureMinutes, OvertimeMinutes, WorkedMinutes,
                                    Status, IsManualEntry, CreatedAt, CreatedBy
                                ) VALUES (
                                    @EmployeeID, @AttendanceDate, @TimeIn, @TimeOut, @WorkHoursID,
                                    @LateMinutes, @EarlyDepartureMinutes, @OvertimeMinutes, @WorkedMinutes,
                                    @Status, @IsManualEntry, @CreatedAt, @CreatedBy
                                )";
                                
                                SqlCommand insertAttendanceCmd = new SqlCommand(insertAttendanceQuery, connection, transaction);
                                insertAttendanceCmd.Parameters.AddRange(new SqlParameter[]
                                {
                                    new SqlParameter("@EmployeeID", employeeId),
                                    new SqlParameter("@AttendanceDate", dateOnly),
                                    new SqlParameter("@TimeIn", timeIn ?? (object)DBNull.Value),
                                    new SqlParameter("@TimeOut", timeOut ?? (object)DBNull.Value),
                                    new SqlParameter("@WorkHoursID", workHoursId > 0 ? (object)workHoursId : DBNull.Value),
                                    new SqlParameter("@LateMinutes", lateMinutes),
                                    new SqlParameter("@EarlyDepartureMinutes", earlyDepartureMinutes),
                                    new SqlParameter("@OvertimeMinutes", overtimeMinutes),
                                    new SqlParameter("@WorkedMinutes", workedMinutes),
                                    new SqlParameter("@Status", status),
                                    new SqlParameter("@IsManualEntry", false),
                                    new SqlParameter("@CreatedAt", DateTime.Now),
                                    new SqlParameter("@CreatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                                });
                                
                                insertAttendanceCmd.ExecuteNonQuery();
                            }
                            
                            processedCount++;
                        }
                    }
                    
                    // تحديث حالة السجلات الخام إلى معالجة
                    string updateProcessedQuery = @"
                    UPDATE RawAttendanceLogs
                    SET IsProcessed = 1
                    WHERE IsMatched = 1 AND IsProcessed = 0";
                    
                    SqlCommand updateProcessedCmd = new SqlCommand(updateProcessedQuery, connection, transaction);
                    updateProcessedCmd.ExecuteNonQuery();
                });

                return processedCount;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ أثناء معالجة سجلات الحضور: " + ex.Message);
            }
        }

        /// <summary>
        /// وصل جهاز البصمة ZKTeco
        /// يجب استبدال هذه الدالة بالتنفيذ الفعلي للاتصال بجهاز ZKTeco
        /// </summary>
        /// <param name="device">بيانات الجهاز</param>
        /// <returns>نجاح الاتصال</returns>
        public bool ConnectDevice(BiometricDevice device)
        {
            // هذه الدالة نموذجية للتوضيح فقط
            // في التطبيق الفعلي، يجب استخدام مكتبة ZKTeco SDK للاتصال بالجهاز
            return TestConnection(device.IPAddress, device.Port);
        }

        /// <summary>
        /// الحصول على سجلات الحضور من جهاز البصمة ZKTeco
        /// يجب استبدال هذه الدالة بالتنفيذ الفعلي لاستيراد البيانات من جهاز ZKTeco
        /// </summary>
        /// <param name="device">بيانات الجهاز</param>
        /// <returns>قائمة سجلات الحضور</returns>
        public List<RawAttendanceLog> GetAttendanceLogs(BiometricDevice device)
        {
            // هذه الدالة نموذجية للتوضيح فقط
            // في التطبيق الفعلي، يجب استخدام مكتبة ZKTeco SDK للحصول على سجلات الحضور
            // والتي يتم تنفيذها مع ZKTeco C# SDK

            // إرجاع قائمة فارغة كمؤشر
            return new List<RawAttendanceLog>();
        }

        /// <summary>
        /// مزامنة بيانات الموظفين مع جهاز البصمة ZKTeco
        /// يجب استبدال هذه الدالة بالتنفيذ الفعلي لتحميل بيانات الموظفين إلى جهاز ZKTeco
        /// </summary>
        /// <param name="device">بيانات الجهاز</param>
        /// <param name="employees">قائمة الموظفين</param>
        /// <returns>نجاح المزامنة</returns>
        public bool SyncEmployeesToDevice(BiometricDevice device, List<Employee> employees)
        {
            // هذه الدالة نموذجية للتوضيح فقط
            // في التطبيق الفعلي، يجب استخدام مكتبة ZKTeco SDK لتحميل بيانات الموظفين
            return true;
        }

        /// <summary>
        /// تنفيذ مزامنة كاملة بين جهاز البصمة والنظام
        /// </summary>
        /// <param name="deviceId">معرف الجهاز</param>
        /// <returns>نتيجة المزامنة</returns>
        public string SynchronizeDevice(int deviceId)
        {
            try
            {
                // الحصول على بيانات الجهاز
                var device = GetDeviceById(deviceId);
                if (device == null)
                {
                    return "فشل المزامنة: الجهاز غير موجود";
                }

                // اختبار الاتصال بالجهاز
                if (!ConnectDevice(device))
                {
                    UpdateSyncStatus(deviceId, "Failed", "فشل الاتصال بالجهاز");
                    return "فشل المزامنة: لا يمكن الاتصال بالجهاز";
                }

                // الحصول على سجلات الحضور
                var logs = GetAttendanceLogs(device);

                // حفظ سجلات الحضور
                int addedCount = SaveRawAttendanceLogs(deviceId, logs);

                // معالجة سجلات الحضور
                int processedCount = ProcessAttendanceRecords();

                // تحديث حالة المزامنة
                UpdateSyncStatus(deviceId, "Success");

                return $"تمت المزامنة بنجاح. تم إضافة {addedCount} سجل، ومعالجة {processedCount} سجل.";
            }
            catch (Exception ex)
            {
                UpdateSyncStatus(deviceId, "Failed", ex.Message);
                return "فشل المزامنة: " + ex.Message;
            }
        }
    }
}