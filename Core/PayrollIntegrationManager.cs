using System;
using System.Collections.Generic;
using System.Linq;
using HR.DataAccess;
using HR.Models;

namespace HR.Core
{
    /// <summary>
    /// مدير التكامل بين نظام الرواتب ونظام الحضور والخصومات
    /// </summary>
    public class PayrollIntegrationManager
    {
        private readonly PayrollRepository _payrollRepository;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly DeductionRepository _deductionRepository;
        private readonly SalaryRepository _salaryRepository;
        private readonly SystemSettingsManager _settingsManager;
        
        /// <summary>
        /// إنشاء مدير التكامل الجديد
        /// </summary>
        public PayrollIntegrationManager()
        {
            _payrollRepository = new PayrollRepository();
            _attendanceRepository = new AttendanceRepository();
            _deductionRepository = new DeductionRepository();
            _salaryRepository = new SalaryRepository();
            _settingsManager = new SystemSettingsManager();
        }
        
        /// <summary>
        /// استخراج بيانات الحضور لغرض حساب الرواتب
        /// </summary>
        /// <param name="employeeID">رقم الموظف</param>
        /// <param name="startDate">تاريخ بداية الفترة</param>
        /// <param name="endDate">تاريخ نهاية الفترة</param>
        /// <returns>ملخص بيانات الحضور</returns>
        public AttendanceSummary GetAttendanceSummary(int employeeID, DateTime startDate, DateTime endDate)
        {
            try
            {
                LogManager.LogInfo($"استخراج ملخص الحضور للموظف {employeeID} من {startDate:yyyy-MM-dd} إلى {endDate:yyyy-MM-dd}");
                
                // الحصول على سجلات الحضور للفترة المحددة
                List<AttendanceRecord> records = _attendanceRepository.GetEmployeeAttendanceRecords(employeeID, startDate, endDate);
                
                if (records == null || records.Count == 0)
                {
                    LogManager.LogWarning($"لم يتم العثور على سجلات حضور للموظف {employeeID} في الفترة المحددة");
                    return new AttendanceSummary
                    {
                        EmployeeID = employeeID,
                        StartDate = startDate,
                        EndDate = endDate,
                        WorkDaysCount = 0,
                        AttendanceDaysCount = 0,
                        AbsenceDaysCount = 0,
                        LateMinutesTotal = 0,
                        EarlyDepartureMinutesTotal = 0,
                        OvertimeHoursTotal = 0
                    };
                }
                
                // الحصول على إعدادات أيام العمل
                var workDaysSettings = _settingsManager.GetWorkDaysSettings();
                
                // حساب عدد أيام العمل في الفترة
                int workDaysCount = CalculateWorkDaysInPeriod(startDate, endDate, workDaysSettings);
                
                // استخراج أيام الحضور الفعلية
                var attendanceDates = records
                    .Where(r => r.LogType == "CheckIn")
                    .Select(r => r.LogDateTime.Date)
                    .Distinct()
                    .ToList();
                
                int attendanceDaysCount = attendanceDates.Count;
                
                // حساب أيام الغياب
                int absenceDaysCount = workDaysCount - attendanceDaysCount;
                if (absenceDaysCount < 0) absenceDaysCount = 0;
                
                // حساب إجمالي دقائق التأخير
                int lateMinutesTotal = CalculateLateMinutes(records, workDaysSettings);
                
                // حساب إجمالي دقائق المغادرة المبكرة
                int earlyDepartureMinutesTotal = CalculateEarlyDepartureMinutes(records, workDaysSettings);
                
                // حساب إجمالي ساعات العمل الإضافي
                decimal overtimeHoursTotal = CalculateOvertimeHours(records, workDaysSettings);
                
                // إنشاء ملخص الحضور
                var summary = new AttendanceSummary
                {
                    EmployeeID = employeeID,
                    StartDate = startDate,
                    EndDate = endDate,
                    WorkDaysCount = workDaysCount,
                    AttendanceDaysCount = attendanceDaysCount,
                    AbsenceDaysCount = absenceDaysCount,
                    LateMinutesTotal = lateMinutesTotal,
                    EarlyDepartureMinutesTotal = earlyDepartureMinutesTotal,
                    OvertimeHoursTotal = overtimeHoursTotal
                };
                
                return summary;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء استخراج ملخص الحضور للموظف {employeeID}");
                throw;
            }
        }
        
