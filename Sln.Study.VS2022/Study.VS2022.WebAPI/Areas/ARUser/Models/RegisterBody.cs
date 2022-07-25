namespace Study.VS2022.WebAPI.Areas.ARUser.Models
{
    /// <summary>
    /// 提交注册的内容
    /// </summary>
    public class RegisterBody
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 加密的密码
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// 加密的密码（对重复密码进行加密）
        /// </summary>
        public string UserPasswordConfirm { get; set; }

        /// <summary>
        /// 验证码编号
        /// </summary>
        public string ValidateGuid { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidateCode { get; set; }
    }
}
