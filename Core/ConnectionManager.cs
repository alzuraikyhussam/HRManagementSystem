using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace HR.Core
{
    /// <summary>
    /// مدير الاتصال بقاعدة البيانات
    /// </summary>
    public class ConnectionManager
    {
        private static string _connectionString;
        private static bool _isInitialized = false;

        /// <summary>
        /// تهيئة مدير الاتصال
        /// </summary>
        /// <returns>نجاح التهيئة</returns>
        public static bool Initialize()
        {
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["HRSystemConnection"].ConnectionString;
                
                // التحقق من صحة الاتصال
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    _isInitialized = true;
                    LogManager.LogInfo("تم الاتصال بقاعدة البيانات بنجاح");
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل الاتصال بقاعدة البيانات");
                _isInitialized = false;
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

            SqlConnection connection = new SqlConnection(_connectionString);
            return connection;
        }

        /// <summary>
        /// التحقق من تكوين النظام (وجود بيانات الشركة)
        /// </summary>
        /// <returns>هل تم تكوين النظام؟</returns>
        public static bool IsSystemConfigured()
        {
            try
            {
                using (SqlConnection connection = CreateConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Company", connection))
                    {
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل التحقق من تكوين النظام");
                return false;
            }
        }

        /// <summary>
        /// تهيئة قاعدة البيانات
        /// </summary>
        /// <returns>نجاح التهيئة</returns>
        public static bool InitializeDatabase()
        {
            try
            {
                // قراءة ملف السكريبت
                string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HRSystem_SQLServer.sql");
                string script = File.ReadAllText(scriptPath);

                // فصل السكريبت إلى جمل SQL منفصلة
                string[] commandTexts = script.Split(new[] { "GO", "go" }, StringSplitOptions.RemoveEmptyEntries);

                using (SqlConnection connection = CreateConnection())
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            foreach (string commandText in commandTexts)
                            {
                                if (!string.IsNullOrWhiteSpace(commandText))
                                {
                                    using (SqlCommand command = new SqlCommand(commandText, connection, transaction))
                                    {
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }

                            transaction.Commit();
                            LogManager.LogInfo("تم تهيئة قاعدة البيانات بنجاح");
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            LogManager.LogException(ex, "فشل تهيئة قاعدة البيانات");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تهيئة قاعدة البيانات");
                return false;
            }
        }

        /// <summary>
        /// تنفيذ استعلام على قاعدة البيانات
        /// </summary>
        /// <param name="commandText">نص الاستعلام</param>
        /// <param name="parameters">المعاملات</param>
        /// <returns>عدد الصفوف المتأثرة</returns>
        public static int ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = CreateConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// تنفيذ استعلام يرجع قيمة واحدة
        /// </summary>
        /// <param name="commandText">نص الاستعلام</param>
        /// <param name="parameters">المعاملات</param>
        /// <returns>القيمة المرجعة</returns>
        public static object ExecuteScalar(string commandText, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = CreateConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// تنفيذ استعلام وملء الجدول بالبيانات المرجعة
        /// </summary>
        /// <param name="commandText">نص الاستعلام</param>
        /// <param name="parameters">المعاملات</param>
        /// <returns>جدول البيانات</returns>
        public static DataTable ExecuteQuery(string commandText, params SqlParameter[] parameters)
        {
            DataTable table = new DataTable();
            using (SqlConnection connection = CreateConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            return table;
        }

        /// <summary>
        /// تنفيذ استعلام وإرجاع قارئ البيانات
        /// </summary>
        /// <param name="commandText">نص الاستعلام</param>
        /// <param name="parameters">المعاملات</param>
        /// <returns>قارئ البيانات</returns>
        public static SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameters)
        {
            SqlConnection connection = CreateConnection();
            connection.Open();
            SqlCommand command = new SqlCommand(commandText, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            // CommandBehavior.CloseConnection يضمن إغلاق الاتصال عند إغلاق القارئ
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}