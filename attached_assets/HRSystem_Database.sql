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
    ComponentID INT REFERENCES SalaryComponents(ID) ON DELETE CASCADE,
    EffectiveDate DATE NOT NULL,
    EndDate DATE,
    IsActive BIT DEFAULT 1,
    Amount DECIMAL(18, 2),
    Percentage DECIMAL(10, 2),
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID),
    CONSTRAINT UC_Employee_Component_Date UNIQUE (EmployeeID, ComponentID, EffectiveDate)
)
GO

-- جدول الرواتب الشهرية
CREATE TABLE Payrolls (
    ID INT PRIMARY KEY IDENTITY(1,1),
    PayrollName NVARCHAR(100) NOT NULL,
    PayrollPeriod NVARCHAR(10) NOT NULL, -- الشهر/السنة (MM/YYYY)
    PayrollType NVARCHAR(20) DEFAULT 'Monthly', -- شهري، نصف شهري، أسبوعي
    PayrollMonth INT NOT NULL,
    PayrollYear INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    PaymentDate DATE,
    TotalEmployees INT DEFAULT 0,
    TotalBasicSalary DECIMAL(18, 2) DEFAULT 0,
    TotalAllowances DECIMAL(18, 2) DEFAULT 0,
    TotalDeductions DECIMAL(18, 2) DEFAULT 0,
    TotalOvertimeAmount DECIMAL(18, 2) DEFAULT 0,
    TotalPayrollAmount DECIMAL(18, 2) DEFAULT 0,
    Status NVARCHAR(20) DEFAULT 'Draft', -- مسودة، قيد المراجعة، معتمد، مدفوع، ملغي
    ProcessedBy INT REFERENCES Users(ID),
    ProcessedAt DATETIME,
    ApprovedBy INT REFERENCES Users(ID),
    ApprovalDate DATETIME,
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT REFERENCES Users(ID),
    UpdatedAt DATETIME,
    UpdatedBy INT REFERENCES Users(ID),
    CONSTRAINT UC_Payroll_Period UNIQUE (PayrollMonth, PayrollYear, PayrollType)
)
GO

-- إضافة الرابط إلى جدول الخصومات
ALTER TABLE Deductions ADD CONSTRAINT FK_Deductions_Payroll
FOREIGN KEY (PayrollID) REFERENCES Payrolls(ID)
GO

-- جدول تفاصيل الرواتب
CREATE TABLE PayrollDetails (
    ID INT PRIMARY KEY IDENTITY(1,1),
    PayrollID INT REFERENCES Payrolls(ID) ON DELETE CASCADE,
    EmployeeID INT REFERENCES Employees(ID),
    WorkingDays INT DEFAULT 0,
    PresentDays INT DEFAULT 0,
    AbsentDays INT DEFAULT 0,
    LeaveDays INT DEFAULT 0,
    LateMinutes INT DEFAULT 0,
    OvertimeHours DECIMAL(18, 2) DEFAULT 0,
    BaseSalary DECIMAL(18, 2) DEFAULT 0,
    TotalAllowances DECIMAL(18, 2) DEFAULT 0,
    TotalDeductions DECIMAL(18, 2) DEFAULT 0,
    OvertimeAmount DECIMAL(18, 2) DEFAULT 0,
    NetSalary DECIMAL(18, 2) DEFAULT 0,
    Status NVARCHAR(20) DEFAULT 'Calculated', -- محسوب، مدفوع، ملغي
    PaymentMethod NVARCHAR(20),
    PaymentReference NVARCHAR(50),
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    CONSTRAINT UC_Payroll_Employee UNIQUE (PayrollID, EmployeeID)
)
GO

