using System;
using System.Collections.Generic;

namespace HR.Models
{
    /// <summary>
    /// نموذج بيانات الموظف
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// الرقم الوظيفي للموظف
        /// </summary>
        public string EmployeeNumber { get; set; }

        /// <summary>
        /// الاسم الأول
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// الاسم الأوسط
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// اسم العائلة
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// الاسم الكامل (محسوب)
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// الجنس
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// تاريخ الميلاد
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// رقم الهوية
        /// </summary>
        public string NationalID { get; set; }

        /// <summary>
        /// رقم جواز السفر
        /// </summary>
        public string PassportNumber { get; set; }

        /// <summary>
        /// الحالة الاجتماعية
        /// </summary>
        public string MaritalStatus { get; set; }

        /// <summary>
        /// الجنسية
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// الديانة
        /// </summary>
        public string Religion { get; set; }

        /// <summary>
        /// فصيلة الدم
        /// </summary>
        public string BloodType { get; set; }

        /// <summary>
        /// رقم الهاتف
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// رقم الجوال
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// البريد الإلكتروني
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// العنوان
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// اسم جهة الاتصال في حالات الطوارئ
        /// </summary>
        public string EmergencyContact { get; set; }

        /// <summary>
        /// رقم هاتف جهة الاتصال في حالات الطوارئ
        /// </summary>
        public string EmergencyPhone { get; set; }

        /// <summary>
        /// معرف القسم
        /// </summary>
        public int? DepartmentID { get; set; }

        /// <summary>
        /// القسم
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        /// معرف المسمى الوظيفي
        /// </summary>
        public int? PositionID { get; set; }

        /// <summary>
        /// المسمى الوظيفي
        /// </summary>
        public virtual Position Position { get; set; }

        /// <summary>
        /// معرف المدير المباشر
        /// </summary>
        public int? DirectManagerID { get; set; }

        /// <summary>
        /// المدير المباشر
        /// </summary>
        public virtual Employee DirectManager { get; set; }

        /// <summary>
        /// الموظفين التابعين للمدير
        /// </summary>
        public virtual ICollection<Employee> Subordinates { get; set; }

        /// <summary>
        /// تاريخ التوظيف
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// تاريخ انتهاء فترة التجربة
        /// </summary>
        public DateTime? ProbationEndDate { get; set; }

        /// <summary>
        /// نوع التوظيف
        /// </summary>
        public string EmploymentType { get; set; }

        /// <summary>
        /// تاريخ بداية العقد
        /// </summary>
        public DateTime? ContractStartDate { get; set; }

        /// <summary>
        /// تاريخ نهاية العقد
        /// </summary>
        public DateTime? ContractEndDate { get; set; }

        /// <summary>
        /// معرف المناوبة
        /// </summary>
        public int? WorkShiftID { get; set; }

        /// <summary>
        /// حالة الموظف
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// تاريخ انهاء الخدمة
        /// </summary>
        public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// سبب انهاء الخدمة
        /// </summary>
        public string TerminationReason { get; set; }

        /// <summary>
        /// اسم البنك
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// فرع البنك
        /// </summary>
        public string BankBranch { get; set; }

        /// <summary>
        /// رقم الحساب البنكي
        /// </summary>
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// رقم الآيبان
        /// </summary>
        public string IBAN { get; set; }

        /// <summary>
        /// الصورة الشخصية
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// معرف البصمة
        /// </summary>
        public int? BiometricID { get; set; }

        /// <summary>
        /// تاريخ الإنشاء
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// منشئ السجل
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// تاريخ التعديل
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// معدل السجل
        /// </summary>
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// بيانات المستخدم المرتبط
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// مستندات الموظف
        /// </summary>
        public virtual ICollection<EmployeeDocument> Documents { get; set; }

        /// <summary>
        /// سجلات الحضور والانصراف
        /// </summary>
        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; }

        /// <summary>
        /// سجلات النقل والترقية
        /// </summary>
        public virtual ICollection<EmployeeTransfer> Transfers { get; set; }

        /// <summary>
        /// طلبات الإجازات
        /// </summary>
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}