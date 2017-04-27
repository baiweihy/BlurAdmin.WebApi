using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Administration;
using LegacyApplication.Repositories.Administration;
using LegacyApplication.Shared.Features.Tree;
using LegacyApplication.ViewModels.Administration;
using LegacyStandalone.Web.Controllers.Bases;

namespace LegacyStandalone.Web.Controllers.Administration
{
    [RoutePrefix("api/Department")]
    public class DepartmentController : ApiControllerBase
    {
        public DepartmentController(IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository)
            : base(unitOfWork, departmentRepository)
        {
        }

        public async Task<IEnumerable<DepartmentViewModel>> Get()
        {
            var models = await DepartmentRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(models);
            return viewModels;
        }

        public async Task<IHttpActionResult> GetOne(int id)
        {
            var model = await DepartmentRepository.GetSingleAsync(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<Department, DepartmentViewModel>(model);
                return Ok(viewModel);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post([FromBody]DepartmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newModel = Mapper.Map<DepartmentViewModel, Department>(viewModel);
            newModel.CreateUser = newModel.UpdateUser = User.Identity.Name;
            if (newModel.ParentId > 0)
            {
                var parent = await DepartmentRepository.GetSingleAsync(newModel.ParentId.Value);
                if (parent != null)
                {
                    newModel.AncestorIds = parent.GetAncestorIdsAsParent();
                }
            }

            DepartmentRepository.Add(newModel);
            await UnitOfWork.SaveChangesAsync();

            return RedirectToRoute("", new { controller = "Department", id = newModel.Id });
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]DepartmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = DepartmentRepository.GetSingle(id);
            if (model == null)
            {
                return NotFound();
            }
            if (model.ParentId != viewModel.ParentId)
            {
                model.ParentId = viewModel.ParentId;
                if (model.ParentId.HasValue)
                {
                    var parent = await DepartmentRepository.GetSingleAsync(model.ParentId.Value);
                    if (parent != null)
                    {
                        model.AncestorIds = parent.GetAncestorIdsAsParent();
                    }
                }
                else
                {
                    model.AncestorIds = null;
                }
            }
            model.Name = viewModel.Name;
            model.IsAbstract = viewModel.IsAbstract;
            model.UpdateUser = User.Identity.Name;
            model.UpdateTime = DateTime.Now;
            model.LastAction = "更新";

            await UnitOfWork.SaveChangesAsync();

            viewModel = Mapper.Map<Department, DepartmentViewModel>(model);
            return Ok(viewModel);
        }
        public async Task<IHttpActionResult> Delete(int id)
        {
            var model = await DepartmentRepository.GetSingleAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            DepartmentRepository.Delete(model);
            await UnitOfWork.SaveChangesAsync();
            return Ok();
        }

        // ---------------------------------------------------------------------------------------------

        [Route("Tree")]
        public async Task<IEnumerable<TreeEntityBase<DepartmentViewModel>>> GetTree()
        {
            var models = await DepartmentRepository.All.ToListAsync();
            var viewModels = Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(models);
            var roots = viewModels.ToMultipleRoots();
            return roots;
        }

    }
}