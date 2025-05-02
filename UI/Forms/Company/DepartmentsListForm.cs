using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using HR.Core;
using HR.Models;

namespace HR.UI.Forms.Company
{
    /// <summary>
    /// نموذج قائمة الأقسام والهيكل التنظيمي
    /// </summary>
    public partial class DepartmentsListForm : XtraForm
    {
        // قائمة الأقسام
        private List<Department> _departments;
        
        /// <summary>
        /// تهيئة نموذج قائمة الأقسام
        /// </summary>
        public DepartmentsListForm()
        {
            InitializeComponent();
            
            // ضبط خصائص النموذج
            this.Text = "الهيكل التنظيمي - الأقسام";
            
            // تسجيل الأحداث
            this.Load += DepartmentsListForm_Load;
            
            // تسجيل أحداث الأزرار
            buttonAdd.Click += ButtonAdd_Click;
            buttonEdit.Click += ButtonEdit_Click;
            buttonDelete.Click += ButtonDelete_Click;
            buttonRefresh.Click += ButtonRefresh_Click;
            
            // تسجيل أحداث الشجرة والجدول
            treeListDepartments.FocusedNodeChanged += TreeListDepartments_FocusedNodeChanged;
            treeListDepartments.DoubleClick += TreeListDepartments_DoubleClick;
            gridViewDepartments.DoubleClick += GridViewDepartments_DoubleClick;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void DepartmentsListForm_Load(object sender, EventArgs e)
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // تحميل البيانات
                LoadDepartments();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل قائمة الأقسام");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل قائمة الأقسام: {ex.Message}",
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
        /// تحميل قائمة الأقسام
        /// </summary>
        private void LoadDepartments()
        {
            try
            {
                // جلب الأقسام من قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    _departments = unitOfWork.DepartmentRepository.GetAllWithParents();
                }
                
                // عرض البيانات في الشجرة
                DisplayDepartmentsInTreeList();
                
                // عرض البيانات في الجدول
                gridControlDepartments.DataSource = _departments;
                
                // إعادة ضبط عرض الأعمدة
                gridViewDepartments.BestFitColumns();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل جلب الأقسام من قاعدة البيانات");
                throw;
            }
        }

        /// <summary>
        /// عرض الأقسام في شجرة الهيكل التنظيمي
        /// </summary>
        private void DisplayDepartmentsInTreeList()
        {
            try
            {
                // إفراغ الشجرة
                treeListDepartments.ClearNodes();
                
                // إيقاف التحديث
                treeListDepartments.BeginUnboundLoad();
                
                // إضافة العقد الجذرية (الأقسام التي ليس لها أب)
                Dictionary<int, TreeListNode> nodesMap = new Dictionary<int, TreeListNode>();
                
                // أولاً: إضافة الأقسام التي ليس لها أب
                foreach (var dept in _departments)
                {
                    if (!dept.ParentID.HasValue)
                    {
                        // إضافة عقدة جديدة
                        TreeListNode node = treeListDepartments.AppendNode(
                            new object[] { dept.ID, dept.Name, dept.Code, dept.IsActive },
                            null);
                        
                        // تخزين مرجع العقدة
                        nodesMap[dept.ID] = node;
                    }
                }
                
                // ثانياً: إضافة الأقسام التي لها أب (بترتيب تسلسلي)
                bool addedAny;
                
                do
                {
                    addedAny = false;
                    
                    foreach (var dept in _departments)
                    {
                        // تخطي الأقسام التي تمت إضافتها بالفعل
                        if (nodesMap.ContainsKey(dept.ID))
                            continue;
                        
                        // التحقق من وجود القسم الأب في الخريطة
                        if (dept.ParentID.HasValue && nodesMap.ContainsKey(dept.ParentID.Value))
                        {
                            // الحصول على عقدة الأب
                            TreeListNode parentNode = nodesMap[dept.ParentID.Value];
                            
                            // إضافة عقدة جديدة
                            TreeListNode node = treeListDepartments.AppendNode(
                                new object[] { dept.ID, dept.Name, dept.Code, dept.IsActive },
                                parentNode);
                            
                            // تخزين مرجع العقدة
                            nodesMap[dept.ID] = node;
                            
                            // تعيين علامة أنه تمت إضافة قسم على الأقل
                            addedAny = true;
                        }
                    }
                }
                while (addedAny); // الاستمرار طالما تمت إضافة أقسام جديدة
                
                // إكمال التحديث
                treeListDepartments.EndUnboundLoad();
                
                // توسيع كافة العقد
                treeListDepartments.ExpandAll();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل عرض الأقسام في شجرة الهيكل التنظيمي");
                throw;
            }
        }

        /// <summary>
        /// حدث النقر على زر إضافة قسم جديد
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // فتح نموذج إضافة قسم جديد
                DepartmentForm form = new DepartmentForm();
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات
                    LoadDepartments();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل فتح نموذج إضافة قسم جديد");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج إضافة قسم جديد: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر تعديل قسم
        /// </summary>
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            // الحصول على القسم المحدد
            Department selectedDepartment = GetSelectedDepartment();
            
