using System.Net;

namespace Study.VS2022.ConsoleAPP
{
    internal class Program
    {
        static void GetTest()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5014/");
            //client.Timeout = new TimeSpan(0);
            Task<string> tsres = client.GetStringAsync("/api/AR1/AH1/GetDemoAuth");
            tsres.Wait();

            string res = tsres.Result;
        }

        static void Main(string[] args)
        {
            // Get 请求测试
            // ------------
            GetTest();

            Console.ReadKey();
        }
    }
}