using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models.QueryParams;

namespace WorkoutApp.API.Helpers
{
    public class OffsetPagedList<T> : List<T>
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }


        public OffsetPagedList(IEnumerable<T> items, int totalItems, int pageNumber, int pageSize)
        {
            TotalItems = totalItems;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            AddRange(items);
        }

        public static async Task<OffsetPagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber = 1, int pageSize = 0)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentException("pageNumber must be greater than 0.");
            }

            if (pageSize < 0)
            {
                throw new ArgumentException("pageSize must be greater than or equal to 0.");
            }

            int totalItems = await source.CountAsync();
            pageSize = pageSize == 0 ? totalItems : pageSize;
            List<T> items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new OffsetPagedList<T>(items, totalItems, pageNumber, pageSize);
        }

        public static Task<OffsetPagedList<T>> CreateAsync(IQueryable<T> source, PaginationParams searchParams)
        {
            return CreateAsync(source, searchParams.PageNumber, searchParams.PageSize);
        }
    }
}