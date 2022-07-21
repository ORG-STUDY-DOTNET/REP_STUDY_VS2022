using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Study.VS2022.WebAPI
{
    public class TokenModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
        public string Sub { get; set; }
    }

    /// <summary>
    /// 生成JWT字符串
    /// </summary>
    public class JWTHelper
    {
        // 密钥，注意不能太短
        public static string secretKey { get; set; } = "xiaomaPrincess@gmail.com";

        /// <summary>
        /// 发行人
        /// </summary>
        public static string Iss = "API";

        /// <summary>
        /// 接收人，订阅人
        /// </summary>
        public static string Aud = "User";

        /// <summary>
        /// 生成JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <param name="userName">相关用户的用户名</param>
        /// <param name="expireSeconds">过期时间，0为不过期</param>
        /// <returns></returns>
        public static string GetJWT(TokenModel tokenModel, string userName, int expireSeconds = 0)
        {
            //DateTime utc = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                // System.IdentityModel.Tokens.Jwt和Microsoft.AspNetCore.Authentication.JwtBearer

                // 获取就使用 Response.HttpContext.User.Identity.Name 获取userName
                new Claim(ClaimTypes.Name, userName),

                new Claim(JwtRegisteredClaimNames.Jti,tokenModel.ID.ToString()),
                // 令牌颁发时间
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                 // 过期时间 100秒
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(100)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss, JWTHelper.Iss), // 签发者
                new Claim(JwtRegisteredClaimNames.Aud, JWTHelper.Aud) // 接收者
            };

            // 密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt;
            if (expireSeconds > 0)
            {
                jwt = new JwtSecurityToken(

               claims: claims,// 声明的集合
               expires: DateTime.Now.AddSeconds(expireSeconds),
               signingCredentials: creds
               );
            }
            else
            {
                jwt = new JwtSecurityToken(

                claims: claims,// 声明的集合
                               //expires: .AddSeconds(36), // token的有效时间
                signingCredentials: creds
                );
            }
            var handler = new JwtSecurityTokenHandler();
            // 生成 jwt字符串
            var strJWT = handler.WriteToken(jwt);
            return strJWT;
        }
    }
}
