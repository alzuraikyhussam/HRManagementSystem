using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Deductions
{
    /// <summary>
    /// نموذج عرض قواعد الخصم
    /// </summary>
    public partial class DeductionRulesListForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly DeductionRepository _deductionRepository;
        
        /// <summary>
        /// منشئ النموذج
        /// </summary>
        public DeductionRulesListForm()
        {
            InitializeComponent();
            
            _deductionRepository = new DeductionRepository();
        }
        
        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void DeductionRulesListForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        
        /// <summary>
        /// تحميل البيانات
        /// </summary>
        private void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // الحصول على جميع قواعد الخصم
                var rules = _deductionRepository.GetAllDeductionRules();
                
                // عرض البيانات
                deductionRuleBindingSource.DataSource = rules;
                
                // تحديث حالة الشريط
                ribbonStatusBar.Description = $"عدد القواعد: {rules.Count}";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل قواعد الخصم");
                XtraMessageBox.Show("حدث خطأ أثناء تحميل البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// حدث النقر على زر التحديث
        /// </summary>
        private void barButtonItemRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadData();
        }
        
        /// <summary>
        /// حدث النقر على زر إضافة قاعدة
        /// </summary>
        private void barButtonItemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var form = new DeductionRuleForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        
        /// <summary>
        /// حدث النقر على زر تعديل
        /// </summary>
        private void barButtonItemEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditSelectedRule();
        }
        
        /// <summary>
        /// حدث النقر على زر حذف
        /// </summary>
        private void barButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteSelectedRule();
        }
        
        /// <summary>
        /// حدث النقر على زر الطباعة
        /// </summary>
        private void barButtonItemPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControlRules.ShowPrintPreview();
        }
        
        /// <summary>
        /// حدث النقر على زر التصدير
        /// </summary>
        private void barButtonItemExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "ملف إكسل (*.xlsx)|*.xlsx|ملف CSV (*.csv)|*.csv";
                saveDialog.FileName = "قواعد_الخصم";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string extension = System.IO.Path.GetExtension(saveDialog.FileName).ToLower();
                    
                    if (extension == ".xlsx")
                    {
                        gridControlRules.ExportToXlsx(saveDialog.FileName);
                    }
                    else if (extension == ".csv")
                    {
                        gridControlRules.ExportToCsv(saveDialog.FileName);
                    }
                    
                    if (XtraMessageBox.Show("تم تصدير البيانات بنجاح. هل تريد فتح الملف؟", "تصدير", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
            }
        }
        
        /// <summary>
        /// تعديل القاعدة المحددة
        /// </summary>
        private void EditSelectedRule()
        {
            var focusedRow = gridViewRules.GetFocusedRow() as DeductionRule;
            if (focusedRow == null)
            {
                XtraMessageBox.Show("الرجاء تحديد قاعدة أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            using (var form = new DeductionRuleForm(focusedRow))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        
        /// <summary>
        /// حذف القاعدة المحددة
        /// </summary>
        private void DeleteSelectedRule()
        {
            var focusedRow = gridViewRules.GetFocusedRow() as DeductionRule;
            if (focusedRow == null)
            {
                XtraMessageBox.Show("الرجاء تحديد قاعدة أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (XtraMessageBox.Show($"هل أنت متأكد من حذف قاعدة الخصم '{focusedRow.Name}'؟", 
                "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool result = _deductionRepository.DeleteDeductionRule(focusedRow.ID);
                    
                    if (result)
                    {
                        LoadData();
                        XtraMessageBox.Show("تم حذف القاعدة بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("فشل في حذف القاعدة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.LogException(ex, $"فشل في حذف قاعدة الخصم {focusedRow.ID}");
                    XtraMessageBox.Show("حدث خطأ أثناء حذف القاعدة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        /// <summary>
        /// حدث النقر على زر التعديل في الجدول
        /// </summary>
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            EditSelectedRule();
        }
        
        /// <summary>
        /// حدث النقر على زر الحذف في الجدول
        /// </summary>
        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DeleteSelectedRule();
        }
    }
}