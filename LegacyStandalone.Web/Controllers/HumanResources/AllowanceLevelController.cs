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
    [RoutePrefix("api/AllowanceLevel")]
    public class AllowanceLevelController : ApiControllerBase
    {
        private readonly IAllowanceLevelRepository _allowanceLevelRepository; 
        public AllowanceLevelController(
            IAllowanceLevelRepository allowanceLevelRepository,
            IUnitOfWork unitOfWork, 
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _allowanceLevelRepository = allowanceLevelRepository;
        }

        public async Task<IEnumerable<AllowanceLevelViewModel>> Get()
        {
            var models = await _allowanceLevelRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<AllowanceLevel>, IEnumerable<AllowanceLevelViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _allowanceLevelRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<AllowanceLevel, AllowanceLevelViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]AllowanceLevelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<AllowanceLevelViewModel, AllowanceLevel>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _allowanceLevelRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "AllowanceLevel", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]AllowanceLevelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<AllowanceLevelViewModel, AllowanceLevel>(viewModel);

            _allowanceLevelRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _allowanceLevelRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _allowanceLevelRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}