using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;
using HR.DataAccess;

namespace HR.UI.Forms.Settings
{
    public partial class WorkHoursForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly WorkHoursRepository _workHoursRepository;
        private DataTable _workHoursData;
        private bool _isEditing = false;
        private int _editingId = 0;
        
        public WorkHoursForm()
        {
            InitializeComponent();
            _workHoursRepository = new WorkHoursRepository();
        }
        
        private void WorkHoursForm_Load(object sender, EventArgs e)
        {
            try
            {
                // التحقق من الصلاحيات
                CheckPermissions();
                
                // تحميل البيانات
                LoadData();
                
                // تحديث حالة الأزرار
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckPermissions()
        {
            // التحقق من صلاحية عرض فترات العمل
            bool canView = SessionManager.HasPermission("Settings.ViewWorkHours");
            if (!canView)
            {
                XtraMessageBox.Show("ليس لديك صلاحية لعرض فترات العمل.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }
            
            // التحقق من صلاحية إضافة وتعديل وحذف فترات العمل
            bool canAdd = SessionManager.HasPermission("Settings.AddWorkHours");
            bool canEdit = SessionManager.HasPermission("Settings.EditWorkHours");
            bool canDelete = SessionManager.HasPermission("Settings.DeleteWorkHours");
            
            // تفعيل/تعطيل الأزرار حسب الصلاحيات
            barButtonItemAdd.Enabled = canAdd;
            barButtonItemEdit.Enabled = canEdit;
            barButtonItemDelete.Enabled = canDelete;
            barButtonItemSave.Enabled = canAdd || canEdit;
        }
        
        /// <summary>
        /// تحميل البيانات
        /// </summary>
        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;
            
            try
            {
                // استرجاع بيانات فترات العمل
                _workHoursData = _workHoursRepository.GetWorkHours();
                
                // ربط البيانات بالجدول
                gridControlWorkHours.DataSource = _workHoursData;
                
                // تحديد عرض الأعمدة
                GridView view = gridControlWorkHours.MainView as GridView;
                if (view != null)
                {
                    view.Columns["ID"].Visible = false;
                    view.Columns["CreatedAt"].Visible = false;
                    view.Columns["CreatedBy"].Visible = false;
                    view.Columns["UpdatedAt"].Visible = false;
                    view.Columns["UpdatedBy"].Visible = false;
                    
                    view.Columns["Name"].Caption = "الاسم";
                    view.Columns["Description"].Caption = "الوصف";
                    view.Columns["StartTime"].Caption = "وقت البدء";
                    view.Columns["EndTime"].Caption = "وقت الانتهاء";
                    view.Columns["TotalHours"].Caption = "إجمالي الساعات";
                    view.Columns["FlexibleMinutes"].Caption = "دقائق السماح للتأخير";
                    view.Columns["LateThresholdMinutes"].Caption = "الحد الأدنى لاحتساب التأخير";
                    view.Columns["ShortDayThresholdMinutes"].Caption = "الحد الأدنى لاحتساب المغادرة المبكرة";
                    view.Columns["OverTimeStartMinutes"].Caption = "الحد الأدنى لاحتساب العمل الإضافي";
                    
                    // تنسيق عرض الوقت
                    view.Columns["StartTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["StartTime"].DisplayFormat.FormatString = "hh:mm tt";
                    view.Columns["EndTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["EndTime"].DisplayFormat.FormatString = "hh:mm tt";
                    
                    // تنسيق عرض الساعات
                    view.Columns["TotalHours"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    view.Columns["TotalHours"].DisplayFormat.FormatString = "n2";
                    
                    // ترتيب الأعمدة
                    view.Columns["Name"].VisibleIndex = 0;
                    view.Columns["Description"].VisibleIndex = 1;
                    view.Columns["StartTime"].VisibleIndex = 2;
                    view.Columns["EndTime"].VisibleIndex = 3;
                    view.Columns["TotalHours"].VisibleIndex = 4;
                    view.Columns["FlexibleMinutes"].VisibleIndex = 5;
                    view.Columns["LateThresholdMinutes"].VisibleIndex = 6;
                    view.Columns["ShortDayThresholdMinutes"].VisibleIndex = 7;
                    view.Columns["OverTimeStartMinutes"].VisibleIndex = 8;
                }
                
                // تنظيف بيانات النموذج
                ClearForm();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل بيانات فترات العمل: {ex.Message}", "خطأ", 
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
            // تفعيل/تعطيل أزرار التحرير
            barButtonItemEdit.Enabled = gridView1.SelectedRowsCount > 0 && !_isEditing && SessionManager.HasPermission("Settings.EditWorkHours");
            barButtonItemDelete.Enabled = gridView1.SelectedRowsCount > 0 && !_isEditing && SessionManager.HasPermission("Settings.DeleteWorkHours");
            
            // تفعيل/تعطيل أزرار الحفظ والإلغاء
            barButtonItemSave.Enabled = _isEditing && (SessionManager.HasPermission("Settings.AddWorkHours") || SessionManager.HasPermission("Settings.EditWorkHours"));
            barButtonItemCancel.Enabled = _isEditing;
            
            // تفعيل/تعطيل عناصر التحرير
            panelControlEdit.Enabled = _isEditing;
            gridControlWorkHours.Enabled = !_isEditing;
        }
        
        /// <summary>
        /// تنظيف النموذج
        /// </summary>
        private void ClearForm()
        {
            textEditName.Text = string.Empty;
            textEditDescription.Text = string.Empty;
            timeEditStartTime.Time = DateTime.Today.AddHours(8);
            timeEditEndTime.Time = DateTime.Today.AddHours(16);
            spinEditFlexibleMinutes.Value = 0;
            spinEditLateThresholdMinutes.Value = 0;
            spinEditShortDayThresholdMinutes.Value = 0;
            spinEditOverTimeStartMinutes.Value = 0;
            
            // إعادة تعيين حالة التحرير
            _isEditing = false;
            _editingId = 0;
            
            // تحديث حالة الأزرار
            UpdateButtonsState();
        }
        
        /// <summary>
        /// حدث الضغط على زر التحديث
        /// </summary>
        private void barButtonItemRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // إعادة تحميل البيانات
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
        /// حدث الضغط على زر الإضافة
        /// </summary>
        private void barButtonItemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // تنظيف النموذج وتهيئته للإضافة
                ClearForm();
                
                // تعيين حالة التحرير
                _isEditing = true;
                _editingId = 0;
                
                // تحديث حالة الأزرار
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
        /// حدث الضغط على زر التعديل
        /// </summary>
        private void barButtonItemEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // التحقق من اختيار صف
                if (gridView1.SelectedRowsCount == 0)
                {
                    XtraMessageBox.Show("يرجى اختيار فترة عمل للتعديل.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // الحصول على معرف السجل المختار
                int rowHandle = gridView1.GetSelectedRows()[0];
                DataRow row = gridView1.GetDataRow(rowHandle);
                _editingId = Convert.ToInt32(row["ID"]);
                
                // ملء النموذج ببيانات السجل المختار
                textEditName.Text = row["Name"].ToString();
                textEditDescription.Text = row["Description"] != DBNull.Value ? row["Description"].ToString() : string.Empty;
                timeEditStartTime.Time = (DateTime)row["StartTime"];
                timeEditEndTime.Time = (DateTime)row["EndTime"];
                spinEditFlexibleMinutes.Value = Convert.ToInt32(row["FlexibleMinutes"]);
                spinEditLateThresholdMinutes.Value = Convert.ToInt32(row["LateThresholdMinutes"]);
                spinEditShortDayThresholdMinutes.Value = Convert.ToInt32(row["ShortDayThresholdMinutes"]);
                spinEditOverTimeStartMinutes.Value = Convert.ToInt32(row["OverTimeStartMinutes"]);
                
                // تعيين حالة التحرير
                _isEditing = true;
                
                // تحديث حالة الأزرار
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تحميل بيانات فترة العمل للتعديل: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث الضغط على زر الحذف
        /// </summary>
        private void barButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // التحقق من اختيار صف
                if (gridView1.SelectedRowsCount == 0)
                {
                    XtraMessageBox.Show("يرجى اختيار فترة عمل للحذف.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // الحصول على معرف السجل المختار
                int rowHandle = gridView1.GetSelectedRows()[0];
                DataRow row = gridView1.GetDataRow(rowHandle);
                int id = Convert.ToInt32(row["ID"]);
                string name = row["Name"].ToString();
                
                // تأكيد الحذف
                DialogResult result = XtraMessageBox.Show($"هل أنت متأكد من حذف فترة العمل \"{name}\"؟", "تأكيد الحذف", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    
                    // حذف السجل
                    bool success = _workHoursRepository.DeleteWorkHours(id);
                    
                    this.Cursor = Cursors.Default;
                    
                    if (success)
                    {
                        XtraMessageBox.Show("تم حذف فترة العمل بنجاح.", "نجاح", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // إعادة تحميل البيانات
                        LoadData();
                    }
                    else
                    {
                        XtraMessageBox.Show("فشل حذف فترة العمل. قد تكون مستخدمة في سجلات أخرى.", "خطأ", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حذف فترة العمل: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث الضغط على زر الحفظ
        /// </summary>
        private void barButtonItemSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // التحقق من البيانات
                if (!ValidateInput())
                    return;
                
                this.Cursor = Cursors.WaitCursor;
                
                // إنشاء كائن بيانات فترة العمل
                WorkHoursModel workHours = new WorkHoursModel
                {
                    ID = _editingId,
                    Name = textEditName.Text,
                    Description = textEditDescription.Text,
                    StartTime = timeEditStartTime.Time.TimeOfDay,
                    EndTime = timeEditEndTime.Time.TimeOfDay,
                    FlexibleMinutes = (int)spinEditFlexibleMinutes.Value,
                    LateThresholdMinutes = (int)spinEditLateThresholdMinutes.Value,
                    ShortDayThresholdMinutes = (int)spinEditShortDayThresholdMinutes.Value,
                    OverTimeStartMinutes = (int)spinEditOverTimeStartMinutes.Value
                };
                
                // حفظ البيانات
                bool success = false;
                if (_editingId == 0)
                {
                    // إضافة سجل جديد
                    success = _workHoursRepository.AddWorkHours(workHours);
                }
                else
                {
                    // تعديل سجل موجود
                    success = _workHoursRepository.UpdateWorkHours(workHours);
                }
                
                this.Cursor = Cursors.Default;
                
                if (success)
                {
                    XtraMessageBox.Show(_editingId == 0 ? "تمت إضافة فترة العمل بنجاح." : "تم تعديل فترة العمل بنجاح.", "نجاح", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // إعادة تحميل البيانات
                    LoadData();
                }
                else
                {
                    XtraMessageBox.Show(_editingId == 0 ? "فشل إضافة فترة العمل." : "فشل تعديل فترة العمل.", "خطأ", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حفظ فترة العمل: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// حدث الضغط على زر الإلغاء
        /// </summary>
        private void barButtonItemCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // إلغاء التحرير
                ClearForm();
                
                // تحديث حالة الأزرار
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
        /// التحقق من البيانات المدخلة
        /// </summary>
        private bool ValidateInput()
        {
            // التحقق من اسم فترة العمل
            if (string.IsNullOrWhiteSpace(textEditName.Text))
            {
                XtraMessageBox.Show("يرجى إدخال اسم فترة العمل.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditName.Focus();
                return false;
            }
            
            // التحقق من وقت البدء
            if (timeEditStartTime.Time == DateTime.MinValue)
            {
                XtraMessageBox.Show("يرجى تحديد وقت البدء.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                timeEditStartTime.Focus();
                return false;
            }
            
            // التحقق من وقت الانتهاء
            if (timeEditEndTime.Time == DateTime.MinValue)
            {
                XtraMessageBox.Show("يرجى تحديد وقت الانتهاء.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                timeEditEndTime.Focus();
                return false;
            }
            
            // التحقق من أن وقت الانتهاء بعد وقت البدء
            if (timeEditEndTime.Time <= timeEditStartTime.Time)
            {
                XtraMessageBox.Show("يجب أن يكون وقت الانتهاء بعد وقت البدء.", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                timeEditEndTime.Focus();
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// حدث تغيير اختيار الصف في الجدول
        /// </summary>
        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            // تحديث حالة الأزرار
            UpdateButtonsState();
        }
    }
    
    /// <summary>
    /// كائن بيانات فترة العمل
    /// </summary>
    public class WorkHoursModel
    {
        /// <summary>
        /// معرف فترة العمل
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// اسم فترة العمل
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// وصف فترة العمل
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// وقت البدء
        /// </summary>
        public TimeSpan StartTime { get; set; }
        
        /// <summary>
        /// وقت الانتهاء
        /// </summary>
        public TimeSpan EndTime { get; set; }
        
        /// <summary>
        /// دقائق السماح للتأخير
        /// </summary>
        public int FlexibleMinutes { get; set; }
        
        /// <summary>
        /// الحد الأدنى لاحتساب التأخير
        /// </summary>
        public int LateThresholdMinutes { get; set; }
        
        /// <summary>
        /// الحد الأدنى لاحتساب المغادرة المبكرة
        /// </summary>
        public int ShortDayThresholdMinutes { get; set; }
        
        /// <summary>
        /// الحد الأدنى لاحتساب العمل الإضافي
        /// </summary>
        public int OverTimeStartMinutes { get; set; }
    }
}