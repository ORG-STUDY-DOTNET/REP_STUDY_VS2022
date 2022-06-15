using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Study.VS2022.Model;

namespace Study.VS2022.WebAPI.Areas.AR1.Controllers
{
    /// <summary>
    /// AH1 Controller
    /// </summary>
    [Area("AR1")] // 添加 Area 和 Route 两个特性
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

        // <param name="files">文件</param> List<IFormFile> files, 

        /// <summary>
        /// 单个 Query 参数与多文件上传(暂未处理大文件的上传)
        /// </summary>
        /// <param name="id">附加参数</param>
        /// <param name="files">其它数据参数</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PostFiles([FromQuery]string id, IList<IFormFile> files)
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
