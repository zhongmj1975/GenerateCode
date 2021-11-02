using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FSELink.Utilities
{
    public class SecretHelper
    {
        public static bool CheckRegister()
        {
            bool result = true;
            if (!SystemInfo.NeedRegister)
            {
                return result;
            }

            if (DateTime.Now.Year >= 2014 && DateTime.Now.Month > 12)
            {
                result = false;
            }

            return result;
        }

        public static string AESEncrypt(string toEncrypt)
        {
            if (string.IsNullOrEmpty(toEncrypt.Trim()))
            {
                return string.Empty;
            }

            byte[] bytes = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            byte[] bytes2 = Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Key = bytes;
            rijndaelManaged.Mode = CipherMode.ECB;
            rijndaelManaged.Padding = PaddingMode.PKCS7;
            ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor();
            byte[] array = cryptoTransform.TransformFinalBlock(bytes2, 0, bytes2.Length);
            return Convert.ToBase64String(array, 0, array.Length);
        }

        public static string AESDecrypt(string toDecrypt)
        {
            if (string.IsNullOrEmpty(toDecrypt.Trim()))
            {
                return string.Empty;
            }

            byte[] bytes = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            byte[] array = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Key = bytes;
            rijndaelManaged.Mode = CipherMode.ECB;
            rijndaelManaged.Padding = PaddingMode.PKCS7;
            ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
            byte[] bytes2 = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
            return Encoding.UTF8.GetString(bytes2);
        }

        public static string EncodeBase64(Encoding encode, string source)
        {
            byte[] bytes = encode.GetBytes(source);
            string text = "";
            try
            {
                return Convert.ToBase64String(bytes);
            }
            catch
            {
                return source;
            }
        }

        public static string EncodeBase64(string source)
        {
            return EncodeBase64(Encoding.UTF8, source);
        }

        public static string DecodeBase64(Encoding encode, string result)
        {
            string text = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                return encode.GetString(bytes);
            }
            catch
            {
                return result;
            }
        }

        public static string DecodeBase64(string result)
        {
            return DecodeBase64(Encoding.UTF8, result);
        }

        public static string GenerateKey()
        {
            DESCryptoServiceProvider dESCryptoServiceProvider = (DESCryptoServiceProvider)DES.Create();
            return Encoding.ASCII.GetString(dESCryptoServiceProvider.Key);
        }

        public static string MD5Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(pToEncrypt);
            dESCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(sKey);
            dESCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(sKey);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder stringBuilder = new StringBuilder();
            byte[] array = memoryStream.ToArray();
            foreach (byte b in array)
            {
                stringBuilder.AppendFormat("{0:X2}", b);
            }

            stringBuilder.ToString();
            return stringBuilder.ToString();
        }

        public static string MD5Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            byte[] array = new byte[pToDecrypt.Length / 2];
            for (int i = 0; i < pToDecrypt.Length / 2; i++)
            {
                int num = Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 16);
                array[i] = (byte)num;
            }

            dESCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(sKey);
            dESCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(sKey);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(array, 0, array.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder stringBuilder = new StringBuilder();
            return Encoding.Default.GetString(memoryStream.ToArray());
        }
    }
}
