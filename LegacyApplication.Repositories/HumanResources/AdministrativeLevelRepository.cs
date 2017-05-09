using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.HumanResources;

namespace LegacyApplication.Repositories.HumanResources
{
    public interface IAdministrativeLevelRepository : IEntityBaseRepository<AdministrativeLevel>
    {
    }

    public class AdministrativeLevelRepository : EntityBaseRepository<AdministrativeLevel>, IAdministrativeLevelRepository
    {
        public AdministrativeLevelRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