-- جدول تفاصيل عناصر الراتب لكل موظف في الراتب الشهري
CREATE TABLE PayrollComponentDetails (
    ID INT PRIMARY KEY IDENTITY(1,1),
    PayrollDetailID INT REFERENCES PayrollDetails(ID) ON DELETE CASCADE,
    ComponentID INT REFERENCES SalaryComponents(ID),
    ComponentName NVARCHAR(100) NOT NULL,
    ComponentType NVARCHAR(20) NOT NULL, -- أساسي، بدل، استقطاع، مكافأة، إلخ
    Amount DECIMAL(18, 2) NOT NULL,
    Formula NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE()
)
GO

-- جدول الإشعارات
CREATE TABLE Notifications (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Type NVARCHAR(50) NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    Message NVARCHAR(MAX),
    EmployeeID INT REFERENCES Employees(ID),
    IsRead BIT DEFAULT 0,
    ReadAt DATETIME,
    IsSystemNotification BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    ExpiryDate DATETIME
)
GO

-- جدول إعدادات النظام
CREATE TABLE SystemSettings (
    ID INT PRIMARY KEY IDENTITY(1,1),
    SettingKey NVARCHAR(100) NOT NULL UNIQUE,
    SettingValue NVARCHAR(MAX),
    SettingGroup NVARCHAR(50),
    Description NVARCHAR(255),
    DataType NVARCHAR(20),
    IsVisible BIT DEFAULT 1,
    IsEditable BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME
)
GO

-- إعدادات افتراضية للنظام
INSERT INTO SystemSettings (SettingKey, SettingValue, SettingGroup, Description, DataType)
VALUES
    ('CompanyName', N'شركتي للبرمجيات', 'Company', N'اسم الشركة', 'string'),
    ('SystemName', N'نظام إدارة الموارد البشرية', 'General', N'اسم النظام', 'string'),
    ('SystemVersion', '1.0.0', 'General', N'إصدار النظام', 'string'),
    ('DefaultWorkingDays', 'Sun,Mon,Tue,Wed,Thu', 'Attendance', N'أيام العمل الافتراضية', 'array'),
    ('AttendanceGracePeriod', '15', 'Attendance', N'فترة السماح للتأخير (بالدقائق)', 'integer'),
    ('AutoCalculateAttendance', 'true', 'Attendance', N'حساب الحضور تلقائياً', 'boolean'),
    ('DefaultTheme', 'light', 'Appearance', N'سمة النظام الافتراضية', 'string'),
    ('IsFirstRun', 'true', 'System', N'هل هذا أول تشغيل للنظام', 'boolean'),
    ('DatabaseVersion', '1.0', 'System', N'إصدار قاعدة البيانات', 'string'),
    ('OvertimeRate', '1.5', 'Payroll', N'معدل العمل الإضافي', 'decimal'),
    ('WorkStartTime', '08:00', 'Attendance', N'وقت بدء العمل الافتراضي', 'time'),
    ('WorkEndTime', '17:00', 'Attendance', N'وقت نهاية العمل الافتراضي', 'time')
GO

-- إنشاء دور المدير الافتراضي
INSERT INTO Roles (Name, Description)
VALUES ('Administrator', N'مدير النظام مع كامل الصلاحيات')
GO

-- إنشاء إجراءات مخزنة

-- إجراء للتحقق من اسم المستخدم وكلمة المرور
CREATE PROCEDURE ValidateUserLogin
    @Username NVARCHAR(50),
    @Password NVARCHAR(255)
