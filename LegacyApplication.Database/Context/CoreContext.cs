using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Reflection;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Core;
using LegacyApplication.Models.HumanResources;
using LegacyApplication.Models.Scrum;
using LegacyApplication.Models.Work;
using LegacyApplication.Shared.Configurations;

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

        //Core
        public DbSet<UploadedFile> UploadedFiles { get; set; }

        //Work
        public DbSet<InternalMail> InternalMails { get; set; }
        public DbSet<InternalMailTo> InternalMailTos { get; set; }
        public DbSet<InternalMailAttachment> InternalMailAttachments { get; set; }

        //HR
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobPostLevel> JobPostLevels { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<AdministrativeLevel> AdministrativeLevels { get; set; }
        public DbSet<AdministrativePost> AdministrativePosts { get; set; }
        public DbSet<TitleLevel> TitleLevels { get; set; }
        public DbSet<TitlePost> TitlePosts { get; set; }

        //Scrum
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTeamMember> ProjectTeamMembers { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<ProductBacklogItem> ProductBacklogItems { get; set; }
        public DbSet<ProductBacklogItemTask> ProductBacklogItemTasks { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<BugTask> BugTasks { get; set; }
    }
}