using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.HumanResources;
using LegacyApplication.Repositories.HumanResources;

namespace LegacyStandalone.Web.Controllers.Bases
{
    public abstract class ApiControllerBase : ApiController
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IDepartmentRepository DepartmentRepository;

        protected ApiControllerBase(
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository)
        {
            UnitOfWork = unitOfWork;

            DepartmentRepository = departmentRepository;
        }

        #region Current Information

        protected DateTime Now => DateTime.Now;
        protected string UserName => User.Identity.Name;

        private Department _myDepartment;
        protected async Task<Department> GetMyDepartment()
        {
            if (_myDepartment != null)
            {
                return _myDepartment;
            }
            _myDepartment = await DepartmentRepository.GetSingleAsync(x => x.Employees.Any(y => y.UserName == UserName));
            return _myDepartment;
        }

        #endregion

    }
}