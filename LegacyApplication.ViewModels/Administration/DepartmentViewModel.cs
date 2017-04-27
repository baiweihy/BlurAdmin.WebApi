using System.ComponentModel.DataAnnotations;
using LegacyApplication.Shared.Features.Tree;

namespace LegacyApplication.ViewModels.Administration
{
    public class DepartmentViewModel: TreeEntityBase<DepartmentViewModel>
    {
        [Display(Name = "名称")]
        [StringLength(100, ErrorMessage = "{0}的长度不可超过{1}")]
        public string Name { get; set; }
    }
}
