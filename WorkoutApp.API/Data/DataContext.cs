using System;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models;

namespace WorkoutApp.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseCategory> ExerciseCategorys { get; set; }
        public DbSet<ExerciseGroup> ExerciseGroups { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<ScheduledUserWorkout> ScheduledUserWorkouts { get; set; }
    
        public DataContext(DbContextOptions<DataContext> options) : base (options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseStep>()
                .HasKey(k => new { k.ExerciseId, k.ExerciseStepNumber });
            
            modelBuilder.Entity<ExerciseCategoryExercise>()
                .HasKey(k => new { k.ExerciseCategoryId, k.ExerciseId });

            modelBuilder.Entity<ExerciseCategoryExercise>()
                .HasOne(ece => ece.ExerciseCategory)
                .WithMany(ec => ec.Exercises)
                .HasForeignKey(ece => ece.ExerciseCategoryId);

            modelBuilder.Entity<ExerciseCategoryExercise>()
                .HasOne(ece => ece.Exercise)
                .WithMany(ex => ex.ExerciseCategorys)
                .HasForeignKey(ece => ece.ExerciseId);

            modelBuilder.Entity<EquipmentExercise>()
                .HasKey(k => new { k.ExerciseId, k.EquipmentId });

            modelBuilder.Entity<EquipmentExercise>()
                .HasOne(ee => ee.Exercise)
                .WithMany(e => e.Equipment)
                .HasForeignKey(ee => ee.ExerciseId);

            modelBuilder.Entity<EquipmentExercise>()
                .HasOne(ee => ee.Equipment)
                .WithMany(eq => eq.Exercises)
                .HasForeignKey(ee => ee.EquipmentId);
        }
    }
}