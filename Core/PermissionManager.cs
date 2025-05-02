using System;
using System.Collections.Generic;
using HR.Models;

namespace HR.Core
{
    /// <summary>
    /// Manages application permissions and access control
    /// </summary>
    public static class PermissionManager
    {
        // List of available modules in the system
        private static readonly Dictionary<string, string> _modules = new Dictionary<string, string>
        {
            { "Dashboard", "لوحة المعلومات" },
            { "Company", "بيانات الشركة" },
            { "Departments", "الإدارات والأقسام" },
            { "Positions", "المسميات الوظيفية" },
            { "Users", "المستخدمين" },
            { "Roles", "الصلاحيات" },
            { "Employees", "الموظفين" },
            { "Attendance", "الحضور والانصراف" },
            { "Leaves", "الإجازات" },
            { "Payroll", "الرواتب" },
            { "Biometric", "أجهزة البصمة" },
            { "Reports", "التقارير" },
            { "Settings", "الإعدادات" }
        };

        /// <summary>
        /// Gets the list of available modules in the system
        /// </summary>
        /// <returns>Dictionary of module codes and names</returns>
        public static Dictionary<string, string> GetModules()
        {
            return _modules;
        }

        /// <summary>
        /// Checks if the current user has permission to access a specific module
        /// </summary>
        /// <param name="moduleName">The module name</param>
        /// <returns>True if the user has access, false otherwise</returns>
        public static bool CanAccessModule(string moduleName)
        {
            return SessionManager.HasPermission(moduleName, "view");
        }

        /// <summary>
        /// Checks if the current user has permission to view data in a module
        /// </summary>
        /// <param name="moduleName">The module name</param>
        /// <returns>True if the user has permission, false otherwise</returns>
        public static bool CanView(string moduleName)
        {
            return SessionManager.HasPermission(moduleName, "view");
        }

        /// <summary>
        /// Checks if the current user has permission to add data in a module
        /// </summary>
        /// <param name="moduleName">The module name</param>
        /// <returns>True if the user has permission, false otherwise</returns>
        public static bool CanAdd(string moduleName)
        {
            return SessionManager.HasPermission(moduleName, "add");
        }

        /// <summary>
        /// Checks if the current user has permission to edit data in a module
        /// </summary>
        /// <param name="moduleName">The module name</param>
        /// <returns>True if the user has permission, false otherwise</returns>
        public static bool CanEdit(string moduleName)
        {
            return SessionManager.HasPermission(moduleName, "edit");
        }

        /// <summary>
        /// Checks if the current user has permission to delete data in a module
        /// </summary>
        /// <param name="moduleName">The module name</param>
        /// <returns>True if the user has permission, false otherwise</returns>
        public static bool CanDelete(string moduleName)
        {
            return SessionManager.HasPermission(moduleName, "delete");
        }

        /// <summary>
        /// Checks if the current user has permission to print data in a module
        /// </summary>
        /// <param name="moduleName">The module name</param>
        /// <returns>True if the user has permission, false otherwise</returns>
        public static bool CanPrint(string moduleName)
        {
            return SessionManager.HasPermission(moduleName, "print");
        }

        /// <summary>
        /// Checks if the current user has permission to export data in a module
        /// </summary>
        /// <param name="moduleName">The module name</param>
        /// <returns>True if the user has permission, false otherwise</returns>
        public static bool CanExport(string moduleName)
        {
            return SessionManager.HasPermission(moduleName, "export");
        }

        /// <summary>
        /// Checks if the current user has permission to import data in a module
        /// </summary>
        /// <param name="moduleName">The module name</param>
        /// <returns>True if the user has permission, false otherwise</returns>
        public static bool CanImport(string moduleName)
        {
            return SessionManager.HasPermission(moduleName, "import");
        }

        /// <summary>
        /// Checks if the current user has permission to approve actions in a module
        /// </summary>
        /// <param name="moduleName">The module name</param>
        /// <returns>True if the user has permission, false otherwise</returns>
        public static bool CanApprove(string moduleName)
        {
            return SessionManager.HasPermission(moduleName, "approve");
        }

        /// <summary>
        /// Creates default permissions for a new role
        /// </summary>
        /// <param name="roleId">The role ID</param>
        /// <param name="isAdmin">Whether this is an admin role</param>
        public static void CreateDefaultPermissions(int roleId, bool isAdmin)
        {
            DataAccess.RoleRepository roleRepo = new DataAccess.RoleRepository();
            
            foreach (var module in _modules.Keys)
            {
                var permission = new RolePermissionDTO
                {
                    RoleID = roleId,
                    ModuleName = module,
                    CanView = isAdmin,
                    CanAdd = isAdmin,
                    CanEdit = isAdmin,
                    CanDelete = isAdmin,
                    CanPrint = isAdmin,
                    CanExport = isAdmin,
                    CanImport = isAdmin,
                    CanApprove = isAdmin
                };
                
                roleRepo.SaveRolePermission(permission);
            }
        }
    }
}
