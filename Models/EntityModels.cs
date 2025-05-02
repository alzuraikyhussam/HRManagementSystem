using System;
using System.Collections.Generic;

namespace HR.Models
{
    #region Company

    /// <summary>
    /// Company information model
    /// </summary>
    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string CommercialRecord { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public byte[] Logo { get; set; }
        public DateTime? EstablishmentDate { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    #endregion

    #region Departments

    /// <summary>
    /// Department model
    /// </summary>
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentID { get; set; }
        public int? ManagerPositionID { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public Department ParentDepartment { get; set; }
        public Position ManagerPosition { get; set; }
        public List<Position> Positions { get; set; }
        public List<Department> ChildDepartments { get; set; }
        public List<Employee> Employees { get; set; }
    }

    #endregion

    #region Positions

    /// <summary>
    /// Position model
    /// </summary>
    public class Position
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? DepartmentID { get; set; }
        public int? GradeLevel { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public bool IsManagerPosition { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public Department Department { get; set; }
        public List<Employee> Employees { get; set; }
    }

    #endregion

    #region Roles and Permissions

    /// <summary>
    /// Role model
    /// </summary>
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public List<RolePermission> Permissions { get; set; }
        public List<User> Users { get; set; }
    }

    /// <summary>
    /// Role permission model
    /// </summary>
    public class RolePermission
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string ModuleName { get; set; }
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }
        public bool CanExport { get; set; }
        public bool CanImport { get; set; }
        public bool CanApprove { get; set; }

        // Navigation properties
        public Role Role { get; set; }
    }

    #endregion

    #region Users

