using System;

namespace HR.Models.DTOs
{
    /// <summary>
    /// نموذج بيانات الإجازة للعرض
    /// </summary>
    public class LeaveDTO
    {
        /// <summary>
        /// معرف الإجازة
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// اسم الموظف
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// نوع الإجازة
        /// </summary>
        public string LeaveType { get; set; }
        
        /// <summary>
        /// تاريخ البداية
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// تاريخ النهاية
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// المدة (عدد الأيام)
        /// </summary>
        public int Duration { get; set; }
        
        /// <summary>
        /// حالة الإجازة
        /// </summary>
        public string Status { get; set; }
    }
    
    /// <summary>
    /// نموذج بيانات طلب الإجازة للعرض
    /// </summary>
    public class LeaveRequestDTO
    {
        /// <summary>
        /// معرف طلب الإجازة
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// اسم الموظف
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// معرف نوع الإجازة
        /// </summary>
        public int LeaveTypeId { get; set; }
        
        /// <summary>
        /// نوع الإجازة
        /// </summary>
        public string LeaveType { get; set; }
        
        /// <summary>
        /// تاريخ البداية
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// تاريخ النهاية
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// عدد الأيام
        /// </summary>
        public int Days { get; set; }
        
        /// <summary>
        /// تاريخ تقديم الطلب
        /// </summary>
        public DateTime SubmissionDate { get; set; }
        
        /// <summary>
        /// حالة الطلب
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// تاريخ الموافقة أو الرفض
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
        
        /// <summary>
        /// معرف الشخص الذي وافق أو رفض
        /// </summary>
        public int? ApprovedById { get; set; }
        
        /// <summary>
        /// اسم الشخص الذي وافق أو رفض
        /// </summary>
        public string ApprovedByName { get; set; }
        
        /// <summary>
        /// سبب الرفض
        /// </summary>
        public string ReasonForRejection { get; set; }
    }
    
    /// <summary>
    /// نموذج بيانات رصيد الإجازات
    /// </summary>
    public class LeaveBalanceDTO
    {
        /// <summary>
        /// معرف الرصيد
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// اسم الموظف
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// معرف نوع الإجازة
        /// </summary>
        public int LeaveTypeId { get; set; }
        
        /// <summary>
        /// نوع الإجازة
        /// </summary>
        public string LeaveType { get; set; }
        
        /// <summary>
        /// السنة
        /// </summary>
        public int Year { get; set; }
        
        /// <summary>
        /// الرصيد الأساسي
        /// </summary>
        public decimal BaseBalance { get; set; }
        
        /// <summary>
        /// الرصيد الإضافي
        /// </summary>
        public decimal AdditionalBalance { get; set; }
        
        /// <summary>
        /// الرصيد المستخدم
        /// </summary>
        public decimal UsedBalance { get; set; }
        
        /// <summary>
        /// الرصيد المتبقي
        /// </summary>
        public decimal RemainingBalance { get; set; }
        
        /// <summary>
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }
    }
}