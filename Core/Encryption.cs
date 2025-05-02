using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HR.Core
{
    /// <summary>
    /// Provides encryption and hashing utilities for the application
    /// </summary>
    public static class Encryption
    {
        // Salt size for password hashing
        private const int SaltSize = 16;
        
        // Size of hash
        private const int HashSize = 20;
        
        // Number of iterations for password hashing
        private const int Iterations = 10000;

        /// <summary>
        /// Creates a hash from a password with a random salt
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <param name="salt">Output parameter for the salt used</param>
        /// <returns>The password hash</returns>
        public static string HashPassword(string password, out string salt)
        {
            // Generate a random salt
            byte[] saltBytes = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            // Convert salt to base64 string for storage
            salt = Convert.ToBase64String(saltBytes);

            // Create the hash
            byte[] hashBytes = GetHashBytes(password, saltBytes);
            
            // Convert hash to base64 string for storage
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Verifies a password against a stored hash and salt
        /// </summary>
        /// <param name="password">The password to verify</param>
        /// <param name="storedHash">The stored hash to compare against</param>
        /// <param name="storedSalt">The stored salt used for hashing</param>
        /// <returns>True if the password matches, false otherwise</returns>
        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            // Convert stored salt from base64 string to bytes
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            
            // Create hash of the input password with the stored salt
            byte[] hashBytes = GetHashBytes(password, saltBytes);
            
            // Convert the computed hash to a base64 string
            string computedHash = Convert.ToBase64String(hashBytes);
            
            // Compare the computed hash with the stored hash
            return storedHash == computedHash;
        }

        /// <summary>
        /// Generates a hash for the given password and salt
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <param name="salt">The salt to use</param>
        /// <returns>The hash bytes</returns>
        private static byte[] GetHashBytes(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                return pbkdf2.GetBytes(HashSize);
            }
        }

        /// <summary>
        /// Encrypts a string using AES algorithm
        /// </summary>
        /// <param name="plainText">The text to encrypt</param>
        /// <param name="key">The encryption key</param>
        /// <returns>The encrypted text</returns>
        public static string EncryptString(string plainText, string key)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        /// <summary>
        /// Decrypts a string that was encrypted using AES algorithm
        /// </summary>
        /// <param name="cipherText">The text to decrypt</param>
        /// <param name="key">The decryption key</param>
        /// <returns>The decrypted text</returns>
        public static string DecryptString(string cipherText, string key)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
