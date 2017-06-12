using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Inventory
{
    public class NotificationTimeViewModel : EntityBase
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
