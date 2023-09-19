
using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Crescent.WinForms.Helpers
{
    /// <summary>
    /// Enables encryption/decryption through use of static methods.
    /// </summary>
    public static class EncryptionHelper
    {
        #region static Fields
        private static TripleDesEncryption _tripleDesEncryption = null;
        private static AesManagedEncryption _managedAesEncryption = null;
        #endregion

        #region static Properties

        public static TripleDesEncryption MyTripleDesEncryption => _tripleDesEncryption ?? (_tripleDesEncryption = new TripleDesEncryption());
        internal static AesManagedEncryption MyAesManagedEncryption => _managedAesEncryption ?? (_managedAesEncryption = new AesManagedEncryption());

        #endregion

        #region static Methods
        /// <summary>
        /// Decrypt bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>Decrypted data as bytes</returns>
        public static string Decrypt(byte[] bytes)
        {
            if (bytes == null)
                return null;

            if (MyAesManagedEncryption.TryDecrypt(bytes, out var plainText))
                return plainText;

            var planText = MyTripleDesEncryption.DecryptToString(bytes);
            return planText;
        }

        ///// <summary>
        ///// Encrypt bytes
        ///// </summary>
        ///// <param name="bytes"></param>
        ///// <returns>Encrypted data as bytes</returns>
        //public static byte[] Encrypt(byte[] bytes)
        //{
        //    //return MyTripleDesEncryption.Encrypt(bytes);// Create a new instance of the Aes
        //    // class.  This generates a new key and initialization 
        //    // vector (IV).
        //    using (Aes myAes = Aes.Create())
        //    {

        //        // Encrypt the string to an array of bytes.
        //        return EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);
        //    }
        //}

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns>Decrypted data as string</returns>
        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            if (MyAesManagedEncryption.TryDecrypt(cipherText, out var plainText))
                return plainText;
            return MyTripleDesEncryption.Decrypt(cipherText);
        }

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="decryptedText"></param>
        /// <returns>Decrypted data as string</returns>
        public static bool TryDecrypt(string cipherText, out string decryptedText)
        {
            if (MyAesManagedEncryption.TryDecrypt(cipherText, out var plainText))
            {
                decryptedText = plainText;
                return true;
            }
            return MyTripleDesEncryption.TryDecrypt(cipherText, out decryptedText);
        }

        public static SecureString DecryptToSecureString(string cipherText)
        {
            return Decrypt(cipherText)?.ConvertToSecureString();
        }

        /// <summary>
        /// Encrypt a string as a byte array
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>Encrypted data as string</returns>
        public static byte[] EncryptToByteArray(string plainText)
        {
            return MyAesManagedEncryption.EncryptToByteArray(plainText);
        }

        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>Encrypted data as string</returns>
        public static string EncryptToString(string plainText)
        {
            return MyAesManagedEncryption.EncryptToString(plainText);
        }

        /// <summary>
        /// Masks a Reference eg. 123456...123
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static string Mask(string reference)
        {
            string maskedReference = string.Empty;
            if (!string.IsNullOrWhiteSpace(reference))
            {
                if (reference.Length > 12)
                    maskedReference = reference.Substring(0, 6) + "..." + reference.Substring(reference.Length - 3);
                else if (reference.Length > 8)
                    maskedReference = reference.Substring(0, 4) + "..." + reference.Substring(reference.Length - 3);
                else if (reference.Length > 2)
                    maskedReference = "..." + reference.Substring(reference.Length - 2);
                else
                    maskedReference = "..." + reference;
            }
            return maskedReference;
        }

        //public static string MaskPasswordInConnectionString(string connectionString)
        //{
        //    var _connectionString = new SqlConnectionStringBuilder(connectionString);
        //    return connectionString.Replace(Decrypt(Constants.ApplicationRoleEncryptedPassword),
        //        $"{Constants.ApplicationRoleEncryptedPassword}(encrypted)");
        //}
        #endregion

        #region Mobile Encryption

        public static string MobileDecrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            return MyAesManagedEncryption.DecryptString(cipherText);
        }

        #endregion
    }
}
