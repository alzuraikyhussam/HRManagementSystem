using System;
using System.Collections.Generic;

namespace HR.Models.DTOs
{
    /// <summary>
    /// نموذج بيانات لوحة المعلومات
    /// </summary>
    public class DashboardDataDTO
    {
        /// <summary>
        /// إحصائيات الموظفين
        /// </summary>
        public EmployeeStatisticsDTO EmployeeStatistics { get; set; }
        
        /// <summary>
        /// إحصائيات الحضور
        /// </summary>
        public AttendanceStatisticsDTO AttendanceStatistics { get; set; }
        
        /// <summary>
        /// إحصائيات الإجازات
        /// </summary>
        public LeaveStatisticsDTO LeaveStatistics { get; set; }
        
        /// <summary>
        /// إحصائيات الرواتب
        /// </summary>
        public PayrollStatisticsDTO PayrollStatistics { get; set; }
        
        /// <summary>
        /// الإجازات الحالية
        /// </summary>
        public List<LeaveDTO> CurrentLeaves { get; set; }
        
        /// <summary>
        /// بيانات مخطط الحضور
        /// </summary>
        public List<ChartDataDTO> AttendanceChartData { get; set; }
        
        /// <summary>
        /// بيانات مخطط اتجاهات الحضور
        /// </summary>
        public AttendanceTrendChartDataDTO AttendanceTrendData { get; set; }
        
        /// <summary>
        /// بيانات مخطط الإجازات
        /// </summary>
        public List<ChartDataDTO> LeavesChartData { get; set; }
        
        /// <summary>
        /// بيانات مخطط الحضور حسب القسم
        /// </summary>
        public DepartmentAttendanceDataDTO DepartmentAttendanceData { get; set; }
        
        /// <summary>
        /// بيانات مخطط الحضور حسب أيام الأسبوع
        /// </summary>
        public WeekdayAttendanceDataDTO WeekdayAttendanceData { get; set; }
        
        /// <summary>
        /// الإشعارات
        /// </summary>
        public List<NotificationDTO> Notifications { get; set; }
        
        /// <summary>
        /// الأحداث القادمة
        /// </summary>
        public List<EventDTO> UpcomingEvents { get; set; }
        
        /// <summary>
        /// بيانات مؤشرات الأداء الرئيسية
        /// </summary>
        public KPIDataDTO KPIData { get; set; }
    }
    
    /// <summary>
    /// نموذج إحصائيات الموظفين
    /// </summary>
    public class EmployeeStatisticsDTO
    {
        /// <summary>
        /// إجمالي عدد الموظفين
        /// </summary>
        public int TotalEmployees { get; set; }
        
        /// <summary>
        /// عدد الموظفين النشطين
        /// </summary>
        public int ActiveEmployees { get; set; }
        
        /// <summary>
        /// عدد الموظفين الجدد
        /// </summary>
        public int NewEmployees { get; set; }
        
        /// <summary>
        /// عدد الموظفين المغادرين
        /// </summary>
        public int TerminatedEmployees { get; set; }
        
        /// <summary>
        /// معدل دوران الموظفين (نسبة مئوية)
        /// </summary>
        public decimal TurnoverRate { get; set; }
        
        /// <summary>
        /// توزيع الموظفين حسب الإدارات
        /// </summary>
        public List<ChartDataDTO> DepartmentDistribution { get; set; }
        
        /// <summary>
        /// توزيع الموظفين حسب سنوات الخبرة
        /// </summary>
        public List<ChartDataDTO> ExperienceDistribution { get; set; }
    }
    
    /// <summary>
    /// نموذج إحصائيات الحضور
    /// </summary>
    public class AttendanceStatisticsDTO
    {
        /// <summary>
        /// إجمالي عدد الموظفين
        /// </summary>
        public int TotalEmployees { get; set; }
        
        /// <summary>
        /// عدد الموظفين الحاضرين اليوم
        /// </summary>
        public int PresentToday { get; set; }
        
        /// <summary>
        /// عدد الموظفين الغائبين اليوم
        /// </summary>
        public int AbsentToday { get; set; }
        
        /// <summary>
        /// عدد الموظفين المتأخرين اليوم
        /// </summary>
        public int LateToday { get; set; }
        
        /// <summary>
        /// عدد الموظفين في إجازة اليوم
        /// </summary>
        public int OnLeaveToday { get; set; }
        
        /// <summary>
        /// معدل الالتزام بالدوام (نسبة مئوية)
        /// </summary>
        public decimal AttendanceComplianceRate { get; set; }
        
        /// <summary>
        /// متوسط التأخير اليومي (بالدقائق)
        /// </summary>
        public decimal AverageDailyLateMinutes { get; set; }
        
        /// <summary>
        /// متوسط ساعات العمل اليومية
        /// </summary>
        public decimal AverageDailyWorkHours { get; set; }
        
