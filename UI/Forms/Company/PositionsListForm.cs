using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;
using HR.Models;

namespace HR.UI.Forms.Company
{
    /// <summary>
    /// نموذج قائمة المسميات الوظيفية
    /// </summary>
    public partial class PositionsListForm : XtraForm
    {
        // قائمة المسميات الوظيفية
        private List<Position> _positions;
        
        /// <summary>
        /// تهيئة نموذج قائمة المسميات الوظيفية
        /// </summary>
        public PositionsListForm()
        {
            InitializeComponent();
            
            // ضبط خصائص النموذج
            this.Text = "إدارة المسميات الوظيفية";
            
            // تسجيل الأحداث
            this.Load += PositionsListForm_Load;
            
            // تسجيل أحداث الأزرار
            buttonAdd.Click += ButtonAdd_Click;
            buttonEdit.Click += ButtonEdit_Click;
            buttonDelete.Click += ButtonDelete_Click;
            buttonRefresh.Click += ButtonRefresh_Click;
            
            // تسجيل أحداث الجدول
            gridViewPositions.DoubleClick += GridViewPositions_DoubleClick;
            
            // تهيئة عناصر الترشيح
            InitializeFilterControls();
        }

        /// <summary>
        /// تهيئة عناصر الترشيح
        /// </summary>
        private void InitializeFilterControls()
        {
            try
            {
                // جلب قائمة الأقسام للترشيح
                using (var unitOfWork = new UnitOfWork())
                {
                    // تحميل الأقسام
                    var departments = unitOfWork.DepartmentRepository.GetAllActive();
                    
                    // إضافة عنصر "جميع الأقسام"
                    departments.Insert(0, new Department { ID = 0, Name = "جميع الأقسام" });
                    
                    // تعيين مصدر البيانات للقائمة المنسدلة
                    lookUpEditFilterDepartment.Properties.DataSource = departments;
                    lookUpEditFilterDepartment.Properties.DisplayMember = "Name";
                    lookUpEditFilterDepartment.Properties.ValueMember = "ID";
                    lookUpEditFilterDepartment.EditValue = 0; // تحديد "جميع الأقسام" افتراضياً
                }
                
                // تسجيل أحداث عناصر الترشيح
                lookUpEditFilterDepartment.EditValueChanged += FilterControl_ValueChanged;
                checkEditShowInactivePositions.CheckedChanged += FilterControl_ValueChanged;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تهيئة عناصر الترشيح");
            }
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void PositionsListForm_Load(object sender, EventArgs e)
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // تحميل البيانات
                LoadPositions();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل قائمة المسميات الوظيفية");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل قائمة المسميات الوظيفية: {ex.Message}",
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
        /// حدث تغيير قيمة عنصر الترشيح
        /// </summary>
        private void FilterControl_ValueChanged(object sender, EventArgs e)
        {
            // إعادة تحميل البيانات بناءً على معايير الترشيح
            LoadPositions();
        }

        /// <summary>
        /// تحميل قائمة المسميات الوظيفية
        /// </summary>
        private void LoadPositions()
        {
            try
            {
                // الحصول على معايير الترشيح
                int departmentId = Convert.ToInt32(lookUpEditFilterDepartment.EditValue);
                bool showInactive = checkEditShowInactivePositions.Checked;
                
                // جلب المسميات الوظيفية من قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    if (departmentId > 0)
                    {
                        // ترشيح حسب القسم
                        _positions = unitOfWork.PositionRepository.GetByDepartment(departmentId, showInactive);
                    }
                    else
                    {
                        // جلب جميع المسميات الوظيفية
                        _positions = unitOfWork.PositionRepository.GetAll(showInactive);
                    }
                }
                
                // عرض البيانات في الجدول
                gridControlPositions.DataSource = _positions;
                
                // إعادة ضبط عرض الأعمدة
                gridViewPositions.BestFitColumns();
                
                // تحديث حالة الأزرار
                UpdateButtonState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل جلب المسميات الوظيفية من قاعدة البيانات");
                throw;
            }
        }

        /// <summary>
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonState()
        {
            // تعطيل أزرار التعديل والحذف إذا لم يكن هناك عنصر محدد
            bool hasSelection = gridViewPositions.GetSelectedRows().Length > 0;
            buttonEdit.Enabled = hasSelection;
            buttonDelete.Enabled = hasSelection;
        }

