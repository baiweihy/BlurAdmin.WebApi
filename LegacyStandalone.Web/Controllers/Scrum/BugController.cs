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
    [RoutePrefix("api/Bug")]
    public class BugController : ApiControllerBase
    {
        private readonly IBugRepository _bugRepository;
        public BugController(
            IBugRepository bugRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _bugRepository = bugRepository;
        }

        public async Task<IEnumerable<BugViewModel>> Get()
        {
            var models = await _bugRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<Bug>, IEnumerable<BugViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _bugRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<Bug, BugViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]BugViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<BugViewModel, Bug>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _bugRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "Bug", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]BugViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<BugViewModel, Bug>(viewModel);

            _bugRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _bugRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _bugRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}