using System;
using System.Data;
using System.Data.SqlClient;
using HR.DataAccess;

namespace HR.Core
{
    /// <summary>
    /// وحدة العمل التي تدير كافة مستودعات البيانات في النظام
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        private bool _disposed = false;

        // مستودعات البيانات
        private CompanyRepository _companyRepository;
        private DepartmentRepository _departmentRepository;
        private PositionRepository _positionRepository;
        private EmployeeRepository _employeeRepository;
        private UserRepository _userRepository;
        private RoleRepository _roleRepository;
        private WorkScheduleRepository _workScheduleRepository;
        private AttendanceRepository _attendanceRepository;
        private LeaveRepository _leaveRepository;
        private PayrollRepository _payrollRepository;
        private SystemSettingsRepository _systemSettingsRepository;

        /// <summary>
        /// إنشاء وحدة العمل
        /// </summary>
        public UnitOfWork()
        {
            _connection = ConnectionManager.CreateConnection();
        }

        /// <summary>
        /// فتح الاتصال
        /// </summary>
        public void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        /// <summary>
        /// إغلاق الاتصال
        /// </summary>
        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// بدء معاملة جديدة
        /// </summary>
        public void BeginTransaction()
        {
            OpenConnection();
            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// تأكيد المعاملة
        /// </summary>
        public void CommitTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }

        /// <summary>
        /// التراجع عن المعاملة
        /// </summary>
        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }

        #region Repositories

        /// <summary>
        /// مستودع بيانات الشركة
        /// </summary>
        public CompanyRepository CompanyRepository
        {
            get
            {
                if (_companyRepository == null)
                {
                    _companyRepository = new CompanyRepository();
                }
                return _companyRepository;
            }
        }

        /// <summary>
        /// مستودع بيانات الإدارات
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
        public PositionRepository PositionRepository
        {
            get
            {
                if (_positionRepository == null)
                {
                    _positionRepository = new PositionRepository();
                }
                return _positionRepository;
            }
        }

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
        /// مستودع بيانات الأدوار
        /// </summary>
        public RoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new RoleRepository();
                }
                return _roleRepository;
            }
        }

        /// <summary>
        /// مستودع بيانات جداول العمل
        /// </summary>
        public WorkScheduleRepository WorkScheduleRepository
        {
            get
            {
                if (_workScheduleRepository == null)
                {
                    _workScheduleRepository = new WorkScheduleRepository();
                }
                return _workScheduleRepository;
            }
        }

        /// <summary>
        /// مستودع بيانات الحضور والانصراف
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
        /// مستودع بيانات إعدادات النظام
        /// </summary>
        public SystemSettingsRepository SystemSettingsRepository
        {
            get
            {
                if (_systemSettingsRepository == null)
                {
                    _systemSettingsRepository = new SystemSettingsRepository();
                }
                return _systemSettingsRepository;
            }
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// التخلص من موارد وحدة العمل
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// التخلص من موارد وحدة العمل
        /// </summary>
        /// <param name="disposing">هل يتم التخلص من الموارد المدارة</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // التخلص من الموارد المدارة
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }

                    if (_connection != null)
                    {
                        CloseConnection();
                        _connection.Dispose();
                        _connection = null;
                    }
                }

                // التخلص من الموارد غير المدارة
                _disposed = true;
            }
        }

        /// <summary>
        /// الهادم
        /// </summary>
        ~UnitOfWork()
        {
            Dispose(false);
        }

        #endregion
    }
}