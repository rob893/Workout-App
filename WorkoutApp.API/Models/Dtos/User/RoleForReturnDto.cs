using WorkoutApp.API.Models.Domain;

namespace WorkoutApp.API.Models.Dtos
{
    public class RoleForReturnDto : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}