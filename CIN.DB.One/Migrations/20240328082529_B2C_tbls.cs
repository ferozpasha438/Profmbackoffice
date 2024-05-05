using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CIN.DB.One.Migrations
{
    public partial class B2C_tbls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemSubCatNameAr",
                table: "tblInvDefSubCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeptCodes",
                table: "tblErpInvItemMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ItemQty",
                table: "tblErpInvItemMaster",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "tblFomB2CJobTicket",
                columns: table => new
                {
                    TicketNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CustRegEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JODate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JODocNum = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    JOSubject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    JOStatus = table.Column<short>(type: "smallint", nullable: false),
                    JODescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JODeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    JOBookedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpWorkEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActWorkEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosingRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JOImg1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JOImg2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JOImg3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeoLatitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GeoLongitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OnlyTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    JobMaintenanceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JOSupervisor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInScope = table.Column<bool>(type: "bit", nullable: false),
                    IsCreatedByCustomer = table.Column<bool>(type: "bit", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsLateResponse = table.Column<bool>(type: "bit", nullable: false),
                    IsVoid = table.Column<bool>(type: "bit", nullable: false),
                    IsSurvey = table.Column<bool>(type: "bit", nullable: false),
                    IsWorkInProgress = table.Column<bool>(type: "bit", nullable: false),
                    IsForeClosed = table.Column<bool>(type: "bit", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    IsReconcile = table.Column<bool>(type: "bit", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsTransit = table.Column<bool>(type: "bit", nullable: false),
                    IsConvertedToWorkOrder = table.Column<bool>(type: "bit", nullable: false),
                    ForecloseReasonCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForecloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ForecloseBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancelReasonCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuotationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuotationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsQuotationSubmitted = table.Column<bool>(type: "bit", nullable: false),
                    IsPoRecieved = table.Column<bool>(type: "bit", nullable: false),
                    IsHold = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFomB2CJobTicket", x => x.TicketNumber);
                    table.ForeignKey(
                        name: "FK_tblFomB2CJobTicket_tblErpFomDepartment_JODeptCode",
                        column: x => x.JODeptCode,
                        principalTable: "tblErpFomDepartment",
                        principalColumn: "DeptCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFomB2CJobTicket_tblSndDefCustomerMaster_CustomerCode",
                        column: x => x.CustomerCode,
                        principalTable: "tblSndDefCustomerMaster",
                        principalColumn: "CustCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFomJobTicketFeedBack",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamRating = table.Column<int>(type: "int", nullable: false),
                    CompanyRating = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(750)", maxLength: 750, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFomJobTicketFeedBack", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFomJobTicketPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TokenNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFomJobTicketPayment", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFomB2CJobTicket_CustomerCode",
                table: "tblFomB2CJobTicket",
                column: "CustomerCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFomB2CJobTicket_JODeptCode",
                table: "tblFomB2CJobTicket",
                column: "JODeptCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFomB2CJobTicket");

            migrationBuilder.DropTable(
                name: "tblFomJobTicketFeedBack");

            migrationBuilder.DropTable(
                name: "tblFomJobTicketPayment");

            migrationBuilder.DropColumn(
                name: "ItemSubCatNameAr",
                table: "tblInvDefSubCategory");

            migrationBuilder.DropColumn(
                name: "DeptCodes",
                table: "tblErpInvItemMaster");

            migrationBuilder.DropColumn(
                name: "ItemQty",
                table: "tblErpInvItemMaster");
        }
    }
}
