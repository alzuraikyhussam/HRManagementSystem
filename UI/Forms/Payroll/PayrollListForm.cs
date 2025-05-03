using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using HR.Core;
using HR.DataAccess;

namespace HR.UI.Forms.Payroll
{
    public partial class PayrollListForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly PayrollRepository _payrollRepository;
        private List<Models.Payroll> _payrolls;
        
        public PayrollListForm()
        {
            InitializeComponent();
            _payrollRepository = new PayrollRepository();
        }

        private void PayrollListForm_Load(object sender, EventArgs e)
        {
            try
            {
                // تهيئة عناصر التحكم
                InitializeControls();
                
                // تحميل البيانات
                LoadData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// تهيئة عناصر التحكم
        /// </summary>
        private void InitializeControls()
        {
            // تعبئة قائمة السنوات
            comboBoxEditYear.Properties.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 5; i <= currentYear + 5; i++)
            {
                comboBoxEditYear.Properties.Items.Add(i.ToString());
            }
            comboBoxEditYear.EditValue = currentYear.ToString();
            
            // التحقق من الصلاحيات
            CheckPermissions();
        }
        
        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckPermissions()
        {
            // التحقق من صلاحية إنشاء كشف رواتب جديد
            barButtonItemCreate.Enabled = SessionManager.HasPermission("Payroll.Create");
            
            // التحقق من صلاحية اعتماد كشف الرواتب
            barButtonItemApprove.Enabled = SessionManager.HasPermission("Payroll.Approve");
            
            // التحقق من صلاحية إقفال كشف الرواتب
            barButtonItemClose.Enabled = SessionManager.HasPermission("Payroll.Close");
            
            // التحقق من صلاحية إلغاء كشف الرواتب
            barButtonItemCancel.Enabled = SessionManager.HasPermission("Payroll.Cancel");
            
            // التحقق من صلاحية الطباعة
            barButtonItemPrint.Enabled = SessionManager.HasPermission("Payroll.Print");
            
            // التحقق من صلاحية التصدير
            barButtonItemExport.Enabled = SessionManager.HasPermission("Payroll.Export");
            
            // التحقق من صلاحية إدارة عناصر الرواتب
            barButtonItemSalaryComponents.Enabled = SessionManager.HasPermission("Payroll.ManageSalaryComponents");
            
            // التحقق من صلاحية إدارة رواتب الموظفين
            barButtonItemEmployeeSalaries.Enabled = SessionManager.HasPermission("Payroll.ManageEmployeeSalaries");
        }
        
        /// <summary>
        /// تحميل البيانات
        /// </summary>
        private void LoadData()
        {
            try
            {
                // تحميل كشوف الرواتب
                LoadPayrolls();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// تحميل كشوف الرواتب
        /// </summary>
        private void LoadPayrolls()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // الحصول على السنة المختارة
                int year = int.Parse(comboBoxEditYear.EditValue.ToString());
                
                // الحصول على كشوف الرواتب
                _payrolls = _payrollRepository.GetAllPayrolls()
                    .Where(p => p.PayrollYear == year)
                    .OrderByDescending(p => p.PayrollYear)
                    .ThenByDescending(p => p.PayrollMonth)
                    .ToList();
                
                // عرض البيانات
                payrollBindingSource.DataSource = _payrolls;
                gridViewPayrolls.RefreshData();
                
                // تحديث حالة الأزرار
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل كشوف الرواتب: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonsState()
        {
            // الحصول على كشف الرواتب المحدد
            Models.Payroll selectedPayroll = GetSelectedPayroll();
            
            if (selectedPayroll != null)
            {
                // تحديث حالة زر الاعتماد
                barButtonItemApprove.Enabled = selectedPayroll.Status == "Calculated" &&
                    SessionManager.HasPermission("Payroll.Approve");
                
                // تحديث حالة زر الإقفال
                barButtonItemClose.Enabled = selectedPayroll.Status == "Approved" &&
                    SessionManager.HasPermission("Payroll.Close");
                
                // تحديث حالة زر الإلغاء
                barButtonItemCancel.Enabled = (selectedPayroll.Status == "Created" || selectedPayroll.Status == "Calculated") &&
                    SessionManager.HasPermission("Payroll.Cancel");
            }
            else
            {
                barButtonItemApprove.Enabled = false;
                barButtonItemClose.Enabled = false;
                barButtonItemCancel.Enabled = false;
            }
        }
        
        /// <summary>
        /// الحصول على كشف الرواتب المحدد
        /// </summary>
        /// <returns>كشف الرواتب المحدد</returns>
        private Models.Payroll GetSelectedPayroll()
        {
            if (gridViewPayrolls.GetSelectedRows().Length > 0)
            {
                int rowHandle = gridViewPayrolls.GetSelectedRows()[0];
                if (rowHandle >= 0)
                {
                    return gridViewPayrolls.GetRow(rowHandle) as Models.Payroll;
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// حدث النقر على زر التحديث
        /// </summary>
        private void barButtonItemRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحديث البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر إنشاء كشف رواتب
        /// </summary>
        private void barButtonItemCreate_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // فتح نموذج إنشاء كشف رواتب جديد
                CreatePayrollForm createForm = new CreatePayrollForm();
                if (createForm.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء إنشاء كشف رواتب جديد: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر اعتماد كشف رواتب
        /// </summary>
        private void barButtonItemApprove_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على كشف الرواتب المحدد
                Models.Payroll selectedPayroll = GetSelectedPayroll();
                
                if (selectedPayroll != null)
                {
                    // التأكد من أن كشف الرواتب في حالة محسوب
                    if (selectedPayroll.Status != "Calculated")
                    {
                        XtraMessageBox.Show("لا يمكن اعتماد كشف الرواتب إلا في حالة محسوب.", "تنبيه", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // تأكيد العملية
                    if (XtraMessageBox.Show($"هل تريد اعتماد كشف الرواتب {selectedPayroll.PayrollName}؟", "تأكيد", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // اعتماد كشف الرواتب
                        bool result = _payrollRepository.ApprovePayroll(selectedPayroll.ID, SessionManager.CurrentUser.ID);
                        
                        if (result)
                        {
                            XtraMessageBox.Show("تم اعتماد كشف الرواتب بنجاح.", "نجاح", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            // إعادة تحميل البيانات
                            LoadData();
                        }
                        else
                        {
                            XtraMessageBox.Show("فشل اعتماد كشف الرواتب.", "خطأ", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("يرجى تحديد كشف رواتب أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء اعتماد كشف الرواتب: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر إقفال كشف رواتب
        /// </summary>
        private void barButtonItemClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على كشف الرواتب المحدد
                Models.Payroll selectedPayroll = GetSelectedPayroll();
                
                if (selectedPayroll != null)
                {
                    // التأكد من أن كشف الرواتب في حالة معتمد
                    if (selectedPayroll.Status != "Approved")
                    {
                        XtraMessageBox.Show("لا يمكن إقفال كشف الرواتب إلا في حالة معتمد.", "تنبيه", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // تأكيد العملية
                    if (XtraMessageBox.Show($"هل تريد إقفال كشف الرواتب {selectedPayroll.PayrollName}؟", "تأكيد", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // طلب تاريخ الدفع
                        PaymentDateForm paymentForm = new PaymentDateForm();
                        if (paymentForm.ShowDialog() == DialogResult.OK)
                        {
                            // إقفال كشف الرواتب
                            bool result = _payrollRepository.ClosePayroll(selectedPayroll.ID, paymentForm.PaymentDate);
                            
                            if (result)
                            {
                                XtraMessageBox.Show("تم إقفال كشف الرواتب بنجاح.", "نجاح", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                // إعادة تحميل البيانات
                                LoadData();
                            }
                            else
                            {
                                XtraMessageBox.Show("فشل إقفال كشف الرواتب.", "خطأ", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("يرجى تحديد كشف رواتب أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء إقفال كشف الرواتب: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر إلغاء كشف رواتب
        /// </summary>
        private void barButtonItemCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // الحصول على كشف الرواتب المحدد
                Models.Payroll selectedPayroll = GetSelectedPayroll();
                
                if (selectedPayroll != null)
                {
                    // التأكد من أن كشف الرواتب في حالة منشأ أو محسوب
                    if (selectedPayroll.Status != "Created" && selectedPayroll.Status != "Calculated")
                    {
                        XtraMessageBox.Show("لا يمكن إلغاء كشف الرواتب إلا في حالة منشأ أو محسوب.", "تنبيه", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // تأكيد العملية
                    if (XtraMessageBox.Show($"هل تريد إلغاء كشف الرواتب {selectedPayroll.PayrollName}؟", "تأكيد", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // إلغاء كشف الرواتب
                        bool result = _payrollRepository.CancelPayroll(selectedPayroll.ID);
                        
                        if (result)
                        {
                            XtraMessageBox.Show("تم إلغاء كشف الرواتب بنجاح.", "نجاح", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            // إعادة تحميل البيانات
                            LoadData();
                        }
                        else
                        {
                            XtraMessageBox.Show("فشل إلغاء كشف الرواتب.", "خطأ", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("يرجى تحديد كشف رواتب أولاً.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء إلغاء كشف الرواتب: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر طباعة
        /// </summary>
        private void barButtonItemPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // طباعة الجدول
                gridControlPayrolls.ShowPrintPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء الطباعة: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر تصدير
        /// </summary>
        private void barButtonItemExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // إنشاء حوار حفظ الملف
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "ملف إكسل (*.xlsx)|*.xlsx|ملف PDF (*.pdf)|*.pdf";
                saveFileDialog.Title = "تصدير البيانات";
                saveFileDialog.FileName = $"كشوف_الرواتب_{DateTime.Now:yyyy-MM-dd}";
                
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    
                    string fileName = saveFileDialog.FileName;
                    string extension = System.IO.Path.GetExtension(fileName).ToLower();
                    
                    // تصدير البيانات حسب نوع الملف
                    switch (extension)
                    {
                        case ".xlsx":
                            gridControlPayrolls.ExportToXlsx(fileName);
                            break;
                        case ".pdf":
                            gridControlPayrolls.ExportToPdf(fileName);
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
        /// حدث النقر على زر عناصر الرواتب
        /// </summary>
        private void barButtonItemSalaryComponents_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // فتح نموذج إدارة عناصر الرواتب
                SalaryComponentsForm componentsForm = new SalaryComponentsForm();
                componentsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء فتح نموذج عناصر الرواتب: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر رواتب الموظفين
        /// </summary>
        private void barButtonItemEmployeeSalaries_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // فتح نموذج إدارة رواتب الموظفين
                EmployeeSalariesForm salariesForm = new EmployeeSalariesForm();
                salariesForm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء فتح نموذج رواتب الموظفين: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر عرض
        /// </summary>
        private void simpleButtonFilter_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPayrolls();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على صف في الجدول
        /// </summary>
        private void gridViewPayrolls_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر العرض في الجدول
        /// </summary>
        private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                // الحصول على كشف الرواتب المحدد
                int rowHandle = gridViewPayrolls.FocusedRowHandle;
                if (rowHandle >= 0)
                {
                    Models.Payroll selectedPayroll = gridViewPayrolls.GetRow(rowHandle) as Models.Payroll;
                    if (selectedPayroll != null)
                    {
                        // فتح نموذج عرض تفاصيل كشف الرواتب
                        PayrollDetailsForm detailsForm = new PayrollDetailsForm(selectedPayroll.ID);
                        detailsForm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء عرض تفاصيل كشف الرواتب: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
    }
    
    /// <summary>
    /// نموذج إدخال تاريخ الدفع
    /// </summary>
    public class PaymentDateForm : XtraForm
    {
        private LabelControl labelControl1;
        private DateEdit dateEditPaymentDate;
        private SimpleButton simpleButtonOK;
        private SimpleButton simpleButtonCancel;
        
        public DateTime PaymentDate { get; private set; }
        
        public PaymentDateForm()
        {
            InitializeComponent();
            dateEditPaymentDate.DateTime = DateTime.Now.Date;
        }
        
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditPaymentDate = new DevExpress.XtraEditors.DateEdit();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPaymentDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPaymentDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(235, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "تاريخ الدفع:";
            // 
            // dateEditPaymentDate
            // 
            this.dateEditPaymentDate.EditValue = null;
            this.dateEditPaymentDate.Location = new System.Drawing.Point(20, 22);
            this.dateEditPaymentDate.Name = "dateEditPaymentDate";
            this.dateEditPaymentDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPaymentDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPaymentDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateEditPaymentDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditPaymentDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateEditPaymentDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditPaymentDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateEditPaymentDate.Size = new System.Drawing.Size(200, 20);
            this.dateEditPaymentDate.TabIndex = 1;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(20, 60);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOK.TabIndex = 2;
            this.simpleButtonOK.Text = "موافق";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonCancel.Location = new System.Drawing.Point(110, 60);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCancel.TabIndex = 3;
            this.simpleButtonCancel.Text = "إلغاء";
            // 
            // PaymentDateForm
            // 
            this.AcceptButton = this.simpleButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButtonCancel;
            this.ClientSize = new System.Drawing.Size(294, 100);
            this.Controls.Add(this.simpleButtonCancel);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.dateEditPaymentDate);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentDateForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "تاريخ الدفع";
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPaymentDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPaymentDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (dateEditPaymentDate.EditValue == null)
            {
                XtraMessageBox.Show("يرجى إدخال تاريخ الدفع.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            PaymentDate = dateEditPaymentDate.DateTime;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}