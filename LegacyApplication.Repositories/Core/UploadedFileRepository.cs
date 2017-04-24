using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Models.Core;

namespace LegacyApplication.Repositories.Core
{
    public interface IUploadedFileRepository : IEntityBaseRepository<UploadedFile>
    {
    }

    public class UploadedFileRepository : EntityBaseRepository<UploadedFile>, IUploadedFileRepository
    {
        public UploadedFileRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
