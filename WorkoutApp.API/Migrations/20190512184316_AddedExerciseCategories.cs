using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutApp.API.Migrations
{
    public partial class AddedExerciseCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Muscles_MuscleId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Muscles_MuscleGroups_MuscleGroupId",
                table: "Muscles");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_Users_userId",
                table: "WorkoutPlans");

            migrationBuilder.DropTable(
                name: "MuscleGroups");

            migrationBuilder.DropIndex(
                name: "IX_Muscles_MuscleGroupId",
                table: "Muscles");

            migrationBuilder.DropColumn(
                name: "MuscleGroupId",
                table: "Muscles");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "WorkoutPlans",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutPlans_userId",
                table: "WorkoutPlans",
                newName: "IX_WorkoutPlans_UserId");

            migrationBuilder.RenameColumn(
                name: "MuscleId",
                table: "Exercises",
                newName: "SecondaryMuscleId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_MuscleId",
                table: "Exercises",
                newName: "IX_Exercises_SecondaryMuscleId");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryMuscleId",
                table: "Exercises",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExerciseCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseCategorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseCategoryExercise",
                columns: table => new
                {
                    ExerciseCategoryId = table.Column<int>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseCategoryExercise", x => new { x.ExerciseCategoryId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_ExerciseCategoryExercise_ExerciseCategorys_ExerciseCategoryId",
                        column: x => x.ExerciseCategoryId,
                        principalTable: "ExerciseCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseCategoryExercise_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_PrimaryMuscleId",
                table: "Exercises",
                column: "PrimaryMuscleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseCategoryExercise_ExerciseId",
                table: "ExerciseCategoryExercise",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Muscles_PrimaryMuscleId",
                table: "Exercises",
                column: "PrimaryMuscleId",
                principalTable: "Muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Muscles_SecondaryMuscleId",
                table: "Exercises",
                column: "SecondaryMuscleId",
                principalTable: "Muscles",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Muscles_PrimaryMuscleId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Muscles_SecondaryMuscleId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_Users_UserId",
                table: "WorkoutPlans");

            migrationBuilder.DropTable(
                name: "ExerciseCategoryExercise");

            migrationBuilder.DropTable(
                name: "ExerciseCategorys");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_PrimaryMuscleId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "PrimaryMuscleId",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WorkoutPlans",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutPlans_UserId",
                table: "WorkoutPlans",
                newName: "IX_WorkoutPlans_userId");

            migrationBuilder.RenameColumn(
                name: "SecondaryMuscleId",
                table: "Exercises",
                newName: "MuscleId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_SecondaryMuscleId",
                table: "Exercises",
                newName: "IX_Exercises_MuscleId");

            migrationBuilder.AddColumn<int>(
                name: "MuscleGroupId",
                table: "Muscles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MuscleGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Muscles_MuscleGroupId",
                table: "Muscles",
                column: "MuscleGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Muscles_MuscleId",
                table: "Exercises",
                column: "MuscleId",
                principalTable: "Muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Muscles_MuscleGroups_MuscleGroupId",
                table: "Muscles",
                column: "MuscleGroupId",
                principalTable: "MuscleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_Users_userId",
                table: "WorkoutPlans",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
