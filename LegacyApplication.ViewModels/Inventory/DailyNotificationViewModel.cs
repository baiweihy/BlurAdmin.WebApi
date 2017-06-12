using System;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Inventory
{
    public class DailyNotificationViewModel: EntityBase
    {
        public DateTime NotificationTime { get; set; }
        public string UserName { get; set; }
    }
}
