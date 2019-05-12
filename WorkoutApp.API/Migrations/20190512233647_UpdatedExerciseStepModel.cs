using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutApp.API.Migrations
{
    public partial class UpdatedExerciseStepModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSteps_Exercises_ExerciseId",
                table: "ExerciseSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseSteps",
                table: "ExerciseSteps");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseSteps_ExerciseId",
                table: "ExerciseSteps");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExerciseSteps");

            migrationBuilder.RenameTable(
                name: "ExerciseSteps",
                newName: "ExerciseStep");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseStep",
                table: "ExerciseStep",
                columns: new[] { "ExerciseId", "ExerciseStepNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseStep_Exercises_ExerciseId",
                table: "ExerciseStep",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseStep_Exercises_ExerciseId",
                table: "ExerciseStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseStep",
                table: "ExerciseStep");

            migrationBuilder.RenameTable(
                name: "ExerciseStep",
                newName: "ExerciseSteps");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ExerciseSteps",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseSteps",
                table: "ExerciseSteps",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSteps_ExerciseId",
                table: "ExerciseSteps",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSteps_Exercises_ExerciseId",
                table: "ExerciseSteps",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
