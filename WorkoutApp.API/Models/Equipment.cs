using System.Collections.Generic;

namespace WorkoutApp.API.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EquipmentExercise> Exercises { get; set; }
    }
}