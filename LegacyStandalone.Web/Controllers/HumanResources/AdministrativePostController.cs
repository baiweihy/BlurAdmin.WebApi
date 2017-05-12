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
    [RoutePrefix("api/AdministrativePost")]
    public class AdministrativePostController : ApiControllerBase
    {
        private readonly IAdministrativePostRepository _administrativePostRepository;
        public AdministrativePostController(
            IAdministrativePostRepository administrativePostRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _administrativePostRepository = administrativePostRepository;
        }

        public async Task<IEnumerable<AdministrativePostViewModel>> Get()
        {
            var models = await _administrativePostRepository.AllIncluding(x => x.AdministrativeLevel).ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<AdministrativePost>, IEnumerable<AdministrativePostViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _administrativePostRepository.GetSingleAsync(x => x.Id == id, x => x.AdministrativeLevel);
            if (model != null)
            {
                var viewModel = Mapper.Map<AdministrativePost, AdministrativePostViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]AdministrativePostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<AdministrativePostViewModel, AdministrativePost>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _administrativePostRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "AdministrativePost", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]AdministrativePostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<AdministrativePostViewModel, AdministrativePost>(viewModel);

            _administrativePostRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _administrativePostRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _administrativePostRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}