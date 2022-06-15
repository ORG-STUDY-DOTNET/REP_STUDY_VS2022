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

            var app = builder.Build();

            #region Area Router
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