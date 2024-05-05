using Microsoft.EntityFrameworkCore.Migrations;

namespace CIN.DB.One.Migrations
{
    public partial class LoginAuthority_tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblErpFomSysLoginAuthority",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginID = table.Column<int>(type: "int", nullable: false),
                    RaiseTicket = table.Column<bool>(type: "bit", nullable: false),
                    VoidTicket = table.Column<bool>(type: "bit", nullable: false),
                    ForeCloseWO = table.Column<bool>(type: "bit", nullable: false),
                    ApproveTicket = table.Column<bool>(type: "bit", nullable: false),
                    CloseWO = table.Column<bool>(type: "bit", nullable: false),
                    ManageWO = table.Column<bool>(type: "bit", nullable: false),
                    ModifyTicket = table.Column<bool>(type: "bit", nullable: false),
                    ModifyWO = table.Column<bool>(type: "bit", nullable: false),
                    VoidAfterApproval = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomSysLoginAuthority", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblErpFomSysLoginAuthority");
        }
    }
}
