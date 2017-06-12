using System;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Work
{
    public class ScheduleViewModel: EntityBase
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string UserName { get; set; }
        public string Color { get; set; }
    }
}
