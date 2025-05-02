using System;
using System.Collections.Generic;

namespace HR.Models
{
    /// <summary>
    /// نموذج فترات العمل
    /// </summary>
    public class WorkHours
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
        /// وقت بداية العمل
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// وقت نهاية العمل
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// مدة السماح (بالدقائق)
        /// </summary>
        public int? FlexibleMinutes { get; set; }

        /// <summary>
        /// الحد الأدنى لاحتساب التأخير (بالدقائق)
        /// </summary>
        public int? LateThresholdMinutes { get; set; }

        /// <summary>
        /// الحد الأدنى لاحتساب الخروج المبكر (بالدقائق)
        /// </summary>
        public int? ShortDayThresholdMinutes { get; set; }

        /// <summary>
        /// الحد الأدنى لاحتساب العمل الإضافي (بالدقائق)
        /// </summary>
        public int? OverTimeStartMinutes { get; set; }

        /// <summary>
        /// إجمالي ساعات العمل (محسوبة)
        /// </summary>
        public decimal? TotalHours { get; set; }

        /// <summary>
        /// تاريخ الإنشاء
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// منشئ السجل
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// تاريخ التعديل
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// معدل السجل
        /// </summary>
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// مناوبات العمل المرتبطة بهذه الفترة
        /// </summary>
        public virtual ICollection<WorkShift> WorkShifts { get; set; }
    }
}