using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using HR.UI.Forms.Reports;
using System;
using System.Windows.Forms;

namespace HR.UI.Forms
{
    /// <summary>
    /// تطبيق نظام نماذج التقارير في النموذج الرئيسي
    /// </summary>
    public partial class MainForm
    {
        /// <summary>
        /// إعداد وحدة التقارير في القائمة الرئيسية
        /// </summary>
        private void SetupReportsModule()
        {
            try
            {
                // إضافة وحدة التقارير للقائمة الجانبية إذا لم تكن موجودة
                AccordionControlElement reportsGroup = null;
                
                // التحقق من وجود وحدة التقارير
                foreach (AccordionControlElement group in accordionControl.Elements)
                {
                    if (group.Tag != null && group.Tag.ToString() == "Reports")
                    {
                        reportsGroup = group;
                        break;
                    }
                }
                
                // إنشاء وحدة التقارير إذا لم تكن موجودة
                if (reportsGroup == null)
                {
                    reportsGroup = new AccordionControlElement
                    {
                        Text = "التقارير",
                        Style = ElementStyle.Group,
                        Tag = "Reports",
                        HeaderTemplate = new BottomElementTemplate()
                    };
                    
                    // إضافة رمز للتقارير
                    reportsGroup.ImageOptions.Image = Properties.Resources.reports;
                    
                    // إضافة الوحدة للقائمة
                    accordionControl.Elements.Add(reportsGroup);
                }
                
                // إضافة عناصر التقارير لوحدة التقارير
                // تقرير الموظفين
                AddReportItem(reportsGroup, "تقرير الموظفين", "EmployeeReport", Properties.Resources.employee_reports);
                
                // تقرير الحضور والغياب
                AddReportItem(reportsGroup, "تقرير الحضور والغياب", "AttendanceReport", Properties.Resources.attendance_reports);
                
                // تقرير الإجازات
                AddReportItem(reportsGroup, "تقرير الإجازات", "LeaveReport", Properties.Resources.leave_reports);
                
                // تقرير الرواتب
                AddReportItem(reportsGroup, "تقرير الرواتب", "PayrollReport", Properties.Resources.payroll_reports);
                
                // تقرير العمليات والمخالفات
                AddReportItem(reportsGroup, "تقرير العمليات والمخالفات", "OperationsReport", Properties.Resources.operations_reports);
                
                // مولد التقارير المخصصة
                AddReportItem(reportsGroup, "مولد التقارير المخصصة", "CustomReportGenerator", Properties.Resources.custom_reports);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إعداد وحدة التقارير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// إضافة عنصر تقرير لوحدة التقارير
        /// </summary>
        /// <param name="parent">وحدة التقارير الرئيسية</param>
        /// <param name="text">اسم التقرير</param>
        /// <param name="tag">الاسم المميز للتقرير</param>
        /// <param name="image">رمز التقرير</param>
        private void AddReportItem(AccordionControlElement parent, string text, string tag, System.Drawing.Image image)
        {
            try
            {
                // التحقق من وجود العنصر مسبقاً
                foreach (AccordionControlElement item in parent.Elements)
                {
                    if (item.Tag != null && item.Tag.ToString() == tag)
                    {
                        return; // العنصر موجود بالفعل
                    }
                }
                
                // إنشاء عنصر التقرير
                AccordionControlElement reportItem = new AccordionControlElement
                {
                    Text = text,
                    Style = ElementStyle.Item,
                    Tag = tag
                };
                
                // تعيين الرمز
                if (image != null)
                {
                    reportItem.ImageOptions.Image = image;
                }
                
                // إضافة حدث النقر
                reportItem.Click += ReportItem_Click;
                
                // إضافة العنصر للقائمة
                parent.Elements.Add(reportItem);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إضافة عنصر تقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// حدث النقر على عنصر تقرير
        /// </summary>
        private void ReportItem_Click(object sender, EventArgs e)
        {
            try
            {
                // الحصول على العنصر المحدد
                AccordionControlElement element = sender as AccordionControlElement;
                if (element == null || element.Tag == null) return;
                
                // عرض التقرير المناسب
                ShowSpecificReport(element.Tag.ToString());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء فتح التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// عرض نموذج تقرير محدد
        /// </summary>
        /// <param name="reportTag">الاسم المميز للتقرير</param>
        private void ShowSpecificReport(string reportTag)
        {
            try
            {
                // إغلاق النموذج الحالي في منطقة العرض إذا كان موجوداً
                CloseCurrentForm();
                
                // تحديد النموذج المطلوب عرضه بناءً على الاسم المميز
                XtraForm reportForm = null;
                
                switch (reportTag)
                {
                    case "EmployeeReport":
                        reportForm = new EmployeeReportForm();
                        break;
                    
                    case "AttendanceReport":
                        reportForm = new AttendanceReportForm();
                        break;
                    
                    case "LeaveReport":
                        reportForm = new LeaveReportForm();
                        break;
                    
                    case "PayrollReport":
                        // سيتم تنفيذ تقرير الرواتب لاحقاً
                        reportForm = new PayrollReportForm();
                        break;
                    
                    case "OperationsReport":
                        // سيتم تنفيذ تقرير العمليات والمخالفات لاحقاً
                        reportForm = new OperationsReportForm();
                        break;
                    
                    case "CustomReportGenerator":
                        // سيتم تنفيذ مولد التقارير المخصصة لاحقاً
                        reportForm = new CustomReportGeneratorForm();
                        break;
                    
                    default:
                        XtraMessageBox.Show("التقرير غير متوفر حالياً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                }
                
                if (reportForm != null)
                {
                    // إعداد النموذج ليظهر في المنطقة الرئيسية
                    reportForm.TopLevel = false;
                    reportForm.FormBorderStyle = FormBorderStyle.None;
                    reportForm.Dock = DockStyle.Fill;
                    
                    // عرض النموذج في المنطقة الرئيسية
                    mainPanel.Controls.Add(reportForm);
                    reportForm.Show();
                    currentForm = reportForm;
                    
                    // تغيير عنوان النموذج الرئيسي
                    lblFormTitle.Text = reportForm.Text;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء عرض التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}