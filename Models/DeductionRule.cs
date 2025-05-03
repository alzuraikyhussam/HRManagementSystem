using System;

namespace HR.Models
{
    /// <summary>
    /// كائن يمثل قاعدة خصم
    /// </summary>
    public class DeductionRule
    {
        /// <summary>
        /// معرف القاعدة
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// اسم القاعدة
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// وصف القاعدة
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// نوع المخالفة (تأخير، غياب، مخالفة، إلخ)
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// طريقة الخصم (نسبة من الراتب، مبلغ ثابت، أيام، ساعات)
        /// </summary>
        public string DeductionMethod { get; set; }
        
        /// <summary>
        /// قيمة الخصم
        /// </summary>
        public decimal DeductionValue { get; set; }
        
        /// <summary>
        /// تطبق على (كل الموظفين، قسم محدد، درجة وظيفية محددة)
        /// </summary>
        public string AppliesTo { get; set; }
        
        /// <summary>
        /// معرف القسم
        /// </summary>
        public int? DepartmentID { get; set; }
        
        /// <summary>
        /// اسم القسم (للعرض فقط)
        /// </summary>
        public string DepartmentName { get; set; }
        
        /// <summary>
        /// معرف المنصب
        /// </summary>
        public int? PositionID { get; set; }
        
        /// <summary>
        /// اسم المنصب (للعرض فقط)
        /// </summary>
        public string PositionName { get; set; }
        
        /// <summary>
        /// الحد الأدنى للمخالفة (مثلاً دقائق التأخير)
        /// </summary>
        public decimal? MinViolation { get; set; }
        
        /// <summary>
        /// الحد الأقصى للمخالفة
        /// </summary>
        public decimal? MaxViolation { get; set; }
        
        /// <summary>
        /// تاريخ تفعيل القاعدة
        /// </summary>
        public DateTime? ActivationDate { get; set; }
        
        /// <summary>
        /// هل القاعدة نشطة
        /// </summary>
        public bool IsActive { get; set; }
        
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
        /// اسم المستخدم المنشئ (للعرض فقط)
        /// </summary>
        public string CreatedByUser { get; set; }
    }
}