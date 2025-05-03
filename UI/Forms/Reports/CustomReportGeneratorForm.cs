using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using HR.Core;

namespace HR.UI.Forms.Reports
{
    /// <summary>
    /// نموذج مولد التقارير المخصصة
    /// </summary>
    public partial class CustomReportGeneratorForm : XtraForm
    {
        private readonly ConnectionManager _connectionManager;
        private DataTable _reportData;
        private List<string> _selectedFields = new List<string>();
        
        public CustomReportGeneratorForm()
        {
            InitializeComponent();
            
            _connectionManager = new ConnectionManager();
            
            // تهيئة البيانات الأولية
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
                dtStartDate.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtEndDate.DateTime = DateTime.Now;
                
                // تحميل قائمة أنواع التقارير
                LoadReportTypes();
                
                // تحميل قائمة الجداول المتاحة
                LoadAvailableTables();
                
                // ربط الأحداث
                cboReportType.EditValueChanged += cboReportType_EditValueChanged;
                cboMainTable.EditValueChanged += cboMainTable_EditValueChanged;
                btnAddField.Click += btnAddField_Click;
                btnRemoveField.Click += btnRemoveField_Click;
                btnMoveUp.Click += btnMoveUp_Click;
                btnMoveDown.Click += btnMoveDown_Click;
                rgChartType.EditValueChanged += rgChartType_EditValueChanged;
                dtStartDate.EditValueChanged += filter_ValueChanged;
                dtEndDate.EditValueChanged += filter_ValueChanged;
                
                // تهيئة GridView
                InitializeGridView();
                
                // تهيئة الرسم البياني
                InitializeChartControl();
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
        /// تحميل قائمة أنواع التقارير
        /// </summary>
        private void LoadReportTypes()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                
                dt.Rows.Add(1, "تقرير موظفين");
                dt.Rows.Add(2, "تقرير حضور");
                dt.Rows.Add(3, "تقرير إجازات");
                dt.Rows.Add(4, "تقرير رواتب");
                dt.Rows.Add(5, "تقرير تحليلي");
                dt.Rows.Add(6, "تقرير مخصص");
                
                cboReportType.Properties.DataSource = dt;
                cboReportType.Properties.DisplayMember = "Name";
                cboReportType.Properties.ValueMember = "ID";
                cboReportType.Properties.PopulateColumns();
                cboReportType.Properties.Columns["ID"].Visible = false;
                cboReportType.EditValue = 6; // تعيين "تقرير مخصص" كخيار افتراضي
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تحميل قائمة أنواع التقارير: {ex.Message}");
            }
        }
        
