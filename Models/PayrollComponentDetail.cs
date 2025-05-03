using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج تفاصيل مكون راتب
    /// </summary>
    public class PayrollComponentDetail
    {
        /// <summary>
        /// المعرف
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// معرف تفاصيل كشف الراتب
        /// </summary>
        public int PayrollDetailID { get; set; }
        
        /// <summary>
        /// معرف عنصر الراتب
        /// </summary>
        public int ComponentID { get; set; }
        
        /// <summary>
        /// اسم عنصر الراتب
        /// </summary>
        public string ComponentName { get; set; }
        
        /// <summary>
        /// نوع العنصر (أساسي، بدل، استقطاع، مكافأة، إلخ)
        /// </summary>
        public string ComponentType { get; set; }
        
        /// <summary>
        /// المبلغ
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// المعادلة المستخدمة
        /// </summary>
        public string Formula { get; set; }
        
        /// <summary>
        /// تاريخ الإنشاء
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}