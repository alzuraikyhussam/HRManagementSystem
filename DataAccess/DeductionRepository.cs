using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// مستودع بيانات الخصومات
    /// </summary>
    public class DeductionRepository
    {
        private readonly ConnectionManager _connectionManager;
        
        /// <summary>
        /// إنشاء مستودع بيانات الخصومات
        /// </summary>
        public DeductionRepository()
        {
            _connectionManager = ConnectionManager.Instance;
        }
        
        #region Deduction Rules Methods
        
        /// <summary>
        /// الحصول على جميع قواعد الخصم
        /// </summary>
        /// <returns>قائمة قواعد الخصم</returns>
        public List<DeductionRule> GetAllDeductionRules()
        {
            List<DeductionRule> rules = new List<DeductionRule>();
            
            string query = @"
                SELECT 
                    dr.ID, dr.Name, dr.Description, dr.Type, dr.DeductionMethod, dr.DeductionValue,
                    dr.AppliesTo, dr.DepartmentID, d.Name AS DepartmentName, dr.PositionID, 
                    p.Name AS PositionName, dr.MinViolation, dr.MaxViolation, dr.ActivationDate, 
                    dr.IsActive, dr.Notes, dr.CreatedAt, dr.CreatedBy, u.Username AS CreatedByUser
                FROM DeductionRules dr
                LEFT JOIN Departments d ON dr.DepartmentID = d.ID
                LEFT JOIN Positions p ON dr.PositionID = p.ID
                LEFT JOIN Users u ON dr.CreatedBy = u.ID
                ORDER BY dr.IsActive DESC, dr.Name";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rules.Add(MapDeductionRuleFromReader(reader));
                        }
                    }
                }
            }
            
            return rules;
        }
        
        /// <summary>
        /// الحصول على قواعد الخصم النشطة
        /// </summary>
        /// <returns>قائمة قواعد الخصم النشطة</returns>
        public List<DeductionRule> GetActiveDeductionRules()
        {
            List<DeductionRule> rules = new List<DeductionRule>();
            
            string query = @"
                SELECT 
                    dr.ID, dr.Name, dr.Description, dr.Type, dr.DeductionMethod, dr.DeductionValue,
                    dr.AppliesTo, dr.DepartmentID, d.Name AS DepartmentName, dr.PositionID, 
                    p.Name AS PositionName, dr.MinViolation, dr.MaxViolation, dr.ActivationDate, 
                    dr.IsActive, dr.Notes, dr.CreatedAt, dr.CreatedBy, u.Username AS CreatedByUser
                FROM DeductionRules dr
                LEFT JOIN Departments d ON dr.DepartmentID = d.ID
                LEFT JOIN Positions p ON dr.PositionID = p.ID
                LEFT JOIN Users u ON dr.CreatedBy = u.ID
                WHERE dr.IsActive = 1
                ORDER BY dr.Name";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rules.Add(MapDeductionRuleFromReader(reader));
                        }
                    }
                }
            }
            
            return rules;
        }
        
        /// <summary>
        /// الحصول على قاعدة خصم بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف القاعدة</param>
        /// <returns>قاعدة الخصم</returns>
        public DeductionRule GetDeductionRuleById(int id)
        {
            string query = @"
                SELECT 
                    dr.ID, dr.Name, dr.Description, dr.Type, dr.DeductionMethod, dr.DeductionValue,
                    dr.AppliesTo, dr.DepartmentID, d.Name AS DepartmentName, dr.PositionID, 
                    p.Name AS PositionName, dr.MinViolation, dr.MaxViolation, dr.ActivationDate, 
                    dr.IsActive, dr.Notes, dr.CreatedAt, dr.CreatedBy, u.Username AS CreatedByUser
                FROM DeductionRules dr
                LEFT JOIN Departments d ON dr.DepartmentID = d.ID
                LEFT JOIN Positions p ON dr.PositionID = p.ID
                LEFT JOIN Users u ON dr.CreatedBy = u.ID
                WHERE dr.ID = @ID";
            
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
                            return MapDeductionRuleFromReader(reader);
                        }
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// الحصول على قواعد الخصم المطبقة على موظف معين
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <returns>قائمة قواعد الخصم</returns>
        public List<DeductionRule> GetDeductionRulesForEmployee(int employeeId)
        {
            List<DeductionRule> rules = new List<DeductionRule>();
            
            // الحصول على بيانات الموظف
            var employee = new EmployeeRepository().GetEmployeeById(employeeId);
            
            if (employee == null)
                return rules;
            
            string query = @"
                SELECT 
                    dr.ID, dr.Name, dr.Description, dr.Type, dr.DeductionMethod, dr.DeductionValue,
                    dr.AppliesTo, dr.DepartmentID, d.Name AS DepartmentName, dr.PositionID, 
                    p.Name AS PositionName, dr.MinViolation, dr.MaxViolation, dr.ActivationDate, 
                    dr.IsActive, dr.Notes, dr.CreatedAt, dr.CreatedBy, u.Username AS CreatedByUser
                FROM DeductionRules dr
                LEFT JOIN Departments d ON dr.DepartmentID = d.ID
                LEFT JOIN Positions p ON dr.PositionID = p.ID
                LEFT JOIN Users u ON dr.CreatedBy = u.ID
                WHERE dr.IsActive = 1
                  AND (
                    dr.AppliesTo = 'All' OR
                    (dr.AppliesTo = 'Department' AND dr.DepartmentID = @DepartmentID) OR
                    (dr.AppliesTo = 'Position' AND dr.PositionID = @PositionID)
                  )
                ORDER BY dr.Name";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DepartmentID", (object)employee.DepartmentID ?? DBNull.Value);
                    command.Parameters.AddWithValue("@PositionID", (object)employee.PositionID ?? DBNull.Value);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rules.Add(MapDeductionRuleFromReader(reader));
                        }
                    }
                }
            }
            
            return rules;
        }
        
        /// <summary>
        /// إضافة قاعدة خصم جديدة
        /// </summary>
        /// <param name="rule">بيانات القاعدة</param>
        /// <returns>معرف القاعدة الجديدة</returns>
        public int AddDeductionRule(DeductionRule rule)
        {
            string query = @"
                INSERT INTO DeductionRules (
                    Name, Description, Type, DeductionMethod, DeductionValue,
                    AppliesTo, DepartmentID, PositionID, MinViolation, MaxViolation,
                    ActivationDate, IsActive, Notes, CreatedAt, CreatedBy
                ) 
                VALUES (
                    @Name, @Description, @Type, @DeductionMethod, @DeductionValue,
                    @AppliesTo, @DepartmentID, @PositionID, @MinViolation, @MaxViolation,
                    @ActivationDate, @IsActive, @Notes, @CreatedAt, @CreatedBy
                );
                SELECT SCOPE_IDENTITY();";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddDeductionRuleParameters(command, rule);
                    
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
        /// تحديث قاعدة خصم
        /// </summary>
        /// <param name="rule">بيانات القاعدة</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateDeductionRule(DeductionRule rule)
        {
            string query = @"
                UPDATE DeductionRules
                SET 
                    Name = @Name,
                    Description = @Description,
                    Type = @Type,
                    DeductionMethod = @DeductionMethod,
                    DeductionValue = @DeductionValue,
                    AppliesTo = @AppliesTo,
                    DepartmentID = @DepartmentID,
                    PositionID = @PositionID,
                    MinViolation = @MinViolation,
                    MaxViolation = @MaxViolation,
                    ActivationDate = @ActivationDate,
                    IsActive = @IsActive,
                    Notes = @Notes
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", rule.ID);
                    AddDeductionRuleParameters(command, rule);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// حذف قاعدة خصم
        /// </summary>
        /// <param name="id">معرف القاعدة</param>
        /// <returns>نجاح العملية</returns>
        public bool DeleteDeductionRule(int id)
        {
            // التحقق من استخدام القاعدة في أي خصومات
            bool isUsed = IsDeductionRuleUsed(id);
            
            if (isUsed)
            {
                // إذا كانت القاعدة مستخدمة، نقوم بإلغاء تفعيلها بدلاً من حذفها
                string deactivateQuery = "UPDATE DeductionRules SET IsActive = 0 WHERE ID = @ID";
                
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(deactivateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        
                        connection.Open();
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        
                        return rowsAffected > 0;
                    }
                }
            }
            else
            {
                // إذا لم تكن القاعدة مستخدمة، يمكن حذفها
                string deleteQuery = "DELETE FROM DeductionRules WHERE ID = @ID";
                
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        
                        connection.Open();
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        
                        return rowsAffected > 0;
                    }
                }
            }
        }
        
        /// <summary>
        /// التحقق مما إذا كانت قاعدة الخصم مستخدمة في أي خصومات
        /// </summary>
        /// <param name="ruleId">معرف القاعدة</param>
        /// <returns>نتيجة التحقق</returns>
        private bool IsDeductionRuleUsed(int ruleId)
        {
            string query = "SELECT COUNT(*) FROM Deductions WHERE DeductionRuleID = @RuleID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RuleID", ruleId);
                    
                    connection.Open();
                    
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    
                    return count > 0;
                }
            }
        }
        
        /// <summary>
        /// إضافة بارامترات قاعدة الخصم إلى الكوماند
        /// </summary>
        /// <param name="command">الكوماند</param>
        /// <param name="rule">بيانات القاعدة</param>
        private void AddDeductionRuleParameters(SqlCommand command, DeductionRule rule)
        {
            command.Parameters.AddWithValue("@Name", rule.Name);
            command.Parameters.AddWithValue("@Description", (object)rule.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@Type", rule.Type);
            command.Parameters.AddWithValue("@DeductionMethod", rule.DeductionMethod);
            command.Parameters.AddWithValue("@DeductionValue", rule.DeductionValue);
            command.Parameters.AddWithValue("@AppliesTo", rule.AppliesTo);
            command.Parameters.AddWithValue("@DepartmentID", (object)rule.DepartmentID ?? DBNull.Value);
            command.Parameters.AddWithValue("@PositionID", (object)rule.PositionID ?? DBNull.Value);
            command.Parameters.AddWithValue("@MinViolation", (object)rule.MinViolation ?? DBNull.Value);
            command.Parameters.AddWithValue("@MaxViolation", (object)rule.MaxViolation ?? DBNull.Value);
            command.Parameters.AddWithValue("@ActivationDate", (object)rule.ActivationDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", rule.IsActive);
            command.Parameters.AddWithValue("@Notes", (object)rule.Notes ?? DBNull.Value);
            
            if (command.CommandText.Contains("INSERT"))
            {
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                command.Parameters.AddWithValue("@CreatedBy", (object)SessionManager.CurrentUser?.ID ?? DBNull.Value);
            }
        }
        
        /// <summary>
        /// تحويل من قارئ البيانات إلى كائن قاعدة خصم
        /// </summary>
        /// <param name="reader">قارئ البيانات</param>
        /// <returns>كائن قاعدة خصم</returns>
        private DeductionRule MapDeductionRuleFromReader(SqlDataReader reader)
        {
            return new DeductionRule
            {
                ID = Convert.ToInt32(reader["ID"]),
                Name = reader["Name"].ToString(),
                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                Type = reader["Type"].ToString(),
                DeductionMethod = reader["DeductionMethod"].ToString(),
                DeductionValue = Convert.ToDecimal(reader["DeductionValue"]),
                AppliesTo = reader["AppliesTo"].ToString(),
                DepartmentID = reader["DepartmentID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["DepartmentID"]) : null,
                DepartmentName = reader["DepartmentName"] != DBNull.Value ? reader["DepartmentName"].ToString() : null,
                PositionID = reader["PositionID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["PositionID"]) : null,
                PositionName = reader["PositionName"] != DBNull.Value ? reader["PositionName"].ToString() : null,
                MinViolation = reader["MinViolation"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["MinViolation"]) : null,
                MaxViolation = reader["MaxViolation"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["MaxViolation"]) : null,
                ActivationDate = reader["ActivationDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ActivationDate"]) : null,
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                CreatedBy = reader["CreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CreatedBy"]) : null,
                CreatedByUser = reader["CreatedByUser"] != DBNull.Value ? reader["CreatedByUser"].ToString() : null
            };
        }
        
        #endregion
        
        #region Deductions Methods
        
        /// <summary>
        /// الحصول على جميع الخصومات
        /// </summary>
        /// <returns>قائمة الخصومات</returns>
        public List<Deduction> GetAllDeductions()
        {
            List<Deduction> deductions = new List<Deduction>();
            
            string query = @"
                SELECT 
                    d.ID, d.EmployeeID, e.FullName AS EmployeeName, dept.Name AS DepartmentName,
                    d.DeductionRuleID, dr.Name AS DeductionRuleName, d.DeductionDate, d.ViolationDate,
                    d.ViolationType, d.ViolationValue, d.DeductionMethod, d.DeductionValue,
                    d.Description, d.Status, d.ApprovedBy, u1.Username AS ApprovedByUser,
                    d.ApprovalDate, d.IsPayrollProcessed, d.PayrollID, d.CreatedBy, 
                    u2.Username AS CreatedByUser
                FROM Deductions d
                LEFT JOIN Employees e ON d.EmployeeID = e.ID
                LEFT JOIN Departments dept ON e.DepartmentID = dept.ID
                LEFT JOIN DeductionRules dr ON d.DeductionRuleID = dr.ID
                LEFT JOIN Users u1 ON d.ApprovedBy = u1.ID
                LEFT JOIN Users u2 ON d.CreatedBy = u2.ID
                ORDER BY d.DeductionDate DESC, e.FullName";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            deductions.Add(MapDeductionFromReader(reader));
                        }
                    }
                }
            }
            
            return deductions;
        }
        
        /// <summary>
        /// الحصول على الخصومات في فترة زمنية محددة
        /// </summary>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <returns>قائمة الخصومات</returns>
        public List<Deduction> GetDeductionsInPeriod(DateTime startDate, DateTime endDate)
        {
            List<Deduction> deductions = new List<Deduction>();
            
            string query = @"
                SELECT 
                    d.ID, d.EmployeeID, e.FullName AS EmployeeName, dept.Name AS DepartmentName,
                    d.DeductionRuleID, dr.Name AS DeductionRuleName, d.DeductionDate, d.ViolationDate,
                    d.ViolationType, d.ViolationValue, d.DeductionMethod, d.DeductionValue,
                    d.Description, d.Status, d.ApprovedBy, u1.Username AS ApprovedByUser,
                    d.ApprovalDate, d.IsPayrollProcessed, d.PayrollID, d.CreatedBy, 
                    u2.Username AS CreatedByUser
                FROM Deductions d
                LEFT JOIN Employees e ON d.EmployeeID = e.ID
                LEFT JOIN Departments dept ON e.DepartmentID = dept.ID
                LEFT JOIN DeductionRules dr ON d.DeductionRuleID = dr.ID
                LEFT JOIN Users u1 ON d.ApprovedBy = u1.ID
                LEFT JOIN Users u2 ON d.CreatedBy = u2.ID
                WHERE d.DeductionDate BETWEEN @StartDate AND @EndDate
                ORDER BY d.DeductionDate DESC, e.FullName";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.Date);
                    command.Parameters.AddWithValue("@EndDate", endDate.Date);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            deductions.Add(MapDeductionFromReader(reader));
                        }
                    }
                }
            }
            
            return deductions;
        }
        
        /// <summary>
        /// الحصول على خصومات موظف
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <returns>قائمة الخصومات</returns>
        public List<Deduction> GetEmployeeDeductions(int employeeId)
        {
            List<Deduction> deductions = new List<Deduction>();
            
            string query = @"
                SELECT 
                    d.ID, d.EmployeeID, e.FullName AS EmployeeName, dept.Name AS DepartmentName,
                    d.DeductionRuleID, dr.Name AS DeductionRuleName, d.DeductionDate, d.ViolationDate,
                    d.ViolationType, d.ViolationValue, d.DeductionMethod, d.DeductionValue,
                    d.Description, d.Status, d.ApprovedBy, u1.Username AS ApprovedByUser,
                    d.ApprovalDate, d.IsPayrollProcessed, d.PayrollID, d.CreatedBy, 
                    u2.Username AS CreatedByUser
                FROM Deductions d
                LEFT JOIN Employees e ON d.EmployeeID = e.ID
                LEFT JOIN Departments dept ON e.DepartmentID = dept.ID
                LEFT JOIN DeductionRules dr ON d.DeductionRuleID = dr.ID
                LEFT JOIN Users u1 ON d.ApprovedBy = u1.ID
                LEFT JOIN Users u2 ON d.CreatedBy = u2.ID
                WHERE d.EmployeeID = @EmployeeID
                ORDER BY d.DeductionDate DESC";
            
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
                            deductions.Add(MapDeductionFromReader(reader));
                        }
                    }
                }
            }
            
            return deductions;
        }
        
        /// <summary>
        /// الحصول على خصومات موظف في فترة زمنية محددة
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <returns>قائمة الخصومات</returns>
        public List<Deduction> GetEmployeeDeductionsInPeriod(int employeeId, DateTime startDate, DateTime endDate)
        {
            List<Deduction> deductions = new List<Deduction>();
            
            string query = @"
                SELECT 
                    d.ID, d.EmployeeID, e.FullName AS EmployeeName, dept.Name AS DepartmentName,
                    d.DeductionRuleID, dr.Name AS DeductionRuleName, d.DeductionDate, d.ViolationDate,
                    d.ViolationType, d.ViolationValue, d.DeductionMethod, d.DeductionValue,
                    d.Description, d.Status, d.ApprovedBy, u1.Username AS ApprovedByUser,
                    d.ApprovalDate, d.IsPayrollProcessed, d.PayrollID, d.CreatedBy, 
                    u2.Username AS CreatedByUser
                FROM Deductions d
                LEFT JOIN Employees e ON d.EmployeeID = e.ID
                LEFT JOIN Departments dept ON e.DepartmentID = dept.ID
                LEFT JOIN DeductionRules dr ON d.DeductionRuleID = dr.ID
                LEFT JOIN Users u1 ON d.ApprovedBy = u1.ID
                LEFT JOIN Users u2 ON d.CreatedBy = u2.ID
                WHERE d.EmployeeID = @EmployeeID
                  AND d.DeductionDate BETWEEN @StartDate AND @EndDate
                ORDER BY d.DeductionDate DESC";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@StartDate", startDate.Date);
                    command.Parameters.AddWithValue("@EndDate", endDate.Date);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            deductions.Add(MapDeductionFromReader(reader));
                        }
                    }
                }
            }
            
            return deductions;
        }
        
        /// <summary>
        /// الحصول على خصم بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف الخصم</param>
        /// <returns>الخصم</returns>
        public Deduction GetDeductionById(int id)
        {
            string query = @"
                SELECT 
                    d.ID, d.EmployeeID, e.FullName AS EmployeeName, dept.Name AS DepartmentName,
                    d.DeductionRuleID, dr.Name AS DeductionRuleName, d.DeductionDate, d.ViolationDate,
                    d.ViolationType, d.ViolationValue, d.DeductionMethod, d.DeductionValue,
                    d.Description, d.Status, d.ApprovedBy, u1.Username AS ApprovedByUser,
                    d.ApprovalDate, d.IsPayrollProcessed, d.PayrollID, d.CreatedBy, 
                    u2.Username AS CreatedByUser
                FROM Deductions d
                LEFT JOIN Employees e ON d.EmployeeID = e.ID
                LEFT JOIN Departments dept ON e.DepartmentID = dept.ID
                LEFT JOIN DeductionRules dr ON d.DeductionRuleID = dr.ID
                LEFT JOIN Users u1 ON d.ApprovedBy = u1.ID
                LEFT JOIN Users u2 ON d.CreatedBy = u2.ID
                WHERE d.ID = @ID";
            
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
                            return MapDeductionFromReader(reader);
                        }
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// إضافة خصم جديد
        /// </summary>
        /// <param name="deduction">بيانات الخصم</param>
        /// <returns>معرف الخصم الجديد</returns>
        public int AddDeduction(Deduction deduction)
        {
            deduction.DeductionDate = DateTime.Now.Date;
            deduction.Status = "Pending";
            
            string query = @"
                INSERT INTO Deductions (
                    EmployeeID, DeductionRuleID, DeductionDate, ViolationDate,
                    ViolationType, ViolationValue, DeductionMethod, DeductionValue,
                    Description, Status, CreatedBy
                ) 
                VALUES (
                    @EmployeeID, @DeductionRuleID, @DeductionDate, @ViolationDate,
                    @ViolationType, @ViolationValue, @DeductionMethod, @DeductionValue,
                    @Description, @Status, @CreatedBy
                );
                SELECT SCOPE_IDENTITY();";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddDeductionParameters(command, deduction);
                    
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
        /// تحديث خصم
        /// </summary>
        /// <param name="deduction">بيانات الخصم</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateDeduction(Deduction deduction)
        {
            string query = @"
                UPDATE Deductions
                SET 
                    EmployeeID = @EmployeeID,
                    DeductionRuleID = @DeductionRuleID,
                    ViolationDate = @ViolationDate,
                    ViolationType = @ViolationType,
                    ViolationValue = @ViolationValue,
                    DeductionMethod = @DeductionMethod,
                    DeductionValue = @DeductionValue,
                    Description = @Description,
                    Status = @Status
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", deduction.ID);
                    AddDeductionParameters(command, deduction);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// حذف خصم
        /// </summary>
        /// <param name="id">معرف الخصم</param>
        /// <returns>نجاح العملية</returns>
        public bool DeleteDeduction(int id)
        {
            // التحقق مما إذا كان الخصم قد تمت معالجته في الرواتب
            var deduction = GetDeductionById(id);
            
            if (deduction == null)
                return false;
            
            if (deduction.IsPayrollProcessed)
            {
                // إذا تمت معالجة الخصم في الرواتب، نقوم بتغيير حالته إلى ملغي بدلاً من حذفه
                string cancelQuery = "UPDATE Deductions SET Status = 'Cancelled' WHERE ID = @ID";
                
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(cancelQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        
                        connection.Open();
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        
                        return rowsAffected > 0;
                    }
                }
            }
            else
            {
                // إذا لم تتم معالجة الخصم في الرواتب، يمكن حذفه
                string deleteQuery = "DELETE FROM Deductions WHERE ID = @ID";
                
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        
                        connection.Open();
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        
                        return rowsAffected > 0;
                    }
                }
            }
        }
        
        /// <summary>
        /// اعتماد خصم
        /// </summary>
        /// <param name="id">معرف الخصم</param>
        /// <param name="approvedBy">معرف المستخدم المعتمد</param>
        /// <returns>نجاح العملية</returns>
        public bool ApproveDeduction(int id, int approvedBy)
        {
            string query = @"
                UPDATE Deductions
                SET 
                    Status = 'Approved',
                    ApprovedBy = @ApprovedBy,
                    ApprovalDate = @ApprovalDate
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@ApprovedBy", approvedBy);
                    command.Parameters.AddWithValue("@ApprovalDate", DateTime.Now);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// رفض خصم
        /// </summary>
        /// <param name="id">معرف الخصم</param>
        /// <param name="approvedBy">معرف المستخدم الرافض</param>
        /// <returns>نجاح العملية</returns>
        public bool RejectDeduction(int id, int approvedBy)
        {
            string query = @"
                UPDATE Deductions
                SET 
                    Status = 'Rejected',
                    ApprovedBy = @ApprovedBy,
                    ApprovalDate = @ApprovalDate
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@ApprovedBy", approvedBy);
                    command.Parameters.AddWithValue("@ApprovalDate", DateTime.Now);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// احتساب الخصم التلقائي للتأخير
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="attendanceDate">تاريخ الحضور</param>
        /// <param name="lateMinutes">دقائق التأخير</param>
        /// <returns>معرف الخصم الجديد</returns>
        public int CalculateLateDeduction(int employeeId, DateTime attendanceDate, int lateMinutes)
        {
            if (lateMinutes <= 0)
                return 0;
            
            // البحث عن قاعدة خصم مناسبة للتأخير
            var rules = GetDeductionRulesForEmployee(employeeId);
            var lateRule = rules.FirstOrDefault(r => 
                r.Type == "Late" && 
                (!r.MinViolation.HasValue || lateMinutes >= r.MinViolation.Value) && 
                (!r.MaxViolation.HasValue || lateMinutes <= r.MaxViolation.Value));
            
            if (lateRule == null)
                return 0;
            
            // إنشاء خصم جديد
            var deduction = new Deduction
            {
                EmployeeID = employeeId,
                DeductionRuleID = lateRule.ID,
                ViolationDate = attendanceDate,
                ViolationType = "Late",
                ViolationValue = lateMinutes,
                DeductionMethod = lateRule.DeductionMethod,
                DeductionValue = lateRule.DeductionValue,
                Description = $"خصم بسبب تأخير {lateMinutes} دقيقة في تاريخ {attendanceDate:yyyy-MM-dd}",
                Status = "Pending",
                CreatedBy = SessionManager.CurrentUser?.ID
            };
            
            return AddDeduction(deduction);
        }
        
        /// <summary>
        /// احتساب الخصم التلقائي للغياب
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="attendanceDate">تاريخ الغياب</param>
        /// <returns>معرف الخصم الجديد</returns>
        public int CalculateAbsentDeduction(int employeeId, DateTime attendanceDate)
        {
            // البحث عن قاعدة خصم مناسبة للغياب
            var rules = GetDeductionRulesForEmployee(employeeId);
            var absentRule = rules.FirstOrDefault(r => r.Type == "Absent");
            
            if (absentRule == null)
                return 0;
            
            // إنشاء خصم جديد
            var deduction = new Deduction
            {
                EmployeeID = employeeId,
                DeductionRuleID = absentRule.ID,
                ViolationDate = attendanceDate,
                ViolationType = "Absent",
                DeductionMethod = absentRule.DeductionMethod,
                DeductionValue = absentRule.DeductionValue,
                Description = $"خصم بسبب غياب في تاريخ {attendanceDate:yyyy-MM-dd}",
                Status = "Pending",
                CreatedBy = SessionManager.CurrentUser?.ID
            };
            
            return AddDeduction(deduction);
        }
        
        /// <summary>
        /// إضافة بارامترات الخصم إلى الكوماند
        /// </summary>
        /// <param name="command">الكوماند</param>
        /// <param name="deduction">بيانات الخصم</param>
        private void AddDeductionParameters(SqlCommand command, Deduction deduction)
        {
            command.Parameters.AddWithValue("@EmployeeID", deduction.EmployeeID);
            command.Parameters.AddWithValue("@DeductionRuleID", (object)deduction.DeductionRuleID ?? DBNull.Value);
            command.Parameters.AddWithValue("@DeductionDate", deduction.DeductionDate);
            command.Parameters.AddWithValue("@ViolationDate", deduction.ViolationDate);
            command.Parameters.AddWithValue("@ViolationType", deduction.ViolationType);
            command.Parameters.AddWithValue("@ViolationValue", (object)deduction.ViolationValue ?? DBNull.Value);
            command.Parameters.AddWithValue("@DeductionMethod", deduction.DeductionMethod);
            command.Parameters.AddWithValue("@DeductionValue", deduction.DeductionValue);
            command.Parameters.AddWithValue("@Description", (object)deduction.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@Status", deduction.Status);
            
            if (command.CommandText.Contains("INSERT"))
            {
                command.Parameters.AddWithValue("@CreatedBy", (object)SessionManager.CurrentUser?.ID ?? DBNull.Value);
            }
        }
        
        /// <summary>
        /// تحويل من قارئ البيانات إلى كائن خصم
        /// </summary>
        /// <param name="reader">قارئ البيانات</param>
        /// <returns>كائن خصم</returns>
        private Deduction MapDeductionFromReader(SqlDataReader reader)
        {
            return new Deduction
            {
                ID = Convert.ToInt32(reader["ID"]),
                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                EmployeeName = reader["EmployeeName"] != DBNull.Value ? reader["EmployeeName"].ToString() : null,
                DepartmentName = reader["DepartmentName"] != DBNull.Value ? reader["DepartmentName"].ToString() : null,
                DeductionRuleID = reader["DeductionRuleID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["DeductionRuleID"]) : null,
                DeductionRuleName = reader["DeductionRuleName"] != DBNull.Value ? reader["DeductionRuleName"].ToString() : null,
                DeductionDate = Convert.ToDateTime(reader["DeductionDate"]),
                ViolationDate = Convert.ToDateTime(reader["ViolationDate"]),
                ViolationType = reader["ViolationType"].ToString(),
                ViolationValue = reader["ViolationValue"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["ViolationValue"]) : null,
                DeductionMethod = reader["DeductionMethod"].ToString(),
                DeductionValue = Convert.ToDecimal(reader["DeductionValue"]),
                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                Status = reader["Status"].ToString(),
                ApprovedBy = reader["ApprovedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ApprovedBy"]) : null,
                ApprovedByUser = reader["ApprovedByUser"] != DBNull.Value ? reader["ApprovedByUser"].ToString() : null,
                ApprovalDate = reader["ApprovalDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ApprovalDate"]) : null,
                IsPayrollProcessed = reader["IsPayrollProcessed"] != DBNull.Value && Convert.ToBoolean(reader["IsPayrollProcessed"]),
                PayrollID = reader["PayrollID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["PayrollID"]) : null,
                CreatedBy = reader["CreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CreatedBy"]) : null,
                CreatedByUser = reader["CreatedByUser"] != DBNull.Value ? reader["CreatedByUser"].ToString() : null
            };
        }
        
        #endregion
    }
}