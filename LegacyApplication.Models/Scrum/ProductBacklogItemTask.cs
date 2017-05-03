using System;
using LegacyApplication.Shared.ByModule.Scrum.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Scrum
{
    public class ProductBacklogItemTask : EntityBase
    {
        public int ProductBacklogItemId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime? FinishDate { get; set; }
        public ProductBacklogItemTaskStatus Status { get; set; }

        public virtual ProductBacklogItem ProductBacklogItem { get; set; }
    }

    public class ProductBacklogItemTaskConfiguration : EntityBaseConfiguration<ProductBacklogItemTask>
    {
        public ProductBacklogItemTaskConfiguration()
        {
            ToTable("Scrum.ProductBacklogItemTask");

            Property(x => x.Title).HasMaxLength(20).IsRequired();
            Property(x => x.Description).IsRequired();

            HasRequired(x => x.ProductBacklogItem).WithMany(x => x.ProductBacklogItemTasks).HasForeignKey(x => x.ProductBacklogItemId).WillCascadeOnDelete(false);
        }
    }
}
