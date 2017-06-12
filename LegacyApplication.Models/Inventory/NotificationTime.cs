using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Inventory
{
    public class NotificationTime : EntityBase
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
    }

    public class NotificationTimeConfiguration : EntityBaseConfiguration<NotificationTime>
    {
        public NotificationTimeConfiguration()
        {
        }
    }
}
