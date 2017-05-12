using System;
using System.Collections.Generic;
using LegacyApplication.Shared.ByModule.Scrum.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Scrum
{
    public class Bug : EntityBase
    {
        public int FeatureId { get; set; }
        public int SprintId { get; set; }

        public string Title { get; set; }
        public string Principal { get; set; }
        public string ReproSteps { get; set; }
        public string SystemInfo { get; set; }
        public string AcceptanceCriteria { get; set; }
        public int Priority { get; set; }
        public BugSeverity Severity { get; set; }
        public DateTime? FinishDate { get; set; }
        public BugStatus Status { get; set; }

        public virtual Feature Feature { get; set; }
        public virtual Sprint Sprint { get; set; }
        public virtual ICollection<BugTask> BugTasks { get; set; }
    }

    public class BugConfiguration : EntityBaseConfiguration<Bug>
    {
        public BugConfiguration()
        {
            ToTable("Scrum.Bug");

            Property(x => x.Title).HasMaxLength(20).IsRequired();
            Property(x => x.Principal).HasMaxLength(100).IsRequired();
            Property(x => x.ReproSteps).IsRequired();
            Property(x => x.SystemInfo).HasMaxLength(100).IsRequired();
            Property(x => x.AcceptanceCriteria);

            HasRequired(x => x.Feature).WithMany(x => x.Bugs).HasForeignKey(x => x.FeatureId).WillCascadeOnDelete(false);
            HasRequired(x => x.Sprint).WithMany(x => x.Bugs).HasForeignKey(x => x.SprintId).WillCascadeOnDelete(false);
        }
    }
}
