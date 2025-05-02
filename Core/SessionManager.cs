using System;
using System.Collections.Generic;
using HR.Models;

namespace HR.Core
{
    /// <summary>
    /// Manages user session information
    /// </summary>
    public static class SessionManager
    {
        private static UserDTO _currentUser;
        private static CompanyDTO _companyInfo;
        private static Dictionary<string, bool> _userPermissions;
        private static bool _isInitialized;

        /// <summary>
        /// Initializes the session manager
        /// </summary>
        public static void Initialize()
        {
            _isInitialized = true;
            _userPermissions = new Dictionary<string, bool>();
            LogManager.LogInfo("Session manager initialized");
        }

        /// <summary>
        /// Creates a new user session
        /// </summary>
        /// <param name="user">User information</param>
        /// <returns>True if login was successful</returns>
        public static bool CreateSession(UserDTO user)
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Session manager is not initialized");
            }

            try
            {
                _currentUser = user;
                
                // Load company information
                DataAccess.CompanyRepository companyRepo = new DataAccess.CompanyRepository();
                _companyInfo = companyRepo.GetCompanyInfo();
                
                // Load user permissions
                LoadUserPermissions();
                
                // Log user login
                LogManager.LogInfo($"User logged in: {user.Username} (ID: {user.ID})");
                
                // Update last login time
                DataAccess.UserRepository userRepo = new DataAccess.UserRepository();
                userRepo.UpdateLastLogin(user.ID);
                
                // Log activity
                DataAccess.ActivityLogRepository activityRepo = new DataAccess.ActivityLogRepository();
                activityRepo.LogActivity(user.ID, "Login", "Security", "User logged in", null, null, null);
                
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                _currentUser = null;
                _companyInfo = null;
                _userPermissions = new Dictionary<string, bool>();
                return false;
            }
        }

        /// <summary>
        /// Ends the current user session
        /// </summary>
        public static void EndSession()
        {
            if (_currentUser != null)
            {
                // Log activity
                DataAccess.ActivityLogRepository activityRepo = new DataAccess.ActivityLogRepository();
                activityRepo.LogActivity(_currentUser.ID, "Logout", "Security", "User logged out", null, null, null);
                
                // Log user logout
                LogManager.LogInfo($"User logged out: {_currentUser.Username} (ID: {_currentUser.ID})");
            }
            
            _currentUser = null;
            _companyInfo = null;
            _userPermissions = new Dictionary<string, bool>();
        }

        /// <summary>
        /// Gets the current user information
        /// </summary>
        public static UserDTO CurrentUser
        {
            get { return _currentUser; }
        }

        /// <summary>
        /// Gets the company information
        /// </summary>
        public static CompanyDTO CompanyInfo
        {
            get { return _companyInfo; }
        }

        /// <summary>
        /// Checks if a user is logged in
        /// </summary>
        public static bool IsLoggedIn
        {
            get { return _currentUser != null; }
        }

        /// <summary>
        /// Checks if the current user has a specific permission
        /// </summary>
        /// <param name="module">Module name</param>
        /// <param name="action">Permission action (View, Add, Edit, Delete, etc.)</param>
        /// <returns>True if the user has the permission</returns>
        public static bool HasPermission(string module, string action)
        {
            if (!IsLoggedIn)
            {
                return false;
            }

            // Admin has all permissions
            if (_currentUser.RoleID == 1)
            {
                return true;
            }

            string permissionKey = $"{module}_{action}";
            return _userPermissions.ContainsKey(permissionKey) && _userPermissions[permissionKey];
        }

        /// <summary>
        /// Loads user permissions into memory
        /// </summary>
        private static void LoadUserPermissions()
        {
            _userPermissions = new Dictionary<string, bool>();
            
            if (_currentUser == null || _currentUser.RoleID == null)
            {
                return;
            }

            try
            {
                DataAccess.RoleRepository roleRepo = new DataAccess.RoleRepository();
                List<RolePermissionDTO> permissions = roleRepo.GetRolePermissions(_currentUser.RoleID);

                foreach (var permission in permissions)
                {
                    // Add view permission
                    _userPermissions[$"{permission.ModuleName}_View"] = permission.CanView;
                    
                    // Add add permission
                    _userPermissions[$"{permission.ModuleName}_Add"] = permission.CanAdd;
                    
                    // Add edit permission
                    _userPermissions[$"{permission.ModuleName}_Edit"] = permission.CanEdit;
                    
                    // Add delete permission
                    _userPermissions[$"{permission.ModuleName}_Delete"] = permission.CanDelete;
                    
                    // Add print permission
                    _userPermissions[$"{permission.ModuleName}_Print"] = permission.CanPrint;
                    
                    // Add export permission
                    _userPermissions[$"{permission.ModuleName}_Export"] = permission.CanExport;
                    
                    // Add import permission
                    _userPermissions[$"{permission.ModuleName}_Import"] = permission.CanImport;
                    
                    // Add approve permission
                    _userPermissions[$"{permission.ModuleName}_Approve"] = permission.CanApprove;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to load user permissions");
            }
        }
    }
}