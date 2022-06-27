using System.Net;
using System.Net.Http.Headers;

namespace Study.VS2022.ConsoleAPP
{
    internal class Program
    {
        /// <summary>
        /// 不带身份信息，直接进行 Get 请求
        /// </summary>
        static void GetTest()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5014/");
            //client.Timeout = new TimeSpan(0);
            Task<string> tsres = client.GetStringAsync("/api/AR1/AH1/GetDemoAuth");
            tsres.Wait();

            string res = tsres.Result;
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

        static void Main(string[] args)
        {
            // Get 请求测试
            // ------------
            //GetTest();

            // 请求需要身份信息的接口
            // ----------------------
            //GetNeedAuth();

            Console.ReadKey();
        }
    }
}