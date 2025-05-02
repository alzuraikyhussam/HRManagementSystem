using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج سجل الحضور والانصراف
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
        /// تاريخ التسجيل
        /// </summary>
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// وقت الحضور
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// وقت الانصراف
        /// </summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// نوع سجل الحضور
        /// </summary>
        public string RecordType { get; set; }

        /// <summary>
        /// بصمة الحضور
        /// </summary>
        public int? CheckInDeviceID { get; set; }

        /// <summary>
        /// بصمة الانصراف
        /// </summary>
        public int? CheckOutDeviceID { get; set; }

        /// <summary>
        /// حالة الحضور (طبيعي، متأخر، إلخ)
        /// </summary>
        public string AttendanceStatus { get; set; }

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
        /// إجمالي ساعات العمل (محسوبة)
        /// </summary>
        public decimal? WorkHours { get; set; }

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