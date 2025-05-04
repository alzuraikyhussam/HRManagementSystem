using System;
using System.Collections.Generic;
using System.Linq;
using HR.Models.DTOs;

namespace HR.Core
{
    /// <summary>
    /// مدير لوحة المعلومات
    /// </summary>
    public static class DashboardManager
    {
        /// <summary>
        /// الحصول على بيانات لوحة المعلومات
        /// </summary>
        /// <param name="fromDate">من تاريخ</param>
        /// <param name="toDate">إلى تاريخ</param>
        /// <returns>بيانات لوحة المعلومات</returns>
        public static DashboardDataDTO GetDashboardData(DateTime fromDate, DateTime toDate)
        {
            try
            {
                // استخدام UnitOfWork للوصول إلى البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    // إنشاء كائن بيانات لوحة المعلومات
                    DashboardDataDTO dashboardData = new DashboardDataDTO();
                    
                    // الحصول على إحصائيات الموظفين
                    dashboardData.EmployeeStatistics = GetEmployeeStatistics(unitOfWork);
                    
                    // الحصول على إحصائيات الحضور
                    dashboardData.AttendanceStatistics = GetAttendanceStatistics(unitOfWork);
                    
                    // الحصول على إحصائيات الإجازات
                    dashboardData.LeaveStatistics = GetLeaveStatistics(unitOfWork, fromDate, toDate);
                    
                    // الحصول على إحصائيات الرواتب
                    dashboardData.PayrollStatistics = GetPayrollStatistics(unitOfWork, fromDate, toDate);
                    
                    // الحصول على الإجازات الحالية
                    dashboardData.CurrentLeaves = GetCurrentLeaves(unitOfWork);
                    
                    // الحصول على بيانات مخطط الحضور
                    dashboardData.AttendanceChartData = GetAttendanceChartData(unitOfWork, fromDate, toDate);
                    
                    // الحصول على بيانات مخطط اتجاهات الحضور
                    dashboardData.AttendanceTrendData = GetAttendanceTrendData(unitOfWork, fromDate, toDate);
                    
                    // الحصول على بيانات مخطط الإجازات
                    dashboardData.LeavesChartData = GetLeavesChartData(unitOfWork, fromDate, toDate);
                    
                    // الحصول على بيانات مخطط الحضور حسب القسم
                    dashboardData.DepartmentAttendanceData = GetDepartmentAttendanceData(unitOfWork, fromDate, toDate);
                    
                    // الحصول على بيانات مخطط الحضور حسب أيام الأسبوع
                    dashboardData.WeekdayAttendanceData = GetWeekdayAttendanceData(unitOfWork, fromDate, toDate);
                    
                    // الحصول على الإشعارات
                    dashboardData.Notifications = GetNotifications(unitOfWork);
                    
                    // الحصول على الأحداث القادمة
                    dashboardData.UpcomingEvents = GetUpcomingEvents(unitOfWork);
                    
                    // الحصول على بيانات مؤشرات الأداء الرئيسية
                    dashboardData.KPIData = GetKPIData(unitOfWork, fromDate, toDate);
                    
                    return dashboardData;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على بيانات لوحة المعلومات");
                throw;
            }
        }

        /// <summary>
        /// الحصول على إحصائيات الموظفين
        /// </summary>
        private static EmployeeStatisticsDTO GetEmployeeStatistics(UnitOfWork unitOfWork)
        {
            try
            {
                // في التطبيق الفعلي، سيتم الحصول على هذه البيانات من قاعدة البيانات
                // هنا سنستخدم بيانات وهمية للتوضيح
                
                // الحصول على عدد الموظفين
                int totalEmployees = unitOfWork.EmployeeRepository.GetCount();
                
                // الحصول على عدد الموظفين النشطين
                int activeEmployees = unitOfWork.EmployeeRepository.GetCount(e => e.IsActive);
                
                // الحصول على عدد الموظفين الجدد (تم توظيفهم في الشهر الحالي)
                DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                int newEmployees = unitOfWork.EmployeeRepository.GetCount(e => e.HireDate >= firstDayOfMonth);
                
                // الحصول على عدد الموظفين المغادرين (تم إنهاء خدمتهم)
                int terminatedEmployees = unitOfWork.EmployeeRepository.GetCount(e => e.TerminationDate != null);
                
                return new EmployeeStatisticsDTO
                {
                    TotalEmployees = totalEmployees,
                    ActiveEmployees = activeEmployees,
                    NewEmployees = newEmployees,
                    TerminatedEmployees = terminatedEmployees
                };
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على إحصائيات الموظفين");
                throw;
            }
        }

