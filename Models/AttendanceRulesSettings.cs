using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR.Core
{
    /// <summary>
    /// كائن إعدادات قواعد الحضور والانصراف
    /// </summary>
    public class AttendanceRulesSettings
    {
        #region قواعد الغياب
        
        /// <summary>
        /// عدد أيام الخصم عن اليوم الغياب
        /// </summary>
        public decimal AbsentDaysDeduction { get; set; }
        
        /// <summary>
        /// الحد الأقصى لأيام الغياب المسموح بها في الشهر
        /// </summary>
        public int MaxAllowedAbsentDays { get; set; }
        
        #endregion
        
        #region قواعد التأخير
        
        /// <summary>
        /// تفعيل خصم التأخير
        /// </summary>
        public bool LateArrivalPenaltyEnabled { get; set; }
        
        /// <summary>
        /// دقائق السماح للتأخير
        /// </summary>
        public int LateArrivalGraceMinutes { get; set; }
        
        /// <summary>
        /// مقدار الخصم لكل دقيقة تأخير
        /// </summary>
        public decimal LateArrivalPenaltyPerMinute { get; set; }
        
        /// <summary>
        /// الحد الأقصى للخصم اليومي بسبب التأخير
        /// </summary>
        public decimal LateArrivalMaxPenaltyPerDay { get; set; }
        
        #endregion
        
        #region قواعد المغادرة المبكرة
        
        /// <summary>
        /// تفعيل خصم المغادرة المبكرة
        /// </summary>
        public bool EarlyDeparturePenaltyEnabled { get; set; }
        
        /// <summary>
        /// دقائق السماح للمغادرة المبكرة
        /// </summary>
        public int EarlyDepartureGraceMinutes { get; set; }
        
        /// <summary>
        /// مقدار الخصم لكل دقيقة مغادرة مبكرة
        /// </summary>
        public decimal EarlyDeparturePenaltyPerMinute { get; set; }
        
        /// <summary>
        /// الحد الأقصى للخصم اليومي بسبب المغادرة المبكرة
        /// </summary>
        public decimal EarlyDepartureMaxPenaltyPerDay { get; set; }
        
        #endregion
        
        #region إعدادات احتساب العمل الإضافي
        
        /// <summary>
        /// تفعيل احتساب العمل الإضافي
        /// </summary>
        public bool OvertimeEnabled { get; set; }
        
        /// <summary>
        /// بدء احتساب العمل الإضافي بعد عدد دقائق
        /// </summary>
        public int OvertimeStartAfterMinutes { get; set; }
        
        /// <summary>
        /// مضاعف العمل الإضافي
        /// </summary>
        public decimal OvertimeMultiplier { get; set; }
        
        /// <summary>
        /// مضاعف العمل الإضافي في نهاية الأسبوع
        /// </summary>
        public decimal WeekendOvertimeMultiplier { get; set; }
        
        /// <summary>
        /// مضاعف العمل الإضافي في العطلات
        /// </summary>
        public decimal HolidayOvertimeMultiplier { get; set; }
        
        #endregion
        
        #region إعدادات التصاريح
        
        /// <summary>
        /// الحد الأقصى للتصاريح في الشهر
        /// </summary>
        public int MaxPermissionsPerMonth { get; set; }
        
        /// <summary>
        /// الحد الأقصى لدقائق التصريح في اليوم
        /// </summary>
        public int MaxPermissionMinutesPerDay { get; set; }
        
        #endregion
        
        /// <summary>
        /// الإعدادات الافتراضية
        /// </summary>
        public AttendanceRulesSettings()
        {
            // قواعد الغياب
            AbsentDaysDeduction = 1.0m;
            MaxAllowedAbsentDays = 5;
            
            // قواعد التأخير
            LateArrivalPenaltyEnabled = true;
            LateArrivalGraceMinutes = 15;
            LateArrivalPenaltyPerMinute = 0.5m;
            LateArrivalMaxPenaltyPerDay = 0.25m;
            
            // قواعد المغادرة المبكرة
            EarlyDeparturePenaltyEnabled = true;
            EarlyDepartureGraceMinutes = 15;
            EarlyDeparturePenaltyPerMinute = 0.5m;
            EarlyDepartureMaxPenaltyPerDay = 0.25m;
            
            // إعدادات احتساب العمل الإضافي
            OvertimeEnabled = true;
            OvertimeStartAfterMinutes = 15;
            OvertimeMultiplier = 1.25m;
            WeekendOvertimeMultiplier = 1.5m;
            HolidayOvertimeMultiplier = 2.0m;
            
            // إعدادات التصاريح
            MaxPermissionsPerMonth = 2;
            MaxPermissionMinutesPerDay = 120;
        }
    }
}