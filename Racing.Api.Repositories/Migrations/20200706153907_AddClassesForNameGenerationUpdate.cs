using Microsoft.EntityFrameworkCore.Migrations;

namespace Racing.Api.Repositories.Migrations
{
    public partial class AddClassesForNameGenerationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FirstNamesList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    NationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstNamesList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirstNamesList_NationList_NationId",
                        column: x => x.NationId,
                        principalTable: "NationList",
                        principalColumn: "NationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LastNamesList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(nullable: true),
                    NationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastNamesList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LastNamesList_NationList_NationId",
                        column: x => x.NationId,
                        principalTable: "NationList",
                        principalColumn: "NationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FirstNamesList_NationId",
                table: "FirstNamesList",
                column: "NationId");

            migrationBuilder.CreateIndex(
                name: "IX_LastNamesList_NationId",
                table: "LastNamesList",
                column: "NationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirstNamesList");

            migrationBuilder.DropTable(
                name: "LastNamesList");
        }
    }
}
