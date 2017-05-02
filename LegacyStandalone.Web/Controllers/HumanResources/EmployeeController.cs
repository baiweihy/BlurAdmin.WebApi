using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.HumanResources;
using LegacyApplication.Repositories.HumanResources;
using LegacyApplication.Shared.Features.Pagination;
using LegacyApplication.ViewModels.HumanResources;
using LegacyStandalone.Web.Controllers.Bases;

namespace LegacyStandalone.Web.Controllers.HumanResources
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeViewModel>> Get()
        {
            var models = await _employeeRepository.AllIncluding(x => x.Post).ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _employeeRepository.GetSingleAsync(x => x.Id == id, x => x.Post);
            if (model != null)
            {
                var viewModel = Mapper.Map<Employee, EmployeeViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        [Route("ByPage/{pageIndex}/{pageSize}/{includeChildren}/{departmentId?}")]
        public async Task<PaginatedItemsViewModel<EmployeeViewModel>> GetByPage(int pageIndex, int pageSize, bool includeChildren, int? departmentId = null)
        {
            var exp = _employeeRepository.AllIncluding(x => x.Department, x => x.Post).AsQueryable();
            if (departmentId != null)
            {
                if (includeChildren)
                {
                    var departmentIdStr = departmentId.Value.ToString();
                    var endStr = $"-{departmentIdStr}";
                    var containStr = $"-{departmentIdStr}-";
                    exp = exp.Where(x => x.DepartmentId == departmentId.Value || x.Department.AncestorIds.StartsWith(departmentIdStr) || x.Department.AncestorIds.EndsWith(endStr) || x.Department.AncestorIds.Contains(containStr));
                }
                else
                {
                    exp = exp.Where(x => x.DepartmentId == departmentId.Value);
                }
            }
            var items = await exp.OrderBy(x => x.Department.Order).ThenBy(x => x.No)
                .Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var count = await exp.CountAsync();
            var vms = Mapper.Map<IEnumerable<Employee>, List<EmployeeViewModel>>(items);
            var result = new PaginatedItemsViewModel<EmployeeViewModel>(pageIndex, pageSize, count, vms);
            return result;
        }

        public async Task<IHttpActionResult> Post([FromBody]EmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<EmployeeViewModel, Employee>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _employeeRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "Employee", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]EmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<EmployeeViewModel, Employee>(viewModel);

            _employeeRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _employeeRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _employeeRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }

    }
}