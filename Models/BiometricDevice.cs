using System;

namespace HR.Models
{
    /// <summary>
    /// كائن يمثل جهاز بصمة
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
        /// عنوان IP
        /// </summary>
        public string IPAddress { get; set; }
        
        /// <summary>
        /// المنفذ
        /// </summary>
        public int? Port { get; set; }
        
        /// <summary>
        /// مفتاح الاتصال
        /// </summary>
        public string CommunicationKey { get; set; }
        
        /// <summary>
        /// الوصف
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// موقع الجهاز
        /// </summary>
        public string Location { get; set; }
        
        /// <summary>
        /// حالة التفعيل
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// وقت آخر مزامنة
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
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// معرف المستخدم المنشئ
        /// </summary>
        public int? CreatedBy { get; set; }
    }
}