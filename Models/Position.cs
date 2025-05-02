using System;
using System.Collections.Generic;

namespace HR.Models
{
    /// <summary>
    /// نموذج المسمى الوظيفي - يتوافق مع جدول Positions في قاعدة البيانات
    /// </summary>
    public class Position
    {
        /// <summary>
        /// معرف المسمى الوظيفي
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// اسم المسمى الوظيفي
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// وصف المسمى الوظيفي
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// معرف القسم
        /// </summary>
        public int? DepartmentID { get; set; }

        /// <summary>
        /// القسم التابع له المسمى الوظيفي
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        /// الدرجة/المستوى الوظيفي
        /// </summary>
        public int? GradeLevel { get; set; }

        /// <summary>
        /// الحد الأدنى للراتب
        /// </summary>
        public decimal? MinSalary { get; set; }

        /// <summary>
        /// الحد الأقصى للراتب
        /// </summary>
        public decimal? MaxSalary { get; set; }

        /// <summary>
        /// هل المسمى الوظيفي لمدير
        /// </summary>
        public bool IsManagerPosition { get; set; }

        /// <summary>
        /// حالة المسمى الوظيفي (نشط/غير نشط)
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
        /// قائمة الموظفين الذين يشغلون هذا المسمى
        /// </summary>
        public virtual ICollection<Employee> Employees { get; set; }

        /// <summary>
        /// الأقسام التي يديرها هذا المسمى
        /// </summary>
        public virtual ICollection<Department> ManagedDepartments { get; set; }
    }
}