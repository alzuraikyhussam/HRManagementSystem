using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;

namespace HR.UI.Forms.Leave
{
    /// <summary>
    /// نموذج عرض أنواع الإجازات
    /// </summary>
    public partial class LeaveTypesListForm : XtraForm
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// تهيئة نموذج عرض أنواع الإجازات
        /// </summary>
        public LeaveTypesListForm()
        {
            InitializeComponent();
            
            // تهيئة وحدة العمل
            _unitOfWork = new UnitOfWork();
            
            // تسجيل الأحداث
            this.Load += LeaveTypesListForm_Load;
            buttonAdd.Click += ButtonAdd_Click;
            buttonEdit.Click += ButtonEdit_Click;
            buttonDelete.Click += ButtonDelete_Click;
            buttonRefresh.Click += ButtonRefresh_Click;
            gridViewLeaveTypes.DoubleClick += GridViewLeaveTypes_DoubleClick;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void LeaveTypesListForm_Load(object sender, EventArgs e)
        {
            LoadLeaveTypes();
            SetupGridView();
        }

        /// <summary>
        /// إعداد عرض الشبكة
        /// </summary>
        private void SetupGridView()
        {
            // إعداد خصائص العرض
            gridViewLeaveTypes.OptionsBehavior.Editable = false;
            gridViewLeaveTypes.OptionsView.ShowGroupPanel = false;
            gridViewLeaveTypes.OptionsView.ShowIndicator = false;
            gridViewLeaveTypes.OptionsView.EnableAppearanceEvenRow = true;
            gridViewLeaveTypes.OptionsView.EnableAppearanceOddRow = true;
            
            // إعداد الأعمدة
            gridViewLeaveTypes.Columns["ID"].Caption = "الرقم";
            gridViewLeaveTypes.Columns["ID"].Width = 50;
            gridViewLeaveTypes.Columns["ID"].VisibleIndex = 0;
            
            gridViewLeaveTypes.Columns["Name"].Caption = "نوع الإجازة";
            gridViewLeaveTypes.Columns["Name"].Width = 150;
            gridViewLeaveTypes.Columns["Name"].VisibleIndex = 1;
            
            gridViewLeaveTypes.Columns["Description"].Caption = "الوصف";
            gridViewLeaveTypes.Columns["Description"].Width = 250;
            gridViewLeaveTypes.Columns["Description"].VisibleIndex = 2;
            
            gridViewLeaveTypes.Columns["DefaultDays"].Caption = "الأيام الافتراضية";
            gridViewLeaveTypes.Columns["DefaultDays"].Width = 80;
            gridViewLeaveTypes.Columns["DefaultDays"].VisibleIndex = 3;
            
            gridViewLeaveTypes.Columns["RequiresApproval"].Caption = "يتطلب موافقة";
            gridViewLeaveTypes.Columns["RequiresApproval"].Width = 80;
            gridViewLeaveTypes.Columns["RequiresApproval"].VisibleIndex = 4;
            
            gridViewLeaveTypes.Columns["IsPaid"].Caption = "مدفوعة الأجر";
            gridViewLeaveTypes.Columns["IsPaid"].Width = 80;
            gridViewLeaveTypes.Columns["IsPaid"].VisibleIndex = 5;
            
            gridViewLeaveTypes.Columns["IsActive"].Caption = "نشط";
            gridViewLeaveTypes.Columns["IsActive"].Width = 50;
            gridViewLeaveTypes.Columns["IsActive"].VisibleIndex = 6;
            
            // تنسيق العرض
            gridViewLeaveTypes.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewLeaveTypes.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            
            // تطبيق أفضل عرض للأعمدة
            gridViewLeaveTypes.BestFitColumns();
        }

        /// <summary>
        /// تحميل أنواع الإجازات
        /// </summary>
        private void LoadLeaveTypes()
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // الحصول على بيانات أنواع الإجازات
                var leaveTypes = _unitOfWork.LeaveRepository.GetAllLeaveTypes();
                
                // تعيين مصدر البيانات
                gridControlLeaveTypes.DataSource = leaveTypes;
                
                // تحديث حالة الأزرار
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل أنواع الإجازات");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل أنواع الإجازات: {ex.Message}",
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
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonsState()
        {
            bool hasSelectedRows = gridViewLeaveTypes.SelectedRowsCount > 0;
            buttonEdit.Enabled = hasSelectedRows;
            buttonDelete.Enabled = hasSelectedRows;
        }

        /// <summary>
        /// حدث نقر زر الإضافة
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // إنشاء نموذج نوع الإجازة
                var leaveTypeForm = new LeaveTypeForm();
                
                // عرض النموذج
                if (leaveTypeForm.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات بعد الإضافة
                    LoadLeaveTypes();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في فتح نموذج إضافة نوع إجازة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج إضافة نوع إجازة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث نقر زر التعديل
        /// </summary>
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            EditSelectedLeaveType();
        }

        /// <summary>
        /// حدث النقر المزدوج على الشبكة
        /// </summary>
        private void GridViewLeaveTypes_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedLeaveType();
        }

        /// <summary>
        /// تعديل نوع الإجازة المحدد
        /// </summary>
        private void EditSelectedLeaveType()
        {
            if (gridViewLeaveTypes.SelectedRowsCount == 0)
                return;
                
            try
            {
                // الحصول على معرف نوع الإجازة المحدد
                int leaveTypeId = Convert.ToInt32(gridViewLeaveTypes.GetFocusedRowCellValue("ID"));
                
                // إنشاء نموذج نوع الإجازة للتعديل
                var leaveTypeForm = new LeaveTypeForm(leaveTypeId);
                
                // عرض النموذج
                if (leaveTypeForm.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات بعد التعديل
                    LoadLeaveTypes();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في فتح نموذج تعديل نوع إجازة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج تعديل نوع إجازة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث نقر زر الحذف
        /// </summary>
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (gridViewLeaveTypes.SelectedRowsCount == 0)
                return;
                
            try
            {
                // الحصول على معرف نوع الإجازة المحدد
                int leaveTypeId = Convert.ToInt32(gridViewLeaveTypes.GetFocusedRowCellValue("ID"));
                string leaveTypeName = gridViewLeaveTypes.GetFocusedRowCellValue("Name").ToString();
                
                // التأكيد على الحذف
                DialogResult result = XtraMessageBox.Show(
                    $"هل أنت متأكد من حذف نوع الإجازة '{leaveTypeName}'؟",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    
                if (result != DialogResult.Yes)
                    return;
                    
                // حذف نوع الإجازة
                bool success = _unitOfWork.LeaveRepository.DeleteLeaveType(leaveTypeId);
                
                if (success)
                {
                    // إعادة تحميل البيانات بعد الحذف
                    LoadLeaveTypes();
                    
                    XtraMessageBox.Show(
                        "تم حذف نوع الإجازة بنجاح.",
                        "نجاح",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(
                        "فشل في حذف نوع الإجازة. قد يكون هناك إجازات مرتبطة بهذا النوع.",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في حذف نوع إجازة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء حذف نوع الإجازة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث نقر زر التحديث
        /// </summary>
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            LoadLeaveTypes();
        }

        /// <summary>
        /// التنظيف عند إغلاق النموذج
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            
            // التخلص من الموارد
            _unitOfWork.Dispose();
        }
    }
}