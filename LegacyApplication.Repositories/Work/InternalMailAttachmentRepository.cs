using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Work;

namespace LegacyApplication.Repositories.Work
{
    public interface IInternalMailAttachmentRepository : IEntityBaseRepository<InternalMailAttachment>
    {
    }

    public class InternalMailAttachmentRepository : EntityBaseRepository<InternalMailAttachment>, IInternalMailAttachmentRepository
    {
        public InternalMailAttachmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
