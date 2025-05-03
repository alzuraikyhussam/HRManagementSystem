using System;

namespace HR.Models
{
    /// <summary>
    /// كائن يمثل سجل حضور خام من جهاز البصمة
    /// </summary>
    public class RawAttendanceLog
    {
        /// <summary>
        /// معرف السجل
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// معرف الجهاز
        /// </summary>
        public int DeviceID { get; set; }
        
        /// <summary>
        /// معرف المستخدم في جهاز البصمة
        /// </summary>
        public int BiometricUserID { get; set; }
        
        /// <summary>
        /// وقت تسجيل البصمة
        /// </summary>
        public DateTime LogDateTime { get; set; }
        
        /// <summary>
        /// نوع البصمة (دخول أو خروج)
        /// </summary>
        public int? LogType { get; set; }
        
        /// <summary>
        /// طريقة التحقق (بصمة، بطاقة، كلمة مرور)
        /// </summary>
        public int? VerifyMode { get; set; }
        
        /// <summary>
        /// رمز العمل
        /// </summary>
        public int? WorkCode { get; set; }
        
        /// <summary>
        /// هل تمت معالجة السجل
        /// </summary>
        public bool IsProcessed { get; set; }
        
        /// <summary>
        /// هل تم ربط السجل بموظف
        /// </summary>
        public bool IsMatched { get; set; }
        
        /// <summary>
        /// معرف الموظف
        /// </summary>
        public int? EmployeeID { get; set; }
        
        /// <summary>
        /// وقت المزامنة
        /// </summary>
        public DateTime SyncTime { get; set; }
    }
}