using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// مستودع بيانات وثائق الموظفين
    /// </summary>
    public class EmployeeDocumentRepository
    {
        private readonly ConnectionManager _connectionManager;
        private readonly string _documentsBasePath;
        
        /// <summary>
        /// إنشاء مستودع بيانات وثائق الموظفين
        /// </summary>
        public EmployeeDocumentRepository()
        {
            _connectionManager = ConnectionManager.Instance;
            
            // تحديد المسار الأساسي لحفظ الوثائق
            _documentsBasePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "EmployeeDocuments");
                
            // التأكد من وجود المجلد
            if (!Directory.Exists(_documentsBasePath))
            {
                Directory.CreateDirectory(_documentsBasePath);
            }
        }
        
        #region Document Types
        
        /// <summary>
        /// الحصول على جميع أنواع الوثائق
        /// </summary>
        public List<DocumentType> GetAllDocumentTypes()
        {
            List<DocumentType> documentTypes = new List<DocumentType>();
            
            string query = @"
                SELECT 
                    ID, Name, Description, IsRequired, IsRenewable, 
                    RequiresVerification, AllowedFileTypes, MaxFileSizeMB, 
                    DefaultValidityDays, DefaultReminderDays
                FROM DocumentTypes
                ORDER BY Name";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            documentTypes.Add(MapDocumentTypeFromReader(reader));
                        }
                    }
                }
            }
            
            return documentTypes;
        }
        
        /// <summary>
        /// الحصول على نوع وثيقة بواسطة المعرف
        /// </summary>
        public DocumentType GetDocumentTypeById(int id)
        {
            string query = @"
                SELECT 
                    ID, Name, Description, IsRequired, IsRenewable, 
                    RequiresVerification, AllowedFileTypes, MaxFileSizeMB, 
                    DefaultValidityDays, DefaultReminderDays
                FROM DocumentTypes
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapDocumentTypeFromReader(reader);
                        }
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// إضافة نوع وثيقة جديد
        /// </summary>
        public int AddDocumentType(DocumentType documentType)
        {
            string query = @"
                INSERT INTO DocumentTypes (
                    Name, Description, IsRequired, IsRenewable, 
                    RequiresVerification, AllowedFileTypes, MaxFileSizeMB, 
                    DefaultValidityDays, DefaultReminderDays
                ) 
                VALUES (
                    @Name, @Description, @IsRequired, @IsRenewable, 
                    @RequiresVerification, @AllowedFileTypes, @MaxFileSizeMB, 
                    @DefaultValidityDays, @DefaultReminderDays
                );
                SELECT SCOPE_IDENTITY();";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddDocumentTypeParameters(command, documentType);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// تحديث نوع وثيقة
        /// </summary>
        public bool UpdateDocumentType(DocumentType documentType)
        {
            string query = @"
                UPDATE DocumentTypes
                SET 
                    Name = @Name,
                    Description = @Description,
                    IsRequired = @IsRequired,
                    IsRenewable = @IsRenewable,
                    RequiresVerification = @RequiresVerification,
                    AllowedFileTypes = @AllowedFileTypes,
                    MaxFileSizeMB = @MaxFileSizeMB,
                    DefaultValidityDays = @DefaultValidityDays,
                    DefaultReminderDays = @DefaultReminderDays
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", documentType.ID);
                    AddDocumentTypeParameters(command, documentType);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// حذف نوع وثيقة
        /// </summary>
        public bool DeleteDocumentType(int id)
        {
            // التحقق من عدم وجود وثائق مرتبطة
            if (GetDocumentCountByType(id) > 0)
            {
                throw new InvalidOperationException("لا يمكن حذف نوع الوثيقة لأنه مرتبط بوثائق موظفين");
            }
            
            string query = "DELETE FROM DocumentTypes WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// إضافة باراميترات نوع الوثيقة إلى الأمر
        /// </summary>
        private void AddDocumentTypeParameters(SqlCommand command, DocumentType documentType)
        {
            command.Parameters.AddWithValue("@Name", documentType.Name);
            command.Parameters.AddWithValue("@Description", (object)documentType.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsRequired", documentType.IsRequired);
            command.Parameters.AddWithValue("@IsRenewable", documentType.IsRenewable);
            command.Parameters.AddWithValue("@RequiresVerification", documentType.RequiresVerification);
            command.Parameters.AddWithValue("@AllowedFileTypes", (object)documentType.AllowedFileTypes ?? DBNull.Value);
            command.Parameters.AddWithValue("@MaxFileSizeMB", documentType.MaxFileSizeMB);
            command.Parameters.AddWithValue("@DefaultValidityDays", documentType.DefaultValidityDays);
            command.Parameters.AddWithValue("@DefaultReminderDays", documentType.DefaultReminderDays);
        }
        
        /// <summary>
        /// الحصول على عدد الوثائق لنوع محدد
        /// </summary>
        private int GetDocumentCountByType(int documentTypeId)
        {
            string query = "SELECT COUNT(*) FROM EmployeeDocuments WHERE DocumentTypeID = @DocumentTypeID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DocumentTypeID", documentTypeId);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            
            return 0;
        }
        
        /// <summary>
        /// تحويل بيانات نوع الوثيقة من القارئ
        /// </summary>
        private DocumentType MapDocumentTypeFromReader(SqlDataReader reader)
        {
            return new DocumentType
            {
                ID = Convert.ToInt32(reader["ID"]),
                Name = reader["Name"].ToString(),
                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                IsRequired = Convert.ToBoolean(reader["IsRequired"]),
                IsRenewable = Convert.ToBoolean(reader["IsRenewable"]),
                RequiresVerification = Convert.ToBoolean(reader["RequiresVerification"]),
                AllowedFileTypes = reader["AllowedFileTypes"] != DBNull.Value ? reader["AllowedFileTypes"].ToString() : null,
                MaxFileSizeMB = Convert.ToDecimal(reader["MaxFileSizeMB"]),
                DefaultValidityDays = Convert.ToInt32(reader["DefaultValidityDays"]),
                DefaultReminderDays = Convert.ToInt32(reader["DefaultReminderDays"])
            };
        }
        
        #endregion
        
        #region Employee Documents
        
        /// <summary>
        /// الحصول على وثائق موظف
        /// </summary>
        public List<EmployeeDocument> GetEmployeeDocuments(int employeeId)
        {
            List<EmployeeDocument> documents = new List<EmployeeDocument>();
            
            string query = @"
                SELECT 
                    d.ID, d.EmployeeID, d.DocumentTypeID, d.Title, d.Description, 
                    d.FilePath, d.FileSize, d.FileType, d.IssueDate, d.ExpiryDate, 
                    d.Notes, d.ReminderDays, d.UploadDate, d.UploadedByUserID, 
                    d.IsVerified, d.VerifiedByUserID, d.VerificationDate,
                    t.Name AS DocumentTypeName,
                    e.FullName AS EmployeeName
                FROM EmployeeDocuments d
                INNER JOIN DocumentTypes t ON d.DocumentTypeID = t.ID
                INNER JOIN Employees e ON d.EmployeeID = e.ID
                WHERE d.EmployeeID = @EmployeeID
                ORDER BY d.UploadDate DESC";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            documents.Add(MapEmployeeDocumentFromReader(reader));
                        }
                    }
                }
            }
            
            return documents;
        }
        
        /// <summary>
        /// الحصول على وثيقة بواسطة المعرف
        /// </summary>
        public EmployeeDocument GetDocumentById(int id)
        {
            string query = @"
                SELECT 
                    d.ID, d.EmployeeID, d.DocumentTypeID, d.Title, d.Description, 
                    d.FilePath, d.FileSize, d.FileType, d.IssueDate, d.ExpiryDate, 
                    d.Notes, d.ReminderDays, d.UploadDate, d.UploadedByUserID, 
                    d.IsVerified, d.VerifiedByUserID, d.VerificationDate,
                    t.Name AS DocumentTypeName,
                    e.FullName AS EmployeeName
                FROM EmployeeDocuments d
                INNER JOIN DocumentTypes t ON d.DocumentTypeID = t.ID
                INNER JOIN Employees e ON d.EmployeeID = e.ID
                WHERE d.ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapEmployeeDocumentFromReader(reader);
                        }
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// إضافة وثيقة موظف جديدة
        /// </summary>
        public int AddEmployeeDocument(EmployeeDocument document, byte[] fileData)
        {
            // التحقق من وجود المسار
            string employeeFolder = Path.Combine(_documentsBasePath, document.EmployeeID.ToString());
            if (!Directory.Exists(employeeFolder))
            {
                Directory.CreateDirectory(employeeFolder);
            }
            
            // إنشاء اسم ملف فريد
            string fileName = $"{Guid.NewGuid()}{document.FileType}";
            string filePath = Path.Combine(employeeFolder, fileName);
            string relativeFilePath = Path.Combine(document.EmployeeID.ToString(), fileName);
            
            // حفظ الملف
            File.WriteAllBytes(filePath, fileData);
            
            // حفظ بيانات الوثيقة في قاعدة البيانات
            string query = @"
                INSERT INTO EmployeeDocuments (
                    EmployeeID, DocumentTypeID, Title, Description, 
                    FilePath, FileSize, FileType, IssueDate, ExpiryDate, 
                    Notes, ReminderDays, UploadDate, UploadedByUserID, 
                    IsVerified, VerifiedByUserID, VerificationDate
                ) 
                VALUES (
                    @EmployeeID, @DocumentTypeID, @Title, @Description, 
                    @FilePath, @FileSize, @FileType, @IssueDate, @ExpiryDate, 
                    @Notes, @ReminderDays, @UploadDate, @UploadedByUserID, 
                    @IsVerified, @VerifiedByUserID, @VerificationDate
                );
                SELECT SCOPE_IDENTITY();";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    document.FilePath = relativeFilePath;
                    document.FileSize = fileData.Length;
                    document.UploadDate = DateTime.Now;
                    
                    AddEmployeeDocumentParameters(command, document);
                    
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                    
                    if (result != null && result != DBNull.Value)
                    {
                        int documentId = Convert.ToInt32(result);
                        
                        // إذا كان نوع الوثيقة يتطلب تحققاً، فإننا ننشئ تذكيراً
                        var documentType = GetDocumentTypeById(document.DocumentTypeID);
                        if (documentType != null && documentType.RequiresVerification)
                        {
                            CreateVerificationNotification(documentId, document.EmployeeID, document.Title);
                        }
                        
                        return documentId;
                    }
                }
            }
            
            // إذا فشلت العملية، نحذف الملف
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            
            return 0;
        }
        
        /// <summary>
        /// تحديث وثيقة موظف
        /// </summary>
        public bool UpdateEmployeeDocument(EmployeeDocument document, byte[] fileData = null)
        {
            // تحديث الملف إذا كان موجوداً
            if (fileData != null && fileData.Length > 0)
            {
                string currentFilePath = GetFullFilePath(document.FilePath);
                
                // التحقق من وجود الملف القديم وحذفه
                if (File.Exists(currentFilePath))
                {
                    File.Delete(currentFilePath);
                }
                
                // إنشاء اسم ملف جديد وحفظه
                string employeeFolder = Path.Combine(_documentsBasePath, document.EmployeeID.ToString());
                if (!Directory.Exists(employeeFolder))
                {
                    Directory.CreateDirectory(employeeFolder);
                }
                
                string fileName = $"{Guid.NewGuid()}{document.FileType}";
                string filePath = Path.Combine(employeeFolder, fileName);
                string relativeFilePath = Path.Combine(document.EmployeeID.ToString(), fileName);
                
                File.WriteAllBytes(filePath, fileData);
                
                document.FilePath = relativeFilePath;
                document.FileSize = fileData.Length;
            }
            
            // تحديث بيانات الوثيقة في قاعدة البيانات
            string query = @"
                UPDATE EmployeeDocuments
                SET 
                    EmployeeID = @EmployeeID,
                    DocumentTypeID = @DocumentTypeID,
                    Title = @Title,
                    Description = @Description,
                    FilePath = @FilePath,
                    FileSize = @FileSize,
                    FileType = @FileType,
                    IssueDate = @IssueDate,
                    ExpiryDate = @ExpiryDate,
                    Notes = @Notes,
                    ReminderDays = @ReminderDays,
                    UploadDate = @UploadDate,
                    UploadedByUserID = @UploadedByUserID,
                    IsVerified = @IsVerified,
                    VerifiedByUserID = @VerifiedByUserID,
                    VerificationDate = @VerificationDate
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", document.ID);
                    AddEmployeeDocumentParameters(command, document);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// حذف وثيقة موظف
        /// </summary>
        public bool DeleteEmployeeDocument(int id)
        {
            // الحصول على مسار الملف قبل الحذف
            EmployeeDocument document = GetDocumentById(id);
            if (document == null)
            {
                return false;
            }
            
            string filePath = GetFullFilePath(document.FilePath);
            
            // حذف الوثيقة من قاعدة البيانات
            string query = "DELETE FROM EmployeeDocuments WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    // إذا تم الحذف بنجاح، نحذف الملف
                    if (rowsAffected > 0 && File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        return true;
                    }
                    
                    return rowsAffected > 0;
                }
            }
        }
        
        /// <summary>
        /// الحصول على بيانات ملف وثيقة
        /// </summary>
        public byte[] GetDocumentFile(int id)
        {
            EmployeeDocument document = GetDocumentById(id);
            if (document == null || string.IsNullOrEmpty(document.FilePath))
            {
                return null;
            }
            
            string filePath = GetFullFilePath(document.FilePath);
            
            if (File.Exists(filePath))
            {
                return File.ReadAllBytes(filePath);
            }
            
            return null;
        }
        
        /// <summary>
        /// التحقق من وثيقة
        /// </summary>
        public bool VerifyDocument(int id, int userId)
        {
            string query = @"
                UPDATE EmployeeDocuments
                SET 
                    IsVerified = 1,
                    VerifiedByUserID = @VerifiedByUserID,
                    VerificationDate = @VerificationDate
                WHERE ID = @ID";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@VerifiedByUserID", userId);
                    command.Parameters.AddWithValue("@VerificationDate", DateTime.Now);
                    
                    connection.Open();
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    if (rowsAffected > 0)
                    {
                        // إلغاء التذكير بالتحقق
                        RemoveVerificationNotification(id);
                        return true;
                    }
                    
                    return false;
                }
            }
        }
        
        /// <summary>
        /// تجديد وثيقة
        /// </summary>
        public bool RenewDocument(int id, DateTime newExpiryDate, int userId, decimal? renewalCost = null, string notes = null)
        {
            // الحصول على الوثيقة
            EmployeeDocument document = GetDocumentById(id);
            if (document == null || !document.ExpiryDate.HasValue)
            {
                return false;
            }
            
            DateTime previousExpiryDate = document.ExpiryDate.Value;
            
            // تحديث تاريخ انتهاء الصلاحية
            string updateQuery = @"
                UPDATE EmployeeDocuments
                SET ExpiryDate = @NewExpiryDate
                WHERE ID = @ID";
            
            // إضافة سجل التجديد
            string historyQuery = @"
                INSERT INTO DocumentRenewalHistory (
                    DocumentID, RenewalDate, PreviousExpiryDate, NewExpiryDate, 
                    RenewalCost, RenewedByUserID, Notes
                ) 
                VALUES (
                    @DocumentID, @RenewalDate, @PreviousExpiryDate, @NewExpiryDate, 
                    @RenewalCost, @RenewedByUserID, @Notes
                )";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                connection.Open();
                
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // تحديث الوثيقة
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@ID", id);
                            updateCommand.Parameters.AddWithValue("@NewExpiryDate", newExpiryDate);
                            
                            updateCommand.ExecuteNonQuery();
                        }
                        
                        // إضافة سجل التجديد
                        using (SqlCommand historyCommand = new SqlCommand(historyQuery, connection, transaction))
                        {
                            historyCommand.Parameters.AddWithValue("@DocumentID", id);
                            historyCommand.Parameters.AddWithValue("@RenewalDate", DateTime.Now);
                            historyCommand.Parameters.AddWithValue("@PreviousExpiryDate", previousExpiryDate);
                            historyCommand.Parameters.AddWithValue("@NewExpiryDate", newExpiryDate);
                            historyCommand.Parameters.AddWithValue("@RenewalCost", (object)renewalCost ?? DBNull.Value);
                            historyCommand.Parameters.AddWithValue("@RenewedByUserID", userId);
                            historyCommand.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
                            
                            historyCommand.ExecuteNonQuery();
                        }
                        
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        
        /// <summary>
        /// الحصول على الوثائق التي قاربت على انتهاء الصلاحية
        /// </summary>
        public List<EmployeeDocument> GetExpiringDocuments()
        {
            List<EmployeeDocument> documents = new List<EmployeeDocument>();
            
            string query = @"
                SELECT 
                    d.ID, d.EmployeeID, d.DocumentTypeID, d.Title, d.Description, 
                    d.FilePath, d.FileSize, d.FileType, d.IssueDate, d.ExpiryDate, 
                    d.Notes, d.ReminderDays, d.UploadDate, d.UploadedByUserID, 
                    d.IsVerified, d.VerifiedByUserID, d.VerificationDate,
                    t.Name AS DocumentTypeName,
                    e.FullName AS EmployeeName
                FROM EmployeeDocuments d
                INNER JOIN DocumentTypes t ON d.DocumentTypeID = t.ID
                INNER JOIN Employees e ON d.EmployeeID = e.ID
                WHERE 
                    d.ExpiryDate IS NOT NULL AND
                    d.ExpiryDate > GETDATE() AND
                    DATEDIFF(DAY, GETDATE(), d.ExpiryDate) <= ISNULL(d.ReminderDays, t.DefaultReminderDays)
                ORDER BY d.ExpiryDate";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            documents.Add(MapEmployeeDocumentFromReader(reader));
                        }
                    }
                }
            }
            
            return documents;
        }
        
        /// <summary>
        /// الحصول على الوثائق منتهية الصلاحية
        /// </summary>
        public List<EmployeeDocument> GetExpiredDocuments()
        {
            List<EmployeeDocument> documents = new List<EmployeeDocument>();
            
            string query = @"
                SELECT 
                    d.ID, d.EmployeeID, d.DocumentTypeID, d.Title, d.Description, 
                    d.FilePath, d.FileSize, d.FileType, d.IssueDate, d.ExpiryDate, 
                    d.Notes, d.ReminderDays, d.UploadDate, d.UploadedByUserID, 
                    d.IsVerified, d.VerifiedByUserID, d.VerificationDate,
                    t.Name AS DocumentTypeName,
                    e.FullName AS EmployeeName
                FROM EmployeeDocuments d
                INNER JOIN DocumentTypes t ON d.DocumentTypeID = t.ID
                INNER JOIN Employees e ON d.EmployeeID = e.ID
                WHERE 
                    d.ExpiryDate IS NOT NULL AND
                    d.ExpiryDate < GETDATE()
                ORDER BY d.ExpiryDate";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            documents.Add(MapEmployeeDocumentFromReader(reader));
                        }
                    }
                }
            }
            
            return documents;
        }
        
        /// <summary>
        /// الحصول على الوثائق التي تحتاج إلى تحقق
        /// </summary>
        public List<EmployeeDocument> GetUnverifiedDocuments()
        {
            List<EmployeeDocument> documents = new List<EmployeeDocument>();
            
            string query = @"
                SELECT 
                    d.ID, d.EmployeeID, d.DocumentTypeID, d.Title, d.Description, 
                    d.FilePath, d.FileSize, d.FileType, d.IssueDate, d.ExpiryDate, 
                    d.Notes, d.ReminderDays, d.UploadDate, d.UploadedByUserID, 
                    d.IsVerified, d.VerifiedByUserID, d.VerificationDate,
                    t.Name AS DocumentTypeName,
                    e.FullName AS EmployeeName
                FROM EmployeeDocuments d
                INNER JOIN DocumentTypes t ON d.DocumentTypeID = t.ID
                INNER JOIN Employees e ON d.EmployeeID = e.ID
                WHERE 
                    t.RequiresVerification = 1 AND
                    d.IsVerified = 0
                ORDER BY d.UploadDate";
            
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            documents.Add(MapEmployeeDocumentFromReader(reader));
                        }
                    }
                }
            }
            
            return documents;
        }
        
        /// <summary>
        /// إضافة باراميترات وثيقة الموظف إلى الأمر
        /// </summary>
        private void AddEmployeeDocumentParameters(SqlCommand command, EmployeeDocument document)
        {
            command.Parameters.AddWithValue("@EmployeeID", document.EmployeeID);
            command.Parameters.AddWithValue("@DocumentTypeID", document.DocumentTypeID);
            command.Parameters.AddWithValue("@Title", document.Title);
            command.Parameters.AddWithValue("@Description", (object)document.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@FilePath", document.FilePath);
            command.Parameters.AddWithValue("@FileSize", document.FileSize);
            command.Parameters.AddWithValue("@FileType", document.FileType);
            command.Parameters.AddWithValue("@IssueDate", document.IssueDate);
            command.Parameters.AddWithValue("@ExpiryDate", (object)document.ExpiryDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@Notes", (object)document.Notes ?? DBNull.Value);
            command.Parameters.AddWithValue("@ReminderDays", (object)document.ReminderDays ?? DBNull.Value);
            command.Parameters.AddWithValue("@UploadDate", document.UploadDate);
            command.Parameters.AddWithValue("@UploadedByUserID", document.UploadedByUserID);
            command.Parameters.AddWithValue("@IsVerified", document.IsVerified);
            command.Parameters.AddWithValue("@VerifiedByUserID", (object)document.VerifiedByUserID ?? DBNull.Value);
            command.Parameters.AddWithValue("@VerificationDate", (object)document.VerificationDate ?? DBNull.Value);
        }
        
        /// <summary>
        /// تحويل بيانات وثيقة الموظف من القارئ
        /// </summary>
        private EmployeeDocument MapEmployeeDocumentFromReader(SqlDataReader reader)
        {
            var document = new EmployeeDocument
            {
                ID = Convert.ToInt32(reader["ID"]),
                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                DocumentTypeID = Convert.ToInt32(reader["DocumentTypeID"]),
                Title = reader["Title"].ToString(),
                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                FilePath = reader["FilePath"].ToString(),
                FileSize = Convert.ToInt64(reader["FileSize"]),
                FileType = reader["FileType"].ToString(),
                IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                ExpiryDate = reader["ExpiryDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ExpiryDate"]) : null,
                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                ReminderDays = reader["ReminderDays"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ReminderDays"]) : null,
                UploadDate = Convert.ToDateTime(reader["UploadDate"]),
                UploadedByUserID = Convert.ToInt32(reader["UploadedByUserID"]),
                IsVerified = Convert.ToBoolean(reader["IsVerified"]),
                VerifiedByUserID = reader["VerifiedByUserID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["VerifiedByUserID"]) : null,
                VerificationDate = reader["VerificationDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["VerificationDate"]) : null
            };
            
            if (reader.HasColumn("DocumentTypeName"))
            {
                document.DocumentType = new DocumentType
                {
                    ID = document.DocumentTypeID,
                    Name = reader["DocumentTypeName"].ToString()
                };
            }
            
            if (reader.HasColumn("EmployeeName"))
            {
                document.Employee = new Employee
                {
                    ID = document.EmployeeID,
                    FullName = reader["EmployeeName"].ToString()
                };
            }
            
            return document;
        }
        
        /// <summary>
        /// الحصول على المسار الكامل للملف
        /// </summary>
        private string GetFullFilePath(string relativePath)
        {
            return Path.Combine(_documentsBasePath, relativePath);
        }
        
        /// <summary>
        /// إنشاء تذكير بالتحقق من وثيقة
        /// </summary>
        private void CreateVerificationNotification(int documentId, int employeeId, string documentTitle)
        {
            try
            {
                string query = @"
                    INSERT INTO Notifications (
                        Title, Message, Type, Date, IsRead,
                        RelatedEntityType, RelatedEntityID, CreatedAt
                    ) 
                    VALUES (
                        @Title, @Message, @Type, @Date, @IsRead,
                        @RelatedEntityType, @RelatedEntityID, @CreatedAt
                    )";
                
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", "وثيقة تحتاج إلى تحقق");
                        command.Parameters.AddWithValue("@Message", $"تم تحميل وثيقة جديدة ({documentTitle}) للموظف وتحتاج إلى التحقق");
                        command.Parameters.AddWithValue("@Type", "VerificationRequired");
                        command.Parameters.AddWithValue("@Date", DateTime.Now);
                        command.Parameters.AddWithValue("@IsRead", false);
                        command.Parameters.AddWithValue("@RelatedEntityType", "EmployeeDocument");
                        command.Parameters.AddWithValue("@RelatedEntityID", documentId);
                        command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل إنشاء تذكير بالتحقق من وثيقة");
            }
        }
        
        /// <summary>
        /// إزالة تذكير التحقق
        /// </summary>
        private void RemoveVerificationNotification(int documentId)
        {
            try
            {
                string query = @"
                    DELETE FROM Notifications 
                    WHERE RelatedEntityType = 'EmployeeDocument' 
                    AND RelatedEntityID = @DocumentID
                    AND Type = 'VerificationRequired'";
                
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DocumentID", documentId);
                        
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل إزالة تذكير التحقق");
            }
        }
        
        #endregion
    }
    
    /// <summary>
    /// امتدادات للقارئ
    /// </summary>
    public static class ReaderExtensions
    {
        /// <summary>
        /// التحقق من وجود عمود في القارئ
        /// </summary>
        public static bool HasColumn(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}