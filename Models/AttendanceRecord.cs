using System;

namespace HR.Models
{
    /// <summary>
    /// كائن يمثل سجل حضور
    /// </summary>
    public class AttendanceRecord
    {
        /// <summary>
        /// معرف السجل
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
        /// تاريخ الحضور
        /// </summary>
        public DateTime AttendanceDate { get; set; }
        
        /// <summary>
        /// وقت الدخول
        /// </summary>
        public DateTime? TimeIn { get; set; }
        
        /// <summary>
        /// وقت الخروج
        /// </summary>
        public DateTime? TimeOut { get; set; }
        
        /// <summary>
        /// معرف جدول العمل
        /// </summary>
        public int? WorkHoursID { get; set; }
        
        /// <summary>
        /// دقائق التأخير
        /// </summary>
        public int LateMinutes { get; set; }
        
        /// <summary>
        /// دقائق المغادرة المبكرة
        /// </summary>
        public int EarlyDepartureMinutes { get; set; }
        
        /// <summary>
        /// دقائق العمل الإضافي
        /// </summary>
        public int OvertimeMinutes { get; set; }
        
        /// <summary>
        /// دقائق العمل الفعلي
        /// </summary>
        public int WorkedMinutes { get; set; }
        
        /// <summary>
        /// حالة الحضور (حاضر، غائب، متأخر، مغادرة مبكرة، إجازة)
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// هل تم إدخال السجل يدوياً
        /// </summary>
        public bool IsManualEntry { get; set; }
        
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
    }
}