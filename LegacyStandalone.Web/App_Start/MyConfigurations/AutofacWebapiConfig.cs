using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using LegacyApplication.Database.Context;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Repositories.Core;
using LegacyApplication.Repositories.HumanResources;
using LegacyApplication.Repositories.Scrum;

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

            //HR
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>().InstancePerRequest();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerRequest();
            builder.RegisterType<JobPostLevelRepository>().As<IJobPostLevelRepository>().InstancePerRequest();
            builder.RegisterType<JobPostRepository>().As<IJobPostRepository>().InstancePerRequest();

            //Scrum
            builder.RegisterType<BugRepository>().As<IBugRepository>().InstancePerRequest();
            builder.RegisterType<BugTaskRepository>().As<IBugTaskRepository>().InstancePerRequest();
            builder.RegisterType<FeatureRepository>().As<IFeatureRepository>().InstancePerRequest();
            builder.RegisterType<ProductBacklogItemRepository>().As<IProductBacklogItemRepository>().InstancePerRequest();
            builder.RegisterType<ProductBacklogItemTaskRepository>().As<IProductBacklogItemTaskRepository>().InstancePerRequest();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>().InstancePerRequest();
            builder.RegisterType<ProjectTeamMemberRepository>().As<IProjectTeamMemberRepository>().InstancePerRequest();
            builder.RegisterType<SprintRepository>().As<ISprintRepository>().InstancePerRequest();


            Container = builder.Build();

            return Container;
        }
    }
}