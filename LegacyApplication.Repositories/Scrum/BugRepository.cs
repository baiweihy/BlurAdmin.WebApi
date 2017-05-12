using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Scrum;

namespace LegacyApplication.Repositories.Scrum
{
    public interface IBugRepository : IEntityBaseRepository<Bug>
    {
    }

    public class BugRepository : EntityBaseRepository<Bug>, IBugRepository
    {
        public BugRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
