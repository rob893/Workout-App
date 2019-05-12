namespace WorkoutApp.API.Models
{
    public class ExerciseGroup
    {
        public int Id { get; set; }
        public Exercise Exercise { get; set; }
        public int ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
    }
}