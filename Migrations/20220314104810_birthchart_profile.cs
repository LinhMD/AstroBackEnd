using Microsoft.EntityFrameworkCore.Migrations;

namespace AstroBackEnd.Migrations
{
    public partial class birthchart_profile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgLink",
                table: "BirthCharts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgLink",
                table: "BirthCharts");
        }
    }
}
