using Autofac;
using System.Reflection;

namespace Study.VS2022.Web.Modules
{
    [Obsolete]
    public class AutofacAutowiredModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            Assembly assembly = Assembly.Load("Study.VS2022.BLL");
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().InstancePerDependency();
        }
    }
}
