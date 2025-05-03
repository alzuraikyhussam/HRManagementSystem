using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR.Models
{
    /// <summary>
    /// نموذج بيانات فترة العمل
    /// </summary>
    public class WorkHoursModel
    {
        /// <summary>
        /// معرف فترة العمل
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// اسم فترة العمل
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// وصف فترة العمل
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// وقت البدء
        /// </summary>
        public TimeSpan StartTime { get; set; }
        
        /// <summary>
        /// وقت الانتهاء
        /// </summary>
        public TimeSpan EndTime { get; set; }
        
        /// <summary>
        /// دقائق السماح للتأخير
        /// </summary>
        public int FlexibleMinutes { get; set; }
        
        /// <summary>
        /// الحد الأدنى لاحتساب التأخير
        /// </summary>
        public int LateThresholdMinutes { get; set; }
        
        /// <summary>
        /// الحد الأدنى لاحتساب المغادرة المبكرة
        /// </summary>
        public int ShortDayThresholdMinutes { get; set; }
        
        /// <summary>
        /// الحد الأدنى لاحتساب العمل الإضافي
        /// </summary>
        public int OverTimeStartMinutes { get; set; }
        
        /// <summary>
        /// إجمالي ساعات فترة العمل
        /// </summary>
        public double TotalHours => (EndTime - StartTime).TotalHours;
    }
}