-- إنشاء قاعدة البيانات (هذا يجب أن يتم تنفيذه يدويًا قبل تشغيل البقية)
-- CREATE DATABASE hrsystem;

-- استخدام قاعدة البيانات
-- \c hrsystem

-- جدول بيانات الشركة
CREATE TABLE "Company" (
    "ID" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "LegalName" VARCHAR(150),
    "CommercialRecord" VARCHAR(50),
    "TaxNumber" VARCHAR(50),
    "Address" VARCHAR(255),
    "Phone" VARCHAR(20),
    "Email" VARCHAR(100),
    "Website" VARCHAR(100),
    "Logo" BYTEA,
    "EstablishmentDate" DATE,
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP
);

-- جدول الإدارات والأقسام
CREATE TABLE "Departments" (
    "ID" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Description" VARCHAR(255),
    "ParentID" INTEGER REFERENCES "Departments"("ID"),
    "ManagerPositionID" INTEGER, -- سيتم ربطه لاحقًا بجدول المسميات الوظيفية
    "Location" VARCHAR(100),
    "IsActive" BOOLEAN DEFAULT TRUE,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP
);

-- جدول المسميات الوظيفية
CREATE TABLE "Positions" (
    "ID" SERIAL PRIMARY KEY,
    "Title" VARCHAR(100) NOT NULL,
    "Description" VARCHAR(255),
    "DepartmentID" INTEGER REFERENCES "Departments"("ID"),
    "GradeLevel" INTEGER,
    "MinSalary" DECIMAL(18, 2),
    "MaxSalary" DECIMAL(18, 2),
    "IsManagerPosition" BOOLEAN DEFAULT FALSE,
    "IsActive" BOOLEAN DEFAULT TRUE,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP
);

-- إضافة العلاقة بعد إنشاء جدول المسميات الوظيفية
ALTER TABLE "Departments" ADD CONSTRAINT "FK_Departments_ManagerPosition"
FOREIGN KEY ("ManagerPositionID") REFERENCES "Positions"("ID");

-- جدول الأدوار الوظيفية
CREATE TABLE "Roles" (
    "ID" SERIAL PRIMARY KEY,
    "Name" VARCHAR(50) NOT NULL,
    "Description" VARCHAR(255),
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP
);

-- جدول صلاحيات الأدوار
CREATE TABLE "RolePermissions" (
    "ID" SERIAL PRIMARY KEY,
    "RoleID" INTEGER REFERENCES "Roles"("ID") ON DELETE CASCADE,
    "ModuleName" VARCHAR(50) NOT NULL,
    "CanView" BOOLEAN DEFAULT FALSE,
    "CanAdd" BOOLEAN DEFAULT FALSE,
    "CanEdit" BOOLEAN DEFAULT FALSE,
    "CanDelete" BOOLEAN DEFAULT FALSE,
    "CanPrint" BOOLEAN DEFAULT FALSE,
    "CanExport" BOOLEAN DEFAULT FALSE,
    "CanImport" BOOLEAN DEFAULT FALSE,
    "CanApprove" BOOLEAN DEFAULT FALSE
);

