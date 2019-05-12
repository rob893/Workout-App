using System.Collections.Generic;

namespace WorkoutApp.API.Models
{
    public class MuscleGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Muscle> Muscles { get; set; }
    }
}