        /// <summary>
        /// الحصول على إحصائيات الحضور
        /// </summary>
        private static AttendanceStatisticsDTO GetAttendanceStatistics(UnitOfWork unitOfWork)
        {
            try
            {
                // في التطبيق الفعلي، سيتم الحصول على هذه البيانات من قاعدة البيانات
                // هنا سنستخدم بيانات وهمية للتوضيح
                
                // الحصول على عدد الموظفين الإجمالي
                int totalEmployees = unitOfWork.EmployeeRepository.GetCount(e => e.IsActive);
                
                // الحصول على عدد الموظفين الحاضرين اليوم
                DateTime today = DateTime.Now.Date;
                int presentToday = unitOfWork.AttendanceRepository.GetCount(a => a.Date == today && a.CheckInTime != null);
                
                // الحصول على عدد الموظفين الغائبين اليوم
                int absentToday = totalEmployees - presentToday - GetOnLeaveToday(unitOfWork);
                
                // الحصول على عدد الموظفين المتأخرين اليوم
                TimeSpan lateThreshold = new TimeSpan(9, 0, 0); // 9:00 صباحاً
                int lateToday = unitOfWork.AttendanceRepository.GetCount(a => a.Date == today && a.CheckInTime != null && a.CheckInTime.Value.TimeOfDay > lateThreshold);
                
                // الحصول على عدد الموظفين في إجازة اليوم
                int onLeaveToday = GetOnLeaveToday(unitOfWork);
                
                return new AttendanceStatisticsDTO
                {
                    TotalEmployees = totalEmployees,
                    PresentToday = presentToday,
                    AbsentToday = absentToday,
                    LateToday = lateToday,
                    OnLeaveToday = onLeaveToday
                };
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على إحصائيات الحضور");
                throw;
            }
        }

        /// <summary>
        /// الحصول على عدد الموظفين في إجازة اليوم
        /// </summary>
        private static int GetOnLeaveToday(UnitOfWork unitOfWork)
        {
            try
            {
                DateTime today = DateTime.Now.Date;
                return unitOfWork.LeaveRepository.GetCount(l => l.StartDate <= today && l.EndDate >= today && l.Status == "Approved");
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على عدد الموظفين في إجازة اليوم");
                throw;
            }
        }

        /// <summary>
        /// الحصول على الإجازات الحالية
        /// </summary>
        private static List<LeaveDTO> GetCurrentLeaves(UnitOfWork unitOfWork)
        {
            try
            {
                // في التطبيق الفعلي، سيتم الحصول على هذه البيانات من قاعدة البيانات
                DateTime today = DateTime.Now.Date;
                
                // الحصول على الإجازات النشطة
                var leaves = unitOfWork.LeaveRepository.GetActiveLeaves(today);
                
                // تحويل البيانات إلى DTOs
                return leaves.Select(l => new LeaveDTO
                {
                    ID = l.ID,
                    EmployeeId = l.EmployeeID,
                    EmployeeName = $"{l.Employee.FirstName} {l.Employee.LastName}",
                    LeaveType = l.LeaveType.Name,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    Duration = (l.EndDate - l.StartDate).Days + 1,
                    Status = l.Status
                }).ToList();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على الإجازات الحالية");
                throw;
            }
        }

        /// <summary>
        /// الحصول على بيانات مخطط الحضور
        /// </summary>
        private static List<ChartDataDTO> GetAttendanceChartData(UnitOfWork unitOfWork, DateTime fromDate, DateTime toDate)
        {
            try
            {
                // في التطبيق الفعلي، سيتم الحصول على هذه البيانات من قاعدة البيانات
                // هنا سنستخدم بيانات وهمية للتوضيح
                
                // إنشاء قائمة النتائج
                List<ChartDataDTO> result = new List<ChartDataDTO>();
                
                // الحصول على بيانات الحضور من قاعدة البيانات
                var attendanceData = unitOfWork.AttendanceRepository.GetAttendanceByDateRange(fromDate, toDate);
                
                // تجميع البيانات حسب التاريخ
                var groupedData = attendanceData.GroupBy(a => a.Date.Date)
                    .Select(g => new
                    {
                        Date = g.Key,
                        Count = g.Count()
                    })
                    .OrderBy(g => g.Date)
                    .ToList();
                
                // إضافة البيانات إلى النتيجة
                foreach (var item in groupedData)
                {
                    result.Add(new ChartDataDTO
                    {
                        CategoryName = item.Date.ToString("yyyy-MM-dd"),
                        Value = item.Count
                    });
                }
                
                return result;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على بيانات مخطط الحضور");
                throw;
            }
        }

