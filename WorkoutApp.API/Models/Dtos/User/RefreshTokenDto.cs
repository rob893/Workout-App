using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Models.Dtos
{
    public class RefreshTokenDto
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public string Source { get; set; }
    }
}