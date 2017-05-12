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
    [RoutePrefix("api/Feature")]
    public class FeatureController : ApiControllerBase
    {
        private readonly IFeatureRepository _featureRepository;
        public FeatureController(
            IFeatureRepository featureRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _featureRepository = featureRepository;
        }

        public async Task<IEnumerable<FeatureViewModel>> Get()
        {
            var models = await _featureRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<Feature>, IEnumerable<FeatureViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _featureRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<Feature, FeatureViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]FeatureViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<FeatureViewModel, Feature>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _featureRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "Feature", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]FeatureViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<FeatureViewModel, Feature>(viewModel);

            _featureRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _featureRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _featureRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}