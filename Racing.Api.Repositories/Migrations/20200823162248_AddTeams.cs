using Microsoft.EntityFrameworkCore.Migrations;

namespace Racing.Api.Repositories.Migrations
{
    public partial class AddTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "RacerPersonList",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeamList",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamList", x => x.TeamId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RacerPersonList_TeamId",
                table: "RacerPersonList",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_RacerPersonList_TeamList_TeamId",
                table: "RacerPersonList",
                column: "TeamId",
                principalTable: "TeamList",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RacerPersonList_TeamList_TeamId",
                table: "RacerPersonList");

            migrationBuilder.DropTable(
                name: "TeamList");

            migrationBuilder.DropIndex(
                name: "IX_RacerPersonList_TeamId",
                table: "RacerPersonList");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "RacerPersonList");
        }
    }
}
