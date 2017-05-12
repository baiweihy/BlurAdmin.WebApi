using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Scrum;

namespace LegacyApplication.Repositories.Scrum
{
    public interface IProductBacklogItemRepository : IEntityBaseRepository<ProductBacklogItem>
    {
    }

    public class ProductBacklogItemRepository : EntityBaseRepository<ProductBacklogItem>, IProductBacklogItemRepository
    {
        public ProductBacklogItemRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
