namespace WorkoutApp.API.Models.Dtos
{
    public class EquipmentForReturnDto : BaseDtoForReturn
    {
        public string Name { get; set; }
        public string ExercisesForEquipmentUrl { get => $"{Url}/exercises"; }

        public EquipmentForReturnDto(string baseUrl) : base(baseUrl, "equipment") { }
    }
}