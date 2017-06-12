using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Work;

namespace LegacyApplication.Repositories.Work
{
    public interface IScheduleRepository : IEntityBaseRepository<Schedule> { }

    public class ScheduleRepository : EntityBaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