        /// <summary>
        /// استخراج بيانات الخصومات لغرض حساب الرواتب
        /// </summary>
        /// <param name="employeeID">رقم الموظف</param>
        /// <param name="startDate">تاريخ بداية الفترة</param>
        /// <param name="endDate">تاريخ نهاية الفترة</param>
        /// <returns>ملخص بيانات الخصومات</returns>
        public DeductionSummary GetDeductionSummary(int employeeID, DateTime startDate, DateTime endDate)
        {
            try
            {
                LogManager.LogInfo($"استخراج ملخص الخصومات للموظف {employeeID} من {startDate:yyyy-MM-dd} إلى {endDate:yyyy-MM-dd}");
                
                // الحصول على الخصومات للفترة المحددة
                List<Deduction> deductions = _deductionRepository.GetEmployeeDeductions(employeeID, startDate, endDate);
                
                if (deductions == null || deductions.Count == 0)
                {
                    LogManager.LogInfo($"لم يتم العثور على خصومات للموظف {employeeID} في الفترة المحددة");
                    return new DeductionSummary
                    {
                        EmployeeID = employeeID,
                        StartDate = startDate,
                        EndDate = endDate,
                        ManualDeductionsCount = 0,
                        ManualDeductionsTotal = 0,
                        AutomaticDeductionsCount = 0,
                        AutomaticDeductionsTotal = 0
                    };
                }
                
                // حساب عدد وإجمالي الخصومات اليدوية
                var manualDeductions = deductions.Where(d => d.DeductionSource == "Manual").ToList();
                int manualDeductionsCount = manualDeductions.Count;
                decimal manualDeductionsTotal = manualDeductions.Sum(d => d.Amount);
                
                // حساب عدد وإجمالي الخصومات التلقائية
                var automaticDeductions = deductions.Where(d => d.DeductionSource == "Automatic").ToList();
                int automaticDeductionsCount = automaticDeductions.Count;
                decimal automaticDeductionsTotal = automaticDeductions.Sum(d => d.Amount);
                
                // إنشاء ملخص الخصومات
                var summary = new DeductionSummary
                {
                    EmployeeID = employeeID,
                    StartDate = startDate,
                    EndDate = endDate,
                    ManualDeductionsCount = manualDeductionsCount,
                    ManualDeductionsTotal = manualDeductionsTotal,
                    AutomaticDeductionsCount = automaticDeductionsCount,
                    AutomaticDeductionsTotal = automaticDeductionsTotal
                };
                
                return summary;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء استخراج ملخص الخصومات للموظف {employeeID}");
                throw;
            }
        }
        
