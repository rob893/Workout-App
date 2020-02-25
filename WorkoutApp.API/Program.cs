using System.Linq;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WorkoutApp.API.Data;
using Microsoft.Extensions.Logging;

namespace WorkoutApp.API
{
    public class Program
    {
        private const string seedArgument = "seed";
        private const string migrateArgument = "migrate";
        private const string clearDataArgument = "clear";


        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            if (args.Contains(seedArgument, StringComparer.OrdinalIgnoreCase))
            {
                var migrate = args.Contains(migrateArgument, StringComparer.OrdinalIgnoreCase);
                var clearData = args.Contains(clearDataArgument, StringComparer.OrdinalIgnoreCase);

                var scope = host.Services.CreateScope();
                var serviceProvider = scope.ServiceProvider;
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                var seeder = serviceProvider.GetRequiredService<Seeder>();

                logger.LogInformation($"Seeding database: Clear old data: {clearData} Apply Migrations: {migrate}");
                seeder.SeedDatabase(clearData, migrate);
                scope.Dispose();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(new string[] {"https://localhost:5003", "http://localhost:5002"});

                });
        }
    }
}
