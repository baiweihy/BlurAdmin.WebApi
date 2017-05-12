using System;
using LegacyApplication.Shared.ByModule.Scrum.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Scrum
{
    public class BugViewModel : EntityBase
    {
        public int FeatureId { get; set; }
        public int SprintId { get; set; }

        public string Title { get; set; }
        public string Principal { get; set; }
        public string ReproSteps { get; set; }
        public string SystemInfo { get; set; }
        public string AcceptanceCriteria { get; set; }
        public int Priority { get; set; }
        public BugSeverity Severity { get; set; }
        public string SeverityDisplay => Severity.ToString();
        public DateTime? FinishDate { get; set; }
        public BugStatus Status { get; set; }
        public string StatusDisplay => Status.ToString();

        public FeatureViewModel Feature { get; set; }
        public SprintViewModel Sprint { get; set; }
    }
}
