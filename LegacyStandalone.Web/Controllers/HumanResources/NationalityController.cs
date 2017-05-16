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
    [RoutePrefix("api/Nationality")]
    public class NationalityController : ApiControllerBase
    {
        private readonly INationalityRepository _nationalityRepository;
        public NationalityController(
            INationalityRepository nationalityRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _nationalityRepository = nationalityRepository;
        }

        public async Task<IEnumerable<NationalityViewModel>> Get()
        {
            var models = await _nationalityRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<Nationality>, IEnumerable<NationalityViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _nationalityRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<Nationality, NationalityViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]NationalityViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<NationalityViewModel, Nationality>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _nationalityRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "Nationality", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]NationalityViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<NationalityViewModel, Nationality>(viewModel);

            _nationalityRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _nationalityRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _nationalityRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}