AS
BEGIN
    DECLARE @UserID INT
    DECLARE @PasswordHash NVARCHAR(255)
    DECLARE @PasswordSalt NVARCHAR(255)
    DECLARE @IsActive BIT
    DECLARE @IsLocked BIT
    DECLARE @FailedAttempts INT
    DECLARE @Success BIT = 0
    
    -- الحصول على بيانات المستخدم
    SELECT 
        @UserID = ID,
        @PasswordHash = PasswordHash,
        @PasswordSalt = PasswordSalt,
        @IsActive = IsActive,
        @IsLocked = IsLocked,
        @FailedAttempts = FailedLoginAttempts
    FROM Users
    WHERE Username = @Username
    
    -- التحقق من وجود المستخدم
    IF @UserID IS NULL
    BEGIN
        SELECT 0 AS Success, N'اسم المستخدم غير موجود' AS Message
        RETURN
    END
    
    -- التحقق من حالة المستخدم
    IF @IsActive = 0
    BEGIN
        SELECT 0 AS Success, N'الحساب غير نشط' AS Message
        RETURN
    END
    
    IF @IsLocked = 1
    BEGIN
        SELECT 0 AS Success, N'الحساب مقفل. يرجى التواصل مع الإدارة' AS Message
        RETURN
    END
    
    -- TODO: هنا يجب التحقق من كلمة المرور باستخدام دالة تشفير مناسبة
    -- لأغراض التبسيط نفترض أن عملية التحقق تمت بنجاح
    SET @Success = 1
    
    IF @Success = 1
    BEGIN
        -- تحديث بيانات الدخول
        UPDATE Users
        SET 
            LastLogin = GETDATE(),
            FailedLoginAttempts = 0
        WHERE ID = @UserID
        
        -- إضافة سجل دخول
        INSERT INTO LoginHistory (UserID, LoginTime, IPAddress, LoginStatus)
        VALUES (@UserID, GETDATE(), 'Unknown', 'Success')
        
        SELECT 1 AS Success, N'تم تسجيل الدخول بنجاح' AS Message, @UserID AS UserID
    END
    ELSE
    BEGIN
        -- زيادة عدد محاولات الدخول الفاشلة
        UPDATE Users
        SET 
            FailedLoginAttempts = @FailedAttempts + 1,
            IsLocked = CASE WHEN @FailedAttempts + 1 >= 5 THEN 1 ELSE 0 END,
            LockoutEnd = CASE WHEN @FailedAttempts + 1 >= 5 THEN DATEADD(HOUR, 1, GETDATE()) ELSE NULL END
        WHERE ID = @UserID
        
        -- إضافة سجل دخول فاشل
        INSERT INTO LoginHistory (UserID, LoginTime, IPAddress, LoginStatus)
        VALUES (@UserID, GETDATE(), 'Unknown', 'Failed')
        
        SELECT 0 AS Success, N'كلمة المرور غير صحيحة' AS Message
    END
END
GO

-- إجراء لإضافة سجل نشاط
CREATE PROCEDURE AddActivityLog
    @UserID INT,
    @ActivityType NVARCHAR(50),
    @ModuleName NVARCHAR(50),
    @Description NVARCHAR(MAX),
    @IPAddress NVARCHAR(50),
    @RecordID INT = NULL,
    @OldValues NVARCHAR(MAX) = NULL,
    @NewValues NVARCHAR(MAX) = NULL
AS
BEGIN
    INSERT INTO ActivityLog (
        UserID, ActivityType, ModuleName, Description,
        IPAddress, RecordID, OldValues, NewValues
    )
    VALUES (
        @UserID, @ActivityType, @ModuleName, @Description,
        @IPAddress, @RecordID, @OldValues, @NewValues
    )
END
GO

