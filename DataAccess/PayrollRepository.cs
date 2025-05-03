using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// مستودع بيانات الرواتب
    /// </summary>
    public class PayrollRepository
    {
        private readonly ConnectionManager _connectionManager;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly SalaryRepository _salaryRepository;
        private readonly DeductionRepository _deductionRepository;
        
        /// <summary>
        /// إنشاء مستودع بيانات الرواتب
        /// </summary>
        public PayrollRepository()
        {
            _connectionManager = ConnectionManager.Instance;
            _attendanceRepository = new AttendanceRepository();
            _salaryRepository = new SalaryRepository();
            _deductionRepository = new DeductionRepository();
        }
        
        #region Payroll Methods
        
        /// <summary>
        /// الحصول على جميع كشوف الرواتب
        /// </summary>
        /// <returns>قائمة كشوف الرواتب</returns>
        public List<Payroll> GetAllPayrolls()
        {
            List<Payroll> payrolls = new List<Payroll>();
            
            string query = @"
                SELECT 
                    p.ID, p.PayrollName, p.PayrollPeriod, p.PayrollType, p.PayrollMonth, p.PayrollYear,
                    p.StartDate, p.EndDate, p.PaymentDate, p.TotalEmployees, p.TotalBasicSalary,
                    p.TotalAllowances, p.TotalDeductions, p.TotalOvertimeAmount, p.TotalPayrollAmount,
                    p.Notes, p.Status, p.IsClosed, p.CreatedBy, u1.Username AS CreatedByUser,
                    p.CreatedAt, p.ApprovedBy, u2.Username AS ApprovedByUser, p.ApprovalDate
                FROM Payrolls p
                LEFT JOIN Users u1 ON p.CreatedBy = u1.ID
                LEFT JOIN Users u2 ON p.ApprovedBy = u2.ID
                ORDER BY p.PayrollYear DESC, p.PayrollMonth DESC";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payrolls.Add(MapPayrollFromReader(reader));
                        }
                    }
                }
            }
            
            return payrolls;
        }
        
        /// <summary>
        /// الحصول على كشوف الرواتب لشهر وسنة محددين
        /// </summary>
        /// <param name="month">الشهر</param>
        /// <param name="year">السنة</param>
        /// <returns>قائمة كشوف الرواتب</returns>
        public List<Payroll> GetPayrollsByMonthAndYear(int month, int year)
        {
            List<Payroll> payrolls = new List<Payroll>();
            
            string query = @"
                SELECT 
                    p.ID, p.PayrollName, p.PayrollPeriod, p.PayrollType, p.PayrollMonth, p.PayrollYear,
                    p.StartDate, p.EndDate, p.PaymentDate, p.TotalEmployees, p.TotalBasicSalary,
                    p.TotalAllowances, p.TotalDeductions, p.TotalOvertimeAmount, p.TotalPayrollAmount,
                    p.Notes, p.Status, p.IsClosed, p.CreatedBy, u1.Username AS CreatedByUser,
                    p.CreatedAt, p.ApprovedBy, u2.Username AS ApprovedByUser, p.ApprovalDate
                FROM Payrolls p
                LEFT JOIN Users u1 ON p.CreatedBy = u1.ID
                LEFT JOIN Users u2 ON p.ApprovedBy = u2.ID
                WHERE p.PayrollMonth = @Month AND p.PayrollYear = @Year
                ORDER BY p.CreatedAt DESC";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Month", month);
                    command.Parameters.AddWithValue("@Year", year);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payrolls.Add(MapPayrollFromReader(reader));
                        }
                    }
                }
            }
            
            return payrolls;
        }
        
        /// <summary>
        /// الحصول على كشف رواتب بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف كشف الرواتب</param>
        /// <returns>كشف الرواتب</returns>
        public Payroll GetPayrollById(int id)
        {
            string query = @"
                SELECT 
                    p.ID, p.PayrollName, p.PayrollPeriod, p.PayrollType, p.PayrollMonth, p.PayrollYear,
                    p.StartDate, p.EndDate, p.PaymentDate, p.TotalEmployees, p.TotalBasicSalary,
                    p.TotalAllowances, p.TotalDeductions, p.TotalOvertimeAmount, p.TotalPayrollAmount,
                    p.Notes, p.Status, p.IsClosed, p.CreatedBy, u1.Username AS CreatedByUser,
                    p.CreatedAt, p.ApprovedBy, u2.Username AS ApprovedByUser, p.ApprovalDate
                FROM Payrolls p
                LEFT JOIN Users u1 ON p.CreatedBy = u1.ID
                LEFT JOIN Users u2 ON p.ApprovedBy = u2.ID
                WHERE p.ID = @ID";
            
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
                            Payroll payroll = MapPayrollFromReader(reader);
                            
                            // الحصول على تفاصيل الرواتب
                            payroll.PayrollDetails = GetPayrollDetails(id);
                            
                            return payroll;
                        }
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// إنشاء كشف رواتب جديد
        /// </summary>
        /// <param name="month">الشهر</param>
        /// <param name="year">السنة</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <returns>معرف كشف الرواتب الجديد</returns>
        public int CreatePayroll(int month, int year, DateTime startDate, DateTime endDate)
        {
            // التحقق من عدم وجود كشف رواتب للشهر والسنة
            if (PayrollExistsForMonthAndYear(month, year))
            {
                throw new Exception($"يوجد بالفعل كشف رواتب لشهر {GetMonthName(month)} {year}");
            }
            
            string payrollPeriod = $"{month:D2}/{year}";
            string payrollName = $"رواتب {GetMonthName(month)} {year}";
            
            string query = @"
                INSERT INTO Payrolls (
                    PayrollName, PayrollPeriod, PayrollType, PayrollMonth, PayrollYear,
                    StartDate, EndDate, TotalEmployees, TotalBasicSalary, TotalAllowances,
                    TotalDeductions, TotalOvertimeAmount, TotalPayrollAmount, Status, IsClosed,
                    CreatedBy, CreatedAt
                ) 
                VALUES (
                    @PayrollName, @PayrollPeriod, @PayrollType, @PayrollMonth, @PayrollYear,
                    @StartDate, @EndDate, 0, 0, 0, 0, 0, 0, 'Created', 0, @CreatedBy, @CreatedAt
                );
                SELECT SCOPE_IDENTITY();";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PayrollName", payrollName);
                    command.Parameters.AddWithValue("@PayrollPeriod", payrollPeriod);
                    command.Parameters.AddWithValue("@PayrollType", "Monthly");
                    command.Parameters.AddWithValue("@PayrollMonth", month);
                    command.Parameters.AddWithValue("@PayrollYear", year);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@CreatedBy", (object)SessionManager.CurrentUser?.ID ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    
                    if (result != null && result != DBNull.Value)
                    {
                        int payrollId = Convert.ToInt32(result);
                        
                        // إنشاء تفاصيل الرواتب للموظفين
                        CreatePayrollDetails(payrollId, startDate, endDate);
                        
                        // تحديث إجماليات كشف الرواتب
                        UpdatePayrollTotals(payrollId);
                        
                        return payrollId;
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// التحقق من وجود كشف رواتب لشهر وسنة محددين
        /// </summary>
        /// <param name="month">الشهر</param>
        /// <param name="year">السنة</param>
        /// <returns>نتيجة التحقق</returns>
        public bool PayrollExistsForMonthAndYear(int month, int year)
        {
            string query = @"
                SELECT COUNT(*)
                FROM Payrolls
                WHERE PayrollMonth = @Month AND PayrollYear = @Year
                  AND Status <> 'Cancelled'";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Month", month);
                    command.Parameters.AddWithValue("@Year", year);
                    
                    connection.Open();
                    
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    
                    return count > 0;
                }
            }
        }
        
        /// <summary>
        /// إنشاء تفاصيل كشف الرواتب للموظفين
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        private void CreatePayrollDetails(int payrollId, DateTime startDate, DateTime endDate)
        {
            // الحصول على قائمة الموظفين النشطين
            EmployeeRepository employeeRepository = new EmployeeRepository();
            var employees = employeeRepository.GetActiveEmployees();
            
            foreach (var employee in employees)
            {
                // إنشاء سجل تفاصيل راتب للموظف
                CreateEmployeePayrollDetail(payrollId, employee.ID, startDate, endDate);
            }
        }
        
        /// <summary>
        /// إنشاء سجل تفاصيل راتب لموظف
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <returns>معرف سجل التفاصيل الجديد</returns>
        private int CreateEmployeePayrollDetail(int payrollId, int employeeId, DateTime startDate, DateTime endDate)
        {
            // احتساب بيانات الحضور للموظف
            int workingDays = GetWorkingDaysInPeriod(startDate, endDate);
            int presentDays = GetPresentDaysInPeriod(employeeId, startDate, endDate);
            int absentDays = workingDays - presentDays;
            int leaveDays = GetLeaveDaysInPeriod(employeeId, startDate, endDate);
            int lateMinutes = GetLateMinutesInPeriod(employeeId, startDate, endDate);
            decimal overtimeHours = GetOvertimeHoursInPeriod(employeeId, startDate, endDate);
            
            // احتساب الراتب الأساسي للموظف في نهاية الفترة
            decimal baseSalary = _salaryRepository.GetEmployeeBasicSalaryAtDate(employeeId, endDate);
            
            // احتساب البدلات والمستحقات
            decimal allowancesTotal = CalculateEmployeeAllowances(employeeId, endDate, baseSalary);
            
            // احتساب قيمة العمل الإضافي
            decimal overtimeRate = 1.5m; // معدل ساعة العمل الإضافي (1.5 ضعف ساعة العمل العادية)
            decimal hourlyRate = baseSalary / (workingDays * 8); // معدل ساعة العمل العادية
            decimal overtimeAmount = overtimeHours * hourlyRate * overtimeRate;
            
            // احتساب الخصومات والاستقطاعات
            decimal deductionsTotal = CalculateEmployeeDeductions(employeeId, startDate, endDate, baseSalary);
            
            // احتساب صافي الراتب
            decimal netSalary = baseSalary + allowancesTotal + overtimeAmount - deductionsTotal;
            
            string query = @"
                INSERT INTO PayrollDetails (
                    PayrollID, EmployeeID, WorkingDays, PresentDays, AbsentDays, LeaveDays,
                    LateMinutes, OvertimeHours, BaseSalary, TotalAllowances, TotalDeductions,
                    OvertimeAmount, NetSalary, Status
                ) 
                VALUES (
                    @PayrollID, @EmployeeID, @WorkingDays, @PresentDays, @AbsentDays, @LeaveDays,
                    @LateMinutes, @OvertimeHours, @BaseSalary, @TotalAllowances, @TotalDeductions,
                    @OvertimeAmount, @NetSalary, 'Calculated'
                );
                SELECT SCOPE_IDENTITY();";
            
            int payrollDetailId = 0;
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PayrollID", payrollId);
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@WorkingDays", workingDays);
                    command.Parameters.AddWithValue("@PresentDays", presentDays);
                    command.Parameters.AddWithValue("@AbsentDays", absentDays);
                    command.Parameters.AddWithValue("@LeaveDays", leaveDays);
                    command.Parameters.AddWithValue("@LateMinutes", lateMinutes);
                    command.Parameters.AddWithValue("@OvertimeHours", overtimeHours);
                    command.Parameters.AddWithValue("@BaseSalary", baseSalary);
                    command.Parameters.AddWithValue("@TotalAllowances", allowancesTotal);
                    command.Parameters.AddWithValue("@TotalDeductions", deductionsTotal);
                    command.Parameters.AddWithValue("@OvertimeAmount", overtimeAmount);
                    command.Parameters.AddWithValue("@NetSalary", netSalary);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    
                    if (result != null && result != DBNull.Value)
                    {
                        payrollDetailId = Convert.ToInt32(result);
                        
                        // إنشاء تفاصيل مكونات الراتب
                        CreateEmployeePayrollComponentDetails(payrollDetailId, employeeId, endDate, baseSalary);
                    }
                }
            }
            
            return payrollDetailId;
        }
        
        /// <summary>
        /// إنشاء تفاصيل مكونات راتب الموظف
        /// </summary>
        /// <param name="payrollDetailId">معرف تفاصيل كشف الراتب</param>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="calculationDate">تاريخ الاحتساب</param>
        /// <param name="baseSalary">الراتب الأساسي</param>
        private void CreateEmployeePayrollComponentDetails(int payrollDetailId, int employeeId, DateTime calculationDate, decimal baseSalary)
        {
            // الحصول على عناصر راتب الموظف النشطة
            var salaryComponents = _salaryRepository.GetActiveEmployeeSalariesAtDate(employeeId, calculationDate);
            
            // إنشاء تفاصيل لكل عنصر
            foreach (var component in salaryComponents)
            {
                // احتساب قيمة العنصر
                decimal amount = CalculateComponentAmount(component, baseSalary);
                
                // إنشاء سجل تفاصيل عنصر
                CreatePayrollComponentDetail(payrollDetailId, component.ComponentID, component.ComponentName, component.ComponentType, amount, null);
            }
        }
        
        /// <summary>
        /// إنشاء سجل تفاصيل عنصر راتب
        /// </summary>
        /// <param name="payrollDetailId">معرف تفاصيل كشف الراتب</param>
        /// <param name="componentId">معرف عنصر الراتب</param>
        /// <param name="componentName">اسم عنصر الراتب</param>
        /// <param name="componentType">نوع عنصر الراتب</param>
        /// <param name="amount">المبلغ</param>
        /// <param name="formula">المعادلة المستخدمة</param>
        /// <returns>معرف سجل التفاصيل الجديد</returns>
        private int CreatePayrollComponentDetail(int payrollDetailId, int componentId, string componentName, string componentType, decimal amount, string formula)
        {
            string query = @"
                INSERT INTO PayrollComponentDetails (
                    PayrollDetailID, ComponentID, ComponentName, ComponentType, Amount, Formula, CreatedAt
                ) 
                VALUES (
                    @PayrollDetailID, @ComponentID, @ComponentName, @ComponentType, @Amount, @Formula, @CreatedAt
                );
                SELECT SCOPE_IDENTITY();";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PayrollDetailID", payrollDetailId);
                    command.Parameters.AddWithValue("@ComponentID", componentId);
                    command.Parameters.AddWithValue("@ComponentName", componentName);
                    command.Parameters.AddWithValue("@ComponentType", componentType);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Formula", (object)formula ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    
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
        /// احتساب قيمة عنصر راتب
        /// </summary>
        /// <param name="component">عنصر الراتب</param>
        /// <param name="baseSalary">الراتب الأساسي</param>
        /// <returns>قيمة العنصر</returns>
        private decimal CalculateComponentAmount(EmployeeSalary component, decimal baseSalary)
        {
            // إذا كان العنصر هو الراتب الأساسي، نعيد قيمة الراتب الأساسي
            if (component.ComponentType == "Basic")
            {
                return baseSalary;
            }
            
            // إذا كان العنصر مبلغ ثابت
            if (component.Amount.HasValue)
            {
                return component.Amount.Value;
            }
            
            // إذا كان العنصر نسبة من الراتب الأساسي
            if (component.Percentage.HasValue)
            {
                return baseSalary * (component.Percentage.Value / 100);
            }
            
            return 0;
        }
        
        /// <summary>
        /// احتساب إجمالي البدلات والمستحقات للموظف
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="calculationDate">تاريخ الاحتساب</param>
        /// <param name="baseSalary">الراتب الأساسي</param>
        /// <returns>إجمالي البدلات</returns>
        private decimal CalculateEmployeeAllowances(int employeeId, DateTime calculationDate, decimal baseSalary)
        {
            decimal total = 0;
            
            // الحصول على عناصر راتب الموظف النشطة
            var salaryComponents = _salaryRepository.GetActiveEmployeeSalariesAtDate(employeeId, calculationDate);
            
            // مجموع البدلات والمستحقات (باستثناء الراتب الأساسي)
            foreach (var component in salaryComponents)
            {
                if (component.ComponentType == "Allowance" || component.ComponentType == "Bonus")
                {
                    total += CalculateComponentAmount(component, baseSalary);
                }
            }
            
            return total;
        }
        
        /// <summary>
        /// احتساب إجمالي الخصومات والاستقطاعات للموظف
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <param name="baseSalary">الراتب الأساسي</param>
        /// <returns>إجمالي الخصومات</returns>
        private decimal CalculateEmployeeDeductions(int employeeId, DateTime startDate, DateTime endDate, decimal baseSalary)
        {
            decimal total = 0;
            
            // الحصول على عناصر راتب الموظف النشطة (الاستقطاعات الثابتة)
            var salaryComponents = _salaryRepository.GetActiveEmployeeSalariesAtDate(employeeId, endDate);
            
            // مجموع الاستقطاعات الثابتة
            foreach (var component in salaryComponents)
            {
                if (component.ComponentType == "Deduction")
                {
                    total += CalculateComponentAmount(component, baseSalary);
                }
            }
            
            // الحصول على خصومات الموظف المعتمدة في الفترة
            var deductions = _deductionRepository.GetEmployeeDeductionsInPeriod(employeeId, startDate, endDate)
                .Where(d => d.Status == "Approved").ToList();
            
            // مجموع الخصومات المعتمدة
            foreach (var deduction in deductions)
            {
                total += deduction.DeductionValue;
                
                // تحديث حالة الخصم كمعالج في الرواتب
                UpdateDeductionAsProcessed(deduction.ID, endDate);
            }
            
            return total;
        }
        
        /// <summary>
        /// تحديث حالة الخصم كمعالج في الرواتب
        /// </summary>
        /// <param name="deductionId">معرف الخصم</param>
        /// <param name="payrollDate">تاريخ كشف الرواتب</param>
        private void UpdateDeductionAsProcessed(int deductionId, DateTime payrollDate)
        {
            string query = @"
                UPDATE Deductions
                SET IsPayrollProcessed = 1
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", deductionId);
                    
                    connection.Open();
                    
                    command.ExecuteNonQuery();
                }
            }
        }
        
        /// <summary>
        /// تحديث إجماليات كشف الرواتب
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        private void UpdatePayrollTotals(int payrollId)
        {
            string query = @"
                UPDATE Payrolls
                SET 
                    TotalEmployees = (SELECT COUNT(*) FROM PayrollDetails WHERE PayrollID = @PayrollID),
                    TotalBasicSalary = (SELECT SUM(BaseSalary) FROM PayrollDetails WHERE PayrollID = @PayrollID),
                    TotalAllowances = (SELECT SUM(TotalAllowances) FROM PayrollDetails WHERE PayrollID = @PayrollID),
                    TotalDeductions = (SELECT SUM(TotalDeductions) FROM PayrollDetails WHERE PayrollID = @PayrollID),
                    TotalOvertimeAmount = (SELECT SUM(OvertimeAmount) FROM PayrollDetails WHERE PayrollID = @PayrollID),
                    TotalPayrollAmount = (SELECT SUM(NetSalary) FROM PayrollDetails WHERE PayrollID = @PayrollID),
                    Status = 'Calculated'
                WHERE ID = @PayrollID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PayrollID", payrollId);
                    
                    connection.Open();
                    
                    command.ExecuteNonQuery();
                }
            }
        }
        
        /// <summary>
        /// اعتماد كشف رواتب
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        /// <param name="approvedBy">معرف المستخدم المعتمد</param>
        /// <returns>نجاح العملية</returns>
        public bool ApprovePayroll(int payrollId, int approvedBy)
        {
            string query = @"
                UPDATE Payrolls
                SET 
                    Status = 'Approved',
                    ApprovedBy = @ApprovedBy,
                    ApprovalDate = @ApprovalDate
                WHERE ID = @PayrollID AND Status = 'Calculated'";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PayrollID", payrollId);
                    command.Parameters.AddWithValue("@ApprovedBy", approvedBy);
                    command.Parameters.AddWithValue("@ApprovalDate", DateTime.Now);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// إقفال كشف رواتب
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        /// <param name="paymentDate">تاريخ الدفع</param>
        /// <returns>نجاح العملية</returns>
        public bool ClosePayroll(int payrollId, DateTime paymentDate)
        {
            string query = @"
                UPDATE Payrolls
                SET 
                    Status = 'Paid',
                    IsClosed = 1,
                    PaymentDate = @PaymentDate
                WHERE ID = @PayrollID AND Status = 'Approved'";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PayrollID", payrollId);
                    command.Parameters.AddWithValue("@PaymentDate", paymentDate);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    if (rowsAffected > 0)
                    {
                        // تحديث حالة تفاصيل الرواتب
                        UpdatePayrollDetailsStatus(payrollId, "Paid");
                    }
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// إلغاء كشف رواتب
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        /// <returns>نجاح العملية</returns>
        public bool CancelPayroll(int payrollId)
        {
            string query = @"
                UPDATE Payrolls
                SET 
                    Status = 'Cancelled',
                    IsClosed = 1
                WHERE ID = @PayrollID AND Status IN ('Created', 'Calculated')";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PayrollID", payrollId);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    if (rowsAffected > 0)
                    {
                        // تحديث حالة تفاصيل الرواتب
                        UpdatePayrollDetailsStatus(payrollId, "Cancelled");
                    }
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// تحديث حالة تفاصيل كشف الرواتب
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        /// <param name="status">الحالة الجديدة</param>
        private void UpdatePayrollDetailsStatus(int payrollId, string status)
        {
            string query = @"
                UPDATE PayrollDetails
                SET Status = @Status
                WHERE PayrollID = @PayrollID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PayrollID", payrollId);
                    command.Parameters.AddWithValue("@Status", status);
                    
                    connection.Open();
                    
                    command.ExecuteNonQuery();
                }
            }
        }
        
        /// <summary>
        /// تحويل من قارئ البيانات إلى كائن كشف رواتب
        /// </summary>
        /// <param name="reader">قارئ البيانات</param>
        /// <returns>كائن كشف رواتب</returns>
        private Payroll MapPayrollFromReader(SqlDataReader reader)
        {
            return new Payroll
            {
                ID = Convert.ToInt32(reader["ID"]),
                PayrollName = reader["PayrollName"].ToString(),
                PayrollPeriod = reader["PayrollPeriod"].ToString(),
                PayrollType = reader["PayrollType"].ToString(),
                PayrollMonth = Convert.ToInt32(reader["PayrollMonth"]),
                PayrollYear = Convert.ToInt32(reader["PayrollYear"]),
                StartDate = Convert.ToDateTime(reader["StartDate"]),
                EndDate = Convert.ToDateTime(reader["EndDate"]),
                PaymentDate = reader["PaymentDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["PaymentDate"]) : null,
                TotalEmployees = Convert.ToInt32(reader["TotalEmployees"]),
                TotalBasicSalary = Convert.ToDecimal(reader["TotalBasicSalary"]),
                TotalAllowances = Convert.ToDecimal(reader["TotalAllowances"]),
                TotalDeductions = Convert.ToDecimal(reader["TotalDeductions"]),
                TotalOvertimeAmount = Convert.ToDecimal(reader["TotalOvertimeAmount"]),
                TotalPayrollAmount = Convert.ToDecimal(reader["TotalPayrollAmount"]),
                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                Status = reader["Status"].ToString(),
                IsClosed = Convert.ToBoolean(reader["IsClosed"]),
                CreatedBy = reader["CreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CreatedBy"]) : null,
                CreatedByUser = reader["CreatedByUser"] != DBNull.Value ? reader["CreatedByUser"].ToString() : null,
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                ApprovedBy = reader["ApprovedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ApprovedBy"]) : null,
                ApprovedByUser = reader["ApprovedByUser"] != DBNull.Value ? reader["ApprovedByUser"].ToString() : null,
                ApprovalDate = reader["ApprovalDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ApprovalDate"]) : null
            };
        }
        
        #endregion
        
        #region Payroll Details Methods
        
        /// <summary>
        /// الحصول على تفاصيل كشف الرواتب
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        /// <returns>قائمة تفاصيل كشف الرواتب</returns>
        public List<PayrollDetail> GetPayrollDetails(int payrollId)
        {
            List<PayrollDetail> details = new List<PayrollDetail>();
            
            string query = @"
                SELECT 
                    pd.ID, pd.PayrollID, p.PayrollName,
                    pd.EmployeeID, e.FullName AS EmployeeName,
                    e.JobTitle, d.Name AS Department,
                    pd.WorkingDays, pd.PresentDays, pd.AbsentDays, pd.LeaveDays,
                    pd.LateMinutes, pd.OvertimeHours, pd.BaseSalary, pd.TotalAllowances,
                    pd.TotalDeductions, pd.OvertimeAmount, pd.NetSalary, pd.Status, pd.Notes
                FROM PayrollDetails pd
                JOIN Payrolls p ON pd.PayrollID = p.ID
                JOIN Employees e ON pd.EmployeeID = e.ID
                LEFT JOIN Departments d ON e.DepartmentID = d.ID
                WHERE pd.PayrollID = @PayrollID
                ORDER BY e.FullName";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PayrollID", payrollId);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PayrollDetail detail = MapPayrollDetailFromReader(reader);
                            
                            // الحصول على تفاصيل مكونات الراتب
                            detail.ComponentDetails = GetPayrollComponentDetails(detail.ID);
                            
                            details.Add(detail);
                        }
                    }
                }
            }
            
            return details;
        }
        
        /// <summary>
        /// الحصول على تفاصيل كشف راتب موظف
        /// </summary>
        /// <param name="payrollId">معرف كشف الرواتب</param>
        /// <param name="employeeId">معرف الموظف</param>
        /// <returns>تفاصيل كشف الراتب</returns>
        public PayrollDetail GetEmployeePayrollDetail(int payrollId, int employeeId)
        {
            string query = @"
                SELECT 
                    pd.ID, pd.PayrollID, p.PayrollName,
                    pd.EmployeeID, e.FullName AS EmployeeName,
                    e.JobTitle, d.Name AS Department,
                    pd.WorkingDays, pd.PresentDays, pd.AbsentDays, pd.LeaveDays,
                    pd.LateMinutes, pd.OvertimeHours, pd.BaseSalary, pd.TotalAllowances,
                    pd.TotalDeductions, pd.OvertimeAmount, pd.NetSalary, pd.Status, pd.Notes
                FROM PayrollDetails pd
                JOIN Payrolls p ON pd.PayrollID = p.ID
                JOIN Employees e ON pd.EmployeeID = e.ID
                LEFT JOIN Departments d ON e.DepartmentID = d.ID
                WHERE pd.PayrollID = @PayrollID AND pd.EmployeeID = @EmployeeID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PayrollID", payrollId);
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PayrollDetail detail = MapPayrollDetailFromReader(reader);
                            
                            // الحصول على تفاصيل مكونات الراتب
                            detail.ComponentDetails = GetPayrollComponentDetails(detail.ID);
                            
                            return detail;
                        }
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// الحصول على تفاصيل مكونات الراتب
        /// </summary>
        /// <param name="payrollDetailId">معرف تفاصيل كشف الراتب</param>
        /// <returns>قائمة تفاصيل مكونات الراتب</returns>
        public List<PayrollComponentDetail> GetPayrollComponentDetails(int payrollDetailId)
        {
            List<PayrollComponentDetail> components = new List<PayrollComponentDetail>();
            
            string query = @"
                SELECT 
                    ID, PayrollDetailID, ComponentID, ComponentName, ComponentType,
                    Amount, Formula, CreatedAt
                FROM PayrollComponentDetails
                WHERE PayrollDetailID = @PayrollDetailID
                ORDER BY ComponentType, ComponentName";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PayrollDetailID", payrollDetailId);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            components.Add(MapPayrollComponentDetailFromReader(reader));
                        }
                    }
                }
            }
            
            return components;
        }
        
        /// <summary>
        /// تحويل من قارئ البيانات إلى كائن تفاصيل كشف راتب
        /// </summary>
        /// <param name="reader">قارئ البيانات</param>
        /// <returns>كائن تفاصيل كشف راتب</returns>
        private PayrollDetail MapPayrollDetailFromReader(SqlDataReader reader)
        {
            return new PayrollDetail
            {
                ID = Convert.ToInt32(reader["ID"]),
                PayrollID = Convert.ToInt32(reader["PayrollID"]),
                PayrollName = reader["PayrollName"].ToString(),
                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                EmployeeName = reader["EmployeeName"].ToString(),
                JobTitle = reader["JobTitle"] != DBNull.Value ? reader["JobTitle"].ToString() : null,
                Department = reader["Department"] != DBNull.Value ? reader["Department"].ToString() : null,
                WorkingDays = Convert.ToInt32(reader["WorkingDays"]),
                PresentDays = Convert.ToInt32(reader["PresentDays"]),
                AbsentDays = Convert.ToInt32(reader["AbsentDays"]),
                LeaveDays = Convert.ToInt32(reader["LeaveDays"]),
                LateMinutes = Convert.ToInt32(reader["LateMinutes"]),
                OvertimeHours = Convert.ToDecimal(reader["OvertimeHours"]),
                BaseSalary = Convert.ToDecimal(reader["BaseSalary"]),
                TotalAllowances = Convert.ToDecimal(reader["TotalAllowances"]),
                TotalDeductions = Convert.ToDecimal(reader["TotalDeductions"]),
                OvertimeAmount = Convert.ToDecimal(reader["OvertimeAmount"]),
                NetSalary = Convert.ToDecimal(reader["NetSalary"]),
                Status = reader["Status"].ToString(),
                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null
            };
        }
        
        /// <summary>
        /// تحويل من قارئ البيانات إلى كائن تفاصيل مكون راتب
        /// </summary>
        /// <param name="reader">قارئ البيانات</param>
        /// <returns>كائن تفاصيل مكون راتب</returns>
        private PayrollComponentDetail MapPayrollComponentDetailFromReader(SqlDataReader reader)
        {
            return new PayrollComponentDetail
            {
                ID = Convert.ToInt32(reader["ID"]),
                PayrollDetailID = Convert.ToInt32(reader["PayrollDetailID"]),
                ComponentID = Convert.ToInt32(reader["ComponentID"]),
                ComponentName = reader["ComponentName"].ToString(),
                ComponentType = reader["ComponentType"].ToString(),
                Amount = Convert.ToDecimal(reader["Amount"]),
                Formula = reader["Formula"] != DBNull.Value ? reader["Formula"].ToString() : null,
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }
        
        #endregion
        
        #region Utility Methods
        
        /// <summary>
        /// الحصول على عدد أيام العمل في فترة
        /// </summary>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <returns>عدد أيام العمل</returns>
        private int GetWorkingDaysInPeriod(DateTime startDate, DateTime endDate)
        {
            int workingDays = 0;
            
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // استبعاد أيام الجمعة والسبت (يمكن تعديلها حسب أيام العمل المعتمدة)
                if (date.DayOfWeek != DayOfWeek.Friday && date.DayOfWeek != DayOfWeek.Saturday)
                {
                    workingDays++;
                }
            }
            
            return workingDays;
        }
        
        /// <summary>
        /// الحصول على عدد أيام الحضور في فترة
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <returns>عدد أيام الحضور</returns>
        private int GetPresentDaysInPeriod(int employeeId, DateTime startDate, DateTime endDate)
        {
            string query = @"
                SELECT COUNT(DISTINCT CAST(AttendanceDate AS DATE))
                FROM AttendanceRecords
                WHERE EmployeeID = @EmployeeID
                  AND AttendanceDate BETWEEN @StartDate AND @EndDate
                  AND Status <> 'Absent'";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    
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
        /// الحصول على عدد أيام الإجازة في فترة
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <returns>عدد أيام الإجازة</returns>
        private int GetLeaveDaysInPeriod(int employeeId, DateTime startDate, DateTime endDate)
        {
            string query = @"
                SELECT ISNULL(SUM(LeaveDays), 0)
                FROM (
                    SELECT 
                        CASE 
                            WHEN l.StartDate < @StartDate AND l.EndDate > @EndDate 
                                THEN DATEDIFF(DAY, @StartDate, @EndDate) + 1
                            WHEN l.StartDate < @StartDate 
                                THEN DATEDIFF(DAY, @StartDate, l.EndDate) + 1
                            WHEN l.EndDate > @EndDate 
                                THEN DATEDIFF(DAY, l.StartDate, @EndDate) + 1
                            ELSE DATEDIFF(DAY, l.StartDate, l.EndDate) + 1
                        END AS LeaveDays
                    FROM LeaveRequests l
                    WHERE l.EmployeeID = @EmployeeID
                      AND l.Status = 'Approved'
                      AND (
                          (l.StartDate BETWEEN @StartDate AND @EndDate) OR
                          (l.EndDate BETWEEN @StartDate AND @EndDate) OR
                          (l.StartDate <= @StartDate AND l.EndDate >= @EndDate)
                      )
                ) AS LeaveDaysCalculation";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    
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
        /// الحصول على دقائق التأخير في فترة
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <returns>دقائق التأخير</returns>
        private int GetLateMinutesInPeriod(int employeeId, DateTime startDate, DateTime endDate)
        {
            string query = @"
                SELECT ISNULL(SUM(LateMinutes), 0)
                FROM AttendanceRecords
                WHERE EmployeeID = @EmployeeID
                  AND AttendanceDate BETWEEN @StartDate AND @EndDate";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    
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
        /// الحصول على ساعات العمل الإضافي في فترة
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <returns>ساعات العمل الإضافي</returns>
        private decimal GetOvertimeHoursInPeriod(int employeeId, DateTime startDate, DateTime endDate)
        {
            string query = @"
                SELECT ISNULL(SUM(OvertimeMinutes) / 60.0, 0)
                FROM AttendanceRecords
                WHERE EmployeeID = @EmployeeID
                  AND AttendanceDate BETWEEN @StartDate AND @EndDate";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    
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
        /// الحصول على اسم الشهر باللغة العربية
        /// </summary>
        /// <param name="month">رقم الشهر</param>
        /// <returns>اسم الشهر</returns>
        private string GetMonthName(int month)
        {
            string[] arabicMonths = new string[]
            {
                "يناير", "فبراير", "مارس", "أبريل", "مايو", "يونيو",
                "يوليو", "أغسطس", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر"
            };
            
            if (month >= 1 && month <= 12)
            {
                return arabicMonths[month - 1];
            }
            
            return month.ToString();
        }
        
        #endregion
    }
}