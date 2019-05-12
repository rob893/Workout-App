using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseGroup> ExerciseGroups { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }

    
        public DataContext(DbContextOptions<DataContext> options) : base (options){}
    }
}