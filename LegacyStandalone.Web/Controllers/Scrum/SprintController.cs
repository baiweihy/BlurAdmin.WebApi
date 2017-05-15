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
using System.Linq;

namespace LegacyStandalone.Web.Controllers.Scrum
{
    [RoutePrefix("api/Sprint")]
    public class SprintController : ApiControllerBase
    {
        private readonly ISprintRepository _sprintRepository;
        public SprintController(
            ISprintRepository sprintRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _sprintRepository = sprintRepository;
        }

        public async Task<IEnumerable<SprintViewModel>> Get()
        {
            var models = await _sprintRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<Sprint>, IEnumerable<SprintViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _sprintRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<Sprint, SprintViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]SprintViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<SprintViewModel, Sprint>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _sprintRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "Sprint", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]SprintViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<SprintViewModel, Sprint>(viewModel);

            _sprintRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _sprintRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _sprintRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("QuerySprints/{projectId}")]
        public async Task<IEnumerable<SprintViewModel>> QuerySprints(int projectId)
        {
            var models = await _sprintRepository.All.Where(x => x.ProjectId == projectId).ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<Sprint>, IEnumerable<SprintViewModel>>(models);
            return viewModels;
        }
    }
}