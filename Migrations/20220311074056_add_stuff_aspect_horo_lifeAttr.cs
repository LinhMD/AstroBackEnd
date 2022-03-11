using Microsoft.EntityFrameworkCore.Migrations;

namespace AstroBackEnd.Migrations
{
    public partial class add_stuff_aspect_horo_lifeAttr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Horoscopes_HoroscopeId",
                table: "Quotes");

           /* migrationBuilder.DropColumn(
                name: "HoroscopeId",
                table: "Quotes");*/

            migrationBuilder.AlterColumn<int>(
                name: "HoroscopeId",
                table: "Quotes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ZodiacId",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Horoscopes_HoroscopeId",
                table: "Quotes",
                column: "HoroscopeId",
                principalTable: "Horoscopes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Horoscopes_HoroscopeId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "ZodiacId",
                table: "Quotes");

            /*migrationBuilder.AddColumn<int>(
               name: "HoroscopeId",
               table: "Quotes",
               type: "int",
               nullable: false,
               defaultValue: 0);*/
            migrationBuilder.AlterColumn<int>(
                name: "HoroscopeId",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Horoscopes_HoroscopeId",
                table: "Quotes",
                column: "HoroscopeId",
                principalTable: "Horoscopes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
