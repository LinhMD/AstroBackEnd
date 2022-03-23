using Microsoft.EntityFrameworkCore.Migrations;

namespace AstroBackEnd.Migrations
{
    public partial class add_tag_remove_zodiac_in_product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductZodiac");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductZodiac",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    ZodiacsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductZodiac", x => new { x.ProductsId, x.ZodiacsId });
                    table.ForeignKey(
                        name: "FK_ProductZodiac_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductZodiac_Zodiacs_ZodiacsId",
                        column: x => x.ZodiacsId,
                        principalTable: "Zodiacs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductZodiac_ZodiacsId",
                table: "ProductZodiac",
                column: "ZodiacsId");
        }
    }
}
