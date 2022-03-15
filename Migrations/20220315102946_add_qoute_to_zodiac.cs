using Microsoft.EntityFrameworkCore.Migrations;

namespace AstroBackEnd.Migrations
{
    public partial class add_qoute_to_zodiac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoroscopeZodiac");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_ZodiacId",
                table: "Quotes",
                column: "ZodiacId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Zodiacs_ZodiacId",
                table: "Quotes",
                column: "ZodiacId",
                principalTable: "Zodiacs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Zodiacs_ZodiacId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_ZodiacId",
                table: "Quotes");

            migrationBuilder.CreateTable(
                name: "HoroscopeZodiac",
                columns: table => new
                {
                    HoroscopesId = table.Column<int>(type: "int", nullable: false),
                    ZodiacsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoroscopeZodiac", x => new { x.HoroscopesId, x.ZodiacsId });
                    table.ForeignKey(
                        name: "FK_HoroscopeZodiac_Horoscopes_HoroscopesId",
                        column: x => x.HoroscopesId,
                        principalTable: "Horoscopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoroscopeZodiac_Zodiacs_ZodiacsId",
                        column: x => x.ZodiacsId,
                        principalTable: "Zodiacs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoroscopeZodiac_ZodiacsId",
                table: "HoroscopeZodiac",
                column: "ZodiacsId");
        }
    }
}
