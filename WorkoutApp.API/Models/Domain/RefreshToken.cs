using System;

namespace WorkoutApp.API.Models.Domain
{
    public class RefreshToken
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Source { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}