        /// <summary>
        /// إنشاء الخصومات التلقائية بناءً على بيانات الحضور
        /// </summary>
        /// <param name="employeeID">رقم الموظف</param>
        /// <param name="startDate">تاريخ بداية الفترة</param>
        /// <param name="endDate">تاريخ نهاية الفترة</param>
        /// <returns>عدد الخصومات المُنشأة</returns>
        public int GenerateAutomaticDeductions(int employeeID, DateTime startDate, DateTime endDate)
        {
            try
            {
                LogManager.LogInfo($"إنشاء الخصومات التلقائية للموظف {employeeID} من {startDate:yyyy-MM-dd} إلى {endDate:yyyy-MM-dd}");
                
                // الحصول على ملخص الحضور
                var attendanceSummary = GetAttendanceSummary(employeeID, startDate, endDate);
                
                // الحصول على قواعد الخصم
                var absenteeismRule = _deductionRepository.GetDeductionRuleByCode("ABSENT");
                var latenessRule = _deductionRepository.GetDeductionRuleByCode("LATE");
                var earlyDepartureRule = _deductionRepository.GetDeductionRuleByCode("EARLY");
                
                List<Deduction> deductions = new List<Deduction>();
                
                // إنشاء خصومات الغياب
                if (absenteeismRule != null && attendanceSummary.AbsenceDaysCount > 0)
                {
                    decimal amount = CalculateDeductionAmount(absenteeismRule, attendanceSummary.AbsenceDaysCount, employeeID);
                    
                    Deduction deduction = new Deduction
                    {
                        EmployeeID = employeeID,
                        DeductionTypeID = absenteeismRule.ID,
                        DeductionTypeName = absenteeismRule.RuleName,
                        DeductionDate = DateTime.Now,
                        Amount = amount,
                        Notes = $"خصم غياب: {attendanceSummary.AbsenceDaysCount} يوم",
                        DeductionSource = "Automatic",
                        CreatedBy = 1, // System
                        StartDate = startDate,
                        EndDate = endDate
                    };
                    
                    deductions.Add(deduction);
                }
                
                // إنشاء خصومات التأخير
                if (latenessRule != null && attendanceSummary.LateMinutesTotal > 0)
                {
                    decimal amount = CalculateDeductionAmount(latenessRule, attendanceSummary.LateMinutesTotal, employeeID);
                    
                    Deduction deduction = new Deduction
                    {
                        EmployeeID = employeeID,
                        DeductionTypeID = latenessRule.ID,
                        DeductionTypeName = latenessRule.RuleName,
                        DeductionDate = DateTime.Now,
                        Amount = amount,
                        Notes = $"خصم تأخير: {attendanceSummary.LateMinutesTotal} دقيقة",
                        DeductionSource = "Automatic",
                        CreatedBy = 1, // System
                        StartDate = startDate,
                        EndDate = endDate
                    };
                    
                    deductions.Add(deduction);
                }
                
                // إنشاء خصومات المغادرة المبكرة
                if (earlyDepartureRule != null && attendanceSummary.EarlyDepartureMinutesTotal > 0)
                {
                    decimal amount = CalculateDeductionAmount(earlyDepartureRule, attendanceSummary.EarlyDepartureMinutesTotal, employeeID);
                    
                    Deduction deduction = new Deduction
                    {
                        EmployeeID = employeeID,
                        DeductionTypeID = earlyDepartureRule.ID,
                        DeductionTypeName = earlyDepartureRule.RuleName,
                        DeductionDate = DateTime.Now,
                        Amount = amount,
                        Notes = $"خصم مغادرة مبكرة: {attendanceSummary.EarlyDepartureMinutesTotal} دقيقة",
                        DeductionSource = "Automatic",
                        CreatedBy = 1, // System
                        StartDate = startDate,
                        EndDate = endDate
                    };
                    
                    deductions.Add(deduction);
                }
                
                // حفظ الخصومات
                int createdCount = 0;
                foreach (var deduction in deductions)
                {
                    int id = _deductionRepository.AddDeduction(deduction);
                    if (id > 0)
                    {
                        createdCount++;
                    }
                }
                
                LogManager.LogInfo($"تم إنشاء {createdCount} خصم تلقائي للموظف {employeeID}");
                return createdCount;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء إنشاء الخصومات التلقائية للموظف {employeeID}");
                return 0;
            }
        }
        
