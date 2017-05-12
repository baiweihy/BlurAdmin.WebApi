using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Reflection;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Core;
using LegacyApplication.Models.HumanResources;
using LegacyApplication.Shared.Configurations;
using LegacyApplication.Models.OnlineTraining;

namespace LegacyApplication.Database.Context
{
    public class CoreContext : DbContext, IUnitOfWork
    {
        public CoreContext() : base(AppSettings.DefaultConnection)
        {
            //System.Data.Entity.Database.SetInitializer<CoreContext>(null);
#if DEBUG
            Database.Log = Console.Write;
            Database.Log = message => Trace.WriteLine(message);
#endif
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); //去掉默认开启的级联删除

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(UploadedFile)));
        }

        public DbSet<UploadedFile> UploadedFiles { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobPostLevel> JobPostLevels { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<Category> Categorys { get; set; }
    }
}