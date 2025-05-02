using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// Repository for employee operations
    /// </summary>
    public class EmployeeRepository
    {
        /// <summary>
        /// Gets all employees
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive employees</param>
        /// <returns>List of EmployeeDTO objects</returns>
        public List<EmployeeDTO> GetAllEmployees(bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT e.ID, e.EmployeeNumber, e.FirstName, e.MiddleName, e.LastName, 
                        e.FullName, e.Gender, e.BirthDate, e.NationalID, e.PassportNumber, 
                        e.MaritalStatus, e.Nationality, e.Religion, e.BloodType, 
                        e.Phone, e.Mobile, e.Email, e.Address, e.EmergencyContact, e.EmergencyPhone, 
                        e.DepartmentID, d.Name AS DepartmentName, e.PositionID, p.Title AS PositionTitle, 
                        e.DirectManagerID, manager.FullName AS DirectManagerName, 
                        e.HireDate, e.ProbationEndDate, e.EmploymentType, 
                        e.ContractStartDate, e.ContractEndDate, e.WorkShiftID, ws.Name AS WorkShiftName, 
                        e.Status, e.TerminationDate, e.TerminationReason, 
                        e.BankName, e.BankBranch, e.BankAccountNumber, e.IBAN, 
                        e.Photo, e.Notes, e.BiometricID, 
                        e.CreatedAt, e.CreatedBy, creator.Username AS CreatedByName, 
                        e.UpdatedAt, e.UpdatedBy, updater.Username AS UpdatedByName
                    FROM Employees e
                    LEFT JOIN Departments d ON e.DepartmentID = d.ID
                    LEFT JOIN Positions p ON e.PositionID = p.ID
                    LEFT JOIN Employees manager ON e.DirectManagerID = manager.ID
                    LEFT JOIN WorkShifts ws ON e.WorkShiftID = ws.ID
                    LEFT JOIN Users creator ON e.CreatedBy = creator.ID
                    LEFT JOIN Users updater ON e.UpdatedBy = updater.ID";

                if (!includeInactive)
                {
                    query += " WHERE e.Status <> 'Terminated'";
                }

                query += " ORDER BY e.EmployeeNumber";

                DataTable dataTable = ConnectionManager.ExecuteQuery(query);
                List<EmployeeDTO> employees = new List<EmployeeDTO>();

                foreach (DataRow row in dataTable.Rows)
                {
                    EmployeeDTO employee = MapRowToEmployeeDTO(row);
                    employees.Add(employee);
                }

                return employees;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<EmployeeDTO>();
            }
        }

        /// <summary>
        /// Gets employees by department
        /// </summary>
        /// <param name="departmentId">Department ID</param>
        /// <param name="includeInactive">Whether to include inactive employees</param>
        /// <returns>List of EmployeeDTO objects</returns>
        public List<EmployeeDTO> GetEmployeesByDepartment(int departmentId, bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT e.ID, e.EmployeeNumber, e.FirstName, e.MiddleName, e.LastName, 
                        e.FullName, e.Gender, e.BirthDate, e.NationalID, e.PassportNumber, 
                        e.MaritalStatus, e.Nationality, e.Religion, e.BloodType, 
                        e.Phone, e.Mobile, e.Email, e.Address, e.EmergencyContact, e.EmergencyPhone, 
                        e.DepartmentID, d.Name AS DepartmentName, e.PositionID, p.Title AS PositionTitle, 
                        e.DirectManagerID, manager.FullName AS DirectManagerName, 
                        e.HireDate, e.ProbationEndDate, e.EmploymentType, 
                        e.ContractStartDate, e.ContractEndDate, e.WorkShiftID, ws.Name AS WorkShiftName, 
                        e.Status, e.TerminationDate, e.TerminationReason, 
                        e.BankName, e.BankBranch, e.BankAccountNumber, e.IBAN, 
                        e.Photo, e.Notes, e.BiometricID, 
                        e.CreatedAt, e.CreatedBy, creator.Username AS CreatedByName, 
                        e.UpdatedAt, e.UpdatedBy, updater.Username AS UpdatedByName
                    FROM Employees e
                    LEFT JOIN Departments d ON e.DepartmentID = d.ID
                    LEFT JOIN Positions p ON e.PositionID = p.ID
                    LEFT JOIN Employees manager ON e.DirectManagerID = manager.ID
                    LEFT JOIN WorkShifts ws ON e.WorkShiftID = ws.ID
                    LEFT JOIN Users creator ON e.CreatedBy = creator.ID
                    LEFT JOIN Users updater ON e.UpdatedBy = updater.ID
                    WHERE e.DepartmentID = @DepartmentID";

                if (!includeInactive)
                {
                    query += " AND e.Status <> 'Terminated'";
                }

                query += " ORDER BY e.EmployeeNumber";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentID", departmentId)
                };

                DataTable dataTable = ConnectionManager.ExecuteQuery(query, parameters);
                List<EmployeeDTO> employees = new List<EmployeeDTO>();

                foreach (DataRow row in dataTable.Rows)
                {
                    EmployeeDTO employee = MapRowToEmployeeDTO(row);
                    employees.Add(employee);
                }

                return employees;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<EmployeeDTO>();
            }
        }

        /// <summary>
        /// Gets an employee by ID
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>EmployeeDTO object</returns>
        public EmployeeDTO GetEmployeeById(int employeeId)
        {
            try
            {
                string query = @"
                    SELECT e.ID, e.EmployeeNumber, e.FirstName, e.MiddleName, e.LastName, 
                        e.FullName, e.Gender, e.BirthDate, e.NationalID, e.PassportNumber, 
                        e.MaritalStatus, e.Nationality, e.Religion, e.BloodType, 
                        e.Phone, e.Mobile, e.Email, e.Address, e.EmergencyContact, e.EmergencyPhone, 
                        e.DepartmentID, d.Name AS DepartmentName, e.PositionID, p.Title AS PositionTitle, 
                        e.DirectManagerID, manager.FullName AS DirectManagerName, 
                        e.HireDate, e.ProbationEndDate, e.EmploymentType, 
                        e.ContractStartDate, e.ContractEndDate, e.WorkShiftID, ws.Name AS WorkShiftName, 
                        e.Status, e.TerminationDate, e.TerminationReason, 
                        e.BankName, e.BankBranch, e.BankAccountNumber, e.IBAN, 
                        e.Photo, e.Notes, e.BiometricID, 
                        e.CreatedAt, e.CreatedBy, creator.Username AS CreatedByName, 
                        e.UpdatedAt, e.UpdatedBy, updater.Username AS UpdatedByName
                    FROM Employees e
                    LEFT JOIN Departments d ON e.DepartmentID = d.ID
                    LEFT JOIN Positions p ON e.PositionID = p.ID
                    LEFT JOIN Employees manager ON e.DirectManagerID = manager.ID
                    LEFT JOIN WorkShifts ws ON e.WorkShiftID = ws.ID
                    LEFT JOIN Users creator ON e.CreatedBy = creator.ID
                    LEFT JOIN Users updater ON e.UpdatedBy = updater.ID
                    WHERE e.ID = @EmployeeID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", employeeId)
                };

                DataTable dataTable = ConnectionManager.ExecuteQuery(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                return MapRowToEmployeeDTO(dataTable.Rows[0]);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets an employee by employee number
        /// </summary>
        /// <param name="employeeNumber">Employee number</param>
        /// <returns>EmployeeDTO object</returns>
        public EmployeeDTO GetEmployeeByNumber(string employeeNumber)
        {
            try
            {
                string query = @"
                    SELECT e.ID, e.EmployeeNumber, e.FirstName, e.MiddleName, e.LastName, 
                        e.FullName, e.Gender, e.BirthDate, e.NationalID, e.PassportNumber, 
                        e.MaritalStatus, e.Nationality, e.Religion, e.BloodType, 
                        e.Phone, e.Mobile, e.Email, e.Address, e.EmergencyContact, e.EmergencyPhone, 
                        e.DepartmentID, d.Name AS DepartmentName, e.PositionID, p.Title AS PositionTitle, 
                        e.DirectManagerID, manager.FullName AS DirectManagerName, 
                        e.HireDate, e.ProbationEndDate, e.EmploymentType, 
                        e.ContractStartDate, e.ContractEndDate, e.WorkShiftID, ws.Name AS WorkShiftName, 
                        e.Status, e.TerminationDate, e.TerminationReason, 
                        e.BankName, e.BankBranch, e.BankAccountNumber, e.IBAN, 
                        e.Photo, e.Notes, e.BiometricID, 
                        e.CreatedAt, e.CreatedBy, creator.Username AS CreatedByName, 
                        e.UpdatedAt, e.UpdatedBy, updater.Username AS UpdatedByName
                    FROM Employees e
                    LEFT JOIN Departments d ON e.DepartmentID = d.ID
                    LEFT JOIN Positions p ON e.PositionID = p.ID
                    LEFT JOIN Employees manager ON e.DirectManagerID = manager.ID
                    LEFT JOIN WorkShifts ws ON e.WorkShiftID = ws.ID
                    LEFT JOIN Users creator ON e.CreatedBy = creator.ID
                    LEFT JOIN Users updater ON e.UpdatedBy = updater.ID
                    WHERE e.EmployeeNumber = @EmployeeNumber";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeNumber", employeeNumber)
                };

                DataTable dataTable = ConnectionManager.ExecuteQuery(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                return MapRowToEmployeeDTO(dataTable.Rows[0]);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets an employee by biometric ID
        /// </summary>
        /// <param name="biometricId">Biometric ID</param>
        /// <returns>EmployeeDTO object</returns>
        public EmployeeDTO GetEmployeeByBiometricId(int biometricId)
        {
            try
            {
                string query = @"
                    SELECT e.ID, e.EmployeeNumber, e.FirstName, e.MiddleName, e.LastName, 
                        e.FullName, e.Gender, e.BirthDate, e.NationalID, e.PassportNumber, 
                        e.MaritalStatus, e.Nationality, e.Religion, e.BloodType, 
                        e.Phone, e.Mobile, e.Email, e.Address, e.EmergencyContact, e.EmergencyPhone, 
                        e.DepartmentID, d.Name AS DepartmentName, e.PositionID, p.Title AS PositionTitle, 
                        e.DirectManagerID, manager.FullName AS DirectManagerName, 
                        e.HireDate, e.ProbationEndDate, e.EmploymentType, 
                        e.ContractStartDate, e.ContractEndDate, e.WorkShiftID, ws.Name AS WorkShiftName, 
                        e.Status, e.TerminationDate, e.TerminationReason, 
                        e.BankName, e.BankBranch, e.BankAccountNumber, e.IBAN, 
                        e.Photo, e.Notes, e.BiometricID, 
                        e.CreatedAt, e.CreatedBy, creator.Username AS CreatedByName, 
                        e.UpdatedAt, e.UpdatedBy, updater.Username AS UpdatedByName
                    FROM Employees e
                    LEFT JOIN Departments d ON e.DepartmentID = d.ID
                    LEFT JOIN Positions p ON e.PositionID = p.ID
                    LEFT JOIN Employees manager ON e.DirectManagerID = manager.ID
                    LEFT JOIN WorkShifts ws ON e.WorkShiftID = ws.ID
                    LEFT JOIN Users creator ON e.CreatedBy = creator.ID
                    LEFT JOIN Users updater ON e.UpdatedBy = updater.ID
                    WHERE e.BiometricID = @BiometricID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@BiometricID", biometricId)
                };

                DataTable dataTable = ConnectionManager.ExecuteQuery(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                return MapRowToEmployeeDTO(dataTable.Rows[0]);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Saves an employee (inserts if ID is 0, updates otherwise)
        /// </summary>
        /// <param name="employee">EmployeeDTO object</param>
        /// <param name="userId">User ID performing the operation</param>
        /// <returns>ID of the saved employee</returns>
        public int SaveEmployee(EmployeeDTO employee, int userId)
        {
            try
            {
                string query;
                SqlParameter[] parameters;

                if (employee.ID == 0)
                {
                    // Check if employee number already exists
                    string checkQuery = "SELECT COUNT(*) FROM Employees WHERE EmployeeNumber = @EmployeeNumber";
                    SqlParameter[] checkParams = new SqlParameter[]
                    {
                        new SqlParameter("@EmployeeNumber", employee.EmployeeNumber)
                    };
                    int count = Convert.ToInt32(ConnectionManager.ExecuteScalar(checkQuery, checkParams));

                    if (count > 0)
                    {
                        return -1; // Employee number already exists
                    }

                    // Insert new employee
                    query = @"
                        INSERT INTO Employees (
                            EmployeeNumber, FirstName, MiddleName, LastName, 
                            Gender, BirthDate, NationalID, PassportNumber, 
                            MaritalStatus, Nationality, Religion, BloodType, 
                            Phone, Mobile, Email, Address, EmergencyContact, EmergencyPhone, 
                            DepartmentID, PositionID, DirectManagerID, 
                            HireDate, ProbationEndDate, EmploymentType, 
                            ContractStartDate, ContractEndDate, WorkShiftID, 
                            Status, TerminationDate, TerminationReason, 
                            BankName, BankBranch, BankAccountNumber, IBAN, 
                            Photo, Notes, BiometricID, 
                            CreatedAt, CreatedBy)
                        VALUES (
                            @EmployeeNumber, @FirstName, @MiddleName, @LastName, 
                            @Gender, @BirthDate, @NationalID, @PassportNumber, 
                            @MaritalStatus, @Nationality, @Religion, @BloodType, 
                            @Phone, @Mobile, @Email, @Address, @EmergencyContact, @EmergencyPhone, 
                            @DepartmentID, @PositionID, @DirectManagerID, 
                            @HireDate, @ProbationEndDate, @EmploymentType, 
                            @ContractStartDate, @ContractEndDate, @WorkShiftID, 
                            @Status, @TerminationDate, @TerminationReason, 
                            @BankName, @BankBranch, @BankAccountNumber, @IBAN, 
                            @Photo, @Notes, @BiometricID, 
                            @CreatedAt, @CreatedBy);
                        SELECT SCOPE_IDENTITY();";

                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@EmployeeNumber", employee.EmployeeNumber),
                        new SqlParameter("@FirstName", employee.FirstName),
                        new SqlParameter("@MiddleName", (object)employee.MiddleName ?? DBNull.Value),
                        new SqlParameter("@LastName", employee.LastName),
                        new SqlParameter("@Gender", (object)employee.Gender ?? DBNull.Value),
                        new SqlParameter("@BirthDate", (object)employee.BirthDate ?? DBNull.Value),
                        new SqlParameter("@NationalID", (object)employee.NationalID ?? DBNull.Value),
                        new SqlParameter("@PassportNumber", (object)employee.PassportNumber ?? DBNull.Value),
                        new SqlParameter("@MaritalStatus", (object)employee.MaritalStatus ?? DBNull.Value),
                        new SqlParameter("@Nationality", (object)employee.Nationality ?? DBNull.Value),
                        new SqlParameter("@Religion", (object)employee.Religion ?? DBNull.Value),
                        new SqlParameter("@BloodType", (object)employee.BloodType ?? DBNull.Value),
                        
                        new SqlParameter("@Phone", (object)employee.Phone ?? DBNull.Value),
                        new SqlParameter("@Mobile", (object)employee.Mobile ?? DBNull.Value),
                        new SqlParameter("@Email", (object)employee.Email ?? DBNull.Value),
                        new SqlParameter("@Address", (object)employee.Address ?? DBNull.Value),
                        new SqlParameter("@EmergencyContact", (object)employee.EmergencyContact ?? DBNull.Value),
                        new SqlParameter("@EmergencyPhone", (object)employee.EmergencyPhone ?? DBNull.Value),
                        
                        new SqlParameter("@DepartmentID", (object)employee.DepartmentID ?? DBNull.Value),
                        new SqlParameter("@PositionID", (object)employee.PositionID ?? DBNull.Value),
                        new SqlParameter("@DirectManagerID", (object)employee.DirectManagerID ?? DBNull.Value),
                        new SqlParameter("@HireDate", employee.HireDate),
                        new SqlParameter("@ProbationEndDate", (object)employee.ProbationEndDate ?? DBNull.Value),
                        new SqlParameter("@EmploymentType", (object)employee.EmploymentType ?? DBNull.Value),
                        new SqlParameter("@ContractStartDate", (object)employee.ContractStartDate ?? DBNull.Value),
                        new SqlParameter("@ContractEndDate", (object)employee.ContractEndDate ?? DBNull.Value),
                        new SqlParameter("@WorkShiftID", (object)employee.WorkShiftID ?? DBNull.Value),
                        
                        new SqlParameter("@Status", (object)employee.Status ?? "Active"),
                        new SqlParameter("@TerminationDate", (object)employee.TerminationDate ?? DBNull.Value),
                        new SqlParameter("@TerminationReason", (object)employee.TerminationReason ?? DBNull.Value),
                        
                        new SqlParameter("@BankName", (object)employee.BankName ?? DBNull.Value),
                        new SqlParameter("@BankBranch", (object)employee.BankBranch ?? DBNull.Value),
                        new SqlParameter("@BankAccountNumber", (object)employee.BankAccountNumber ?? DBNull.Value),
                        new SqlParameter("@IBAN", (object)employee.IBAN ?? DBNull.Value),
                        
                        new SqlParameter("@Photo", (object)employee.Photo ?? DBNull.Value),
                        new SqlParameter("@Notes", (object)employee.Notes ?? DBNull.Value),
                        new SqlParameter("@BiometricID", (object)employee.BiometricID ?? DBNull.Value),
                        
                        new SqlParameter("@CreatedAt", DateTime.Now),
                        new SqlParameter("@CreatedBy", (object)userId ?? DBNull.Value)
                    };

                    object result = ConnectionManager.ExecuteScalar(query, parameters);
                    employee.ID = Convert.ToInt32(result);

                    // Log activity
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    activityRepo.LogActivity(userId, "Add", "Employees", 
                        $"Added new employee: {employee.FullName} ({employee.EmployeeNumber})", employee.ID, null, null);

                    return employee.ID;
                }
                else
                {
                    // Get existing employee for activity log
                    EmployeeDTO existingEmployee = GetEmployeeById(employee.ID);
                    string oldValues = Newtonsoft.Json.JsonConvert.SerializeObject(existingEmployee);

                    // Check if employee number changed and if new number already exists
                    if (existingEmployee.EmployeeNumber != employee.EmployeeNumber)
                    {
                        string checkQuery = "SELECT COUNT(*) FROM Employees WHERE EmployeeNumber = @EmployeeNumber AND ID <> @EmployeeID";
                        SqlParameter[] checkParams = new SqlParameter[]
                        {
                            new SqlParameter("@EmployeeNumber", employee.EmployeeNumber),
                            new SqlParameter("@EmployeeID", employee.ID)
                        };
                        int count = Convert.ToInt32(ConnectionManager.ExecuteScalar(checkQuery, checkParams));

                        if (count > 0)
                        {
                            return -1; // Employee number already exists
                        }
                    }

                    // Update existing employee
                    query = @"
                        UPDATE Employees
                        SET EmployeeNumber = @EmployeeNumber,
                            FirstName = @FirstName,
                            MiddleName = @MiddleName,
                            LastName = @LastName,
                            Gender = @Gender,
                            BirthDate = @BirthDate,
                            NationalID = @NationalID,
                            PassportNumber = @PassportNumber,
                            MaritalStatus = @MaritalStatus,
                            Nationality = @Nationality,
                            Religion = @Religion,
                            BloodType = @BloodType,
                            
                            Phone = @Phone,
                            Mobile = @Mobile,
                            Email = @Email,
                            Address = @Address,
                            EmergencyContact = @EmergencyContact,
                            EmergencyPhone = @EmergencyPhone,
                            
                            DepartmentID = @DepartmentID,
                            PositionID = @PositionID,
                            DirectManagerID = @DirectManagerID,
                            HireDate = @HireDate,
                            ProbationEndDate = @ProbationEndDate,
                            EmploymentType = @EmploymentType,
                            ContractStartDate = @ContractStartDate,
                            ContractEndDate = @ContractEndDate,
                            WorkShiftID = @WorkShiftID,
                            
                            Status = @Status,
                            TerminationDate = @TerminationDate,
                            TerminationReason = @TerminationReason,
                            
                            BankName = @BankName,
                            BankBranch = @BankBranch,
                            BankAccountNumber = @BankAccountNumber,
                            IBAN = @IBAN,
                            
                            Notes = @Notes,
                            BiometricID = @BiometricID,
                            
                            UpdatedAt = @UpdatedAt,
                            UpdatedBy = @UpdatedBy
                        WHERE ID = @ID";

                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@ID", employee.ID),
                        new SqlParameter("@EmployeeNumber", employee.EmployeeNumber),
                        new SqlParameter("@FirstName", employee.FirstName),
                        new SqlParameter("@MiddleName", (object)employee.MiddleName ?? DBNull.Value),
                        new SqlParameter("@LastName", employee.LastName),
                        new SqlParameter("@Gender", (object)employee.Gender ?? DBNull.Value),
                        new SqlParameter("@BirthDate", (object)employee.BirthDate ?? DBNull.Value),
                        new SqlParameter("@NationalID", (object)employee.NationalID ?? DBNull.Value),
                        new SqlParameter("@PassportNumber", (object)employee.PassportNumber ?? DBNull.Value),
                        new SqlParameter("@MaritalStatus", (object)employee.MaritalStatus ?? DBNull.Value),
                        new SqlParameter("@Nationality", (object)employee.Nationality ?? DBNull.Value),
                        new SqlParameter("@Religion", (object)employee.Religion ?? DBNull.Value),
                        new SqlParameter("@BloodType", (object)employee.BloodType ?? DBNull.Value),
                        
                        new SqlParameter("@Phone", (object)employee.Phone ?? DBNull.Value),
                        new SqlParameter("@Mobile", (object)employee.Mobile ?? DBNull.Value),
                        new SqlParameter("@Email", (object)employee.Email ?? DBNull.Value),
                        new SqlParameter("@Address", (object)employee.Address ?? DBNull.Value),
                        new SqlParameter("@EmergencyContact", (object)employee.EmergencyContact ?? DBNull.Value),
                        new SqlParameter("@EmergencyPhone", (object)employee.EmergencyPhone ?? DBNull.Value),
                        
                        new SqlParameter("@DepartmentID", (object)employee.DepartmentID ?? DBNull.Value),
                        new SqlParameter("@PositionID", (object)employee.PositionID ?? DBNull.Value),
                        new SqlParameter("@DirectManagerID", (object)employee.DirectManagerID ?? DBNull.Value),
                        new SqlParameter("@HireDate", employee.HireDate),
                        new SqlParameter("@ProbationEndDate", (object)employee.ProbationEndDate ?? DBNull.Value),
                        new SqlParameter("@EmploymentType", (object)employee.EmploymentType ?? DBNull.Value),
                        new SqlParameter("@ContractStartDate", (object)employee.ContractStartDate ?? DBNull.Value),
                        new SqlParameter("@ContractEndDate", (object)employee.ContractEndDate ?? DBNull.Value),
                        new SqlParameter("@WorkShiftID", (object)employee.WorkShiftID ?? DBNull.Value),
                        
                        new SqlParameter("@Status", (object)employee.Status ?? "Active"),
                        new SqlParameter("@TerminationDate", (object)employee.TerminationDate ?? DBNull.Value),
                        new SqlParameter("@TerminationReason", (object)employee.TerminationReason ?? DBNull.Value),
                        
                        new SqlParameter("@BankName", (object)employee.BankName ?? DBNull.Value),
                        new SqlParameter("@BankBranch", (object)employee.BankBranch ?? DBNull.Value),
                        new SqlParameter("@BankAccountNumber", (object)employee.BankAccountNumber ?? DBNull.Value),
                        new SqlParameter("@IBAN", (object)employee.IBAN ?? DBNull.Value),
                        
                        new SqlParameter("@Notes", (object)employee.Notes ?? DBNull.Value),
                        new SqlParameter("@BiometricID", (object)employee.BiometricID ?? DBNull.Value),
                        
                        new SqlParameter("@UpdatedAt", DateTime.Now),
                        new SqlParameter("@UpdatedBy", (object)userId ?? DBNull.Value)
                    };

                    int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                    // Log activity
                    if (result > 0)
                    {
                        ActivityLogRepository activityRepo = new ActivityLogRepository();
                        string newValues = Newtonsoft.Json.JsonConvert.SerializeObject(employee);
                        activityRepo.LogActivity(userId, "Edit", "Employees", 
                            $"Updated employee: {employee.FullName} ({employee.EmployeeNumber})", employee.ID, oldValues, newValues);
                    }

                    return employee.ID;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Updates an employee's photo
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <param name="photo">Photo as byte array</param>
        /// <param name="userId">User ID performing the operation</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool UpdateEmployeePhoto(int employeeId, byte[] photo, int userId)
        {
            try
            {
                string query = @"
                    UPDATE Employees
                    SET Photo = @Photo,
                        UpdatedAt = @UpdatedAt,
                        UpdatedBy = @UpdatedBy
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", employeeId),
                    new SqlParameter("@Photo", (object)photo ?? DBNull.Value),
                    new SqlParameter("@UpdatedAt", DateTime.Now),
                    new SqlParameter("@UpdatedBy", (object)userId ?? DBNull.Value)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                // Log activity
                if (result > 0)
                {
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    activityRepo.LogActivity(userId, "Edit", "Employees", 
                        $"Updated photo for employee ID: {employeeId}", employeeId, null, null);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Updates an employee's biometric ID
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <param name="biometricId">Biometric ID</param>
        /// <param name="userId">User ID performing the operation</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool UpdateEmployeeBiometricId(int employeeId, int biometricId, int userId)
        {
            try
            {
                // Check if biometric ID is already assigned to another employee
                if (biometricId > 0)
                {
                    string checkQuery = "SELECT COUNT(*) FROM Employees WHERE BiometricID = @BiometricID AND ID <> @EmployeeID";
                    SqlParameter[] checkParams = new SqlParameter[]
                    {
                        new SqlParameter("@BiometricID", biometricId),
                        new SqlParameter("@EmployeeID", employeeId)
                    };
                    int count = Convert.ToInt32(ConnectionManager.ExecuteScalar(checkQuery, checkParams));

                    if (count > 0)
                    {
                        return false; // Biometric ID already assigned
                    }
                }

                string query = @"
                    UPDATE Employees
                    SET BiometricID = @BiometricID,
                        UpdatedAt = @UpdatedAt,
                        UpdatedBy = @UpdatedBy
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", employeeId),
                    new SqlParameter("@BiometricID", (object)biometricId != null && biometricId > 0 ? biometricId : DBNull.Value),
                    new SqlParameter("@UpdatedAt", DateTime.Now),
                    new SqlParameter("@UpdatedBy", (object)userId ?? DBNull.Value)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                // Log activity
                if (result > 0)
                {
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    activityRepo.LogActivity(userId, "Edit", "Employees", 
                        $"Updated biometric ID for employee ID: {employeeId} to {biometricId}", employeeId, null, null);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Terminates an employee
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <param name="terminationDate">Termination date</param>
        /// <param name="terminationReason">Termination reason</param>
        /// <param name="userId">User ID performing the operation</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool TerminateEmployee(int employeeId, DateTime terminationDate, string terminationReason, int userId)
        {
            try
            {
                // Get existing employee for activity log
                EmployeeDTO existingEmployee = GetEmployeeById(employeeId);
                if (existingEmployee == null)
                {
                    return false;
                }

                string oldValues = Newtonsoft.Json.JsonConvert.SerializeObject(existingEmployee);

                string query = @"
                    UPDATE Employees
                    SET Status = 'Terminated',
                        TerminationDate = @TerminationDate,
                        TerminationReason = @TerminationReason,
                        UpdatedAt = @UpdatedAt,
                        UpdatedBy = @UpdatedBy
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", employeeId),
                    new SqlParameter("@TerminationDate", terminationDate),
                    new SqlParameter("@TerminationReason", (object)terminationReason ?? DBNull.Value),
                    new SqlParameter("@UpdatedAt", DateTime.Now),
                    new SqlParameter("@UpdatedBy", (object)userId ?? DBNull.Value)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                // Log activity
                if (result > 0)
                {
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    
                    // Get updated employee for new values
                    EmployeeDTO updatedEmployee = GetEmployeeById(employeeId);
                    string newValues = Newtonsoft.Json.JsonConvert.SerializeObject(updatedEmployee);
                    
                    activityRepo.LogActivity(userId, "Terminate", "Employees", 
                        $"Terminated employee: {existingEmployee.FullName} ({existingEmployee.EmployeeNumber})", employeeId, oldValues, newValues);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Gets employees for a dropdown list
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive employees</param>
        /// <returns>DataTable with employee ID, number, and name</returns>
        public DataTable GetEmployeesForDropDown(bool includeInactive = false)
        {
            try
            {
                string query = @"
                    SELECT ID, 
                           EmployeeNumber + ' - ' + FullName AS Name
                    FROM Employees";

                if (!includeInactive)
                {
                    query += " WHERE Status <> 'Terminated'";
                }

                query += " ORDER BY EmployeeNumber";

                return ConnectionManager.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Gets managers for a dropdown list
        /// </summary>
        /// <returns>DataTable with employee ID, number, and name</returns>
        public DataTable GetManagersForDropDown()
        {
            try
            {
                string query = @"
                    SELECT DISTINCT e.ID, 
                           e.EmployeeNumber + ' - ' + e.FullName AS Name
                    FROM Employees e
                    INNER JOIN Positions p ON e.PositionID = p.ID
                    WHERE p.IsManagerPosition = 1
                      AND e.Status <> 'Terminated'
                    ORDER BY e.EmployeeNumber";

                return ConnectionManager.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Gets employee documents
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>DataTable with employee documents</returns>
        public DataTable GetEmployeeDocuments(int employeeId)
        {
            try
            {
                string query = @"
                    SELECT d.ID, d.EmployeeID, d.DocumentType, d.DocumentTitle, 
                           d.DocumentNumber, d.IssueDate, d.ExpiryDate, d.IssuedBy, 
                           CASE WHEN d.DocumentFile IS NOT NULL THEN 1 ELSE 0 END AS HasFile,
                           d.DocumentPath, d.Notes, d.CreatedAt, 
                           creator.Username AS CreatedByName, d.UpdatedAt,
                           updater.Username AS UpdatedByName
                    FROM EmployeeDocuments d
                    LEFT JOIN Users creator ON d.CreatedBy = creator.ID
                    LEFT JOIN Users updater ON d.UpdatedBy = updater.ID
                    WHERE d.EmployeeID = @EmployeeID
                    ORDER BY d.DocumentType, d.ExpiryDate DESC";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", employeeId)
                };

                return ConnectionManager.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Gets employee document by ID
        /// </summary>
        /// <param name="documentId">Document ID</param>
        /// <returns>EmployeeDocumentDTO object</returns>
        public EmployeeDocumentDTO GetEmployeeDocumentById(int documentId)
        {
            try
            {
                string query = @"
                    SELECT d.ID, d.EmployeeID, e.FullName AS EmployeeName, 
                           d.DocumentType, d.DocumentTitle, d.DocumentNumber, 
                           d.IssueDate, d.ExpiryDate, d.IssuedBy, 
                           d.DocumentFile, d.DocumentPath, d.Notes, 
                           d.CreatedAt, d.CreatedBy, creator.Username AS CreatedByName, 
                           d.UpdatedAt, d.UpdatedBy, updater.Username AS UpdatedByName
                    FROM EmployeeDocuments d
                    LEFT JOIN Employees e ON d.EmployeeID = e.ID
                    LEFT JOIN Users creator ON d.CreatedBy = creator.ID
                    LEFT JOIN Users updater ON d.UpdatedBy = updater.ID
                    WHERE d.ID = @DocumentID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DocumentID", documentId)
                };

                DataTable dataTable = ConnectionManager.ExecuteQuery(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                DataRow row = dataTable.Rows[0];

                EmployeeDocumentDTO document = new EmployeeDocumentDTO
                {
                    ID = Convert.ToInt32(row["ID"]),
                    EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                    EmployeeName = row["EmployeeName"].ToString(),
                    DocumentType = row["DocumentType"].ToString(),
                    DocumentTitle = row["DocumentTitle"].ToString(),
                    DocumentNumber = row["DocumentNumber"] != DBNull.Value ? row["DocumentNumber"].ToString() : null,
                    IssueDate = row["IssueDate"] != DBNull.Value ? Convert.ToDateTime(row["IssueDate"]) : (DateTime?)null,
                    ExpiryDate = row["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(row["ExpiryDate"]) : (DateTime?)null,
                    IssuedBy = row["IssuedBy"] != DBNull.Value ? row["IssuedBy"].ToString() : null,
                    DocumentFile = row["DocumentFile"] != DBNull.Value ? (byte[])row["DocumentFile"] : null,
                    DocumentPath = row["DocumentPath"] != DBNull.Value ? row["DocumentPath"].ToString() : null,
                    Notes = row["Notes"] != DBNull.Value ? row["Notes"].ToString() : null,
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : (int?)null,
                    CreatedByName = row["CreatedByName"] != DBNull.Value ? row["CreatedByName"].ToString() : null,
                    UpdatedAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : (DateTime?)null,
                    UpdatedBy = row["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(row["UpdatedBy"]) : (int?)null,
                    UpdatedByName = row["UpdatedByName"] != DBNull.Value ? row["UpdatedByName"].ToString() : null
                };

                return document;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Saves an employee document
        /// </summary>
        /// <param name="document">EmployeeDocumentDTO object</param>
        /// <param name="userId">User ID performing the operation</param>
        /// <returns>ID of the saved document</returns>
        public int SaveEmployeeDocument(EmployeeDocumentDTO document, int userId)
        {
            try
            {
                string query;
                SqlParameter[] parameters;

                if (document.ID == 0)
                {
                    // Insert new document
                    query = @"
                        INSERT INTO EmployeeDocuments (
                            EmployeeID, DocumentType, DocumentTitle, DocumentNumber, 
                            IssueDate, ExpiryDate, IssuedBy, DocumentFile, 
                            DocumentPath, Notes, CreatedAt, CreatedBy)
                        VALUES (
                            @EmployeeID, @DocumentType, @DocumentTitle, @DocumentNumber, 
                            @IssueDate, @ExpiryDate, @IssuedBy, @DocumentFile, 
                            @DocumentPath, @Notes, @CreatedAt, @CreatedBy);
                        SELECT SCOPE_IDENTITY();";

                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@EmployeeID", document.EmployeeID),
                        new SqlParameter("@DocumentType", document.DocumentType),
                        new SqlParameter("@DocumentTitle", document.DocumentTitle),
                        new SqlParameter("@DocumentNumber", (object)document.DocumentNumber ?? DBNull.Value),
                        new SqlParameter("@IssueDate", (object)document.IssueDate ?? DBNull.Value),
                        new SqlParameter("@ExpiryDate", (object)document.ExpiryDate ?? DBNull.Value),
                        new SqlParameter("@IssuedBy", (object)document.IssuedBy ?? DBNull.Value),
                        new SqlParameter("@DocumentFile", (object)document.DocumentFile ?? DBNull.Value),
                        new SqlParameter("@DocumentPath", (object)document.DocumentPath ?? DBNull.Value),
                        new SqlParameter("@Notes", (object)document.Notes ?? DBNull.Value),
                        new SqlParameter("@CreatedAt", DateTime.Now),
                        new SqlParameter("@CreatedBy", (object)userId ?? DBNull.Value)
                    };

                    object result = ConnectionManager.ExecuteScalar(query, parameters);
                    document.ID = Convert.ToInt32(result);

                    // Log activity
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    activityRepo.LogActivity(userId, "Add", "EmployeeDocuments", 
                        $"Added new document: {document.DocumentTitle} for employee ID: {document.EmployeeID}", document.ID, null, null);

                    return document.ID;
                }
                else
                {
                    // Get existing document for activity log
                    EmployeeDocumentDTO existingDocument = GetEmployeeDocumentById(document.ID);
                    string oldValues = Newtonsoft.Json.JsonConvert.SerializeObject(existingDocument);

                    // Update existing document
                    query = @"
                        UPDATE EmployeeDocuments
                        SET DocumentType = @DocumentType,
                            DocumentTitle = @DocumentTitle,
                            DocumentNumber = @DocumentNumber,
                            IssueDate = @IssueDate,
                            ExpiryDate = @ExpiryDate,
                            IssuedBy = @IssuedBy,
                            DocumentPath = @DocumentPath,
                            Notes = @Notes,
                            UpdatedAt = @UpdatedAt,
                            UpdatedBy = @UpdatedBy
                        WHERE ID = @ID";

                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@ID", document.ID),
                        new SqlParameter("@DocumentType", document.DocumentType),
                        new SqlParameter("@DocumentTitle", document.DocumentTitle),
                        new SqlParameter("@DocumentNumber", (object)document.DocumentNumber ?? DBNull.Value),
                        new SqlParameter("@IssueDate", (object)document.IssueDate ?? DBNull.Value),
                        new SqlParameter("@ExpiryDate", (object)document.ExpiryDate ?? DBNull.Value),
                        new SqlParameter("@IssuedBy", (object)document.IssuedBy ?? DBNull.Value),
                        new SqlParameter("@DocumentPath", (object)document.DocumentPath ?? DBNull.Value),
                        new SqlParameter("@Notes", (object)document.Notes ?? DBNull.Value),
                        new SqlParameter("@UpdatedAt", DateTime.Now),
                        new SqlParameter("@UpdatedBy", (object)userId ?? DBNull.Value)
                    };

                    int updateResult = ConnectionManager.ExecuteNonQuery(query, parameters);

                    // Update document file if provided separately
                    if (document.DocumentFile != null && document.DocumentFile.Length > 0)
                    {
                        string fileQuery = @"
                            UPDATE EmployeeDocuments
                            SET DocumentFile = @DocumentFile,
                                UpdatedAt = @UpdatedAt,
                                UpdatedBy = @UpdatedBy
                            WHERE ID = @ID";

                        SqlParameter[] fileParameters = new SqlParameter[]
                        {
                            new SqlParameter("@ID", document.ID),
                            new SqlParameter("@DocumentFile", document.DocumentFile),
                            new SqlParameter("@UpdatedAt", DateTime.Now),
                            new SqlParameter("@UpdatedBy", (object)userId ?? DBNull.Value)
                        };

                        ConnectionManager.ExecuteNonQuery(fileQuery, fileParameters);
                    }

                    // Log activity
                    if (updateResult > 0)
                    {
                        ActivityLogRepository activityRepo = new ActivityLogRepository();
                        string newValues = Newtonsoft.Json.JsonConvert.SerializeObject(document);
                        activityRepo.LogActivity(userId, "Edit", "EmployeeDocuments", 
                            $"Updated document: {document.DocumentTitle} for employee ID: {document.EmployeeID}", document.ID, oldValues, newValues);
                    }

                    return document.ID;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Deletes an employee document
        /// </summary>
        /// <param name="documentId">Document ID</param>
        /// <param name="userId">User ID performing the operation</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool DeleteEmployeeDocument(int documentId, int userId)
        {
            try
            {
                // Get document details for activity log
                EmployeeDocumentDTO document = GetEmployeeDocumentById(documentId);
                if (document == null)
                {
                    return false;
                }

                string oldValues = Newtonsoft.Json.JsonConvert.SerializeObject(document);

                // Delete the document
                string query = "DELETE FROM EmployeeDocuments WHERE ID = @DocumentID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DocumentID", documentId)
                };

                int result = ConnectionManager.ExecuteNonQuery(query, parameters);

                // Log activity
                if (result > 0)
                {
                    ActivityLogRepository activityRepo = new ActivityLogRepository();
                    activityRepo.LogActivity(userId, "Delete", "EmployeeDocuments", 
                        $"Deleted document: {document.DocumentTitle} for employee ID: {document.EmployeeID}", documentId, oldValues, null);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Gets employee transfers
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>DataTable with employee transfers</returns>
        public DataTable GetEmployeeTransfers(int employeeId)
        {
            try
            {
                string query = @"
                    SELECT t.ID, t.EmployeeID, t.TransferType, 
                           t.FromDepartmentID, fromDept.Name AS FromDepartmentName,
                           t.ToDepartmentID, toDept.Name AS ToDepartmentName,
                           t.FromPositionID, fromPos.Title AS FromPositionTitle,
                           t.ToPositionID, toPos.Title AS ToPositionTitle,
                           t.EffectiveDate, t.Reason, t.Notes, 
                           t.CreatedAt, creator.Username AS CreatedByName, 
                           t.UpdatedAt, updater.Username AS UpdatedByName
                    FROM EmployeeTransfers t
                    LEFT JOIN Departments fromDept ON t.FromDepartmentID = fromDept.ID
                    LEFT JOIN Departments toDept ON t.ToDepartmentID = toDept.ID
                    LEFT JOIN Positions fromPos ON t.FromPositionID = fromPos.ID
                    LEFT JOIN Positions toPos ON t.ToPositionID = toPos.ID
                    LEFT JOIN Users creator ON t.CreatedBy = creator.ID
                    LEFT JOIN Users updater ON t.UpdatedBy = updater.ID
                    WHERE t.EmployeeID = @EmployeeID
                    ORDER BY t.EffectiveDate DESC";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", employeeId)
                };

                return ConnectionManager.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Saves an employee transfer
        /// </summary>
        /// <param name="transfer">EmployeeTransferDTO object</param>
        /// <param name="userId">User ID performing the operation</param>
        /// <returns>ID of the saved transfer</returns>
        public int SaveEmployeeTransfer(EmployeeTransferDTO transfer, int userId)
        {
            try
            {
                // Get current employee details
                EmployeeDTO employee = GetEmployeeById(transfer.EmployeeID);
                if (employee == null)
                {
                    return 0;
                }

                // Start a transaction
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Set from department and position if not provided
                        if (transfer.FromDepartmentID == null)
                        {
                            transfer.FromDepartmentID = employee.DepartmentID;
                            transfer.FromDepartmentName = employee.DepartmentName;
                        }

                        if (transfer.FromPositionID == null)
                        {
                            transfer.FromPositionID = employee.PositionID;
                            transfer.FromPositionTitle = employee.PositionTitle;
                        }

                        // Insert the transfer record
                        string transferQuery = @"
                            INSERT INTO EmployeeTransfers (
                                EmployeeID, TransferType, FromDepartmentID, ToDepartmentID, 
                                FromPositionID, ToPositionID, EffectiveDate, Reason, 
                                Notes, CreatedAt, CreatedBy)
                            VALUES (
                                @EmployeeID, @TransferType, @FromDepartmentID, @ToDepartmentID, 
                                @FromPositionID, @ToPositionID, @EffectiveDate, @Reason, 
                                @Notes, @CreatedAt, @CreatedBy);
                            SELECT SCOPE_IDENTITY();";

                        SqlCommand transferCmd = new SqlCommand(transferQuery, connection, transaction);
                        transferCmd.Parameters.AddWithValue("@EmployeeID", transfer.EmployeeID);
                        transferCmd.Parameters.AddWithValue("@TransferType", transfer.TransferType);
                        transferCmd.Parameters.AddWithValue("@FromDepartmentID", (object)transfer.FromDepartmentID ?? DBNull.Value);
                        transferCmd.Parameters.AddWithValue("@ToDepartmentID", (object)transfer.ToDepartmentID ?? DBNull.Value);
                        transferCmd.Parameters.AddWithValue("@FromPositionID", (object)transfer.FromPositionID ?? DBNull.Value);
                        transferCmd.Parameters.AddWithValue("@ToPositionID", (object)transfer.ToPositionID ?? DBNull.Value);
                        transferCmd.Parameters.AddWithValue("@EffectiveDate", transfer.EffectiveDate);
                        transferCmd.Parameters.AddWithValue("@Reason", (object)transfer.Reason ?? DBNull.Value);
                        transferCmd.Parameters.AddWithValue("@Notes", (object)transfer.Notes ?? DBNull.Value);
                        transferCmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        transferCmd.Parameters.AddWithValue("@CreatedBy", (object)userId ?? DBNull.Value);

                        int transferId = Convert.ToInt32(transferCmd.ExecuteScalar());
                        transfer.ID = transferId;

                        // Update the employee's department and/or position
                        string updateQuery = "UPDATE Employees SET ";
                        bool needsComma = false;
                        SqlCommand updateCmd = new SqlCommand("", connection, transaction);

                        if (transfer.ToDepartmentID.HasValue)
                        {
                            updateQuery += "DepartmentID = @ToDepartmentID";
                            updateCmd.Parameters.AddWithValue("@ToDepartmentID", transfer.ToDepartmentID.Value);
                            needsComma = true;
                        }

                        if (transfer.ToPositionID.HasValue)
                        {
                            if (needsComma) updateQuery += ", ";
                            updateQuery += "PositionID = @ToPositionID";
                            updateCmd.Parameters.AddWithValue("@ToPositionID", transfer.ToPositionID.Value);
                            needsComma = true;
                        }

                        if (needsComma)
                        {
                            updateQuery += ", UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy ";
                            updateCmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                            updateCmd.Parameters.AddWithValue("@UpdatedBy", (object)userId ?? DBNull.Value);
                            updateQuery += "WHERE ID = @EmployeeID";
                            updateCmd.Parameters.AddWithValue("@EmployeeID", transfer.EmployeeID);

                            updateCmd.CommandText = updateQuery;
                            updateCmd.ExecuteNonQuery();
                        }

                        // Commit transaction
                        transaction.Commit();

                        // Log activity
                        ActivityLogRepository activityRepo = new ActivityLogRepository();
                        activityRepo.LogActivity(userId, "Add", "EmployeeTransfers", 
                            $"Added new transfer for employee: {employee.FullName} ({employee.EmployeeNumber})", transferId, null, null);

                        return transferId;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        LogManager.LogException(ex);
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Gets total employees count
        /// </summary>
        /// <param name="activeOnly">Whether to count only active employees</param>
        /// <returns>Number of employees</returns>
        public int GetEmployeesCount(bool activeOnly = true)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Employees";
                
                if (activeOnly)
                {
                    query += " WHERE Status <> 'Terminated'";
                }
                
                return Convert.ToInt32(ConnectionManager.ExecuteScalar(query));
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Gets employees count by department
        /// </summary>
        /// <returns>DataTable with department ID, name, and employee count</returns>
        public DataTable GetEmployeeCountByDepartment()
        {
            try
            {
                string query = @"
                    SELECT d.ID AS DepartmentID, d.Name AS DepartmentName, 
                           COUNT(e.ID) AS EmployeeCount
                    FROM Departments d
                    LEFT JOIN Employees e ON d.ID = e.DepartmentID AND e.Status <> 'Terminated'
                    GROUP BY d.ID, d.Name
                    ORDER BY d.Name";

                return ConnectionManager.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Maps a DataRow to an EmployeeDTO object
        /// </summary>
        /// <param name="row">DataRow to map</param>
        /// <returns>EmployeeDTO object</returns>
        private EmployeeDTO MapRowToEmployeeDTO(DataRow row)
        {
            return new EmployeeDTO
            {
                ID = Convert.ToInt32(row["ID"]),
                EmployeeNumber = row["EmployeeNumber"].ToString(),
                FirstName = row["FirstName"].ToString(),
                MiddleName = row["MiddleName"] != DBNull.Value ? row["MiddleName"].ToString() : null,
                LastName = row["LastName"].ToString(),
                FullName = row["FullName"].ToString(),
                Gender = row["Gender"] != DBNull.Value ? row["Gender"].ToString() : null,
                BirthDate = row["BirthDate"] != DBNull.Value ? Convert.ToDateTime(row["BirthDate"]) : (DateTime?)null,
                NationalID = row["NationalID"] != DBNull.Value ? row["NationalID"].ToString() : null,
                PassportNumber = row["PassportNumber"] != DBNull.Value ? row["PassportNumber"].ToString() : null,
                MaritalStatus = row["MaritalStatus"] != DBNull.Value ? row["MaritalStatus"].ToString() : null,
                Nationality = row["Nationality"] != DBNull.Value ? row["Nationality"].ToString() : null,
                Religion = row["Religion"] != DBNull.Value ? row["Religion"].ToString() : null,
                BloodType = row["BloodType"] != DBNull.Value ? row["BloodType"].ToString() : null,
                
                Phone = row["Phone"] != DBNull.Value ? row["Phone"].ToString() : null,
                Mobile = row["Mobile"] != DBNull.Value ? row["Mobile"].ToString() : null,
                Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                Address = row["Address"] != DBNull.Value ? row["Address"].ToString() : null,
                EmergencyContact = row["EmergencyContact"] != DBNull.Value ? row["EmergencyContact"].ToString() : null,
                EmergencyPhone = row["EmergencyPhone"] != DBNull.Value ? row["EmergencyPhone"].ToString() : null,
                
                DepartmentID = row["DepartmentID"] != DBNull.Value ? Convert.ToInt32(row["DepartmentID"]) : (int?)null,
                DepartmentName = row["DepartmentName"] != DBNull.Value ? row["DepartmentName"].ToString() : null,
                PositionID = row["PositionID"] != DBNull.Value ? Convert.ToInt32(row["PositionID"]) : (int?)null,
                PositionTitle = row["PositionTitle"] != DBNull.Value ? row["PositionTitle"].ToString() : null,
                DirectManagerID = row["DirectManagerID"] != DBNull.Value ? Convert.ToInt32(row["DirectManagerID"]) : (int?)null,
                DirectManagerName = row["DirectManagerName"] != DBNull.Value ? row["DirectManagerName"].ToString() : null,
                HireDate = Convert.ToDateTime(row["HireDate"]),
                ProbationEndDate = row["ProbationEndDate"] != DBNull.Value ? Convert.ToDateTime(row["ProbationEndDate"]) : (DateTime?)null,
                EmploymentType = row["EmploymentType"] != DBNull.Value ? row["EmploymentType"].ToString() : null,
                ContractStartDate = row["ContractStartDate"] != DBNull.Value ? Convert.ToDateTime(row["ContractStartDate"]) : (DateTime?)null,
                ContractEndDate = row["ContractEndDate"] != DBNull.Value ? Convert.ToDateTime(row["ContractEndDate"]) : (DateTime?)null,
                WorkShiftID = row["WorkShiftID"] != DBNull.Value ? Convert.ToInt32(row["WorkShiftID"]) : (int?)null,
                WorkShiftName = row["WorkShiftName"] != DBNull.Value ? row["WorkShiftName"].ToString() : null,
                
                Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : null,
                TerminationDate = row["TerminationDate"] != DBNull.Value ? Convert.ToDateTime(row["TerminationDate"]) : (DateTime?)null,
                TerminationReason = row["TerminationReason"] != DBNull.Value ? row["TerminationReason"].ToString() : null,
                
                BankName = row["BankName"] != DBNull.Value ? row["BankName"].ToString() : null,
                BankBranch = row["BankBranch"] != DBNull.Value ? row["BankBranch"].ToString() : null,
                BankAccountNumber = row["BankAccountNumber"] != DBNull.Value ? row["BankAccountNumber"].ToString() : null,
                IBAN = row["IBAN"] != DBNull.Value ? row["IBAN"].ToString() : null,
                
                Photo = row["Photo"] != DBNull.Value ? (byte[])row["Photo"] : null,
                Notes = row["Notes"] != DBNull.Value ? row["Notes"].ToString() : null,
                BiometricID = row["BiometricID"] != DBNull.Value ? Convert.ToInt32(row["BiometricID"]) : (int?)null,
                
                CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : (int?)null,
                CreatedByName = row["CreatedByName"] != DBNull.Value ? row["CreatedByName"].ToString() : null,
                UpdatedAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : (DateTime?)null,
                UpdatedBy = row["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(row["UpdatedBy"]) : (int?)null,
                UpdatedByName = row["UpdatedByName"] != DBNull.Value ? row["UpdatedByName"].ToString() : null
            };
        }
    }
}