        /// <summary>
        /// متوسط ساعات العمل المستهدفة
        /// </summary>
        public decimal TargetDailyWorkHours { get; set; }
        
        /// <summary>
        /// عدد المغادرات المبكرة اليوم
        /// </summary>
        public int EarlyDepartureToday { get; set; }
        
        /// <summary>
        /// معدل الغياب بدون إذن (نسبة مئوية)
        /// </summary>
        public decimal UnauthorizedAbsenceRate { get; set; }
    }
    
    /// <summary>
    /// نموذج إحصائيات الإجازات
    /// </summary>
    public class LeaveStatisticsDTO
    {
        /// <summary>
        /// إجمالي عدد الإجازات في الفترة المحددة
        /// </summary>
        public int TotalLeaves { get; set; }
        
        /// <summary>
        /// عدد الإجازات المرضية
        /// </summary>
        public int SickLeaves { get; set; }
        
        /// <summary>
        /// عدد الإجازات السنوية
        /// </summary>
        public int AnnualLeaves { get; set; }
        
        /// <summary>
        /// عدد الإجازات الاضطرارية
        /// </summary>
        public int EmergencyLeaves { get; set; }
        
        /// <summary>
        /// عدد إجازات الحج/العمرة
        /// </summary>
        public int ReligiousLeaves { get; set; }
        
        /// <summary>
        /// عدد الإجازات غير مدفوعة الأجر
        /// </summary>
        public int UnpaidLeaves { get; set; }
        
        /// <summary>
        /// متوسط أيام الإجازة لكل موظف
        /// </summary>
        public decimal AverageLeaveDaysPerEmployee { get; set; }
        
        /// <summary>
        /// مقارنة الإجازات المرضية بالشهر السابق (نسبة التغيير)
        /// </summary>
        public decimal SickLeaveChangePercent { get; set; }
        
        /// <summary>
        /// معدل الإجازات المقدمة مقابل الخطة السنوية (نسبة مئوية)
        /// </summary>
        public decimal LeaveUtilizationRate { get; set; }
    }
    
    /// <summary>
    /// نموذج إحصائيات الرواتب
    /// </summary>
    public class PayrollStatisticsDTO
    {
        /// <summary>
        /// إجمالي الرواتب للفترة الحالية
        /// </summary>
        public decimal TotalSalariesCurrentPeriod { get; set; }
        
        /// <summary>
        /// إجمالي الرواتب للفترة السابقة
        /// </summary>
        public decimal TotalSalariesPreviousPeriod { get; set; }
        
        /// <summary>
        /// نسبة التغيير في إجمالي الرواتب
        /// </summary>
        public decimal SalaryChangePercent { get; set; }
        
        /// <summary>
        /// إجمالي البدلات
        /// </summary>
        public decimal TotalAllowances { get; set; }
        
        /// <summary>
        /// إجمالي الخصومات
        /// </summary>
        public decimal TotalDeductions { get; set; }
        
        /// <summary>
        /// إجمالي الخصومات المتعلقة بالحضور
        /// </summary>
        public decimal AttendanceRelatedDeductions { get; set; }
        
        /// <summary>
        /// توزيع الرواتب حسب الإدارات
        /// </summary>
        public List<ChartDataDTO> SalaryByDepartment { get; set; }
        
        /// <summary>
        /// متوسط الراتب حسب المستوى الوظيفي
        /// </summary>
        public List<ChartDataDTO> AverageSalaryByLevel { get; set; }
    }

    /// <summary>
    /// نموذج بيانات المخطط
    /// </summary>
    public class ChartDataDTO
    {
        /// <summary>
        /// اسم الفئة
        /// </summary>
        public string CategoryName { get; set; }
        
        /// <summary>
        /// القيمة
        /// </summary>
        public int Value { get; set; }
        
        /// <summary>
        /// التغيير عن الفترة السابقة (نسبة مئوية)
        /// </summary>
        public decimal ChangePercent { get; set; }
        
        /// <summary>
        /// اللون المقترح للعرض
        /// </summary>
        public string ColorCode { get; set; }
    }
    
    /// <summary>
    /// نموذج بيانات اتجاهات الحضور
    /// </summary>
    public class AttendanceTrendChartDataDTO
    {
        /// <summary>
        /// فترات الزمن (الأشهر أو الأسابيع)
        /// </summary>
        public List<string> TimeCategories { get; set; }
        
        /// <summary>
        /// نسب الحضور
        /// </summary>
        public List<decimal> AttendanceRates { get; set; }
        
        /// <summary>
        /// نسب التأخير
        /// </summary>
        public List<decimal> LateRates { get; set; }
        
        /// <summary>
        /// نسب الغياب
        /// </summary>
        public List<decimal> AbsenceRates { get; set; }
    }
    
    /// <summary>
    /// نموذج بيانات الحضور حسب القسم
    /// </summary>
    public class DepartmentAttendanceDataDTO
    {
        /// <summary>
        /// أسماء الأقسام
        /// </summary>
        public List<string> DepartmentNames { get; set; }
        
