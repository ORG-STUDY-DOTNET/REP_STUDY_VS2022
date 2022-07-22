namespace Study.VS2022.WebAPI.Areas.AR1.Models
{
    public enum ERet
    {
        /// <summary>
        /// 技术层面错误，前端日志输出
        /// </summary>
        TECH_ERROR,

        /// <summary>
        /// 业务层面错误，前端根据需求，通过 data 进行本地化文字渲染
        /// </summary>
        BUSI_ERROR,

        /// <summary>
        /// 无错误
        /// </summary>
        OK
    }

    public enum EData
    {
        /// <summary>
        /// 用户名及密码为空（不建议）
        /// </summary>
        UNAME_PWD_EMPTY_ERROR,

        /// <summary>
        /// 已废弃，不使用（不建议）
        /// </summary>
        UNAME_PWD_NOTEQUAL,

        /// <summary>
        /// 用户名密码不正确（不建议）
        /// </summary>
        UNAME_PWD_INCORRECT,

        /// <summary>
        /// 验证码不正确
        /// </summary>
        VALIDATE_CODE_ERROR
    }

    public class RetData
    {
        public RetData(ERet ret, object data, string token = null)
        {
            #region switch 写法（废弃）
            //switch (ret)
            //{
            //    case ERet.TECH_ERROR:
            //        this.Ret = "TECH_ERROR";
            //        break;
            //    case ERet.BUSI_ERROR:
            //        this.Ret = "BUSI_ERROR";
            //        break;
            //    case ERet.OK:
            //        this.Ret = "OK";
            //        break;
            //    default:
            //        this.Ret = "OK";
            //        break;
            //} 
            #endregion
            this.Ret = Enum.GetName(typeof(ERet), ret);

            if (data != null && Enum.IsDefined(typeof(EData), data))
            {
                #region switch 写法（废弃）
                //switch ((EData)data)
                //{
                //    case EData.UNAME_PWD_EMPTY_ERROR:
                //        this.Data = "UNAME_PWD_EMPTY_ERROR";
                //        break;
                //    case EData.UNAME_PWD_NOTEQUAL:
                //        this.Data = "UNAME_PWD_NOTEQUAL";
                //        break;
                //    case EData.VALIDATE_CODE_ERROR:
                //        this.Data = "VALIDATE_CODE_ERROR";
                //        break;
                //    case EData.UNAME_PWD_INCORRECT:
                //        this.Data = "UNAME_PWD_INCORRECT";
                //        break;
                //    default:
                //        break;
                //} 
                #endregion
                this.Data = Enum.GetName(typeof(EData), data);
            }
            else
            {
                this.Data = data;
            }

            this.Token = token;
        }

        /// <summary>
        /// "TECH_ERROR", "BUSI_ERROR", "OK" 必须为其中一个值
        /// </summary>
        public string Ret { get; set; }

        /// <summary>
        /// 为 null，空字符串，空白字符串，或正确的 jwtToken值 （不含 Bearer），
        /// 当不满足 String.IsNullOrWhiteSpace 时，表示客户端需要更新 jwtToken
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 为空，或者具体对象
        /// </summary>
        public object Data { get; set; }
    }
}
