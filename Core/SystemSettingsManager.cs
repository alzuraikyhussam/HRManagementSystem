using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HR.Core
{
    /// <summary>
    /// إدارة إعدادات النظام
    /// </summary>
    public class SystemSettingsManager
    {
        private readonly ConnectionManager _connectionManager;
        
        public SystemSettingsManager()
        {
            _connectionManager = new ConnectionManager();
        }
        
        /// <summary>
        /// الحصول على قيمة إعداد معين
        /// </summary>
        /// <param name="key">مفتاح الإعداد</param>
        /// <param name="defaultValue">القيمة الافتراضية في حالة عدم وجود الإعداد</param>
        /// <returns>قيمة الإعداد</returns>
        public string GetSettingValue(string key, string defaultValue = "")
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = "SELECT SettingValue FROM SystemSettings WHERE SettingKey = @SettingKey";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SettingKey", key);
                        
                        connection.Open();
                        object result = command.ExecuteScalar();
                        
                        if (result != null && result != DBNull.Value)
                        {
                            return result.ToString();
                        }
                    }
                }
                
                return defaultValue;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء استرجاع إعداد: {key}");
                return defaultValue;
            }
        }
        
        /// <summary>
        /// الحصول على قيمة إعداد بنوع محدد
        /// </summary>
        /// <typeparam name="T">نوع القيمة المرجعة</typeparam>
        /// <param name="key">مفتاح الإعداد</param>
        /// <param name="defaultValue">القيمة الافتراضية في حالة عدم وجود الإعداد</param>
        /// <returns>قيمة الإعداد</returns>
        public T GetSettingValue<T>(string key, T defaultValue = default)
        {
            string stringValue = GetSettingValue(key, string.Empty);
            
            if (string.IsNullOrEmpty(stringValue))
            {
                return defaultValue;
            }
            
            try
            {
                // محاولة تحويل القيمة إلى النوع المطلوب
                return (T)Convert.ChangeType(stringValue, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
        
        /// <summary>
        /// حفظ قيمة إعداد
        /// </summary>
        /// <param name="key">مفتاح الإعداد</param>
        /// <param name="value">قيمة الإعداد</param>
        /// <param name="group">مجموعة الإعداد</param>
        /// <param name="description">وصف الإعداد</param>
        /// <param name="dataType">نوع البيانات</param>
        /// <returns>نجاح العملية</returns>
        public bool SaveSettingValue(string key, string value, string group = "", string description = "", string dataType = "")
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    // التحقق من وجود الإعداد
                    string checkQuery = "SELECT COUNT(*) FROM SystemSettings WHERE SettingKey = @SettingKey";
                    
                    connection.Open();
                    
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@SettingKey", key);
                        int count = (int)checkCommand.ExecuteScalar();
                        
                        // تحديث أو إضافة الإعداد
                        string query = count > 0 ?
                            "UPDATE SystemSettings SET SettingValue = @SettingValue, UpdatedAt = GETDATE() WHERE SettingKey = @SettingKey" :
                            "INSERT INTO SystemSettings (SettingKey, SettingValue, SettingGroup, Description, DataType, IsVisible, IsEditable, CreatedAt) " +
                            "VALUES (@SettingKey, @SettingValue, @SettingGroup, @Description, @DataType, 1, 1, GETDATE())";
                        
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@SettingKey", key);
                            command.Parameters.AddWithValue("@SettingValue", value);
                            
                            // إضافة المعلمات الإضافية في حالة الإدراج
                            if (count == 0)
                            {
                                command.Parameters.AddWithValue("@SettingGroup", string.IsNullOrEmpty(group) ? (object)DBNull.Value : group);
                                command.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(description) ? (object)DBNull.Value : description);
                                command.Parameters.AddWithValue("@DataType", string.IsNullOrEmpty(dataType) ? (object)DBNull.Value : dataType);
                            }
                            
                            return command.ExecuteNonQuery() > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء حفظ إعداد: {key}");
                return false;
            }
        }
        
        /// <summary>
        /// حفظ قيمة إعداد بنوع محدد
        /// </summary>
        /// <typeparam name="T">نوع القيمة</typeparam>
        /// <param name="key">مفتاح الإعداد</param>
        /// <param name="value">قيمة الإعداد</param>
        /// <param name="group">مجموعة الإعداد</param>
        /// <param name="description">وصف الإعداد</param>
        /// <returns>نجاح العملية</returns>
        public bool SaveSettingValue<T>(string key, T value, string group = "", string description = "")
        {
            string dataType = typeof(T).Name;
            string stringValue = Convert.ToString(value);
            
            return SaveSettingValue(key, stringValue, group, description, dataType);
        }
        
        /// <summary>
        /// الحصول على مجموعة من الإعدادات حسب مجموعة معينة
        /// </summary>
        /// <param name="group">مجموعة الإعدادات</param>
        /// <returns>قائمة بالإعدادات</returns>
        public Dictionary<string, string> GetSettingsByGroup(string group)
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = "SELECT SettingKey, SettingValue FROM SystemSettings WHERE SettingGroup = @SettingGroup";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SettingGroup", group);
                        
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string key = reader["SettingKey"].ToString();
                                string value = reader["SettingValue"].ToString();
                                
                                settings[key] = value;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, $"حدث خطأ أثناء استرجاع إعدادات المجموعة: {group}");
            }
            
            return settings;
        }
        
        /// <summary>
        /// الحصول على جميع الإعدادات
        /// </summary>
        /// <returns>جدول بيانات يحتوي على جميع الإعدادات</returns>
        public DataTable GetAllSettings()
        {
            DataTable settings = new DataTable();
            
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                {
                    string query = "SELECT * FROM SystemSettings ORDER BY SettingGroup, SettingKey";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(settings);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء استرجاع جميع الإعدادات");
            }
            
            return settings;
        }
        
        /// <summary>
        /// الحصول على الإعدادات العامة
        /// </summary>
        /// <returns>كائن الإعدادات العامة</returns>
        public GeneralSettings GetGeneralSettings()
        {
            GeneralSettings settings = new GeneralSettings();
            
            try
            {
                // استرجاع إعدادات المجموعة
                Dictionary<string, string> generalSettings = GetSettingsByGroup("General");
                
                // تعبئة البيانات
                settings.CompanyName = GetSettingFromDictionary(generalSettings, "CompanyName", "شركة نظام الموارد البشرية");
                settings.CompanyLogo = GetSettingFromDictionary(generalSettings, "CompanyLogo", "");
                settings.SystemTitle = GetSettingFromDictionary(generalSettings, "SystemTitle", "نظام إدارة الموارد البشرية");
                
                // إعدادات اللغة والتنسيق
                settings.DefaultLanguage = GetSettingFromDictionary(generalSettings, "DefaultLanguage", "ar");
                settings.DefaultDateFormat = GetSettingFromDictionary(generalSettings, "DefaultDateFormat", "dd/MM/yyyy");
                settings.DefaultTimeFormat = GetSettingFromDictionary(generalSettings, "DefaultTimeFormat", "hh:mm tt");
                settings.DefaultCurrency = GetSettingFromDictionary(generalSettings, "DefaultCurrency", "ر.س");
                
                // إعدادات الإشعارات
                settings.EnableNotifications = GetSettingFromDictionaryBool(generalSettings, "EnableNotifications", true);
                settings.EnableEmails = GetSettingFromDictionaryBool(generalSettings, "EnableEmails", false);
                settings.EnableSMS = GetSettingFromDictionaryBool(generalSettings, "EnableSMS", false);
                
                // إعدادات البريد الإلكتروني
                settings.SystemEmail = GetSettingFromDictionary(generalSettings, "SystemEmail", "");
                settings.SMTPServer = GetSettingFromDictionary(generalSettings, "SMTPServer", "");
                settings.SMTPPort = GetSettingFromDictionaryInt(generalSettings, "SMTPPort", 587);
                settings.SMTPUsername = GetSettingFromDictionary(generalSettings, "SMTPUsername", "");
                settings.SMTPPassword = GetSettingFromDictionary(generalSettings, "SMTPPassword", "");
                settings.EnableSSL = GetSettingFromDictionaryBool(generalSettings, "EnableSSL", true);
                
                // إعدادات الرسائل النصية
                settings.SMSGatewayURL = GetSettingFromDictionary(generalSettings, "SMSGatewayURL", "");
                settings.SMSUsername = GetSettingFromDictionary(generalSettings, "SMSUsername", "");
                settings.SMSPassword = GetSettingFromDictionary(generalSettings, "SMSPassword", "");
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء استرجاع الإعدادات العامة");
            }
            
            return settings;
        }
        
        /// <summary>
        /// حفظ الإعدادات العامة
        /// </summary>
        /// <param name="settings">كائن الإعدادات العامة</param>
        /// <returns>نجاح العملية</returns>
        public bool SaveGeneralSettings(GeneralSettings settings)
        {
            try
            {
                // حفظ الإعدادات العامة
                SaveSettingValue("CompanyName", settings.CompanyName, "General", "اسم الشركة", "String");
                SaveSettingValue("CompanyLogo", settings.CompanyLogo, "General", "شعار الشركة", "String");
                SaveSettingValue("SystemTitle", settings.SystemTitle, "General", "عنوان النظام", "String");
                
                // إعدادات اللغة والتنسيق
                SaveSettingValue("DefaultLanguage", settings.DefaultLanguage, "General", "اللغة الافتراضية", "String");
                SaveSettingValue("DefaultDateFormat", settings.DefaultDateFormat, "General", "تنسيق التاريخ الافتراضي", "String");
                SaveSettingValue("DefaultTimeFormat", settings.DefaultTimeFormat, "General", "تنسيق الوقت الافتراضي", "String");
                SaveSettingValue("DefaultCurrency", settings.DefaultCurrency, "General", "العملة الافتراضية", "String");
                
                // إعدادات الإشعارات
                SaveSettingValue("EnableNotifications", settings.EnableNotifications.ToString(), "General", "تفعيل الإشعارات", "Boolean");
                SaveSettingValue("EnableEmails", settings.EnableEmails.ToString(), "General", "تفعيل إشعارات البريد الإلكتروني", "Boolean");
                SaveSettingValue("EnableSMS", settings.EnableSMS.ToString(), "General", "تفعيل إشعارات الرسائل النصية", "Boolean");
                
                // إعدادات البريد الإلكتروني
                SaveSettingValue("SystemEmail", settings.SystemEmail, "General", "البريد الإلكتروني للنظام", "String");
                SaveSettingValue("SMTPServer", settings.SMTPServer, "General", "خادم SMTP", "String");
                SaveSettingValue("SMTPPort", settings.SMTPPort.ToString(), "General", "منفذ SMTP", "Int32");
                SaveSettingValue("SMTPUsername", settings.SMTPUsername, "General", "اسم مستخدم SMTP", "String");
                SaveSettingValue("SMTPPassword", settings.SMTPPassword, "General", "كلمة مرور SMTP", "String");
                SaveSettingValue("EnableSSL", settings.EnableSSL.ToString(), "General", "تفعيل SSL", "Boolean");
                
                // إعدادات الرسائل النصية
                SaveSettingValue("SMSGatewayURL", settings.SMSGatewayURL, "General", "رابط بوابة الرسائل النصية", "String");
                SaveSettingValue("SMSUsername", settings.SMSUsername, "General", "اسم مستخدم الرسائل النصية", "String");
                SaveSettingValue("SMSPassword", settings.SMSPassword, "General", "كلمة مرور الرسائل النصية", "String");
                
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء حفظ الإعدادات العامة");
                return false;
            }
        }
        
        /// <summary>
        /// الحصول على إعدادات أيام العمل
        /// </summary>
        /// <returns>كائن إعدادات أيام العمل</returns>
        public WorkDaysSettings GetWorkDaysSettings()
        {
            WorkDaysSettings settings = new WorkDaysSettings();
            
            try
            {
                // استرجاع إعدادات المجموعة
                Dictionary<string, string> workDaysSettings = GetSettingsByGroup("WorkDays");
                
                // تعبئة البيانات
                // أيام العمل
                settings.Saturday = GetSettingFromDictionaryBool(workDaysSettings, "Saturday", true);
                settings.Sunday = GetSettingFromDictionaryBool(workDaysSettings, "Sunday", true);
                settings.Monday = GetSettingFromDictionaryBool(workDaysSettings, "Monday", true);
                settings.Tuesday = GetSettingFromDictionaryBool(workDaysSettings, "Tuesday", true);
                settings.Wednesday = GetSettingFromDictionaryBool(workDaysSettings, "Wednesday", true);
                settings.Thursday = GetSettingFromDictionaryBool(workDaysSettings, "Thursday", true);
                settings.Friday = GetSettingFromDictionaryBool(workDaysSettings, "Friday", false);
                
                // أوقات الدوام
                settings.SaturdayStartTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "SaturdayStartTime", new TimeSpan(8, 0, 0));
                settings.SaturdayEndTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "SaturdayEndTime", new TimeSpan(16, 0, 0));
                settings.SundayStartTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "SundayStartTime", new TimeSpan(8, 0, 0));
                settings.SundayEndTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "SundayEndTime", new TimeSpan(16, 0, 0));
                settings.MondayStartTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "MondayStartTime", new TimeSpan(8, 0, 0));
                settings.MondayEndTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "MondayEndTime", new TimeSpan(16, 0, 0));
                settings.TuesdayStartTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "TuesdayStartTime", new TimeSpan(8, 0, 0));
                settings.TuesdayEndTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "TuesdayEndTime", new TimeSpan(16, 0, 0));
                settings.WednesdayStartTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "WednesdayStartTime", new TimeSpan(8, 0, 0));
                settings.WednesdayEndTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "WednesdayEndTime", new TimeSpan(16, 0, 0));
                settings.ThursdayStartTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "ThursdayStartTime", new TimeSpan(8, 0, 0));
                settings.ThursdayEndTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "ThursdayEndTime", new TimeSpan(16, 0, 0));
                settings.FridayStartTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "FridayStartTime", new TimeSpan(8, 0, 0));
                settings.FridayEndTime = GetSettingFromDictionaryTimeSpan(workDaysSettings, "FridayEndTime", new TimeSpan(16, 0, 0));
                
                // خيارات المرونة
                settings.FlexibleStartTime = GetSettingFromDictionaryBool(workDaysSettings, "FlexibleStartTime", false);
                settings.FlexibleEndTime = GetSettingFromDictionaryBool(workDaysSettings, "FlexibleEndTime", false);
                settings.GraceMinutes = GetSettingFromDictionaryInt(workDaysSettings, "GraceMinutes", 15);
                settings.ShiftWorkEnabled = GetSettingFromDictionaryBool(workDaysSettings, "ShiftWorkEnabled", false);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء استرجاع إعدادات أيام العمل");
            }
            
            return settings;
        }
        
        /// <summary>
        /// حفظ إعدادات أيام العمل
        /// </summary>
        /// <param name="settings">كائن إعدادات أيام العمل</param>
        /// <returns>نجاح العملية</returns>
        public bool SaveWorkDaysSettings(WorkDaysSettings settings)
        {
            try
            {
                // حفظ إعدادات أيام العمل
                SaveSettingValue("Saturday", settings.Saturday.ToString(), "WorkDays", "يوم السبت", "Boolean");
                SaveSettingValue("Sunday", settings.Sunday.ToString(), "WorkDays", "يوم الأحد", "Boolean");
                SaveSettingValue("Monday", settings.Monday.ToString(), "WorkDays", "يوم الإثنين", "Boolean");
                SaveSettingValue("Tuesday", settings.Tuesday.ToString(), "WorkDays", "يوم الثلاثاء", "Boolean");
                SaveSettingValue("Wednesday", settings.Wednesday.ToString(), "WorkDays", "يوم الأربعاء", "Boolean");
                SaveSettingValue("Thursday", settings.Thursday.ToString(), "WorkDays", "يوم الخميس", "Boolean");
                SaveSettingValue("Friday", settings.Friday.ToString(), "WorkDays", "يوم الجمعة", "Boolean");
                
                // أوقات الدوام
                SaveSettingValue("SaturdayStartTime", settings.SaturdayStartTime?.ToString() ?? string.Empty, "WorkDays", "وقت بداية السبت", "TimeSpan");
                SaveSettingValue("SaturdayEndTime", settings.SaturdayEndTime?.ToString() ?? string.Empty, "WorkDays", "وقت نهاية السبت", "TimeSpan");
                SaveSettingValue("SundayStartTime", settings.SundayStartTime?.ToString() ?? string.Empty, "WorkDays", "وقت بداية الأحد", "TimeSpan");
                SaveSettingValue("SundayEndTime", settings.SundayEndTime?.ToString() ?? string.Empty, "WorkDays", "وقت نهاية الأحد", "TimeSpan");
                SaveSettingValue("MondayStartTime", settings.MondayStartTime?.ToString() ?? string.Empty, "WorkDays", "وقت بداية الإثنين", "TimeSpan");
                SaveSettingValue("MondayEndTime", settings.MondayEndTime?.ToString() ?? string.Empty, "WorkDays", "وقت نهاية الإثنين", "TimeSpan");
                SaveSettingValue("TuesdayStartTime", settings.TuesdayStartTime?.ToString() ?? string.Empty, "WorkDays", "وقت بداية الثلاثاء", "TimeSpan");
                SaveSettingValue("TuesdayEndTime", settings.TuesdayEndTime?.ToString() ?? string.Empty, "WorkDays", "وقت نهاية الثلاثاء", "TimeSpan");
                SaveSettingValue("WednesdayStartTime", settings.WednesdayStartTime?.ToString() ?? string.Empty, "WorkDays", "وقت بداية الأربعاء", "TimeSpan");
                SaveSettingValue("WednesdayEndTime", settings.WednesdayEndTime?.ToString() ?? string.Empty, "WorkDays", "وقت نهاية الأربعاء", "TimeSpan");
                SaveSettingValue("ThursdayStartTime", settings.ThursdayStartTime?.ToString() ?? string.Empty, "WorkDays", "وقت بداية الخميس", "TimeSpan");
                SaveSettingValue("ThursdayEndTime", settings.ThursdayEndTime?.ToString() ?? string.Empty, "WorkDays", "وقت نهاية الخميس", "TimeSpan");
                SaveSettingValue("FridayStartTime", settings.FridayStartTime?.ToString() ?? string.Empty, "WorkDays", "وقت بداية الجمعة", "TimeSpan");
                SaveSettingValue("FridayEndTime", settings.FridayEndTime?.ToString() ?? string.Empty, "WorkDays", "وقت نهاية الجمعة", "TimeSpan");
                
                // خيارات المرونة
                SaveSettingValue("FlexibleStartTime", settings.FlexibleStartTime.ToString(), "WorkDays", "تفعيل المرونة في وقت الحضور", "Boolean");
                SaveSettingValue("FlexibleEndTime", settings.FlexibleEndTime.ToString(), "WorkDays", "تفعيل المرونة في وقت المغادرة", "Boolean");
                SaveSettingValue("GraceMinutes", settings.GraceMinutes.ToString(), "WorkDays", "دقائق السماح للتأخير", "Int32");
                SaveSettingValue("ShiftWorkEnabled", settings.ShiftWorkEnabled.ToString(), "WorkDays", "تفعيل نظام المناوبات", "Boolean");
                
                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "حدث خطأ أثناء حفظ إعدادات أيام العمل");
                return false;
            }
        }
        
        #region Helper Methods
        
        private string GetSettingFromDictionary(Dictionary<string, string> settings, string key, string defaultValue)
        {
            return settings.ContainsKey(key) ? settings[key] : defaultValue;
        }
        
        private bool GetSettingFromDictionaryBool(Dictionary<string, string> settings, string key, bool defaultValue)
        {
            if (settings.ContainsKey(key))
            {
                bool result;
                if (bool.TryParse(settings[key], out result))
                {
                    return result;
                }
            }
            
            return defaultValue;
        }
        
        private int GetSettingFromDictionaryInt(Dictionary<string, string> settings, string key, int defaultValue)
        {
            if (settings.ContainsKey(key))
            {
                int result;
                if (int.TryParse(settings[key], out result))
                {
                    return result;
                }
            }
            
            return defaultValue;
        }
        
        private decimal GetSettingFromDictionaryDecimal(Dictionary<string, string> settings, string key, decimal defaultValue)
        {
            if (settings.ContainsKey(key))
            {
                decimal result;
                if (decimal.TryParse(settings[key], out result))
                {
                    return result;
                }
            }
            
            return defaultValue;
        }
        
        private DateTime? GetSettingFromDictionaryDateTime(Dictionary<string, string> settings, string key, DateTime? defaultValue)
        {
            if (settings.ContainsKey(key))
            {
                DateTime result;
                if (DateTime.TryParse(settings[key], out result))
                {
                    return result;
                }
            }
            
            return defaultValue;
        }
        
        private TimeSpan? GetSettingFromDictionaryTimeSpan(Dictionary<string, string> settings, string key, TimeSpan? defaultValue)
        {
            if (settings.ContainsKey(key) && !string.IsNullOrEmpty(settings[key]))
            {
                TimeSpan result;
                if (TimeSpan.TryParse(settings[key], out result))
                {
                    return result;
                }
            }
            
            return defaultValue;
        }
        
        #endregion
    }
    
    /// <summary>
    /// الإعدادات العامة
    /// </summary>
    public class GeneralSettings
    {
        /// <summary>
        /// اسم الشركة
        /// </summary>
        public string CompanyName { get; set; }
        
        /// <summary>
        /// شعار الشركة
        /// </summary>
        public string CompanyLogo { get; set; }
        
        /// <summary>
        /// عنوان النظام
        /// </summary>
        public string SystemTitle { get; set; }
        
        /// <summary>
        /// اللغة الافتراضية
        /// </summary>
        public string DefaultLanguage { get; set; }
        
        /// <summary>
        /// تنسيق التاريخ الافتراضي
        /// </summary>
        public string DefaultDateFormat { get; set; }
        
        /// <summary>
        /// تنسيق الوقت الافتراضي
        /// </summary>
        public string DefaultTimeFormat { get; set; }
        
        /// <summary>
        /// العملة الافتراضية
        /// </summary>
        public string DefaultCurrency { get; set; }
        
        /// <summary>
        /// تفعيل الإشعارات
        /// </summary>
        public bool EnableNotifications { get; set; }
        
        /// <summary>
        /// تفعيل إشعارات البريد الإلكتروني
        /// </summary>
        public bool EnableEmails { get; set; }
        
        /// <summary>
        /// تفعيل إشعارات الرسائل النصية
        /// </summary>
        public bool EnableSMS { get; set; }
        
        /// <summary>
        /// البريد الإلكتروني للنظام
        /// </summary>
        public string SystemEmail { get; set; }
        
        /// <summary>
        /// خادم SMTP
        /// </summary>
        public string SMTPServer { get; set; }
        
        /// <summary>
        /// منفذ SMTP
        /// </summary>
        public int SMTPPort { get; set; }
        
        /// <summary>
        /// اسم مستخدم SMTP
        /// </summary>
        public string SMTPUsername { get; set; }
        
        /// <summary>
        /// كلمة مرور SMTP
        /// </summary>
        public string SMTPPassword { get; set; }
        
        /// <summary>
        /// تفعيل SSL
        /// </summary>
        public bool EnableSSL { get; set; }
        
        /// <summary>
        /// رابط بوابة الرسائل النصية
        /// </summary>
        public string SMSGatewayURL { get; set; }
        
        /// <summary>
        /// اسم مستخدم الرسائل النصية
        /// </summary>
        public string SMSUsername { get; set; }
        
        /// <summary>
        /// كلمة مرور الرسائل النصية
        /// </summary>
        public string SMSPassword { get; set; }
    }
    
    /// <summary>
    /// إعدادات أيام العمل
    /// </summary>
    public class WorkDaysSettings
    {
        /// <summary>
        /// تفعيل يوم السبت
        /// </summary>
        public bool Saturday { get; set; }
        
        /// <summary>
        /// تفعيل يوم الأحد
        /// </summary>
        public bool Sunday { get; set; }
        
        /// <summary>
        /// تفعيل يوم الإثنين
        /// </summary>
        public bool Monday { get; set; }
        
        /// <summary>
        /// تفعيل يوم الثلاثاء
        /// </summary>
        public bool Tuesday { get; set; }
        
        /// <summary>
        /// تفعيل يوم الأربعاء
        /// </summary>
        public bool Wednesday { get; set; }
        
        /// <summary>
        /// تفعيل يوم الخميس
        /// </summary>
        public bool Thursday { get; set; }
        
        /// <summary>
        /// تفعيل يوم الجمعة
        /// </summary>
        public bool Friday { get; set; }
        
        /// <summary>
        /// وقت بداية العمل يوم السبت
        /// </summary>
        public TimeSpan? SaturdayStartTime { get; set; }
        
        /// <summary>
        /// وقت نهاية العمل يوم السبت
        /// </summary>
        public TimeSpan? SaturdayEndTime { get; set; }
        
        /// <summary>
        /// وقت بداية العمل يوم الأحد
        /// </summary>
        public TimeSpan? SundayStartTime { get; set; }
        
        /// <summary>
        /// وقت نهاية العمل يوم الأحد
        /// </summary>
        public TimeSpan? SundayEndTime { get; set; }
        
        /// <summary>
        /// وقت بداية العمل يوم الإثنين
        /// </summary>
        public TimeSpan? MondayStartTime { get; set; }
        
        /// <summary>
        /// وقت نهاية العمل يوم الإثنين
        /// </summary>
        public TimeSpan? MondayEndTime { get; set; }
        
        /// <summary>
        /// وقت بداية العمل يوم الثلاثاء
        /// </summary>
        public TimeSpan? TuesdayStartTime { get; set; }
        
        /// <summary>
        /// وقت نهاية العمل يوم الثلاثاء
        /// </summary>
        public TimeSpan? TuesdayEndTime { get; set; }
        
        /// <summary>
        /// وقت بداية العمل يوم الأربعاء
        /// </summary>
        public TimeSpan? WednesdayStartTime { get; set; }
        
        /// <summary>
        /// وقت نهاية العمل يوم الأربعاء
        /// </summary>
        public TimeSpan? WednesdayEndTime { get; set; }
        
        /// <summary>
        /// وقت بداية العمل يوم الخميس
        /// </summary>
        public TimeSpan? ThursdayStartTime { get; set; }
        
        /// <summary>
        /// وقت نهاية العمل يوم الخميس
        /// </summary>
        public TimeSpan? ThursdayEndTime { get; set; }
        
        /// <summary>
        /// وقت بداية العمل يوم الجمعة
        /// </summary>
        public TimeSpan? FridayStartTime { get; set; }
        
        /// <summary>
        /// وقت نهاية العمل يوم الجمعة
        /// </summary>
        public TimeSpan? FridayEndTime { get; set; }
        
        /// <summary>
        /// تفعيل المرونة في وقت الحضور
        /// </summary>
        public bool FlexibleStartTime { get; set; }
        
        /// <summary>
        /// تفعيل المرونة في وقت المغادرة
        /// </summary>
        public bool FlexibleEndTime { get; set; }
        
        /// <summary>
        /// دقائق السماح للتأخير
        /// </summary>
        public int GraceMinutes { get; set; }
        
        /// <summary>
        /// تفعيل نظام المناوبات
        /// </summary>
        public bool ShiftWorkEnabled { get; set; }
    }
}