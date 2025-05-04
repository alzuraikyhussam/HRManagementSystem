using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DevExpress.XtraReports.UI;
using HR.DataAccess;
using HR.Models;

namespace HR.Core
{
    /// <summary>
    /// خدمة إنشاء وإدارة التقارير
    /// </summary>
    public class ReportingService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly string _reportsDirectory;
        
        /// <summary>
        /// إنشاء مثيل جديد من خدمة التقارير
        /// </summary>
        public ReportingService()
        {
            _unitOfWork = new UnitOfWork();
            _reportsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            
            // التأكد من وجود المجلد
            if (!Directory.Exists(_reportsDirectory))
            {
                Directory.CreateDirectory(_reportsDirectory);
            }
        }
        
        #region التقارير العامة
        
        /// <summary>
        /// إنشاء تقرير قائمة الموظفين
        /// </summary>
        /// <param name="departmentId">معرف القسم (اختياري)</param>
        /// <param name="statusFilter">حالة الموظف (اختياري)</param>
        /// <returns>تقرير قائمة الموظفين</returns>
        public XtraReport CreateEmployeeListReport(int? departmentId = null, string statusFilter = null)
        {
            try
            {
                // الحصول على بيانات الموظفين
                List<Employee> employees = _unitOfWork.EmployeeRepository.GetAllEmployees(departmentId, statusFilter);
                
                // إنشاء تقرير
                EmployeeListReport report = new EmployeeListReport();
                report.DataSource = employees;
                
                return report;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل إنشاء تقرير قائمة الموظفين");
                throw;
            }
        }
        
        /// <summary>
        /// إنشاء تقرير الحضور الشهري
        /// </summary>
        /// <param name="year">السنة</param>
        /// <param name="month">الشهر</param>
        /// <param name="departmentId">معرف القسم (اختياري)</param>
        /// <returns>تقرير الحضور الشهري</returns>
        public XtraReport CreateMonthlyAttendanceReport(int year, int month, int? departmentId = null)
        {
            try
            {
                // تحديد الفترة الزمنية
                DateTime startDate = new DateTime(year, month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                
                // الحصول على بيانات الحضور
                List<AttendanceRecordDTO> attendanceRecords = GetAttendanceRecordsForReport(startDate, endDate, departmentId);
                
                // إنشاء تقرير
                MonthlyAttendanceReport report = new MonthlyAttendanceReport();
                report.DataSource = attendanceRecords;
                report.StartDate = startDate;
                report.EndDate = endDate;
                
                return report;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل إنشاء تقرير الحضور لشهر {month}/{year}");
                throw;
            }
        }
        
        /// <summary>
        /// إنشاء تقرير الرواتب الشهري
        /// </summary>
        /// <param name="year">السنة</param>
        /// <param name="month">الشهر</param>
        /// <param name="departmentId">معرف القسم (اختياري)</param>
        /// <returns>تقرير الرواتب الشهري</returns>
        public XtraReport CreateMonthlyPayrollReport(int year, int month, int? departmentId = null)
        {
            try
            {
                // الحصول على بيانات الرواتب
                List<PayrollReportDTO> payrollData = GetPayrollDataForReport(year, month, departmentId);
                
                // إنشاء تقرير
                MonthlyPayrollReport report = new MonthlyPayrollReport();
                report.DataSource = payrollData;
                report.Year = year;
                report.Month = month;
                
                return report;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل إنشاء تقرير الرواتب لشهر {month}/{year}");
                throw;
            }
        }
        
        /// <summary>
        /// إنشاء تقرير الإجازات
        /// </summary>
        /// <param name="startDate">تاريخ البداية</param>
        /// <param name="endDate">تاريخ النهاية</param>
        /// <param name="departmentId">معرف القسم (اختياري)</param>
        /// <param name="leaveTypeId">نوع الإجازة (اختياري)</param>
        /// <returns>تقرير الإجازات</returns>
        public XtraReport CreateLeavesReport(DateTime startDate, DateTime endDate, int? departmentId = null, int? leaveTypeId = null)
        {
            try
            {
                // الحصول على بيانات الإجازات
                List<LeaveReportDTO> leaveData = GetLeaveDataForReport(startDate, endDate, departmentId, leaveTypeId);
                
                // إنشاء تقرير
                LeavesReport report = new LeavesReport();
                report.DataSource = leaveData;
                report.StartDate = startDate;
                report.EndDate = endDate;
                
                return report;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل إنشاء تقرير الإجازات");
                throw;
            }
        }
        
        /// <summary>
        /// إنشاء تقرير الوثائق المنتهية الصلاحية أو قاربت على الانتهاء
        /// </summary>
        /// <param name="daysThreshold">عدد الأيام المتبقية للتنبيه</param>
        /// <returns>تقرير الوثائق</returns>
        public XtraReport CreateExpiringDocumentsReport(int daysThreshold = 30)
        {
            try
            {
                // الحصول على بيانات الوثائق
                List<EmployeeDocument> documents = _unitOfWork.EmployeeDocumentRepository.GetExpiringDocuments();
                
                // تصفية البيانات حسب عدد الأيام المحدد
                var filteredDocuments = documents
                    .Where(d => d.GetRemainingDays() <= daysThreshold)
                    .ToList();
                
                // إنشاء تقرير
                ExpiringDocumentsReport report = new ExpiringDocumentsReport();
                report.DataSource = filteredDocuments;
                report.DaysThreshold = daysThreshold;
                
                return report;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "فشل إنشاء تقرير الوثائق المنتهية الصلاحية");
                throw;
            }
        }
        
