using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WorkoutApp.API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }


        public PagedList(List<T> items, int totalItems, int pageNumber, int pageSize)
        {
            TotalItems = totalItems;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            AddRange(items);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber = 1, int pageSize = 0)
        {
            int totalItems = await source.CountAsync();
            pageSize = pageSize == 0 ? totalItems : pageSize;
            List<T> items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<T>(items, totalItems, pageNumber, pageSize);
        }
    }
}