        /// <summary>
        /// الحصول على بيانات مخطط الإجازات
        /// </summary>
        private static List<ChartDataDTO> GetLeavesChartData(UnitOfWork unitOfWork, DateTime fromDate, DateTime toDate)
        {
            try
            {
                // في التطبيق الفعلي، سيتم الحصول على هذه البيانات من قاعدة البيانات
                // هنا سنستخدم بيانات وهمية للتوضيح
                
                // إنشاء قائمة النتائج
                List<ChartDataDTO> result = new List<ChartDataDTO>();
                
                // الحصول على بيانات الإجازات من قاعدة البيانات
                var leavesData = unitOfWork.LeaveRepository.GetLeavesByDateRange(fromDate, toDate);
                
                // تجميع البيانات حسب نوع الإجازة
                var groupedData = leavesData.GroupBy(l => l.LeaveType.Name)
                    .Select(g => new
                    {
                        LeaveTypeName = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(g => g.Count)
                    .ToList();
                
                // إضافة البيانات إلى النتيجة
                foreach (var item in groupedData)
                {
                    result.Add(new ChartDataDTO
                    {
                        CategoryName = item.LeaveTypeName,
                        Value = item.Count
                    });
                }
                
                return result;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على بيانات مخطط الإجازات");
                throw;
            }
        }

        /// <summary>
        /// الحصول على الإشعارات
        /// </summary>
        private static List<NotificationDTO> GetNotifications(UnitOfWork unitOfWork)
        {
            try
            {
                // في التطبيق الفعلي، سيتم الحصول على هذه البيانات من قاعدة البيانات
                // هنا سنستخدم بيانات وهمية للتوضيح
                
                // إنشاء قائمة النتائج
                List<NotificationDTO> result = new List<NotificationDTO>();
                
                // إضافة بعض الإشعارات للتوضيح
                result.Add(new NotificationDTO
                {
                    ID = 1,
                    Type = "Warning",
                    Title = "تنبيه: موظفون متغيبون",
                    Message = "يوجد 3 موظفين متغيبين اليوم بدون إذن مسبق",
                    Date = DateTime.Now.AddHours(-2)
                });
                
                result.Add(new NotificationDTO
                {
                    ID = 2,
                    Type = "Info",
                    Title = "تذكير: اجتماع قسم الموارد البشرية",
                    Message = "سيعقد اجتماع قسم الموارد البشرية اليوم الساعة 2:00 مساءً",
                    Date = DateTime.Now.AddHours(-5)
                });
                
                result.Add(new NotificationDTO
                {
                    ID = 3,
                    Type = "Error",
                    Title = "خطأ: فشل في استيراد بيانات البصمة",
                    Message = "فشل في استيراد بيانات البصمة من الجهاز رقم 2. يرجى التحقق من الاتصال",
                    Date = DateTime.Now.AddDays(-1)
                });
                
                // ترتيب الإشعارات حسب التاريخ
                return result.OrderByDescending(n => n.Date).ToList();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على الإشعارات");
                throw;
            }
        }

        /// <summary>
        /// الحصول على الأحداث القادمة
        /// </summary>
        private static List<EventDTO> GetUpcomingEvents(UnitOfWork unitOfWork)
        {
            try
            {
                // في التطبيق الفعلي، سيتم الحصول على هذه البيانات من قاعدة البيانات
                // هنا سنستخدم بيانات وهمية للتوضيح
                
                // إنشاء قائمة النتائج
                List<EventDTO> result = new List<EventDTO>();
                
                // إضافة بعض الأحداث للتوضيح
                result.Add(new EventDTO
                {
                    ID = 1,
                    Type = "Birthday",
                    Title = "عيد ميلاد: أحمد محمد",
                    Description = "عيد ميلاد الموظف أحمد محمد",
                    Date = DateTime.Now.AddDays(2)
                });
                
                result.Add(new EventDTO
                {
                    ID = 2,
                    Type = "Meeting",
                    Title = "اجتماع: مراجعة الأداء",
                    Description = "اجتماع مراجعة أداء الموظفين للربع الأول",
                    Date = DateTime.Now.AddDays(5)
                });
                
                result.Add(new EventDTO
                {
                    ID = 3,
                    Type = "Holiday",
                    Title = "عطلة: عيد الفطر",
                    Description = "عطلة عيد الفطر المبارك",
                    Date = DateTime.Now.AddDays(10)
                });
                
                result.Add(new EventDTO
                {
                    ID = 4,
                    Type = "Anniversary",
                    Title = "ذكرى: سنة عمل سارة أحمد",
                    Description = "مرت سنة على انضمام سارة أحمد للشركة",
                    Date = DateTime.Now.AddDays(3)
                });
                
                // ترتيب الأحداث حسب التاريخ
                return result.OrderBy(e => e.Date).ToList();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على الأحداث القادمة");
                throw;
            }
        }
        /// <summary>
        /// الحصول على إحصائيات الإجازات
        /// </summary>
        private static LeaveStatisticsDTO GetLeaveStatistics(UnitOfWork unitOfWork, DateTime fromDate, DateTime toDate)
        {
            try
            {
                // إنشاء كائن إحصائيات الإجازات
                LeaveStatisticsDTO statistics = new LeaveStatisticsDTO();
                
                // الحصول على بيانات الإجازات للفترة المحددة
                var leavesData = unitOfWork.LeaveRepository.GetLeavesByDateRange(fromDate, toDate);
                
                // حساب إجمالي عدد الإجازات
                statistics.TotalLeaves = leavesData.Count;
                
                // تصنيف الإجازات حسب النوع
                statistics.SickLeaves = leavesData.Count(l => l.LeaveType.Name == "مرضية");
                statistics.AnnualLeaves = leavesData.Count(l => l.LeaveType.Name == "سنوية");
                statistics.EmergencyLeaves = leavesData.Count(l => l.LeaveType.Name == "طارئة");
                statistics.ReligiousLeaves = leavesData.Count(l => l.LeaveType.Name == "حج" || l.LeaveType.Name == "عمرة");
                statistics.UnpaidLeaves = leavesData.Count(l => l.LeaveType.Name == "غير مدفوعة");
                
                // حساب متوسط أيام الإجازة لكل موظف
                int activeEmployeesCount = unitOfWork.EmployeeRepository.GetCount(e => e.IsActive);
                if (activeEmployeesCount > 0)
                {
                    int totalLeaveDays = leavesData.Sum(l => (l.EndDate - l.StartDate).Days + 1);
                    statistics.AverageLeaveDaysPerEmployee = Math.Round((decimal)totalLeaveDays / activeEmployeesCount, 2);
                }
                
                // مقارنة الإجازات المرضية بالشهر السابق
                DateTime lastMonthStart = fromDate.AddMonths(-1);
                DateTime lastMonthEnd = toDate.AddMonths(-1);
                
                var lastMonthLeaves = unitOfWork.LeaveRepository.GetLeavesByDateRange(lastMonthStart, lastMonthEnd);
                int sickLeavesLastMonth = lastMonthLeaves.Count(l => l.LeaveType.Name == "مرضية");
                
                if (sickLeavesLastMonth > 0)
                {
                    statistics.SickLeaveChangePercent = Math.Round(((decimal)statistics.SickLeaves - sickLeavesLastMonth) / sickLeavesLastMonth * 100, 2);
                }
                
                // معدل الإجازات المقدمة مقابل الخطة السنوية
                var annualLeavesThisYear = unitOfWork.LeaveRepository.GetAllLeavesByType(
                    new DateTime(DateTime.Now.Year, 1, 1), 
                    new DateTime(DateTime.Now.Year, 12, 31), 
                    "سنوية");
                    
                int totalAnnualLeavesDays = annualLeavesThisYear.Sum(l => (l.EndDate - l.StartDate).Days + 1);
                
                int totalAnnualLeavesAllowance = unitOfWork.EmployeeRepository.GetTotalAnnualLeavesEntitlement();
                
                if (totalAnnualLeavesAllowance > 0)
                {
                    statistics.LeaveUtilizationRate = Math.Round((decimal)totalAnnualLeavesDays / totalAnnualLeavesAllowance * 100, 2);
                }
                
                return statistics;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على إحصائيات الإجازات");
                throw;
            }
        }
        
        /// <summary>
        /// الحصول على إحصائيات الرواتب
        /// </summary>
        private static PayrollStatisticsDTO GetPayrollStatistics(UnitOfWork unitOfWork, DateTime fromDate, DateTime toDate)
        {
            try
            {
                // إنشاء كائن إحصائيات الرواتب
                PayrollStatisticsDTO statistics = new PayrollStatisticsDTO();
                
                // الحصول على بيانات الرواتب للفترة الحالية
                var currentPayrolls = unitOfWork.PayrollRepository.GetPayrollsByDateRange(fromDate, toDate);
                decimal currentPeriodTotal = currentPayrolls.Sum(p => p.NetSalary);
                statistics.TotalSalariesCurrentPeriod = currentPeriodTotal;
                
                // الحصول على بيانات الرواتب للفترة السابقة
                DateTime previousPeriodStart = fromDate.AddMonths(-1);
                DateTime previousPeriodEnd = toDate.AddMonths(-1);
                
                var previousPayrolls = unitOfWork.PayrollRepository.GetPayrollsByDateRange(previousPeriodStart, previousPeriodEnd);
                decimal previousPeriodTotal = previousPayrolls.Sum(p => p.NetSalary);
                statistics.TotalSalariesPreviousPeriod = previousPeriodTotal;
                
                // حساب نسبة التغيير
                if (previousPeriodTotal > 0)
                {
                    statistics.SalaryChangePercent = Math.Round(
                        (currentPeriodTotal - previousPeriodTotal) / previousPeriodTotal * 100, 2);
                }
                
                // إجمالي البدلات
                statistics.TotalAllowances = currentPayrolls.Sum(p => p.TotalAllowances);
                
                // إجمالي الخصومات
                statistics.TotalDeductions = currentPayrolls.Sum(p => p.TotalDeductions);
                
                // إجمالي الخصومات المتعلقة بالحضور
                statistics.AttendanceRelatedDeductions = currentPayrolls.Sum(p => p.AttendanceDeductions);
                
                // توزيع الرواتب حسب الإدارات
                statistics.SalaryByDepartment = GetSalaryByDepartment(unitOfWork, fromDate, toDate);
                
                // متوسط الراتب حسب المستوى الوظيفي
                statistics.AverageSalaryByLevel = GetAverageSalaryByLevel(unitOfWork, fromDate, toDate);
                
                return statistics;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على إحصائيات الرواتب");
                throw;
            }
        }
        
        /// <summary>
        /// الحصول على توزيع الرواتب حسب الإدارات
        /// </summary>
        private static List<ChartDataDTO> GetSalaryByDepartment(UnitOfWork unitOfWork, DateTime fromDate, DateTime toDate)
        {
            try
            {
                // إنشاء قائمة بيانات المخطط
                List<ChartDataDTO> chartData = new List<ChartDataDTO>();
                
                // الحصول على بيانات الرواتب موزعة حسب الإدارات
                var departmentSalaries = unitOfWork.PayrollRepository.GetSalariesByDepartment(fromDate, toDate);
                
                // تحويل البيانات إلى كائنات ChartDataDTO
                foreach (var dept in departmentSalaries)
                {
                    chartData.Add(new ChartDataDTO
                    {
                        CategoryName = dept.DepartmentName,
                        Value = (int)dept.TotalSalary,
                        ColorCode = GetDepartmentColor(dept.DepartmentName)
                    });
                }
                
                return chartData;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على توزيع الرواتب حسب الإدارات");
                throw;
            }
        }
        
        /// <summary>
        /// الحصول على متوسط الراتب حسب المستوى الوظيفي
        /// </summary>
        private static List<ChartDataDTO> GetAverageSalaryByLevel(UnitOfWork unitOfWork, DateTime fromDate, DateTime toDate)
        {
            try
            {
                // إنشاء قائمة بيانات المخطط
                List<ChartDataDTO> chartData = new List<ChartDataDTO>();
                
                // الحصول على بيانات متوسط الراتب حسب المستوى الوظيفي
                var salariesByLevel = unitOfWork.PayrollRepository.GetAverageSalaryByLevel(fromDate, toDate);
                
                // تحويل البيانات إلى كائنات ChartDataDTO
                foreach (var level in salariesByLevel)
                {
                    chartData.Add(new ChartDataDTO
                    {
                        CategoryName = level.JobLevel,
                        Value = (int)level.AverageSalary
                    });
                }
                
                return chartData;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على متوسط الراتب حسب المستوى الوظيفي");
                throw;
            }
        }
        
        /// <summary>
        /// الحصول على بيانات مخطط اتجاهات الحضور
        /// </summary>
        private static AttendanceTrendChartDataDTO GetAttendanceTrendData(UnitOfWork unitOfWork, DateTime fromDate, DateTime toDate)
        {
            try
            {
                // إنشاء كائن بيانات مخطط اتجاهات الحضور
                AttendanceTrendChartDataDTO trendData = new AttendanceTrendChartDataDTO
                {
                    TimeCategories = new List<string>(),
                    AttendanceRates = new List<decimal>(),
                    LateRates = new List<decimal>(),
                    AbsenceRates = new List<decimal>()
                };
                
                // تحديد عدد الأشهر للعرض (بحد أقصى 12 شهر)
                int monthsToShow = Math.Min(12, 
                    ((toDate.Year - fromDate.Year) * 12) + toDate.Month - fromDate.Month + 1);
                
                // ضبط تاريخ البداية على أول يوم في الشهر
                DateTime startDate = new DateTime(fromDate.Year, fromDate.Month, 1);
                
                for (int i = 0; i < monthsToShow; i++)
                {
                    DateTime currentMonth = startDate.AddMonths(i);
                    DateTime monthStart = new DateTime(currentMonth.Year, currentMonth.Month, 1);
                    DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1);
                    
                    // إضافة اسم الشهر/السنة للفئات
                    trendData.TimeCategories.Add(monthStart.ToString("MM/yyyy"));
                    
                    // حساب معدلات الحضور والتأخير والغياب للشهر
                    int totalEmployees = unitOfWork.EmployeeRepository.GetCount(e => e.IsActive && e.HireDate <= monthEnd);
                    
                    // حساب إجمالي أيام العمل في الشهر
                    int workingDaysInMonth = GetWorkingDaysInMonth(monthStart);
                    
                    // إجمالي سجلات الحضور للشهر
                    var monthAttendance = unitOfWork.AttendanceRepository.GetAttendanceByDateRange(monthStart, monthEnd);
                    
                    // إجمالي سجلات التأخير للشهر
                    TimeSpan lateThreshold = new TimeSpan(9, 0, 0); // 9:00 صباحاً
                    int lateCount = monthAttendance.Count(a => a.CheckInTime != null && a.CheckInTime.Value.TimeOfDay > lateThreshold);
                    
                    // حساب معدل الحضور
                    decimal attendanceRate = 0;
                    if (totalEmployees > 0 && workingDaysInMonth > 0)
                    {
                        decimal expectedAttendance = totalEmployees * workingDaysInMonth;
                        decimal actualAttendance = monthAttendance.Count;
                        attendanceRate = Math.Round(actualAttendance / expectedAttendance * 100, 2);
                    }
                    trendData.AttendanceRates.Add(attendanceRate);
                    
                    // حساب معدل التأخير
                    decimal lateRate = 0;
                    if (monthAttendance.Count > 0)
                    {
                        lateRate = Math.Round((decimal)lateCount / monthAttendance.Count * 100, 2);
                    }
                    trendData.LateRates.Add(lateRate);
                    
                    // حساب معدل الغياب
                    decimal absenceRate = 0;
                    if (totalEmployees > 0 && workingDaysInMonth > 0)
                    {
                        decimal expectedAttendance = totalEmployees * workingDaysInMonth;
                        decimal actualAttendance = monthAttendance.Count;
                        absenceRate = Math.Round(100 - (actualAttendance / expectedAttendance * 100), 2);
                    }
                    trendData.AbsenceRates.Add(absenceRate);
                }
                
                return trendData;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على بيانات مخطط اتجاهات الحضور");
                throw;
            }
        }
        
        /// <summary>
        /// الحصول على بيانات مخطط الحضور حسب القسم
        /// </summary>
        private static DepartmentAttendanceDataDTO GetDepartmentAttendanceData(UnitOfWork unitOfWork, DateTime fromDate, DateTime toDate)
        {
            try
            {
                // إنشاء كائن بيانات مخطط الحضور حسب القسم
                DepartmentAttendanceDataDTO deptData = new DepartmentAttendanceDataDTO
                {
                    DepartmentNames = new List<string>(),
                    AttendanceRates = new List<decimal>(),
                    LateRates = new List<decimal>(),
                    LeaveRates = new List<decimal>()
                };
                
                // الحصول على جميع الأقسام
                var departments = unitOfWork.DepartmentRepository.GetAll();
                
                foreach (var dept in departments)
                {
                    deptData.DepartmentNames.Add(dept.Name);
                    
                    // الحصول على الموظفين في القسم
                    var employees = unitOfWork.EmployeeRepository.GetByDepartment(dept.ID);
                    int totalEmployeesInDept = employees.Count(e => e.IsActive);
                    
                    if (totalEmployeesInDept == 0)
                    {
                        deptData.AttendanceRates.Add(0);
                        deptData.LateRates.Add(0);
                        deptData.LeaveRates.Add(0);
                        continue;
                    }
                    
                    // حساب إجمالي أيام العمل في الفترة
                    int workingDaysInPeriod = GetWorkingDaysInRange(fromDate, toDate);
                    
                    // إجمالي الحضور المتوقع
                    decimal expectedAttendance = totalEmployeesInDept * workingDaysInPeriod;
                    
                    // الحصول على سجلات الحضور للموظفين في القسم
                    var employeeIds = employees.Where(e => e.IsActive).Select(e => e.ID).ToList();
                    var attendance = unitOfWork.AttendanceRepository.GetAttendanceByEmployeeIds(employeeIds, fromDate, toDate);
                    
                    // حساب معدل الحضور
                    decimal attendanceRate = 0;
                    if (expectedAttendance > 0)
                    {
                        attendanceRate = Math.Round(attendance.Count / expectedAttendance * 100, 2);
                    }
                    deptData.AttendanceRates.Add(attendanceRate);
                    
                    // حساب معدل التأخير
                    TimeSpan lateThreshold = new TimeSpan(9, 0, 0); // 9:00 صباحاً
                    int lateCount = attendance.Count(a => a.CheckInTime != null && a.CheckInTime.Value.TimeOfDay > lateThreshold);
                    decimal lateRate = 0;
                    if (attendance.Count > 0)
                    {
                        lateRate = Math.Round((decimal)lateCount / attendance.Count * 100, 2);
                    }
                    deptData.LateRates.Add(lateRate);
                    
                    // حساب معدل الإجازات
                    var leaves = unitOfWork.LeaveRepository.GetLeavesByEmployeeIds(employeeIds, fromDate, toDate);
                    int totalLeaveDays = leaves.Sum(l => (l.EndDate - l.StartDate).Days + 1);
                    decimal leaveRate = 0;
                    if (expectedAttendance > 0)
                    {
                        leaveRate = Math.Round((decimal)totalLeaveDays / expectedAttendance * 100, 2);
                    }
                    deptData.LeaveRates.Add(leaveRate);
                }
                
                return deptData;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على بيانات مخطط الحضور حسب القسم");
                throw;
            }
        }
        
        /// <summary>
        /// الحصول على بيانات مخطط الحضور حسب أيام الأسبوع
        /// </summary>
        private static WeekdayAttendanceDataDTO GetWeekdayAttendanceData(UnitOfWork unitOfWork, DateTime fromDate, DateTime toDate)
        {
            try
            {
                // إنشاء كائن بيانات مخطط الحضور حسب أيام الأسبوع
                WeekdayAttendanceDataDTO weekdayData = new WeekdayAttendanceDataDTO
                {
                    Weekdays = new List<string> { "الأحد", "الإثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت" },
                    AttendanceRates = new List<decimal>(),
                    LateRates = new List<decimal>(),
                    AbsenceRates = new List<decimal>()
                };
                
                // الحصول على جميع سجلات الحضور خلال الفترة
                var attendance = unitOfWork.AttendanceRepository.GetAttendanceByDateRange(fromDate, toDate);
                
                // الحصول على عدد الموظفين النشطين
                int totalActiveEmployees = unitOfWork.EmployeeRepository.GetCount(e => e.IsActive);
                
                // معالجة البيانات لكل يوم من أيام الأسبوع
                for (int dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++)
                {
                    // عدد الأيام من هذا اليوم في الفترة
                    int daysOfThisWeekday = 0;
                    for (DateTime day = fromDate; day <= toDate; day = day.AddDays(1))
                    {
                        if ((int)day.DayOfWeek == dayOfWeek)
                        {
                            daysOfThisWeekday++;
                        }
                    }
                    
                    // إجمالي الحضور المتوقع لهذا اليوم
                    decimal expectedAttendance = totalActiveEmployees * daysOfThisWeekday;
                    
                    // الحضور الفعلي لهذا اليوم
                    var dayAttendance = attendance.Where(a => (int)a.Date.DayOfWeek == dayOfWeek).ToList();
                    
                    // حساب معدل الحضور
                    decimal attendanceRate = 0;
                    if (expectedAttendance > 0)
                    {
                        attendanceRate = Math.Round(dayAttendance.Count / expectedAttendance * 100, 2);
                    }
                    weekdayData.AttendanceRates.Add(attendanceRate);
                    
                    // حساب معدل التأخير
                    TimeSpan lateThreshold = new TimeSpan(9, 0, 0); // 9:00 صباحاً
                    int lateCount = dayAttendance.Count(a => a.CheckInTime != null && a.CheckInTime.Value.TimeOfDay > lateThreshold);
                    decimal lateRate = 0;
                    if (dayAttendance.Count > 0)
                    {
                        lateRate = Math.Round((decimal)lateCount / dayAttendance.Count * 100, 2);
                    }
                    weekdayData.LateRates.Add(lateRate);
                    
                    // حساب معدل الغياب
                    decimal absenceRate = 0;
                    if (expectedAttendance > 0)
                    {
                        absenceRate = Math.Round(100 - attendanceRate, 2);
                    }
                    weekdayData.AbsenceRates.Add(absenceRate);
                }
                
                return weekdayData;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على بيانات مخطط الحضور حسب أيام الأسبوع");
                throw;
            }
        }
        
        /// <summary>
        /// الحصول على بيانات مؤشرات الأداء الرئيسية
        /// </summary>
        private static KPIDataDTO GetKPIData(UnitOfWork unitOfWork, DateTime fromDate, DateTime toDate)
        {
            try
            {
                // إنشاء كائن بيانات مؤشرات الأداء الرئيسية
                KPIDataDTO kpiData = new KPIDataDTO();
                
                // حساب عدد أيام العمل في الفترة
                int workingDays = GetWorkingDaysInRange(fromDate, toDate);
                
                // الحصول على عدد الموظفين النشطين
                int totalActiveEmployees = unitOfWork.EmployeeRepository.GetCount(e => e.IsActive);
                
                // إجمالي الحضور المتوقع
                decimal expectedAttendance = totalActiveEmployees * workingDays;
                
                // الحصول على جميع سجلات الحضور خلال الفترة
                var attendance = unitOfWork.AttendanceRepository.GetAttendanceByDateRange(fromDate, toDate);
                
                // معدل الالتزام بالدوام
                if (expectedAttendance > 0)
                {
                    kpiData.AttendanceComplianceRate = Math.Round(attendance.Count / expectedAttendance * 100, 2);
                }
                
                // القيمة المستهدفة لمعدل الالتزام بالدوام
                kpiData.AttendanceComplianceTarget = 95;
                
                // معدل التأخير
                TimeSpan lateThreshold = new TimeSpan(9, 0, 0); // 9:00 صباحاً
                int lateCount = attendance.Count(a => a.CheckInTime != null && a.CheckInTime.Value.TimeOfDay > lateThreshold);
                
                if (attendance.Count > 0)
                {
                    kpiData.LateRate = Math.Round((decimal)lateCount / attendance.Count * 100, 2);
                }
                
                // القيمة المستهدفة لمعدل التأخير
                kpiData.LateRateTarget = 5;
                
                // معدل دوران الموظفين
                DateTime yearStart = new DateTime(fromDate.Year, 1, 1);
                int terminatedEmployees = unitOfWork.EmployeeRepository.GetCount(e => e.TerminationDate >= yearStart && e.TerminationDate <= toDate);
                
                int totalEmployeesStartOfYear = unitOfWork.EmployeeRepository.GetCount(e => e.HireDate < yearStart);
                int totalEmployeesEndOfPeriod = unitOfWork.EmployeeRepository.GetCount(e => e.IsActive || (e.TerminationDate != null && e.TerminationDate <= toDate));
                
                decimal avgEmployees = (totalEmployeesStartOfYear + totalEmployeesEndOfPeriod) / 2m;
                
                if (avgEmployees > 0)
                {
                    kpiData.EmployeeTurnoverRate = Math.Round(terminatedEmployees / avgEmployees * 100, 2);
                }
                
                // القيمة المستهدفة لمعدل دوران الموظفين
                kpiData.EmployeeTurnoverTarget = 10;
                
                // معدل استغلال الإجازات
                var annualLeavesThisYear = unitOfWork.LeaveRepository.GetAllLeavesByType(
                    new DateTime(DateTime.Now.Year, 1, 1), 
                    new DateTime(DateTime.Now.Year, 12, 31), 
                    "سنوية");
                    
                int totalAnnualLeavesDays = annualLeavesThisYear.Sum(l => (l.EndDate - l.StartDate).Days + 1);
                
                int totalAnnualLeavesAllowance = unitOfWork.EmployeeRepository.GetTotalAnnualLeavesEntitlement();
                
                if (totalAnnualLeavesAllowance > 0)
                {
                    kpiData.LeaveUtilizationRate = Math.Round((decimal)totalAnnualLeavesDays / totalAnnualLeavesAllowance * 100, 2);
                }
                
                // القيمة المستهدفة لمعدل استغلال الإجازات
                kpiData.LeaveUtilizationTarget = 100;
                
                // نسبة تكلفة الرواتب إلى التكلفة الإجمالية
                var payrolls = unitOfWork.PayrollRepository.GetPayrollsByDateRange(fromDate, toDate);
                decimal totalSalaries = payrolls.Sum(p => p.NetSalary);
                
                decimal totalCosts = unitOfWork.FinanceRepository.GetTotalCosts(fromDate, toDate);
                
                if (totalCosts > 0)
                {
                    kpiData.SalaryToTotalCostRatio = Math.Round(totalSalaries / totalCosts * 100, 2);
                }
                
                // القيمة المستهدفة لنسبة تكلفة الرواتب
                kpiData.SalaryToTotalCostTarget = 40;
                
                return kpiData;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على بيانات مؤشرات الأداء الرئيسية");
                throw;
            }
        }
        
        /// <summary>
        /// الحصول على عدد أيام العمل في شهر معين
        /// </summary>
        private static int GetWorkingDaysInMonth(DateTime date)
        {
            // عدد أيام الشهر
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            
            int workingDays = 0;
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(date.Year, date.Month, day);
                
                // التحقق مما إذا كان يوم عمل (ليس الجمعة أو السبت)
                if (currentDate.DayOfWeek != DayOfWeek.Friday && currentDate.DayOfWeek != DayOfWeek.Saturday)
                {
                    workingDays++;
                }
            }
            
            return workingDays;
        }
        