        /// <summary>
        /// حدث النقر على زر إضافة مسمى وظيفي جديد
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // فتح نموذج إضافة مسمى وظيفي جديد
                PositionForm form = new PositionForm();
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات
                    LoadPositions();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل فتح نموذج إضافة مسمى وظيفي جديد");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج إضافة مسمى وظيفي جديد: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر تعديل مسمى وظيفي
        /// </summary>
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            // الحصول على المسمى الوظيفي المحدد
            Position selectedPosition = GetSelectedPosition();
            
            if (selectedPosition != null)
            {
                EditPosition(selectedPosition.ID);
            }
            else
            {
                XtraMessageBox.Show(
                    "الرجاء تحديد مسمى وظيفي أولاً",
                    "تنبيه",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// حدث النقر على زر حذف مسمى وظيفي
        /// </summary>
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            // الحصول على المسمى الوظيفي المحدد
            Position selectedPosition = GetSelectedPosition();
            
            if (selectedPosition != null)
            {
                DeletePosition(selectedPosition);
            }
            else
            {
                XtraMessageBox.Show(
                    "الرجاء تحديد مسمى وظيفي أولاً",
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
                LoadPositions();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث قائمة المسميات الوظيفية");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحديث قائمة المسميات الوظيفية: {ex.Message}",
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
        private void GridViewPositions_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // الحصول على الصف المحدد
                int rowHandle = gridViewPositions.FocusedRowHandle;
                
                if (rowHandle >= 0)
                {
                    int positionId = Convert.ToInt32(gridViewPositions.GetRowCellValue(rowHandle, "ID"));
                    
                    // فتح نموذج تعديل المسمى الوظيفي
                    EditPosition(positionId);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل معالجة النقر المزدوج على الجدول");
            }
        }

        /// <summary>
        /// الحصول على المسمى الوظيفي المحدد
        /// </summary>
        private Position GetSelectedPosition()
        {
            try
            {
                // الحصول على الصف المحدد في الجدول
                int rowHandle = gridViewPositions.FocusedRowHandle;
                
                if (rowHandle >= 0)
                {
                    int positionId = Convert.ToInt32(gridViewPositions.GetRowCellValue(rowHandle, "ID"));
                    return _positions.Find(p => p.ID == positionId);
                }
                
                return null;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل الحصول على المسمى الوظيفي المحدد");
                return null;
            }
        }

        /// <summary>
        /// تعديل مسمى وظيفي
        /// </summary>
        private void EditPosition(int positionId)
        {
            try
            {
                // فتح نموذج تعديل المسمى الوظيفي
                PositionForm form = new PositionForm(positionId);
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات
                    LoadPositions();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل فتح نموذج تعديل المسمى الوظيفي");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج تعديل المسمى الوظيفي: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حذف مسمى وظيفي
        /// </summary>
        private void DeletePosition(Position position)
        {
            try
            {
                // التحقق من وجود موظفين يستخدمون هذا المسمى
                using (var unitOfWork = new UnitOfWork())
                {
                    if (unitOfWork.EmployeeRepository.Exists(e => e.PositionID == position.ID))
                    {
                        XtraMessageBox.Show(
                            "لا يمكن حذف هذا المسمى الوظيفي لأنه مستخدم من قبل موظفين حاليين. يمكنك تعطيله بدلاً من حذفه.",
                            "تنبيه",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // التحقق من استخدام المسمى الوظيفي كمنصب إداري في أي قسم
                    if (unitOfWork.DepartmentRepository.Exists(d => d.ManagerPositionID == position.ID))
                    {
                        XtraMessageBox.Show(
                            "لا يمكن حذف هذا المسمى الوظيفي لأنه مستخدم كمنصب إداري في أحد الأقسام.",
                            "تنبيه",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }
                
                // تأكيد الحذف
                DialogResult result = XtraMessageBox.Show(
                    $"هل أنت متأكد من حذف المسمى الوظيفي '{position.Title}'؟",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // حذف المسمى الوظيفي
                    using (var unitOfWork = new UnitOfWork())
                    {
                        unitOfWork.PositionRepository.Delete(position.ID);
                        unitOfWork.Complete();
                    }
                    
                    // إعادة تحميل البيانات
                    LoadPositions();
                    
                    // عرض رسالة نجاح
                    XtraMessageBox.Show(
                        "تم حذف المسمى الوظيفي بنجاح",
                        "تم الحذف",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حذف المسمى الوظيفي");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء حذف المسمى الوظيفي: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}