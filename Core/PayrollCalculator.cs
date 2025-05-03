using System;
using System.Collections.Generic;
using System.Linq;
using HR.DataAccess;
using HR.Models;

namespace HR.Core
{
    /// <summary>
    /// محرك حساب الرواتب
    /// </summary>
    public class PayrollCalculator
    {
        private readonly PayrollRepository _payrollRepository;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly DeductionRepository _deductionRepository;
        private readonly SalaryRepository _salaryRepository;
        
        /// <summary>
        /// إنشاء محرك حساب جديد
        /// </summary>
        public PayrollCalculator()
        {
            _payrollRepository = new PayrollRepository();
            _attendanceRepository = new AttendanceRepository();
            _deductionRepository = new DeductionRepository();
            _salaryRepository = new SalaryRepository();
        }
        
        /// <summary>
        /// حساب كشف رواتب
        /// </summary>
        /// <param name="payrollID">رقم كشف الرواتب</param>
        /// <returns>نتيجة الحساب</returns>
        public bool CalculatePayroll(int payrollID)
        {
            try
            {
                LogManager.LogInfo($"بدء حساب كشف الرواتب {payrollID}");
                
                // الحصول على بيانات كشف الرواتب
                Payroll payroll = _payrollRepository.GetPayrollByID(payrollID);
                if (payroll == null)
                {
                    LogManager.LogError($"لم يتم العثور على كشف الرواتب {payrollID}");
                    return false;
                }
                
                // التأكد من أن الكشف في حالة تسمح بالحساب
                if (payroll.Status != "Created")
                {
                    LogManager.LogError($"كشف الرواتب {payrollID} في حالة {payroll.Status} لا تسمح بالحساب");
                    return false;
                }
                
                // تحديد فترة الكشف
                DateTime startDate = new DateTime(payroll.PayrollYear, payroll.PayrollMonth, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                
                // الحصول على قائمة الموظفين النشطين
                List<Employee> employees = GetActiveEmployees();
                
                // حساب راتب كل موظف
                foreach (Employee employee in employees)
                {
                    CalculateEmployeePayroll(payroll, employee, startDate, endDate);
                }
                
                // تحديث حالة الكشف
                payroll.Status = "Calculated";
                payroll.CalculationDate = DateTime.Now;
                _payrollRepository.UpdatePayroll(payroll);
                
                LogManager.LogInfo($"تم حساب كشف الرواتب {payrollID} بنجاح");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء حساب كشف الرواتب {payrollID}");
                return false;
            }
        }
        
        /// <summary>
        /// الحصول على قائمة الموظفين النشطين
        /// </summary>
        /// <returns>قائمة الموظفين</returns>
        private List<Employee> GetActiveEmployees()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            return employeeRepository.GetActiveEmployees();
        }
        
