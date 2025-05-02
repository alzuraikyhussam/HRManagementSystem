using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Employees
{
    /// <summary>
    /// نموذج إدارة الموظف
    /// </summary>
    public partial class EmployeeForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;
        private Employee _currentEmployee;
        private bool _isNew = true;
        private bool _isPhotoChanged = false;
        private byte[] _employeePhoto = null;

        /// <summary>
        /// إنشاء نموذج جديد
        /// </summary>
        public EmployeeForm()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
            _currentEmployee = new Employee();
        }

        /// <summary>
        /// إنشاء نموذج لتعديل موظف موجود
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        public EmployeeForm(int employeeId) : this()
        {
            _isNew = false;
            LoadEmployee(employeeId);
        }

        /// <summary>
        /// تحميل بيانات الموظف
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        private void LoadEmployee(int employeeId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@EmployeeID", employeeId)
                };

                string query = @"
                SELECT 
                    e.*,
                    d.Name AS DepartmentName,
                    p.Title AS PositionTitle,
                    manager.FullName AS ManagerName,
                    ws.Name AS WorkShiftName,
                    creator.FullName AS CreatorName,
                    updater.FullName AS UpdaterName
                FROM 
                    Employees e
                LEFT JOIN 
                    Departments d ON e.DepartmentID = d.ID
                LEFT JOIN 
                    Positions p ON e.PositionID = p.ID
                LEFT JOIN 
                    Employees manager ON e.DirectManagerID = manager.ID
                LEFT JOIN 
                    WorkShifts ws ON e.WorkShiftID = ws.ID
                LEFT JOIN 
                    Users creator ON e.CreatedBy = creator.ID
                LEFT JOIN 
                    Users updater ON e.UpdatedBy = updater.ID
                WHERE 
                    e.ID = @EmployeeID";

                var dataTable = _dbContext.ExecuteReader(query, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    var row = dataTable.Rows[0];
                    _currentEmployee = new Employee
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        EmployeeNumber = row["EmployeeNumber"].ToString(),
                        FirstName = row["FirstName"].ToString(),
                        MiddleName = row["MiddleName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        FullName = row["FullName"].ToString(),
                        Gender = row["Gender"].ToString(),
                        BirthDate = row["BirthDate"] as DateTime?,
                        NationalID = row["NationalID"].ToString(),
                        PassportNumber = row["PassportNumber"].ToString(),
                        MaritalStatus = row["MaritalStatus"].ToString(),
                        Nationality = row["Nationality"].ToString(),
                        Religion = row["Religion"].ToString(),
                        BloodType = row["BloodType"].ToString(),
                        Phone = row["Phone"].ToString(),
                        Mobile = row["Mobile"].ToString(),
                        Email = row["Email"].ToString(),
                        Address = row["Address"].ToString(),
                        EmergencyContact = row["EmergencyContact"].ToString(),
                        EmergencyPhone = row["EmergencyPhone"].ToString(),
                        DepartmentID = row["DepartmentID"] as int?,
                        PositionID = row["PositionID"] as int?,
                        DirectManagerID = row["DirectManagerID"] as int?,
                        HireDate = Convert.ToDateTime(row["HireDate"]),
                        ProbationEndDate = row["ProbationEndDate"] as DateTime?,
                        EmploymentType = row["EmploymentType"].ToString(),
                        ContractStartDate = row["ContractStartDate"] as DateTime?,
                        ContractEndDate = row["ContractEndDate"] as DateTime?,
                        WorkShiftID = row["WorkShiftID"] as int?,
                        Status = row["Status"].ToString(),
                        TerminationDate = row["TerminationDate"] as DateTime?,
                        TerminationReason = row["TerminationReason"].ToString(),
                        BankName = row["BankName"].ToString(),
                        BankBranch = row["BankBranch"].ToString(),
                        BankAccountNumber = row["BankAccountNumber"].ToString(),
                        IBAN = row["IBAN"].ToString(),
                        Notes = row["Notes"].ToString(),
                        BiometricID = row["BiometricID"] as int?,
                        CreatedAt = row["CreatedAt"] as DateTime?,
                        CreatedBy = row["CreatedBy"] as int?,
                        UpdatedAt = row["UpdatedAt"] as DateTime?,
                        UpdatedBy = row["UpdatedBy"] as int?
                    };

                    // تحميل الصورة
                    if (row["Photo"] != DBNull.Value)
                    {
                        _employeePhoto = (byte[])row["Photo"];
                        using (MemoryStream ms = new MemoryStream(_employeePhoto))
                        {
                            picEmployeePhoto.Image = System.Drawing.Image.FromStream(ms);
                        }
                    }

                    // تعبئة البيانات الشخصية
                    txtEmployeeNumber.Text = _currentEmployee.EmployeeNumber;
                    txtFirstName.Text = _currentEmployee.FirstName;
                    txtMiddleName.Text = _currentEmployee.MiddleName;
                    txtLastName.Text = _currentEmployee.LastName;
                    cmbGender.EditValue = _currentEmployee.Gender;
                    dtBirthDate.EditValue = _currentEmployee.BirthDate;
                    txtNationalID.Text = _currentEmployee.NationalID;
                    txtPassportNumber.Text = _currentEmployee.PassportNumber;
                    cmbMaritalStatus.EditValue = _currentEmployee.MaritalStatus;
                    txtNationality.Text = _currentEmployee.Nationality;
                    txtReligion.Text = _currentEmployee.Religion;
                    cmbBloodType.EditValue = _currentEmployee.BloodType;

                    // تعبئة بيانات الاتصال
                    txtPhone.Text = _currentEmployee.Phone;
                    txtMobile.Text = _currentEmployee.Mobile;
                    txtEmail.Text = _currentEmployee.Email;
                    txtAddress.Text = _currentEmployee.Address;
                    txtEmergencyContact.Text = _currentEmployee.EmergencyContact;
                    txtEmergencyPhone.Text = _currentEmployee.EmergencyPhone;

                    // تعبئة البيانات الوظيفية
                    LoadDepartments();
                    if (_currentEmployee.DepartmentID.HasValue)
                    {
                        cmbDepartment.EditValue = _currentEmployee.DepartmentID.Value;
                    }

                    LoadPositions();
                    if (_currentEmployee.PositionID.HasValue)
                    {
                        cmbPosition.EditValue = _currentEmployee.PositionID.Value;
                    }

                    LoadManagers();
                    if (_currentEmployee.DirectManagerID.HasValue)
                    {
                        cmbManager.EditValue = _currentEmployee.DirectManagerID.Value;
                    }

                    dtHireDate.EditValue = _currentEmployee.HireDate;
                    dtProbationEndDate.EditValue = _currentEmployee.ProbationEndDate;
                    cmbEmploymentType.EditValue = _currentEmployee.EmploymentType;
                    dtContractStartDate.EditValue = _currentEmployee.ContractStartDate;
                    dtContractEndDate.EditValue = _currentEmployee.ContractEndDate;

                    LoadWorkShifts();
                    if (_currentEmployee.WorkShiftID.HasValue)
                    {
                        cmbWorkShift.EditValue = _currentEmployee.WorkShiftID.Value;
                    }

                    cmbStatus.EditValue = _currentEmployee.Status;
                    dtTerminationDate.EditValue = _currentEmployee.TerminationDate;
                    txtTerminationReason.Text = _currentEmployee.TerminationReason;

                    // تعبئة البيانات المالية
                    txtBankName.Text = _currentEmployee.BankName;
                    txtBankBranch.Text = _currentEmployee.BankBranch;
                    txtBankAccountNumber.Text = _currentEmployee.BankAccountNumber;
                    txtIBAN.Text = _currentEmployee.IBAN;

                    // تعبئة بيانات إضافية
                    txtNotes.Text = _currentEmployee.Notes;
                    txtBiometricID.Text = _currentEmployee.BiometricID?.ToString();

                    // معلومات إضافية
                    lblCreatedInfo.Text = _currentEmployee.CreatedAt.HasValue
                        ? $"تم الإنشاء بواسطة: {row["CreatorName"]} بتاريخ: {_currentEmployee.CreatedAt.Value:yyyy-MM-dd hh:mm tt}"
                        : "";
                    
                    lblUpdatedInfo.Text = _currentEmployee.UpdatedAt.HasValue
                        ? $"آخر تعديل بواسطة: {row["UpdaterName"]} بتاريخ: {_currentEmployee.UpdatedAt.Value:yyyy-MM-dd hh:mm tt}"
                        : "";

                    // تحميل المستندات
                    LoadDocuments();

                    // تحميل سجلات النقل والترقية
                    LoadTransfers();

                    // تفعيل/تعطيل بعض الحقول حسب حالة الموظف
                    UpdateFormBasedOnStatus();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات الموظف: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل الأقسام
        /// </summary>
        private void LoadDepartments()
        {
            try
            {
                string query = @"
                SELECT ID, Name 
                FROM Departments 
                WHERE IsActive = 1 
                ORDER BY Name";

                var dataTable = _dbContext.ExecuteReader(query);

                cmbDepartment.Properties.DataSource = dataTable;
                cmbDepartment.Properties.DisplayMember = "Name";
                cmbDepartment.Properties.ValueMember = "ID";
                cmbDepartment.Properties.PopulateColumns();
                cmbDepartment.Properties.Columns["ID"].Visible = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل الأقسام: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل المسميات الوظيفية
        /// </summary>
        private void LoadPositions()
        {
            try
            {
                string query = @"
                SELECT ID, Title, DepartmentID 
                FROM Positions 
                WHERE IsActive = 1 ";

                if (cmbDepartment.EditValue != null)
                {
                    query += "AND DepartmentID = " + cmbDepartment.EditValue.ToString();
                }

                query += " ORDER BY Title";

                var dataTable = _dbContext.ExecuteReader(query);

                cmbPosition.Properties.DataSource = dataTable;
                cmbPosition.Properties.DisplayMember = "Title";
                cmbPosition.Properties.ValueMember = "ID";
                cmbPosition.Properties.PopulateColumns();
                cmbPosition.Properties.Columns["ID"].Visible = false;
                cmbPosition.Properties.Columns["DepartmentID"].Visible = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل المسميات الوظيفية: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل المدراء
        /// </summary>
        private void LoadManagers()
        {
            try
            {
                string query = @"
                SELECT 
                    e.ID, 
                    e.FullName,
                    p.Title AS Position,
                    d.Name AS Department
                FROM 
                    Employees e
                INNER JOIN 
                    Positions p ON e.PositionID = p.ID
                INNER JOIN 
                    Departments d ON e.DepartmentID = d.ID
                WHERE 
                    e.Status = 'Active' AND
                    p.IsManagerPosition = 1
                ORDER BY 
                    e.FullName";

                var dataTable = _dbContext.ExecuteReader(query);

                cmbManager.Properties.DataSource = dataTable;
                cmbManager.Properties.DisplayMember = "FullName";
                cmbManager.Properties.ValueMember = "ID";
                cmbManager.Properties.PopulateColumns();
                cmbManager.Properties.Columns["ID"].Visible = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل المدراء: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل المناوبات
        /// </summary>
        private void LoadWorkShifts()
        {
            try
            {
                string query = @"
                SELECT 
                    ws.ID, 
                    ws.Name,
                    wh.StartTime,
                    wh.EndTime
                FROM 
                    WorkShifts ws
                INNER JOIN 
                    WorkHours wh ON ws.WorkHoursID = wh.ID
                WHERE 
                    ws.IsActive = 1
                ORDER BY 
                    ws.Name";

                var dataTable = _dbContext.ExecuteReader(query);

                cmbWorkShift.Properties.DataSource = dataTable;
                cmbWorkShift.Properties.DisplayMember = "Name";
                cmbWorkShift.Properties.ValueMember = "ID";
                cmbWorkShift.Properties.PopulateColumns();
                cmbWorkShift.Properties.Columns["ID"].Visible = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل المناوبات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل المستندات
        /// </summary>
        private void LoadDocuments()
        {
            if (_isNew) return;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@EmployeeID", _currentEmployee.ID)
                };

                string query = @"
                SELECT 
                    ID,
                    DocumentType,
                    DocumentTitle,
                    DocumentNumber,
                    IssueDate,
                    ExpiryDate,
                    IssuedBy,
                    (CASE WHEN DocumentFile IS NOT NULL THEN 1 ELSE 0 END) AS HasFile,
                    DocumentPath,
                    Notes,
                    CreatedAt
                FROM 
                    EmployeeDocuments
                WHERE 
                    EmployeeID = @EmployeeID
                ORDER BY 
                    DocumentType, IssueDate DESC";

                var dataTable = _dbContext.ExecuteReader(query, parameters);

                gridDocuments.DataSource = dataTable;
                gridViewDocuments.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل المستندات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل سجلات النقل والترقية
        /// </summary>
        private void LoadTransfers()
        {
            if (_isNew) return;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@EmployeeID", _currentEmployee.ID)
                };

                string query = @"
                SELECT 
                    et.ID,
                    et.TransferType,
                    fromDept.Name AS FromDepartment,
                    toDept.Name AS ToDepartment,
                    fromPos.Title AS FromPosition,
                    toPos.Title AS ToPosition,
                    et.EffectiveDate,
                    et.Reason,
                    u.FullName AS CreatedBy,
                    et.CreatedAt
                FROM 
                    EmployeeTransfers et
                LEFT JOIN 
                    Departments fromDept ON et.FromDepartmentID = fromDept.ID
                LEFT JOIN 
                    Departments toDept ON et.ToDepartmentID = toDept.ID
                LEFT JOIN 
                    Positions fromPos ON et.FromPositionID = fromPos.ID
                LEFT JOIN 
                    Positions toPos ON et.ToPositionID = toPos.ID
                LEFT JOIN 
                    Users u ON et.CreatedBy = u.ID
                WHERE 
                    et.EmployeeID = @EmployeeID
                ORDER BY 
                    et.EffectiveDate DESC";

                var dataTable = _dbContext.ExecuteReader(query, parameters);

                gridTransfers.DataSource = dataTable;
                gridViewTransfers.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل سجلات النقل والترقية: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحديث النموذج حسب حالة الموظف
        /// </summary>
        private void UpdateFormBasedOnStatus()
        {
            bool isActive = cmbStatus.EditValue?.ToString() == "Active";
            bool isTerminated = cmbStatus.EditValue?.ToString() == "Terminated";

            // تفعيل/تعطيل حقول التوظيف
            cmbDepartment.Properties.ReadOnly = isTerminated;
            cmbPosition.Properties.ReadOnly = isTerminated;
            cmbManager.Properties.ReadOnly = isTerminated;
            cmbEmploymentType.Properties.ReadOnly = isTerminated;
            cmbWorkShift.Properties.ReadOnly = isTerminated;

            // عرض/إخفاء حقول إنهاء الخدمة
            lblTerminationDate.Visible = isTerminated;
            dtTerminationDate.Visible = isTerminated;
            lblTerminationReason.Visible = isTerminated;
            txtTerminationReason.Visible = isTerminated;

            // إذا كان الموظف منتهي الخدمة، يجب تعبئة تاريخ وسبب إنهاء الخدمة
            if (isTerminated)
            {
                dtTerminationDate.Properties.NullText = "يجب تحديد تاريخ إنهاء الخدمة";
            }
        }

        /// <summary>
        /// تعيين قيم البيانات الافتراضية
        /// </summary>
        private void SetDefaultValues()
        {
            // تعيين تاريخ التوظيف إلى اليوم الحالي
            dtHireDate.EditValue = DateTime.Today;

            // تعيين حالة الموظف الافتراضية
            cmbStatus.EditValue = "Active";

            // تعيين نوع التوظيف الافتراضي
            cmbEmploymentType.EditValue = "FullTime";

            // تعيين تاريخ نهاية فترة التجربة الافتراضي (بعد 3 أشهر من تاريخ التوظيف)
            dtProbationEndDate.EditValue = DateTime.Today.AddMonths(3);
        }

        /// <summary>
        /// حفظ بيانات الموظف
        /// </summary>
        private void SaveEmployee()
        {
            try
            {
                // التحقق من صحة البيانات
                if (string.IsNullOrWhiteSpace(txtEmployeeNumber.Text))
                {
                    XtraMessageBox.Show("يرجى إدخال الرقم الوظيفي للموظف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmployeeNumber.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    XtraMessageBox.Show("يرجى إدخال الاسم الأول للموظف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFirstName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    XtraMessageBox.Show("يرجى إدخال اسم العائلة للموظف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLastName.Focus();
                    return;
                }

                if (dtHireDate.EditValue == null)
                {
                    XtraMessageBox.Show("يرجى تحديد تاريخ التوظيف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtHireDate.Focus();
                    return;
                }

                if (cmbStatus.EditValue == null)
                {
                    XtraMessageBox.Show("يرجى تحديد حالة الموظف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbStatus.Focus();
                    return;
                }

                // التحقق من عدم تكرار الرقم الوظيفي
                if (_isNew || txtEmployeeNumber.Text != _currentEmployee.EmployeeNumber)
                {
                    List<SqlParameter> checkParams = new List<SqlParameter>
                    {
                        new SqlParameter("@EmployeeNumber", txtEmployeeNumber.Text)
                    };
                    
                    if (!_isNew)
                    {
                        checkParams.Add(new SqlParameter("@ID", _currentEmployee.ID));
                    }

                    string checkQuery = @"
                    SELECT COUNT(*) 
                    FROM Employees 
                    WHERE EmployeeNumber = @EmployeeNumber";
                    
                    if (!_isNew)
                    {
                        checkQuery += " AND ID != @ID";
                    }

                    int count = Convert.ToInt32(_dbContext.ExecuteScalar(checkQuery, checkParams));
                    
                    if (count > 0)
                    {
                        XtraMessageBox.Show("الرقم الوظيفي مستخدم بالفعل. يرجى إدخال رقم وظيفي آخر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEmployeeNumber.Focus();
                        return;
                    }
                }

                // تحديث بيانات الموظف
                _currentEmployee.EmployeeNumber = txtEmployeeNumber.Text;
                _currentEmployee.FirstName = txtFirstName.Text;
                _currentEmployee.MiddleName = txtMiddleName.Text;
                _currentEmployee.LastName = txtLastName.Text;
                _currentEmployee.Gender = cmbGender.EditValue?.ToString();
                _currentEmployee.BirthDate = dtBirthDate.EditValue as DateTime?;
                _currentEmployee.NationalID = txtNationalID.Text;
                _currentEmployee.PassportNumber = txtPassportNumber.Text;
                _currentEmployee.MaritalStatus = cmbMaritalStatus.EditValue?.ToString();
                _currentEmployee.Nationality = txtNationality.Text;
                _currentEmployee.Religion = txtReligion.Text;
                _currentEmployee.BloodType = cmbBloodType.EditValue?.ToString();
                _currentEmployee.Phone = txtPhone.Text;
                _currentEmployee.Mobile = txtMobile.Text;
                _currentEmployee.Email = txtEmail.Text;
                _currentEmployee.Address = txtAddress.Text;
                _currentEmployee.EmergencyContact = txtEmergencyContact.Text;
                _currentEmployee.EmergencyPhone = txtEmergencyPhone.Text;
                _currentEmployee.DepartmentID = cmbDepartment.EditValue != null ? Convert.ToInt32(cmbDepartment.EditValue) : (int?)null;
                _currentEmployee.PositionID = cmbPosition.EditValue != null ? Convert.ToInt32(cmbPosition.EditValue) : (int?)null;
                _currentEmployee.DirectManagerID = cmbManager.EditValue != null ? Convert.ToInt32(cmbManager.EditValue) : (int?)null;
                _currentEmployee.HireDate = (DateTime)dtHireDate.EditValue;
                _currentEmployee.ProbationEndDate = dtProbationEndDate.EditValue as DateTime?;
                _currentEmployee.EmploymentType = cmbEmploymentType.EditValue?.ToString();
                _currentEmployee.ContractStartDate = dtContractStartDate.EditValue as DateTime?;
                _currentEmployee.ContractEndDate = dtContractEndDate.EditValue as DateTime?;
                _currentEmployee.WorkShiftID = cmbWorkShift.EditValue != null ? Convert.ToInt32(cmbWorkShift.EditValue) : (int?)null;
                _currentEmployee.Status = cmbStatus.EditValue?.ToString();
                _currentEmployee.TerminationDate = dtTerminationDate.EditValue as DateTime?;
                _currentEmployee.TerminationReason = txtTerminationReason.Text;
                _currentEmployee.BankName = txtBankName.Text;
                _currentEmployee.BankBranch = txtBankBranch.Text;
                _currentEmployee.BankAccountNumber = txtBankAccountNumber.Text;
                _currentEmployee.IBAN = txtIBAN.Text;
                _currentEmployee.Notes = txtNotes.Text;
                _currentEmployee.BiometricID = string.IsNullOrEmpty(txtBiometricID.Text) ? (int?)null : Convert.ToInt32(txtBiometricID.Text);

                // إذا تم تغيير الصورة
                if (_isPhotoChanged && picEmployeePhoto.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        picEmployeePhoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        _employeePhoto = ms.ToArray();
                    }
                }

                // بدء المعاملة
                _dbContext.ExecuteTransaction((connection, transaction) =>
                {
                    if (_isNew)
                    {
                        // إضافة موظف جديد
                        string insertQuery = @"
                        INSERT INTO Employees (
                            EmployeeNumber, FirstName, MiddleName, LastName, Gender, BirthDate, NationalID, PassportNumber, 
                            MaritalStatus, Nationality, Religion, BloodType, Phone, Mobile, Email, Address, EmergencyContact, EmergencyPhone,
                            DepartmentID, PositionID, DirectManagerID, HireDate, ProbationEndDate, EmploymentType, ContractStartDate, ContractEndDate,
                            WorkShiftID, Status, TerminationDate, TerminationReason, BankName, BankBranch, BankAccountNumber, IBAN, 
                            Photo, Notes, BiometricID, CreatedAt, CreatedBy
                        ) VALUES (
                            @EmployeeNumber, @FirstName, @MiddleName, @LastName, @Gender, @BirthDate, @NationalID, @PassportNumber, 
                            @MaritalStatus, @Nationality, @Religion, @BloodType, @Phone, @Mobile, @Email, @Address, @EmergencyContact, @EmergencyPhone,
                            @DepartmentID, @PositionID, @DirectManagerID, @HireDate, @ProbationEndDate, @EmploymentType, @ContractStartDate, @ContractEndDate,
                            @WorkShiftID, @Status, @TerminationDate, @TerminationReason, @BankName, @BankBranch, @BankAccountNumber, @IBAN, 
                            @Photo, @Notes, @BiometricID, @CreatedAt, @CreatedBy
                        );
                        SELECT SCOPE_IDENTITY();";

                        List<SqlParameter> insertParams = new List<SqlParameter>
                        {
                            new SqlParameter("@EmployeeNumber", _currentEmployee.EmployeeNumber),
                            new SqlParameter("@FirstName", _currentEmployee.FirstName),
                            new SqlParameter("@MiddleName", _currentEmployee.MiddleName ?? (object)DBNull.Value),
                            new SqlParameter("@LastName", _currentEmployee.LastName),
                            new SqlParameter("@Gender", _currentEmployee.Gender ?? (object)DBNull.Value),
                            new SqlParameter("@BirthDate", _currentEmployee.BirthDate ?? (object)DBNull.Value),
                            new SqlParameter("@NationalID", _currentEmployee.NationalID ?? (object)DBNull.Value),
                            new SqlParameter("@PassportNumber", _currentEmployee.PassportNumber ?? (object)DBNull.Value),
                            new SqlParameter("@MaritalStatus", _currentEmployee.MaritalStatus ?? (object)DBNull.Value),
                            new SqlParameter("@Nationality", _currentEmployee.Nationality ?? (object)DBNull.Value),
                            new SqlParameter("@Religion", _currentEmployee.Religion ?? (object)DBNull.Value),
                            new SqlParameter("@BloodType", _currentEmployee.BloodType ?? (object)DBNull.Value),
                            new SqlParameter("@Phone", _currentEmployee.Phone ?? (object)DBNull.Value),
                            new SqlParameter("@Mobile", _currentEmployee.Mobile ?? (object)DBNull.Value),
                            new SqlParameter("@Email", _currentEmployee.Email ?? (object)DBNull.Value),
                            new SqlParameter("@Address", _currentEmployee.Address ?? (object)DBNull.Value),
                            new SqlParameter("@EmergencyContact", _currentEmployee.EmergencyContact ?? (object)DBNull.Value),
                            new SqlParameter("@EmergencyPhone", _currentEmployee.EmergencyPhone ?? (object)DBNull.Value),
                            new SqlParameter("@DepartmentID", _currentEmployee.DepartmentID ?? (object)DBNull.Value),
                            new SqlParameter("@PositionID", _currentEmployee.PositionID ?? (object)DBNull.Value),
                            new SqlParameter("@DirectManagerID", _currentEmployee.DirectManagerID ?? (object)DBNull.Value),
                            new SqlParameter("@HireDate", _currentEmployee.HireDate),
                            new SqlParameter("@ProbationEndDate", _currentEmployee.ProbationEndDate ?? (object)DBNull.Value),
                            new SqlParameter("@EmploymentType", _currentEmployee.EmploymentType ?? (object)DBNull.Value),
                            new SqlParameter("@ContractStartDate", _currentEmployee.ContractStartDate ?? (object)DBNull.Value),
                            new SqlParameter("@ContractEndDate", _currentEmployee.ContractEndDate ?? (object)DBNull.Value),
                            new SqlParameter("@WorkShiftID", _currentEmployee.WorkShiftID ?? (object)DBNull.Value),
                            new SqlParameter("@Status", _currentEmployee.Status),
                            new SqlParameter("@TerminationDate", _currentEmployee.TerminationDate ?? (object)DBNull.Value),
                            new SqlParameter("@TerminationReason", _currentEmployee.TerminationReason ?? (object)DBNull.Value),
                            new SqlParameter("@BankName", _currentEmployee.BankName ?? (object)DBNull.Value),
                            new SqlParameter("@BankBranch", _currentEmployee.BankBranch ?? (object)DBNull.Value),
                            new SqlParameter("@BankAccountNumber", _currentEmployee.BankAccountNumber ?? (object)DBNull.Value),
                            new SqlParameter("@IBAN", _currentEmployee.IBAN ?? (object)DBNull.Value),
                            new SqlParameter("@Photo", _employeePhoto ?? (object)DBNull.Value),
                            new SqlParameter("@Notes", _currentEmployee.Notes ?? (object)DBNull.Value),
                            new SqlParameter("@BiometricID", _currentEmployee.BiometricID ?? (object)DBNull.Value),
                            new SqlParameter("@CreatedAt", DateTime.Now),
                            new SqlParameter("@CreatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                        };

                        SqlCommand cmd = new SqlCommand(insertQuery, connection, transaction);
                        cmd.Parameters.AddRange(insertParams.ToArray());
                        _currentEmployee.ID = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    else
                    {
                        // تحديث موظف موجود
                        string updateQuery = @"
                        UPDATE Employees SET
                            EmployeeNumber = @EmployeeNumber,
                            FirstName = @FirstName,
                            MiddleName = @MiddleName,
                            LastName = @LastName,
                            Gender = @Gender,
                            BirthDate = @BirthDate,
                            NationalID = @NationalID,
                            PassportNumber = @PassportNumber,
                            MaritalStatus = @MaritalStatus,
                            Nationality = @Nationality,
                            Religion = @Religion,
                            BloodType = @BloodType,
                            Phone = @Phone,
                            Mobile = @Mobile,
                            Email = @Email,
                            Address = @Address,
                            EmergencyContact = @EmergencyContact,
                            EmergencyPhone = @EmergencyPhone,
                            DepartmentID = @DepartmentID,
                            PositionID = @PositionID,
                            DirectManagerID = @DirectManagerID,
                            HireDate = @HireDate,
                            ProbationEndDate = @ProbationEndDate,
                            EmploymentType = @EmploymentType,
                            ContractStartDate = @ContractStartDate,
                            ContractEndDate = @ContractEndDate,
                            WorkShiftID = @WorkShiftID,
                            Status = @Status,
                            TerminationDate = @TerminationDate,
                            TerminationReason = @TerminationReason,
                            BankName = @BankName,
                            BankBranch = @BankBranch,
                            BankAccountNumber = @BankAccountNumber,
                            IBAN = @IBAN,
                            Notes = @Notes,
                            BiometricID = @BiometricID,
                            UpdatedAt = @UpdatedAt,
                            UpdatedBy = @UpdatedBy";

                        // إضافة تحديث الصورة إذا تم تغييرها
                        if (_isPhotoChanged)
                        {
                            updateQuery += ", Photo = @Photo";
                        }

                        updateQuery += " WHERE ID = @ID";

                        List<SqlParameter> updateParams = new List<SqlParameter>
                        {
                            new SqlParameter("@ID", _currentEmployee.ID),
                            new SqlParameter("@EmployeeNumber", _currentEmployee.EmployeeNumber),
                            new SqlParameter("@FirstName", _currentEmployee.FirstName),
                            new SqlParameter("@MiddleName", _currentEmployee.MiddleName ?? (object)DBNull.Value),
                            new SqlParameter("@LastName", _currentEmployee.LastName),
                            new SqlParameter("@Gender", _currentEmployee.Gender ?? (object)DBNull.Value),
                            new SqlParameter("@BirthDate", _currentEmployee.BirthDate ?? (object)DBNull.Value),
                            new SqlParameter("@NationalID", _currentEmployee.NationalID ?? (object)DBNull.Value),
                            new SqlParameter("@PassportNumber", _currentEmployee.PassportNumber ?? (object)DBNull.Value),
                            new SqlParameter("@MaritalStatus", _currentEmployee.MaritalStatus ?? (object)DBNull.Value),
                            new SqlParameter("@Nationality", _currentEmployee.Nationality ?? (object)DBNull.Value),
                            new SqlParameter("@Religion", _currentEmployee.Religion ?? (object)DBNull.Value),
                            new SqlParameter("@BloodType", _currentEmployee.BloodType ?? (object)DBNull.Value),
                            new SqlParameter("@Phone", _currentEmployee.Phone ?? (object)DBNull.Value),
                            new SqlParameter("@Mobile", _currentEmployee.Mobile ?? (object)DBNull.Value),
                            new SqlParameter("@Email", _currentEmployee.Email ?? (object)DBNull.Value),
                            new SqlParameter("@Address", _currentEmployee.Address ?? (object)DBNull.Value),
                            new SqlParameter("@EmergencyContact", _currentEmployee.EmergencyContact ?? (object)DBNull.Value),
                            new SqlParameter("@EmergencyPhone", _currentEmployee.EmergencyPhone ?? (object)DBNull.Value),
                            new SqlParameter("@DepartmentID", _currentEmployee.DepartmentID ?? (object)DBNull.Value),
                            new SqlParameter("@PositionID", _currentEmployee.PositionID ?? (object)DBNull.Value),
                            new SqlParameter("@DirectManagerID", _currentEmployee.DirectManagerID ?? (object)DBNull.Value),
                            new SqlParameter("@HireDate", _currentEmployee.HireDate),
                            new SqlParameter("@ProbationEndDate", _currentEmployee.ProbationEndDate ?? (object)DBNull.Value),
                            new SqlParameter("@EmploymentType", _currentEmployee.EmploymentType ?? (object)DBNull.Value),
                            new SqlParameter("@ContractStartDate", _currentEmployee.ContractStartDate ?? (object)DBNull.Value),
                            new SqlParameter("@ContractEndDate", _currentEmployee.ContractEndDate ?? (object)DBNull.Value),
                            new SqlParameter("@WorkShiftID", _currentEmployee.WorkShiftID ?? (object)DBNull.Value),
                            new SqlParameter("@Status", _currentEmployee.Status),
                            new SqlParameter("@TerminationDate", _currentEmployee.TerminationDate ?? (object)DBNull.Value),
                            new SqlParameter("@TerminationReason", _currentEmployee.TerminationReason ?? (object)DBNull.Value),
                            new SqlParameter("@BankName", _currentEmployee.BankName ?? (object)DBNull.Value),
                            new SqlParameter("@BankBranch", _currentEmployee.BankBranch ?? (object)DBNull.Value),
                            new SqlParameter("@BankAccountNumber", _currentEmployee.BankAccountNumber ?? (object)DBNull.Value),
                            new SqlParameter("@IBAN", _currentEmployee.IBAN ?? (object)DBNull.Value),
                            new SqlParameter("@Notes", _currentEmployee.Notes ?? (object)DBNull.Value),
                            new SqlParameter("@BiometricID", _currentEmployee.BiometricID ?? (object)DBNull.Value),
                            new SqlParameter("@UpdatedAt", DateTime.Now),
                            new SqlParameter("@UpdatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                        };

                        // إضافة بارامتر الصورة إذا تم تغييرها
                        if (_isPhotoChanged)
                        {
                            updateParams.Add(new SqlParameter("@Photo", _employeePhoto ?? (object)DBNull.Value));
                        }

                        SqlCommand cmd = new SqlCommand(updateQuery, connection, transaction);
                        cmd.Parameters.AddRange(updateParams.ToArray());
                        cmd.ExecuteNonQuery();
                    }
                });

                // عرض رسالة نجاح
                XtraMessageBox.Show("تم حفظ بيانات الموظف بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حفظ بيانات الموظف: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// إضافة وثيقة جديدة
        /// </summary>
        private void AddDocument()
        {
            if (_isNew)
            {
                XtraMessageBox.Show("يجب حفظ بيانات الموظف أولاً قبل إضافة المستندات", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // فتح نموذج إضافة مستند جديد
            using (var documentForm = new EmployeeDocumentForm(_currentEmployee.ID))
            {
                if (documentForm.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل المستندات
                    LoadDocuments();
                }
            }
        }

        /// <summary>
        /// تعديل وثيقة موجودة
        /// </summary>
        private void EditDocument()
        {
            if (gridViewDocuments.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("يرجى اختيار مستند للتعديل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int documentId = Convert.ToInt32(gridViewDocuments.GetFocusedRowCellValue("ID"));

            // فتح نموذج تعديل المستند
            using (var documentForm = new EmployeeDocumentForm(_currentEmployee.ID, documentId))
            {
                if (documentForm.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل المستندات
                    LoadDocuments();
                }
            }
        }

        /// <summary>
        /// حذف وثيقة موجودة
        /// </summary>
        private void DeleteDocument()
        {
            if (gridViewDocuments.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("يرجى اختيار مستند للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int documentId = Convert.ToInt32(gridViewDocuments.GetFocusedRowCellValue("ID"));
            string documentTitle = gridViewDocuments.GetFocusedRowCellValue("DocumentTitle").ToString();

            // تأكيد الحذف
            if (XtraMessageBox.Show($"هل أنت متأكد من حذف المستند '{documentTitle}'؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // حذف المستند
                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@ID", documentId)
                    };

                    string query = "DELETE FROM EmployeeDocuments WHERE ID = @ID";
                    _dbContext.ExecuteNonQuery(query, parameters);

                    // إعادة تحميل المستندات
                    LoadDocuments();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("حدث خطأ أثناء حذف المستند: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// إضافة سجل نقل/ترقية جديد
        /// </summary>
        private void AddTransfer()
        {
            if (_isNew)
            {
                XtraMessageBox.Show("يجب حفظ بيانات الموظف أولاً قبل إضافة سجلات النقل/الترقية", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // فتح نموذج إضافة سجل نقل/ترقية جديد
            using (var transferForm = new EmployeeTransferForm(_currentEmployee.ID))
            {
                if (transferForm.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل سجلات النقل/الترقية
                    LoadTransfers();
                }
            }
        }

        /// <summary>
        /// تعديل سجل نقل/ترقية موجود
        /// </summary>
        private void EditTransfer()
        {
            if (gridViewTransfers.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("يرجى اختيار سجل نقل/ترقية للتعديل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int transferId = Convert.ToInt32(gridViewTransfers.GetFocusedRowCellValue("ID"));

            // فتح نموذج تعديل سجل النقل/الترقية
            using (var transferForm = new EmployeeTransferForm(_currentEmployee.ID, transferId))
            {
                if (transferForm.ShowDialog() == DialogResult.OK)
                {
                    // إعادة تحميل سجلات النقل/الترقية
                    LoadTransfers();
                }
            }
        }

        /// <summary>
        /// حذف سجل نقل/ترقية موجود
        /// </summary>
        private void DeleteTransfer()
        {
            if (gridViewTransfers.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("يرجى اختيار سجل نقل/ترقية للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int transferId = Convert.ToInt32(gridViewTransfers.GetFocusedRowCellValue("ID"));
            string transferType = gridViewTransfers.GetFocusedRowCellValue("TransferType").ToString();

            // تأكيد الحذف
            if (XtraMessageBox.Show($"هل أنت متأكد من حذف سجل {transferType}؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // حذف سجل النقل/الترقية
                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@ID", transferId)
                    };

                    string query = "DELETE FROM EmployeeTransfers WHERE ID = @ID";
                    _dbContext.ExecuteNonQuery(query, parameters);

                    // إعادة تحميل سجلات النقل/الترقية
                    LoadTransfers();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("حدث خطأ أثناء حذف سجل النقل/الترقية: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            // تهيئة القوائم المنسدلة
            InitializeComboBoxes();

            // إذا كان موظف جديد، تعيين القيم الافتراضية
            if (_isNew)
            {
                SetDefaultValues();
            }

            // تعيين عنوان النموذج
            Text = _isNew ? "إضافة موظف جديد" : "تعديل موظف: " + _currentEmployee.FullName;
        }

        /// <summary>
        /// تهيئة القوائم المنسدلة
        /// </summary>
        private void InitializeComboBoxes()
        {
            // تهيئة قائمة الجنس
            cmbGender.Properties.Items.AddRange(new string[] { "ذكر", "أنثى" });

            // تهيئة قائمة الحالة الاجتماعية
            cmbMaritalStatus.Properties.Items.AddRange(new string[] { "أعزب", "متزوج", "مطلق", "أرمل" });

            // تهيئة قائمة فصائل الدم
            cmbBloodType.Properties.Items.AddRange(new string[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" });

            // تهيئة قائمة نوع التوظيف
            cmbEmploymentType.Properties.Items.AddRange(new string[] { "FullTime", "PartTime", "Contractor", "Temporary" });

            // تهيئة قائمة حالة الموظف
            cmbStatus.Properties.Items.AddRange(new string[] { "Active", "OnProbation", "OnLeave", "Suspended", "Terminated" });

            // تحميل الأقسام والمناصب والمدراء والمناوبات
            if (!_isNew)
            {
                return; // سيتم تحميلهم في LoadEmployee
            }

            LoadDepartments();
            LoadManagers();
            LoadWorkShifts();
        }

        /// <summary>
        /// حدث اختيار القسم
        /// </summary>
        private void cmbDepartment_EditValueChanged(object sender, EventArgs e)
        {
            // تحميل المسميات الوظيفية للقسم المحدد
            LoadPositions();
        }

        /// <summary>
        /// حدث اختيار حالة الموظف
        /// </summary>
        private void cmbStatus_EditValueChanged(object sender, EventArgs e)
        {
            // تحديث النموذج حسب حالة الموظف
            UpdateFormBasedOnStatus();
        }

        /// <summary>
        /// حدث الضغط على زر اختيار الصورة
        /// </summary>
        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "ملفات الصور (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                openFileDialog.Title = "اختيار صورة الموظف";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // تحميل الصورة
                        picEmployeePhoto.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
                        _isPhotoChanged = true;
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("حدث خطأ أثناء تحميل الصورة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// حدث الضغط على زر حذف الصورة
        /// </summary>
        private void btnClearPhoto_Click(object sender, EventArgs e)
        {
            picEmployeePhoto.Image = null;
            _employeePhoto = null;
            _isPhotoChanged = true;
        }

        /// <summary>
        /// حدث الضغط على زر الحفظ
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveEmployee();
        }

        /// <summary>
        /// حدث الضغط على زر الإلغاء
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// حدث الضغط على زر إضافة مستند
        /// </summary>
        private void btnAddDocument_Click(object sender, EventArgs e)
        {
            AddDocument();
        }

        /// <summary>
        /// حدث الضغط على زر تعديل مستند
        /// </summary>
        private void btnEditDocument_Click(object sender, EventArgs e)
        {
            EditDocument();
        }

        /// <summary>
        /// حدث الضغط على زر حذف مستند
        /// </summary>
        private void btnDeleteDocument_Click(object sender, EventArgs e)
        {
            DeleteDocument();
        }

        /// <summary>
        /// حدث النقر المزدوج على صف في جدول المستندات
        /// </summary>
        private void gridViewDocuments_DoubleClick(object sender, EventArgs e)
        {
            EditDocument();
        }

        /// <summary>
        /// حدث الضغط على زر إضافة سجل نقل/ترقية
        /// </summary>
        private void btnAddTransfer_Click(object sender, EventArgs e)
        {
            AddTransfer();
        }

        /// <summary>
        /// حدث الضغط على زر تعديل سجل نقل/ترقية
        /// </summary>
        private void btnEditTransfer_Click(object sender, EventArgs e)
        {
            EditTransfer();
        }

        /// <summary>
        /// حدث الضغط على زر حذف سجل نقل/ترقية
        /// </summary>
        private void btnDeleteTransfer_Click(object sender, EventArgs e)
        {
            DeleteTransfer();
        }

        /// <summary>
        /// حدث النقر المزدوج على صف في جدول سجلات النقل/الترقية
        /// </summary>
        private void gridViewTransfers_DoubleClick(object sender, EventArgs e)
        {
            EditTransfer();
        }
    }
}