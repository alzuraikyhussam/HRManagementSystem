using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج سجل البصمة الخام - يتوافق مع جدول RawAttendanceLogs في قاعدة البيانات
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
        public int BiometricDeviceID { get; set; }

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
        public string PunchType { get; set; }

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
        /// تاريخ الإنشاء
        /// </summary>
        public DateTime? CreatedAt { get; set; }
        
        /// <summary>
        /// منشئ السجل
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// الموظف المرتبط
        /// </summary>
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// الجهاز المصدر
        /// </summary>
        public virtual BiometricDevice BiometricDevice { get; set; }
    }
}