using System;
using System.Collections.Generic;

namespace HR.Models
{
    #region Company

    /// <summary>
    /// Company information DTO
    /// </summary>
    public class CompanyDTO
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
    }

    #endregion

    #region Departments

    /// <summary>
    /// Department DTO
    /// </summary>
    public class DepartmentDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentID { get; set; }
        public string ParentName { get; set; }
        public int? ManagerPositionID { get; set; }
        public string ManagerPositionTitle { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public List<DepartmentDTO> ChildDepartments { get; set; }
    }

    /// <summary>
    /// Department tree node DTO (for tree views)
    /// </summary>
    public class DepartmentTreeNodeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }
        public bool HasChildren { get; set; }
        public bool IsActive { get; set; }
        public List<DepartmentTreeNodeDTO> Children { get; set; }
    }

    #endregion

    #region Positions

    /// <summary>
    /// Position DTO
    /// </summary>
    public class PositionDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int? GradeLevel { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public bool IsManagerPosition { get; set; }
        public bool IsActive { get; set; }
    }

    #endregion

    #region Roles and Permissions

    /// <summary>
    /// Role DTO
    /// </summary>
    public class RoleDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserCount { get; set; }
        public List<RolePermissionDTO> Permissions { get; set; }
    }

    /// <summary>
    /// Role permission DTO
    /// </summary>
    public class RolePermissionDTO
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
    }

    /// <summary>
    /// Module DTO
    /// </summary>
    public class ModuleDTO
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public int Order { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
    }

    #endregion

    #region Users

    /// <summary>
    /// User DTO
    /// </summary>
    public class UserDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int? RoleID { get; set; }
        public string RoleName { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public bool IsActive { get; set; }
        public bool MustChangePassword { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public int FailedLoginAttempts { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? LockoutEnd { get; set; }
    }

    /// <summary>
    /// User creation DTO
    /// </summary>
    public class UserCreateDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int? RoleID { get; set; }
        public int? EmployeeID { get; set; }
        public bool IsActive { get; set; }
        public bool MustChangePassword { get; set; }
    }

    /// <summary>
    /// User update DTO
    /// </summary>
    public class UserUpdateDTO
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int? RoleID { get; set; }
        public int? EmployeeID { get; set; }
        public bool IsActive { get; set; }
        public bool MustChangePassword { get; set; }
    }

    /// <summary>
    /// Password change DTO
    /// </summary>
    public class PasswordChangeDTO
    {
        public int UserID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Login DTO
    /// </summary>
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// Login history DTO
    /// </summary>
    public class LoginHistoryDTO
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public string IPAddress { get; set; }
        public string MachineName { get; set; }
        public string LoginStatus { get; set; }
        public string UserAgent { get; set; }
    }

    /// <summary>
    /// Activity log DTO
    /// </summary>
    public class ActivityLogDTO
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityType { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public string IPAddress { get; set; }
        public int? RecordID { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }

    #endregion

    #region Employees

    /// <summary>
    /// Employee DTO
    /// </summary>
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
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
        public string DepartmentName { get; set; }
        public int? PositionID { get; set; }
        public string PositionTitle { get; set; }
        public int? DirectManagerID { get; set; }
        public string DirectManagerName { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? ProbationEndDate { get; set; }
        public string EmploymentType { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public int? WorkShiftID { get; set; }
        public string WorkShiftName { get; set; }

        // Status
        public string Status { get; set; }
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
        public int? BiometricID { get; set; }

        // User information
        public int? UserID { get; set; }
        public string Username { get; set; }
    }

    /// <summary>
    /// Employee list item DTO (for grid views)
    /// </summary>
    public class EmployeeListItemDTO
    {
        public int ID { get; set; }
        public string EmployeeNumber { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionTitle { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public string Status { get; set; }
        public byte[] Photo { get; set; }
    }

    /// <summary>
    /// Employee document DTO
    /// </summary>
    public class EmployeeDocumentDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string IssuedBy { get; set; }
        public byte[] DocumentFile { get; set; }
        public string DocumentPath { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Employee transfer DTO
    /// </summary>
    public class EmployeeTransferDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public string TransferType { get; set; }
        public int? FromDepartmentID { get; set; }
        public string FromDepartmentName { get; set; }
        public int? ToDepartmentID { get; set; }
        public string ToDepartmentName { get; set; }
        public int? FromPositionID { get; set; }
        public string FromPositionTitle { get; set; }
        public int? ToPositionID { get; set; }
        public string ToPositionTitle { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
    }

    #endregion

    #region Attendance

    /// <summary>
    /// Work hours DTO
    /// </summary>
    public class WorkHoursDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int FlexibleMinutes { get; set; }
        public int LateThresholdMinutes { get; set; }
        public int ShortDayThresholdMinutes { get; set; }
        public int OverTimeStartMinutes { get; set; }
        public decimal TotalHours { get; set; }
    }

    /// <summary>
    /// Work shift DTO
    /// </summary>
    public class WorkShiftDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? WorkHoursID { get; set; }
        public string WorkHoursName { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool SundayEnabled { get; set; }
        public bool MondayEnabled { get; set; }
        public bool TuesdayEnabled { get; set; }
        public bool WednesdayEnabled { get; set; }
        public bool ThursdayEnabled { get; set; }
        public bool FridayEnabled { get; set; }
        public bool SaturdayEnabled { get; set; }
        public string ColorCode { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Biometric device DTO
    /// </summary>
    public class BiometricDeviceDTO
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
    }

    /// <summary>
    /// Raw attendance log DTO
    /// </summary>
    public class RawAttendanceLogDTO
    {
        public int ID { get; set; }
        public int? DeviceID { get; set; }
        public string DeviceName { get; set; }
        public int BiometricUserID { get; set; }
        public DateTime LogDateTime { get; set; }
        public int? LogType { get; set; }
        public string LogTypeString { get; set; }
        public int? VerifyMode { get; set; }
        public string VerifyModeString { get; set; }
        public int? WorkCode { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsMatched { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public DateTime SyncTime { get; set; }
    }

    /// <summary>
    /// Attendance record DTO
    /// </summary>
    public class AttendanceRecordDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public int? WorkHoursID { get; set; }
        public string WorkHoursName { get; set; }
        public TimeSpan? ScheduledStartTime { get; set; }
        public TimeSpan? ScheduledEndTime { get; set; }
        public int LateMinutes { get; set; }
        public int EarlyDepartureMinutes { get; set; }
        public int OvertimeMinutes { get; set; }
        public int WorkedMinutes { get; set; }
        public string Status { get; set; }
        public bool IsManualEntry { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Attendance permission DTO
    /// </summary>
    public class AttendancePermissionDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public DateTime PermissionDate { get; set; }
        public string PermissionType { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? TotalMinutes { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApproverFullName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Employee attendance summary DTO
    /// </summary>
    public class EmployeeAttendanceSummaryDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public int TotalDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public int LateDays { get; set; }
        public int EarlyDepartureDays { get; set; }
        public int LeaveDays { get; set; }
        public int Holidays { get; set; }
        public int WeekendDays { get; set; }
        public int TotalLateMinutes { get; set; }
        public int TotalEarlyDepartureMinutes { get; set; }
        public int TotalOvertimeMinutes { get; set; }
        public int TotalWorkedMinutes { get; set; }
    }

    #endregion

    #region Leaves

    /// <summary>
    /// Leave type DTO
    /// </summary>
    public class LeaveTypeDTO
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
        public string Gender { get; set; }
        public string ColorCode { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Leave balance DTO
    /// </summary>
    public class LeaveBalanceDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeNumber { get; set; }
        public int LeaveTypeID { get; set; }
        public string LeaveTypeName { get; set; }
        public int Year { get; set; }
        public int TotalDays { get; set; }
        public int UsedDays { get; set; }
        public int PendingDays { get; set; }
        public int AvailableDays { get; set; }
        public int CarriedOverDays { get; set; }
        public DateTime? CarryOverExpiryDate { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Leave request DTO
    /// </summary>
    public class LeaveRequestDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public int LeaveTypeID { get; set; }
        public string LeaveTypeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int TotalDays { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApproverFullName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string RejectionReason { get; set; }
        public string ContactDuringLeave { get; set; }
        public int? AlternateEmployeeID { get; set; }
        public string AlternateEmployeeFullName { get; set; }
        public string Notes { get; set; }
        public byte[] Attachments { get; set; }
        public string AttachmentsPath { get; set; }
    }

    #endregion

    #region Payroll

    /// <summary>
    /// Deduction rule DTO
    /// </summary>
    public class DeductionRuleDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string DeductionMethod { get; set; }
        public decimal DeductionValue { get; set; }
        public string AppliesTo { get; set; }
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int? PositionID { get; set; }
        public string PositionTitle { get; set; }
        public decimal? MinViolation { get; set; }
        public decimal? MaxViolation { get; set; }
        public DateTime? ActivationDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Deduction DTO
    /// </summary>
    public class DeductionDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public int? DeductionRuleID { get; set; }
        public string DeductionRuleName { get; set; }
        public DateTime DeductionDate { get; set; }
        public DateTime ViolationDate { get; set; }
        public string ViolationType { get; set; }
        public decimal? ViolationValue { get; set; }
        public string DeductionMethod { get; set; }
        public decimal DeductionValue { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApproverFullName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public bool IsPayrollProcessed { get; set; }
        public int? PayrollID { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Salary component DTO
    /// </summary>
    public class SalaryComponentDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool IsBasic { get; set; }
        public bool IsVariable { get; set; }
        public bool IsTaxable { get; set; }
        public bool AffectsNetSalary { get; set; }
        public int? Position { get; set; }
        public string FormulaType { get; set; }
        public string PercentageOf { get; set; }
        public decimal? DefaultAmount { get; set; }
        public decimal? DefaultPercentage { get; set; }
        public string Formula { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Employee salary DTO
    /// </summary>
    public class EmployeeSalaryDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public string PositionTitle { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public List<EmployeeSalaryComponentDTO> SalaryComponents { get; set; }
        public decimal TotalSalary { get; set; }
    }

    /// <summary>
    /// Employee salary component DTO
    /// </summary>
    public class EmployeeSalaryComponentDTO
    {
        public int ID { get; set; }
        public int EmployeeSalaryID { get; set; }
        public int SalaryComponentID { get; set; }
        public string SalaryComponentName { get; set; }
        public string SalaryComponentType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Percentage { get; set; }
        public string Formula { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Payroll period DTO
    /// </summary>
    public class PayrollPeriodDTO
    {
        public int ID { get; set; }
        public string PeriodName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public int NumberOfEmployees { get; set; }
        public decimal TotalPayroll { get; set; }
    }

    /// <summary>
    /// Payroll record DTO
    /// </summary>
    public class PayrollRecordDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public string PositionTitle { get; set; }
        public int PayrollPeriodID { get; set; }
        public string PayrollPeriodName { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal TotalAllowances { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal NetSalary { get; set; }
        public int? WorkDays { get; set; }
        public int AbsentDays { get; set; }
        public int LeaveDays { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal OvertimeAmount { get; set; }
        public decimal LoanDeductions { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal SocialInsuranceAmount { get; set; }
        public decimal OtherDeductions { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApproverFullName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentReference { get; set; }
        public DateTime? PaymentDate { get; set; }
        public List<PayrollComponentDetailDTO> ComponentDetails { get; set; }
    }

    /// <summary>
    /// Payroll component detail DTO
    /// </summary>
    public class PayrollComponentDetailDTO
    {
        public int ID { get; set; }
        public int PayrollRecordID { get; set; }
        public int SalaryComponentID { get; set; }
        public string ComponentName { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Loan DTO
    /// </summary>
    public class LoanDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public string LoanType { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public string Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApproverFullName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string RejectionReason { get; set; }
        public string Notes { get; set; }
        public List<LoanInstallmentDTO> Installments { get; set; }
    }

    /// <summary>
    /// Loan installment DTO
    /// </summary>
    public class LoanInstallmentDTO
    {
        public int ID { get; set; }
        public int LoanID { get; set; }
        public int InstallmentNumber { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Status { get; set; }
        public int? PayrollRecordID { get; set; }
        public string PayrollPeriodName { get; set; }
        public string Notes { get; set; }
    }

    #endregion

    #region System Settings

    /// <summary>
    /// System setting DTO
    /// </summary>
    public class SystemSettingDTO
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string SettingGroup { get; set; }
        public string Description { get; set; }
        public bool IsSecure { get; set; }
    }

    #endregion

    #region Dashboard

    /// <summary>
    /// Dashboard statistics DTO
    /// </summary>
    public class DashboardStatisticsDTO
    {
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int OnLeaveEmployees { get; set; }
        public int NewEmployees { get; set; }
        public int ProbationEmployees { get; set; }
        public int ExpiringContractsCount { get; set; }
        public int ExpiringDocumentsCount { get; set; }
        public int BirthdaysThisMonth { get; set; }
        public decimal TotalPayroll { get; set; }
        public int AbsentToday { get; set; }
        public int LateToday { get; set; }
        public int PresentToday { get; set; }
        public int PendingLeaveRequests { get; set; }
        public int ApprovedLeaveRequests { get; set; }
        public int TotalDepartments { get; set; }
        public int TotalPositions { get; set; }
    }

    /// <summary>
    /// Department statistics DTO
    /// </summary>
    public class DepartmentStatisticsDTO
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int EmployeeCount { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal AverageSalary { get; set; }
        public double AverageTenure { get; set; } // In years
        public double AverageAge { get; set; }
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }
    }

    /// <summary>
    /// Attendance statistics DTO
    /// </summary>
    public class AttendanceStatisticsDTO
    {
        public string Date { get; set; } // For grouping by day, month, or year
        public int TotalEmployees { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int LateCount { get; set; }
        public int EarlyDepartureCount { get; set; }
        public int LeaveCount { get; set; }
        public double PresentPercentage { get; set; }
        public double AbsentPercentage { get; set; }
        public double LatePercentage { get; set; }
    }

    /// <summary>
    /// Upcoming event DTO (birthdays, contract expirations, etc.)
    /// </summary>
    public class UpcomingEventDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public string EventType { get; set; } // Birthday, contract expiry, document expiry, etc.
        public DateTime EventDate { get; set; }
        public int DaysRemaining { get; set; }
        public string Notes { get; set; }
    }

    #endregion

    #region Reports

    /// <summary>
    /// Report parameter DTO
    /// </summary>
    public class ReportParameterDTO
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Type { get; set; } // Text, Date, Number, Boolean, List, etc.
        public string Value { get; set; }
        public List<KeyValuePair<string, string>> Options { get; set; } // For dropdown lists
        public bool IsRequired { get; set; }
    }

    /// <summary>
    /// Report DTO
    /// </summary>
    public class ReportDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string FileName { get; set; }
        public bool IsActive { get; set; }
        public List<ReportParameterDTO> Parameters { get; set; }
    }

    #endregion

    #region ZKTeco Integration

    /// <summary>
    /// ZKTeco user DTO
    /// </summary>
    public class ZKUserDTO
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Privilege { get; set; }
        public bool Enabled { get; set; }
        public string CardNo { get; set; }
    }

    /// <summary>
    /// ZKTeco template (fingerprint) DTO
    /// </summary>
    public class ZKTemplateDTO
    {
        public int UserID { get; set; }
        public int FingerIndex { get; set; }
        public int Flag { get; set; }
        public string Template { get; set; }
        public int Size { get; set; }
    }

    /// <summary>
    /// ZKTeco attendance log DTO
    /// </summary>
    public class ZKAttendanceDTO
    {
        public int UserID { get; set; }
        public DateTime LogTime { get; set; }
        public int VerifyMode { get; set; }
        public int InOutMode { get; set; }
        public int WorkCode { get; set; }
    }

    /// <summary>
    /// ZKTeco device status DTO
    /// </summary>
    public class ZKDeviceStatusDTO
    {
        public bool IsConnected { get; set; }
        public string SerialNumber { get; set; }
        public string DeviceModel { get; set; }
        public string Firmware { get; set; }
        public int UserCount { get; set; }
        public int TemplateCount { get; set; }
        public int AttendanceCount { get; set; }
        public DateTime DeviceTime { get; set; }
    }

    #endregion

    #region Common

    /// <summary>
    /// Key value pair DTO
    /// </summary>
    public class KeyValueDTO
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    /// Tree node DTO (for tree views)
    /// </summary>
    public class TreeNodeDTO
    {
        public string ID { get; set; }
        public string ParentID { get; set; }
        public string Text { get; set; }
        public bool HasChildren { get; set; }
        public bool IsExpanded { get; set; }
        public bool IsSelected { get; set; }
        public string IconCls { get; set; }
        public List<TreeNodeDTO> Children { get; set; }
    }

    /// <summary>
    /// Result DTO (for returning operation results)
    /// </summary>
    public class ResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    /// <summary>
    /// Pagination DTO
    /// </summary>
    public class PaginationDTO
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
    }

    /// <summary>
    /// Paged result DTO
    /// </summary>
    public class PagedResultDTO<T>
    {
        public List<T> Items { get; set; }
        public PaginationDTO Pagination { get; set; }
    }

    #endregion
}