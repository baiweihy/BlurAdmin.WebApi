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
    [RoutePrefix("api/TitleLevel")]
    public class TitleLevelController : ApiControllerBase
    {
        private readonly ITitleLevelRepository _titleLevelRepository;
        public TitleLevelController(
            ITitleLevelRepository titleLevelRepository,
            ICommonService commonService,
            IUnitOfWork unitOfWork) : base(commonService, unitOfWork)
        {
            _titleLevelRepository = titleLevelRepository;
        }

        public async Task<IEnumerable<TitleLevelViewModel>> Get()
        {
            var models = await _titleLevelRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<TitleLevel>, IEnumerable<TitleLevelViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _titleLevelRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<TitleLevel, TitleLevelViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]TitleLevelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<TitleLevelViewModel, TitleLevel>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _titleLevelRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "TitleLevel", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]TitleLevelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<TitleLevelViewModel, TitleLevel>(viewModel);

            _titleLevelRepository.AttachAsModified(model);

            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _titleLevelRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _titleLevelRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}