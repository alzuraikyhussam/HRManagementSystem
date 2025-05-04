using System;

namespace HR.DataAccess
{
    /// <summary>
    /// وحدة العمل لإدارة مستودعات البيانات
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private EmployeeRepository _employeeRepository;
        private AttendanceRepository _attendanceRepository;
        private LeaveRepository _leaveRepository;
        private DepartmentRepository _departmentRepository;
        private JobTitleRepository _jobTitleRepository;
        private UserRepository _userRepository;
        private EmployeeDocumentRepository _employeeDocumentRepository;
        private PayrollRepository _payrollRepository;
        private AllowanceRepository _allowanceRepository;
        private DeductionRepository _deductionRepository;
        private LoanRepository _loanRepository;
        
        /// <summary>
        /// مستودع بيانات الموظفين
        /// </summary>
        public EmployeeRepository EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository();
                }
                return _employeeRepository;
            }
        }
        
        /// <summary>
        /// مستودع بيانات الحضور
        /// </summary>
        public AttendanceRepository AttendanceRepository
        {
            get
            {
                if (_attendanceRepository == null)
                {
                    _attendanceRepository = new AttendanceRepository();
                }
                return _attendanceRepository;
            }
        }
        
        /// <summary>
        /// مستودع بيانات الإجازات
        /// </summary>
        public LeaveRepository LeaveRepository
        {
            get
            {
                if (_leaveRepository == null)
                {
                    _leaveRepository = new LeaveRepository();
                }
                return _leaveRepository;
            }
        }
        
        /// <summary>
        /// مستودع بيانات الأقسام
        /// </summary>
        public DepartmentRepository DepartmentRepository
        {
            get
            {
                if (_departmentRepository == null)
                {
                    _departmentRepository = new DepartmentRepository();
                }
                return _departmentRepository;
            }
        }
        
        /// <summary>
        /// مستودع بيانات المسميات الوظيفية
        /// </summary>
        public JobTitleRepository JobTitleRepository
        {
            get
            {
                if (_jobTitleRepository == null)
                {
                    _jobTitleRepository = new JobTitleRepository();
                }
                return _jobTitleRepository;
            }
        }
        
        /// <summary>
        /// مستودع بيانات المستخدمين
        /// </summary>
        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository();
                }
                return _userRepository;
            }
        }
        
        /// <summary>
        /// مستودع بيانات وثائق الموظفين
        /// </summary>
        public EmployeeDocumentRepository EmployeeDocumentRepository
        {
            get
            {
                if (_employeeDocumentRepository == null)
                {
                    _employeeDocumentRepository = new EmployeeDocumentRepository();
                }
                return _employeeDocumentRepository;
            }
        }
        
        /// <summary>
        /// مستودع بيانات الرواتب
        /// </summary>
        public PayrollRepository PayrollRepository
        {
            get
            {
                if (_payrollRepository == null)
                {
                    _payrollRepository = new PayrollRepository();
                }
                return _payrollRepository;
            }
        }
        
        /// <summary>
        /// مستودع بيانات البدلات
        /// </summary>
        public AllowanceRepository AllowanceRepository
        {
            get
            {
                if (_allowanceRepository == null)
                {
                    _allowanceRepository = new AllowanceRepository();
                }
                return _allowanceRepository;
            }
        }
        
        /// <summary>
        /// مستودع بيانات الخصومات
        /// </summary>
        public DeductionRepository DeductionRepository
        {
            get
            {
                if (_deductionRepository == null)
                {
                    _deductionRepository = new DeductionRepository();
                }
                return _deductionRepository;
            }
        }
        
        /// <summary>
        /// مستودع بيانات القروض
        /// </summary>
        public LoanRepository LoanRepository
        {
            get
            {
                if (_loanRepository == null)
                {
                    _loanRepository = new LoanRepository();
                }
                return _loanRepository;
            }
        }
        
        /// <summary>
        /// تحرير الموارد
        /// </summary>
        public void Dispose()
        {
            // التحرير إذا كان هناك موارد تحتاج إلى ذلك
            GC.SuppressFinalize(this);
        }
    }
    
    /// <summary>
    /// مدير السجلات
    /// </summary>
    public static class LogManager
    {
        /// <summary>
        /// تسجيل استثناء
        /// </summary>
        public static void LogException(Exception ex, string message = null)
        {
            try
            {
                // تسجيل الاستثناء في ملف أو قاعدة بيانات
                string logMessage = $"[{DateTime.Now}] ERROR: {message ?? "حدث خطأ"} - {ex.Message}";
                
                // طباعة الخطأ في وحدة التحكم (للتطوير)
                Console.WriteLine(logMessage);
                
                // يمكن إضافة آلية حفظ السجلات إلى ملف أو قاعدة البيانات هنا
            }
            catch
            {
                // تجاهل أي أخطاء في عملية التسجيل نفسها
            }
        }
        
        /// <summary>
        /// تسجيل معلومة
        /// </summary>
        public static void LogInfo(string message)
        {
            try
            {
                // تسجيل المعلومة في ملف أو قاعدة بيانات
                string logMessage = $"[{DateTime.Now}] INFO: {message}";
                
                // طباعة المعلومة في وحدة التحكم (للتطوير)
                Console.WriteLine(logMessage);
                
                // يمكن إضافة آلية حفظ السجلات إلى ملف أو قاعدة البيانات هنا
            }
            catch
            {
                // تجاهل أي أخطاء في عملية التسجيل نفسها
            }
        }
        
        /// <summary>
        /// تسجيل تحذير
        /// </summary>
        public static void LogWarning(string message)
        {
            try
            {
                // تسجيل التحذير في ملف أو قاعدة بيانات
                string logMessage = $"[{DateTime.Now}] WARNING: {message}";
                
                // طباعة التحذير في وحدة التحكم (للتطوير)
                Console.WriteLine(logMessage);
                
                // يمكن إضافة آلية حفظ السجلات إلى ملف أو قاعدة البيانات هنا
            }
            catch
            {
                // تجاهل أي أخطاء في عملية التسجيل نفسها
            }
        }
    }
}