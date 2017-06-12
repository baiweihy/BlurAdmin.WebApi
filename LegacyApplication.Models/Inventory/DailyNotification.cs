using System;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Inventory
{
    public class DailyNotification : EntityBase
    {
        public DateTime NotificationTime { get; set; }
        public string UserName { get; set; }
    }

    public class DailyNotificationConfiguration : EntityBaseConfiguration<DailyNotification>
    {
        public DailyNotificationConfiguration()
        {
            Property(x => x.UserName).IsRequired().HasMaxLength(100);
        }
    }
}
