using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// Repository for role and permission operations
    /// </summary>
    public class RoleRepository
    {
        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <returns>List of RoleDTO objects</returns>
        public List<RoleDTO> GetAllRoles()
        {
            try
            {
                string query = @"
                    SELECT ID, Name, Description, CreatedAt, UpdatedAt
                    FROM Roles
                    ORDER BY Name";

                DataTable dataTable = ConnectionManager.ExecuteQuery(query);
                List<RoleDTO> roles = new List<RoleDTO>();

                foreach (DataRow row in dataTable.Rows)
                {
                    RoleDTO role = new RoleDTO
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Name = row["Name"].ToString(),
                        Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                        CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                        UpdatedAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : (DateTime?)null,
                        Permissions = GetRolePermissions(Convert.ToInt32(row["ID"]))
                    };

                    roles.Add(role);
                }

                return roles;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<RoleDTO>();
            }
        }

        /// <summary>
        /// Gets a role by ID
        /// </summary>
        /// <param name="roleId">Role ID</param>
        /// <returns>RoleDTO object</returns>
        public RoleDTO GetRoleById(int roleId)
        {
            try
            {
                string query = @"
                    SELECT ID, Name, Description, CreatedAt, UpdatedAt
                    FROM Roles
                    WHERE ID = @RoleID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@RoleID", roleId)
                };

                DataTable dataTable = ConnectionManager.ExecuteQuery(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                DataRow row = dataTable.Rows[0];

                RoleDTO role = new RoleDTO
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = row["Name"].ToString(),
                    Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    UpdatedAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : (DateTime?)null,
                    Permissions = GetRolePermissions(roleId)
                };

                return role;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets permissions for a role
        /// </summary>
        /// <param name="roleId">Role ID</param>
        /// <returns>List of RolePermissionDTO objects</returns>
        public List<RolePermissionDTO> GetRolePermissions(int? roleId)
        {
            try
            {
                if (!roleId.HasValue)
                {
                    return new List<RolePermissionDTO>();
                }

                string query = @"
                    SELECT ID, RoleID, ModuleName, CanView, CanAdd, CanEdit, 
                           CanDelete, CanPrint, CanExport, CanImport, CanApprove
                    FROM RolePermissions
                    WHERE RoleID = @RoleID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@RoleID", roleId.Value)
                };

                DataTable dataTable = ConnectionManager.ExecuteQuery(query, parameters);
                List<RolePermissionDTO> permissions = new List<RolePermissionDTO>();

                foreach (DataRow row in dataTable.Rows)
                {
                    RolePermissionDTO permission = new RolePermissionDTO
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        RoleID = Convert.ToInt32(row["RoleID"]),
                        ModuleName = row["ModuleName"].ToString(),
                        CanView = Convert.ToBoolean(row["CanView"]),
                        CanAdd = Convert.ToBoolean(row["CanAdd"]),
                        CanEdit = Convert.ToBoolean(row["CanEdit"]),
                        CanDelete = Convert.ToBoolean(row["CanDelete"]),
                        CanPrint = Convert.ToBoolean(row["CanPrint"]),
                        CanExport = Convert.ToBoolean(row["CanExport"]),
                        CanImport = Convert.ToBoolean(row["CanImport"]),
                        CanApprove = Convert.ToBoolean(row["CanApprove"])
                    };

                    permissions.Add(permission);
                }

                return permissions;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<RolePermissionDTO>();
            }
        }

        /// <summary>
        /// Saves a role (inserts if ID is 0, updates otherwise)
        /// </summary>
        /// <param name="role">RoleDTO object</param>
        /// <param name="userId">User ID performing the operation</param>
        /// <returns>ID of the saved role</returns>
        public int SaveRole(RoleDTO role, int userId)
        {
            try
            {
                string query;
                SqlParameter[] parameters;

                if (role.ID == 0)
                {
                    // Insert new role
                    query = @"
                        INSERT INTO Roles (Name, Description, CreatedAt)
                        VALUES (@Name, @Description, @CreatedAt);
                        SELECT SCOPE_IDENTITY();";

                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Name", role.Name),
                        new SqlParameter("@Description", (object)role.Description ?? DBNull.Value),
                        new SqlParameter("@CreatedAt", DateTime.Now)
                    };

                    object result = ConnectionManager.ExecuteScalar(query, parameters);
                    role.ID = Convert.ToInt32(result);

                    // Log activity
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    activityRepo.LogActivity(userId, "Add", "Roles", 
                        $"Added new role: {role.Name}", role.ID, null, null);

                    // Create default permissions for the role
                    if (role.Permissions != null && role.Permissions.Count > 0)
                    {
                        foreach (var permission in role.Permissions)
                        {
                            permission.RoleID = role.ID;
                            SaveRolePermission(permission);
                        }
                    }
                    else
                    {
                        // Create default empty permissions for all modules
                        PermissionManager.CreateDefaultPermissions(role.ID, false);
                    }

                    return role.ID;
                }
                else
                {
                    // Get existing role for activity log
                    RoleDTO existingRole = GetRoleById(role.ID);
                    string oldValues = Newtonsoft.Json.JsonConvert.SerializeObject(existingRole);

                    // Update existing role
                    query = @"
                        UPDATE Roles
                        SET Name = @Name,
                            Description = @Description,
                            UpdatedAt = @UpdatedAt
                        WHERE ID = @ID";

                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@ID", role.ID),
                        new SqlParameter("@Name", role.Name),
                        new SqlParameter("@Description", (object)role.Description ?? DBNull.Value),
                        new SqlParameter("@UpdatedAt", DateTime.Now)
                    };

                    int updateResult = ConnectionManager.ExecuteNonQuery(query, parameters);

                    // Update permissions if provided
                    if (role.Permissions != null && role.Permissions.Count > 0)
                    {
                        foreach (var permission in role.Permissions)
                        {
                            permission.RoleID = role.ID;
                            SaveRolePermission(permission);
                        }
                    }

                    // Log activity
                    if (updateResult > 0)
                    {
                        ActivityLogRepository activityRepo = new ActivityLogRepository();
                        string newValues = Newtonsoft.Json.JsonConvert.SerializeObject(role);
                        activityRepo.LogActivity(userId, "Edit", "Roles", 
                            $"Updated role: {role.Name}", role.ID, oldValues, newValues);
                    }

                    return role.ID;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Saves a role permission
        /// </summary>
        /// <param name="permission">RolePermissionDTO object</param>
        /// <returns>ID of the saved permission</returns>
        public int SaveRolePermission(RolePermissionDTO permission)
        {
            try
            {
                // Check if permission exists
                string checkQuery = @"
                    SELECT ID FROM RolePermissions 
                    WHERE RoleID = @RoleID AND ModuleName = @ModuleName";

                SqlParameter[] checkParams = new SqlParameter[]
                {
                    new SqlParameter("@RoleID", permission.RoleID),
                    new SqlParameter("@ModuleName", permission.ModuleName)
                };

                object existingId = ConnectionManager.ExecuteScalar(checkQuery, checkParams);

                if (existingId != null && Convert.ToInt32(existingId) > 0)
                {
                    // Update existing permission
                    string query = @"
                        UPDATE RolePermissions
                        SET CanView = @CanView,
                            CanAdd = @CanAdd,
                            CanEdit = @CanEdit,
                            CanDelete = @CanDelete,
                            CanPrint = @CanPrint,
                            CanExport = @CanExport,
                            CanImport = @CanImport,
                            CanApprove = @CanApprove
                        WHERE ID = @ID";

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@ID", Convert.ToInt32(existingId)),
                        new SqlParameter("@CanView", permission.CanView),
                        new SqlParameter("@CanAdd", permission.CanAdd),
                        new SqlParameter("@CanEdit", permission.CanEdit),
                        new SqlParameter("@CanDelete", permission.CanDelete),
                        new SqlParameter("@CanPrint", permission.CanPrint),
                        new SqlParameter("@CanExport", permission.CanExport),
                        new SqlParameter("@CanImport", permission.CanImport),
                        new SqlParameter("@CanApprove", permission.CanApprove)
                    };

                    ConnectionManager.ExecuteNonQuery(query, parameters);
                    return Convert.ToInt32(existingId);
                }
                else
                {
                    // Insert new permission
                    string query = @"
                        INSERT INTO RolePermissions (
                            RoleID, ModuleName, CanView, CanAdd, CanEdit, 
                            CanDelete, CanPrint, CanExport, CanImport, CanApprove)
                        VALUES (
                            @RoleID, @ModuleName, @CanView, @CanAdd, @CanEdit, 
                            @CanDelete, @CanPrint, @CanExport, @CanImport, @CanApprove);
                        SELECT SCOPE_IDENTITY();";

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@RoleID", permission.RoleID),
                        new SqlParameter("@ModuleName", permission.ModuleName),
                        new SqlParameter("@CanView", permission.CanView),
                        new SqlParameter("@CanAdd", permission.CanAdd),
                        new SqlParameter("@CanEdit", permission.CanEdit),
                        new SqlParameter("@CanDelete", permission.CanDelete),
                        new SqlParameter("@CanPrint", permission.CanPrint),
                        new SqlParameter("@CanExport", permission.CanExport),
                        new SqlParameter("@CanImport", permission.CanImport),
                        new SqlParameter("@CanApprove", permission.CanApprove)
                    };

                    object result = ConnectionManager.ExecuteScalar(query, parameters);
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Deletes a role
        /// </summary>
        /// <param name="roleId">Role ID to delete</param>
        /// <param name="userId">User ID performing the operation</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool DeleteRole(int roleId, int userId)
        {
            try
            {
                // Check if role is assigned to any users
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE RoleID = @RoleID";
                SqlParameter[] checkParams = new SqlParameter[]
                {
                    new SqlParameter("@RoleID", roleId)
                };
                int userCount = Convert.ToInt32(ConnectionManager.ExecuteScalar(checkQuery, checkParams));

                if (userCount > 0)
                {
                    return false; // Can't delete role that's assigned to users
                }

                // Get role details for activity log
                RoleDTO role = GetRoleById(roleId);
                if (role == null)
                {
                    return false;
                }

                string oldValues = Newtonsoft.Json.JsonConvert.SerializeObject(role);

                // Delete role permissions first
                string deletePermissions = "DELETE FROM RolePermissions WHERE RoleID = @RoleID";
                SqlParameter[] permissionParams = new SqlParameter[]
                {
                    new SqlParameter("@RoleID", roleId)
                };
                ConnectionManager.ExecuteNonQuery(deletePermissions, permissionParams);

                // Delete the role
                string query = "DELETE FROM Roles WHERE ID = @RoleID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@RoleID", roleId)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                // Log activity
                if (result > 0)
                {
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    activityRepo.LogActivity(userId, "Delete", "Roles", 
                        $"Deleted role: {role.Name}", roleId, oldValues, null);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Gets roles for a dropdown list
        /// </summary>
        /// <returns>DataTable with role ID and name</returns>
        public DataTable GetRolesForDropDown()
        {
            try
            {
                string query = @"
                    SELECT ID, Name
                    FROM Roles
                    ORDER BY Name";

                return ConnectionManager.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Creates an administrator role with full permissions
        /// </summary>
        /// <returns>ID of the created role</returns>
        public int CreateAdminRole()
        {
            try
            {
                // Check if admin role already exists
                string checkQuery = "SELECT COUNT(*) FROM Roles WHERE Name = N'مدير النظام'";
                int count = Convert.ToInt32(ConnectionManager.ExecuteScalar(checkQuery));

                if (count > 0)
                {
                    string getIdQuery = "SELECT ID FROM Roles WHERE Name = N'مدير النظام'";
                    return Convert.ToInt32(ConnectionManager.ExecuteScalar(getIdQuery));
                }

                // Create admin role
                string query = @"
                    INSERT INTO Roles (Name, Description, CreatedAt)
                    VALUES (N'مدير النظام', N'لديه كافة الصلاحيات في النظام', @CreatedAt);
                    SELECT SCOPE_IDENTITY();";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@CreatedAt", DateTime.Now)
                };

                object result = ConnectionManager.ExecuteScalar(query, parameters);
                int roleId = Convert.ToInt32(result);

                // Create permissions with full access for all modules
                PermissionManager.CreateDefaultPermissions(roleId, true);

                return roleId;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Gets the count of roles
        /// </summary>
        /// <returns>Number of roles</returns>
        public int GetRolesCount()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Roles";
                return Convert.ToInt32(ConnectionManager.ExecuteScalar(query));
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }
    }
}