        #endregion
        
        #region التقارير التحليلية
        
        /// <summary>
        /// إنشاء تقرير تحليل الحضور والغياب
        /// </summary>
        /// <param name="year">السنة</param>
        /// <param name="startMonth">الشهر البداية</param>
        /// <param name="endMonth">الشهر النهاية</param>
        /// <param name="departmentId">معرف القسم (اختياري)</param>
        /// <returns>تقرير تحليل الحضور والغياب</returns>
        public XtraReport CreateAttendanceAnalysisReport(int year, int startMonth, int endMonth, int? departmentId = null)
        {
            try
            {
                // الحصول على بيانات تحليل الحضور
                List<AttendanceAnalysisDTO> analysisData = GetAttendanceAnalysisForReport(year, startMonth, endMonth, departmentId);
                
                // إنشاء تقرير
                AttendanceAnalysisReport report = new AttendanceAnalysisReport();
                report.DataSource = analysisData;
                report.Year = year;
                report.StartMonth = startMonth;
                report.EndMonth = endMonth;
                
                return report;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل إنشاء تقرير تحليل الحضور لعام {year}");
                throw;
            }
        }
        
        /// <summary>
        /// إنشاء تقرير تكلفة الموارد البشرية
        /// </summary>
        /// <param name="year">السنة</param>
        /// <param name="quarter">الربع السنوي (1-4، اختياري)</param>
        /// <returns>تقرير تكلفة الموارد البشرية</returns>
        public XtraReport CreateHRCostReport(int year, int? quarter = null)
        {
            try
            {
                // الحصول على بيانات تكلفة الموارد البشرية
                HRCostReportDTO costData = GetHRCostDataForReport(year, quarter);
                
                // إنشاء تقرير
                HRCostReport report = new HRCostReport();
                report.DataSource = new List<HRCostReportDTO> { costData };
                report.Year = year;
                report.Quarter = quarter;
                
                return report;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل إنشاء تقرير تكلفة الموارد البشرية لعام {year}");
                throw;
            }
        }
        
        /// <summary>
        /// إنشاء تقرير تحليل تقييمات الأداء
        /// </summary>
        /// <param name="year">السنة</param>
        /// <param name="evaluationPeriodId">فترة التقييم (اختياري)</param>
        /// <returns>تقرير تحليل تقييمات الأداء</returns>
        public XtraReport CreatePerformanceAnalysisReport(int year, int? evaluationPeriodId = null)
        {
            try
            {
                // الحصول على بيانات تحليل تقييمات الأداء
                List<PerformanceAnalysisDTO> analysisData = GetPerformanceAnalysisForReport(year, evaluationPeriodId);
                
                // إنشاء تقرير
                PerformanceAnalysisReport report = new PerformanceAnalysisReport();
                report.DataSource = analysisData;
                report.Year = year;
                
                return report;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل إنشاء تقرير تحليل تقييمات الأداء لعام {year}");
                throw;
            }
        }
        
