using System;

namespace HR.Models
{
    /// <summary>
    /// كائن نقل البيانات الخاص بالموظفين
    /// </summary>
    public class EmployeeDTO
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
        /// الاسم الكامل للموظف
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// الجنس (ذكر، أنثى)
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// تاريخ الميلاد
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// رقم الهوية/البطاقة
        /// </summary>
        public string NationalID { get; set; }

        /// <summary>
        /// رقم جواز السفر
        /// </summary>
        public string PassportNumber { get; set; }

        /// <summary>
        /// الحالة الاجتماعية (متزوج، أعزب، إلخ)
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
        /// رقم الهاتف الثابت
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// رقم الهاتف المحمول
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// البريد الإلكتروني
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// عنوان السكن
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// جهة اتصال في حالات الطوارئ
        /// </summary>
        public string EmergencyContact { get; set; }

        /// <summary>
        /// هاتف جهة اتصال الطوارئ
        /// </summary>
        public string EmergencyPhone { get; set; }

        /// <summary>
        /// معرف القسم
        /// </summary>
        public int? DepartmentID { get; set; }

        /// <summary>
        /// اسم القسم/الإدارة التي يتبع لها الموظف
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// معرف المسمى الوظيفي
        /// </summary>
        public int? PositionID { get; set; }

        /// <summary>
        /// المسمى الوظيفي
        /// </summary>
        public string PositionTitle { get; set; }

        /// <summary>
        /// معرف المدير المباشر
        /// </summary>
        public int? DirectManagerID { get; set; }

        /// <summary>
        /// اسم المدير المباشر
        /// </summary>
        public string DirectManagerName { get; set; }

        /// <summary>
        /// تاريخ التوظيف
        /// </summary>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// تاريخ انتهاء فترة التجربة
        /// </summary>
        public DateTime? ProbationEndDate { get; set; }

        /// <summary>
        /// نوع التوظيف (دوام كامل، دوام جزئي، متعاقد، إلخ)
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
        /// اسم المناوبة
        /// </summary>
        public string WorkShiftName { get; set; }

        /// <summary>
        /// الحالة (نشط، تحت التجربة، منتهي الخدمة، إلخ)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// تاريخ إنهاء العمل
        /// </summary>
        public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// سبب إنهاء العمل
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
        /// مسار الصورة الشخصية
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// معرف البصمة في نظام ZKTeco
        /// </summary>
        public int? BiometricID { get; set; }

        /// <summary>
        /// تاريخ إنشاء السجل
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// معرف المستخدم الذي أنشأ السجل
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// تاريخ آخر تعديل للسجل
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// معرف المستخدم الذي قام بآخر تعديل
        /// </summary>
        public int? UpdatedBy { get; set; }
    }
}