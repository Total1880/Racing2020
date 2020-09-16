using Microsoft.EntityFrameworkCore.Migrations;

namespace Racing.Api.Repositories.Migrations
{
    public partial class AddPrizeMoney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RacerPersonList");

            migrationBuilder.AddColumn<int>(
                name: "PrizeMoneyForOnePoint",
                table: "RaceList",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrizeMoneyForOnePoint",
                table: "RaceList");

            migrationBuilder.CreateTable(
                name: "RacerPersonList",
                columns: table => new
                {
                    RacerPersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ability = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacerPersonList", x => x.RacerPersonId);
                    table.ForeignKey(
                        name: "FK_RacerPersonList_NationList_NationId",
                        column: x => x.NationId,
                        principalTable: "NationList",
                        principalColumn: "NationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RacerPersonList_TeamList_TeamId",
                        column: x => x.TeamId,
                        principalTable: "TeamList",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RacerPersonList_NationId",
                table: "RacerPersonList",
                column: "NationId");

            migrationBuilder.CreateIndex(
                name: "IX_RacerPersonList_TeamId",
                table: "RacerPersonList",
                column: "TeamId");
        }
    }
}
