using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج وثيقة موظف
    /// </summary>
    public class EmployeeDocument
    {
        /// <summary>
        /// المعرف
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// نوع الوثيقة
        /// </summary>
        public int DocumentTypeID { get; set; }
        
        /// <summary>
        /// عنوان الوثيقة
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// الوصف
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// مسار الملف
        /// </summary>
        public string FilePath { get; set; }
        
        /// <summary>
        /// حجم الملف (بالبايت)
        /// </summary>
        public long FileSize { get; set; }
        
        /// <summary>
        /// نوع الملف (الامتداد)
        /// </summary>
        public string FileType { get; set; }
        
        /// <summary>
        /// تاريخ الإصدار
        /// </summary>
        public DateTime IssueDate { get; set; }
        
        /// <summary>
        /// تاريخ انتهاء الصلاحية
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
        
        /// <summary>
        /// الملاحظات
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// التذكير (عدد الأيام قبل انتهاء الصلاحية)
        /// </summary>
        public int? ReminderDays { get; set; }
        
        /// <summary>
        /// تاريخ التحميل
        /// </summary>
        public DateTime UploadDate { get; set; }
        
        /// <summary>
        /// معرف المستخدم الذي قام بالتحميل
        /// </summary>
        public int UploadedByUserID { get; set; }
        
        /// <summary>
        /// علامة التحقق من الوثيقة
        /// </summary>
        public bool IsVerified { get; set; }
        
        /// <summary>
        /// معرف المستخدم الذي قام بالتحقق
        /// </summary>
        public int? VerifiedByUserID { get; set; }
        
        /// <summary>
        /// تاريخ التحقق
        /// </summary>
        public DateTime? VerificationDate { get; set; }
        
        // العلاقات
        
        /// <summary>
        /// الموظف المرتبط
        /// </summary>
        public virtual Employee Employee { get; set; }
        
        /// <summary>
        /// نوع الوثيقة المرتبط
        /// </summary>
        public virtual DocumentType DocumentType { get; set; }
        
        /// <summary>
        /// الحصول على حالة الوثيقة
        /// </summary>
        public string GetDocumentStatus()
        {
            // التحقق من انتهاء الصلاحية
            if (ExpiryDate.HasValue && ExpiryDate.Value < DateTime.Now)
            {
                return "منتهية الصلاحية";
            }
            
            // التحقق من اقتراب انتهاء الصلاحية
            if (ExpiryDate.HasValue && ReminderDays.HasValue)
            {
                double daysRemaining = (ExpiryDate.Value - DateTime.Now).TotalDays;
                if (daysRemaining <= ReminderDays.Value)
                {
                    return "قرب انتهاء الصلاحية";
                }
            }
            
            // التحقق من التحقق
            if (!IsVerified)
            {
                return "غير متحقق منها";
            }
            
            return "سارية";
        }
        
        /// <summary>
        /// الحصول على الأيام المتبقية حتى انتهاء الصلاحية
        /// </summary>
        public int GetRemainingDays()
        {
            if (ExpiryDate.HasValue)
            {
                var remainingDays = (int)(ExpiryDate.Value - DateTime.Now).TotalDays;
                return remainingDays > 0 ? remainingDays : 0;
            }
            
            return -1; // غير محدد
        }
    }
    
    /// <summary>
    /// نموذج نوع الوثيقة
    /// </summary>
    public class DocumentType
    {
        /// <summary>
        /// المعرف
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// اسم نوع الوثيقة
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// الوصف
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// هل الوثيقة إلزامية
        /// </summary>
        public bool IsRequired { get; set; }
        
        /// <summary>
        /// هل الوثيقة قابلة للتجديد
        /// </summary>
        public bool IsRenewable { get; set; }
        
        /// <summary>
        /// هل تحتاج الوثيقة إلى تحقق
        /// </summary>
        public bool RequiresVerification { get; set; }
        
        /// <summary>
        /// أنواع الملفات المسموح بها (بالفواصل)
        /// </summary>
        public string AllowedFileTypes { get; set; }
        
        /// <summary>
        /// الحد الأقصى لحجم الملف (بالميجابايت)
        /// </summary>
        public decimal MaxFileSizeMB { get; set; }
        
        /// <summary>
        /// المدة الافتراضية للصلاحية (بالأيام، 0 للوثائق غير محددة المدة)
        /// </summary>
        public int DefaultValidityDays { get; set; }
        
        /// <summary>
        /// الأيام الافتراضية للتذكير قبل انتهاء الصلاحية
        /// </summary>
        public int DefaultReminderDays { get; set; }
    }
    
    /// <summary>
    /// نموذج تتبع تجديد الوثيقة
    /// </summary>
    public class DocumentRenewalHistory
    {
        /// <summary>
        /// المعرف
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// معرف الوثيقة
        /// </summary>
        public int DocumentID { get; set; }
        
        /// <summary>
        /// تاريخ التجديد
        /// </summary>
        public DateTime RenewalDate { get; set; }
        
        /// <summary>
        /// تاريخ الصلاحية السابق
        /// </summary>
        public DateTime PreviousExpiryDate { get; set; }
        
        /// <summary>
        /// تاريخ الصلاحية الجديد
        /// </summary>
        public DateTime NewExpiryDate { get; set; }
        
        /// <summary>
        /// تكلفة التجديد (إن وجدت)
        /// </summary>
        public decimal? RenewalCost { get; set; }
        
        /// <summary>
        /// المستخدم الذي قام بالتجديد
        /// </summary>
        public int RenewedByUserID { get; set; }
        
        /// <summary>
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// الوثيقة المرتبطة
        /// </summary>
        public virtual EmployeeDocument Document { get; set; }
    }
}