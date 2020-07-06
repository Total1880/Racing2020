using Microsoft.EntityFrameworkCore.Migrations;

namespace Racing.Api.Repositories.Migrations
{
    public partial class FirstSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NationList",
                columns: table => new
                {
                    NationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationList", x => x.NationId);
                });

            migrationBuilder.CreateTable(
                name: "RacerPersonList",
                columns: table => new
                {
                    RacerPersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    NationId = table.Column<int>(nullable: true)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_RacerPersonList_NationId",
                table: "RacerPersonList",
                column: "NationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RacerPersonList");

            migrationBuilder.DropTable(
                name: "NationList");
        }
    }
}
