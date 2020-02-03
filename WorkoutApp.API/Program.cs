using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WorkoutApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
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