        /// <summary>
        /// إنشاء الاستحقاقات والمكافآت التلقائية بناءً على بيانات الحضور
        /// </summary>
        /// <param name="employeeID">رقم الموظف</param>
        /// <param name="startDate">تاريخ بداية الفترة</param>
        /// <param name="endDate">تاريخ نهاية الفترة</param>
        /// <returns>نتيجة الإنشاء</returns>
        public bool GenerateAutomaticBonuses(int employeeID, DateTime startDate, DateTime endDate)
        {
            try
            {
                LogManager.LogInfo($"إنشاء المكافآت التلقائية للموظف {employeeID} من {startDate:yyyy-MM-dd} إلى {endDate:yyyy-MM-dd}");
                
                // الحصول على ملخص الحضور
                var attendanceSummary = GetAttendanceSummary(employeeID, startDate, endDate);
                
                // استخراج ساعات العمل الإضافي
                decimal overtimeHours = attendanceSummary.OvertimeHoursTotal;
                
                if (overtimeHours <= 0)
                {
                    LogManager.LogInfo($"لا توجد ساعات عمل إضافي للموظف {employeeID} في الفترة المحددة");
                    return true;
                }
                
                // الحصول على راتب الموظف
                EmployeeSalary salary = _salaryRepository.GetEmployeeActiveSalary(employeeID, startDate);
                if (salary == null)
                {
                    LogManager.LogWarning($"لم يتم العثور على راتب للموظف {employeeID}");
                    return false;
                }
                
                // حساب قيمة ساعة العمل الإضافي
                decimal hourlyRate = CalculateOvertimeHourlyRate(employeeID, salary);
                
                // حساب قيمة العمل الإضافي
                decimal amount = overtimeHours * hourlyRate;
                
                // إنشاء استحقاق العمل الإضافي
                Bonus bonus = new Bonus
                {
                    EmployeeID = employeeID,
                    BonusTypeID = 1, // Overtime
                    BonusTypeName = "العمل الإضافي",
                    BonusDate = DateTime.Now,
                    Amount = amount,
                    Notes = $"مكافأة العمل الإضافي: {overtimeHours} ساعة × {hourlyRate:N2}",
                    BonusSource = "Automatic",
                    CreatedBy = 1, // System
                    StartDate = startDate,
                    EndDate = endDate
                };
                
                // حفظ الاستحقاق
                int id = _payrollRepository.AddBonus(bonus);
                
                LogManager.LogInfo($"تم إنشاء مكافأة العمل الإضافي للموظف {employeeID} بقيمة {amount:N2}");
                return id > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء إنشاء المكافآت التلقائية للموظف {employeeID}");
                return false;
            }
        }
        
        /// <summary>
        /// توليد البيانات التكاملية للراتب
        /// </summary>
        /// <param name="payrollID">رقم كشف الراتب</param>
        /// <returns>نتيجة التوليد</returns>
        public bool GeneratePayrollIntegrationData(int payrollID)
        {
            try
            {
                LogManager.LogInfo($"توليد البيانات التكاملية لكشف الراتب {payrollID}");
                
                // الحصول على بيانات كشف الراتب
                Payroll payroll = _payrollRepository.GetPayrollByID(payrollID);
                if (payroll == null)
                {
                    LogManager.LogError($"لم يتم العثور على كشف الراتب {payrollID}");
                    return false;
                }
                
                // تحديد فترة الكشف
                DateTime startDate = new DateTime(payroll.PayrollYear, payroll.PayrollMonth, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                
                // الحصول على قائمة الموظفين النشطين
                EmployeeRepository employeeRepo = new EmployeeRepository();
                List<Employee> employees = employeeRepo.GetActiveEmployees();
                
                if (employees == null || employees.Count == 0)
                {
                    LogManager.LogWarning("لم يتم العثور على موظفين نشطين");
                    return false;
                }
                
                // توليد البيانات لكل موظف
                int successCount = 0;
                foreach (Employee employee in employees)
                {
                    // إنشاء الخصومات التلقائية
                    int deductionsCount = GenerateAutomaticDeductions(employee.ID, startDate, endDate);
                    
                    // إنشاء المكافآت التلقائية
                    bool bonusesCreated = GenerateAutomaticBonuses(employee.ID, startDate, endDate);
                    
                    if (deductionsCount > 0 || bonusesCreated)
                    {
                        successCount++;
                    }
                }
                
                // تحديث حالة كشف الراتب
                payroll.IntegrationStatus = "Completed";
                payroll.IntegrationDate = DateTime.Now;
                _payrollRepository.UpdatePayroll(payroll);
                
                LogManager.LogInfo($"تم توليد البيانات التكاملية لـ {successCount} موظف من أصل {employees.Count}");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء توليد البيانات التكاملية لكشف الراتب {payrollID}");
                return false;
            }
        }
        
        /// <summary>
        /// حساب عدد أيام العمل في الفترة
        /// </summary>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <param name="workDaysSettings">إعدادات أيام العمل</param>
        /// <returns>عدد أيام العمل</returns>
        private int CalculateWorkDaysInPeriod(DateTime startDate, DateTime endDate, WorkDaysSettings workDaysSettings)
        {
            int count = 0;
            
            // استخراج أيام العمل من الإعدادات
            bool workOnSaturday = workDaysSettings.Saturday;
            bool workOnSunday = workDaysSettings.Sunday;
            bool workOnMonday = workDaysSettings.Monday;
            bool workOnTuesday = workDaysSettings.Tuesday;
            bool workOnWednesday = workDaysSettings.Wednesday;
            bool workOnThursday = workDaysSettings.Thursday;
            bool workOnFriday = workDaysSettings.Friday;
            
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // التحقق مما إذا كان اليوم يوم عمل
                bool isWorkDay = false;
                
                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                        isWorkDay = workOnSaturday;
                        break;
                    case DayOfWeek.Sunday:
                        isWorkDay = workOnSunday;
                        break;
                    case DayOfWeek.Monday:
                        isWorkDay = workOnMonday;
                        break;
                    case DayOfWeek.Tuesday:
                        isWorkDay = workOnTuesday;
                        break;
                    case DayOfWeek.Wednesday:
                        isWorkDay = workOnWednesday;
                        break;
                    case DayOfWeek.Thursday:
                        isWorkDay = workOnThursday;
                        break;
                    case DayOfWeek.Friday:
                        isWorkDay = workOnFriday;
                        break;
                }
                
                if (isWorkDay)
                {
                    // التحقق مما إذا كان اليوم إجازة رسمية
                    bool isHoliday = _attendanceRepository.IsHoliday(date);
                    
                    if (!isHoliday)
                    {
                        count++;
                    }
                }
            }
            
