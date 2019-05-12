using System.Collections.Generic;
using WorkoutApp.API.Models;
using Newtonsoft.Json;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace WorkoutApp.API.Data
{
    public class Seed
    {
        private DataContext context;


        public Seed(DataContext context)
        {
            this.context = context;
        }

        public void SeedDatabase(bool overrideExistingData = false)
        {
            if (overrideExistingData)
            {
                ClearAllData();
            }

            SeedUsers();
            SeedEquipment();
            SeedMuscles();
            SeedExerciseCategories();
            SeedExercises();
        }

        private void ClearAllData()
        {
            context.Muscles.RemoveRange(context.Muscles);
            context.Equipment.RemoveRange(context.Equipment.Include(e => e.Exercises));
            context.Workouts.RemoveRange(context.Workouts);
            context.ExerciseGroups.RemoveRange(context.ExerciseGroups);
            context.Exercises.RemoveRange(context.Exercises.Include(ex => ex.Equipment).Include(ex => ex.ExerciseCategorys).Include(ex => ex.ExerciseSteps));
            context.ExerciseCategorys.RemoveRange(context.ExerciseCategorys.Include(ec => ec.Exercises));
            context.WorkoutPlans.RemoveRange(context.WorkoutPlans);
            context.Users.RemoveRange(context.Users);

            context.SaveChanges();
        }

        private void SeedUsers()
        {
            if (context.Users.Any())
            {
                return;
            }

            string data = System.IO.File.ReadAllText("Data/Seed Data/UserSeedData.json");
            List<User> users = JsonConvert.DeserializeObject<List<User>>(data);

            foreach (User user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();

                context.Users.Add(user);
            }

            context.SaveChanges();
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

            foreach (Exercise exercise in exercises)
            {
                context.Exercises.Add(exercise);
            }

            context.SaveChanges();
        }

        private void SeedWorkoutPlans()
        {
            if (context.WorkoutPlans.Any())
            {
                return;
            }

            string data = System.IO.File.ReadAllText("Data/WorkoutPlanSeedData.json");
            List<WorkoutPlan> plans = JsonConvert.DeserializeObject<List<WorkoutPlan>>(data);

            foreach (WorkoutPlan plan in plans)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);

                plan.User.PasswordHash = passwordHash;
                plan.User.PasswordSalt = passwordSalt;
                plan.User.Username = plan.User.Username.ToLower();

                context.WorkoutPlans.Add(plan);
            }

            context.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}