using System;
using System.Configuration;
using System.Data.SqlClient;

namespace HR.Core
{
    /// <summary>
    /// مدير الاتصال بقاعدة البيانات
    /// </summary>
    public class ConnectionManager
    {
        private readonly string _connectionString;

        /// <summary>
        /// إنشاء مدير الاتصال
        /// </summary>
        public ConnectionManager()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["HRSystemConnection"].ConnectionString;
        }

        /// <summary>
        /// الحصول على اتصال جديد لقاعدة البيانات
        /// </summary>
        /// <returns>اتصال قاعدة البيانات</returns>
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// اختبار الاتصال بقاعدة البيانات
        /// </summary>
        /// <returns>نجاح الاتصال</returns>
        public bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}