        /// <summary>
        /// تحميل قائمة الجداول المتاحة في قاعدة البيانات
        /// </summary>
        private void LoadAvailableTables()
        {
            try
            {
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT 
                            TABLE_NAME AS TableName,
                            TABLE_NAME AS DisplayName
                        FROM 
                            INFORMATION_SCHEMA.TABLES
                        WHERE 
                            TABLE_TYPE = 'BASE TABLE'
                            AND TABLE_NAME NOT LIKE 'sys%'
                            AND TABLE_NAME NOT LIKE 'dt%'
                        ORDER BY 
                            TABLE_NAME";
                    
                    using (var adapter = new System.Data.SqlClient.SqlDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        
                        try
                        {
                            adapter.Fill(dt);
                        }
                        catch
                        {
                            // في حالة عدم القدرة على الوصول للجداول، نقوم بإنشاء قائمة افتراضية
                            dt = CreateDefaultTablesList();
                        }
                        
                        cboMainTable.Properties.DataSource = dt;
                        cboMainTable.Properties.DisplayMember = "DisplayName";
                        cboMainTable.Properties.ValueMember = "TableName";
                        cboMainTable.Properties.PopulateColumns();
                        cboMainTable.Properties.Columns["TableName"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                // في حالة حدوث خطأ، نقوم بإنشاء قائمة افتراضية
                DataTable dt = CreateDefaultTablesList();
                cboMainTable.Properties.DataSource = dt;
                cboMainTable.Properties.DisplayMember = "DisplayName";
                cboMainTable.Properties.ValueMember = "TableName";
                cboMainTable.Properties.PopulateColumns();
                cboMainTable.Properties.Columns["TableName"].Visible = false;
            }
        }
        
        /// <summary>
        /// إنشاء قائمة افتراضية للجداول في حالة عدم القدرة على الوصول للجداول الفعلية
        /// </summary>
        private DataTable CreateDefaultTablesList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TableName", typeof(string));
            dt.Columns.Add("DisplayName", typeof(string));
            
            dt.Rows.Add("Employees", "الموظفين");
            dt.Rows.Add("Departments", "الإدارات");
            dt.Rows.Add("Positions", "المناصب");
            dt.Rows.Add("Attendance", "الحضور والانصراف");
            dt.Rows.Add("LeaveRequests", "طلبات الإجازات");
            dt.Rows.Add("LeaveTypes", "أنواع الإجازات");
            dt.Rows.Add("Salaries", "الرواتب");
            dt.Rows.Add("PayrollSheets", "كشوف الرواتب");
            dt.Rows.Add("WorkShifts", "ورديات العمل");
            dt.Rows.Add("WorkHours", "ساعات العمل");
            
            return dt;
        }
        
        /// <summary>
        /// تحميل حقول الجدول المحدد
        /// </summary>
        private void LoadTableFields(string tableName)
        {
            try
            {
                // تفريغ القوائم
                lstAvailableFields.Items.Clear();
                lstSelectedFields.Items.Clear();
                _selectedFields.Clear();
                
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT 
                            COLUMN_NAME AS FieldName,
                            CASE 
                                WHEN DATA_TYPE IN ('bigint', 'int', 'smallint', 'tinyint', 'decimal', 'numeric', 'float', 'real', 'money', 'smallmoney') THEN 'رقم'
                                WHEN DATA_TYPE IN ('char', 'varchar', 'text', 'nchar', 'nvarchar', 'ntext') THEN 'نص'
                                WHEN DATA_TYPE IN ('date', 'datetime', 'datetime2', 'smalldatetime', 'time', 'timestamp') THEN 'تاريخ'
                                WHEN DATA_TYPE IN ('bit') THEN 'منطقي'
                                ELSE DATA_TYPE
                            END AS DataType
                        FROM 
                            INFORMATION_SCHEMA.COLUMNS
                        WHERE 
                            TABLE_NAME = @TableName
                        ORDER BY 
                            ORDINAL_POSITION";
                    
                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TableName", tableName);
                        
                        try
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string fieldName = reader["FieldName"].ToString();
                                    string dataType = reader["DataType"].ToString();
                                    
                                    ListViewItem item = new ListViewItem(fieldName);
                                    item.SubItems.Add(dataType);
                                    lstAvailableFields.Items.Add(item);
                                }
                            }
                        }
                        catch
                        {
                            // في حالة عدم القدرة على الوصول للحقول، نقوم بإنشاء قائمة افتراضية
                            LoadDefaultTableFields(tableName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // في حالة حدوث خطأ، نقوم بإنشاء قائمة افتراضية
                LoadDefaultTableFields(tableName);
            }
        }
        
