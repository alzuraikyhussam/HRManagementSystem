using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Security
{
    /// <summary>
    /// نموذج إدارة الدور
    /// </summary>
    public partial class RoleForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;
        private Role _currentRole;
        private bool _isNew = true;
        private List<ModulePermission> _modulePermissions = new List<ModulePermission>();

        /// <summary>
        /// إنشاء نموذج جديد
        /// </summary>
        public RoleForm()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
            _currentRole = new Role();
        }

        /// <summary>
        /// إنشاء نموذج لتعديل دور موجود
        /// </summary>
        /// <param name="roleId">معرف الدور</param>
        public RoleForm(int roleId) : this()
        {
            _isNew = false;
            LoadRole(roleId);
        }

        /// <summary>
        /// تحميل بيانات الدور
        /// </summary>
        /// <param name="roleId">معرف الدور</param>
        private void LoadRole(int roleId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@RoleID", roleId)
                };

                string query = @"
                SELECT * FROM Roles 
                WHERE ID = @RoleID";

                var dataTable = _dbContext.ExecuteReader(query, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    var row = dataTable.Rows[0];
                    _currentRole = new Role
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Name = row["Name"].ToString(),
                        Description = row["Description"].ToString(),
                        IsDefault = Convert.ToBoolean(row["IsDefault"]),
                        IsAdmin = Convert.ToBoolean(row["IsAdmin"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        CreatedAt = row["CreatedAt"] as DateTime?,
                        CreatedBy = row["CreatedBy"] as int?,
                        UpdatedAt = row["UpdatedAt"] as DateTime?,
                        UpdatedBy = row["UpdatedBy"] as int?
                    };

                    // تعبئة البيانات في الحقول
                    txtRoleName.Text = _currentRole.Name;
                    txtDescription.Text = _currentRole.Description;
                    chkIsDefault.Checked = _currentRole.IsDefault;
                    chkIsAdmin.Checked = _currentRole.IsAdmin;
                    chkIsActive.Checked = _currentRole.IsActive;

                    // تحميل صلاحيات الدور
                    LoadRolePermissions(roleId);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات الدور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل صلاحيات الدور
        /// </summary>
        /// <param name="roleId">معرف الدور</param>
        private void LoadRolePermissions(int roleId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@RoleID", roleId)
                };

                string query = @"
                SELECT * FROM RolePermissions 
                WHERE RoleID = @RoleID";

                var dataTable = _dbContext.ExecuteReader(query, parameters);

                // تحميل الوحدات والصلاحيات
                LoadModules();

                // تعيين صلاحيات الدور
                foreach (var row in dataTable.AsEnumerable())
                {
                    string moduleName = row["ModuleName"].ToString();
                    
                    var modulePermission = _modulePermissions.Find(m => m.ModuleName == moduleName);
                    if (modulePermission != null)
                    {
                        modulePermission.CanView = Convert.ToBoolean(row["CanView"]);
                        modulePermission.CanAdd = Convert.ToBoolean(row["CanAdd"]);
                        modulePermission.CanEdit = Convert.ToBoolean(row["CanEdit"]);
                        modulePermission.CanDelete = Convert.ToBoolean(row["CanDelete"]);
                        modulePermission.CanPrint = Convert.ToBoolean(row["CanPrint"]);
                        modulePermission.CanExport = Convert.ToBoolean(row["CanExport"]);
                        modulePermission.CanApprove = Convert.ToBoolean(row["CanApprove"]);
                    }
                }

                // تحديث عرض الصلاحيات
                RefreshPermissionsGrid();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل صلاحيات الدور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل الوحدات النظام
        /// </summary>
        private void LoadModules()
        {
            // إضافة وحدات النظام
            _modulePermissions.Clear();
            _modulePermissions.Add(new ModulePermission { ModuleName = "الإعدادات", Description = "إدارة إعدادات النظام" });
            _modulePermissions.Add(new ModulePermission { ModuleName = "المستخدمين", Description = "إدارة المستخدمين والصلاحيات" });
            _modulePermissions.Add(new ModulePermission { ModuleName = "الأدوار", Description = "إدارة أدوار المستخدمين" });
            _modulePermissions.Add(new ModulePermission { ModuleName = "الموظفين", Description = "إدارة بيانات الموظفين" });
            _modulePermissions.Add(new ModulePermission { ModuleName = "الحضور", Description = "إدارة الحضور والانصراف" });
            _modulePermissions.Add(new ModulePermission { ModuleName = "الإجازات", Description = "إدارة طلبات الإجازات" });
            _modulePermissions.Add(new ModulePermission { ModuleName = "الرواتب", Description = "إدارة الرواتب والمكافآت" });
            _modulePermissions.Add(new ModulePermission { ModuleName = "التقارير", Description = "استخراج التقارير" });
            _modulePermissions.Add(new ModulePermission { ModuleName = "الشركة", Description = "إدارة بيانات الشركة" });
            _modulePermissions.Add(new ModulePermission { ModuleName = "الأقسام", Description = "إدارة الأقسام والإدارات" });
            _modulePermissions.Add(new ModulePermission { ModuleName = "الوظائف", Description = "إدارة المسميات الوظيفية" });
            _modulePermissions.Add(new ModulePermission { ModuleName = "المناوبات", Description = "إدارة مناوبات العمل" });
        }

        /// <summary>
        /// تحديث عرض صلاحيات الدور
        /// </summary>
        private void RefreshPermissionsGrid()
        {
            // تحديث عرض الصلاحيات في الشبكة
            gridPermissions.DataSource = null;
            gridPermissions.DataSource = _modulePermissions;
            gridViewPermissions.RefreshData();
        }

        /// <summary>
        /// حفظ بيانات الدور
        /// </summary>
        private void SaveRole()
        {
            try
            {
                // التحقق من صحة البيانات
                if (string.IsNullOrWhiteSpace(txtRoleName.Text))
                {
                    XtraMessageBox.Show("يرجى إدخال اسم الدور", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRoleName.Focus();
                    return;
                }

                // تحديث بيانات الدور
                _currentRole.Name = txtRoleName.Text;
                _currentRole.Description = txtDescription.Text;
                _currentRole.IsDefault = chkIsDefault.Checked;
                _currentRole.IsAdmin = chkIsAdmin.Checked;
                _currentRole.IsActive = chkIsActive.Checked;

                // بدء المعاملة
                _dbContext.ExecuteTransaction((connection, transaction) =>
                {
                    if (_isNew)
                    {
                        // إضافة دور جديد
                        string insertRoleQuery = @"
                        INSERT INTO Roles (Name, Description, IsDefault, IsAdmin, IsActive, CreatedAt, CreatedBy)
                        VALUES (@Name, @Description, @IsDefault, @IsAdmin, @IsActive, @CreatedAt, @CreatedBy);
                        SELECT SCOPE_IDENTITY();";

                        List<SqlParameter> insertRoleParams = new List<SqlParameter>
                        {
                            new SqlParameter("@Name", _currentRole.Name),
                            new SqlParameter("@Description", _currentRole.Description ?? (object)DBNull.Value),
                            new SqlParameter("@IsDefault", _currentRole.IsDefault),
                            new SqlParameter("@IsAdmin", _currentRole.IsAdmin),
                            new SqlParameter("@IsActive", _currentRole.IsActive),
                            new SqlParameter("@CreatedAt", DateTime.Now),
                            new SqlParameter("@CreatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                        };

                        // تنفيذ الاستعلام وإرجاع المعرف الجديد
                        SqlCommand cmd = new SqlCommand(insertRoleQuery, connection, transaction);
                        cmd.Parameters.AddRange(insertRoleParams.ToArray());
                        _currentRole.ID = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    else
                    {
                        // تحديث دور موجود
                        string updateRoleQuery = @"
                        UPDATE Roles 
                        SET Name = @Name, 
                            Description = @Description, 
                            IsDefault = @IsDefault, 
                            IsAdmin = @IsAdmin, 
                            IsActive = @IsActive, 
                            UpdatedAt = @UpdatedAt, 
                            UpdatedBy = @UpdatedBy
                        WHERE ID = @ID";

                        List<SqlParameter> updateRoleParams = new List<SqlParameter>
                        {
                            new SqlParameter("@ID", _currentRole.ID),
                            new SqlParameter("@Name", _currentRole.Name),
                            new SqlParameter("@Description", _currentRole.Description ?? (object)DBNull.Value),
                            new SqlParameter("@IsDefault", _currentRole.IsDefault),
                            new SqlParameter("@IsAdmin", _currentRole.IsAdmin),
                            new SqlParameter("@IsActive", _currentRole.IsActive),
                            new SqlParameter("@UpdatedAt", DateTime.Now),
                            new SqlParameter("@UpdatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                        };

                        SqlCommand cmd = new SqlCommand(updateRoleQuery, connection, transaction);
                        cmd.Parameters.AddRange(updateRoleParams.ToArray());
                        cmd.ExecuteNonQuery();

                        // حذف الصلاحيات الحالية
                        string deletePermissionsQuery = "DELETE FROM RolePermissions WHERE RoleID = @RoleID";
                        SqlCommand deleteCmd = new SqlCommand(deletePermissionsQuery, connection, transaction);
                        deleteCmd.Parameters.Add(new SqlParameter("@RoleID", _currentRole.ID));
                        deleteCmd.ExecuteNonQuery();
                    }

                    // إضافة صلاحيات الدور
                    string insertPermissionQuery = @"
                    INSERT INTO RolePermissions (RoleID, ModuleName, PermissionName, Description, CanView, CanAdd, CanEdit, CanDelete, CanPrint, CanExport, CanApprove)
                    VALUES (@RoleID, @ModuleName, @PermissionName, @Description, @CanView, @CanAdd, @CanEdit, @CanDelete, @CanPrint, @CanExport, @CanApprove)";

                    foreach (var permission in _modulePermissions)
                    {
                        SqlCommand permCmd = new SqlCommand(insertPermissionQuery, connection, transaction);
                        permCmd.Parameters.AddRange(new SqlParameter[]
                        {
                            new SqlParameter("@RoleID", _currentRole.ID),
                            new SqlParameter("@ModuleName", permission.ModuleName),
                            new SqlParameter("@PermissionName", permission.ModuleName),
                            new SqlParameter("@Description", permission.Description),
                            new SqlParameter("@CanView", permission.CanView),
                            new SqlParameter("@CanAdd", permission.CanAdd),
                            new SqlParameter("@CanEdit", permission.CanEdit),
                            new SqlParameter("@CanDelete", permission.CanDelete),
                            new SqlParameter("@CanPrint", permission.CanPrint),
                            new SqlParameter("@CanExport", permission.CanExport),
                            new SqlParameter("@CanApprove", permission.CanApprove)
                        });
                        permCmd.ExecuteNonQuery();
                    }
                });

                // عرض رسالة نجاح
                XtraMessageBox.Show("تم حفظ بيانات الدور بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حفظ بيانات الدور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث الضغط على زر الحفظ
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRole();
        }

        /// <summary>
        /// حدث الضغط على زر الإلغاء
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void RoleForm_Load(object sender, EventArgs e)
        {
            // إذا كان دور جديد، تحميل الوحدات فقط
            if (_isNew)
            {
                LoadModules();
                RefreshPermissionsGrid();
            }

            // تعيين عنوان النموذج
            Text = _isNew ? "إضافة دور جديد" : "تعديل دور: " + _currentRole.Name;
        }

        /// <summary>
        /// تغيير جميع صلاحيات الوحدة المحددة
        /// </summary>
        private void SetAllPermissions(int rowIndex, bool value)
        {
            if (rowIndex >= 0 && rowIndex < _modulePermissions.Count)
            {
                var permission = _modulePermissions[rowIndex];
                permission.CanView = value;
                permission.CanAdd = value;
                permission.CanEdit = value;
                permission.CanDelete = value;
                permission.CanPrint = value;
                permission.CanExport = value;
                permission.CanApprove = value;

                gridViewPermissions.RefreshData();
            }
        }

        /// <summary>
        /// حدث تحديد الكل
        /// </summary>
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            // تحديد جميع الصلاحيات للصف المحدد
            int rowIndex = gridViewPermissions.FocusedRowHandle;
            SetAllPermissions(rowIndex, true);
        }

        /// <summary>
        /// حدث إلغاء تحديد الكل
        /// </summary>
        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            // إلغاء تحديد جميع الصلاحيات للصف المحدد
            int rowIndex = gridViewPermissions.FocusedRowHandle;
            SetAllPermissions(rowIndex, false);
        }
    }

    /// <summary>
    /// فئة مساعدة لعرض صلاحيات الوحدة
    /// </summary>
    public class ModulePermission
    {
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }
        public bool CanExport { get; set; }
        public bool CanApprove { get; set; }
    }
}