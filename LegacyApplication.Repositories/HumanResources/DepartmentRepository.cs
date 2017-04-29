using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.HumanResources;

namespace LegacyApplication.Repositories.HumanResources
{
    public interface IDepartmentRepository : IEntityBaseRepository<Department> { }

    public class DepartmentRepository : EntityBaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
