namespace WorkoutApp.API.Models
{
    public class Muscle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
    }
}