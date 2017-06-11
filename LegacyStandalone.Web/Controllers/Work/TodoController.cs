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
    [RoutePrefix("api/Todo")]
    public class TodoController : ApiControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        public TodoController(
            ITodoRepository todoRepository,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository) : base(unitOfWork, departmentRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoViewModel>> Get()
        {
            var models = await _todoRepository.All.Where(x => x.UserName == UserName && !x.Deleted && !x.Completed).OrderBy(x => x.Order).ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<Todo>, IEnumerable<TodoViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await _todoRepository.GetSingleAsync(x => x.Id == id && x.UserName == UserName);
            if (model != null)
            {
                var viewModel = Mapper.Map<Todo, TodoViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]TodoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            viewModel.UserName = UserName;
            var newModel = Mapper.Map<TodoViewModel, Todo>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            _todoRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "Todo", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]TodoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            viewModel.UpdateUser = User.Identity.Name;
            viewModel.UpdateTime = Now;
            viewModel.LastAction = "更新";
            var model = Mapper.Map<TodoViewModel, Todo>(viewModel);

            _todoRepository.AttachAsModified(model);
            await UnitOfWork.SaveChangesAsync();

            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await _todoRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _todoRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }

        [Route("Order")]
        [HttpPost]
        public async Task<IHttpActionResult> Order(JObject jObj)
        {
            var ids = jObj["ids"].ToObject<List<int>>();
            var items = await _todoRepository.All.Where(x => ids.Contains(x.Id)).ToListAsync();
            var startOrder = 0;
            foreach (var id in ids)
            {
                var item = items.SingleOrDefault(x => x.Id == id);
                if (item != null)
                {
                    item.Order = startOrder++;
                    _todoRepository.Update(item);
                }
            }
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }

    }
}