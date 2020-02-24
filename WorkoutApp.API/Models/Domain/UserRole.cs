using Microsoft.AspNetCore.Identity;

namespace WorkoutApp.API.Models.Domain
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}