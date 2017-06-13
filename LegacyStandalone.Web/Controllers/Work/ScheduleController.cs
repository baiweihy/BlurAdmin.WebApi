using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Work;
using LegacyApplication.Repositories.HumanResources;
using LegacyApplication.Repositories.Work;
using LegacyApplication.ViewModels.Work;
using LegacyStandalone.Web.Controllers.Bases;
using Newtonsoft.Json.Linq;

namespace LegacyStandalone.Web.Controllers.Work
{
    [RoutePrefix("api/Schedule")]
    public class ScheduleController : ApiControllerBase
    {
        private readonly IScheduleRepository _scheduleRepository;
        public ScheduleController(
            IScheduleRepository scheduleRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<IEnumerable<ScheduleViewModel>> Get()
        {
            var models = await _scheduleRepository.All.Where(x => x.UserName == UserName && x.End >= Now).ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _scheduleRepository.GetSingleAsync(x => x.Id == id && x.UserName == UserName);
            if (model != null)
            {
                var viewModel = Mapper.Map<Schedule, ScheduleViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]ScheduleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            viewModel.UserName = UserName;
            var newModel = Mapper.Map<ScheduleViewModel, Schedule>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _scheduleRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "Schedule", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]ScheduleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<ScheduleViewModel, Schedule>(viewModel);

            _scheduleRepository.AttachAsModified(model);
            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _scheduleRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _scheduleRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }

        [Route("ByRange")]
        [HttpGet]
        public async Task<IEnumerable<ScheduleViewModel>> GetByRange(DateTime start, DateTime? end = null)
        {
            var endTime = end?.AddDays(1) ?? start.AddMonths(2);
            var models = await _scheduleRepository.All.Where(x => x.UserName == UserName &&
                ((x.Start <= start && x.End >= start)
                || (x.Start >= start && x.End < endTime)
                || (x.Start < endTime && x.End >= endTime)
                || (x.Start <= start && x.End >= endTime))).OrderBy(x => x.Start).ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleViewModel>>(models);
            return viewModels;
        }

    }
}