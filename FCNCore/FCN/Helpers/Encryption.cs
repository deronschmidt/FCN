using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace FCN.Helpers
{
    public class Encryption
    {
        private const string encryptionKey = "12345abs";

        public static string Encrypt(string encryptText)
        {
            byte[] encryptBytes = Encoding.Unicode.GetBytes(encryptText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes rdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 87, 97, 115, 104, 105, 110, 103, 116, 111, 110, 32, 78, 97, 116, 105, 111, 110, 97, 108, 115 });
                encryptor.Key = rdb.GetBytes(32);
                encryptor.IV = rdb.GetBytes(16);
                encryptor.Padding = PaddingMode.Zeros;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptBytes, 0, encryptBytes.Length);
                        cs.Close();
                    }
                    encryptText = Convert.ToBase64String(ms.ToArray());
                }
                rdb.Dispose();

            }
            return encryptText;
        }

        public static string Decrypt(string decryptText)
        {
            decryptText = decryptText.Replace(" ", "+");
            byte[] decryptBytes = Convert.FromBase64String(decryptText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes rdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 87, 97, 115, 104, 105, 110, 103, 116, 111, 110, 32, 78, 97, 116, 105, 111, 110, 97, 108, 115 });
                encryptor.Key = rdb.GetBytes(32);
                encryptor.IV = rdb.GetBytes(16);
                encryptor.Padding = PaddingMode.Zeros;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(decryptBytes, 0, decryptBytes.Length);
                        cs.Close();
                    }
                    decryptText = Encoding.Unicode.GetString(ms.ToArray());
                }
                rdb.Dispose();
            }
            return decryptText;
        }
    }
}
