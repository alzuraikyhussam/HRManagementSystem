using System;
using System.Collections.Generic;

namespace HR.Models
{
    /// <summary>
    /// نموذج أنواع الإجازات
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
        /// الحد الأقصى للأيام المسموحة سنويًا
        /// </summary>
        public int? MaxDaysPerYear { get; set; }

        /// <summary>
        /// هل هي إجازة مدفوعة الراتب؟
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// هل تحتاج إلى موافقة؟
        /// </summary>
        public bool RequiresApproval { get; set; }

        /// <summary>
        /// هل هي متاحة لجميع الموظفين؟
        /// </summary>
        public bool IsActiveForAll { get; set; }

        /// <summary>
        /// الحد الأدنى للخدمة بالأشهر لاستحقاق هذا النوع من الإجازات
        /// </summary>
        public int? MinServiceMonths { get; set; }

        /// <summary>
        /// لون عرض هذا النوع من الإجازات في التقويم
        /// </summary>
        public string ColorCode { get; set; }

        /// <summary>
        /// ترتيب العرض
        /// </summary>
        public int? DisplayOrder { get; set; }

        /// <summary>
        /// هل يمكن ترحيل أيام الإجازة غير المستخدمة للسنة التالية؟
        /// </summary>
        public bool CanCarryForward { get; set; }

        /// <summary>
        /// الحد الأقصى للأيام التي يمكن ترحيلها
        /// </summary>
        public int? MaxCarryForwardDays { get; set; }

        /// <summary>
        /// حالة نوع الإجازة (نشط/غير نشط)
        /// </summary>
        public bool IsActive { get; set; }

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

        /// <summary>
        /// طلبات الإجازات المرتبطة بهذا النوع
        /// </summary>
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}