            return count;
        }
        
        /// <summary>
        /// حساب إجمالي دقائق التأخير
        /// </summary>
        /// <param name="records">سجلات الحضور</param>
        /// <param name="workDaysSettings">إعدادات أيام العمل</param>
        /// <returns>إجمالي دقائق التأخير</returns>
        private int CalculateLateMinutes(List<AttendanceRecord> records, WorkDaysSettings workDaysSettings)
        {
            int totalMinutes = 0;
            
            foreach (var record in records.Where(r => r.LogType == "CheckIn"))
            {
                DateTime recordDate = record.LogDateTime.Date;
                
                // تحديد وقت بداية الدوام لليوم
                TimeSpan workStartTime = GetWorkStartTimeForDay(recordDate.DayOfWeek, workDaysSettings);
                
                // حساب التأخير
                DateTime expectedCheckIn = recordDate.Add(workStartTime);
                TimeSpan lateTime = record.LogDateTime - expectedCheckIn;
                
                if (lateTime.TotalMinutes > 0)
                {
                    totalMinutes += (int)lateTime.TotalMinutes;
                }
            }
            
            return totalMinutes;
        }
        
        /// <summary>
        /// حساب إجمالي دقائق المغادرة المبكرة
        /// </summary>
        /// <param name="records">سجلات الحضور</param>
        /// <param name="workDaysSettings">إعدادات أيام العمل</param>
        /// <returns>إجمالي دقائق المغادرة المبكرة</returns>
        private int CalculateEarlyDepartureMinutes(List<AttendanceRecord> records, WorkDaysSettings workDaysSettings)
        {
            int totalMinutes = 0;
            
            foreach (var record in records.Where(r => r.LogType == "CheckOut"))
            {
                DateTime recordDate = record.LogDateTime.Date;
                
                // تحديد وقت نهاية الدوام لليوم
                TimeSpan workEndTime = GetWorkEndTimeForDay(recordDate.DayOfWeek, workDaysSettings);
                
                // حساب المغادرة المبكرة
                DateTime expectedCheckOut = recordDate.Add(workEndTime);
                TimeSpan earlyTime = expectedCheckOut - record.LogDateTime;
                
                if (earlyTime.TotalMinutes > 0)
                {
                    totalMinutes += (int)earlyTime.TotalMinutes;
                }
            }
            
            return totalMinutes;
        }
        
