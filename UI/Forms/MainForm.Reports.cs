using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;

namespace HR.UI.Forms
{
    /// <summary>
    /// الجزء الخاص بوحدة التقارير في النموذج الرئيسي
    /// </summary>
    public partial class MainForm
    {
        /// <summary>
        /// إعداد وحدة التقارير وإضافة العناصر إلى القائمة
        /// </summary>
        private void SetupReportsModule()
        {
            try
            {
                // التأكد من أن قائمة عناصر التقارير فارغة
                accordionControlReports.Elements.Clear();
                
                // إضافة أنواع التقارير المختلفة
                
                // تقرير الموظفين
                var employeesReport = new DevExpress.XtraBars.Navigation.AccordionControlElement()
                {
                    Name = "employeesReport",
                    Text = "تقارير الموظفين",
                    Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
                };
                employeesReport.Click += (s, e) => ShowSpecificReport("Employees");
                accordionControlReports.Elements.Add(employeesReport);
                
                // تقرير الحضور والانصراف
                var attendanceReport = new DevExpress.XtraBars.Navigation.AccordionControlElement()
                {
                    Name = "attendanceReport",
                    Text = "تقارير الحضور والانصراف",
                    Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
                };
                attendanceReport.Click += (s, e) => ShowSpecificReport("Attendance");
                accordionControlReports.Elements.Add(attendanceReport);
                
                // تقرير الإجازات
                var leaveReport = new DevExpress.XtraBars.Navigation.AccordionControlElement()
                {
                    Name = "leaveReport",
                    Text = "تقارير الإجازات",
                    Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
                };
                leaveReport.Click += (s, e) => ShowSpecificReport("Leave");
                accordionControlReports.Elements.Add(leaveReport);
                
                // تقرير الرواتب
                var payrollReport = new DevExpress.XtraBars.Navigation.AccordionControlElement()
                {
                    Name = "payrollReport",
                    Text = "تقارير الرواتب",
                    Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
                };
                payrollReport.Click += (s, e) => ShowSpecificReport("Payroll");
                accordionControlReports.Elements.Add(payrollReport);
                
                // تقرير العمليات والمخالفات
                var operationsReport = new DevExpress.XtraBars.Navigation.AccordionControlElement()
                {
                    Name = "operationsReport",
                    Text = "تقارير العمليات والمخالفات",
                    Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
                };
                operationsReport.Click += (s, e) => ShowSpecificReport("Operations");
                accordionControlReports.Elements.Add(operationsReport);
                
                // مولد التقارير المخصصة
                var customReportGenerator = new DevExpress.XtraBars.Navigation.AccordionControlElement()
                {
                    Name = "customReportGenerator",
                    Text = "مولد التقارير المخصصة",
                    Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
                };
                customReportGenerator.Click += (s, e) => ShowSpecificReport("CustomGenerator");
                accordionControlReports.Elements.Add(customReportGenerator);
                
                // توسيع قائمة التقارير
                accordionControlReports.Expanded = true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل إعداد وحدة التقارير");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء إعداد قائمة التقارير: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}