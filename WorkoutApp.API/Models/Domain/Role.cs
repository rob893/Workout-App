using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WorkoutApp.API.Models.Domain
{
    public class Role : IdentityRole<int>, IIdentifiable
    {
        public List<UserRole> UserRoles { get; set; }
    }
}