using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Administration;

namespace LegacyApplication.Repositories.Administration
{
    public interface IDepartmentRepository : IEntityBaseRepository<Department> { }

    public class DepartmentRepository : EntityBaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
