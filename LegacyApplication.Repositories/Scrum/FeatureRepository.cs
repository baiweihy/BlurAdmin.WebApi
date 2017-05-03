using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Scrum;

namespace LegacyApplication.Repositories.Scrum
{
    public interface IFeatureRepository : IEntityBaseRepository<Feature>
    {
    }
    public class FeatureRepository : EntityBaseRepository<Feature>, IFeatureRepository
    {
        public FeatureRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
