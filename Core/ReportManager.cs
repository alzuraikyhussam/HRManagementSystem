using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Drawing;
using HR.DataAccess;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.Data;
using System.Windows.Forms;
using HR.Core.Reports;
using System.Drawing.Imaging;

namespace HR.Core
{
    /// <summary>
    /// مدير إنشاء وعرض التقارير
    /// </summary>
    public class ReportManager
    {
        private readonly ConnectionManager _connectionManager;
        
        // الحجم القياسي لورقة A4 بالملم (بحسب DevExpress)
        private const float A4_WIDTH_MM = 210f;
        private const float A4_HEIGHT_MM = 297f;
        
        public ReportManager()
        {
            _connectionManager = new ConnectionManager();
        }
        
        /// <summary>
        /// الحصول على صورة خلفية التقرير من الموارد
        /// </summary>
        private Image GetReportBackgroundImage()
        {
            try
            {
                // استخدام الصورة من موارد المشروع
                return Properties.Resources.ReportBackground;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في استرجاع صورة خلفية التقرير");
                return null;
            }
        }
        
        #region القالب الأساسي للتقارير
        
        /// <summary>
        /// إنشاء التقرير الأساسي مع القالب الموحد
        /// </summary>
        /// <param name="reportTitle">عنوان التقرير</param>
        /// <returns>تقرير جديد بالقالب الموحد</returns>
        public XtraReport CreateBaseReport(string reportTitle)
        {
            // إنشاء التقرير الجديد
            XtraReport report = new XtraReport();
            
            // ضبط إعدادات التقرير
            report.DisplayName = reportTitle;
            report.Font = new Font("Arial", 9);
            report.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            report.RightToLeftLayout = DevExpress.XtraReports.UI.RightToLeftLayout.Yes;
            
            // تعيين حجم الورقة إلى A4 بشكل صريح
            report.PaperKind = System.Drawing.Printing.PaperKind.A4; // A4 (210mm x 297mm)
            report.PageWidth = A4_WIDTH_MM; // 210mm
            report.PageHeight = A4_HEIGHT_MM; // 297mm
            report.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.Millimeters; // تعيين وحدة القياس إلى ملم
            
            // التأكد من تعيين الهوامش بشكل دقيق (بالملم)
            report.Margins = new System.Drawing.Printing.Margins(10, 10, 15, 15);
            
            // إضافة الخلفية للتقرير
            SetReportBackground(report);
            
            // إعداد العناوين والتذييل
            SetupReportHeaderAndFooter(report, reportTitle);
            
            return report;
        }
        
        /// <summary>
        /// تعيين خلفية التقرير
        /// </summary>
        /// <param name="report">التقرير</param>
        private void SetReportBackground(XtraReport report)
        {
            try
            {
                // الحصول على صورة الخلفية
                Image backgroundImage = GetReportBackgroundImage();
                
                if (backgroundImage != null)
                {
                    // تعيين خلفية التقرير
                    report.Watermark.ImageSource = new ImageSource(backgroundImage);
                    report.Watermark.ImageViewMode = ImageViewMode.Fit; // تضبيط لتغطية الورقة بالكامل مع المحافظة على النسب
                    report.Watermark.ImageTransparency = 0; // خلفية غير شفافة
                    report.Watermark.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    report.Watermark.ShowBehind = true;
                    report.Watermark.PageRange = "1-"; // تطبيق على جميع الصفحات
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في تعيين خلفية التقرير");
            }
        }
        
        /// <summary>
        /// إعداد ترويسة وتذييل التقرير
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="reportTitle">عنوان التقرير</param>
        private void SetupReportHeaderAndFooter(XtraReport report, string reportTitle)
        {
            // جلب بيانات الشركة
            var companyInfo = GetCompanyInfo();
            string companyName = companyInfo["Name"];
            string companyAddress = companyInfo["Address"];
            string companyPhone = companyInfo["Phone"];
            byte[] companyLogo = companyInfo["Logo"] as byte[];
            
            #region إنشاء ترويسة التقرير ReportHeader
            
            // إنشاء ترويسة التقرير
            ReportHeaderBand reportHeaderBand = new ReportHeaderBand();
            reportHeaderBand.HeightF = 120; // ارتفاع الترويسة
            report.Bands.Add(reportHeaderBand);
            
            // شعار الشركة (في الوسط)
            PictureBox logoBox = new PictureBox();
            if (companyLogo != null && companyLogo.Length > 0)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(companyLogo))
                    {
                        logoBox.Image = Image.FromStream(ms);
                    }
                }
                catch
                {
                    logoBox.Image = null;
                }
            }
            