    /// <summary>
    /// User model
    /// </summary>
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int? RoleID { get; set; }
        public int? EmployeeID { get; set; }
        public bool IsActive { get; set; }
        public bool MustChangePassword { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public int FailedLoginAttempts { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public Role Role { get; set; }
        public Employee Employee { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
    }

    /// <summary>
    /// Login history model
    /// </summary>
    public class LoginHistory
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public string IPAddress { get; set; }
        public string MachineName { get; set; }
        public string LoginStatus { get; set; }
        public string UserAgent { get; set; }

        // Navigation properties
        public User User { get; set; }
    }

    /// <summary>
    /// Activity log model
    /// </summary>
    public class ActivityLog
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityType { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public string IPAddress { get; set; }
        public int? RecordID { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }

        // Navigation properties
        public User User { get; set; }
    }

    #endregion

    #region Employees

    /// <summary>
    /// Employee model
    /// </summary>
    public class Employee
    {
        public int ID { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; } // Computed in database
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string NationalID { get; set; }
        public string PassportNumber { get; set; }
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string BloodType { get; set; }

        // Contact information
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyPhone { get; set; }

        // Work information
        public int? DepartmentID { get; set; }
        public int? PositionID { get; set; }
        public int? DirectManagerID { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? ProbationEndDate { get; set; }
        public string EmploymentType { get; set; } // Full time, part time, contract, etc.
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public int? WorkShiftID { get; set; }

        // Status
        public string Status { get; set; } // Active, probation, terminated, etc.
        public DateTime? TerminationDate { get; set; }
        public string TerminationReason { get; set; }

        // Financial information
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string BankAccountNumber { get; set; }
        public string IBAN { get; set; }

        // Additional information
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public int? BiometricID { get; set; } // ZKTeco biometric ID

        // Audit fields
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public Department Department { get; set; }
        public Position Position { get; set; }
        public Employee DirectManager { get; set; }
        public WorkShift WorkShift { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
        public List<Employee> Subordinates { get; set; }
        public List<EmployeeDocument> Documents { get; set; }
        public List<AttendanceRecord> AttendanceRecords { get; set; }
        public List<LeaveRequest> LeaveRequests { get; set; }
        public List<LeaveBalance> LeaveBalances { get; set; }
        public User User { get; set; }
    }

    /// <summary>
    /// Employee document model
    /// </summary>
    public class EmployeeDocument
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string IssuedBy { get; set; }
        public byte[] DocumentFile { get; set; }
        public string DocumentPath { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
    }

    /// <summary>
    /// Employee transfer model
    /// </summary>
    public class EmployeeTransfer
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string TransferType { get; set; } // Transfer, promotion, etc.
        public int? FromDepartmentID { get; set; }
        public int? ToDepartmentID { get; set; }
        public int? FromPositionID { get; set; }
        public int? ToPositionID { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public Department FromDepartment { get; set; }
        public Department ToDepartment { get; set; }
        public Position FromPosition { get; set; }
        public Position ToPosition { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
    }

    #endregion

    #region Attendance

    /// <summary>
    /// Work hours model
    /// </summary>
    public class WorkHours
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int FlexibleMinutes { get; set; } // Minutes allowed for being late
        public int LateThresholdMinutes { get; set; } // Minimum minutes to consider late
        public int ShortDayThresholdMinutes { get; set; } // Minimum minutes to consider early departure
        public int OverTimeStartMinutes { get; set; } // Minimum minutes to consider overtime
        public decimal TotalHours { get; set; } // Computed in database
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public User Creator { get; set; }
        public User Updater { get; set; }
        public List<WorkShift> WorkShifts { get; set; }
    }

    /// <summary>
    /// Work shift model
    /// </summary>
    public class WorkShift
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? WorkHoursID { get; set; }
        public bool SundayEnabled { get; set; }
        public bool MondayEnabled { get; set; }
        public bool TuesdayEnabled { get; set; }
        public bool WednesdayEnabled { get; set; }
        public bool ThursdayEnabled { get; set; }
        public bool FridayEnabled { get; set; }
        public bool SaturdayEnabled { get; set; }
        public string ColorCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public WorkHours WorkHours { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
        public List<Employee> Employees { get; set; }
    }

    /// <summary>
    /// Biometric device model
    /// </summary>
    public class BiometricDevice
    {
        public int ID { get; set; }
        public string DeviceName { get; set; }
        public string DeviceModel { get; set; }
        public string SerialNumber { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string CommunicationKey { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastSyncTime { get; set; }
        public string LastSyncStatus { get; set; }
        public string LastSyncErrors { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public User Creator { get; set; }
        public User Updater { get; set; }
        public List<RawAttendanceLog> AttendanceLogs { get; set; }
    }

    /// <summary>
    /// Raw attendance log model (from biometric device)
    /// </summary>
    public class RawAttendanceLog
    {
        public int ID { get; set; }
        public int? DeviceID { get; set; }
        public int BiometricUserID { get; set; } // User ID in the biometric device
        public DateTime LogDateTime { get; set; }
        public int? LogType { get; set; } // 0 = check-in, 1 = check-out, etc.
        public int? VerifyMode { get; set; } // 0 = fingerprint, 1 = card, 2 = password, etc.
        public int? WorkCode { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsMatched { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime SyncTime { get; set; }

        // Navigation properties
        public BiometricDevice Device { get; set; }
        public Employee Employee { get; set; }
    }

    /// <summary>
    /// Attendance record model
    /// </summary>
    public class AttendanceRecord
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public int? WorkHoursID { get; set; }
        public int LateMinutes { get; set; }
        public int EarlyDepartureMinutes { get; set; }
        public int OvertimeMinutes { get; set; }
        public int WorkedMinutes { get; set; }
        public string Status { get; set; } // Present, absent, late, early departure, leave, etc.
        public bool IsManualEntry { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public WorkHours WorkHours { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
    }

    /// <summary>
    /// Attendance permission model
    /// </summary>
    public class AttendancePermission
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime PermissionDate { get; set; }
        public string PermissionType { get; set; } // Late arrival, early departure, etc.
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? TotalMinutes { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; } // Pending, approved, rejected, etc.
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public User Approver { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
    }

    #endregion

    #region Leaves

    /// <summary>
    /// Leave type model
    /// </summary>
    public class LeaveType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DefaultDays { get; set; }
        public bool IsPaid { get; set; }
        public bool AffectsSalary { get; set; }
        public bool RequiresApproval { get; set; }
        public int? MaxDaysPerRequest { get; set; }
        public int MinDaysBeforeRequest { get; set; }
        public int CarryOverDays { get; set; }
        public int CarryOverExpiryMonths { get; set; }
        public string Gender { get; set; } // For gender-specific leave types (e.g., maternity leave)
        public string ColorCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public User Creator { get; set; }
        public User Updater { get; set; }
        public List<LeaveBalance> LeaveBalances { get; set; }
        public List<LeaveRequest> LeaveRequests { get; set; }
    }

    /// <summary>
    /// Leave balance model
    /// </summary>
    public class LeaveBalance
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int LeaveTypeID { get; set; }
        public int Year { get; set; }
        public int TotalDays { get; set; }
        public int UsedDays { get; set; }
        public int PendingDays { get; set; }
        public int AvailableDays { get; set; } // Computed in database
        public int CarriedOverDays { get; set; }
        public DateTime? CarryOverExpiryDate { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public LeaveType LeaveType { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
    }

    /// <summary>
    /// Leave request model
    /// </summary>
    public class LeaveRequest
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int LeaveTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int TotalDays { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; } // Pending, approved, rejected, canceled, etc.
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string RejectionReason { get; set; }
        public string ContactDuringLeave { get; set; }
        public int? AlternateEmployeeID { get; set; }
        public string Notes { get; set; }
        public byte[] Attachments { get; set; }
        public string AttachmentsPath { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public LeaveType LeaveType { get; set; }
        public User Approver { get; set; }
        public Employee AlternateEmployee { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
    }

    #endregion

    #region Payroll

    /// <summary>
    /// Deduction rule model
    /// </summary>
    public class DeductionRule
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // Late, absent, violation, etc.
        public string DeductionMethod { get; set; } // Percentage of salary, fixed amount, days, hours, etc.
        public decimal DeductionValue { get; set; }
        public string AppliesTo { get; set; } // All employees, specific department, specific position, etc.
        public int? DepartmentID { get; set; }
        public int? PositionID { get; set; }
        public decimal? MinViolation { get; set; } // Minimum violation value (e.g., minutes late)
        public decimal? MaxViolation { get; set; } // Maximum violation value
        public DateTime? ActivationDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public Department Department { get; set; }
        public Position Position { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
        public List<Deduction> Deductions { get; set; }
    }

    /// <summary>
    /// Deduction model
    /// </summary>
    public class Deduction
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int? DeductionRuleID { get; set; }
        public DateTime DeductionDate { get; set; }
        public DateTime ViolationDate { get; set; }
        public string ViolationType { get; set; } // Late, absent, violation, etc.
        public decimal? ViolationValue { get; set; } // Value of the violation (e.g., minutes late)
        public string DeductionMethod { get; set; } // Percentage of salary, fixed amount, days, hours, etc.
        public decimal DeductionValue { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // Pending, approved, rejected, canceled, etc.
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public bool IsPayrollProcessed { get; set; }
        public int? PayrollID { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public DeductionRule DeductionRule { get; set; }
        public User Approver { get; set; }
        public PayrollRecord Payroll { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
    }

    /// <summary>
    /// Salary component model
    /// </summary>
    public class SalaryComponent
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // Basic, allowance, deduction, bonus, etc.
        public bool IsBasic { get; set; }
        public bool IsVariable { get; set; }
        public bool IsTaxable { get; set; }
        public bool AffectsNetSalary { get; set; }
        public int? Position { get; set; } // Order of appearance in the payslip
        public string FormulaType { get; set; } // Fixed, percentage of basic, formula, etc.
        public string PercentageOf { get; set; } // Basic salary, gross salary, other component, etc.
        public decimal? DefaultAmount { get; set; }
        public decimal? DefaultPercentage { get; set; }
        public string Formula { get; set; } // Custom formula if applicable
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public User Creator { get; set; }
        public User Updater { get; set; }
        public List<EmployeeSalaryComponent> EmployeeSalaryComponents { get; set; }
        public List<PayrollComponentDetail> PayrollComponentDetails { get; set; }
    }

    /// <summary>
    /// Employee salary model
    /// </summary>
    public class EmployeeSalary
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int ComponentID { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Percentage { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public SalaryComponent Component { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
    }

    /// <summary>
    /// Payroll model
    /// </summary>
    public class Payroll
    {
        public int ID { get; set; }
        public string PayrollName { get; set; }
        public string PayrollPeriod { get; set; } // MM/YYYY
        public string PayrollType { get; set; } // Monthly, semi-monthly, weekly
        public int PayrollMonth { get; set; }
        public int PayrollYear { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int TotalEmployees { get; set; }
        public decimal TotalBasicSalary { get; set; }
        public decimal TotalAllowances { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal TotalOvertimeAmount { get; set; }
        public decimal TotalPayrollAmount { get; set; }
        public string Status { get; set; } // Draft, under review, approved, paid, canceled
        public int? ProcessedBy { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public User Processor { get; set; }
        public User Approver { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
        public List<PayrollDetail> PayrollDetails { get; set; }
    }

    /// <summary>
    /// Payroll detail model
    /// </summary>
    public class PayrollDetail
    {
        public int ID { get; set; }
        public int PayrollID { get; set; }
        public int EmployeeID { get; set; }
        public int WorkingDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public int LeaveDays { get; set; }
        public int LateMinutes { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal TotalAllowances { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal OvertimeAmount { get; set; }
        public decimal NetSalary { get; set; }
        public string Status { get; set; } // Calculated, paid, canceled
        public string PaymentMethod { get; set; }
        public string PaymentReference { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public Payroll Payroll { get; set; }
        public Employee Employee { get; set; }
        public List<PayrollComponentDetail> ComponentDetails { get; set; }
    }

    /// <summary>
    /// Payroll component detail model
    /// </summary>
    public class PayrollComponentDetail
    {
        public int ID { get; set; }
        public int PayrollDetailID { get; set; }
        public int ComponentID { get; set; }
        public string ComponentName { get; set; }
        public string ComponentType { get; set; } // Basic, allowance, deduction, bonus, etc.
        public decimal Amount { get; set; }
        public string Formula { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public PayrollDetail PayrollDetail { get; set; }
        public SalaryComponent Component { get; set; }
    }

    /// <summary>
    /// Notification model
    /// </summary>
    public class Notification
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int? EmployeeID { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public bool IsSystemNotification { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiryDate { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
    }

    /// <summary>
    /// System setting model
    /// </summary>
    public class SystemSetting
    {
        public int ID { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string SettingGroup { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Loan model
    /// </summary>
    public class Loan
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string LoanType { get; set; } // Personal loan, advance, etc.
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; } // Computed in database
        public string Status { get; set; } // Pending, approved, rejected, paid, canceled, etc.
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string RejectionReason { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public User Approver { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
        public List<LoanInstallment> Installments { get; set; }
    }

    /// <summary>
    /// Loan installment model
    /// </summary>
    public class LoanInstallment
    {
        public int ID { get; set; }
        public int LoanID { get; set; }
        public int InstallmentNumber { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Status { get; set; } // Pending, paid, overdue, etc.
        public int? PayrollDetailID { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public Loan Loan { get; set; }
        public PayrollDetail PayrollDetail { get; set; }
    }

    #endregion

    #region System Settings

    /// <summary>
    /// System setting model
    /// </summary>
    public class SystemSetting
    {
        public int ID { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string SettingGroup { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    #endregion
}