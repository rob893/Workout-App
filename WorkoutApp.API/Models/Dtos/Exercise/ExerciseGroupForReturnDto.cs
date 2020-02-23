namespace WorkoutApp.API.Models.Dtos
{
    public class ExerciseGroupForReturnDto
    {
        public int Id { get; set; }
        public ExerciseForReturnDto Exercise { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
    }
}