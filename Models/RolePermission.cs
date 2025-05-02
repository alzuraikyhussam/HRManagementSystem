using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج صلاحيات الأدوار
    /// </summary>
    public class RolePermission
    {
        /// <summary>
        /// معرف الصلاحية
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// معرف الدور
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// الدور المرتبط
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// اسم الصلاحية
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// وصف الصلاحية
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// اسم الوحدة
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// صلاحية القراءة
        /// </summary>
        public bool CanView { get; set; }

        /// <summary>
        /// صلاحية الإضافة
        /// </summary>
        public bool CanAdd { get; set; }

        /// <summary>
        /// صلاحية التعديل
        /// </summary>
        public bool CanEdit { get; set; }

        /// <summary>
        /// صلاحية الحذف
        /// </summary>
        public bool CanDelete { get; set; }

        /// <summary>
        /// صلاحية الطباعة
        /// </summary>
        public bool CanPrint { get; set; }

        /// <summary>
        /// صلاحية التصدير
        /// </summary>
        public bool CanExport { get; set; }

        /// <summary>
        /// صلاحية الموافقة
        /// </summary>
        public bool CanApprove { get; set; }

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
    }
}