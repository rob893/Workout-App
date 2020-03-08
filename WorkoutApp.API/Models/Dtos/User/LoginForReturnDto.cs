namespace WorkoutApp.API.Models.Dtos
{
    public class LoginForReturnDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserForReturnDto User { get; set; }
    }
}