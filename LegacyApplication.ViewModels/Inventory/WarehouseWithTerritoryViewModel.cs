namespace LegacyApplication.ViewModels.Inventory
{
    public class WarehouseWithTerritoryViewModel
    {
        public int TerritoryId { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseNo { get; set; }
        public string WarehouseName { get; set; }
        public string TerritoryNo { get; set; }
        public string TerritoryName { get; set; }
        public string FullWarehouseNo => TerritoryNo + " - " + WarehouseNo;
        public string FullWarehouseName => TerritoryName + " - " + WarehouseName;
        public string Description => $"{TerritoryNo}({TerritoryName}) - {WarehouseNo}({WarehouseName})";
    }
}
