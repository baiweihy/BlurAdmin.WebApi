using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using LegacyApplication.ViewModels.Administration;
using LegacyStandalone.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;

namespace LegacyStandalone.Web.Controllers.Administration
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


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _userManager?.Dispose();
        }
    }
}