using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.Models;

namespace HR.UI.Forms.Security
{
    /// <summary>
    /// نموذج إدارة الأدوار والصلاحيات
    /// </summary>
    public partial class RoleForm : XtraForm
    {
        // كائن الدور الحالي
        private Role _role;
        
        // قائمة صلاحيات الدور
        private List<RolePermission> _permissions;
        
        // تحديد ما إذا كان هناك تغييرات
        private bool _hasChanges = false;
        
        // تحديد ما إذا كانت عملية إضافة جديدة
        private bool _isNewRole = false;
        
        // قائمة بوحدات النظام (موديولات) المتاحة للصلاحيات
        private List<string> _systemModules = new List<string>
        {
            "الشركة والهيكل التنظيمي",
            "المستخدمين والصلاحيات",
            "إدارة الموظفين",
            "الحضور والانصراف",
            "الإجازات",
            "الرواتب",
            "التقارير",
            "إعدادات النظام"
        };
        
        /// <summary>
        /// تهيئة نموذج إدارة الدور
        /// </summary>
        public RoleForm()
        {
            InitializeComponent();
            
            // إنشاء دور جديد
            _role = new Role();
            _permissions = new List<RolePermission>();
            _isNewRole = true;
            
            // ضبط خصائص النموذج
            this.Text = "إضافة دور جديد";
            
            // تهيئة عناصر التحكم
            InitializeControls();
            
            // تسجيل الأحداث
            this.Load += RoleForm_Load;
        }
        
        /// <summary>
        /// تهيئة نموذج إدارة الدور (تعديل دور موجود)
        /// </summary>
        public RoleForm(int roleId)
        {
            InitializeComponent();
            
            // جلب الدور من قاعدة البيانات
            using (var unitOfWork = new UnitOfWork())
            {
                _role = unitOfWork.RoleRepository.GetById(roleId);
                
                if (_role == null)
                {
                    // إذا لم يتم العثور على الدور، إنشاء دور جديد
                    _role = new Role();
                    _permissions = new List<RolePermission>();
                    _isNewRole = true;
                    this.Text = "إضافة دور جديد";
                }
                else
                {
                    // جلب صلاحيات الدور
                    _permissions = unitOfWork.RoleRepository.GetRolePermissions(roleId);
                    _isNewRole = false;
                    this.Text = $"تعديل دور: {_role.Name}";
                }
            }
            
            // تهيئة عناصر التحكم
            InitializeControls();
            
            // تسجيل الأحداث
            this.Load += RoleForm_Load;
        }

        /// <summary>
        /// تهيئة عناصر التحكم
        /// </summary>
        private void InitializeControls()
        {
            try
            {
                // تسجيل أحداث التغيير للحقول
                RegisterChangeEvents();
                
                // تسجيل أحداث الأزرار
                buttonSave.Click += ButtonSave_Click;
                buttonCancel.Click += ButtonCancel_Click;
                buttonSelectAll.Click += ButtonSelectAll_Click;
                buttonDeselectAll.Click += ButtonDeselectAll_Click;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تهيئة عناصر التحكم في نموذج إدارة الدور");
            }
        }

