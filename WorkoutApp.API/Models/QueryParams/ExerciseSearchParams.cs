using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WorkoutApp.API.Models.QueryParams
{
    public class ExerciseSearchParams : CursorPaginationParams
    {
        public List<int> ExerciseCategoryId { get; set; } = new List<int>();
        public List<int> EquipmentId { get; set; } = new List<int>();
        public List<int> PrimaryMuscleId { get; set; } = new List<int>();
        public List<int> SecondaryMuscleId { get; set; } = new List<int>();
    }

    public class RandomExerciseSearchParams : ExerciseSearchParams
    {
        public string ExerciseCategory { get; set; }
        [Required]
        public int? NumExercises { get; set; }
    }
}