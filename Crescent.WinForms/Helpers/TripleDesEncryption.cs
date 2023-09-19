using System;
using System.Security;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Crescent.WinForms.Helpers
{
    /// <summary>
    /// Wrapper class for Triple Des encryption
    /// </summary>
    /// <remarks>
    /// Author : Paul Hayman<br></br>
    /// Date : Feb 2006<br></br>
    /// info@PaulHayman.com
    /// http://www.geekzilla.co.uk/view7B360BD8-A77C-4F1F-BCA0-ACD0F6795F61.htm
    /// </remarks>
    public class TripleDesEncryption
    {
        #region Fields
        private readonly TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        private readonly UTF8Encoding utf8 = new UTF8Encoding();

        private readonly byte[] keyValue;
        private readonly byte[] iVValue;
        #endregion

        //#region Properties
        ///// <summary>
        ///// Key to use during encryption and decryption
        ///// </summary>
        ///// <remarks>
        ///// <example>
        ///// byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
        ///// </example>
        ///// </remarks>
        //public byte[] Key
        //{
        //    get { return keyValue; }
        //    set { keyValue = value; }
        //}

        ///// <summary>
        ///// Initialization vector to use during encryption and decryption
        ///// </summary>
        ///// <remarks>
        ///// <example>
        ///// byte[] iv = { 8, 7, 6, 5, 4, 3, 2, 1 };
        ///// </example>
        ///// </remarks>
        //public byte[] iV
        //{
        //    get { return iVValue; }
        //    set { iVValue = value; }
        //}
        //#endregion

        #region Constructors
        /// <summary>
        /// Constructor, uses the default key and initialization vetor
        /// </summary>
        public TripleDesEncryption()
        {
            this.keyValue = new byte[] { 167, 218, 104, 110, 212, 68, 110, 109, 121, 85, 236, 211, 93, 161, 82, 51, 189, 239, 38, 216, 208, 248, 246, 145 };
            this.iVValue = new byte[] { 113, 136, 132, 187, 146, 181, 41, 122 };
        }

         /// <summary>
        /// Constructor, allows the key and initialization vetor to be provided
        /// </summary>
        /// <param name="key"><see cref="Key"/></param>
        /// <param name="iV"><see cref="iV"/></param>
        public TripleDesEncryption(byte[] key, byte[] iV)
        {
            this.keyValue = key;
            this.iVValue = iV;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Decrypt bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>Decrypted data as bytes</returns>
        public byte[] Decrypt(byte[] bytes)
        {
            try
            {
                return Transform(bytes, des.CreateDecryptor(this.keyValue, this.iVValue));
            }
            catch (CryptographicException)
            {
                return bytes;
            }
            catch (System.FormatException)
            {
                return bytes;
            }
            catch (Exception ex)
            {
                //OpenOffice.ExceptionHandler.ExceptionHandler.HandleExceptionAsync(ex, "Decrypting bytes");
                return null;
            }
        }
        public string DecryptToString(byte[] bytes)
        {
            if (bytes == null)
                return null;

            var decryptedBytes = Decrypt(bytes);
            return utf8.GetString(decryptedBytes);
        }

        ///// <summary>
        ///// Encrypt bytes
        ///// </summary>
        ///// <param name="bytes"></param>
        ///// <returns>Encrypted data as bytes</returns>
        //public byte[] Encrypt(byte[] bytes)
        //{
        //    return Transform(bytes, des.CreateEncryptor(this.keyValue, this.iVValue));
        //}

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Decrypted data as string</returns>
        public string Decrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            return (TryDecrypt(text, out var decryptedText))
                ? decryptedText
                : text;
        }

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="decryptedText"></param>
        /// <returns>Decrypted data as string</returns>
        public Boolean TryDecrypt(string text, out string decryptedText)
        {
            decryptedText = null;
            try
            {
                if (text == null)
                    return true;
                if (text == string.Empty)
                {
                    decryptedText = string.Empty;
                }
                else
                {
                    byte[] input = Convert.FromBase64String(text);
                    byte[] output = Transform(input, des.CreateDecryptor(this.keyValue, this.iVValue));
                    decryptedText = utf8.GetString(output);
                }
                return true;
            }
            catch (CryptographicException)
            {
                return false;
            }
            catch (System.FormatException)
            {
                return false;
            }
            catch (Exception ex)
            {
                //ex.AppendData("text", text);
                //OpenOffice.ExceptionHandler.ExceptionHandler.HandleException(ex, "Decrypting text");
                return false;
            }
        }

        public SecureString DecryptToSecureString(string text)
        {
            return Decrypt(text)?.ConvertToSecureString();
        }

        ///// <summary>
        ///// Encrypt a string
        ///// </summary>
        ///// <param name="text"></param>
        ///// <returns>Encrypted data as string</returns>
        public string Encrypt(string text)
        {
            if (text == null) return null;
            byte[] input = utf8.GetBytes(text);
            byte[] output = Transform(input, des.CreateEncryptor(this.keyValue, this.iVValue));
            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// Encrypt or Decrypt bytes.
        /// </summary>
        /// <remarks>
        /// This is used by the public methods
        /// </remarks>
        /// <param name="input">Data to be encrypted/decrypted</param>
        /// <param name="cryptoTransform">
        /// <example>des.CreateEncryptor(this.keyValue, this.iVValue)</example>
        /// </param>
        /// <returns>Byte data containing result of operation</returns>
        private byte[] Transform(byte[] input, ICryptoTransform cryptoTransform)
        {
            byte[] result = null;
            // Create the necessary streams
            using (MemoryStream memory = new MemoryStream())
            {
                using (CryptoStream stream = new CryptoStream(memory, cryptoTransform, CryptoStreamMode.Write))
                {

                    // Transform the bytes as requesed
                    stream.Write(input, 0, input.Length);
                    stream.FlushFinalBlock();

                    // Read the memory stream and convert it back into byte array
                    memory.Position = 0;
                    result = new byte[memory.Length];
                    memory.Read(result, 0, result.Length);

                    // Clean up
                    memory.Close();
                    stream.Close();
                }
            }

            // Return result
            return result;
        }
        #endregion

    }
}
