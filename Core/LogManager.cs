using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace HR.Core
{
    /// <summary>
    /// مدير التسجيل (Logger) المسؤول عن تسجيل الأحداث والأخطاء في النظام
    /// </summary>
    public static class LogManager
    {
        private static readonly object lockObject = new object();
        private static readonly string LogDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Logs");
        private static readonly string ErrorLogFile = Path.Combine(LogDirectory, "Error.log");
        private static readonly string WarningLogFile = Path.Combine(LogDirectory, "Warning.log");
        private static readonly string InfoLogFile = Path.Combine(LogDirectory, "Info.log");
        private static readonly string AuditLogFile = Path.Combine(LogDirectory, "Audit.log");
        
        /// <summary>
        /// مستويات التسجيل
        /// </summary>
        public enum LogLevel
        {
            Info,
            Warning,
            Error,
            Audit
        }
        
        /// <summary>
        /// تهيئة مدير التسجيل
        /// </summary>
        public static void Initialize()
        {
            // إنشاء مجلد السجلات إذا لم يكن موجودًا
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
        }
        
        /// <summary>
        /// تسجيل معلومة
        /// </summary>
        /// <param name="message">الرسالة</param>
        public static void LogInfo(string message)
        {
            Log(LogLevel.Info, message);
        }
        
        /// <summary>
        /// تسجيل تحذير
        /// </summary>
        /// <param name="message">الرسالة</param>
        public static void LogWarning(string message)
        {
            Log(LogLevel.Warning, message);
        }
        
        /// <summary>
        /// تسجيل خطأ
        /// </summary>
        /// <param name="message">الرسالة</param>
        public static void LogError(string message)
        {
            Log(LogLevel.Error, message);
        }
        
        /// <summary>
        /// تسجيل استثناء
        /// </summary>
        /// <param name="ex">الاستثناء</param>
        /// <param name="message">رسالة إضافية (اختياري)</param>
        public static void LogException(Exception ex, string message = null)
        {
            StringBuilder sb = new StringBuilder();
            
            if (!string.IsNullOrEmpty(message))
            {
                sb.AppendLine(message);
                sb.AppendLine("---");
            }
            
            sb.AppendLine($"Exception Type: {ex.GetType().FullName}");
            sb.AppendLine($"Message: {ex.Message}");
            sb.AppendLine($"Stack Trace: {ex.StackTrace}");
            
            if (ex.InnerException != null)
            {
                sb.AppendLine("--- Inner Exception ---");
                sb.AppendLine($"Type: {ex.InnerException.GetType().FullName}");
                sb.AppendLine($"Message: {ex.InnerException.Message}");
                sb.AppendLine($"Stack Trace: {ex.InnerException.StackTrace}");
            }
            
            Log(LogLevel.Error, sb.ToString());
        }
        
        /// <summary>
        /// تسجيل إجراء (Audit)
        /// </summary>
        /// <param name="action">الإجراء</param>
        /// <param name="entityType">نوع الكيان</param>
        /// <param name="entityId">معرف الكيان</param>
        /// <param name="userId">معرف المستخدم</param>
        /// <param name="additionalInfo">معلومات إضافية (اختياري)</param>
        public static void LogAudit(string action, string entityType, string entityId, int userId, string additionalInfo = null)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine($"Action: {action}");
            sb.AppendLine($"Entity Type: {entityType}");
            sb.AppendLine($"Entity ID: {entityId}");
            sb.AppendLine($"User ID: {userId}");
            
            if (!string.IsNullOrEmpty(additionalInfo))
            {
                sb.AppendLine($"Additional Info: {additionalInfo}");
            }
            
            Log(LogLevel.Audit, sb.ToString());
        }
        
        /// <summary>
        /// تسجيل بمستوى محدد
        /// </summary>
        /// <param name="level">مستوى التسجيل</param>
        /// <param name="message">الرسالة</param>
        private static void Log(LogLevel level, string message)
        {
            try
            {
                if (string.IsNullOrEmpty(message))
                    return;
                
                Initialize();
                
                string logFile = GetLogFile(level);
                
                lock (lockObject)
                {
                    StringBuilder logEntry = new StringBuilder();
                    logEntry.Append($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ");
                    
                    if (SessionManager.CurrentUser != null)
                    {
                        logEntry.Append($"[User: {SessionManager.CurrentUser.ID}] ");
                    }
                    
                    logEntry.AppendLine(message);
                    logEntry.AppendLine("----------------------------------------");
                    
                    File.AppendAllText(logFile, logEntry.ToString());
                }
            }
            catch
            {
                // لا نريد أن يؤدي فشل التسجيل إلى تعطيل التطبيق
            }
        }
        
        /// <summary>
        /// الحصول على ملف السجل المناسب للمستوى
        /// </summary>
        /// <param name="level">مستوى التسجيل</param>
        /// <returns>مسار الملف</returns>
        private static string GetLogFile(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Info:
                    return InfoLogFile;
                case LogLevel.Warning:
                    return WarningLogFile;
                case LogLevel.Error:
                    return ErrorLogFile;
                case LogLevel.Audit:
                    return AuditLogFile;
                default:
                    return InfoLogFile;
            }
        }
        
        /// <summary>
        /// تنظيف السجلات القديمة (مثلاً السجلات الأقدم من شهر)
        /// </summary>
        /// <param name="olderThanDays">عدد الأيام</param>
        public static void CleanupOldLogs(int olderThanDays = 30)
        {
            try
            {
                Initialize();
                
                DateTime cutoffDate = DateTime.Now.AddDays(-olderThanDays);
                
                DirectoryInfo dirInfo = new DirectoryInfo(LogDirectory);
                
                foreach (FileInfo file in dirInfo.GetFiles("*.log"))
                {
                    if (file.LastWriteTime < cutoffDate)
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                // لا يمكننا استخدام LogException هنا لتجنب التكرار
                Console.WriteLine($"Error cleaning up logs: {ex.Message}");
            }
        }
    }
}