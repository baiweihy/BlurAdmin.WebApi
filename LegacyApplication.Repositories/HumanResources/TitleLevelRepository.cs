using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.HumanResources;

namespace LegacyApplication.Repositories.HumanResources
{
    public interface ITitleLevelRepository : IEntityBaseRepository<TitleLevel>
    {
    }

    public class TitleLevelRepository : EntityBaseRepository<TitleLevel>, ITitleLevelRepository
    {
        public TitleLevelRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
