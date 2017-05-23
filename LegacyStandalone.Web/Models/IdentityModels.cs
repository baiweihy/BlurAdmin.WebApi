using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using LegacyApplication.Shared.Configurations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LegacyStandalone.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [MaxLength(50)]
        public string LastName { get; set; }

        [Display(Name = "职业")]
        [MaxLength(100, ErrorMessage = "{0}的长度不可以超过{1}")]
        public string Occupation { get; set; }

        public int? PictureFileId { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(AppSettings.DefaultConnection, throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}