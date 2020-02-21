using System.Collections.Generic;
using WorkoutApp.API.Models;
using Newtonsoft.Json;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WorkoutApp.API.Data
{
    public class Seed
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;


        public Seed(DataContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void SeedDatabase(bool clearCurrentData = false, bool applyMigrations = false)
        {
            if (applyMigrations)
            {
                context.Database.Migrate();
            }

            if (clearCurrentData)
            {
                ClearAllData();
            }

            SeedRoles();
            SeedUsers();
            SeedEquipment();
            SeedMuscles();
            SeedExerciseCategories();
            SeedExercises();
            SeedWorkouts();
            SeedScheduledWorkouts();
        }

        private void ClearAllData()
        {
            context.WorkoutCompletionRecords.RemoveRange(context.WorkoutCompletionRecords);
            context.ExerciseGroupCompletionRecords.RemoveRange(context.ExerciseGroupCompletionRecords);
            context.ExerciseSetCompletionRecords.RemoveRange(context.ExerciseSetCompletionRecords);
            context.Muscles.RemoveRange(context.Muscles);
            context.Equipment.RemoveRange(context.Equipment.Include(e => e.Exercises));
            context.WorkoutInvitations.RemoveRange(context.WorkoutInvitations);
            context.ScheduledWorkouts.RemoveRange(context.ScheduledWorkouts);
            context.Workouts.RemoveRange(context.Workouts);
            context.ExerciseGroups.RemoveRange(context.ExerciseGroups);
            context.Exercises.RemoveRange(context.Exercises.Include(ex => ex.Equipment).Include(ex => ex.ExerciseCategorys).Include(ex => ex.ExerciseSteps));
            context.ExerciseCategorys.RemoveRange(context.ExerciseCategorys.Include(ec => ec.Exercises));
            context.Users.RemoveRange(context.Users);
            context.Roles.RemoveRange(context.Roles);

            context.SaveChanges();
        }

        private void SeedRoles()
        {
            List<Role> roles = new List<Role> 
            {
                new Role { Name = "Admin" },
                new Role { Name = "User" }
            };

            foreach (Role role in roles)
            {
                roleManager.CreateAsync(role).Wait();
            }
        }

        private void SeedUsers()
        {
            if (userManager.Users.Any())
            {
                return;
            }

            string data = System.IO.File.ReadAllText("Data/Seed Data/UserSeedData.json");
            List<User> users = JsonConvert.DeserializeObject<List<User>>(data);

            foreach (User user in users)
            {
                userManager.CreateAsync(user, "password").Wait();

                if (user.UserName.ToUpper() == "ADMIN")
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
                else
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }

        private void SeedEquipment()
        {
            if (context.Equipment.Any())
            {
                return;
            }

            string data = System.IO.File.ReadAllText("Data/Seed Data/EquipmentSeedData.json");
            List<Equipment> equipment = JsonConvert.DeserializeObject<List<Equipment>>(data);

            foreach (Equipment e in equipment)
            {
                context.Equipment.Add(e);
            }

            context.SaveChanges();
        }

        private void SeedMuscles()
        {
            if (context.Muscles.Any())
            {
                return;
            }

            string data = System.IO.File.ReadAllText("Data/Seed Data/MuscleSeedData.json");
            List<Muscle> muscles = JsonConvert.DeserializeObject<List<Muscle>>(data);

            foreach (Muscle muscle in muscles)
            {
                context.Muscles.Add(muscle);
            }

            context.SaveChanges();
        }

        private void SeedExerciseCategories()
        {
            if (context.ExerciseCategorys.Any())
            {
                return;
            }

            string data = System.IO.File.ReadAllText("Data/Seed Data/ExerciseCategorySeedData.json");
            List<ExerciseCategory> exerciseCategories = JsonConvert.DeserializeObject<List<ExerciseCategory>>(data);

            foreach (ExerciseCategory exerciseCategory in exerciseCategories)
            {
                context.ExerciseCategorys.Add(exerciseCategory);
            }

            context.SaveChanges();
        }

        private void SeedExercises()
        {
            if (context.Exercises.Any())
            {
                return;
            }

            string data = System.IO.File.ReadAllText("Data/Seed Data/ExerciseSeedData.json");
            List<Exercise> exercises = JsonConvert.DeserializeObject<List<Exercise>>(data);
            // This is needed to ensure we don't try to create a new entity with same PK as one being tracked.
            var muscleDict = context.Muscles.ToDictionary(m => m.Id);

            foreach (var exercise in exercises) 
            {
                if (muscleDict.ContainsKey(exercise.PrimaryMuscle.Id))
                {
                    exercise.PrimaryMuscle = muscleDict[exercise.PrimaryMuscle.Id];
                }
                
                if (exercise.SecondaryMuscle != null && muscleDict.ContainsKey(exercise.SecondaryMuscle.Id)) 
                {
                    exercise.SecondaryMuscle = muscleDict[exercise.SecondaryMuscle.Id];
                }

                context.Exercises.Add(exercise);
            }

            context.SaveChanges();
        }

        private void SeedWorkouts()
        {
            if (context.Workouts.Any())
            {
                return;
            }

            string data = System.IO.File.ReadAllText("Data/Seed Data/WorkoutSeedData.json");
            List<Workout> workouts = JsonConvert.DeserializeObject<List<Workout>>(data);

            workouts.ForEach(wo => context.Workouts.Add(wo));

            context.SaveChanges();
        }

        private void SeedScheduledWorkouts()
        {
            if (context.ScheduledWorkouts.Any())
            {
                return;
            }

            string data = System.IO.File.ReadAllText("Data/Seed Data/ScheduledUserWorkoutsSeedData.json");
            List<ScheduledWorkout> scheduledWorkouts = JsonConvert.DeserializeObject<List<ScheduledWorkout>>(data);

            scheduledWorkouts.ForEach(sWo => context.ScheduledWorkouts.Add(sWo));

            context.SaveChanges();
        }
    }
}