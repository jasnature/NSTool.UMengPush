using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace NSTool.UMengPush.Base
{
    /// <summary>
    /// MD5加密工具类(MD5为不可逆加密方式)
    ///  author: 刘敬
    /// </summary>
    public sealed class MD5CryptionUMeng : IDisposable
    {
        private MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
        private Encoding encoder = Encoding.UTF8;

        /// <summary>
        /// 生成MD5加密摘要
        /// </summary>
        /// <param name="original">数据源</param>
        /// <returns>MD5加密后</returns>
        public byte[] GenerateMD5(byte[] original)
        {
            byte[] keyhash = md5Provider.ComputeHash(original);
            return keyhash;
        }

        /// <summary>
        /// 生成MD5加密摘要
        /// </summary>
        /// <param name="original">数据源</param>
        /// <param name="index">起始位置</param>
        /// <param name="length">长度</param>
        /// <returns>MD5加密后</returns>
        public byte[] GenerateMD5(byte[] original, int index, int length)
        {
            return md5Provider.TransformFinalBlock(original, index, length);
        }

        /// <summary>
        /// 生成MD5加密摘要
        /// </summary>
        /// <param name="strOriginal">字符串数据源</param>
        /// <returns>MD5加密后</returns>
        public string GenerateMD5(string strOriginal)
        {
            byte[] btemp = GenerateMD5(encoder.GetBytes(strOriginal));
            ////把加密后的字节转换成精度2的十六进制数据
            //StringBuilder ret = new StringBuilder();
            //foreach (byte b in btemp)
            //{
            //    ret.AppendFormat("{0:X2}", b);
            //}
            //return ret.ToString();
            return BitConverter.ToString(btemp).Replace("-", "");
        }

        /// <summary>
        /// 获取文件的MD5码
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <returns>文件内容加密后的字符串</returns>
        public string GetFileMD5(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))//打开文件
            {
                byte[] bsre = md5Provider.ComputeHash(fs);
                return BitConverter.ToString(bsre).Replace("-", "");
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            md5Provider.Clear();
        }

        #endregion
    }
}
