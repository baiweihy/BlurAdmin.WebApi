using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Scrum;

namespace LegacyApplication.Repositories.Scrum
{
    public interface IBugTaskRepository : IEntityBaseRepository<BugTask>
    {
    }
    public class BugTaskRepository : EntityBaseRepository<BugTask>, IBugTaskRepository
    {
        public BugTaskRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}