using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.HumanResources;

namespace LegacyApplication.Repositories.HumanResources
{
    public interface IJobPostLevelRepository : IEntityBaseRepository<JobPostLevel>
    {
    }

    public class JobPostLevelRepository : EntityBaseRepository<JobPostLevel>, IJobPostLevelRepository
    {
        public JobPostLevelRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
