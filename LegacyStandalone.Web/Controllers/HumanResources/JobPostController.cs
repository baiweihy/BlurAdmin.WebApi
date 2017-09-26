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
using LegacyApplication.Services.Core;

namespace LegacyStandalone.Web.Controllers.HumanResources
{
    [RoutePrefix("api/JobPost")]
    public class JobPostController : ApiControllerBase
    {
        private readonly IJobPostRepository _jobPostRepository;
        public JobPostController(
            IJobPostRepository jobPostRepository,
            ICommonService commonService,
            IUnitOfWork unitOfWork) : base(commonService, unitOfWork)
        {
            _jobPostRepository = jobPostRepository;
        }

        public async Task<IEnumerable<JobPostViewModel>> Get()
        {
            var models = await _jobPostRepository.AllIncluding(x => x.Level).ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<JobPost>, IEnumerable<JobPostViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _jobPostRepository.GetSingleAsync(x => x.Id == id, x => x.Level);
            if (model != null)
            {
                var viewModel = Mapper.Map<JobPost, JobPostViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]JobPostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<JobPostViewModel, JobPost>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _jobPostRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "JobPost", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]JobPostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<JobPostViewModel, JobPost>(viewModel);

            _jobPostRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _jobPostRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _jobPostRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}