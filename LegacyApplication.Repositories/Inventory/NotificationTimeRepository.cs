using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Inventory;

namespace LegacyApplication.Repositories.Inventory
{
    public interface INotificationTimeRepository : IEntityBaseRepository<NotificationTime> { }

    public class NotificationTimeRepository : EntityBaseRepository<NotificationTime>, INotificationTimeRepository
    {
        public NotificationTimeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
