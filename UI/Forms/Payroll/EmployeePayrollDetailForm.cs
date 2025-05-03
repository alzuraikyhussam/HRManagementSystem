using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using HR.Core;
using HR.Models;

namespace HR.UI.Forms.Payroll
{
    /// <summary>
    /// نموذج عرض تفاصيل راتب موظف
    /// </summary>
    public partial class EmployeePayrollDetailForm : XtraForm
    {
        private readonly PayrollDetail _payrollDetail;
        
        public EmployeePayrollDetailForm(PayrollDetail payrollDetail)
        {
            InitializeComponent();
            _payrollDetail = payrollDetail;
        }
        
        private void EmployeePayrollDetailForm_Load(object sender, EventArgs e)
        {
            try
            {
                // تعيين عنوان النموذج
                this.Text = $"تفاصيل راتب الموظف: {_payrollDetail.EmployeeName}";
                
                // عرض بيانات راتب الموظف
                DisplayEmployeePayrollData();
                
                // عرض تفاصيل مكونات الراتب
                componentDetailBindingSource.DataSource = _payrollDetail.ComponentDetails;
                gridViewComponentDetails.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// عرض بيانات راتب الموظف
        /// </summary>
        private void DisplayEmployeePayrollData()
        {
            textEditEmployeeName.Text = _payrollDetail.EmployeeName;
            textEditJobTitle.Text = _payrollDetail.JobTitle;
            textEditDepartment.Text = _payrollDetail.Department;
            textEditWorkingDays.Text = _payrollDetail.WorkingDays.ToString();
            textEditPresentDays.Text = _payrollDetail.PresentDays.ToString();
            textEditAbsentDays.Text = _payrollDetail.AbsentDays.ToString();
            textEditLeaveDays.Text = _payrollDetail.LeaveDays.ToString();
            textEditLateMinutes.Text = _payrollDetail.LateMinutes.ToString();
            textEditOvertimeHours.Text = _payrollDetail.OvertimeHours.ToString();
            textEditBaseSalary.Text = _payrollDetail.BaseSalary.ToString("N2");
            textEditTotalAllowances.Text = _payrollDetail.TotalAllowances.ToString("N2");
            textEditTotalDeductions.Text = _payrollDetail.TotalDeductions.ToString("N2");
            textEditOvertimeAmount.Text = _payrollDetail.OvertimeAmount.ToString("N2");
            textEditNetSalary.Text = _payrollDetail.NetSalary.ToString("N2");
            textEditStatus.Text = _payrollDetail.Status;
        }
        
        /// <summary>
        /// حدث النقر على زر الطباعة
        /// </summary>
        private void simpleButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintingSystem printingSystem = new PrintingSystem();
                PrintableComponentLink link = new PrintableComponentLink(printingSystem);
                
                // إنشاء تقرير مخصص
                ReportPrintTool printTool = new ReportPrintTool(new XtraReport());
                
                // إعداد التقرير
                ((XtraReport)printTool.Report).DataSource = componentDetailBindingSource;
                ((XtraReport)printTool.Report).CreateDocument();
                
                // عرض معاينة الطباعة
                printTool.ShowPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء الطباعة: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
    }
    
    /// <summary>
    /// تقرير كشف الراتب
    /// </summary>
    public class XtraReport : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            // تهيئة التقرير
        }
    }
}