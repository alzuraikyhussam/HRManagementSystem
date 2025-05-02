using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Security
{
    /// <summary>
    /// نموذج إدارة المستخدم
    /// </summary>
    public partial class UserForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;
        private User _currentUser;
        private bool _isNew = true;
        private bool _isPasswordChanged = false;
        private byte[] _userPhoto = null;

        /// <summary>
        /// إنشاء نموذج جديد
        /// </summary>
        public UserForm()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
            _currentUser = new User();
        }

        /// <summary>
        /// إنشاء نموذج لتعديل مستخدم موجود
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        public UserForm(int userId) : this()
        {
            _isNew = false;
            LoadUser(userId);
        }

        /// <summary>
        /// تحميل بيانات المستخدم
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        private void LoadUser(int userId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UserID", userId)
                };

                string query = @"
                SELECT u.*, r.Name as RoleName, e.ID as EmployeeID, e.FullName as EmployeeFullName
                FROM Users u
                LEFT JOIN Roles r ON u.RoleID = r.ID
                LEFT JOIN Employees e ON u.EmployeeID = e.ID
                WHERE u.ID = @UserID";

                var dataTable = _dbContext.ExecuteReader(query, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    var row = dataTable.Rows[0];
                    _currentUser = new User
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Username = row["Username"].ToString(),
                        PasswordHash = row["PasswordHash"].ToString(),
                        PasswordSalt = row["PasswordSalt"].ToString(),
                        FullName = row["FullName"].ToString(),
                        Email = row["Email"].ToString(),
                        Mobile = row["Mobile"].ToString(),
                        RoleID = Convert.ToInt32(row["RoleID"]),
                        EmployeeID = row["EmployeeID"] as int?,
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        LastLoginDate = row["LastLogin"] as DateTime?,
                        FailedLoginAttempts = Convert.ToInt32(row["FailedLoginAttempts"]),
                        IsLocked = Convert.ToBoolean(row["IsLocked"]),
                        CreatedAt = row["CreatedAt"] as DateTime?,
                        CreatedBy = row["CreatedBy"] as int?,
                        UpdatedAt = row["UpdatedAt"] as DateTime?,
                        UpdatedBy = row["UpdatedBy"] as int?
                    };

                    // تعبئة البيانات في الحقول
                    txtUsername.Text = _currentUser.Username;
                    txtFullName.Text = _currentUser.FullName;
                    txtEmail.Text = _currentUser.Email;
                    txtMobile.Text = _currentUser.Mobile;
                    chkIsActive.Checked = _currentUser.IsActive;
                    
                    // تحميل الأدوار
                    LoadRoles();
                    if (_currentUser.RoleID > 0)
                    {
                        cmbRoles.EditValue = _currentUser.RoleID;
                    }

                    // تحميل الموظفين
                    LoadEmployees();
                    if (_currentUser.EmployeeID.HasValue)
                    {
                        cmbEmployees.EditValue = _currentUser.EmployeeID.Value;
                    }

                    // عرض معلومات الحساب
                    lblCreatedAt.Text = _currentUser.CreatedAt.HasValue ? 
                        $"تاريخ الإنشاء: {_currentUser.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss")}" : "";
                    
                    lblLastLogin.Text = _currentUser.LastLoginDate.HasValue ? 
                        $"آخر تسجيل دخول: {_currentUser.LastLoginDate.Value.ToString("yyyy-MM-dd HH:mm:ss")}" : "لم يسجل الدخول بعد";
                    
                    lblLockedStatus.Text = _currentUser.IsLocked ? 
                        "حالة الحساب: مغلق" : "حالة الحساب: مفتوح";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات المستخدم: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل الأدوار
        /// </summary>
        private void LoadRoles()
        {
            try
            {
                string query = "SELECT ID, Name FROM Roles WHERE IsActive = 1 ORDER BY Name";
                var dataTable = _dbContext.ExecuteReader(query);

                cmbRoles.Properties.DataSource = dataTable;
                cmbRoles.Properties.DisplayMember = "Name";
                cmbRoles.Properties.ValueMember = "ID";
                cmbRoles.Properties.PopulateColumns();
                cmbRoles.Properties.Columns["ID"].Visible = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل الأدوار: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل الموظفين
        /// </summary>
        private void LoadEmployees()
        {
            try
            {
                string query = @"
                SELECT ID, FullName, EmployeeNumber
                FROM Employees
                WHERE Status = 'Active'
                ORDER BY FullName";

                var dataTable = _dbContext.ExecuteReader(query);

                cmbEmployees.Properties.DataSource = dataTable;
                cmbEmployees.Properties.DisplayMember = "FullName";
                cmbEmployees.Properties.ValueMember = "ID";
                cmbEmployees.Properties.PopulateColumns();
                cmbEmployees.Properties.Columns["ID"].Visible = false;
                
                // إضافة خيار "بدون موظف مرتبط"
                DataRow nullRow = dataTable.NewRow();
                nullRow["ID"] = DBNull.Value;
                nullRow["FullName"] = "-- بدون موظف مرتبط --";
                nullRow["EmployeeNumber"] = "";
                dataTable.Rows.InsertAt(nullRow, 0);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل الموظفين: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حفظ بيانات المستخدم
        /// </summary>
        private void SaveUser()
        {
            try
            {
                // التحقق من صحة البيانات
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    XtraMessageBox.Show("يرجى إدخال اسم المستخدم", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                if (_isNew && string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    XtraMessageBox.Show("يرجى إدخال كلمة المرور", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (_isNew && txtPassword.Text != txtConfirmPassword.Text)
                {
                    XtraMessageBox.Show("كلمة المرور وتأكيدها غير متطابقين", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmPassword.Focus();
                    return;
                }

                if (cmbRoles.EditValue == null)
                {
                    XtraMessageBox.Show("يرجى اختيار الدور", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRoles.Focus();
                    return;
                }

                // تحديث بيانات المستخدم
                _currentUser.Username = txtUsername.Text;
                _currentUser.FullName = txtFullName.Text;
                _currentUser.Email = txtEmail.Text;
                _currentUser.Mobile = txtMobile.Text;
                _currentUser.RoleID = Convert.ToInt32(cmbRoles.EditValue);
                _currentUser.EmployeeID = cmbEmployees.EditValue != null && cmbEmployees.EditValue != DBNull.Value ? 
                    Convert.ToInt32(cmbEmployees.EditValue) : (int?)null;
                _currentUser.IsActive = chkIsActive.Checked;

                // إذا كان مستخدم جديد أو تم تغيير كلمة المرور
                if (_isNew || _isPasswordChanged)
                {
                    _currentUser.PasswordSalt = _sessionManager.GenerateSalt();
                    _currentUser.PasswordHash = _sessionManager.HashPassword(txtPassword.Text, _currentUser.PasswordSalt);
                }

                // بدء المعاملة
                _dbContext.ExecuteTransaction((connection, transaction) =>
                {
                    if (_isNew)
                    {
                        // إضافة مستخدم جديد
                        string insertUserQuery = @"
                        INSERT INTO Users (
                            Username, PasswordHash, PasswordSalt, FullName, Email, Mobile, 
                            RoleID, EmployeeID, IsActive, MustChangePassword, 
                            CreatedAt, CreatedBy
                        )
                        VALUES (
                            @Username, @PasswordHash, @PasswordSalt, @FullName, @Email, @Mobile, 
                            @RoleID, @EmployeeID, @IsActive, @MustChangePassword, 
                            @CreatedAt, @CreatedBy
                        );
                        SELECT SCOPE_IDENTITY();";

                        List<SqlParameter> insertUserParams = new List<SqlParameter>
                        {
                            new SqlParameter("@Username", _currentUser.Username),
                            new SqlParameter("@PasswordHash", _currentUser.PasswordHash),
                            new SqlParameter("@PasswordSalt", _currentUser.PasswordSalt),
                            new SqlParameter("@FullName", _currentUser.FullName ?? (object)DBNull.Value),
                            new SqlParameter("@Email", _currentUser.Email ?? (object)DBNull.Value),
                            new SqlParameter("@Mobile", _currentUser.Mobile ?? (object)DBNull.Value),
                            new SqlParameter("@RoleID", _currentUser.RoleID),
                            new SqlParameter("@EmployeeID", _currentUser.EmployeeID ?? (object)DBNull.Value),
                            new SqlParameter("@IsActive", _currentUser.IsActive),
                            new SqlParameter("@MustChangePassword", chkMustChangePassword.Checked),
                            new SqlParameter("@CreatedAt", DateTime.Now),
                            new SqlParameter("@CreatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                        };

                        // تنفيذ الاستعلام وإرجاع المعرف الجديد
                        SqlCommand cmd = new SqlCommand(insertUserQuery, connection, transaction);
                        cmd.Parameters.AddRange(insertUserParams.ToArray());
                        _currentUser.ID = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    else
                    {
                        // تحديث مستخدم موجود
                        string updateUserQuery = @"
                        UPDATE Users 
                        SET Username = @Username, 
                            FullName = @FullName, 
                            Email = @Email, 
                            Mobile = @Mobile, 
                            RoleID = @RoleID, 
                            EmployeeID = @EmployeeID, 
                            IsActive = @IsActive,
                            UpdatedAt = @UpdatedAt, 
                            UpdatedBy = @UpdatedBy";

                        // إضافة تحديث كلمة المرور إذا تم تغييرها
                        if (_isPasswordChanged)
                        {
                            updateUserQuery += @",
                            PasswordHash = @PasswordHash,
                            PasswordSalt = @PasswordSalt,
                            MustChangePassword = @MustChangePassword,
                            LastPasswordChange = @LastPasswordChange";
                        }

                        updateUserQuery += " WHERE ID = @ID";

                        List<SqlParameter> updateUserParams = new List<SqlParameter>
                        {
                            new SqlParameter("@ID", _currentUser.ID),
                            new SqlParameter("@Username", _currentUser.Username),
                            new SqlParameter("@FullName", _currentUser.FullName ?? (object)DBNull.Value),
                            new SqlParameter("@Email", _currentUser.Email ?? (object)DBNull.Value),
                            new SqlParameter("@Mobile", _currentUser.Mobile ?? (object)DBNull.Value),
                            new SqlParameter("@RoleID", _currentUser.RoleID),
                            new SqlParameter("@EmployeeID", _currentUser.EmployeeID ?? (object)DBNull.Value),
                            new SqlParameter("@IsActive", _currentUser.IsActive),
                            new SqlParameter("@UpdatedAt", DateTime.Now),
                            new SqlParameter("@UpdatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                        };

                        // إضافة بارامترات كلمة المرور إذا تم تغييرها
                        if (_isPasswordChanged)
                        {
                            updateUserParams.Add(new SqlParameter("@PasswordHash", _currentUser.PasswordHash));
                            updateUserParams.Add(new SqlParameter("@PasswordSalt", _currentUser.PasswordSalt));
                            updateUserParams.Add(new SqlParameter("@MustChangePassword", chkMustChangePassword.Checked));
                            updateUserParams.Add(new SqlParameter("@LastPasswordChange", DateTime.Now));
                        }

                        SqlCommand cmd = new SqlCommand(updateUserQuery, connection, transaction);
                        cmd.Parameters.AddRange(updateUserParams.ToArray());
                        cmd.ExecuteNonQuery();
                    }
                });

                // عرض رسالة نجاح
                XtraMessageBox.Show("تم حفظ بيانات المستخدم بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حفظ بيانات المستخدم: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث الضغط على زر الحفظ
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveUser();
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
        /// حدث تحميل النموذج
        /// </summary>
        private void UserForm_Load(object sender, EventArgs e)
        {
            // تحميل الأدوار والموظفين إذا كان مستخدم جديد
            if (_isNew)
            {
                LoadRoles();
                LoadEmployees();
                
                // تفعيل خيار تغيير كلمة المرور عند أول تسجيل دخول
                chkMustChangePassword.Checked = true;
            }
            else
            {
                // تعطيل تغيير اسم المستخدم
                txtUsername.ReadOnly = true;
                txtUsername.Properties.ReadOnly = true;
                
                // إخفاء خيار تغيير كلمة المرور عند أول تسجيل دخول
                chkMustChangePassword.Visible = false;
            }

            // تغيير مجموعة كلمة المرور حسب حالة المستخدم
            groupPassword.Text = _isNew ? "كلمة المرور" : "تغيير كلمة المرور (اتركها فارغة إذا لم ترغب في تغييرها)";
            
            // تعيين عنوان النموذج
            Text = _isNew ? "إضافة مستخدم جديد" : "تعديل مستخدم: " + _currentUser.Username;
        }

        /// <summary>
        /// حدث تغيير النص في حقل كلمة المرور
        /// </summary>
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (!_isNew && !string.IsNullOrEmpty(txtPassword.Text))
            {
                _isPasswordChanged = true;
                txtConfirmPassword.Enabled = true;
            }
            else if (!_isNew && string.IsNullOrEmpty(txtPassword.Text))
            {
                _isPasswordChanged = false;
                txtConfirmPassword.Text = "";
                txtConfirmPassword.Enabled = false;
            }
        }

        /// <summary>
        /// حدث تغيير النص في حقل تأكيد كلمة المرور
        /// </summary>
        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                lblPasswordMatch.Text = "كلمة المرور وتأكيدها غير متطابقين";
                lblPasswordMatch.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblPasswordMatch.Text = "كلمة المرور وتأكيدها متطابقين";
                lblPasswordMatch.ForeColor = System.Drawing.Color.Green;
            }
        }

        /// <summary>
        /// حدث تغيير النص في حقل اسم المستخدم للتحقق من وجوده مسبقاً
        /// </summary>
        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (_isNew && !string.IsNullOrEmpty(txtUsername.Text))
            {
                CheckUsernameExists();
            }
        }

        /// <summary>
        /// التحقق من وجود اسم المستخدم
        /// </summary>
        private void CheckUsernameExists()
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Username", txtUsername.Text)
                };

                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                
                int count = Convert.ToInt32(_dbContext.ExecuteScalar(query, parameters));
                
                if (count > 0)
                {
                    XtraMessageBox.Show("اسم المستخدم موجود بالفعل. يرجى اختيار اسم مستخدم آخر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    txtUsername.SelectAll();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء التحقق من اسم المستخدم: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث اختيار موظف
        /// </summary>
        private void cmbEmployees_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbEmployees.EditValue != null && cmbEmployees.EditValue != DBNull.Value)
            {
                try
                {
                    int employeeId = Convert.ToInt32(cmbEmployees.EditValue);
                    var selectedRow = ((DataTable)cmbEmployees.Properties.DataSource).Select($"ID = {employeeId}");
                    
                    if (selectedRow.Length > 0)
                    {
                        // تلقائياً تعبئة اسم المستخدم من رقم الموظف إذا كان فارغاً
                        if (string.IsNullOrEmpty(txtUsername.Text) && !txtUsername.ReadOnly)
                        {
                            string employeeNumber = selectedRow[0]["EmployeeNumber"].ToString();
                            if (!string.IsNullOrEmpty(employeeNumber))
                            {
                                txtUsername.Text = "emp" + employeeNumber;
                            }
                        }

                        // تلقائياً تعبئة الاسم الكامل من اسم الموظف إذا كان فارغاً
                        if (string.IsNullOrEmpty(txtFullName.Text))
                        {
                            txtFullName.Text = selectedRow[0]["FullName"].ToString();
                        }

                        // تحميل صورة الموظف
                        LoadEmployeePhoto(employeeId);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("حدث خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// تحميل صورة الموظف
        /// </summary>
        private void LoadEmployeePhoto(int employeeId)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@EmployeeID", employeeId)
                };

                string query = "SELECT Photo FROM Employees WHERE ID = @EmployeeID";
                
                var result = _dbContext.ExecuteScalar(query, parameters);
                
                if (result != null && result != DBNull.Value)
                {
                    _userPhoto = (byte[])result;
                    using (MemoryStream ms = new MemoryStream(_userPhoto))
                    {
                        pictureEdit1.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                else
                {
                    pictureEdit1.Image = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل صورة الموظف: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث إلغاء قفل حساب المستخدم
        /// </summary>
        private void btnUnlockAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isNew || !_currentUser.IsLocked)
                {
                    return;
                }

                // تأكيد إلغاء قفل الحساب
                if (XtraMessageBox.Show("هل أنت متأكد من إلغاء قفل حساب المستخدم؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@UserID", _currentUser.ID)
                    };

                    string query = @"
                    UPDATE Users 
                    SET IsLocked = 0,
                        LockoutEnd = NULL,
                        FailedLoginAttempts = 0,
                        UpdatedAt = @UpdatedAt,
                        UpdatedBy = @UpdatedBy
                    WHERE ID = @UserID";

                    parameters.Add(new SqlParameter("@UpdatedAt", DateTime.Now));
                    parameters.Add(new SqlParameter("@UpdatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value));

                    int result = _dbContext.ExecuteNonQuery(query, parameters);

                    if (result > 0)
                    {
                        XtraMessageBox.Show("تم إلغاء قفل حساب المستخدم بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _currentUser.IsLocked = false;
                        lblLockedStatus.Text = "حالة الحساب: مفتوح";
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إلغاء قفل حساب المستخدم: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}