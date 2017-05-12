using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Scrum;

namespace LegacyApplication.Repositories.Scrum
{
    public interface IProjectTeamMemberRepository : IEntityBaseRepository<ProjectTeamMember>
    {
    }

    public class ProjectTeamMemberRepository : EntityBaseRepository<ProjectTeamMember>, IProjectTeamMemberRepository
    {
        public ProjectTeamMemberRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