-- إجراء لمعالجة سجلات البصمة الخام
CREATE PROCEDURE ProcessRawAttendanceLogs
AS
BEGIN
    DECLARE @Today DATE = CAST(GETDATE() AS DATE)
    
    -- تحديث ارتباط سجلات البصمة الخام بالموظفين
    UPDATE R
    SET 
        R.EmployeeID = E.ID,
        R.IsMatched = 1
    FROM RawAttendanceLogs R
    INNER JOIN Employees E ON R.BiometricUserID = E.BiometricID
    WHERE R.IsMatched = 0
    
    -- معالجة سجلات اليوم الغير معالجة
    DECLARE @EmployeeID INT
    DECLARE @LogDate DATE
    DECLARE @FirstLog DATETIME
    DECLARE @LastLog DATETIME
    DECLARE @WorkShiftID INT
    DECLARE @WorkHoursID INT
    DECLARE @StartTime TIME
    DECLARE @EndTime TIME
    DECLARE @LateMinutes INT
    DECLARE @EarlyDepartureMinutes INT
    DECLARE @OvertimeMinutes INT
    DECLARE @WorkedMinutes INT
    DECLARE @Status NVARCHAR(20)
    
    -- الحصول على الموظفين الذين لديهم سجلات بصمة لليوم
    DECLARE EmployeeCursor CURSOR FOR
    SELECT DISTINCT E.ID, E.WorkShiftID
    FROM Employees E
    INNER JOIN RawAttendanceLogs R ON E.ID = R.EmployeeID
    WHERE CAST(R.LogDateTime AS DATE) = @Today
      AND R.IsProcessed = 0
    
    OPEN EmployeeCursor
    FETCH NEXT FROM EmployeeCursor INTO @EmployeeID, @WorkShiftID
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- الحصول على معلومات المناوبة وفترة العمل
        SELECT @WorkHoursID = W.WorkHoursID
        FROM WorkShifts W
        WHERE W.ID = @WorkShiftID
        
        SELECT 
            @StartTime = WH.StartTime,
            @EndTime = WH.EndTime
        FROM WorkHours WH
        WHERE WH.ID = @WorkHoursID
        
        -- الحصول على أول وآخر بصمة للموظف في اليوم الحالي
        SELECT 
            @FirstLog = MIN(LogDateTime),
            @LastLog = MAX(LogDateTime)
        FROM RawAttendanceLogs
        WHERE EmployeeID = @EmployeeID
          AND CAST(LogDateTime AS DATE) = @Today
        
        -- حساب الوقت المتأخر والمغادرة المبكرة
        SET @LateMinutes = DATEDIFF(MINUTE, 
            CAST(CAST(@Today AS DATETIME) + CAST(@StartTime AS DATETIME) AS DATETIME),
            @FirstLog)
        
        IF @LateMinutes < 0 SET @LateMinutes = 0
        
        SET @EarlyDepartureMinutes = DATEDIFF(MINUTE,
            @LastLog,
            CAST(CAST(@Today AS DATETIME) + CAST(@EndTime AS DATETIME) AS DATETIME))
        
        IF @EarlyDepartureMinutes < 0 SET @EarlyDepartureMinutes = 0
        
        -- حساب وقت العمل الإضافي
        IF @LastLog > CAST(CAST(@Today AS DATETIME) + CAST(@EndTime AS DATETIME) AS DATETIME)
            SET @OvertimeMinutes = DATEDIFF(MINUTE,
                CAST(CAST(@Today AS DATETIME) + CAST(@EndTime AS DATETIME) AS DATETIME),
                @LastLog)
        ELSE
            SET @OvertimeMinutes = 0
        
        -- حساب إجمالي دقائق العمل
        SET @WorkedMinutes = DATEDIFF(MINUTE, @FirstLog, @LastLog)
        
        -- تحديد حالة الحضور
        IF @LateMinutes > 0 AND @EarlyDepartureMinutes > 0
            SET @Status = N'متأخر ومغادر مبكرًا'
        ELSE IF @LateMinutes > 0
            SET @Status = N'متأخر'
        ELSE IF @EarlyDepartureMinutes > 0
            SET @Status = N'مغادر مبكرًا'
        ELSE
            SET @Status = N'حاضر'
        
        -- إنشاء أو تحديث سجل الحضور
        IF EXISTS (SELECT 1 FROM AttendanceRecords 
                 WHERE EmployeeID = @EmployeeID AND AttendanceDate = @Today)
        BEGIN
            UPDATE AttendanceRecords
            SET 
                TimeIn = @FirstLog,
                TimeOut = @LastLog,
                WorkHoursID = @WorkHoursID,
                LateMinutes = @LateMinutes,
                EarlyDepartureMinutes = @EarlyDepartureMinutes,
                OvertimeMinutes = @OvertimeMinutes,
                WorkedMinutes = @WorkedMinutes,
                Status = @Status,
                IsManualEntry = 0,
                UpdatedAt = GETDATE()
            WHERE EmployeeID = @EmployeeID 
              AND AttendanceDate = @Today
        END
        ELSE
        BEGIN
            INSERT INTO AttendanceRecords (
                EmployeeID, AttendanceDate, TimeIn, TimeOut,
                WorkHoursID, LateMinutes, EarlyDepartureMinutes,
                OvertimeMinutes, WorkedMinutes, Status
            )
            VALUES (
                @EmployeeID, @Today, @FirstLog, @LastLog,
                @WorkHoursID, @LateMinutes, @EarlyDepartureMinutes,
                @OvertimeMinutes, @WorkedMinutes, @Status
            )
        END
        
        -- تحديث حالة المعالجة للسجلات الخام
        UPDATE RawAttendanceLogs
        SET IsProcessed = 1
        WHERE EmployeeID = @EmployeeID
          AND CAST(LogDateTime AS DATE) = @Today
        
        FETCH NEXT FROM EmployeeCursor INTO @EmployeeID, @WorkShiftID
    END
    
    CLOSE EmployeeCursor
    DEALLOCATE EmployeeCursor
    
    -- إنشاء سجلات غياب للموظفين الذين لم يسجلوا حضورًا اليوم
    INSERT INTO AttendanceRecords (
        EmployeeID, AttendanceDate, Status
    )
    SELECT 
        E.ID, @Today, N'غائب'
    FROM Employees E
    LEFT JOIN AttendanceRecords AR 
        ON E.ID = AR.EmployeeID AND AR.AttendanceDate = @Today
    LEFT JOIN LeaveRequests LR 
        ON E.ID = LR.EmployeeID 
        AND @Today BETWEEN LR.StartDate AND LR.EndDate
        AND LR.Status = 'Approved'
    WHERE AR.ID IS NULL -- لا يوجد سجل حضور
      AND LR.ID IS NULL -- ليس في إجازة معتمدة
      AND E.Status = 'Active' -- الموظف نشط
      AND (
        (DATEPART(DW, @Today) = 1 AND EXISTS(SELECT 1 FROM WorkShifts WHERE ID = E.WorkShiftID AND SundayEnabled = 1)) OR
        (DATEPART(DW, @Today) = 2 AND EXISTS(SELECT 1 FROM WorkShifts WHERE ID = E.WorkShiftID AND MondayEnabled = 1)) OR
        (DATEPART(DW, @Today) = 3 AND EXISTS(SELECT 1 FROM WorkShifts WHERE ID = E.WorkShiftID AND TuesdayEnabled = 1)) OR
        (DATEPART(DW, @Today) = 4 AND EXISTS(SELECT 1 FROM WorkShifts WHERE ID = E.WorkShiftID AND WednesdayEnabled = 1)) OR
        (DATEPART(DW, @Today) = 5 AND EXISTS(SELECT 1 FROM WorkShifts WHERE ID = E.WorkShiftID AND ThursdayEnabled = 1)) OR
        (DATEPART(DW, @Today) = 6 AND EXISTS(SELECT 1 FROM WorkShifts WHERE ID = E.WorkShiftID AND FridayEnabled = 1)) OR
        (DATEPART(DW, @Today) = 7 AND EXISTS(SELECT 1 FROM WorkShifts WHERE ID = E.WorkShiftID AND SaturdayEnabled = 1))
      )
