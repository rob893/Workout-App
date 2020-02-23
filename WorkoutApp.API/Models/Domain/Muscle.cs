using System.Collections.Generic;

namespace WorkoutApp.API.Models.Domain
{
    public class Muscle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Exercise> PrimaryExercises { get; set; }
        public List<Exercise> SecondaryExercises { get; set; }
    }
}