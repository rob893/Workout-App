namespace WorkoutApp.API.Models
{
    public class ExerciseGroup
    {
        public Exercise Exercise { get; set; }
        public int ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public int TimeInSeconds { get; set; }
    }
}