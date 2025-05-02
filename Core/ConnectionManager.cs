using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace HR.Core
{
    /// <summary>
    /// مدير الاتصال بقاعدة البيانات
    /// </summary>
    public static class ConnectionManager
    {
        private static string _connectionString;
        private static bool _isInitialized = false;

        /// <summary>
        /// تهيئة مدير الاتصال
        /// </summary>
        public static bool Initialize()
        {
            try
            {
                // قراءة سلسلة الاتصال من ملف الإعدادات
                _connectionString = ConfigurationManager.ConnectionStrings["HRSystemConnection"]?.ConnectionString;
                
                if (string.IsNullOrEmpty(_connectionString))
                {
                    // استخدام سلسلة اتصال تجريبية للاختبار
                    _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\HRSystem.mdf;Integrated Security=True";
                    LogManager.LogWarning("تم استخدام سلسلة اتصال تجريبية. يرجى تكوين سلسلة اتصال HRSystemConnection في ملف App.config");
                }

                _isInitialized = true;
                return TestConnection();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تهيئة مدير الاتصال");
                return false;
            }
        }

        /// <summary>
        /// إنشاء اتصال جديد بقاعدة البيانات
        /// </summary>
        /// <returns>اتصال قاعدة البيانات</returns>
        public static SqlConnection CreateConnection()
        {
            if (!_isInitialized)
            {
                Initialize();
            }

            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// اختبار الاتصال بقاعدة البيانات
        /// </summary>
        /// <returns>نجاح الاتصال</returns>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = CreateConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في اختبار الاتصال بقاعدة البيانات: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// التحقق مما إذا كان النظام تم تكوينه مسبقاً
        /// </summary>
        /// <returns>حالة تكوين النظام</returns>
        public static bool IsSystemConfigured()
        {
            try
            {
                using (SqlConnection connection = CreateConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Company'", connection))
                    {
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في التحقق من تكوين النظام");
                return false;
            }
        }

        /// <summary>
        /// تهيئة قاعدة البيانات من ملف التصميم
        /// </summary>
        /// <returns>نجاح التهيئة</returns>
        public static bool InitializeDatabase()
        {
            try
            {
                // قراءة ملف التهيئة SQL
                string sqlScript = File.ReadAllText("attached_assets/HRSystem_Database.sql");
                
                // تنفيذ أوامر SQL لإنشاء جداول قاعدة البيانات
                using (SqlConnection connection = CreateConnection())
                {
                    connection.Open();
                    
                    // تقسيم النص إلى أوامر منفصلة لتنفيذها على حدة
                    string[] commands = sqlScript.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                    
                    foreach (string command in commands)
                    {
                        if (!string.IsNullOrWhiteSpace(command))
                        {
                            using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                            {
                                sqlCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تهيئة قاعدة البيانات: " + ex.Message);
                return false;
            }
        }
    }
}