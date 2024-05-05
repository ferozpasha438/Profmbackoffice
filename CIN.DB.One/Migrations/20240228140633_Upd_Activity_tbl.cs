using Microsoft.EntityFrameworkCore.Migrations;

namespace CIN.DB.One.Migrations
{
    public partial class Upd_Activity_tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullImagePath",
                table: "tblErpFomDepartment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbNailImage",
                table: "tblErpFomDepartment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullImagePath",
                table: "tblErpFomActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbNailImage",
                table: "tblErpFomActivities",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullImagePath",
                table: "tblErpFomDepartment");

            migrationBuilder.DropColumn(
                name: "ThumbNailImage",
                table: "tblErpFomDepartment");

            migrationBuilder.DropColumn(
                name: "FullImagePath",
                table: "tblErpFomActivities");

            migrationBuilder.DropColumn(
                name: "ThumbNailImage",
                table: "tblErpFomActivities");
        }
    }
}
