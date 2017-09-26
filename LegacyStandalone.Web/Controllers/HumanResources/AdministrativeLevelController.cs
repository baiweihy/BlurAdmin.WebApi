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
    [RoutePrefix("api/AdministrativeLevel")]
    public class AdministrativeLevelController : ApiControllerBase
    {
        private readonly IAdministrativeLevelRepository _administrativeLevelRepository;
        public AdministrativeLevelController(
            IAdministrativeLevelRepository administrativeLevelRepository, 
            ICommonService commonService,
            IUnitOfWork unitOfWork) : base(commonService, unitOfWork)
        {
            _administrativeLevelRepository = administrativeLevelRepository;
        }

        public async Task<IEnumerable<AdministrativeLevelViewModel>> Get()
        {
            var models = await _administrativeLevelRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<AdministrativeLevel>, IEnumerable<AdministrativeLevelViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _administrativeLevelRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<AdministrativeLevel, AdministrativeLevelViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]AdministrativeLevelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<AdministrativeLevelViewModel, AdministrativeLevel>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _administrativeLevelRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "AdministrativeLevel", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]AdministrativeLevelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<AdministrativeLevelViewModel, AdministrativeLevel>(viewModel);

            _administrativeLevelRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _administrativeLevelRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _administrativeLevelRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}