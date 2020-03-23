using System.Collections.Generic;

namespace WorkoutApp.API.Models.Dtos
{
    public class ExerciseForReturnDetailedDto : ExerciseForReturnDto
    {
        public MuscleForReturnDto PrimaryMuscle { get; set; }
        public MuscleForReturnDto SecondaryMuscle { get; set; }
        public List<ExerciseStepForReturnDto> ExerciseSteps { get; set; }
        public List<EquipmentForReturnDto> Equipment { get; set; }
        public List<ExerciseCategoryForReturnDto> ExerciseCategorys { get; set; }


        public ExerciseForReturnDetailedDto(string baseUrl) : base(baseUrl) { }
    }
}