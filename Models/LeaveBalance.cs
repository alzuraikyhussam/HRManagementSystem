using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج رصيد الإجازات
    /// </summary>
    public class LeaveBalance
    {
        /// <summary>
        /// معرف رصيد الإجازة
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
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }
        
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
        /// الموظف المرتبط بالرصيد
        /// </summary>
        public virtual Employee Employee { get; set; }
        
        /// <summary>
        /// نوع الإجازة المرتبط بالرصيد
        /// </summary>
        public virtual LeaveType LeaveType { get; set; }
        
        /// <summary>
        /// الرصيد المتبقي (Calculated Property)
        /// </summary>
        public decimal RemainingBalance
        {
            get { return BaseBalance + AdditionalBalance - UsedBalance; }
        }
    }
}