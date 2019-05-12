using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutApp.API.Migrations
{
    public partial class AddedExerciseSteps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseGroups_Exercises_ExerciseId",
                table: "ExerciseGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_Users_UserId",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Exercises");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WorkoutPlans",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseId",
                table: "ExerciseGroups",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ExerciseSteps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExerciseId = table.Column<int>(nullable: false),
                    ExerciseStepNumber = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSteps_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSteps_ExerciseId",
                table: "ExerciseSteps",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseGroups_Exercises_ExerciseId",
                table: "ExerciseGroups",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_Users_UserId",
                table: "WorkoutPlans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseGroups_Exercises_ExerciseId",
                table: "ExerciseGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_Users_UserId",
                table: "WorkoutPlans");

            migrationBuilder.DropTable(
                name: "ExerciseSteps");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WorkoutPlans",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Exercises",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseId",
                table: "ExerciseGroups",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseGroups_Exercises_ExerciseId",
                table: "ExerciseGroups",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_Users_UserId",
                table: "WorkoutPlans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
