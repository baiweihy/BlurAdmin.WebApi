using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using LegacyApplication.Shared.Features.Pagination;
using LegacyApplication.ViewModels.Core;
using LegacyStandalone.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;

namespace LegacyStandalone.Web.Controllers.Core
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager => _userManager ?? (_userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>());

        public UserController()
        {

        }

        public UserController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserViewModel>> Get()
        {
            var users = await UserManager.Users.ToListAsync();
            var vms = Mapper.Map<IEnumerable<ApplicationUser>, List<UserViewModel>>(users);
            return vms;
        }

        [Route("ByPage/{pageIndex}/{pageSize}/{userName?}")]
        public async Task<PaginatedItemsViewModel<UserViewModel>> GetByPage(int pageIndex, int pageSize, string userName = null)
        {
            var exp = UserManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(userName))
            {
                exp = exp.Where(x => x.UserName.Contains(userName));
            }
            var users = await exp.OrderBy(x => x.UserName)
                .Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var count = await exp.CountAsync();
            var vms = Mapper.Map<IEnumerable<ApplicationUser>, List<UserViewModel>>(users);
            var result = new PaginatedItemsViewModel<UserViewModel>(pageIndex, pageSize, count, vms);
            return result;
        }

        public async Task<IHttpActionResult> Post([FromBody] UserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = vm.UserName
                };
                var result = await UserManager.CreateAsync(user, "123456");
                if (result.Succeeded)
                {
                    user = await UserManager.FindByNameAsync(vm.UserName);
                    return Ok(user);
                }
                var temp = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    temp.Append(error).Append(". ");
                }
                throw new Exception(temp.ToString());
            }
            return BadRequest(ModelState);
        }

        public async Task<IHttpActionResult> Put(string id, [FromBody] UserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(vm.UserName);
                if (user == null)
                {
                    return NotFound();
                }
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.Email = vm.Email;
                user.Occupation = vm.Occupation;
                user.PhoneNumber = vm.PhoneNumber;
                user.PictureFileId = vm.PictureFileId;
                await UserManager.UpdateAsync(user);
                vm = Mapper.Map<ApplicationUser, UserViewModel>(user);
                return Ok(vm);
            }
            return BadRequest(ModelState);
        }

        public async Task<IHttpActionResult> Delete(string id)
        {
            var item = await UserManager.FindByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            if (item.UserName == "administrator")
            {
                return BadRequest("不可以删除管理员");
            }
            var result = UserManager.DeleteAsync(item);
            if (result.Result.Succeeded)
            {
                return Ok();
            }
            StringBuilder temp = new StringBuilder();
            foreach (var error in result.Result.Errors)
            {
                temp.Append(error).Append(". ");
            }
            throw new Exception(temp.ToString());
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword([FromBody] JToken jObj)
        {
            var userName = jObj["userName"].ToObject<string>();
            var resetPassword = jObj["resetPassword"].ToObject<string>();
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }
            await UserManager.RemovePasswordAsync(user.Id);
            await UserManager.AddPasswordAsync(user.Id, resetPassword);
            return Ok();
        }

        [HttpGet]
        [Route("Current")]
        public async Task<IHttpActionResult> GetCurrent()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                var vm = Mapper.Map<ApplicationUser, UserViewModel>(user);
                return Ok(vm);
            }
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _userManager?.Dispose();
        }
    }
}