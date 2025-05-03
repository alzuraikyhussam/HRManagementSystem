using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج راتب الموظف
    /// </summary>
    public class EmployeeSalary
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
        /// اسم الموظف (للعرض)
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// معرف عنصر الراتب
        /// </summary>
        public int ComponentID { get; set; }
        
        /// <summary>
        /// اسم عنصر الراتب (للعرض)
        /// </summary>
        public string ComponentName { get; set; }
        
        /// <summary>
        /// نوع عنصر الراتب (للعرض)
        /// </summary>
        public string ComponentType { get; set; }
        
        /// <summary>
        /// تاريخ بدء التطبيق
        /// </summary>
        public DateTime EffectiveDate { get; set; }
        
        /// <summary>
        /// تاريخ انتهاء التطبيق
        /// </summary>
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// هل العنصر نشط
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// المبلغ
        /// </summary>
        public decimal? Amount { get; set; }
        
        /// <summary>
        /// النسبة
        /// </summary>
        public decimal? Percentage { get; set; }
        
        /// <summary>
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// تاريخ الإنشاء
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// معرف المستخدم المنشئ
        /// </summary>
        public int? CreatedBy { get; set; }
        
        /// <summary>
        /// اسم المستخدم المنشئ (للعرض)
        /// </summary>
        public string CreatedByUser { get; set; }
        
        /// <summary>
        /// تاريخ التحديث
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        
        /// <summary>
        /// معرف المستخدم المحدث
        /// </summary>
        public int? UpdatedBy { get; set; }
        
        /// <summary>
        /// اسم المستخدم المحدث (للعرض)
        /// </summary>
        public string UpdatedByUser { get; set; }
    }
}