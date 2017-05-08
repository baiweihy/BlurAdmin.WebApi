using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.HumanResources
{
    public class TitleLevelViewModel : EntityBase
    {
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}的长度不可超过{1}")]
        public string Name { get; set; }

        
    }
}
