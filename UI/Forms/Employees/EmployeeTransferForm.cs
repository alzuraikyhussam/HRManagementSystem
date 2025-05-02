using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Employees
{
    /// <summary>
    /// نموذج إدارة سجلات النقل والترقية للموظف
    /// </summary>
    public partial class EmployeeTransferForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;
        private readonly int _employeeId;
        private int _transferId = 0;
        private bool _isNew = true;
        private EmployeeTransfer _currentTransfer;

        /// <summary>
        /// إنشاء نموذج جديد لإضافة سجل نقل/ترقية
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        public EmployeeTransferForm(int employeeId)
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
            _employeeId = employeeId;
            _currentTransfer = new EmployeeTransfer();
        }

        /// <summary>
        /// إنشاء نموذج لتعديل سجل نقل/ترقية موجود
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="transferId">معرف سجل النقل/الترقية</param>
        public EmployeeTransferForm(int employeeId, int transferId) : this(employeeId)
        {
            _isNew = false;
            _transferId = transferId;
            LoadTransfer();
        }

        /// <summary>
        /// تحميل بيانات سجل النقل/الترقية
        /// </summary>
        private void LoadTransfer()
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@TransferID", _transferId)
                };

                string query = @"
                SELECT 
                    ID, EmployeeID, TransferType, FromDepartmentID, ToDepartmentID, 
                    FromPositionID, ToPositionID, EffectiveDate, Reason, Notes,
                    CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
                FROM 
                    EmployeeTransfers
                WHERE 
                    ID = @TransferID";

                var dataTable = _dbContext.ExecuteReader(query, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    var row = dataTable.Rows[0];
                    _currentTransfer = new EmployeeTransfer
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                        TransferType = row["TransferType"].ToString(),
                        FromDepartmentID = row["FromDepartmentID"] as int?,
                        ToDepartmentID = row["ToDepartmentID"] as int?,
                        FromPositionID = row["FromPositionID"] as int?,
                        ToPositionID = row["ToPositionID"] as int?,
                        EffectiveDate = Convert.ToDateTime(row["EffectiveDate"]),
                        Reason = row["Reason"].ToString(),
                        Notes = row["Notes"].ToString(),
                        CreatedAt = row["CreatedAt"] as DateTime?,
                        CreatedBy = row["CreatedBy"] as int?,
                        UpdatedAt = row["UpdatedAt"] as DateTime?,
                        UpdatedBy = row["UpdatedBy"] as int?
                    };

                    // تعبئة بيانات السجل
                    cmbTransferType.EditValue = _currentTransfer.TransferType;
                    dtEffectiveDate.EditValue = _currentTransfer.EffectiveDate;
                    txtReason.Text = _currentTransfer.Reason;
                    txtNotes.Text = _currentTransfer.Notes;

                    // تحميل الأقسام والمسميات الوظيفية
                    LoadDepartments();
                    LoadPositions();

                    // تعيين القيم المحددة
                    if (_currentTransfer.FromDepartmentID.HasValue)
                    {
                        cmbFromDepartment.EditValue = _currentTransfer.FromDepartmentID.Value;
                    }

                    if (_currentTransfer.ToDepartmentID.HasValue)
                    {
                        cmbToDepartment.EditValue = _currentTransfer.ToDepartmentID.Value;
                    }

                    if (_currentTransfer.FromPositionID.HasValue)
                    {
                        cmbFromPosition.EditValue = _currentTransfer.FromPositionID.Value;
                    }

                    if (_currentTransfer.ToPositionID.HasValue)
                    {
                        cmbToPosition.EditValue = _currentTransfer.ToPositionID.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات سجل النقل/الترقية: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تهيئة القوائم المنسدلة
        /// </summary>
        private void InitializeComboBoxes()
        {
            // تهيئة قائمة أنواع النقل/الترقية
            cmbTransferType.Properties.Items.AddRange(new string[]
            {
                "نقل",
                "ترقية",
                "تعيين جديد",
                "إعادة هيكلة",
                "تغيير مسمى وظيفي",
                "أخرى"
            });
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

                cmbFromDepartment.Properties.DataSource = dataTable;
                cmbFromDepartment.Properties.DisplayMember = "Name";
                cmbFromDepartment.Properties.ValueMember = "ID";
                cmbFromDepartment.Properties.PopulateColumns();
                cmbFromDepartment.Properties.Columns["ID"].Visible = false;

                // استخدام نسخة من نفس البيانات للقسم الجديد
                var toDeptTable = dataTable.Copy();
                cmbToDepartment.Properties.DataSource = toDeptTable;
                cmbToDepartment.Properties.DisplayMember = "Name";
                cmbToDepartment.Properties.ValueMember = "ID";
                cmbToDepartment.Properties.PopulateColumns();
                cmbToDepartment.Properties.Columns["ID"].Visible = false;
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

                // تصفية المسميات الوظيفية حسب القسم القديم إذا تم اختياره
                if (cmbFromDepartment.EditValue != null)
                {
                    string fromPositionsQuery = query + " AND DepartmentID = " + cmbFromDepartment.EditValue.ToString() + " ORDER BY Title";
                    var fromPositionsTable = _dbContext.ExecuteReader(fromPositionsQuery);

                    cmbFromPosition.Properties.DataSource = fromPositionsTable;
                    cmbFromPosition.Properties.DisplayMember = "Title";
                    cmbFromPosition.Properties.ValueMember = "ID";
                    cmbFromPosition.Properties.PopulateColumns();
                    cmbFromPosition.Properties.Columns["ID"].Visible = false;
                    cmbFromPosition.Properties.Columns["DepartmentID"].Visible = false;
                }

                // تصفية المسميات الوظيفية حسب القسم الجديد إذا تم اختياره
                if (cmbToDepartment.EditValue != null)
                {
                    string toPositionsQuery = query + " AND DepartmentID = " + cmbToDepartment.EditValue.ToString() + " ORDER BY Title";
                    var toPositionsTable = _dbContext.ExecuteReader(toPositionsQuery);

                    cmbToPosition.Properties.DataSource = toPositionsTable;
                    cmbToPosition.Properties.DisplayMember = "Title";
                    cmbToPosition.Properties.ValueMember = "ID";
                    cmbToPosition.Properties.PopulateColumns();
                    cmbToPosition.Properties.Columns["ID"].Visible = false;
                    cmbToPosition.Properties.Columns["DepartmentID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل المسميات الوظيفية: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حفظ بيانات سجل النقل/الترقية
        /// </summary>
        private void SaveTransfer()
        {
            try
            {
                // التحقق من صحة البيانات
                if (cmbTransferType.EditValue == null)
                {
                    XtraMessageBox.Show("يرجى اختيار نوع النقل/الترقية", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbTransferType.Focus();
                    return;
                }

                if (dtEffectiveDate.EditValue == null)
                {
                    XtraMessageBox.Show("يرجى تحديد تاريخ السريان", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtEffectiveDate.Focus();
                    return;
                }

                if (cmbToDepartment.EditValue == null)
                {
                    XtraMessageBox.Show("يرجى اختيار القسم الجديد", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbToDepartment.Focus();
                    return;
                }

                if (cmbToPosition.EditValue == null)
                {
                    XtraMessageBox.Show("يرجى اختيار المسمى الوظيفي الجديد", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbToPosition.Focus();
                    return;
                }

                // تحديث بيانات السجل
                _currentTransfer.EmployeeID = _employeeId;
                _currentTransfer.TransferType = cmbTransferType.EditValue.ToString();
                _currentTransfer.FromDepartmentID = cmbFromDepartment.EditValue != null ? Convert.ToInt32(cmbFromDepartment.EditValue) : (int?)null;
                _currentTransfer.ToDepartmentID = Convert.ToInt32(cmbToDepartment.EditValue);
                _currentTransfer.FromPositionID = cmbFromPosition.EditValue != null ? Convert.ToInt32(cmbFromPosition.EditValue) : (int?)null;
                _currentTransfer.ToPositionID = Convert.ToInt32(cmbToPosition.EditValue);
                _currentTransfer.EffectiveDate = (DateTime)dtEffectiveDate.EditValue;
                _currentTransfer.Reason = txtReason.Text;
                _currentTransfer.Notes = txtNotes.Text;

                // بدء المعاملة
                _dbContext.ExecuteTransaction((connection, transaction) =>
                {
                    if (_isNew)
                    {
                        // إضافة سجل جديد
                        string insertQuery = @"
                        INSERT INTO EmployeeTransfers (
                            EmployeeID, TransferType, FromDepartmentID, ToDepartmentID, 
                            FromPositionID, ToPositionID, EffectiveDate, Reason, Notes,
                            CreatedAt, CreatedBy
                        ) VALUES (
                            @EmployeeID, @TransferType, @FromDepartmentID, @ToDepartmentID, 
                            @FromPositionID, @ToPositionID, @EffectiveDate, @Reason, @Notes,
                            @CreatedAt, @CreatedBy
                        );
                        SELECT SCOPE_IDENTITY();";

                        List<SqlParameter> insertParams = new List<SqlParameter>
                        {
                            new SqlParameter("@EmployeeID", _currentTransfer.EmployeeID),
                            new SqlParameter("@TransferType", _currentTransfer.TransferType),
                            new SqlParameter("@FromDepartmentID", _currentTransfer.FromDepartmentID ?? (object)DBNull.Value),
                            new SqlParameter("@ToDepartmentID", _currentTransfer.ToDepartmentID),
                            new SqlParameter("@FromPositionID", _currentTransfer.FromPositionID ?? (object)DBNull.Value),
                            new SqlParameter("@ToPositionID", _currentTransfer.ToPositionID),
                            new SqlParameter("@EffectiveDate", _currentTransfer.EffectiveDate),
                            new SqlParameter("@Reason", _currentTransfer.Reason ?? (object)DBNull.Value),
                            new SqlParameter("@Notes", _currentTransfer.Notes ?? (object)DBNull.Value),
                            new SqlParameter("@CreatedAt", DateTime.Now),
                            new SqlParameter("@CreatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                        };

                        SqlCommand cmd = new SqlCommand(insertQuery, connection, transaction);
                        cmd.Parameters.AddRange(insertParams.ToArray());
                        _currentTransfer.ID = Convert.ToInt32(cmd.ExecuteScalar());

                        // تحديث بيانات الموظف
                        string updateEmployeeQuery = @"
                        UPDATE Employees SET
                            DepartmentID = @DepartmentID,
                            PositionID = @PositionID,
                            UpdatedAt = @UpdatedAt,
                            UpdatedBy = @UpdatedBy
                        WHERE ID = @EmployeeID";

                        List<SqlParameter> updateEmployeeParams = new List<SqlParameter>
                        {
                            new SqlParameter("@EmployeeID", _currentTransfer.EmployeeID),
                            new SqlParameter("@DepartmentID", _currentTransfer.ToDepartmentID),
                            new SqlParameter("@PositionID", _currentTransfer.ToPositionID),
                            new SqlParameter("@UpdatedAt", DateTime.Now),
                            new SqlParameter("@UpdatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                        };

                        SqlCommand updateEmployeeCmd = new SqlCommand(updateEmployeeQuery, connection, transaction);
                        updateEmployeeCmd.Parameters.AddRange(updateEmployeeParams.ToArray());
                        updateEmployeeCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // تحديث سجل موجود
                        string updateQuery = @"
                        UPDATE EmployeeTransfers SET
                            TransferType = @TransferType,
                            FromDepartmentID = @FromDepartmentID,
                            ToDepartmentID = @ToDepartmentID,
                            FromPositionID = @FromPositionID,
                            ToPositionID = @ToPositionID,
                            EffectiveDate = @EffectiveDate,
                            Reason = @Reason,
                            Notes = @Notes,
                            UpdatedAt = @UpdatedAt,
                            UpdatedBy = @UpdatedBy
                        WHERE ID = @ID";

                        List<SqlParameter> updateParams = new List<SqlParameter>
                        {
                            new SqlParameter("@ID", _currentTransfer.ID),
                            new SqlParameter("@TransferType", _currentTransfer.TransferType),
                            new SqlParameter("@FromDepartmentID", _currentTransfer.FromDepartmentID ?? (object)DBNull.Value),
                            new SqlParameter("@ToDepartmentID", _currentTransfer.ToDepartmentID),
                            new SqlParameter("@FromPositionID", _currentTransfer.FromPositionID ?? (object)DBNull.Value),
                            new SqlParameter("@ToPositionID", _currentTransfer.ToPositionID),
                            new SqlParameter("@EffectiveDate", _currentTransfer.EffectiveDate),
                            new SqlParameter("@Reason", _currentTransfer.Reason ?? (object)DBNull.Value),
                            new SqlParameter("@Notes", _currentTransfer.Notes ?? (object)DBNull.Value),
                            new SqlParameter("@UpdatedAt", DateTime.Now),
                            new SqlParameter("@UpdatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                        };

                        SqlCommand cmd = new SqlCommand(updateQuery, connection, transaction);
                        cmd.Parameters.AddRange(updateParams.ToArray());
                        cmd.ExecuteNonQuery();

                        // تحديث بيانات الموظف إذا كان سجل النقل/الترقية هو الأحدث
                        string checkLatestQuery = @"
                        SELECT TOP 1 ID
                        FROM EmployeeTransfers
                        WHERE EmployeeID = @EmployeeID
                        ORDER BY EffectiveDate DESC, ID DESC";

                        SqlCommand checkLatestCmd = new SqlCommand(checkLatestQuery, connection, transaction);
                        checkLatestCmd.Parameters.Add(new SqlParameter("@EmployeeID", _currentTransfer.EmployeeID));
                        int latestTransferId = Convert.ToInt32(checkLatestCmd.ExecuteScalar());

                        if (latestTransferId == _currentTransfer.ID)
                        {
                            string updateEmployeeQuery = @"
                            UPDATE Employees SET
                                DepartmentID = @DepartmentID,
                                PositionID = @PositionID,
                                UpdatedAt = @UpdatedAt,
                                UpdatedBy = @UpdatedBy
                            WHERE ID = @EmployeeID";

                            List<SqlParameter> updateEmployeeParams = new List<SqlParameter>
                            {
                                new SqlParameter("@EmployeeID", _currentTransfer.EmployeeID),
                                new SqlParameter("@DepartmentID", _currentTransfer.ToDepartmentID),
                                new SqlParameter("@PositionID", _currentTransfer.ToPositionID),
                                new SqlParameter("@UpdatedAt", DateTime.Now),
                                new SqlParameter("@UpdatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                            };

                            SqlCommand updateEmployeeCmd = new SqlCommand(updateEmployeeQuery, connection, transaction);
                            updateEmployeeCmd.Parameters.AddRange(updateEmployeeParams.ToArray());
                            updateEmployeeCmd.ExecuteNonQuery();
                        }
                    }
                });

                // عرض رسالة نجاح
                XtraMessageBox.Show("تم حفظ بيانات سجل النقل/الترقية بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حفظ بيانات سجل النقل/الترقية: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث تحميل بيانات الموظف الحالية
        /// </summary>
        private void LoadCurrentEmployeeData()
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@EmployeeID", _employeeId)
                };

                string query = @"
                SELECT 
                    e.DepartmentID, d.Name AS DepartmentName,
                    e.PositionID, p.Title AS PositionTitle
                FROM 
                    Employees e
                LEFT JOIN 
                    Departments d ON e.DepartmentID = d.ID
                LEFT JOIN 
                    Positions p ON e.PositionID = p.ID
                WHERE 
                    e.ID = @EmployeeID";

                var dataTable = _dbContext.ExecuteReader(query, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    var row = dataTable.Rows[0];
                    
                    // تعيين القسم والمسمى الوظيفي الحاليين
                    if (row["DepartmentID"] != DBNull.Value)
                    {
                        int departmentId = Convert.ToInt32(row["DepartmentID"]);
                        cmbFromDepartment.EditValue = departmentId;
                    }

                    if (row["PositionID"] != DBNull.Value)
                    {
                        int positionId = Convert.ToInt32(row["PositionID"]);
                        cmbFromPosition.EditValue = positionId;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات الموظف الحالية: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void EmployeeTransferForm_Load(object sender, EventArgs e)
        {
            // تهيئة القوائم المنسدلة
            InitializeComboBoxes();

            // إذا كان سجل جديد، تحميل بيانات الموظف الحالية
            if (_isNew)
            {
                // تعيين تاريخ السريان إلى اليوم الحالي
                dtEffectiveDate.EditValue = DateTime.Today;

                // تحميل الأقسام
                LoadDepartments();

                // تحميل بيانات الموظف الحالية
                LoadCurrentEmployeeData();
            }

            // تعيين عنوان النموذج
            Text = _isNew ? "إضافة سجل نقل/ترقية جديد" : "تعديل سجل نقل/ترقية";
        }

        /// <summary>
        /// حدث تغيير القسم القديم
        /// </summary>
        private void cmbFromDepartment_EditValueChanged(object sender, EventArgs e)
        {
            // تحميل المسميات الوظيفية للقسم القديم
            if (cmbFromDepartment.EditValue != null)
            {
                try
                {
                    string query = @"
                    SELECT ID, Title, DepartmentID 
                    FROM Positions 
                    WHERE IsActive = 1 AND DepartmentID = @DepartmentID
                    ORDER BY Title";

                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@DepartmentID", Convert.ToInt32(cmbFromDepartment.EditValue))
                    };

                    var dataTable = _dbContext.ExecuteReader(query, parameters);

                    cmbFromPosition.Properties.DataSource = dataTable;
                    cmbFromPosition.Properties.DisplayMember = "Title";
                    cmbFromPosition.Properties.ValueMember = "ID";
                    cmbFromPosition.Properties.PopulateColumns();
                    cmbFromPosition.Properties.Columns["ID"].Visible = false;
                    cmbFromPosition.Properties.Columns["DepartmentID"].Visible = false;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("حدث خطأ أثناء تحميل المسميات الوظيفية للقسم: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// حدث تغيير القسم الجديد
        /// </summary>
        private void cmbToDepartment_EditValueChanged(object sender, EventArgs e)
        {
            // تحميل المسميات الوظيفية للقسم الجديد
            if (cmbToDepartment.EditValue != null)
            {
                try
                {
                    string query = @"
                    SELECT ID, Title, DepartmentID 
                    FROM Positions 
                    WHERE IsActive = 1 AND DepartmentID = @DepartmentID
                    ORDER BY Title";

                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@DepartmentID", Convert.ToInt32(cmbToDepartment.EditValue))
                    };

                    var dataTable = _dbContext.ExecuteReader(query, parameters);

                    cmbToPosition.Properties.DataSource = dataTable;
                    cmbToPosition.Properties.DisplayMember = "Title";
                    cmbToPosition.Properties.ValueMember = "ID";
                    cmbToPosition.Properties.PopulateColumns();
                    cmbToPosition.Properties.Columns["ID"].Visible = false;
                    cmbToPosition.Properties.Columns["DepartmentID"].Visible = false;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("حدث خطأ أثناء تحميل المسميات الوظيفية للقسم: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// حدث الضغط على زر الحفظ
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveTransfer();
        }

        /// <summary>
        /// حدث الضغط على زر الإلغاء
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}