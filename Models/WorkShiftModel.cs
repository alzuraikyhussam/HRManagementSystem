using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR.Models
{
    /// <summary>
    /// نموذج بيانات المناوبة
    /// </summary>
    public class WorkShiftModel
    {
        /// <summary>
        /// معرف المناوبة
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// اسم المناوبة
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// وصف المناوبة
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// معرف فترة العمل
        /// </summary>
        public int WorkHoursID { get; set; }
        
        /// <summary>
        /// اسم فترة العمل
        /// </summary>
        public string WorkHoursName { get; set; }
        
        /// <summary>
        /// وقت بدء فترة العمل
        /// </summary>
        public TimeSpan? StartTime { get; set; }
        
        /// <summary>
        /// وقت انتهاء فترة العمل
        /// </summary>
        public TimeSpan? EndTime { get; set; }
        
        /// <summary>
        /// تفعيل يوم الأحد
        /// </summary>
        public bool SundayEnabled { get; set; }
        
        /// <summary>
        /// تفعيل يوم الإثنين
        /// </summary>
        public bool MondayEnabled { get; set; }
        
        /// <summary>
        /// تفعيل يوم الثلاثاء
        /// </summary>
        public bool TuesdayEnabled { get; set; }
        
        /// <summary>
        /// تفعيل يوم الأربعاء
        /// </summary>
        public bool WednesdayEnabled { get; set; }
        
        /// <summary>
        /// تفعيل يوم الخميس
        /// </summary>
        public bool ThursdayEnabled { get; set; }
        
        /// <summary>
        /// تفعيل يوم الجمعة
        /// </summary>
        public bool FridayEnabled { get; set; }
        
        /// <summary>
        /// تفعيل يوم السبت
        /// </summary>
        public bool SaturdayEnabled { get; set; }
        
        /// <summary>
        /// رمز اللون
        /// </summary>
        public string ColorCode { get; set; }
        
        /// <summary>
        /// الحالة النشطة
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// أيام الأسبوع كنص
        /// </summary>
        public string WeekDaysText
        {
            get
            {
                List<string> days = new List<string>();
                
                if (SundayEnabled) days.Add("الأحد");
                if (MondayEnabled) days.Add("الإثنين");
                if (TuesdayEnabled) days.Add("الثلاثاء");
                if (WednesdayEnabled) days.Add("الأربعاء");
                if (ThursdayEnabled) days.Add("الخميس");
                if (FridayEnabled) days.Add("الجمعة");
                if (SaturdayEnabled) days.Add("السبت");
                
                if (days.Count == 7)
                    return "كل الأيام";
                else if (days.Count == 0)
                    return "لا يوجد";
                
                return string.Join(", ", days);
            }
        }
        
        /// <summary>
        /// وقت المناوبة كنص
        /// </summary>
        public string ShiftTimeText
        {
            get
            {
                if (StartTime.HasValue && EndTime.HasValue)
                {
                    return $"{StartTime.Value.ToString(@"hh\:mm")} - {EndTime.Value.ToString(@"hh\:mm")}";
                }
                
                return "غير محدد";
            }
        }
    }
}