using System;
using System.Collections.Generic;

namespace HR.Models
{
    /// <summary>
    /// نموذج ملخص الحضور للموظف
    /// </summary>
    public class AttendanceSummary
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
        /// اسم القسم
        /// </summary>
        public string DepartmentName { get; set; }
        
        /// <summary>
        /// العام
        /// </summary>
        public int Year { get; set; }
        
        /// <summary>
        /// الشهر
        /// </summary>
        public int Month { get; set; }
        
        /// <summary>
        /// إجمالي عدد أيام التسجيل
        /// </summary>
        public int TotalDays { get; set; }
        
        /// <summary>
        /// عدد أيام الحضور
        /// </summary>
        public int PresentDays { get; set; }
        
        /// <summary>
        /// عدد أيام الغياب
        /// </summary>
        public int AbsentDays { get; set; }
        
        /// <summary>
        /// عدد أيام التأخير
        /// </summary>
        public int LateDays { get; set; }
        
        /// <summary>
        /// عدد أيام المغادرة المبكرة
        /// </summary>
        public int EarlyDepartureDays { get; set; }
        
        /// <summary>
        /// عدد أيام الإجازات
        /// </summary>
        public int LeaveDays { get; set; }
        
        /// <summary>
        /// عدد أيام التصاريح
        /// </summary>
        public int PermissionDays { get; set; }
        
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
        /// إجمالي دقائق العمل
        /// </summary>
        public int TotalWorkedMinutes { get; set; }
        
        // ميزات إضافية (تحسينات)
        
        /// <summary>
        /// متوسط ساعات العمل اليومية
        /// </summary>
        public decimal AverageDailyWorkHours { get; set; }
        
        /// <summary>
        /// معدل الالتزام بوقت الحضور (نسبة مئوية)
        /// </summary>
        public decimal PunctualityRate { get; set; }
        
        /// <summary>
        /// معدل الالتزام بوقت الانصراف (نسبة مئوية)
        /// </summary>
        public decimal DepartureComplianceRate { get; set; }
        
        /// <summary>
        /// معدل الالتزام العام (نسبة مئوية)
        /// </summary>
        public decimal OverallComplianceRate { get; set; }
        
        /// <summary>
        /// نسبة الساعات الإضافية (نسبة مئوية)
        /// </summary>
        public decimal OvertimeRate { get; set; }
        
        /// <summary>
        /// ساعات العمل الأسبوعية (رقم الأسبوع => ساعات العمل)
        /// </summary>
        public Dictionary<int, decimal> WeeklyHours { get; set; }
        
        /// <summary>
        /// أنماط الحضور حسب يوم الأسبوع
        /// </summary>
        public List<DayOfWeekPattern> DayOfWeekPatterns { get; set; }
        
        /// <summary>
        /// اليوم الأكثر تأخيراً في الأسبوع
        /// </summary>
        public int MostLateDayOfWeek { get; set; }
        
        /// <summary>
        /// معدل التأخير في اليوم الأكثر تأخيراً
        /// </summary>
        public decimal MostLateDayFrequency { get; set; }
        
        /// <summary>
        /// متوسط وقت الحضور اليومي
        /// </summary>
        public TimeSpan AverageTimeIn { get; set; }
        
        /// <summary>
        /// متوسط وقت الانصراف اليومي
        /// </summary>
        public TimeSpan AverageTimeOut { get; set; }
        
        /// <summary>
        /// عدد أيام الحضور المثالي (بدون تأخير أو مغادرة مبكرة)
        /// </summary>
        public int PerfectAttendanceDays { get; set; }
        
        /// <summary>
        /// تنبيهات الأنماط المكتشفة
        /// </summary>
        public List<AttendancePattern> DetectedPatterns { get; set; }
        
        /// <summary>
        /// بناء الكائن
        /// </summary>
        public AttendanceSummary()
        {
            WeeklyHours = new Dictionary<int, decimal>();
            DayOfWeekPatterns = new List<DayOfWeekPattern>();
            DetectedPatterns = new List<AttendancePattern>();
        }
        
        /// <summary>
        /// حساب عدد أيام العمل الحقيقية في الشهر
        /// </summary>
        public int GetActualWorkingDays(DateTime fromDate, DateTime toDate)
        {
            int workingDays = 0;
            
            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                // اعتبار جميع الأيام عدا الجمعة والسبت أيام عمل
                if (date.DayOfWeek != DayOfWeek.Friday && date.DayOfWeek != DayOfWeek.Saturday)
                {
                    workingDays++;
                }
            }
            
