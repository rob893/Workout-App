namespace WorkoutApp.API.Models.Dtos
{
    public class MuscleForReturnDto : BaseDtoForReturn
    {
        public string PrimaryExercisesUrl { get => $"{Url}/primaryExercises"; }
        public string SecondaryExercisesUrl { get => $"{Url}/secondaryExercises"; }
        public string Name { get; set; }
        public string Description { get; set; }


        public MuscleForReturnDto(string baseUrl) : base(baseUrl, "muscles") { }
    }
}