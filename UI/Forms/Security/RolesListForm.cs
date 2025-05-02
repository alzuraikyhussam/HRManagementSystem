using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;
using HR.Models;

namespace HR.UI.Forms.Security
{
    /// <summary>
    /// نموذج قائمة الأدوار والصلاحيات
    /// </summary>
    public partial class RolesListForm : XtraForm
    {
        // قائمة الأدوار
        private List<Role> _roles;
        
        /// <summary>
        /// تهيئة نموذج قائمة الأدوار
        /// </summary>
        public RolesListForm()
        {
            InitializeComponent();
            
            // ضبط خصائص النموذج
            this.Text = "إدارة الأدوار والصلاحيات";
            
            // تسجيل الأحداث
            this.Load += RolesListForm_Load;
            
            // تسجيل أحداث الأزرار
            buttonAdd.Click += ButtonAdd_Click;
            buttonEdit.Click += ButtonEdit_Click;
            buttonDelete.Click += ButtonDelete_Click;
            buttonRefresh.Click += ButtonRefresh_Click;
            
            // تسجيل أحداث الجدول
            gridViewRoles.DoubleClick += GridViewRoles_DoubleClick;
            gridViewRoles.FocusedRowChanged += GridViewRoles_FocusedRowChanged;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void RolesListForm_Load(object sender, EventArgs e)
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // تحميل البيانات
                LoadRoles();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل قائمة الأدوار");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل قائمة الأدوار: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// تحميل قائمة الأدوار
        /// </summary>
        private void LoadRoles()
        {
            try
            {
                // جلب الأدوار من قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    _roles = unitOfWork.RoleRepository.GetAll();
                }
                
                // عرض البيانات في الجدول
                gridControlRoles.DataSource = _roles;
                
                // إعادة ضبط عرض الأعمدة
                gridViewRoles.BestFitColumns();
                
                // تحديث حالة الأزرار
                UpdateButtonState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل جلب الأدوار من قاعدة البيانات");
                throw;
            }
        }

        /// <summary>
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonState()
        {
            // تعطيل أزرار التعديل والحذف إذا لم يكن هناك عنصر محدد
            bool hasSelection = gridViewRoles.GetSelectedRows().Length > 0;
            buttonEdit.Enabled = hasSelection;
            buttonDelete.Enabled = hasSelection;
        }

        /// <summary>
        /// حدث تغيير الصف المحدد في الجدول
        /// </summary>
        private void GridViewRoles_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            UpdateButtonState();
        }

        /// <summary>
        /// حدث النقر على زر إضافة دور جديد
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // فتح نموذج إضافة دور جديد
                RoleForm form = new RoleForm();
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات
                    LoadRoles();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل فتح نموذج إضافة دور جديد");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج إضافة دور جديد: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر تعديل دور
        /// </summary>
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            // الحصول على الدور المحدد
            Role selectedRole = GetSelectedRole();
            
            if (selectedRole != null)
            {
                EditRole(selectedRole.ID);
            }
            else
            {
                XtraMessageBox.Show(
                    "الرجاء تحديد دور أولاً",
                    "تنبيه",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// حدث النقر على زر حذف دور
        /// </summary>
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            // الحصول على الدور المحدد
            Role selectedRole = GetSelectedRole();
            
            if (selectedRole != null)
            {
                DeleteRole(selectedRole);
            }
            else
            {
                XtraMessageBox.Show(
                    "الرجاء تحديد دور أولاً",
                    "تنبيه",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// حدث النقر على زر تحديث البيانات
        /// </summary>
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // إعادة تحميل البيانات
                LoadRoles();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث قائمة الأدوار");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحديث قائمة الأدوار: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// حدث النقر المزدوج على الجدول
        /// </summary>
        private void GridViewRoles_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // الحصول على الصف المحدد
                int rowHandle = gridViewRoles.FocusedRowHandle;
                
                if (rowHandle >= 0)
                {
                    int roleId = Convert.ToInt32(gridViewRoles.GetRowCellValue(rowHandle, "ID"));
                    
                    // فتح نموذج تعديل الدور
                    EditRole(roleId);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل معالجة النقر المزدوج على الجدول");
            }
        }

        /// <summary>
        /// الحصول على الدور المحدد
        /// </summary>
        private Role GetSelectedRole()
        {
            try
            {
                // الحصول على الصف المحدد في الجدول
                int rowHandle = gridViewRoles.FocusedRowHandle;
                
                if (rowHandle >= 0)
                {
                    int roleId = Convert.ToInt32(gridViewRoles.GetRowCellValue(rowHandle, "ID"));
                    return _roles.Find(r => r.ID == roleId);
                }
                
                return null;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل الحصول على الدور المحدد");
                return null;
            }
        }

        /// <summary>
        /// تعديل دور
        /// </summary>
        private void EditRole(int roleId)
        {
            try
            {
                // فتح نموذج تعديل الدور
                RoleForm form = new RoleForm(roleId);
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات
                    LoadRoles();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل فتح نموذج تعديل الدور");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج تعديل الدور: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حذف دور
        /// </summary>
        private void DeleteRole(Role role)
        {
            try
            {
                // التحقق من وجود مستخدمين مرتبطين بهذا الدور
                using (var unitOfWork = new UnitOfWork())
                {
                    if (unitOfWork.UserRepository.Exists(u => u.RoleID == role.ID))
                    {
                        XtraMessageBox.Show(
                            "لا يمكن حذف هذا الدور لأنه مستخدم من قبل مستخدمين حاليين. يجب تغيير أدوار المستخدمين أولاً.",
                            "تنبيه",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }
                
                // تأكيد الحذف
                DialogResult result = XtraMessageBox.Show(
                    $"هل أنت متأكد من حذف الدور '{role.Name}'؟ سيتم حذف جميع الصلاحيات المرتبطة به.",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // حذف الدور
                    using (var unitOfWork = new UnitOfWork())
                    {
                        unitOfWork.RoleRepository.Delete(role.ID);
                        unitOfWork.Complete();
                    }
                    
                    // إعادة تحميل البيانات
                    LoadRoles();
                    
                    // عرض رسالة نجاح
                    XtraMessageBox.Show(
                        "تم حذف الدور بنجاح",
                        "تم الحذف",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حذف الدور");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء حذف الدور: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}