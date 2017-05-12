using System.ComponentModel.DataAnnotations;
using LegacyApplication.Shared.ByModule.HumanResources.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.HumanResources
{
    public class TitlePostViewModel : EntityBase
    {
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}的长度不可超过{1}")]
        public string Name { get; set; }

        public TitlePostNature TitlePostNature { get; set; }
        public string TitlePostNatureDisplay => TitlePostNature.ToString();

        public int TitleLevelId { get; set; }
        public TitleLevelViewModel TitleLevel { get; set; }
    }
}
