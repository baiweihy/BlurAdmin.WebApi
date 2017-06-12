using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Inventory;

namespace LegacyApplication.Repositories.Inventory
{
    public interface ITerritoryRepository : IEntityBaseRepository<Territory> { }
    public interface IWarehouseRepository : IEntityBaseRepository<Warehouse> { }
    public interface IInventoryItemRepository : IEntityBaseRepository<InventoryItem> { }
    public interface IInventoryItemLogRepository : IEntityBaseRepository<InventoryItemLog> { }
}
