using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WorkoutApp.API.Models
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}