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
        /// الإجازات الحالية
        /// </summary>
        public List<LeaveDTO> CurrentLeaves { get; set; }
        
        /// <summary>
        /// بيانات مخطط الحضور
        /// </summary>
        public List<ChartDataDTO> AttendanceChartData { get; set; }
        
        /// <summary>
        /// بيانات مخطط الإجازات
        /// </summary>
        public List<ChartDataDTO> LeavesChartData { get; set; }
        
        /// <summary>
        /// الإشعارات
        /// </summary>
        public List<NotificationDTO> Notifications { get; set; }
        
        /// <summary>
        /// الأحداث القادمة
        /// </summary>
        public List<EventDTO> UpcomingEvents { get; set; }
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
        /// تاريخ الحدث
        /// </summary>
        public DateTime Date { get; set; }
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