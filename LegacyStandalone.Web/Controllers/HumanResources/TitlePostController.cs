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
    [RoutePrefix("api/TitlePost")]
    public class TitlePostController : ApiControllerBase
    {
        private readonly ITitlePostRepository _titlePostRepository;
        public TitlePostController(
            ITitlePostRepository titlePostRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _titlePostRepository = titlePostRepository;
        }

        public async Task<IEnumerable<TitlePostViewModel>> Get()
        {
            var models = await _titlePostRepository.AllIncluding(x => x.TitleLevel).ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<TitlePost>, IEnumerable<TitlePostViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _titlePostRepository.GetSingleAsync(x => x.Id == id, x => x.TitleLevel);
            if (model != null)
            {
                var viewModel = Mapper.Map<TitlePost, TitlePostViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]TitlePostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<TitlePostViewModel, TitlePost>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _titlePostRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "TitlePost", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]TitlePostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<TitlePostViewModel, TitlePost>(viewModel);

            _titlePostRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _titlePostRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _titlePostRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}