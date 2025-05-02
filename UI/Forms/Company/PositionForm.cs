using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.Models;

namespace HR.UI.Forms.Company
{
    /// <summary>
    /// نموذج إدارة المسميات الوظيفية
    /// </summary>
    public partial class PositionForm : XtraForm
    {
        // كائن المسمى الوظيفي الحالي
        private Position _position;
        
        // قائمة الأقسام المتاحة
        private List<Department> _departments;
        
        // تحديد ما إذا كان هناك تغييرات
        private bool _hasChanges = false;
        
        // تحديد ما إذا كانت عملية إضافة جديدة
        private bool _isNewPosition = false;
        
        /// <summary>
        /// تهيئة نموذج إدارة المسمى الوظيفي
        /// </summary>
        public PositionForm()
        {
            InitializeComponent();
            
            // إنشاء مسمى وظيفي جديد
            _position = new Position();
            _isNewPosition = true;
            
            // ضبط خصائص النموذج
            this.Text = "إضافة مسمى وظيفي جديد";
            
            // تهيئة عناصر التحكم
            InitializeControls();
            
            // تسجيل الأحداث
            this.Load += PositionForm_Load;
        }
        
        /// <summary>
        /// تهيئة نموذج إدارة المسمى الوظيفي (تعديل مسمى موجود)
        /// </summary>
        public PositionForm(int positionId)
        {
            InitializeComponent();
            
            // جلب المسمى الوظيفي من قاعدة البيانات
            using (var unitOfWork = new UnitOfWork())
            {
                _position = unitOfWork.PositionRepository.GetById(positionId);
                
                if (_position == null)
                {
                    // إذا لم يتم العثور على المسمى الوظيفي، إنشاء مسمى جديد
                    _position = new Position();
                    _isNewPosition = true;
                    this.Text = "إضافة مسمى وظيفي جديد";
                }
                else
                {
                    _isNewPosition = false;
                    this.Text = "تعديل مسمى وظيفي";
                }
            }
            
            // تهيئة عناصر التحكم
            InitializeControls();
            
            // تسجيل الأحداث
            this.Load += PositionForm_Load;
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
                LogManager.LogException(ex, "فشل تهيئة عناصر التحكم في نموذج إدارة المسمى الوظيفي");
            }
        }

