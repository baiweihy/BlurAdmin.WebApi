using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Scrum;
using LegacyApplication.Repositories.HumanResources;
using LegacyApplication.Repositories.Scrum;
using LegacyApplication.ViewModels.Scrum;
using LegacyStandalone.Web.Controllers.Bases;

namespace LegacyStandalone.Web.Controllers.Scrum
{
    [RoutePrefix("api/BugTask")]
    public class BugTaskController : ApiControllerBase
    {
        private readonly IBugTaskRepository _bugTaskRepository;
        public BugTaskController(
            IBugTaskRepository bugTaskRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _bugTaskRepository = bugTaskRepository;
        }

        public async Task<IEnumerable<BugTaskViewModel>> Get()
        {
            var models = await _bugTaskRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<BugTask>, IEnumerable<BugTaskViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _bugTaskRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<BugTask, BugTaskViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]BugTaskViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<BugTaskViewModel, BugTask>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _bugTaskRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "BugTask", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]BugTaskViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<BugTaskViewModel, BugTask>(viewModel);

            _bugTaskRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _bugTaskRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _bugTaskRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}