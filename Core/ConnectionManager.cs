using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace HR.Core
{
    /// <summary>
    /// Manages database connections
    /// </summary>
    public static class ConnectionManager
    {
        private static string _connectionString;
        private static bool _isInitialized;

        /// <summary>
        /// Initializes the connection manager
        /// </summary>
        /// <returns>True if the initialization was successful</returns>
        public static bool Initialize()
        {
            try
            {
                // Get connection string from configuration
                _connectionString = ConfigurationManager.ConnectionStrings["HRSystemConnection"].ConnectionString;

                // Test connection
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    LogManager.LogInfo("Database connection initialized successfully");
                }

                _isInitialized = true;
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to initialize database connection");
                _isInitialized = false;
                return false;
            }
        }

        /// <summary>
        /// Creates a new database connection
        /// </summary>
        /// <returns>SQL connection object</returns>
        public static SqlConnection CreateConnection()
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Connection manager is not initialized");
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            return connection;
        }

        /// <summary>
        /// Executes a non-query SQL command
        /// </summary>
        /// <param name="commandText">SQL command text</param>
        /// <param name="parameters">SQL parameters</param>
        /// <returns>Number of rows affected</returns>
        public static int ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Connection manager is not initialized");
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Executes a SQL query and returns a data reader
        /// </summary>
        /// <param name="commandText">SQL command text</param>
        /// <param name="parameters">SQL parameters</param>
        /// <returns>SQL data reader</returns>
        public static SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameters)
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Connection manager is not initialized");
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(commandText, connection);
            command.CommandType = CommandType.Text;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Executes a SQL query and returns a scalar value
        /// </summary>
        /// <param name="commandText">SQL command text</param>
        /// <param name="parameters">SQL parameters</param>
        /// <returns>Scalar value</returns>
        public static object ExecuteScalar(string commandText, params SqlParameter[] parameters)
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Connection manager is not initialized");
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Executes a SQL query and returns a DataTable
        /// </summary>
        /// <param name="commandText">SQL command text</param>
        /// <param name="parameters">SQL parameters</param>
        /// <returns>DataTable with query results</returns>
        public static DataTable ExecuteDataTable(string commandText, params SqlParameter[] parameters)
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Connection manager is not initialized");
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// Executes a stored procedure and returns a DataTable
        /// </summary>
        /// <param name="procedureName">Stored procedure name</param>
        /// <param name="parameters">SQL parameters</param>
        /// <returns>DataTable with query results</returns>
        public static DataTable ExecuteStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Connection manager is not initialized");
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// Begins a new transaction
        /// </summary>
        /// <returns>SQL transaction object</returns>
        public static SqlTransaction BeginTransaction()
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Connection manager is not initialized");
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection.BeginTransaction();
        }

        /// <summary>
        /// Checks if the system is configured (has company information)
        /// </summary>
        /// <returns>True if the system is configured</returns>
        public static bool IsSystemConfigured()
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Connection manager is not initialized");
            }

            try
            {
                object result = ExecuteScalar("SELECT COUNT(*) FROM Company");
                return Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to check if system is configured");
                return false;
            }
        }

        /// <summary>
        /// Initializes the database (creates tables and initial data)
        /// </summary>
        /// <returns>True if the initialization was successful</returns>
        public static bool InitializeDatabase()
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Connection manager is not initialized");
            }

            try
            {
                // Check if database already has tables
                object result = ExecuteScalar("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'");
                if (Convert.ToInt32(result) > 0)
                {
                    // Database already has tables
                    LogManager.LogInfo("Database already initialized");
                    return true;
                }

                // Read SQL script from file
                string sqlScript = File.ReadAllText("HRSystem_SQLServer.sql");
                
                // Execute SQL script
                string[] commandTexts = sqlScript.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string commandText in commandTexts)
                {
                    if (!string.IsNullOrWhiteSpace(commandText))
                    {
                        ExecuteNonQuery(commandText);
                    }
                }

                LogManager.LogInfo("Database initialized successfully");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to initialize database");
                return false;
            }
        }

        /// <summary>
        /// Gets the connection string
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (!_isInitialized)
                {
                    throw new InvalidOperationException("Connection manager is not initialized");
                }
                return _connectionString;
            }
        }
    }
}