        /// <summary>
        /// نسب الحضور
        /// </summary>
        public List<decimal> AttendanceRates { get; set; }
        
        /// <summary>
        /// نسب التأخير
        /// </summary>
        public List<decimal> LateRates { get; set; }
        
        /// <summary>
        /// نسب الإجازات
        /// </summary>
        public List<decimal> LeaveRates { get; set; }
    }
    
    /// <summary>
    /// نموذج بيانات الحضور حسب أيام الأسبوع
    /// </summary>
    public class WeekdayAttendanceDataDTO
    {
        /// <summary>
        /// أيام الأسبوع
        /// </summary>
        public List<string> Weekdays { get; set; }
        
        /// <summary>
        /// معدلات الحضور
        /// </summary>
        public List<decimal> AttendanceRates { get; set; }
        
        /// <summary>
        /// معدلات التأخير
        /// </summary>
        public List<decimal> LateRates { get; set; }
        
        /// <summary>
        /// معدلات الغياب
        /// </summary>
        public List<decimal> AbsenceRates { get; set; }
    }
    
    /// <summary>
    /// نموذج بيانات مؤشرات الأداء الرئيسية
    /// </summary>
    public class KPIDataDTO
    {
        /// <summary>
        /// معدل الالتزام بالدوام (نسبة مئوية)
        /// </summary>
        public decimal AttendanceComplianceRate { get; set; }
        
        /// <summary>
        /// القيمة المستهدفة لمعدل الالتزام بالدوام
        /// </summary>
        public decimal AttendanceComplianceTarget { get; set; }
        
        /// <summary>
        /// معدل التأخير (نسبة مئوية)
        /// </summary>
        public decimal LateRate { get; set; }
        
        /// <summary>
        /// القيمة المستهدفة لمعدل التأخير
        /// </summary>
        public decimal LateRateTarget { get; set; }
        
        /// <summary>
        /// معدل دوران الموظفين (نسبة مئوية)
        /// </summary>
        public decimal EmployeeTurnoverRate { get; set; }
        
        /// <summary>
        /// القيمة المستهدفة لمعدل دوران الموظفين
        /// </summary>
        public decimal EmployeeTurnoverTarget { get; set; }
        
        /// <summary>
        /// معدل استغلال الإجازات (نسبة مئوية)
        /// </summary>
        public decimal LeaveUtilizationRate { get; set; }
        
        /// <summary>
        /// القيمة المستهدفة لمعدل استغلال الإجازات
        /// </summary>
        public decimal LeaveUtilizationTarget { get; set; }
        
        /// <summary>
        /// نسبة تكلفة الرواتب إلى التكلفة الإجمالية
        /// </summary>
        public decimal SalaryToTotalCostRatio { get; set; }
        
        /// <summary>
        /// القيمة المستهدفة لنسبة تكلفة الرواتب
        /// </summary>
        public decimal SalaryToTotalCostTarget { get; set; }
    }

    /// <summary>
    /// نموذج الإشعار
    /// </summary>
    public class NotificationDTO
    {
        /// <summary>
        /// المعرف
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// نوع الإشعار (Info, Warning, Error)
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// عنوان الإشعار
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// نص الإشعار
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// تاريخ الإشعار
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// هل تم قراءة الإشعار
        /// </summary>
        public bool IsRead { get; set; }
        
        /// <summary>
        /// الإجراء المتعلق بالإشعار (إن وجد)
        /// </summary>
        public string ActionName { get; set; }
        
        /// <summary>
        /// رابط الإجراء
        /// </summary>
        public string ActionLink { get; set; }
    }
    
    /// <summary>
    /// نموذج الحدث
    /// </summary>
    public class EventDTO
    {
        /// <summary>
        /// المعرف
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// نوع الحدث (Birthday, Meeting, Holiday, Anniversary)
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// عنوان الحدث
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// وصف الحدث
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// تاريخ بداية الحدث
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// تاريخ نهاية الحدث
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// الأشخاص المرتبطين بالحدث
        /// </summary>
        public List<string> RelatedPeople { get; set; }
        
        /// <summary>
        /// مستوى أهمية الحدث
        /// </summary>
        public string Priority { get; set; }
        
        /// <summary>
        /// حالة الحدث
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// الموقع أو المكان
        /// </summary>
        public string Location { get; set; }
    }
    
    /// <summary>
    /// نموذج الإجازة
    /// </summary>
    public class LeaveDTO
    {
        /// <summary>
        /// المعرف
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// اسم الموظف
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// نوع الإجازة
        /// </summary>
        public string LeaveType { get; set; }
        
        /// <summary>
        /// تاريخ البداية
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// تاريخ النهاية
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// المدة بالأيام
        /// </summary>
        public int Duration { get; set; }
        
        /// <summary>
        /// حالة الإجازة (Pending, Approved, Rejected)
        /// </summary>
        public string Status { get; set; }
    }
}