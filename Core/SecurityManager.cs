using System;
using System.Security.Cryptography;
using System.Text;

namespace HR.Core
{
    /// <summary>
    /// مدير الأمان والتشفير
    /// </summary>
    public class SecurityManager
    {
        /// <summary>
        /// طول الملح (بالبايت) المستخدم في التشفير
        /// </summary>
        private const int SaltSize = 32;

        /// <summary>
        /// عدد تكرارات الهاش لتقوية التشفير
        /// </summary>
        private const int HashIterations = 10000;

        /// <summary>
        /// إنشاء ملح عشوائي للتشفير
        /// </summary>
        /// <returns>الملح كسلسلة نصية</returns>
        public static string GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);
                
                // تحويل الملح إلى سلسلة نصية Base64
                return Convert.ToBase64String(salt);
            }
        }

        /// <summary>
        /// تشفير كلمة المرور باستخدام ملح محدد
        /// </summary>
        /// <param name="password">كلمة المرور</param>
        /// <param name="salt">الملح</param>
        /// <returns>كلمة المرور المشفرة</returns>
        public static string HashPassword(string password, string salt)
        {
            // تحويل الملح من سلسلة نصية Base64 إلى مصفوفة بايت
            byte[] saltBytes = Convert.FromBase64String(salt);
            
            // تشفير كلمة المرور باستخدام PBKDF2
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, HashIterations))
            {
                byte[] hash = pbkdf2.GetBytes(SaltSize);
                
                // تحويل الهاش إلى سلسلة نصية Base64
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// إنشاء كلمة مرور وملح وهاش جديدة
        /// </summary>
        /// <param name="password">كلمة المرور</param>
        /// <param name="salt">الملح المُنتج</param>
        /// <param name="passwordHash">هاش كلمة المرور المُنتج</param>
        public static void CreatePasswordHash(string password, out string salt, out string passwordHash)
        {
            salt = GenerateSalt();
            passwordHash = HashPassword(password, salt);
        }

        /// <summary>
        /// التحقق من صحة كلمة المرور
        /// </summary>
        /// <param name="password">كلمة المرور المدخلة</param>
        /// <param name="salt">الملح المخزن</param>
        /// <param name="passwordHash">هاش كلمة المرور المخزن</param>
        /// <returns>هل كلمة المرور صحيحة؟</returns>
        public static bool VerifyPassword(string password, string salt, string passwordHash)
        {
            string computedHash = HashPassword(password, salt);
            return computedHash == passwordHash;
        }

        /// <summary>
        /// إنشاء مفتاح عشوائي بطول محدد
        /// </summary>
        /// <param name="length">طول المفتاح</param>
        /// <returns>المفتاح المُنتج</returns>
        public static string GenerateRandomKey(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] keyBytes = new byte[length];
                rng.GetBytes(keyBytes);
                return Convert.ToBase64String(keyBytes);
            }
        }

        /// <summary>
        /// إنشاء رمز تفعيل عشوائي
        /// </summary>
        /// <param name="length">طول الرمز</param>
        /// <returns>رمز التفعيل</returns>
        public static string GenerateActivationCode(int length = 6)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] codeBytes = new byte[4]; // 4 bytes = 32 bits = 10^10-1 as max value
                rng.GetBytes(codeBytes);
                uint randomValue = BitConverter.ToUInt32(codeBytes, 0);
                
                // تحويل القيمة العشوائية إلى رمز تفعيل برقمي
                return (randomValue % (uint)Math.Pow(10, length)).ToString($"D{length}");
            }
        }

        /// <summary>
        /// تشفير نص
        /// </summary>
        /// <param name="plainText">النص المراد تشفيره</param>
        /// <param name="key">المفتاح السري</param>
        /// <returns>النص المشفر</returns>
        public static string Encrypt(string plainText, string key)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] keyBytes = Convert.FromBase64String(key);
            
            using (var aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.GenerateIV();
                
                using (var encryptor = aes.CreateEncryptor())
                {
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    
                    // دمج متجه التهيئة (IV) مع النص المشفر
                    byte[] result = new byte[aes.IV.Length + encryptedBytes.Length];
                    Array.Copy(aes.IV, 0, result, 0, aes.IV.Length);
                    Array.Copy(encryptedBytes, 0, result, aes.IV.Length, encryptedBytes.Length);
                    
                    return Convert.ToBase64String(result);
                }
            }
        }

        /// <summary>
        /// فك تشفير نص
        /// </summary>
        /// <param name="encryptedText">النص المشفر</param>
        /// <param name="key">المفتاح السري</param>
        /// <returns>النص الأصلي</returns>
        public static string Decrypt(string encryptedText, string key)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = Convert.FromBase64String(key);
            
            using (var aes = Aes.Create())
            {
                aes.Key = keyBytes;
                
                // استخراج متجه التهيئة (IV) من بداية النص المشفر
                byte[] iv = new byte[aes.BlockSize / 8];
                Array.Copy(encryptedBytes, 0, iv, 0, iv.Length);
                aes.IV = iv;
                
                // استخراج النص المشفر الفعلي
                byte[] cipherBytes = new byte[encryptedBytes.Length - iv.Length];
                Array.Copy(encryptedBytes, iv.Length, cipherBytes, 0, cipherBytes.Length);
                
                using (var decryptor = aes.CreateDecryptor())
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }

        /// <summary>
        /// إنشاء توقيع رقمي لنص
        /// </summary>
        /// <param name="data">النص المراد توقيعه</param>
        /// <param name="key">المفتاح السري</param>
        /// <returns>التوقيع الرقمي</returns>
        public static string CreateSignature(string data, string key)
        {
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            
            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] signatureBytes = hmac.ComputeHash(dataBytes);
                return Convert.ToBase64String(signatureBytes);
            }
        }

        /// <summary>
        /// التحقق من صحة توقيع رقمي
        /// </summary>
        /// <param name="data">النص الأصلي</param>
        /// <param name="signature">التوقيع الرقمي</param>
        /// <param name="key">المفتاح السري</param>
        /// <returns>هل التوقيع صحيح؟</returns>
        public static bool VerifySignature(string data, string signature, string key)
        {
            string computedSignature = CreateSignature(data, key);
            return computedSignature == signature;
        }
    }
}