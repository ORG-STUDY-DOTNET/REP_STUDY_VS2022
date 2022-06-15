using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

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
            });
            #endregion

            #region 跨域处理
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowSpecificOrigin", bd => {
                    bd.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });               
            });
            #endregion

            var app = builder.Build();

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

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}