using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infrasturcture.Presistence.Services
{
    public class EncryptionService
    {
        private readonly string _encryptionKey;

        public EncryptionService(string encryptionKey)
        {
            if (string.IsNullOrEmpty(encryptionKey))
                throw new ArgumentNullException(nameof(encryptionKey), "Encryption key cannot be null or empty.");

            _encryptionKey = encryptionKey;
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText), "Plain text cannot be null or empty.");

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
                aes.GenerateIV(); // Generate a random IV for each encryption

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] encryptedBytes;
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                    }
                    encryptedBytes = memoryStream.ToArray();
                }
                byte[] ivPlusCipher = new byte[aes.IV.Length + encryptedBytes.Length];
                Buffer.BlockCopy(aes.IV, 0, ivPlusCipher, 0, aes.IV.Length);
                Buffer.BlockCopy(encryptedBytes, 0, ivPlusCipher, aes.IV.Length, encryptedBytes.Length);
                return Convert.ToBase64String(ivPlusCipher);
            }
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText), "Cipher text cannot be null or empty.");

            byte[] ivPlusCipher = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
                byte[] iv = new byte[aes.IV.Length];
                byte[] cipher = new byte[ivPlusCipher.Length - aes.IV.Length];
                Buffer.BlockCopy(ivPlusCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(ivPlusCipher, iv.Length, cipher, 0, cipher.Length);
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream(cipher))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