END
GO

-- إجراء لحساب رصيد الإجازات
CREATE PROCEDURE CalculateLeaveBalances
    @Year INT = NULL
AS
BEGIN
    IF @Year IS NULL
        SET @Year = YEAR(GETDATE())
    
    -- التأكد من وجود أرصدة إجازات للعام المطلوب
    INSERT INTO LeaveBalances (EmployeeID, LeaveTypeID, Year, TotalDays)
    SELECT 
        E.ID AS EmployeeID, 
        LT.ID AS LeaveTypeID, 
        @Year AS Year,
        LT.DefaultDays AS TotalDays
    FROM 
        Employees E
    CROSS JOIN 
        LeaveTypes LT
    LEFT JOIN 
        LeaveBalances LB ON E.ID = LB.EmployeeID AND LT.ID = LB.LeaveTypeID AND LB.Year = @Year
    WHERE
        LB.ID IS NULL
        AND E.Status = 'Active'
        AND (LT.Gender IS NULL OR LT.Gender = E.Gender)
    
    -- تحديث الأيام المستخدمة
    UPDATE LB
    SET 
        UsedDays = ISNULL(
            (SELECT SUM(TotalDays)
             FROM LeaveRequests LR
             WHERE LR.EmployeeID = LB.EmployeeID
               AND LR.LeaveTypeID = LB.LeaveTypeID
               AND YEAR(LR.StartDate) = LB.Year
               AND LR.Status = 'Approved'), 0),
        PendingDays = ISNULL(
            (SELECT SUM(TotalDays)
             FROM LeaveRequests LR
             WHERE LR.EmployeeID = LB.EmployeeID
               AND LR.LeaveTypeID = LB.LeaveTypeID
               AND YEAR(LR.StartDate) = LB.Year
               AND LR.Status = 'Pending'), 0),
        UpdatedAt = GETDATE()
    FROM LeaveBalances LB
    WHERE LB.Year = @Year
