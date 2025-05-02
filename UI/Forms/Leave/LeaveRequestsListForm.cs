using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;

namespace HR.UI.Forms.Leave
{
    /// <summary>
    /// نموذج عرض طلبات الإجازات
    /// </summary>
    public partial class LeaveRequestsListForm : XtraForm
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly int _employeeId; // إذا كان 0 فسيتم عرض جميع الطلبات

        /// <summary>
        /// تهيئة نموذج عرض طلبات الإجازات (كل الطلبات)
        /// </summary>
        public LeaveRequestsListForm()
        {
            InitializeComponent();
            
            // تهيئة وحدة العمل
            _unitOfWork = new UnitOfWork();
            _employeeId = 0;
            
            // تسجيل الأحداث
            this.Load += LeaveRequestsListForm_Load;
            buttonAdd.Click += ButtonAdd_Click;
            buttonView.Click += ButtonView_Click;
            buttonApprove.Click += ButtonApprove_Click;
            buttonReject.Click += ButtonReject_Click;
            buttonRefresh.Click += ButtonRefresh_Click;
            gridViewLeaveRequests.DoubleClick += GridViewLeaveRequests_DoubleClick;
            gridViewLeaveRequests.SelectionChanged += GridViewLeaveRequests_SelectionChanged;
            dateEditFrom.EditValueChanged += DateFilter_Changed;
            dateEditTo.EditValueChanged += DateFilter_Changed;
            radioGroupLeaveStatus.SelectedIndexChanged += RadioGroupLeaveStatus_SelectedIndexChanged;
        }

