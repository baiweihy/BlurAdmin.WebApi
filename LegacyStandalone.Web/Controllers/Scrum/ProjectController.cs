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
    [RoutePrefix("api/Project")]
    public class ProjectController : ApiControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(
            IProjectRepository projectRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectViewModel>> Get()
        {
            var models = await _projectRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _projectRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<Project, ProjectViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]ProjectViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<ProjectViewModel, Project>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _projectRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "Project", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]ProjectViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<ProjectViewModel, Project>(viewModel);

            _projectRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _projectRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _projectRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}