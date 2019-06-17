using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutApp.API.Migrations
{
    public partial class AddedWorkoutInvitations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkoutInvitations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InviterId = table.Column<int>(nullable: false),
                    InviteeId = table.Column<int>(nullable: false),
                    ScheduledUserWorkoutId = table.Column<int>(nullable: false),
                    Accepted = table.Column<bool>(nullable: false),
                    Declined = table.Column<bool>(nullable: false),
                    RespondedAtDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutInvitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutInvitations_AspNetUsers_InviteeId",
                        column: x => x.InviteeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutInvitations_AspNetUsers_InviterId",
                        column: x => x.InviterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutInvitations_ScheduledUserWorkouts_ScheduledUserWorkou~",
                        column: x => x.ScheduledUserWorkoutId,
                        principalTable: "ScheduledUserWorkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutInvitations_InviteeId",
                table: "WorkoutInvitations",
                column: "InviteeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutInvitations_InviterId",
                table: "WorkoutInvitations",
                column: "InviterId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutInvitations_ScheduledUserWorkoutId",
                table: "WorkoutInvitations",
                column: "ScheduledUserWorkoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutInvitations");
        }
    }
}
