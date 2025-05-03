using System;
using System.Collections.Generic;

namespace HR.Models
{
    /// <summary>
    /// نموذج نوع الإجازة
    /// </summary>
    public class LeaveType
    {
        /// <summary>
        /// معرف نوع الإجازة
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// اسم نوع الإجازة
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// وصف نوع الإجازة
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// عدد الأيام الافتراضية
        /// </summary>
        public int DefaultDays { get; set; }
        
        /// <summary>
        /// الحد الأقصى لعدد الأيام
        /// </summary>
        public int MaximumDays { get; set; }
        
        /// <summary>
        /// هل هي إجازة مدفوعة الأجر
        /// </summary>
        public bool IsPaid { get; set; }
        
        /// <summary>
        /// هل تتطلب موافقة
        /// </summary>
        public bool RequiresApproval { get; set; }
        
        /// <summary>
        /// هل نوع الإجازة نشط
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// رمز اللون المستخدم في العرض
        /// </summary>
        public string ColorCode { get; set; }
        
        /// <summary>
        /// أولوية الإجازة (منخفضة، متوسطة، عالية)
        /// </summary>
        public string Priority { get; set; }
        
        /// <summary>
        /// معرف منشئ السجل
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// تاريخ إنشاء السجل
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// معرف آخر من قام بتعديل السجل
        /// </summary>
        public int ModifiedBy { get; set; }
        
        /// <summary>
        /// تاريخ آخر تعديل للسجل
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        
        /// <summary>
        /// قائمة طلبات الإجازات المرتبطة بهذا النوع
        /// </summary>
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
        
        /// <summary>
        /// قائمة أرصدة الإجازات المرتبطة بهذا النوع
        /// </summary>
        public virtual ICollection<LeaveBalance> LeaveBalances { get; set; }
    }
}