using Autofac;
using Microsoft.AspNetCore.Mvc;
using Study.VS2022.BLL;
using Study.VS2022.IBLL;
using Study.VS2022.Model.AutoModels;
using Study.VS2022.Web.Models;
using System.Diagnostics;

namespace Study.VS2022.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 测试添加数据
        /// </summary>
        /// <returns>测试返回情况</returns>
        public string TestAdd()
        {
            #region Log4net 步骤：
            /*
             1. 添加依赖：Microsoft.Extensions.Logging.Log4Net.AspNetCore 6.1.0
             2. 添加配置文件：Configures/log4net.config（文件内容见：log4net.config）
             3. 注册：在 Program.cs 中添加： builder.Logging.AddLog4Net("Configures/log4net.config");
             4. 构造函数中注入：代码已在创建项目时，自动添加好
             5. 使用
             */
            #endregion

            // 日志测试
            // ALL|DEBUG|INFO|WARN|ERROR|FATAL|OFF
            _logger.LogDebug("TestAdd Debug!");
            _logger.LogInformation("TestAdd Information!");
            _logger.LogWarning("Test Add Warning!");
            _logger.LogTrace("Test Add Trace!");
            _logger.LogError("Test Add Error!");
            _logger.LogCritical("Test Add Critical!");

            // VS2022 会出现 BUG，即明明添加了引用，但是无法编译通过，重启 VS2022 即可
            ITOrderService orderService = Program.AutofacContainer.Resolve<ITOrderService>();
            TOrder to = new TOrder();
            to.TO_Price = 2033;
            to.TO_GUID = Guid.NewGuid().ToString();
            orderService.Add(to);

            // 不建议这样用
            int sr = orderService.SaveChanges();
            return null;

            // TODO:
            /*
             Swagger    WebAPI 项目自带 Swagger!
            大文件上传
            大量数据插入方案
            日志 ！
            部署问题
            Docker 部署问题
             */
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}