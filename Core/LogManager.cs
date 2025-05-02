using System;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace HR.Core
{
    /// <summary>
    /// Manages application logging
    /// </summary>
    public static class LogManager
    {
        private static string _logFilePath;
        private static bool _enableDebugLogging;
        private static object _lockObject = new object();

        /// <summary>
        /// Initializes the log manager
        /// </summary>
        static LogManager()
        {
            try
            {
                _logFilePath = ConfigurationManager.AppSettings["LogFilePath"] ?? "Logs";
                _enableDebugLogging = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableDebugLogging"] ?? "false");

                // Make sure log directory exists
                if (!Directory.Exists(_logFilePath))
                {
                    Directory.CreateDirectory(_logFilePath);
                }
            }
            catch
            {
                // Use default values if configuration failed
                _logFilePath = "Logs";
                _enableDebugLogging = false;

                if (!Directory.Exists(_logFilePath))
                {
                    Directory.CreateDirectory(_logFilePath);
                }
            }
        }

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <param name="additionalInfo">Additional information about the exception</param>
        public static void LogException(Exception ex, string additionalInfo = null)
        {
            try
            {
                string logFileName = Path.Combine(_logFilePath, $"error_{DateTime.Now:yyyyMMdd}.log");
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] EXCEPTION: {ex.Message}\r\n";

                if (!string.IsNullOrEmpty(additionalInfo))
                {
                    logEntry += $"Additional Info: {additionalInfo}\r\n";
                }

                logEntry += $"Source: {ex.Source}\r\n";
                logEntry += $"Stack Trace: {ex.StackTrace}\r\n";

                if (ex.InnerException != null)
                {
                    logEntry += $"Inner Exception: {ex.InnerException.Message}\r\n";
                    logEntry += $"Inner Stack Trace: {ex.InnerException.StackTrace}\r\n";
                }

                logEntry += new string('-', 80) + "\r\n";

                lock (_lockObject)
                {
                    File.AppendAllText(logFileName, logEntry);
                }

                // Also log to console in debug mode
                Debug.WriteLine(logEntry);
                Console.WriteLine(logEntry);
            }
            catch
            {
                // If logging fails, we can't do much
                Debug.WriteLine($"Failed to log exception: {ex.Message}");
                Console.WriteLine($"Failed to log exception: {ex.Message}");
            }
        }

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void LogInfo(string message)
        {
            try
            {
                string logFileName = Path.Combine(_logFilePath, $"info_{DateTime.Now:yyyyMMdd}.log");
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] INFO: {message}\r\n";

                lock (_lockObject)
                {
                    File.AppendAllText(logFileName, logEntry);
                }

                // Also log to console in debug mode
                Debug.WriteLine(logEntry);
                Console.WriteLine(logEntry);
            }
            catch
            {
                // If logging fails, we can't do much
                Debug.WriteLine($"Failed to log info: {message}");
                Console.WriteLine($"Failed to log info: {message}");
            }
        }

        /// <summary>
        /// Logs a debug message, only if debug logging is enabled
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void LogDebug(string message)
        {
            if (!_enableDebugLogging)
            {
                return;
            }

            try
            {
                string logFileName = Path.Combine(_logFilePath, $"debug_{DateTime.Now:yyyyMMdd}.log");
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] DEBUG: {message}\r\n";

                lock (_lockObject)
                {
                    File.AppendAllText(logFileName, logEntry);
                }

                // Also log to console in debug mode
                Debug.WriteLine(logEntry);
                Console.WriteLine(logEntry);
            }
            catch
            {
                // If logging fails, we can't do much
                Debug.WriteLine($"Failed to log debug: {message}");
                Console.WriteLine($"Failed to log debug: {message}");
            }
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void LogWarning(string message)
        {
            try
            {
                string logFileName = Path.Combine(_logFilePath, $"warning_{DateTime.Now:yyyyMMdd}.log");
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] WARNING: {message}\r\n";

                lock (_lockObject)
                {
                    File.AppendAllText(logFileName, logEntry);
                }

                // Also log to console in debug mode
                Debug.WriteLine(logEntry);
                Console.WriteLine(logEntry);
            }
            catch
            {
                // If logging fails, we can't do much
                Debug.WriteLine($"Failed to log warning: {message}");
                Console.WriteLine($"Failed to log warning: {message}");
            }
        }

        /// <summary>
        /// Logs a database operation
        /// </summary>
        /// <param name="operation">Database operation description</param>
        /// <param name="details">Operation details</param>
        public static void LogDatabaseOperation(string operation, string details)
        {
            try
            {
                string logFileName = Path.Combine(_logFilePath, $"database_{DateTime.Now:yyyyMMdd}.log");
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] DB OPERATION: {operation}\r\n";
                logEntry += $"Details: {details}\r\n";
                logEntry += new string('-', 80) + "\r\n";

                lock (_lockObject)
                {
                    File.AppendAllText(logFileName, logEntry);
                }

                // Also log to console in debug mode
                if (_enableDebugLogging)
                {
                    Debug.WriteLine(logEntry);
                    Console.WriteLine(logEntry);
                }
            }
            catch
            {
                // If logging fails, we can't do much
                Debug.WriteLine($"Failed to log database operation: {operation}");
                Console.WriteLine($"Failed to log database operation: {operation}");
            }
        }
    }
}