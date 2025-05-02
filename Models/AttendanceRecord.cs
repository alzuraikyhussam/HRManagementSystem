using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج سجل الحضور والانصراف - يتوافق مع جدول AttendanceRecords في قاعدة البيانات
    /// </summary>
    public class AttendanceRecord
    {
        /// <summary>
        /// معرف سجل الحضور
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// الموظف المرتبط
        /// </summary>
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// تاريخ الحضور
        /// </summary>
        public DateTime AttendanceDate { get; set; }

        /// <summary>
        /// وقت الحضور
        /// </summary>
        public DateTime? TimeIn { get; set; }

        /// <summary>
        /// وقت الانصراف
        /// </summary>
        public DateTime? TimeOut { get; set; }

        /// <summary>
        /// معرف فترة العمل
        /// </summary>
        public int? WorkHoursID { get; set; }
        
        /// <summary>
        /// فترة العمل المرتبطة
        /// </summary>
        public virtual WorkHours WorkHoursShift { get; set; }

        /// <summary>
        /// حالة الحضور (حاضر، غائب، متأخر، مغادرة مبكرة، إجازة)
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// هل الإدخال يدوي
        /// </summary>
        public bool IsManualEntry { get; set; }

        /// <summary>
        /// عدد دقائق التأخير
        /// </summary>
        public int? LateMinutes { get; set; }

        /// <summary>
        /// عدد دقائق الخروج المبكر
        /// </summary>
        public int? EarlyDepartureMinutes { get; set; }

        /// <summary>
        /// عدد دقائق العمل الإضافي
        /// </summary>
        public int? OvertimeMinutes { get; set; }

        /// <summary>
        /// إجمالي دقائق العمل
        /// </summary>
        public int? WorkedMinutes { get; set; }

        /// <summary>
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }

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
    }
}