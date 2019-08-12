using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPihare.Migrations
{
    public partial class clientjson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientJson",
                table: "Visitregistration",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientJson",
                table: "Visitregistration");
        }
    }
}
