using Books.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace Books.Infrastructure.Extensions
{
    public static class PagedListExtensions
    {
        public static async Task<PagedList<T>> CreatePagedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
