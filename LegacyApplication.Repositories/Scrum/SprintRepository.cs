using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Scrum;

namespace LegacyApplication.Repositories.Scrum
{
    public interface ISprintRepository : IEntityBaseRepository<Sprint>
    {
    }

    public class SprintRepository : EntityBaseRepository<Sprint>, ISprintRepository
    {
        public SprintRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