        /// <summary>
        /// حساب إجمالي ساعات العمل الإضافي
        /// </summary>
        /// <param name="records">سجلات الحضور</param>
        /// <param name="workDaysSettings">إعدادات أيام العمل</param>
        /// <returns>إجمالي ساعات العمل الإضافي</returns>
        private decimal CalculateOvertimeHours(List<AttendanceRecord> records, WorkDaysSettings workDaysSettings)
        {
            decimal totalHours = 0;
            
            // تجميع سجلات الحضور حسب اليوم
            var recordsByDate = records.GroupBy(r => r.LogDateTime.Date);
            
            foreach (var dayRecords in recordsByDate)
            {
                DateTime recordDate = dayRecords.Key;
                
                // الحصول على سجلات الحضور والانصراف لليوم
                var checkInRecord = dayRecords.FirstOrDefault(r => r.LogType == "CheckIn");
                var checkOutRecord = dayRecords.FirstOrDefault(r => r.LogType == "CheckOut");
                
                if (checkInRecord != null && checkOutRecord != null)
                {
                    // تحديد وقت نهاية الدوام لليوم
                    TimeSpan workEndTime = GetWorkEndTimeForDay(recordDate.DayOfWeek, workDaysSettings);
                    
                    // حساب العمل الإضافي
                    DateTime expectedCheckOut = recordDate.Add(workEndTime);
                    TimeSpan overtimeSpan = checkOutRecord.LogDateTime - expectedCheckOut;
                    
                    if (overtimeSpan.TotalMinutes > 0)
                    {
                        totalHours += (decimal)(overtimeSpan.TotalHours);
                    }
                }
            }
            
            return Math.Round(totalHours, 2);
        }
        
        /// <summary>
        /// الحصول على وقت بداية الدوام ليوم محدد
        /// </summary>
        /// <param name="dayOfWeek">اليوم</param>
        /// <param name="workDaysSettings">إعدادات أيام العمل</param>
        /// <returns>وقت بداية الدوام</returns>
        private TimeSpan GetWorkStartTimeForDay(DayOfWeek dayOfWeek, WorkDaysSettings workDaysSettings)
        {
            // افتراضياً، وقت بداية الدوام هو 8:00 صباحاً
            TimeSpan defaultStartTime = new TimeSpan(8, 0, 0);
            
            switch (dayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return workDaysSettings.SaturdayStartTime ?? defaultStartTime;
                case DayOfWeek.Sunday:
                    return workDaysSettings.SundayStartTime ?? defaultStartTime;
                case DayOfWeek.Monday:
                    return workDaysSettings.MondayStartTime ?? defaultStartTime;
                case DayOfWeek.Tuesday:
                    return workDaysSettings.TuesdayStartTime ?? defaultStartTime;
                case DayOfWeek.Wednesday:
                    return workDaysSettings.WednesdayStartTime ?? defaultStartTime;
                case DayOfWeek.Thursday:
                    return workDaysSettings.ThursdayStartTime ?? defaultStartTime;
                case DayOfWeek.Friday:
                    return workDaysSettings.FridayStartTime ?? defaultStartTime;
                default:
                    return defaultStartTime;
            }
        }
        
