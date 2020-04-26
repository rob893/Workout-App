using System.Linq;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WorkoutApp.API.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IO;
using CommandLine;
using WorkoutApp.API.Helpers;

namespace WorkoutApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            if (args.Contains(CommandLineOptions.seedArgument, StringComparer.OrdinalIgnoreCase))
            {
                Parser.Default.ParseArguments<CommandLineOptions>(args)
                    .WithParsed(o =>
                    {
                        var scope = host.Services.CreateScope();
                        var serviceProvider = scope.ServiceProvider;
                        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

                        if (o.Password != null && o.Password == GetSeederPasswordFromConfiguration())
                        {
                            var migrate = args.Contains(CommandLineOptions.migrateArgument, StringComparer.OrdinalIgnoreCase);
                            var clearData = args.Contains(CommandLineOptions.clearDataArgument, StringComparer.OrdinalIgnoreCase);

                            var seeder = serviceProvider.GetRequiredService<Seeder>();

                            logger.LogInformation($"Seeding database: Clear old data: {clearData} Apply Migrations: {migrate}");
                            seeder.SeedDatabase(clearData, migrate);
                        }
                        else
                        {
                            logger.LogWarning("Invalid seeder password");
                        }

                        scope.Dispose();
                    });
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(new string[] { "http://localhost:5002" });

                });
        }

        private static string GetSeederPasswordFromConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            var config = builder.Build();

            return config.GetValue<string>("SeederPassword");
        }
    }
}
