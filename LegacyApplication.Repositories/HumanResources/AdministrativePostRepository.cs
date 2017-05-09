using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.HumanResources;

namespace LegacyApplication.Repositories.HumanResources
{
    public interface IAdministrativePostRepository : IEntityBaseRepository<AdministrativePost>
    {

    }

    public class AdministrativePostRepository : EntityBaseRepository<AdministrativePost>, IAdministrativePostRepository
    {
        public AdministrativePostRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
