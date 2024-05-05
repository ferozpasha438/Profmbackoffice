using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CIN.DB.One.Migrations
{
    public partial class FomB2CUserClientLogin_tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblFomB2CUserClientLoginMapping",
                columns: table => new
                {
                    UserClientLoginCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RegMobile = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LoginType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    LastLoginDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFomB2CUserClientLoginMapping", x => x.UserClientLoginCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFomB2CUserClientLoginMapping");
        }
    }
}
