using System.Collections.Generic;

namespace LegacyApplication.ViewModels.Administration
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PersonName { get; set; }
        public string PhoneNumber { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
