using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using LegacyApplication.Database.Context;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Repositories.Administration;
using LegacyApplication.Repositories.Core;

namespace LegacyStandalone.Web.MyConfigurations
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            //builder.RegisterType<CoreContext>()
            //       .As<DbContext>()
            //       .InstancePerRequest();

            builder.RegisterType<CoreContext>().As<IUnitOfWork>().InstancePerRequest();
            
            builder.RegisterType<UploadedFileRepository>().As<IUploadedFileRepository>().InstancePerRequest();

            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>().InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}