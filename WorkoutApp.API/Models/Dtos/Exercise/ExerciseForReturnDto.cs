namespace WorkoutApp.API.Models.Dtos
{
    public class ExerciseForReturnDto : BaseDtoForReturn
    {
        public string EquipmentUrl { get => $"{Url}/equipment"; }
        public string Name { get; set; }


        public ExerciseForReturnDto(string baseUrl) : base(baseUrl, "exercises") { }
    }
}