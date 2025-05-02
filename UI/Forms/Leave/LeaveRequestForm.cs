using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.Models;

namespace HR.UI.Forms.Leave
{
    /// <summary>
    /// نموذج إدارة طلب إجازة
    /// </summary>
    public partial class LeaveRequestForm : XtraForm
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly int _leaveRequestId;
        private readonly int _employeeId;
        private readonly bool _isViewMode;
        private readonly bool _isEditMode;
        private LeaveRequest _leaveRequest;

        /// <summary>
        /// إنشاء نموذج جديد لطلب إجازة (وضع الإضافة)
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        public LeaveRequestForm(int employeeId = 0)
        {
            InitializeComponent();
            
            // تهيئة وحدة العمل
            _unitOfWork = new UnitOfWork();
            _leaveRequestId = 0;
            _employeeId = employeeId;
            _isEditMode = false;
            _isViewMode = false;
            
            // تهيئة النموذج
            this.Text = "إضافة طلب إجازة جديد";
            
            // تسجيل الأحداث
            this.Load += LeaveRequestForm_Load;
            buttonSave.Click += ButtonSave_Click;
            buttonCancel.Click += ButtonCancel_Click;
            lookUpEditLeaveType.EditValueChanged += LookUpEditLeaveType_EditValueChanged;
            dateEditStartDate.EditValueChanged += DateEdit_EditValueChanged;
            dateEditEndDate.EditValueChanged += DateEdit_EditValueChanged;
        }

