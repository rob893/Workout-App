using System.Collections.Generic;

namespace WorkoutApp.API.Models.QueryParams
{
    public class EquipmentParams : PaginationParams
    {
        public List<int> ExerciseIds { get; set; } = new List<int>();
    }
}