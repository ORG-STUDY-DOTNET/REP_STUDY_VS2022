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
            // VS2022 会出现 BUG，即明明添加了引用，但是无法编译通过，重启 VS2022 即可
            ITOrderService orderService = new TOrderService();
            TOrder to = new TOrder();
            to.TO_Price = 1652;
            to.TO_GUID = Guid.NewGuid().ToString();
            orderService.Add(to);

            // 不建议这样用
            orderService.SaveChanges();
            return null;
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