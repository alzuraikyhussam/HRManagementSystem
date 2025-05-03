using System;

namespace HR.Models
{
    /// <summary>
    /// كائن يمثل خصم على موظف
    /// </summary>
    public class Deduction
    {
        /// <summary>
        /// معرف الخصم
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// اسم الموظف (للعرض فقط)
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// القسم (للعرض فقط)
        /// </summary>
        public string DepartmentName { get; set; }
        
        /// <summary>
        /// معرف قاعدة الخصم
        /// </summary>
        public int? DeductionRuleID { get; set; }
        
        /// <summary>
        /// اسم قاعدة الخصم (للعرض فقط)
        /// </summary>
        public string DeductionRuleName { get; set; }
        
        /// <summary>
        /// تاريخ تسجيل الخصم
        /// </summary>
        public DateTime DeductionDate { get; set; }
        
        /// <summary>
        /// تاريخ المخالفة
        /// </summary>
        public DateTime ViolationDate { get; set; }
        
        /// <summary>
        /// نوع المخالفة (تأخير، غياب، مخالفة، إلخ)
        /// </summary>
        public string ViolationType { get; set; }
        
        /// <summary>
        /// قيمة المخالفة (عدد دقائق التأخير مثلاً)
        /// </summary>
        public decimal? ViolationValue { get; set; }
        
        /// <summary>
        /// طريقة الخصم (نسبة من الراتب، مبلغ ثابت، أيام، ساعات)
        /// </summary>
        public string DeductionMethod { get; set; }
        
        /// <summary>
        /// قيمة الخصم
        /// </summary>
        public decimal DeductionValue { get; set; }
        
        /// <summary>
        /// وصف الخصم
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// حالة الخصم (مقدم، معتمد، مرفوض، ملغي)
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// معرف المستخدم المعتمد
        /// </summary>
        public int? ApprovedBy { get; set; }
        
        /// <summary>
        /// اسم المستخدم المعتمد (للعرض فقط)
        /// </summary>
        public string ApprovedByUser { get; set; }
        
        /// <summary>
        /// تاريخ الاعتماد
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
        
        /// <summary>
        /// هل تم معالجته في الرواتب
        /// </summary>
        public bool IsPayrollProcessed { get; set; }
        
        /// <summary>
        /// معرف الراتب المرتبط
        /// </summary>
        public int? PayrollID { get; set; }
        
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