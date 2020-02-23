using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WorkoutApp.API.Models.QueryParams
{
    public class ExerciseSearchParams : PaginationParams
    {
        public List<int> ExerciseCategoryId { get; set; } = new List<int>();
        public List<int> EquipmentId { get; set; } = new List<int>();
    }

    public class RandomExerciseSearchParams : ExerciseSearchParams
    {
        public string ExerciseCategory { get; set; }
        [Required]
        public int NumExercises { get; set; }
        public bool Favorites { get; set; }
    }
}