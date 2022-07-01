using Nancy.Json;
using Study.VS2022.Common;
using Study.VS2022.Model;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Study.VS2022.ConsoleAPP
{
    internal class Program
    {
        private static readonly string demofilepath //= @"C:\Users\Where\Downloads\EF Core Power Tools v2.5.1005.vsix";
            = @"C:\Users\DEll\Downloads\大话设计模式.pdf";

        /// <summary>
        /// 不带身份信息，直接进行 Get 请求
        /// </summary>
        static string GetTest()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5014/");
            //client.Timeout = new TimeSpan(0);
            Task<string> tsres = client.GetStringAsync("/api/AR1/AH1/GetDemoAuth");
            tsres.Wait();

            string res = tsres.Result;
            return res;
        }

        /// <summary>
        /// 带身份信息，进行 Get 的无参请求
        /// </summary>
        static void GetNeedAuth()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5014/");
            //client.Timeout = new TimeSpan(0);

            #region Headers
            //client.DefaultRequestHeaders.Authorization 
            //    = new AuthenticationHeaderValue(String.Format("Bearer {0}", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI3IiwiaWF0IjoiMTY1NjI5NzYyNiIsIm5iZiI6IjE2NTYyOTc2MjYiLCJleHAiOiIxNjU2Mjk3NzI2IiwiaXNzIjoiQVBJIiwiYXVkIjoiVXNlciJ9.lG3j40LX3R_udcPiW4J8rM7ztv2jLIhgBQk80o90Uxk"));
            client.DefaultRequestHeaders.Add("Authorization"
                , String.Format("Bearer {0}"
                , "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI3IiwiaWF0IjoiMTY1NjI5ODE1NCIsIm5iZiI6IjE2NTYyOTgxNTQiLCJleHAiOiIxNjU2Mjk4MjU0IiwiaXNzIjoiQVBJIiwiYXVkIjoiVXNlciJ9.BcUM8gZWMpf45nZ26tKuDZXHHdmRMnCjq-ziGSUL_4g"));
            #endregion

            Task<string> tsres = client.GetStringAsync("/api/AR1/AH1/GetByNoParam");

            try
            {
                tsres.Wait();
                string res = tsres.Result;
                Console.WriteLine("res = {0}", res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
          
        }

        /// <summary>
        /// 内部会进行拿 Token 的过程，进行 PostJson 测试
        /// </summary>
        static void PostJsonNeedAuth()
        {
            // 拿 auth 字符串
            string strWithAuthStr = GetTest();
            Nancy.Json.JavaScriptSerializer jser = new JavaScriptSerializer();
            RetModel2 obj = jser.Deserialize<RetModel2>(strWithAuthStr);
            string authStr = (string)obj.Data;

            // 发起请求
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5014/");
            //client.Timeout = new TimeSpan(0);

            #region Headers
            //client.DefaultRequestHeaders.Authorization 
            //    = new AuthenticationHeaderValue(String.Format("Bearer {0}", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI3IiwiaWF0IjoiMTY1NjI5NzYyNiIsIm5iZiI6IjE2NTYyOTc2MjYiLCJleHAiOiIxNjU2Mjk3NzI2IiwiaXNzIjoiQVBJIiwiYXVkIjoiVXNlciJ9.lG3j40LX3R_udcPiW4J8rM7ztv2jLIhgBQk80o90Uxk"));
            client.DefaultRequestHeaders.Add("Authorization"
                , String.Format("Bearer {0}"
                , authStr));
            #endregion

            var tsres = client.PostAsync("/api/AR1/AH1/PostJson?id=8899", new StringContent(String.Format(@"
{{
    ""PageSize"": 12,
    ""PageIndex"": 13
}}
"), Encoding.UTF8, "application/json"));

            try
            {
                var rasa = tsres.Result.Content.ReadAsStringAsync();
                rasa.Wait();
                string res = rasa.Result;
                Console.WriteLine("res = {0}", res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void PostFileAndForm_InEachParameter()
        {
            // 拿 auth 字符串
            string strWithAuthStr = GetTest();
            Nancy.Json.JavaScriptSerializer jser = new JavaScriptSerializer();
            RetModel2 obj = jser.Deserialize<RetModel2>(strWithAuthStr);
            string authStr = (string)obj.Data;

            // 发起请求
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5014/");
            //client.Timeout = new TimeSpan(0);

            #region Headers
            //client.DefaultRequestHeaders.Authorization 
            //    = new AuthenticationHeaderValue(String.Format("Bearer {0}", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI3IiwiaWF0IjoiMTY1NjI5NzYyNiIsIm5iZiI6IjE2NTYyOTc2MjYiLCJleHAiOiIxNjU2Mjk3NzI2IiwiaXNzIjoiQVBJIiwiYXVkIjoiVXNlciJ9.lG3j40LX3R_udcPiW4J8rM7ztv2jLIhgBQk80o90Uxk"));
            client.DefaultRequestHeaders.Add("Authorization"
                , String.Format("Bearer {0}"
                , authStr));
            #endregion

            //
            HttpRequestMessage hrm = new HttpRequestMessage(HttpMethod.Post, "/api/AR1/AH1/PostFileAndForm_InEachParameter?id=8899");
            string boundary = DateTime.Now.Ticks.ToString("x");
            hrm.Content = new MultipartFormDataContent(boundary);

            // 添加文件
            string fp1 = demofilepath;
            using (FileStream fs1 = File.Open(fp1, FileMode.Open))
            {
                var bytes = new byte[fs1.Length];
                fs1.Read(bytes, 0, bytes.Length);
                ByteArrayContent bac1 = new ByteArrayContent(bytes);
                bac1.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                bac1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { 
                    Name = "file1",
                    FileName = Path.GetFileName(fp1)
                };

                //
                ((MultipartFormDataContent)hrm.Content).Add(bac1);
            }

            // 添加参数
            ByteArrayContent p1 = new ByteArrayContent(Encoding.UTF8.GetBytes("para1Value1"));
            p1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "f1"
            };
            ((MultipartFormDataContent)hrm.Content).Add(p1);

            // 添加文件
            string fp2 = demofilepath;
            using (FileStream fs1 = File.Open(fp2, FileMode.Open))
            {
                var bytes = new byte[fs1.Length];
                fs1.Read(bytes, 0, bytes.Length);
                ByteArrayContent bac = new ByteArrayContent(bytes);
                bac.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                bac.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "file2",
                    FileName = Path.GetFileName(fp2)
                };

                //
                ((MultipartFormDataContent)hrm.Content).Add(bac);
            }

            // 添加参数
            ByteArrayContent p2 = new ByteArrayContent(Encoding.UTF8.GetBytes("para1Value1"));
            p2.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "f2"
            };
            ((MultipartFormDataContent)hrm.Content).Add(p2);

            var sendR = client.SendAsync(hrm);

            try
            {
                var rasa = sendR.Result.Content.ReadAsStringAsync();
                rasa.Wait();
                string res = rasa.Result;
                Console.WriteLine("res = {0}", res);

                // Dispose 掉
                // ----------
                hrm.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static void Main(string[] args)
        {
            // Get 请求测试
            // ------------
            //GetTest();

            // 请求需要身份信息的接口
            // ----------------------
            //GetNeedAuth();

            // 反序列化字符串
            //string strWithAuthStr = GetTest();
            //Nancy.Json.JavaScriptSerializer jser = new JavaScriptSerializer();
            //RetModel2 obj = jser.Deserialize<RetModel2>(strWithAuthStr);

            //
            //PostJsonNeedAuth();

            //// 上传文件1
            //PostFileAndForm_InEachParameter();

            // 上传文件2
            //PostFileAndForm_MulParasAndMulFilesInEachPara();

            // AES 加解密测试
            //AESTest();

            // 压缩、解压测试
            // --------------
            //ZipAndUnZip();

            // 文件拷贝测试
            //FileHelper.BigFileCopy(@"C:\Users\DEll\Downloads\大话设计模式2.pdf"
            //    , @"C:\Users\DEll\Downloads\大话设计模式3.pdf", false, true);

            // 文件计算 MD5 测试
            string md5 = MD5Helper.GetMD5(@"C:\Users\DEll\Downloads\大话设计模式3.pdf", true);
            Console.WriteLine("md5 is " + md5);
            
            Console.ReadKey();
        }

        private static void ZipAndUnZip()
        {
            string qqDir = @"D:\Program Files (x86)\Tencent\QQ";
            string dest = @"D:\Program Files (x86)\QQ22\2.zip";

            ZipHelper.ZipFile(qqDir, dest);

            Console.WriteLine("OK1");

            ZipHelper.UnZipFile(dest, @"D:\Program Files (x86)\Tencent\QQ3");

            Console.WriteLine("OK2");
        }

        private static void AESTest()
        {
            var key = "99669966996699669966996699669966"; // 至少32位数字
            var pazzword = "杰克马的财富密码";

            string end = AESHelper.Encryption2(pazzword, key);
            Console.WriteLine(end);
            string ded = AESHelper.Decryption2(end, key);
            Console.WriteLine(ded);
        }

        private static void PostFileAndForm_MulParasAndMulFilesInEachPara()
        {
            // 拿 auth 字符串
            string strWithAuthStr = GetTest();
            Nancy.Json.JavaScriptSerializer jser = new JavaScriptSerializer();
            RetModel2 obj = jser.Deserialize<RetModel2>(strWithAuthStr);
            string authStr = (string)obj.Data;

            // 发起请求
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5014/");
            //client.Timeout = new TimeSpan(0);

            #region Headers
            //client.DefaultRequestHeaders.Authorization 
            //    = new AuthenticationHeaderValue(String.Format("Bearer {0}", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI3IiwiaWF0IjoiMTY1NjI5NzYyNiIsIm5iZiI6IjE2NTYyOTc2MjYiLCJleHAiOiIxNjU2Mjk3NzI2IiwiaXNzIjoiQVBJIiwiYXVkIjoiVXNlciJ9.lG3j40LX3R_udcPiW4J8rM7ztv2jLIhgBQk80o90Uxk"));
            client.DefaultRequestHeaders.Add("Authorization"
                , String.Format("Bearer {0}"
                , authStr));
            #endregion

            //
            HttpRequestMessage hrm = new HttpRequestMessage(HttpMethod.Post, "/api/AR1/AH1/PostFileAndForm_MulParasAndMulFilesInEachPara?id=8899");
            string boundary = DateTime.Now.Ticks.ToString("x");
            hrm.Content = new MultipartFormDataContent(boundary);

            // 添加文件
            string fp1 = demofilepath;
            using (FileStream fs1 = File.Open(fp1, FileMode.Open))
            {
                var bytes = new byte[fs1.Length];
                fs1.Read(bytes, 0, bytes.Length);
                ByteArrayContent bac1 = new ByteArrayContent(bytes);
                bac1.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                bac1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "files",
                    FileName = Path.GetFileName(fp1)
                };

                //
                ((MultipartFormDataContent)hrm.Content).Add(bac1);
            }

            // 添加参数
            ByteArrayContent p1 = new ByteArrayContent(Encoding.UTF8.GetBytes("para1Value1"));
            p1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "s1"
            };
            ((MultipartFormDataContent)hrm.Content).Add(p1);

            // 添加文件
            string fp2 = demofilepath;
            using (FileStream fs1 = File.Open(fp2, FileMode.Open))
            {
                var bytes = new byte[fs1.Length];
                fs1.Read(bytes, 0, bytes.Length);
                ByteArrayContent bac = new ByteArrayContent(bytes);
                bac.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                bac.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "files",
                    FileName = Path.GetFileName(fp2)
                };

                //
                ((MultipartFormDataContent)hrm.Content).Add(bac);
            }

            // 添加参数
            ByteArrayContent p2 = new ByteArrayContent(Encoding.UTF8.GetBytes("para1Value1"));
            p2.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "s1"
            };
            ((MultipartFormDataContent)hrm.Content).Add(p2);

            var sendR = client.SendAsync(hrm);

            try
            {
                var rasa = sendR.Result.Content.ReadAsStringAsync();
                rasa.Wait();
                string res = rasa.Result;
                Console.WriteLine("res = {0}", res);

                // Dispose 掉
                // ----------
                hrm.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}