using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutApp.API.Migrations
{
    public partial class WorkoutsCompletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Complete",
                table: "Workouts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complete",
                table: "Workouts");
        }
    }
}