        /// <summary>
        /// تحميل حقول افتراضية للجدول المحدد
        /// </summary>
        private void LoadDefaultTableFields(string tableName)
        {
            // تفريغ القوائم
            lstAvailableFields.Items.Clear();
            
            List<Tuple<string, string>> fields = new List<Tuple<string, string>>();
            
            // تحديد الحقول الافتراضية حسب اسم الجدول
            switch (tableName.ToLower())
            {
                case "employees":
                    fields.Add(new Tuple<string, string>("ID", "رقم"));
                    fields.Add(new Tuple<string, string>("EmployeeNumber", "نص"));
                    fields.Add(new Tuple<string, string>("FullName", "نص"));
                    fields.Add(new Tuple<string, string>("DepartmentID", "رقم"));
                    fields.Add(new Tuple<string, string>("PositionID", "رقم"));
                    fields.Add(new Tuple<string, string>("HireDate", "تاريخ"));
                    fields.Add(new Tuple<string, string>("BasicSalary", "رقم"));
                    fields.Add(new Tuple<string, string>("JobTitle", "نص"));
                    fields.Add(new Tuple<string, string>("Email", "نص"));
                    fields.Add(new Tuple<string, string>("Phone", "نص"));
                    fields.Add(new Tuple<string, string>("Gender", "نص"));
                    fields.Add(new Tuple<string, string>("BirthDate", "تاريخ"));
                    fields.Add(new Tuple<string, string>("Address", "نص"));
                    fields.Add(new Tuple<string, string>("IsActive", "منطقي"));
                    fields.Add(new Tuple<string, string>("WorkShiftID", "رقم"));
                    break;
                    
                case "departments":
                    fields.Add(new Tuple<string, string>("ID", "رقم"));
                    fields.Add(new Tuple<string, string>("Name", "نص"));
                    fields.Add(new Tuple<string, string>("Code", "نص"));
                    fields.Add(new Tuple<string, string>("Description", "نص"));
                    fields.Add(new Tuple<string, string>("ManagerID", "رقم"));
                    fields.Add(new Tuple<string, string>("ParentDepartmentID", "رقم"));
                    fields.Add(new Tuple<string, string>("IsActive", "منطقي"));
                    break;
                    
                case "attendance":
                    fields.Add(new Tuple<string, string>("ID", "رقم"));
                    fields.Add(new Tuple<string, string>("EmployeeID", "رقم"));
                    fields.Add(new Tuple<string, string>("RecordDate", "تاريخ"));
                    fields.Add(new Tuple<string, string>("TimeIn", "تاريخ"));
                    fields.Add(new Tuple<string, string>("TimeOut", "تاريخ"));
                    fields.Add(new Tuple<string, string>("IsExcused", "منطقي"));
                    fields.Add(new Tuple<string, string>("ExcuseReason", "نص"));
                    fields.Add(new Tuple<string, string>("Notes", "نص"));
                    fields.Add(new Tuple<string, string>("DeviceID", "رقم"));
                    break;
                    
                case "leaverequests":
                    fields.Add(new Tuple<string, string>("ID", "رقم"));
                    fields.Add(new Tuple<string, string>("EmployeeID", "رقم"));
                    fields.Add(new Tuple<string, string>("LeaveTypeID", "رقم"));
                    fields.Add(new Tuple<string, string>("RequestDate", "تاريخ"));
                    fields.Add(new Tuple<string, string>("StartDate", "تاريخ"));
                    fields.Add(new Tuple<string, string>("EndDate", "تاريخ"));
                    fields.Add(new Tuple<string, string>("Duration", "رقم"));
                    fields.Add(new Tuple<string, string>("Comments", "نص"));
                    fields.Add(new Tuple<string, string>("Status", "رقم"));
                    fields.Add(new Tuple<string, string>("ApprovalDate", "تاريخ"));
                    fields.Add(new Tuple<string, string>("ApprovedBy", "رقم"));
                    fields.Add(new Tuple<string, string>("ApprovalComments", "نص"));
                    break;
                    
                case "salaries":
                    fields.Add(new Tuple<string, string>("ID", "رقم"));
                    fields.Add(new Tuple<string, string>("EmployeeID", "رقم"));
                    fields.Add(new Tuple<string, string>("PayrollSheetID", "رقم"));
                    fields.Add(new Tuple<string, string>("BasicSalary", "رقم"));
                    fields.Add(new Tuple<string, string>("TotalAllowances", "رقم"));
                    fields.Add(new Tuple<string, string>("TotalDeductions", "رقم"));
                    fields.Add(new Tuple<string, string>("NetSalary", "رقم"));
                    fields.Add(new Tuple<string, string>("PaymentDate", "تاريخ"));
                    fields.Add(new Tuple<string, string>("PaymentMethod", "رقم"));
                    fields.Add(new Tuple<string, string>("PaymentReference", "نص"));
                    fields.Add(new Tuple<string, string>("Status", "رقم"));
                    fields.Add(new Tuple<string, string>("Notes", "نص"));
                    break;
                    
                default:
                    fields.Add(new Tuple<string, string>("ID", "رقم"));
                    fields.Add(new Tuple<string, string>("Name", "نص"));
                    fields.Add(new Tuple<string, string>("Description", "نص"));
                    fields.Add(new Tuple<string, string>("IsActive", "منطقي"));
                    fields.Add(new Tuple<string, string>("CreatedDate", "تاريخ"));
                    fields.Add(new Tuple<string, string>("CreatedBy", "رقم"));
                    break;
            }
            
            // إضافة الحقول إلى القائمة
            foreach (var field in fields)
            {
                ListViewItem item = new ListViewItem(field.Item1);
                item.SubItems.Add(field.Item2);
                lstAvailableFields.Items.Add(item);
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
                view.OptionsPrint.RtfPageHeader = "التقرير المخصص";
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
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تهيئة جدول البيانات: {ex.Message}");
            }
        }
        
