using System;

namespace HR.Models
{
    /// <summary>
    /// كائن يمثل تصريح حضور
    /// </summary>
    public class AttendancePermission
    {
        /// <summary>
        /// معرف التصريح
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
        /// اسم القسم (للعرض فقط)
        /// </summary>
        public string DepartmentName { get; set; }
        
        /// <summary>
        /// تاريخ التصريح
        /// </summary>
        public DateTime PermissionDate { get; set; }
        
        /// <summary>
        /// نوع التصريح (تأخير صباحي، خروج مبكر، إلخ)
        /// </summary>
        public string PermissionType { get; set; }
        
        /// <summary>
        /// وقت البداية
        /// </summary>
        public TimeSpan? StartTime { get; set; }
        
        /// <summary>
        /// وقت النهاية
        /// </summary>
        public TimeSpan? EndTime { get; set; }
        
        /// <summary>
        /// إجمالي الدقائق
        /// </summary>
        public int? TotalMinutes { get; set; }
        
        /// <summary>
        /// سبب التصريح
        /// </summary>
        public string Reason { get; set; }
        
        /// <summary>
        /// حالة التصريح (مقدم، معتمد، مرفوض)
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
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }
        
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
        
        /// <summary>
        /// تاريخ التحديث
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}