        /// <summary>
        /// تسجيل أحداث التغيير للحقول
        /// </summary>
        private void RegisterChangeEvents()
        {
            textEditTitle.EditValueChanged += Control_ValueChanged;
            lookUpEditDepartment.EditValueChanged += Control_ValueChanged;
            spinEditGradeLevel.EditValueChanged += Control_ValueChanged;
            spinEditMinSalary.EditValueChanged += Control_ValueChanged;
            spinEditMaxSalary.EditValueChanged += Control_ValueChanged;
            checkEditIsManagerPosition.CheckedChanged += Control_ValueChanged;
            checkEditIsActive.CheckedChanged += Control_ValueChanged;
            memoEditDescription.EditValueChanged += Control_ValueChanged;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void PositionForm_Load(object sender, EventArgs e)
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // جلب الأقسام المتاحة
                LoadDepartments();
                
                // عرض بيانات المسمى الوظيفي
                DisplayPositionData();
                
                // تحديث حالة الأزرار
                UpdateButtonState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل بيانات المسمى الوظيفي");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل بيانات المسمى الوظيفي: {ex.Message}",
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
        /// جلب الأقسام المتاحة
        /// </summary>
        private void LoadDepartments()
        {
            try
            {
                // جلب جميع الأقسام النشطة
                using (var unitOfWork = new UnitOfWork())
                {
                    _departments = unitOfWork.DepartmentRepository.GetAllActive();
                    
                    // تعيين مصدر البيانات للقائمة المنسدلة
                    lookUpEditDepartment.Properties.DataSource = _departments;
                    lookUpEditDepartment.Properties.DisplayMember = "Name";
                    lookUpEditDepartment.Properties.ValueMember = "ID";
                    
                    // إضافة خيار "لا يوجد" للقسم
                    lookUpEditDepartment.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
                    lookUpEditDepartment.Properties.NullText = "لا يوجد";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل جلب الأقسام المتاحة");
                throw;
            }
        }

        /// <summary>
        /// عرض بيانات المسمى الوظيفي في النموذج
        /// </summary>
        private void DisplayPositionData()
        {
            if (_position == null)
                return;
            
            // عرض البيانات في الحقول
            textEditTitle.Text = _position.Title;
            lookUpEditDepartment.EditValue = _position.DepartmentID;
            spinEditGradeLevel.Value = _position.GradeLevel.HasValue ? _position.GradeLevel.Value : 0;
            spinEditMinSalary.Value = _position.MinSalary.HasValue ? (decimal)_position.MinSalary.Value : 0;
            spinEditMaxSalary.Value = _position.MaxSalary.HasValue ? (decimal)_position.MaxSalary.Value : 0;
            checkEditIsManagerPosition.Checked = _position.IsManagerPosition;
            checkEditIsActive.Checked = _position.IsActive;
            memoEditDescription.Text = _position.Description;
            
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
            buttonSave.Enabled = _hasChanges && !string.IsNullOrWhiteSpace(textEditTitle.Text);
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
                
                // تحديث كائن المسمى الوظيفي
                UpdatePositionObject();
                
                // حفظ البيانات
                SavePositionData();
                
                // عرض رسالة النجاح
                XtraMessageBox.Show(
                    _isNewPosition ? "تم إضافة المسمى الوظيفي بنجاح" : "تم تعديل المسمى الوظيفي بنجاح",
                    "تم الحفظ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                // إغلاق النموذج
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حفظ بيانات المسمى الوظيفي");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء حفظ بيانات المسمى الوظيفي: {ex.Message}",
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
            // التحقق من عنوان المسمى الوظيفي
            if (string.IsNullOrWhiteSpace(textEditTitle.Text))
            {
                XtraMessageBox.Show(
                    "الرجاء إدخال عنوان المسمى الوظيفي",
                    "خطأ في البيانات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                textEditTitle.Focus();
                return false;
            }
            
            // التحقق من عدم وجود مسمى وظيفي بنفس العنوان في نفس القسم
            using (var unitOfWork = new UnitOfWork())
            {
                int? departmentId = null;
                if (lookUpEditDepartment.EditValue != null)
                {
                    departmentId = Convert.ToInt32(lookUpEditDepartment.EditValue);
                }
                
                if (_isNewPosition)
                {
                    // في حالة الإضافة
                    if (unitOfWork.PositionRepository.Exists(p => 
                        p.Title == textEditTitle.Text && 
                        (p.DepartmentID == departmentId || (p.DepartmentID == null && departmentId == null))))
                    {
                        XtraMessageBox.Show(
                            "يوجد مسمى وظيفي آخر بنفس العنوان في نفس القسم. الرجاء اختيار عنوان مختلف",
                            "خطأ في البيانات",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        textEditTitle.Focus();
                        return false;
                    }
                }
                else
                {
                    // في حالة التعديل
                    if (unitOfWork.PositionRepository.Exists(p => 
                        p.Title == textEditTitle.Text && 
                        (p.DepartmentID == departmentId || (p.DepartmentID == null && departmentId == null)) && 
                        p.ID != _position.ID))
                    {
                        XtraMessageBox.Show(
                            "يوجد مسمى وظيفي آخر بنفس العنوان في نفس القسم. الرجاء اختيار عنوان مختلف",
                            "خطأ في البيانات",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        textEditTitle.Focus();
                        return false;
                    }
                }
            }
            
            // التحقق من صحة نطاق الراتب
            if (spinEditMinSalary.Value > 0 && spinEditMaxSalary.Value > 0 && spinEditMinSalary.Value > spinEditMaxSalary.Value)
            {
                XtraMessageBox.Show(
                    "الحد الأدنى للراتب يجب أن يكون أقل من أو يساوي الحد الأقصى",
                    "خطأ في البيانات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                spinEditMinSalary.Focus();
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// تحديث كائن المسمى الوظيفي
        /// </summary>
        private void UpdatePositionObject()
        {
            if (_position == null)
                _position = new Position();
            
            // تحديث البيانات من الحقول
            _position.Title = textEditTitle.Text;
            _position.Description = memoEditDescription.Text;
            
            // تحديث القسم
            if (lookUpEditDepartment.EditValue != null)
            {
                _position.DepartmentID = Convert.ToInt32(lookUpEditDepartment.EditValue);
            }
            else
            {
                _position.DepartmentID = null;
            }
            
            // تحديث مستوى الدرجة
            _position.GradeLevel = spinEditGradeLevel.Value > 0 ? (int)spinEditGradeLevel.Value : (int?)null;
            
            // تحديث نطاق الراتب
            _position.MinSalary = spinEditMinSalary.Value > 0 ? (double)spinEditMinSalary.Value : (double?)null;
            _position.MaxSalary = spinEditMaxSalary.Value > 0 ? (double)spinEditMaxSalary.Value : (double?)null;
            
            // تحديث الخصائص الأخرى
            _position.IsManagerPosition = checkEditIsManagerPosition.Checked;
            _position.IsActive = checkEditIsActive.Checked;
            
            // تحديث تواريخ الإنشاء والتعديل
            if (_isNewPosition)
            {
                _position.CreatedAt = DateTime.Now;
            }
            
            _position.UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// حفظ بيانات المسمى الوظيفي
        /// </summary>
        private void SavePositionData()
        {
            try
            {
                // حفظ بيانات المسمى الوظيفي في قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    if (_isNewPosition)
                    {
                        // إضافة مسمى وظيفي جديد
                        unitOfWork.PositionRepository.Add(_position);
                    }
                    else
                    {
                        // تحديث مسمى وظيفي موجود
                        unitOfWork.PositionRepository.Update(_position);
                    }
                    
                    unitOfWork.Complete();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حفظ بيانات المسمى الوظيفي في قاعدة البيانات");
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