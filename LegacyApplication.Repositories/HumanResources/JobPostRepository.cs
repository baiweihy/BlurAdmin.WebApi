using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.HumanResources;

namespace LegacyApplication.Repositories.HumanResources
{
    public interface IJobPostRepository : IEntityBaseRepository<JobPost>
    {

    }

    public class JobPostRepository : EntityBaseRepository<JobPost>, IJobPostRepository
    {
        public JobPostRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
