using System;
using System.Diagnostics;
using System.Security;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;

namespace Crescent.WinForms.Helpers
{
    public class AesManagedEncryption
    {
        #region Fields
        private readonly UTF8Encoding utf8 = new UTF8Encoding();

        private readonly byte[] keyValue;
        private readonly byte[] iVValue;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor, uses the default key and initialization vetor
        /// </summary>
        public AesManagedEncryption()
        {
            this.keyValue = new byte[] { 167, 218, 104, 110, 212, 68, 110, 109, 121, 85, 236, 211, 93, 161, 82, 51, 189, 239, 38, 216, 208, 248, 246, 145 };
            this.iVValue = new byte[] { 241, 165, 47, 63, 116, 72, 61, 54, 236, 3, 83, 124, 205, 173, 71, 18 };
        }

         /// <summary>
        /// Constructor, allows the key and initialization vector to be provided
        /// </summary>
        /// <param name="key"><see cref="Key"/></param>
        /// <param name="iV"><see cref="iV"/></param>
        public AesManagedEncryption(byte[] key, byte[] iV)
        {
            this.keyValue = key;
            this.iVValue = iV;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Decrypt bytes
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns>Decrypted data as bytes</returns>
        public string Decrypt(byte[] cipherText)
        {
            if (cipherText == null)
                return null;

            try
            {
                // Declare the string used to hold
                // the decrypted text.
                string plaintext = null;

                // Create an AesCryptoServiceProvider object
                // with the specified key and IV.
                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = keyValue;
                    aesAlg.IV = iVValue;

                    // Create a decryptor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                }

                return plaintext;
            }
            catch (CryptographicException)
            {
                var decryptedText = utf8.GetString(cipherText);
                return decryptedText;
            }
            catch (System.FormatException)
            {
                var decryptedText = utf8.GetString(cipherText);
                return decryptedText;
            }
            catch (Exception ex)
            {
                //OpenOffice.ExceptionHandler.ExceptionHandler.HandleException(ex, "Decrypting bytes");
                return null;
            }
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            if (!cipherText.StartsWith("~")
                || !cipherText.EndsWith("~"))
                throw new FormatException("Value must start and end '~'");

            var byteArray = cipherText.Substring(1, cipherText.Length-2)
                .Split('|')
                .Select(Byte.Parse)
                .ToArray();

            return Decrypt(byteArray);
        }

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="decryptedText"></param>
        /// <returns>Decrypted data as string</returns>
        public Boolean TryDecrypt(byte[] bytes, out string decryptedText)
        {
            if (bytes == null)
            {
                decryptedText = null;
                return true;
            }

            decryptedText = null;
            try
            {
                decryptedText = Decrypt(bytes);
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
                //ex.AppendData(nameof(bytes), bytes);
                //OpenOffice.ExceptionHandler.ExceptionHandler.HandleException(ex, "Decrypting text");
                return false;
            }
        }

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="decryptedText"></param>
        /// <returns>Decrypted data as string</returns>
        public Boolean TryDecrypt(string cipherText, out string decryptedText)
        {
            decryptedText = null;
            try
            {
                decryptedText = Decrypt(cipherText);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Decrypting '{cipherText}': {ex}");
                return false;
            }
        }

        public SecureString DecryptToSecureString(string text)
        {
            return Decrypt(text)?.ConvertToSecureString();
        }

        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>Encrypted data as string</returns>
        public byte[] EncryptToByteArray(string plainText)
        {
            if (plainText == null)
                return null;

            try
            {
                // Create an AesCryptoServiceProvider object
                // with the specified key and IV.
                byte[] encrypted;
                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.KeySize = keyValue.Length * 8;
                    aesAlg.Key = keyValue;
                    aesAlg.BlockSize = iVValue.Length * 8;
                    aesAlg.IV = iVValue;

                    // Create an encryptor to perform the stream transform.
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption.
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }

                // Return the encrypted bytes from the memory stream.
                return encrypted;
            }
            catch (Exception ex)
            {
                //ex.AppendData(nameof(plainText), plainText);
                //OpenOffice.ExceptionHandler.ExceptionHandler.HandleException(ex, "Encrypting text");
                throw;
            }
        }


        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>Encrypted data as string</returns>
        public string EncryptToString(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            var byteArray = EncryptToByteArray(plainText);
            var encryptedString = "~" + byteArray
                                      .Select(a => a.ToString())
                                      .Concatenate("|") + "~";
            return encryptedString;
        }
        #endregion

