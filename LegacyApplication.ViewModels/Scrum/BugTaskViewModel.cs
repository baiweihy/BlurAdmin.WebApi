using System;
using LegacyApplication.Shared.ByModule.Scrum.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Scrum
{
    public class BugTaskViewModel : EntityBase
    {
        public int BugId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime? FinishDate { get; set; }
        public BugTaskStatus Status { get; set; }
        public string StatusDisplay => Status.ToString();

        public BugViewModel Bug { get; set; }
    }
}
