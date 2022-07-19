using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.VS2022.Common
{
    public class BatHelper
    {
        [Obsolete]
        public static string ExecBAT(string fileName)
        {
            ProcessStartInfo pro = new System.Diagnostics.ProcessStartInfo("cmd.exe");
            pro.UseShellExecute = false;
            pro.RedirectStandardOutput = true;
            pro.RedirectStandardError = true;
            pro.CreateNoWindow = true;
            pro.FileName = fileName;
            pro.Arguments = "";
            //pro.WorkingDirectory = System.Environment.CurrentDirectory;
            System.Diagnostics.Process proc = System.Diagnostics.Process.Start(pro);
            System.IO.StreamReader sOut = proc.StandardOutput;
            proc.Close();
            string results = sOut.ReadToEnd().Trim(); //回显内容  
            sOut.Close();
            string[] values = results.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            return values[values.Length - 1];
        }

        //另外一种获取方式，在bat中设置exit code
        [Obsolete]
        public static string ExecBAT2(string fileName)
        {
            /* test2.bat
@echo off
set file=%1
copy %file% C:\Users\DEll\Downloads\cn_sql_server_2008_r2_enterprise_x86_x64_ia64_dvd_522233_4.iso

echo 0
exit 0
             */

            ProcessStartInfo pro = new System.Diagnostics.ProcessStartInfo("cmd.exe");
            pro.UseShellExecute = false;
            pro.RedirectStandardOutput = true;
            pro.RedirectStandardError = true;
            pro.CreateNoWindow = true;
            pro.FileName = fileName;
            pro.Arguments = @"C:\Users\DEll\Downloads\cn_sql_server_2008_r2_enterprise_x86_x64_ia64_dvd_522233.iso"; // 参数1
            //pro.WorkingDirectory = System.Environment.CurrentDirectory;
            System.Diagnostics.Process proc = System.Diagnostics.Process.Start(pro);
            proc.WaitForExit();
            return proc.ExitCode.ToString();
        }

        /// <summary>
        /// 执行某个程序，返回错误信息【未验证】
        /// </summary>
        /// <param name="exeFullPath"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Exec(string exeFullPath, string args)
        {
            Process p = new Process();
            p.StartInfo.FileName = exeFullPath;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.Arguments = args;
            p.StartInfo.RedirectStandardError = true;
            p.Start();

            // 用于显示执行sqlloader后的结果
            string strExecuteSqlloaderResult = p.StandardError.ReadToEnd();
            p.WaitForExit();
            return strExecuteSqlloaderResult.Trim();
        }

        ///// <summary>
        ///// 执行 Bat 文件并拿到结果
        ///// </summary>
        //public static string ExcuteBatFile(string batPath, ref string errMsg)
        //{
        //    try
        //    {
        //        string output;
        //        using (Process process = new Process())
        //        {
        //            FileInfo file = new FileInfo(batPath);
        //            if (file.Directory != null)
        //            {
        //                process.StartInfo.WorkingDirectory = file.Directory.FullName;
        //            }
        //            process.StartInfo.FileName = batPath;
        //            process.StartInfo.RedirectStandardOutput = true;
        //            process.StartInfo.RedirectStandardError = true;
        //            process.StartInfo.UseShellExecute = false;
        //            process.StartInfo.CreateNoWindow = true;
        //            process.Start();
        //            process.WaitForExit(60000);//-1:最大等待，0：不等待
        //            //output = process.StandardOutput.ReadToEnd();
        //            //errMsg = process.StandardError.ReadToEnd();
        //            output = "";
        //            errMsg = "";
        //        }
        //        return output + "|||||" + errMsg;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }
}
