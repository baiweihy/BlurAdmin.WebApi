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
    [RoutePrefix("api/ProductBacklogItemTask")]
    public class ProductBacklogItemTaskController : ApiControllerBase
    {
        private readonly IProductBacklogItemTaskRepository _productBacklogItemTaskRepository;
        public ProductBacklogItemTaskController(
            IProductBacklogItemTaskRepository productBacklogItemTaskRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _productBacklogItemTaskRepository = productBacklogItemTaskRepository;
        }

        public async Task<IEnumerable<ProductBacklogItemTaskViewModel>> Get()
        {
            var models = await _productBacklogItemTaskRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<ProductBacklogItemTask>, IEnumerable<ProductBacklogItemTaskViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _productBacklogItemTaskRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<ProductBacklogItemTask, ProductBacklogItemTaskViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]ProductBacklogItemTaskViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<ProductBacklogItemTaskViewModel, ProductBacklogItemTask>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _productBacklogItemTaskRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "ProductBacklogItemTask", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]ProductBacklogItemTaskViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<ProductBacklogItemTaskViewModel, ProductBacklogItemTask>(viewModel);

            _productBacklogItemTaskRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _productBacklogItemTaskRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _productBacklogItemTaskRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}