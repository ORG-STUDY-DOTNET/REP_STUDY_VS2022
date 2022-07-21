namespace Study.VS2022.WebAPI.Areas.AR1.Models
{
    public enum ERet
    {
        TECH_ERROR,
        BUSI_ERROR,
        OK
    }

    public enum EData
    {
        UNAME_PWD_EMPTY_ERROR,
        UNAME_PWD_NOTEQUAL,
        UNAME_PWD_INCORRECT,
        VALIDATE_CODE_ERROR
    }

    public class RetData
    {
        public RetData(ERet ret, object data, string token = null)
        {
            switch (ret)
            {
                case ERet.TECH_ERROR:
                    this.Ret = "TECH_ERROR";
                    break;
                case ERet.BUSI_ERROR:
                    this.Ret = "BUSI_ERROR";
                    break;
                case ERet.OK:
                    this.Ret = "OK";
                    break;
                default:
                    this.Ret = "OK";
                    break;
            }

            if (data != null && Enum.IsDefined(typeof(EData), data))
            {
                switch ((EData)data)
                {
                    case EData.UNAME_PWD_EMPTY_ERROR:
                        this.Data = "UNAME_PWD_EMPTY_ERROR";
                        break;
                    case EData.UNAME_PWD_NOTEQUAL:
                        this.Data = "UNAME_PWD_NOTEQUAL";
                        break;
                    case EData.VALIDATE_CODE_ERROR:
                        this.Data = "VALIDATE_CODE_ERROR";
                        break;
                    case EData.UNAME_PWD_INCORRECT:
                        this.Data = "UNAME_PWD_INCORRECT";
                        break;
                    default:
                        break;
                }
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
