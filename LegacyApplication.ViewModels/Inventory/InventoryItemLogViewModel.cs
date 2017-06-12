using LegacyApplication.Shared.ByModule.Inventory;
using LegacyApplication.Shared.ByModule.Inventory.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Inventory
{
    public class InventoryItemLogViewModel : EntityBase
    {
        public int InventoryItemId { get; set; }
        public InventoryOperationType InventoryOperationType { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }

        public string ScannedSerialNo { get; set; }
        public string Remark { get; set; }

        public InventoryItemViewModel InventoryItem { get; set; }
    }
}
