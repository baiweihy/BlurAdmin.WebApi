using System;
using System.Collections.Generic;
using LegacyApplication.Shared.ByModule.Scrum.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Scrum
{
    public class Sprint : EntityBase
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SprintStatus Status { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<ProductBacklogItem> ProductBacklogItems { get; set; }
        public virtual ICollection<Bug> Bugs { get; set; }
    }

    public class SprintConfiguration : EntityBaseConfiguration<Sprint>
    {
        public SprintConfiguration()
        {
            ToTable("Scrum.Sprint");

            Property(x => x.Name).HasMaxLength(20).IsRequired();

            HasRequired(x => x.Project).WithMany(x => x.Sprints).HasForeignKey(x => x.ProjectId).WillCascadeOnDelete(false);
        }
    }
}
