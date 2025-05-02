using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج سجل البصمة الخام
    /// </summary>
    public class RawAttendanceLog
    {
        /// <summary>
        /// معرف السجل
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// معرف الجهاز
        /// </summary>
        public int DeviceID { get; set; }

        /// <summary>
        /// معرف المستخدم في جهاز البصمة
        /// </summary>
        public int BiometricUserID { get; set; }

        /// <summary>
        /// تاريخ ووقت السجل
        /// </summary>
        public DateTime LogDateTime { get; set; }

        /// <summary>
        /// نوع السجل (دخول/خروج)
        /// </summary>
        public int? LogType { get; set; }

        /// <summary>
        /// طريقة التحقق (بصمة، بطاقة، كلمة مرور)
        /// </summary>
        public int? VerifyMode { get; set; }

        /// <summary>
        /// رمز العمل (إن وجد)
        /// </summary>
        public int? WorkCode { get; set; }

        /// <summary>
        /// هل تمت معالجة السجل
        /// </summary>
        public bool IsProcessed { get; set; }

        /// <summary>
        /// هل تمت مطابقة السجل مع موظف
        /// </summary>
        public bool IsMatched { get; set; }

        /// <summary>
        /// معرف الموظف المطابق (إن وجد)
        /// </summary>
        public int? EmployeeID { get; set; }

        /// <summary>
        /// وقت المزامنة
        /// </summary>
        public DateTime SyncTime { get; set; }

        /// <summary>
        /// الموظف المرتبط
        /// </summary>
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// الجهاز المصدر
        /// </summary>
        public virtual BiometricDevice Device { get; set; }
    }
}