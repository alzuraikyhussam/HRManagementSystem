using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Security
{
    /// <summary>
    /// نموذج قائمة المستخدمين
    /// </summary>
    public partial class UsersListForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;

        /// <summary>
        /// إنشاء نموذج جديد
        /// </summary>
        public UsersListForm()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void UsersListForm_Load(object sender, EventArgs e)
        {
            // التحقق من وجود صلاحية للمستخدم
            CheckUserPermissions();

            // تحميل بيانات المستخدمين
            LoadUsers();
        }

        /// <summary>
        /// التحقق من صلاحيات المستخدم
        /// </summary>
        private void CheckUserPermissions()
        {
            // التحقق من وجود صلاحية للمستخدم
            bool canAdd = _sessionManager.HasPermission("المستخدمين", "add");
            bool canEdit = _sessionManager.HasPermission("المستخدمين", "edit");
            bool canDelete = _sessionManager.HasPermission("المستخدمين", "delete");

            // تفعيل/تعطيل الأزرار حسب الصلاحيات
            btnAdd.Enabled = canAdd;
            btnEdit.Enabled = canEdit;
            btnDelete.Enabled = canDelete;
        }

        /// <summary>
        /// تحميل بيانات المستخدمين
        /// </summary>
        private void LoadUsers()
        {
            try
            {
                string query = @"
                SELECT 
                    u.ID,
                    u.Username,
                    u.FullName,
                    u.Email,
                    u.Mobile,
                    r.Name AS RoleName,
                    e.FullName AS EmployeeName,
                    u.IsActive,
                    u.IsLocked,
                    u.LastLogin,
                    u.LastPasswordChange,
                    u.FailedLoginAttempts,
                    u.CreatedAt,
                    creator.FullName AS CreatedBy,
                    u.UpdatedAt,
                    updater.FullName AS UpdatedBy
                FROM 
                    Users u
                LEFT JOIN 
                    Roles r ON u.RoleID = r.ID
                LEFT JOIN 
                    Employees e ON u.EmployeeID = e.ID
                LEFT JOIN 
                    Users creator ON u.CreatedBy = creator.ID
                LEFT JOIN 
                    Users updater ON u.UpdatedBy = updater.ID
                ORDER BY 
                    u.Username";

                var dataTable = _dbContext.ExecuteReader(query);

                // عرض البيانات في الشبكة
                gridUsers.DataSource = dataTable;
                gridViewUsers.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات المستخدمين: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث إضافة مستخدم جديد
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود صلاحية للمستخدم
                if (!_sessionManager.HasPermission("المستخدمين", "add"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية إضافة مستخدمين جدد", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // فتح نموذج إضافة مستخدم جديد
                using (UserForm userForm = new UserForm())
                {
                    if (userForm.ShowDialog() == DialogResult.OK)
                    {
                        // إعادة تحميل البيانات
                        LoadUsers();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إضافة المستخدم: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث تعديل مستخدم موجود
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود صلاحية للمستخدم
                if (!_sessionManager.HasPermission("المستخدمين", "edit"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية تعديل المستخدمين", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // التحقق من اختيار مستخدم
                if (gridViewUsers.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("يرجى اختيار مستخدم للتعديل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // الحصول على معرف المستخدم
                int userId = Convert.ToInt32(gridViewUsers.GetFocusedRowCellValue("ID"));

                // فتح نموذج تعديل المستخدم
                using (UserForm userForm = new UserForm(userId))
                {
                    if (userForm.ShowDialog() == DialogResult.OK)
                    {
                        // إعادة تحميل البيانات
                        LoadUsers();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تعديل المستخدم: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث حذف مستخدم
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود صلاحية للمستخدم
                if (!_sessionManager.HasPermission("المستخدمين", "delete"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية حذف المستخدمين", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // التحقق من اختيار مستخدم
                if (gridViewUsers.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("يرجى اختيار مستخدم للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // الحصول على معرف وبيانات المستخدم
                int userId = Convert.ToInt32(gridViewUsers.GetFocusedRowCellValue("ID"));
                string username = gridViewUsers.GetFocusedRowCellValue("Username").ToString();

                // التحقق من عدم حذف المستخدم الحالي
                if (userId == _sessionManager.CurrentUser?.ID)
                {
                    XtraMessageBox.Show("لا يمكن حذف حساب المستخدم الحالي", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // تأكيد الحذف
                if (XtraMessageBox.Show($"هل أنت متأكد من حذف المستخدم '{username}'؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // حذف المستخدم
                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@UserID", userId)
                    };

                    _dbContext.ExecuteTransaction((connection, transaction) =>
                    {
                        // تسجيل نشاط الحذف
                        string logQuery = @"
                        INSERT INTO ActivityLog (UserID, ActivityDate, ActivityType, ModuleName, Description, RecordID)
                        VALUES (@CurrentUserID, @ActivityDate, 'Delete', 'Users', @Description, @RecordID)";

                        SqlCommand logCmd = new SqlCommand(logQuery, connection, transaction);
                        logCmd.Parameters.Add(new SqlParameter("@CurrentUserID", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value));
                        logCmd.Parameters.Add(new SqlParameter("@ActivityDate", DateTime.Now));
                        logCmd.Parameters.Add(new SqlParameter("@Description", $"تم حذف المستخدم: {username}"));
                        logCmd.Parameters.Add(new SqlParameter("@RecordID", userId));
                        logCmd.ExecuteNonQuery();

                        // حذف سجل الدخول
                        string deleteLoginQuery = "DELETE FROM LoginHistory WHERE UserID = @UserID";
                        SqlCommand deleteLoginCmd = new SqlCommand(deleteLoginQuery, connection, transaction);
                        deleteLoginCmd.Parameters.Add(new SqlParameter("@UserID", userId));
                        deleteLoginCmd.ExecuteNonQuery();

                        // حذف سجل النشاطات
                        string deleteActivityQuery = "DELETE FROM ActivityLog WHERE UserID = @UserID";
                        SqlCommand deleteActivityCmd = new SqlCommand(deleteActivityQuery, connection, transaction);
                        deleteActivityCmd.Parameters.Add(new SqlParameter("@UserID", userId));
                        deleteActivityCmd.ExecuteNonQuery();

                        // حذف المستخدم
                        string deleteUserQuery = "DELETE FROM Users WHERE ID = @UserID";
                        SqlCommand deleteUserCmd = new SqlCommand(deleteUserQuery, connection, transaction);
                        deleteUserCmd.Parameters.Add(new SqlParameter("@UserID", userId));
                        deleteUserCmd.ExecuteNonQuery();
                    });

                    // عرض رسالة نجاح
                    XtraMessageBox.Show("تم حذف المستخدم بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // إعادة تحميل البيانات
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حذف المستخدم: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث تحديث البيانات
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        /// <summary>
        /// حدث النقر المزدوج على صف في الشبكة
        /// </summary>
        private void gridViewUsers_DoubleClick(object sender, EventArgs e)
        {
            if (btnEdit.Enabled)
            {
                btnEdit_Click(sender, e);
            }
        }

        /// <summary>
        /// حدث إلغاء قفل حساب المستخدم
        /// </summary>
        private void btnUnlockAccount_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود صلاحية للمستخدم
                if (!_sessionManager.HasPermission("المستخدمين", "edit"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية تعديل المستخدمين", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // التحقق من اختيار مستخدم
                if (gridViewUsers.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("يرجى اختيار مستخدم لإلغاء قفل حسابه", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // الحصول على معرف وبيانات المستخدم
                int userId = Convert.ToInt32(gridViewUsers.GetFocusedRowCellValue("ID"));
                string username = gridViewUsers.GetFocusedRowCellValue("Username").ToString();
                bool isLocked = Convert.ToBoolean(gridViewUsers.GetFocusedRowCellValue("IsLocked"));

                if (!isLocked)
                {
                    XtraMessageBox.Show("حساب المستخدم غير مقفل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // تأكيد إلغاء قفل الحساب
                if (XtraMessageBox.Show($"هل أنت متأكد من إلغاء قفل حساب المستخدم '{username}'؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@UserID", userId),
                        new SqlParameter("@UpdatedAt", DateTime.Now),
                        new SqlParameter("@UpdatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                    };

                    string query = @"
                    UPDATE Users 
                    SET IsLocked = 0,
                        LockoutEnd = NULL,
                        FailedLoginAttempts = 0,
                        UpdatedAt = @UpdatedAt,
                        UpdatedBy = @UpdatedBy
                    WHERE ID = @UserID";

                    int result = _dbContext.ExecuteNonQuery(query, parameters);

                    if (result > 0)
                    {
                        XtraMessageBox.Show("تم إلغاء قفل حساب المستخدم بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // إعادة تحميل البيانات
                        LoadUsers();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إلغاء قفل حساب المستخدم: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث إعادة تعيين كلمة المرور
        /// </summary>
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود صلاحية للمستخدم
                if (!_sessionManager.HasPermission("المستخدمين", "edit"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية تعديل المستخدمين", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // التحقق من اختيار مستخدم
                if (gridViewUsers.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("يرجى اختيار مستخدم لإعادة تعيين كلمة المرور", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // الحصول على معرف وبيانات المستخدم
                int userId = Convert.ToInt32(gridViewUsers.GetFocusedRowCellValue("ID"));
                string username = gridViewUsers.GetFocusedRowCellValue("Username").ToString();

                // نافذة إدخال كلمة المرور الجديدة
                using (var resetForm = new PasswordResetForm())
                {
                    if (resetForm.ShowDialog() == DialogResult.OK)
                    {
                        string newPassword = resetForm.Password;
                        bool mustChangePassword = resetForm.MustChangePassword;

                        if (string.IsNullOrWhiteSpace(newPassword))
                        {
                            XtraMessageBox.Show("يرجى إدخال كلمة المرور الجديدة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // تعيين كلمة المرور الجديدة
                        string salt = _sessionManager.GenerateSalt();
                        string passwordHash = _sessionManager.HashPassword(newPassword, salt);

                        List<SqlParameter> parameters = new List<SqlParameter>
                        {
                            new SqlParameter("@UserID", userId),
                            new SqlParameter("@PasswordHash", passwordHash),
                            new SqlParameter("@PasswordSalt", salt),
                            new SqlParameter("@MustChangePassword", mustChangePassword),
                            new SqlParameter("@LastPasswordChange", DateTime.Now),
                            new SqlParameter("@UpdatedAt", DateTime.Now),
                            new SqlParameter("@UpdatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                        };

                        string query = @"
                        UPDATE Users 
                        SET PasswordHash = @PasswordHash,
                            PasswordSalt = @PasswordSalt,
                            MustChangePassword = @MustChangePassword,
                            LastPasswordChange = @LastPasswordChange,
                            UpdatedAt = @UpdatedAt,
                            UpdatedBy = @UpdatedBy
                        WHERE ID = @UserID";

                        int result = _dbContext.ExecuteNonQuery(query, parameters);

                        if (result > 0)
                        {
                            XtraMessageBox.Show("تم إعادة تعيين كلمة المرور بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            // إعادة تحميل البيانات
                            LoadUsers();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء إعادة تعيين كلمة المرور: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث عرض سجل تسجيل الدخول
        /// </summary>
        private void btnViewLoginHistory_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من اختيار مستخدم
                if (gridViewUsers.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("يرجى اختيار مستخدم لعرض سجل تسجيل الدخول", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // الحصول على معرف وبيانات المستخدم
                int userId = Convert.ToInt32(gridViewUsers.GetFocusedRowCellValue("ID"));
                string username = gridViewUsers.GetFocusedRowCellValue("Username").ToString();

                // فتح نموذج سجل تسجيل الدخول
                using (var loginHistoryForm = new LoginHistoryForm(userId, username))
                {
                    loginHistoryForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء عرض سجل تسجيل الدخول: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث تغيير تخطيط الشبكة
        /// </summary>
        private void btnChangeLayout_Click(object sender, EventArgs e)
        {
            // عرض محرر التخطيط
            gridViewUsers.ShowLayoutEditor();
        }

        /// <summary>
        /// حدث تصدير البيانات
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود صلاحية للمستخدم
                if (!_sessionManager.HasPermission("المستخدمين", "export"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية تصدير بيانات المستخدمين", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // عرض حوار حفظ الملف
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "ملف إكسل (*.xlsx)|*.xlsx|ملف PDF (*.pdf)|*.pdf";
                saveDialog.FileName = "تقرير_المستخدمين";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveDialog.FileName;
                    string extension = System.IO.Path.GetExtension(filePath).ToLower();

                    switch (extension)
                    {
                        case ".xlsx":
                            gridUsers.ExportToXlsx(filePath);
                            break;
                        case ".pdf":
                            gridUsers.ExportToPdf(filePath);
                            break;
                    }

                    // فتح الملف بعد التصدير
                    if (XtraMessageBox.Show("تم تصدير البيانات بنجاح. هل تريد فتح الملف؟", "نجاح", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تصدير البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث طباعة البيانات
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من وجود صلاحية للمستخدم
                if (!_sessionManager.HasPermission("المستخدمين", "print"))
                {
                    XtraMessageBox.Show("ليس لديك صلاحية طباعة بيانات المستخدمين", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // عرض معاينة الطباعة
                gridUsers.ShowPrintPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء طباعة البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// نموذج إعادة تعيين كلمة المرور
    /// </summary>
    public class PasswordResetForm : XtraForm
    {
        private TextEdit txtPassword;
        private TextEdit txtConfirmPassword;
        private CheckEdit chkMustChangePassword;
        private SimpleButton btnOK;
        private SimpleButton btnCancel;
        private LabelControl lblPasswordMatch;

        /// <summary>
        /// كلمة المرور الجديدة
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// هل يجب تغيير كلمة المرور عند أول تسجيل دخول
        /// </summary>
        public bool MustChangePassword { get; private set; }

        /// <summary>
        /// إنشاء نموذج إعادة تعيين كلمة المرور
        /// </summary>
        public PasswordResetForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// تهيئة مكونات النموذج
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtConfirmPassword = new DevExpress.XtraEditors.TextEdit();
            this.chkMustChangePassword = new DevExpress.XtraEditors.CheckEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblPasswordMatch = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMustChangePassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(12, 36);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Properties.NullText = "أدخل كلمة المرور الجديدة";
            this.txtPassword.Size = new System.Drawing.Size(360, 20);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(12, 84);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Properties.PasswordChar = '*';
            this.txtConfirmPassword.Properties.NullText = "تأكيد كلمة المرور الجديدة";
            this.txtConfirmPassword.Size = new System.Drawing.Size(360, 20);
            this.txtConfirmPassword.TabIndex = 1;
            this.txtConfirmPassword.TextChanged += new System.EventHandler(this.txtConfirmPassword_TextChanged);
            // 
            // chkMustChangePassword
            // 
            this.chkMustChangePassword.Location = new System.Drawing.Point(12, 124);
            this.chkMustChangePassword.Name = "chkMustChangePassword";
            this.chkMustChangePassword.Properties.Caption = "يجب تغيير كلمة المرور عند أول تسجيل دخول";
            this.chkMustChangePassword.Size = new System.Drawing.Size(360, 20);
            this.chkMustChangePassword.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(297, 160);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "موافق";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(216, 160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "إلغاء";
            // 
            // lblPasswordMatch
            // 
            this.lblPasswordMatch.Location = new System.Drawing.Point(12, 110);
            this.lblPasswordMatch.Name = "lblPasswordMatch";
            this.lblPasswordMatch.Size = new System.Drawing.Size(0, 13);
            this.lblPasswordMatch.TabIndex = 5;
            // 
            // PasswordResetForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 195);
            this.Controls.Add(this.lblPasswordMatch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkMustChangePassword);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.txtPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordResetForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "إعادة تعيين كلمة المرور";
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMustChangePassword.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            // Labels
            DevExpress.XtraEditors.LabelControl lblPassword = new DevExpress.XtraEditors.LabelControl();
            lblPassword.Location = new System.Drawing.Point(12, 12);
            lblPassword.Name = "lblPassword";
            lblPassword.Text = "كلمة المرور الجديدة:";
            this.Controls.Add(lblPassword);

            DevExpress.XtraEditors.LabelControl lblConfirmPassword = new DevExpress.XtraEditors.LabelControl();
            lblConfirmPassword.Location = new System.Drawing.Point(12, 62);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Text = "تأكيد كلمة المرور:";
            this.Controls.Add(lblConfirmPassword);

            // Default values
            chkMustChangePassword.Checked = true;
        }

        /// <summary>
        /// حدث تغيير النص في حقل كلمة المرور
        /// </summary>
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            CheckPasswordsMatch();
        }

        /// <summary>
        /// حدث تغيير النص في حقل تأكيد كلمة المرور
        /// </summary>
        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            CheckPasswordsMatch();
        }

        /// <summary>
        /// التحقق من تطابق كلمات المرور
        /// </summary>
        private void CheckPasswordsMatch()
        {
            if (txtConfirmPassword.Text.Length > 0)
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
            else
            {
                lblPasswordMatch.Text = "";
            }
        }

        /// <summary>
        /// حدث النقر على زر الموافقة
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // التحقق من صحة البيانات
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                XtraMessageBox.Show("يرجى إدخال كلمة المرور الجديدة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                XtraMessageBox.Show("كلمة المرور وتأكيدها غير متطابقين", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            // تعيين قيم الخاصيات
            Password = txtPassword.Text;
            MustChangePassword = chkMustChangePassword.Checked;

            // إغلاق النموذج
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    /// <summary>
    /// نموذج سجل تسجيل الدخول
    /// </summary>
    public class LoginHistoryForm : XtraForm
    {
        private DevExpress.XtraGrid.GridControl gridLoginHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLoginHistory;
        private int _userId;
        private string _username;
        private DatabaseContext _dbContext;

        /// <summary>
        /// إنشاء نموذج سجل تسجيل الدخول
        /// </summary>
        /// <param name="userId">معرف المستخدم</param>
        /// <param name="username">اسم المستخدم</param>
        public LoginHistoryForm(int userId, string username)
        {
            _userId = userId;
            _username = username;
            _dbContext = new DatabaseContext();
            InitializeComponent();
            LoadLoginHistory();
        }

        /// <summary>
        /// تهيئة مكونات النموذج
        /// </summary>
        private void InitializeComponent()
        {
            this.gridLoginHistory = new DevExpress.XtraGrid.GridControl();
            this.gridViewLoginHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridLoginHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLoginHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLoginHistory
            // 
            this.gridLoginHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLoginHistory.Location = new System.Drawing.Point(0, 0);
            this.gridLoginHistory.MainView = this.gridViewLoginHistory;
            this.gridLoginHistory.Name = "gridLoginHistory";
            this.gridLoginHistory.Size = new System.Drawing.Size(684, 461);
            this.gridLoginHistory.TabIndex = 0;
            this.gridLoginHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLoginHistory});
            // 
            // gridViewLoginHistory
            // 
            this.gridViewLoginHistory.GridControl = this.gridLoginHistory;
            this.gridViewLoginHistory.Name = "gridViewLoginHistory";
            this.gridViewLoginHistory.OptionsBehavior.Editable = false;
            this.gridViewLoginHistory.OptionsBehavior.ReadOnly = true;
            this.gridViewLoginHistory.OptionsFind.AlwaysVisible = true;
            this.gridViewLoginHistory.OptionsView.ShowGroupPanel = false;
            // 
            // LoginHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.gridLoginHistory);
            this.MinimizeBox = false;
            this.Name = "LoginHistoryForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = $"سجل تسجيل الدخول للمستخدم: {_username}";
            ((System.ComponentModel.ISupportInitialize)(this.gridLoginHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLoginHistory)).EndInit();
            this.ResumeLayout(false);
        }

        /// <summary>
        /// تحميل سجل تسجيل الدخول
        /// </summary>
        private void LoadLoginHistory()
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UserID", _userId)
                };

                string query = @"
                SELECT 
                    LoginTime,
                    LogoutTime,
                    IPAddress,
                    MachineName,
                    LoginStatus,
                    UserAgent
                FROM 
                    LoginHistory
                WHERE 
                    UserID = @UserID
                ORDER BY 
                    LoginTime DESC";

                var dataTable = _dbContext.ExecuteReader(query, parameters);

                gridLoginHistory.DataSource = dataTable;
                gridViewLoginHistory.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل سجل تسجيل الدخول: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}