            return workingDays;
        }
        
        /// <summary>
        /// حساب نسبة الالتزام بالدوام
        /// </summary>
        public decimal CalculateAttendanceComplianceRate(int workingDays)
        {
            if (workingDays == 0) return 0;
            
            return Math.Round((decimal)PresentDays / workingDays * 100, 2);
        }
        
        /// <summary>
        /// اكتشاف الأنماط والتنبيهات
        /// </summary>
        public void DetectPatterns()
        {
            DetectedPatterns.Clear();
            
            // اكتشاف نمط الغياب المتكرر
            if (AbsentDays > 2)
            {
                DetectedPatterns.Add(new AttendancePattern
                {
                    Type = AttendancePatternType.FrequentAbsence,
                    Description = $"تكرر الغياب {AbsentDays} مرات هذا الشهر",
                    Severity = AttendancePatternSeverity.High
                });
            }
            
            // اكتشاف نمط التأخير المتكرر
            if (LateDays > 5)
            {
                DetectedPatterns.Add(new AttendancePattern
                {
                    Type = AttendancePatternType.FrequentLateness,
                    Description = $"تكرر التأخير {LateDays} مرات هذا الشهر",
                    Severity = AttendancePatternSeverity.Medium
                });
            }
            
            // اكتشاف نمط المغادرة المبكرة المتكررة
            if (EarlyDepartureDays > 5)
            {
                DetectedPatterns.Add(new AttendancePattern
                {
                    Type = AttendancePatternType.FrequentEarlyDeparture,
                    Description = $"تكررت المغادرة المبكرة {EarlyDepartureDays} مرات هذا الشهر",
                    Severity = AttendancePatternSeverity.Medium
                });
            }
            
            // اكتشاف نمط التأخير في يوم محدد من الأسبوع
            if (MostLateDayFrequency > 0.75m) // أكثر من 75% من المرات
            {
                string dayName = GetDayOfWeekName(MostLateDayOfWeek);
                DetectedPatterns.Add(new AttendancePattern
                {
                    Type = AttendancePatternType.WeekdayPattern,
                    Description = $"يتكرر التأخير يوم {dayName} بنسبة {MostLateDayFrequency * 100:0.0}%",
                    Severity = AttendancePatternSeverity.Low
                });
            }
            
            // اكتشاف نمط معدل ساعات العمل المنخفض
            if (AverageDailyWorkHours < 7.0m)
            {
                DetectedPatterns.Add(new AttendancePattern
                {
                    Type = AttendancePatternType.LowWorkHours,
                    Description = $"متوسط ساعات العمل اليومية {AverageDailyWorkHours:0.0} ساعات (أقل من المعدل المطلوب 8 ساعات)",
                    Severity = AttendancePatternSeverity.Medium
                });
            }
        }
        
        /// <summary>
        /// الحصول على اسم يوم الأسبوع باللغة العربية
        /// </summary>
        private string GetDayOfWeekName(int dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case 0: return "الأحد";
                case 1: return "الإثنين";
                case 2: return "الثلاثاء";
                case 3: return "الأربعاء";
                case 4: return "الخميس";
                case 5: return "الجمعة";
                case 6: return "السبت";
                default: return "";
            }
        }
    }
    
    /// <summary>
    /// نموذج نمط الحضور حسب يوم الأسبوع
    /// </summary>
    public class DayOfWeekPattern
    {
        /// <summary>
        /// يوم الأسبوع (0: الأحد، 1: الإثنين، إلخ)
        /// </summary>
        public int DayOfWeek { get; set; }
        
        /// <summary>
        /// عدد أيام التسجيل
        /// </summary>
        public int DayCount { get; set; }
        
        /// <summary>
        /// متوسط وقت الحضور
        /// </summary>
        public TimeSpan AverageTimeIn { get; set; }
        
        /// <summary>
        /// متوسط وقت الانصراف
        /// </summary>
        public TimeSpan AverageTimeOut { get; set; }
        
        /// <summary>
        /// معدل التأخير (نسبة مئوية)
        /// </summary>
        public decimal LateFrequency { get; set; }
    }
    
    /// <summary>
    /// نموذج نمط الحضور المكتشف
    /// </summary>
    public class AttendancePattern
    {
        /// <summary>
        /// نوع النمط
        /// </summary>
        public AttendancePatternType Type { get; set; }
        
        /// <summary>
        /// وصف النمط
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// مستوى الخطورة
        /// </summary>
        public AttendancePatternSeverity Severity { get; set; }
    }
    
    /// <summary>
    /// أنواع أنماط الحضور
    /// </summary>
    public enum AttendancePatternType
    {
        /// <summary>
        /// الغياب المتكرر
        /// </summary>
        FrequentAbsence,
        
        /// <summary>
        /// التأخير المتكرر
        /// </summary>
        FrequentLateness,
        
        /// <summary>
        /// المغادرة المبكرة المتكررة
        /// </summary>
        FrequentEarlyDeparture,
        
        /// <summary>
        /// نمط يوم محدد من الأسبوع
        /// </summary>
        WeekdayPattern,
        
        /// <summary>
        /// معدل ساعات عمل منخفض
        /// </summary>
        LowWorkHours
    }
    
    /// <summary>
    /// مستويات خطورة نمط الحضور
    /// </summary>
    public enum AttendancePatternSeverity
    {
        /// <summary>
        /// منخفض
        /// </summary>
        Low,
        
        /// <summary>
        /// متوسط
        /// </summary>
        Medium,
        
        /// <summary>
        /// مرتفع
        /// </summary>
        High
    }
}