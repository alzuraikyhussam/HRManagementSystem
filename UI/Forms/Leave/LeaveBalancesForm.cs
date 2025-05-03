using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;
using HR.Models.DTOs;

namespace HR.UI.Forms.Leave
{
    /// <summary>
    /// نموذج عرض أرصدة الإجازات
    /// </summary>
    public partial class LeaveBalancesForm : XtraForm
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly int _employeeId; // إذا كان 0 فسيتم عرض جميع الأرصدة
        
        /// <summary>
        /// تهيئة نموذج عرض أرصدة الإجازات (كل الموظفين)
        /// </summary>
        public LeaveBalancesForm()
        {
            InitializeComponent();
            
            // تهيئة وحدة العمل
            _unitOfWork = new UnitOfWork();
            _employeeId = 0;
            
            // تسجيل الأحداث
            this.Load += LeaveBalancesForm_Load;
            buttonAdd.Click += ButtonAdd_Click;
            buttonEdit.Click += ButtonEdit_Click;
            buttonRefresh.Click += ButtonRefresh_Click;
            gridViewLeaveBalances.DoubleClick += GridViewLeaveBalances_DoubleClick;
            gridViewLeaveBalances.SelectionChanged += GridViewLeaveBalances_SelectionChanged;
            comboBoxEmployees.SelectedIndexChanged += ComboBoxEmployees_SelectedIndexChanged;
            comboBoxLeaveTypes.SelectedIndexChanged += ComboBoxLeaveTypes_SelectedIndexChanged;
            comboBoxYear.SelectedIndexChanged += ComboBoxYear_SelectedIndexChanged;
        }
        
