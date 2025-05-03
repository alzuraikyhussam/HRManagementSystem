using System;
using System.Collections.Generic;

namespace HR.Models
{
    /// <summary>
    /// نموذج تفاصيل كشف راتب موظف
    /// </summary>
    public class PayrollDetail
    {
        /// <summary>
        /// المعرف
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// معرف كشف الرواتب
        /// </summary>
        public int PayrollID { get; set; }
        
        /// <summary>
        /// اسم كشف الرواتب (للعرض)
        /// </summary>
        public string PayrollName { get; set; }
        
        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// اسم الموظف (للعرض)
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// المسمى الوظيفي (للعرض)
        /// </summary>
        public string JobTitle { get; set; }
        
        /// <summary>
        /// القسم (للعرض)
        /// </summary>
        public string Department { get; set; }
        
        /// <summary>
        /// عدد أيام العمل
        /// </summary>
        public int WorkingDays { get; set; }
        
        /// <summary>
        /// عدد أيام الحضور
        /// </summary>
        public int PresentDays { get; set; }
        
        /// <summary>
        /// عدد أيام الغياب
        /// </summary>
        public int AbsentDays { get; set; }
        
        /// <summary>
        /// عدد أيام الإجازة
        /// </summary>
        public int LeaveDays { get; set; }
        
        /// <summary>
        /// دقائق التأخير
        /// </summary>
        public int LateMinutes { get; set; }
        
        /// <summary>
        /// ساعات العمل الإضافي
        /// </summary>
        public decimal OvertimeHours { get; set; }
        
        /// <summary>
        /// الراتب الأساسي
        /// </summary>
        public decimal BaseSalary { get; set; }
        
        /// <summary>
        /// إجمالي البدلات
        /// </summary>
        public decimal TotalAllowances { get; set; }
        
        /// <summary>
        /// إجمالي الخصومات
        /// </summary>
        public decimal TotalDeductions { get; set; }
        
        /// <summary>
        /// مبلغ العمل الإضافي
        /// </summary>
        public decimal OvertimeAmount { get; set; }
        
        /// <summary>
        /// صافي الراتب
        /// </summary>
        public decimal NetSalary { get; set; }
        
        /// <summary>
        /// حالة الراتب (محسوب، مدفوع، ملغي)
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// تفاصيل مكونات الراتب
        /// </summary>
        public List<PayrollComponentDetail> ComponentDetails { get; set; }
    }
}