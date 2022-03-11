using Microsoft.EntityFrameworkCore.Migrations;

namespace AstroBackEnd.Migrations
{
    public partial class horo_remove_qoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Horoscopes_HoroscopeId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_HoroscopeId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "HoroscopeId",
                table: "Quotes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HoroscopeId",
                table: "Quotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_HoroscopeId",
                table: "Quotes",
                column: "HoroscopeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Horoscopes_HoroscopeId",
                table: "Quotes",
                column: "HoroscopeId",
                principalTable: "Horoscopes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
