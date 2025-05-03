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
    /// نموذج تقرير الإجازات
    /// </summary>
    public partial class LeaveReportForm : XtraForm
    {
        private readonly ConnectionManager _connectionManager;
        
        public LeaveReportForm()
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
                
                // تحميل أنواع الإجازات
                LoadLeaveTypes();
                
                // ربط الأحداث
                cboDepartment.EditValueChanged += cboDepartment_EditValueChanged;
                cboEmployee.EditValueChanged += filter_ValueChanged;
                cboLeaveType.EditValueChanged += filter_ValueChanged;
                dtStartDate.EditValueChanged += filter_ValueChanged;
                dtEndDate.EditValueChanged += filter_ValueChanged;
                rgStatus.EditValueChanged += filter_ValueChanged;
                
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
        /// تحميل قائمة أنواع الإجازات
        /// </summary>
        private void LoadLeaveTypes()
        {
            try
            {
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT ID, Name FROM LeaveTypes ORDER BY Name";
                    
                    using (var adapter = new System.Data.SqlClient.SqlDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        // إضافة خيار "كل أنواع الإجازات"
                        DataRow allRow = dt.NewRow();
                        allRow["ID"] = 0;
                        allRow["Name"] = "كل أنواع الإجازات";
                        dt.Rows.InsertAt(allRow, 0);
                        
                        cboLeaveType.Properties.DataSource = dt;
                        cboLeaveType.Properties.DisplayMember = "Name";
                        cboLeaveType.Properties.ValueMember = "ID";
                        cboLeaveType.Properties.PopulateColumns();
                        cboLeaveType.Properties.Columns["ID"].Visible = false;
                        cboLeaveType.EditValue = 0; // تعيين "كل أنواع الإجازات" كخيار افتراضي
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تحميل قائمة أنواع الإجازات: {ex.Message}");
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
                view.OptionsPrint.RtfPageHeader = "تقرير الإجازات";
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
                    string whereClause = BuildWhereClause(filter);
                    
                    // استعلام البيانات الرئيسي
                    string query = $@"
                        SELECT 
                            LR.ID,
                            LR.RequestDate,
                            CONVERT(varchar(10), LR.RequestDate, 103) AS RequestDateFormatted,
                            E.ID AS EmployeeID,
                            E.EmployeeNumber,
                            E.FullName,
                            D.Name AS DepartmentName,
                            LT.Name AS LeaveTypeName,
                            LR.StartDate,
                            CONVERT(varchar(10), LR.StartDate, 103) AS StartDateFormatted,
                            LR.EndDate,
                            CONVERT(varchar(10), LR.EndDate, 103) AS EndDateFormatted,
                            LR.Duration,
                            LR.Comments,
                            LR.Status,
                            CASE 
                                WHEN LR.Status = 1 THEN 'معتمدة'
                                WHEN LR.Status = 2 THEN 'مرفوضة'
                                ELSE 'قيد الانتظار'
                            END AS StatusText,
                            LR.ApprovalDate,
                            CASE WHEN LR.ApprovalDate IS NOT NULL THEN CONVERT(varchar(10), LR.ApprovalDate, 103) ELSE '' END AS ApprovalDateFormatted,
                            LR.ApprovedBy,
                            A.FullName AS ApproverName,
                            LR.ApprovalComments
                        FROM LeaveRequests LR
                        INNER JOIN Employees E ON LR.EmployeeID = E.ID
                        INNER JOIN Departments D ON E.DepartmentID = D.ID
                        INNER JOIN LeaveTypes LT ON LR.LeaveTypeID = LT.ID
                        LEFT JOIN Employees A ON LR.ApprovedBy = A.ID
                        {whereClause}
                        ORDER BY LR.RequestDate DESC, E.FullName";
                    
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
                        
                        // تنسيق متقدم للأعمدة
                        ConfigureColumn(view, "RequestDateFormatted", "تاريخ الطلب", 90, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
                        ConfigureColumn(view, "EmployeeNumber", "رقم الموظف", 80, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None);
                        ConfigureColumn(view, "FullName", "اسم الموظف", 180, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
                        ConfigureColumn(view, "DepartmentName", "الإدارة", 140, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
                        ConfigureColumn(view, "LeaveTypeName", "نوع الإجازة", 120, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
                        ConfigureColumn(view, "StartDateFormatted", "تاريخ البداية", 100, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
                        ConfigureColumn(view, "EndDateFormatted", "تاريخ النهاية", 100, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, false);
                        ConfigureColumn(view, "Duration", "المدة (أيام)", 80, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.Numeric, true, false, "n0");
                        ConfigureColumn(view, "Comments", "ملاحظات الموظف", 180, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None);
                        ConfigureColumn(view, "StatusText", "الحالة", 80, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None, true, true);
                        ConfigureColumn(view, "ApprovalDateFormatted", "تاريخ الرد", 100, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None);
                        ConfigureColumn(view, "ApproverName", "معتمد بواسطة", 150, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None);
                        ConfigureColumn(view, "ApprovalComments", "ملاحظات الاعتماد", 180, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.None);
                        
                        // إخفاء الأعمدة غير المطلوبة
                        HideColumn(view, "ID");
                        HideColumn(view, "EmployeeID");
                        HideColumn(view, "RequestDate");
                        HideColumn(view, "StartDate");
                        HideColumn(view, "EndDate");
                        HideColumn(view, "Status");
                        HideColumn(view, "ApprovalDate");
                        HideColumn(view, "ApprovedBy");
                        
                        // إعداد الإجماليات في الذيل
                        // عدد الطلبات
                        view.Columns["EmployeeNumber"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        view.Columns["EmployeeNumber"].SummaryItem.DisplayFormat = "عدد الطلبات: {0}";
                        
                        // إجمالي أيام الإجازات
                        view.Columns["Duration"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        view.Columns["Duration"].SummaryItem.DisplayFormat = "المجموع: {0:n0} يوم";
                        
                        // إضافة ملخص للتاريخ
                        if (view.Columns["RequestDateFormatted"] != null)
                        {
                            // إعداد تجميع البيانات حسب نوع الإجازة
                            view.Columns["LeaveTypeName"].Group();
                            
                            // إضافة ملخص عدد الطلبات لكل نوع
                            view.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "EmployeeNumber", view.Columns["EmployeeNumber"], "العدد: {0}");
                            
                            // إضافة ملخص إجمالي أيام الإجازات لكل نوع
                            view.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Duration", view.Columns["Duration"], "المدة: {0:n0} يوم");
                        }
                        
                        // إضافة تلوين حسب حالة الطلب
                        DevExpress.XtraGrid.StyleFormatCondition approvedCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                        approvedCondition.Appearance.ForeColor = Color.Green;
                        approvedCondition.Appearance.BackColor = Color.FromArgb(240, 255, 240);
                        approvedCondition.Appearance.Options.UseForeColor = true;
                        approvedCondition.Appearance.Options.UseBackColor = true;
                        approvedCondition.ApplyToRow = true;
                        approvedCondition.Column = view.Columns["StatusText"];
                        approvedCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
                        approvedCondition.Value1 = "معتمدة";
                        view.FormatConditions.Add(approvedCondition);
                        
                        DevExpress.XtraGrid.StyleFormatCondition rejectedCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                        rejectedCondition.Appearance.ForeColor = Color.Red;
                        rejectedCondition.Appearance.BackColor = Color.FromArgb(255, 240, 240);
                        rejectedCondition.Appearance.Options.UseForeColor = true;
                        rejectedCondition.Appearance.Options.UseBackColor = true;
                        rejectedCondition.ApplyToRow = true;
                        rejectedCondition.Column = view.Columns["StatusText"];
                        rejectedCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
                        rejectedCondition.Value1 = "مرفوضة";
                        view.FormatConditions.Add(rejectedCondition);
                        
                        DevExpress.XtraGrid.StyleFormatCondition pendingCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                        pendingCondition.Appearance.ForeColor = Color.FromArgb(230, 126, 34); // لون برتقالي للطلبات المعلقة
                        pendingCondition.Appearance.BackColor = Color.FromArgb(255, 248, 240);
                        pendingCondition.Appearance.Options.UseForeColor = true;
                        pendingCondition.Appearance.Options.UseBackColor = true;
                        pendingCondition.ApplyToRow = true;
                        pendingCondition.Column = view.Columns["StatusText"];
                        pendingCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
                        pendingCondition.Value1 = "قيد الانتظار";
                        view.FormatConditions.Add(pendingCondition);
                        
                        // تحديث عنوان التقرير بعدد النتائج
                        lblReportTitle.Text = $"تقرير الإجازات - عدد النتائج: {resultTable.Rows.Count}";
                        
                        // إحصائيات سريعة
                        int totalCount = resultTable.Rows.Count;
                        int approvedCount = resultTable.AsEnumerable().Count(r => r.Field<int>("Status") == 1);
                        int rejectedCount = resultTable.AsEnumerable().Count(r => r.Field<int>("Status") == 2);
                        int pendingCount = resultTable.AsEnumerable().Count(r => r.Field<int>("Status") == 0);
                        int totalDays = resultTable.AsEnumerable().Sum(r => r.Field<int>("Duration"));
                        
                        lblStatistics.Text = $"إجمالي: {totalCount} | معتمدة: {approvedCount} | مرفوضة: {rejectedCount} | معلقة: {pendingCount} | إجمالي الأيام: {totalDays}";
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
            
            // تعيين نوع الإجازة
            if (cboLeaveType.EditValue != null && Convert.ToInt32(cboLeaveType.EditValue) > 0)
            {
                filter.LeaveTypeId = Convert.ToInt32(cboLeaveType.EditValue);
            }
            
            // تعيين حالة الطلب
            if (rgStatus.SelectedIndex >= 0 && rgStatus.SelectedIndex < 3)
            {
                filter.Status = rgStatus.SelectedIndex;
            }
            
            return filter;
        }
        
        /// <summary>
        /// بناء شرط WHERE للاستعلام
        /// </summary>
        private string BuildWhereClause(ReportFilterOptions filter)
        {
            List<string> conditions = new List<string>();
            
            // شرط الفترة الزمنية
            conditions.Add("LR.RequestDate BETWEEN @StartDate AND @EndDate");
            
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
            
            // شرط نوع الإجازة
            if (filter.LeaveTypeId.HasValue && filter.LeaveTypeId.Value > 0)
            {
                conditions.Add("LR.LeaveTypeID = @LeaveTypeId");
            }
            
            // شرط حالة الطلب
            if (filter.Status.HasValue && filter.Status.Value < 3)
            {
                conditions.Add("LR.Status = @Status");
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
            
            // بارامتر نوع الإجازة
            if (filter.LeaveTypeId.HasValue && filter.LeaveTypeId.Value > 0)
            {
                command.Parameters.AddWithValue("@LeaveTypeId", filter.LeaveTypeId.Value);
            }
            
            // بارامتر حالة الطلب
            if (filter.Status.HasValue && filter.Status.Value < 3)
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
            view.OptionsPrint.RtfPageHeader = $"تقرير الإجازات - {DateTime.Now.ToString("yyyy/MM/dd")}";
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
                saveDialog.FileName = "تقرير الإجازات.xlsx";
                
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
                saveDialog.FileName = "تقرير الإجازات.pdf";
                
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
            public int? LeaveTypeId { get; set; }
            public int? Status { get; set; }
        }
    }
}