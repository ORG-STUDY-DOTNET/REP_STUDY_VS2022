using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Study.VS2022.Model;

namespace Study.VS2022.WebAPI.Areas.AR1.Controllers
{
    /// <summary>
    /// AH1 Controller
    /// </summary>
    [Area("AR1")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    public class AH1Controller : ControllerBase
    {
        /// <summary>
        /// GetByNoParam
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetByNoParam()
        {
            JsonResult jr = new JsonResult(new 
            {
                Ret = 1,
                Msg = "OK"
            });
            return jr;
        }

        /// <summary>
        /// PostByModel
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PostByModel([FromBody]PostParam body)
        {
            JsonResult jr = new JsonResult(new
            {
                Ret = 1,
                Msg = "OK"
            });
            return jr;
        }
    }
}
