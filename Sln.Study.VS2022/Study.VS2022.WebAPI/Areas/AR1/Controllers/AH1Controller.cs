using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Study.VS2022.Model;

namespace Study.VS2022.WebAPI.Areas.AR1.Controllers
{
    /// <summary>
    /// AH1 Controller
    /// </summary>
    //[EnableCors("AllowSpecificOrigin")]   // ----> 这一项不影响大文件上传问题
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
        //[RequestSizeLimit(525336576)]//501MB
        [DisableRequestSizeLimit]


        //[RequestFormLimits(MultipartBodyLengthLimit = 524288000)]//500MB, which is already too high
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

        /// <summary>
        /// post 传递固定的多组 文件与 form
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file1"></param>
        /// <param name="f1"></param>
        /// <param name="file2"></param>
        /// <param name="f2"></param>
        /// <returns></returns>
        [DisableRequestSizeLimit]
        [HttpPost]
        public JsonResult PostFileAndForm([FromQuery] string id, IFormFile file1, [FromForm]string f1
            , IFormFile file2, [FromForm] string f2)
        {
            JsonResult jr = new JsonResult(new
            {
                Ret = 1,
                Msg = "OK"
            });
            return jr;
        }

        /// <summary>
        /// post 文件及 json 不可行，因为 application/json 的 Content-Type 无法附带文件，这个接口会报 415
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file1"></param>
        /// <param name="f1"></param>
        /// <returns></returns>
        [DisableRequestSizeLimit]
        [HttpPost]
        public JsonResult PostJsonError([FromQuery] string id, IFormFile file1, [FromBody]PostParam f1)
        {
            JsonResult jr = new JsonResult(new
            {
                Ret = 1,
                Msg = "OK"
            });
            return jr;
        }

        /// <summary>
        /// Json 不能和文件同时提交，不要重载， Swagger 会无法显示
        /// </summary>
        [DisableRequestSizeLimit]
        [HttpPost]
        public JsonResult PostJson([FromQuery] string id, [FromBody] PostParam f1)
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
