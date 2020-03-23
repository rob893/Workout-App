namespace WorkoutApp.API.Models.Dtos
{
    public class ExerciseCategoryForReturnDto : BaseDtoForReturn
    {
        public string Name { get; set; }
        public string ExercisesUrl { get => $"{Url}/exercises"; }


        public ExerciseCategoryForReturnDto(string baseUrl) : base(baseUrl, "exerciseCategories") { }
    }
}