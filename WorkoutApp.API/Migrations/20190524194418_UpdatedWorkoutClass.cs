using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutApp.API.Migrations
{
    public partial class UpdatedWorkoutClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_WorkoutPlans_WorkoutPlanId",
                table: "Workouts");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "WorkoutPlanId",
                table: "Workouts",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "Complete",
                table: "Workouts",
                newName: "Shareable");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_WorkoutPlanId",
                table: "Workouts",
                newName: "IX_Workouts_CreatedByUserId");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Workouts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnDate",
                table: "Workouts",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 24, 12, 44, 18, 649, DateTimeKind.Local).AddTicks(5159));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Workouts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Workouts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Workouts",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 24, 12, 44, 18, 651, DateTimeKind.Local).AddTicks(395));

            migrationBuilder.AddColumn<int>(
                name: "WorkoutCopiedFromId",
                table: "Workouts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ScheduledUserWorkouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    WorkoutId = table.Column<int>(nullable: false),
                    StartedDateTime = table.Column<DateTime>(nullable: true),
                    CompletedDateTime = table.Column<DateTime>(nullable: true),
                    ScheduledDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledUserWorkouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledUserWorkouts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledUserWorkouts_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_WorkoutCopiedFromId",
                table: "Workouts",
                column: "WorkoutCopiedFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledUserWorkouts_UserId",
                table: "ScheduledUserWorkouts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledUserWorkouts_WorkoutId",
                table: "ScheduledUserWorkouts",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_CreatedByUserId",
                table: "Workouts",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Workouts_WorkoutCopiedFromId",
                table: "Workouts",
                column: "WorkoutCopiedFromId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_CreatedByUserId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Workouts_WorkoutCopiedFromId",
                table: "Workouts");

            migrationBuilder.DropTable(
                name: "ScheduledUserWorkouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_WorkoutCopiedFromId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "CreatedOnDate",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "WorkoutCopiedFromId",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "Shareable",
                table: "Workouts",
                newName: "Complete");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Workouts",
                newName: "WorkoutPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_CreatedByUserId",
                table: "Workouts",
                newName: "IX_Workouts_WorkoutPlanId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Workouts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutPlans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_UserId",
                table: "WorkoutPlans",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_WorkoutPlans_WorkoutPlanId",
                table: "Workouts",
                column: "WorkoutPlanId",
                principalTable: "WorkoutPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