            if (selectedDepartment != null)
            {
                EditDepartment(selectedDepartment.ID);
            }
            else
            {
                XtraMessageBox.Show(
                    "الرجاء تحديد قسم أولاً",
                    "تنبيه",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// حدث النقر على زر حذف قسم
        /// </summary>
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            // الحصول على القسم المحدد
            Department selectedDepartment = GetSelectedDepartment();
            
            if (selectedDepartment != null)
            {
                DeleteDepartment(selectedDepartment);
            }
            else
            {
                XtraMessageBox.Show(
                    "الرجاء تحديد قسم أولاً",
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
                LoadDepartments();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث قائمة الأقسام");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحديث قائمة الأقسام: {ex.Message}",
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
        /// حدث تغيير العقدة المحددة في الشجرة
        /// </summary>
        private void TreeListDepartments_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            try
            {
                // الحصول على القسم المحدد
                if (e.Node != null)
                {
                    int departmentId = Convert.ToInt32(e.Node.GetValue("ID"));
                    
                    // البحث عن الصف المقابل في الجدول
                    int rowHandle = gridViewDepartments.LocateByValue("ID", departmentId);
                    
                    if (rowHandle >= 0)
                    {
                        // تحديد الصف في الجدول
                        gridViewDepartments.FocusedRowHandle = rowHandle;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تغيير العقدة المحددة في الشجرة");
            }
        }

        /// <summary>
        /// حدث النقر المزدوج على الشجرة
        /// </summary>
        private void TreeListDepartments_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // الحصول على العقدة المحددة
                TreeListNode node = treeListDepartments.FocusedNode;
                
                if (node != null)
                {
                    int departmentId = Convert.ToInt32(node.GetValue("ID"));
                    
                    // فتح نموذج تعديل القسم
                    EditDepartment(departmentId);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل معالجة النقر المزدوج على الشجرة");
            }
        }

        /// <summary>
        /// حدث النقر المزدوج على الجدول
        /// </summary>
        private void GridViewDepartments_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // الحصول على الصف المحدد
                int rowHandle = gridViewDepartments.FocusedRowHandle;
                
                if (rowHandle >= 0)
                {
                    int departmentId = Convert.ToInt32(gridViewDepartments.GetRowCellValue(rowHandle, "ID"));
                    
                    // فتح نموذج تعديل القسم
                    EditDepartment(departmentId);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل معالجة النقر المزدوج على الجدول");
            }
        }

        /// <summary>
        /// الحصول على القسم المحدد
        /// </summary>
        private Department GetSelectedDepartment()
        {
            try
            {
                // التحقق من نشاط عنصر التحكم
                if (xtraTabControl.SelectedTabPage == xtraTabPageTree)
                {
                    // الحصول على العقدة المحددة في الشجرة
                    TreeListNode node = treeListDepartments.FocusedNode;
                    
                    if (node != null)
                    {
                        int departmentId = Convert.ToInt32(node.GetValue("ID"));
                        return _departments.Find(d => d.ID == departmentId);
                    }
                }
                else if (xtraTabControl.SelectedTabPage == xtraTabPageGrid)
                {
                    // الحصول على الصف المحدد في الجدول
                    int rowHandle = gridViewDepartments.FocusedRowHandle;
                    
                    if (rowHandle >= 0)
                    {
                        int departmentId = Convert.ToInt32(gridViewDepartments.GetRowCellValue(rowHandle, "ID"));
                        return _departments.Find(d => d.ID == departmentId);
                    }
                }
                
                return null;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل الحصول على القسم المحدد");
                return null;
            }
        }

        /// <summary>
        /// تعديل قسم
        /// </summary>
        private void EditDepartment(int departmentId)
        {
            try
            {
                // فتح نموذج تعديل القسم
                DepartmentForm form = new DepartmentForm(departmentId);
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات
                    LoadDepartments();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل فتح نموذج تعديل القسم");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج تعديل القسم: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حذف قسم
        /// </summary>
        private void DeleteDepartment(Department department)
        {
            try
            {
                // التحقق من وجود أقسام فرعية
                bool hasChildren = _departments.Exists(d => d.ParentID.HasValue && d.ParentID.Value == department.ID);
                
                if (hasChildren)
                {
                    XtraMessageBox.Show(
                        "لا يمكن حذف هذا القسم لأنه يحتوي على أقسام فرعية. يجب حذف الأقسام الفرعية أولاً.",
                        "تنبيه",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
                // التحقق من وجود موظفين في هذا القسم
                using (var unitOfWork = new UnitOfWork())
                {
                    if (unitOfWork.EmployeeRepository.Exists(e => e.DepartmentID == department.ID))
                    {
                        XtraMessageBox.Show(
                            "لا يمكن حذف هذا القسم لأنه يحتوي على موظفين. يجب نقل الموظفين إلى قسم آخر أولاً.",
                            "تنبيه",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }
                
                // تأكيد الحذف
                DialogResult result = XtraMessageBox.Show(
                    $"هل أنت متأكد من حذف القسم '{department.Name}'؟",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // حذف القسم
                    using (var unitOfWork = new UnitOfWork())
                    {
                        unitOfWork.DepartmentRepository.Delete(department.ID);
                        unitOfWork.Complete();
                    }
                    
                    // إعادة تحميل البيانات
                    LoadDepartments();
                    
                    // عرض رسالة نجاح
                    XtraMessageBox.Show(
                        "تم حذف القسم بنجاح",
                        "تم الحذف",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حذف القسم");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء حذف القسم: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}