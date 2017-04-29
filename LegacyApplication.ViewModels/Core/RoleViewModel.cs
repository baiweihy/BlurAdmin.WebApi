using System.ComponentModel.DataAnnotations;

namespace LegacyApplication.ViewModels.Core
{
    public class RoleViewModel
    {
        [Display(Name = "ID"), Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0}的长度范围是{1}到{2}.")]
        public string Id { get; set; }

        [Display(Name = "名称"), Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0}的长度范围是{1}到{2}.")]
        public string Name { get; set; }

        public int UserCount { get; set; }
    }
}
