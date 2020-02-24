using System.Collections.Generic;

namespace WorkoutApp.API.Models.Dtos
{
    public class EquipmentForReturnDetailedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ExerciseForReturnDto> Exercises { get; set; }
    }
}