using System;
using System.Security.Cryptography;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Models;

namespace HR.Core
{
    /// <summary>
    /// مدير جلسة المستخدم
    /// </summary>
    public static class SessionManager
    {
        /// <summary>
        /// المستخدم الحالي
        /// </summary>
        public static User CurrentUser { get; private set; }
        
        /// <summary>
        /// مسار إعادة التوجيه بعد تسجيل الدخول
        /// </summary>
        public static string RedirectPath { get; set; }
        
        /// <summary>
        /// مدة صلاحية الجلسة
        /// </summary>
        public static TimeSpan SessionTimeout { get; set; } = TimeSpan.FromHours(8);
        
        /// <summary>
        /// وقت آخر نشاط
        /// </summary>
        private static DateTime _lastActivity;
        
        /// <summary>
        /// تهيئة جلسة المستخدم
        /// </summary>
        static SessionManager()
        {
            Reset();
        }
        
        /// <summary>
        /// إعادة تعيين الجلسة
        /// </summary>
        public static void Reset()
        {
            CurrentUser = null;
            RedirectPath = null;
            _lastActivity = DateTime.Now;
        }
        
        /// <summary>
        /// محاولة تسجيل الدخول
        /// </summary>
        public static bool Login(string username, string password)
        {
            try
            {
                // البحث عن المستخدم
                using (var unitOfWork = new UnitOfWork())
                {
                    var user = unitOfWork.UserRepository.GetByUsername(username);
                    
                    // التحقق من وجود المستخدم وتفعيله
                    if (user == null)
                    {
                        XtraMessageBox.Show(
                            "اسم المستخدم غير موجود",
                            "فشل تسجيل الدخول",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return false;
                    }
                    
                    // التحقق من تفعيل الحساب
                    if (!user.IsActive)
                    {
                        XtraMessageBox.Show(
                            "الحساب غير نشط. الرجاء التواصل مع مدير النظام",
                            "فشل تسجيل الدخول",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return false;
                    }
                    
                    // التحقق من قفل الحساب
                    if (user.IsLocked)
                    {
                        XtraMessageBox.Show(
                            "الحساب مقفل. الرجاء التواصل مع مدير النظام",
                            "فشل تسجيل الدخول",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return false;
                    }
                    
                    // التحقق من كلمة المرور
                    if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                    {
                        // زيادة عدد محاولات تسجيل الدخول الفاشلة
                        user.FailedLoginAttempts = (user.FailedLoginAttempts ?? 0) + 1;
                        
                        // قفل الحساب بعد 5 محاولات فاشلة
                        if (user.FailedLoginAttempts >= 5)
                        {
                            user.IsLocked = true;
                            unitOfWork.UserRepository.Update(user);
                            unitOfWork.Complete();
                            
                            XtraMessageBox.Show(
                                "تم قفل الحساب بعد 5 محاولات تسجيل دخول فاشلة. الرجاء التواصل مع مدير النظام",
                                "فشل تسجيل الدخول",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return false;
                        }
                        
                        unitOfWork.UserRepository.Update(user);
                        unitOfWork.Complete();
                        
                        XtraMessageBox.Show(
                            "كلمة المرور غير صحيحة",
                            "فشل تسجيل الدخول",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return false;
                    }
                    
                    // تحديث معلومات تسجيل الدخول
                    user.FailedLoginAttempts = 0;
                    user.LastLogin = DateTime.Now;
                    
                    unitOfWork.UserRepository.Update(user);
                    unitOfWork.Complete();
                    
                    // تعيين المستخدم الحالي
                    CurrentUser = user;
                    _lastActivity = DateTime.Now;
                    
                    // جلب الدور والصلاحيات
                    CurrentUser.Role = unitOfWork.RoleRepository.GetById(CurrentUser.RoleID);
                    CurrentUser.Role.Permissions = unitOfWork.RoleRepository.GetRolePermissions(CurrentUser.RoleID);
                    
                    // جلب الموظف المرتبط (إن وجد)
                    if (CurrentUser.EmployeeID.HasValue)
                    {
                        CurrentUser.Employee = unitOfWork.EmployeeRepository.GetById(CurrentUser.EmployeeID.Value);
                    }
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تسجيل الدخول");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تسجيل الدخول: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }
        
        /// <summary>
        /// تسجيل الخروج
        /// </summary>
        public static void Logout()
        {
            Reset();
        }
        
        /// <summary>
        /// تحديث وقت آخر نشاط
        /// </summary>
        public static void UpdateActivity()
        {
            _lastActivity = DateTime.Now;
        }
        
        /// <summary>
        /// التحقق من انتهاء صلاحية الجلسة
        /// </summary>
        public static bool IsSessionExpired()
        {
            return (DateTime.Now - _lastActivity) > SessionTimeout;
        }
        
        /// <summary>
        /// التحقق من صحة كلمة المرور
        /// </summary>
        private static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            byte[] salt = Convert.FromBase64String(storedSalt);
            byte[] hash = Convert.FromBase64String(storedHash);
            
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] newHash = pbkdf2.GetBytes(20);
                
                // مقارنة الهاش المخزن مع الهاش الجديد
                for (int i = 0; i < 20; i++)
                {
                    if (hash[i] != newHash[i])
                    {
                        return false;
                    }
                }
                
                return true;
            }
        }
        
        /// <summary>
        /// التحقق من صلاحية المستخدم للوصول إلى وحدة معينة
        /// </summary>
        public static bool HasPermission(string moduleName, string permissionType)
        {
            // التحقق من تسجيل الدخول
            if (CurrentUser == null || CurrentUser.Role == null || CurrentUser.Role.Permissions == null)
            {
                return false;
            }
            
            // البحث عن الصلاحية للوحدة المطلوبة
            foreach (var permission in CurrentUser.Role.Permissions)
            {
                if (permission.ModuleName == moduleName)
                {
                    switch (permissionType.ToLower())
                    {
                        case "view":
                            return permission.CanView;
                        case "add":
                            return permission.CanAdd;
                        case "edit":
                            return permission.CanEdit;
                        case "delete":
                            return permission.CanDelete;
                        case "print":
                            return permission.CanPrint;
                        case "export":
                            return permission.CanExport;
                        case "import":
                            return permission.CanImport;
                        case "approve":
                            return permission.CanApprove;
                        default:
                            return false;
                    }
                }
            }
            
            return false;
        }
    }
}