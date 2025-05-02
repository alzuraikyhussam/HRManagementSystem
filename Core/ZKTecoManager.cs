using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using HR.Models;

namespace HR.Core
{
    /// <summary>
    /// Manages communication with ZKTeco fingerprint devices
    /// </summary>
    public class ZKTecoManager : IDisposable
    {
        #region Fields and Properties

        private IntPtr _deviceHandle = IntPtr.Zero;
        private bool _isConnected = false;
        private bool _disposed = false;
        private readonly object _lockObject = new object();
        private readonly BiometricDeviceDTO _deviceInfo;
        private Timer _syncTimer;
        private readonly int _syncIntervalMinutes;

        /// <summary>
        /// Gets a value indicating whether the device is connected
        /// </summary>
        public bool IsConnected
        {
            get { return _isConnected; }
        }

        /// <summary>
        /// Gets the device information
        /// </summary>
        public BiometricDeviceDTO DeviceInfo
        {
            get { return _deviceInfo; }
        }

        #endregion

        #region Constructor and Initialization

        /// <summary>
        /// Initializes a new instance of the ZKTecoManager class
        /// </summary>
        /// <param name="deviceInfo">Biometric device information</param>
        public ZKTecoManager(BiometricDeviceDTO deviceInfo)
        {
            _deviceInfo = deviceInfo ?? throw new ArgumentNullException(nameof(deviceInfo));
            
            // Get sync interval from configuration
            string intervalStr = ConfigurationManager.AppSettings["BiometricSyncInterval"] ?? "5";
            if (!int.TryParse(intervalStr, out _syncIntervalMinutes) || _syncIntervalMinutes < 1)
            {
                _syncIntervalMinutes = 5; // Default to 5 minutes
            }
            
            // Log manager initialization
            LogManager.LogInfo($"ZKTeco manager initialized for device {deviceInfo.DeviceName} ({deviceInfo.IPAddress})");
        }

        #endregion

        #region Connection Methods

        /// <summary>
        /// Connects to the ZKTeco device
        /// </summary>
        /// <returns>True if the connection was successful</returns>
        public bool Connect()
        {
            lock (_lockObject)
            {
                if (_isConnected)
                {
                    return true;
                }

                try
                {
                    LogManager.LogInfo($"Connecting to ZKTeco device {_deviceInfo.DeviceName} ({_deviceInfo.IPAddress})...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to connect to the device
                    // For now, we'll just simulate a successful connection
                    _deviceHandle = new IntPtr(1); // Simulate a valid device handle
                    _isConnected = true;
                    
                    // Start the sync timer
                    StartSyncTimer();
                    
                    LogManager.LogInfo($"Successfully connected to ZKTeco device {_deviceInfo.DeviceName} ({_deviceInfo.IPAddress})");
                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to connect to ZKTeco device {_deviceInfo.DeviceName} ({_deviceInfo.IPAddress})");
                    _deviceHandle = IntPtr.Zero;
                    _isConnected = false;
                    return false;
                }
            }
        }

        /// <summary>
        /// Disconnects from the ZKTeco device
        /// </summary>
        public void Disconnect()
        {
            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    return;
                }

                try
                {
                    // Stop the sync timer
                    StopSyncTimer();
                    
                    // In a real implementation, we would use the ZKTeco SDK to disconnect from the device
                    // For now, we'll just simulate a successful disconnection
                    _deviceHandle = IntPtr.Zero;
                    _isConnected = false;
                    
                    LogManager.LogInfo($"Disconnected from ZKTeco device {_deviceInfo.DeviceName} ({_deviceInfo.IPAddress})");
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to disconnect from ZKTeco device {_deviceInfo.DeviceName} ({_deviceInfo.IPAddress})");
                }
            }
        }

        #endregion

        #region Synchronization Methods

        /// <summary>
        /// Starts the sync timer
        /// </summary>
        private void StartSyncTimer()
        {
            if (_syncTimer != null)
            {
                StopSyncTimer();
            }
            
            _syncTimer = new Timer(SyncTimerCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(_syncIntervalMinutes));
            LogManager.LogInfo($"ZKTeco sync timer started with interval {_syncIntervalMinutes} minutes");
        }

        /// <summary>
        /// Stops the sync timer
        /// </summary>
        private void StopSyncTimer()
        {
            if (_syncTimer != null)
            {
                _syncTimer.Dispose();
                _syncTimer = null;
                LogManager.LogInfo("ZKTeco sync timer stopped");
            }
        }

        /// <summary>
        /// Timer callback method for syncing attendance data
        /// </summary>
        private void SyncTimerCallback(object state)
        {
            try
            {
                SynchronizeAttendanceLogs();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to synchronize attendance logs in timer callback");
            }
        }

