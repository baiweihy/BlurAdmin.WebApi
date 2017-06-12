using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using LegacyApplication.Database.Context;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Repositories.Core;
using LegacyApplication.Repositories.HumanResources;
using LegacyApplication.Repositories.Work;

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
            
            //Core
            builder.RegisterType<UploadedFileRepository>().As<IUploadedFileRepository>().InstancePerRequest();

            //Work
            builder.RegisterType<InternalMailRepository>().As<IInternalMailRepository>().InstancePerRequest();
            builder.RegisterType<InternalMailToRepository>().As<IInternalMailToRepository>().InstancePerRequest();
            builder.RegisterType<InternalMailAttachmentRepository>().As<IInternalMailAttachmentRepository>().InstancePerRequest();
            builder.RegisterType<TodoRepository>().As<ITodoRepository>().InstancePerRequest();
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>().InstancePerRequest();

            //HR
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>().InstancePerRequest();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerRequest();
            builder.RegisterType<JobPostLevelRepository>().As<IJobPostLevelRepository>().InstancePerRequest();
            builder.RegisterType<JobPostRepository>().As<IJobPostRepository>().InstancePerRequest();
            builder.RegisterType<AdministrativeLevelRepository>().As<IAdministrativeLevelRepository>().InstancePerRequest();
            builder.RegisterType<TitleLevelRepository>().As<ITitleLevelRepository>().InstancePerRequest();
            builder.RegisterType<NationalityRepository>().As<INationalityRepository>().InstancePerRequest();
            
            Container = builder.Build();

            return Container;
        }
    }
}