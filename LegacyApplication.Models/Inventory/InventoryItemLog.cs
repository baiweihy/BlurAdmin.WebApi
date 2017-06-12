using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Inventory
{
    public class InventoryItemLog : EntityBase
    {
        public int InventoryItemId { get; set; }
        public InventoryOperationType InventoryOperationType { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }

        public string ScannedSerialNo { get; set; }

        public string Remark { get; set; }

        public InventoryItem InventoryItem { get; set; }
    }
    
    public class InventoryItemLogConfiguration : EntityBaseConfiguration<InventoryItemLog>
    {
        public InventoryItemLogConfiguration()
        {
            Property(x => x.Image).HasColumnType("nvarchar(max)");
            Property(x => x.Remark).HasMaxLength(4000);
            HasRequired(x => x.InventoryItem).WithMany(x => x.InventoryItemLogs).HasForeignKey(x => x.InventoryItemId).WillCascadeOnDelete(true);
        }
    }
}
