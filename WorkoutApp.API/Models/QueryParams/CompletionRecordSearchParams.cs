using System;

namespace WorkoutApp.API.Models.QueryParams
{
    public class CompletionRecordSearchParams : PaginationParams
    {
        public DateTime? MinStartDateTime { get; set; }
        public DateTime? MaxStartDateTime { get; set; }
        public DateTime? MinCompletionDateTime { get; set; }
        public DateTime? MaxCompletionDateTime { get; set; }
        public int? UserId { get; set; }
    }
}