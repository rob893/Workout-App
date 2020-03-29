using System;

namespace WorkoutApp.API.Models.QueryParams
{
    public class ScheduledWorkoutSearchParams : PaginationParams
    {
        public int? ScheduledByUserId { get; set; }
        public DateTimeOffset? MinDate { get; set; }
        public DateTimeOffset? MaxDate { get; set; }
    }
}