        /// <summary>
        /// تهيئة نموذج عرض أرصدة الإجازات لموظف معين
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        public LeaveBalancesForm(int employeeId)
        {
            InitializeComponent();
            
            // تهيئة وحدة العمل
            _unitOfWork = new UnitOfWork();
            _employeeId = employeeId;
            
            // تسجيل الأحداث
            this.Load += LeaveBalancesForm_Load;
            buttonAdd.Click += ButtonAdd_Click;
            buttonEdit.Click += ButtonEdit_Click;
            buttonRefresh.Click += ButtonRefresh_Click;
            gridViewLeaveBalances.DoubleClick += GridViewLeaveBalances_DoubleClick;
            gridViewLeaveBalances.SelectionChanged += GridViewLeaveBalances_SelectionChanged;
            comboBoxEmployees.SelectedIndexChanged += ComboBoxEmployees_SelectedIndexChanged;
            comboBoxLeaveTypes.SelectedIndexChanged += ComboBoxLeaveTypes_SelectedIndexChanged;
            comboBoxYear.SelectedIndexChanged += ComboBoxYear_SelectedIndexChanged;
        }
        
        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void LeaveBalancesForm_Load(object sender, EventArgs e)
        {
            try
            {
                // تعديل العنوان بناءً على طريقة العرض
                if (_employeeId > 0)
                {
                    var employee = _unitOfWork.EmployeeRepository.GetById(_employeeId);
                    if (employee != null)
                    {
                        this.Text = $"أرصدة إجازات الموظف: {employee.FirstName} {employee.LastName}";
                        labelTitle.Text = $"أرصدة إجازات {employee.FirstName} {employee.LastName}";
                    }
                }
                
                // إعداد عناصر التحكم
                InitializeControls();
                
                // تحميل البيانات
                LoadLeaveBalances();
                
                // إعداد عرض الشبكة
                SetupGridView();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تهيئة نموذج عرض أرصدة الإجازات");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تهيئة النموذج: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// إعداد عناصر التحكم
        /// </summary>
        private void InitializeControls()
        {
            // إعداد قائمة السنوات
            comboBoxYear.Properties.Items.Clear();
            
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear - 5; year <= currentYear + 1; year++)
            {
                comboBoxYear.Properties.Items.Add(year);
            }
            
            comboBoxYear.SelectedItem = currentYear;
            
            // إعداد قائمة أنواع الإجازات
            comboBoxLeaveTypes.Properties.Items.Clear();
            comboBoxLeaveTypes.Properties.Items.Add("الكل");
            
            var leaveTypes = _unitOfWork.LeaveRepository.GetAllLeaveTypes();
            foreach (var leaveType in leaveTypes)
            {
                comboBoxLeaveTypes.Properties.Items.Add(leaveType.Name);
            }
            
            comboBoxLeaveTypes.SelectedIndex = 0;
            
            // إعداد قائمة الموظفين
            comboBoxEmployees.Properties.Items.Clear();
            comboBoxEmployees.Properties.Items.Add("الكل");
            
            if (_employeeId == 0)
            {
                var employees = _unitOfWork.EmployeeRepository.GetAllEmployees();
                foreach (var employee in employees)
                {
                    comboBoxEmployees.Properties.Items.Add($"{employee.FirstName} {employee.LastName}");
                }
                
                comboBoxEmployees.SelectedIndex = 0;
            }
            else
            {
                var employee = _unitOfWork.EmployeeRepository.GetById(_employeeId);
                if (employee != null)
                {
                    comboBoxEmployees.Properties.Items.Add($"{employee.FirstName} {employee.LastName}");
                    comboBoxEmployees.SelectedIndex = 1;
                }
                
                // تعطيل تغيير الموظف في حالة التقييد بموظف معين
                comboBoxEmployees.Enabled = false;
            }
        }
        
        /// <summary>
        /// إعداد عرض الشبكة
        /// </summary>
        private void SetupGridView()
        {
            // إعداد خصائص العرض
            gridViewLeaveBalances.OptionsBehavior.Editable = false;
            gridViewLeaveBalances.OptionsView.ShowGroupPanel = false;
            gridViewLeaveBalances.OptionsView.EnableAppearanceEvenRow = true;
            gridViewLeaveBalances.OptionsView.EnableAppearanceOddRow = true;
            
            // إعداد الأعمدة
            gridViewLeaveBalances.Columns["ID"].Caption = "الرقم";
            gridViewLeaveBalances.Columns["ID"].Width = 50;
            gridViewLeaveBalances.Columns["ID"].VisibleIndex = 0;
            
            if (_employeeId == 0)
            {
                gridViewLeaveBalances.Columns["EmployeeName"].Caption = "الموظف";
                gridViewLeaveBalances.Columns["EmployeeName"].Width = 150;
                gridViewLeaveBalances.Columns["EmployeeName"].VisibleIndex = 1;
            }
            else
            {
                // إخفاء عمود الموظف في حالة عرض أرصدة موظف واحد
                if (gridViewLeaveBalances.Columns["EmployeeName"] != null)
                {
                    gridViewLeaveBalances.Columns["EmployeeName"].Visible = false;
                }
            }
            
            gridViewLeaveBalances.Columns["LeaveType"].Caption = "نوع الإجازة";
            gridViewLeaveBalances.Columns["LeaveType"].Width = 120;
            gridViewLeaveBalances.Columns["LeaveType"].VisibleIndex = 2;
            
            gridViewLeaveBalances.Columns["Year"].Caption = "السنة";
            gridViewLeaveBalances.Columns["Year"].Width = 60;
            gridViewLeaveBalances.Columns["Year"].VisibleIndex = 3;
            
            gridViewLeaveBalances.Columns["BaseBalance"].Caption = "الرصيد الأساسي";
            gridViewLeaveBalances.Columns["BaseBalance"].Width = 100;
            gridViewLeaveBalances.Columns["BaseBalance"].VisibleIndex = 4;
            
            gridViewLeaveBalances.Columns["AdditionalBalance"].Caption = "الرصيد الإضافي";
            gridViewLeaveBalances.Columns["AdditionalBalance"].Width = 100;
            gridViewLeaveBalances.Columns["AdditionalBalance"].VisibleIndex = 5;
            
            gridViewLeaveBalances.Columns["UsedBalance"].Caption = "الرصيد المستخدم";
            gridViewLeaveBalances.Columns["UsedBalance"].Width = 100;
            gridViewLeaveBalances.Columns["UsedBalance"].VisibleIndex = 6;
            
            gridViewLeaveBalances.Columns["RemainingBalance"].Caption = "الرصيد المتبقي";
            gridViewLeaveBalances.Columns["RemainingBalance"].Width = 100;
            gridViewLeaveBalances.Columns["RemainingBalance"].VisibleIndex = 7;
            
            // تنسيق الأعمدة الرقمية
            gridViewLeaveBalances.Columns["BaseBalance"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewLeaveBalances.Columns["BaseBalance"].DisplayFormat.FormatString = "n1";
            
            gridViewLeaveBalances.Columns["AdditionalBalance"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewLeaveBalances.Columns["AdditionalBalance"].DisplayFormat.FormatString = "n1";
            
            gridViewLeaveBalances.Columns["UsedBalance"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewLeaveBalances.Columns["UsedBalance"].DisplayFormat.FormatString = "n1";
            
            gridViewLeaveBalances.Columns["RemainingBalance"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewLeaveBalances.Columns["RemainingBalance"].DisplayFormat.FormatString = "n1";
            
            // تنسيق خلايا الرصيد المتبقي بألوان مختلفة حسب القيمة
            gridViewLeaveBalances.RowCellStyle += GridViewLeaveBalances_RowCellStyle;
            
            // تطبيق أفضل عرض للأعمدة
            gridViewLeaveBalances.BestFitColumns();
        }
        
        /// <summary>
        /// تنسيق خلايا الشبكة
        /// </summary>
        private void GridViewLeaveBalances_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "RemainingBalance" && e.RowHandle >= 0)
            {
                decimal remainingBalance = Convert.ToDecimal(e.CellValue);
                
                if (remainingBalance <= 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (remainingBalance < 3)
                {
                    e.Appearance.ForeColor = Color.DarkOrange;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Green;
                }
            }
        }
        
        /// <summary>
        /// حدث تغيير التحديد في الشبكة
        /// </summary>
        private void GridViewLeaveBalances_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            UpdateButtonsState();
        }
        
        /// <summary>
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonsState()
        {
            bool hasSelectedRows = gridViewLeaveBalances.SelectedRowsCount > 0;
            buttonEdit.Enabled = hasSelectedRows;
        }
        
        /// <summary>
        /// تحميل أرصدة الإجازات
        /// </summary>
        private void LoadLeaveBalances()
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // تحديد معايير التصفية
                int employeeId = _employeeId;
                string leaveType = null;
                int year = DateTime.Now.Year;
                
                // التحقق من الموظف المحدد
                if (_employeeId == 0 && comboBoxEmployees.SelectedIndex > 0)
                {
                    var employee = _unitOfWork.EmployeeRepository.GetByFullName(comboBoxEmployees.Text);
                    if (employee != null)
                    {
                        employeeId = employee.ID;
                    }
                }
                
                // التحقق من نوع الإجازة المحدد
                if (comboBoxLeaveTypes.SelectedIndex > 0)
                {
                    leaveType = comboBoxLeaveTypes.Text;
                }
                
                // التحقق من السنة المحددة
                if (comboBoxYear.SelectedItem != null)
                {
                    year = Convert.ToInt32(comboBoxYear.SelectedItem);
                }
                
                // الحصول على بيانات أرصدة الإجازات
                var leaveBalances = _unitOfWork.LeaveRepository.GetLeaveBalances(employeeId, leaveType, year);
                
                // تعيين مصدر البيانات
                gridControlLeaveBalances.DataSource = leaveBalances;
                
                // تحديث حالة الأزرار
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل أرصدة الإجازات");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل أرصدة الإجازات: {ex.Message}",
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
        /// حدث تغيير الموظف المحدد
        /// </summary>
        private void ComboBoxEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLeaveBalances();
        }
        
        /// <summary>
        /// حدث تغيير نوع الإجازة المحدد
        /// </summary>
        private void ComboBoxLeaveTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLeaveBalances();
        }
        
        /// <summary>
        /// حدث تغيير السنة المحددة
        /// </summary>
        private void ComboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLeaveBalances();
        }
        
