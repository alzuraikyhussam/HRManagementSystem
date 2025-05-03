using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج عنصر الراتب
    /// </summary>
    public class SalaryComponent
    {
        /// <summary>
        /// المعرف
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// اسم العنصر
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// وصف العنصر
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// نوع العنصر (أساسي، بدل، استقطاع، مكافأة)
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// هل هو راتب أساسي
        /// </summary>
        public bool IsBasic { get; set; }
        
        /// <summary>
        /// هل هو متغير من شهر لآخر
        /// </summary>
        public bool IsVariable { get; set; }
        
        /// <summary>
        /// هل يخضع للضريبة
        /// </summary>
        public bool IsTaxable { get; set; }
        
        /// <summary>
        /// هل يؤثر في صافي الراتب
        /// </summary>
        public bool AffectsNetSalary { get; set; }
        
        /// <summary>
        /// ترتيب ظهور العنصر في كشف الراتب
        /// </summary>
        public int? Position { get; set; }
        
        /// <summary>
        /// نوع المعادلة (ثابت، نسبة من الأساسي، معادلة)
        /// </summary>
        public string FormulaType { get; set; }
        
        /// <summary>
        /// نسبة من (الراتب الأساسي، إجمالي الراتب، عنصر آخر)
        /// </summary>
        public string PercentageOf { get; set; }
        
        /// <summary>
        /// المبلغ الافتراضي
        /// </summary>
        public decimal? DefaultAmount { get; set; }
        
        /// <summary>
        /// النسبة الافتراضية
        /// </summary>
        public decimal? DefaultPercentage { get; set; }
        
        /// <summary>
        /// المعادلة الحسابية إن وجدت
        /// </summary>
        public string Formula { get; set; }
        
        /// <summary>
        /// هل العنصر نشط
        /// </summary>
        public bool IsActive { get; set; }
        
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
    }
}