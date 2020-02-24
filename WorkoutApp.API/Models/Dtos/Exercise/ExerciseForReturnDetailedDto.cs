using System.Collections.Generic;
using WorkoutApp.API.Models.Domain;

namespace WorkoutApp.API.Models.Dtos
{
    public class ExerciseForReturnDetailedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MuscleForReturnDto PrimaryMuscle { get; set; }
        public MuscleForReturnDto SecondaryMuscle { get; set; }
        public List<ExerciseStepForReturnDto> ExerciseSteps { get; set; }
        public List<EquipmentForReturnDto> Equipment { get; set; }
        public List<ExerciseCategoryForReturnDto> ExerciseCategorys { get; set; }
    }
}