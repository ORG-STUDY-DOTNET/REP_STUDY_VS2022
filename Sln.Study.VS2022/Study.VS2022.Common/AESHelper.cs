using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Study.VS2022.Common
{
    public class AESHelper
    {
        //加密字符串[旧版]
        [Obsolete]
        public static string Encryption(string toE, string key_need32letter)
        {
            //加密和解密必须采用相同的key，具体自己填写，但是必须为32位
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key_need32letter);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Key = keyArray;
            rijndaelManaged.Mode = CipherMode.ECB;
            rijndaelManaged.Padding = PaddingMode.PKCS7;
            ICryptoTransform icryptoTransform = rijndaelManaged.CreateEncryptor();

            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toE);
            byte[] resultArray = icryptoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        //解密字符串[旧版]
        [Obsolete]
        public static string Decryption(string toD, string key_need32letter)
        {
            //加密和解密必须采用相同的key，具体值自己填，但是必须为32位
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key_need32letter);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Key = keyArray;
            rijndaelManaged.Mode = CipherMode.ECB;
            rijndaelManaged.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rijndaelManaged.CreateDecryptor();

            byte[] toEncryptArray = Convert.FromBase64String(toD);
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encryption2(string input, string key_need32letter)
        {
            Aes aes = Aes.Create();
            aes.Key = UTF8Encoding.UTF8.GetBytes(key_need32letter);
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;

            //aes.KeySize = 32;
            //byteaes.EncryptEcb(Encoding.UTF8.GetBytes(input), PaddingMode.PKCS7);
            ICryptoTransform icryptoTransform = aes.CreateEncryptor();

            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(input);
            byte[] resultArray = icryptoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decryption2(string input, string key_need32letter)
        {
            //加密和解密必须采用相同的key，具体值自己填，但是必须为32位
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key_need32letter);
            Aes aes = Aes.Create();
            aes.Key = UTF8Encoding.UTF8.GetBytes(key_need32letter);
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = aes.CreateDecryptor();

            byte[] toEncryptArray = Convert.FromBase64String(input);
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
