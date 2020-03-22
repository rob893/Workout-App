using System.Collections.Generic;

namespace WorkoutApp.API.Models.Dtos
{
    public class ExerciseCategoryForReturnDetailedDto : ExerciseCategoryForReturnDto
    {
        public IEnumerable<ExerciseForReturnDto> Exercises { get; set; }


        public ExerciseCategoryForReturnDetailedDto(string baseUrl) : base(baseUrl) { }
    }
}