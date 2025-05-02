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
                    
                    // الحصول على الإجازات الحالية
                    dashboardData.CurrentLeaves = GetCurrentLeaves(unitOfWork);
                    
                    // الحصول على بيانات مخطط الحضور
                    dashboardData.AttendanceChartData = GetAttendanceChartData(unitOfWork, fromDate, toDate);
                    
                    // الحصول على بيانات مخطط الإجازات
                    dashboardData.LeavesChartData = GetLeavesChartData(unitOfWork, fromDate, toDate);
                    
                    // الحصول على الإشعارات
                    dashboardData.Notifications = GetNotifications(unitOfWork);
                    
                    // الحصول على الأحداث القادمة
                    dashboardData.UpcomingEvents = GetUpcomingEvents(unitOfWork);
                    
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
    }
}