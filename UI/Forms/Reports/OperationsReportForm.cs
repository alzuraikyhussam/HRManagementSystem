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
    /// نموذج تقرير العمليات والمخالفات
    /// </summary>
    public partial class OperationsReportForm : XtraForm
    {
        private readonly ConnectionManager _connectionManager;
        
        public OperationsReportForm()
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
                dtStartDate.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtEndDate.DateTime = DateTime.Now;
                
                // تحميل قائمة الإدارات
                LoadDepartments();
                
                // تحميل قائمة الموظفين
                LoadEmployees();
                
                // تحميل قائمة أنواع العمليات
                LoadOperationTypes();
                
                // ربط الأحداث
                cboDepartment.EditValueChanged += cboDepartment_EditValueChanged;
                cboEmployee.EditValueChanged += filter_ValueChanged;
                cboOperationType.EditValueChanged += filter_ValueChanged;
                dtStartDate.EditValueChanged += filter_ValueChanged;
                dtEndDate.EditValueChanged += filter_ValueChanged;
                rgStatus.EditValueChanged += filter_ValueChanged;
                rgSeverity.EditValueChanged += filter_ValueChanged;
                
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
        /// تحميل قائمة أنواع العمليات
        /// </summary>
        private void LoadOperationTypes()
        {
            try
            {
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    // استعلام لجلب أنواع العمليات من جدول DeductionRules أو ActivityLog بحسب بنية قاعدة البيانات الفعلية
                    // في هذه الحالة نستخدم DeductionRules لقواعد الخصومات والجزاءات
                    string query = @"
                    SELECT 
                        ID, 
                        Name 
                    FROM DeductionRules
                    ORDER BY Name";
                    
                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        DataTable dt = new DataTable();
                        
                        try
                        {
                            var adapter = new System.Data.SqlClient.SqlDataAdapter(command);
                            adapter.Fill(dt);
                        }
                        catch
                        {
                            // في حالة عدم وجود الجدول، نقوم بقراءة أنواع من جدول ActivityLog
                            dt = CreateOperationTypesFromActivityLog(connection);
                        }
                        
                        // إضافة خيار "كل الأنواع"
                        DataRow allRow = dt.NewRow();
                        allRow["ID"] = 0;
                        allRow["Name"] = "كل الأنواع";
                        dt.Rows.InsertAt(allRow, 0);
                        
                        cboOperationType.Properties.DataSource = dt;
                        cboOperationType.Properties.DisplayMember = "Name";
                        cboOperationType.Properties.ValueMember = "ID";
                        cboOperationType.Properties.PopulateColumns();
                        cboOperationType.Properties.Columns["ID"].Visible = false;
                        cboOperationType.EditValue = 0; // تعيين "كل الأنواع" كخيار افتراضي
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تحميل قائمة أنواع العمليات: {ex.Message}");
            }
        }
        
        /// <summary>
        /// قراءة أنواع العمليات من جدول ActivityLog
        /// </summary>
        private DataTable CreateOperationTypesFromActivityLog(System.Data.SqlClient.SqlConnection connection)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            
            try
            {
                // محاولة قراءة أنواع النشاط من جدول ActivityLog
                string query = @"
                SELECT DISTINCT
                    ROW_NUMBER() OVER (ORDER BY ActivityType) AS ID,
                    ActivityType AS Name
                FROM ActivityLog
                WHERE ActivityType IS NOT NULL";
                
                using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        int id = 1;
                        while (reader.Read())
                        {
                            string activityType = reader["Name"].ToString();
                            dt.Rows.Add(id++, activityType);
                        }
                    }
                }
            }
            catch
            {
                // في حالة فشل قراءة البيانات، نضيف قائمة افتراضية
                dt.Rows.Add(1, "مخالفة تأخير");
                dt.Rows.Add(2, "مخالفة غياب");
                dt.Rows.Add(3, "مخالفة سلوكية");
                dt.Rows.Add(4, "مخالفة أداء");
                dt.Rows.Add(5, "مخالفة إنتاجية");
                dt.Rows.Add(6, "تحذير");
                dt.Rows.Add(7, "لفت نظر");
                dt.Rows.Add(8, "إنذار");
            }
            
            return dt;
        }
        
        /// <summary>
        /// إنشاء قائمة بأنواع المخالفات من جدول DeductionRules
        /// </summary>
        private DataTable CreateDeductionTypes()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            
            dt.Rows.Add(1, "مخالفة تأخير");
            dt.Rows.Add(2, "مخالفة غياب");
            dt.Rows.Add(3, "مخالفة سلوكية");
            dt.Rows.Add(4, "مخالفة أداء");
            dt.Rows.Add(5, "مخالفة إنتاجية");
            dt.Rows.Add(6, "تحذير");
            dt.Rows.Add(7, "لفت نظر");
            dt.Rows.Add(8, "إنذار");
            
            return dt;
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
                view.OptionsPrint.RtfPageHeader = "تقرير العمليات والمخالفات";
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
                    
                    // بناء جملة استعلام للعمليات والمخالفات
                    // هذا استعلام افتراضي، يجب تعديله حسب بنية قاعدة البيانات الفعلية
                    string query = "";
                    
                    try
                    {
                        // التحقق من وجود جدول الخصومات
                        string checkQuery = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Deductions'";
                        using (var checkCommand = new System.Data.SqlClient.SqlCommand(checkQuery, connection))
                        {
                            int deductionsExists = (int)checkCommand.ExecuteScalar();
                            
                            if (deductionsExists > 0)
                            {
                                // بناء استعلام من جدول الخصومات وجدول ActivityLog
                                query = BuildDeductionsAndActivityQuery(filter);
                            }
                            else
                            {
                                // استخدام سجل النشاطات فقط
                                query = BuildActivityLogQuery(filter);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(
                            $"حدث خطأ أثناء التحقق من وجود جداول التقرير: {ex.Message}",
                            "خطأ",
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Error);
                            
                        // استخدام استعلام فقط على جدول سجل النشاطات
                        query = BuildActivityLogQuery(filter);
                    }
                    
                    // تنفيذ الاستعلام الحقيقي
                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        // إضافة البارامترات
                        AddFilterParameters(command, filter);
                        
                        // تنفيذ الاستعلام وعرض البيانات
                        DataTable resultTable = new DataTable();
                        var adapter = new System.Data.SqlClient.SqlDataAdapter(command);
                        adapter.Fill(resultTable);
                        
                        DisplayReportData(resultTable);
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
        /// عرض بيانات التقرير في الجدول
        /// </summary>
        private void DisplayReportData(DataTable resultTable)
        {
            try
            {
                // تعيين البيانات للجدول
                gridControl.DataSource = resultTable;
                
                // تنسيق متقدم للأعمدة
                GridView view = gridControl.MainView as GridView;
                
                // تنسيق الأعمدة
                ConfigureColumn(view, "OperationDate", "التاريخ", 90, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
                ConfigureColumn(view, "EmployeeNumber", "رقم الموظف", 80, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None);
                ConfigureColumn(view, "EmployeeName", "اسم الموظف", 180, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
                ConfigureColumn(view, "DepartmentName", "الإدارة", 140, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
                ConfigureColumn(view, "OperationType", "نوع العملية", 120, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
                ConfigureColumn(view, "Description", "الوصف", 250, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None);
                ConfigureColumn(view, "SeverityText", "مستوى الخطورة", 100, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
                ConfigureColumn(view, "StatusText", "الحالة", 90, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
                ConfigureColumn(view, "CreatedBy", "تم بواسطة", 150, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None);
                ConfigureColumn(view, "CreatedDate", "تاريخ الإنشاء", 110, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None);
                ConfigureColumn(view, "Notes", "ملاحظات", 200, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None);
                
                // إخفاء الأعمدة غير المطلوبة
                HideColumn(view, "ID");
                HideColumn(view, "EmployeeID");
                HideColumn(view, "OperationTypeID");
                HideColumn(view, "Severity");
                HideColumn(view, "Status");
                
                // إعداد الإجماليات في الذيل
                // عدد السجلات
                view.Columns["EmployeeNumber"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                view.Columns["EmployeeNumber"].SummaryItem.DisplayFormat = "العدد: {0}";
                
                // إضافة ملخص للتاريخ
                if (view.Columns["OperationDate"] != null)
                {
                    // إعداد تجميع البيانات حسب التاريخ
                    view.Columns["OperationDate"].Group();
                    
                    // إضافة ملخص عدد المخالفات لكل يوم
                    view.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "EmployeeNumber", view.Columns["EmployeeNumber"], "العدد: {0}");
                }
                
                // إضافة تلوين حسب مستوى الخطورة
                DevExpress.XtraGrid.StyleFormatCondition highCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                highCondition.Appearance.ForeColor = Color.Red;
                highCondition.Appearance.BackColor = Color.FromArgb(255, 235, 235);
                highCondition.Appearance.Options.UseForeColor = true;
                highCondition.Appearance.Options.UseBackColor = true;
                highCondition.ApplyToRow = true;
                highCondition.Column = view.Columns["SeverityText"];
                highCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
                highCondition.Value1 = "مرتفع";
                view.FormatConditions.Add(highCondition);
                
                DevExpress.XtraGrid.StyleFormatCondition mediumCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                mediumCondition.Appearance.ForeColor = Color.FromArgb(230, 126, 34); // لون برتقالي
                mediumCondition.Appearance.BackColor = Color.FromArgb(255, 248, 240);
                mediumCondition.Appearance.Options.UseForeColor = true;
                mediumCondition.Appearance.Options.UseBackColor = true;
                mediumCondition.ApplyToRow = true;
                mediumCondition.Column = view.Columns["SeverityText"];
                mediumCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
                mediumCondition.Value1 = "متوسط";
                view.FormatConditions.Add(mediumCondition);
                
                DevExpress.XtraGrid.StyleFormatCondition lowCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                lowCondition.Appearance.ForeColor = Color.Green;
                lowCondition.Appearance.BackColor = Color.FromArgb(240, 255, 240);
                lowCondition.Appearance.Options.UseForeColor = true;
                lowCondition.Appearance.Options.UseBackColor = true;
                lowCondition.ApplyToRow = true;
                lowCondition.Column = view.Columns["SeverityText"];
                lowCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
                lowCondition.Value1 = "منخفض";
                view.FormatConditions.Add(lowCondition);
                
                // إضافة تلوين حسب الحالة
                DevExpress.XtraGrid.StyleFormatCondition openCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                openCondition.Appearance.ForeColor = Color.Blue;
                openCondition.Appearance.Options.UseForeColor = true;
                openCondition.Column = view.Columns["StatusText"];
                openCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
                openCondition.Value1 = "مفتوح";
                view.FormatConditions.Add(openCondition);
                
                DevExpress.XtraGrid.StyleFormatCondition closedCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                closedCondition.Appearance.ForeColor = Color.DarkGray;
                closedCondition.Appearance.Options.UseForeColor = true;
                closedCondition.Column = view.Columns["StatusText"];
                closedCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
                closedCondition.Value1 = "مغلق";
                view.FormatConditions.Add(closedCondition);
                
                // تحديث عنوان التقرير بعدد النتائج
                lblReportTitle.Text = $"تقرير العمليات والمخالفات - عدد النتائج: {resultTable.Rows.Count}";
                
                // إحصائيات سريعة
                int totalCount = resultTable.Rows.Count;
                int highCount = CountBySeverity(resultTable, "مرتفع");
                int mediumCount = CountBySeverity(resultTable, "متوسط");
                int lowCount = CountBySeverity(resultTable, "منخفض");
                
                lblStatistics.Text = $"إجمالي: {totalCount} | مرتفع: {highCount} | متوسط: {mediumCount} | منخفض: {lowCount}";
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في عرض بيانات التقرير: {ex.Message}");
            }
        }
        
        /// <summary>
        /// حساب عدد العمليات حسب مستوى الخطورة
        /// </summary>
        private int CountBySeverity(DataTable table, string severityText)
        {
            return table.AsEnumerable().Count(r => r.Field<string>("SeverityText") == severityText);
        }
        
        /// <summary>
        /// بناء استعلام للخصومات وسجل النشاطات معًا
        /// </summary>
        private string BuildDeductionsAndActivityQuery(ReportFilterOptions filter)
        {
            string whereClause = BuildWhereClause(filter);
            
            // جزء استعلام الخصومات
            string deductionsQuery = $@"
            -- الاستعلام عن جدول الخصومات
            SELECT 
                D.ID,
                D.EmployeeID,
                E.EmployeeNumber,
                E.FullName AS EmployeeName,
                DEPT.Name AS DepartmentName,
                D.ViolationDate AS OperationDate,
                CONVERT(varchar(10), D.ViolationDate, 103) AS OperationDateFormatted,
                D.DeductionRuleID AS OperationTypeID,
                DR.Name AS OperationType,
                D.Description,
                CASE 
                    WHEN DR.DeductionValue <= 1 THEN 1
                    WHEN DR.DeductionValue <= 3 THEN 2
                    ELSE 3
                END AS Severity,
                CASE 
                    WHEN DR.DeductionValue <= 1 THEN 'منخفض'
                    WHEN DR.DeductionValue <= 3 THEN 'متوسط'
                    ELSE 'مرتفع'
                END AS SeverityText,
                CASE 
                    WHEN D.Status = 'Pending' THEN 1
                    ELSE 0
                END AS Status,
                CASE 
                    WHEN D.Status = 'Pending' THEN 'مفتوح'
                    ELSE 'مغلق'
                END AS StatusText,
                U.FullName AS CreatedBy,
                D.CreatedAt AS CreatedDate,
                CONVERT(varchar(10), D.CreatedAt, 103) AS CreatedDateFormatted,
                D.Notes
            FROM Deductions D
            INNER JOIN Employees E ON D.EmployeeID = E.ID
            INNER JOIN Departments DEPT ON E.DepartmentID = DEPT.ID
            INNER JOIN DeductionRules DR ON D.DeductionRuleID = DR.ID
            LEFT JOIN Users U ON D.CreatedBy = U.ID
            WHERE D.ViolationDate BETWEEN @StartDate AND @EndDate
            ";
            
            // إضافة جزء استعلام ActivityLog
            string activityLogQuery = BuildActivityLogQuery(filter);
            
            // دمج الاستعلامين
            return $@"{deductionsQuery}
            
            UNION ALL
            
            {activityLogQuery}
            
            ORDER BY OperationDate DESC, EmployeeName";
        }
        
        /// <summary>
        /// بناء استعلام لسجل النشاطات
        /// </summary>
        private string BuildActivityLogQuery(ReportFilterOptions filter)
        {
            return $@"
            -- الاستعلام عن جدول سجل النشاطات
            SELECT 
                A.ID,
                U.EmployeeID AS EmployeeID,
                ISNULL(E.EmployeeNumber, 'NA') AS EmployeeNumber,
                ISNULL(E.FullName, U.FullName) AS EmployeeName,
                ISNULL(DEPT.Name, 'غير محدد') AS DepartmentName,
                A.ActivityDate AS OperationDate,
                CONVERT(varchar(10), A.ActivityDate, 103) AS OperationDateFormatted,
                0 AS OperationTypeID,
                A.ActivityType AS OperationType,
                A.Description,
                CASE 
                    WHEN A.ActivityType LIKE '%حذف%' OR A.ActivityType LIKE '%خطأ%' THEN 3
                    WHEN A.ActivityType LIKE '%تعديل%' THEN 2
                    ELSE 1
                END AS Severity,
                CASE 
                    WHEN A.ActivityType LIKE '%حذف%' OR A.ActivityType LIKE '%خطأ%' THEN 'مرتفع'
                    WHEN A.ActivityType LIKE '%تعديل%' THEN 'متوسط'
                    ELSE 'منخفض'
                END AS SeverityText,
                0 AS Status,
                'مغلق' AS StatusText,
                U.FullName AS CreatedBy,
                A.ActivityDate AS CreatedDate,
                CONVERT(varchar(10), A.ActivityDate, 103) AS CreatedDateFormatted,
                A.ModuleName AS Notes
            FROM ActivityLog A
            INNER JOIN Users U ON A.UserID = U.ID
            LEFT JOIN Employees E ON U.EmployeeID = E.ID
            LEFT JOIN Departments DEPT ON E.DepartmentID = DEPT.ID
            WHERE A.ActivityDate BETWEEN @StartDate AND @EndDate
            ";
        }
        
        /// <summary>
        /// إنشاء بيانات وهمية للعمليات والمخالفات للعرض
        /// </summary>
        private DataTable CreateDummyOperationsData(ReportFilterOptions filter)
        {
            DataTable dt = new DataTable();
            
            // إنشاء هيكل الجدول
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("EmployeeID", typeof(int));
            dt.Columns.Add("EmployeeNumber", typeof(string));
            dt.Columns.Add("EmployeeName", typeof(string));
            dt.Columns.Add("DepartmentName", typeof(string));
            dt.Columns.Add("OperationDate", typeof(string));
            dt.Columns.Add("OperationTypeID", typeof(int));
            dt.Columns.Add("OperationType", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Severity", typeof(int));
            dt.Columns.Add("SeverityText", typeof(string));
            dt.Columns.Add("Status", typeof(int));
            dt.Columns.Add("StatusText", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));
            dt.Columns.Add("CreatedDate", typeof(string));
            dt.Columns.Add("Notes", typeof(string));
            
            // إضافة بيانات نموذجية
            AddDummyOperationRow(dt, 1, 1, "EMP001", "أحمد محمد علي", "الإدارة المالية", "2023/05/15", 1, "مخالفة تأخير", "تأخر عن الدوام لمدة ساعة بدون عذر", 2, "متوسط", 1, "مفتوح", "مدير النظام", "2023/05/15", "تكرر التأخير أكثر من مرة خلال الشهر");
            AddDummyOperationRow(dt, 2, 2, "EMP002", "محمد علي حسن", "إدارة الموارد البشرية", "2023/05/16", 2, "مخالفة غياب", "غياب بدون إذن مسبق", 3, "مرتفع", 1, "مفتوح", "مدير النظام", "2023/05/16", "يجب متابعة الحالة مع المدير المباشر");
            AddDummyOperationRow(dt, 3, 3, "EMP003", "فاطمة أحمد محمود", "إدارة تقنية المعلومات", "2023/05/17", 3, "مخالفة سلوكية", "مشادة كلامية مع زميل في العمل", 3, "مرتفع", 0, "مغلق", "مدير النظام", "2023/05/17", "تم حل المشكلة وتقديم اعتذار رسمي");
            AddDummyOperationRow(dt, 4, 4, "EMP004", "خالد محمد سعيد", "إدارة المبيعات", "2023/05/18", 6, "تحذير", "عدم الالتزام بمواعيد تسليم التقارير", 1, "منخفض", 0, "مغلق", "مدير النظام", "2023/05/18", "تم تقديم تعهد بالالتزام");
            AddDummyOperationRow(dt, 5, 5, "EMP005", "سارة علي محمد", "إدارة التسويق", "2023/05/19", 7, "لفت نظر", "عدم ارتداء الزي الرسمي", 1, "منخفض", 0, "مغلق", "مدير النظام", "2023/05/19", "تم الالتزام بعد لفت النظر");
            AddDummyOperationRow(dt, 6, 1, "EMP001", "أحمد محمد علي", "الإدارة المالية", "2023/05/20", 8, "إنذار", "الخروج قبل نهاية الدوام بدون إذن", 2, "متوسط", 1, "مفتوح", "مدير النظام", "2023/05/20", "إنذار أول");
            AddDummyOperationRow(dt, 7, 2, "EMP002", "محمد علي حسن", "إدارة الموارد البشرية", "2023/05/21", 4, "مخالفة أداء", "انخفاض مستوى الأداء في المهام المكلف بها", 2, "متوسط", 1, "مفتوح", "مدير النظام", "2023/05/21", "يحتاج إلى دورة تدريبية");
            AddDummyOperationRow(dt, 8, 6, "EMP006", "عمر خالد سعيد", "إدارة المشتريات", "2023/05/22", 5, "مخالفة إنتاجية", "عدم تحقيق المستهدف الشهري", 3, "مرتفع", 1, "مفتوح", "مدير النظام", "2023/05/22", "يجب عقد اجتماع لمناقشة الأسباب");
            
            // تطبيق فلتر الإدارة إذا تم تحديده
            if (filter.DepartmentId.HasValue && filter.DepartmentId.Value > 0)
            {
                string departmentName = GetDepartmentName(filter.DepartmentId.Value);
                if (!string.IsNullOrEmpty(departmentName))
                {
                    DataTable filteredDt = dt.Clone();
                    foreach (DataRow row in dt.Select($"DepartmentName = '{departmentName}'"))
                    {
                        filteredDt.ImportRow(row);
                    }
                    dt = filteredDt;
                }
            }
            
            // تطبيق فلتر الموظف إذا تم تحديده
            if (filter.EmployeeId.HasValue && filter.EmployeeId.Value > 0)
            {
                string employeeName = GetEmployeeName(filter.EmployeeId.Value);
                if (!string.IsNullOrEmpty(employeeName))
                {
                    DataTable filteredDt = dt.Clone();
                    foreach (DataRow row in dt.Select($"EmployeeName = '{employeeName}'"))
                    {
                        filteredDt.ImportRow(row);
                    }
                    dt = filteredDt;
                }
            }
            
            // تطبيق فلتر نوع العملية إذا تم تحديده
            if (filter.OperationTypeId.HasValue && filter.OperationTypeId.Value > 0)
            {
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in dt.Select($"OperationTypeID = {filter.OperationTypeId.Value}"))
                {
                    filteredDt.ImportRow(row);
                }
                dt = filteredDt;
            }
            
            // تطبيق فلتر مستوى الخطورة إذا تم تحديده
            if (filter.Severity.HasValue)
            {
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in dt.Select($"Severity = {filter.Severity.Value}"))
                {
                    filteredDt.ImportRow(row);
                }
                dt = filteredDt;
            }
            
            // تطبيق فلتر الحالة إذا تم تحديده
            if (filter.Status.HasValue)
            {
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in dt.Select($"Status = {filter.Status.Value}"))
                {
                    filteredDt.ImportRow(row);
                }
                dt = filteredDt;
            }
            
            return dt;
        }
        
        /// <summary>
        /// إضافة صف بيانات وهمية
        /// </summary>
        private void AddDummyOperationRow(DataTable dt, int id, int employeeId, string employeeNumber, string employeeName, string departmentName, string operationDate, int operationTypeId, string operationType, string description, int severity, string severityText, int status, string statusText, string createdBy, string createdDate, string notes)
        {
            DataRow row = dt.NewRow();
            row["ID"] = id;
            row["EmployeeID"] = employeeId;
            row["EmployeeNumber"] = employeeNumber;
            row["EmployeeName"] = employeeName;
            row["DepartmentName"] = departmentName;
            row["OperationDate"] = operationDate;
            row["OperationTypeID"] = operationTypeId;
            row["OperationType"] = operationType;
            row["Description"] = description;
            row["Severity"] = severity;
            row["SeverityText"] = severityText;
            row["Status"] = status;
            row["StatusText"] = statusText;
            row["CreatedBy"] = createdBy;
            row["CreatedDate"] = createdDate;
            row["Notes"] = notes;
            dt.Rows.Add(row);
        }
        
        /// <summary>
        /// الحصول على اسم الإدارة من خلال المعرف
        /// </summary>
        private string GetDepartmentName(int departmentId)
        {
            try
            {
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT Name FROM Departments WHERE ID = @DepartmentId";
                    
                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DepartmentId", departmentId);
                        var result = command.ExecuteScalar();
                        return result != null ? result.ToString() : "";
                    }
                }
            }
            catch
            {
                // في حالة وجود مشكلة، نعيد أسماء إدارات وهمية حسب المعرف
                switch (departmentId)
                {
                    case 1: return "الإدارة المالية";
                    case 2: return "إدارة الموارد البشرية";
                    case 3: return "إدارة تقنية المعلومات";
                    case 4: return "إدارة المبيعات";
                    case 5: return "إدارة التسويق";
                    case 6: return "إدارة المشتريات";
                    default: return "";
                }
            }
        }
        
        /// <summary>
        /// الحصول على اسم الموظف من خلال المعرف
        /// </summary>
        private string GetEmployeeName(int employeeId)
        {
            try
            {
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT FullName FROM Employees WHERE ID = @EmployeeId";
                    
                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);
                        var result = command.ExecuteScalar();
                        return result != null ? result.ToString() : "";
                    }
                }
            }
            catch
            {
                // في حالة وجود مشكلة، نعيد أسماء موظفين وهمية حسب المعرف
                switch (employeeId)
                {
                    case 1: return "أحمد محمد علي";
                    case 2: return "محمد علي حسن";
                    case 3: return "فاطمة أحمد محمود";
                    case 4: return "خالد محمد سعيد";
                    case 5: return "سارة علي محمد";
                    case 6: return "عمر خالد سعيد";
                    default: return "";
                }
            }
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
            
            // تعيين نوع العملية
            if (cboOperationType.EditValue != null && Convert.ToInt32(cboOperationType.EditValue) > 0)
            {
                filter.OperationTypeId = Convert.ToInt32(cboOperationType.EditValue);
            }
            
            // تعيين مستوى الخطورة
            if (rgSeverity.SelectedIndex >= 0 && rgSeverity.SelectedIndex < 3)
            {
                filter.Severity = rgSeverity.SelectedIndex + 1; // 1: منخفض، 2: متوسط، 3: مرتفع
            }
            
            // تعيين الحالة
            if (rgStatus.SelectedIndex >= 0 && rgStatus.SelectedIndex < 2)
            {
                filter.Status = rgStatus.SelectedIndex == 0 ? 1 : 0; // 1: مفتوح، 0: مغلق
            }
            
            return filter;
        }
        
        /// <summary>
        /// بناء شرط WHERE للاستعلام
        /// </summary>
        private string BuildWhereClause(ReportFilterOptions filter)
        {
            List<string> activityConditions = new List<string>();
            List<string> deductionConditions = new List<string>();
            
            // شرط الإدارة للنشاطات
            if (filter.DepartmentId.HasValue && filter.DepartmentId.Value > 0)
            {
                activityConditions.Add("DEPT.ID = @DepartmentId");
                deductionConditions.Add("DEPT.ID = @DepartmentId");
            }
            
            // شرط الموظف للنشاطات
            if (filter.EmployeeId.HasValue && filter.EmployeeId.Value > 0)
            {
                activityConditions.Add("E.ID = @EmployeeId");
                deductionConditions.Add("E.ID = @EmployeeId");
            }
            
            // شرط نوع العملية
            if (filter.OperationTypeId.HasValue && filter.OperationTypeId.Value > 0)
            {
                // هذا يطبق فقط على جدول الخصومات
                deductionConditions.Add("DR.ID = @OperationTypeId");
            }
            
            // شرط مستوى الخطورة
            if (filter.Severity.HasValue)
            {
                // تطبيق على النشاطات
                if (filter.Severity.Value == 1)
                    activityConditions.Add("A.ActivityType NOT LIKE '%حذف%' AND A.ActivityType NOT LIKE '%خطأ%' AND A.ActivityType NOT LIKE '%تعديل%'");
                else if (filter.Severity.Value == 2)
                    activityConditions.Add("A.ActivityType LIKE '%تعديل%'");
                else if (filter.Severity.Value == 3)
                    activityConditions.Add("(A.ActivityType LIKE '%حذف%' OR A.ActivityType LIKE '%خطأ%')");
                
                // تطبيق على الخصومات
                if (filter.Severity.Value == 1)
                    deductionConditions.Add("DR.DeductionValue <= 1");
                else if (filter.Severity.Value == 2)
                    deductionConditions.Add("DR.DeductionValue > 1 AND DR.DeductionValue <= 3");
                else if (filter.Severity.Value == 3)
                    deductionConditions.Add("DR.DeductionValue > 3");
            }
            
            // شرط الحالة
            if (filter.Status.HasValue)
            {
                // هذا يطبق فقط على جدول الخصومات
                if (filter.Status.Value == 1)
                    deductionConditions.Add("D.Status = 'Pending'");
                else
                    deductionConditions.Add("D.Status != 'Pending'");
            }
            
            // لن نستخدم هذه الدالة للجمل حيث تم دمج الشروط في استعلامات BuildDeductionsAndActivityQuery و BuildActivityLogQuery
            return string.Empty;
        }
        
        /// <summary>
        /// إضافة بارامترات الاستعلام
        /// </summary>
        private void AddFilterParameters(System.Data.SqlClient.SqlCommand command, ReportFilterOptions filter)
        {
            // بارامترات الفترة الزمنية
            command.Parameters.AddWithValue("@StartDate", filter.StartDate);
            command.Parameters.AddWithValue("@EndDate", filter.EndDate.AddDays(1).AddSeconds(-1)); // حتى نهاية اليوم المحدد
            
            // بارامترات الإدارة والموظف
            if (filter.DepartmentId.HasValue && filter.DepartmentId.Value > 0)
            {
                command.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId.Value);
            }
            
            if (filter.EmployeeId.HasValue && filter.EmployeeId.Value > 0)
            {
                command.Parameters.AddWithValue("@EmployeeId", filter.EmployeeId.Value);
            }
            
            // بارامتر نوع العملية
            if (filter.OperationTypeId.HasValue && filter.OperationTypeId.Value > 0)
            {
                command.Parameters.AddWithValue("@OperationTypeId", filter.OperationTypeId.Value);
            }
            
            // بارامتر مستوى الخطورة
            if (filter.Severity.HasValue)
            {
                command.Parameters.AddWithValue("@Severity", filter.Severity.Value);
            }
            
            // بارامتر الحالة
            if (filter.Status.HasValue)
            {
                command.Parameters.AddWithValue("@Status", filter.Status.Value);
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
            view.OptionsPrint.RtfPageHeader = $"تقرير العمليات والمخالفات - {DateTime.Now.ToString("yyyy/MM/dd")}";
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
                saveDialog.FileName = "تقرير العمليات والمخالفات.xlsx";
                
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
                saveDialog.FileName = "تقرير العمليات والمخالفات.pdf";
                
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
            public int? OperationTypeId { get; set; }
            public int? Severity { get; set; }
            public int? Status { get; set; }
        }
    }
}