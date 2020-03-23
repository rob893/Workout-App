using System.Collections.Generic;

namespace WorkoutApp.API.Models.QueryParams
{
    public class ExerciseCategorySearchParams : PaginationParams
    {
        public List<int> ExerciseId { get; set; } = new List<int>();
    }
}