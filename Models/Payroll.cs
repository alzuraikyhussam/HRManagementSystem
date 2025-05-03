using System;
using System.Collections.Generic;

namespace HR.Models
{
    /// <summary>
    /// نموذج كشف الرواتب
    /// </summary>
    public class Payroll
    {
        /// <summary>
        /// المعرف
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// اسم كشف الرواتب
        /// </summary>
        public string PayrollName { get; set; }
        
        /// <summary>
        /// فترة كشف الرواتب (الشهر/السنة)
        /// </summary>
        public string PayrollPeriod { get; set; }
        
        /// <summary>
        /// نوع كشف الرواتب (شهري، نصف شهري، أسبوعي)
        /// </summary>
        public string PayrollType { get; set; }
        
        /// <summary>
        /// شهر كشف الرواتب
        /// </summary>
        public int PayrollMonth { get; set; }
        
        /// <summary>
        /// سنة كشف الرواتب
        /// </summary>
        public int PayrollYear { get; set; }
        
        /// <summary>
        /// تاريخ بداية الفترة
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// تاريخ نهاية الفترة
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// تاريخ الدفع
        /// </summary>
        public DateTime? PaymentDate { get; set; }
        
        /// <summary>
        /// عدد الموظفين
        /// </summary>
        public int TotalEmployees { get; set; }
        
        /// <summary>
        /// إجمالي الراتب الأساسي
        /// </summary>
        public decimal TotalBasicSalary { get; set; }
        
        /// <summary>
        /// إجمالي البدلات
        /// </summary>
        public decimal TotalAllowances { get; set; }
        
        /// <summary>
        /// إجمالي الخصومات
        /// </summary>
        public decimal TotalDeductions { get; set; }
        
        /// <summary>
        /// إجمالي مبلغ العمل الإضافي
        /// </summary>
        public decimal TotalOvertimeAmount { get; set; }
        
        /// <summary>
        /// إجمالي مبلغ كشف الرواتب
        /// </summary>
        public decimal TotalPayrollAmount { get; set; }
        
        /// <summary>
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// حالة كشف الرواتب
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// هل تم إقفال كشف الرواتب
        /// </summary>
        public bool IsClosed { get; set; }
        
        /// <summary>
        /// معرف المستخدم المنشئ
        /// </summary>
        public int? CreatedBy { get; set; }
        
        /// <summary>
        /// اسم المستخدم المنشئ (للعرض)
        /// </summary>
        public string CreatedByUser { get; set; }
        
        /// <summary>
        /// تاريخ الإنشاء
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// معرف المستخدم المعتمد
        /// </summary>
        public int? ApprovedBy { get; set; }
        
        /// <summary>
        /// اسم المستخدم المعتمد (للعرض)
        /// </summary>
        public string ApprovedByUser { get; set; }
        
        /// <summary>
        /// تاريخ الاعتماد
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
        
        /// <summary>
        /// تفاصيل كشف الرواتب
        /// </summary>
        public List<PayrollDetail> PayrollDetails { get; set; }
    }
}