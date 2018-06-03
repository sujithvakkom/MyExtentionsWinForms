
using MyExtentions.DIalogs;
using MyExtentions.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MyExtentions.DIalogs
{
    public static class SettingsProvider
    {
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public static String GetDataSource()
        {
            return Decrypt(FormMaintanceSettings.Default.DataSource);
        }
        public static String GetDatabase()
        {
            return Decrypt(FormMaintanceSettings.Default.Database);
        }
        public static String GetUserName()
        {
            return Decrypt(FormMaintanceSettings.Default.UserName);
        }
        public static String GetPassword()
        {
            return Decrypt(FormMaintanceSettings.Default.Password);
        }
        public static String GetDefaultPrinter()
        {
            return Decrypt(FormMaintanceSettings.Default.DefaultPrinterName);
        }

        public static String GetHQDataSource()
        {
            return Decrypt(FormMaintanceSettings.Default.HQDataSource);
        }
        public static String GetHQDatabase()
        {
            return Decrypt(FormMaintanceSettings.Default.HQDatabase);
        }
        public static String GetHQUserName()
        {
            return Decrypt(FormMaintanceSettings.Default.HQUserName);
        }
        public static String GetHQPassword()
        {
            return Decrypt(FormMaintanceSettings.Default.HQPassword);
        }

        public static void SetDataSource(String Key)
        {
            FormMaintanceSettings.Default.DataSource = Encrypt(Key);
            FormMaintanceSettings.Default.Save();
        }
        public static void SetDatabase(String Key)
        {
            FormMaintanceSettings.Default.Database = Encrypt(Key);
            FormMaintanceSettings.Default.Save();
        }
        public static void SetUserName(String Key)
        {
            FormMaintanceSettings.Default.UserName = Encrypt(Key);
            FormMaintanceSettings.Default.Save();
        }
        public static void SetPassword(String Key)
        {
            FormMaintanceSettings.Default.Password = Encrypt(Key);
            FormMaintanceSettings.Default.Save();
        }
        public static void SetDefaultPrinter(String Key)
        {
            FormMaintanceSettings.Default.DefaultPrinterName = Encrypt(Key);
            FormMaintanceSettings.Default.Save();
        }

        public static void SetHQDataSource(String Key)
        {
            FormMaintanceSettings.Default.HQDataSource = Encrypt(Key);
            FormMaintanceSettings.Default.Save();
        }
        public static void SetHQDatabase(String Key)
        {
            FormMaintanceSettings.Default.HQDatabase = Encrypt(Key);
            FormMaintanceSettings.Default.Save();
        }
        public static void SetHQUserName(String Key)
        {
            FormMaintanceSettings.Default.HQUserName = Encrypt(Key);
            FormMaintanceSettings.Default.Save();
        }
        public static void SetHQPassword(String Key)
        {
            FormMaintanceSettings.Default.HQPassword = Encrypt(Key);
            FormMaintanceSettings.Default.Save();
        }

        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

    }
}
