using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.VS2022.Common
{
    public class FileHelper
    {
        private static byte[] seed = { 251, 0, 1, 255, 23, 98, 76, 54, 39, 21, 16 };
        //private static int _EachReadLength = 10;

        /// <summary>
        /// 字节加密
        /// </summary>
        /// <param name="src"></param>
        private static void Encryption(byte[] src)
        { 
            for (int i = 0; i < src.Length; i++)
            {
                byte oriByt = src[i];
                int iByt = (int)oriByt;
                iByt += seed[i % 10];
                if (iByt >= 256)
                {
                    iByt -= 256;
                }
                src[i] = (byte)iByt;
            }
        }

        private static void Decryption(byte[] src)
        {
            for (int i = 0; i < src.Length; i++)
            {
                byte oriByt = src[i];
                int iByt = (int)oriByt;
                iByt -= seed[i % 10];
                if (iByt < 0)
                {
                    iByt += 256;
                }
                src[i] = (byte)iByt;
            }
        }

        /// <summary>
        /// 拷贝文件，附带加解密功能
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="willEnc"></param>
        /// <param name="willDec"></param>
        public static void BigFileCopy(string source, string target, bool willEnc, bool willDec)
        {
            using (FileStream fsRead = File.OpenRead(source))
            {
                using (FileStream fsWrite = File.OpenWrite(target))
                {
                    byte[] bytes = new byte[5 * 1024 * 1024];//定义5M空间
                    int count = 0;
                    while ((count = fsRead.Read(bytes, 0, bytes.Length)) > 0)
                    {
                        if (willEnc)
                        {
                            Encryption(bytes);
                        }
                        if (willDec)
                        {
                            Decryption(bytes);
                        }
                        fsWrite.Write(bytes, 0, count);
                        //fsRead.Position 表示已经读取的字节个数
                        double per = (fsRead.Position / (double)fsRead.Length) * 100;
                        Console.WriteLine($"{per:0.00}%");
                    }
                }
            }
        }


        ///// <summary>
        ///// 将 src 文件拷贝到目标路径 dest
        ///// </summary>
        ///// <param name="fromPath"></param>
        ///// <param name="toPath"></param>
        ///// <param name="willEnc">是否加密</param>
        ///// <param name="willDec">是否解密</param>
        //public static void BigCopy(string fromPath, string toPath, bool willEnc, bool willDec)
        //{
        //    string destDir = Path.GetDirectoryName(toPath);
        //    if (!Directory.Exists(destDir))
        //    {
        //        Directory.CreateDirectory(destDir);
        //    }

        //    int eachReadLength = _EachReadLength;

        //    //将源文件 读取成文件流
        //    FileStream fromFile = new FileStream(fromPath, FileMode.Open, FileAccess.Read);
        //    //已追加的方式 写入文件流
        //    FileStream toFile = new FileStream(toPath, FileMode.Append, FileAccess.Write);
        //    //实际读取的文件长度
        //    int toCopyLength = 0;
        //    //如果每次读取的长度小于 源文件的长度 分段读取
        //    if (eachReadLength < fromFile.Length)
        //    {
        //        byte[] buffer = new byte[eachReadLength];
        //        long copied = 0;
        //        while (copied <= fromFile.Length - eachReadLength)
        //        {
        //            toCopyLength = fromFile.Read(buffer, 0, eachReadLength);
        //            fromFile.Flush();
        //            toFile.Write(buffer, 0, eachReadLength);
        //            toFile.Flush();
        //            //流的当前位置
        //            toFile.Position = fromFile.Position;
        //            copied += toCopyLength;

        //        }
        //        int left = (int)(fromFile.Length - copied);
        //        toCopyLength = fromFile.Read(buffer, 0, left);
        //        fromFile.Flush();
        //        if (willEnc)
        //        {
        //            Encryption(buffer);
        //        }
        //        if (willDec)
        //        {
        //            Decryption(buffer);
        //        }
        //        toFile.Write(buffer, 0, left);
        //        toFile.Flush();

        //    }
        //    else
        //    {
        //        //如果每次拷贝的文件长度大于源文件的长度 则将实际文件长度直接拷贝
        //        byte[] buffer = new byte[fromFile.Length];
        //        fromFile.Read(buffer, 0, buffer.Length);
        //        fromFile.Flush();
        //        if (willEnc)
        //        {
        //            Encryption(buffer);
        //        }
        //        if (willDec)
        //        {
        //            Decryption(buffer);
        //        }
        //        toFile.Write(buffer, 0, buffer.Length);
        //        toFile.Flush();

        //    }
        //    fromFile.Close();
        //    toFile.Close();
        //    fromFile.Dispose();
        //    toFile.Dispose();
        //}
    }
}