            XRPictureBox xrPictureBoxLogo = new XRPictureBox();
            xrPictureBoxLogo.ImageSource = new ImageSource(logoBox.Image);
            xrPictureBoxLogo.Sizing = ImageSizeMode.ZoomImage;
            xrPictureBoxLogo.LocationF = new PointF(report.PageWidth / 2 - 50, 5);
            xrPictureBoxLogo.SizeF = new SizeF(100, 70);
            reportHeaderBand.Controls.Add(xrPictureBoxLogo);
            
            // اسم الشركة (في اليمين)
            XRLabel xrLabelCompanyName = new XRLabel();
            xrLabelCompanyName.Text = companyName;
            xrLabelCompanyName.Font = new Font("Arial", 14, FontStyle.Bold);
            xrLabelCompanyName.LocationF = new PointF(report.PageWidth - 300, 5);
            xrLabelCompanyName.SizeF = new SizeF(280, 25);
            xrLabelCompanyName.TextAlignment = TextAlignment.MiddleRight;
            reportHeaderBand.Controls.Add(xrLabelCompanyName);
            
            // إدارة الموارد البشرية
            XRLabel xrLabelHRDept = new XRLabel();
            xrLabelHRDept.Text = "إدارة الموارد البشرية";
            xrLabelHRDept.Font = new Font("Arial", 12, FontStyle.Bold);
            xrLabelHRDept.LocationF = new PointF(report.PageWidth - 280, 30);
            xrLabelHRDept.SizeF = new SizeF(260, 20);
            xrLabelHRDept.TextAlignment = TextAlignment.MiddleRight;
            reportHeaderBand.Controls.Add(xrLabelHRDept);
            
            // عنوان الشركة
            XRLabel xrLabelCompanyAddress = new XRLabel();
            xrLabelCompanyAddress.Text = companyAddress;
            xrLabelCompanyAddress.Font = new Font("Arial", 10);
            xrLabelCompanyAddress.LocationF = new PointF(report.PageWidth - 280, 50);
            xrLabelCompanyAddress.SizeF = new SizeF(260, 20);
            xrLabelCompanyAddress.TextAlignment = TextAlignment.MiddleRight;
            reportHeaderBand.Controls.Add(xrLabelCompanyAddress);
            
            // رقم هاتف الشركة
            XRLabel xrLabelCompanyPhone = new XRLabel();
            xrLabelCompanyPhone.Text = "هاتف: " + companyPhone;
            xrLabelCompanyPhone.Font = new Font("Arial", 10);
            xrLabelCompanyPhone.LocationF = new PointF(report.PageWidth - 280, 70);
            xrLabelCompanyPhone.SizeF = new SizeF(260, 20);
            xrLabelCompanyPhone.TextAlignment = TextAlignment.MiddleRight;
            reportHeaderBand.Controls.Add(xrLabelCompanyPhone);
            
            // خط أفقي تحت الترويسة
            XRLine xrLineHeader = new XRLine();
            xrLineHeader.LocationF = new PointF(0, 95);
            xrLineHeader.SizeF = new SizeF(report.PageWidth - 20, 2);
            xrLineHeader.LineWidth = 2;
            reportHeaderBand.Controls.Add(xrLineHeader);
            
            // عنوان التقرير
            XRLabel xrLabelReportTitle = new XRLabel();
            xrLabelReportTitle.Text = reportTitle;
            xrLabelReportTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            xrLabelReportTitle.LocationF = new PointF(10, 100);
            xrLabelReportTitle.SizeF = new SizeF(report.PageWidth - 20, 25);
            xrLabelReportTitle.TextAlignment = TextAlignment.MiddleCenter;
            reportHeaderBand.Controls.Add(xrLabelReportTitle);
            
