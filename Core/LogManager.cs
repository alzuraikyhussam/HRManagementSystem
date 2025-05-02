using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace HR.Core
{
    /// <summary>
    /// مدير سجلات النظام
    /// </summary>
    public static class LogManager
    {
        private static readonly object _lockObject = new object();
        private static string _logFolderPath;
        private static string _logFilePath;
        private static readonly int _maxLogFileSizeBytes = 5 * 1024 * 1024; // 5 ميجابايت كحد أقصى

        /// <summary>
        /// تهيئة مدير السجلات
        /// </summary>
        public static void Initialize()
        {
            try
            {
                string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                _logFolderPath = Path.Combine(appPath, "Logs");
                
                // إنشاء مجلد السجلات إذا لم يكن موجودًا
                if (!Directory.Exists(_logFolderPath))
                {
                    Directory.CreateDirectory(_logFolderPath);
                }

                // اسم ملف السجل بناءً على التاريخ
                string logFileName = $"Log_{DateTime.Now:yyyy-MM-dd}.log";
                _logFilePath = Path.Combine(_logFolderPath, logFileName);

                // تسجيل بدء النظام
                LogInfo("System initialized");
            }
            catch (Exception ex)
            {
                // لا يمكن استخدام LogException هنا لتجنب التكرار
                Debug.WriteLine($"Failed to initialize LogManager: {ex}");
            }
        }

        /// <summary>
        /// تسجيل معلومة
        /// </summary>
        /// <param name="message">نص المعلومة</param>
        public static void LogInfo(string message)
        {
            WriteToLog("INFO", message);
        }

        /// <summary>
        /// تسجيل تحذير
        /// </summary>
        /// <param name="message">نص التحذير</param>
        public static void LogWarning(string message)
        {
            WriteToLog("WARNING", message);
        }

        /// <summary>
        /// تسجيل خطأ
        /// </summary>
        /// <param name="message">نص الخطأ</param>
        public static void LogError(string message)
        {
            WriteToLog("ERROR", message);
        }

        /// <summary>
        /// تسجيل استثناء
        /// </summary>
        /// <param name="ex">كائن الاستثناء</param>
        /// <param name="context">سياق الاستثناء</param>
        public static void LogException(Exception ex, string context = null)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(context))
            {
                sb.Append(context).Append(": ");
            }

            sb.Append(ex.Message);

            // تضمين تفاصيل الاستثناء
            sb.AppendLine().Append("Exception Type: ").Append(ex.GetType().FullName);
            sb.AppendLine().Append("Stack Trace: ").Append(ex.StackTrace);

            // تضمين الاستثناءات الداخلية
            Exception innerEx = ex.InnerException;
            while (innerEx != null)
            {
                sb.AppendLine().Append("Inner Exception: ").Append(innerEx.Message);
                sb.AppendLine().Append("Inner Stack Trace: ").Append(innerEx.StackTrace);
                innerEx = innerEx.InnerException;
            }

            WriteToLog("EXCEPTION", sb.ToString());
        }

        /// <summary>
        /// تسجيل أثر
        /// </summary>
        /// <param name="message">نص الأثر</param>
        public static void LogTrace(string message)
        {
            WriteToLog("TRACE", message);
        }

        /// <summary>
        /// كتابة السجل إلى الملف
        /// </summary>
        /// <param name="level">مستوى السجل</param>
        /// <param name="message">نص السجل</param>
        private static void WriteToLog(string level, string message)
        {
            // التأكد من تهيئة مدير السجلات
            if (string.IsNullOrEmpty(_logFilePath))
            {
                Initialize();
            }

            try
            {
                lock (_lockObject)
                {
                    // التحقق من حجم ملف السجل
                    CheckLogFileSize();

                    // إنشاء سطر السجل
                    string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] [{Thread.CurrentThread.ManagedThreadId}] {message}{Environment.NewLine}";

                    // كتابة السجل
                    File.AppendAllText(_logFilePath, logLine);

                    // كتابة السجل إلى وحدة التصحيح
                    Debug.WriteLine(logLine);
                }
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ إلى وحدة التصحيح
                Debug.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }

        /// <summary>
        /// التحقق من حجم ملف السجل
        /// </summary>
        private static void CheckLogFileSize()
        {
            try
            {
                if (File.Exists(_logFilePath))
                {
                    FileInfo fileInfo = new FileInfo(_logFilePath);
                    if (fileInfo.Length > _maxLogFileSizeBytes)
                    {
                        // إنشاء ملف سجل جديد بناءً على الوقت
                        string newLogFileName = $"Log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log";
                        _logFilePath = Path.Combine(_logFolderPath, newLogFileName);
                        LogInfo($"Log file size exceeded {_maxLogFileSizeBytes} bytes. Created new log file: {newLogFileName}");
                    }
                }
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ إلى وحدة التصحيح
                Debug.WriteLine($"Failed to check log file size: {ex.Message}");
            }
        }

        /// <summary>
        /// تنظيف ملفات السجلات القديمة
        /// </summary>
        /// <param name="daysToKeep">عدد الأيام للاحتفاظ بملفات السجلات</param>
        public static void CleanupOldLogs(int daysToKeep = 30)
        {
            try
            {
                if (string.IsNullOrEmpty(_logFolderPath) || !Directory.Exists(_logFolderPath))
                {
                    return;
                }

                DateTime cutoffDate = DateTime.Now.AddDays(-daysToKeep);
                foreach (string logFile in Directory.GetFiles(_logFolderPath, "Log_*.log"))
                {
                    FileInfo fileInfo = new FileInfo(logFile);
                    if (fileInfo.CreationTime < cutoffDate)
                    {
                        fileInfo.Delete();
                        LogInfo($"Deleted old log file: {Path.GetFileName(logFile)}");
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "Failed to clean up old log files");
            }
        }
    }
}