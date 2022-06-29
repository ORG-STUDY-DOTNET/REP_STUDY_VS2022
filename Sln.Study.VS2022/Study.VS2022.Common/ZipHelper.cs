using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.VS2022.Common
{
    public class ZipHelper
    {
        /// <summary>
        /// 将 dirToZip 这个文件夹的全部内容准备压缩，压缩到 zipFullPath 这个 zip 路径中
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="strZip"></param>
        public static void ZipFile(string dirToZip, string zipFullPath)
        {
            // 路径不存在则创建
            string zipDir = Path.GetDirectoryName(zipFullPath);
            if (!Directory.Exists(zipDir))
            {
                Directory.CreateDirectory(zipDir);
            }

            if (dirToZip[dirToZip.Length - 1] != Path.DirectorySeparatorChar)
                dirToZip += Path.DirectorySeparatorChar;
            ZipOutputStream s = new ZipOutputStream(File.Create(zipFullPath));
            s.SetLevel(6); // 0 - store only to 9 - means best compression
            zip(dirToZip, s, dirToZip);
            s.Finish();
            s.Close();
            s.Dispose();
        }

        private static void zip(string strFile, ZipOutputStream s, string staticFile)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar) strFile += Path.DirectorySeparatorChar;
            Crc32 crc = new Crc32();
            string[] filenames = Directory.GetFileSystemEntries(strFile);
            foreach (string file in filenames)
            {

                if (Directory.Exists(file))
                {
                    zip(file, s, staticFile);
                }

                else // 否则直接压缩文件
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempfile);

                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                  
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);

                    s.Write(buffer, 0, buffer.Length);


                    fs.Dispose();
                }
            }
        }

        /// <summary>
        /// 将 TargetFile 这个 zip 文件解压到 fileDir 这个目录
        /// </summary>
        /// <param name="TargetFile"></param>
        /// <param name="fileDir"></param>
        /// <returns></returns>
        public static string UnZipFile(string TargetFile, string fileDir)
        {
            string rootFile = " ";
            try
            {
                //读取压缩文件(zip文件),准备解压缩
                ZipInputStream s = new ZipInputStream(File.OpenRead(TargetFile.Trim()));
                ZipEntry theEntry;
                string path = fileDir;
                //解压出来的文件保存的路径

                string rootDir = " ";
                //根目录下的第一个子文件夹的名称
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    rootDir = Path.GetDirectoryName(theEntry.Name);
                    //得到根目录下的第一级子文件夹的名称
                    if (rootDir.IndexOf("\\") >= 0)
                    {
                        rootDir = rootDir.Substring(0, rootDir.IndexOf("\\") + 1);
                    }
                    string dir = Path.GetDirectoryName(theEntry.Name);
                    //根目录下的第一级子文件夹的下的文件夹的名称
                    string fileName = Path.GetFileName(theEntry.Name);
                    //根目录下的文件名称
                    if (dir != " ")
                    //创建根目录下的子文件夹,不限制级别
                    {
                        if (!Directory.Exists(fileDir + "\\" + dir))
                        {
                            path = fileDir + "\\" + dir;
                            //在指定的路径创建文件夹
                            Directory.CreateDirectory(path);
                        }
                    }
                    else if (dir == " " && fileName != "")
                    //根目录下的文件
                    {
                        path = fileDir;
                        rootFile = fileName;
                    }
                    else if (dir != " " && fileName != "")
                    //根目录下的第一级子文件夹下的文件
                    {
                        if (dir.IndexOf("\\") > 0)
                        //指定文件保存的路径
                        {
                            path = fileDir + "\\" + dir;
                        }
                    }

                    if (dir == rootDir)
                    //判断是不是需要保存在根目录下的文件
                    {
                        path = fileDir + "\\" + rootDir;
                    }

                    //以下为解压缩zip文件的基本步骤
                    //基本思路就是遍历压缩文件里的所有文件,创建一个相同的文件。
                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(path + "\\" + fileName);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }

                        streamWriter.Close();
                        streamWriter.Dispose();
                    }
                }
                s.Close();
                s.Dispose();

                return rootFile;
            }
            catch (Exception ex)
            {
                return "1; " + ex.Message;
            }
        }
    }
}
