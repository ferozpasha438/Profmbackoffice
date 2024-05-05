using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CIN.DB.One.Migrations
{
    public partial class FOM_UPd_tbls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "tblErpFomPeriod",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Period_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblErpFomPeriod", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "tblErpFomScheduleSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    DeptCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApproveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSchGenerated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomScheduleSummary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomServiceItems",
                columns: table => new
                {
                    ServiceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ActivityCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ServiceShortDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceShortDescAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceDetailsAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeUnitPrimary = table.Column<TimeSpan>(type: "time", nullable: false),
                    ResourceUnitPrimary = table.Column<int>(type: "int", nullable: false),
                    MinReqResource = table.Column<int>(type: "int", nullable: false),
                    MinRequiredHrs = table.Column<TimeSpan>(type: "time", nullable: false),
                    PotentialCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrimaryUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApplicableDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsOnOffer = table.Column<bool>(type: "bit", nullable: false),
                    OfferPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OfferStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfferEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbNailImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomServiceItems", x => x.ServiceCode);
                    table.ForeignKey(
                        name: "FK_tblErpFomServiceItems_tblErpFomActivities_ActivityCode",
                        column: x => x.ActivityCode,
                        principalTable: "tblErpFomActivities",
                        principalColumn: "ActCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpFomServiceItems_tblErpFomDepartment_DeptCode",
                        column: x => x.DeptCode,
                        principalTable: "tblErpFomDepartment",
                        principalColumn: "DeptCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomScheduleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchId = table.Column<int>(type: "int", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    SchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomScheduleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpFomScheduleDetails_tblErpFomScheduleSummary_SchId",
                        column: x => x.SchId,
                        principalTable: "tblErpFomScheduleSummary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomScheduleWeekdays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchId = table.Column<int>(type: "int", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    WeekDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AllDayLong = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomScheduleWeekdays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpFomScheduleWeekdays_tblErpFomScheduleSummary_SchId",
                        column: x => x.SchId,
                        principalTable: "tblErpFomScheduleSummary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomServiceItemsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ImagePath = table.Column<int>(type: "int", nullable: false),
                    Desc1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc1Ar = table.Column<int>(type: "int", nullable: false),
                    Desc2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc2Ar = table.Column<int>(type: "int", nullable: false),
                    ServiceDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomServiceItemsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpFomServiceItemsDetails_tblErpFomServiceItems_ServiceCode",
                        column: x => x.ServiceCode,
                        principalTable: "tblErpFomServiceItems",
                        principalColumn: "ServiceCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomServiceUnitItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    UnitFlag = table.Column<int>(type: "int", nullable: false),
                    TimeUnitPrimary = table.Column<TimeSpan>(type: "time", nullable: false),
                    ResourceUnitPrimary = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OfferPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PotentialCostFactor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PotentialUnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomServiceUnitItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpFomServiceUnitItems_tblErpFomServiceItems_ServiceCode",
                        column: x => x.ServiceCode,
                        principalTable: "tblErpFomServiceItems",
                        principalColumn: "ServiceCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblErpFomScheduleDetails_SchId",
                table: "tblErpFomScheduleDetails",
                column: "SchId");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpFomScheduleWeekdays_SchId",
                table: "tblErpFomScheduleWeekdays",
                column: "SchId");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpFomServiceItems_ActivityCode",
                table: "tblErpFomServiceItems",
                column: "ActivityCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpFomServiceItems_DeptCode",
                table: "tblErpFomServiceItems",
                column: "DeptCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpFomServiceItemsDetails_ServiceCode",
                table: "tblErpFomServiceItemsDetails",
                column: "ServiceCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpFomServiceUnitItems_ServiceCode",
                table: "tblErpFomServiceUnitItems",
                column: "ServiceCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "tblErpFomPeriod");

            migrationBuilder.DropTable(
                name: "tblErpFomScheduleDetails");

            migrationBuilder.DropTable(
                name: "tblErpFomScheduleWeekdays");

            migrationBuilder.DropTable(
                name: "tblErpFomServiceItemsDetails");

            migrationBuilder.DropTable(
                name: "tblErpFomServiceUnitItems");

            migrationBuilder.DropTable(
                name: "tblErpFomScheduleSummary");

            migrationBuilder.DropTable(
                name: "tblErpFomServiceItems");
        }
    }
}
