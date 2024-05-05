using Microsoft.EntityFrameworkCore.Migrations;

namespace CIN.DB.One.Migrations
{
    public partial class upd_Fom_DeptResorcestbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApprovalAuth",
                table: "tblErpFomResources",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSheduleRequired1",
                table: "tblErpFomDepartment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSheduleRequired2",
                table: "tblErpFomDepartment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalAuth",
                table: "tblErpFomResources");

            migrationBuilder.DropColumn(
                name: "IsSheduleRequired1",
                table: "tblErpFomDepartment");

            migrationBuilder.DropColumn(
                name: "IsSheduleRequired2",
                table: "tblErpFomDepartment");
        }
    }
}