        /// <summary>
        /// تهيئة الرسم البياني
        /// </summary>
        private void InitializeChartControl()
        {
            try
            {
                // تهيئة الرسم البياني
                chartControl.Titles.Clear();
                
                // إضافة عنوان للرسم البياني
                ChartTitle title = new ChartTitle();
                title.Text = "رسم بياني";
                title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                title.Alignment = StringAlignment.Center;
                chartControl.Titles.Add(title);
                
                // إخفاء الرسم البياني في البداية
                splitContainerControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تهيئة الرسم البياني: {ex.Message}");
            }
        }
        
        /// <summary>
        /// إنشاء الرسم البياني
        /// </summary>
        private void CreateChart(DataTable data, string categoryField, string valueField)
        {
            try
            {
                if (data == null || data.Rows.Count == 0 || string.IsNullOrEmpty(categoryField) || string.IsNullOrEmpty(valueField))
                {
                    return;
                }
                
                // تفريغ الرسم البياني
                chartControl.Series.Clear();
                
                // إضافة السلسلة (Series)
                Series series = new Series("البيانات", ViewType.Bar);
                
                // تحديد نوع الرسم البياني
                switch (rgChartType.SelectedIndex)
                {
                    case 0: // أعمدة
                        series.ChangeView(ViewType.Bar);
                        break;
                    case 1: // خطي
                        series.ChangeView(ViewType.Line);
                        break;
                    case 2: // دائري
                        series.ChangeView(ViewType.Pie);
                        break;
                    case 3: // منطقة
                        series.ChangeView(ViewType.Area);
                        break;
                    default:
                        series.ChangeView(ViewType.Bar);
                        break;
                }
                
                // إضافة البيانات إلى السلسلة
                foreach (DataRow row in data.Rows)
                {
                    string category = row[categoryField]?.ToString() ?? "غير محدد";
                    double value = 0;
                    
                    // محاولة تحويل القيمة إلى رقم
                    if (row[valueField] != null && row[valueField] != DBNull.Value)
                    {
                        double.TryParse(row[valueField].ToString(), out value);
                    }
                    
                    series.Points.Add(new SeriesPoint(category, value));
                }
                
                // إضافة السلسلة إلى الرسم البياني
                chartControl.Series.Add(series);
                
                // تحديث عنوان الرسم البياني
                chartControl.Titles[0].Text = $"رسم بياني - {cboChartValue.Text} حسب {cboChartCategory.Text}";
                
                // إظهار الرسم البياني
                splitContainerControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء إنشاء الرسم البياني: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                
                // إخفاء الرسم البياني في حالة الخطأ
                splitContainerControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
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
                
                // التحقق من وجود حقول محددة
                if (_selectedFields.Count == 0 || cboMainTable.EditValue == null)
                {
                    XtraMessageBox.Show(
                        "يرجى تحديد الجدول والحقول المطلوبة للتقرير.",
                        "تنبيه",
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                    return;
                }
                
                string tableName = cboMainTable.EditValue.ToString();
                
                // بناء استعلام التقرير
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("SELECT ");
                
                // إضافة الحقول المحددة
                for (int i = 0; i < _selectedFields.Count; i++)
                {
                    queryBuilder.Append(_selectedFields[i]);
                    
                    if (i < _selectedFields.Count - 1)
                    {
                        queryBuilder.Append(", ");
                    }
                }
                
                // إضافة الجدول المحدد
                queryBuilder.Append($" FROM {tableName}");
                
                // إضافة شرط الفترة الزمنية إذا كان هناك حقل تاريخ في قائمة الحقول المحددة
                string dateField = FindDateField();
                
                if (!string.IsNullOrEmpty(dateField))
                {
                    queryBuilder.Append($" WHERE {dateField} BETWEEN @StartDate AND @EndDate");
                }
                
                // ترتيب النتائج
                if (!string.IsNullOrEmpty(dateField))
                {
                    queryBuilder.Append($" ORDER BY {dateField} DESC");
                }
                else if (_selectedFields.Contains("ID"))
                {
                    queryBuilder.Append(" ORDER BY ID");
                }
                
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    using (var command = new System.Data.SqlClient.SqlCommand(queryBuilder.ToString(), connection))
                    {
                        // إضافة المعايير إذا كان هناك حقل تاريخ
                        if (!string.IsNullOrEmpty(dateField))
                        {
                            command.Parameters.AddWithValue("@StartDate", dtStartDate.DateTime);
                            command.Parameters.AddWithValue("@EndDate", dtEndDate.DateTime);
                        }
                        
                        try
                        {
                            // تنفيذ الاستعلام وعرض البيانات
                            DataTable resultTable = new DataTable();
                            var adapter = new System.Data.SqlClient.SqlDataAdapter(command);
                            adapter.Fill(resultTable);
                            
                            // تعيين البيانات للجدول
                            gridControl.DataSource = resultTable;
                            _reportData = resultTable;
                            
                            // تحديث عنوان التقرير بعدد النتائج
                            lblReportTitle.Text = $"التقرير المخصص - {tableName} - عدد النتائج: {resultTable.Rows.Count}";
                            
                            // تحديث قوائم الرسم البياني
                            UpdateChartLists(resultTable);
                            
                            // تنسيق الأعمدة
                            FormatGridColumns();
                        }
                        catch (Exception ex)
                        {
                            // في حالة فشل الاستعلام، نعرض خطأ ونقوم بإنشاء بيانات تجريبية
                            XtraMessageBox.Show(
                                $"حدث خطأ أثناء استرجاع البيانات: {ex.Message}\n\nسيتم عرض بيانات تجريبية.",
                                "خطأ",
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Error);
                            
                            // إنشاء بيانات تجريبية
                            DataTable dummyData = CreateDummyReportData(tableName, _selectedFields);
                            
                            // تعيين البيانات للجدول
                            gridControl.DataSource = dummyData;
                            _reportData = dummyData;
                            
                            // تحديث عنوان التقرير بعدد النتائج
                            lblReportTitle.Text = $"التقرير المخصص - {tableName} - عدد النتائج: {dummyData.Rows.Count} (بيانات تجريبية)";
                            
                            // تحديث قوائم الرسم البياني
                            UpdateChartLists(dummyData);
                            
                            // تنسيق الأعمدة
                            FormatGridColumns();
                        }
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
        /// البحث عن حقل تاريخ في قائمة الحقول المحددة
        /// </summary>
        private string FindDateField()
        {
            // التحقق من وجود حقول تاريخ شائعة
            string[] commonDateFields = { "CreatedDate", "Date", "HireDate", "BirthDate", "RecordDate", "RequestDate", "StartDate", "EndDate", "PaymentDate", "ApprovalDate" };
            
            foreach (var field in commonDateFields)
            {
                if (_selectedFields.Contains(field))
                {
                    return field;
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// تنسيق أعمدة الجدول
        /// </summary>
        private void FormatGridColumns()
        {
            try
            {
                GridView view = gridControl.MainView as GridView;
                
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in view.Columns)
                {
                    // تعيين عرض العمود
                    column.Width = 120;
                    
                    // تعيين المحاذاة
                    column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    
                    // تنسيق العمود حسب نوع البيانات
                    if (column.ColumnType == typeof(DateTime) || column.FieldName.EndsWith("Date"))
                    {
                        column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        column.DisplayFormat.FormatString = "dd/MM/yyyy";
                    }
                    else if (column.ColumnType == typeof(decimal) || column.ColumnType == typeof(double) || column.ColumnType == typeof(float))
                    {
                        column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        column.DisplayFormat.FormatString = "n2";
                        column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تنسيق أعمدة الجدول: {ex.Message}");
            }
        }
        
        /// <summary>
        /// تحديث قوائم الرسم البياني
        /// </summary>
        private void UpdateChartLists(DataTable data)
        {
            try
            {
                if (data == null || data.Columns.Count == 0)
                {
                    return;
                }
                
                // تفريغ القوائم
                cboChartCategory.Properties.Items.Clear();
                cboChartValue.Properties.Items.Clear();
                
                // إضافة الحقول إلى القوائم
                foreach (DataColumn column in data.Columns)
                {
                    // إضافة جميع الحقول إلى قائمة التصنيف
                    cboChartCategory.Properties.Items.Add(column.ColumnName);
                    
                    // إضافة الحقول الرقمية فقط إلى قائمة القيم
                    if (column.DataType == typeof(int) || column.DataType == typeof(long) || 
                        column.DataType == typeof(float) || column.DataType == typeof(double) || 
                        column.DataType == typeof(decimal))
                    {
                        cboChartValue.Properties.Items.Add(column.ColumnName);
                    }
                }
                
                // تحديد القيم الافتراضية
                if (cboChartCategory.Properties.Items.Count > 0)
                {
                    cboChartCategory.SelectedIndex = 0;
                }
                
                if (cboChartValue.Properties.Items.Count > 0)
                {
                    cboChartValue.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تحديث قوائم الرسم البياني: {ex.Message}");
            }
        }
        
        /// <summary>
        /// إنشاء بيانات تجريبية للتقرير
        /// </summary>
        private DataTable CreateDummyReportData(string tableName, List<string> fields)
        {
            DataTable dt = new DataTable();
            
            // إضافة الأعمدة
            foreach (string field in fields)
            {
                // تحديد نوع البيانات بناءً على اسم الحقل
                Type dataType = typeof(string);
                
                if (field == "ID" || field.EndsWith("ID") || field.EndsWith("Count") || field == "Duration")
                {
                    dataType = typeof(int);
                }
                else if (field.EndsWith("Date") || field == "TimeIn" || field == "TimeOut")
                {
                    dataType = typeof(DateTime);
                }
                else if (field.Contains("Salary") || field.Contains("Amount") || field.EndsWith("Cost") || field.EndsWith("Price"))
                {
                    dataType = typeof(decimal);
                }
                else if (field.StartsWith("Is") || field == "Status")
                {
                    dataType = typeof(bool);
                }
                
                dt.Columns.Add(field, dataType);
            }
            
            // إضافة بيانات تجريبية
            Random rand = new Random();
            
            for (int i = 1; i <= 10; i++)
            {
                DataRow row = dt.NewRow();
                
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.DataType == typeof(int))
                    {
                        if (col.ColumnName == "ID" || col.ColumnName.EndsWith("ID"))
                        {
                            row[col.ColumnName] = i;
                        }
                        else
                        {
                            row[col.ColumnName] = rand.Next(1, 100);
                        }
                    }
                    else if (col.DataType == typeof(decimal))
                    {
                        row[col.ColumnName] = Math.Round(rand.NextDouble() * 10000, 2);
                    }
                    else if (col.DataType == typeof(DateTime))
                    {
                        row[col.ColumnName] = DateTime.Now.AddDays(-rand.Next(0, 30)).AddHours(-rand.Next(0, 24));
                    }
                    else if (col.DataType == typeof(bool))
                    {
                        row[col.ColumnName] = rand.Next(0, 2) == 1;
                    }
                    else
                    {
                        // حقول نصية
                        if (col.ColumnName == "FullName" || col.ColumnName == "Name")
                        {
                            string[] names = { "أحمد محمد", "محمد علي", "فاطمة أحمد", "علي حسن", "سارة محمد", "خالد أحمد", "نورا علي", "حسن محمود", "ليلى خالد", "عمر سعيد" };
                            row[col.ColumnName] = names[i - 1];
                        }
                        else if (col.ColumnName == "Email")
                        {
                            row[col.ColumnName] = $"user{i}@example.com";
                        }
                        else if (col.ColumnName == "Phone")
                        {
                            row[col.ColumnName] = $"0555{rand.Next(100000, 999999)}";
                        }
                        else if (col.ColumnName == "Department" || col.ColumnName == "DepartmentName")
                        {
                            string[] departments = { "الإدارة المالية", "الموارد البشرية", "تقنية المعلومات", "المبيعات", "التسويق", "خدمة العملاء", "المشتريات", "الإنتاج", "الجودة", "الإدارة العليا" };
                            row[col.ColumnName] = departments[rand.Next(0, departments.Length)];
                        }
                        else
                        {
                            row[col.ColumnName] = $"بيانات {i} - {col.ColumnName}";
                        }
                    }
                }
                
                dt.Rows.Add(row);
            }
            
            return dt;
        }
        
        #region أحداث أدوات التحكم
        
        /// <summary>
        /// حدث تغيير نوع التقرير
        /// </summary>
        private void cboReportType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboReportType.EditValue == null)
                {
                    return;
                }
                
                int reportTypeId = Convert.ToInt32(cboReportType.EditValue);
                
                // تحديد الجدول المناسب حسب نوع التقرير
                switch (reportTypeId)
                {
                    case 1: // تقرير موظفين
                        cboMainTable.EditValue = "Employees";
                        break;
                    case 2: // تقرير حضور
                        cboMainTable.EditValue = "Attendance";
                        break;
                    case 3: // تقرير إجازات
                        cboMainTable.EditValue = "LeaveRequests";
                        break;
                    case 4: // تقرير رواتب
                        cboMainTable.EditValue = "Salaries";
                        break;
                    case 5: // تقرير تحليلي
                        // لا نقوم بتغيير الجدول المحدد
                        break;
                    case 6: // تقرير مخصص
                        // لا نقوم بتغيير الجدول المحدد
                        break;
                }
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
        /// حدث تغيير الجدول الرئيسي
        /// </summary>
        private void cboMainTable_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboMainTable.EditValue == null)
                {
                    return;
                }
                
                string tableName = cboMainTable.EditValue.ToString();
                
                // تحميل حقول الجدول
                LoadTableFields(tableName);
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
        /// حدث النقر على زر إضافة حقل
        /// </summary>
        private void btnAddField_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود عنصر محدد
                if (lstAvailableFields.SelectedItems.Count == 0)
                {
                    return;
                }
                
                // إضافة الحقل المحدد إلى قائمة الحقول المحددة
                foreach (ListViewItem item in lstAvailableFields.SelectedItems)
                {
                    if (!_selectedFields.Contains(item.Text))
                    {
                        _selectedFields.Add(item.Text);
                        
                        ListViewItem newItem = new ListViewItem(item.Text);
                        newItem.SubItems.Add(item.SubItems[1].Text);
                        lstSelectedFields.Items.Add(newItem);
                    }
                }
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
        /// حدث النقر على زر إزالة حقل
        /// </summary>
        private void btnRemoveField_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود عنصر محدد
                if (lstSelectedFields.SelectedItems.Count == 0)
                {
                    return;
                }
                
                // إزالة الحقل المحدد من قائمة الحقول المحددة
                foreach (ListViewItem item in lstSelectedFields.SelectedItems)
                {
                    _selectedFields.Remove(item.Text);
                    lstSelectedFields.Items.Remove(item);
                }
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
        /// حدث النقر على زر تحريك لأعلى
        /// </summary>
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود عنصر محدد
                if (lstSelectedFields.SelectedItems.Count == 0)
                {
                    return;
                }
                
                ListViewItem selectedItem = lstSelectedFields.SelectedItems[0];
                int index = selectedItem.Index;
                
                // التحقق من إمكانية التحريك لأعلى
                if (index > 0)
                {
                    // إزالة العنصر من موقعه الحالي
                    lstSelectedFields.Items.RemoveAt(index);
                    
                    // إعادة إدراجه في الموقع الجديد
                    lstSelectedFields.Items.Insert(index - 1, selectedItem);
                    
                    // تحديث قائمة الحقول المحددة
                    _selectedFields.Clear();
                    foreach (ListViewItem item in lstSelectedFields.Items)
                    {
                        _selectedFields.Add(item.Text);
                    }
                    
                    // إعادة تحديد العنصر
                    lstSelectedFields.Items[index - 1].Selected = true;
                }
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
        /// حدث النقر على زر تحريك لأسفل
        /// </summary>
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود عنصر محدد
                if (lstSelectedFields.SelectedItems.Count == 0)
                {
                    return;
                }
                
                ListViewItem selectedItem = lstSelectedFields.SelectedItems[0];
                int index = selectedItem.Index;
                
                // التحقق من إمكانية التحريك لأسفل
                if (index < lstSelectedFields.Items.Count - 1)
                {
                    // إزالة العنصر من موقعه الحالي
                    lstSelectedFields.Items.RemoveAt(index);
                    
                    // إعادة إدراجه في الموقع الجديد
                    lstSelectedFields.Items.Insert(index + 1, selectedItem);
                    
                    // تحديث قائمة الحقول المحددة
                    _selectedFields.Clear();
                    foreach (ListViewItem item in lstSelectedFields.Items)
                    {
                        _selectedFields.Add(item.Text);
                    }
                    
                    // إعادة تحديد العنصر
                    lstSelectedFields.Items[index + 1].Selected = true;
                }
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
        /// حدث تغيير نوع الرسم البياني
        /// </summary>
        private void rgChartType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                // إعادة إنشاء الرسم البياني
                if (_reportData != null && cboChartCategory.Text != "" && cboChartValue.Text != "")
                {
                    CreateChart(_reportData, cboChartCategory.Text, cboChartValue.Text);
                }
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
            // عدم تنفيذ أي إجراء هنا
        }
        
        /// <summary>
        /// زر إنشاء التقرير
        /// </summary>
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // تحميل بيانات التقرير
                LoadReportData();
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
        /// زر إنشاء الرسم البياني
        /// </summary>
        private void btnCreateChart_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // التحقق من وجود بيانات
                if (_reportData == null || _reportData.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "لا توجد بيانات لإنشاء الرسم البياني. قم بإنشاء التقرير أولاً.",
                        "تنبيه",
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                    return;
                }
                
                // التحقق من اختيار حقول الرسم البياني
                if (string.IsNullOrEmpty(cboChartCategory.Text) || string.IsNullOrEmpty(cboChartValue.Text))
                {
                    XtraMessageBox.Show(
                        "يرجى اختيار حقول التصنيف والقيمة للرسم البياني.",
                        "تنبيه",
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                    return;
                }
                
                // إنشاء الرسم البياني
                CreateChart(_reportData, cboChartCategory.Text, cboChartValue.Text);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء إنشاء الرسم البياني: {ex.Message}",
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
        /// زر طباعة التقرير
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود بيانات
                if (gridControl.DataSource == null)
                {
                    XtraMessageBox.Show(
                        "لا توجد بيانات للطباعة. قم بإنشاء التقرير أولاً.",
                        "تنبيه",
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                    return;
                }
                
                // طباعة التقرير
                GridView view = gridControl.MainView as GridView;
                view.ShowRibbonPrintPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء طباعة التقرير: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// زر تصدير البيانات
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود بيانات
                if (gridControl.DataSource == null)
                {
                    XtraMessageBox.Show(
                        "لا توجد بيانات للتصدير. قم بإنشاء التقرير أولاً.",
                        "تنبيه",
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                    return;
                }
                
                // عرض قائمة التصدير
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
                saveDialog.FileName = $"تقرير {cboMainTable.Text}.xlsx";
                
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
                saveDialog.FileName = $"تقرير {cboMainTable.Text}.pdf";
                
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
        /// زر إغلاق الرسم البياني
        /// </summary>
        private void btnCloseChart_Click(object sender, EventArgs e)
        {
            // إخفاء الرسم البياني
            splitContainerControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
        }
        
        #endregion
    }
}