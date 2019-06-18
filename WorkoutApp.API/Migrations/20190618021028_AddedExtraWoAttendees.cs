using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutApp.API.Migrations
{
    public partial class AddedExtraWoAttendees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtraSchUsrWoAttendee",
                columns: table => new
                {
                    ScheduledUserWorkoutId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraSchUsrWoAttendee", x => new { x.UserId, x.ScheduledUserWorkoutId });
                    table.ForeignKey(
                        name: "FK_ExtraSchUsrWoAttendee_ScheduledUserWorkouts_ScheduledUserWor~",
                        column: x => x.ScheduledUserWorkoutId,
                        principalTable: "ScheduledUserWorkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtraSchUsrWoAttendee_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraSchUsrWoAttendee_ScheduledUserWorkoutId",
                table: "ExtraSchUsrWoAttendee",
                column: "ScheduledUserWorkoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraSchUsrWoAttendee");
        }
    }
}
