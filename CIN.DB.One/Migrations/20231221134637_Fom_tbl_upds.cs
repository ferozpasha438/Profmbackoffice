using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CIN.DB.One.Migrations
{
    public partial class Fom_tbl_upds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblErpFomActivities",
                columns: table => new
                {
                    ActCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ActName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsB2B = table.Column<bool>(type: "bit", nullable: false),
                    IsB2C = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomActivities", x => x.ActCode);
                    table.ForeignKey(
                        name: "FK_tblErpFomActivities_tblErpFomDepartment_DeptCode",
                        column: x => x.DeptCode,
                        principalTable: "tblErpFomDepartment",
                        principalColumn: "DeptCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomResourceType",
                columns: table => new
                {
                    ResTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResTypeNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomResourceType", x => x.ResTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomResources",
                columns: table => new
                {
                    ResCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeptCodes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomResources", x => x.ResCode);
                    table.ForeignKey(
                        name: "FK_tblErpFomResources_tblErpFomResourceType_ResTypeCode",
                        column: x => x.ResTypeCode,
                        principalTable: "tblErpFomResourceType",
                        principalColumn: "ResTypeCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblErpFomActivities_DeptCode",
                table: "tblErpFomActivities",
                column: "DeptCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpFomResources_ResTypeCode",
                table: "tblErpFomResources",
                column: "ResTypeCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblErpFomActivities");

            migrationBuilder.DropTable(
                name: "tblErpFomResources");

            migrationBuilder.DropTable(
                name: "tblErpFomResourceType");
        }
    }
}
