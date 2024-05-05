using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CIN.DB.One.Migrations
{
    public partial class ScheduleDetailstbl_upd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tblErpFomScheduleDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReschedule",
                table: "tblErpFomScheduleDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "tblErpFomScheduleDetails",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tblErpFomScheduleDetails");

            migrationBuilder.DropColumn(
                name: "IsReschedule",
                table: "tblErpFomScheduleDetails");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "tblErpFomScheduleDetails");
        }
    }
}
