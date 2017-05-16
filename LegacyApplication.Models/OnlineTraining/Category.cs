using System.Collections.Generic;
using LegacyApplication.Shared.Features.Tree;

namespace LegacyApplication.Models.OnlineTraining
{
    public class Category : TreeEntityBase<Category>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CategoryConfiguration : TreeEntityBaseConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            ToTable("ot.Category");
            Property(x => x.Name).IsRequired().HasMaxLength(20);
            Property(x => x.Description).IsRequired().HasMaxLength(500);
        }
    }
}
