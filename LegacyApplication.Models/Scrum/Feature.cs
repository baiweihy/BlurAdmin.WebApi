using System;
using System.Collections.Generic;
using LegacyApplication.Shared.ByModule.Scrum.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Scrum
{
    public class Feature : EntityBase
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }
        public string Principal { get; set; }
        public DateTime? TargetDate { get; set; }
        public int Priority { get; set; }
        public FeatureStatus Status { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<ProductBacklogItem> ProductBacklogItems { get; set; }
        public virtual ICollection<Bug> Bugs { get; set; }
    }

    public class FeatureConfiguration : EntityBaseConfiguration<Feature>
    {
        public FeatureConfiguration()
        {
            ToTable("Scrum.Feature");

            Property(x => x.Name).HasMaxLength(100).IsRequired();
            Property(x => x.Principal).HasMaxLength(100).IsRequired();

            HasRequired(x => x.Project).WithMany(x => x.Features).HasForeignKey(x => x.ProjectId).WillCascadeOnDelete(false);
        }
    }
}
