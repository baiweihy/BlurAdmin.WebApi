using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.OnlineTraining;

namespace LegacyApplication.Repositories.OnlineTraining
{
    public interface ICategoryRepository : IEntityBaseRepository<Category> { }
    public class CategoryRepository : EntityBaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
