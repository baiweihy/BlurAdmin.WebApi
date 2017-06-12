using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Inventory;

namespace LegacyApplication.Repositories.Inventory
{
    public interface IDailyNotificationRepository : IEntityBaseRepository<DailyNotification> { }
    public class DailyNotificationRepository : EntityBaseRepository<DailyNotification>, IDailyNotificationRepository
    {
        public DailyNotificationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
