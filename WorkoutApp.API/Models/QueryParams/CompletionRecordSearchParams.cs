using System;

namespace WorkoutApp.API.Models.QueryParams
{
    public class CompletionRecordSearchParams : PaginationParams
    {
        public DateTimeOffset? MinStartDateTime { get; set; }
        public DateTimeOffset? MaxStartDateTime { get; set; }
        public DateTimeOffset? MinCompletionDateTime { get; set; }
        public DateTimeOffset? MaxCompletionDateTime { get; set; }
        public int? UserId { get; set; }
    }
}