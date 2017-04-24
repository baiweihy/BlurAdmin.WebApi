using LegacyStandalone.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LegacyStandalone.Web.MyConfigurations
{
    public class DatabaseInitializer
    {
        public static void SeedData()
        {
            using (var userManager =
                new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                var user = userManager.FindByName("administrator");
                if (user == null)
                {
                    user = new ApplicationUser { UserName = "administrator", Email = "admin@126.com" };
                    userManager.Create(user, "Bx@steel");
                }
                else
                {
                    userManager.RemovePassword(user.Id);
                    userManager.AddPassword(user.Id, "Bx@steel");
                }
            }
        }
    }
}