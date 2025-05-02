using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Security
{
    /// <summary>
    /// نموذج قائمة الأدوار
    /// </summary>
    public partial class RolesListForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;

        /// <summary>
        /// إنشاء نموذج جديد
        /// </summary>
        public RolesListForm()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void RolesListForm_Load(object sender, EventArgs e)
        {
            // التحقق من وجود صلاحية للمستخدم
            CheckUserPermissions();

            // تحميل بيانات الأدوار
            LoadRoles();
        }

        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckUserPermissions()
        {
            // التحقق من وجود صلاحية للمستخدم
            bool canAdd = _sessionManager.HasPermission("الأدوار", "add");
            bool canEdit = _sessionManager.HasPermission("الأدوار", "edit");
            bool canDelete = _sessionManager.HasPermission("الأدوار", "delete");

            // تفعيل/تعطيل الأزرار حسب الصلاحيات
            btnAdd.Enabled = canAdd;
            btnEdit.Enabled = canEdit;
            btnDelete.Enabled = canDelete;
        }

        /// <summary>
        /// تحميل بيانات الأدوار
        /// </summary>
        private void LoadRoles()
        {
            try
            {
                string query = @"
                SELECT 
                    r.ID,
                    r.Name,
                    r.Description,
                    r.IsDefault,
                    r.IsAdmin,
                    r.IsActive,
                    r.CreatedAt,
                    u.FullName AS CreatedBy,
                    r.UpdatedAt,
                    u2.FullName AS UpdatedBy,
                    (SELECT COUNT(*) FROM Users WHERE RoleID = r.ID) AS UsersCount
                FROM 
                    Roles r
                LEFT JOIN 
                    Users u ON r.CreatedBy = u.ID
                LEFT JOIN 
                    Users u2 ON r.UpdatedBy = u2.ID
                ORDER BY 
                    r.Name";

                var dataTable = _dbContext.ExecuteReader(query);

                // عرض البيانات في الشبكة
                gridRoles.DataSource = dataTable;
                gridViewRoles.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات الأدوار: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث إضافة دور جديد
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود صلاحية للمستخدم
                if (!_sessionManager.HasPermission("الأدوار", "add"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية إضافة أدوار جديدة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // فتح نموذج إضافة دور جديد
                using (RoleForm roleForm = new RoleForm())
                {
                    if (roleForm.ShowDialog() == DialogResult.OK)
                    {
                        // إعادة تحميل البيانات
                        LoadRoles();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إضافة الدور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث تعديل دور موجود
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود صلاحية للمستخدم
                if (!_sessionManager.HasPermission("الأدوار", "edit"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية تعديل الأدوار", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // التحقق من اختيار دور
                if (gridViewRoles.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("يرجى اختيار دور للتعديل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // الحصول على معرف الدور
                int roleId = Convert.ToInt32(gridViewRoles.GetFocusedRowCellValue("ID"));

                // فتح نموذج تعديل الدور
                using (RoleForm roleForm = new RoleForm(roleId))
                {
                    if (roleForm.ShowDialog() == DialogResult.OK)
                    {
                        // إعادة تحميل البيانات
                        LoadRoles();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تعديل الدور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث حذف دور
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود صلاحية للمستخدم
                if (!_sessionManager.HasPermission("الأدوار", "delete"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية حذف الأدوار", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // التحقق من اختيار دور
                if (gridViewRoles.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("يرجى اختيار دور للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // الحصول على معرف وبيانات الدور
                int roleId = Convert.ToInt32(gridViewRoles.GetFocusedRowCellValue("ID"));
                string roleName = gridViewRoles.GetFocusedRowCellValue("Name").ToString();
                int usersCount = Convert.ToInt32(gridViewRoles.GetFocusedRowCellValue("UsersCount"));

                // التحقق مما إذا كان الدور مرتبط بمستخدمين
                if (usersCount > 0)
                {
                    XtraMessageBox.Show($"لا يمكن حذف الدور '{roleName}' لأنه مرتبط بـ {usersCount} مستخدم. يجب تعديل أدوار هؤلاء المستخدمين أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // تأكيد الحذف
                if (XtraMessageBox.Show($"هل أنت متأكد من حذف الدور '{roleName}'؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // حذف الدور
                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@RoleID", roleId)
                    };

                    _dbContext.ExecuteTransaction((connection, transaction) =>
                    {
                        // حذف صلاحيات الدور أولاً
                        string deletePermissionsQuery = "DELETE FROM RolePermissions WHERE RoleID = @RoleID";
                        SqlCommand deletePermCmd = new SqlCommand(deletePermissionsQuery, connection, transaction);
                        deletePermCmd.Parameters.Add(new SqlParameter("@RoleID", roleId));
                        deletePermCmd.ExecuteNonQuery();

                        // ثم حذف الدور
                        string deleteRoleQuery = "DELETE FROM Roles WHERE ID = @RoleID";
                        SqlCommand deleteRoleCmd = new SqlCommand(deleteRoleQuery, connection, transaction);
                        deleteRoleCmd.Parameters.Add(new SqlParameter("@RoleID", roleId));
                        deleteRoleCmd.ExecuteNonQuery();
                    });

                    // عرض رسالة نجاح
                    XtraMessageBox.Show("تم حذف الدور بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // إعادة تحميل البيانات
                    LoadRoles();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حذف الدور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث تحديث البيانات
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRoles();
        }

        /// <summary>
        /// حدث النقر المزدوج على صف في الشبكة
        /// </summary>
        private void gridViewRoles_DoubleClick(object sender, EventArgs e)
        {
            if (btnEdit.Enabled)
            {
                btnEdit_Click(sender, e);
            }
        }

        /// <summary>
        /// حدث تغيير تخطيط الشبكة
        /// </summary>
        private void btnChangeLayout_Click(object sender, EventArgs e)
        {
            // عرض محرر التخطيط
            gridViewRoles.ShowLayoutEditor();
        }

        /// <summary>
        /// حدث تصدير البيانات
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود صلاحية للمستخدم
                if (!_sessionManager.HasPermission("الأدوار", "export"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية تصدير بيانات الأدوار", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // عرض حوار حفظ الملف
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "ملف إكسل (*.xlsx)|*.xlsx|ملف PDF (*.pdf)|*.pdf";
                saveDialog.FileName = "تقرير_الأدوار";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveDialog.FileName;
                    string extension = System.IO.Path.GetExtension(filePath).ToLower();

                    switch (extension)
                    {
                        case ".xlsx":
                            gridRoles.ExportToXlsx(filePath);
                            break;
                        case ".pdf":
                            gridRoles.ExportToPdf(filePath);
                            break;
                    }

                    // فتح الملف بعد التصدير
                    if (XtraMessageBox.Show("تم تصدير البيانات بنجاح. هل تريد فتح الملف؟", "نجاح", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تصدير البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث طباعة البيانات
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود صلاحية للمستخدم
                if (!_sessionManager.HasPermission("الأدوار", "print"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية طباعة بيانات الأدوار", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // عرض معاينة الطباعة
                gridRoles.ShowPrintPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء طباعة البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}