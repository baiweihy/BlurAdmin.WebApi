using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Inventory
{
    public class Territory: EntityBase
    {
        public string No { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Warehouse> Warehouses { get; set; }
    }

    public class TerritoryConfiguration : EntityBaseConfiguration<Territory>
    {
        public TerritoryConfiguration()
        {
            Property(x => x.No).IsRequired().HasMaxLength(50);
            Property(x => x.Name).IsRequired().HasMaxLength(50);

            Property(x => x.No).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Territory_No") { IsUnique = true }));
            Property(x => x.Name).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Territory_Name") { IsUnique = true }));
        }
    }
}