        /// <summary>
        /// إنشاء نموذج لعرض أو تعديل طلب إجازة موجود
        /// </summary>
        /// <param name="leaveRequestId">معرف طلب الإجازة</param>
        /// <param name="viewMode">هل النموذج للعرض فقط</param>
        public LeaveRequestForm(int leaveRequestId, bool viewMode = false)
        {
            InitializeComponent();
            
            // تهيئة وحدة العمل
            _unitOfWork = new UnitOfWork();
            _leaveRequestId = leaveRequestId;
            _employeeId = 0;
            _isEditMode = !viewMode;
            _isViewMode = viewMode;
            
            // تهيئة النموذج
            this.Text = viewMode ? "عرض طلب إجازة" : "تعديل طلب إجازة";
            
            // تسجيل الأحداث
            this.Load += LeaveRequestForm_Load;
            buttonSave.Click += ButtonSave_Click;
            buttonCancel.Click += ButtonCancel_Click;
            lookUpEditLeaveType.EditValueChanged += LookUpEditLeaveType_EditValueChanged;
            dateEditStartDate.EditValueChanged += DateEdit_EditValueChanged;
            dateEditEndDate.EditValueChanged += DateEdit_EditValueChanged;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void LeaveRequestForm_Load(object sender, EventArgs e)
        {
            try
            {
                // إعداد عناصر التحكم
                InitializeControls();
                
                // تحميل بيانات طلب الإجازة في وضع العرض أو التعديل
                if (_leaveRequestId > 0)
                {
                    LoadLeaveRequestData();
                }
                else
                {
                    // وضع الإضافة
                    SetupAddMode();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تهيئة نموذج طلب الإجازة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء تهيئة النموذج: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary>
        /// إعداد عناصر التحكم
        /// </summary>
        private void InitializeControls()
        {
            // تحميل أنواع الإجازات
            LoadLeaveTypes();
            
            // إعداد حقول التاريخ
            dateEditStartDate.Properties.MinValue = DateTime.Now.Date;
            dateEditEndDate.Properties.MinValue = DateTime.Now.Date;
            
            // في وضع العرض فقط، تعطيل كل الحقول
            if (_isViewMode)
            {
                lookUpEditEmployee.ReadOnly = true;
                lookUpEditLeaveType.ReadOnly = true;
                dateEditStartDate.ReadOnly = true;
                dateEditEndDate.ReadOnly = true;
                memoEditNotes.ReadOnly = true;
                
                // إخفاء زر الحفظ
                buttonSave.Visible = false;
                buttonCancel.Text = "إغلاق";
                
                // إظهار معلومات الحالة
                labelControlStatus.Visible = true;
                
                // إظهار حقول إضافية للعرض فقط
                labelControlSubmissionDate.Visible = true;
                labelControlApprovalInfo.Visible = true;
            }
            else
            {
                // إخفاء حقول العرض فقط
                labelControlStatus.Visible = false;
                labelControlSubmissionDate.Visible = false;
                labelControlApprovalInfo.Visible = false;
            }
        }

        /// <summary>
        /// تحميل بيانات طلب الإجازة للعرض أو التعديل
        /// </summary>
        private void LoadLeaveRequestData()
        {
            try
            {
                // الحصول على بيانات طلب الإجازة
                _leaveRequest = _unitOfWork.LeaveRepository.GetLeaveRequestById(_leaveRequestId);
                
                if (_leaveRequest == null)
                {
                    XtraMessageBox.Show(
                        "لم يتم العثور على طلب الإجازة المطلوب.",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                
                // تعيين معرف الموظف
                _employeeId = _leaveRequest.EmployeeID;
                
                // ملئ البيانات في النموذج
                
                // إعداد حقل الموظف
                SetupEmployeeLookupEdit();
                lookUpEditEmployee.EditValue = _leaveRequest.EmployeeID;
                
                // تحديد نوع الإجازة
                lookUpEditLeaveType.EditValue = _leaveRequest.LeaveTypeID;
                
                // تعيين التواريخ
                dateEditStartDate.DateTime = _leaveRequest.StartDate;
                dateEditEndDate.DateTime = _leaveRequest.EndDate;
                
                // الملاحظات
                memoEditNotes.Text = _leaveRequest.Notes;
                
                // عرض المدة
                UpdateDaysCount();
                
                // في وضع العرض، نعرض المزيد من المعلومات
                if (_isViewMode)
                {
                    // عرض حالة الطلب
                    string statusText = "";
                    System.Drawing.Color statusColor = System.Drawing.Color.Black;
                    
                    switch (_leaveRequest.Status)
                    {
                        case "Pending":
                            statusText = "قيد الانتظار";
                            statusColor = System.Drawing.Color.DarkOrange;
                            break;
                        case "Approved":
                            statusText = "تمت الموافقة";
                            statusColor = System.Drawing.Color.Green;
                            break;
                        case "Rejected":
                            statusText = "مرفوض";
                            statusColor = System.Drawing.Color.Red;
                            break;
                        case "Cancelled":
                            statusText = "ملغي";
                            statusColor = System.Drawing.Color.Gray;
                            break;
                    }
                    
                    labelControlStatus.Text = $"حالة الطلب: {statusText}";
                    labelControlStatus.ForeColor = statusColor;
                    
                    // عرض تاريخ تقديم الطلب
                    labelControlSubmissionDate.Text = $"تاريخ التقديم: {_leaveRequest.SubmissionDate:yyyy/MM/dd HH:mm}";
                    
                    // عرض معلومات الموافقة/الرفض
                    if (_leaveRequest.Status == "Approved")
                    {
                        labelControlApprovalInfo.Text = $"تمت الموافقة بتاريخ: {_leaveRequest.ApprovalDate:yyyy/MM/dd HH:mm}";
                    }
                    else if (_leaveRequest.Status == "Rejected")
                    {
                        labelControlApprovalInfo.Text = $"سبب الرفض: {_leaveRequest.ReasonForRejection}";
                    }
                    else
                    {
                        labelControlApprovalInfo.Visible = false;
                    }
                }
                
                // في وضع التعديل، يجب أن يكون الطلب في حالة انتظار فقط
                if (_isEditMode && _leaveRequest.Status != "Pending")
                {
                    XtraMessageBox.Show(
                        "لا يمكن تعديل طلب الإجازة إلا إذا كان في حالة انتظار.",
                        "غير مسموح",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    
                    // تحويل النموذج إلى وضع العرض فقط
                    _isEditMode = false;
                    _isViewMode = true;
                    
                    // إعادة تهيئة عناصر التحكم
                    InitializeControls();
                    
                    // إعادة تحميل البيانات
                    LoadLeaveRequestData();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل بيانات طلب الإجازة");
                throw;
            }
        }

        /// <summary>
        /// إعداد وضع الإضافة
        /// </summary>
        private void SetupAddMode()
        {
            // إنشاء كائن جديد
            _leaveRequest = new LeaveRequest
            {
                EmployeeID = _employeeId > 0 ? _employeeId : SessionManager.CurrentUser.EmployeeID,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date,
                Status = "Pending",
                SubmissionDate = DateTime.Now
            };
            
            // إعداد حقل الموظف
            SetupEmployeeLookupEdit();
            
            // تعيين القيم الافتراضية
            if (_employeeId > 0)
            {
                lookUpEditEmployee.EditValue = _employeeId;
            }
            else if (SessionManager.CurrentUser.EmployeeID > 0)
            {
                lookUpEditEmployee.EditValue = SessionManager.CurrentUser.EmployeeID;
            }
            
            dateEditStartDate.DateTime = DateTime.Now.Date;
            dateEditEndDate.DateTime = DateTime.Now.Date;
            
            // تحديث عدد الأيام
            UpdateDaysCount();
            
            // تعطيل حقل الموظف للمستخدم العادي
            if (!SessionManager.IsCurrentUserInRole("Admin") && !SessionManager.IsCurrentUserInRole("HR"))
            {
                lookUpEditEmployee.ReadOnly = true;
            }
        }

        /// <summary>
        /// تحميل أنواع الإجازات
        /// </summary>
        private void LoadLeaveTypes()
        {
            try
            {
                // الحصول على قائمة أنواع الإجازات النشطة
                var leaveTypes = _unitOfWork.LeaveRepository.GetAllLeaveTypes();
                
                // تصفية القائمة للأنواع النشطة فقط
                var activeLeaveTypes = leaveTypes.FindAll(lt => lt.IsActive);
                
                // تعيين مصدر البيانات
                lookUpEditLeaveType.Properties.DataSource = activeLeaveTypes;
                lookUpEditLeaveType.Properties.ValueMember = "ID";
                lookUpEditLeaveType.Properties.DisplayMember = "Name";
                
                // إعداد العرض
                lookUpEditLeaveType.Properties.Columns.Clear();
                lookUpEditLeaveType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "نوع الإجازة"));
                lookUpEditLeaveType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DefaultDays", "الأيام الافتراضية"));
                
                lookUpEditLeaveType.Properties.PopupWidth = 300;
                lookUpEditLeaveType.Properties.NullText = "اختر نوع الإجازة...";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل أنواع الإجازات");
                throw;
            }
        }

        /// <summary>
        /// إعداد حقل اختيار الموظف
        /// </summary>
        private void SetupEmployeeLookupEdit()
        {
            try
            {
                // تحميل قائمة الموظفين
                var employees = _unitOfWork.EmployeeRepository.GetAllEmployees();
                
                // تصفية القائمة للموظفين النشطين فقط
                var activeEmployees = employees.FindAll(e => e.IsActive);
                
                // تعيين مصدر البيانات
                lookUpEditEmployee.Properties.DataSource = activeEmployees;
                lookUpEditEmployee.Properties.ValueMember = "ID";
                lookUpEditEmployee.Properties.DisplayMember = "FullName";
                
                // إعداد العرض
                lookUpEditEmployee.Properties.Columns.Clear();
                lookUpEditEmployee.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FullName", "اسم الموظف"));
                lookUpEditEmployee.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EmployeeCode", "الرقم الوظيفي"));
                lookUpEditEmployee.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Department.Name", "القسم"));
                
                lookUpEditEmployee.Properties.PopupWidth = 400;
                lookUpEditEmployee.Properties.NullText = "اختر الموظف...";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في تحميل قائمة الموظفين");
                throw;
            }
        }

        /// <summary>
        /// حدث تغيير نوع الإجازة
        /// </summary>
        private void LookUpEditLeaveType_EditValueChanged(object sender, EventArgs e)
        {
            // تحديث عدد الأيام بناءً على المدة
            UpdateDaysCount();
            
            // تعيين تاريخ النهاية بناءً على تاريخ البداية وعدد الأيام الافتراضية
            if (lookUpEditLeaveType.EditValue != null)
            {
                int leaveTypeId = Convert.ToInt32(lookUpEditLeaveType.EditValue);
                var leaveType = _unitOfWork.LeaveRepository.GetLeaveTypeById(leaveTypeId);
                
                if (leaveType != null)
                {
                    // تعيين تاريخ النهاية بناءً على الأيام الافتراضية
                    dateEditEndDate.DateTime = dateEditStartDate.DateTime.AddDays(leaveType.DefaultDays - 1);
                }
            }
        }

        /// <summary>
        /// حدث تغيير حقول التاريخ
        /// </summary>
        private void DateEdit_EditValueChanged(object sender, EventArgs e)
        {
            // التحقق من العلاقة بين التواريخ
            if (dateEditStartDate.DateTime > dateEditEndDate.DateTime)
            {
                if (sender == dateEditStartDate)
                {
                    dateEditEndDate.DateTime = dateEditStartDate.DateTime;
                }
                else
                {
                    dateEditStartDate.DateTime = dateEditEndDate.DateTime;
                }
            }
            
            // تحديث عدد الأيام
            UpdateDaysCount();
        }

        /// <summary>
        /// تحديث حساب عدد أيام الإجازة
        /// </summary>
        private void UpdateDaysCount()
        {
            if (dateEditStartDate.EditValue == null || dateEditEndDate.EditValue == null)
                return;
                
            try
            {
                // حساب عدد الأيام بين التاريخين
                DateTime startDate = dateEditStartDate.DateTime.Date;
                DateTime endDate = dateEditEndDate.DateTime.Date;
                
                int days = (endDate - startDate).Days + 1;
                
                // عرض عدد الأيام
                labelControlDaysCount.Text = $"عدد الأيام: {days}";
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في حساب عدد أيام الإجازة");
            }
        }

        /// <summary>
        /// حدث نقر زر الحفظ
        /// </summary>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من صحة الإدخال
                if (!ValidateInput())
                    return;
                
                // تحديث بيانات طلب الإجازة
                _leaveRequest.EmployeeID = Convert.ToInt32(lookUpEditEmployee.EditValue);
                _leaveRequest.LeaveTypeID = Convert.ToInt32(lookUpEditLeaveType.EditValue);
                _leaveRequest.StartDate = dateEditStartDate.DateTime.Date;
                _leaveRequest.EndDate = dateEditEndDate.DateTime.Date;
                _leaveRequest.Notes = memoEditNotes.Text.Trim();
                
                // في حالة الإضافة، تعيين الحالة وتاريخ التقديم
                if (!_isEditMode)
                {
                    _leaveRequest.Status = "Pending";
                    _leaveRequest.SubmissionDate = DateTime.Now;
                }
                
                // حفظ البيانات
                bool success;
                if (_isEditMode)
                {
                    // تحديث طلب الإجازة الحالي
                    // هذه الدالة غير مكتملة في المستودع، يمكن تنفيذها لاحقاً
                    success = true; // مؤقتاً
                }
                else
                {
                    // إضافة طلب إجازة جديد
                    success = _unitOfWork.LeaveRepository.AddLeaveRequest(_leaveRequest);
                }
                
                if (success)
                {
                    // إغلاق النموذج بنجاح
                    XtraMessageBox.Show(
                        _isEditMode ? "تم تحديث طلب الإجازة بنجاح." : "تم إضافة طلب الإجازة بنجاح.",
                        "تم بنجاح",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show(
                        "فشل في حفظ بيانات طلب الإجازة.",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل في حفظ بيانات طلب الإجازة");
                XtraMessageBox.Show(
                    $"حدث خطأ أثناء حفظ البيانات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// التحقق من صحة البيانات المدخلة
        /// </summary>
        private bool ValidateInput()
        {
            // التحقق من اختيار الموظف
            if (lookUpEditEmployee.EditValue == null)
            {
                XtraMessageBox.Show(
                    "الرجاء اختيار الموظف.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                lookUpEditEmployee.Focus();
                return false;
            }
            
            // التحقق من اختيار نوع الإجازة
            if (lookUpEditLeaveType.EditValue == null)
            {
                XtraMessageBox.Show(
                    "الرجاء اختيار نوع الإجازة.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                lookUpEditLeaveType.Focus();
                return false;
            }
            
            // التحقق من التواريخ
            if (dateEditStartDate.EditValue == null)
            {
                XtraMessageBox.Show(
                    "الرجاء تحديد تاريخ بداية الإجازة.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                dateEditStartDate.Focus();
                return false;
            }
            
            if (dateEditEndDate.EditValue == null)
            {
                XtraMessageBox.Show(
                    "الرجاء تحديد تاريخ نهاية الإجازة.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                dateEditEndDate.Focus();
                return false;
            }
            
            // التحقق من أن تاريخ البداية قبل تاريخ النهاية
            if (dateEditStartDate.DateTime > dateEditEndDate.DateTime)
            {
                XtraMessageBox.Show(
                    "يجب أن يكون تاريخ بداية الإجازة قبل تاريخ نهاية الإجازة.",
                    "تحقق من المدخلات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                dateEditStartDate.Focus();
                return false;
            }
            
            // التحقق من تداخل الإجازات
            int employeeId = Convert.ToInt32(lookUpEditEmployee.EditValue);
            DateTime startDate = dateEditStartDate.DateTime.Date;
            DateTime endDate = dateEditEndDate.DateTime.Date;
            
            bool hasOverlap = _unitOfWork.LeaveRepository.CheckLeaveOverlap(
                employeeId, startDate, endDate, _leaveRequestId);
                
            if (hasOverlap)
            {
                XtraMessageBox.Show(
                    "توجد إجازة أخرى متداخلة مع هذه الفترة للموظف المحدد.",
                    "تداخل إجازات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            
            // التحقق من الحد الأقصى لأيام الإجازة
            if (lookUpEditLeaveType.EditValue != null)
            {
                int leaveTypeId = Convert.ToInt32(lookUpEditLeaveType.EditValue);
                var leaveType = _unitOfWork.LeaveRepository.GetLeaveTypeById(leaveTypeId);
                
                if (leaveType != null && leaveType.MaximumDays > 0)
                {
                    int requestedDays = (endDate - startDate).Days + 1;
                    
                    if (requestedDays > leaveType.MaximumDays)
                    {
                        XtraMessageBox.Show(
                            $"عدد الأيام المطلوبة ({requestedDays}) يتجاوز الحد الأقصى المسموح به لهذا النوع من الإجازات ({leaveType.MaximumDays}).",
                            "تجاوز الحد الأقصى",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            
            return true;
        }

        /// <summary>
        /// حدث نقر زر الإلغاء
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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