using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Inventory;

namespace LegacyApplication.Repositories.Inventory
{
    public class WarehouseRepository : EntityBaseRepository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(IUnitOfWork dbFactory) : base(dbFactory)
        {
        }
    }
}
