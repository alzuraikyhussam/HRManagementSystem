-- إنشاء قاعدة البيانات
CREATE DATABASE HRSystem
GO

USE HRSystem
GO

-- جدول بيانات الشركة
CREATE TABLE Company (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    LegalName NVARCHAR(150),
    CommercialRecord NVARCHAR(50),
    TaxNumber NVARCHAR(50),
    Address NVARCHAR(255),
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    Website NVARCHAR(100),
    Logo VARBINARY(MAX),
    EstablishmentDate DATE,
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME
)
GO

-- جدول الإدارات والأقسام
CREATE TABLE Departments (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    ParentID INT REFERENCES Departments(ID), -- للأقسام الفرعية
    ManagerPositionID INT, -- سيتم ربطه لاحقًا بجدول المسميات الوظيفية
    Location NVARCHAR(100),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME
)
GO

-- جدول المسميات الوظيفية
CREATE TABLE Positions (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    DepartmentID INT REFERENCES Departments(ID),
    GradeLevel INT,
    MinSalary DECIMAL(18, 2),
    MaxSalary DECIMAL(18, 2),
    IsManagerPosition BIT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME
)
GO

-- إضافة العلاقة بعد إنشاء جدول المسميات الوظيفية
ALTER TABLE Departments ADD CONSTRAINT FK_Departments_ManagerPosition
FOREIGN KEY (ManagerPositionID) REFERENCES Positions(ID)
GO

-- جدول الأدوار الوظيفية
CREATE TABLE Roles (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME
)
GO

-- جدول صلاحيات الأدوار
CREATE TABLE RolePermissions (
    ID INT PRIMARY KEY IDENTITY(1,1),
    RoleID INT REFERENCES Roles(ID) ON DELETE CASCADE,
    ModuleName NVARCHAR(50) NOT NULL,
    CanView BIT DEFAULT 0,
    CanAdd BIT DEFAULT 0,
    CanEdit BIT DEFAULT 0,
    CanDelete BIT DEFAULT 0,
    CanPrint BIT DEFAULT 0,
    CanExport BIT DEFAULT 0,
    CanImport BIT DEFAULT 0,
    CanApprove BIT DEFAULT 0
)
GO

-- جدول المستخدمين
CREATE TABLE Users (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    PasswordSalt NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100),
    FullName NVARCHAR(100) NOT NULL,
    RoleID INT REFERENCES Roles(ID),
    EmployeeID INT, -- سيتم ربطه لاحقًا بجدول الموظفين
    IsActive BIT DEFAULT 1,
    MustChangePassword BIT DEFAULT 0,
    LastLogin DATETIME,
    LastPasswordChange DATETIME,
    FailedLoginAttempts INT DEFAULT 0,
    IsLocked BIT DEFAULT 0,
    LockoutEnd DATETIME,
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول سجل تسجيل الدخول
CREATE TABLE LoginHistory (
    ID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT REFERENCES Users(ID),
    LoginTime DATETIME NOT NULL,
    LogoutTime DATETIME,
    IPAddress NVARCHAR(50),
    MachineName NVARCHAR(100),
    LoginStatus NVARCHAR(20),
    UserAgent NVARCHAR(255)
)
GO

-- جدول سجل نشاطات المستخدمين
CREATE TABLE ActivityLog (
    ID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT REFERENCES Users(ID),
    ActivityDate DATETIME DEFAULT GETDATE(),
    ActivityType NVARCHAR(50) NOT NULL,
    ModuleName NVARCHAR(50),
    Description NVARCHAR(MAX),
    IPAddress NVARCHAR(50),
    RecordID INT,
    OldValues NVARCHAR(MAX),
    NewValues NVARCHAR(MAX)
)
GO

