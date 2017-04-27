using System.Collections.Generic;

namespace LegacyApplication.Shared.Features.Pagination
{
    public class PaginatedItemsViewModel<TEntity> where TEntity : class
    {
        public int PageIndex { get; }

        public int PageSize { get; }

        public long Count { get; }

        public long PageCount => Count / PageSize + (Count % PageSize > 0 ? 1 : 0);

        public IEnumerable<TEntity> Data { get; }

        public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
