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

                #region Swagger ���� jwt ��֤

                ////���һ�������ȫ�ְ�ȫ��Ϣ����AddSecurityDefinition����ָ���ķ�������һ�¼��ɣ�CoreAPI��
                ////var security = new Dictionary<string, IEnumerable<string>> { { "CoreAPI", new string[] { } }, };
                //OpenApiSecurityRequirement osr = new OpenApiSecurityRequirement();
                //OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme() {
                //    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ���·�����Bearer {token} ���ɣ�ע������֮���пո�",
                //    Name = "Bear",//jwtĬ�ϵĲ�������
                //    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                //    Type = SecuritySchemeType.ApiKey
                //};
                //osr.Add(securityScheme, new string[] { });
                //c.AddSecurityRequirement(osr);
                //c.AddSecurityDefinition(securityScheme.Name/*Ҳ���Բ�ʹ�����*/, securityScheme);

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

            #region ������
            builder.Services.AddCors(options =>         // ----> ��һ�Ӱ����ļ��ϴ�����
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

            #region �����֤����

            // �����֤����
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    // �Ƿ���ǩ����֤
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTHelper.secretKey)),
                    // ��������֤������Ҫ��token����Claim���͵ķ����˱���һ��
                    ValidateIssuer = true,
                    ValidIssuer = JWTHelper.Iss,//������
                    // ��������֤
                    ValidateAudience = true,
                    ValidAudience = JWTHelper.Aud,//������
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            #endregion

            var app = builder.Build();

            app.UseCors("AllowSpecificOrigin");

            // ������֤�м��
            // -------------------------
            app.UseAuthentication();

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

            // �������
            //app.UseCors("AllowSpecificOrigin");     // ----> ��һ�Ӱ����ļ��ϴ�����

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}