using System;
using System.Collections.Generic;

namespace HR.Models
{
    /// <summary>
    /// نموذج القسم/الإدارة - يتوافق مع جدول Departments في قاعدة البيانات
    /// </summary>
    public class Department
    {
        /// <summary>
        /// معرف القسم
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// اسم القسم
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// وصف القسم
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// معرف القسم الرئيسي
        /// </summary>
        public int? ParentID { get; set; }

        /// <summary>
        /// القسم الرئيسي
        /// </summary>
        public virtual Department Parent { get; set; }

        /// <summary>
        /// الأقسام الفرعية
        /// </summary>
        public virtual ICollection<Department> SubDepartments { get; set; }

        /// <summary>
        /// معرف المسمى الوظيفي للمدير
        /// </summary>
        public int? ManagerPositionID { get; set; }

        /// <summary>
        /// المسمى الوظيفي للمدير
        /// </summary>
        public virtual Position ManagerPosition { get; set; }

        /// <summary>
        /// موقع القسم
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// حالة القسم (نشط/غير نشط)
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// تاريخ الإنشاء
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// تاريخ التعديل
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// قائمة الموظفين في القسم
        /// </summary>
        public virtual ICollection<Employee> Employees { get; set; }
    }
}