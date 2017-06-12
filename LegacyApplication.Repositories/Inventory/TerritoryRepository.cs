using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Inventory;

namespace LegacyApplication.Repositories.Inventory
{
    public class TerritoryRepository : EntityBaseRepository<Territory>, ITerritoryRepository
    {
        public TerritoryRepository(IUnitOfWork dbFactory) : base(dbFactory)
        {
        }
    }
}
