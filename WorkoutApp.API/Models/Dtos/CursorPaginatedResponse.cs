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
        public long? TotalCount { get; set; }


        public CursorPaginatedResponse(CursorPagedList<T> items)
        {
            Edges = items.Select(item => new Edge<T>
            {
                Cursor = item.Id.ConvertInt32ToBase64(),
                Node = item
            });

            Nodes = items;
            PageInfo = new PageInfo
            {
                StartCursor = items.StartCursor,
                EndCursor = items.EndCursor,
                HasNextPage = items.HasNextPage,
                HasPreviousPage = items.HasPreviousPage
            };
            TotalCount = items.TotalItems;
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