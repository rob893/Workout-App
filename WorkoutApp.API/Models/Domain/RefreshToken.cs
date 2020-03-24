using System;
using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.API.Models.Domain
{
    public class RefreshToken
    {
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        [MaxLength(255)]
        public string Source { get; set; }
        [Required]
        [MaxLength(255)]
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}