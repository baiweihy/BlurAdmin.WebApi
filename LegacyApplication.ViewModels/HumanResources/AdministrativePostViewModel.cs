using System.ComponentModel.DataAnnotations;
using LegacyApplication.Shared.ByModule.HumanResources.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.HumanResources
{
    public class AdministrativePostViewModel : EntityBase
    {
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}的长度不可超过{1}")]
        public string Name { get; set; }

        public AdministrativePostNature AdministrativePostNature { get; set; }
        public string AdministrativePostNatureDisplay => AdministrativePostNature.ToString();

        public int AdministrativeLevelId { get; set; }
        public AdministrativeLevelViewModel AdministrativeLevel { get; set; }
    }
}
