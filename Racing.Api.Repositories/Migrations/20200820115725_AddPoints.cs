using Microsoft.EntityFrameworkCore.Migrations;

namespace Racing.Api.Repositories.Migrations
{
    public partial class AddPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RacePoint",
                columns: table => new
                {
                    RacePointId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Point = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    RaceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacePoint", x => x.RacePointId);
                    table.ForeignKey(
                        name: "FK_RacePoint_RaceList_RaceId",
                        column: x => x.RaceId,
                        principalTable: "RaceList",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RacePoint_RaceId",
                table: "RacePoint",
                column: "RaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RacePoint");
        }
    }
}
