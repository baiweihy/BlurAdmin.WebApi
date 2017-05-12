using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Scrum;

namespace LegacyApplication.Repositories.Scrum
{
    public interface IProjectRepository : IEntityBaseRepository<Project>
    {
    }

    public class ProjectRepository : EntityBaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
