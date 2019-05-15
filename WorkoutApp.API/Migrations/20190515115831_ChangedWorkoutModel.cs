using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutApp.API.Migrations
{
    public partial class ChangedWorkoutModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseGroups_Workouts_WorkoutId",
                table: "ExerciseGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_WorkoutPlans_WorkoutPlanId",
                table: "Workouts");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutPlanId",
                table: "Workouts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_WorkoutPlans_WorkoutPlanId",
                table: "Workouts",
                column: "WorkoutPlanId",
                principalTable: "WorkoutPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseGroups_Workouts_WorkoutId",
                table: "ExerciseGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_WorkoutPlans_WorkoutPlanId",
                table: "Workouts");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutPlanId",
                table: "Workouts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "ExerciseGroups",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseGroups_Workouts_WorkoutId",
                table: "ExerciseGroups",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_WorkoutPlans_WorkoutPlanId",
                table: "Workouts",
                column: "WorkoutPlanId",
                principalTable: "WorkoutPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
