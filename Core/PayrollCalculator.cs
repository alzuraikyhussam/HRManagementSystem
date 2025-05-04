using System;
using System.Collections.Generic;
using System.Linq;
using HR.DataAccess;
using HR.Models;

namespace HR.Core
{
    /// <summary>
    /// حاسبة الرواتب المتقدمة
    /// </summary>
    public class PayrollCalculator
    {
        private readonly UnitOfWork _unitOfWork;
        
        /// <summary>
        /// إنشاء مثيل جديد من حاسبة الرواتب
        /// </summary>
        public PayrollCalculator()
        {
            _unitOfWork = new UnitOfWork();
        }
        
        /// <summary>
        /// حساب راتب الموظف للشهر المحدد
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="year">السنة</param>
        /// <param name="month">الشهر</param>
        /// <returns>كائن تفاصيل الراتب</returns>
        public PayrollDetails CalculateEmployeePayroll(int employeeId, int year, int month)
        {
            try
            {
                // الحصول على بيانات الموظف
                var employee = _unitOfWork.EmployeeRepository.GetEmployeeById(employeeId);
                if (employee == null)
                {
                    throw new ApplicationException("الموظف غير موجود");
                }
                
                // بناء كائن تفاصيل الراتب
                var payrollDetails = new PayrollDetails
                {
                    EmployeeID = employeeId,
                    EmployeeName = employee.FullName,
                    JobTitle = employee.JobTitle,
                    Department = employee.DepartmentName,
                    Year = year,
                    Month = month,
                    PayPeriodStartDate = new DateTime(year, month, 1),
                    PayPeriodEndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month)),
                    BasicSalary = employee.BasicSalary,
                    Allowances = new List<PayrollItem>(),
                    Deductions = new List<PayrollItem>(),
                    Adjustments = new List<PayrollItem>()
                };
                
                // إضافة البدلات الثابتة من ملف الموظف
                AddFixedAllowances(payrollDetails, employee);
                
                // حساب البدلات المتغيرة والخصومات بناءً على الحضور
                CalculateAttendanceBasedItems(payrollDetails, employeeId, year, month);
                
                // إضافة الخصومات الثابتة
                AddFixedDeductions(payrollDetails, employee);
                
                // حساب إجمالي البدلات والخصومات
                payrollDetails.TotalAllowances = payrollDetails.Allowances.Sum(a => a.Amount);
                payrollDetails.TotalDeductions = payrollDetails.Deductions.Sum(d => d.Amount);
                payrollDetails.TotalAdjustments = payrollDetails.Adjustments.Sum(a => a.Amount);
                
                // حساب صافي الراتب
                payrollDetails.GrossSalary = payrollDetails.BasicSalary + payrollDetails.TotalAllowances;
                payrollDetails.NetSalary = payrollDetails.GrossSalary - payrollDetails.TotalDeductions + payrollDetails.TotalAdjustments;
                
