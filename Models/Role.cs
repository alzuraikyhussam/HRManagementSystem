using System;
using System.Collections.Generic;

namespace HR.Models
{
    /// <summary>
    /// نموذج دور المستخدم
    /// </summary>
    public class Role
    {
        /// <summary>
        /// معرف الدور
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// اسم الدور
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// وصف الدور
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// هل هو الدور الافتراضي للمستخدمين الجدد؟
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// هل هو دور مدير النظام؟
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// حالة الدور (نشط/غير نشط)
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
        /// قائمة المستخدمين الذين لديهم هذا الدور
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// قائمة صلاحيات هذا الدور
        /// </summary>
        public virtual ICollection<RolePermission> Permissions { get; set; }
    }
}