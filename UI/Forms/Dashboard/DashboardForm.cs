using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraCharts;
using DevExpress.Utils;
using HR.Core;
using HR.Models.DTOs;

namespace HR.UI.Forms.Dashboard
{
    /// <summary>
    /// نموذج لوحة المعلومات التحليلية للنظام
    /// </summary>
    public partial class DashboardForm : XtraForm
    {
        // فترة التحديث التلقائي بالثواني
        private const int AutoRefreshInterval = 300; // 5 دقائق
        
        // مؤقت التحديث التلقائي
        private Timer _refreshTimer;
        
        /// <summary>
        /// تهيئة نموذج لوحة المعلومات
        /// </summary>
        public DashboardForm()
        {
            InitializeComponent();
            
            // ضبط الإعدادات
            this.Text = "لوحة المعلومات التحليلية";
            
            // تسجيل الأحداث
            this.Load += DashboardForm_Load;
            buttonRefresh.Click += ButtonRefresh_Click;
            dateEditFrom.EditValueChanged += DateFilter_Changed;
            dateEditTo.EditValueChanged += DateFilter_Changed;
            
            // إعداد عناصر التحكم
            InitializeControls();
            
            // إنشاء مؤقت للتحديث التلقائي
            _refreshTimer = new Timer();
            _refreshTimer.Interval = AutoRefreshInterval * 1000;
            _refreshTimer.Tick += TimerRefresh_Tick;
        }

