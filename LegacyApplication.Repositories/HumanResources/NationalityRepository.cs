using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.HumanResources;
namespace LegacyApplication.Repositories.HumanResources
{
    public interface INationalityRepository : IEntityBaseRepository<Nationality>
    {
    }

    public class NationalityRepository : EntityBaseRepository<Nationality>, INationalityRepository
    {
        public NationalityRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
