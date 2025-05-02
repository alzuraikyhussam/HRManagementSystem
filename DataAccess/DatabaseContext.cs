using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Core;

namespace HR.DataAccess
{
    /// <summary>
    /// فئة قاعدة للتعامل مع قاعدة البيانات
    /// </summary>
    public class DatabaseContext
    {
        private readonly ConnectionManager _connectionManager;

        public DatabaseContext()
        {
            _connectionManager = new ConnectionManager();
        }

        /// <summary>
        /// تنفيذ استعلام بدون إرجاع نتائج
        /// </summary>
        /// <param name="commandText">نص الاستعلام</param>
        /// <param name="parameters">بارامترات الاستعلام</param>
        /// <param name="commandType">نوع الاستعلام</param>
        /// <returns>عدد الصفوف المتأثرة</returns>
        public int ExecuteNonQuery(string commandText, List<SqlParameter> parameters = null, CommandType commandType = CommandType.Text)
        {
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;

                    if (parameters != null && parameters.Count > 0)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();

                    return result;
                }
            }
        }

        /// <summary>
        /// تنفيذ استعلام وإرجاع قيمة واحدة
        /// </summary>
        /// <param name="commandText">نص الاستعلام</param>
        /// <param name="parameters">بارامترات الاستعلام</param>
        /// <param name="commandType">نوع الاستعلام</param>
        /// <returns>القيمة المرجعة من الاستعلام</returns>
        public object ExecuteScalar(string commandText, List<SqlParameter> parameters = null, CommandType commandType = CommandType.Text)
        {
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;

                    if (parameters != null && parameters.Count > 0)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    connection.Open();
                    object result = command.ExecuteScalar();
                    connection.Close();

                    return result;
                }
            }
        }

        /// <summary>
        /// تنفيذ استعلام وإرجاع قارئ البيانات
        /// </summary>
        /// <param name="commandText">نص الاستعلام</param>
        /// <param name="parameters">بارامترات الاستعلام</param>
        /// <param name="commandType">نوع الاستعلام</param>
        /// <returns>قارئ البيانات</returns>
        public DataTable ExecuteReader(string commandText, List<SqlParameter> parameters = null, CommandType commandType = CommandType.Text)
        {
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;

                    if (parameters != null && parameters.Count > 0)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    DataTable dataTable = new DataTable();
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                    connection.Close();

                    return dataTable;
                }
            }
        }

        /// <summary>
        /// تنفيذ مجموعة من الاستعلامات في معاملة واحدة
        /// </summary>
        /// <param name="action">الإجراءات المراد تنفيذها في المعاملة</param>
        /// <returns>نتيجة التنفيذ</returns>
        public bool ExecuteTransaction(Action<SqlConnection, SqlTransaction> action)
        {
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    action(connection, transaction);
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}