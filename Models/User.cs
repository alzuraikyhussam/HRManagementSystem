using System;
using System.Collections.Generic;

namespace HR.Models
{
    /// <summary>
    /// نموذج بيانات المستخدم
    /// </summary>
    public class User
    {
        /// <summary>
        /// معرف المستخدم
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// اسم المستخدم
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// كلمة المرور المشفرة
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// ملح التشفير
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// الاسم الكامل للمستخدم
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// البريد الإلكتروني
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// رقم الجوال
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// معرف الموظف المرتبط (إن وجد)
        /// </summary>
        public int? EmployeeID { get; set; }

        /// <summary>
        /// الموظف المرتبط
        /// </summary>
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// معرف دور المستخدم
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// دور المستخدم
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// رمز التحقق لتغيير كلمة المرور
        /// </summary>
        public string ResetToken { get; set; }

        /// <summary>
        /// تاريخ انتهاء صلاحية رمز التحقق
        /// </summary>
        public DateTime? ResetTokenExpiry { get; set; }

        /// <summary>
        /// تاريخ آخر تسجيل دخول
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// عنوان IP لآخر تسجيل دخول
        /// </summary>
        public string LastLoginIP { get; set; }

        /// <summary>
        /// عدد محاولات تسجيل الدخول الفاشلة
        /// </summary>
        public int? FailedLoginAttempts { get; set; }

        /// <summary>
        /// هل الحساب مغلق؟
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// تاريخ غلق الحساب
        /// </summary>
        public DateTime? LockoutDate { get; set; }

        /// <summary>
        /// هل الحساب نشط؟
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// معرف معاملة إعادة التوثيق
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// تاريخ انتهاء صلاحية معاملة إعادة التوثيق
        /// </summary>
        public DateTime? RefreshTokenExpiry { get; set; }

        /// <summary>
        /// الاعدادات الشخصية للمستخدم (JSON)
        /// </summary>
        public string UserPreferences { get; set; }

        /// <summary>
        /// آخر تغيير لكلمة المرور
        /// </summary>
        public DateTime? LastPasswordChange { get; set; }

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
        /// سجل الدخول للنظام
        /// </summary>
        public virtual ICollection<LoginHistory> LoginHistory { get; set; }
    }
}