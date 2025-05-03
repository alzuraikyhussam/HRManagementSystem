using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Settings
{
    public partial class WorkShiftsForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly WorkShiftRepository _workShiftRepository;
        private readonly WorkHoursRepository _workHoursRepository;
        private DataTable _dtWorkShifts;
        
        public WorkShiftsForm()
        {
            InitializeComponent();
            _workShiftRepository = new WorkShiftRepository();
            _workHoursRepository = new WorkHoursRepository();
        }
        
        private void WorkShiftsForm_Load(object sender, EventArgs e)
        {
            try
            {
                // جلب البيانات
                LoadData();
                
                // جلب قائمة فترات العمل للاختيار
                LoadWorkHoursList();
                
                // التحقق من الصلاحيات
                CheckPermissions();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تهيئة البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// جلب البيانات
        /// </summary>
        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;
            
            try
            {
                // جلب بيانات المناوبات
                _dtWorkShifts = _workShiftRepository.GetWorkShifts();
                
                // ربط البيانات بعنصر العرض
                gridControlWorkShifts.DataSource = _dtWorkShifts;
                
                // ضبط عرض الأعمدة
                SetupGridView();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء جلب البيانات: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// ضبط عرض الجدول
        /// </summary>
        private void SetupGridView()
        {
            // التأكد من وجود عرض الجدول
            if (gridViewWorkShifts == null)
                return;
            
            try
            {
                // إخفاء بعض الأعمدة
                gridViewWorkShifts.Columns["CreatedAt"]?.SetVisible(false);
                gridViewWorkShifts.Columns["CreatedBy"]?.SetVisible(false);
                gridViewWorkShifts.Columns["UpdatedAt"]?.SetVisible(false);
                gridViewWorkShifts.Columns["UpdatedBy"]?.SetVisible(false);
                
                // تعيين عناوين الأعمدة
                gridViewWorkShifts.Columns["ID"]?.Caption = "الرقم";
                gridViewWorkShifts.Columns["Name"]?.Caption = "اسم المناوبة";
                gridViewWorkShifts.Columns["Description"]?.Caption = "الوصف";
                gridViewWorkShifts.Columns["WorkHoursID"]?.Caption = "معرف فترة العمل";
                gridViewWorkShifts.Columns["WorkHoursName"]?.Caption = "فترة العمل";
                gridViewWorkShifts.Columns["StartTime"]?.Caption = "وقت البدء";
                gridViewWorkShifts.Columns["EndTime"]?.Caption = "وقت الانتهاء";
                gridViewWorkShifts.Columns["SundayEnabled"]?.Caption = "الأحد";
                gridViewWorkShifts.Columns["MondayEnabled"]?.Caption = "الإثنين";
                gridViewWorkShifts.Columns["TuesdayEnabled"]?.Caption = "الثلاثاء";
                gridViewWorkShifts.Columns["WednesdayEnabled"]?.Caption = "الأربعاء";
                gridViewWorkShifts.Columns["ThursdayEnabled"]?.Caption = "الخميس";
                gridViewWorkShifts.Columns["FridayEnabled"]?.Caption = "الجمعة";
                gridViewWorkShifts.Columns["SaturdayEnabled"]?.Caption = "السبت";
                gridViewWorkShifts.Columns["ColorCode"]?.Caption = "رمز اللون";
                gridViewWorkShifts.Columns["IsActive"]?.Caption = "نشط";
                
                // إخفاء العمود الخاص بمعرف فترة العمل
                gridViewWorkShifts.Columns["WorkHoursID"]?.SetVisible(false);
                
                // ضبط عرض الأعمدة
                gridViewWorkShifts.Columns["ID"]?.Width = 60;
                gridViewWorkShifts.Columns["Name"]?.Width = 120;
                gridViewWorkShifts.Columns["Description"]?.Width = 150;
                gridViewWorkShifts.Columns["WorkHoursName"]?.Width = 120;
                gridViewWorkShifts.Columns["StartTime"]?.Width = 80;
                gridViewWorkShifts.Columns["EndTime"]?.Width = 80;
                
                // ضبط تنسيق عرض الوقت
                gridViewWorkShifts.Columns["StartTime"]?.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridViewWorkShifts.Columns["StartTime"]?.DisplayFormat.FormatString = "hh:mm tt";
                gridViewWorkShifts.Columns["EndTime"]?.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridViewWorkShifts.Columns["EndTime"]?.DisplayFormat.FormatString = "hh:mm tt";
                
                // ضبط ترتيب الأعمدة
                gridViewWorkShifts.Columns["ID"]?.VisibleIndex = 0;
                gridViewWorkShifts.Columns["Name"]?.VisibleIndex = 1;
                gridViewWorkShifts.Columns["Description"]?.VisibleIndex = 2;
                gridViewWorkShifts.Columns["WorkHoursName"]?.VisibleIndex = 3;
                gridViewWorkShifts.Columns["StartTime"]?.VisibleIndex = 4;
                gridViewWorkShifts.Columns["EndTime"]?.VisibleIndex = 5;
                gridViewWorkShifts.Columns["SundayEnabled"]?.VisibleIndex = 6;
                gridViewWorkShifts.Columns["MondayEnabled"]?.VisibleIndex = 7;
                gridViewWorkShifts.Columns["TuesdayEnabled"]?.VisibleIndex = 8;
                gridViewWorkShifts.Columns["WednesdayEnabled"]?.VisibleIndex = 9;
                gridViewWorkShifts.Columns["ThursdayEnabled"]?.VisibleIndex = 10;
                gridViewWorkShifts.Columns["FridayEnabled"]?.VisibleIndex = 11;
                gridViewWorkShifts.Columns["SaturdayEnabled"]?.VisibleIndex = 12;
                gridViewWorkShifts.Columns["IsActive"]?.VisibleIndex = 13;
                
                // إضافة تذييل للجدول
                gridViewWorkShifts.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
                gridViewWorkShifts.OptionsView.ShowFooter = true;
                gridViewWorkShifts.Columns["ID"]?.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                gridViewWorkShifts.Columns["ID"]?.SummaryItem.DisplayFormat = "العدد: {0}";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء ضبط عرض الجدول");
            }
        }
        
        /// <summary>
        /// جلب قائمة فترات العمل للاختيار
        /// </summary>
        private void LoadWorkHoursList()
        {
            try
            {
                // جلب قائمة فترات العمل
                List<WorkHoursListItem> workHoursList = _workHoursRepository.GetWorkHoursDropDownList();
                
                // ربط البيانات بعنصر القائمة المنسدلة
                lookUpEditWorkHours.Properties.DataSource = workHoursList;
                lookUpEditWorkHours.Properties.DisplayMember = "Name";
                lookUpEditWorkHours.Properties.ValueMember = "ID";
                
                // ضبط عرض عناصر القائمة
                lookUpEditWorkHours.Properties.Columns.Clear();
                lookUpEditWorkHours.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "الرقم", 30));
                lookUpEditWorkHours.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "اسم الفترة", 150));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء جلب قائمة فترات العمل: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckPermissions()
        {
            try
            {
                // التحقق من صلاحيات المستخدم
                bool canAdd = SessionManager.HasPermission("Settings.AddWorkShift");
                bool canEdit = SessionManager.HasPermission("Settings.EditWorkShift");
                bool canDelete = SessionManager.HasPermission("Settings.DeleteWorkShift");
                
                // ضبط حالة الأزرار
                barButtonItemAdd.Enabled = canAdd;
                barButtonItemEdit.Enabled = canEdit;
                barButtonItemDelete.Enabled = canDelete;
                barButtonItemSave.Enabled = canAdd || canEdit;
                
                // ضبط حالة عناصر التحرير
                SetEditControlsEnabled(false);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء التحقق من الصلاحيات");
            }
        }
        
        /// <summary>
        /// تفعيل/تعطيل عناصر التحرير
        /// </summary>
        /// <param name="enabled">حالة التفعيل</param>
        private void SetEditControlsEnabled(bool enabled)
        {
            try
            {
                // تفعيل/تعطيل عناصر التحرير
                textEditName.Enabled = enabled;
                memoEditDescription.Enabled = enabled;
                lookUpEditWorkHours.Enabled = enabled;
                
                // تفعيل/تعطيل عناصر أيام الأسبوع
                checkEditSundayEnabled.Enabled = enabled;
                checkEditMondayEnabled.Enabled = enabled;
                checkEditTuesdayEnabled.Enabled = enabled;
                checkEditWednesdayEnabled.Enabled = enabled;
                checkEditThursdayEnabled.Enabled = enabled;
                checkEditFridayEnabled.Enabled = enabled;
                checkEditSaturdayEnabled.Enabled = enabled;
                
                // تفعيل/تعطيل عنصر اللون
                colorPickEditShiftColor.Enabled = enabled;
                
                // تفعيل/تعطيل عنصر الحالة
                checkEditIsActive.Enabled = enabled;
                
                // تفعيل/تعطيل زر الحفظ
                barButtonItemSave.Enabled = enabled && (SessionManager.HasPermission("Settings.AddWorkShift") || SessionManager.HasPermission("Settings.EditWorkShift"));
                
                // تفعيل/تعطيل زر الإلغاء
                barButtonItemCancel.Enabled = enabled;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء ضبط حالة عناصر التحرير");
            }
        }
        
        /// <summary>
        /// مسح بيانات النموذج
        /// </summary>
        private void ClearForm()
        {
            try
            {
                // مسح بيانات النموذج
                textEditID.Text = string.Empty;
                textEditName.Text = string.Empty;
                memoEditDescription.Text = string.Empty;
                lookUpEditWorkHours.EditValue = null;
                
                // إعادة ضبط عناصر أيام الأسبوع
                checkEditSundayEnabled.Checked = false;
                checkEditMondayEnabled.Checked = false;
                checkEditTuesdayEnabled.Checked = false;
                checkEditWednesdayEnabled.Checked = false;
                checkEditThursdayEnabled.Checked = false;
                checkEditFridayEnabled.Checked = false;
                checkEditSaturdayEnabled.Checked = false;
                
                // إعادة ضبط عنصر اللون
                colorPickEditShiftColor.Color = Color.White;
                
                // إعادة ضبط عنصر الحالة
                checkEditIsActive.Checked = true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء مسح بيانات النموذج");
            }
        }
        
        /// <summary>
        /// عرض بيانات المناوبة المحددة
        /// </summary>
        private void DisplaySelectedShift()
        {
            try
            {
                // التأكد من وجود صف محدد
                int focusedRowHandle = gridViewWorkShifts.FocusedRowHandle;
                if (focusedRowHandle < 0)
                    return;
                
                // الحصول على معرف المناوبة المحددة
                object idObj = gridViewWorkShifts.GetRowCellValue(focusedRowHandle, "ID");
                if (idObj == null)
                    return;
                
                int shiftId = Convert.ToInt32(idObj);
                
                // جلب بيانات المناوبة
                WorkShiftModel shift = _workShiftRepository.GetWorkShiftById(shiftId);
                if (shift == null)
                {
                    XtraMessageBox.Show("لم يتم العثور على بيانات المناوبة المحددة.", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // عرض بيانات المناوبة
                textEditID.Text = shift.ID.ToString();
                textEditName.Text = shift.Name;
                memoEditDescription.Text = shift.Description;
                lookUpEditWorkHours.EditValue = shift.WorkHoursID;
                
                // عرض بيانات أيام الأسبوع
                checkEditSundayEnabled.Checked = shift.SundayEnabled;
                checkEditMondayEnabled.Checked = shift.MondayEnabled;
                checkEditTuesdayEnabled.Checked = shift.TuesdayEnabled;
                checkEditWednesdayEnabled.Checked = shift.WednesdayEnabled;
                checkEditThursdayEnabled.Checked = shift.ThursdayEnabled;
                checkEditFridayEnabled.Checked = shift.FridayEnabled;
                checkEditSaturdayEnabled.Checked = shift.SaturdayEnabled;
                
                // عرض بيانات اللون
                if (!string.IsNullOrEmpty(shift.ColorCode))
                {
                    try
                    {
                        colorPickEditShiftColor.Color = ColorTranslator.FromHtml(shift.ColorCode);
                    }
                    catch
                    {
                        colorPickEditShiftColor.Color = Color.White;
                    }
                }
                else
                {
                    colorPickEditShiftColor.Color = Color.White;
                }
                
                // عرض حالة التفعيل
                checkEditIsActive.Checked = shift.IsActive;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء عرض بيانات المناوبة: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// التحقق من صحة البيانات المدخلة
        /// </summary>
        /// <returns>صحة البيانات</returns>
        private bool ValidateInput()
        {
            // التحقق من إدخال اسم المناوبة
            if (string.IsNullOrWhiteSpace(textEditName.Text))
            {
                XtraMessageBox.Show("يجب إدخال اسم المناوبة", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditName.Focus();
                return false;
            }
            
            // التحقق من اختيار فترة العمل
            if (lookUpEditWorkHours.EditValue == null)
            {
                XtraMessageBox.Show("يجب اختيار فترة العمل", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lookUpEditWorkHours.Focus();
                return false;
            }
            
            // التحقق من تفعيل يوم واحد على الأقل
            if (!checkEditSundayEnabled.Checked && !checkEditMondayEnabled.Checked && 
                !checkEditTuesdayEnabled.Checked && !checkEditWednesdayEnabled.Checked && 
                !checkEditThursdayEnabled.Checked && !checkEditFridayEnabled.Checked && 
                !checkEditSaturdayEnabled.Checked)
            {
                XtraMessageBox.Show("يجب تفعيل يوم واحد على الأقل", "تنبيه", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                checkEditSundayEnabled.Focus();
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// حفظ بيانات المناوبة
        /// </summary>
        private bool SaveShift()
        {
            try
            {
                // التحقق من صحة البيانات المدخلة
                if (!ValidateInput())
                    return false;
                
                // إنشاء كائن المناوبة
                WorkShiftModel shift = new WorkShiftModel
                {
                    Name = textEditName.Text.Trim(),
                    Description = memoEditDescription.Text.Trim(),
                    WorkHoursID = Convert.ToInt32(lookUpEditWorkHours.EditValue),
                    SundayEnabled = checkEditSundayEnabled.Checked,
                    MondayEnabled = checkEditMondayEnabled.Checked,
                    TuesdayEnabled = checkEditTuesdayEnabled.Checked,
                    WednesdayEnabled = checkEditWednesdayEnabled.Checked,
                    ThursdayEnabled = checkEditThursdayEnabled.Checked,
                    FridayEnabled = checkEditFridayEnabled.Checked,
                    SaturdayEnabled = checkEditSaturdayEnabled.Checked,
                    ColorCode = ColorTranslator.ToHtml(colorPickEditShiftColor.Color),
                    IsActive = checkEditIsActive.Checked
                };
                
                bool result;
                
                // التحقق من وجود معرف للمناوبة (تعديل/إضافة)
                if (!string.IsNullOrEmpty(textEditID.Text) && int.TryParse(textEditID.Text, out int shiftId))
                {
                    // تعديل مناوبة موجودة
                    shift.ID = shiftId;
                    result = _workShiftRepository.UpdateWorkShift(shift);
                    
                    if (result)
                    {
                        XtraMessageBox.Show("تم تعديل المناوبة بنجاح", "نجاح", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("فشل تعديل المناوبة", "خطأ", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // إضافة مناوبة جديدة
                    result = _workShiftRepository.AddWorkShift(shift);
                    
                    if (result)
                    {
                        XtraMessageBox.Show("تمت إضافة المناوبة بنجاح", "نجاح", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("فشلت إضافة المناوبة", "خطأ", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حفظ بيانات المناوبة: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
                return false;
            }
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
                
                // مسح بيانات النموذج
                ClearForm();
                
                // تعطيل عناصر التحرير
                SetEditControlsEnabled(false);
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
                // التحقق من الصلاحيات
                if (!SessionManager.HasPermission("Settings.AddWorkShift"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية إضافة مناوبة جديدة", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // مسح بيانات النموذج
                ClearForm();
                
                // تفعيل عناصر التحرير
                SetEditControlsEnabled(true);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تهيئة الإضافة: {ex.Message}", "خطأ", 
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
                // التحقق من الصلاحيات
                if (!SessionManager.HasPermission("Settings.EditWorkShift"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية تعديل المناوبات", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // التأكد من وجود صف محدد
                int focusedRowHandle = gridViewWorkShifts.FocusedRowHandle;
                if (focusedRowHandle < 0)
                {
                    XtraMessageBox.Show("يرجى تحديد مناوبة للتعديل", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // عرض بيانات المناوبة المحددة
                DisplaySelectedShift();
                
                // تفعيل عناصر التحرير
                SetEditControlsEnabled(true);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء تهيئة التعديل: {ex.Message}", "خطأ", 
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
                // التحقق من الصلاحيات
                if (!SessionManager.HasPermission("Settings.DeleteWorkShift"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية حذف المناوبات", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // التأكد من وجود صف محدد
                int focusedRowHandle = gridViewWorkShifts.FocusedRowHandle;
                if (focusedRowHandle < 0)
                {
                    XtraMessageBox.Show("يرجى تحديد مناوبة للحذف", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // الحصول على معرف المناوبة المحددة
                object idObj = gridViewWorkShifts.GetRowCellValue(focusedRowHandle, "ID");
                if (idObj == null)
                    return;
                
                int shiftId = Convert.ToInt32(idObj);
                
                // طلب تأكيد الحذف
                DialogResult dialogResult = XtraMessageBox.Show("هل أنت متأكد من حذف المناوبة المحددة؟", "تأكيد الحذف", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (dialogResult == DialogResult.Yes)
                {
                    // حذف المناوبة
                    bool result = _workShiftRepository.DeleteWorkShift(shiftId);
                    
                    if (result)
                    {
                        XtraMessageBox.Show("تم حذف المناوبة بنجاح", "نجاح", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // إعادة تحميل البيانات
                        LoadData();
                        
                        // مسح بيانات النموذج
                        ClearForm();
                    }
                    else
                    {
                        XtraMessageBox.Show("فشل حذف المناوبة. قد تكون مرتبطة بموظف أو بيانات أخرى.", "خطأ", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حذف المناوبة: {ex.Message}", "خطأ", 
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
                // التحقق من الصلاحيات
                bool canAdd = SessionManager.HasPermission("Settings.AddWorkShift");
                bool canEdit = SessionManager.HasPermission("Settings.EditWorkShift");
                
                if (!canAdd && !canEdit)
                {
                    XtraMessageBox.Show("ليس لديك صلاحية حفظ بيانات المناوبات", "تنبيه", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // حفظ بيانات المناوبة
                bool result = SaveShift();
                
                if (result)
                {
                    // إعادة تحميل البيانات
                    LoadData();
                    
                    // تعطيل عناصر التحرير
                    SetEditControlsEnabled(false);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"حدث خطأ أثناء حفظ بيانات المناوبة: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.LogException(ex);
            }
        }
        
        /// <summary>
        /// حدث الضغط على زر الإلغاء
        /// </summary>
        private void barButtonItemCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // مسح بيانات النموذج
                ClearForm();
                
                // تعطيل عناصر التحرير
                SetEditControlsEnabled(false);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء إلغاء العملية");
            }
        }
        
        /// <summary>
        /// حدث الضغط على عنصر من الجدول
        /// </summary>
        private void gridViewWorkShifts_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                // عرض بيانات المناوبة المحددة
                DisplaySelectedShift();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء عرض بيانات المناوبة المحددة");
            }
        }
        
        /// <summary>
        /// حدث الضغط المزدوج على عنصر من الجدول
        /// </summary>
        private void gridViewWorkShifts_DoubleClick(object sender, EventArgs e)
        {
            // تنفيذ نفس عملية زر التعديل
            barButtonItemEdit_ItemClick(sender, null);
        }
        
        /// <summary>
        /// حدث تغيير فترة العمل المحددة
        /// </summary>
        private void lookUpEditWorkHours_EditValueChanged(object sender, EventArgs e)
        {
            // يمكن إضافة منطق إضافي هنا إذا لزم الأمر
        }
        
        /// <summary>
        /// حدث تفعيل جميع أيام الأسبوع
        /// </summary>
        private void checkEditAllDays_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // تفعيل/تعطيل جميع أيام الأسبوع
                bool state = checkEditAllDays.Checked;
                
                checkEditSundayEnabled.Checked = state;
                checkEditMondayEnabled.Checked = state;
                checkEditTuesdayEnabled.Checked = state;
                checkEditWednesdayEnabled.Checked = state;
                checkEditThursdayEnabled.Checked = state;
                checkEditFridayEnabled.Checked = state;
                checkEditSaturdayEnabled.Checked = state;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء تفعيل/تعطيل جميع أيام الأسبوع");
            }
        }
    }
}