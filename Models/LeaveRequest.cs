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
        /// الموظف المرتبط
        /// </summary>
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// معرف نوع الإجازة
        /// </summary>
        public int LeaveTypeID { get; set; }

        /// <summary>
        /// نوع الإجازة
        /// </summary>
        public virtual LeaveType LeaveType { get; set; }

        /// <summary>
        /// تاريخ بداية الإجازة
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// تاريخ نهاية الإجازة
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// عدد أيام الإجازة
        /// </summary>
        public int DaysCount { get; set; }

        /// <summary>
        /// سبب الإجازة
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// حالة طلب الإجازة
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// معرف المدير الذي قام بالموافقة/الرفض
        /// </summary>
        public int? ApprovedByID { get; set; }

        /// <summary>
        /// المدير الذي قام بالموافقة/الرفض
        /// </summary>
        public virtual Employee ApprovedBy { get; set; }

        /// <summary>
        /// تاريخ الموافقة/الرفض
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// سبب الرفض
        /// </summary>
        public string RejectionReason { get; set; }

        /// <summary>
        /// مرفقات طلب الإجازة
        /// </summary>
        public byte[] Attachments { get; set; }

        /// <summary>
        /// مسار مرفقات طلب الإجازة
        /// </summary>
        public string AttachmentsPath { get; set; }

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