        /// <summary>
        /// حساب راتب موظف
        /// </summary>
        /// <param name="payroll">كشف الرواتب</param>
        /// <param name="employee">الموظف</param>
        /// <param name="startDate">تاريخ بداية الفترة</param>
        /// <param name="endDate">تاريخ نهاية الفترة</param>
        private void CalculateEmployeePayroll(Payroll payroll, Employee employee, DateTime startDate, DateTime endDate)
        {
            try
            {
                LogManager.LogInfo($"حساب راتب الموظف {employee.FullName} لكشف الرواتب {payroll.PayrollName}");
                
                // الحصول على راتب الموظف الساري
                EmployeeSalary salary = _salaryRepository.GetEmployeeActiveSalary(employee.ID, startDate);
                if (salary == null)
                {
                    LogManager.LogWarning($"لم يتم العثور على راتب للموظف {employee.FullName}، سيتم تخطي الموظف");
                    return;
                }
                
                // الحصول على عناصر راتب الموظف
                List<EmployeeSalaryComponent> components = _salaryRepository.GetEmployeeSalaryComponents(salary.ID);
                if (components.Count == 0)
                {
                    LogManager.LogWarning($"لم يتم العثور على عناصر راتب للموظف {employee.FullName}، سيتم تخطي الموظف");
                    return;
                }
                
                // إنشاء تفاصيل راتب الموظف
                PayrollDetail payrollDetail = new PayrollDetail
                {
                    PayrollID = payroll.ID,
                    EmployeeID = employee.ID,
                    BaseSalary = GetBaseSalary(components),
                    TotalAllowances = 0,
                    TotalDeductions = 0,
                    TotalBonus = 0,
                    NetSalary = 0,
                    PaymentStatus = "Unpaid"
                };
                
                // حساب الراتب الأساسي
                
                // حساب البدلات والحوافز
                CalculateAllowancesAndBonuses(payrollDetail, components);
                
                // حساب العمل الإضافي
                CalculateOvertime(payrollDetail, employee, startDate, endDate);
                
                // حساب الخصومات
                CalculateDeductions(payrollDetail, employee, startDate, endDate);
                
                // حساب صافي الراتب
                payrollDetail.NetSalary = payrollDetail.BaseSalary +
                    payrollDetail.TotalAllowances +
                    payrollDetail.TotalBonus -
                    payrollDetail.TotalDeductions;
                
                // حفظ تفاصيل الراتب
                int detailID = _payrollRepository.AddPayrollDetail(payrollDetail);
                
                if (detailID > 0)
                {
                    // حفظ تفاصيل مكونات الراتب
                    SavePayrollComponents(detailID, components);
                    
                    LogManager.LogInfo($"تم حساب راتب الموظف {employee.FullName} بنجاح");
                }
                else
                {
                    LogManager.LogError($"فشل في حفظ تفاصيل راتب الموظف {employee.FullName}");
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء حساب راتب الموظف {employee.FullName}");
            }
        }
        
        /// <summary>
        /// الحصول على الراتب الأساسي
        /// </summary>
        /// <param name="components">عناصر الراتب</param>
        /// <returns>الراتب الأساسي</returns>
        private decimal GetBaseSalary(List<EmployeeSalaryComponent> components)
        {
            EmployeeSalaryComponent basicComponent = components.FirstOrDefault(c =>
                _salaryRepository.GetSalaryComponentByID(c.ComponentID).ComponentType == "Basic");
            
            return basicComponent != null ? basicComponent.Amount : 0;
        }
        
        /// <summary>
        /// حساب البدلات والحوافز
        /// </summary>
        /// <param name="payrollDetail">تفاصيل الراتب</param>
        /// <param name="components">عناصر الراتب</param>
        private void CalculateAllowancesAndBonuses(PayrollDetail payrollDetail, List<EmployeeSalaryComponent> components)
        {
            decimal totalAllowances = 0;
            decimal totalBonus = 0;
            
            foreach (EmployeeSalaryComponent component in components)
            {
                SalaryComponent salaryComponent = _salaryRepository.GetSalaryComponentByID(component.ComponentID);
                if (salaryComponent == null)
                    continue;
                
                if (salaryComponent.ComponentType == "Allowance")
                {
                    totalAllowances += component.Amount;
                }
                else if (salaryComponent.ComponentType == "Bonus")
                {
                    totalBonus += component.Amount;
                }
            }
            
            payrollDetail.TotalAllowances = totalAllowances;
            payrollDetail.TotalBonus = totalBonus;
        }
        
        /// <summary>
        /// حساب العمل الإضافي
        /// </summary>
        /// <param name="payrollDetail">تفاصيل الراتب</param>
        /// <param name="employee">الموظف</param>
        /// <param name="startDate">تاريخ بداية الفترة</param>
        /// <param name="endDate">تاريخ نهاية الفترة</param>
        private void CalculateOvertime(PayrollDetail payrollDetail, Employee employee, DateTime startDate, DateTime endDate)
        {
            try
            {
                // الحصول على مجموع ساعات العمل الإضافي للفترة
                decimal overtimeHours = _attendanceRepository.GetEmployeeOvertimeHours(employee.ID, startDate, endDate);
                if (overtimeHours <= 0)
                    return;
                
                // حساب قيمة ساعة العمل الإضافي
                // تعتمد قيمة ساعة العمل الإضافي على مجموع الراتب الأساسي والبدلات التي تؤثر على العمل الإضافي
                decimal hourlyRate = CalculateOvertimeHourlyRate(employee.ID, payrollDetail.BaseSalary);
                
                // حساب قيمة العمل الإضافي
                decimal overtimeAmount = overtimeHours * hourlyRate;
                
                // إضافة العمل الإضافي إلى الحوافز
                payrollDetail.TotalBonus += overtimeAmount;
                
                // إضافة بند العمل الإضافي
                PayrollComponentDetail overtimeComponent = new PayrollComponentDetail
                {
                    PayrollDetailID = payrollDetail.ID,
                    ComponentName = "العمل الإضافي",
                    ComponentType = "Overtime",
                    Amount = overtimeAmount,
                    Notes = $"{overtimeHours} ساعة × {hourlyRate:N2}"
                };
                
                _payrollRepository.AddPayrollComponentDetail(overtimeComponent);
                
                LogManager.LogInfo($"تم حساب العمل الإضافي للموظف {employee.FullName}: {overtimeHours} ساعة بقيمة {overtimeAmount:N2}");
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء حساب العمل الإضافي للموظف {employee.FullName}");
            }
        }
        
        /// <summary>
        /// حساب قيمة ساعة العمل الإضافي
        /// </summary>
        /// <param name="employeeID">رقم الموظف</param>
        /// <param name="baseSalary">الراتب الأساسي</param>
        /// <returns>قيمة ساعة العمل الإضافي</returns>
        private decimal CalculateOvertimeHourlyRate(int employeeID, decimal baseSalary)
        {
            // افتراضياً، ساعة العمل الإضافي = (الراتب الأساسي / 30 / 8) * 1.5
            // حيث 30 هو عدد أيام الشهر، و 8 هو عدد ساعات العمل اليومية، و 1.5 هو معامل العمل الإضافي
            return (baseSalary / 30 / 8) * 1.5m;
        }
        
        /// <summary>
        /// حساب الخصومات
        /// </summary>
        /// <param name="payrollDetail">تفاصيل الراتب</param>
        /// <param name="employee">الموظف</param>
        /// <param name="startDate">تاريخ بداية الفترة</param>
        /// <param name="endDate">تاريخ نهاية الفترة</param>
        private void CalculateDeductions(PayrollDetail payrollDetail, Employee employee, DateTime startDate, DateTime endDate)
        {
            try
            {
                decimal totalDeductions = 0;
                
                // حساب خصومات الغياب
                decimal absenceDeduction = CalculateAbsenceDeduction(payrollDetail, employee, startDate, endDate);
                totalDeductions += absenceDeduction;
                
                // حساب خصومات التأخير
                decimal lateDeduction = CalculateLateDeduction(payrollDetail, employee, startDate, endDate);
                totalDeductions += lateDeduction;
                
                // إضافة الخصومات الثابتة من جدول عناصر الراتب
                List<EmployeeSalaryComponent> components = _salaryRepository.GetEmployeeSalaryComponentsByType(employee.ID, "Deduction");
                foreach (EmployeeSalaryComponent component in components)
                {
                    totalDeductions += component.Amount;
                    
                    // إضافة بند الخصم
                    PayrollComponentDetail deductionComponent = new PayrollComponentDetail
                    {
                        PayrollDetailID = payrollDetail.ID,
                        ComponentName = component.ComponentName,
                        ComponentType = "Deduction",
                        Amount = component.Amount,
                        Notes = component.Notes
                    };
                    
                    _payrollRepository.AddPayrollComponentDetail(deductionComponent);
                }
                
                // إضافة الخصومات الإدارية
                List<Deduction> administrativeDeductions = _deductionRepository.GetEmployeeDeductions(employee.ID, startDate, endDate);
                foreach (Deduction deduction in administrativeDeductions)
                {
                    totalDeductions += deduction.Amount;
                    
                    // إضافة بند الخصم
                    PayrollComponentDetail deductionComponent = new PayrollComponentDetail
                    {
                        PayrollDetailID = payrollDetail.ID,
                        ComponentName = deduction.DeductionTypeName,
                        ComponentType = "Deduction",
                        Amount = deduction.Amount,
                        Notes = deduction.Notes
                    };
                    
                    _payrollRepository.AddPayrollComponentDetail(deductionComponent);
                }
                
                payrollDetail.TotalDeductions = totalDeductions;
                
                LogManager.LogInfo($"تم حساب خصومات الموظف {employee.FullName}: {totalDeductions:N2}");
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء حساب خصومات الموظف {employee.FullName}");
            }
        }
        
        /// <summary>
        /// حساب خصومات الغياب
        /// </summary>
        /// <param name="payrollDetail">تفاصيل الراتب</param>
        /// <param name="employee">الموظف</param>
        /// <param name="startDate">تاريخ بداية الفترة</param>
        /// <param name="endDate">تاريخ نهاية الفترة</param>
        /// <returns>قيمة خصومات الغياب</returns>
        private decimal CalculateAbsenceDeduction(PayrollDetail payrollDetail, Employee employee, DateTime startDate, DateTime endDate)
        {
            try
            {
                // الحصول على عدد أيام الغياب
                int absenceDays = _attendanceRepository.GetEmployeeAbsenceDays(employee.ID, startDate, endDate);
                if (absenceDays <= 0)
                    return 0;
                
                // حساب قيمة يوم الغياب (الراتب الأساسي / 30)
                decimal dayRate = payrollDetail.BaseSalary / 30;
                
                // حساب قيمة خصم الغياب
                decimal absenceDeduction = absenceDays * dayRate;
                
                // إضافة بند خصم الغياب
                PayrollComponentDetail absenceComponent = new PayrollComponentDetail
                {
                    PayrollDetailID = payrollDetail.ID,
                    ComponentName = "خصم غياب",
                    ComponentType = "Deduction",
                    Amount = absenceDeduction,
                    Notes = $"{absenceDays} يوم × {dayRate:N2}"
                };
                
                _payrollRepository.AddPayrollComponentDetail(absenceComponent);
                
                LogManager.LogInfo($"تم حساب خصم الغياب للموظف {employee.FullName}: {absenceDays} يوم بقيمة {absenceDeduction:N2}");
                
                return absenceDeduction;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء حساب خصم الغياب للموظف {employee.FullName}");
                return 0;
            }
        }
        
        /// <summary>
        /// حساب خصومات التأخير
        /// </summary>
        /// <param name="payrollDetail">تفاصيل الراتب</param>
        /// <param name="employee">الموظف</param>
        /// <param name="startDate">تاريخ بداية الفترة</param>
        /// <param name="endDate">تاريخ نهاية الفترة</param>
        /// <returns>قيمة خصومات التأخير</returns>
        private decimal CalculateLateDeduction(PayrollDetail payrollDetail, Employee employee, DateTime startDate, DateTime endDate)
        {
            try
            {
                // الحصول على عدد دقائق التأخير
                int lateMins = _attendanceRepository.GetEmployeeLateMinutes(employee.ID, startDate, endDate);
                if (lateMins <= 0)
                    return 0;
                
                // الحصول على قواعد خصم التأخير
                DeductionRule lateRule = _deductionRepository.GetDeductionRuleByCode("LATE");
                if (lateRule == null)
                {
                    // إذا لم توجد قاعدة، استخدم قيمة افتراضية
                    // لكل 60 دقيقة تأخير، خصم ساعة من الراتب
                    decimal hourRate = payrollDetail.BaseSalary / 30 / 8;
                    decimal lateHours = lateMins / 60m;
                    decimal lateDeduction = lateHours * hourRate;
                    
                    // إضافة بند خصم التأخير
                    PayrollComponentDetail lateComponent = new PayrollComponentDetail
                    {
                        PayrollDetailID = payrollDetail.ID,
                        ComponentName = "خصم تأخير",
                        ComponentType = "Deduction",
                        Amount = lateDeduction,
                        Notes = $"{lateMins} دقيقة تأخير"
                    };
                    
                    _payrollRepository.AddPayrollComponentDetail(lateComponent);
                    
                    LogManager.LogInfo($"تم حساب خصم التأخير للموظف {employee.FullName}: {lateMins} دقيقة بقيمة {lateDeduction:N2}");
                    
                    return lateDeduction;
                }
                else
                {
                    // استخدام قاعدة الخصم المعرفة في النظام
                    // تطبيق المعادلة حسب القاعدة
                    decimal lateDeduction = ApplyDeductionRule(lateRule, lateMins, payrollDetail.BaseSalary);
                    
                    // إضافة بند خصم التأخير
                    PayrollComponentDetail lateComponent = new PayrollComponentDetail
                    {
                        PayrollDetailID = payrollDetail.ID,
                        ComponentName = "خصم تأخير",
                        ComponentType = "Deduction",
                        Amount = lateDeduction,
                        Notes = $"{lateMins} دقيقة تأخير - حسب قاعدة {lateRule.RuleName}"
                    };
                    
                    _payrollRepository.AddPayrollComponentDetail(lateComponent);
                    
                    LogManager.LogInfo($"تم حساب خصم التأخير للموظف {employee.FullName}: {lateMins} دقيقة بقيمة {lateDeduction:N2}");
                    
                    return lateDeduction;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء حساب خصم التأخير للموظف {employee.FullName}");
                return 0;
            }
        }
        
        /// <summary>
        /// تطبيق قاعدة الخصم
        /// </summary>
        /// <param name="rule">قاعدة الخصم</param>
        /// <param name="value">القيمة (عدد الدقائق)</param>
        /// <param name="baseSalary">الراتب الأساسي</param>
        /// <returns>قيمة الخصم</returns>
        private decimal ApplyDeductionRule(DeductionRule rule, int value, decimal baseSalary)
        {
            switch (rule.Formula)
            {
                case "FixedAmount":
                    return rule.Amount;
                
                case "PercentOfDay":
                    decimal dayRate = baseSalary / 30;
                    return dayRate * (rule.Amount / 100);
                
                case "PerUnitValue":
                    // لكل وحدة (دقيقة) قيمة محددة
                    return value * rule.Amount;
                
                case "PerHour":
                    // لكل ساعة قيمة محددة
                    decimal hours = value / 60m;
                    return hours * rule.Amount;
                
                default:
                    return 0;
            }
        }
        
        /// <summary>
        /// حفظ تفاصيل مكونات الراتب
        /// </summary>
        /// <param name="payrollDetailID">رقم تفاصيل الراتب</param>
        /// <param name="components">عناصر الراتب</param>
        private void SavePayrollComponents(int payrollDetailID, List<EmployeeSalaryComponent> components)
        {
            foreach (EmployeeSalaryComponent component in components)
            {
                SalaryComponent salaryComponent = _salaryRepository.GetSalaryComponentByID(component.ComponentID);
                if (salaryComponent == null)
                    continue;
                
                PayrollComponentDetail componentDetail = new PayrollComponentDetail
                {
                    PayrollDetailID = payrollDetailID,
                    ComponentName = salaryComponent.ComponentName,
                    ComponentType = salaryComponent.ComponentType,
                    Amount = component.Amount,
                    Notes = component.Notes
                };
                
                _payrollRepository.AddPayrollComponentDetail(componentDetail);
            }
        }
    }
}