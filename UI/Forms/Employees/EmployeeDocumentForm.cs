using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HR.Core;
using HR.DataAccess;
using HR.Models;

namespace HR.UI.Forms.Employees
{
    /// <summary>
    /// نموذج إدارة وثائق الموظف
    /// </summary>
    public partial class EmployeeDocumentForm : XtraForm
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionManager _sessionManager;
        private readonly int _employeeId;
        private int _documentId = 0;
        private bool _isNew = true;
        private EmployeeDocument _currentDocument;
        private byte[] _documentFile = null;
        private bool _isFileChanged = false;

        /// <summary>
        /// إنشاء نموذج جديد لإضافة وثيقة
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        public EmployeeDocumentForm(int employeeId)
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
            _sessionManager = SessionManager.Instance;
            _employeeId = employeeId;
            _currentDocument = new EmployeeDocument();
        }

        /// <summary>
        /// إنشاء نموذج لتعديل وثيقة موجودة
        /// </summary>
        /// <param name="employeeId">معرف الموظف</param>
        /// <param name="documentId">معرف الوثيقة</param>
        public EmployeeDocumentForm(int employeeId, int documentId) : this(employeeId)
        {
            _isNew = false;
            _documentId = documentId;
            LoadDocument();
        }

        /// <summary>
        /// تحميل بيانات الوثيقة
        /// </summary>
        private void LoadDocument()
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@DocumentID", _documentId)
                };

                string query = @"
                SELECT 
                    ID, EmployeeID, DocumentType, DocumentTitle, DocumentNumber, IssueDate, ExpiryDate, IssuedBy,
                    DocumentFile, DocumentPath, Notes, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
                FROM 
                    EmployeeDocuments
                WHERE 
                    ID = @DocumentID";

                var dataTable = _dbContext.ExecuteReader(query, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    var row = dataTable.Rows[0];
                    _currentDocument = new EmployeeDocument
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                        DocumentType = row["DocumentType"].ToString(),
                        DocumentTitle = row["DocumentTitle"].ToString(),
                        DocumentNumber = row["DocumentNumber"].ToString(),
                        IssueDate = row["IssueDate"] as DateTime?,
                        ExpiryDate = row["ExpiryDate"] as DateTime?,
                        IssuedBy = row["IssuedBy"].ToString(),
                        DocumentPath = row["DocumentPath"].ToString(),
                        Notes = row["Notes"].ToString(),
                        CreatedAt = row["CreatedAt"] as DateTime?,
                        CreatedBy = row["CreatedBy"] as int?,
                        UpdatedAt = row["UpdatedAt"] as DateTime?,
                        UpdatedBy = row["UpdatedBy"] as int?
                    };

                    // تحميل ملف الوثيقة
                    if (row["DocumentFile"] != DBNull.Value)
                    {
                        _documentFile = (byte[])row["DocumentFile"];
                        txtFilePath.Text = "الملف موجود. يمكنك تنزيله باستخدام زر التنزيل.";
                        btnDownloadFile.Enabled = true;
                    }

                    // تعبئة بيانات الوثيقة
                    cmbDocumentType.EditValue = _currentDocument.DocumentType;
                    txtDocumentTitle.Text = _currentDocument.DocumentTitle;
                    txtDocumentNumber.Text = _currentDocument.DocumentNumber;
                    dtIssueDate.EditValue = _currentDocument.IssueDate;
                    dtExpiryDate.EditValue = _currentDocument.ExpiryDate;
                    txtIssuedBy.Text = _currentDocument.IssuedBy;
                    txtNotes.Text = _currentDocument.Notes;
                    txtDocumentPath.Text = _currentDocument.DocumentPath;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل بيانات الوثيقة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تهيئة القوائم المنسدلة
        /// </summary>
        private void InitializeComboBoxes()
        {
            // تهيئة قائمة أنواع الوثائق
            cmbDocumentType.Properties.Items.AddRange(new string[]
            {
                "بطاقة هوية",
                "جواز سفر",
                "رخصة قيادة",
                "شهادة ميلاد",
                "شهادة دراسية",
                "عقد عمل",
                "شهادة صحية",
                "شهادة خبرة",
                "شهادة تدريب",
                "إقامة",
                "تأشيرة",
                "تصريح عمل",
                "أخرى"
            });
        }

        /// <summary>
        /// حفظ بيانات الوثيقة
        /// </summary>
        private void SaveDocument()
        {
            try
            {
                // التحقق من صحة البيانات
                if (cmbDocumentType.EditValue == null)
                {
                    XtraMessageBox.Show("يرجى اختيار نوع الوثيقة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbDocumentType.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDocumentTitle.Text))
                {
                    XtraMessageBox.Show("يرجى إدخال عنوان الوثيقة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDocumentTitle.Focus();
                    return;
                }

                // تحديث بيانات الوثيقة
                _currentDocument.EmployeeID = _employeeId;
                _currentDocument.DocumentType = cmbDocumentType.EditValue.ToString();
                _currentDocument.DocumentTitle = txtDocumentTitle.Text;
                _currentDocument.DocumentNumber = txtDocumentNumber.Text;
                _currentDocument.IssueDate = dtIssueDate.EditValue as DateTime?;
                _currentDocument.ExpiryDate = dtExpiryDate.EditValue as DateTime?;
                _currentDocument.IssuedBy = txtIssuedBy.Text;
                _currentDocument.DocumentPath = txtDocumentPath.Text;
                _currentDocument.Notes = txtNotes.Text;

                // بدء المعاملة
                _dbContext.ExecuteTransaction((connection, transaction) =>
                {
                    if (_isNew)
                    {
                        // إضافة وثيقة جديدة
                        string insertQuery = @"
                        INSERT INTO EmployeeDocuments (
                            EmployeeID, DocumentType, DocumentTitle, DocumentNumber, IssueDate, ExpiryDate, IssuedBy,
                            DocumentFile, DocumentPath, Notes, CreatedAt, CreatedBy
                        ) VALUES (
                            @EmployeeID, @DocumentType, @DocumentTitle, @DocumentNumber, @IssueDate, @ExpiryDate, @IssuedBy,
                            @DocumentFile, @DocumentPath, @Notes, @CreatedAt, @CreatedBy
                        );
                        SELECT SCOPE_IDENTITY();";

                        List<SqlParameter> insertParams = new List<SqlParameter>
                        {
                            new SqlParameter("@EmployeeID", _currentDocument.EmployeeID),
                            new SqlParameter("@DocumentType", _currentDocument.DocumentType),
                            new SqlParameter("@DocumentTitle", _currentDocument.DocumentTitle),
                            new SqlParameter("@DocumentNumber", _currentDocument.DocumentNumber ?? (object)DBNull.Value),
                            new SqlParameter("@IssueDate", _currentDocument.IssueDate ?? (object)DBNull.Value),
                            new SqlParameter("@ExpiryDate", _currentDocument.ExpiryDate ?? (object)DBNull.Value),
                            new SqlParameter("@IssuedBy", _currentDocument.IssuedBy ?? (object)DBNull.Value),
                            new SqlParameter("@DocumentFile", _documentFile ?? (object)DBNull.Value),
                            new SqlParameter("@DocumentPath", _currentDocument.DocumentPath ?? (object)DBNull.Value),
                            new SqlParameter("@Notes", _currentDocument.Notes ?? (object)DBNull.Value),
                            new SqlParameter("@CreatedAt", DateTime.Now),
                            new SqlParameter("@CreatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                        };

                        SqlCommand cmd = new SqlCommand(insertQuery, connection, transaction);
                        cmd.Parameters.AddRange(insertParams.ToArray());
                        _currentDocument.ID = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    else
                    {
                        // تحديث وثيقة موجودة
                        string updateQuery = @"
                        UPDATE EmployeeDocuments SET
                            DocumentType = @DocumentType,
                            DocumentTitle = @DocumentTitle,
                            DocumentNumber = @DocumentNumber,
                            IssueDate = @IssueDate,
                            ExpiryDate = @ExpiryDate,
                            IssuedBy = @IssuedBy,
                            DocumentPath = @DocumentPath,
                            Notes = @Notes,
                            UpdatedAt = @UpdatedAt,
                            UpdatedBy = @UpdatedBy";

                        // إضافة تحديث الملف إذا تم تغييره
                        if (_isFileChanged)
                        {
                            updateQuery += ", DocumentFile = @DocumentFile";
                        }

                        updateQuery += " WHERE ID = @ID";

                        List<SqlParameter> updateParams = new List<SqlParameter>
                        {
                            new SqlParameter("@ID", _currentDocument.ID),
                            new SqlParameter("@DocumentType", _currentDocument.DocumentType),
                            new SqlParameter("@DocumentTitle", _currentDocument.DocumentTitle),
                            new SqlParameter("@DocumentNumber", _currentDocument.DocumentNumber ?? (object)DBNull.Value),
                            new SqlParameter("@IssueDate", _currentDocument.IssueDate ?? (object)DBNull.Value),
                            new SqlParameter("@ExpiryDate", _currentDocument.ExpiryDate ?? (object)DBNull.Value),
                            new SqlParameter("@IssuedBy", _currentDocument.IssuedBy ?? (object)DBNull.Value),
                            new SqlParameter("@DocumentPath", _currentDocument.DocumentPath ?? (object)DBNull.Value),
                            new SqlParameter("@Notes", _currentDocument.Notes ?? (object)DBNull.Value),
                            new SqlParameter("@UpdatedAt", DateTime.Now),
                            new SqlParameter("@UpdatedBy", _sessionManager.CurrentUser?.ID ?? (object)DBNull.Value)
                        };

                        // إضافة بارامتر الملف إذا تم تغييره
                        if (_isFileChanged)
                        {
                            updateParams.Add(new SqlParameter("@DocumentFile", _documentFile ?? (object)DBNull.Value));
                        }

                        SqlCommand cmd = new SqlCommand(updateQuery, connection, transaction);
                        cmd.Parameters.AddRange(updateParams.ToArray());
                        cmd.ExecuteNonQuery();
                    }
                });

                // عرض رسالة نجاح
                XtraMessageBox.Show("تم حفظ بيانات الوثيقة بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء حفظ بيانات الوثيقة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// اختيار ملف الوثيقة
        /// </summary>
        private void SelectFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "جميع الملفات (*.*)|*.*|PDF (*.pdf)|*.pdf|Microsoft Word (*.doc;*.docx)|*.doc;*.docx|صور (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                openFileDialog.Title = "اختيار ملف الوثيقة";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // تحميل الملف
                        _documentFile = File.ReadAllBytes(openFileDialog.FileName);
                        _isFileChanged = true;
                        txtFilePath.Text = openFileDialog.FileName;
                        btnDownloadFile.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("حدث خطأ أثناء تحميل الملف: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// تنزيل ملف الوثيقة
        /// </summary>
        private void DownloadFile()
        {
            if (_documentFile == null)
            {
                XtraMessageBox.Show("لا يوجد ملف للتنزيل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string fileExtension = ".pdf"; // افتراضي
                
                // محاولة استنتاج نوع الملف من العنوان
                if (!string.IsNullOrEmpty(_currentDocument.DocumentTitle))
                {
                    string title = _currentDocument.DocumentTitle.ToLower();
                    if (title.EndsWith(".pdf") || title.Contains("pdf"))
                        fileExtension = ".pdf";
                    else if (title.EndsWith(".doc") || title.EndsWith(".docx") || title.Contains("word"))
                        fileExtension = ".docx";
                    else if (title.EndsWith(".jpg") || title.EndsWith(".jpeg") || title.EndsWith(".png") || title.Contains("صورة") || title.Contains("image"))
                        fileExtension = ".jpg";
                }

                saveFileDialog.FileName = _currentDocument.DocumentTitle + fileExtension;
                saveFileDialog.Filter = "كل الملفات (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, _documentFile);
                        XtraMessageBox.Show("تم تنزيل الملف بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("حدث خطأ أثناء تنزيل الملف: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// حدث تحميل النموذج
        /// </summary>
        private void EmployeeDocumentForm_Load(object sender, EventArgs e)
        {
            // تهيئة القوائم المنسدلة
            InitializeComboBoxes();

            // تعيين عنوان النموذج
            Text = _isNew ? "إضافة وثيقة جديدة" : "تعديل وثيقة: " + _currentDocument.DocumentTitle;
        }

        /// <summary>
        /// حدث الضغط على زر اختيار الملف
        /// </summary>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            SelectFile();
        }

        /// <summary>
        /// حدث الضغط على زر تنزيل الملف
        /// </summary>
        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            DownloadFile();
        }

        /// <summary>
        /// حدث الضغط على زر حذف الملف
        /// </summary>
        private void btnClearFile_Click(object sender, EventArgs e)
        {
            _documentFile = null;
            _isFileChanged = true;
            txtFilePath.Text = "";
            btnDownloadFile.Enabled = false;
        }

        /// <summary>
        /// حدث الضغط على زر الحفظ
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDocument();
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