            #endregion
            
            #region إنشاء تذييل الصفحة PageFooter
            
            // إنشاء تذييل الصفحة
            PageFooterBand pageFooterBand = new PageFooterBand();
            pageFooterBand.HeightF = 50;
            report.Bands.Add(pageFooterBand);
            
            // خط أفقي فوق التذييل
            XRLine xrLineFooter = new XRLine();
            xrLineFooter.LocationF = new PointF(0, 5);
            xrLineFooter.SizeF = new SizeF(report.PageWidth - 20, 2);
            xrLineFooter.LineWidth = 1;
            pageFooterBand.Controls.Add(xrLineFooter);
            
            // اسم المستخدم (في اليمين)
            XRLabel xrLabelUserName = new XRLabel();
            string userFullName = string.Empty;
            if (SessionManager.CurrentUser != null)
            {
                var nameParts = SessionManager.CurrentUser.FullName.Split(' ');
                if (nameParts.Length >= 3)
                {
                    userFullName = $"{nameParts[0]} {nameParts[1]} {nameParts[2]}";
                }
                else
                {
                    userFullName = SessionManager.CurrentUser.FullName;
                }
            }
            xrLabelUserName.Text = userFullName;
            xrLabelUserName.Font = new Font("Arial", 9);
            xrLabelUserName.LocationF = new PointF(report.PageWidth - 270, 10);
            xrLabelUserName.SizeF = new SizeF(250, 20);
            xrLabelUserName.TextAlignment = TextAlignment.MiddleRight;
            pageFooterBand.Controls.Add(xrLabelUserName);
            
            // تاريخ الطباعة (في اليسار)
            XRLabel xrLabelPrintDate = new XRLabel();
            xrLabelPrintDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            xrLabelPrintDate.Font = new Font("Arial", 9);
            xrLabelPrintDate.LocationF = new PointF(10, 10);
            xrLabelPrintDate.SizeF = new SizeF(200, 20);
            xrLabelPrintDate.TextAlignment = TextAlignment.MiddleLeft;
            pageFooterBand.Controls.Add(xrLabelPrintDate);
            
            // رقم الصفحة (في الوسط)
            XRPageInfo xrPageInfo = new XRPageInfo();
            xrPageInfo.LocationF = new PointF(report.PageWidth / 2 - 50, 10);
            xrPageInfo.SizeF = new SizeF(100, 20);
            xrPageInfo.TextAlignment = TextAlignment.MiddleCenter;
            xrPageInfo.TextFormatString = "صفحة {0} من {1}";
            xrPageInfo.Font = new Font("Arial", 9);
            pageFooterBand.Controls.Add(xrPageInfo);
            
