using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج جهاز البصمة
    /// </summary>
    public class BiometricDevice
    {
        /// <summary>
        /// معرف الجهاز
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// اسم الجهاز
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// موديل الجهاز
        /// </summary>
        public string DeviceModel { get; set; }

        /// <summary>
        /// الرقم التسلسلي
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// عنوان IP للجهاز
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// منفذ الاتصال
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// مفتاح الاتصال (للأجهزة المؤمنة)
        /// </summary>
        public string CommunicationKey { get; set; }

        /// <summary>
        /// وصف الجهاز
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// موقع الجهاز
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// حالة الجهاز (نشط/غير نشط)
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// آخر مزامنة للجهاز
        /// </summary>
        public DateTime? LastSyncTime { get; set; }

        /// <summary>
        /// حالة آخر مزامنة
        /// </summary>
        public string LastSyncStatus { get; set; }

        /// <summary>
        /// أخطاء آخر مزامنة
        /// </summary>
        public string LastSyncErrors { get; set; }

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