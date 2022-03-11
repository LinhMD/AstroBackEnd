using Microsoft.EntityFrameworkCore.Migrations;

namespace AstroBackEnd.Migrations
{
    public partial class add_stuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aspects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanetBaseId = table.Column<int>(type: "int", nullable: false),
                    PlanetCompareId = table.Column<int>(type: "int", nullable: false),
                    AngleType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aspects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aspects_Planets_PlanetBaseId",
                        column: x => x.PlanetBaseId,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aspects_Planets_PlanetCompareId",
                        column: x => x.PlanetCompareId,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LifeAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HoroscopeItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspectId = table.Column<int>(type: "int", nullable: false),
                    LifeAttributeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoroscopeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoroscopeItems_Aspects_AspectId",
                        column: x => x.AspectId,
                        principalTable: "Aspects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoroscopeItems_LifeAttributes_LifeAttributeId",
                        column: x => x.LifeAttributeId,
                        principalTable: "LifeAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aspects_PlanetBaseId",
                table: "Aspects",
                column: "PlanetBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Aspects_PlanetCompareId",
                table: "Aspects",
                column: "PlanetCompareId");

            migrationBuilder.CreateIndex(
                name: "IX_HoroscopeItems_AspectId",
                table: "HoroscopeItems",
                column: "AspectId");

            migrationBuilder.CreateIndex(
                name: "IX_HoroscopeItems_LifeAttributeId",
                table: "HoroscopeItems",
                column: "LifeAttributeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoroscopeItems");

            migrationBuilder.DropTable(
                name: "Aspects");

            migrationBuilder.DropTable(
                name: "LifeAttributes");
        }
    }
}
