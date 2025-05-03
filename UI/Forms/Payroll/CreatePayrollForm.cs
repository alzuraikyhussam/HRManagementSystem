using System;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.DataAccess;

namespace HR.UI.Forms.Payroll
{
    public partial class CreatePayrollForm : XtraForm
    {
        private readonly PayrollRepository _payrollRepository;
        
        public CreatePayrollForm()
        {
            InitializeComponent();
            _payrollRepository = new PayrollRepository();
        }
        
        private void CreatePayrollForm_Load(object sender, EventArgs e)
        {
            try
            {
                // تهيئة عناصر التحكم
                InitializeControls();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل النموذج: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// تهيئة عناصر التحكم
        /// </summary>
        private void InitializeControls()
        {
            // تعبئة قائمة الشهور
            comboBoxEditMonth.Properties.Items.Clear();
            string[] months = new string[]
            {
                "يناير", "فبراير", "مارس", "أبريل", "مايو", "يونيو",
                "يوليو", "أغسطس", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر"
            };
            
            for (int i = 0; i < months.Length; i++)
            {
                comboBoxEditMonth.Properties.Items.Add(new MonthItem
                {
                    Month = i + 1,
                    Name = months[i]
                });
            }
            
            // تعيين الشهر الحالي
            int currentMonth = DateTime.Now.Month;
            comboBoxEditMonth.SelectedIndex = currentMonth - 1;
            
            // تعبئة قائمة السنوات
            comboBoxEditYear.Properties.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 5; i <= currentYear + 5; i++)
            {
                comboBoxEditYear.Properties.Items.Add(i);
            }
            comboBoxEditYear.EditValue = currentYear;
            
            // تعيين تاريخي البداية والنهاية
            UpdateDates();
        }
        
        /// <summary>
        /// تحديث تواريخ البداية والنهاية
        /// </summary>
        private void UpdateDates()
        {
            if (comboBoxEditMonth.SelectedItem == null || comboBoxEditYear.EditValue == null)
                return;
                
            MonthItem selectedMonth = comboBoxEditMonth.SelectedItem as MonthItem;
            int selectedYear = (int)comboBoxEditYear.EditValue;
            
            if (selectedMonth != null)
            {
                // تعيين تاريخ بداية الشهر
                dateEditStartDate.DateTime = new DateTime(selectedYear, selectedMonth.Month, 1);
                
                // تعيين تاريخ نهاية الشهر
                dateEditEndDate.DateTime = new DateTime(selectedYear, selectedMonth.Month, 
                    DateTime.DaysInMonth(selectedYear, selectedMonth.Month));
            }
        }
        
        /// <summary>
        /// حدث تغيير الشهر
        /// </summary>
        private void comboBoxEditMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateDates();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحديث التواريخ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث تغيير السنة
        /// </summary>
        private void comboBoxEditYear_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateDates();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحديث التواريخ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر الإنشاء
        /// </summary>
        private void simpleButtonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من إدخال البيانات
                if (!ValidateInputs())
                    return;
                
                // الحصول على بيانات كشف الرواتب
                MonthItem selectedMonth = comboBoxEditMonth.SelectedItem as MonthItem;
                int selectedYear = (int)comboBoxEditYear.EditValue;
                DateTime startDate = dateEditStartDate.DateTime;
                DateTime endDate = dateEditEndDate.DateTime;
                
                // التحقق من عدم وجود كشف رواتب للشهر والسنة
                if (_payrollRepository.PayrollExistsForMonthAndYear(selectedMonth.Month, selectedYear))
                {
                    XtraMessageBox.Show($"يوجد بالفعل كشف رواتب لشهر {selectedMonth.Name} {selectedYear}.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // تأكيد العملية
                if (XtraMessageBox.Show($"هل تريد إنشاء كشف رواتب لشهر {selectedMonth.Name} {selectedYear}؟", "تأكيد", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // إنشاء كشف الرواتب
                    int payrollId = _payrollRepository.CreatePayroll(selectedMonth.Month, selectedYear, startDate, endDate);
                    
                    if (payrollId > 0)
                    {
                        XtraMessageBox.Show("تم إنشاء كشف الرواتب بنجاح.", "نجاح", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // إغلاق النموذج
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("فشل إنشاء كشف الرواتب.", "خطأ", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء إنشاء كشف الرواتب: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث النقر على زر الإلغاء
        /// </summary>
        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        
        /// <summary>
        /// التحقق من إدخال البيانات
        /// </summary>
        /// <returns>نتيجة التحقق</returns>
        private bool ValidateInputs()
        {
            // التحقق من اختيار الشهر
            if (comboBoxEditMonth.SelectedItem == null)
            {
                XtraMessageBox.Show("يرجى اختيار الشهر.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxEditMonth.Focus();
                return false;
            }
            
            // التحقق من اختيار السنة
            if (comboBoxEditYear.EditValue == null)
            {
                XtraMessageBox.Show("يرجى اختيار السنة.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxEditYear.Focus();
                return false;
            }
            
            // التحقق من إدخال تاريخ البداية
            if (dateEditStartDate.EditValue == null)
            {
                XtraMessageBox.Show("يرجى إدخال تاريخ البداية.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateEditStartDate.Focus();
                return false;
            }
            
            // التحقق من إدخال تاريخ النهاية
            if (dateEditEndDate.EditValue == null)
            {
                XtraMessageBox.Show("يرجى إدخال تاريخ النهاية.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateEditEndDate.Focus();
                return false;
            }
            
            // التحقق من أن تاريخ النهاية أكبر من أو يساوي تاريخ البداية
            if (dateEditEndDate.DateTime < dateEditStartDate.DateTime)
            {
                XtraMessageBox.Show("يجب أن يكون تاريخ النهاية أكبر من أو يساوي تاريخ البداية.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateEditEndDate.Focus();
                return false;
            }
            
            return true;
        }
    }
    
    /// <summary>
    /// فئة عنصر الشهر
    /// </summary>
    public class MonthItem
    {
        /// <summary>
        /// رقم الشهر
        /// </summary>
        public int Month { get; set; }
        
        /// <summary>
        /// اسم الشهر
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// تمثيل العنصر كنص
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}