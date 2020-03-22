using System.Collections.Generic;

namespace WorkoutApp.API.Models.Dtos
{
    public class MuscleForReturnDetailedDto : MuscleForReturnDto
    {
        public List<ExerciseForReturnDto> PrimaryExercises { get; set; }
        public List<ExerciseForReturnDto> SecondaryExercises { get; set; }


        public MuscleForReturnDetailedDto(string baseUrl) : base(baseUrl) { }
    }
}