using System.Collections.Generic;

namespace WorkoutApp.API.Models.Dtos
{
    public class EquipmentForReturnDetailedDto : EquipmentForReturnDto
    {
        public IEnumerable<ExerciseForReturnDto> Exercises { get; set; }


        public EquipmentForReturnDetailedDto(string baseUrl) : base(baseUrl) { }
    }
}