using System.Collections.Generic;
using WorkoutApp.API.Models;
using Newtonsoft.Json;

namespace WorkoutApp.API.Data
{
    public class Seed
    {
        private DataContext context;


        public Seed(DataContext context)
        {
            this.context = context;
        }

        public void SeedWorkoutPlans()
        {
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