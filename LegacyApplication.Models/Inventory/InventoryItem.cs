using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using LegacyApplication.Shared.Features.Base;
using LegacyApplication.Shared.Features.File;

namespace LegacyApplication.Models.Inventory
{
    public class InventoryItem: EntityBase, IFileEntity
    {
        public int WarehouseId { get; set; }
        public string Type { get; set; }
        public string Serial { get; set; }

        public string Model { get; set; }
        public string Specification { get; set; }
        public string Manufacturer { get; set; }
        public string Phone { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int Quantity { get; set; }

        public string Name { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime UseDate { get; set; }
        public string TechnicalStatus { get; set; }
        public string Remark { get; set; }

        public string SourceWay { get; set; }
        public string OriginalValues { get; set; }
        public string Location { get; set; }
        public string ChangingStatus { get; set; }
        public DateTime ChangeDate { get; set; }

        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<InventoryItemLog> InventoryItemLogs { get; set; }

        public int FileId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }

    public class InventoryItemConfiguration : EntityBaseConfiguration<InventoryItem>
    {
        public InventoryItemConfiguration()
        {
            Property(x => x.Type).HasMaxLength(10);
            Property(x => x.Serial).HasMaxLength(10);

            Property(x => x.Model).HasMaxLength(100);
            Property(x => x.Specification).HasMaxLength(4000);
            Property(x => x.Manufacturer).HasMaxLength(200);
            Property(x => x.Phone).HasMaxLength(100);
            Property(x => x.UnitOfMeasurement).HasMaxLength(20);

            Property(x => x.Name).HasMaxLength(50);
            Property(x => x.TechnicalStatus).HasMaxLength(50);
            Property(x => x.Remark).HasMaxLength(4000);

            Property(x => x.SourceWay).HasMaxLength(200);
            Property(x => x.OriginalValues).HasMaxLength(4000);
            Property(x => x.Location).HasMaxLength(200);
            Property(x => x.ChangingStatus).HasMaxLength(4000);

            HasRequired(x => x.Warehouse).WithMany(x => x.InventoryItems).HasForeignKey(x => x.WarehouseId).WillCascadeOnDelete(false);

            Property(x => x.WarehouseId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_InventoryItem_SerialNo", 0) { IsUnique = true }));
            Property(x => x.Type).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_InventoryItem_SerialNo", 1) { IsUnique = true }));
            Property(x => x.Serial).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_InventoryItem_SerialNo", 2) { IsUnique = true }));

            Property(x => x.FileName).HasMaxLength(200).IsRequired();
            Property(x => x.Path).HasMaxLength(200).IsRequired();
        }
    }
}
