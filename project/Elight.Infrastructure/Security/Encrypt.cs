using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Elight.Infrastructure
{
    /// <summary>
    /// 字符串加密解密方法扩展类。
    /// </summary>
    public static class Encrypt
    {
        #region DES加密解密

        /// <summary>
        /// 默认密钥。
        /// </summary>
        private const string DESENCRYPT_KEY = "hsdjxlzf";

        /// <summary>
        /// DES加密，使用自定义密钥。
        /// </summary>
        /// <param name="text">待加密的明文</param>
        /// <param name="key">8位字符的密钥字符串</param>
        /// <returns></returns>
        public static string DESEncrypt(this string text, string key)
        {
            if (key.Length != 8)
            {
                key = DESENCRYPT_KEY;
            }
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(text);

            byte[] a = ASCIIEncoding.ASCII.GetBytes(key);
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);//将第一个参数转换为十六进制数,长度为2,不足前面补0
            }
            return ret.ToString();
        }

        /// <summary>
        /// DES解密，使用自定义密钥。
        /// </summary>
        /// <param name="cyphertext">待解密的秘文</param>
        /// <param name="key">必须是8位字符的密钥字符串(不能有特殊字符)</param>
        /// <returns></returns>
        public static string DESDecrypt(this string cyphertext, string key)
        {
            if (key.Length != 8)
            {
                key = DESENCRYPT_KEY;
            }
            if (string.IsNullOrEmpty(cyphertext))
                return string.Empty;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[cyphertext.Length / 2];
            for (int x = 0; x < cyphertext.Length / 2; x++)
            {
                int i = (Convert.ToInt32(cyphertext.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.GetEncoding("UTF-8").GetString(ms.ToArray());
        }

        /// <summary>
        /// DES加密，使用默认密钥。
        /// </summary>
        /// <param name="text">待加密的明文</param>
        /// <returns></returns>
        public static string DESEncrypt(this string text)
        {
            return DESEncrypt(text, DESENCRYPT_KEY);
        }

        /// <summary>
        /// DES解密，使用默认密钥。
        /// </summary>
        /// <param name="cyphertext">待解密的秘文</param>
        /// <returns></returns>
        public static string DESDecrypt(this string cyphertext)
        {
            return DESDecrypt(cyphertext, DESENCRYPT_KEY);
        }
        #endregion

        #region Base64加密解密
        /// <summary>
        /// Base64加密，采用指定字符编码方式加密。
        /// </summary>
        /// <param name="input">待加密的明文</param>
        /// <param name="encode">字符编码</param>
        /// <returns></returns>
        public static string Base64Encrypt(this string input, Encoding encode)
        {
            return Convert.ToBase64String(encode.GetBytes(input));
        }

        /// <summary>
        /// Base64加密，采用UTF8编码方式加密。
        /// </summary>
        /// <param name="input">待加密的明文</param>
        /// <returns></returns>
        public static string Base64Encrypt(this string input)
        {
            return Base64Encrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64解密，采用UTF8编码方式解密。
        /// </summary>
        /// <param name="input">待解密的秘文</param>
        /// <returns></returns>
        public static string Base64Decrypt(this string input)
        {
            return Base64Decrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64解密，采用指定字符编码方式解密。
        /// </summary>
        /// <param name="input">待解密的秘文</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string Base64Decrypt(this string input, Encoding encode)
        {
            return encode.GetString(Convert.FromBase64String(input));
        }
        #endregion

        #region MD5加密
        /// <summary>
        /// 字符串MD5加密。
        /// </summary>
        /// <param name="strOri">需要加密的字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(this string text)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// 文件流MD5加密。
        /// </summary>
        /// <param name="stream">需要加密的文件流</param>
        /// <returns></returns>
        public static string MD5Encrypt(this Stream stream)
        {
            MD5 md5serv = MD5CryptoServiceProvider.Create();
            byte[] buffer = md5serv.ComputeHash(stream);
            StringBuilder sb = new StringBuilder();
            foreach (byte var in buffer)
            {
                sb.Append(var.ToString("x2"));
            }
            return sb.ToString();
        }
        #endregion
    }
}
