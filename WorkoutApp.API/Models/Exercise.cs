namespace WorkoutApp.API.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Muscle Muscle { get; set; }
    }
}