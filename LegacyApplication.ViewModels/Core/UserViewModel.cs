using System.Collections.Generic;

namespace LegacyApplication.ViewModels.Core
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Occupation { get; set; }
        public int? PictureFileId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