        /// <summary>
        /// الحصول على وقت نهاية الدوام ليوم محدد
        /// </summary>
        /// <param name="dayOfWeek">اليوم</param>
        /// <param name="workDaysSettings">إعدادات أيام العمل</param>
        /// <returns>وقت نهاية الدوام</returns>
        private TimeSpan GetWorkEndTimeForDay(DayOfWeek dayOfWeek, WorkDaysSettings workDaysSettings)
        {
            // افتراضياً، وقت نهاية الدوام هو 4:00 مساءً
            TimeSpan defaultEndTime = new TimeSpan(16, 0, 0);
            
            switch (dayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return workDaysSettings.SaturdayEndTime ?? defaultEndTime;
                case DayOfWeek.Sunday:
                    return workDaysSettings.SundayEndTime ?? defaultEndTime;
                case DayOfWeek.Monday:
                    return workDaysSettings.MondayEndTime ?? defaultEndTime;
                case DayOfWeek.Tuesday:
                    return workDaysSettings.TuesdayEndTime ?? defaultEndTime;
                case DayOfWeek.Wednesday:
                    return workDaysSettings.WednesdayEndTime ?? defaultEndTime;
                case DayOfWeek.Thursday:
                    return workDaysSettings.ThursdayEndTime ?? defaultEndTime;
                case DayOfWeek.Friday:
                    return workDaysSettings.FridayEndTime ?? defaultEndTime;
                default:
                    return defaultEndTime;
            }
        }
        
        /// <summary>
        /// حساب قيمة الخصم بناءً على قاعدة الخصم
        /// </summary>
        /// <param name="rule">قاعدة الخصم</param>
        /// <param name="value">القيمة</param>
        /// <param name="employeeID">رقم الموظف</param>
        /// <returns>قيمة الخصم</returns>
        private decimal CalculateDeductionAmount(DeductionRule rule, int value, int employeeID)
        {
            // الحصول على راتب الموظف
            EmployeeSalary salary = _salaryRepository.GetEmployeeActiveSalary(employeeID, DateTime.Now);
            if (salary == null)
            {
                return 0;
            }
            
            // الحصول على عناصر راتب الموظف
            List<EmployeeSalaryComponent> components = _salaryRepository.GetEmployeeSalaryComponents(salary.ID);
            if (components == null || components.Count == 0)
            {
                return 0;
            }
            
            // الحصول على الراتب الأساسي
            decimal baseSalary = 0;
            var basicComponent = components.FirstOrDefault(c => 
                _salaryRepository.GetSalaryComponentByID(c.ComponentID).ComponentType == "Basic");
            
            if (basicComponent != null)
            {
                baseSalary = basicComponent.Amount;
            }
            
            if (baseSalary <= 0)
            {
                return 0;
            }
            
            switch (rule.Formula)
            {
                case "FixedAmount":
                    return rule.Amount;
                
                case "PerDay":
                    // قيمة ثابتة لكل يوم
                    return value * rule.Amount;
                
                case "PerDayPercent":
                    // نسبة من الراتب اليومي لكل يوم
                    decimal dayRate = baseSalary / 30;
                    return value * (dayRate * rule.Amount / 100);
                
                case "PerHour":
                    // قيمة ثابتة لكل ساعة
                    decimal hours = value / 60;
                    return hours * rule.Amount;
                
                case "PerHourPercent":
                    // نسبة من الراتب الساعي لكل ساعة
                    decimal hourRate = baseSalary / 30 / 8;
                    decimal hoursValue = value / 60;
                    return hoursValue * (hourRate * rule.Amount / 100);
                
                case "PerMinute":
                    // قيمة ثابتة لكل دقيقة
                    return value * rule.Amount;
                
                case "PerMinutePercent":
                    // نسبة من الراتب الدقيقي لكل دقيقة
                    decimal minuteRate = baseSalary / 30 / 8 / 60;
                    return value * (minuteRate * rule.Amount / 100);
                
                default:
                    return 0;
            }
        }
        
