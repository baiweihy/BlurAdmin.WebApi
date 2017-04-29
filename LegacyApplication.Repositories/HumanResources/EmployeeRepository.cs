using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.HumanResources;

namespace LegacyApplication.Repositories.HumanResources
{
    public interface IEmployeeRepository : IEntityBaseRepository<Employee> { }

    public class EmployeeRepository : EntityBaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
