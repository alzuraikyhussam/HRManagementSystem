using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج وثائق الموظف - يتوافق مع جدول EmployeeDocuments في قاعدة البيانات
    /// </summary>
    public class EmployeeDocument
    {
        /// <summary>
        /// معرف الوثيقة
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
        /// نوع الوثيقة
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// عنوان الوثيقة
        /// </summary>
        public string DocumentTitle { get; set; }

        /// <summary>
        /// رقم الوثيقة
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// تاريخ الإصدار
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// تاريخ الانتهاء
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// الجهة المصدرة
        /// </summary>
        public string IssuedBy { get; set; }

        /// <summary>
        /// ملف الوثيقة
        /// </summary>
        public byte[] DocumentFile { get; set; }

        /// <summary>
        /// مسار ملف الوثيقة
        /// </summary>
        public string DocumentPath { get; set; }

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