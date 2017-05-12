using System;
using LegacyApplication.Shared.ByModule.Scrum.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Scrum
{
    public class FeatureViewModel : EntityBase
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }
        public string Principal { get; set; }
        public DateTime? TargetDate { get; set; }
        public int Priority { get; set; }
        public FeatureStatus Status { get; set; }
        public string StatusDisplay => Status.ToString();

        public ProjectViewModel Project { get; set; }
    }
}