-- جدول المستخدمين
CREATE TABLE "Users" (
    "ID" SERIAL PRIMARY KEY,
    "Username" VARCHAR(50) NOT NULL UNIQUE,
    "PasswordHash" VARCHAR(255) NOT NULL,
    "PasswordSalt" VARCHAR(255) NOT NULL,
    "Email" VARCHAR(100),
    "FullName" VARCHAR(100) NOT NULL,
    "RoleID" INTEGER REFERENCES "Roles"("ID"),
    "EmployeeID" INTEGER, -- سيتم ربطه لاحقًا بجدول الموظفين
    "IsActive" BOOLEAN DEFAULT TRUE,
    "MustChangePassword" BOOLEAN DEFAULT FALSE,
    "LastLogin" TIMESTAMP,
    "LastPasswordChange" TIMESTAMP,
    "FailedLoginAttempts" INTEGER DEFAULT 0,
    "IsLocked" BOOLEAN DEFAULT FALSE,
    "LockoutEnd" TIMESTAMP,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول سجل تسجيل الدخول
CREATE TABLE "LoginHistory" (
    "ID" SERIAL PRIMARY KEY,
    "UserID" INTEGER REFERENCES "Users"("ID"),
    "LoginTime" TIMESTAMP NOT NULL,
    "LogoutTime" TIMESTAMP,
    "IPAddress" VARCHAR(50),
    "MachineName" VARCHAR(100),
    "LoginStatus" VARCHAR(20),
    "UserAgent" VARCHAR(255)
);

-- جدول سجل نشاطات المستخدمين
CREATE TABLE "ActivityLog" (
    "ID" SERIAL PRIMARY KEY,
    "UserID" INTEGER REFERENCES "Users"("ID"),
    "ActivityDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "ActivityType" VARCHAR(50) NOT NULL,
    "ModuleName" VARCHAR(50),
    "Description" TEXT,
    "IPAddress" VARCHAR(50),
    "RecordID" INTEGER,
    "OldValues" TEXT,
    "NewValues" TEXT
);

-- جدول الموظفين
CREATE TABLE "Employees" (
    "ID" SERIAL PRIMARY KEY,
    "EmployeeNumber" VARCHAR(20) NOT NULL UNIQUE,
    "FirstName" VARCHAR(50) NOT NULL,
    "MiddleName" VARCHAR(50),
    "LastName" VARCHAR(50) NOT NULL,
    "FullName" VARCHAR(150), -- سيتم حسابه تلقائيًا في التطبيق
    "Gender" VARCHAR(10),
    "BirthDate" DATE,
    "NationalID" VARCHAR(20),
    "PassportNumber" VARCHAR(20),
    "MaritalStatus" VARCHAR(20),
    "Nationality" VARCHAR(50),
    "Religion" VARCHAR(50),
    "BloodType" VARCHAR(5),
    
    -- معلومات الاتصال
    "Phone" VARCHAR(20),
    "Mobile" VARCHAR(20),
    "Email" VARCHAR(100),
    "Address" VARCHAR(255),
    "EmergencyContact" VARCHAR(100),
    "EmergencyPhone" VARCHAR(20),
    
    -- معلومات العمل
    "DepartmentID" INTEGER REFERENCES "Departments"("ID"),
    "PositionID" INTEGER REFERENCES "Positions"("ID"),
    "DirectManagerID" INTEGER REFERENCES "Employees"("ID"),
    "HireDate" DATE NOT NULL,
    "ProbationEndDate" DATE,
    "EmploymentType" VARCHAR(20), -- دوام كامل، دوام جزئي، متعاقد، إلخ
    "ContractStartDate" DATE,
    "ContractEndDate" DATE,
    "WorkShiftID" INTEGER, -- سيتم ربطه لاحقًا بجدول المناوبات
    
    -- الحالة
    "Status" VARCHAR(20) DEFAULT 'Active', -- نشط، تحت التجربة، منتهي الخدمة، إلخ
    "TerminationDate" DATE,
    "TerminationReason" VARCHAR(255),
    
    -- المعلومات المالية
    "BankName" VARCHAR(100),
    "BankBranch" VARCHAR(100),
    "BankAccountNumber" VARCHAR(50),
    "IBAN" VARCHAR(50),
    
    -- البيانات الإضافية
    "Photo" BYTEA,
    "Notes" TEXT,
    "BiometricID" INTEGER, -- معرف البصمة في نظام ZKTeco
    
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- إضافة العلاقة للمستخدمين بعد إنشاء جدول الموظفين
ALTER TABLE "Users" ADD CONSTRAINT "FK_Users_Employee"
FOREIGN KEY ("EmployeeID") REFERENCES "Employees"("ID");

-- جدول وثائق الموظفين
CREATE TABLE "EmployeeDocuments" (
    "ID" SERIAL PRIMARY KEY,
    "EmployeeID" INTEGER REFERENCES "Employees"("ID") ON DELETE CASCADE,
    "DocumentType" VARCHAR(50) NOT NULL,
    "DocumentTitle" VARCHAR(100) NOT NULL,
    "DocumentNumber" VARCHAR(50),
    "IssueDate" DATE,
    "ExpiryDate" DATE,
    "IssuedBy" VARCHAR(100),
    "DocumentFile" BYTEA,
    "DocumentPath" VARCHAR(255),
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول تاريخ النقل والترقيات
CREATE TABLE "EmployeeTransfers" (
    "ID" SERIAL PRIMARY KEY,
    "EmployeeID" INTEGER REFERENCES "Employees"("ID") ON DELETE CASCADE,
    "TransferType" VARCHAR(20) NOT NULL, -- نقل، ترقية، إلخ
    "FromDepartmentID" INTEGER REFERENCES "Departments"("ID"),
    "ToDepartmentID" INTEGER REFERENCES "Departments"("ID"),
    "FromPositionID" INTEGER REFERENCES "Positions"("ID"),
    "ToPositionID" INTEGER REFERENCES "Positions"("ID"),
    "EffectiveDate" DATE NOT NULL,
    "Reason" VARCHAR(255),
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول فترات العمل
CREATE TABLE "WorkHours" (
    "ID" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Description" VARCHAR(255),
    "StartTime" TIME NOT NULL,
    "EndTime" TIME NOT NULL,
    "FlexibleMinutes" INTEGER DEFAULT 0, -- دقائق السماح للتأخير
    "LateThresholdMinutes" INTEGER DEFAULT 0, -- الحد الأدنى لاحتساب التأخير
    "ShortDayThresholdMinutes" INTEGER DEFAULT 0, -- الحد الأدنى لاحتساب الخروج المبكر
    "OverTimeStartMinutes" INTEGER DEFAULT 0, -- الحد الأدنى لاحتساب العمل الإضافي
    "TotalHours" DECIMAL(18,2), -- سيتم حسابه تلقائيًا في التطبيق
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول ورديات العمل
CREATE TABLE "WorkShifts" (
    "ID" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Description" VARCHAR(255),
    "WorkHoursID" INTEGER REFERENCES "WorkHours"("ID"),
    "SundayEnabled" BOOLEAN DEFAULT FALSE,
    "MondayEnabled" BOOLEAN DEFAULT FALSE,
    "TuesdayEnabled" BOOLEAN DEFAULT FALSE,
    "WednesdayEnabled" BOOLEAN DEFAULT FALSE,
    "ThursdayEnabled" BOOLEAN DEFAULT FALSE,
    "FridayEnabled" BOOLEAN DEFAULT FALSE,
    "SaturdayEnabled" BOOLEAN DEFAULT FALSE,
    "ColorCode" VARCHAR(10),
    "IsActive" BOOLEAN DEFAULT TRUE,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- إضافة العلاقة بعد إنشاء جدول المناوبات
ALTER TABLE "Employees" ADD CONSTRAINT "FK_Employees_WorkShift"
FOREIGN KEY ("WorkShiftID") REFERENCES "WorkShifts"("ID");

-- جدول أجهزة البصمة
CREATE TABLE "BiometricDevices" (
    "ID" SERIAL PRIMARY KEY,
    "DeviceName" VARCHAR(100) NOT NULL,
    "DeviceModel" VARCHAR(50),
    "SerialNumber" VARCHAR(50),
    "IPAddress" VARCHAR(20) NOT NULL,
    "Port" INTEGER DEFAULT 4370,
    "CommunicationKey" VARCHAR(50),
    "Description" VARCHAR(255),
    "Location" VARCHAR(100),
    "IsActive" BOOLEAN DEFAULT TRUE,
    "LastSyncTime" TIMESTAMP,
    "LastSyncStatus" VARCHAR(50),
    "LastSyncErrors" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول سجلات البصمة الخام
CREATE TABLE "RawAttendanceLogs" (
    "ID" SERIAL PRIMARY KEY,
    "DeviceID" INTEGER REFERENCES "BiometricDevices"("ID"),
    "BiometricUserID" INTEGER NOT NULL, -- معرف المستخدم في جهاز البصمة
    "LogDateTime" TIMESTAMP NOT NULL,
    "LogType" INTEGER, -- نوع البصمة (دخول أو خروج)
    "VerifyMode" INTEGER, -- طريقة التحقق (بصمة، بطاقة، كلمة مرور)
    "WorkCode" INTEGER,
    "IsProcessed" BOOLEAN DEFAULT FALSE,
    "IsMatched" BOOLEAN DEFAULT FALSE,
    "EmployeeID" INTEGER REFERENCES "Employees"("ID"),
    "SyncTime" TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- جدول سجلات الحضور والانصراف
CREATE TABLE "AttendanceRecords" (
    "ID" SERIAL PRIMARY KEY,
    "EmployeeID" INTEGER REFERENCES "Employees"("ID") ON DELETE CASCADE,
    "AttendanceDate" DATE NOT NULL,
    "TimeIn" TIMESTAMP,
    "TimeOut" TIMESTAMP,
    "WorkHoursID" INTEGER REFERENCES "WorkHours"("ID"),
    "LateMinutes" INTEGER DEFAULT 0,
    "EarlyDepartureMinutes" INTEGER DEFAULT 0,
    "OvertimeMinutes" INTEGER DEFAULT 0,
    "WorkedMinutes" INTEGER DEFAULT 0,
    "Status" VARCHAR(20), -- حاضر، غائب، متأخر، مغادرة مبكرة، إجازة
    "IsManualEntry" BOOLEAN DEFAULT FALSE,
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID"),
    CONSTRAINT "UC_Employee_Date" UNIQUE ("EmployeeID", "AttendanceDate")
);

-- جدول تصاريح الدوام
CREATE TABLE "AttendancePermissions" (
    "ID" SERIAL PRIMARY KEY,
    "EmployeeID" INTEGER REFERENCES "Employees"("ID") ON DELETE CASCADE,
    "PermissionDate" DATE NOT NULL,
    "PermissionType" VARCHAR(20) NOT NULL, -- تأخير صباحي، خروج مبكر، إلخ
    "StartTime" TIME,
    "EndTime" TIME,
    "TotalMinutes" INTEGER,
    "Reason" VARCHAR(255),
    "Status" VARCHAR(20) DEFAULT 'Pending', -- مقدم، معتمد، مرفوض
    "ApprovedBy" INTEGER REFERENCES "Users"("ID"),
    "ApprovalDate" TIMESTAMP,
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول أنواع الإجازات
CREATE TABLE "LeaveTypes" (
    "ID" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Description" VARCHAR(255),
    "DefaultDays" INTEGER DEFAULT 0,
    "IsPaid" BOOLEAN DEFAULT TRUE,
    "AffectsSalary" BOOLEAN DEFAULT FALSE,
    "RequiresApproval" BOOLEAN DEFAULT TRUE,
    "MaxDaysPerRequest" INTEGER,
    "MinDaysBeforeRequest" INTEGER DEFAULT 0,
    "CarryOverDays" INTEGER DEFAULT 0,
    "CarryOverExpiryMonths" INTEGER DEFAULT 0,
    "Gender" VARCHAR(10), -- للإجازات الخاصة بنوع محدد (مثل إجازة الأمومة)
    "ColorCode" VARCHAR(10),
    "IsActive" BOOLEAN DEFAULT TRUE,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول أرصدة الإجازات
CREATE TABLE "LeaveBalances" (
    "ID" SERIAL PRIMARY KEY,
    "EmployeeID" INTEGER REFERENCES "Employees"("ID") ON DELETE CASCADE,
    "LeaveTypeID" INTEGER REFERENCES "LeaveTypes"("ID") ON DELETE CASCADE,
    "Year" INTEGER NOT NULL,
    "TotalDays" INTEGER DEFAULT 0,
    "UsedDays" INTEGER DEFAULT 0,
    "PendingDays" INTEGER DEFAULT 0,
    "AvailableDays" INTEGER, -- سيتم حسابه تلقائيًا في التطبيق
    "CarriedOverDays" INTEGER DEFAULT 0,
    "CarryOverExpiryDate" DATE,
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID"),
    CONSTRAINT "UC_Employee_LeaveType_Year" UNIQUE ("EmployeeID", "LeaveTypeID", "Year")
);

-- جدول طلبات الإجازات
CREATE TABLE "LeaveRequests" (
    "ID" SERIAL PRIMARY KEY,
    "EmployeeID" INTEGER REFERENCES "Employees"("ID") ON DELETE CASCADE,
    "LeaveTypeID" INTEGER REFERENCES "LeaveTypes"("ID"),
    "StartDate" DATE NOT NULL,
    "EndDate" DATE NOT NULL,
    "ReturnDate" DATE,
    "TotalDays" INTEGER NOT NULL,
    "Reason" VARCHAR(255),
    "Status" VARCHAR(20) DEFAULT 'Pending', -- مقدم، معتمد، مرفوض، ملغي
    "ApprovedBy" INTEGER REFERENCES "Users"("ID"),
    "ApprovalDate" TIMESTAMP,
    "RejectionReason" VARCHAR(255),
    "ContactDuringLeave" VARCHAR(100),
    "AlternateEmployeeID" INTEGER REFERENCES "Employees"("ID"),
    "Notes" TEXT,
    "Attachments" BYTEA,
    "AttachmentsPath" VARCHAR(255),
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول قواعد الخصومات والجزاءات
CREATE TABLE "DeductionRules" (
    "ID" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Description" VARCHAR(255),
    "Type" VARCHAR(20) NOT NULL, -- تأخير، غياب، مخالفة، إلخ
    "DeductionMethod" VARCHAR(20) NOT NULL, -- نسبة من الراتب، مبلغ ثابت، أيام، ساعات
    "DeductionValue" DECIMAL(18, 2) NOT NULL,
    "AppliesTo" VARCHAR(20), -- كل الموظفين، قسم محدد، درجة وظيفية محددة
    "DepartmentID" INTEGER REFERENCES "Departments"("ID"),
    "PositionID" INTEGER REFERENCES "Positions"("ID"),
    "MinViolation" DECIMAL(18, 2), -- الحد الأدنى للمخالفة (مثلاً دقائق التأخير)
    "MaxViolation" DECIMAL(18, 2), -- الحد الأقصى للمخالفة
    "ActivationDate" DATE,
    "IsActive" BOOLEAN DEFAULT TRUE,
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول الخصومات المطبقة
CREATE TABLE "Deductions" (
    "ID" SERIAL PRIMARY KEY,
    "EmployeeID" INTEGER REFERENCES "Employees"("ID") ON DELETE CASCADE,
    "DeductionRuleID" INTEGER REFERENCES "DeductionRules"("ID"),
    "DeductionDate" DATE NOT NULL,
    "ViolationDate" DATE NOT NULL,
    "ViolationType" VARCHAR(20) NOT NULL, -- تأخير، غياب، مخالفة، إلخ
    "ViolationValue" DECIMAL(18, 2), -- قيمة المخالفة (عدد دقائق التأخير مثلاً)
    "DeductionMethod" VARCHAR(20) NOT NULL, -- نسبة من الراتب، مبلغ ثابت، أيام، ساعات
    "DeductionValue" DECIMAL(18, 2) NOT NULL, -- قيمة الخصم
    "Description" VARCHAR(255),
    "Status" VARCHAR(20) DEFAULT 'Pending', -- مقدم، معتمد، مرفوض، ملغي
    "ApprovedBy" INTEGER REFERENCES "Users"("ID"),
    "ApprovalDate" TIMESTAMP,
    "IsPayrollProcessed" BOOLEAN DEFAULT FALSE,
    "PayrollID" INTEGER, -- سيتم ربطه لاحقاً بجدول الرواتب
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول عناصر الراتب
CREATE TABLE "SalaryComponents" (
    "ID" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Description" VARCHAR(255),
    "Type" VARCHAR(20) NOT NULL, -- أساسي، بدل، استقطاع، مكافأة، إلخ
    "IsBasic" BOOLEAN DEFAULT FALSE, -- هل هو راتب أساسي
    "IsVariable" BOOLEAN DEFAULT FALSE, -- هل هو متغير من شهر لآخر
    "IsTaxable" BOOLEAN DEFAULT FALSE, -- هل يخضع للضريبة
    "AffectsNetSalary" BOOLEAN DEFAULT TRUE, -- هل يؤثر في صافي الراتب
    "Position" INTEGER, -- ترتيب ظهور العنصر في كشف الراتب
    "FormulaType" VARCHAR(20), -- ثابت، نسبة من الأساسي، معادلة، إلخ
    "PercentageOf" VARCHAR(50), -- نسبة من (الراتب الأساسي، إجمالي الراتب، عنصر آخر)
    "DefaultAmount" DECIMAL(18, 2),
    "DefaultPercentage" DECIMAL(10, 2),
    "Formula" TEXT, -- معادلة حسابية إن وجدت
    "IsActive" BOOLEAN DEFAULT TRUE,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول الرواتب المخصصة للموظفين
CREATE TABLE "EmployeeSalaries" (
    "ID" SERIAL PRIMARY KEY,
    "EmployeeID" INTEGER REFERENCES "Employees"("ID") ON DELETE CASCADE,
    "EffectiveDate" DATE NOT NULL,
    "EndDate" DATE,
    "IsActive" BOOLEAN DEFAULT TRUE,
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول عناصر الراتب المخصصة لكل موظف
CREATE TABLE "EmployeeSalaryComponents" (
    "ID" SERIAL PRIMARY KEY,
    "EmployeeSalaryID" INTEGER REFERENCES "EmployeeSalaries"("ID") ON DELETE CASCADE,
    "SalaryComponentID" INTEGER REFERENCES "SalaryComponents"("ID"),
    "Amount" DECIMAL(18, 2),
    "Percentage" DECIMAL(10, 2),
    "Formula" TEXT,
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول فترات الرواتب
CREATE TABLE "PayrollPeriods" (
    "ID" SERIAL PRIMARY KEY,
    "PeriodName" VARCHAR(100) NOT NULL,
    "StartDate" DATE NOT NULL,
    "EndDate" DATE NOT NULL,
    "PaymentDate" DATE,
    "Status" VARCHAR(20) DEFAULT 'Open', -- مفتوح، مغلق، مدفوع
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول سجلات الرواتب
CREATE TABLE "PayrollRecords" (
    "ID" SERIAL PRIMARY KEY,
    "EmployeeID" INTEGER REFERENCES "Employees"("ID") ON DELETE CASCADE,
    "PayrollPeriodID" INTEGER REFERENCES "PayrollPeriods"("ID"),
    "BasicSalary" DECIMAL(18, 2) NOT NULL,
    "TotalAllowances" DECIMAL(18, 2) DEFAULT 0,
    "TotalDeductions" DECIMAL(18, 2) DEFAULT 0,
    "GrossSalary" DECIMAL(18, 2) NOT NULL,
    "NetSalary" DECIMAL(18, 2) NOT NULL,
    "WorkDays" INTEGER,
    "AbsentDays" INTEGER DEFAULT 0,
    "LeaveDays" INTEGER DEFAULT 0,
    "OvertimeHours" DECIMAL(10, 2) DEFAULT 0,
    "OvertimeAmount" DECIMAL(18, 2) DEFAULT 0,
    "LoanDeductions" DECIMAL(18, 2) DEFAULT 0,
    "TaxAmount" DECIMAL(18, 2) DEFAULT 0,
    "SocialInsuranceAmount" DECIMAL(18, 2) DEFAULT 0,
    "OtherDeductions" DECIMAL(18, 2) DEFAULT 0,
    "Notes" TEXT,
    "Status" VARCHAR(20) DEFAULT 'Draft', -- مسودة، معتمد، مدفوع
    "ApprovedBy" INTEGER REFERENCES "Users"("ID"),
    "ApprovalDate" TIMESTAMP,
    "PaymentMethod" VARCHAR(50),
    "PaymentReference" VARCHAR(100),
    "PaymentDate" DATE,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID"),
    CONSTRAINT "UC_Employee_PayrollPeriod" UNIQUE ("EmployeeID", "PayrollPeriodID")
);

-- جدول تفاصيل عناصر الراتب
CREATE TABLE "PayrollComponentDetails" (
    "ID" SERIAL PRIMARY KEY,
    "PayrollRecordID" INTEGER REFERENCES "PayrollRecords"("ID") ON DELETE CASCADE,
    "SalaryComponentID" INTEGER REFERENCES "SalaryComponents"("ID"),
    "ComponentName" VARCHAR(100) NOT NULL,
    "Amount" DECIMAL(18, 2) NOT NULL,
    "Type" VARCHAR(20) NOT NULL, -- أساسي، بدل، استقطاع، مكافأة، إلخ
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- جدول القروض
CREATE TABLE "Loans" (
    "ID" SERIAL PRIMARY KEY,
    "EmployeeID" INTEGER REFERENCES "Employees"("ID") ON DELETE CASCADE,
    "LoanType" VARCHAR(50) NOT NULL, -- قرض شخصي، سلفة، إلخ
    "LoanAmount" DECIMAL(18, 2) NOT NULL,
    "InterestRate" DECIMAL(10, 2) DEFAULT 0,
    "InterestAmount" DECIMAL(18, 2) DEFAULT 0,
    "TotalAmount" DECIMAL(18, 2) NOT NULL,
    "NumberOfInstallments" INTEGER NOT NULL,
    "InstallmentAmount" DECIMAL(18, 2) NOT NULL,
    "StartDate" DATE NOT NULL,
    "EndDate" DATE,
    "PaidAmount" DECIMAL(18, 2) DEFAULT 0,
    "RemainingAmount" DECIMAL(18, 2), -- سيتم حسابه تلقائيًا في التطبيق
    "Status" VARCHAR(20) DEFAULT 'Pending', -- مقدم، معتمد، مرفوض، مدفوع، ملغي
    "ApprovedBy" INTEGER REFERENCES "Users"("ID"),
    "ApprovalDate" TIMESTAMP,
    "RejectionReason" VARCHAR(255),
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "CreatedBy" INTEGER REFERENCES "Users"("ID"),
    "UpdatedAt" TIMESTAMP,
    "UpdatedBy" INTEGER REFERENCES "Users"("ID")
);

-- جدول أقساط القروض
CREATE TABLE "LoanInstallments" (
    "ID" SERIAL PRIMARY KEY,
    "LoanID" INTEGER REFERENCES "Loans"("ID") ON DELETE CASCADE,
    "InstallmentNumber" INTEGER NOT NULL,
    "DueDate" DATE NOT NULL,
    "Amount" DECIMAL(18, 2) NOT NULL,
    "PaidAmount" DECIMAL(18, 2) DEFAULT 0,
    "PaymentDate" DATE,
    "Status" VARCHAR(20) DEFAULT 'Pending', -- مستحق، مدفوع، متأخر
    "PayrollRecordID" INTEGER REFERENCES "PayrollRecords"("ID"),
    "Notes" TEXT,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP
);

-- جدول الإعدادات العامة
CREATE TABLE "SystemSettings" (
    "SettingKey" VARCHAR(100) PRIMARY KEY,
    "SettingValue" TEXT,
    "SettingGroup" VARCHAR(50),
    "Description" VARCHAR(255),
    "IsSecure" BOOLEAN DEFAULT FALSE,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP
);

-- إدخال بيانات المستخدم الأساسي (المدير)
INSERT INTO "Roles" ("Name", "Description") 
VALUES ('مدير النظام', 'دور المسؤول مع كامل الصلاحيات');

-- إنشاء صلاحيات المدير
INSERT INTO "RolePermissions" ("RoleID", "ModuleName", "CanView", "CanAdd", "CanEdit", "CanDelete", "CanPrint", "CanExport", "CanImport", "CanApprove")
VALUES 
(1, 'إدارة المستخدمين', true, true, true, true, true, true, true, true),
(1, 'إدارة الصلاحيات', true, true, true, true, true, true, true, true),
(1, 'إدارة الشركة', true, true, true, true, true, true, true, true),
(1, 'إدارة الإدارات', true, true, true, true, true, true, true, true),
(1, 'إدارة الوظائف', true, true, true, true, true, true, true, true),
(1, 'إدارة الموظفين', true, true, true, true, true, true, true, true),
(1, 'إدارة الدوام', true, true, true, true, true, true, true, true),
(1, 'إدارة الإجازات', true, true, true, true, true, true, true, true),
(1, 'إدارة الرواتب', true, true, true, true, true, true, true, true),
(1, 'التقارير', true, true, true, true, true, true, true, true),
(1, 'إعدادات النظام', true, true, true, true, true, true, true, true);

-- إنشاء المستخدم المدير (كلمة المرور: Admin@123)
INSERT INTO "Users" ("Username", "PasswordHash", "PasswordSalt", "Email", "FullName", "RoleID", "IsActive", "LastPasswordChange", "CreatedAt")
VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'HR_SALT', 'admin@example.com', 'مدير النظام', 1, true, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

-- إعدادات النظام الأساسية
INSERT INTO "SystemSettings" ("SettingKey", "SettingValue", "SettingGroup", "Description")
VALUES 
('CompanyName', '', 'Company', 'اسم الشركة'),
('CompanyLogo', '', 'Company', 'شعار الشركة'),
('SystemTitle', 'نظام إدارة الموارد البشرية', 'Application', 'عنوان النظام'),
('SystemLanguage', 'ar-SA', 'Application', 'لغة النظام'),
('EmailHost', '', 'Email', 'خادم البريد الإلكتروني'),
('EmailPort', '587', 'Email', 'منفذ خادم البريد'),
('EmailUsername', '', 'Email', 'اسم مستخدم البريد'),
('EmailPassword', '', 'Email', 'كلمة مرور البريد الإلكتروني'),
('EmailSender', '', 'Email', 'عنوان المرسل للبريد الإلكتروني'),
('WorkingDaysPerWeek', '5', 'Attendance', 'عدد أيام العمل في الأسبوع'),
('WorkingHoursPerDay', '8', 'Attendance', 'عدد ساعات العمل في اليوم'),
('DefaultVacationDays', '21', 'Leaves', 'عدد أيام الإجازة السنوية الافتراضية'),
('DefaultSickLeaveDays', '14', 'Leaves', 'عدد أيام الإجازة المرضية الافتراضية'),
('PayrollStartDay', '1', 'Payroll', 'يوم بداية فترة الرواتب'),
('PayrollEndDay', '30', 'Payroll', 'يوم نهاية فترة الرواتب'),
('TaxRate', '0', 'Payroll', 'نسبة الضريبة'),
('SocialInsuranceRate', '0', 'Payroll', 'نسبة التأمينات الاجتماعية'),
('EnableBiometricIntegration', 'false', 'Biometric', 'تفعيل تكامل أجهزة البصمة'),
('BiometricSyncInterval', '5', 'Biometric', 'الفاصل الزمني لمزامنة البصمة (بالدقائق)'),
('AutoProcessAttendance', 'true', 'Attendance', 'معالجة سجلات الحضور تلقائيًا'),
('EnableAuditing', 'true', 'Security', 'تفعيل تسجيل النشاطات'),
('BackupReminder', '7', 'System', 'تذكير النسخ الاحتياطي (بالأيام)'),
('LastBackupDate', '', 'System', 'تاريخ آخر نسخة احتياطية');