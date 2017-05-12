using System.ComponentModel.DataAnnotations;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.OnlineTraining
{
    public class CategoryViewModel : EntityBase
    {
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, ErrorMessage = "{0}的长度不可超过{1}")]
        public string Name { get; set; }

        [Display(Name = "描述")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(500, ErrorMessage = "{0}的长度不可超过{1}")]
        public string Description { get; set; }
    }
}
