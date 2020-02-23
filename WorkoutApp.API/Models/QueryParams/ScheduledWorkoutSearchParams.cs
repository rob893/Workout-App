using System;

namespace WorkoutApp.API.Models.QueryParams
{
    public class ScheduledWorkoutSearchParams : PaginationParams
    {
        public int? UserId { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}