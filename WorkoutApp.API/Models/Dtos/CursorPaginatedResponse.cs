using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;

namespace WorkoutApp.API.Models.Dtos
{
    public class CursorPaginatedResponse<T> where T : class, IIdentifiable
    {
        public IEnumerable<Edge<T>> Edges { get; set; }
        public IEnumerable<T> Nodes { get; set; }
        public PageInfo PageInfo { get; set; }
        public int? TotalCount { get; set; }


        public CursorPaginatedResponse(IEnumerable<T> items, string startCursor, string endCursor, bool hasNextPage, bool hasPreviousPage, int? totalCount)
        {
            SetEdges(items);
            Nodes = items.ToList();
            PageInfo = new PageInfo
            {
                StartCursor = startCursor,
                EndCursor = endCursor,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPreviousPage
            };
            TotalCount = totalCount;
        }

        public CursorPaginatedResponse(CursorPagedList<T> items)
        {
            SetEdges(items);

            Nodes = items.ToList();
            PageInfo = new PageInfo
            {
                StartCursor = items.StartCursor,
                EndCursor = items.EndCursor,
                HasNextPage = items.HasNextPage,
                HasPreviousPage = items.HasPreviousPage
            };
            TotalCount = items.TotalCount;
        }

        public static CursorPaginatedResponse<T> CreateFrom<TSource>(CursorPagedList<TSource> items, Func<IEnumerable<TSource>, IEnumerable<T>> mappingFunction) where TSource : class, IIdentifiable
        {
            var mappedItems = mappingFunction(items);

            return new CursorPaginatedResponse<T>(mappedItems, items.StartCursor, items.EndCursor, items.HasNextPage, items.HasPreviousPage, items.TotalCount);
        }

        private void SetEdges(IEnumerable<T> items)
        {
            Edges = items.Select(item => new Edge<T>
            {
                Cursor = item.Id.ConvertInt32ToBase64(),
                Node = item
            });
        }
    }

    public class Edge<T>
    {
        public string Cursor { get; set; }
        public T Node { get; set; }
    }

    public class PageInfo
    {
        public string StartCursor { get; set; }
        public string EndCursor { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}