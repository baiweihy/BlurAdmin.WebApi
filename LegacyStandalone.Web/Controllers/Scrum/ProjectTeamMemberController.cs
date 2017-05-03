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
    [RoutePrefix("api/ProjectTeamMember")]
    public class ProjectTeamMemberController : ApiControllerBase
    {
        private readonly IProjectTeamMemberRepository _projectTeamMemberRepository;
        public ProjectTeamMemberController(
            IProjectTeamMemberRepository projectTeamMemberRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _projectTeamMemberRepository = projectTeamMemberRepository;
        }

        public async Task<IEnumerable<ProjectTeamMemberViewModel>> Get()
        {
            var models = await _projectTeamMemberRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<ProjectTeamMember>, IEnumerable<ProjectTeamMemberViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _projectTeamMemberRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<ProjectTeamMember, ProjectTeamMemberViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]ProjectTeamMemberViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<ProjectTeamMemberViewModel, ProjectTeamMember>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _projectTeamMemberRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "ProjectTeamMember", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]ProjectTeamMemberViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<ProjectTeamMemberViewModel, ProjectTeamMember>(viewModel);

            _projectTeamMemberRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _projectTeamMemberRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _projectTeamMemberRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}