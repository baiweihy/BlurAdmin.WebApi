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
    [RoutePrefix("api/ProductBacklogItem")]
    public class ProductBacklogItemController : ApiControllerBase
    {
        private readonly IProductBacklogItemRepository _productBacklogItemRepository;
        public ProductBacklogItemController(
            IProductBacklogItemRepository productBacklogItemRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _productBacklogItemRepository = productBacklogItemRepository;
        }

        public async Task<IEnumerable<ProductBacklogItemViewModel>> Get()
        {
            var models = await _productBacklogItemRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<ProductBacklogItem>, IEnumerable<ProductBacklogItemViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _productBacklogItemRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<ProductBacklogItem, ProductBacklogItemViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]ProductBacklogItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<ProductBacklogItemViewModel, ProductBacklogItem>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _productBacklogItemRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "ProductBacklogItem", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]ProductBacklogItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<ProductBacklogItemViewModel, ProductBacklogItem>(viewModel);

            _productBacklogItemRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _productBacklogItemRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _productBacklogItemRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}