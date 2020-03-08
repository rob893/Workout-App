using CommandLine;

namespace WorkoutApp.API.Helpers
{
    public class CommandLineOptions
    {
        public const string seedArgument = "seed";
        public const string migrateArgument = "migrate";
        public const string clearDataArgument = "clear";

        [Option("password", Required = false, HelpText = "Input password.")]
        public string Password { get; set; }
    }
}