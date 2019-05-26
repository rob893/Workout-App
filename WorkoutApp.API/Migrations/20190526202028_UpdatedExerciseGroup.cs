using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutApp.API.Migrations
{
    public partial class UpdatedExerciseGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseGroups_Workouts_WorkoutId",
                table: "ExerciseGroups");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "ExerciseGroups",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ScheduledUserWorkoutId",
                table: "ExerciseGroups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseGroups_ScheduledUserWorkoutId",
                table: "ExerciseGroups",
                column: "ScheduledUserWorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseGroups_ScheduledUserWorkouts_ScheduledUserWorkoutId",
                table: "ExerciseGroups",
                column: "ScheduledUserWorkoutId",
                principalTable: "ScheduledUserWorkouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseGroups_Workouts_WorkoutId",
                table: "ExerciseGroups",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseGroups_ScheduledUserWorkouts_ScheduledUserWorkoutId",
                table: "ExerciseGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseGroups_Workouts_WorkoutId",
                table: "ExerciseGroups");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseGroups_ScheduledUserWorkoutId",
                table: "ExerciseGroups");

            migrationBuilder.DropColumn(
                name: "ScheduledUserWorkoutId",
                table: "ExerciseGroups");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "ExerciseGroups",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseGroups_Workouts_WorkoutId",
                table: "ExerciseGroups",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
