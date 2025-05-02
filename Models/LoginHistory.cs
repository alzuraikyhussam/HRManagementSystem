using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج سجل الدخول للنظام
    /// </summary>
    public class LoginHistory
    {
        /// <summary>
        /// معرف السجل
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// معرف المستخدم
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// المستخدم المرتبط
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// تاريخ ووقت تسجيل الدخول
        /// </summary>
        public DateTime LoginDate { get; set; }

        /// <summary>
        /// عنوان IP
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// نوع الجهاز
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// اسم المتصفح
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// نظام التشغيل
        /// </summary>
        public string OperatingSystem { get; set; }

        /// <summary>
        /// حالة تسجيل الدخول (نجاح/فشل)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// معلومات الخطأ في حالة الفشل
        /// </summary>
        public string ErrorInfo { get; set; }

        /// <summary>
        /// تاريخ تسجيل الخروج
        /// </summary>
        public DateTime? LogoutDate { get; set; }

        /// <summary>
        /// مدة الجلسة بالثواني
        /// </summary>
        public int? SessionDuration { get; set; }
    }
}