using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Dtos
{
    public class ExerciseGroupForCreationDto
    {
        [Required]
        public int ExerciseId { get; set; }

        [Required]
        public int Sets { get; set; }

        [Required]
        public int Repetitions { get; set; }
    }
}