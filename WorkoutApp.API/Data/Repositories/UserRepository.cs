using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Data.Repositories
{
    public class UserRepository : Repository<User>
    {
        // Perhaps use UserManager<User> in future?
        public UserRepository(DataContext context) : base(context) { }

        public async Task<User> GetUserAsync(int id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserDetailedAsync(int id)
        {
            return await context.Users
                .Include(u => u.UserRoles).ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<List<User>> GetUsersDetailedAsync()
        {
            return await context.Users
                .Include(u => u.UserRoles).ThenInclude(r => r.Role)
                .ToListAsync();
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await context.Roles.ToListAsync();
        }
    }
}