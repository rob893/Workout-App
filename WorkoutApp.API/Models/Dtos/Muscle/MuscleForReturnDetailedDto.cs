using System.Collections.Generic;

namespace WorkoutApp.API.Models.Dtos
{
    public class MuscleForReturnDetailedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ExerciseForReturnDto> PrimaryExercises { get; set; }
        public List<ExerciseForReturnDto> SecondaryExercises { get; set; }
    }
}