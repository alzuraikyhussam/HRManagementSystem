using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Payroll
{
    public partial class PayrollDetailsForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly PayrollRepository _payrollRepository;
        private readonly int _payrollId;
        private Models.Payroll _payroll;
        
        public PayrollDetailsForm(int payrollId)
        {
            InitializeComponent();
            _payrollRepository = new PayrollRepository();
            _payrollId = payrollId;
        }
        
        private void PayrollDetailsForm_Load(object sender, EventArgs e)
        {
            try
            {
                // تحميل كشف الراتب
                LoadPayroll();
                
                // تهيئة عناصر التحكم
                InitializeControls();
                
                // التحقق من الصلاحيات
                CheckPermissions();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// تحميل بيانات كشف الراتب
        /// </summary>
        private void LoadPayroll()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // الحصول على كشف الراتب
                _payroll = _payrollRepository.GetPayrollById(_payrollId);
                
                if (_payroll == null)
                {
                    XtraMessageBox.Show("لم يتم العثور على كشف الراتب المطلوب.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }
                
                // تعيين عنوان النموذج
                this.Text = $"تفاصيل كشف الرواتب: {_payroll.PayrollName}";
                
                // عرض بيانات كشف الراتب
                DisplayPayrollData();
                
                // عرض تفاصيل كشف الراتب
                payrollDetailBindingSource.DataSource = _payroll.PayrollDetails;
                gridViewPayrollDetails.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل بيانات كشف الراتب: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// عرض بيانات كشف الراتب
        /// </summary>
        private void DisplayPayrollData()
        {
            // تعبئة معلومات كشف الراتب
            textEditPayrollName.Text = _payroll.PayrollName;
            textEditPayrollPeriod.Text = _payroll.PayrollPeriod;
            textEditStartDate.Text = _payroll.StartDate.ToString("yyyy-MM-dd");
            textEditEndDate.Text = _payroll.EndDate.ToString("yyyy-MM-dd");
            textEditStatus.Text = GetStatusText(_payroll.Status);
            
            if (_payroll.PaymentDate.HasValue)
            {
                textEditPaymentDate.Text = _payroll.PaymentDate.Value.ToString("yyyy-MM-dd");
            }
            
            textEditTotalEmployees.Text = _payroll.TotalEmployees.ToString();
            textEditTotalBasicSalary.Text = _payroll.TotalBasicSalary.ToString("N2");
            textEditTotalAllowances.Text = _payroll.TotalAllowances.ToString("N2");
            textEditTotalDeductions.Text = _payroll.TotalDeductions.ToString("N2");
            textEditTotalOvertimeAmount.Text = _payroll.TotalOvertimeAmount.ToString("N2");
            textEditTotalPayrollAmount.Text = _payroll.TotalPayrollAmount.ToString("N2");
            
            // تلوين خلية الحالة
            switch (_payroll.Status)
            {
                case "Created":
                    textEditStatus.BackColor = Color.LightBlue;
                    break;
                case "Calculated":
                    textEditStatus.BackColor = Color.LightGreen;
                    break;
                case "Approved":
                    textEditStatus.BackColor = Color.Yellow;
                    break;
                case "Paid":
                    textEditStatus.BackColor = Color.Green;
                    textEditStatus.ForeColor = Color.White;
                    break;
                case "Cancelled":
                    textEditStatus.BackColor = Color.Red;
                    textEditStatus.ForeColor = Color.White;
                    break;
            }
        }
        
        /// <summary>
        /// الحصول على نص الحالة
        /// </summary>
        /// <param name="status">الحالة</param>
        /// <returns>نص الحالة</returns>
        private string GetStatusText(string status)
        {
            switch (status)
            {
                case "Created":
                    return "منشأ";
                case "Calculated":
                    return "محسوب";
                case "Approved":
                    return "معتمد";
                case "Paid":
                    return "مدفوع";
                case "Cancelled":
                    return "ملغي";
                default:
                    return status;
            }
        }
        
        /// <summary>
        /// تهيئة عناصر التحكم
        /// </summary>
        private void InitializeControls()
        {
            // تعيين خصائص الشبكة
            gridViewPayrollDetails.OptionsBehavior.ReadOnly = true;
            gridViewPayrollDetails.OptionsView.ShowFooter = true;
            
            // تعيين إعدادات التذييل
            colBaseSalary.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            colBaseSalary.SummaryItem.DisplayFormat = "المجموع: {0:N2}";
            
            colTotalAllowances.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            colTotalAllowances.SummaryItem.DisplayFormat = "المجموع: {0:N2}";
            
            colTotalDeductions.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            colTotalDeductions.SummaryItem.DisplayFormat = "المجموع: {0:N2}";
            
            colOvertimeAmount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            colOvertimeAmount.SummaryItem.DisplayFormat = "المجموع: {0:N2}";
            
            colNetSalary.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            colNetSalary.SummaryItem.DisplayFormat = "المجموع: {0:N2}";
        }
        
        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckPermissions()
        {
            // التحقق من صلاحية الطباعة
            barButtonItemPrint.Enabled = SessionManager.HasPermission("Payroll.Print");
            
            // التحقق من صلاحية التصدير
            barButtonItemExport.Enabled = SessionManager.HasPermission("Payroll.Export");
        }
        
        /// <summary>
        /// حدث النقر على زر الطباعة
        /// </summary>
        private void barButtonItemPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                PrintingSystem printingSystem = new PrintingSystem();
                PrintableComponentLink link = new PrintableComponentLink(printingSystem);
                
                link.Component = gridControlPayrollDetails;
                link.Landscape = true;
                link.PaperKind = System.Drawing.Printing.PaperKind.A4;
                
                // تعيين رأس الصفحة
                link.CreateMarginalHeaderArea += (s, args) =>
                {
                    args.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
                    args.Graph.Font = new Font("Arial", 16, FontStyle.Bold);
                    
                    RectangleF headerRect = new RectangleF(0, 5, args.Brick.RectangleF.Width, 25);
                    args.Graph.DrawString($"{_payroll.PayrollName}", Color.Black, headerRect, BorderSide.None);
                    
                    headerRect.Y += 30;
                    args.Graph.Font = new Font("Arial", 10, FontStyle.Regular);
                    args.Graph.DrawString($"الفترة: {_payroll.PayrollPeriod} | من: {_payroll.StartDate:yyyy-MM-dd} | إلى: {_payroll.EndDate:yyyy-MM-dd} | الحالة: {GetStatusText(_payroll.Status)}", 
                        Color.Black, headerRect, BorderSide.None);
                };
                
                // تعيين تذييل الصفحة
                link.CreateMarginalFooterArea += (s, args) =>
                {
                    args.Graph.StringFormat = new BrickStringFormat(StringAlignment.Far);
                    args.Graph.Font = new Font("Arial", 8);
                    
                    PageInfoBrick pageInfo = new PageInfoBrick(BorderSide.None, 50, Color.Black, Color.Transparent);
                    pageInfo.PageNumberFormat = "الصفحة {0} من {1}";
                    pageInfo.RectangleF = new RectangleF(0, 0, args.Brick.RectangleF.Width, 15);
                    args.Graph.DrawBrick(pageInfo);
                    
                    args.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
                    RectangleF footerRect = new RectangleF(0, 0, args.Brick.RectangleF.Width, 15);
                    args.Graph.DrawString($"تاريخ الطباعة: {DateTime.Now:yyyy-MM-dd HH:mm}", Color.Black, footerRect, BorderSide.None);
                };
                
                // عرض معاينة الطباعة
                link.ShowPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء الطباعة: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر التصدير
        /// </summary>
        private void barButtonItemExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // إنشاء حوار حفظ الملف
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "ملف إكسل (*.xlsx)|*.xlsx|ملف PDF (*.pdf)|*.pdf";
                saveFileDialog.Title = "تصدير البيانات";
                saveFileDialog.FileName = $"كشف_رواتب_{_payroll.PayrollPeriod.Replace("/", "_")}";
                
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    
                    string fileName = saveFileDialog.FileName;
                    string extension = System.IO.Path.GetExtension(fileName).ToLower();
                    
                    // تصدير البيانات حسب نوع الملف
                    switch (extension)
                    {
                        case ".xlsx":
                            gridControlPayrollDetails.ExportToXlsx(fileName);
                            break;
                        case ".pdf":
                            gridControlPayrollDetails.ExportToPdf(fileName);
                            break;
                    }
                    
                    // فتح الملف بعد التصدير
                    if (XtraMessageBox.Show("تم تصدير البيانات بنجاح. هل تريد فتح الملف؟", "نجاح",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تصدير البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// حدث النقر المزدوج على صف في الجدول
        /// </summary>
        private void gridViewPayrollDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // الحصول على التفاصيل المحددة
                GridView view = sender as GridView;
                if (view == null) return;
                
                int rowHandle = view.FocusedRowHandle;
                if (rowHandle < 0) return;
                
                PayrollDetail detail = view.GetRow(rowHandle) as PayrollDetail;
                if (detail == null) return;
                
                // فتح نموذج تفاصيل راتب الموظف
                EmployeePayrollDetailForm detailForm = new EmployeePayrollDetailForm(detail);
                detailForm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء عرض تفاصيل راتب الموظف: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
    }
}