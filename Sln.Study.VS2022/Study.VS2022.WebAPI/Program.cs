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
            // 1. ע�͵������һ��
            // 2. ������������, ���� Model ��Ŀ������ xml ·������Ϊ: .\Study.VS2022.Model.xml,
            // ��������Ϊʼ�ո���
            // 3. ͨ��������޸�,ʹ Swagger �а����� Model �����͵�˵��
            builder.Services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo() 
                {
                    Title = "v1����",
                    Version = "v1",
                    Description = "Desc ����"
                });

                var file = Path.Combine(AppContext.BaseDirectory, "Study.VS2022.Model.xml");
                var path = Path.Combine(AppContext.BaseDirectory, file);
                c.IncludeXmlComments(path, true);
                c.OrderActionsBy(o => o.RelativePath);
            });
            #endregion

            #region ������
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowSpecificOrigin", bd => {
                    bd.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });               
            });
            #endregion

            var app = builder.Build();

            #region Area Router
            // �����治��Ҫ���� Router

            // �������,����Ų��ᱨ��
            //app.UseRouting();
            // ����·��
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