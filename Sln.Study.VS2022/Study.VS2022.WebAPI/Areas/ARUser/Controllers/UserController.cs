using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Study.VS2022.WebAPI.Areas.AR1.Models;
using Study.VS2022.WebAPI.Areas.ARUser.Models;
using Study.VS2022.WebAPI.Filters;

namespace Study.VS2022.WebAPI.Areas.ARUser.Controllers
{
    /// <summary>
    /// 用户相关
    /// </summary>
    [TestFilter]
    [DisableRequestSizeLimit]
    [Authorize]
    [Area("ARUser")] // 添加 Area 和 Route 两个特性
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 注册接口
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        public JsonResult Register([FromForm]RegisterBody regBody)
        {
            JsonResult jr = new JsonResult(new RetData(ERet.TECH_ERROR, "未实现", null));
            return jr;
        }
    }
}
