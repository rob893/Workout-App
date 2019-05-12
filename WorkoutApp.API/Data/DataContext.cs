using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

    
        public DataContext(DbContextOptions<DataContext> options) : base (options){}
    }
}