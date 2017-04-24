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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;

namespace LegacyStandalone.Web.Controllers.Administration
{
    [RoutePrefix("api/Role")]
    public class RoleController : ApiController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationUserManager _userManager;

        public RoleManager<IdentityRole> RoleManager => _roleManager ?? new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        public ApplicationUserManager UserManager => _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public RoleController()
        {
        }

        public RoleController(RoleManager<IdentityRole> roleManager, ApplicationUserManager userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IEnumerable<RoleViewModel>> Get()
        {
            var roles = await RoleManager.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                UserCount = x.Users.Count
            })
                .ToListAsync();
            return roles;
        }

        [HttpGet]
        [Route("byUserName/{userName}")]
        public async Task<IEnumerable<RoleViewModel>> GetRolesByUserName(string userName)
        {
            var user = await UserManager.FindByNameAsync(userName);
            if (user != null)
            {
                var roles = await RoleManager.Roles.Where(x => x.Users.Any(y => y.UserId == user.Id))
                    .Select(x => new RoleViewModel
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .ToListAsync();
                return roles;
            }
            throw new Exception("没找到用户名为" + userName + "的用户");
        }

        public async Task<IHttpActionResult> Post([FromBody] RoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var role = Mapper.Map<RoleViewModel, IdentityRole>(vm);
                var result = await RoleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok();
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

        public async Task<IHttpActionResult> Put(string id, [FromBody] RoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return NotFound();
                }
                role.Name = vm.Name;
                var result = await RoleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return Ok();
                }
                StringBuilder temp = new StringBuilder();
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
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var result = RoleManager.DeleteAsync(role);
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

        [HttpGet]
        [Route("Users/{id}")]
        public async Task<object> GetUsersInRole(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                throw new Exception("未能找到此角色.");
            }
            var allUsers = await UserManager.Users.ToListAsync();
            var userIds = role.Users.Select(x => x.UserId);
            var usersInRole =
                await UserManager.Users.Where(x => userIds.Contains(x.Id)).ToListAsync();
            var usersInRoleVm = Mapper.Map<IEnumerable<ApplicationUser>, List<UserViewModel>>(usersInRole);
            var usersNotInRoleVm = new List<UserViewModel>();
            foreach (var user in allUsers)
            {
                if (usersInRole.All(x => x.Id != user.Id))
                {
                    var userVm = Mapper.Map<ApplicationUser, UserViewModel>(user);
                    usersNotInRoleVm.Add(userVm);
                }
            }
            return new { InRole = usersInRoleVm, NotInRole = usersNotInRoleVm };
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IHttpActionResult> AddUserToRole(JObject jObj)
        {
            var roleId = jObj["roleId"].ToObject<string>();
            var userName = jObj["userName"].ToObject<string>();

            var role = await RoleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var user = await UserManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var result = await UserManager.AddToRoleAsync(user.Id, role.Name);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("RemoveUser/{userName}/{roleId}")]
        public async Task<IHttpActionResult> RemoveUserFromRole(string userName, string roleId)
        {
            var role = await RoleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var user = await UserManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var result = await UserManager.RemoveFromRoleAsync(user.Id, role.Name);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _roleManager?.Dispose();
            _userManager?.Dispose();
        }
    }
}