        #region Ecrypt Mobile
        /// <summary>
        /// Encrpyts the sourceString, returns this result as an Aes encrpyted, BASE64 encoded string
        /// </summary>
        /// <param name="plainSourceStringToEncrypt">a plain, Framework string (ASCII, null terminated)</param>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <returns>
        /// returns an Aes encrypted, BASE64 encoded string
        /// </returns>
        //public string EncryptString(string plainSourceStringToEncrypt)
        //{
        //    //Set up the encryption objects
        //    using (AesCryptoServiceProvider acsp = GetProvider(Encoding.Default.GetBytes(Constants.ApplicationRoleEncryptedPassword)))
        //    {
        //        byte[] sourceBytes = Encoding.ASCII.GetBytes(plainSourceStringToEncrypt);
        //        ICryptoTransform ictE = acsp.CreateEncryptor();

        //        //Set up stream to contain the encryption
        //        MemoryStream msS = new MemoryStream();

        //        //Perform the encrpytion, storing output into the stream
        //        CryptoStream csS = new CryptoStream(msS, ictE, CryptoStreamMode.Write);
        //        csS.Write(sourceBytes, 0, sourceBytes.Length);
        //        csS.FlushFinalBlock();

        //        //sourceBytes are now encrypted as an array of secure bytes
        //        byte[] encryptedBytes = msS.ToArray(); //.ToArray() is important, don't mess with the buffer

        //        //return the encrypted bytes as a BASE64 encoded string
        //        return Convert.ToBase64String(encryptedBytes);
        //    }
        //}


        /// <summary>
        /// Decrypts a BASE64 encoded string of encrypted data, returns a plain string
        /// </summary>
        /// <param name="base64StringToDecrypt">an Aes encrypted AND base64 encoded string</param>
        /// <param name="passphrase">The passphrase.</param>
        /// <returns>returns a plain string</returns>
        public string DecryptString(string base64StringToDecrypt)
        {
            //Set up the encryption objects
            using (AesCryptoServiceProvider acsp = GetProvider(Encoding.Default.GetBytes(ConfigurationManager.AppSettings["OpenAI.APIKey.Encrypted"].ToString())))
            {
                byte[] RawBytes = Convert.FromBase64String(base64StringToDecrypt);
                ICryptoTransform ictD = acsp.CreateDecryptor();

                //RawBytes now contains original byte array, still in Encrypted state

                //Decrypt into stream
                MemoryStream msD = new MemoryStream(RawBytes, 0, RawBytes.Length);
                CryptoStream csD = new CryptoStream(msD, ictD, CryptoStreamMode.Read);
                //csD now contains original byte array, fully decrypted

                //return the content of msD as a regular string
                return (new StreamReader(csD)).ReadToEnd();
            }
        }

        private AesCryptoServiceProvider GetProvider(byte[] key)
        {
            AesCryptoServiceProvider result = new AesCryptoServiceProvider();
            result.BlockSize = 128;
            result.KeySize = 128;
            result.Mode = CipherMode.CBC;
            result.Padding = PaddingMode.PKCS7;

            result.GenerateIV();
            result.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            byte[] RealKey = GetKey(key, result);
            result.Key = RealKey;
            // result.IV = RealKey;
            return result;
        }

        private byte[] GetKey(byte[] suggestedKey, SymmetricAlgorithm p)
        {
            byte[] kRaw = suggestedKey;
            List<byte> kList = new List<byte>();

            for (int i = 0; i < p.LegalKeySizes[0].MinSize; i += 8)
            {
                kList.Add(kRaw[(i / 8) % kRaw.Length]);
            }
            byte[] k = kList.ToArray();
            return k;
        }
        #endregion
    }
}
