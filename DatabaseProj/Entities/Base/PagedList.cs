using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Base
{
    public class PagedList<T>
    {
        private PagedList(IEnumerable<T> items, int page, int pageSize, int totalCount)
        {
            Data = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public IEnumerable<T> Data { get; }

        public int Page { get; }

        public int PageSize { get; }

        public int TotalCount { get; }

        public bool HasNextPage => Page * PageSize < TotalCount;

        public bool HasPreviousPage => Page > 1;

        public static async Task<PagedList<T>> Create(IQueryable<T> query, int page, int pageSize, CancellationToken token)
        {
            var totalCount = await query.CountAsync();
            var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(token);

            return new(data, page, pageSize, totalCount);
        }

        public static async Task<PagedList<T>> Create(IEnumerable<T> collection, int page, int pageSize)
        {
            var totalCount = collection.Count();
            var data = await Task.Run(() =>
            {
                return collection.Skip((page - 1) * pageSize).Take(pageSize);
            });

            return new(data, page, pageSize, totalCount);
        }
    }
}