        /// <summary>
        /// إنشاء تقرير معدل دوران الموظفين
        /// </summary>
        /// <param name="year">السنة</param>
        /// <returns>تقرير معدل دوران الموظفين</returns>
        public XtraReport CreateEmployeeTurnoverReport(int year)
        {
            try
            {
                // الحصول على بيانات معدل دوران الموظفين
                EmployeeTurnoverDTO turnoverData = GetEmployeeTurnoverDataForReport(year);
                
                // إنشاء تقرير
                EmployeeTurnoverReport report = new EmployeeTurnoverReport();
                report.DataSource = new List<EmployeeTurnoverDTO> { turnoverData };
                report.Year = year;
                
                return report;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"فشل إنشاء تقرير معدل دوران الموظفين لعام {year}");
                throw;
            }
        }
        
        #endregion
        
        #region عمليات مساعدة لاستخراج البيانات
        
        /// <summary>
        /// الحصول على بيانات الحضور للتقرير
        /// </summary>
        private List<AttendanceRecordDTO> GetAttendanceRecordsForReport(DateTime startDate, DateTime endDate, int? departmentId = null)
        {
            // الحصول على سجلات الحضور
            List<AttendanceRecord> records = _unitOfWork.AttendanceRepository.GetAttendanceRecords(startDate, endDate);
            
            // تصفية البيانات حسب القسم إذا تم تحديده
            if (departmentId.HasValue)
            {
                // الحصول على موظفي القسم
                List<Employee> departmentEmployees = _unitOfWork.EmployeeRepository.GetEmployeesByDepartment(departmentId.Value);
                var employeeIds = departmentEmployees.Select(e => e.ID).ToList();
                
                records = records.Where(r => employeeIds.Contains(r.EmployeeID)).ToList();
            }
            
            // تحويل البيانات إلى نموذج DTO
            return records.Select(r => new AttendanceRecordDTO
            {
                ID = r.ID,
                EmployeeID = r.EmployeeID,
                EmployeeName = r.EmployeeName,
                DepartmentName = r.DepartmentName,
                AttendanceDate = r.AttendanceDate,
                TimeIn = r.TimeIn,
                TimeOut = r.TimeOut,
                WorkedHours = (decimal)r.WorkedMinutes / 60,
                LateMinutes = r.LateMinutes,
                EarlyDepartureMinutes = r.EarlyDepartureMinutes,
                OvertimeMinutes = r.OvertimeMinutes,
                Status = r.Status,
                IsManualEntry = r.IsManualEntry,
                Notes = r.Notes
            }).ToList();
        }
        
        /// <summary>
        /// الحصول على بيانات الرواتب للتقرير
        /// </summary>
        private List<PayrollReportDTO> GetPayrollDataForReport(int year, int month, int? departmentId = null)
        {
            // الحصول على سجلات الرواتب
            var payrollCalculator = new PayrollCalculator();
            List<PayrollReportDTO> payrollData = new List<PayrollReportDTO>();
            
            // الحصول على الموظفين
            List<Employee> employees;
            if (departmentId.HasValue)
            {
                employees = _unitOfWork.EmployeeRepository.GetEmployeesByDepartment(departmentId.Value);
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetAllEmployees();
            }
            
            // حساب الرواتب لكل موظف
            foreach (var employee in employees)
            {
                var payrollDetails = payrollCalculator.CalculateEmployeePayroll(employee.ID, year, month);
                
                var payrollDto = new PayrollReportDTO
                {
                    EmployeeID = employee.ID,
                    EmployeeName = employee.FullName,
                    EmployeeNumber = employee.EmployeeNumber,
                    DepartmentName = employee.DepartmentName,
                    JobTitle = employee.JobTitle,
                    BasicSalary = payrollDetails.BasicSalary,
                    TotalAllowances = payrollDetails.TotalAllowances,
                    TotalDeductions = payrollDetails.TotalDeductions,
                    GrossSalary = payrollDetails.GrossSalary,
                    NetSalary = payrollDetails.NetSalary,
                    
                    // تفاصيل البدلات والخصومات
                    HousingAllowance = employee.HousingAllowance,
                    TransportationAllowance = employee.TransportationAllowance,
                    GosiDeduction = payrollDetails.Deductions
                        .FirstOrDefault(d => d.Name == "التأمينات الاجتماعية")?.Amount ?? 0,
                    AbsenceDeduction = payrollDetails.Deductions
                        .FirstOrDefault(d => d.Name == "خصم غياب")?.Amount ?? 0,
                    LateDeduction = payrollDetails.Deductions
                        .FirstOrDefault(d => d.Name == "خصم تأخير")?.Amount ?? 0,
                    OvertimeAllowance = payrollDetails.Allowances
                        .FirstOrDefault(a => a.Name == "بدل عمل إضافي")?.Amount ?? 0,
                        
                    OtherAllowances = payrollDetails.Allowances
                        .Where(a => a.Name != "بدل سكن" && a.Name != "بدل مواصلات" && a.Name != "بدل عمل إضافي")
                        .Sum(a => a.Amount),
                        
                    OtherDeductions = payrollDetails.Deductions
                        .Where(d => d.Name != "التأمينات الاجتماعية" && d.Name != "خصم غياب" && d.Name != "خصم تأخير")
                        .Sum(d => d.Amount),
                        
                    // إحصائيات الحضور
                    PresentDays = payrollDetails.AttendanceStatistics?.PresentDays ?? 0,
                    AbsentDays = payrollDetails.AttendanceStatistics?.AbsentDays ?? 0,
                    LeaveDays = payrollDetails.AttendanceStatistics?.LeaveDays ?? 0
                };
                
                payrollData.Add(payrollDto);
            }
            
            return payrollData;
        }
        
        /// <summary>
        /// الحصول على بيانات الإجازات للتقرير
        /// </summary>
        private List<LeaveReportDTO> GetLeaveDataForReport(DateTime startDate, DateTime endDate, int? departmentId = null, int? leaveTypeId = null)
        {
            // الحصول على بيانات الإجازات
            var leaves = _unitOfWork.LeaveRepository.GetLeaveRequests(startDate, endDate, departmentId, leaveTypeId);
            
            // تحويل البيانات إلى نموذج DTO
            return leaves.Select(l => new LeaveReportDTO
            {
                ID = l.ID,
                EmployeeID = l.EmployeeID,
                EmployeeName = l.EmployeeName,
                DepartmentName = l.DepartmentName,
                LeaveTypeName = l.LeaveTypeName,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                Days = l.Days,
                Status = l.Status,
                Notes = l.Notes,
                SubmissionDate = l.SubmissionDate
            }).ToList();
        }
        
        /// <summary>
        /// الحصول على بيانات تحليل الحضور للتقرير
        /// </summary>
        private List<AttendanceAnalysisDTO> GetAttendanceAnalysisForReport(int year, int startMonth, int endMonth, int? departmentId = null)
        {
            List<AttendanceAnalysisDTO> result = new List<AttendanceAnalysisDTO>();
            
            // الحصول على الموظفين
            List<Employee> employees;
            if (departmentId.HasValue)
            {
                employees = _unitOfWork.EmployeeRepository.GetEmployeesByDepartment(departmentId.Value);
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetAllEmployees();
            }
            
            // بناء التحليل لكل موظف
            foreach (var employee in employees)
            {
                var analysisDto = new AttendanceAnalysisDTO
                {
                    EmployeeID = employee.ID,
                    EmployeeName = employee.FullName,
                    DepartmentName = employee.DepartmentName,
                    JobTitle = employee.JobTitle,
                    MonthlyData = new List<MonthlyAttendanceData>()
                };
                
                // الإحصائيات الشهرية
                for (int month = startMonth; month <= endMonth; month++)
                {
                    var summary = _unitOfWork.AttendanceRepository.GetEmployeeMonthlyAttendanceSummary(employee.ID, year, month);
                    
                    if (summary != null)
                    {
                        analysisDto.MonthlyData.Add(new MonthlyAttendanceData
                        {
                            Month = month,
                            TotalDays = summary.TotalDays,
                            PresentDays = summary.PresentDays,
                            AbsentDays = summary.AbsentDays,
                            LateDays = summary.LateDays,
                            LeaveDays = summary.LeaveDays,
                            AttendanceRate = summary.PunctualityRate,
                            AverageDailyHours = summary.AverageDailyWorkHours
                        });
                    }
                }
                
                // حساب الإجماليات
                if (analysisDto.MonthlyData.Any())
                {
                    analysisDto.TotalPresentDays = analysisDto.MonthlyData.Sum(m => m.PresentDays);
                    analysisDto.TotalAbsentDays = analysisDto.MonthlyData.Sum(m => m.AbsentDays);
                    analysisDto.TotalLateDays = analysisDto.MonthlyData.Sum(m => m.LateDays);
                    analysisDto.TotalLeaveDays = analysisDto.MonthlyData.Sum(m => m.LeaveDays);
                    analysisDto.AverageAttendanceRate = analysisDto.MonthlyData.Average(m => m.AttendanceRate);
                    analysisDto.AverageDailyHours = analysisDto.MonthlyData.Average(m => m.AverageDailyHours);
                }
                
                result.Add(analysisDto);
            }
            
            return result;
        }
        
        /// <summary>
        /// الحصول على بيانات تكلفة الموارد البشرية للتقرير
        /// </summary>
        private HRCostReportDTO GetHRCostDataForReport(int year, int? quarter = null)
        {
            HRCostReportDTO result = new HRCostReportDTO
            {
                Year = year,
                Quarter = quarter,
                DepartmentCosts = new List<DepartmentCostData>(),
                SalaryCategoryData = new List<SalaryCategoryData>(),
                MonthlyData = new List<MonthlyCostData>()
            };
            
            // تحديد الأشهر المطلوبة
            List<int> months = new List<int>();
            if (quarter.HasValue)
            {
                // تحديد الأشهر حسب الربع السنوي
                switch (quarter.Value)
                {
                    case 1:
                        months.AddRange(new[] { 1, 2, 3 });
                        break;
                    case 2:
                        months.AddRange(new[] { 4, 5, 6 });
                        break;
                    case 3:
                        months.AddRange(new[] { 7, 8, 9 });
                        break;
                    case 4:
                        months.AddRange(new[] { 10, 11, 12 });
                        break;
                }
            }
            else
            {
                // جميع أشهر السنة
                months.AddRange(Enumerable.Range(1, 12));
            }
            
            // الحصول على الأقسام
            var departments = _unitOfWork.DepartmentRepository.GetAllDepartments();
            
            // حساب التكاليف لكل قسم
            foreach (var department in departments)
            {
                decimal totalDepartmentCost = 0;
                
                // الحصول على جميع موظفي القسم
                var employees = _unitOfWork.EmployeeRepository.GetEmployeesByDepartment(department.ID);
                
                // حساب الرواتب لكل موظف في الأشهر المحددة
                foreach (var month in months)
                {
                    decimal monthlyCost = 0;
                    
                    foreach (var employee in employees)
                    {
                        var payrollCalculator = new PayrollCalculator();
                        var payrollDetails = payrollCalculator.CalculateEmployeePayroll(employee.ID, year, month);
                        
                        // إضافة الراتب الإجمالي للتكلفة
                        monthlyCost += payrollDetails.GrossSalary;
                    }
                    
                    totalDepartmentCost += monthlyCost;
                    
                    // إضافة التكلفة الشهرية
                    var existingMonth = result.MonthlyData.FirstOrDefault(m => m.Month == month);
                    if (existingMonth != null)
                    {
                        existingMonth.TotalCost += monthlyCost;
                        existingMonth.DepartmentCosts.Add(new MonthlyDepartmentCost
                        {
                            DepartmentID = department.ID,
                            DepartmentName = department.Name,
                            Cost = monthlyCost
                        });
                    }
                    else
                    {
                        result.MonthlyData.Add(new MonthlyCostData
                        {
                            Month = month,
                            TotalCost = monthlyCost,
                            DepartmentCosts = new List<MonthlyDepartmentCost>
                            {
                                new MonthlyDepartmentCost
                                {
                                    DepartmentID = department.ID,
                                    DepartmentName = department.Name,
                                    Cost = monthlyCost
                                }
                            }
                        });
                    }
                }
                
                // إضافة تكلفة القسم الإجمالية
                result.DepartmentCosts.Add(new DepartmentCostData
                {
                    DepartmentID = department.ID,
                    DepartmentName = department.Name,
                    TotalCost = totalDepartmentCost,
                    EmployeeCount = employees.Count
                });
            }
            
            // حساب إجمالي التكلفة
            result.TotalSalaryCost = result.DepartmentCosts.Sum(d => d.TotalCost);
            
            // حساب متوسط تكلفة الموظف
            int totalEmployees = result.DepartmentCosts.Sum(d => d.EmployeeCount);
            result.TotalEmployeeCount = totalEmployees;
            result.AverageEmployeeCost = totalEmployees > 0 ? result.TotalSalaryCost / totalEmployees : 0;
            
            // تصنيف الموظفين حسب فئات الرواتب
            result.SalaryCategoryData = GetSalaryCategoryData();
            
            return result;
        }
        
        /// <summary>
        /// الحصول على بيانات فئات الرواتب
        /// </summary>
        private List<SalaryCategoryData> GetSalaryCategoryData()
        {
            // تعريف فئات الرواتب
            var categories = new List<SalaryCategory>
            {
                new SalaryCategory { ID = 1, Name = "أقل من 5,000", MinSalary = 0, MaxSalary = 4999 },
                new SalaryCategory { ID = 2, Name = "5,000 - 10,000", MinSalary = 5000, MaxSalary = 10000 },
                new SalaryCategory { ID = 3, Name = "10,001 - 15,000", MinSalary = 10001, MaxSalary = 15000 },
                new SalaryCategory { ID = 4, Name = "15,001 - 20,000", MinSalary = 15001, MaxSalary = 20000 },
                new SalaryCategory { ID = 5, Name = "أكثر من 20,000", MinSalary = 20001, MaxSalary = decimal.MaxValue }
            };
            
            // الحصول على جميع الموظفين
            var employees = _unitOfWork.EmployeeRepository.GetAllEmployees();
            
            // تصنيف الموظفين حسب الراتب
            List<SalaryCategoryData> result = new List<SalaryCategoryData>();
            
            foreach (var category in categories)
            {
                int count = employees.Count(e => e.BasicSalary >= category.MinSalary && e.BasicSalary <= category.MaxSalary);
                decimal percentage = employees.Count > 0 ? (decimal)count / employees.Count * 100 : 0;
                
                result.Add(new SalaryCategoryData
                {
                    CategoryID = category.ID,
                    CategoryName = category.Name,
                    EmployeeCount = count,
                    Percentage = Math.Round(percentage, 2)
                });
            }
            
            return result;
        }
        
        /// <summary>
        /// الحصول على بيانات تحليل تقييمات الأداء للتقرير
        /// </summary>
        private List<PerformanceAnalysisDTO> GetPerformanceAnalysisForReport(int year, int? evaluationPeriodId = null)
        {
            // نموذج للتطوير المستقبلي - يعتمد على وجود نظام تقييم أداء
            // ستعيد هذه الدالة بيانات تقييم الأداء للتقرير
            
            // نموذج بدون تنفيذ لأنه يحتاج إلى مستودع تقييمات الأداء
            return new List<PerformanceAnalysisDTO>();
        }
        
        /// <summary>
        /// الحصول على بيانات معدل دوران الموظفين للتقرير
        /// </summary>
        private EmployeeTurnoverDTO GetEmployeeTurnoverDataForReport(int year)
        {
            // نموذج للتطوير المستقبلي - يعتمد على وجود سجلات بدء وانتهاء خدمة الموظفين
            
            // نموذج بدون تنفيذ تفصيلي
            return new EmployeeTurnoverDTO
            {
                Year = year,
                MonthlyData = new List<MonthlyTurnoverData>(),
                DepartmentData = new List<DepartmentTurnoverData>()
            };
        }
        
        #endregion
        
        #region تصدير التقارير
        
        /// <summary>
        /// تصدير تقرير إلى ملف PDF
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="fileName">اسم الملف</param>
        /// <returns>مسار الملف</returns>
        public string ExportToPdf(XtraReport report, string fileName)
        {
            string filePath = Path.Combine(_reportsDirectory, $"{fileName}.pdf");
            
            // التأكد من عدم وجود ملف بنفس الاسم
            if (File.Exists(filePath))
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                filePath = Path.Combine(_reportsDirectory, $"{fileName}_{timestamp}.pdf");
            }
            
            // تصدير التقرير إلى PDF
            report.ExportToPdf(filePath);
            
            return filePath;
        }
        
        /// <summary>
        /// تصدير تقرير إلى ملف Excel
        /// </summary>
        /// <param name="report">التقرير</param>
        /// <param name="fileName">اسم الملف</param>
        /// <returns>مسار الملف</returns>
        public string ExportToExcel(XtraReport report, string fileName)
        {
            string filePath = Path.Combine(_reportsDirectory, $"{fileName}.xlsx");
            
            // التأكد من عدم وجود ملف بنفس الاسم
            if (File.Exists(filePath))
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                filePath = Path.Combine(_reportsDirectory, $"{fileName}_{timestamp}.xlsx");
            }
            
            // تصدير التقرير إلى Excel
            report.ExportToXlsx(filePath);
            
            return filePath;
        }
        
        #endregion
    }
    
    #region نماذج المعلومات للتقارير
    
    /// <summary>
    /// نموذج سجل الحضور للتقرير
    /// </summary>
    public class AttendanceRecordDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public decimal WorkedHours { get; set; }
        public int LateMinutes { get; set; }
        public int EarlyDepartureMinutes { get; set; }
        public int OvertimeMinutes { get; set; }
        public string Status { get; set; }
        public bool IsManualEntry { get; set; }
        public string Notes { get; set; }
    }
    
    /// <summary>
    /// نموذج تقرير الرواتب
    /// </summary>
    public class PayrollReportDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string DepartmentName { get; set; }
        public string JobTitle { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal TotalAllowances { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal NetSalary { get; set; }
        
        // تفاصيل البدلات والخصومات
        public decimal HousingAllowance { get; set; }
        public decimal TransportationAllowance { get; set; }
        public decimal GosiDeduction { get; set; }
        public decimal AbsenceDeduction { get; set; }
        public decimal LateDeduction { get; set; }
        public decimal OvertimeAllowance { get; set; }
        public decimal OtherAllowances { get; set; }
        public decimal OtherDeductions { get; set; }
        
        // إحصائيات الحضور
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public int LeaveDays { get; set; }
    }
    
    /// <summary>
    /// نموذج تقرير الإجازات
    /// </summary>
    public class LeaveReportDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string LeaveTypeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
    
    /// <summary>
    /// نموذج تحليل الحضور
    /// </summary>
    public class AttendanceAnalysisDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string JobTitle { get; set; }
        
        // الإحصائيات الإجمالية
        public int TotalPresentDays { get; set; }
        public int TotalAbsentDays { get; set; }
        public int TotalLateDays { get; set; }
        public int TotalLeaveDays { get; set; }
        public decimal AverageAttendanceRate { get; set; }
        public decimal AverageDailyHours { get; set; }
        
        // البيانات الشهرية
        public List<MonthlyAttendanceData> MonthlyData { get; set; }
    }
    
    /// <summary>
    /// نموذج البيانات الشهرية للحضور
    /// </summary>
    public class MonthlyAttendanceData
    {
        public int Month { get; set; }
        public int TotalDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public int LateDays { get; set; }
        public int LeaveDays { get; set; }
        public decimal AttendanceRate { get; set; }
        public decimal AverageDailyHours { get; set; }
    }
    
    /// <summary>
    /// نموذج تقرير تكلفة الموارد البشرية
    /// </summary>
    public class HRCostReportDTO
    {
        public int Year { get; set; }
        public int? Quarter { get; set; }
        
        // الإحصائيات الإجمالية
        public decimal TotalSalaryCost { get; set; }
        public int TotalEmployeeCount { get; set; }
        public decimal AverageEmployeeCost { get; set; }
        
        // التكاليف حسب القسم
        public List<DepartmentCostData> DepartmentCosts { get; set; }
        
        // التكاليف الشهرية
        public List<MonthlyCostData> MonthlyData { get; set; }
        
        // توزيع الموظفين حسب فئات الرواتب
        public List<SalaryCategoryData> SalaryCategoryData { get; set; }
    }
    
    /// <summary>
    /// نموذج تكلفة القسم
    /// </summary>
    public class DepartmentCostData
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public decimal TotalCost { get; set; }
        public int EmployeeCount { get; set; }
        
        // حساب متوسط تكلفة الموظف في القسم
        public decimal AverageEmployeeCost => EmployeeCount > 0 ? TotalCost / EmployeeCount : 0;
    }
    
    /// <summary>
    /// نموذج التكلفة الشهرية
    /// </summary>
    public class MonthlyCostData
    {
        public int Month { get; set; }
        public decimal TotalCost { get; set; }
        public List<MonthlyDepartmentCost> DepartmentCosts { get; set; }
    }
    
    /// <summary>
    /// نموذج تكلفة القسم الشهرية
    /// </summary>
    public class MonthlyDepartmentCost
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public decimal Cost { get; set; }
    }
    
    /// <summary>
    /// نموذج فئة الراتب
    /// </summary>
    public class SalaryCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
    }
    
    /// <summary>
    /// نموذج بيانات فئة الراتب
    /// </summary>
    public class SalaryCategoryData
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int EmployeeCount { get; set; }
        public decimal Percentage { get; set; }
    }
    
    /// <summary>
    /// نموذج تحليل تقييمات الأداء
    /// </summary>
    public class PerformanceAnalysisDTO
    {
        public int Year { get; set; }
        public string EvaluationPeriodName { get; set; }
        
        // الإحصائيات الإجمالية
        public decimal AveragePerformanceScore { get; set; }
        public int TotalEvaluatedEmployees { get; set; }
        
        // البيانات التفصيلية
        public List<EmployeePerformanceData> EmployeeData { get; set; }
        public List<DepartmentPerformanceData> DepartmentData { get; set; }
        public List<CompetencyAnalysisData> CompetencyData { get; set; }
    }
    
    /// <summary>
    /// نموذج بيانات أداء الموظف
    /// </summary>
    public class EmployeePerformanceData
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public decimal OverallScore { get; set; }
        public string PerformanceLevel { get; set; }
        public List<CompetencyScore> CompetencyScores { get; set; }
    }
    
    /// <summary>
    /// نموذج بيانات أداء القسم
    /// </summary>
    public class DepartmentPerformanceData
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public decimal AverageScore { get; set; }
        public int TotalEmployees { get; set; }
        public int HighPerformersCount { get; set; }
        public int LowPerformersCount { get; set; }
    }
    
    /// <summary>
    /// نموذج تحليل الكفاءات
    /// </summary>
    public class CompetencyAnalysisData
    {
        public int CompetencyID { get; set; }
        public string CompetencyName { get; set; }
        public decimal AverageScore { get; set; }
        public decimal MaxPossibleScore { get; set; }
        public decimal AchievementPercentage { get; set; }
    }
    
    /// <summary>
    /// نموذج درجة الكفاءة
    /// </summary>
    public class CompetencyScore
    {
        public int CompetencyID { get; set; }
        public string CompetencyName { get; set; }
        public decimal Score { get; set; }
        public decimal MaxScore { get; set; }
    }
    
    /// <summary>
    /// نموذج تقرير معدل دوران الموظفين
    /// </summary>
    public class EmployeeTurnoverDTO
    {
        public int Year { get; set; }
        
        // الإحصائيات الإجمالية
        public int TotalEmployeesStart { get; set; }
        public int TotalEmployeesEnd { get; set; }
        public int TotalJoined { get; set; }
        public int TotalLeft { get; set; }
        public decimal TurnoverRate { get; set; }
        
        // البيانات الشهرية
        public List<MonthlyTurnoverData> MonthlyData { get; set; }
        
        // البيانات حسب القسم
        public List<DepartmentTurnoverData> DepartmentData { get; set; }
    }
    
    /// <summary>
    /// نموذج بيانات معدل الدوران الشهري
    /// </summary>
    public class MonthlyTurnoverData
    {
        public int Month { get; set; }
        public int TotalEmployees { get; set; }
        public int JoinedCount { get; set; }
        public int LeftCount { get; set; }
        public decimal TurnoverRate { get; set; }
    }
    
    /// <summary>
    /// نموذج بيانات معدل دوران موظفي القسم
    /// </summary>
    public class DepartmentTurnoverData
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int TotalEmployees { get; set; }
        public int JoinedCount { get; set; }
        public int LeftCount { get; set; }
        public decimal TurnoverRate { get; set; }
    }
    
    #endregion
    
    #region نماذج التقارير - المراجع فقط
    
    // هذه النماذج هي مراجع فقط لنتخيل كيف ستبدو فئات التقارير في DevExpress
    
    /// <summary>
    /// تقرير قائمة الموظفين
    /// </summary>
    public class EmployeeListReport : XtraReport
    {
        // مرجع فقط - ستنفذ باستخدام DevExpress XtraReports
    }
    
    /// <summary>
    /// تقرير الحضور الشهري
    /// </summary>
    public class MonthlyAttendanceReport : XtraReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        // مرجع فقط - ستنفذ باستخدام DevExpress XtraReports
    }
    
    /// <summary>
    /// تقرير الرواتب الشهري
    /// </summary>
    public class MonthlyPayrollReport : XtraReport
    {
        public int Year { get; set; }
        public int Month { get; set; }
        // مرجع فقط - ستنفذ باستخدام DevExpress XtraReports
    }
    
    /// <summary>
    /// تقرير الإجازات
    /// </summary>
    public class LeavesReport : XtraReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        // مرجع فقط - ستنفذ باستخدام DevExpress XtraReports
    }
    
    /// <summary>
    /// تقرير الوثائق المنتهية الصلاحية
    /// </summary>
    public class ExpiringDocumentsReport : XtraReport
    {
        public int DaysThreshold { get; set; }
        // مرجع فقط - ستنفذ باستخدام DevExpress XtraReports
    }
    
    /// <summary>
    /// تقرير تحليل الحضور
    /// </summary>
    public class AttendanceAnalysisReport : XtraReport
    {
        public int Year { get; set; }
        public int StartMonth { get; set; }
        public int EndMonth { get; set; }
        // مرجع فقط - ستنفذ باستخدام DevExpress XtraReports
    }
    
    /// <summary>
    /// تقرير تكلفة الموارد البشرية
    /// </summary>
    public class HRCostReport : XtraReport
    {
        public int Year { get; set; }
        public int? Quarter { get; set; }
        // مرجع فقط - ستنفذ باستخدام DevExpress XtraReports
    }
    
    /// <summary>
    /// تقرير تحليل تقييمات الأداء
    /// </summary>
    public class PerformanceAnalysisReport : XtraReport
    {
        public int Year { get; set; }
        // مرجع فقط - ستنفذ باستخدام DevExpress XtraReports
    }
    
    /// <summary>
    /// تقرير معدل دوران الموظفين
    /// </summary>
    public class EmployeeTurnoverReport : XtraReport
    {
        public int Year { get; set; }
        // مرجع فقط - ستنفذ باستخدام DevExpress XtraReports
    }
    
    #endregion
}