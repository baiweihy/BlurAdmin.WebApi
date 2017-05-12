using System;
using System.Collections.Generic;
using LegacyApplication.Shared.ByModule.Scrum.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Scrum
{
    public class ProductBacklogItem : EntityBase
    {
        public int FeatureId { get; set; }
        public int SprintId { get; set; }

        public string Title { get; set; }
        public string Principal { get; set; }
        public string Description { get; set; }
        public string AcceptanceCriteria { get; set; }
        public int Priority { get; set; }
        public DateTime? FinishDate { get; set; }
        public ProductBacklogItemStatus Status { get; set; }

        public virtual Feature Feature { get; set; }
        public virtual Sprint Sprint { get; set; }
        public virtual ICollection<ProductBacklogItemTask> ProductBacklogItemTasks { get; set; }
    }

    public class ProductBacklogItemConfiguration : EntityBaseConfiguration<ProductBacklogItem>
    {
        public ProductBacklogItemConfiguration()
        {
            ToTable("Scrum.ProductBacklogItem");

            Property(x => x.Title).HasMaxLength(20).IsRequired();
            Property(x => x.Principal).HasMaxLength(100).IsRequired();
            Property(x => x.Description).IsRequired();
            Property(x => x.AcceptanceCriteria);

            HasRequired(x => x.Feature).WithMany(x => x.ProductBacklogItems).HasForeignKey(x => x.FeatureId).WillCascadeOnDelete(false);
            HasRequired(x => x.Sprint).WithMany(x => x.ProductBacklogItems).HasForeignKey(x => x.SprintId).WillCascadeOnDelete(false);
        }
    }
}
