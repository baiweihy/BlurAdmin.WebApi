using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.HumanResources;
using LegacyApplication.Repositories.HumanResources;
using LegacyApplication.ViewModels.HumanResources;
using LegacyStandalone.Web.Controllers.Bases;

namespace LegacyStandalone.Web.Controllers.HumanResources
{
    [RoutePrefix("api/JobPostLevel")]
    public class JobPostLevelController: ApiControllerBase
    {
        private readonly IJobPostLevelRepository _jobPostLevelRepository;
        public JobPostLevelController(
            IJobPostLevelRepository jobPostLevelRepository,
            IUnitOfWork unitOfWork, 
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _jobPostLevelRepository = jobPostLevelRepository;
        }

        public async Task<IEnumerable<JobPostLevelViewModel>> Get()
        {
            var models = await _jobPostLevelRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<JobPostLevel>, IEnumerable<JobPostLevelViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _jobPostLevelRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<JobPostLevel, JobPostLevelViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]JobPostLevelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<JobPostLevelViewModel, JobPostLevel>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _jobPostLevelRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "JobPostLevel", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]JobPostLevelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<JobPostLevelViewModel, JobPostLevel>(viewModel);

            _jobPostLevelRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _jobPostLevelRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _jobPostLevelRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}