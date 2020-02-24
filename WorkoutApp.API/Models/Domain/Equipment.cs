using System.Collections.Generic;

namespace WorkoutApp.API.Models.Domain
{
    public class Equipment : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EquipmentExercise> Exercises { get; set; }
    }
}