        /// <summary>
        /// Synchronizes attendance logs from the device to the database
        /// </summary>
        /// <returns>Number of new logs synchronized</returns>
        public int SynchronizeAttendanceLogs()
        {
            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot synchronize attendance logs: Device {_deviceInfo.DeviceName} is not connected");
                    return 0;
                }

                try
                {
                    LogManager.LogInfo($"Synchronizing attendance logs from device {_deviceInfo.DeviceName}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to get attendance logs from the device
                    // For now, we'll just simulate getting some attendance logs
                    
                    // Get the last sync time for this device
                    DateTime lastSyncTime = _deviceInfo.LastSyncTime ?? DateTime.MinValue;
                    
                    // Get attendance logs since the last sync time
                    List<ZKAttendanceDTO> attendanceLogs = GetAttendanceLogs(lastSyncTime);
                    
                    if (attendanceLogs.Count == 0)
                    {
                        LogManager.LogInfo($"No new attendance logs found on device {_deviceInfo.DeviceName}");
                        return 0;
                    }
                    
                    // Save the attendance logs to the database
                    int count = SaveAttendanceLogs(attendanceLogs);
                    
                    // Update the device's last sync time and status
                    UpdateDeviceSyncStatus(DateTime.Now, "Success", null);
                    
                    LogManager.LogInfo($"Successfully synchronized {count} attendance logs from device {_deviceInfo.DeviceName}");
                    return count;
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to synchronize attendance logs from device {_deviceInfo.DeviceName}");
                    
                    // Update the device's sync status to reflect the error
                    UpdateDeviceSyncStatus(DateTime.Now, "Failed", ex.Message);
                    
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets attendance logs from the device
        /// </summary>
        /// <param name="lastSyncTime">Last sync time</param>
        /// <returns>List of attendance logs</returns>
        private List<ZKAttendanceDTO> GetAttendanceLogs(DateTime lastSyncTime)
        {
            // In a real implementation, we would use the ZKTeco SDK to get attendance logs from the device
            // For now, we'll just return an empty list
            return new List<ZKAttendanceDTO>();
        }

        /// <summary>
        /// Saves attendance logs to the database
        /// </summary>
        /// <param name="attendanceLogs">List of attendance logs</param>
        /// <returns>Number of logs saved</returns>
        private int SaveAttendanceLogs(List<ZKAttendanceDTO> attendanceLogs)
        {
            if (attendanceLogs == null || attendanceLogs.Count == 0)
            {
                return 0;
            }

            try
            {
                int count = 0;
                
                // In a real implementation, we would save the attendance logs to the database
                // For now, we'll just return the count of logs
                count = attendanceLogs.Count;
                
                return count;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to save attendance logs to the database");
                throw;
            }
        }

        /// <summary>
        /// Updates the device's sync status in the database
        /// </summary>
        /// <param name="syncTime">Sync time</param>
        /// <param name="status">Sync status</param>
        /// <param name="errors">Sync errors</param>
        private void UpdateDeviceSyncStatus(DateTime syncTime, string status, string errors)
        {
            try
            {
                // In a real implementation, we would update the device's sync status in the database
                // For now, we'll just log the status
                LogManager.LogInfo($"Device {_deviceInfo.DeviceName} sync status updated: {status}");
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to update device sync status in the database");
            }
        }

        #endregion

        #region User Management Methods

        /// <summary>
        /// Gets all users from the device
        /// </summary>
        /// <returns>List of ZKTeco users</returns>
        public List<ZKUserDTO> GetAllUsers()
        {
            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot get users: Device {_deviceInfo.DeviceName} is not connected");
                    return new List<ZKUserDTO>();
                }

                try
                {
                    LogManager.LogInfo($"Getting users from device {_deviceInfo.DeviceName}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to get users from the device
                    // For now, we'll just return an empty list
                    return new List<ZKUserDTO>();
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to get users from device {_deviceInfo.DeviceName}");
                    return new List<ZKUserDTO>();
                }
            }
        }

        /// <summary>
        /// Adds a user to the device
        /// </summary>
        /// <param name="user">ZKTeco user information</param>
        /// <returns>True if the user was added successfully</returns>
        public bool AddUser(ZKUserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot add user: Device {_deviceInfo.DeviceName} is not connected");
                    return false;
                }

                try
                {
                    LogManager.LogInfo($"Adding user {user.Name} (ID: {user.UserID}) to device {_deviceInfo.DeviceName}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to add the user to the device
                    // For now, we'll just simulate a successful operation
                    
                    LogManager.LogInfo($"Successfully added user {user.Name} (ID: {user.UserID}) to device {_deviceInfo.DeviceName}");
                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to add user {user.Name} (ID: {user.UserID}) to device {_deviceInfo.DeviceName}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Updates a user on the device
        /// </summary>
        /// <param name="user">ZKTeco user information</param>
        /// <returns>True if the user was updated successfully</returns>
        public bool UpdateUser(ZKUserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot update user: Device {_deviceInfo.DeviceName} is not connected");
                    return false;
                }

                try
                {
                    LogManager.LogInfo($"Updating user {user.Name} (ID: {user.UserID}) on device {_deviceInfo.DeviceName}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to update the user on the device
                    // For now, we'll just simulate a successful operation
                    
                    LogManager.LogInfo($"Successfully updated user {user.Name} (ID: {user.UserID}) on device {_deviceInfo.DeviceName}");
                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to update user {user.Name} (ID: {user.UserID}) on device {_deviceInfo.DeviceName}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Deletes a user from the device
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>True if the user was deleted successfully</returns>
        public bool DeleteUser(int userID)
        {
            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot delete user: Device {_deviceInfo.DeviceName} is not connected");
                    return false;
                }

                try
                {
                    LogManager.LogInfo($"Deleting user with ID {userID} from device {_deviceInfo.DeviceName}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to delete the user from the device
                    // For now, we'll just simulate a successful operation
                    
                    LogManager.LogInfo($"Successfully deleted user with ID {userID} from device {_deviceInfo.DeviceName}");
                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to delete user with ID {userID} from device {_deviceInfo.DeviceName}");
                    return false;
                }
            }
        }

        #endregion

        #region Template Management Methods

        /// <summary>
        /// Gets all templates (fingerprints) from the device
        /// </summary>
        /// <returns>List of ZKTeco templates</returns>
        public List<ZKTemplateDTO> GetAllTemplates()
        {
            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot get templates: Device {_deviceInfo.DeviceName} is not connected");
                    return new List<ZKTemplateDTO>();
                }

                try
                {
                    LogManager.LogInfo($"Getting templates from device {_deviceInfo.DeviceName}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to get templates from the device
                    // For now, we'll just return an empty list
                    return new List<ZKTemplateDTO>();
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to get templates from device {_deviceInfo.DeviceName}");
                    return new List<ZKTemplateDTO>();
                }
            }
        }

        /// <summary>
        /// Gets templates for a specific user from the device
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>List of ZKTeco templates</returns>
        public List<ZKTemplateDTO> GetUserTemplates(int userID)
        {
            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot get user templates: Device {_deviceInfo.DeviceName} is not connected");
                    return new List<ZKTemplateDTO>();
                }

                try
                {
                    LogManager.LogInfo($"Getting templates for user with ID {userID} from device {_deviceInfo.DeviceName}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to get templates for the user from the device
                    // For now, we'll just return an empty list
                    return new List<ZKTemplateDTO>();
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to get templates for user with ID {userID} from device {_deviceInfo.DeviceName}");
                    return new List<ZKTemplateDTO>();
                }
            }
        }

        /// <summary>
        /// Adds a template to the device
        /// </summary>
        /// <param name="template">ZKTeco template information</param>
        /// <returns>True if the template was added successfully</returns>
        public bool AddTemplate(ZKTemplateDTO template)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot add template: Device {_deviceInfo.DeviceName} is not connected");
                    return false;
                }

                try
                {
                    LogManager.LogInfo($"Adding template for user with ID {template.UserID} to device {_deviceInfo.DeviceName}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to add the template to the device
                    // For now, we'll just simulate a successful operation
                    
                    LogManager.LogInfo($"Successfully added template for user with ID {template.UserID} to device {_deviceInfo.DeviceName}");
                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to add template for user with ID {template.UserID} to device {_deviceInfo.DeviceName}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Deletes a template from the device
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="fingerIndex">Finger index</param>
        /// <returns>True if the template was deleted successfully</returns>
        public bool DeleteTemplate(int userID, int fingerIndex)
        {
            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot delete template: Device {_deviceInfo.DeviceName} is not connected");
                    return false;
                }

                try
                {
                    LogManager.LogInfo($"Deleting template for user with ID {userID}, finger index {fingerIndex} from device {_deviceInfo.DeviceName}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to delete the template from the device
                    // For now, we'll just simulate a successful operation
                    
                    LogManager.LogInfo($"Successfully deleted template for user with ID {userID}, finger index {fingerIndex} from device {_deviceInfo.DeviceName}");
                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to delete template for user with ID {userID}, finger index {fingerIndex} from device {_deviceInfo.DeviceName}");
                    return false;
                }
            }
        }

        #endregion

        #region Device Management Methods

        /// <summary>
        /// Gets the device's time
        /// </summary>
        /// <returns>Device time</returns>
        public DateTime GetDeviceTime()
        {
            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot get device time: Device {_deviceInfo.DeviceName} is not connected");
                    return DateTime.MinValue;
                }

                try
                {
                    LogManager.LogInfo($"Getting device time from {_deviceInfo.DeviceName}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to get the device time
                    // For now, we'll just return the current time
                    DateTime deviceTime = DateTime.Now;
                    
                    LogManager.LogInfo($"Device {_deviceInfo.DeviceName} time: {deviceTime}");
                    return deviceTime;
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to get device time from {_deviceInfo.DeviceName}");
                    return DateTime.MinValue;
                }
            }
        }

        /// <summary>
        /// Sets the device's time
        /// </summary>
        /// <param name="dateTime">Date and time to set</param>
        /// <returns>True if the time was set successfully</returns>
        public bool SetDeviceTime(DateTime dateTime)
        {
            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot set device time: Device {_deviceInfo.DeviceName} is not connected");
                    return false;
                }

                try
                {
                    LogManager.LogInfo($"Setting device time for {_deviceInfo.DeviceName} to {dateTime}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to set the device time
                    // For now, we'll just simulate a successful operation
                    
                    LogManager.LogInfo($"Successfully set device time for {_deviceInfo.DeviceName} to {dateTime}");
                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to set device time for {_deviceInfo.DeviceName} to {dateTime}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the device's status
        /// </summary>
        /// <returns>Device status information</returns>
        public ZKDeviceStatusDTO GetDeviceStatus()
        {
            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot get device status: Device {_deviceInfo.DeviceName} is not connected");
                    return new ZKDeviceStatusDTO { IsConnected = false };
                }

                try
                {
                    LogManager.LogInfo($"Getting device status from {_deviceInfo.DeviceName}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to get the device status
                    // For now, we'll just return a simulated status
                    ZKDeviceStatusDTO status = new ZKDeviceStatusDTO
                    {
                        IsConnected = true,
                        SerialNumber = _deviceInfo.SerialNumber,
                        DeviceModel = _deviceInfo.DeviceModel,
                        Firmware = "1.0.0",
                        UserCount = 0,
                        TemplateCount = 0,
                        AttendanceCount = 0,
                        DeviceTime = DateTime.Now
                    };
                    
                    return status;
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to get device status from {_deviceInfo.DeviceName}");
                    return new ZKDeviceStatusDTO { IsConnected = false };
                }
            }
        }

        /// <summary>
        /// Restarts the device
        /// </summary>
        /// <returns>True if the device was restarted successfully</returns>
        public bool RestartDevice()
        {
            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot restart device: Device {_deviceInfo.DeviceName} is not connected");
                    return false;
                }

                try
                {
                    LogManager.LogInfo($"Restarting device {_deviceInfo.DeviceName}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to restart the device
                    // For now, we'll just simulate a successful operation
                    
                    // Disconnect from the device (it will be restarting)
                    Disconnect();
                    
                    // Wait for the device to restart (this would be a real delay in a real implementation)
                    Thread.Sleep(1000);
                    
                    // Attempt to reconnect
                    bool result = Connect();
                    
                    if (result)
                    {
                        LogManager.LogInfo($"Successfully restarted device {_deviceInfo.DeviceName}");
                    }
                    else
                    {
                        LogManager.LogWarning($"Device {_deviceInfo.DeviceName} was restarted, but reconnection failed");
                    }
                    
                    return result;
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to restart device {_deviceInfo.DeviceName}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Clears all attendance logs from the device
        /// </summary>
        /// <returns>True if the logs were cleared successfully</returns>
        public bool ClearAttendanceLogs()
        {
            lock (_lockObject)
            {
                if (!_isConnected)
                {
                    LogManager.LogWarning($"Cannot clear attendance logs: Device {_deviceInfo.DeviceName} is not connected");
                    return false;
                }

                try
                {
                    LogManager.LogInfo($"Clearing attendance logs from device {_deviceInfo.DeviceName}...");
                    
                    // In a real implementation, we would use the ZKTeco SDK to clear attendance logs from the device
                    // For now, we'll just simulate a successful operation
                    
                    LogManager.LogInfo($"Successfully cleared attendance logs from device {_deviceInfo.DeviceName}");
                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"Failed to clear attendance logs from device {_deviceInfo.DeviceName}");
                    return false;
                }
            }
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Disposes the ZKTeco manager
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the ZKTeco manager
        /// </summary>
        /// <param name="disposing">True if disposing, false if finalizing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed resources
                StopSyncTimer();
                Disconnect();
            }

            // Dispose unmanaged resources
            _disposed = true;
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~ZKTecoManager()
        {
            Dispose(false);
        }

        #endregion
    }
}