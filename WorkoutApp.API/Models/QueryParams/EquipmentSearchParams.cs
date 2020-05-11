using System.Collections.Generic;

namespace WorkoutApp.API.Models.QueryParams
{
    public class EquipmentSearchParams : CursorPaginationParams
    {
        public List<int> ExerciseIds { get; set; } = new List<int>();
    }
}