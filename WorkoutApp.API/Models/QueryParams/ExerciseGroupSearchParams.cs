using System.Collections.Generic;

namespace WorkoutApp.API.Models.QueryParams
{
    public class ExerciseGroupSearchParams : CursorPaginationParams
    {
        public List<int> WorkoutId { get; set; } = new List<int>();
        public List<int> ScheduledWorkoutId { get; set; } = new List<int>();
    }
}