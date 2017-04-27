using System;
using System.Web.Http;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Repositories.Administration;

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

        #endregion

    }
}