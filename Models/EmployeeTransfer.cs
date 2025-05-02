using System;

namespace HR.Models
{
    /// <summary>
    /// نموذج تاريخ النقل والترقيات للموظف
    /// </summary>
    public class EmployeeTransfer
    {
        /// <summary>
        /// معرف النقل/الترقية
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// الموظف المرتبط
        /// </summary>
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// نوع النقل (نقل، ترقية، إلخ)
        /// </summary>
        public string TransferType { get; set; }

        /// <summary>
        /// معرف القسم السابق
        /// </summary>
        public int? FromDepartmentID { get; set; }

        /// <summary>
        /// القسم السابق
        /// </summary>
        public virtual Department FromDepartment { get; set; }

        /// <summary>
        /// معرف القسم الجديد
        /// </summary>
        public int? ToDepartmentID { get; set; }

        /// <summary>
        /// القسم الجديد
        /// </summary>
        public virtual Department ToDepartment { get; set; }

        /// <summary>
        /// معرف المسمى الوظيفي السابق
        /// </summary>
        public int? FromPositionID { get; set; }

        /// <summary>
        /// المسمى الوظيفي السابق
        /// </summary>
        public virtual Position FromPosition { get; set; }

        /// <summary>
        /// معرف المسمى الوظيفي الجديد
        /// </summary>
        public int? ToPositionID { get; set; }

        /// <summary>
        /// المسمى الوظيفي الجديد
        /// </summary>
        public virtual Position ToPosition { get; set; }

        /// <summary>
        /// تاريخ سريان النقل/الترقية
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// سبب النقل/الترقية
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// ملاحظات
        /// </summary>
        public string Notes { get; set; }

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
    }
}