using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.Models;

namespace HR.UI.Forms.Company
{
    /// <summary>
    /// نموذج إدارة القسم
    /// </summary>
    public partial class DepartmentForm : XtraForm
    {
        // كائن القسم الحالي
        private Department _department;
        
        // قائمة الأقسام المتاحة للأب
        private List<Department> _parentDepartments;
        
        // قائمة المناصب المتاحة لمدير القسم
        private List<Position> _managerPositions;
        
        // تحديد ما إذا كان هناك تغييرات
        private bool _hasChanges = false;
        
        // تحديد ما إذا كانت عملية إضافة جديدة
        private bool _isNewDepartment = false;
        
        /// <summary>
        /// تهيئة نموذج إدارة القسم
        /// </summary>
        public DepartmentForm()
        {
            InitializeComponent();
            
            // إنشاء قسم جديد
            _department = new Department();
            _isNewDepartment = true;
            
            // ضبط خصائص النموذج
            this.Text = "إضافة قسم جديد";
            
            // تهيئة عناصر التحكم
            InitializeControls();
            
            // تسجيل الأحداث
            this.Load += DepartmentForm_Load;
        }
        
        /// <summary>
        /// تهيئة نموذج إدارة القسم (تعديل قسم موجود)
        /// </summary>
        public DepartmentForm(int departmentId)
        {
            InitializeComponent();
            
            // جلب القسم من قاعدة البيانات
            using (var unitOfWork = new UnitOfWork())
            {
                _department = unitOfWork.DepartmentRepository.GetById(departmentId);
                
                if (_department == null)
                {
                    // إذا لم يتم العثور على القسم، إنشاء قسم جديد
                    _department = new Department();
                    _isNewDepartment = true;
                    this.Text = "إضافة قسم جديد";
                }
                else
                {
                    _isNewDepartment = false;
                    this.Text = "تعديل قسم";
                }
            }
            
            // تهيئة عناصر التحكم
            InitializeControls();
            
            // تسجيل الأحداث
            this.Load += DepartmentForm_Load;
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
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تهيئة عناصر التحكم في نموذج إدارة القسم");
            }
        }