-- جدول الموظفين
CREATE TABLE Employees (
    ID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeNumber NVARCHAR(20) NOT NULL UNIQUE,
    FirstName NVARCHAR(50) NOT NULL,
    MiddleName NVARCHAR(50),
    LastName NVARCHAR(50) NOT NULL,
    FullName AS (FirstName + ' ' + ISNULL(MiddleName + ' ', '') + LastName) PERSISTED,
    Gender NVARCHAR(10),
    BirthDate DATE,
    NationalID NVARCHAR(20),
    PassportNumber NVARCHAR(20),
    MaritalStatus NVARCHAR(20),
    Nationality NVARCHAR(50),
    Religion NVARCHAR(50),
    BloodType NVARCHAR(5),
    
    -- معلومات الاتصال
    Phone NVARCHAR(20),
    Mobile NVARCHAR(20),
    Email NVARCHAR(100),
    Address NVARCHAR(255),
    EmergencyContact NVARCHAR(100),
    EmergencyPhone NVARCHAR(20),
    
    -- معلومات العمل
    DepartmentID INT REFERENCES Departments(ID),
    PositionID INT REFERENCES Positions(ID),
    DirectManagerID INT REFERENCES Employees(ID),
    HireDate DATE NOT NULL,
    ProbationEndDate DATE,
    EmploymentType NVARCHAR(20), -- دوام كامل، دوام جزئي، متعاقد، إلخ
    ContractStartDate DATE,
    ContractEndDate DATE,
    WorkShiftID INT, -- سيتم ربطه لاحقًا بجدول المناوبات
    
    -- الحالة
    Status NVARCHAR(20) DEFAULT 'Active', -- نشط، تحت التجربة، منتهي الخدمة، إلخ
    TerminationDate DATE,
    TerminationReason NVARCHAR(255),
    
    -- المعلومات المالية
    BankName NVARCHAR(100),
    BankBranch NVARCHAR(100),
    BankAccountNumber NVARCHAR(50),
    IBAN NVARCHAR(50),
    
    -- البيانات الإضافية
    Photo VARBINARY(MAX),
    Notes NVARCHAR(MAX),
    BiometricID INT, -- معرف البصمة في نظام ZKTeco
    
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- إضافة العلاقة للمستخدمين بعد إنشاء جدول الموظفين
ALTER TABLE Users ADD CONSTRAINT FK_Users_Employee
FOREIGN KEY (EmployeeID) REFERENCES Employees(ID)
GO

-- جدول وثائق الموظفين
CREATE TABLE EmployeeDocuments (
    ID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT REFERENCES Employees(ID) ON DELETE CASCADE,
    DocumentType NVARCHAR(50) NOT NULL,
    DocumentTitle NVARCHAR(100) NOT NULL,
    DocumentNumber NVARCHAR(50),
    IssueDate DATE,
    ExpiryDate DATE,
    IssuedBy NVARCHAR(100),
    DocumentFile VARBINARY(MAX),
    DocumentPath NVARCHAR(255),
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول تاريخ النقل والترقيات
CREATE TABLE EmployeeTransfers (
    ID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT REFERENCES Employees(ID) ON DELETE CASCADE,
    TransferType NVARCHAR(20) NOT NULL, -- نقل، ترقية، إلخ
    FromDepartmentID INT REFERENCES Departments(ID),
    ToDepartmentID INT REFERENCES Departments(ID),
    FromPositionID INT REFERENCES Positions(ID),
    ToPositionID INT REFERENCES Positions(ID),
    EffectiveDate DATE NOT NULL,
    Reason NVARCHAR(255),
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول فترات العمل
CREATE TABLE WorkHours (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    FlexibleMinutes INT DEFAULT 0, -- دقائق السماح للتأخير
    LateThresholdMinutes INT DEFAULT 0, -- الحد الأدنى لاحتساب التأخير
    ShortDayThresholdMinutes INT DEFAULT 0, -- الحد الأدنى لاحتساب الخروج المبكر
    OverTimeStartMinutes INT DEFAULT 0, -- الحد الأدنى لاحتساب العمل الإضافي
    TotalHours AS CAST(DATEDIFF(MINUTE, StartTime, EndTime) AS DECIMAL(18,2)) / 60,
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول ورديات العمل
CREATE TABLE WorkShifts (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    WorkHoursID INT REFERENCES WorkHours(ID),
    SundayEnabled BIT DEFAULT 0,
    MondayEnabled BIT DEFAULT 0,
    TuesdayEnabled BIT DEFAULT 0,
    WednesdayEnabled BIT DEFAULT 0,
    ThursdayEnabled BIT DEFAULT 0,
    FridayEnabled BIT DEFAULT 0,
    SaturdayEnabled BIT DEFAULT 0,
    ColorCode NVARCHAR(10),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- إضافة العلاقة بعد إنشاء جدول المناوبات
ALTER TABLE Employees ADD CONSTRAINT FK_Employees_WorkShift
FOREIGN KEY (WorkShiftID) REFERENCES WorkShifts(ID)
GO

-- جدول أجهزة البصمة
CREATE TABLE BiometricDevices (
    ID INT PRIMARY KEY IDENTITY(1,1),
    DeviceName NVARCHAR(100) NOT NULL,
    DeviceModel NVARCHAR(50),
    SerialNumber NVARCHAR(50),
    IPAddress NVARCHAR(20) NOT NULL,
    Port INT DEFAULT 4370,
    CommunicationKey NVARCHAR(50),
    Description NVARCHAR(255),
    Location NVARCHAR(100),
    IsActive BIT DEFAULT 1,
    LastSyncTime DATETIME,
    LastSyncStatus NVARCHAR(50),
    LastSyncErrors NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول سجلات البصمة الخام
CREATE TABLE RawAttendanceLogs (
    ID INT PRIMARY KEY IDENTITY(1,1),
    DeviceID INT REFERENCES BiometricDevices(ID),
    BiometricUserID INT NOT NULL, -- معرف المستخدم في جهاز البصمة
    LogDateTime DATETIME NOT NULL,
    LogType INT, -- نوع البصمة (دخول أو خروج)
    VerifyMode INT, -- طريقة التحقق (بصمة، بطاقة، كلمة مرور)
    WorkCode INT,
    IsProcessed BIT DEFAULT 0,
    IsMatched BIT DEFAULT 0,
    EmployeeID INT REFERENCES Employees(ID),
    SyncTime DATETIME DEFAULT GETDATE()
)
GO

-- جدول سجلات الحضور والانصراف
CREATE TABLE AttendanceRecords (
    ID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT REFERENCES Employees(ID) ON DELETE CASCADE,
    AttendanceDate DATE NOT NULL,
    TimeIn DATETIME,
    TimeOut DATETIME,
    WorkHoursID INT REFERENCES WorkHours(ID),
    LateMinutes INT DEFAULT 0,
    EarlyDepartureMinutes INT DEFAULT 0,
    OvertimeMinutes INT DEFAULT 0,
    WorkedMinutes INT DEFAULT 0,
    Status NVARCHAR(20), -- حاضر، غائب، متأخر، مغادرة مبكرة، إجازة
    IsManualEntry BIT DEFAULT 0,
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID),
    CONSTRAINT UC_Employee_Date UNIQUE (EmployeeID, AttendanceDate)
)
GO

-- جدول تصاريح الدوام
CREATE TABLE AttendancePermissions (
    ID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT REFERENCES Employees(ID) ON DELETE CASCADE,
    PermissionDate DATE NOT NULL,
    PermissionType NVARCHAR(20) NOT NULL, -- تأخير صباحي، خروج مبكر، إلخ
    StartTime TIME,
    EndTime TIME,
    TotalMinutes INT,
    Reason NVARCHAR(255),
    Status NVARCHAR(20) DEFAULT 'Pending', -- مقدم، معتمد، مرفوض
    ApprovedBy INT REFERENCES Users(ID),
    ApprovalDate DATETIME,
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول أنواع الإجازات
CREATE TABLE LeaveTypes (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    DefaultDays INT DEFAULT 0,
    IsPaid BIT DEFAULT 1,
    AffectsSalary BIT DEFAULT 0,
    RequiresApproval BIT DEFAULT 1,
    MaxDaysPerRequest INT,
    MinDaysBeforeRequest INT DEFAULT 0,
    CarryOverDays INT DEFAULT 0,
    CarryOverExpiryMonths INT DEFAULT 0,
    Gender NVARCHAR(10), -- للإجازات الخاصة بنوع محدد (مثل إجازة الأمومة)
    ColorCode NVARCHAR(10),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول أرصدة الإجازات
CREATE TABLE LeaveBalances (
    ID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT REFERENCES Employees(ID) ON DELETE CASCADE,
    LeaveTypeID INT REFERENCES LeaveTypes(ID) ON DELETE CASCADE,
    Year INT NOT NULL,
    TotalDays INT DEFAULT 0,
    UsedDays INT DEFAULT 0,
    PendingDays INT DEFAULT 0,
    AvailableDays AS (TotalDays - UsedDays - PendingDays) PERSISTED,
    CarriedOverDays INT DEFAULT 0,
    CarryOverExpiryDate DATE,
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID),
    CONSTRAINT UC_Employee_LeaveType_Year UNIQUE (EmployeeID, LeaveTypeID, Year)
)
GO

-- جدول طلبات الإجازات
CREATE TABLE LeaveRequests (
    ID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT REFERENCES Employees(ID) ON DELETE CASCADE,
    LeaveTypeID INT REFERENCES LeaveTypes(ID),
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    ReturnDate DATE,
    TotalDays INT NOT NULL,
    Reason NVARCHAR(255),
    Status NVARCHAR(20) DEFAULT 'Pending', -- مقدم، معتمد، مرفوض، ملغي
    ApprovedBy INT REFERENCES Users(ID),
    ApprovalDate DATETIME,
    RejectionReason NVARCHAR(255),
    ContactDuringLeave NVARCHAR(100),
    AlternateEmployeeID INT REFERENCES Employees(ID),
    Notes NVARCHAR(MAX),
    Attachments VARBINARY(MAX),
    AttachmentsPath NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول قواعد الخصومات والجزاءات
CREATE TABLE DeductionRules (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Type NVARCHAR(20) NOT NULL, -- تأخير، غياب، مخالفة، إلخ
    DeductionMethod NVARCHAR(20) NOT NULL, -- نسبة من الراتب، مبلغ ثابت، أيام، ساعات
    DeductionValue DECIMAL(18, 2) NOT NULL,
    AppliesTo NVARCHAR(20), -- كل الموظفين، قسم محدد، درجة وظيفية محددة
    DepartmentID INT REFERENCES Departments(ID),
    PositionID INT REFERENCES Positions(ID),
    MinViolation DECIMAL(18, 2), -- الحد الأدنى للمخالفة (مثلاً دقائق التأخير)
    MaxViolation DECIMAL(18, 2), -- الحد الأقصى للمخالفة
    ActivationDate DATE,
    IsActive BIT DEFAULT 1,
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول الخصومات المطبقة
CREATE TABLE Deductions (
    ID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT REFERENCES Employees(ID) ON DELETE CASCADE,
    DeductionRuleID INT REFERENCES DeductionRules(ID),
    DeductionDate DATE NOT NULL,
    ViolationDate DATE NOT NULL,
    ViolationType NVARCHAR(20) NOT NULL, -- تأخير، غياب، مخالفة، إلخ
    ViolationValue DECIMAL(18, 2), -- قيمة المخالفة (عدد دقائق التأخير مثلاً)
    DeductionMethod NVARCHAR(20) NOT NULL, -- نسبة من الراتب، مبلغ ثابت، أيام، ساعات
    DeductionValue DECIMAL(18, 2) NOT NULL, -- قيمة الخصم
    Description NVARCHAR(255),
    Status NVARCHAR(20) DEFAULT 'Pending', -- مقدم، معتمد، مرفوض، ملغي
    ApprovedBy INT REFERENCES Users(ID),
    ApprovalDate DATETIME,
    IsPayrollProcessed BIT DEFAULT 0,
    PayrollID INT, -- سيتم ربطه لاحقاً بجدول الرواتب
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول عناصر الراتب
CREATE TABLE SalaryComponents (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Type NVARCHAR(20) NOT NULL, -- أساسي، بدل، استقطاع، مكافأة، إلخ
    IsBasic BIT DEFAULT 0, -- هل هو راتب أساسي
    IsVariable BIT DEFAULT 0, -- هل هو متغير من شهر لآخر
    IsTaxable BIT DEFAULT 0, -- هل يخضع للضريبة
    AffectsNetSalary BIT DEFAULT 1, -- هل يؤثر في صافي الراتب
    Position INT, -- ترتيب ظهور العنصر في كشف الراتب
    FormulaType NVARCHAR(20), -- ثابت، نسبة من الأساسي، معادلة، إلخ
    PercentageOf NVARCHAR(50), -- نسبة من (الراتب الأساسي، إجمالي الراتب، عنصر آخر)
    DefaultAmount DECIMAL(18, 2),
    DefaultPercentage DECIMAL(10, 2),
    Formula NVARCHAR(MAX), -- معادلة حسابية إن وجدت
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول الرواتب المخصصة للموظفين
CREATE TABLE EmployeeSalaries (
    ID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT REFERENCES Employees(ID) ON DELETE CASCADE,
    EffectiveDate DATE NOT NULL,
    EndDate DATE,
    IsActive BIT DEFAULT 1,
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول عناصر الراتب المخصصة لكل موظف
CREATE TABLE EmployeeSalaryComponents (
    ID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeSalaryID INT REFERENCES EmployeeSalaries(ID) ON DELETE CASCADE,
    SalaryComponentID INT REFERENCES SalaryComponents(ID),
    Amount DECIMAL(18, 2),
    Percentage DECIMAL(10, 2),
    Formula NVARCHAR(MAX),
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول فترات الرواتب
CREATE TABLE PayrollPeriods (
    ID INT PRIMARY KEY IDENTITY(1,1),
    PeriodName NVARCHAR(100) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    PaymentDate DATE,
    Status NVARCHAR(20) DEFAULT 'Open', -- مفتوح، مغلق، مدفوع
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول سجلات الرواتب
CREATE TABLE PayrollRecords (
    ID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT REFERENCES Employees(ID) ON DELETE CASCADE,
    PayrollPeriodID INT REFERENCES PayrollPeriods(ID),
    BasicSalary DECIMAL(18, 2) NOT NULL,
    TotalAllowances DECIMAL(18, 2) DEFAULT 0,
    TotalDeductions DECIMAL(18, 2) DEFAULT 0,
    GrossSalary DECIMAL(18, 2) NOT NULL,
    NetSalary DECIMAL(18, 2) NOT NULL,
    WorkDays INT,
    AbsentDays INT DEFAULT 0,
    LeaveDays INT DEFAULT 0,
    OvertimeHours DECIMAL(10, 2) DEFAULT 0,
    OvertimeAmount DECIMAL(18, 2) DEFAULT 0,
    LoanDeductions DECIMAL(18, 2) DEFAULT 0,
    TaxAmount DECIMAL(18, 2) DEFAULT 0,
    SocialInsuranceAmount DECIMAL(18, 2) DEFAULT 0,
    OtherDeductions DECIMAL(18, 2) DEFAULT 0,
    Notes NVARCHAR(MAX),
    Status NVARCHAR(20) DEFAULT 'Draft', -- مسودة، معتمد، مدفوع
    ApprovedBy INT REFERENCES Users(ID),
    ApprovalDate DATETIME,
    PaymentMethod NVARCHAR(50),
    PaymentReference NVARCHAR(100),
    PaymentDate DATE,
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID),
    CONSTRAINT UC_Employee_PayrollPeriod UNIQUE (EmployeeID, PayrollPeriodID)
)
GO

-- جدول تفاصيل عناصر الراتب
CREATE TABLE PayrollComponentDetails (
    ID INT PRIMARY KEY IDENTITY(1,1),
    PayrollRecordID INT REFERENCES PayrollRecords(ID) ON DELETE CASCADE,
    SalaryComponentID INT REFERENCES SalaryComponents(ID),
    ComponentName NVARCHAR(100) NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    Type NVARCHAR(20) NOT NULL, -- أساسي، بدل، استقطاع، مكافأة، إلخ
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE()
)
GO

-- جدول القروض
CREATE TABLE Loans (
    ID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT REFERENCES Employees(ID) ON DELETE CASCADE,
    LoanType NVARCHAR(50) NOT NULL, -- قرض شخصي، سلفة، إلخ
    LoanAmount DECIMAL(18, 2) NOT NULL,
    InterestRate DECIMAL(10, 2) DEFAULT 0,
    InterestAmount DECIMAL(18, 2) DEFAULT 0,
    TotalAmount DECIMAL(18, 2) NOT NULL,
    NumberOfInstallments INT NOT NULL,
    InstallmentAmount DECIMAL(18, 2) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE,
    PaidAmount DECIMAL(18, 2) DEFAULT 0,
    RemainingAmount AS (TotalAmount - PaidAmount) PERSISTED,
    Status NVARCHAR(20) DEFAULT 'Pending', -- مقدم، معتمد، مرفوض، مدفوع، ملغي
    ApprovedBy INT REFERENCES Users(ID),
    ApprovalDate DATETIME,
    RejectionReason NVARCHAR(255),
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID)
)
GO

-- جدول أقساط القروض
CREATE TABLE LoanInstallments (
    ID INT PRIMARY KEY IDENTITY(1,1),
    LoanID INT REFERENCES Loans(ID) ON DELETE CASCADE,
    InstallmentNumber INT NOT NULL,
    DueDate DATE NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    PaidAmount DECIMAL(18, 2) DEFAULT 0,
    PaymentDate DATE,
    Status NVARCHAR(20) DEFAULT 'Pending', -- مستحق، مدفوع، متأخر
    PayrollRecordID INT REFERENCES PayrollRecords(ID),
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME
)
GO

-- جدول الإعدادات العامة
CREATE TABLE SystemSettings (
    SettingKey NVARCHAR(100) PRIMARY KEY,
    SettingValue NVARCHAR(MAX),
    SettingGroup NVARCHAR(50),
    Description NVARCHAR(255),
    IsSecure BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME
)
GO

-- إدخال بيانات المستخدم الأساسي (المدير)
INSERT INTO Roles (Name, Description) 
VALUES (N'مدير النظام', N'دور المسؤول مع كامل الصلاحيات')
GO

-- إنشاء صلاحيات المدير
INSERT INTO RolePermissions (RoleID, ModuleName, CanView, CanAdd, CanEdit, CanDelete, CanPrint, CanExport, CanImport, CanApprove)
VALUES 
(1, N'إدارة المستخدمين', 1, 1, 1, 1, 1, 1, 1, 1),
(1, N'إدارة الصلاحيات', 1, 1, 1, 1, 1, 1, 1, 1),
(1, N'إدارة الشركة', 1, 1, 1, 1, 1, 1, 1, 1),
(1, N'إدارة الإدارات', 1, 1, 1, 1, 1, 1, 1, 1),
(1, N'إدارة الوظائف', 1, 1, 1, 1, 1, 1, 1, 1),
(1, N'إدارة الموظفين', 1, 1, 1, 1, 1, 1, 1, 1),
(1, N'إدارة الدوام', 1, 1, 1, 1, 1, 1, 1, 1),
(1, N'إدارة الإجازات', 1, 1, 1, 1, 1, 1, 1, 1),
(1, N'إدارة الرواتب', 1, 1, 1, 1, 1, 1, 1, 1),
(1, N'التقارير', 1, 1, 1, 1, 1, 1, 1, 1),
(1, N'إعدادات النظام', 1, 1, 1, 1, 1, 1, 1, 1)
GO

-- إنشاء المستخدم المدير (كلمة المرور: Admin@123)
INSERT INTO Users (Username, PasswordHash, PasswordSalt, Email, FullName, RoleID, IsActive, LastPasswordChange, CreatedAt)
VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'HR_SALT', 'admin@example.com', N'مدير النظام', 1, 1, GETDATE(), GETDATE())
GO

-- إعدادات النظام الأساسية
INSERT INTO SystemSettings (SettingKey, SettingValue, SettingGroup, Description)
VALUES 
('CompanyName', '', 'Company', N'اسم الشركة'),
('CompanyLogo', '', 'Company', N'شعار الشركة'),
('SystemTitle', N'نظام إدارة الموارد البشرية', 'Application', N'عنوان النظام'),
('SystemLanguage', 'ar-SA', 'Application', N'لغة النظام'),
('EmailHost', '', 'Email', N'خادم البريد الإلكتروني'),
('EmailPort', '587', 'Email', N'منفذ خادم البريد'),
('EmailUsername', '', 'Email', N'اسم مستخدم البريد'),
('EmailPassword', '', 'Email', N'كلمة مرور البريد الإلكتروني'),
('EmailSender', '', 'Email', N'عنوان المرسل للبريد الإلكتروني'),
('WorkingDaysPerWeek', '5', 'Attendance', N'عدد أيام العمل في الأسبوع'),
('WorkingHoursPerDay', '8', 'Attendance', N'عدد ساعات العمل في اليوم'),
('DefaultVacationDays', '21', 'Leaves', N'عدد أيام الإجازة السنوية الافتراضية'),
('DefaultSickLeaveDays', '14', 'Leaves', N'عدد أيام الإجازة المرضية الافتراضية'),
('PayrollStartDay', '1', 'Payroll', N'يوم بداية فترة الرواتب'),
('PayrollEndDay', '30', 'Payroll', N'يوم نهاية فترة الرواتب'),
('TaxRate', '0', 'Payroll', N'نسبة الضريبة'),
('SocialInsuranceRate', '0', 'Payroll', N'نسبة التأمينات الاجتماعية'),
('EnableBiometricIntegration', 'false', 'Biometric', N'تفعيل تكامل أجهزة البصمة'),
('BiometricSyncInterval', '5', 'Biometric', N'الفاصل الزمني لمزامنة البصمة (بالدقائق)'),
('AutoProcessAttendance', 'true', 'Attendance', N'معالجة سجلات الحضور تلقائيًا'),
('EnableAuditing', 'true', 'Security', N'تفعيل تسجيل النشاطات'),
('BackupReminder', '7', 'System', N'تذكير النسخ الاحتياطي (بالأيام)'),
('LastBackupDate', '', 'System', N'تاريخ آخر نسخة احتياطية')
GO