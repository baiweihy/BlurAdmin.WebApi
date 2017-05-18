using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.HumanResources;

namespace LegacyApplication.Repositories.HumanResources
{
 
    public interface IAllowanceLevelRepository : IEntityBaseRepository<AllowanceLevel>
    {

    }

    public class AllowanceLevelRepository : EntityBaseRepository<AllowanceLevel>, IAllowanceLevelRepository
    {
        public AllowanceLevelRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
