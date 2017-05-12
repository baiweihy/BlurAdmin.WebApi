using System;
using LegacyApplication.Shared.ByModule.Scrum.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Scrum
{
    public class ProductBacklogItemTaskViewModel : EntityBase
    {
        public int ProductBacklogItemId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime? FinishDate { get; set; }
        public ProductBacklogItemTaskStatus Status { get; set; }
        public string StatusDisplay => Status.ToString();

        public ProductBacklogItemViewModel ProductBacklogItem { get; set; }
    }
}
