using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Work;

namespace LegacyApplication.Repositories.Work
{
    public interface IInternalMailToRepository : IEntityBaseRepository<InternalMailTo>
    {
    }

    public class InternalMailToRepository : EntityBaseRepository<InternalMailTo>, IInternalMailToRepository
    {
        public InternalMailToRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
