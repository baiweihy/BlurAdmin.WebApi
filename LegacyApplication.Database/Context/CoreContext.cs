using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Core;

namespace LegacyApplication.Database.Context
{
    public class CoreContext : DbContext, IUnitOfWork
    {
        public CoreContext(): base("DefaultConnection")
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
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            
            modelBuilder.Configurations.Add(new UploadedFileConfiguration());
        }
        
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        
    }
}