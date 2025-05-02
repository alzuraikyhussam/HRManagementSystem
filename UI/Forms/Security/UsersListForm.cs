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
    /// نموذج قائمة المستخدمين
    /// </summary>
    public partial class UsersListForm : XtraForm
    {
        // قائمة المستخدمين
        private List<User> _users;
        
        /// <summary>
        /// تهيئة نموذج قائمة المستخدمين
        /// </summary>
        public UsersListForm()
        {
            InitializeComponent();
            
            // ضبط خصائص النموذج
            this.Text = "إدارة المستخدمين";
            
            // تسجيل الأحداث
            this.Load += UsersListForm_Load;
            
            // تسجيل أحداث الأزرار
            buttonAdd.Click += ButtonAdd_Click;
            buttonEdit.Click += ButtonEdit_Click;
            buttonDelete.Click += ButtonDelete_Click;
            buttonRefresh.Click += ButtonRefresh_Click;
            
            // تسجيل أحداث الجدول
            gridViewUsers.DoubleClick += GridViewUsers_DoubleClick;
            gridViewUsers.FocusedRowChanged += GridViewUsers_FocusedRowChanged;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void UsersListForm_Load(object sender, EventArgs e)
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // تحميل البيانات
                LoadUsers();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل قائمة المستخدمين");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل قائمة المستخدمين: {ex.Message}",
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
        /// تحميل قائمة المستخدمين
        /// </summary>
        private void LoadUsers()
        {
            try
            {
                // جلب المستخدمين من قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    _users = unitOfWork.UserRepository.GetAllWithRelated();
                }
                
                // عرض البيانات في الجدول
                gridControlUsers.DataSource = _users;
                
                // إعادة ضبط عرض الأعمدة
                gridViewUsers.BestFitColumns();
                
                // تحديث حالة الأزرار
                UpdateButtonState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل جلب المستخدمين من قاعدة البيانات");
                throw;
            }
        }

        /// <summary>
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonState()
        {
            // تعطيل أزرار التعديل والحذف إذا لم يكن هناك عنصر محدد
            bool hasSelection = gridViewUsers.GetSelectedRows().Length > 0;
            buttonEdit.Enabled = hasSelection;
            buttonDelete.Enabled = hasSelection;
        }

        /// <summary>
        /// حدث تغيير الصف المحدد في الجدول
        /// </summary>
        private void GridViewUsers_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            UpdateButtonState();
        }

        /// <summary>
        /// حدث النقر على زر إضافة مستخدم جديد
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // فتح نموذج إضافة مستخدم جديد
                UserForm form = new UserForm();
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل فتح نموذج إضافة مستخدم جديد");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج إضافة مستخدم جديد: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر تعديل مستخدم
        /// </summary>
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            // الحصول على المستخدم المحدد
            User selectedUser = GetSelectedUser();
            
            if (selectedUser != null)
            {
                EditUser(selectedUser.ID);
            }
            else
            {
                XtraMessageBox.Show(
                    "الرجاء تحديد مستخدم أولاً",
                    "تنبيه",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// حدث النقر على زر حذف مستخدم
        /// </summary>
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            // الحصول على المستخدم المحدد
            User selectedUser = GetSelectedUser();
            
            if (selectedUser != null)
            {
                DeleteUser(selectedUser);
            }
            else
            {
                XtraMessageBox.Show(
                    "الرجاء تحديد مستخدم أولاً",
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
                LoadUsers();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث قائمة المستخدمين");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحديث قائمة المستخدمين: {ex.Message}",
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
        private void GridViewUsers_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // الحصول على الصف المحدد
                int rowHandle = gridViewUsers.FocusedRowHandle;
                
                if (rowHandle >= 0)
                {
                    int userId = Convert.ToInt32(gridViewUsers.GetRowCellValue(rowHandle, "ID"));
                    
                    // فتح نموذج تعديل المستخدم
                    EditUser(userId);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل معالجة النقر المزدوج على الجدول");
            }
        }

        /// <summary>
        /// الحصول على المستخدم المحدد
        /// </summary>
        private User GetSelectedUser()
        {
            try
            {
                // الحصول على الصف المحدد في الجدول
                int rowHandle = gridViewUsers.FocusedRowHandle;
                
                if (rowHandle >= 0)
                {
                    int userId = Convert.ToInt32(gridViewUsers.GetRowCellValue(rowHandle, "ID"));
                    return _users.Find(u => u.ID == userId);
                }
                
                return null;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل الحصول على المستخدم المحدد");
                return null;
            }
        }

        /// <summary>
        /// تعديل مستخدم
        /// </summary>
        private void EditUser(int userId)
        {
            try
            {
                // فتح نموذج تعديل المستخدم
                UserForm form = new UserForm(userId);
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل فتح نموذج تعديل المستخدم");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج تعديل المستخدم: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حذف مستخدم
        /// </summary>
        private void DeleteUser(User user)
        {
            try
            {
                // لا يمكن حذف المستخدم الحالي
                if (user.ID == SessionManager.CurrentUser?.ID)
                {
                    XtraMessageBox.Show(
                        "لا يمكن حذف المستخدم الحالي (أنت).",
                        "تنبيه",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
                // تأكيد الحذف
                DialogResult result = XtraMessageBox.Show(
                    $"هل أنت متأكد من حذف المستخدم '{user.Username}'؟",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // حذف المستخدم
                    using (var unitOfWork = new UnitOfWork())
                    {
                        unitOfWork.UserRepository.Delete(user.ID);
                        unitOfWork.Complete();
                    }
                    
                    // إعادة تحميل البيانات
                    LoadUsers();
                    
                    // عرض رسالة نجاح
                    XtraMessageBox.Show(
                        "تم حذف المستخدم بنجاح",
                        "تم الحذف",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حذف المستخدم");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء حذف المستخدم: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}