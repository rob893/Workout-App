using System.Collections.Generic;

namespace WorkoutApp.API.Models.QueryParams
{
    public class EquipmentSearchParams : PaginationParams
    {
        public List<int> ExerciseIds { get; set; } = new List<int>();
    }

    public class EquipmentCursorSearchParams : CursorPaginationParams
    {
        public List<int> ExerciseIds { get; set; } = new List<int>();
    }
}