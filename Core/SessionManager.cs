using System;
using System.Security.Cryptography;
using System.Text;
using HR.Models;

namespace HR.Core
{
    /// <summary>
    /// مدير جلسات المستخدمين ومعلومات الجلسة الحالية
    /// </summary>
    public static class SessionManager
    {
        private static UserDTO _currentUser;
        private static CompanyDTO _companyInfo;
        private static string _sessionToken;

        /// <summary>
        /// تهيئة مدير الجلسات
        /// </summary>
        public static void Initialize()
        {
            LogManager.LogInfo("Session manager initialized");
        }

        /// <summary>
        /// تسجيل دخول المستخدم وإنشاء جلسة جديدة
        /// </summary>
        /// <param name="user">بيانات المستخدم المسجل</param>
        /// <returns>رمز الجلسة</returns>
        public static string Login(UserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                _currentUser = user;
                
                // إنشاء رمز جلسة عشوائي
                _sessionToken = GenerateSessionToken(user.ID);
                
                // تحميل معلومات الشركة
                LoadCompanyInfo();
                
                LogManager.LogInfo($"User {user.Username} logged in successfully");

                return _sessionToken;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"Failed to login user {user.Username}");
                _currentUser = null;
                _sessionToken = null;
                throw;
            }
        }

        /// <summary>
        /// تسجيل خروج المستخدم الحالي
        /// </summary>
        public static void Logout()
        {
            try
            {
                if (_currentUser != null)
                {
                    string username = _currentUser.Username;
                    _currentUser = null;
                    _sessionToken = null;
                    LogManager.LogInfo($"User {username} logged out");
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to logout user");
            }
        }

        /// <summary>
        /// تحميل معلومات الشركة
        /// </summary>
        private static void LoadCompanyInfo()
        {
            try
            {
                var companyRepository = new DataAccess.CompanyRepository();
                _companyInfo = companyRepository.GetCompanyInfo();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to load company information");
                _companyInfo = null;
            }
        }

        /// <summary>
        /// التحقق مما إذا كان هناك مستخدم مسجل دخوله حاليًا
        /// </summary>
        /// <returns>نتيجة التحقق</returns>
        public static bool IsUserLoggedIn()
        {
            return _currentUser != null && !string.IsNullOrEmpty(_sessionToken);
        }

        /// <summary>
        /// التحقق مما إذا كان المستخدم الحالي مدير النظام
        /// </summary>
        /// <returns>نتيجة التحقق</returns>
        public static bool IsCurrentUserAdmin()
        {
            return IsUserLoggedIn() && _currentUser.RoleName == "Administrator";
        }

        /// <summary>
        /// الحصول على المستخدم الحالي
        /// </summary>
        /// <returns>بيانات المستخدم الحالي</returns>
        public static UserDTO GetCurrentUser()
        {
            return _currentUser;
        }

        /// <summary>
        /// الحصول على معلومات الشركة الحالية
        /// </summary>
        /// <returns>بيانات الشركة</returns>
        public static CompanyDTO GetCompanyInfo()
        {
            return _companyInfo;
        }

        /// <summary>
        /// تحديث معلومات الشركة
        /// </summary>
        public static void RefreshCompanyInfo()
        {
            LoadCompanyInfo();
        }

        /// <summary>
        /// الحصول على رمز الجلسة الحالية
        /// </summary>
        /// <returns>رمز الجلسة</returns>
        public static string GetSessionToken()
        {
            return _sessionToken;
        }

        /// <summary>
        /// إنشاء رمز جلسة عشوائي
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        /// <returns>رمز الجلسة</returns>
        private static string GenerateSessionToken(int userId)
        {
            using (var sha256 = SHA256.Create())
            {
                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                string randomValue = Guid.NewGuid().ToString();
                string tokenInput = $"{userId}_{timeStamp}_{randomValue}";
                
                byte[] inputBytes = Encoding.UTF8.GetBytes(tokenInput);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                
                return sb.ToString();
            }
        }

        /// <summary>
        /// التحقق من صلاحية المستخدم الحالي للوصول إلى وحدة معينة
        /// </summary>
        /// <param name="moduleName">اسم الوحدة</param>
        /// <returns>نتيجة التحقق</returns>
        public static bool HasAccessToModule(string moduleName)
        {
            if (!IsUserLoggedIn() || string.IsNullOrEmpty(moduleName))
            {
                return false;
            }

            // مدير النظام له حق الوصول إلى كافة الوحدات
            if (IsCurrentUserAdmin())
            {
                return true;
            }

            // التحقق من الصلاحيات
            try
            {
                var roleRepository = new DataAccess.RoleRepository();
                var permissions = roleRepository.GetRolePermissions(_currentUser.RoleID.Value);

                foreach (var permission in permissions)
                {
                    if (permission.ModuleName == moduleName && permission.CanView)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"Failed to check access to module {moduleName}");
                return false;
            }
        }

        /// <summary>
        /// التحقق من صلاحية المستخدم الحالي لإجراء عملية معينة على وحدة معينة
        /// </summary>
        /// <param name="moduleName">اسم الوحدة</param>
        /// <param name="action">نوع العملية (Add, Edit, Delete, Export, إلخ)</param>
        /// <returns>نتيجة التحقق</returns>
        public static bool HasPermission(string moduleName, string action)
        {
            if (!IsUserLoggedIn() || string.IsNullOrEmpty(moduleName) || string.IsNullOrEmpty(action))
            {
                return false;
            }

            // مدير النظام له كافة الصلاحيات
            if (IsCurrentUserAdmin())
            {
                return true;
            }

            // التحقق من الصلاحيات
            try
            {
                var roleRepository = new DataAccess.RoleRepository();
                var permissions = roleRepository.GetRolePermissions(_currentUser.RoleID.Value);

                foreach (var permission in permissions)
                {
                    if (permission.ModuleName == moduleName)
                    {
                        switch (action.ToLower())
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
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"Failed to check permission for {action} on module {moduleName}");
                return false;
            }
        }
    }
}