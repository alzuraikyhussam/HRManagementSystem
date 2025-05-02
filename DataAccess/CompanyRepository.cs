using System;
using System.Data;
using System.Data.SqlClient;
using HR.Core;
using HR.Models;

namespace HR.DataAccess
{
    /// <summary>
    /// Repository for company information
    /// </summary>
    public class CompanyRepository
    {
        /// <summary>
        /// Gets the company information
        /// </summary>
        /// <returns>Company information or null if not found</returns>
        public CompanyDTO GetCompanyInfo()
        {
            try
            {
                string query = "SELECT ID, Name, LegalName, CommercialRecord, TaxNumber, Address, Phone, Email, Website, Logo, EstablishmentDate, Notes, CreatedAt, UpdatedAt FROM Company";

                using (SqlDataReader reader = ConnectionManager.ExecuteReader(query))
                {
                    if (reader.Read())
                    {
                        CompanyDTO company = new CompanyDTO
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            LegalName = reader.IsDBNull(2) ? null : reader.GetString(2),
                            CommercialRecord = reader.IsDBNull(3) ? null : reader.GetString(3),
                            TaxNumber = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Address = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Phone = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Email = reader.IsDBNull(7) ? null : reader.GetString(7),
                            Website = reader.IsDBNull(8) ? null : reader.GetString(8),
                            Logo = reader.IsDBNull(9) ? null : (byte[])reader.GetValue(9),
                            EstablishmentDate = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10),
                            Notes = reader.IsDBNull(11) ? null : reader.GetString(11)
                        };

                        return company;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to get company information");
                throw;
            }
        }

        /// <summary>
        /// Saves company information
        /// </summary>
        /// <param name="company">Company information</param>
        /// <returns>True if the operation was successful</returns>
        public bool SaveCompanyInfo(CompanyDTO company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            try
            {
                // Check if a company already exists
                object result = ConnectionManager.ExecuteScalar("SELECT COUNT(*) FROM Company");
                int count = Convert.ToInt32(result);

                if (count > 0)
                {
                    // Update existing company
                    string query = @"
                        UPDATE Company
                        SET Name = @Name,
                            LegalName = @LegalName,
                            CommercialRecord = @CommercialRecord,
                            TaxNumber = @TaxNumber,
                            Address = @Address,
                            Phone = @Phone,
                            Email = @Email,
                            Website = @Website,
                            Logo = @Logo,
                            EstablishmentDate = @EstablishmentDate,
                            Notes = @Notes,
                            UpdatedAt = GETDATE()
                        WHERE ID = @ID";

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@ID", company.ID),
                        new SqlParameter("@Name", (object)company.Name ?? DBNull.Value),
                        new SqlParameter("@LegalName", (object)company.LegalName ?? DBNull.Value),
                        new SqlParameter("@CommercialRecord", (object)company.CommercialRecord ?? DBNull.Value),
                        new SqlParameter("@TaxNumber", (object)company.TaxNumber ?? DBNull.Value),
                        new SqlParameter("@Address", (object)company.Address ?? DBNull.Value),
                        new SqlParameter("@Phone", (object)company.Phone ?? DBNull.Value),
                        new SqlParameter("@Email", (object)company.Email ?? DBNull.Value),
                        new SqlParameter("@Website", (object)company.Website ?? DBNull.Value),
                        new SqlParameter("@Logo", (object)company.Logo ?? DBNull.Value),
                        new SqlParameter("@EstablishmentDate", (object)company.EstablishmentDate ?? DBNull.Value),
                        new SqlParameter("@Notes", (object)company.Notes ?? DBNull.Value)
                    };

                    int rowsAffected = ConnectionManager.ExecuteNonQuery(query, parameters);
                    return rowsAffected > 0;
                }
                else
                {
                    // Insert new company
                    string query = @"
                        INSERT INTO Company (Name, LegalName, CommercialRecord, TaxNumber, Address, Phone, Email, Website, Logo, EstablishmentDate, Notes, CreatedAt)
                        VALUES (@Name, @LegalName, @CommercialRecord, @TaxNumber, @Address, @Phone, @Email, @Website, @Logo, @EstablishmentDate, @Notes, GETDATE())";

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@Name", (object)company.Name ?? DBNull.Value),
                        new SqlParameter("@LegalName", (object)company.LegalName ?? DBNull.Value),
                        new SqlParameter("@CommercialRecord", (object)company.CommercialRecord ?? DBNull.Value),
                        new SqlParameter("@TaxNumber", (object)company.TaxNumber ?? DBNull.Value),
                        new SqlParameter("@Address", (object)company.Address ?? DBNull.Value),
                        new SqlParameter("@Phone", (object)company.Phone ?? DBNull.Value),
                        new SqlParameter("@Email", (object)company.Email ?? DBNull.Value),
                        new SqlParameter("@Website", (object)company.Website ?? DBNull.Value),
                        new SqlParameter("@Logo", (object)company.Logo ?? DBNull.Value),
                        new SqlParameter("@EstablishmentDate", (object)company.EstablishmentDate ?? DBNull.Value),
                        new SqlParameter("@Notes", (object)company.Notes ?? DBNull.Value)
                    };

                    int rowsAffected = ConnectionManager.ExecuteNonQuery(query, parameters);
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to save company information");
                throw;
            }
        }

        /// <summary>
        /// Updates the company logo
        /// </summary>
        /// <param name="logo">Logo bytes</param>
        /// <returns>True if the operation was successful</returns>
        public bool UpdateLogo(byte[] logo)
        {
            try
            {
                string query = "UPDATE Company SET Logo = @Logo, UpdatedAt = GETDATE()";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Logo", (object)logo ?? DBNull.Value)
                };

                int rowsAffected = ConnectionManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to update company logo");
                throw;
            }
        }

        /// <summary>
        /// Gets the company logo
        /// </summary>
        /// <returns>Logo bytes or null if not found</returns>
        public byte[] GetLogo()
        {
            try
            {
                string query = "SELECT Logo FROM Company";
                object result = ConnectionManager.ExecuteScalar(query);
                return result == DBNull.Value ? null : (byte[])result;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Failed to get company logo");
                throw;
            }
        }
    }
}