            #endregion
        }
        
        /// <summary>
        /// الحصول على بيانات الشركة
        /// </summary>
        /// <returns>قاموس بمعلومات الشركة</returns>
        private Dictionary<string, object> GetCompanyInfo()
        {
            Dictionary<string, object> companyInfo = new Dictionary<string, object>();
            
            try
            {
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    
                    string query = @"SELECT 
                        Name, LegalName, Address, Phone, Email, Website, Logo 
                        FROM Company 
                        WHERE ID = (SELECT TOP 1 ID FROM Company ORDER BY ID)";
                    
                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                companyInfo["Name"] = reader["Name"].ToString();
                                companyInfo["LegalName"] = reader["LegalName"] != DBNull.Value ? reader["LegalName"].ToString() : string.Empty;
                                companyInfo["Address"] = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : string.Empty;
                                companyInfo["Phone"] = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : string.Empty;
                                companyInfo["Email"] = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty;
                                companyInfo["Website"] = reader["Website"] != DBNull.Value ? reader["Website"].ToString() : string.Empty;
                                
                                if (reader["Logo"] != DBNull.Value)
                                {
                                    companyInfo["Logo"] = (byte[])reader["Logo"];
                                }
                                else
                                {
                                    companyInfo["Logo"] = null;
                                }
                            }
                            else
                            {
                                // إذا لم يتم العثور على بيانات الشركة، يتم تعيين قيم افتراضية
                                companyInfo["Name"] = "شركة نظام الموارد البشرية";
                                companyInfo["LegalName"] = string.Empty;
                                companyInfo["Address"] = "المملكة العربية السعودية";
                                companyInfo["Phone"] = "0000000000";
                                companyInfo["Email"] = string.Empty;
                                companyInfo["Website"] = string.Empty;
                                companyInfo["Logo"] = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في استرجاع بيانات الشركة للتقرير");
                
                // في حالة حدوث خطأ، يتم تعيين قيم افتراضية
                companyInfo["Name"] = "شركة نظام الموارد البشرية";
                companyInfo["LegalName"] = string.Empty;
                companyInfo["Address"] = "المملكة العربية السعودية";
                companyInfo["Phone"] = "0000000000";
                companyInfo["Email"] = string.Empty;
                companyInfo["Website"] = string.Empty;
                companyInfo["Logo"] = null;
            }
            
            return companyInfo;
        }
        
        #endregion
        
        #region طرق عرض وطباعة التقارير
        
        /// <summary>
        /// عرض التقرير في المعاينة
        /// </summary>
        /// <param name="report">التقرير المراد عرضه</param>
        public void ShowPreview(XtraReport report)
        {
            try
            {
                report.ShowPreview();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في عرض معاينة التقرير");
                XtraMessageBox.Show("حدث خطأ أثناء عرض معاينة التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// طباعة التقرير مباشرة
        /// </summary>
        /// <param name="report">التقرير المراد طباعته</param>
        public void Print(XtraReport report)
        {
            try
            {
                report.Print();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في طباعة التقرير");
                XtraMessageBox.Show("حدث خطأ أثناء طباعة التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تصدير التقرير إلى ملف PDF
        /// </summary>
        /// <param name="report">التقرير المراد تصديره</param>
        /// <param name="filePath">مسار الملف</param>
        public void ExportToPdf(XtraReport report, string filePath)
        {
            try
            {
                report.ExportToPdf(filePath);
                XtraMessageBox.Show("تم تصدير التقرير بنجاح إلى: " + filePath, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في تصدير التقرير إلى PDF");
                XtraMessageBox.Show("حدث خطأ أثناء تصدير التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تصدير التقرير إلى ملف Excel
        /// </summary>
        /// <param name="report">التقرير المراد تصديره</param>
        /// <param name="filePath">مسار الملف</param>
        public void ExportToExcel(XtraReport report, string filePath)
        {
            try
            {
                report.ExportToXlsx(filePath);
                XtraMessageBox.Show("تم تصدير التقرير بنجاح إلى: " + filePath, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في تصدير التقرير إلى Excel");
                XtraMessageBox.Show("حدث خطأ أثناء تصدير التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تصدير التقرير إلى صورة
        /// </summary>
        /// <param name="report">التقرير المراد تصديره</param>
        /// <param name="filePath">مسار الملف</param>
        public void ExportToImage(XtraReport report, string filePath)
        {
            try
            {
                report.ExportToImage(filePath);
                XtraMessageBox.Show("تم تصدير التقرير بنجاح إلى: " + filePath, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في تصدير التقرير إلى صورة");
                XtraMessageBox.Show("حدث خطأ أثناء تصدير التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// تصدير التقرير إلى Word
        /// </summary>
        /// <param name="report">التقرير المراد تصديره</param>
        /// <param name="filePath">مسار الملف</param>
        public void ExportToWord(XtraReport report, string filePath)
        {
            try
            {
                report.ExportToDocx(filePath);
                XtraMessageBox.Show("تم تصدير التقرير بنجاح إلى: " + filePath, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "خطأ في تصدير التقرير إلى Word");
                XtraMessageBox.Show("حدث خطأ أثناء تصدير التقرير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion
    }
}