        /// <summary>
        /// تسجيل أحداث التغيير للحقول
        /// </summary>
        private void RegisterChangeEvents()
        {
            textEditName.EditValueChanged += Control_ValueChanged;
            memoEditDescription.EditValueChanged += Control_ValueChanged;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void RoleForm_Load(object sender, EventArgs e)
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // تهيئة جدول الصلاحيات
                InitializePermissionsGrid();
                
                // عرض بيانات الدور
                DisplayRoleData();
                
                // تحديث حالة الأزرار
                UpdateButtonState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل بيانات الدور");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل بيانات الدور: {ex.Message}",
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
        /// تهيئة جدول الصلاحيات
        /// </summary>
        private void InitializePermissionsGrid()
        {
            try
            {
                // إنشاء جدول البيانات للصلاحيات
                System.Data.DataTable permissionsTable = new System.Data.DataTable();
                permissionsTable.Columns.Add("ModuleName", typeof(string));
                permissionsTable.Columns.Add("CanView", typeof(bool));
                permissionsTable.Columns.Add("CanAdd", typeof(bool));
                permissionsTable.Columns.Add("CanEdit", typeof(bool));
                permissionsTable.Columns.Add("CanDelete", typeof(bool));
                permissionsTable.Columns.Add("CanPrint", typeof(bool));
                permissionsTable.Columns.Add("CanExport", typeof(bool));
                permissionsTable.Columns.Add("CanImport", typeof(bool));
                permissionsTable.Columns.Add("CanApprove", typeof(bool));
                
                // إضافة بيانات وحدات النظام
                foreach (string moduleName in _systemModules)
                {
                    // البحث عن صلاحيات الوحدة الحالية
                    RolePermission modulePermission = _permissions.Find(p => p.ModuleName == moduleName);
                    
                    if (modulePermission != null)
                    {
                        // إضافة صف بالصلاحيات الموجودة
                        permissionsTable.Rows.Add(
                            moduleName,
                            modulePermission.CanView,
                            modulePermission.CanAdd,
                            modulePermission.CanEdit,
                            modulePermission.CanDelete,
                            modulePermission.CanPrint,
                            modulePermission.CanExport,
                            modulePermission.CanImport,
                            modulePermission.CanApprove
                        );
                    }
                    else
                    {
                        // إضافة صف بصلاحيات افتراضية (غير مفعلة)
                        permissionsTable.Rows.Add(
                            moduleName,
                            false, false, false, false, false, false, false, false
                        );
                    }
                }
                
                // تعيين مصدر البيانات للجدول
                gridControlPermissions.DataSource = permissionsTable;
                
                // تسجيل حدث تغيير قيمة خلية
                gridViewPermissions.CellValueChanged += GridViewPermissions_CellValueChanged;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تهيئة جدول الصلاحيات");
                throw;
            }
        }

        /// <summary>
        /// عرض بيانات الدور في النموذج
        /// </summary>
        private void DisplayRoleData()
        {
            if (_role == null)
                return;
            
            // عرض البيانات في الحقول
            textEditName.Text = _role.Name;
            memoEditDescription.Text = _role.Description;
            
            // إعادة تعيين حالة التغييرات
            _hasChanges = false;
        }

        /// <summary>
        /// حدث تغيير قيمة أي عنصر تحكم
        /// </summary>
        private void Control_ValueChanged(object sender, EventArgs e)
        {
            _hasChanges = true;
            UpdateButtonState();
        }

        /// <summary>
        /// حدث تغيير قيمة خلية في جدول الصلاحيات
        /// </summary>
        private void GridViewPermissions_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            _hasChanges = true;
            UpdateButtonState();
            
            // إذا كانت الخلية المتغيرة هي "عرض" وتم إلغاء تفعيلها
            if (e.Column.FieldName == "CanView" && !(bool)e.Value)
            {
                // إلغاء تفعيل جميع الصلاحيات الأخرى للصف
                gridViewPermissions.SetRowCellValue(e.RowHandle, "CanAdd", false);
                gridViewPermissions.SetRowCellValue(e.RowHandle, "CanEdit", false);
                gridViewPermissions.SetRowCellValue(e.RowHandle, "CanDelete", false);
                gridViewPermissions.SetRowCellValue(e.RowHandle, "CanPrint", false);
                gridViewPermissions.SetRowCellValue(e.RowHandle, "CanExport", false);
                gridViewPermissions.SetRowCellValue(e.RowHandle, "CanImport", false);
                gridViewPermissions.SetRowCellValue(e.RowHandle, "CanApprove", false);
            }
            
            // إذا كانت الخلية المتغيرة ليست "عرض" وتم تفعيلها
            if (e.Column.FieldName != "CanView" && (bool)e.Value)
            {
                // تفعيل صلاحية "عرض" للصف إذا كانت غير مفعلة
                bool canView = (bool)gridViewPermissions.GetRowCellValue(e.RowHandle, "CanView");
                if (!canView)
                {
                    gridViewPermissions.SetRowCellValue(e.RowHandle, "CanView", true);
                }
            }
        }

