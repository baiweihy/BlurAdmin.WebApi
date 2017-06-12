using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Inventory;

namespace LegacyApplication.Repositories.Inventory
{
    public class InventoryItemLogRepository : EntityBaseRepository<InventoryItemLog>, IInventoryItemLogRepository
    {
        public InventoryItemLogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
