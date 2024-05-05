using Microsoft.EntityFrameworkCore.Migrations;

namespace CIN.DB.One.Migrations
{
    public partial class itemCat_ar_tblupd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemCatName_Ar",
                table: "tblInvDefCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemCatName_Ar",
                table: "tblInvDefCategory");
        }
    }
}
