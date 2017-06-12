using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Inventory
{
    public class Warehouse: EntityBase
    {
        public int TerritoryId { get; set; }
        public string No { get; set; }
        public string Name { get; set; }

        public virtual Territory Territory { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }

    public class WarehouseConfiguration : EntityBaseConfiguration<Warehouse>
    {
        public WarehouseConfiguration()
        {
            Property(x => x.No).IsRequired().HasMaxLength(50);
            Property(x => x.Name).IsRequired().HasMaxLength(50);

            HasRequired(x => x.Territory).WithMany(x => x.Warehouses).HasForeignKey(x => x.TerritoryId).WillCascadeOnDelete(false);

            Property(x => x.TerritoryId).HasColumnAnnotation("Index", new IndexAnnotation(new[]
            {
                new IndexAttribute("IX_Warehouse_TerritoryId_No", 0) {IsUnique =  true} ,
                new IndexAttribute("IX_Warehouse_TerritoryId_Name", 0) {IsUnique =  true}
            }));

            Property(x => x.No).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Warehouse_TerritoryId_No", 1) { IsUnique = true }));
            Property(x => x.Name).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Warehouse_TerritoryId_Name", 1) { IsUnique = true }));
        }
    }
}