                return payrollDetails;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل حساب راتب الموظف {employeeId} لشهر {month}/{year}");
                throw;
            }
        }
        
        /// <summary>
        /// إضافة البدلات الثابتة من ملف الموظف
        /// </summary>
        private void AddFixedAllowances(PayrollDetails payrollDetails, Employee employee)
        {
            // بدل السكن
            if (employee.HousingAllowance > 0)
            {
                payrollDetails.Allowances.Add(new PayrollItem
                {
                    Name = "بدل سكن",
                    Amount = employee.HousingAllowance,
                    Type = PayrollItemType.Allowance,
                    Category = "بدلات ثابتة",
                    Description = "البدل الشهري للسكن"
                });
            }
            
            // بدل المواصلات
            if (employee.TransportationAllowance > 0)
            {
                payrollDetails.Allowances.Add(new PayrollItem
                {
                    Name = "بدل مواصلات",
                    Amount = employee.TransportationAllowance,
                    Type = PayrollItemType.Allowance,
                    Category = "بدلات ثابتة",
                    Description = "البدل الشهري للمواصلات"
                });
            }
            
            // بدل الهاتف
            if (employee.PhoneAllowance > 0)
            {
                payrollDetails.Allowances.Add(new PayrollItem
                {
                    Name = "بدل هاتف",
                    Amount = employee.PhoneAllowance,
                    Type = PayrollItemType.Allowance,
                    Category = "بدلات ثابتة",
                    Description = "البدل الشهري للهاتف"
                });
            }
            
            // بدل طبيعة العمل
            if (employee.NatureOfWorkAllowance > 0)
            {
                payrollDetails.Allowances.Add(new PayrollItem
                {
                    Name = "بدل طبيعة عمل",
                    Amount = employee.NatureOfWorkAllowance,
                    Type = PayrollItemType.Allowance,
                    Category = "بدلات ثابتة",
                    Description = "البدل الشهري لطبيعة العمل"
                });
            }
            
            // البدلات الإضافية المخصصة
            var additionalAllowances = _unitOfWork.AllowanceRepository.GetEmployeeAllowances(employee.ID);
            foreach (var allowance in additionalAllowances)
            {
                payrollDetails.Allowances.Add(new PayrollItem
                {
                    Name = allowance.Name,
                    Amount = allowance.Amount,
                    Type = PayrollItemType.Allowance,
                    Category = "بدلات إضافية",
                    Description = allowance.Description
                });
            }
        }
        
        /// <summary>
        /// إضافة الخصومات الثابتة
        /// </summary>
        private void AddFixedDeductions(PayrollDetails payrollDetails, Employee employee)
        {
            // خصم التأمينات الاجتماعية (GOSI)
            if (employee.GosiSubscription)
            {
                // حساب خصم التأمينات الاجتماعية (نسبة من الراتب الأساسي)
                decimal gosiAmount = CalculateGosiDeduction(employee);
                
                payrollDetails.Deductions.Add(new PayrollItem
                {
                    Name = "التأمينات الاجتماعية",
                    Amount = gosiAmount,
                    Type = PayrollItemType.Deduction,
                    Category = "خصومات نظامية",
                    Description = "اشتراك التأمينات الاجتماعية"
                });
            }
            
            // خصم التأمين الطبي
            if (employee.MedicalInsuranceAmount > 0)
            {
                payrollDetails.Deductions.Add(new PayrollItem
                {
                    Name = "التأمين الطبي",
                    Amount = employee.MedicalInsuranceAmount,
                    Type = PayrollItemType.Deduction,
                    Category = "خصومات ثابتة",
                    Description = "اشتراك التأمين الطبي"
                });
            }
            
            // الاستقطاعات المجدولة (مثل القروض)
            var loans = _unitOfWork.LoanRepository.GetActiveEmployeeLoans(employee.ID);
            foreach (var loan in loans)
            {
                // التحقق من أن هذا الشهر هو ضمن فترة سداد القرض
                if (IsLoanPaymentDue(loan, payrollDetails.Year, payrollDetails.Month))
                {
                    payrollDetails.Deductions.Add(new PayrollItem
                    {
                        Name = $"قسط {loan.LoanType}",
                        Amount = loan.MonthlyPayment,
                        Type = PayrollItemType.Deduction,
                        Category = "قروض واستقطاعات",
                        Description = $"القسط الشهري للقرض: {loan.Description}"
                    });
                }
            }
            
            // الاستقطاعات المخصصة الإضافية
            var additionalDeductions = _unitOfWork.DeductionRepository.GetEmployeeDeductions(employee.ID);
            foreach (var deduction in additionalDeductions)
            {
                payrollDetails.Deductions.Add(new PayrollItem
                {
                    Name = deduction.Name,
                    Amount = deduction.Amount,
                    Type = PayrollItemType.Deduction,
                    Category = "خصومات إضافية",
                    Description = deduction.Description
                });
            }
        }
        
        /// <summary>
        /// حساب البدلات والخصومات بناءً على الحضور
        /// </summary>
        private void CalculateAttendanceBasedItems(PayrollDetails payrollDetails, int employeeId, int year, int month)
        {
            // الحصول على ملخص الحضور الشهري
            var attendanceSummary = _unitOfWork.AttendanceRepository.GetEmployeeMonthlyAttendanceSummary(employeeId, year, month);
            
            if (attendanceSummary != null)
            {
                // إرفاق البيانات الإحصائية للحضور
                payrollDetails.AttendanceStatistics = MapAttendanceStatistics(attendanceSummary);
                
                // حساب خصومات الغياب
                CalculateAbsenceDeductions(payrollDetails, attendanceSummary);
                
                // حساب خصومات التأخير
                CalculateLateDeductions(payrollDetails, attendanceSummary);
                
                // حساب بدل العمل الإضافي
                CalculateOvertimeAllowance(payrollDetails, attendanceSummary);
            }
        }
        
        /// <summary>
        /// حساب خصومات الغياب
        /// </summary>
        private void CalculateAbsenceDeductions(PayrollDetails payrollDetails, AttendanceSummary attendanceSummary)
        {
            // عدد الأيام المفترض حضورها في الشهر (عدا أيام الراحة والإجازات الرسمية)
            int expectedWorkDays = attendanceSummary.GetActualWorkingDays(
                attendanceSummary.Year, 
                attendanceSummary.Month,
                // نهاية الشهر
                new DateTime(attendanceSummary.Year, attendanceSummary.Month, DateTime.DaysInMonth(attendanceSummary.Year, attendanceSummary.Month))
            );
            
            // عدد أيام الغياب (غير المصرح به)
            int unauthorizedAbsenceDays = attendanceSummary.AbsentDays;
            
            if (unauthorizedAbsenceDays > 0)
            {
                // حساب الخصم اليومي (الراتب الأساسي / 30)
                decimal dailySalary = payrollDetails.BasicSalary / 30m;
                
                // الخصم = عدد أيام الغياب × الخصم اليومي
                decimal absenceDeduction = dailySalary * unauthorizedAbsenceDays;
                
                payrollDetails.Deductions.Add(new PayrollItem
                {
                    Name = "خصم غياب",
                    Amount = absenceDeduction,
                    Type = PayrollItemType.Deduction,
                    Category = "خصومات الحضور",
                    Description = $"خصم {unauthorizedAbsenceDays} أيام غياب"
                });
            }
        }
        
        /// <summary>
        /// حساب خصومات التأخير
        /// </summary>
        private void CalculateLateDeductions(PayrollDetails payrollDetails, AttendanceSummary attendanceSummary)
        {
            // التحقق من وجود دقائق تأخير
            if (attendanceSummary.TotalLateMinutes > 0)
            {
                // تحويل دقائق التأخير إلى ساعات
                decimal lateHours = attendanceSummary.TotalLateMinutes / 60m;
                
                // حساب الخصم لكل ساعة (الراتب الأساسي / 240)
                decimal hourlyRate = payrollDetails.BasicSalary / 240m;
                
                // الخصم = عدد ساعات التأخير × الخصم الساعي
                decimal lateDeduction = hourlyRate * lateHours;
                
                payrollDetails.Deductions.Add(new PayrollItem
                {
                    Name = "خصم تأخير",
                    Amount = lateDeduction,
                    Type = PayrollItemType.Deduction,
                    Category = "خصومات الحضور",
                    Description = $"خصم {attendanceSummary.TotalLateMinutes} دقيقة تأخير"
                });
            }
            
            // التحقق من وجود دقائق مغادرة مبكرة
            if (attendanceSummary.TotalEarlyDepartureMinutes > 0)
            {
                // تحويل دقائق المغادرة المبكرة إلى ساعات
                decimal earlyDepartureHours = attendanceSummary.TotalEarlyDepartureMinutes / 60m;
                
                // حساب الخصم لكل ساعة (الراتب الأساسي / 240)
                decimal hourlyRate = payrollDetails.BasicSalary / 240m;
                
                // الخصم = عدد ساعات المغادرة المبكرة × الخصم الساعي
                decimal earlyDepartureDeduction = hourlyRate * earlyDepartureHours;
                
                payrollDetails.Deductions.Add(new PayrollItem
                {
                    Name = "خصم مغادرة مبكرة",
                    Amount = earlyDepartureDeduction,
                    Type = PayrollItemType.Deduction,
                    Category = "خصومات الحضور",
                    Description = $"خصم {attendanceSummary.TotalEarlyDepartureMinutes} دقيقة مغادرة مبكرة"
                });
            }
        }
        
        /// <summary>
        /// حساب بدل العمل الإضافي
        /// </summary>
        private void CalculateOvertimeAllowance(PayrollDetails payrollDetails, AttendanceSummary attendanceSummary)
        {
            // التحقق من وجود ساعات عمل إضافية
            if (attendanceSummary.TotalOvertimeMinutes > 0)
            {
                // تحويل دقائق العمل الإضافي إلى ساعات
                decimal overtimeHours = attendanceSummary.TotalOvertimeMinutes / 60m;
                
                // حساب معدل الساعة الإضافية (الراتب الأساسي / 240 × 1.5)
                decimal overtimeRate = (payrollDetails.BasicSalary / 240m) * 1.5m;
                
                // البدل = عدد ساعات العمل الإضافي × معدل الساعة الإضافية
                decimal overtimeAllowance = overtimeRate * overtimeHours;
                
                payrollDetails.Allowances.Add(new PayrollItem
                {
                    Name = "بدل عمل إضافي",
                    Amount = overtimeAllowance,
                    Type = PayrollItemType.Allowance,
                    Category = "بدلات متغيرة",
                    Description = $"بدل {overtimeHours:0.00} ساعات عمل إضافي"
                });
            }
        }
        
        /// <summary>
        /// حساب خصم التأمينات الاجتماعية
        /// </summary>
        private decimal CalculateGosiDeduction(Employee employee)
        {
            // نسبة اشتراك الموظف في التأمينات (قيمة افتراضية: 10%)
            decimal gosiRate = 0.10m;
            
            // حساب الخصم بناءً على الراتب الأساسي
            decimal gosiAmount = employee.BasicSalary * gosiRate;
            
            // التأكد من أن الخصم لا يتجاوز الحد الأقصى (إن وجد)
            decimal maxGosiDeduction = 4000m; // قيمة افتراضية
            if (gosiAmount > maxGosiDeduction)
            {
                gosiAmount = maxGosiDeduction;
            }
            
            return gosiAmount;
        }
        
        /// <summary>
        /// التحقق من استحقاق قسط القرض في الشهر الحالي
        /// </summary>
        private bool IsLoanPaymentDue(Loan loan, int year, int month)
        {
            // تاريخ الشهر الحالي
            DateTime currentMonth = new DateTime(year, month, 1);
            
            // التحقق من أن الشهر الحالي يقع بين تاريخ بدء السداد وتاريخ انتهاء السداد
            return currentMonth >= loan.StartDate.Date &&
                  (loan.EndDate == null || currentMonth <= loan.EndDate.Value.Date);
        }
        
        /// <summary>
        /// تحويل ملخص الحضور إلى إحصائيات الحضور
        /// </summary>
        private PayrollAttendanceStatistics MapAttendanceStatistics(AttendanceSummary attendanceSummary)
        {
            return new PayrollAttendanceStatistics
            {
                TotalWorkDays = attendanceSummary.TotalDays,
                PresentDays = attendanceSummary.PresentDays,
                AbsentDays = attendanceSummary.AbsentDays,
                LateDays = attendanceSummary.LateDays,
                EarlyDepartureDays = attendanceSummary.EarlyDepartureDays,
                LeaveDays = attendanceSummary.LeaveDays,
                TotalLateMinutes = attendanceSummary.TotalLateMinutes,
                TotalEarlyDepartureMinutes = attendanceSummary.TotalEarlyDepartureMinutes,
                TotalOvertimeMinutes = attendanceSummary.TotalOvertimeMinutes,
                AttendanceCompliance = attendanceSummary.PunctualityRate
            };
        }
    }
    
    /// <summary>
    /// تفاصيل الراتب
    /// </summary>
    public class PayrollDetails
    {
        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// اسم الموظف
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// المسمى الوظيفي
        /// </summary>
        public string JobTitle { get; set; }
        
        /// <summary>
        /// الإدارة/القسم
        /// </summary>
        public string Department { get; set; }
        
        /// <summary>
        /// السنة
        /// </summary>
        public int Year { get; set; }
        
        /// <summary>
        /// الشهر
        /// </summary>
        public int Month { get; set; }
        
        /// <summary>
        /// تاريخ بداية فترة الدفع
        /// </summary>
        public DateTime PayPeriodStartDate { get; set; }
        
        /// <summary>
        /// تاريخ نهاية فترة الدفع
        /// </summary>
        public DateTime PayPeriodEndDate { get; set; }
        
        /// <summary>
        /// الراتب الأساسي
        /// </summary>
        public decimal BasicSalary { get; set; }
        
        /// <summary>
        /// قائمة البدلات
        /// </summary>
        public List<PayrollItem> Allowances { get; set; }
        
        /// <summary>
        /// قائمة الخصومات
        /// </summary>
        public List<PayrollItem> Deductions { get; set; }
        
        /// <summary>
        /// قائمة التعديلات
        /// </summary>
        public List<PayrollItem> Adjustments { get; set; }
        
        /// <summary>
        /// إجمالي البدلات
        /// </summary>
        public decimal TotalAllowances { get; set; }
        
        /// <summary>
        /// إجمالي الخصومات
        /// </summary>
        public decimal TotalDeductions { get; set; }
        
        /// <summary>
        /// إجمالي التعديلات
        /// </summary>
        public decimal TotalAdjustments { get; set; }
        
        /// <summary>
        /// إجمالي الراتب (الراتب الأساسي + البدلات)
        /// </summary>
        public decimal GrossSalary { get; set; }
        
        /// <summary>
        /// صافي الراتب (الراتب الإجمالي - الخصومات + التعديلات)
        /// </summary>
        public decimal NetSalary { get; set; }
        
        /// <summary>
        /// إحصائيات الحضور
        /// </summary>
        public PayrollAttendanceStatistics AttendanceStatistics { get; set; }
    }
    
    /// <summary>
    /// عنصر في الراتب (بدل، خصم، تعديل)
    /// </summary>
    public class PayrollItem
    {
        /// <summary>
        /// اسم العنصر
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// المبلغ
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// نوع العنصر
        /// </summary>
        public PayrollItemType Type { get; set; }
        
        /// <summary>
        /// تصنيف العنصر
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// الوصف
        /// </summary>
        public string Description { get; set; }
    }
    
    /// <summary>
    /// نوع عنصر الراتب
    /// </summary>
    public enum PayrollItemType
    {
        /// <summary>
        /// بدل
        /// </summary>
        Allowance,
        
        /// <summary>
        /// خصم
        /// </summary>
        Deduction,
        
        /// <summary>
        /// تعديل
        /// </summary>
        Adjustment
    }
    
    /// <summary>
    /// إحصائيات الحضور المتعلقة بالراتب
    /// </summary>
    public class PayrollAttendanceStatistics
    {
        /// <summary>
        /// إجمالي أيام العمل
        /// </summary>
        public int TotalWorkDays { get; set; }
        
        /// <summary>
        /// أيام الحضور
        /// </summary>
        public int PresentDays { get; set; }
        
        /// <summary>
        /// أيام الغياب
        /// </summary>
        public int AbsentDays { get; set; }
        
        /// <summary>
        /// أيام التأخير
        /// </summary>
        public int LateDays { get; set; }
        
        /// <summary>
        /// أيام المغادرة المبكرة
        /// </summary>
        public int EarlyDepartureDays { get; set; }
        
        /// <summary>
        /// أيام الإجازات
        /// </summary>
        public int LeaveDays { get; set; }
        
        /// <summary>
        /// إجمالي دقائق التأخير
        /// </summary>
        public int TotalLateMinutes { get; set; }
        
        /// <summary>
        /// إجمالي دقائق المغادرة المبكرة
        /// </summary>
        public int TotalEarlyDepartureMinutes { get; set; }
        
        /// <summary>
        /// إجمالي دقائق العمل الإضافي
        /// </summary>
        public int TotalOvertimeMinutes { get; set; }
        
        /// <summary>
        /// معدل الالتزام بالحضور (نسبة مئوية)
        /// </summary>
        public decimal AttendanceCompliance { get; set; }
    }
    
    /// <summary>
    /// نموذج القرض
    /// </summary>
    public class Loan
    {
        /// <summary>
        /// المعرف
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// نوع القرض
        /// </summary>
        public string LoanType { get; set; }
        
        /// <summary>
        /// المبلغ الإجمالي
        /// </summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>
        /// المبلغ المتبقي
        /// </summary>
        public decimal RemainingAmount { get; set; }
        
        /// <summary>
        /// القسط الشهري
        /// </summary>
        public decimal MonthlyPayment { get; set; }
        
        /// <summary>
        /// عدد الأقساط
        /// </summary>
        public int NumberOfInstallments { get; set; }
        
        /// <summary>
        /// عدد الأقساط المدفوعة
        /// </summary>
        public int PaidInstallments { get; set; }
        
        /// <summary>
        /// تاريخ البدء
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// تاريخ الانتهاء
        /// </summary>
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// الوصف
        /// </summary>
        public string Description { get; set; }
    }
}