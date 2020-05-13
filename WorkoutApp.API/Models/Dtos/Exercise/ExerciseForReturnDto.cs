using System.Collections.Generic;

namespace WorkoutApp.API.Models.Dtos
{
    public class ExerciseForReturnDto : BaseDtoForReturn
    {
        public string EquipmentUrl { get => $"{Url}/equipment"; }
        public string ExerciseCategoriesUrl { get => $"{Url}/exerciseCategories"; }
        public string Name { get; set; }
        public int? PrimaryMuscleId { get; set; }
        public int? SecondaryMuscleId { get; set; }
        public List<ExerciseStepForReturnDto> ExerciseSteps { get; set; }


        public ExerciseForReturnDto(string baseUrl) : base(baseUrl, "exercises") { }
    }
}