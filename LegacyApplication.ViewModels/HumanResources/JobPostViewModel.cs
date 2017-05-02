using System.ComponentModel.DataAnnotations;
using LegacyApplication.Shared.ByModule.HumanResources.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.HumanResources
{
    public class JobPostViewModel: EntityBase
    {
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, ErrorMessage = "{0}的长度不可超过{1}")]
        public string Name { get; set; }

        public PostNature PostNature { get; set; }

        public int JobPostLevelId { get; set; }
        public JobPostLevelViewModel Level { get; set; }
    }
}