        /// <summary>
        /// حساب قيمة ساعة العمل الإضافي
        /// </summary>
        /// <param name="employeeID">رقم الموظف</param>
        /// <param name="salary">راتب الموظف</param>
        /// <returns>قيمة ساعة العمل الإضافي</returns>
        private decimal CalculateOvertimeHourlyRate(int employeeID, EmployeeSalary salary)
        {
            try
            {
                // الحصول على إعدادات العمل الإضافي
                var overtimeSettings = _settingsManager.GetOvertimeSettings();
                
                // الحصول على عناصر راتب الموظف
                List<EmployeeSalaryComponent> components = _salaryRepository.GetEmployeeSalaryComponents(salary.ID);
                
                // حساب الراتب الأساسي والعناصر التي تؤثر على العمل الإضافي
                decimal baseSalary = 0;
                decimal affectingComponents = 0;
                
                foreach (var component in components)
                {
                    var salaryComponent = _salaryRepository.GetSalaryComponentByID(component.ComponentID);
                    if (salaryComponent != null)
                    {
                        if (salaryComponent.ComponentType == "Basic")
                        {
                            baseSalary = component.Amount;
                        }
                        else if (salaryComponent.AffectsOvertime)
                        {
                            affectingComponents += component.Amount;
                        }
                    }
                }
                
                // حساب المبلغ الإجمالي الذي يتم احتساب العمل الإضافي عليه
                decimal totalAmount = baseSalary + affectingComponents;
                
                // حساب معدل الساعة
                decimal hourlyRate = totalAmount / overtimeSettings.DaysPerMonth / overtimeSettings.HoursPerDay;
                
                // تطبيق معامل العمل الإضافي
                decimal overtimeRate = hourlyRate * overtimeSettings.OvertimeMultiplier;
                
                return Math.Round(overtimeRate, 2);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء حساب قيمة ساعة العمل الإضافي للموظف {employeeID}");
                
                // قيمة افتراضية
                return (salary.BaseSalary / 30 / 8) * 1.5m;
            }
        }
    }
    
    /// <summary>
    /// ملخص بيانات الحضور
    /// </summary>
    public class AttendanceSummary
    {
        /// <summary>
        /// رقم الموظف
        /// </summary>
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// تاريخ بداية الفترة
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// تاريخ نهاية الفترة
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// عدد أيام العمل
        /// </summary>
        public int WorkDaysCount { get; set; }
        
        /// <summary>
        /// عدد أيام الحضور
        /// </summary>
        public int AttendanceDaysCount { get; set; }
        
        /// <summary>
        /// عدد أيام الغياب
        /// </summary>
        public int AbsenceDaysCount { get; set; }
        
        /// <summary>
        /// إجمالي دقائق التأخير
        /// </summary>
        public int LateMinutesTotal { get; set; }
        
        /// <summary>
        /// إجمالي دقائق المغادرة المبكرة
        /// </summary>
        public int EarlyDepartureMinutesTotal { get; set; }
        
        /// <summary>
        /// إجمالي ساعات العمل الإضافي
        /// </summary>
        public decimal OvertimeHoursTotal { get; set; }
    }
    
    /// <summary>
    /// ملخص بيانات الخصومات
    /// </summary>
    public class DeductionSummary
    {
        /// <summary>
        /// رقم الموظف
        /// </summary>
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// تاريخ بداية الفترة
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// تاريخ نهاية الفترة
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// عدد الخصومات اليدوية
        /// </summary>
        public int ManualDeductionsCount { get; set; }
        
        /// <summary>
        /// إجمالي الخصومات اليدوية
        /// </summary>
        public decimal ManualDeductionsTotal { get; set; }
        
        /// <summary>
        /// عدد الخصومات التلقائية
        /// </summary>
        public int AutomaticDeductionsCount { get; set; }
        
        /// <summary>
        /// إجمالي الخصومات التلقائية
        /// </summary>
        public decimal AutomaticDeductionsTotal { get; set; }
    }
    
    /// <summary>
    /// مكافأة أو استحقاق
    /// </summary>
    public class Bonus
    {
        /// <summary>
        /// الرقم
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// رقم الموظف
        /// </summary>
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// رقم نوع المكافأة
        /// </summary>
        public int BonusTypeID { get; set; }
        
        /// <summary>
        /// اسم نوع المكافأة
        /// </summary>
        public string BonusTypeName { get; set; }
        
        /// <summary>
        /// تاريخ المكافأة
        /// </summary>
        public DateTime BonusDate { get; set; }
        
        /// <summary>
        /// المبلغ
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// مصدر المكافأة (يدوي/تلقائي)
        /// </summary>
        public string BonusSource { get; set; }
        
        /// <summary>
        /// بواسطة
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// تاريخ البداية
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// تاريخ النهاية
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}