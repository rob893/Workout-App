using System;

namespace WorkoutApp.API.Models.QueryParams
{
    public class ScheduledWorkoutSearchParams : PaginationParams
    {
        public int? ScheduledByUserId { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}