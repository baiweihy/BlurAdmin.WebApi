using System;
using System.Threading;
using System.Threading.Tasks;

namespace LegacyApplication.Database.Infrastructure
{
    public interface IUnitOfWork: IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}