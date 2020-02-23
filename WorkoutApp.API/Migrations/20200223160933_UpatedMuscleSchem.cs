using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutApp.API.Migrations
{
    public partial class UpatedMuscleSchem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Muscles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Muscles");
        }
    }
}
