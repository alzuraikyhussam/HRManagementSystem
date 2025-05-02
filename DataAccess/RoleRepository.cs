using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// مستودع بيانات الأدوار وصلاحياتها
    /// </summary>
    public class RoleRepository
    {
        /// <summary>
        /// الحصول على كافة الأدوار
        /// </summary>
        /// <returns>قائمة بالأدوار</returns>
        public List<RoleDTO> GetAllRoles()
        {
            try
            {
                string query = @"
                    SELECT r.ID, r.Name, r.Description,
                          (SELECT COUNT(*) FROM Users WHERE RoleID = r.ID) AS UserCount
                    FROM Roles r
                    ORDER BY r.Name";

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query))
                {
                    List<RoleDTO> roles = new List<RoleDTO>();

                    while (reader.Read())
                    {
                        RoleDTO role = new RoleDTO
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            UserCount = reader.GetInt32(3),
                            Permissions = new List<RolePermissionDTO>()
                        };

                        roles.Add(role);
                    }

                    return roles;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الحصول على كافة الأدوار");
                throw;
            }
        }

        /// <summary>
        /// الحصول على الدور بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف الدور</param>
        /// <returns>بيانات الدور</returns>
        public RoleDTO GetRoleById(int id)
        {
            try
            {
                // الحصول على بيانات الدور
                string roleQuery = @"
                    SELECT r.ID, r.Name, r.Description,
                          (SELECT COUNT(*) FROM Users WHERE RoleID = r.ID) AS UserCount
                    FROM Roles r
                    WHERE r.ID = @ID";

                SqlParameter[] roleParameters =
                {
                    new SqlParameter("@ID", id)
                };

                RoleDTO role = null;
                using (SqlDataReader reader = ConnectionManager.ExecuteReader(roleQuery, roleParameters))
                {
                    if (reader.Read())
                    {
                        role = new RoleDTO
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            UserCount = reader.GetInt32(3),
                            Permissions = new List<RolePermissionDTO>()
                        };
                    }
                    else
                    {
                        return null;
                    }
                }

                // الحصول على صلاحيات الدور
                string permissionsQuery = @"
                    SELECT ID, RoleID, ModuleName, CanView, CanAdd, CanEdit, CanDelete,
                           CanPrint, CanExport, CanImport, CanApprove
                    FROM RolePermissions
                    WHERE RoleID = @RoleID";

                SqlParameter[] permissionsParameters =
                {
                    new SqlParameter("@RoleID", id)
                };

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(permissionsQuery, permissionsParameters))
                {
                    while (reader.Read())
                    {
                        RolePermissionDTO permission = new RolePermissionDTO
                        {
                            ID = reader.GetInt32(0),
                            RoleID = reader.GetInt32(1),
                            ModuleName = reader.IsDBNull(2) ? null : reader.GetString(2),
                            CanView = reader.GetBoolean(3),
                            CanAdd = reader.GetBoolean(4),
                            CanEdit = reader.GetBoolean(5),
                            CanDelete = reader.GetBoolean(6),
                            CanPrint = reader.GetBoolean(7),
                            CanExport = reader.GetBoolean(8),
                            CanImport = reader.GetBoolean(9),
                            CanApprove = reader.GetBoolean(10)
                        };

                        role.Permissions.Add(permission);
                    }
                }

                return role;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في الحصول على الدور رقم {id}");
                throw;
            }
        }

        /// <summary>
        /// الحصول على صلاحيات الدور
        /// </summary>
        /// <param name="roleId">معرف الدور</param>
        /// <returns>قائمة بصلاحيات الدور</returns>
        public List<RolePermissionDTO> GetRolePermissions(int roleId)
        {
            try
            {
                string query = @"
                    SELECT ID, RoleID, ModuleName, CanView, CanAdd, CanEdit, CanDelete,
                           CanPrint, CanExport, CanImport, CanApprove
                    FROM RolePermissions
                    WHERE RoleID = @RoleID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@RoleID", roleId)
                };

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query, parameters))
                {
                    List<RolePermissionDTO> permissions = new List<RolePermissionDTO>();

                    while (reader.Read())
                    {
                        RolePermissionDTO permission = new RolePermissionDTO
                        {
                            ID = reader.GetInt32(0),
                            RoleID = reader.GetInt32(1),
                            ModuleName = reader.IsDBNull(2) ? null : reader.GetString(2),
                            CanView = reader.GetBoolean(3),
                            CanAdd = reader.GetBoolean(4),
                            CanEdit = reader.GetBoolean(5),
                            CanDelete = reader.GetBoolean(6),
                            CanPrint = reader.GetBoolean(7),
                            CanExport = reader.GetBoolean(8),
                            CanImport = reader.GetBoolean(9),
                            CanApprove = reader.GetBoolean(10)
                        };

                        permissions.Add(permission);
                    }

                    return permissions;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في الحصول على صلاحيات الدور رقم {roleId}");
                throw;
            }
        }

        /// <summary>
        /// إنشاء دور جديد
        /// </summary>
        /// <param name="role">بيانات الدور</param>
        /// <returns>معرف الدور الجديد</returns>
        public int CreateRole(RoleDTO role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // إنشاء الدور
                            string roleQuery = @"
                                INSERT INTO Roles (Name, Description, CreatedAt)
                                VALUES (@Name, @Description, GETDATE());
                                SELECT SCOPE_IDENTITY();";

                            SqlCommand roleCommand = new SqlCommand(roleQuery, connection, transaction);
                            roleCommand.Parameters.AddWithValue("@Name", (object)role.Name ?? DBNull.Value);
                            roleCommand.Parameters.AddWithValue("@Description", (object)role.Description ?? DBNull.Value);

                            int roleId = Convert.ToInt32(roleCommand.ExecuteScalar());

                            // إنشاء صلاحيات الدور
                            if (role.Permissions != null && role.Permissions.Count > 0)
                            {
                                foreach (var permission in role.Permissions)
                                {
                                    string permissionQuery = @"
                                        INSERT INTO RolePermissions (
                                            RoleID, ModuleName, CanView, CanAdd, CanEdit, CanDelete,
                                            CanPrint, CanExport, CanImport, CanApprove
                                        )
                                        VALUES (
                                            @RoleID, @ModuleName, @CanView, @CanAdd, @CanEdit, @CanDelete,
                                            @CanPrint, @CanExport, @CanImport, @CanApprove
                                        )";

                                    SqlCommand permissionCommand = new SqlCommand(permissionQuery, connection, transaction);
                                    permissionCommand.Parameters.AddWithValue("@RoleID", roleId);
                                    permissionCommand.Parameters.AddWithValue("@ModuleName", (object)permission.ModuleName ?? DBNull.Value);
                                    permissionCommand.Parameters.AddWithValue("@CanView", permission.CanView);
                                    permissionCommand.Parameters.AddWithValue("@CanAdd", permission.CanAdd);
                                    permissionCommand.Parameters.AddWithValue("@CanEdit", permission.CanEdit);
                                    permissionCommand.Parameters.AddWithValue("@CanDelete", permission.CanDelete);
                                    permissionCommand.Parameters.AddWithValue("@CanPrint", permission.CanPrint);
                                    permissionCommand.Parameters.AddWithValue("@CanExport", permission.CanExport);
                                    permissionCommand.Parameters.AddWithValue("@CanImport", permission.CanImport);
                                    permissionCommand.Parameters.AddWithValue("@CanApprove", permission.CanApprove);

                                    permissionCommand.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return roleId;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في إنشاء دور جديد");
                throw;
            }
        }

        /// <summary>
        /// تحديث دور
        /// </summary>
        /// <param name="role">بيانات الدور</param>
        /// <returns>نجاح العملية</returns>
        public bool UpdateRole(RoleDTO role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // تحديث الدور
                            string roleQuery = @"
                                UPDATE Roles
                                SET Name = @Name,
                                    Description = @Description,
                                    UpdatedAt = GETDATE()
                                WHERE ID = @ID";

                            SqlCommand roleCommand = new SqlCommand(roleQuery, connection, transaction);
                            roleCommand.Parameters.AddWithValue("@ID", role.ID);
                            roleCommand.Parameters.AddWithValue("@Name", (object)role.Name ?? DBNull.Value);
                            roleCommand.Parameters.AddWithValue("@Description", (object)role.Description ?? DBNull.Value);

                            int rowsAffected = roleCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                // الدور غير موجود
                                transaction.Rollback();
                                return false;
                            }

                            // تحديث صلاحيات الدور

                            // حذف الصلاحيات الحالية
                            string deletePermissionsQuery = "DELETE FROM RolePermissions WHERE RoleID = @RoleID";
                            SqlCommand deletePermissionsCommand = new SqlCommand(deletePermissionsQuery, connection, transaction);
                            deletePermissionsCommand.Parameters.AddWithValue("@RoleID", role.ID);
                            deletePermissionsCommand.ExecuteNonQuery();

                            // إضافة الصلاحيات الجديدة
                            if (role.Permissions != null && role.Permissions.Count > 0)
                            {
                                foreach (var permission in role.Permissions)
                                {
                                    string permissionQuery = @"
                                        INSERT INTO RolePermissions (
                                            RoleID, ModuleName, CanView, CanAdd, CanEdit, CanDelete,
                                            CanPrint, CanExport, CanImport, CanApprove
                                        )
                                        VALUES (
                                            @RoleID, @ModuleName, @CanView, @CanAdd, @CanEdit, @CanDelete,
                                            @CanPrint, @CanExport, @CanImport, @CanApprove
                                        )";

                                    SqlCommand permissionCommand = new SqlCommand(permissionQuery, connection, transaction);
                                    permissionCommand.Parameters.AddWithValue("@RoleID", role.ID);
                                    permissionCommand.Parameters.AddWithValue("@ModuleName", (object)permission.ModuleName ?? DBNull.Value);
                                    permissionCommand.Parameters.AddWithValue("@CanView", permission.CanView);
                                    permissionCommand.Parameters.AddWithValue("@CanAdd", permission.CanAdd);
                                    permissionCommand.Parameters.AddWithValue("@CanEdit", permission.CanEdit);
                                    permissionCommand.Parameters.AddWithValue("@CanDelete", permission.CanDelete);
                                    permissionCommand.Parameters.AddWithValue("@CanPrint", permission.CanPrint);
                                    permissionCommand.Parameters.AddWithValue("@CanExport", permission.CanExport);
                                    permissionCommand.Parameters.AddWithValue("@CanImport", permission.CanImport);
                                    permissionCommand.Parameters.AddWithValue("@CanApprove", permission.CanApprove);

                                    permissionCommand.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في تحديث الدور رقم {role.ID}");
                throw;
            }
        }

        /// <summary>
        /// حذف دور
        /// </summary>
        /// <param name="id">معرف الدور</param>
        /// <returns>نجاح العملية</returns>
        public bool DeleteRole(int id)
        {
            try
            {
                // التحقق من عدم وجود مستخدمين مرتبطين بالدور
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE RoleID = @ID";
                
                SqlParameter[] checkParameters =
                {
                    new SqlParameter("@ID", id)
                };

                object result = ConnectionManager.ExecuteScalar(checkQuery, checkParameters);
                int count = Convert.ToInt32(result);

                if (count > 0)
                {
                    // لا يمكن حذف الدور لوجود مستخدمين مرتبطين به
                    return false;
                }

                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // حذف صلاحيات الدور
                            string deletePermissionsQuery = "DELETE FROM RolePermissions WHERE RoleID = @ID";
                            SqlCommand deletePermissionsCommand = new SqlCommand(deletePermissionsQuery, connection, transaction);
                            deletePermissionsCommand.Parameters.AddWithValue("@ID", id);
                            deletePermissionsCommand.ExecuteNonQuery();

                            // حذف الدور
                            string deleteRoleQuery = "DELETE FROM Roles WHERE ID = @ID";
                            SqlCommand deleteRoleCommand = new SqlCommand(deleteRoleQuery, connection, transaction);
                            deleteRoleCommand.Parameters.AddWithValue("@ID", id);
                            int rowsAffected = deleteRoleCommand.ExecuteNonQuery();

                            transaction.Commit();
                            return rowsAffected > 0;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل في حذف الدور رقم {id}");
                throw;
            }
        }

        /// <summary>
        /// الحصول على قائمة الوحدات (الصفحات) في النظام
        /// </summary>
        /// <returns>قائمة الوحدات</returns>
        public List<ModuleDTO> GetSystemModules()
        {
            // قائمة ثابتة بالوحدات في النظام
            List<ModuleDTO> modules = new List<ModuleDTO>
            {
                new ModuleDTO
                {
                    Name = "Dashboard",
                    DisplayName = "لوحة التحكم",
                    Description = "الصفحة الرئيسية والإحصائيات",
                    Group = "عام",
                    Order = 1,
                    Icon = "Home",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "CompanySettings",
                    DisplayName = "بيانات الشركة",
                    Description = "إعدادات بيانات الشركة",
                    Group = "الإعدادات",
                    Order = 2,
                    Icon = "Building",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "Departments",
                    DisplayName = "الإدارات والأقسام",
                    Description = "إدارة الإدارات والأقسام",
                    Group = "الهيكل التنظيمي",
                    Order = 3,
                    Icon = "OrgChart",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "Positions",
                    DisplayName = "المسميات الوظيفية",
                    Description = "إدارة المسميات الوظيفية",
                    Group = "الهيكل التنظيمي",
                    Order = 4,
                    Icon = "Badge",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "Employees",
                    DisplayName = "الموظفين",
                    Description = "إدارة بيانات الموظفين",
                    Group = "شؤون الموظفين",
                    Order = 5,
                    Icon = "People",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "EmployeeDocuments",
                    DisplayName = "وثائق الموظفين",
                    Description = "إدارة وثائق ومستندات الموظفين",
                    Group = "شؤون الموظفين",
                    Order = 6,
                    Icon = "Document",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "Attendance",
                    DisplayName = "الحضور والانصراف",
                    Description = "إدارة سجلات الحضور والانصراف",
                    Group = "الدوام",
                    Order = 7,
                    Icon = "Clock",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "AttendanceReports",
                    DisplayName = "تقارير الحضور",
                    Description = "تقارير الحضور والانصراف",
                    Group = "الدوام",
                    Order = 8,
                    Icon = "Report",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "Leaves",
                    DisplayName = "الإجازات",
                    Description = "إدارة طلبات وأرصدة الإجازات",
                    Group = "الإجازات",
                    Order = 9,
                    Icon = "Calendar",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "LeaveReports",
                    DisplayName = "تقارير الإجازات",
                    Description = "تقارير الإجازات",
                    Group = "الإجازات",
                    Order = 10,
                    Icon = "Report",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "SalarySettings",
                    DisplayName = "إعدادات الرواتب",
                    Description = "إعدادات وعناصر الرواتب",
                    Group = "الرواتب",
                    Order = 11,
                    Icon = "Money",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "EmployeeSalaries",
                    DisplayName = "رواتب الموظفين",
                    Description = "إدارة رواتب الموظفين",
                    Group = "الرواتب",
                    Order = 12,
                    Icon = "Salary",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "Payroll",
                    DisplayName = "كشوف الرواتب",
                    Description = "إدارة كشوف الرواتب الشهرية",
                    Group = "الرواتب",
                    Order = 13,
                    Icon = "List",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "Deductions",
                    DisplayName = "الخصومات والجزاءات",
                    Description = "إدارة الخصومات والجزاءات",
                    Group = "الرواتب",
                    Order = 14,
                    Icon = "Minus",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "BiometricDevices",
                    DisplayName = "أجهزة البصمة",
                    Description = "إدارة أجهزة البصمة",
                    Group = "النظام",
                    Order = 15,
                    Icon = "Fingerprint",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "Users",
                    DisplayName = "المستخدمين",
                    Description = "إدارة حسابات المستخدمين",
                    Group = "النظام",
                    Order = 16,
                    Icon = "User",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "Roles",
                    DisplayName = "الصلاحيات",
                    Description = "إدارة الأدوار والصلاحيات",
                    Group = "النظام",
                    Order = 17,
                    Icon = "Lock",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "SystemSettings",
                    DisplayName = "إعدادات النظام",
                    Description = "إعدادات النظام العامة",
                    Group = "النظام",
                    Order = 18,
                    Icon = "Settings",
                    IsActive = true
                },
                new ModuleDTO
                {
                    Name = "ActivityLog",
                    DisplayName = "سجل النشاطات",
                    Description = "عرض سجل نشاطات المستخدمين",
                    Group = "النظام",
                    Order = 19,
                    Icon = "History",
                    IsActive = true
                }
            };

            return modules;
        }
    }
}