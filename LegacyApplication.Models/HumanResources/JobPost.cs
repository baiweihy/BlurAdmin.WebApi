using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyApplication.Shared.ByModule.HumanResources.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.HumanResources
{
    public class JobPost : EntityBase
    {
        public string Name { get; set; }
        public PostNature PostNature { get; set; }

        public int JobPostLevelId { get; set; }
        public JobPostLevel Level { get; set; }
    }

    public class JobPostConfiguration : EntityBaseConfiguration<JobPost>
    {
        public JobPostConfiguration()
        {
            ToTable("hr.JobPost");

            Property(x => x.Name).IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasMaxLength(50).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute { IsUnique = true }));
            HasRequired(x => x.Level).WithMany(x => x.Posts).HasForeignKey(x => x.JobPostLevelId);
        }
    }
}
