using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OfficeOpenXml;
using SimpleCaptcha;
using Study.VS2022.Common;
using Study.VS2022.Model;
using Study.VS2022.WebAPI.Filters;

namespace Study.VS2022.WebAPI.Areas.AR1.Controllers
{
    /// <summary>
    /// AH1 Controller
    /// </summary>
    [TestFilter]
    [DisableRequestSizeLimit]
    [Authorize]
    [Area("AR1")] // 添加 Area 和 Route 两个特性
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    public class AH1Controller : ControllerBase
    {
        //
        private readonly ICaptcha _captcha;

        //
        private readonly IConfiguration _configuration;

        //
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AH1Controller(IWebHostEnvironment hostingEnvironment, IConfiguration configuration
            , ICaptcha captcha
            )
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _captcha = captcha;
        }

        /// <summary>
        /// ！！！示例方法：拿到 Token
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetDemoAuth()
        {
            #region Redis 使用

            string redisUrl = this._configuration["Redis:Url"];
            RedisHelper redisHelper = new RedisHelper(redisUrl);

            bool r1 = redisHelper.SetValue("mykey", "hello123", new TimeSpan(0, 0, 1));
            string saveValue = redisHelper.GetValue("mykey");
            bool r2 = redisHelper.SetValue("mykey", "NewValue2");
            saveValue = redisHelper.GetValue("mykey");
            bool r3 = redisHelper.DeleteKey("mykey");
            string uncacheValue = redisHelper.GetValue("mykey");
            #endregion


            JsonResult jr = new JsonResult(new
            {
                Ret = 1,
                Msg = "OK",
                Data = JWTHelper.GetJWT(new TokenModel() { Name = "21", ID = 7 }, 120)
            });
            return jr;
        }

        /// <summary>
        /// GetByNoParam
        /// </summary>
        /// <returns></returns>
        //[EnableCors("AllowSpecificOrigin")]   // ----> 这一项不影响大文件上传问题
        [HttpGet]
        public JsonResult GetByNoParam()
        {
            #region 目前的代码报 CORS 错误
            /*
            // get 请求
            function fget(url) {
                fetch(url, {
                    method: 'GET'
                })
                .then(response => response.json())
                .then(data => {
                    console.log(data)
                })
                .catch(error => {
                    console.error(error)
                })
            }
             */
            #endregion

            JsonResult jr = new JsonResult(new
            {
                Ret = 1,
                Msg = "OK"
            });
            return jr;
        }

        /// <summary>
        /// 下载 Excel 文件
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public FileResult GetExcel()
        {
            //
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // 创建
            // ----
            byte[] bytes;
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage ep = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet sheet1 = ep.Workbook.Worksheets.Add("TheSheet1");
                sheet1.Cells[1, 2].Value = "12";

                ExcelWorksheet sheet2 = ep.Workbook.Worksheets.Add("TheSheet2");
                sheet2.Cells[2, 3].Value = "23";

                bytes = ep.GetAsByteArray();
            }

            // 返回
            // ----
            //FileResult fr = new FileStreamResult(  package.Stream
            // , new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.ms-excel"));

            //FileResult fr = new FileStreamResult(package.Stream
            // , new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
            //return fr;

