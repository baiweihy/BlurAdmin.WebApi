using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Scrum;

namespace LegacyApplication.Repositories.Scrum
{
    public interface IProductBacklogItemTaskRepository : IEntityBaseRepository<ProductBacklogItemTask>
    {
    }

    public class ProductBacklogItemTaskRepository : EntityBaseRepository<ProductBacklogItemTask>, IProductBacklogItemTaskRepository
    {
        public ProductBacklogItemTaskRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}