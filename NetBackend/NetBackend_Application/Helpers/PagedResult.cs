using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBackend_Application.Helpers
{
    public class PagedResult<T>
    {
        private PagedResult(List<T> items, int page, int size, int total) 
        {
            Items = items;
            Page = page;
            Size = size;
            Total = total;
        }

        public List<T> Items { get; private set; }
        public int Page { get; private set; }
        public int Size { get; private set; } 
        public int Total { get; private set; }
        public bool HasNext => Page * Size < Total;
        public bool HasPrev => Page > 1;

        public static async Task<PagedResult<T>> CreatePagedListAsync(IQueryable<T> query, int page, int size, CancellationToken cancellation) 
        {
            var total = await query.CountAsync(cancellation);
            var items = await query.Skip((page - 1) * size).Take(size).ToListAsync(cancellation);
            return new(items, page, size, total);
        }
    }
}
