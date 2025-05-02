using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;
using HR.Models;

namespace HR.UI.Forms.Employees
{
    /// <summary>
    /// نموذج قائمة الموظفين
    /// </summary>
    public partial class EmployeesListForm : XtraForm
    {
        // قائمة الموظفين
        private List<EmployeeDTO> _employees;
        
        /// <summary>
        /// تهيئة نموذج قائمة الموظفين
        /// </summary>
        public EmployeesListForm()
        {
            InitializeComponent();
            
            // ضبط خصائص النموذج
            this.Text = "إدارة الموظفين";
            
            // تسجيل الأحداث
            this.Load += EmployeesListForm_Load;
            
            // تسجيل أحداث الأزرار
            buttonAdd.Click += ButtonAdd_Click;
            buttonEdit.Click += ButtonEdit_Click;
            buttonDelete.Click += ButtonDelete_Click;
            buttonRefresh.Click += ButtonRefresh_Click;
            buttonViewDetails.Click += ButtonViewDetails_Click;
            buttonExport.Click += ButtonExport_Click;
            buttonPrint.Click += ButtonPrint_Click;
            
            // تسجيل أحداث الجدول
            gridViewEmployees.DoubleClick += GridViewEmployees_DoubleClick;
            gridViewEmployees.FocusedRowChanged += GridViewEmployees_FocusedRowChanged;
            
            // تسجيل أحداث التصفية والبحث
            comboBoxEditDepartment.SelectedIndexChanged += Filter_Changed;
            comboBoxEditPosition.SelectedIndexChanged += Filter_Changed;
            comboBoxEditStatus.SelectedIndexChanged += Filter_Changed;
            
            // إعداد قيم حالة الموظف
            comboBoxEditStatus.Properties.Items.AddRange(new string[] { 
                "الكل", 
                "نشط", 
                "تحت التجربة", 
                "إجازة", 
                "موقوف", 
                "منتهي الخدمة" 
            });
            comboBoxEditStatus.SelectedIndex = 0;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void EmployeesListForm_Load(object sender, EventArgs e)
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // تحميل الأقسام
                LoadDepartments();
                
                // تحميل المسميات الوظيفية
                LoadPositions();
                
                // تحميل البيانات
                LoadEmployees();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل قائمة الموظفين");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل قائمة الموظفين: {ex.Message}",
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
        /// تحميل بيانات الأقسام
        /// </summary>
        private void LoadDepartments()
        {
            try
            {
                // جلب الأقسام من قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    var departments = unitOfWork.DepartmentRepository.GetAll();
                    
                    // إضافة خيار "الكل"
                    comboBoxEditDepartment.Properties.Items.Add("الكل");
                    
                    // إضافة الأقسام
                    foreach (var department in departments)
                    {
                        comboBoxEditDepartment.Properties.Items.Add(department.Name);
                    }
                    
                    // تحديد الخيار الافتراضي
                    comboBoxEditDepartment.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل الأقسام");
                throw;
            }
        }

        /// <summary>
        /// تحميل بيانات المسميات الوظيفية
        /// </summary>
        private void LoadPositions()
        {
            try
            {
                // جلب المسميات الوظيفية من قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    var positions = unitOfWork.PositionRepository.GetAll();
                    
                    // إضافة خيار "الكل"
                    comboBoxEditPosition.Properties.Items.Add("الكل");
                    
                    // إضافة المسميات الوظيفية
                    foreach (var position in positions)
                    {
                        comboBoxEditPosition.Properties.Items.Add(position.Title);
                    }
                    
                    // تحديد الخيار الافتراضي
                    comboBoxEditPosition.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل المسميات الوظيفية");
                throw;
            }
        }

        /// <summary>
        /// تحميل قائمة الموظفين
        /// </summary>
        private void LoadEmployees()
        {
            try
            {
                // جلب الموظفين من قاعدة البيانات
                using (var unitOfWork = new UnitOfWork())
                {
                    _employees = unitOfWork.EmployeeRepository.GetEmployeesWithDetails();
                }
                
                // تطبيق الفلترة
                ApplyFilters();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل جلب الموظفين من قاعدة البيانات");
                throw;
            }
        }

        /// <summary>
        /// تطبيق الفلترة على البيانات
        /// </summary>
        private void ApplyFilters()
        {
            try
            {
                // نسخة من الموظفين للتصفية
                List<EmployeeDTO> filteredEmployees = new List<EmployeeDTO>(_employees);
                
                // تصفية حسب القسم
                if (comboBoxEditDepartment.SelectedIndex > 0)
                {
                    string departmentName = comboBoxEditDepartment.Text;
                    filteredEmployees = filteredEmployees.FindAll(e => e.DepartmentName == departmentName);
                }
                
                // تصفية حسب المسمى الوظيفي
                if (comboBoxEditPosition.SelectedIndex > 0)
                {
                    string positionTitle = comboBoxEditPosition.Text;
                    filteredEmployees = filteredEmployees.FindAll(e => e.PositionTitle == positionTitle);
                }
                
                // تصفية حسب الحالة
                if (comboBoxEditStatus.SelectedIndex > 0)
                {
                    string status = comboBoxEditStatus.Text;
                    filteredEmployees = filteredEmployees.FindAll(e => e.Status == status);
                }
                
                // عرض البيانات في الجدول
                gridControlEmployees.DataSource = filteredEmployees;
                
                // إعادة ضبط عرض الأعمدة
                gridViewEmployees.BestFitColumns();
                
                // تحديث حالة الأزرار
                UpdateButtonState();
                
                // تحديث عدد الموظفين
                labelItemCount.Text = $"إجمالي عدد الموظفين: {filteredEmployees.Count}";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تطبيق الفلترة على البيانات");
                throw;
            }
        }

        /// <summary>
        /// حدث تغيير قيم الفلترة
        /// </summary>
        private void Filter_Changed(object sender, EventArgs e)
        {
            try
            {
                // تطبيق الفلترة
                ApplyFilters();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تطبيق الفلترة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تطبيق الفلترة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonState()
        {
            // تعطيل أزرار التعديل والحذف والتفاصيل إذا لم يكن هناك عنصر محدد
            bool hasSelection = gridViewEmployees.GetSelectedRows().Length > 0;
            buttonEdit.Enabled = hasSelection;
            buttonDelete.Enabled = hasSelection;
            buttonViewDetails.Enabled = hasSelection;
        }

        /// <summary>
        /// حدث تغيير الصف المحدد في الجدول
        /// </summary>
        private void GridViewEmployees_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            UpdateButtonState();
        }

        /// <summary>
        /// حدث النقر على زر إضافة موظف جديد
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من صلاحية المستخدم
                if (!SessionManager.HasPermission("إدارة الموظفين", "add"))
                {
                    XtraMessageBox.Show(
                        "ليس لديك صلاحية لإضافة موظف جديد",
                        "تنبيه",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
                // فتح نموذج إضافة موظف جديد
                EmployeeForm form = new EmployeeForm();
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات
                    LoadEmployees();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل فتح نموذج إضافة موظف جديد");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج إضافة موظف جديد: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر تعديل موظف
        /// </summary>
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            // الحصول على الموظف المحدد
            EmployeeDTO selectedEmployee = GetSelectedEmployee();
            
            if (selectedEmployee != null)
            {
                // التحقق من صلاحية المستخدم
                if (!SessionManager.HasPermission("إدارة الموظفين", "edit"))
                {
                    XtraMessageBox.Show(
                        "ليس لديك صلاحية لتعديل بيانات الموظفين",
                        "تنبيه",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
                EditEmployee(selectedEmployee.ID);
            }
            else
            {
                XtraMessageBox.Show(
                    "الرجاء تحديد موظف أولاً",
                    "تنبيه",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// حدث النقر على زر حذف موظف
        /// </summary>
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            // الحصول على الموظف المحدد
            EmployeeDTO selectedEmployee = GetSelectedEmployee();
            
            if (selectedEmployee != null)
            {
                // التحقق من صلاحية المستخدم
                if (!SessionManager.HasPermission("إدارة الموظفين", "delete"))
                {
                    XtraMessageBox.Show(
                        "ليس لديك صلاحية لحذف الموظفين",
                        "تنبيه",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
                DeleteEmployee(selectedEmployee);
            }
            else
            {
                XtraMessageBox.Show(
                    "الرجاء تحديد موظف أولاً",
                    "تنبيه",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// حدث النقر على زر عرض تفاصيل الموظف
        /// </summary>
        private void ButtonViewDetails_Click(object sender, EventArgs e)
        {
            // الحصول على الموظف المحدد
            EmployeeDTO selectedEmployee = GetSelectedEmployee();
            
            if (selectedEmployee != null)
            {
                // التحقق من صلاحية المستخدم
                if (!SessionManager.HasPermission("إدارة الموظفين", "view"))
                {
                    XtraMessageBox.Show(
                        "ليس لديك صلاحية لعرض تفاصيل الموظفين",
                        "تنبيه",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
                // فتح نموذج تفاصيل الموظف
                EmployeeDetailsForm form = new EmployeeDetailsForm(selectedEmployee.ID);
                form.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show(
                    "الرجاء تحديد موظف أولاً",
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
                LoadEmployees();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث قائمة الموظفين");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحديث قائمة الموظفين: {ex.Message}",
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
        /// حدث النقر على زر تصدير البيانات
        /// </summary>
        private void ButtonExport_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من صلاحية المستخدم
                if (!SessionManager.HasPermission("إدارة الموظفين", "export"))
                {
                    XtraMessageBox.Show(
                        "ليس لديك صلاحية لتصدير بيانات الموظفين",
                        "تنبيه",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
                // عرض خيارات التصدير
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv|PDF Files (*.pdf)|*.pdf";
                saveFileDialog.DefaultExt = "xlsx";
                saveFileDialog.FileName = "employees_list_" + DateTime.Now.ToString("yyyyMMdd");
                
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // تصدير البيانات حسب نوع الملف
                    string extension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();
                    
                    switch (extension)
                    {
                        case ".xlsx":
                            gridControlEmployees.ExportToXlsx(saveFileDialog.FileName);
                            break;
                        case ".csv":
                            gridControlEmployees.ExportToCsv(saveFileDialog.FileName);
                            break;
                        case ".pdf":
                            gridControlEmployees.ExportToPdf(saveFileDialog.FileName);
                            break;
                    }
                    
                    // عرض رسالة نجاح
                    XtraMessageBox.Show(
                        "تم تصدير البيانات بنجاح",
                        "تم التصدير",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تصدير بيانات الموظفين");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تصدير بيانات الموظفين: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر على زر طباعة البيانات
        /// </summary>
        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من صلاحية المستخدم
                if (!SessionManager.HasPermission("إدارة الموظفين", "print"))
                {
                    XtraMessageBox.Show(
                        "ليس لديك صلاحية لطباعة بيانات الموظفين",
                        "تنبيه",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
                // طباعة البيانات
                gridControlEmployees.ShowPrintPreview();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل طباعة بيانات الموظفين");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء طباعة بيانات الموظفين: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث النقر المزدوج على الجدول
        /// </summary>
        private void GridViewEmployees_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // الحصول على الصف المحدد
                int rowHandle = gridViewEmployees.FocusedRowHandle;
                
                if (rowHandle >= 0)
                {
                    int employeeId = Convert.ToInt32(gridViewEmployees.GetRowCellValue(rowHandle, "ID"));
                    
                    // التحقق من صلاحية المستخدم
                    if (!SessionManager.HasPermission("إدارة الموظفين", "view"))
                    {
                        XtraMessageBox.Show(
                            "ليس لديك صلاحية لعرض تفاصيل الموظفين",
                            "تنبيه",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // فتح نموذج تفاصيل الموظف
                    EmployeeDetailsForm form = new EmployeeDetailsForm(employeeId);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل معالجة النقر المزدوج على الجدول");
            }
        }

        /// <summary>
        /// الحصول على الموظف المحدد
        /// </summary>
        private EmployeeDTO GetSelectedEmployee()
        {
            try
            {
                // الحصول على الصف المحدد في الجدول
                int rowHandle = gridViewEmployees.FocusedRowHandle;
                
                if (rowHandle >= 0)
                {
                    int employeeId = Convert.ToInt32(gridViewEmployees.GetRowCellValue(rowHandle, "ID"));
                    return _employees.Find(e => e.ID == employeeId);
                }
                
                return null;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل الحصول على الموظف المحدد");
                return null;
            }
        }

        /// <summary>
        /// تعديل موظف
        /// </summary>
        private void EditEmployee(int employeeId)
        {
            try
            {
                // فتح نموذج تعديل الموظف
                EmployeeForm form = new EmployeeForm(employeeId);
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات
                    LoadEmployees();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل فتح نموذج تعديل الموظف");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج تعديل الموظف: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حذف موظف
        /// </summary>
        private void DeleteEmployee(EmployeeDTO employee)
        {
            try
            {
                // التحقق من وجود مستخدم مرتبط بهذا الموظف
                using (var unitOfWork = new UnitOfWork())
                {
                    if (unitOfWork.UserRepository.Exists(u => u.EmployeeID == employee.ID))
                    {
                        XtraMessageBox.Show(
                            "لا يمكن حذف هذا الموظف لأنه مرتبط بحساب مستخدم. يجب حذف حساب المستخدم أولاً.",
                            "تنبيه",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }
                
                // تأكيد الحذف
                DialogResult result = XtraMessageBox.Show(
                    $"هل أنت متأكد من حذف الموظف '{employee.FullName}'؟ سيتم حذف جميع البيانات المرتبطة به.",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // حذف الموظف
                    using (var unitOfWork = new UnitOfWork())
                    {
                        unitOfWork.EmployeeRepository.Delete(employee.ID);
                        unitOfWork.Complete();
                    }
                    
                    // إعادة تحميل البيانات
                    LoadEmployees();
                    
                    // عرض رسالة نجاح
                    XtraMessageBox.Show(
                        "تم حذف الموظف بنجاح",
                        "تم الحذف",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل حذف الموظف");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء حذف الموظف: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}