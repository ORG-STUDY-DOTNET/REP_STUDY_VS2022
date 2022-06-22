using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Study.VS2022.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            #region Swagger Model Xml
            // 1. 注释掉上面的一句
            // 2. 添加下面的内容, 其中 Model 项目的生成 xml 路径设置为: .\Study.VS2022.Model.xml,
            // 并且设置为始终复制
            // 3. 通过下面的修改,使 Swagger 中包含对 Model 中类型的说明
            builder.Services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo() 
                {
                    Title = "v1描述",
                    Version = "v1",
                    Description = "Desc 描述"
                });

                var file = Path.Combine(AppContext.BaseDirectory, "Study.VS2022.Model.xml");
                var path = Path.Combine(AppContext.BaseDirectory, file);
                c.IncludeXmlComments(path, true);
                c.OrderActionsBy(o => o.RelativePath);

                #region Swagger 配置 jwt 验证

                ////添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称一致即可，CoreAPI。
                ////var security = new Dictionary<string, IEnumerable<string>> { { "CoreAPI", new string[] { } }, };
                //OpenApiSecurityRequirement osr = new OpenApiSecurityRequirement();
                //OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme() {
                //    Description = "JWT授权(数据将在请求头中进行传输) 在下方输入Bearer {token} 即可，注意两者之间有空格",
                //    Name = "Bear",//jwt默认的参数名称
                //    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                //    Type = SecuritySchemeType.ApiKey
                //};
                //osr.Add(securityScheme, new string[] { });
                //c.AddSecurityRequirement(osr);
                //c.AddSecurityDefinition(securityScheme.Name/*也可以不使用这个*/, securityScheme);

                var scheme = new OpenApiSecurityScheme()
                {
                    Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Authorization"
                    },
                    Scheme = "oauth2",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                };
                c.AddSecurityDefinition("Authorization", scheme);
                var requirement = new OpenApiSecurityRequirement();
                requirement[scheme] = new List<string>();
                c.AddSecurityRequirement(requirement);


                #endregion
            });
            #endregion

            #region 跨域处理
            builder.Services.AddCors(options =>         // ----> 这一项不影响大文件上传问题
            {
                options.AddPolicy("AllowSpecificOrigin", bd =>
                {
                    bd.WithOrigins("http://localhost:8080")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();

                    //bd.AllowAnyOrigin()
                    //   .AllowAnyMethod()
                    //   .AllowAnyHeader()
                    //   .AllowCredentials();
                });
            });
            #endregion

            #region 添加验证服务

            // 添加验证服务
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    // 是否开启签名认证
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTHelper.secretKey)),
                    // 发行人验证，这里要和token类中Claim类型的发行人保持一致
                    ValidateIssuer = true,
                    ValidIssuer = JWTHelper.Iss,//发行人
                    // 接收人验证
                    ValidateAudience = true,
                    ValidAudience = JWTHelper.Aud,//订阅人
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            #endregion

            var app = builder.Build();

            app.UseCors("AllowSpecificOrigin");

            // 启用认证中间件
            // -------------------------
            app.UseAuthentication();

            #region Area Router
            // 这里面不需要配置 Router

            // 加上这句,下面才不会报错
            //app.UseRouting();
            // 配置路由
            //app.UseEndpoints(endpoints =>
            //{
            //    //endpoints.MapAreaControllerRoute(
            //    //    name: "arrouter",
            //    //    areaName: "AR1",
            //    //    pattern: "api/AR1/{controller}/{action}"
            //    //);

            //    //endpoints.MapControllerRoute(
            //    //    name: "areas",
            //    //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            //    //);
            //});
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // 允许跨域
            //app.UseCors("AllowSpecificOrigin");     // ----> 这一项不影响大文件上传问题

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}