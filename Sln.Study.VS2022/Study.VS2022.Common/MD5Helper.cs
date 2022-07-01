using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Study.VS2022.Common
{
    public class MD5Helper
    {
        /// <summary>
        /// 计算文件MD5
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="upper"></param>
        public static string GetMD5(string filePath, bool upper)
        {
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Open);
                MD5 md5 = MD5.Create();
                byte[] retval = md5.ComputeHash(file);
                file.Close();
                StringBuilder sc = new StringBuilder();
                for (int i = 0; i < retval.Length; i++)
                {
                    if (upper)
                    {
                        sc.Append(retval[i].ToString("X2"));
                    }
                    else
                    {
                        sc.Append(retval[i].ToString("x2"));
                    }
                }
                return sc.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