        /// <summary>
        /// تسجيل أحداث التغيير للحقول
        /// </summary>
        private void RegisterChangeEvents()
        {
            textEditName.EditValueChanged += Control_ValueChanged;
            lookUpEditParentDepartment.EditValueChanged += Control_ValueChanged;
            lookUpEditManagerPosition.EditValueChanged += Control_ValueChanged;
            textEditLocation.EditValueChanged += Control_ValueChanged;
            checkEditIsActive.CheckedChanged += Control_ValueChanged;
            memoEditDescription.EditValueChanged += Control_ValueChanged;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void DepartmentForm_Load(object sender, EventArgs e)
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // جلب الأقسام المتاحة للأب
                LoadParentDepartments();
                
                // جلب المناصب المتاحة لمدير القسم
                LoadManagerPositions();
                
                // عرض بيانات القسم
                DisplayDepartmentData();
                
                // تحديث حالة الأزرار
                UpdateButtonState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل بيانات القسم");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل بيانات القسم: {ex.Message}",
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
        /// جلب الأقسام المتاحة للأب
        /// </summary>
        private void LoadParentDepartments()
        {
            try
            {
                // جلب جميع الأقسام
                using (var unitOfWork = new UnitOfWork())
                {
                    _parentDepartments = unitOfWork.DepartmentRepository.GetAll();
                    
                    // إذا كان تعديل لقسم موجود، استبعاد القسم الحالي وأبنائه من قائمة الآباء المحتملين
                    if (!_isNewDepartment)
                    {
                        // استبعاد القسم الحالي
                        _parentDepartments.RemoveAll(d => d.ID == _department.ID);
                        
                        // استبعاد الأقسام التي تكون أبناء للقسم الحالي (لمنع الدورات)
                        RemoveChildDepartments(_department.ID);
                    }
                    
                    // تعيين مصدر البيانات للقائمة المنسدلة
                    lookUpEditParentDepartment.Properties.DataSource = _parentDepartments;
                    lookUpEditParentDepartment.Properties.DisplayMember = "Name";
                    lookUpEditParentDepartment.Properties.ValueMember = "ID";
                    
                    // إضافة خيار "لا يوجد" للقسم الأب
                    lookUpEditParentDepartment.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
                    lookUpEditParentDepartment.Properties.NullText = "لا يوجد";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل جلب الأقسام المتاحة للأب");
                throw;
            }
        }

        /// <summary>
        /// جلب المناصب المتاحة لمدير القسم
        /// </summary>
        private void LoadManagerPositions()
        {
            try
            {
                // جلب جميع المناصب الإدارية
                using (var unitOfWork = new UnitOfWork())
                {
                    _managerPositions = unitOfWork.PositionRepository.GetManagerPositions();
                    
                    // تعيين مصدر البيانات للقائمة المنسدلة
                    lookUpEditManagerPosition.Properties.DataSource = _managerPositions;
                    lookUpEditManagerPosition.Properties.DisplayMember = "Title";
                    lookUpEditManagerPosition.Properties.ValueMember = "ID";
                    
                    // إضافة خيار "لا يوجد" لمنصب المدير
                    lookUpEditManagerPosition.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
                    lookUpEditManagerPosition.Properties.NullText = "لا يوجد";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل جلب المناصب المتاحة لمدير القسم");
                throw;
            }
        }

        /// <summary>
        /// استبعاد الأقسام التي تكون أبناء للقسم الحالي
        /// </summary>
        private void RemoveChildDepartments(int parentId)
        {
            // البحث عن جميع الأقسام التي لها القسم الحالي كأب
            List<Department> children = _parentDepartments.FindAll(d => d.ParentID.HasValue && d.ParentID.Value == parentId);
            
            foreach (var child in children)
            {
                // استبعاد القسم الابن
                _parentDepartments.Remove(child);
                
                // استبعاد أبناء القسم الابن (بشكل متكرر)
                RemoveChildDepartments(child.ID);
            }
        }

        /// <summary>
        /// عرض بيانات القسم في النموذج
        /// </summary>
        private void DisplayDepartmentData()
        {
            if (_department == null)
                return;
            
            // عرض البيانات في الحقول
            textEditName.Text = _department.Name;
            lookUpEditParentDepartment.EditValue = _department.ParentID;
            lookUpEditManagerPosition.EditValue = _department.ManagerPositionID;
            textEditLocation.Text = _department.Location;
            checkEditIsActive.Checked = _department.IsActive;
            memoEditDescription.Text = _department.Description;
            
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
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonState()
        {
            buttonSave.Enabled = _hasChanges && !string.IsNullOrWhiteSpace(textEditName.Text);
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
                
                // تحديث كائن القسم
                UpdateDepartmentObject();
                
                // حفظ البيانات
                SaveDepartmentData();
                
                // عرض رسالة النجاح
                XtraMessageBox.Show(
                    _isNewDepartment ? "تم إضافة القسم بنجاح" : "تم تعديل القسم بنجاح",
                    "تم الحفظ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                // إغلاق النموذج
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حفظ بيانات القسم");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء حفظ بيانات القسم: {ex.Message}",
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
            // التحقق من اسم القسم
            if (string.IsNullOrWhiteSpace(textEditName.Text))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال اسم القسم",
                    "خطأ في البيانات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditName.Focus();
                return false;
            }
            
            // التحقق من عدم وجود قسم بنفس الاسم
            using (var unitOfWork = new UnitOfWork())
            {
                if (_isNewDepartment)
                {
                    // في حالة الإضافة
                    if (unitOfWork.DepartmentRepository.Exists(d => d.Name == textEditName.Text))
                    {
                        XtraMessageBox.Show(
                            "يوجد قسم آخر بنفس الاسم. الرجاء اختيار اسم مختلف",
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
                    if (unitOfWork.DepartmentRepository.Exists(d => d.Name == textEditName.Text && d.ID != _department.ID))
                    {
                        XtraMessageBox.Show(
                            "يوجد قسم آخر بنفس الاسم. الرجاء اختيار اسم مختلف",
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
        /// تحديث كائن القسم
        /// </summary>
        private void UpdateDepartmentObject()
        {
            if (_department == null)
                _department = new Department();
            
            // تحديث البيانات من الحقول
            _department.Name = textEditName.Text;
            _department.Description = memoEditDescription.Text;
            _department.Location = textEditLocation.Text;
            _department.IsActive = checkEditIsActive.Checked;
            
            // تحديث القسم الأب
            if (lookUpEditParentDepartment.EditValue != null)
            {
                _department.ParentID = Convert.ToInt32(lookUpEditParentDepartment.EditValue);
            }
            else
            {
                _department.ParentID = null;
            }
            
            // تحديث منصب المدير
            if (lookUpEditManagerPosition.EditValue != null)
            {
                _department.ManagerPositionID = Convert.ToInt32(lookUpEditManagerPosition.EditValue);
            }
            else
            {
                _department.ManagerPositionID = null;
            }
            
            // تحديث تواريخ الإنشاء والتعديل
            if (_isNewDepartment)
            {
                _department.CreatedAt = DateTime.Now;
            }
            
            _department.UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// حفظ بيانات القسم
        /// </summary>
        private void SaveDepartmentData()
        {
            try
            {
                // حفظ بيانات القسم في قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    if (_isNewDepartment)
                    {
                        // إضافة قسم جديد
                        unitOfWork.DepartmentRepository.Add(_department);
                    }
                    else
                    {
                        // تحديث قسم موجود
                        unitOfWork.DepartmentRepository.Update(_department);
                    }
                    
                    unitOfWork.Complete();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حفظ بيانات القسم في قاعدة البيانات");
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