using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Work;

namespace LegacyApplication.Repositories.Work
{
    public interface IInternalMailRepository : IEntityBaseRepository<InternalMail>
    {
    }

    public class InternalMailRepository : EntityBaseRepository<InternalMail>, IInternalMailRepository
    {
        public InternalMailRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
