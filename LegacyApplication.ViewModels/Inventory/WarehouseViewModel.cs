using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Inventory
{
    public class WarehouseViewModel : EntityBase
    {
        public int TerritoryId { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public TerritoryViewModel Territory { get; set; }
        public string FullWarehouseNo => Territory?.No + " -" + No;
        public string FullWarehouseName => Territory?.Name + " -" + Name;
    }
}
