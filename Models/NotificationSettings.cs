using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR.Core
{
    /// <summary>
    /// كائن إعدادات الإشعارات
    /// </summary>
    public class NotificationSettings
    {
        /// <summary>
        /// تفعيل الإشعارات
        /// </summary>
        public bool EnableNotifications { get; set; }
        
        /// <summary>
        /// تفعيل إشعارات البريد الإلكتروني
        /// </summary>
        public bool EnableEmailNotifications { get; set; }
        
        /// <summary>
        /// تفعيل إشعارات الرسائل النصية
        /// </summary>
        public bool EnableSMSNotifications { get; set; }
        
        /// <summary>
        /// تفعيل إشعارات النظام
        /// </summary>
        public bool EnableSystemNotifications { get; set; }
        
        /// <summary>
        /// إشعار عند طلب إجازة جديد
        /// </summary>
        public bool NotifyOnNewLeaveRequest { get; set; }
        
        /// <summary>
        /// إشعار عند قبول طلب الإجازة
        /// </summary>
        public bool NotifyOnLeaveRequestApproved { get; set; }
        
        /// <summary>
        /// إشعار عند رفض طلب الإجازة
        /// </summary>
        public bool NotifyOnLeaveRequestRejected { get; set; }
        
        /// <summary>
        /// إشعار عند إضافة موظف جديد
        /// </summary>
        public bool NotifyOnNewEmployee { get; set; }
        
        /// <summary>
        /// إشعار عند إنهاء خدمة موظف
        /// </summary>
        public bool NotifyOnEmployeeTermination { get; set; }
        
        /// <summary>
        /// إشعار عند مشكلة في الحضور
        /// </summary>
        public bool NotifyOnAttendanceIssue { get; set; }
        
        /// <summary>
        /// إشعار عند مشكلة في الراتب
        /// </summary>
        public bool NotifyOnSalaryIssue { get; set; }
        
        /// <summary>
        /// مستوى الإشعارات
        /// </summary>
        public NotificationLevel NotificationLevel { get; set; }
        
        /// <summary>
        /// ساعة الإشعار اليومي (0-23)
        /// </summary>
        public int DailyNotificationHour { get; set; }
    }
    
    /// <summary>
    /// مستوى الإشعارات
    /// </summary>
    public enum NotificationLevel
    {
        /// <summary>
        /// منخفض (الأحداث المهمة فقط)
        /// </summary>
        Low = 0,
        
        /// <summary>
        /// متوسط (الأحداث المهمة والمتوسطة)
        /// </summary>
        Medium = 1,
        
        /// <summary>
        /// عالي (جميع الأحداث)
        /// </summary>
        High = 2
    }
}