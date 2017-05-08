using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.HumanResources;

namespace LegacyApplication.Repositories.HumanResources
{
    public interface ITitlePostRepository : IEntityBaseRepository<TitlePost>
    {

    }

    public class TitlePostRepository : EntityBaseRepository<TitlePost>, ITitlePostRepository
    {
        public TitlePostRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
