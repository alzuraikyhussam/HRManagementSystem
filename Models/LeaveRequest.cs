using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج طلب الإجازة
    /// </summary>
    public class LeaveRequest
    {
        /// <summary>
        /// معرف طلب الإجازة
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// معرف نوع الإجازة
        /// </summary>
        public int LeaveTypeID { get; set; }
        
        /// <summary>
        /// تاريخ بداية الإجازة
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// تاريخ نهاية الإجازة
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// حالة الطلب (في الانتظار، تمت الموافقة، مرفوض، ملغي)
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// ملاحظات الطلب
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// تاريخ تقديم الطلب
        /// </summary>
        public DateTime SubmissionDate { get; set; }
        
        /// <summary>
        /// تاريخ الموافقة أو الرفض
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
        
        /// <summary>
        /// معرف من قام بالموافقة أو الرفض
        /// </summary>
        public int? ApprovedBy { get; set; }
        
        /// <summary>
        /// سبب الرفض
        /// </summary>
        public string ReasonForRejection { get; set; }
        
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
        /// الموظف المرتبط بالطلب
        /// </summary>
        public virtual Employee Employee { get; set; }
        
        /// <summary>
        /// نوع الإجازة المرتبط بالطلب
        /// </summary>
        public virtual LeaveType LeaveType { get; set; }
    }
}