END
GO

-- إجراء لإنشاء كشف الرواتب الشهري
CREATE PROCEDURE CreateMonthlyPayroll
    @Month INT,
    @Year INT,
    @CreatedBy INT
AS
BEGIN
    DECLARE @StartDate DATE
    DECLARE @EndDate DATE
    DECLARE @PayrollID INT
    DECLARE @PayrollPeriod NVARCHAR(10)
    
    -- تحديد فترة الرواتب
    SET @StartDate = DATEFROMPARTS(@Year, @Month, 1)
    SET @EndDate = EOMONTH(@StartDate)
    SET @PayrollPeriod = FORMAT(@Month, '00') + '/' + CAST(@Year AS NVARCHAR(4))
    
    -- التحقق من عدم وجود كشف رواتب لنفس الفترة
    IF EXISTS (SELECT 1 FROM Payrolls WHERE PayrollMonth = @Month AND PayrollYear = @Year AND PayrollType = 'Monthly')
    BEGIN
        RAISERROR(N'يوجد بالفعل كشف رواتب لهذا الشهر', 16, 1)
        RETURN
    END
    
    -- إنشاء كشف الرواتب
    INSERT INTO Payrolls (
        PayrollName, PayrollPeriod, PayrollType, PayrollMonth, PayrollYear,
        StartDate, EndDate, Status, CreatedBy
    )
    VALUES (
        N'كشف رواتب ' + FORMAT(@Month, '00') + '/' + CAST(@Year AS NVARCHAR(4)),
        @PayrollPeriod, 'Monthly', @Month, @Year,
        @StartDate, @EndDate, 'Draft', @CreatedBy
    )
    
    SET @PayrollID = SCOPE_IDENTITY()
    
    -- إضافة الموظفين النشطين إلى كشف الرواتب
    INSERT INTO PayrollDetails (
        PayrollID, EmployeeID, WorkingDays, PresentDays, AbsentDays, LeaveDays,
        LateMinutes, OvertimeHours, BaseSalary, TotalAllowances, TotalDeductions,
        OvertimeAmount, NetSalary
    )
    SELECT
        @PayrollID AS PayrollID,
        E.ID AS EmployeeID,
        -- حساب أيام العمل الفعلية في الشهر
        (SELECT COUNT(*)
         FROM GENERATE_SERIES(@StartDate, @EndDate, 1) AS Dates
         WHERE (
            (DATEPART(DW, Dates) = 1 AND WS.SundayEnabled = 1) OR
            (DATEPART(DW, Dates) = 2 AND WS.MondayEnabled = 1) OR
            (DATEPART(DW, Dates) = 3 AND WS.TuesdayEnabled = 1) OR
            (DATEPART(DW, Dates) = 4 AND WS.WednesdayEnabled = 1) OR
            (DATEPART(DW, Dates) = 5 AND WS.ThursdayEnabled = 1) OR
            (DATEPART(DW, Dates) = 6 AND WS.FridayEnabled = 1) OR
            (DATEPART(DW, Dates) = 7 AND WS.SaturdayEnabled = 1)
         )) AS WorkingDays,
        -- أيام الحضور
        ISNULL((SELECT COUNT(*)
                FROM AttendanceRecords AR
                WHERE AR.EmployeeID = E.ID
                  AND AR.AttendanceDate BETWEEN @StartDate AND @EndDate
                  AND AR.Status IN (N'حاضر', N'متأخر', N'مغادر مبكرًا', N'متأخر ومغادر مبكرًا')), 0) AS PresentDays,
        -- أيام الغياب
        ISNULL((SELECT COUNT(*)
                FROM AttendanceRecords AR
                WHERE AR.EmployeeID = E.ID
                  AND AR.AttendanceDate BETWEEN @StartDate AND @EndDate
                  AND AR.Status = N'غائب'), 0) AS AbsentDays,
        -- أيام الإجازات
        ISNULL((SELECT COUNT(*)
                FROM AttendanceRecords AR
                WHERE AR.EmployeeID = E.ID
                  AND AR.AttendanceDate BETWEEN @StartDate AND @EndDate
                  AND AR.Status = N'إجازة'), 0) AS LeaveDays,
        -- دقائق التأخير
        ISNULL((SELECT SUM(AR.LateMinutes)
                FROM AttendanceRecords AR
                WHERE AR.EmployeeID = E.ID
                  AND AR.AttendanceDate BETWEEN @StartDate AND @EndDate), 0) AS LateMinutes,
        -- ساعات العمل الإضافي
        ISNULL((SELECT SUM(AR.OvertimeMinutes) / 60.0
                FROM AttendanceRecords AR
                WHERE AR.EmployeeID = E.ID
                  AND AR.AttendanceDate BETWEEN @StartDate AND @EndDate), 0) AS OvertimeHours,
        -- الراتب الأساسي
        ISNULL((SELECT ES.Amount
                FROM EmployeeSalaries ES
                INNER JOIN SalaryComponents SC ON ES.ComponentID = SC.ID
                WHERE ES.EmployeeID = E.ID
                  AND SC.IsBasic = 1
                  AND ES.IsActive = 1
                  AND ES.EffectiveDate <= @EndDate
                  AND (ES.EndDate IS NULL OR ES.EndDate >= @StartDate)), 0) AS BaseSalary,
        -- إجمالي البدلات
        ISNULL((SELECT SUM(ES.Amount)
                FROM EmployeeSalaries ES
                INNER JOIN SalaryComponents SC ON ES.ComponentID = SC.ID
                WHERE ES.EmployeeID = E.ID
                  AND SC.Type = N'بدل'
                  AND ES.IsActive = 1
                  AND ES.EffectiveDate <= @EndDate
                  AND (ES.EndDate IS NULL OR ES.EndDate >= @StartDate)), 0) AS TotalAllowances,
        -- إجمالي الخصومات
        ISNULL((SELECT SUM(D.DeductionValue)
                FROM Deductions D
                WHERE D.EmployeeID = E.ID
                  AND D.ViolationDate BETWEEN @StartDate AND @EndDate
                  AND D.Status = 'Approved'
                  AND D.IsPayrollProcessed = 0), 0) AS TotalDeductions,
        -- مبلغ العمل الإضافي
        0 AS OvertimeAmount, -- سيتم حسابه لاحقًا
        -- صافي الراتب
        0 AS NetSalary -- سيتم حسابه لاحقًا
    FROM Employees E
    LEFT JOIN WorkShifts WS ON E.WorkShiftID = WS.ID
    WHERE E.Status = 'Active'
    
    -- حساب مبلغ العمل الإضافي وصافي الراتب
    UPDATE PD
    SET 
        OvertimeAmount = PD.OvertimeHours * (PD.BaseSalary / (22 * 8)) * 1.5, -- معدل العمل الإضافي
        NetSalary = PD.BaseSalary + PD.TotalAllowances - PD.TotalDeductions + (PD.OvertimeHours * (PD.BaseSalary / (22 * 8)) * 1.5)
    FROM PayrollDetails PD
    WHERE PD.PayrollID = @PayrollID
    
    -- تحديث تفاصيل عناصر الراتب
    INSERT INTO PayrollComponentDetails (
        PayrollDetailID, ComponentID, ComponentName, ComponentType, Amount
    )
    SELECT
        PD.ID AS PayrollDetailID,
        ES.ComponentID,
        SC.Name AS ComponentName,
        SC.Type AS ComponentType,
        ES.Amount
    FROM PayrollDetails PD
    INNER JOIN EmployeeSalaries ES ON PD.EmployeeID = ES.EmployeeID
    INNER JOIN SalaryComponents SC ON ES.ComponentID = SC.ID
    WHERE PD.PayrollID = @PayrollID
      AND ES.IsActive = 1
      AND ES.EffectiveDate <= @EndDate
      AND (ES.EndDate IS NULL OR ES.EndDate >= @StartDate)
    
    -- تحديث إجماليات كشف الرواتب
    UPDATE P
    SET 
        TotalEmployees = (SELECT COUNT(*) FROM PayrollDetails WHERE PayrollID = @PayrollID),
        TotalBasicSalary = (SELECT SUM(BaseSalary) FROM PayrollDetails WHERE PayrollID = @PayrollID),
        TotalAllowances = (SELECT SUM(TotalAllowances) FROM PayrollDetails WHERE PayrollID = @PayrollID),
        TotalDeductions = (SELECT SUM(TotalDeductions) FROM PayrollDetails WHERE PayrollID = @PayrollID),
        TotalOvertimeAmount = (SELECT SUM(OvertimeAmount) FROM PayrollDetails WHERE PayrollID = @PayrollID),
        TotalPayrollAmount = (SELECT SUM(NetSalary) FROM PayrollDetails WHERE PayrollID = @PayrollID),
        ProcessedBy = @CreatedBy,
        ProcessedAt = GETDATE()
    FROM Payrolls P
    WHERE P.ID = @PayrollID
    
    -- تحديث حالة الخصومات
    UPDATE D
    SET 
        IsPayrollProcessed = 1,
        PayrollID = @PayrollID
    FROM Deductions D
    INNER JOIN PayrollDetails PD ON D.EmployeeID = PD.EmployeeID
    WHERE PD.PayrollID = @PayrollID
      AND D.ViolationDate BETWEEN @StartDate AND @EndDate
      AND D.Status = 'Approved'
      AND D.IsPayrollProcessed = 0
    
    -- إرجاع معرف كشف الرواتب
    SELECT @PayrollID AS PayrollID
END
GO

-- دالة مساعدة لتوليد سلسلة من التواريخ
CREATE FUNCTION GENERATE_SERIES
(
    @startdate DATE,
    @enddate DATE,
    @increment INT
)
RETURNS TABLE
AS
RETURN 
(
    WITH SERIES(DATE) AS
    (
        SELECT @startdate
        UNION ALL
        SELECT DATEADD(DAY, @increment, DATE)
        FROM SERIES
        WHERE DATEADD(DAY, @increment, DATE) <= @enddate
    )
    SELECT DATE FROM SERIES
    OPTION (MAXRECURSION 1000)
)
GO