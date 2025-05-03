using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Navigation;

namespace HR.UI.Forms.Reports
{
    /// <summary>
    /// واجهة الوصول إلى التقارير - تستخدم لربط MainForm بنماذج التقارير
    /// </summary>
    public static class ReportsInterface
    {
        /// <summary>
        /// فتح نموذج التقرير المحدد في لوحة العرض الرئيسية
        /// </summary>
        /// <param name="reportType">نوع التقرير المطلوب</param>
        /// <param name="mainPanel">لوحة العرض الرئيسية</param>
        /// <returns>نموذج التقرير</returns>
        public static XtraForm OpenReport(ReportType reportType, Control mainPanel)
        {
            try
            {
                // إغلاق أي نموذج عرض حالي
                CloseCurrentForm(mainPanel);
                
                // إنشاء نموذج التقرير المطلوب
                XtraForm reportForm = CreateReportForm(reportType);
                
                if (reportForm != null)
                {
                    // تعيين إعدادات النموذج
                    reportForm.TopLevel = false;
                    reportForm.FormBorderStyle = FormBorderStyle.None;
                    reportForm.Dock = DockStyle.Fill;
                    
                    // إضافة النموذج للوحة وعرضه
                    mainPanel.Controls.Add(reportForm);
                    reportForm.Show();
                    
                    return reportForm;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح التقرير: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
            return null;
        }
        
        /// <summary>
        /// إنشاء نموذج التقرير حسب النوع المطلوب
        /// </summary>
        /// <param name="reportType">نوع التقرير</param>
        /// <returns>نموذج التقرير</returns>
        private static XtraForm CreateReportForm(ReportType reportType)
        {
            switch (reportType)
            {
                case ReportType.EmployeeReport:
                    return new EmployeeReportForm();
                
                case ReportType.AttendanceReport:
                    // سيتم تنفيذه لاحقاً
                    return new EmployeeReportForm(); // مؤقتاً استخدم تقرير الموظفين
                
                case ReportType.LeaveReport:
                    // سيتم تنفيذه لاحقاً
                    return new EmployeeReportForm(); // مؤقتاً استخدم تقرير الموظفين
                
                case ReportType.PayrollReport:
                    // سيتم تنفيذه لاحقاً
                    return new EmployeeReportForm(); // مؤقتاً استخدم تقرير الموظفين
                
                default:
                    return null;
            }
        }
        
        /// <summary>
        /// إغلاق النموذج الحالي في لوحة العرض الرئيسية
        /// </summary>
        /// <param name="mainPanel">لوحة العرض الرئيسية</param>
        private static void CloseCurrentForm(Control mainPanel)
        {
            foreach (Control control in mainPanel.Controls)
            {
                if (control is Form)
                {
                    ((Form)control).Close();
                    mainPanel.Controls.Remove(control);
                    break;
                }
            }
        }
        
        /// <summary>
        /// ربط أحداث النقر على عناصر Accordion بفتح نماذج التقارير
        /// </summary>
        /// <param name="accordion">قائمة الاكورديون الرئيسية</param>
        /// <param name="mainPanel">لوحة العرض الرئيسية</param>
        public static void ConnectAccordionEvents(AccordionControl accordion, Control mainPanel)
        {
            try
            {
                // البحث عن قائمة التقارير الرئيسية
                AccordionControlElement reportsGroup = FindAccordionElement(accordion, "accordionControlReports");
                
                if (reportsGroup != null)
                {
                    // ربط أحداث قوائم التقارير الفرعية
                    foreach (AccordionControlElement child in reportsGroup.Elements)
                    {
                        // تحديد نوع التقرير
                        ReportType reportType = GetReportTypeFromElementName(child.Name);
                        
                        if (reportType != ReportType.Unknown)
                        {
                            // ربط حدث النقر بفتح التقرير المناسب
                            child.Click += (sender, e) => OpenReport(reportType, mainPanel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء ربط أحداث القوائم: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// البحث عن عنصر في Accordion Control حسب الاسم
        /// </summary>
        /// <param name="accordion">قائمة الاكورديون</param>
        /// <param name="elementName">اسم العنصر</param>
        /// <returns>عنصر القائمة</returns>
        private static AccordionControlElement FindAccordionElement(AccordionControl accordion, string elementName)
        {
            foreach (AccordionControlElement element in accordion.Elements)
            {
                if (element.Name == elementName)
                {
                    return element;
                }
                
                AccordionControlElement found = FindAccordionElementInChildren(element, elementName);
                if (found != null)
                {
                    return found;
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// البحث عن عنصر في العناصر الفرعية لـ Accordion Element
        /// </summary>
        /// <param name="parent">العنصر الأب</param>
        /// <param name="elementName">اسم العنصر</param>
        /// <returns>عنصر القائمة</returns>
        private static AccordionControlElement FindAccordionElementInChildren(AccordionControlElement parent, string elementName)
        {
            foreach (AccordionControlElement child in parent.Elements)
            {
                if (child.Name == elementName)
                {
                    return child;
                }
                
                AccordionControlElement found = FindAccordionElementInChildren(child, elementName);
                if (found != null)
                {
                    return found;
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// تحديد نوع التقرير من اسم عنصر القائمة
        /// </summary>
        /// <param name="elementName">اسم عنصر القائمة</param>
        /// <returns>نوع التقرير</returns>
        private static ReportType GetReportTypeFromElementName(string elementName)
        {
            if (string.IsNullOrEmpty(elementName))
            {
                return ReportType.Unknown;
            }
            
            if (elementName.EndsWith("ReportsEmployees"))
            {
                return ReportType.EmployeeReport;
            }
            else if (elementName.EndsWith("ReportsAttendance"))
            {
                return ReportType.AttendanceReport;
            }
            else if (elementName.EndsWith("ReportsLeaves"))
            {
                return ReportType.LeaveReport;
            }
            else if (elementName.EndsWith("ReportsPayroll"))
            {
                return ReportType.PayrollReport;
            }
            
            return ReportType.Unknown;
        }
    }
    
    /// <summary>
    /// أنواع التقارير
    /// </summary>
    public enum ReportType
    {
        Unknown = 0,
        EmployeeReport = 1,
        AttendanceReport = 2,
        LeaveReport = 3,
        PayrollReport = 4
    }
}