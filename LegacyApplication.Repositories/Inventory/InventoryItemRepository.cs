using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Inventory;

namespace LegacyApplication.Repositories.Inventory
{
    public class InventoryItemRepository : EntityBaseRepository<InventoryItem>, IInventoryItemRepository
    {
        public InventoryItemRepository(IUnitOfWork dbFactory) : base(dbFactory)
        {
        }
    }
}