        /// <summary>
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonState()
        {
            buttonSave.Enabled = _hasChanges && !string.IsNullOrWhiteSpace(textEditName.Text);
        }

        /// <summary>
        /// حدث النقر على زر تحديد الكل
        /// </summary>
        private void ButtonSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                // تحديد كل الصلاحيات لجميع الصفوف
                for (int i = 0; i < gridViewPermissions.RowCount; i++)
                {
                    gridViewPermissions.SetRowCellValue(i, "CanView", true);
                    gridViewPermissions.SetRowCellValue(i, "CanAdd", true);
                    gridViewPermissions.SetRowCellValue(i, "CanEdit", true);
                    gridViewPermissions.SetRowCellValue(i, "CanDelete", true);
                    gridViewPermissions.SetRowCellValue(i, "CanPrint", true);
                    gridViewPermissions.SetRowCellValue(i, "CanExport", true);
                    gridViewPermissions.SetRowCellValue(i, "CanImport", true);
                    gridViewPermissions.SetRowCellValue(i, "CanApprove", true);
                }
                
                _hasChanges = true;
                UpdateButtonState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديد كل الصلاحيات");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحديد كل الصلاحيات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر إلغاء تحديد الكل
        /// </summary>
        private void ButtonDeselectAll_Click(object sender, EventArgs e)
        {
            try
            {
                // إلغاء تحديد كل الصلاحيات لجميع الصفوف
                for (int i = 0; i < gridViewPermissions.RowCount; i++)
                {
                    gridViewPermissions.SetRowCellValue(i, "CanView", false);
                    gridViewPermissions.SetRowCellValue(i, "CanAdd", false);
                    gridViewPermissions.SetRowCellValue(i, "CanEdit", false);
                    gridViewPermissions.SetRowCellValue(i, "CanDelete", false);
                    gridViewPermissions.SetRowCellValue(i, "CanPrint", false);
                    gridViewPermissions.SetRowCellValue(i, "CanExport", false);
                    gridViewPermissions.SetRowCellValue(i, "CanImport", false);
                    gridViewPermissions.SetRowCellValue(i, "CanApprove", false);
                }
                
                _hasChanges = true;
                UpdateButtonState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل إلغاء تحديد كل الصلاحيات");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء إلغاء تحديد كل الصلاحيات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر الحفظ
        /// </summary>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من صحة البيانات
                if (!ValidateData())
                    return;
                
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // تحديث كائن الدور
                UpdateRoleObject();
                
                // تحديث قائمة الصلاحيات
                UpdatePermissionsList();
                
                // حفظ البيانات
                SaveRoleData();
                
                // عرض رسالة النجاح
                XtraMessageBox.Show(
                    _isNewRole ? "تم إضافة الدور بنجاح" : "تم تعديل الدور بنجاح",
                    "تم الحفظ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                // إغلاق النموذج
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حفظ بيانات الدور");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء حفظ بيانات الدور: {ex.Message}",
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
        /// التحقق من صحة البيانات
        /// </summary>
        private bool ValidateData()
        {
            // التحقق من اسم الدور
            if (string.IsNullOrWhiteSpace(textEditName.Text))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال اسم الدور",
                    "خطأ في البيانات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditName.Focus();
                return false;
            }
            
            // التحقق من عدم وجود دور بنفس الاسم
            using (var unitOfWork = new UnitOfWork())
            {
                if (_isNewRole)
                {
                    // في حالة الإضافة
                    if (unitOfWork.RoleRepository.Exists(r => r.Name == textEditName.Text))
                    {
                        XtraMessageBox.Show(
                            "يوجد دور آخر بنفس الاسم. الرجاء اختيار اسم مختلف",
                            "خطأ في البيانات",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        textEditName.Focus();
                        return false;
                    }
                }
                else
                {
                    // في حالة التعديل
                    if (unitOfWork.RoleRepository.Exists(r => r.Name == textEditName.Text && r.ID != _role.ID))
                    {
                        XtraMessageBox.Show(
                            "يوجد دور آخر بنفس الاسم. الرجاء اختيار اسم مختلف",
                            "خطأ في البيانات",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        textEditName.Focus();
                        return false;
                    }
                }
            }
            
            return true;
        }

        /// <summary>
        /// تحديث كائن الدور
        /// </summary>
        private void UpdateRoleObject()
        {
            if (_role == null)
                _role = new Role();
            
            // تحديث البيانات من الحقول
            _role.Name = textEditName.Text;
            _role.Description = memoEditDescription.Text;
            
            // تحديث تواريخ الإنشاء والتعديل
            if (_isNewRole)
            {
                _role.CreatedAt = DateTime.Now;
            }
            
            _role.UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// تحديث قائمة الصلاحيات
        /// </summary>
        private void UpdatePermissionsList()
        {
            try
            {
                // إفراغ قائمة الصلاحيات
                _permissions.Clear();
                
                // جمع البيانات من جدول الصلاحيات
                System.Data.DataTable permissionsTable = gridControlPermissions.DataSource as System.Data.DataTable;
                if (permissionsTable != null)
                {
                    foreach (System.Data.DataRow row in permissionsTable.Rows)
                    {
                        string moduleName = row["ModuleName"].ToString();
                        bool canView = Convert.ToBoolean(row["CanView"]);
                        bool canAdd = Convert.ToBoolean(row["CanAdd"]);
                        bool canEdit = Convert.ToBoolean(row["CanEdit"]);
                        bool canDelete = Convert.ToBoolean(row["CanDelete"]);
                        bool canPrint = Convert.ToBoolean(row["CanPrint"]);
                        bool canExport = Convert.ToBoolean(row["CanExport"]);
                        bool canImport = Convert.ToBoolean(row["CanImport"]);
                        bool canApprove = Convert.ToBoolean(row["CanApprove"]);
                        
                        // إنشاء كائن صلاحية جديد
                        RolePermission permission = new RolePermission
                        {
                            ModuleName = moduleName,
                            CanView = canView,
                            CanAdd = canAdd,
                            CanEdit = canEdit,
                            CanDelete = canDelete,
                            CanPrint = canPrint,
                            CanExport = canExport,
                            CanImport = canImport,
                            CanApprove = canApprove
                        };
                        
                        // إضافة الصلاحية إلى القائمة
                        _permissions.Add(permission);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث قائمة الصلاحيات");
                throw;
            }
        }

        /// <summary>
        /// حفظ بيانات الدور والصلاحيات
        /// </summary>
        private void SaveRoleData()
        {
            try
            {
                // حفظ بيانات الدور والصلاحيات في قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    if (_isNewRole)
                    {
                        // إضافة دور جديد
                        unitOfWork.RoleRepository.Add(_role);
                        unitOfWork.Complete();
                        
                        // تحديث معرّف الدور في الصلاحيات
                        foreach (var permission in _permissions)
                        {
                            permission.RoleID = _role.ID;
                        }
                        
                        // إضافة الصلاحيات
                        unitOfWork.RoleRepository.AddPermissions(_permissions);
                    }
                    else
                    {
                        // تحديث دور موجود
                        unitOfWork.RoleRepository.Update(_role);
                        
                        // حذف الصلاحيات القديمة
                        unitOfWork.RoleRepository.DeletePermissions(_role.ID);
                        
                        // تحديث معرّف الدور في الصلاحيات
                        foreach (var permission in _permissions)
                        {
                            permission.RoleID = _role.ID;
                        }
                        
                        // إضافة الصلاحيات الجديدة
                        unitOfWork.RoleRepository.AddPermissions(_permissions);
                    }
                    
                    unitOfWork.Complete();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حفظ بيانات الدور والصلاحيات في قاعدة البيانات");
                throw;
            }
        }

        /// <summary>
        /// حدث النقر على زر الإلغاء
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود تغييرات
                if (_hasChanges)
                {
                    DialogResult result = XtraMessageBox.Show(
                        "هل تريد تجاهل التغييرات؟",
                        "تأكيد",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    
                    if (result == DialogResult.No)
                        return;
                }
                
                // إغلاق النموذج
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل إلغاء التغييرات");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء إلغاء التغييرات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}