using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// مستودع بيانات الرواتب
    /// </summary>
    public class SalaryRepository
    {
        private readonly ConnectionManager _connectionManager;
        
        /// <summary>
        /// إنشاء مستودع بيانات الرواتب
        /// </summary>
        public SalaryRepository()
        {
            _connectionManager = ConnectionManager.Instance;
        }
        
        #region Salary Components Methods
        
        /// <summary>
        /// الحصول على جميع عناصر الراتب
        /// </summary>
        /// <returns>قائمة عناصر الراتب</returns>
        public List<SalaryComponent> GetAllSalaryComponents()
        {
            List<SalaryComponent> components = new List<SalaryComponent>();
            
            string query = @"
                SELECT 
                    c.ID, c.Name, c.Description, c.Type, c.IsBasic, c.IsVariable, c.IsTaxable,
                    c.AffectsNetSalary, c.Position, c.FormulaType, c.PercentageOf, c.DefaultAmount,
                    c.DefaultPercentage, c.Formula, c.IsActive, c.CreatedAt, c.CreatedBy,
                    u.Username AS CreatedByUser
                FROM SalaryComponents c
                LEFT JOIN Users u ON c.CreatedBy = u.ID
                ORDER BY c.Position, c.Name";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            components.Add(MapSalaryComponentFromReader(reader));
                        }
                    }
                }
            }
            
            return components;
        }
        
        /// <summary>
        /// الحصول على عناصر الراتب النشطة
        /// </summary>
        /// <returns>قائمة عناصر الراتب النشطة</returns>
        public List<SalaryComponent> GetActiveSalaryComponents()
        {
            List<SalaryComponent> components = new List<SalaryComponent>();
            
            string query = @"
                SELECT 
                    c.ID, c.Name, c.Description, c.Type, c.IsBasic, c.IsVariable, c.IsTaxable,
                    c.AffectsNetSalary, c.Position, c.FormulaType, c.PercentageOf, c.DefaultAmount,
                    c.DefaultPercentage, c.Formula, c.IsActive, c.CreatedAt, c.CreatedBy,
                    u.Username AS CreatedByUser
                FROM SalaryComponents c
                LEFT JOIN Users u ON c.CreatedBy = u.ID
                WHERE c.IsActive = 1
                ORDER BY c.Position, c.Name";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            components.Add(MapSalaryComponentFromReader(reader));
                        }
                    }
                }
            }
            
            return components;
        }
        
        /// <summary>
        /// الحصول على عنصر راتب بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف العنصر</param>
        /// <returns>عنصر الراتب</returns>
        public SalaryComponent GetSalaryComponentById(int id)
        {
            string query = @"
                SELECT 
                    c.ID, c.Name, c.Description, c.Type, c.IsBasic, c.IsVariable, c.IsTaxable,
                    c.AffectsNetSalary, c.Position, c.FormulaType, c.PercentageOf, c.DefaultAmount,
                    c.DefaultPercentage, c.Formula, c.IsActive, c.CreatedAt, c.CreatedBy,
                    u.Username AS CreatedByUser
                FROM SalaryComponents c
                LEFT JOIN Users u ON c.CreatedBy = u.ID
                WHERE c.ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapSalaryComponentFromReader(reader);
                        }
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// الحصول على عنصر الراتب الأساسي
        /// </summary>
        /// <returns>عنصر الراتب الأساسي</returns>
        public SalaryComponent GetBasicSalaryComponent()
        {
            string query = @"
                SELECT 
                    c.ID, c.Name, c.Description, c.Type, c.IsBasic, c.IsVariable, c.IsTaxable,
                    c.AffectsNetSalary, c.Position, c.FormulaType, c.PercentageOf, c.DefaultAmount,
                    c.DefaultPercentage, c.Formula, c.IsActive, c.CreatedAt, c.CreatedBy,
                    u.Username AS CreatedByUser
                FROM SalaryComponents c
                LEFT JOIN Users u ON c.CreatedBy = u.ID
                WHERE c.IsBasic = 1 AND c.IsActive = 1";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapSalaryComponentFromReader(reader);
                        }
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// إضافة عنصر راتب جديد
        /// </summary>
        /// <param name="component">بيانات العنصر</param>
        /// <returns>معرف العنصر الجديد</returns>
        public int AddSalaryComponent(SalaryComponent component)
        {
            string query = @"
                INSERT INTO SalaryComponents (
                    Name, Description, Type, IsBasic, IsVariable, IsTaxable, AffectsNetSalary,
                    Position, FormulaType, PercentageOf, DefaultAmount, DefaultPercentage,
                    Formula, IsActive, CreatedAt, CreatedBy
                ) 
                VALUES (
                    @Name, @Description, @Type, @IsBasic, @IsVariable, @IsTaxable, @AffectsNetSalary,
                    @Position, @FormulaType, @PercentageOf, @DefaultAmount, @DefaultPercentage,
                    @Formula, @IsActive, @CreatedAt, @CreatedBy
                );
                SELECT SCOPE_IDENTITY();";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddSalaryComponentParameters(command, component);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// تحديث عنصر راتب
        /// </summary>
        /// <param name="component">بيانات العنصر</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateSalaryComponent(SalaryComponent component)
        {
            string query = @"
                UPDATE SalaryComponents
                SET 
                    Name = @Name,
                    Description = @Description,
                    Type = @Type,
                    IsBasic = @IsBasic,
                    IsVariable = @IsVariable,
                    IsTaxable = @IsTaxable,
                    AffectsNetSalary = @AffectsNetSalary,
                    Position = @Position,
                    FormulaType = @FormulaType,
                    PercentageOf = @PercentageOf,
                    DefaultAmount = @DefaultAmount,
                    DefaultPercentage = @DefaultPercentage,
                    Formula = @Formula,
                    IsActive = @IsActive
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", component.ID);
                    AddSalaryComponentParameters(command, component);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// حذف عنصر راتب
        /// </summary>
        /// <param name="id">معرف العنصر</param>
        /// <returns>نجاح العملية</returns>
        public bool DeleteSalaryComponent(int id)
        {
            // لن نقوم بحذف العنصر نهائياً، بل سنقوم بإلغاء تفعيله
            string query = "UPDATE SalaryComponents SET IsActive = 0 WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// إضافة بارامترات عنصر الراتب إلى الكوماند
        /// </summary>
        /// <param name="command">الكوماند</param>
        /// <param name="component">بيانات العنصر</param>
        private void AddSalaryComponentParameters(SqlCommand command, SalaryComponent component)
        {
            command.Parameters.AddWithValue("@Name", component.Name);
            command.Parameters.AddWithValue("@Description", (object)component.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@Type", component.Type);
            command.Parameters.AddWithValue("@IsBasic", component.IsBasic);
            command.Parameters.AddWithValue("@IsVariable", component.IsVariable);
            command.Parameters.AddWithValue("@IsTaxable", component.IsTaxable);
            command.Parameters.AddWithValue("@AffectsNetSalary", component.AffectsNetSalary);
            command.Parameters.AddWithValue("@Position", (object)component.Position ?? DBNull.Value);
            command.Parameters.AddWithValue("@FormulaType", (object)component.FormulaType ?? DBNull.Value);
            command.Parameters.AddWithValue("@PercentageOf", (object)component.PercentageOf ?? DBNull.Value);
            command.Parameters.AddWithValue("@DefaultAmount", (object)component.DefaultAmount ?? DBNull.Value);
            command.Parameters.AddWithValue("@DefaultPercentage", (object)component.DefaultPercentage ?? DBNull.Value);
            command.Parameters.AddWithValue("@Formula", (object)component.Formula ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", component.IsActive);
            
            if (command.CommandText.Contains("INSERT"))
            {
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                command.Parameters.AddWithValue("@CreatedBy", (object)SessionManager.CurrentUser?.ID ?? DBNull.Value);
            }
        }
        
        /// <summary>
        /// تحويل من قارئ البيانات إلى كائن عنصر راتب
        /// </summary>
        /// <param name="reader">قارئ البيانات</param>
        /// <returns>كائن عنصر راتب</returns>
        private SalaryComponent MapSalaryComponentFromReader(SqlDataReader reader)
        {
            return new SalaryComponent
            {
                ID = Convert.ToInt32(reader["ID"]),
                Name = reader["Name"].ToString(),
                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                Type = reader["Type"].ToString(),
                IsBasic = Convert.ToBoolean(reader["IsBasic"]),
                IsVariable = Convert.ToBoolean(reader["IsVariable"]),
                IsTaxable = Convert.ToBoolean(reader["IsTaxable"]),
                AffectsNetSalary = Convert.ToBoolean(reader["AffectsNetSalary"]),
                Position = reader["Position"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Position"]) : null,
                FormulaType = reader["FormulaType"] != DBNull.Value ? reader["FormulaType"].ToString() : null,
                PercentageOf = reader["PercentageOf"] != DBNull.Value ? reader["PercentageOf"].ToString() : null,
                DefaultAmount = reader["DefaultAmount"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["DefaultAmount"]) : null,
                DefaultPercentage = reader["DefaultPercentage"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["DefaultPercentage"]) : null,
                Formula = reader["Formula"] != DBNull.Value ? reader["Formula"].ToString() : null,
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                CreatedBy = reader["CreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CreatedBy"]) : null,
                CreatedByUser = reader["CreatedByUser"] != DBNull.Value ? reader["CreatedByUser"].ToString() : null
            };
        }
        
        #endregion
        
        #region Employee Salary Methods
        
        /// <summary>
        /// الحصول على جميع عناصر رواتب الموظفين
        /// </summary>
        /// <returns>قائمة عناصر رواتب الموظفين</returns>
        public List<EmployeeSalary> GetAllEmployeeSalaries()
        {
            List<EmployeeSalary> salaries = new List<EmployeeSalary>();
            
            string query = @"
                SELECT 
                    es.ID, es.EmployeeID, e.FullName AS EmployeeName,
                    es.ComponentID, sc.Name AS ComponentName, sc.Type AS ComponentType,
                    es.EffectiveDate, es.EndDate, es.IsActive, es.Amount, es.Percentage, es.Notes,
                    es.CreatedAt, es.CreatedBy, u1.Username AS CreatedByUser,
                    es.UpdatedAt, es.UpdatedBy, u2.Username AS UpdatedByUser
                FROM EmployeeSalaries es
                JOIN Employees e ON es.EmployeeID = e.ID
                JOIN SalaryComponents sc ON es.ComponentID = sc.ID
                LEFT JOIN Users u1 ON es.CreatedBy = u1.ID
                LEFT JOIN Users u2 ON es.UpdatedBy = u2.ID
                ORDER BY e.FullName, es.EffectiveDate DESC";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salaries.Add(MapEmployeeSalaryFromReader(reader));
                        }
                    }
                }
            }
            
            return salaries;
        }
        
        /// <summary>
        /// الحصول على رواتب موظف
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <returns>قائمة عناصر راتب الموظف</returns>
        public List<EmployeeSalary> GetEmployeeSalaries(int employeeId)
        {
            List<EmployeeSalary> salaries = new List<EmployeeSalary>();
            
            string query = @"
                SELECT 
                    es.ID, es.EmployeeID, e.FullName AS EmployeeName,
                    es.ComponentID, sc.Name AS ComponentName, sc.Type AS ComponentType,
                    es.EffectiveDate, es.EndDate, es.IsActive, es.Amount, es.Percentage, es.Notes,
                    es.CreatedAt, es.CreatedBy, u1.Username AS CreatedByUser,
                    es.UpdatedAt, es.UpdatedBy, u2.Username AS UpdatedByUser
                FROM EmployeeSalaries es
                JOIN Employees e ON es.EmployeeID = e.ID
                JOIN SalaryComponents sc ON es.ComponentID = sc.ID
                LEFT JOIN Users u1 ON es.CreatedBy = u1.ID
                LEFT JOIN Users u2 ON es.UpdatedBy = u2.ID
                WHERE es.EmployeeID = @EmployeeID
                ORDER BY es.EffectiveDate DESC";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salaries.Add(MapEmployeeSalaryFromReader(reader));
                        }
                    }
                }
            }
            
            return salaries;
        }
        
        /// <summary>
        /// الحصول على عناصر راتب موظف النشطة في تاريخ محدد
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="date">التاريخ</param>
        /// <returns>قائمة عناصر راتب الموظف النشطة</returns>
        public List<EmployeeSalary> GetActiveEmployeeSalariesAtDate(int employeeId, DateTime date)
        {
            List<EmployeeSalary> salaries = new List<EmployeeSalary>();
            
            string query = @"
                SELECT 
                    es.ID, es.EmployeeID, e.FullName AS EmployeeName,
                    es.ComponentID, sc.Name AS ComponentName, sc.Type AS ComponentType,
                    es.EffectiveDate, es.EndDate, es.IsActive, es.Amount, es.Percentage, es.Notes,
                    es.CreatedAt, es.CreatedBy, u1.Username AS CreatedByUser,
                    es.UpdatedAt, es.UpdatedBy, u2.Username AS UpdatedByUser
                FROM EmployeeSalaries es
                JOIN Employees e ON es.EmployeeID = e.ID
                JOIN SalaryComponents sc ON es.ComponentID = sc.ID
                LEFT JOIN Users u1 ON es.CreatedBy = u1.ID
                LEFT JOIN Users u2 ON es.UpdatedBy = u2.ID
                WHERE es.EmployeeID = @EmployeeID
                  AND es.IsActive = 1
                  AND es.EffectiveDate <= @Date
                  AND (es.EndDate IS NULL OR es.EndDate >= @Date)
                ORDER BY sc.Position";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@Date", date.Date);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salaries.Add(MapEmployeeSalaryFromReader(reader));
                        }
                    }
                }
            }
            
            return salaries;
        }
        
        /// <summary>
        /// الحصول على عنصر راتب موظف بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف عنصر الراتب</param>
        /// <returns>عنصر راتب الموظف</returns>
        public EmployeeSalary GetEmployeeSalaryById(int id)
        {
            string query = @"
                SELECT 
                    es.ID, es.EmployeeID, e.FullName AS EmployeeName,
                    es.ComponentID, sc.Name AS ComponentName, sc.Type AS ComponentType,
                    es.EffectiveDate, es.EndDate, es.IsActive, es.Amount, es.Percentage, es.Notes,
                    es.CreatedAt, es.CreatedBy, u1.Username AS CreatedByUser,
                    es.UpdatedAt, es.UpdatedBy, u2.Username AS UpdatedByUser
                FROM EmployeeSalaries es
                JOIN Employees e ON es.EmployeeID = e.ID
                JOIN SalaryComponents sc ON es.ComponentID = sc.ID
                LEFT JOIN Users u1 ON es.CreatedBy = u1.ID
                LEFT JOIN Users u2 ON es.UpdatedBy = u2.ID
                WHERE es.ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapEmployeeSalaryFromReader(reader);
                        }
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// إضافة عنصر راتب موظف جديد
        /// </summary>
        /// <param name="salary">بيانات عنصر الراتب</param>
        /// <returns>معرف عنصر الراتب الجديد</returns>
        public int AddEmployeeSalary(EmployeeSalary salary)
        {
            // نتأكد أولاً من إلغاء تفعيل أي عنصر سابق من نفس النوع في حالة عنصر الراتب الأساسي
            if (salary.ComponentType == "Basic")
            {
                DeactivatePreviousBasicSalaryComponents(salary.EmployeeID, salary.EffectiveDate);
            }
            
            string query = @"
                INSERT INTO EmployeeSalaries (
                    EmployeeID, ComponentID, EffectiveDate, EndDate, IsActive,
                    Amount, Percentage, Notes, CreatedAt, CreatedBy
                ) 
                VALUES (
                    @EmployeeID, @ComponentID, @EffectiveDate, @EndDate, @IsActive,
                    @Amount, @Percentage, @Notes, @CreatedAt, @CreatedBy
                );
                SELECT SCOPE_IDENTITY();";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddEmployeeSalaryParameters(command, salary);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// تحديث عنصر راتب موظف
        /// </summary>
        /// <param name="salary">بيانات عنصر الراتب</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateEmployeeSalary(EmployeeSalary salary)
        {
            string query = @"
                UPDATE EmployeeSalaries
                SET 
                    EmployeeID = @EmployeeID,
                    ComponentID = @ComponentID,
                    EffectiveDate = @EffectiveDate,
                    EndDate = @EndDate,
                    IsActive = @IsActive,
                    Amount = @Amount,
                    Percentage = @Percentage,
                    Notes = @Notes,
                    UpdatedAt = @UpdatedAt,
                    UpdatedBy = @UpdatedBy
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", salary.ID);
                    AddEmployeeSalaryParameters(command, salary);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedBy", (object)SessionManager.CurrentUser?.ID ?? DBNull.Value);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// إلغاء تفعيل عنصر راتب موظف
        /// </summary>
        /// <param name="id">معرف عنصر الراتب</param>
        /// <returns>نجاح العملية</returns>
        public bool DeactivateEmployeeSalary(int id)
        {
            string query = @"
                UPDATE EmployeeSalaries
                SET 
                    IsActive = 0,
                    EndDate = @EndDate,
                    UpdatedAt = @UpdatedAt,
                    UpdatedBy = @UpdatedBy
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@EndDate", DateTime.Now.Date);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedBy", (object)SessionManager.CurrentUser?.ID ?? DBNull.Value);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// إلغاء تفعيل عناصر الراتب الأساسي السابقة للموظف
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="effectiveDate">تاريخ بدء التفعيل</param>
        private void DeactivatePreviousBasicSalaryComponents(int employeeId, DateTime effectiveDate)
        {
            string query = @"
                UPDATE EmployeeSalaries
                SET 
                    IsActive = 0,
                    EndDate = @EndDate,
                    UpdatedAt = @UpdatedAt,
                    UpdatedBy = @UpdatedBy
                FROM EmployeeSalaries es
                JOIN SalaryComponents sc ON es.ComponentID = sc.ID
                WHERE es.EmployeeID = @EmployeeID
                  AND sc.IsBasic = 1
                  AND es.IsActive = 1
                  AND es.EffectiveDate < @EffectiveDate";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@EffectiveDate", effectiveDate.Date);
                    command.Parameters.AddWithValue("@EndDate", effectiveDate.AddDays(-1).Date);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedBy", (object)SessionManager.CurrentUser?.ID ?? DBNull.Value);
                    
                    connection.Open();
                    
                    command.ExecuteNonQuery();
                }
            }
        }
        
        /// <summary>
        /// احتساب الراتب الأساسي للموظف في تاريخ محدد
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="date">التاريخ</param>
        /// <returns>الراتب الأساسي</returns>
        public decimal GetEmployeeBasicSalaryAtDate(int employeeId, DateTime date)
        {
            string query = @"
                SELECT es.Amount
                FROM EmployeeSalaries es
                JOIN SalaryComponents sc ON es.ComponentID = sc.ID
                WHERE es.EmployeeID = @EmployeeID
                  AND sc.IsBasic = 1
                  AND es.IsActive = 1
                  AND es.EffectiveDate <= @Date
                  AND (es.EndDate IS NULL OR es.EndDate >= @Date)";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@Date", date.Date);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToDecimal(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// إضافة بارامترات عنصر راتب موظف إلى الكوماند
        /// </summary>
        /// <param name="command">الكوماند</param>
        /// <param name="salary">بيانات عنصر الراتب</param>
        private void AddEmployeeSalaryParameters(SqlCommand command, EmployeeSalary salary)
        {
            command.Parameters.AddWithValue("@EmployeeID", salary.EmployeeID);
            command.Parameters.AddWithValue("@ComponentID", salary.ComponentID);
            command.Parameters.AddWithValue("@EffectiveDate", salary.EffectiveDate);
            command.Parameters.AddWithValue("@EndDate", (object)salary.EndDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", salary.IsActive);
            command.Parameters.AddWithValue("@Amount", (object)salary.Amount ?? DBNull.Value);
            command.Parameters.AddWithValue("@Percentage", (object)salary.Percentage ?? DBNull.Value);
            command.Parameters.AddWithValue("@Notes", (object)salary.Notes ?? DBNull.Value);
            
            if (command.CommandText.Contains("INSERT"))
            {
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                command.Parameters.AddWithValue("@CreatedBy", (object)SessionManager.CurrentUser?.ID ?? DBNull.Value);
            }
        }
        
        /// <summary>
        /// تحويل من قارئ البيانات إلى كائن عنصر راتب موظف
        /// </summary>
        /// <param name="reader">قارئ البيانات</param>
        /// <returns>كائن عنصر راتب موظف</returns>
        private EmployeeSalary MapEmployeeSalaryFromReader(SqlDataReader reader)
        {
            return new EmployeeSalary
            {
                ID = Convert.ToInt32(reader["ID"]),
                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                EmployeeName = reader["EmployeeName"].ToString(),
                ComponentID = Convert.ToInt32(reader["ComponentID"]),
                ComponentName = reader["ComponentName"].ToString(),
                ComponentType = reader["ComponentType"].ToString(),
                EffectiveDate = Convert.ToDateTime(reader["EffectiveDate"]),
                EndDate = reader["EndDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["EndDate"]) : null,
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                Amount = reader["Amount"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["Amount"]) : null,
                Percentage = reader["Percentage"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["Percentage"]) : null,
                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                CreatedBy = reader["CreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CreatedBy"]) : null,
                CreatedByUser = reader["CreatedByUser"] != DBNull.Value ? reader["CreatedByUser"].ToString() : null,
                UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["UpdatedAt"]) : null,
                UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["UpdatedBy"]) : null,
                UpdatedByUser = reader["UpdatedByUser"] != DBNull.Value ? reader["UpdatedByUser"].ToString() : null
            };
        }
        
        #endregion
    }
}