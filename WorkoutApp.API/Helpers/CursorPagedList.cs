using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models.QueryParams;
using WorkoutApp.API.Models.Domain;

namespace WorkoutApp.API.Helpers
{
    public class CursorPagedList<T> : List<T> where T : class, IIdentifiable
    {
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public string StartCursor { get; set; }
        public string EndCursor { get; set; }
        public int? TotalItems { get; set; }


        public CursorPagedList(IEnumerable<T> items, bool hasNextPage, bool hasPreviousPage, string startCursor, string endCursor, int? totalItems)
        {
            HasNextPage = hasNextPage;
            HasPreviousPage = hasPreviousPage;
            StartCursor = startCursor;
            EndCursor = endCursor;
            TotalItems = totalItems;
            AddRange(items);
        }

        public static async Task<CursorPagedList<T>> CreateAsync(IQueryable<T> source, int? first, string after, int? last, string before, bool includeTotal = false)
        {
            if (first != null && last != null)
            {
                throw new ArgumentException("Passing both `first` and `last` to paginate is not supported.");
            }

            int? totalItems = null;

            if (includeTotal)
            {
                totalItems = await source.CountAsync();
            }

            if (first == null && last == null)
            {
                List<T> items = await source.OrderBy(item => item.Id).ToListAsync();

                var startCursor = items.FirstOrDefault()?.Id.ConvertInt32ToBase64();
                var endCursor = items.LastOrDefault()?.Id.ConvertInt32ToBase64();

                return new CursorPagedList<T>(items, false, false, startCursor, endCursor, totalItems);
            }

            if (first != null)
            {
                if (first.Value < 0)
                {
                    throw new ArgumentException("first cannot be less than 0.");
                }

                var afterId = after == null ? int.MinValue : after.ConvertToInt32FromBase64();
                var beforeId = before == null ? int.MaxValue : before.ConvertToInt32FromBase64();

                List<T> items = await source.Where(item => item.Id > afterId && item.Id < beforeId)
                    .OrderBy(item => item.Id)
                    .Take(first.Value + 1).ToListAsync();

                var hasNextPage = items.Count >= first.Value + 1;
                var hasPreviousPage = after != null;

                if (items.Count >= first.Value + 1)
                {
                    items.RemoveAt(items.Count - 1);
                }

                var startCursor = items.FirstOrDefault()?.Id.ConvertInt32ToBase64();
                var endCursor = items.LastOrDefault()?.Id.ConvertInt32ToBase64();

                return new CursorPagedList<T>(items, hasNextPage, hasPreviousPage, startCursor, endCursor, totalItems);
            }

            if (last != null)
            {
                if (last.Value < 0)
                {
                    throw new ArgumentException("last cannot be less than 0.");
                }

                var afterId = after == null ? int.MinValue : after.ConvertToInt32FromBase64();
                var beforeId = before == null ? int.MaxValue : before.ConvertToInt32FromBase64();

                List<T> items = await source.Where(item => item.Id > afterId && item.Id < beforeId)
                    .OrderByDescending(item => item.Id)
                    .Take(last.Value + 1).ToListAsync();

                var hasNextPage = before != null;
                var hasPreviousPage = items.Count >= last.Value + 1;

                if (items.Count >= last.Value + 1)
                {
                    items.RemoveAt(items.Count - 1);
                }

                items.Reverse();

                var startCursor = items.FirstOrDefault()?.Id.ConvertInt32ToBase64();
                var endCursor = items.LastOrDefault()?.Id.ConvertInt32ToBase64();

                return new CursorPagedList<T>(items, hasNextPage, hasPreviousPage, startCursor, endCursor, totalItems);
            }

            throw new Exception("Error creating cursor paged list.");
        }

        public static Task<CursorPagedList<T>> CreateAsync(IQueryable<T> source, CursorPaginationParams searchParams)
        {
            return CreateAsync(source, searchParams.First, searchParams.After, searchParams.Last, searchParams.Before, searchParams.IncludeTotal);
        }
    }
}