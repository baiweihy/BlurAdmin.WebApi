using System;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Work
{
    public class Schedule : EntityBase
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string UserName { get; set; }
        public string Color { get; set; }
    }

    public class ScheduleConfiguration : EntityBaseConfiguration<Schedule>
    {
        public ScheduleConfiguration()
        {
            ToTable("work.Schedule");

            Property(x => x.Title).IsRequired().HasMaxLength(50);
            Property(x => x.UserName).IsRequired().HasMaxLength(50);
            Property(x => x.Color).IsRequired().HasMaxLength(50);
        }
    }
}
