using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutApp.API.Migrations
{
    public partial class UpdatedTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Workouts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 24, 12, 44, 18, 651, DateTimeKind.Local).AddTicks(395));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOnDate",
                table: "Workouts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 24, 12, 44, 18, 649, DateTimeKind.Local).AddTicks(5159));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Workouts",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 24, 12, 44, 18, 651, DateTimeKind.Local).AddTicks(395),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOnDate",
                table: "Workouts",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 24, 12, 44, 18, 649, DateTimeKind.Local).AddTicks(5159),
                oldClrType: typeof(DateTime));
        }
    }
}
