using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using HR.Core;

namespace HR.UI.Forms.Reports
{
    /// <summary>
    /// نموذج تقرير الرواتب
    /// </summary>
    public partial class PayrollReportForm : XtraForm
    {
        private readonly ConnectionManager _connectionManager;
        
        public PayrollReportForm()
        {
            InitializeComponent();
            
            _connectionManager = new ConnectionManager();
            
            // تحميل البيانات الأولية
            InitializeData();
        }
        
        /// <summary>
        /// تهيئة البيانات الأولية للنموذج
        /// </summary>
        private void InitializeData()
        {
            try
            {
                // تعيين القيم الافتراضية
                dtStartDate.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-3);
                dtEndDate.DateTime = DateTime.Now;
                
                // تحميل قائمة الإدارات
                LoadDepartments();
                
                // تحميل قائمة الموظفين
                LoadEmployees();
                
                // تحميل قائمة كشوف الرواتب
                LoadPayrollSheets();
                
                // ربط الأحداث
                cboDepartment.EditValueChanged += cboDepartment_EditValueChanged;
                cboEmployee.EditValueChanged += filter_ValueChanged;
                cboPayrollSheet.EditValueChanged += filter_ValueChanged;
                dtStartDate.EditValueChanged += filter_ValueChanged;
                dtEndDate.EditValueChanged += filter_ValueChanged;
                chkShowDetails.CheckedChanged += filter_ValueChanged;
                
                // تهيئة GridView
                InitializeGridView();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تهيئة البيانات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تحميل قائمة الإدارات
        /// </summary>
        private void LoadDepartments()
        {
            try
            {
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT ID, Name FROM Departments ORDER BY Name";
                    
                    using (var adapter = new System.Data.SqlClient.SqlDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        // إضافة خيار "كل الإدارات"
                        DataRow allRow = dt.NewRow();
                        allRow["ID"] = 0;
                        allRow["Name"] = "كل الإدارات";
                        dt.Rows.InsertAt(allRow, 0);
                        
                        cboDepartment.Properties.DataSource = dt;
                        cboDepartment.Properties.DisplayMember = "Name";
                        cboDepartment.Properties.ValueMember = "ID";
                        cboDepartment.Properties.PopulateColumns();
                        cboDepartment.Properties.Columns["ID"].Visible = false;
                        cboDepartment.EditValue = 0; // تعيين "كل الإدارات" كخيار افتراضي
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تحميل قائمة الإدارات: {ex.Message}");
            }
        }
        
        /// <summary>
        /// تحميل قائمة الموظفين
        /// </summary>
        private void LoadEmployees(int departmentId = 0)
        {
            try
            {
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT ID, FullName FROM Employees";
                    
                    if (departmentId > 0)
                    {
                        query += " WHERE DepartmentID = @DepartmentId";
                    }
                    
                    query += " ORDER BY FullName";
                    
                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        if (departmentId > 0)
                        {
                            command.Parameters.AddWithValue("@DepartmentId", departmentId);
                        }
                        
                        DataTable dt = new DataTable();
                        var adapter = new System.Data.SqlClient.SqlDataAdapter(command);
                        adapter.Fill(dt);
                        
                        // إضافة خيار "كل الموظفين"
                        DataRow allRow = dt.NewRow();
                        allRow["ID"] = 0;
                        allRow["FullName"] = "كل الموظفين";
                        dt.Rows.InsertAt(allRow, 0);
                        
                        cboEmployee.Properties.DataSource = dt;
                        cboEmployee.Properties.DisplayMember = "FullName";
                        cboEmployee.Properties.ValueMember = "ID";
                        cboEmployee.Properties.PopulateColumns();
                        cboEmployee.Properties.Columns["ID"].Visible = false;
                        cboEmployee.EditValue = 0; // تعيين "كل الموظفين" كخيار افتراضي
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تحميل قائمة الموظفين: {ex.Message}");
            }
        }
        
        /// <summary>
        /// تحميل قائمة كشوف الرواتب
        /// </summary>
        private void LoadPayrollSheets()
        {
            try
            {
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT 
                            ID, 
                            CONCAT(Month, '/', Year, ' - ', Description) AS PayrollName,
                            Year,
                            Month,
                            StartDate,
                            EndDate,
                            Description
                        FROM PayrollSheets 
                        ORDER BY Year DESC, Month DESC";
                    
                    using (var adapter = new System.Data.SqlClient.SqlDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        // إضافة خيار "كل كشوف الرواتب"
                        DataRow allRow = dt.NewRow();
                        allRow["ID"] = 0;
                        allRow["PayrollName"] = "كل كشوف الرواتب";
                        dt.Rows.InsertAt(allRow, 0);
                        
                        cboPayrollSheet.Properties.DataSource = dt;
                        cboPayrollSheet.Properties.DisplayMember = "PayrollName";
                        cboPayrollSheet.Properties.ValueMember = "ID";
                        cboPayrollSheet.Properties.PopulateColumns();
                        cboPayrollSheet.Properties.Columns["ID"].Visible = false;
                        cboPayrollSheet.Properties.Columns["Year"].Visible = false;
                        cboPayrollSheet.Properties.Columns["Month"].Visible = false;
                        cboPayrollSheet.Properties.Columns["StartDate"].Visible = false;
                        cboPayrollSheet.Properties.Columns["EndDate"].Visible = false;
                        cboPayrollSheet.Properties.Columns["Description"].Visible = false;
                        cboPayrollSheet.EditValue = 0; // تعيين "كل كشوف الرواتب" كخيار افتراضي
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تحميل قائمة كشوف الرواتب: {ex.Message}");
            }
        }
        
        /// <summary>
        /// تهيئة إعدادات جدول البيانات المتقدمة
        /// </summary>
        private void InitializeGridView()
        {
            try
            {
                // الحصول على GridView
                GridView view = gridControl.MainView as GridView;
                
                // تهيئة خيارات العرض الأساسية
                view.OptionsView.ShowGroupPanel = true; // تفعيل إمكانية التجميع
                view.OptionsView.ColumnAutoWidth = false;
                view.OptionsView.ShowIndicator = true;
                view.OptionsView.ShowFooter = true; // عرض الإجماليات في الأسفل
                view.OptionsView.ShowAutoFilterRow = true; // تفعيل الفلترة التلقائية
                view.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways; // إظهار شريط الفلترة المتقدمة
                
                // خيارات الطباعة
                view.OptionsPrint.AutoWidth = false;
                view.OptionsPrint.PrintHeader = true;
                view.OptionsPrint.PrintFooter = true;
                view.OptionsPrint.PrintFilterInfo = true;
                view.OptionsPrint.PrintDetails = true;
                view.OptionsPrint.AllowMultilineHeaders = true;
                view.OptionsPrint.RtfPageHeader = "تقرير الرواتب";
                view.OptionsPrint.RtfPageFooter = "صفحة [Page] من [Pages]";
                
                // خيارات التفاعل
                view.OptionsBehavior.Editable = false;
                view.OptionsBehavior.ReadOnly = true;
                view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
                view.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
                view.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True; // تثبيت مجموعات البيانات
                view.OptionsBehavior.AutoExpandAllGroups = true; // توسيع كل المجموعات تلقائيًا
                
                // خيارات الفلاتر
                view.OptionsFilter.AllowFilterEditor = true; // إمكانية تحرير الفلاتر
                view.OptionsFilter.UseNewCustomFilterDialog = true; // استخدام نافذة الفلاتر الجديدة
                view.OptionsFilter.ShowAllTableValuesInFilterPopup = true; // عرض كل القيم في قائمة الفلتر
                view.OptionsFilter.DefaultFilterEditorView = DevExpress.XtraEditors.FilterEditorViewMode.Visual; // عرض محرر الفلتر في الوضع المرئي
                
                // خيارات البحث
                view.OptionsFind.AlwaysVisible = true; // إظهار شريط البحث دائمًا
                view.OptionsFind.SearchInPreview = true; // البحث في المعاينة
                view.OptionsFind.AllowFindPanel = true; // السماح باستخدام لوحة البحث
                view.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always; // وضع البحث دائمًا
                view.OptionsFind.FindNullPrompt = "ادخل نص للبحث ..."; // نص إرشادي لمربع البحث
                
                // تعديل مظهر الجدول
                // تنسيق الصفوف المختلفة
                view.OptionsView.EnableAppearanceEvenRow = true;
                view.OptionsView.EnableAppearanceOddRow = true;
                
                // ألوان خلفية الصفوف الزوجية والفردية
                view.Appearance.EvenRow.BackColor = Color.FromArgb(242, 246, 251); // لون أزرق فاتح جدًا للصفوف الزوجية
                view.Appearance.OddRow.BackColor = Color.White; // أبيض للصفوف الفردية
                
                // مؤثرات الصفوف الزوجية والفردية
                view.Appearance.EvenRow.BorderColor = Color.FromArgb(230, 236, 246);
                view.Appearance.EvenRow.Options.UseBorderColor = true;
                view.Appearance.OddRow.BorderColor = Color.FromArgb(235, 235, 235);
                view.Appearance.OddRow.Options.UseBorderColor = true;
                
                // تنسيق خط ومحاذاة الخلايا
                view.Appearance.Row.Font = new Font("Tahoma", 9F);
                view.Appearance.Row.Options.UseFont = true;
                view.Appearance.Row.Options.UseTextOptions = true;
                view.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                
                // تنسيق رأس الجدول
                view.Appearance.HeaderPanel.Font = new Font("Tahoma", 10F, FontStyle.Bold);
                view.Appearance.HeaderPanel.Options.UseFont = true;
                view.Appearance.HeaderPanel.Options.UseTextOptions = true;
                view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Appearance.HeaderPanel.BackColor = Color.FromArgb(52, 152, 219); // لون أزرق غامق للعناوين
                view.Appearance.HeaderPanel.ForeColor = Color.White; // لون الخط أبيض
                view.Appearance.HeaderPanel.Options.UseBackColor = true;
                view.Appearance.HeaderPanel.Options.UseForeColor = true;
                
                // تنسيق أسفل الجدول (الإجماليات)
                view.Appearance.FooterPanel.Font = new Font("Tahoma", 9F, FontStyle.Bold);
                view.Appearance.FooterPanel.Options.UseFont = true;
                view.Appearance.FooterPanel.Options.UseTextOptions = true;
                view.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Appearance.FooterPanel.BackColor = Color.FromArgb(240, 240, 240);
                view.Appearance.FooterPanel.Options.UseBackColor = true;
                
                // تنسيق صف الفلترة
                view.Appearance.FilterPanel.Font = new Font("Tahoma", 9F);
                view.Appearance.FilterPanel.Options.UseFont = true;
                view.Appearance.FilterPanel.BackColor = Color.FromArgb(245, 245, 245);
                view.Appearance.FilterPanel.Options.UseBackColor = true;
                
                // تنسيق خلفية الخلية المحددة
                view.Appearance.FocusedRow.BackColor = Color.FromArgb(213, 233, 242); // لون أزرق فاتح للصف المحدد
                view.Appearance.FocusedRow.Options.UseBackColor = true;
                
                // تنسيق الخلية المحددة
                view.Appearance.FocusedCell.BackColor = Color.FromArgb(200, 230, 255); // لون أزرق للخلية المحددة
                view.Appearance.FocusedCell.Options.UseBackColor = true;
                
                // تعيين أنماط العناصر الأخرى
                view.Appearance.SelectedRow.BackColor = Color.FromArgb(213, 233, 242);
                view.Appearance.SelectedRow.Options.UseBackColor = true;
                view.Appearance.HideSelectionRow.BackColor = Color.FromArgb(220, 240, 252);
                view.Appearance.HideSelectionRow.Options.UseBackColor = true;
                
                // تحميل البيانات الافتراضية
                LoadReportData();
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تهيئة جدول البيانات: {ex.Message}");
            }
        }
        
        /// <summary>
        /// تحميل بيانات التقرير
        /// </summary>
        private void LoadReportData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // إنشاء معايير التقرير
                var filter = CreateFilterOptions();
                
                // الحصول على البيانات
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    // بناء استعلام التقرير
                    string queryType = chkShowDetails.Checked ? "Details" : "Summary";
                    string query = "";
                    
                    if (queryType == "Summary")
                    {
                        // استعلام ملخص الرواتب
                        query = BuildSummaryQuery(filter);
                    }
                    else
                    {
                        // استعلام تفاصيل الرواتب
                        query = BuildDetailsQuery(filter);
                    }
                    
                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        // إضافة البارامترات
                        AddFilterParameters(command, filter);
                        
                        // تنفيذ الاستعلام
                        DataTable resultTable = new DataTable();
                        var adapter = new System.Data.SqlClient.SqlDataAdapter(command);
                        adapter.Fill(resultTable);
                        
                        // تعيين البيانات للجدول
                        gridControl.DataSource = resultTable;
                        
                        // تنسيق متقدم للأعمدة
                        GridView view = gridControl.MainView as GridView;
                        
                        if (queryType == "Summary")
                        {
                            // تنسيق أعمدة ملخص الرواتب
                            ConfigureSummaryColumns(view);
                            
                            // حساب إجماليات الرواتب
                            decimal totalBasicSalary = resultTable.AsEnumerable().Sum(r => r.Field<decimal>("BasicSalary"));
                            decimal totalAllowances = resultTable.AsEnumerable().Sum(r => r.Field<decimal>("TotalAllowances"));
                            decimal totalDeductions = resultTable.AsEnumerable().Sum(r => r.Field<decimal>("TotalDeductions"));
                            decimal totalNetSalary = resultTable.AsEnumerable().Sum(r => r.Field<decimal>("NetSalary"));
                            
                            lblStatistics.Text = $"إجمالي: {resultTable.Rows.Count} موظف | إجمالي الرواتب الأساسية: {totalBasicSalary:n2} | إجمالي البدلات: {totalAllowances:n2} | إجمالي الاستقطاعات: {totalDeductions:n2} | صافي الرواتب: {totalNetSalary:n2}";
                        }
                        else
                        {
                            // تنسيق أعمدة تفاصيل الرواتب
                            ConfigureDetailsColumns(view);
                            
                            // حساب إجماليات تفاصيل الرواتب
                            decimal totalAmount = resultTable.AsEnumerable().Sum(r => r.Field<decimal>("Amount"));
                            int allowancesCount = resultTable.AsEnumerable().Count(r => r.Field<int>("IsAddition") == 1);
                            int deductionsCount = resultTable.AsEnumerable().Count(r => r.Field<int>("IsAddition") == 0);
                            
                            lblStatistics.Text = $"إجمالي العناصر: {resultTable.Rows.Count} | البدلات: {allowancesCount} | الاستقطاعات: {deductionsCount} | إجمالي المبالغ: {totalAmount:n2}";
                        }
                        
                        // تحديث عنوان التقرير بعدد النتائج
                        lblReportTitle.Text = $"تقرير الرواتب - {(queryType == "Summary" ? "ملخص" : "تفاصيل")} - عدد النتائج: {resultTable.Rows.Count}";
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل بيانات التقرير: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// بناء استعلام ملخص الرواتب
        /// </summary>
        private string BuildSummaryQuery(ReportFilterOptions filter)
        {
            string whereClause = BuildWhereClause(filter);
            
            return $@"
                SELECT 
                    PS.ID AS PayrollSheetID,
                    CONCAT(PS.Month, '/', PS.Year, ' - ', PS.Description) AS PayrollSheetName,
                    PE.ID AS PayrollEntryID,
                    PS.Year,
                    PS.Month,
                    CONVERT(varchar(10), PS.StartDate, 103) AS StartDateFormatted,
                    CONVERT(varchar(10), PS.EndDate, 103) AS EndDateFormatted,
                    E.ID AS EmployeeID,
                    E.EmployeeNumber,
                    E.FullName,
                    D.Name AS DepartmentName,
                    P.Title AS PositionTitle,
                    E.BasicSalary,
                    PE.TotalAllowances,
                    PE.TotalDeductions,
                    PE.NetSalary,
                    PE.PaymentDate,
                    CONVERT(varchar(10), PE.PaymentDate, 103) AS PaymentDateFormatted,
                    PE.PaymentMethod,
                    CASE PE.PaymentMethod 
                        WHEN 1 THEN 'تحويل بنكي'
                        WHEN 2 THEN 'شيك'
                        WHEN 3 THEN 'نقدي'
                        ELSE 'غير محدد'
                    END AS PaymentMethodText,
                    PE.PaymentReference,
                    PE.Notes,
                    PE.Status,
                    CASE PE.Status 
                        WHEN 1 THEN 'مدفوع'
                        WHEN 0 THEN 'غير مدفوع'
                        ELSE 'غير محدد'
                    END AS StatusText
                FROM PayrollEntries PE
                INNER JOIN PayrollSheets PS ON PE.PayrollSheetID = PS.ID
                INNER JOIN Employees E ON PE.EmployeeID = E.ID
                INNER JOIN Departments D ON E.DepartmentID = D.ID
                LEFT JOIN Positions P ON E.PositionID = P.ID
                {whereClause}
                ORDER BY PS.Year DESC, PS.Month DESC, D.Name, E.FullName";
        }
        
        /// <summary>
        /// بناء استعلام تفاصيل الرواتب
        /// </summary>
        private string BuildDetailsQuery(ReportFilterOptions filter)
        {
            string whereClause = BuildWhereClause(filter);
            
            return $@"
                SELECT 
                    PS.ID AS PayrollSheetID,
                    CONCAT(PS.Month, '/', PS.Year, ' - ', PS.Description) AS PayrollSheetName,
                    PE.ID AS PayrollEntryID,
                    PS.Year,
                    PS.Month,
                    CONVERT(varchar(10), PS.StartDate, 103) AS StartDateFormatted,
                    CONVERT(varchar(10), PS.EndDate, 103) AS EndDateFormatted,
                    E.ID AS EmployeeID,
                    E.EmployeeNumber,
                    E.FullName,
                    D.Name AS DepartmentName,
                    P.Title AS PositionTitle,
                    PEI.ID AS PayrollEntryItemID,
                    PEI.Description AS ItemDescription,
                    PEI.Amount,
                    PEI.IsAddition,
                    CASE PEI.IsAddition 
                        WHEN 1 THEN 'بدل'
                        WHEN 0 THEN 'استقطاع'
                        ELSE 'غير محدد'
                    END AS ItemType,
                    PEI.Category,
                    PEI.Reference,
                    PEI.Notes,
                    PE.PaymentDate,
                    CONVERT(varchar(10), PE.PaymentDate, 103) AS PaymentDateFormatted,
                    PE.PaymentMethod,
                    CASE PE.PaymentMethod 
                        WHEN 1 THEN 'تحويل بنكي'
                        WHEN 2 THEN 'شيك'
                        WHEN 3 THEN 'نقدي'
                        ELSE 'غير محدد'
                    END AS PaymentMethodText,
                    PE.Status,
                    CASE PE.Status 
                        WHEN 1 THEN 'مدفوع'
                        WHEN 0 THEN 'غير مدفوع'
                        ELSE 'غير محدد'
                    END AS StatusText
                FROM PayrollEntryItems PEI
                INNER JOIN PayrollEntries PE ON PEI.PayrollEntryID = PE.ID
                INNER JOIN PayrollSheets PS ON PE.PayrollSheetID = PS.ID
                INNER JOIN Employees E ON PE.EmployeeID = E.ID
                INNER JOIN Departments D ON E.DepartmentID = D.ID
                LEFT JOIN Positions P ON E.PositionID = P.ID
                {whereClause}
                ORDER BY PS.Year DESC, PS.Month DESC, D.Name, E.FullName, PEI.IsAddition DESC, PEI.Category, PEI.Description";
        }
        
        /// <summary>
        /// تنسيق أعمدة ملخص الرواتب
        /// </summary>
        private void ConfigureSummaryColumns(GridView view)
        {
            // إخفاء الأعمدة غير المطلوبة
            HideColumn(view, "PayrollSheetID");
            HideColumn(view, "PayrollEntryID");
            HideColumn(view, "EmployeeID");
            HideColumn(view, "PaymentDate");
            HideColumn(view, "Status");
            
            // تكوين الأعمدة الظاهرة
            ConfigureColumn(view, "PayrollSheetName", "كشف الراتب", 150, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            ConfigureColumn(view, "Year", "السنة", 60, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            ConfigureColumn(view, "Month", "الشهر", 60, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            ConfigureColumn(view, "StartDateFormatted", "من تاريخ", 90, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "EndDateFormatted", "إلى تاريخ", 90, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "EmployeeNumber", "رقم الموظف", 80, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "FullName", "اسم الموظف", 180, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            ConfigureColumn(view, "DepartmentName", "الإدارة", 130, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            ConfigureColumn(view, "PositionTitle", "المنصب", 120, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "BasicSalary", "الراتب الأساسي", 100, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.Numeric, true, false, "n2");
            ConfigureColumn(view, "TotalAllowances", "إجمالي البدلات", 100, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.Numeric, true, false, "n2");
            ConfigureColumn(view, "TotalDeductions", "إجمالي الاستقطاعات", 120, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.Numeric, true, false, "n2");
            ConfigureColumn(view, "NetSalary", "صافي الراتب", 100, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.Numeric, true, false, "n2");
            ConfigureColumn(view, "PaymentDateFormatted", "تاريخ الدفع", 90, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "PaymentMethodText", "طريقة الدفع", 90, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            ConfigureColumn(view, "PaymentReference", "المرجع", 120, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "Notes", "ملاحظات", 180, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "StatusText", "الحالة", 90, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            
            // إعداد الإجماليات في الذيل
            // إجمالي الرواتب الأساسية
            view.Columns["BasicSalary"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            view.Columns["BasicSalary"].SummaryItem.DisplayFormat = "المجموع: {0:n2}";
            
            // إجمالي البدلات
            view.Columns["TotalAllowances"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            view.Columns["TotalAllowances"].SummaryItem.DisplayFormat = "المجموع: {0:n2}";
            
            // إجمالي الاستقطاعات
            view.Columns["TotalDeductions"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            view.Columns["TotalDeductions"].SummaryItem.DisplayFormat = "المجموع: {0:n2}";
            
            // إجمالي صافي الرواتب
            view.Columns["NetSalary"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            view.Columns["NetSalary"].SummaryItem.DisplayFormat = "المجموع: {0:n2}";
            
            // عدد الموظفين
            view.Columns["EmployeeNumber"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            view.Columns["EmployeeNumber"].SummaryItem.DisplayFormat = "العدد: {0}";
            
            // إضافة ملخصات للمجموعات
            // تجميع حسب كشف الراتب
            view.Columns["PayrollSheetName"].Group();
            
            // إجماليات المجموعات
            view.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "EmployeeNumber", view.Columns["EmployeeNumber"], "العدد: {0}");
            view.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "BasicSalary", view.Columns["BasicSalary"], "الراتب الأساسي: {0:n2}");
            view.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalAllowances", view.Columns["TotalAllowances"], "البدلات: {0:n2}");
            view.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalDeductions", view.Columns["TotalDeductions"], "الاستقطاعات: {0:n2}");
            view.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NetSalary", view.Columns["NetSalary"], "صافي الرواتب: {0:n2}");
            
            // إضافة تلوين حسب حالة الدفع
            DevExpress.XtraGrid.StyleFormatCondition paidCondition = new DevExpress.XtraGrid.StyleFormatCondition();
            paidCondition.Appearance.ForeColor = Color.Green;
            paidCondition.Appearance.Options.UseForeColor = true;
            paidCondition.Column = view.Columns["StatusText"];
            paidCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            paidCondition.Value1 = "مدفوع";
            view.FormatConditions.Add(paidCondition);
            
            DevExpress.XtraGrid.StyleFormatCondition unpaidCondition = new DevExpress.XtraGrid.StyleFormatCondition();
            unpaidCondition.Appearance.ForeColor = Color.Red;
            unpaidCondition.Appearance.Options.UseForeColor = true;
            unpaidCondition.Column = view.Columns["StatusText"];
            unpaidCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            unpaidCondition.Value1 = "غير مدفوع";
            view.FormatConditions.Add(unpaidCondition);
        }
        
        /// <summary>
        /// تنسيق أعمدة تفاصيل الرواتب
        /// </summary>
        private void ConfigureDetailsColumns(GridView view)
        {
            // إخفاء الأعمدة غير المطلوبة
            HideColumn(view, "PayrollSheetID");
            HideColumn(view, "PayrollEntryID");
            HideColumn(view, "EmployeeID");
            HideColumn(view, "PayrollEntryItemID");
            HideColumn(view, "PaymentDate");
            HideColumn(view, "IsAddition");
            HideColumn(view, "Status");
            
            // تكوين الأعمدة الظاهرة
            ConfigureColumn(view, "PayrollSheetName", "كشف الراتب", 150, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            ConfigureColumn(view, "Year", "السنة", 60, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            ConfigureColumn(view, "Month", "الشهر", 60, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            ConfigureColumn(view, "StartDateFormatted", "من تاريخ", 90, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "EndDateFormatted", "إلى تاريخ", 90, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "EmployeeNumber", "رقم الموظف", 80, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "FullName", "اسم الموظف", 180, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            ConfigureColumn(view, "DepartmentName", "الإدارة", 130, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            ConfigureColumn(view, "ItemType", "النوع", 80, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            ConfigureColumn(view, "Category", "الفئة", 100, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            ConfigureColumn(view, "ItemDescription", "البيان", 180, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "Amount", "المبلغ", 100, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.Numeric, true, false, "n2");
            ConfigureColumn(view, "Reference", "المرجع", 120, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "Notes", "ملاحظات", 180, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "PaymentDateFormatted", "تاريخ الدفع", 90, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "PaymentMethodText", "طريقة الدفع", 90, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
            ConfigureColumn(view, "StatusText", "الحالة", 90, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
            
            // إعداد الإجماليات في الذيل
            // إجمالي المبالغ
            view.Columns["Amount"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            view.Columns["Amount"].SummaryItem.DisplayFormat = "المجموع: {0:n2}";
            
            // عدد العناصر
            view.Columns["ItemDescription"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            view.Columns["ItemDescription"].SummaryItem.DisplayFormat = "العدد: {0}";
            
            // إضافة ملخصات للمجموعات
            // تجميع حسب الموظف
            view.Columns["FullName"].Group();
            
            // تجميع حسب نوع العنصر (بدل/استقطاع)
            view.Columns["ItemType"].Group();
            
            // إجماليات المجموعات
            view.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "ItemDescription", view.Columns["ItemDescription"], "العدد: {0}");
            view.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Amount", view.Columns["Amount"], "المجموع: {0:n2}");
            
            // إضافة تلوين حسب نوع العنصر
            DevExpress.XtraGrid.StyleFormatCondition allowanceCondition = new DevExpress.XtraGrid.StyleFormatCondition();
            allowanceCondition.Appearance.ForeColor = Color.Green;
            allowanceCondition.Appearance.Options.UseForeColor = true;
            allowanceCondition.Column = view.Columns["ItemType"];
            allowanceCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            allowanceCondition.Value1 = "بدل";
            view.FormatConditions.Add(allowanceCondition);
            
            DevExpress.XtraGrid.StyleFormatCondition deductionCondition = new DevExpress.XtraGrid.StyleFormatCondition();
            deductionCondition.Appearance.ForeColor = Color.Red;
            deductionCondition.Appearance.Options.UseForeColor = true;
            deductionCondition.Column = view.Columns["ItemType"];
            deductionCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            deductionCondition.Value1 = "استقطاع";
            view.FormatConditions.Add(deductionCondition);
            
            // تلوين حسب حالة الدفع
            DevExpress.XtraGrid.StyleFormatCondition paidCondition = new DevExpress.XtraGrid.StyleFormatCondition();
            paidCondition.Appearance.ForeColor = Color.DarkGreen;
            paidCondition.Appearance.Options.UseForeColor = true;
            paidCondition.Column = view.Columns["StatusText"];
            paidCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            paidCondition.Value1 = "مدفوع";
            view.FormatConditions.Add(paidCondition);
            
            DevExpress.XtraGrid.StyleFormatCondition unpaidCondition = new DevExpress.XtraGrid.StyleFormatCondition();
            unpaidCondition.Appearance.ForeColor = Color.DarkRed;
            unpaidCondition.Appearance.Options.UseForeColor = true;
            unpaidCondition.Column = view.Columns["StatusText"];
            unpaidCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            unpaidCondition.Value1 = "غير مدفوع";
            view.FormatConditions.Add(unpaidCondition);
        }
        
        /// <summary>
        /// إنشاء معايير التصفية بناءً على اختيارات المستخدم
        /// </summary>
        private ReportFilterOptions CreateFilterOptions()
        {
            var filter = new ReportFilterOptions();
            
            // تعيين الفترة الزمنية
            filter.StartDate = dtStartDate.DateTime;
            filter.EndDate = dtEndDate.DateTime;
            
            // تعيين الإدارة والموظف
            if (cboDepartment.EditValue != null && Convert.ToInt32(cboDepartment.EditValue) > 0)
            {
                filter.DepartmentId = Convert.ToInt32(cboDepartment.EditValue);
            }
            
            if (cboEmployee.EditValue != null && Convert.ToInt32(cboEmployee.EditValue) > 0)
            {
                filter.EmployeeId = Convert.ToInt32(cboEmployee.EditValue);
            }
            
            // تعيين كشف الراتب
            if (cboPayrollSheet.EditValue != null && Convert.ToInt32(cboPayrollSheet.EditValue) > 0)
            {
                filter.PayrollSheetId = Convert.ToInt32(cboPayrollSheet.EditValue);
            }
            
            // تعيين نوع العرض
            filter.ShowDetails = chkShowDetails.Checked;
            
            return filter;
        }
        
        /// <summary>
        /// بناء شرط WHERE للاستعلام
        /// </summary>
        private string BuildWhereClause(ReportFilterOptions filter)
        {
            List<string> conditions = new List<string>();
            
            // شرط الفترة الزمنية
            if (filter.ShowDetails)
            {
                conditions.Add("PS.StartDate BETWEEN @StartDate AND @EndDate");
            }
            else
            {
                conditions.Add("PS.StartDate BETWEEN @StartDate AND @EndDate");
            }
            
            // شرط الإدارة
            if (filter.DepartmentId.HasValue && filter.DepartmentId.Value > 0)
            {
                conditions.Add("E.DepartmentID = @DepartmentId");
            }
            
            // شرط الموظف
            if (filter.EmployeeId.HasValue && filter.EmployeeId.Value > 0)
            {
                conditions.Add("E.ID = @EmployeeId");
            }
            
            // شرط كشف الراتب
            if (filter.PayrollSheetId.HasValue && filter.PayrollSheetId.Value > 0)
            {
                conditions.Add("PS.ID = @PayrollSheetId");
            }
            
            // بناء جملة WHERE
            if (conditions.Count > 0)
            {
                return "WHERE " + string.Join(" AND ", conditions);
            }
            
            return string.Empty;
        }
        
        /// <summary>
        /// إضافة بارامترات الاستعلام
        /// </summary>
        private void AddFilterParameters(System.Data.SqlClient.SqlCommand command, ReportFilterOptions filter)
        {
            // بارامترات الفترة الزمنية
            command.Parameters.AddWithValue("@StartDate", filter.StartDate);
            command.Parameters.AddWithValue("@EndDate", filter.EndDate);
            
            // بارامترات الإدارة والموظف
            if (filter.DepartmentId.HasValue && filter.DepartmentId.Value > 0)
            {
                command.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId.Value);
            }
            
            if (filter.EmployeeId.HasValue && filter.EmployeeId.Value > 0)
            {
                command.Parameters.AddWithValue("@EmployeeId", filter.EmployeeId.Value);
            }
            
            // بارامتر كشف الراتب
            if (filter.PayrollSheetId.HasValue && filter.PayrollSheetId.Value > 0)
            {
                command.Parameters.AddWithValue("@PayrollSheetId", filter.PayrollSheetId.Value);
            }
        }
        
        #region أحداث أدوات التحكم
        
        /// <summary>
        /// حدث تغيير الإدارة
        /// </summary>
        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                int departmentId = 0;
                if (cboDepartment.EditValue != null)
                {
                    departmentId = Convert.ToInt32(cboDepartment.EditValue);
                }
                
                // تحديث قائمة الموظفين
                LoadEmployees(departmentId);
                
                // تحديث البيانات
                LoadReportData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// حدث تغيير أي من أدوات التصفية
        /// </summary>
        private void filter_ValueChanged(object sender, EventArgs e)
        {
            LoadReportData();
        }
        
        /// <summary>
        /// زر إنشاء التقرير
        /// </summary>
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // طباعة التقرير
                PrintReportPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء إنشاء التقرير: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// معاينة طباعة التقرير
        /// </summary>
        private void PrintReportPreview()
        {
            GridView view = gridControl.MainView as GridView;
            
            // إنشاء المعاينة
            view.OptionsPrint.RtfPageHeader = $"تقرير الرواتب - {DateTime.Now.ToString("yyyy/MM/dd")}";
            view.OptionsPrint.RtfReportHeader = lblReportTitle.Text;
            
            view.ShowRibbonPrintPreview();
        }
        
        /// <summary>
        /// زر تصدير البيانات
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                popupMenu.ShowPopup(MousePosition);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// زر تصدير إلى Excel
        /// </summary>
        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // فتح نافذة اختيار المكان
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "ملفات Excel|*.xlsx";
                saveDialog.Title = "تصدير إلى Excel";
                saveDialog.FileName = "تقرير الرواتب.xlsx";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    GridView view = gridControl.MainView as GridView;
                    view.ExportToXlsx(saveDialog.FileName);
                    
                    // فتح الملف
                    if (XtraMessageBox.Show(
                        "تم تصدير الملف بنجاح. هل تريد فتح الملف الآن؟",
                        "تم التصدير",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تصدير البيانات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// زر تصدير إلى PDF
        /// </summary>
        private void btnExportPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // فتح نافذة اختيار المكان
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "ملفات PDF|*.pdf";
                saveDialog.Title = "تصدير إلى PDF";
                saveDialog.FileName = "تقرير الرواتب.pdf";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    GridView view = gridControl.MainView as GridView;
                    view.ExportToPdf(saveDialog.FileName);
                    
                    // فتح الملف
                    if (XtraMessageBox.Show(
                        "تم تصدير الملف بنجاح. هل تريد فتح الملف الآن؟",
                        "تم التصدير",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تصدير البيانات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// زر الطباعة
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintReportPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء الطباعة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// زر تحديث البيانات
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReportData();
        }
        
        #endregion
        
        #region طرق تنسيق وتهيئة GridView
        
        /// <summary>
        /// تكوين عمود في GridView مع تنسيق متقدم
        /// </summary>
        /// <param name="view">عرض الجدول</param>
        /// <param name="fieldName">اسم الحقل</param>
        /// <param name="caption">عنوان العمود</param>
        /// <param name="width">عرض العمود</param>
        /// <param name="alignment">محاذاة النص</param>
        /// <param name="formatType">نوع التنسيق</param>
        /// <param name="allowFilter">السماح بالتصفية</param>
        /// <param name="allowGroup">السماح بالتجميع</param>
        /// <param name="formatString">صيغة التنسيق</param>
        private void ConfigureColumn(
            DevExpress.XtraGrid.Views.Grid.GridView view, 
            string fieldName, 
            string caption, 
            int width, 
            DevExpress.Utils.HorzAlignment alignment, 
            DevExpress.Utils.FormatType formatType, 
            bool allowFilter = true, 
            bool allowGroup = false, 
            string formatString = "")
        {
            if (view == null || view.Columns[fieldName] == null) return;
            
            DevExpress.XtraGrid.Columns.GridColumn column = view.Columns[fieldName];
            
            // تعيين العنوان والعرض
            column.Caption = caption;
            column.Width = width;
            column.MinWidth = 50;
            
            // تنسيق المحاذاة
            column.AppearanceHeader.TextOptions.HAlignment = alignment;
            column.AppearanceCell.TextOptions.HAlignment = alignment;
            
            // تعيين خيارات عرض العمود
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowFocus = true;
            column.OptionsColumn.AllowMove = true;
            column.OptionsColumn.AllowSize = true;
            column.OptionsColumn.AllowSort = true;
            column.OptionsColumn.ReadOnly = true;
            column.OptionsFilter.AllowFilter = allowFilter;
            column.OptionsColumn.AllowGroup = allowGroup ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            
            // حفظ تخصيص العمود
            column.OptionsColumn.SaveInCustomizationForm = true;
            
            // تطبيق قواعد تنسيق البيانات
            column.AppearanceCell.Options.UseTextOptions = true;
            
            // إعداد تنسيق العرض
            if (formatType != DevExpress.Utils.FormatType.None)
            {
                column.DisplayFormat.FormatType = formatType;
                
                if (!string.IsNullOrEmpty(formatString))
                {
                    column.DisplayFormat.FormatString = formatString;
                }
                
                // تعيين تنسيق خاص لبعض أنواع البيانات
                switch (formatType)
                {
                    case DevExpress.Utils.FormatType.DateTime:
                        column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        if (string.IsNullOrEmpty(formatString))
                        {
                            column.DisplayFormat.FormatString = "dd/MM/yyyy";
                        }
                        break;
                        
                    case DevExpress.Utils.FormatType.Numeric:
                        column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        if (string.IsNullOrEmpty(formatString))
                        {
                            column.DisplayFormat.FormatString = "n2";
                        }
                        break;
                        
                    case DevExpress.Utils.FormatType.Custom:
                        // يمكن إضافة تنسيقات مخصصة
                        break;
                }
            }
        }
        
        /// <summary>
        /// إخفاء عمود
        /// </summary>
        /// <param name="view">عرض الجدول</param>
        /// <param name="fieldName">اسم الحقل</param>
        private void HideColumn(DevExpress.XtraGrid.Views.Grid.GridView view, string fieldName)
        {
            if (view != null && view.Columns[fieldName] != null)
            {
                view.Columns[fieldName].Visible = false;
            }
        }
        
        #endregion
        
        /// <summary>
        /// فئة لتخزين معايير التصفية
        /// </summary>
        public class ReportFilterOptions
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int? DepartmentId { get; set; }
            public int? EmployeeId { get; set; }
            public int? PayrollSheetId { get; set; }
            public bool ShowDetails { get; set; }
        }
    }
}