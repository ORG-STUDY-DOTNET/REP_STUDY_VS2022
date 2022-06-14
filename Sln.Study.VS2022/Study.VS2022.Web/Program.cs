using Autofac;
using Autofac.Extensions.DependencyInjection;
using Study.VS2022.Web.Modules;
using System.Reflection;

namespace Study.VS2022.Web
{
    public class Program
    {
        #region Autofac
        /* Autofac.Extensions.DependencyInjection 8.0 ÒýÓÃ
   * ÉùÃ÷
   */
        public static IContainer AutofacContainer; 
        #endregion

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Autofac
            // AutoFac
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            //builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => 
            //{
            //    containerBuilder.RegisterModule(new AutofacAutowiredModule());
            //});
            ContainerBuilder ctnBuilder = new ContainerBuilder();
            Assembly assembly = Assembly.Load("Study.VS2022.BLL");
            ctnBuilder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().InstancePerDependency();
            Program.AutofacContainer = ctnBuilder.Build();
            #endregion

            #region Log4Net
            builder.Logging.AddLog4Net("Configures/log4net.config");
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}