        /// <summary>
        /// حدث نقر زر الإضافة
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // تحديد الموظف المستهدف للإضافة
                int targetEmployeeId = _employeeId;
                
                if (_employeeId == 0 && comboBoxEmployees.SelectedIndex > 0)
                {
                    var employee = _unitOfWork.EmployeeRepository.GetByFullName(comboBoxEmployees.Text);
                    if (employee != null)
                    {
                        targetEmployeeId = employee.ID;
                    }
                }
                
                // إنشاء نموذج رصيد إجازة جديد
                var leaveBalanceForm = new LeaveBalanceForm(targetEmployeeId);
                
                // عرض النموذج
                if (leaveBalanceForm.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات بعد الإضافة
                    LoadLeaveBalances();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في فتح نموذج إضافة رصيد إجازة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج إضافة رصيد إجازة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// حدث نقر زر التعديل
        /// </summary>
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            EditSelectedLeaveBalance();
        }
        
        /// <summary>
        /// حدث النقر المزدوج على الشبكة
        /// </summary>
        private void GridViewLeaveBalances_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedLeaveBalance();
        }
        
        /// <summary>
        /// تعديل رصيد الإجازة المحدد
        /// </summary>
        private void EditSelectedLeaveBalance()
        {
            if (gridViewLeaveBalances.SelectedRowsCount == 0)
                return;
                
            try
            {
                // الحصول على معرف رصيد الإجازة المحدد
                int leaveBalanceId = Convert.ToInt32(gridViewLeaveBalances.GetFocusedRowCellValue("ID"));
                
                // إنشاء نموذج تعديل رصيد الإجازة
                var leaveBalanceForm = new LeaveBalanceForm(leaveBalanceId);
                
                // عرض النموذج
                if (leaveBalanceForm.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات بعد التعديل
                    LoadLeaveBalances();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في فتح نموذج تعديل رصيد إجازة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج تعديل رصيد إجازة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// حدث نقر زر التحديث
        /// </summary>
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            LoadLeaveBalances();
        }
        
        /// <summary>
        /// التنظيف عند إغلاق النموذج
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            
            // التخلص من الموارد
            _unitOfWork.Dispose();
        }
    }
}