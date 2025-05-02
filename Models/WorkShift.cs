using System;
using System.Collections.Generic;

namespace HR.Models
{
    /// <summary>
    /// نموذج ورديات العمل - يتوافق مع جدول WorkShifts في قاعدة البيانات
    /// </summary>
    public class WorkShift
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
        public int? WorkHoursID { get; set; }

        /// <summary>
        /// فترة العمل
        /// </summary>
        public virtual WorkHours WorkHours { get; set; }

        /// <summary>
        /// تفعيل دوام يوم الأحد
        /// </summary>
        public bool SundayEnabled { get; set; }

        /// <summary>
        /// تفعيل دوام يوم الاثنين
        /// </summary>
        public bool MondayEnabled { get; set; }

        /// <summary>
        /// تفعيل دوام يوم الثلاثاء
        /// </summary>
        public bool TuesdayEnabled { get; set; }

        /// <summary>
        /// تفعيل دوام يوم الأربعاء
        /// </summary>
        public bool WednesdayEnabled { get; set; }

        /// <summary>
        /// تفعيل دوام يوم الخميس
        /// </summary>
        public bool ThursdayEnabled { get; set; }

        /// <summary>
        /// تفعيل دوام يوم الجمعة
        /// </summary>
        public bool FridayEnabled { get; set; }

        /// <summary>
        /// تفعيل دوام يوم السبت
        /// </summary>
        public bool SaturdayEnabled { get; set; }

        /// <summary>
        /// رمز اللون
        /// </summary>
        public string ColorCode { get; set; }

        /// <summary>
        /// حالة المناوبة (نشطة/غير نشطة)
        /// </summary>
        public bool IsActive { get; set; }

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
        /// قائمة الموظفين الذين يعملون وفق هذه المناوبة
        /// </summary>
        public virtual ICollection<Employee> Employees { get; set; }
    }
}