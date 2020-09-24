using Microsoft.EntityFrameworkCore.Migrations;

namespace Racing.Api.Repositories.Migrations
{
    public partial class AddPartsToRaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RacePartList",
                columns: table => new
                {
                    RacePartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Part = table.Column<int>(nullable: false),
                    Start = table.Column<int>(nullable: false),
                    End = table.Column<int>(nullable: false),
                    RaceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacePartList", x => x.RacePartId);
                    table.ForeignKey(
                        name: "FK_RacePartList_RaceList_RaceId",
                        column: x => x.RaceId,
                        principalTable: "RaceList",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RacePartList_RaceId",
                table: "RacePartList",
                column: "RaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RacePartList");
        }
    }
}
