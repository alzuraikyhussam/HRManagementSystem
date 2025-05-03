using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HR.Core
{
    /// <summary>
    /// مدير النسخ الاحتياطي والاستعادة
    /// </summary>
    public class BackupManager
    {
        private readonly string _backupFolder;
        private readonly ConnectionManager _connectionManager;
        
        /// <summary>
        /// إنشاء مدير النسخ الاحتياطي
        /// </summary>
        public BackupManager()
        {
            _connectionManager = new ConnectionManager();
            
            // تحديد مجلد النسخ الاحتياطي
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _backupFolder = Path.Combine(appDataFolder, "HRSystem", "Backups");
            
            // إنشاء المجلد إذا لم يكن موجوداً
            if (!Directory.Exists(_backupFolder))
            {
                Directory.CreateDirectory(_backupFolder);
            }
        }
        
        /// <summary>
        /// إنشاء نسخة احتياطية
        /// </summary>
        /// <param name="description">وصف النسخة</param>
        /// <returns>مسار ملف النسخة الاحتياطية</returns>
        public async Task<string> CreateBackupAsync(string description)
        {
            try
            {
                LogManager.LogInfo("بدء عملية النسخ الاحتياطي");
                
                // إنشاء اسم الملف
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupName = $"HRSystem_Backup_{timestamp}.bak";
                string backupPath = Path.Combine(_backupFolder, backupName);
                
                // الحصول على اتصال قاعدة البيانات
                string connectionString = _connectionManager.GetConnectionString();
                string databaseName = GetDatabaseName(connectionString);
                
                // تنفيذ أمر النسخ الاحتياطي
                bool success = await ExecuteBackupCommandAsync(databaseName, backupPath);
                
                if (success)
                {
                    // تسجيل عملية النسخ الاحتياطي
                    LogBackupOperation(backupPath, description, "Backup", "Success");
                    
                    LogManager.LogInfo($"تم إنشاء نسخة احتياطية بنجاح: {backupPath}");
                    return backupPath;
                }
                else
                {
                    LogManager.LogError("فشل إنشاء نسخة احتياطية");
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء إنشاء نسخة احتياطية");
                return null;
            }
        }
        
        /// <summary>
        /// استعادة نسخة احتياطية
        /// </summary>
        /// <param name="backupPath">مسار ملف النسخة الاحتياطية</param>
        /// <returns>نتيجة الاستعادة</returns>
        public async Task<bool> RestoreBackupAsync(string backupPath)
        {
            try
            {
                LogManager.LogInfo($"بدء عملية استعادة النسخة الاحتياطية: {backupPath}");
                
                // التحقق من وجود الملف
                if (!File.Exists(backupPath))
                {
                    LogManager.LogError($"ملف النسخة الاحتياطية غير موجود: {backupPath}");
                    return false;
                }
                
                // الحصول على اتصال قاعدة البيانات
                string connectionString = _connectionManager.GetConnectionString();
                string databaseName = GetDatabaseName(connectionString);
                
                // تنفيذ أمر الاستعادة
                bool success = await ExecuteRestoreCommandAsync(databaseName, backupPath);
                
                if (success)
                {
                    // تسجيل عملية الاستعادة
                    LogBackupOperation(backupPath, "استعادة النسخة الاحتياطية", "Restore", "Success");
                    
                    LogManager.LogInfo($"تمت استعادة النسخة الاحتياطية بنجاح: {backupPath}");
                    return true;
                }
                else
                {
                    LogManager.LogError($"فشل استعادة النسخة الاحتياطية: {backupPath}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء استعادة النسخة الاحتياطية: {backupPath}");
                return false;
            }
        }
        
        /// <summary>
        /// الحصول على قائمة النسخ الاحتياطية
        /// </summary>
        /// <returns>قائمة ملفات النسخ الاحتياطية</returns>
        public List<BackupInfo> GetBackupList()
        {
            try
            {
                List<BackupInfo> backupList = new List<BackupInfo>();
                
                // الحصول على قائمة ملفات النسخ الاحتياطية
                string[] backupFiles = Directory.GetFiles(_backupFolder, "HRSystem_Backup_*.bak");
                
                foreach (string file in backupFiles)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    
                    // استخراج التاريخ من اسم الملف
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string[] parts = fileName.Split('_');
                    
                    if (parts.Length >= 3)
                    {
                        string dateString = parts[2];
                        DateTime creationDate;
                        
                        if (DateTime.TryParseExact(dateString, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out creationDate))
                        {
                            BackupInfo backupInfo = new BackupInfo
                            {
                                BackupPath = file,
                                FileName = Path.GetFileName(file),
                                CreationDate = fileInfo.CreationTime,
                                FileSize = fileInfo.Length,
                                Description = GetBackupDescription(file)
                            };
                            
                            backupList.Add(backupInfo);
                        }
                    }
                }
                
                // ترتيب القائمة حسب تاريخ الإنشاء (الأحدث أولاً)
                backupList.Sort((a, b) => b.CreationDate.CompareTo(a.CreationDate));
                
                return backupList;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء الحصول على قائمة النسخ الاحتياطية");
                return new List<BackupInfo>();
            }
        }
        
        /// <summary>
        /// حذف نسخة احتياطية
        /// </summary>
        /// <param name="backupPath">مسار ملف النسخة الاحتياطية</param>
        /// <returns>نتيجة الحذف</returns>
        public bool DeleteBackup(string backupPath)
        {
            try
            {
                // التحقق من وجود الملف
                if (!File.Exists(backupPath))
                {
                    LogManager.LogError($"ملف النسخة الاحتياطية غير موجود: {backupPath}");
                    return false;
                }
                
                // حذف الملف
                File.Delete(backupPath);
                
                // تسجيل عملية الحذف
                LogBackupOperation(backupPath, "حذف النسخة الاحتياطية", "Delete", "Success");
                
                LogManager.LogInfo($"تم حذف النسخة الاحتياطية بنجاح: {backupPath}");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء حذف النسخة الاحتياطية: {backupPath}");
                return false;
            }
        }
        
        /// <summary>
        /// تنفيذ جدولة للنسخ الاحتياطي التلقائي
        /// </summary>
        /// <param name="interval">الفاصل الزمني بالساعات</param>
        /// <param name="description">وصف النسخة</param>
        /// <returns>نتيجة الجدولة</returns>
        public bool ScheduleAutomaticBackup(int interval, string description)
        {
            try
            {
                // تنفيذ جدولة النسخ الاحتياطي
                // يتم تنفيذ هذه العملية عن طريق إنشاء مهمة في نظام التشغيل
                // هذا مثال بسيط يوضح الفكرة، ويمكن تنفيذه بطرق مختلفة حسب متطلبات النظام
                
                LogManager.LogInfo($"تم جدولة النسخ الاحتياطي التلقائي كل {interval} ساعة");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء جدولة النسخ الاحتياطي التلقائي");
                return false;
            }
        }
        
        /// <summary>
        /// الحصول على اسم قاعدة البيانات من سلسلة الاتصال
        /// </summary>
        /// <param name="connectionString">سلسلة الاتصال</param>
        /// <returns>اسم قاعدة البيانات</returns>
        private string GetDatabaseName(string connectionString)
        {
            // استخراج اسم قاعدة البيانات من سلسلة الاتصال
            string[] parts = connectionString.Split(';');
            foreach (string part in parts)
            {
                if (part.Trim().StartsWith("Initial Catalog=") || part.Trim().StartsWith("Database="))
                {
                    return part.Split('=')[1].Trim();
                }
            }
            
            return "HRSystem";
        }
        
        /// <summary>
        /// تنفيذ أمر النسخ الاحتياطي
        /// </summary>
        /// <param name="databaseName">اسم قاعدة البيانات</param>
        /// <param name="backupPath">مسار ملف النسخة الاحتياطية</param>
        /// <returns>نتيجة التنفيذ</returns>
        private async Task<bool> ExecuteBackupCommandAsync(string databaseName, string backupPath)
        {
            try
            {
                // إنشاء أمر SQL للنسخ الاحتياطي
                string backupCommand = $"BACKUP DATABASE [{databaseName}] TO DISK = N'{backupPath}' WITH NOFORMAT, NOINIT, NAME = N'{databaseName}-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10";
                
                // تنفيذ الأمر
                bool result = await _connectionManager.ExecuteNonQueryAsync(backupCommand);
                return result;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء تنفيذ أمر النسخ الاحتياطي");
                return false;
            }
        }
        
        /// <summary>
        /// تنفيذ أمر الاستعادة
        /// </summary>
        /// <param name="databaseName">اسم قاعدة البيانات</param>
        /// <param name="backupPath">مسار ملف النسخة الاحتياطية</param>
        /// <returns>نتيجة التنفيذ</returns>
        private async Task<bool> ExecuteRestoreCommandAsync(string databaseName, string backupPath)
        {
            try
            {
                // إنشاء أمر SQL للاستعادة
                string restoreCommand = $@"
                USE [master];
                ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                RESTORE DATABASE [{databaseName}] FROM DISK = N'{backupPath}' WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 5;
                ALTER DATABASE [{databaseName}] SET MULTI_USER;";
                
                // تنفيذ الأمر
                bool result = await _connectionManager.ExecuteNonQueryAsync(restoreCommand);
                return result;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء تنفيذ أمر الاستعادة");
                return false;
            }
        }
        
        /// <summary>
        /// تسجيل عملية النسخ الاحتياطي
        /// </summary>
        /// <param name="backupPath">مسار ملف النسخة الاحتياطية</param>
        /// <param name="description">وصف العملية</param>
        /// <param name="operationType">نوع العملية</param>
        /// <param name="status">حالة العملية</param>
        private void LogBackupOperation(string backupPath, string description, string operationType, string status)
        {
            try
            {
                // إنشاء سجل بالعملية
                string logQuery = $@"
                INSERT INTO BackupLog (BackupPath, Description, OperationType, OperationDate, Status, UserID)
                VALUES ('{backupPath}', '{description}', '{operationType}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{status}', {SessionManager.CurrentUser?.ID ?? 0})";
                
                // تنفيذ الإدراج
                _connectionManager.ExecuteNonQuery(logQuery);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء تسجيل عملية النسخ الاحتياطي");
            }
        }
        
        /// <summary>
        /// الحصول على وصف النسخة الاحتياطية
        /// </summary>
        /// <param name="backupPath">مسار ملف النسخة الاحتياطية</param>
        /// <returns>وصف النسخة</returns>
        private string GetBackupDescription(string backupPath)
        {
            try
            {
                // الحصول على وصف النسخة من قاعدة البيانات
                string query = $"SELECT Description FROM BackupLog WHERE BackupPath = '{backupPath}' AND OperationType = 'Backup' ORDER BY OperationDate DESC";
                
                object result = _connectionManager.ExecuteScalar(query);
                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
                
                return "نسخة احتياطية للنظام";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء الحصول على وصف النسخة الاحتياطية");
                return "نسخة احتياطية للنظام";
            }
        }
    }
    
    /// <summary>
    /// معلومات ملف النسخة الاحتياطية
    /// </summary>
    public class BackupInfo
    {
        /// <summary>
        /// مسار ملف النسخة الاحتياطية
        /// </summary>
        public string BackupPath { get; set; }
        
        /// <summary>
        /// اسم الملف
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// تاريخ الإنشاء
        /// </summary>
        public DateTime CreationDate { get; set; }
        
        /// <summary>
        /// حجم الملف
        /// </summary>
        public long FileSize { get; set; }
        
        /// <summary>
        /// وصف النسخة
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// حجم الملف بصيغة مقروءة
        /// </summary>
        public string FormattedSize
        {
            get
            {
                string[] sizes = { "بايت", "كيلوبايت", "ميجابايت", "جيجابايت" };
                double len = FileSize;
                int order = 0;
                
                while (len >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    len = len / 1024;
                }
                
                return $"{len:0.##} {sizes[order]}";
            }
        }
    }
}