            //return File(
            //    package.Stream,
            //    "application/octet-stream",
            //    "ExcelNameHere.xlsx");

            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");
        }

        /// <summary>
        /// Post 方式下载 Excel（特别注意前端的调用过程）
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public FileResult PostGetExcel()
        {
            //
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // 创建
            // ----
            byte[] bytes;
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage ep = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet sheet1 = ep.Workbook.Worksheets.Add("TheSheet1");
                sheet1.Cells[1, 2].Value = "12";

                ExcelWorksheet sheet2 = ep.Workbook.Worksheets.Add("TheSheet2");
                sheet2.Cells[2, 3].Value = "23";

                bytes = ep.GetAsByteArray();
            }

            // 返回
            // ----
            //FileResult fr = new FileStreamResult(  package.Stream
            // , new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.ms-excel"));

            //FileResult fr = new FileStreamResult(package.Stream
            // , new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
            //return fr;

            //return File(
            //    package.Stream,
            //    "application/octet-stream",
            //    "ExcelNameHere.xlsx");

            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");
        }

        /// <summary>
        /// Json 不能和文件同时提交，不要重载， Swagger 会无法显示
        /// </summary>
        //[DisableRequestSizeLimit]
        [HttpPost]
        public JsonResult PostJson([FromQuery] string id, [FromBody] PostParam f1)
        {
            #region 客户端写法

            /*
    var data = {
        PageSize: "12",
        PageIndex: "15"
    };

    //创建XMLHttpRequest异步对象
    var xhr = new XMLHttpRequest();

    xhr.open("POST", url);

    // 
    xhr.setRequestHeader("Content-Type", "application/json");

    //请求完毕
    xhr.onload = function () {
        console.log('in onload');
    }

    //发送
    var msg = JSON.stringify(data);
    xhr.send(msg);
             */

            #endregion

            JsonResult jr = new JsonResult(new
            {
                Ret = 1,
                Msg = "OK"
            });
            return jr;
        }

        /// <summary>
        ///  post 传递固定的多组 文件与 form。 独立的 IFormFile 参数不可以加 FromForm 特性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file1"></param>
        /// <param name="f1"></param>
        /// <param name="file2"></param>
        /// <param name="f2"></param>
        /// <returns></returns>
        //[DisableRequestSizeLimit]
        [HttpPost]
        public JsonResult PostFileAndForm_InEachParameter([FromQuery] string id, IFormFile file1, [FromForm] string f1
            , IFormFile file2, [FromForm] string f2)
        {
            #region 客户端写法
            /*
    //创建XMLHttpRequest异步对象
    var xhr = new XMLHttpRequest();

    xhr.open("POST", url);

    // 
    //xhr.setRequestHeader("Content-Type", "multipart/form-data");

    //监视上传进度
    if (xhr.upload) {
        xhr.upload.onprogress = function (e) {
            if (e.lengthComputable) {
                console.log(e.loaded, e.total);
            }
        };
    }

    //请求完毕
    xhr.onload = function () {
        console.log('in onload');
    }

    //发送
    var data = new FormData();
    var file1 = document.getElementById("id_f1").files[0];
    data.append("file1", file1);
    data.append("f1", 'dea');
    data.append("file2", file1);
    data.append("f2", '3ab');
    xhr.send(data);
             */
            #endregion

            #region 在服务器端保存文件
            if (false)
            {
                //private readonly IWebHostEnvironment _hostingEnvironment;
                //public AH1Controller(IWebHostEnvironment hostingEnvironment)
                //{
                //    _hostingEnvironment = hostingEnvironment;
                //}

                string tosavefilepath = Path.Combine(_hostingEnvironment.ContentRootPath, file1.FileName);
                using (var fileStream = new FileStream(tosavefilepath, FileMode.Create))
                {
                    file1.CopyToAsync(fileStream).Wait();
                }
            }
            #endregion

            JsonResult jr = new JsonResult(new
            {
                Ret = 1,
                Msg = "OK",
                Data = "file1's Name is " + file1.Name
            });
            return jr;
        }

        /// <summary>
        /// 单个 Query 参数与多文件上传(暂未处理大文件的上传)
        /// </summary>
        /// <param name="id">附加参数</param>
        /// <param name="files">其它数据参数</param>
        /// <returns></returns>
        //[RequestSizeLimit(525336576)]//501MB
        //[DisableRequestSizeLimit]
        //[RequestFormLimits(MultipartBodyLengthLimit = 524288000)]//500MB, which is already too high
        [HttpPost]
        public JsonResult PostFileAndForm_MoreFilesAndParaInEachParameter([FromQuery] string id
            , [FromForm] string f1, [FromForm] string f2, IList<IFormFile> files)
        {
            JsonResult jr = new JsonResult(new
            {
                Ret = 1,
                Msg = "OK",
                Data = "files'length" + files.Count
            });
            return jr;
        }

        /// <summary>
        /// Post 提交，参数实体里带文件的方式，这个实体类型一定为 FromForm ，如果为 FromBody ，则接口端拿不到文件
        /// 前端调用未验证
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file1"></param>
        /// <param name="f1"></param>
        /// <param name="file2"></param>
        /// <param name="f2"></param>
        /// <returns></returns>
        //[DisableRequestSizeLimit]
        [HttpPost]
        public JsonResult PostFileAndForm_OneFileAndParasInOneParameter([FromQuery] string id, [FromForm] PostParamAndBody body)
        {
            #region 客户端写法

            /*
    //创建XMLHttpRequest异步对象
    var xhr = new XMLHttpRequest();

    xhr.open("POST", url);

    // 这里带了文件，自动转为了 json，即使后端使用的是 FromForm
    // 写完这一句，拿不到文件了，是因为有 OPTIONS 请求？
    //xhr.setRequestHeader("Content-Type", "application/json");

    //监视上传进度
    if (xhr.upload) {
        xhr.upload.onprogress = function (e) {
            if (e.lengthComputable) {
                console.log(e.loaded, e.total);
            }
        };
    }

    //请求完毕
    xhr.onload = function () {
        console.log('in onload');
    }

    //发送
    var data = new FormData();
    var file1 = document.getElementById("id_f1").files[0];
    data.append("PageSize", '11');
    data.append("PageIndex", '12');
    data.append("File", file1);
    xhr.send(data);             
             */

            #endregion

            JsonResult jr = new JsonResult(new
            {
                Ret = 1,
                Msg = "OK"
            });
            return jr;
        }

        /// <summary>
        /// 不定项的字符串与文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="s1"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        //[DisableRequestSizeLimit]
        [HttpPost]
        public JsonResult PostFileAndForm_MulParasAndMulFilesInEachPara([FromQuery] string id, [FromForm] List<string> s1, IList<IFormFile> files)
        {
            #region 客户端写法
            /*
    //创建XMLHttpRequest异步对象
    var xhr = new XMLHttpRequest();

    xhr.open("POST", url);

    // 这里带了文件，自动转为了 json，即使后端使用的是 FromForm
    // 写完这一句，拿不到文件了，是因为有 OPTIONS 请求？
    //xhr.setRequestHeader("Content-Type", "application/json");

    //监视上传进度
    if (xhr.upload) {
        xhr.upload.onprogress = function (e) {
            if (e.lengthComputable) {
                console.log(e.loaded, e.total);
            }
        };
    }

    //请求完毕
    xhr.onload = function () {
        console.log('in onload');
    }

    //发送
    var data = new FormData();
    var file1 = document.getElementById("id_f1").files[0];
    data.append("s1", '11');
    data.append("s1", '12');
    data.append("files", file1);
    data.append("files", file1);
    xhr.send(data);
             */
            #endregion

            JsonResult jr = new JsonResult(new
            {
                Ret = 1,
                Msg = "OK"
            });
            return jr;
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="guid">验证码批次</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ValidateCode([FromQuery]string guid)
        {
            // 生成 info 对象
            // 创建流
            var info = _captcha.Generate(guid);
            var stream = new MemoryStream(info.CaptchaByteData);

            // 拿 id 和 code
            string codeValidate = info.CaptchaCode;
            string idValidate = info.CaptchaId;

            // 验证码只能用一次
            //bool isValid = _captcha.Validate(idValidate, codeValidate);
            //isValid = _captcha.Validate(idValidate, codeValidate);

            return File(stream, "image/png");
        }

        /// <summary>
        /// 登录接口，返回 JWT
        /// </summary>
        /// <param name="vcodeguid">验证码批次</param>
        /// <param name="uname">用户名</param>
        /// <param name="endPwd">密码</param>
        /// <param name="vcode">输入的验证码</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login([FromQuery]string vcodeguid
            , [FromForm]string uname
            , [FromForm]string endPwd
            , [FromForm]string vcode)
        {
            // 验证 vcode
            bool codeIsRight = _captcha.Validate(vcodeguid, vcode);
            if (!codeIsRight)
            {
                throw new Exception("验证码不正确！");
            }

            // 返回 jwt 信息
            JsonResult jr = new JsonResult(new
            {
                Ret = 1,
                Msg = "OK",
                Data = JWTHelper.GetJWT(new TokenModel() { Name = "21", ID = 7 }, 120)
            });
            return jr;
        }

   



























        ///// <summary>
        ///// PostByModel
        ///// </summary>
        ///// <param name="body"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult PostByModel([FromBody]PostParam body)
        //{
        //    JsonResult jr = new JsonResult(new
        //    {
        //        Ret = 1,
        //        Msg = "OK"
        //    });
        //    return jr;
        //}

        // <param name="files">文件</param> List<IFormFile> files, 







        #region 不可行的写法

        /// <summary>
        /// 这种情况不可行，body 中拿不到任何内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        [Obsolete]
        //[DisableRequestSizeLimit]
        [HttpPost]
        public JsonResult PostFileAndFormForms([FromQuery] string id, [FromForm] List<PostParamAndBody> body)
        {
            JsonResult jr = new JsonResult(new
            {
                Ret = 1,
                Msg = "OK"
            });
            return jr;
        }

        /// <summary>
        /// 这种情况不可行，在 Swagger 中没有办法传入文件，且直接返回 400
        /// </summary>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        [Obsolete]
        //[DisableRequestSizeLimit]
        [HttpPost]
        public JsonResult PostFileAndFormBodys([FromQuery] string id, [FromBody] List<PostParamAndBody> body)
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
        [Obsolete]
        //[DisableRequestSizeLimit]
        [HttpPost]
        public JsonResult PostJsonError([FromQuery] string id, IFormFile file1, [FromBody] PostParam f1)
        {
            JsonResult jr = new JsonResult(new
            {
                Ret = 1,
                Msg = "OK"
            });
            return jr;
        }

        ///// <summary>
        ///// 多组 FromForm 的情况：会导致 Swagger 报错
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="body1"></param>
        ///// <param name="body2"></param>
        ///// <returns></returns>
        ////[DisableRequestSizeLimit]
        //[HttpPost]
        //public JsonResult PostFileAndMulFormBody([FromQuery] string id, [FromForm]PostParamAndBody body1
        //    , [FromForm] PostParamAndBody body2)
        //{
        //    JsonResult jr = new JsonResult(new
        //    {
        //        Ret = 1,
        //        Msg = "OK"
        //    });
        //    return jr;
        //}

        #endregion




    }
}
