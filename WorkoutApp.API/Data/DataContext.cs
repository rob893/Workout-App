using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutApp.API.Models.Domain;

namespace WorkoutApp.API.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, 
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseCategory> ExerciseCategories { get; set; }
        public DbSet<ExerciseGroup> ExerciseGroups { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<ScheduledWorkout> ScheduledWorkouts { get; set; }
        public DbSet<WorkoutInvitation> WorkoutInvitations { get; set; }
        public DbSet<WorkoutCompletionRecord> WorkoutCompletionRecords { get; set; }
        public DbSet<ExerciseGroupCompletionRecord> ExerciseGroupCompletionRecords { get; set; }
        public DbSet<ExerciseSetCompletionRecord> ExerciseSetCompletionRecords { get; set; }
    
        public DataContext(DbContextOptions<DataContext> options) : base (options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<User>(user => 
            {
                user.HasMany(u => u.ReceivedInvitations)
                    .WithOne(inv => inv.Invitee)
                    .HasForeignKey(inv => inv.InviteeId);

                user.HasMany(u => u.SentInvitations)
                    .WithOne(inv => inv.Inviter)
                    .HasForeignKey(inv => inv.InviterId);
            });

            modelBuilder.Entity<RefreshToken>(rToken =>
            {
                rToken.HasKey(k => new { k.UserId, k.Source });
            });

            modelBuilder.Entity<UserFavoriteExercise>(favEx => 
            {
                favEx.HasKey(k => new { k.UserId, k.ExerciseId });

                favEx.HasOne(fe => fe.User)
                    .WithMany(u => u.FavoriteExercises)
                    .HasForeignKey(fe => fe.UserId);

                favEx.HasOne(fe => fe.Exercise)
                    .WithMany(ex => ex.FavoritedBy)
                    .HasForeignKey(fe => fe.ExerciseId);
            });

            modelBuilder.Entity<ScheduledWorkout>(workout =>
            {
                workout.HasOne(wo => wo.ScheduledByUser)
                    .WithMany(user => user.OwnedScheduledWorkouts)
                    .HasForeignKey(wo => wo.ScheduledByUserId);
            });

            modelBuilder.Entity<ScheduledWorkoutUser>(sWoUser =>
            {
                sWoUser.HasKey(k => new { k.UserId, k.ScheduledWorkoutId });

                sWoUser.HasOne(swu => swu.User)
                    .WithMany(user => user.ScheduledWorkouts)
                    .HasForeignKey(swu => swu.UserId);

                sWoUser.HasOne(swu => swu.ScheduledWorkout)
                    .WithMany(swo => swo.Attendees)
                    .HasForeignKey(swu => swu.ScheduledWorkoutId);
            });

            modelBuilder.Entity<Exercise>(exercise =>
            {
                exercise.HasOne(e => e.PrimaryMuscle)
                    .WithMany(m => m.PrimaryExercises);

                exercise.HasOne(e => e.SecondaryMuscle)
                    .WithMany(m => m.SecondaryExercises);
            });
                
            modelBuilder.Entity<ExerciseStep>()
                .HasKey(k => new { k.ExerciseId, k.ExerciseStepNumber });

            modelBuilder.Entity<ExerciseCategoryExercise>(exCatEx => 
            {
                exCatEx.HasKey(k => new { k.ExerciseCategoryId, k.ExerciseId });

                exCatEx.HasOne(ece => ece.ExerciseCategory)
                    .WithMany(ec => ec.Exercises)
                    .HasForeignKey(ece => ece.ExerciseCategoryId);

                exCatEx.HasOne(ece => ece.Exercise)
                    .WithMany(ex => ex.ExerciseCategorys)
                    .HasForeignKey(ece => ece.ExerciseId);
            });
            
            modelBuilder.Entity<EquipmentExercise>(eqEx => 
            {
                eqEx.HasKey(k => new { k.ExerciseId, k.EquipmentId });

                eqEx.HasOne(ee => ee.Exercise)
                    .WithMany(e => e.Equipment)
                    .HasForeignKey(ee => ee.ExerciseId);

                eqEx.HasOne(ee => ee.Equipment)
                    .WithMany(eq => eq.Exercises)
                    .HasForeignKey(ee => ee.EquipmentId);
            });
        }
    }
}