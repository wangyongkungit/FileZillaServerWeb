using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerCommonHelper
{
    public class AES
    {
        public AES()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static readonly string Privatekey = "S181881ld00SFvGD";

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="toDecrypt">要加密的数据</param>
        /// <param name="key">密钥 必须是16位、24位，32位</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key)
        {
            //string siv = "1314181613141816";
            string siv = "YiLiangYiJia0512";
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(siv);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="toDecrypt">要加密的数据</param>
        /// <param name="key">密钥 必须是16位、24位，32位</param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key)
        {
            //string siv = "1314181613141816";
            string siv = "YiLiangYiJia0512";
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(siv);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray).Trim().Replace("\0", "");
        }
    }
}
