using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockByte
{
    public class clsSymmetricEncryption
    {
        //AES

        private static byte[] GetValidKey128bit(string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            Array.Resize(ref keyBytes, 16); // Trim or pad to 16 bytes for AES-128
            return keyBytes;
        }
        private static byte[] GetValidKey256bit(string key)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(key)); // Ensure 256-bit key
            }
        }

        public static void EncryptFile(string filePath, string key)
        {
            string encryptedFilePath = filePath + clsUtil.GlobalEncryptionExtension; // Output file

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetValidKey256bit(key); //encrypt the key using sha256
                aesAlg.GenerateIV();  

                using (FileStream fsOut = new FileStream(encryptedFilePath, FileMode.Create))
                {
                    fsOut.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (CryptoStream csEncrypt = new CryptoStream(fsOut, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    using (FileStream fsIn = new FileStream(filePath, FileMode.Open))
                    {
                        fsIn.CopyTo(csEncrypt); 
                    }
                }
            }
        }
        public static bool EncryptFile(string filePath, string encryptedFilePath, string key)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = GetValidKey256bit(key);  // Encrypt the key using SHA-256
                    aesAlg.GenerateIV();

                    using (FileStream fsOut = new FileStream(encryptedFilePath, FileMode.Create))
                    {
                        fsOut.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                        using (CryptoStream csEncrypt = new CryptoStream(fsOut, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                        using (FileStream fsIn = new FileStream(filePath, FileMode.Open))
                        {
                            fsIn.CopyTo(csEncrypt);
                        }
                    }
                }

                return true; 
            }
            catch
            {
                return false;
            }
        }

        public static void DecryptFile(string encryptedFilePath, string key)
        {
            string decryptedFilePath = GetDecryptedFilePath(encryptedFilePath);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetValidKey256bit(key);

                using (FileStream fsIn = new FileStream(encryptedFilePath, FileMode.Open))
                {
                    byte[] iv = new byte[aesAlg.BlockSize / 8];
                    fsIn.Read(iv, 0, iv.Length); 

                    aesAlg.IV = iv;

                    using (CryptoStream csDecrypt = new CryptoStream(fsIn, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                    using (FileStream fsOut = new FileStream(decryptedFilePath, FileMode.Create))
                    {
                        csDecrypt.CopyTo(fsOut); 
                    }
                }
            }
        }
        public static bool DecryptFile(string encryptedFilePath, string decryptedFilePath, string key)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = GetValidKey256bit(key);

                    using (FileStream fsIn = new FileStream(encryptedFilePath, FileMode.Open))
                    {
                        byte[] iv = new byte[aesAlg.BlockSize / 8];
                        fsIn.Read(iv, 0, iv.Length);

                        aesAlg.IV = iv;

                        using (CryptoStream csDecrypt = new CryptoStream(fsIn, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                        using (FileStream fsOut = new FileStream(decryptedFilePath, FileMode.Create))
                        {
                            csDecrypt.CopyTo(fsOut);
                        }
                    }
                }

                return true; 
            }
            catch (CryptographicException)
            {
                return false;
            }
            catch
            {
                return false;
            }
        }


        private static string GetDecryptedFilePath(string encryptedFilePath)
        {
            string originalFilePath = encryptedFilePath.Replace(".enc", ""); // Remove .enc
            string directory = Path.GetDirectoryName(originalFilePath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFilePath);
            string extension = Path.GetExtension(originalFilePath);

            return Path.Combine(directory, $"{fileNameWithoutExt}.dec{extension}"); // Append .dec before extension
        }





    }
}
