namespace WorkoutApp.API.Models.Dtos
{
    public class ExerciseCategoryForReturnDto : BaseDtoForReturn
    {
        public string Name { get; set; }


        public ExerciseCategoryForReturnDto(string baseUrl) : base(baseUrl, "exerciseCategories") { }
    }
}