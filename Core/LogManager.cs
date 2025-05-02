using System;
using System.IO;
using System.Text;
using System.Threading;

namespace HR.Core
{
    /// <summary>
    /// مدير السجلات والتقارير
    /// </summary>
    public class LogManager
    {
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();
        private static string _logPath;
        private static bool _isInitialized = false;

        /// <summary>
        /// تهيئة مدير السجلات
        /// </summary>
        public static void Initialize()
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string logDirectory = Path.Combine(basePath, "Logs");
                
                // إنشاء مجلد السجلات إذا لم يكن موجودا
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }
                
                // تحديد مسار ملف السجل
                _logPath = Path.Combine(logDirectory, $"HRSystem_{DateTime.Now:yyyy-MM-dd}.log");
                _isInitialized = true;
                
                LogInfo("تم تهيئة مدير السجلات بنجاح");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"فشل تهيئة مدير السجلات: {ex.Message}");
                _isInitialized = false;
            }
        }

        /// <summary>
        /// تسجيل معلومة
        /// </summary>
        /// <param name="message">نص المعلومة</param>
        public static void LogInfo(string message)
        {
            Log("INFO", message);
        }

        /// <summary>
        /// تسجيل تحذير
        /// </summary>
        /// <param name="message">نص التحذير</param>
        public static void LogWarning(string message)
        {
            Log("WARNING", message);
        }

        /// <summary>
        /// تسجيل خطأ
        /// </summary>
        /// <param name="message">نص الخطأ</param>
        public static void LogError(string message)
        {
            Log("ERROR", message);
        }

        /// <summary>
        /// تسجيل استثناء
        /// </summary>
        /// <param name="ex">الاستثناء</param>
        /// <param name="message">رسالة إضافية</param>
        public static void LogException(Exception ex, string message = null)
        {
            StringBuilder sb = new StringBuilder();
            
            if (!string.IsNullOrEmpty(message))
            {
                sb.AppendLine(message);
            }
            
            sb.AppendLine($"استثناء: {ex.Message}");
            sb.AppendLine($"المصدر: {ex.Source}");
            sb.AppendLine($"تفاصيل: {ex.StackTrace}");
            
            if (ex.InnerException != null)
            {
                sb.AppendLine($"استثناء داخلي: {ex.InnerException.Message}");
                sb.AppendLine($"تفاصيل الاستثناء الداخلي: {ex.InnerException.StackTrace}");
            }
            
            Log("EXCEPTION", sb.ToString());
        }

        /// <summary>
        /// تسجيل نشاط المستخدم
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        /// <param name="activity">النشاط</param>
        /// <param name="details">تفاصيل إضافية</param>
        public static void LogUserActivity(int userId, string activity, string details = null)
        {
            string message = $"المستخدم (ID: {userId}) - {activity}";
            
            if (!string.IsNullOrEmpty(details))
            {
                message += $" - {details}";
            }
            
            Log("USER_ACTIVITY", message);
        }

        /// <summary>
        /// تسجيل محاولة تسجيل دخول
        /// </summary>
        /// <param name="username">اسم المستخدم</param>
        /// <param name="success">نجاح العملية</param>
        /// <param name="ipAddress">عنوان IP</param>
        public static void LogLogin(string username, bool success, string ipAddress)
        {
            string status = success ? "ناجح" : "فاشل";
            string message = $"محاولة تسجيل دخول {status} - المستخدم: {username} - IP: {ipAddress}";
            Log("LOGIN", message);
        }

        /// <summary>
        /// كتابة السجل
        /// </summary>
        /// <param name="level">مستوى السجل</param>
        /// <param name="message">نص السجل</param>
        private static void Log(string level, string message)
        {
            try
            {
                if (!_isInitialized)
                {
                    Initialize();
                }

                // تنسيق مدخل السجل
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

                // كتابة السجل بطريقة آمنة لتعدد المستخدمين
                _readWriteLock.EnterWriteLock();
                try
                {
                    using (StreamWriter writer = new StreamWriter(_logPath, true, Encoding.UTF8))
                    {
                        writer.WriteLine(logEntry);
                    }
                }
                finally
                {
                    _readWriteLock.ExitWriteLock();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"فشل في كتابة السجل: {ex.Message}");
            }
        }

        /// <summary>
        /// الحصول على سجلات اليوم
        /// </summary>
        /// <returns>نص السجلات</returns>
        public static string GetTodaysLogs()
        {
            if (!_isInitialized)
            {
                Initialize();
            }

            if (!File.Exists(_logPath))
            {
                return "لا توجد سجلات لهذا اليوم.";
            }

            _readWriteLock.EnterReadLock();
            try
            {
                return File.ReadAllText(_logPath, Encoding.UTF8);
            }
            finally
            {
                _readWriteLock.ExitReadLock();
            }
        }

        /// <summary>
        /// الحصول على سجلات يوم معين
        /// </summary>
        /// <param name="date">التاريخ</param>
        /// <returns>نص السجلات</returns>
        public static string GetLogsByDate(DateTime date)
        {
            string logFile = Path.Combine(
                Path.GetDirectoryName(_logPath), 
                $"HRSystem_{date:yyyy-MM-dd}.log");

            if (!File.Exists(logFile))
            {
                return $"لا توجد سجلات لتاريخ {date:yyyy-MM-dd}.";
            }

            return File.ReadAllText(logFile, Encoding.UTF8);
        }
    }
}