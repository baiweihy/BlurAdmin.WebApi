using System;
using LegacyApplication.Shared.ByModule.Scrum.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Scrum
{
    public class ProductBacklogItemViewModel : EntityBase
    {
        public int FeatureId { get; set; }
        public int SprintId { get; set; }

        public string Title { get; set; }
        public string Principal { get; set; }
        public string Description { get; set; }
        public string AcceptanceCriteria { get; set; }
        public int Priority { get; set; }
        public DateTime? FinishDate { get; set; }
        public ProductBacklogItemStatus Status { get; set; }
        public string StatusDisplay => Status.ToString();

        public FeatureViewModel Feature { get; set; }
        public SprintViewModel Sprint { get; set; }
    }
}