        /// <summary>
        /// تهيئة عناصر التحكم
        /// </summary>
        private void InitializeControls()
        {
            // ضبط فترة التاريخ الافتراضية (الشهر الحالي)
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            
            dateEditFrom.DateTime = firstDayOfMonth;
            dateEditTo.DateTime = lastDayOfMonth;
            
            // ضبط خصائص عناصر التحكم
            gridViewAttendance.OptionsBehavior.ReadOnly = true;
            gridViewLeaves.OptionsBehavior.ReadOnly = true;
            
            // ضبط مخططات الرسوم البيانية
            InitializeCharts();
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void DashboardForm_Load(object sender, EventArgs e)
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // تحميل البيانات
                LoadDashboardData();
                
                // بدء مؤقت التحديث التلقائي
                _refreshTimer.Start();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل لوحة المعلومات");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل لوحة المعلومات: {ex.Message}",
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
        /// حدث نقر زر التحديث
        /// </summary>
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // إعادة تحميل البيانات
                LoadDashboardData();
                
                // عرض رسالة التحديث
                labelLastUpdate.Text = $"آخر تحديث: {DateTime.Now:yyyy/MM/dd HH:mm:ss}";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث لوحة المعلومات");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحديث لوحة المعلومات: {ex.Message}",
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
        /// حدث تغيير فلتر التاريخ
        /// </summary>
        private void DateFilter_Changed(object sender, EventArgs e)
        {
            // التحقق من صحة فترة التاريخ
            if (dateEditFrom.DateTime > dateEditTo.DateTime)
            {
                XtraMessageBox.Show(
                    "تاريخ البداية يجب أن يكون قبل تاريخ النهاية",
                    "فترة غير صالحة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                
                // إعادة ضبط القيم
                DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                
                dateEditFrom.DateTime = firstDayOfMonth;
                dateEditTo.DateTime = lastDayOfMonth;
                
                return;
            }
            
            // تحديث البيانات بناءً على فترة التاريخ الجديدة
            LoadDashboardData();
        }

        /// <summary>
        /// حدث تيك مؤقت التحديث التلقائي
        /// </summary>
        private void TimerRefresh_Tick(object sender, EventArgs e)
        {
            // تحديث البيانات تلقائياً
            LoadDashboardData();
            
            // تحديث وقت آخر تحديث
            labelLastUpdate.Text = $"آخر تحديث: {DateTime.Now:yyyy/MM/dd HH:mm:ss}";
        }

        /// <summary>
        /// تهيئة مخططات الرسوم البيانية
        /// </summary>
        private void InitializeCharts()
        {
            try
            {
                // تهيئة مخطط الحضور
                chartAttendance.Titles.Clear();
                chartAttendance.Titles.Add(new ChartTitle { Text = "إحصائيات الحضور", Dock = ChartTitleDockStyle.Top });
                
                // تهيئة مخطط الإجازات
                chartLeaves.Titles.Clear();
                chartLeaves.Titles.Add(new ChartTitle { Text = "إحصائيات الإجازات", Dock = ChartTitleDockStyle.Top });
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تهيئة مخططات الرسوم البيانية");
            }
        }

        /// <summary>
        /// تحميل بيانات لوحة المعلومات
        /// </summary>
        private void LoadDashboardData()
        {
            try
            {
                // الحصول على بيانات لوحة المعلومات
                DateTime fromDate = dateEditFrom.DateTime.Date;
                DateTime toDate = dateEditTo.DateTime.Date.AddDays(1).AddSeconds(-1);
                
                DashboardDataDTO dashboardData = DashboardManager.GetDashboardData(fromDate, toDate);
                
                if (dashboardData == null)
                {
                    // لا توجد بيانات متاحة
                    XtraMessageBox.Show(
                        "لا توجد بيانات متاحة للفترة المحددة",
                        "بيانات غير متوفرة",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                
                // تحديث إحصائيات الموظفين
                UpdateEmployeeStatistics(dashboardData.EmployeeStatistics);
                
                // تحديث إحصائيات الحضور
                UpdateAttendanceStatistics(dashboardData.AttendanceStatistics);
                
                // تحديث إحصائيات الإجازات
                UpdateLeaveStatistics(dashboardData.LeaveStatistics);
                
                // تحديث إحصائيات الرواتب
                UpdatePayrollStatistics(dashboardData.PayrollStatistics);
                
                // تحديث قائمة الإجازات الحالية
                UpdateCurrentLeaves(dashboardData.CurrentLeaves);
                
                // تحديث رسوم الحضور
                UpdateAttendanceChart(dashboardData.AttendanceChartData);
                
                // تحديث رسوم اتجاهات الحضور
                UpdateAttendanceTrendChart(dashboardData.AttendanceTrendData);
                
                // تحديث رسوم الإجازات
                UpdateLeavesChart(dashboardData.LeavesChartData);
                
                // تحديث رسوم الحضور حسب القسم
                UpdateDepartmentAttendanceChart(dashboardData.DepartmentAttendanceData);
                
                // تحديث رسوم الحضور حسب أيام الأسبوع
                UpdateWeekdayAttendanceChart(dashboardData.WeekdayAttendanceData);
                
                // تحديث قائمة التنبيهات
                UpdateNotifications(dashboardData.Notifications);
                
                // تحديث قائمة الأحداث القادمة
                UpdateUpcomingEvents(dashboardData.UpcomingEvents);
                
                // تحديث مؤشرات الأداء الرئيسية
                UpdateKPIData(dashboardData.KPIData);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحميل بيانات لوحة المعلومات");
                throw;
            }
        }

        /// <summary>
        /// تحديث إحصائيات الموظفين
        /// </summary>
        private void UpdateEmployeeStatistics(EmployeeStatisticsDTO statistics)
        {
            if (statistics == null)
                return;
            
            // تعيين القيم في مؤشرات الأداء
            kpiTotalEmployees.Value = statistics.TotalEmployees;
            kpiActiveEmployees.Value = statistics.ActiveEmployees;
            kpiNewEmployees.Value = statistics.NewEmployees;
            kpiTerminatedEmployees.Value = statistics.TerminatedEmployees;
            
            // تحديث النسب المئوية
            if (statistics.TotalEmployees > 0)
            {
                kpiActiveEmployees.Caption = $"الموظفين النشطين ({statistics.ActiveEmployees * 100 / statistics.TotalEmployees:0}%)";
                kpiNewEmployees.Caption = $"الموظفين الجدد ({statistics.NewEmployees * 100 / statistics.TotalEmployees:0}%)";
                kpiTerminatedEmployees.Caption = $"المغادرين ({statistics.TerminatedEmployees * 100 / statistics.TotalEmployees:0}%)";
            }
        }

        /// <summary>
        /// تحديث إحصائيات الحضور
        /// </summary>
        private void UpdateAttendanceStatistics(AttendanceStatisticsDTO statistics)
        {
            if (statistics == null)
                return;
            
            // تعيين القيم في مؤشرات الأداء
            kpiPresentToday.Value = statistics.PresentToday;
            kpiAbsentToday.Value = statistics.AbsentToday;
            kpiLateToday.Value = statistics.LateToday;
            kpiOnLeaveToday.Value = statistics.OnLeaveToday;
            
            // تحديث النسب المئوية
            if (statistics.TotalEmployees > 0)
            {
                kpiPresentToday.Caption = $"الحاضرون اليوم ({statistics.PresentToday * 100 / statistics.TotalEmployees:0}%)";
                kpiAbsentToday.Caption = $"الغائبون اليوم ({statistics.AbsentToday * 100 / statistics.TotalEmployees:0}%)";
                kpiLateToday.Caption = $"المتأخرون اليوم ({statistics.LateToday * 100 / statistics.TotalEmployees:0}%)";
                kpiOnLeaveToday.Caption = $"في إجازة اليوم ({statistics.OnLeaveToday * 100 / statistics.TotalEmployees:0}%)";
            }
        }

        /// <summary>
        /// تحديث قائمة الإجازات الحالية
        /// </summary>
        private void UpdateCurrentLeaves(List<LeaveDTO> leaves)
        {
            // تعيين مصدر بيانات الجدول
            gridControlLeaves.DataSource = leaves;
            
            // تطبيق تنسيق الشبكة
            gridViewLeaves.BestFitColumns();
        }

        /// <summary>
        /// تحديث رسم الحضور
        /// </summary>
        private void UpdateAttendanceChart(List<ChartDataDTO> chartData)
        {
            if (chartData == null || chartData.Count == 0)
                return;
            
            try
            {
                // تهيئة المخطط
                chartAttendance.Series.Clear();
                
                // إنشاء سلسلة البيانات
                Series series = new Series("الحضور", ViewType.Bar);
                
                // إضافة نقاط البيانات
                foreach (var data in chartData)
                {
                    series.Points.Add(new SeriesPoint(data.CategoryName, data.Value));
                }
                
                // إضافة السلسلة إلى المخطط
                chartAttendance.Series.Add(series);
                
                // ضبط إعدادات المخطط
                ((BarSeriesView)series.View).ColorEach = true;
                
                // تهيئة المحاور
                XYDiagram diagram = chartAttendance.Diagram as XYDiagram;
                if (diagram != null)
                {
                    diagram.AxisX.Label.Angle = -45;
                    diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = true;
                    diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = true;
                    diagram.AxisX.Label.ResolveOverlappingOptions.MinIndent = 1;
                    
                    diagram.AxisX.Title.Text = "الأيام";
                    diagram.AxisX.Title.Visibility = DefaultBoolean.True;
                    
                    diagram.AxisY.Title.Text = "عدد الموظفين";
                    diagram.AxisY.Title.Visibility = DefaultBoolean.True;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث مخطط الحضور");
            }
        }

        /// <summary>
        /// تحديث رسم الإجازات
        /// </summary>
        private void UpdateLeavesChart(List<ChartDataDTO> chartData)
        {
            if (chartData == null || chartData.Count == 0)
                return;
            
            try
            {
                // تهيئة المخطط
                chartLeaves.Series.Clear();
                
                // إنشاء سلسلة البيانات
                Series series = new Series("أنواع الإجازات", ViewType.Pie);
                
                // إضافة نقاط البيانات
                foreach (var data in chartData)
                {
                    series.Points.Add(new SeriesPoint(data.CategoryName, data.Value));
                }
                
                // إضافة السلسلة إلى المخطط
                chartLeaves.Series.Add(series);
                
                // ضبط إعدادات المخطط
                PieSeriesView view = (PieSeriesView)series.View;
                view.ExplodeMode = PieExplodeMode.UseFilters;
                view.ExplodedDistancePercentage = 10;
                
                // إضافة تسميات القيم
                series.LabelsVisibility = DefaultBoolean.True;
                view.ValueDataMember = "Values";
                view.ArgumentDataMember = "Arguments";
                view.ValueDataMember = "Values";
                
                // تنسيق التسميات
                view.DataLabels.TextPattern = "{A}: {VP:0.00%}";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث مخطط الإجازات");
            }
        }

        /// <summary>
        /// تحديث قائمة التنبيهات
        /// </summary>
        private void UpdateNotifications(List<NotificationDTO> notifications)
        {
            // تحديث عدد التنبيهات
            labelNotificationsCount.Text = $"التنبيهات ({notifications?.Count ?? 0})";
            
            // تفريغ المعرض
            galleryControlNotifications.Gallery.Groups.Clear();
            
            if (notifications == null || notifications.Count == 0)
                return;
            
            try
            {
                // إنشاء مجموعة للمعرض
                DevExpress.XtraBars.Ribbon.GalleryItemGroup group = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
                
                // إضافة العناصر
                foreach (var notification in notifications)
                {
                    DevExpress.XtraBars.Ribbon.GalleryItem item = new DevExpress.XtraBars.Ribbon.GalleryItem();
                    item.Caption = notification.Title;
                    item.Description = notification.Message;
                    
                    // تحديد أيقونة العنصر بناءً على نوع التنبيه
                    switch (notification.Type)
                    {
                        case "Warning":
                            item.ImageOptions.SvgImage = svgImageWarning;
                            break;
                        case "Error":
                            item.ImageOptions.SvgImage = svgImageError;
                            break;
                        case "Info":
                        default:
                            item.ImageOptions.SvgImage = svgImageInfo;
                            break;
                    }
                    
                    // إضافة العنصر إلى المجموعة
                    group.Items.Add(item);
                }
                
                // إضافة المجموعة إلى المعرض
                galleryControlNotifications.Gallery.Groups.Add(group);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث قائمة التنبيهات");
            }
        }

        /// <summary>
        /// تحديث قائمة الأحداث القادمة
        /// </summary>
        private void UpdateUpcomingEvents(List<EventDTO> events)
        {
            // تحديث عدد الأحداث
            labelEventsCount.Text = $"الأحداث القادمة ({events?.Count ?? 0})";
            
            // تفريغ المعرض
            galleryControlEvents.Gallery.Groups.Clear();
            
            if (events == null || events.Count == 0)
                return;
            
            try
            {
                // إنشاء مجموعة للمعرض
                DevExpress.XtraBars.Ribbon.GalleryItemGroup group = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
                
                // إضافة العناصر
                foreach (var evt in events)
                {
                    DevExpress.XtraBars.Ribbon.GalleryItem item = new DevExpress.XtraBars.Ribbon.GalleryItem();
                    item.Caption = evt.Title;
                    item.Description = $"{evt.Date:yyyy/MM/dd}: {evt.Description}";
                    
                    // تحديد أيقونة العنصر بناءً على نوع الحدث
                    switch (evt.Type)
                    {
                        case "Birthday":
                            item.ImageOptions.SvgImage = svgImageBirthday;
                            break;
                        case "Anniversary":
                            item.ImageOptions.SvgImage = svgImageAnniversary;
                            break;
                        case "Meeting":
                            item.ImageOptions.SvgImage = svgImageMeeting;
                            break;
                        case "Holiday":
                            item.ImageOptions.SvgImage = svgImageHoliday;
                            break;
                        default:
                            item.ImageOptions.SvgImage = svgImageEvent;
                            break;
                    }
                    
                    // إضافة العنصر إلى المجموعة
                    group.Items.Add(item);
                }
                
                // إضافة المجموعة إلى المعرض
                galleryControlEvents.Gallery.Groups.Add(group);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث قائمة الأحداث القادمة");
            }
        }
        
        /// <summary>
        /// تحديث إحصائيات الإجازات
        /// </summary>
        private void UpdateLeaveStatistics(LeaveStatisticsDTO statistics)
        {
            if (statistics == null)
                return;
                
            try
            {
                // تحديث مؤشرات إحصائيات الإجازات
                kpiTotalLeaves.Value = statistics.TotalLeaves;
                kpiSickLeaves.Value = statistics.SickLeaves;
                kpiAnnualLeaves.Value = statistics.AnnualLeaves;
                kpiEmergencyLeaves.Value = statistics.EmergencyLeaves;
                
                // تحديث النسب المئوية
                if (statistics.TotalLeaves > 0)
                {
                    kpiSickLeaves.Caption = $"الإجازات المرضية ({(decimal)statistics.SickLeaves * 100 / statistics.TotalLeaves:0.0}%)";
                    kpiAnnualLeaves.Caption = $"الإجازات السنوية ({(decimal)statistics.AnnualLeaves * 100 / statistics.TotalLeaves:0.0}%)";
                    kpiEmergencyLeaves.Caption = $"الإجازات الطارئة ({(decimal)statistics.EmergencyLeaves * 100 / statistics.TotalLeaves:0.0}%)";
                }
                
                // تحديث مؤشرات المقارنة
                SetComparisonIndicator(labelSickLeaveChange, statistics.SickLeaveChangePercent);
                SetComparisonIndicator(labelLeaveUtilization, statistics.LeaveUtilizationRate, targetValue: 100);
                
                // تحديث متوسط أيام الإجازة
                labelAverageLeaveDays.Text = $"متوسط أيام الإجازة للموظف: {statistics.AverageLeaveDaysPerEmployee:0.00} يوم";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث إحصائيات الإجازات");
            }
        }
        
        /// <summary>
        /// تحديث إحصائيات الرواتب
        /// </summary>
        private void UpdatePayrollStatistics(PayrollStatisticsDTO statistics)
        {
            if (statistics == null)
                return;
                
            try
            {
                // تحديث مؤشرات إحصائيات الرواتب
                kpiTotalSalaries.Value = (int)statistics.TotalSalariesCurrentPeriod;
                kpiTotalAllowances.Value = (int)statistics.TotalAllowances;
                kpiTotalDeductions.Value = (int)statistics.TotalDeductions;
                
                // تحديث مؤشرات المقارنة
                SetComparisonIndicator(labelSalaryChange, statistics.SalaryChangePercent);
                
                // تحديث نسبة الخصومات المتعلقة بالحضور
                if (statistics.TotalDeductions > 0)
                {
                    labelAttendanceDeductions.Text = $"خصومات الحضور: {statistics.AttendanceRelatedDeductions:N0} ({statistics.AttendanceRelatedDeductions * 100 / statistics.TotalDeductions:0.0}%)";
                }
                else
                {
                    labelAttendanceDeductions.Text = "خصومات الحضور: 0";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث إحصائيات الرواتب");
            }
        }
        
        /// <summary>
        /// تحديث رسم اتجاهات الحضور
        /// </summary>
        private void UpdateAttendanceTrendChart(AttendanceTrendChartDataDTO trendData)
        {
            if (trendData == null || trendData.TimeCategories == null || trendData.TimeCategories.Count == 0)
                return;
                
            try
            {
                // تهيئة المخطط
                chartAttendanceTrend.Series.Clear();
                
                // إنشاء سلسلة بيانات للحضور
                Series attendanceSeries = new Series("نسبة الحضور", ViewType.Line);
                attendanceSeries.ArgumentDataMember = "TimeCategory";
                attendanceSeries.ValueDataMembers.AddRange(new string[] { "AttendanceRate" });
                
                // إنشاء سلسلة بيانات للتأخير
                Series lateSeries = new Series("نسبة التأخير", ViewType.Line);
                lateSeries.ArgumentDataMember = "TimeCategory";
                lateSeries.ValueDataMembers.AddRange(new string[] { "LateRate" });
                
                // إنشاء سلسلة بيانات للغياب
                Series absenceSeries = new Series("نسبة الغياب", ViewType.Line);
                absenceSeries.ArgumentDataMember = "TimeCategory";
                absenceSeries.ValueDataMembers.AddRange(new string[] { "AbsenceRate" });
                
                // إنشاء قائمة من كائنات البيانات
                List<TrendDataPoint> dataPoints = new List<TrendDataPoint>();
                for (int i = 0; i < trendData.TimeCategories.Count; i++)
                {
                    dataPoints.Add(new TrendDataPoint
                    {
                        TimeCategory = trendData.TimeCategories[i],
                        AttendanceRate = trendData.AttendanceRates[i],
                        LateRate = trendData.LateRates[i],
                        AbsenceRate = trendData.AbsenceRates[i]
                    });
                }
                
                // تعيين مصدر البيانات
                chartAttendanceTrend.DataSource = dataPoints;
                
                // إضافة السلاسل إلى المخطط
                chartAttendanceTrend.Series.AddRange(new Series[] 
                { 
                    attendanceSeries, 
                    lateSeries, 
                    absenceSeries 
                });
                
                // ضبط إعدادات المخطط
                XYDiagram diagram = chartAttendanceTrend.Diagram as XYDiagram;
                if (diagram != null)
                {
                    // تنسيق المحاور
                    diagram.AxisX.Label.Angle = -45;
                    diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = true;
                    diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = true;
                    
                    diagram.AxisX.Title.Text = "الفترة";
                    diagram.AxisX.Title.Visibility = DefaultBoolean.True;
                    
                    diagram.AxisY.Title.Text = "النسبة المئوية";
                    diagram.AxisY.Title.Visibility = DefaultBoolean.True;
                    
                    // تعيين نطاق محور Y
                    diagram.AxisY.WholeRange.SetMinMaxValues(0, 100);
                }
                
                // ضبط خصائص السلاسل
                LineSeriesView attendanceView = attendanceSeries.View as LineSeriesView;
                if (attendanceView != null)
                {
                    attendanceView.LineMarkerOptions.Kind = MarkerKind.Circle;
                    attendanceView.LineStyle.Thickness = 2;
                    attendanceView.Color = Color.FromArgb(65, 140, 240); // أزرق
                }
                
                LineSeriesView lateView = lateSeries.View as LineSeriesView;
                if (lateView != null)
                {
                    lateView.LineMarkerOptions.Kind = MarkerKind.Triangle;
                    lateView.LineStyle.Thickness = 2;
                    lateView.Color = Color.FromArgb(252, 180, 65); // برتقالي
                }
                
                LineSeriesView absenceView = absenceSeries.View as LineSeriesView;
                if (absenceView != null)
                {
                    absenceView.LineMarkerOptions.Kind = MarkerKind.Square;
                    absenceView.LineStyle.Thickness = 2;
                    absenceView.Color = Color.FromArgb(224, 64, 10); // أحمر
                }
                
                // تحديث العنوان
                chartAttendanceTrend.Titles[0].Text = "اتجاهات الحضور والتأخير والغياب";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث مخطط اتجاهات الحضور");
            }
        }
        
        /// <summary>
        /// تحديث رسم الحضور حسب القسم
        /// </summary>
        private void UpdateDepartmentAttendanceChart(DepartmentAttendanceDataDTO deptData)
        {
            if (deptData == null || deptData.DepartmentNames == null || deptData.DepartmentNames.Count == 0)
                return;
                
            try
            {
                // تهيئة المخطط
                chartDepartmentAttendance.Series.Clear();
                
                // إنشاء سلسلة بيانات للحضور
                Series attendanceSeries = new Series("نسبة الحضور", ViewType.Bar);
                attendanceSeries.ArgumentDataMember = "Department";
                attendanceSeries.ValueDataMembers.AddRange(new string[] { "AttendanceRate" });
                
                // إنشاء سلسلة بيانات للتأخير
                Series lateSeries = new Series("نسبة التأخير", ViewType.Bar);
                lateSeries.ArgumentDataMember = "Department";
                lateSeries.ValueDataMembers.AddRange(new string[] { "LateRate" });
                
                // إنشاء سلسلة بيانات للإجازات
                Series leaveSeries = new Series("نسبة الإجازات", ViewType.Bar);
                leaveSeries.ArgumentDataMember = "Department";
                leaveSeries.ValueDataMembers.AddRange(new string[] { "LeaveRate" });
                
                // إنشاء قائمة من كائنات البيانات
                List<DepartmentDataPoint> dataPoints = new List<DepartmentDataPoint>();
                for (int i = 0; i < deptData.DepartmentNames.Count; i++)
                {
                    dataPoints.Add(new DepartmentDataPoint
                    {
                        Department = deptData.DepartmentNames[i],
                        AttendanceRate = deptData.AttendanceRates[i],
                        LateRate = deptData.LateRates[i],
                        LeaveRate = deptData.LeaveRates[i]
                    });
                }
                
                // تعيين مصدر البيانات
                chartDepartmentAttendance.DataSource = dataPoints;
                
                // إضافة السلاسل إلى المخطط
                chartDepartmentAttendance.Series.AddRange(new Series[] 
                { 
                    attendanceSeries, 
                    lateSeries, 
                    leaveSeries 
                });
                
                // ضبط إعدادات المخطط
                XYDiagram diagram = chartDepartmentAttendance.Diagram as XYDiagram;
                if (diagram != null)
                {
                    // تنسيق المحاور
                    diagram.AxisX.Label.Angle = -45;
                    diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = true;
                    diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = true;
                    
                    diagram.AxisX.Title.Text = "الأقسام";
                    diagram.AxisX.Title.Visibility = DefaultBoolean.True;
                    
                    diagram.AxisY.Title.Text = "النسبة المئوية";
                    diagram.AxisY.Title.Visibility = DefaultBoolean.True;
                    
                    // تعيين نطاق محور Y
                    diagram.AxisY.WholeRange.SetMinMaxValues(0, 100);
                }
                
                // ضبط الألوان
                ((BarSeriesView)attendanceSeries.View).Color = Color.FromArgb(65, 140, 240); // أزرق
                ((BarSeriesView)lateSeries.View).Color = Color.FromArgb(252, 180, 65); // برتقالي
                ((BarSeriesView)leaveSeries.View).Color = Color.FromArgb(140, 193, 82); // أخضر
                
                // تهيئة وضع المقارنة الجانبية
                ((XYDiagram)chartDepartmentAttendance.Diagram).EnableAxisXZooming = true;
                ((BarSeriesView)attendanceSeries.View).BarDistance = 0.5;
                ((BarSeriesView)attendanceSeries.View).BarWidth = 0.5;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث مخطط الحضور حسب القسم");
            }
        }
        
        /// <summary>
        /// تحديث رسم الحضور حسب أيام الأسبوع
        /// </summary>
        private void UpdateWeekdayAttendanceChart(WeekdayAttendanceDataDTO weekdayData)
        {
            if (weekdayData == null || weekdayData.Weekdays == null || weekdayData.Weekdays.Count == 0)
                return;
                
            try
            {
                // تهيئة المخطط
                chartWeekdayAttendance.Series.Clear();
                
                // إنشاء سلسلة بيانات للحضور
                Series attendanceSeries = new Series("نسبة الحضور", ViewType.Bar);
                attendanceSeries.ArgumentDataMember = "Weekday";
                attendanceSeries.ValueDataMembers.AddRange(new string[] { "AttendanceRate" });
                
                // إنشاء سلسلة بيانات للتأخير
                Series lateSeries = new Series("نسبة التأخير", ViewType.Bar);
                lateSeries.ArgumentDataMember = "Weekday";
                lateSeries.ValueDataMembers.AddRange(new string[] { "LateRate" });
                
                // إنشاء سلسلة بيانات للغياب
                Series absenceSeries = new Series("نسبة الغياب", ViewType.Bar);
                absenceSeries.ArgumentDataMember = "Weekday";
                absenceSeries.ValueDataMembers.AddRange(new string[] { "AbsenceRate" });
                
                // إنشاء قائمة من كائنات البيانات
                List<WeekdayDataPoint> dataPoints = new List<WeekdayDataPoint>();
                for (int i = 0; i < weekdayData.Weekdays.Count; i++)
                {
                    dataPoints.Add(new WeekdayDataPoint
                    {
                        Weekday = weekdayData.Weekdays[i],
                        AttendanceRate = weekdayData.AttendanceRates[i],
                        LateRate = weekdayData.LateRates[i],
                        AbsenceRate = weekdayData.AbsenceRates[i]
                    });
                }
                
                // تعيين مصدر البيانات
                chartWeekdayAttendance.DataSource = dataPoints;
                
                // إضافة السلاسل إلى المخطط
                chartWeekdayAttendance.Series.AddRange(new Series[] 
                { 
                    attendanceSeries, 
                    lateSeries, 
                    absenceSeries 
                });
                
                // ضبط إعدادات المخطط
                XYDiagram diagram = chartWeekdayAttendance.Diagram as XYDiagram;
                if (diagram != null)
                {
                    // تنسيق المحاور
                    diagram.AxisX.Title.Text = "أيام الأسبوع";
                    diagram.AxisX.Title.Visibility = DefaultBoolean.True;
                    
                    diagram.AxisY.Title.Text = "النسبة المئوية";
                    diagram.AxisY.Title.Visibility = DefaultBoolean.True;
                    
                    // تعيين نطاق محور Y
                    diagram.AxisY.WholeRange.SetMinMaxValues(0, 100);
                }
                
                // ضبط الألوان
                ((BarSeriesView)attendanceSeries.View).Color = Color.FromArgb(65, 140, 240); // أزرق
                ((BarSeriesView)lateSeries.View).Color = Color.FromArgb(252, 180, 65); // برتقالي
                ((BarSeriesView)absenceSeries.View).Color = Color.FromArgb(224, 64, 10); // أحمر
                
                // تهيئة وضع المقارنة الجانبية
                ((XYDiagram)chartWeekdayAttendance.Diagram).EnableAxisXZooming = true;
                ((BarSeriesView)attendanceSeries.View).BarDistance = 0.5;
                ((BarSeriesView)attendanceSeries.View).BarWidth = 0.5;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث مخطط الحضور حسب أيام الأسبوع");
            }
        }
        
        /// <summary>
        /// تحديث بيانات مؤشرات الأداء الرئيسية
        /// </summary>
        private void UpdateKPIData(KPIDataDTO kpiData)
        {
            if (kpiData == null)
                return;
                
            try
            {
                // تحديث مؤشرات الأداء الرئيسية
                gaugeAttendanceCompliance.Value = (float)kpiData.AttendanceComplianceRate;
                gaugeAttendanceCompliance.Scales[0].CustomRanges[1].StartValue = kpiData.AttendanceComplianceTarget;
                labelAttendanceComplianceTarget.Text = $"المستهدف: {kpiData.AttendanceComplianceTarget}%";
                
                gaugeLateRate.Value = (float)kpiData.LateRate;
                gaugeLateRate.Scales[0].CustomRanges[0].EndValue = kpiData.LateRateTarget;
                labelLateRateTarget.Text = $"المستهدف: {kpiData.LateRateTarget}%";
                
                gaugeEmployeeTurnover.Value = (float)kpiData.EmployeeTurnoverRate;
                gaugeEmployeeTurnover.Scales[0].CustomRanges[0].EndValue = kpiData.EmployeeTurnoverTarget;
                labelTurnoverTarget.Text = $"المستهدف: {kpiData.EmployeeTurnoverTarget}%";
                
                gaugeLeaveUtilization.Value = (float)kpiData.LeaveUtilizationRate;
                gaugeLeaveUtilization.Scales[0].CustomRanges[1].StartValue = kpiData.LeaveUtilizationTarget;
                labelLeaveUtilizationTarget.Text = $"المستهدف: {kpiData.LeaveUtilizationTarget}%";
                
                gaugeSalaryCostRatio.Value = (float)kpiData.SalaryToTotalCostRatio;
                gaugeSalaryCostRatio.Scales[0].CustomRanges[0].EndValue = kpiData.SalaryToTotalCostTarget;
                labelSalaryCostTarget.Text = $"المستهدف: {kpiData.SalaryToTotalCostTarget}%";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل تحديث بيانات مؤشرات الأداء الرئيسية");
            }
        }
        
        /// <summary>
        /// ضبط مؤشر المقارنة (زيادة/نقصان)
        /// </summary>
        private void SetComparisonIndicator(LabelControl label, decimal changePercent, decimal targetValue = 0)
        {
            if (label == null)
                return;
                
            if (targetValue > 0)
            {
                // المقارنة مع القيمة المستهدفة
                if (changePercent >= targetValue)
                {
                    label.ForeColor = Color.Green;
                    label.Text = $"{changePercent:0.0}% ▲";
                }
                else
                {
                    label.ForeColor = Color.Red;
                    label.Text = $"{changePercent:0.0}% ▼";
                }
            }
            else
            {
                // المقارنة مع الفترة السابقة
                if (changePercent > 0)
                {
                    label.ForeColor = Color.Green;
                    label.Text = $"+{changePercent:0.0}% ▲";
                }
                else if (changePercent < 0)
                {
                    label.ForeColor = Color.Red;
                    label.Text = $"{changePercent:0.0}% ▼";
                }
                else
                {
                    label.ForeColor = Color.Gray;
                    label.Text = "0% ◄►";
                }
            }
        }
        
        /// <summary>
        /// كائن نقطة بيانات لرسم اتجاهات الحضور
        /// </summary>
        private class TrendDataPoint
        {
            public string TimeCategory { get; set; }
            public decimal AttendanceRate { get; set; }
            public decimal LateRate { get; set; }
            public decimal AbsenceRate { get; set; }
        }
        
        /// <summary>
        /// كائن نقطة بيانات للرسم حسب القسم
        /// </summary>
        private class DepartmentDataPoint
        {
            public string Department { get; set; }
            public decimal AttendanceRate { get; set; }
            public decimal LateRate { get; set; }
            public decimal LeaveRate { get; set; }
        }
        
        /// <summary>
        /// كائن نقطة بيانات للرسم حسب يوم الأسبوع
        /// </summary>
        private class WeekdayDataPoint
        {
            public string Weekday { get; set; }
            public decimal AttendanceRate { get; set; }
            public decimal LateRate { get; set; }
            public decimal AbsenceRate { get; set; }
        }
    }
}