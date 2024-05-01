using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Application.Services.ExternalService
{
    public static class EncryptionService
    {
        const string publicKey = "fwze7gdxmaucuewacyyscxhqcr6u66cm"; //32 Length Public Key
        const string privateKey = "hcxcq7gj6nr582h3"; //16 Length Key to be generated Private
        public static string Encrypt(string text, string pubKey = publicKey, string privKey = privateKey)
        {
            byte[] encrypted;
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256; // Set the key size to 256 bits
                aes.Key = Encoding.UTF8.GetBytes(pubKey);
                aes.IV = Encoding.UTF8.GetBytes(privKey); // Generate a random IV

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    // Prepend the generated IV to the encrypted data
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(text);
                        }
                    }
                    encrypted = ms.ToArray();
                }
            }
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string cipherText, string pubKey = publicKey, string privKey = privateKey)
        {
            string decrypted;
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256; // Set the key size to 256 bits
                aes.Key = Encoding.UTF8.GetBytes(pubKey);

                // Extract the IV from the ciphertext
                byte[] iv = new byte[aes.BlockSize / 8];
                Array.Copy(cipherBytes, 0, iv, 0, iv.Length);
                aes.IV = Encoding.UTF8.GetBytes(privKey); ;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(cipherBytes, iv.Length, cipherBytes.Length - iv.Length))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            decrypted = sr.ReadToEnd();
                        }
                    }
                }
            }
            return decrypted;
        }
    }
}