        /// <summary>
        /// الحصول على عدد أيام العمل في فترة زمنية
        /// </summary>
        private static int GetWorkingDaysInRange(DateTime fromDate, DateTime toDate)
        {
            int workingDays = 0;
            
            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                // التحقق مما إذا كان يوم عمل (ليس الجمعة أو السبت)
                if (date.DayOfWeek != DayOfWeek.Friday && date.DayOfWeek != DayOfWeek.Saturday)
                {
                    workingDays++;
                }
            }
            
            return workingDays;
        }
        
        /// <summary>
        /// الحصول على لون للقسم
        /// </summary>
        private static string GetDepartmentColor(string departmentName)
        {
            // تعيين ألوان ثابتة للأقسام المختلفة
            Dictionary<string, string> colors = new Dictionary<string, string>
            {
                { "الإدارة العليا", "#4285F4" },        // أزرق Google
                { "الموارد البشرية", "#EA4335" },       // أحمر Google
                { "المالية", "#34A853" },               // أخضر Google
                { "تقنية المعلومات", "#FBBC05" },       // أصفر Google
                { "المبيعات", "#FF6D01" },              // برتقالي
                { "التسويق", "#46BDC6" },               // فيروزي
                { "خدمة العملاء", "#9C27B0" },          // بنفسجي
                { "البحث والتطوير", "#795548" }         // بني
            };
            
            if (colors.ContainsKey(departmentName))
            {
                return colors[departmentName];
            }
            
            // لون افتراضي للأقسام غير المعروفة
            return "#607D8B"; // رمادي مزرق
        }
    }
}