using System;
using System.Collections.Generic;

namespace HR.Models.DTOs
{
    #region Company

    /// <summary>
    /// Company DTO
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

    /// <summary>
    /// Company settings summary DTO
    /// </summary>
    public class CompanySummaryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LegalName { get; set; }
        public byte[] Logo { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    #endregion

    #region Department

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
        public int EmployeeCount { get; set; }
        public List<DepartmentDTO> ChildDepartments { get; set; }
    }

    /// <summary>
    /// Department lite DTO (for dropdowns and simple lists)
    /// </summary>
    public class DepartmentLiteDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Department organizational structure DTO
    /// </summary>
    public class DepartmentOrgStructureDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }
        public int? ManagerID { get; set; }
        public string ManagerName { get; set; }
        public int EmployeeCount { get; set; }
        public List<DepartmentOrgStructureDTO> Children { get; set; }
    }

    #endregion

    #region Position

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
        public int EmployeeCount { get; set; }
    }

    /// <summary>
    /// Position lite DTO (for dropdowns and simple lists)
    /// </summary>
    public class PositionLiteDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public bool IsManagerPosition { get; set; }
        public bool IsActive { get; set; }
    }

    #endregion

    #region User

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
        public string EmployeeName { get; set; }
        public bool IsActive { get; set; }
        public bool MustChangePassword { get; set; }
        public DateTime? LastLogin { get; set; }
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
        public bool IsActive { get; set; }
        public bool MustChangePassword { get; set; }
    }

    /// <summary>
    /// Change password DTO
    /// </summary>
    public class ChangePasswordDTO
    {
        public int UserID { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Reset password DTO
    /// </summary>
    public class ResetPasswordDTO
    {
        public int UserID { get; set; }
        public string NewPassword { get; set; }
        public bool MustChangePassword { get; set; }
    }

    /// <summary>
    /// User login DTO
    /// </summary>
    public class UserLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// User authentication result DTO
    /// </summary>
    public class AuthResultDTO
    {
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }
        public UserDTO User { get; set; }
        public List<string> Permissions { get; set; }
    }

    #endregion

    #region Role

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
    /// Role lite DTO (for dropdowns and simple lists)
    /// </summary>
    public class RoleLiteDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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

    #endregion

    #region Employee

    /// <summary>
    /// Employee DTO
    /// </summary>
    public class EmployeeDTO
    {
        // Basic info
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

        public int ServiceYears { get; set; }
        public int ServiceMonths { get; set; }
        public int ServiceDays { get; set; }
    }

    /// <summary>
    /// Employee lite DTO (for dropdowns and simple lists)
    /// </summary>
    public class EmployeeLiteDTO
    {
        public int ID { get; set; }
        public string EmployeeNumber { get; set; }
        public string FullName { get; set; }
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int? PositionID { get; set; }
        public string PositionTitle { get; set; }
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
        public string EmployeeName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string IssuedBy { get; set; }
        public byte[] DocumentFile { get; set; }
        public string DocumentPath { get; set; }
        public string Notes { get; set; }
        public bool IsExpired { get; set; }
        public int DaysUntilExpiry { get; set; }
    }

    /// <summary>
    /// Employee transfer DTO
    /// </summary>
    public class EmployeeTransferDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
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
        public string CreatedByName { get; set; }
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
        public bool IsActive { get; set; }
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
        public decimal? WorkHours { get; set; }
        public bool SundayEnabled { get; set; }
        public bool MondayEnabled { get; set; }
        public bool TuesdayEnabled { get; set; }
        public bool WednesdayEnabled { get; set; }
        public bool ThursdayEnabled { get; set; }
        public bool FridayEnabled { get; set; }
        public bool SaturdayEnabled { get; set; }
        public string ColorCode { get; set; }
        public bool IsActive { get; set; }
        public int EmployeeCount { get; set; }
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
        public bool IsConnected { get; set; }
        public DateTime? LastSyncTime { get; set; }
        public string LastSyncStatus { get; set; }
        public string LastSyncErrors { get; set; }
        public int LogCount { get; set; } 
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
        public string LogTypeDescription { get; set; }
        public int? VerifyMode { get; set; }
        public string VerifyModeDescription { get; set; }
        public int? WorkCode { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsMatched { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime SyncTime { get; set; }
    }

    /// <summary>
    /// Attendance record DTO
    /// </summary>
    public class AttendanceRecordDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public string PositionTitle { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public TimeSpan? Duration { get; set; }
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
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public DateTime PermissionDate { get; set; }
        public string DayOfWeek { get; set; }
        public string PermissionType { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? TotalMinutes { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApproverName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Daily attendance summary DTO
    /// </summary>
    public class DailyAttendanceSummaryDTO
    {
        public DateTime AttendanceDate { get; set; }
        public string DayOfWeek { get; set; }
        public int TotalEmployees { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int LateCount { get; set; }
        public int EarlyDepartureCount { get; set; }
        public int LeaveCount { get; set; }
        public int AttendancePermissionCount { get; set; }
        public List<AttendanceRecordDTO> AttendanceRecords { get; set; }
    }

    /// <summary>
    /// Monthly attendance summary DTO (per employee)
    /// </summary>
    public class MonthlyAttendanceDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int TotalWorkDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public int LateDays { get; set; }
        public int EarlyDepartureDays { get; set; }
        public int WeekendDays { get; set; }
        public int HolidayDays { get; set; }
        public int LeaveDays { get; set; }
        public int TotalLateMinutes { get; set; }
        public int TotalEarlyDepartureMinutes { get; set; }
        public int TotalOvertimeMinutes { get; set; }
        public int TotalWorkMinutes { get; set; }
        public decimal AttendancePercentage { get; set; }
        public List<AttendanceRecordDTO> DailyRecords { get; set; }
    }

    #endregion

    #region Leave

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
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public int LeaveTypeID { get; set; }
        public string LeaveTypeName { get; set; }
        public string LeaveTypeColor { get; set; }
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
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public int LeaveTypeID { get; set; }
        public string LeaveTypeName { get; set; }
        public string LeaveTypeColor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int TotalDays { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApproverName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string RejectionReason { get; set; }
        public string ContactDuringLeave { get; set; }
        public int? AlternateEmployeeID { get; set; }
        public string AlternateEmployeeName { get; set; }
        public string Notes { get; set; }
        public byte[] Attachments { get; set; }
        public string AttachmentsPath { get; set; }
    }

    /// <summary>
    /// Employee leave summary DTO
    /// </summary>
    public class EmployeeLeaveSummaryDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public int Year { get; set; }
        public int TotalLeavesTaken { get; set; }
        public int TotalLeaveDays { get; set; }
        public int PendingLeaveRequests { get; set; }
        public int PendingLeaveDays { get; set; }
        public List<LeaveBalanceDTO> LeaveBalances { get; set; }
        public List<LeaveRequestDTO> LeaveHistory { get; set; }
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
        public string EmployeeName { get; set; }
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
        public string ApproverName { get; set; }
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
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public string PositionTitle { get; set; }
        public int ComponentID { get; set; }
        public string ComponentName { get; set; }
        public string ComponentType { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Percentage { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Employee salary summary DTO
    /// </summary>
    public class EmployeeSalarySummaryDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public string PositionTitle { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal TotalAllowances { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetSalary { get; set; }
        public List<EmployeeSalaryComponentDTO> SalaryComponents { get; set; }
    }

    /// <summary>
    /// Employee salary component DTO
    /// </summary>
    public class EmployeeSalaryComponentDTO
    {
        public int ID { get; set; }
        public int ComponentID { get; set; }
        public string ComponentName { get; set; }
        public string ComponentType { get; set; }
        public string Formula { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Percentage { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Payroll DTO
    /// </summary>
    public class PayrollDTO
    {
        public int ID { get; set; }
        public string PayrollName { get; set; }
        public string PayrollPeriod { get; set; }
        public string PayrollType { get; set; }
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
        public string Status { get; set; }
        public int? ProcessedBy { get; set; }
        public string ProcessorName { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApproverName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Payroll detail DTO
    /// </summary>
    public class PayrollDetailDTO
    {
        public int ID { get; set; }
        public int PayrollID { get; set; }
        public string PayrollName { get; set; }
        public string PayrollPeriod { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public string PositionTitle { get; set; }
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
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentReference { get; set; }
        public string Notes { get; set; }
        public List<PayrollComponentDetailDTO> ComponentDetails { get; set; }
    }

    /// <summary>
    /// Payroll component detail DTO
    /// </summary>
    public class PayrollComponentDetailDTO
    {
        public int ID { get; set; }
        public int PayrollDetailID { get; set; }
        public int ComponentID { get; set; }
        public string ComponentName { get; set; }
        public string ComponentType { get; set; }
        public decimal Amount { get; set; }
        public string Formula { get; set; }
    }

    /// <summary>
    /// Loan DTO
    /// </summary>
    public class LoanDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
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
        public string ApproverName { get; set; }
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
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string LoanType { get; set; }
        public int InstallmentNumber { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Status { get; set; }
        public int? PayrollDetailID { get; set; }
        public string PayrollPeriod { get; set; }
        public string Notes { get; set; }
    }

    #endregion

    #region System

    /// <summary>
    /// System setting DTO
    /// </summary>
    public class SystemSettingDTO
    {
        public int ID { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string SettingGroup { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
    }

    /// <summary>
    /// Notification DTO
    /// </summary>
    public class NotificationDTO
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public bool IsSystemNotification { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }

    /// <summary>
    /// Dashboard summary DTO
    /// </summary>
    public class DashboardSummaryDTO
    {
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int OnLeaveEmployees { get; set; }
        public int NewEmployeesThisMonth { get; set; }
        public int TotalDepartments { get; set; }
        public int TotalPositions { get; set; }
        public int PresentToday { get; set; }
        public int AbsentToday { get; set; }
        public int LateToday { get; set; }
        public int OnLeaveToday { get; set; }
        public int PendingLeaveRequests { get; set; }
        public List<UpcomingEventDTO> UpcomingEvents { get; set; }
        public List<BirthdayDTO> UpcomingBirthdays { get; set; }
        public List<ProbationEndDTO> UpcomingProbationEnds { get; set; }
        public List<ContractEndDTO> UpcomingContractEnds { get; set; }
        public List<DocumentExpiryDTO> UpcomingDocumentExpiries { get; set; }
    }

    /// <summary>
    /// Upcoming event DTO
    /// </summary>
    public class UpcomingEventDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int DaysRemaining { get; set; }
    }

    /// <summary>
    /// Employee birthday DTO
    /// </summary>
    public class BirthdayDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public DateTime UpcomingBirthday { get; set; }
        public int DaysRemaining { get; set; }
    }

    /// <summary>
    /// Probation end DTO
    /// </summary>
    public class ProbationEndDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionTitle { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime ProbationEndDate { get; set; }
        public int DaysRemaining { get; set; }
    }

    /// <summary>
    /// Contract end DTO
    /// </summary>
    public class ContractEndDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionTitle { get; set; }
        public string EmploymentType { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public int DaysRemaining { get; set; }
    }

    /// <summary>
    /// Document expiry DTO
    /// </summary>
    public class DocumentExpiryDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int DocumentID { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTitle { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int DaysRemaining { get; set; }
    }

    /// <summary>
    /// Activity log DTO
    /// </summary>
    public class ActivityLogDTO
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityType { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public string IPAddress { get; set; }
        public int? RecordID { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }

    /// <summary>
    /// Login history DTO
    /// </summary>
    public class LoginHistoryDTO
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public string IPAddress { get; set; }
        public string MachineName { get; set; }
        public string LoginStatus { get; set; }
        public string UserAgent { get; set; }
        public TimeSpan? SessionDuration { get; set; }
    }

    #endregion

    #region Common

    /// <summary>
    /// Generic API response DTO
    /// </summary>
    public class ApiResponseDTO<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }

    /// <summary>
    /// Dropdown item DTO
    /// </summary>
    public class DropdownItemDTO
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public string Group { get; set; }
        public bool IsDisabled { get; set; }
        public string AdditionalData { get; set; }
    }

    /// <summary>
    /// Paged result DTO
    /// </summary>
    public class PagedResultDTO<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }

    /// <summary>
    /// Chart data DTO
    /// </summary>
    public class ChartDataDTO
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public List<string> Labels { get; set; }
        public List<ChartSeriesDTO> Series { get; set; }
    }

    /// <summary>
    /// Chart series DTO
    /// </summary>
    public class ChartSeriesDTO
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public List<decimal> Data { get; set; }
    }

    /// <summary>
    /// File upload result DTO
    /// </summary>
    public class FileUploadResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public byte[] FileBytes { get; set; }
    }

    #endregion
}