        /// <summary>
        /// تهيئة نموذج عرض طلبات الإجازات لموظف معين
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        public LeaveRequestsListForm(int employeeId)
        {
            InitializeComponent();
            
            // تهيئة وحدة العمل
            _unitOfWork = new UnitOfWork();
            _employeeId = employeeId;
            
            // تسجيل الأحداث
            this.Load += LeaveRequestsListForm_Load;
            buttonAdd.Click += ButtonAdd_Click;
            buttonView.Click += ButtonView_Click;
            buttonApprove.Click += ButtonApprove_Click;
            buttonReject.Click += ButtonReject_Click;
            buttonRefresh.Click += ButtonRefresh_Click;
            gridViewLeaveRequests.DoubleClick += GridViewLeaveRequests_DoubleClick;
            gridViewLeaveRequests.SelectionChanged += GridViewLeaveRequests_SelectionChanged;
            dateEditFrom.EditValueChanged += DateFilter_Changed;
            dateEditTo.EditValueChanged += DateFilter_Changed;
            radioGroupLeaveStatus.SelectedIndexChanged += RadioGroupLeaveStatus_SelectedIndexChanged;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void LeaveRequestsListForm_Load(object sender, EventArgs e)
        {
            try
            {
                // تعديل العنوان بناءً على طريقة العرض
                if (_employeeId > 0)
                {
                    var employee = _unitOfWork.EmployeeRepository.GetById(_employeeId);
                    if (employee != null)
                    {
                        this.Text = $"طلبات إجازات الموظف: {employee.FirstName} {employee.LastName}";
                        labelTitle.Text = $"طلبات إجازات {employee.FirstName} {employee.LastName}";
                    }
                    
                    // إخفاء عناصر الموافقة والرفض للموظف العادي
                    if (!SessionManager.IsCurrentUserInRole("Admin") && !SessionManager.IsCurrentUserInRole("HR"))
                    {
                        buttonApprove.Visible = false;
                        buttonReject.Visible = false;
                    }
                }
                
                // إعداد عناصر التحكم
                InitializeControls();
                
                // تحميل البيانات
                LoadLeaveRequests();
                
                // إعداد عرض الشبكة
                SetupGridView();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تهيئة نموذج عرض طلبات الإجازات");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تهيئة النموذج: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// إعداد عناصر التحكم
        /// </summary>
        private void InitializeControls()
        {
            // إعداد فلتر التاريخ
            DateTime firstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime lastDayOfYear = new DateTime(DateTime.Now.Year, 12, 31);
            
            dateEditFrom.DateTime = firstDayOfYear;
            dateEditTo.DateTime = lastDayOfYear;
            
            // إعداد فلتر الحالة
            radioGroupLeaveStatus.SelectedIndex = 0; // الكل
        }

        /// <summary>
        /// إعداد عرض الشبكة
        /// </summary>
        private void SetupGridView()
        {
            // إعداد خصائص العرض
            gridViewLeaveRequests.OptionsBehavior.Editable = false;
            gridViewLeaveRequests.OptionsView.ShowGroupPanel = false;
            gridViewLeaveRequests.OptionsView.EnableAppearanceEvenRow = true;
            gridViewLeaveRequests.OptionsView.EnableAppearanceOddRow = true;
            
            // إعداد الأعمدة
            gridViewLeaveRequests.Columns["ID"].Caption = "الرقم";
            gridViewLeaveRequests.Columns["ID"].Width = 50;
            gridViewLeaveRequests.Columns["ID"].VisibleIndex = 0;
            
            if (_employeeId == 0)
            {
                gridViewLeaveRequests.Columns["EmployeeName"].Caption = "الموظف";
                gridViewLeaveRequests.Columns["EmployeeName"].Width = 120;
                gridViewLeaveRequests.Columns["EmployeeName"].VisibleIndex = 1;
            }
            else
            {
                // إخفاء عمود الموظف في حالة عرض طلبات موظف واحد
                if (gridViewLeaveRequests.Columns["EmployeeName"] != null)
                {
                    gridViewLeaveRequests.Columns["EmployeeName"].Visible = false;
                }
            }
            
            gridViewLeaveRequests.Columns["LeaveType"].Caption = "نوع الإجازة";
            gridViewLeaveRequests.Columns["LeaveType"].Width = 100;
            gridViewLeaveRequests.Columns["LeaveType"].VisibleIndex = 2;
            
            gridViewLeaveRequests.Columns["StartDate"].Caption = "تاريخ البداية";
            gridViewLeaveRequests.Columns["StartDate"].Width = 90;
            gridViewLeaveRequests.Columns["StartDate"].VisibleIndex = 3;
            
            gridViewLeaveRequests.Columns["EndDate"].Caption = "تاريخ النهاية";
            gridViewLeaveRequests.Columns["EndDate"].Width = 90;
            gridViewLeaveRequests.Columns["EndDate"].VisibleIndex = 4;
            
            gridViewLeaveRequests.Columns["Days"].Caption = "عدد الأيام";
            gridViewLeaveRequests.Columns["Days"].Width = 70;
            gridViewLeaveRequests.Columns["Days"].VisibleIndex = 5;
            
            gridViewLeaveRequests.Columns["SubmissionDate"].Caption = "تاريخ الطلب";
            gridViewLeaveRequests.Columns["SubmissionDate"].Width = 90;
            gridViewLeaveRequests.Columns["SubmissionDate"].VisibleIndex = 6;
            
            gridViewLeaveRequests.Columns["Status"].Caption = "الحالة";
            gridViewLeaveRequests.Columns["Status"].Width = 80;
            gridViewLeaveRequests.Columns["Status"].VisibleIndex = 7;
            
            // تنسيق خلايا الحالة بألوان مختلفة
            gridViewLeaveRequests.RowStyle += GridViewLeaveRequests_RowStyle;
            
            // تطبيق أفضل عرض للأعمدة
            gridViewLeaveRequests.BestFitColumns();
        }

        /// <summary>
        /// تنسيق صفوف الشبكة بناءً على حالة الطلب
        /// </summary>
        private void GridViewLeaveRequests_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
                
            GridView view = sender as GridView;
            string status = view.GetRowCellValue(e.RowHandle, "Status")?.ToString();
            
            if (status == "Approved" || status == "تمت الموافقة")
            {
                e.Appearance.BackColor = Color.FromArgb(200, 255, 200); // أخضر فاتح
            }
            else if (status == "Rejected" || status == "مرفوض")
            {
                e.Appearance.BackColor = Color.FromArgb(255, 200, 200); // أحمر فاتح
            }
            else if (status == "Pending" || status == "قيد الانتظار")
            {
                e.Appearance.BackColor = Color.FromArgb(255, 255, 200); // أصفر فاتح
            }
            else if (status == "Cancelled" || status == "ملغي")
            {
                e.Appearance.BackColor = Color.FromArgb(225, 225, 225); // رمادي فاتح
            }
        }

        /// <summary>
        /// حدث تغيير التحديد في الشبكة
        /// </summary>
        private void GridViewLeaveRequests_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            UpdateButtonsState();
        }

        /// <summary>
        /// تحديث حالة الأزرار
        /// </summary>
        private void UpdateButtonsState()
        {
            bool hasSelectedRows = gridViewLeaveRequests.SelectedRowsCount > 0;
            buttonView.Enabled = hasSelectedRows;
            
            // تحديد ما إذا كان يمكن الموافقة أو الرفض
            bool canApproveReject = false;
            
            if (hasSelectedRows && (SessionManager.IsCurrentUserInRole("Admin") || SessionManager.IsCurrentUserInRole("HR")))
            {
                string status = gridViewLeaveRequests.GetFocusedRowCellValue("Status")?.ToString();
                canApproveReject = (status == "Pending" || status == "قيد الانتظار");
            }
            
            buttonApprove.Enabled = canApproveReject;
            buttonReject.Enabled = canApproveReject;
        }

        /// <summary>
        /// تحميل طلبات الإجازات
        /// </summary>
        private void LoadLeaveRequests()
        {
            try
            {
                // عرض مؤشر الانتظار
                this.Cursor = Cursors.WaitCursor;
                
                // تحديد حالة الفلتر
                string statusFilter = null;
                
                switch (radioGroupLeaveStatus.SelectedIndex)
                {
                    case 1: // قيد الانتظار
                        statusFilter = "Pending";
                        break;
                    case 2: // تمت الموافقة
                        statusFilter = "Approved";
                        break;
                    case 3: // مرفوض
                        statusFilter = "Rejected";
                        break;
                    case 4: // ملغي
                        statusFilter = "Cancelled";
                        break;
                    case 0: // الكل
                    default:
                        statusFilter = null;
                        break;
                }
                
                // الحصول على بيانات طلبات الإجازات
                var leaveRequests = _unitOfWork.LeaveRepository.GetLeaveRequests(
                    _employeeId,
                    dateEditFrom.DateTime,
                    dateEditTo.DateTime,
                    statusFilter);
                
                // تعيين مصدر البيانات
                gridControlLeaveRequests.DataSource = leaveRequests;
                
                // تحديث حالة الأزرار
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل طلبات الإجازات");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تحميل طلبات الإجازات: {ex.Message}",
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
        /// حدث تغيير فلتر التاريخ
        /// </summary>
        private void DateFilter_Changed(object sender, EventArgs e)
        {
            try
            {
                // التحقق من صحة نطاق التاريخ
                if (dateEditFrom.DateTime > dateEditTo.DateTime)
                {
                    XtraMessageBox.Show(
                        "تاريخ البداية يجب أن يكون قبل تاريخ النهاية",
                        "خطأ في النطاق",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    
                    if (sender == dateEditFrom)
                    {
                        dateEditFrom.DateTime = dateEditTo.DateTime.AddMonths(-1);
                    }
                    else
                    {
                        dateEditTo.DateTime = dateEditFrom.DateTime.AddMonths(1);
                    }
                }
                
                // إعادة تحميل البيانات
                LoadLeaveRequests();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تطبيق فلتر التاريخ");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تطبيق فلتر التاريخ: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث تغيير فلتر الحالة
        /// </summary>
        private void RadioGroupLeaveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLeaveRequests();
        }

        /// <summary>
        /// حدث نقر زر الإضافة
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // إنشاء نموذج طلب إجازة جديد
                var leaveRequestForm = new LeaveRequestForm(_employeeId);
                
                // عرض النموذج
                if (leaveRequestForm.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل البيانات بعد الإضافة
                    LoadLeaveRequests();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في فتح نموذج إضافة طلب إجازة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج إضافة طلب إجازة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث نقر زر عرض
        /// </summary>
        private void ButtonView_Click(object sender, EventArgs e)
        {
            ViewSelectedLeaveRequest();
        }

        /// <summary>
        /// حدث النقر المزدوج على الشبكة
        /// </summary>
        private void GridViewLeaveRequests_DoubleClick(object sender, EventArgs e)
        {
            ViewSelectedLeaveRequest();
        }

        /// <summary>
        /// عرض طلب الإجازة المحدد
        /// </summary>
        private void ViewSelectedLeaveRequest()
        {
            if (gridViewLeaveRequests.SelectedRowsCount == 0)
                return;
                
            try
            {
                // الحصول على معرف طلب الإجازة المحدد
                int leaveRequestId = Convert.ToInt32(gridViewLeaveRequests.GetFocusedRowCellValue("ID"));
                
                // إنشاء نموذج عرض طلب الإجازة
                var leaveRequestForm = new LeaveRequestForm(leaveRequestId, true);
                
                // عرض النموذج
                leaveRequestForm.ShowDialog();
                
                // لا نحتاج إلى إعادة تحميل البيانات لأنه مجرد عرض
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في فتح نموذج عرض طلب إجازة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء فتح نموذج عرض طلب إجازة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث نقر زر الموافقة
        /// </summary>
        private void ButtonApprove_Click(object sender, EventArgs e)
        {
            if (gridViewLeaveRequests.SelectedRowsCount == 0)
                return;
                
            try
            {
                // الحصول على معرف طلب الإجازة المحدد
                int leaveRequestId = Convert.ToInt32(gridViewLeaveRequests.GetFocusedRowCellValue("ID"));
                string employeeName = gridViewLeaveRequests.GetFocusedRowCellValue("EmployeeName")?.ToString();
                
                // التأكيد على الموافقة
                DialogResult result = XtraMessageBox.Show(
                    $"هل أنت متأكد من الموافقة على طلب الإجازة للموظف '{employeeName}'؟",
                    "تأكيد الموافقة",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    
                if (result != DialogResult.Yes)
                    return;
                
                // الموافقة على طلب الإجازة
                bool success = _unitOfWork.LeaveRepository.ApproveLeaveRequest(
                    leaveRequestId, 
                    SessionManager.CurrentUser.ID,
                    DateTime.Now,
                    "تمت الموافقة على الإجازة");
                
                if (success)
                {
                    // إعادة تحميل البيانات بعد الموافقة
                    LoadLeaveRequests();
                    
                    XtraMessageBox.Show(
                        "تمت الموافقة على طلب الإجازة بنجاح.",
                        "نجاح",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(
                        "فشل في الموافقة على طلب الإجازة. يرجى التحقق من رصيد الإجازات المتاح.",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في الموافقة على طلب إجازة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء الموافقة على طلب الإجازة: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث نقر زر الرفض
        /// </summary>
        private void ButtonReject_Click(object sender, EventArgs e)
        {
            if (gridViewLeaveRequests.SelectedRowsCount == 0)
                return;
                
            try
            {
                // الحصول على معرف طلب الإجازة المحدد
                int leaveRequestId = Convert.ToInt32(gridViewLeaveRequests.GetFocusedRowCellValue("ID"));
                string employeeName = gridViewLeaveRequests.GetFocusedRowCellValue("EmployeeName")?.ToString();
                
                // طلب سبب الرفض
                string reason = "";
                
                XtraInputBoxArgs args = new XtraInputBoxArgs();
                args.Caption = "سبب الرفض";
                args.Prompt = "يرجى إدخال سبب رفض طلب الإجازة:";
                args.DefaultButtonIndex = 0;
                
                MemoEdit editor = new MemoEdit();
                args.Editor = editor;
                
                var result = XtraInputBox.Show(args);
                
                if (result == null || string.IsNullOrWhiteSpace(result.ToString()))
                {
                    XtraMessageBox.Show(
                        "يجب إدخال سبب الرفض.",
                        "تنبيه",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                
                reason = result.ToString();
                
                // رفض طلب الإجازة
                bool success = _unitOfWork.LeaveRepository.RejectLeaveRequest(
                    leaveRequestId, 
                    SessionManager.CurrentUser.ID,
                    DateTime.Now,
                    reason);
                
                if (success)
                {
                    // إعادة تحميل البيانات بعد الرفض
                    LoadLeaveRequests();
                    
                    XtraMessageBox.Show(
                        "تم رفض طلب الإجازة بنجاح.",
                        "نجاح",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(
                        "فشل في رفض طلب الإجازة.",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في رفض طلب إجازة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء رفض طلب الإجازة: {ex.Message}",
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
            LoadLeaveRequests();
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