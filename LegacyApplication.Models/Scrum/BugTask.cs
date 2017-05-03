using System;
using LegacyApplication.Shared.ByModule.Scrum.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Scrum
{
    public class BugTask : EntityBase
    {
        public int BugId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime? FinishDate { get; set; }
        public BugTaskStatus Status { get; set; }

        public virtual Bug Bug { get; set; }
    }

    public class BugTaskConfiguration : EntityBaseConfiguration<BugTask>
    {
        public BugTaskConfiguration()
        {
            ToTable("Scrum.BugTask");

            Property(x => x.Title).HasMaxLength(20).IsRequired();
            Property(x => x.Description).IsRequired();

            HasRequired(x => x.Bug).WithMany(x => x.BugTasks).HasForeignKey(x => x.BugId).WillCascadeOnDelete(false);
        }
    }
}
