using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPihare.Migrations
{
    public partial class changedecimaltofloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "DeparmentPrice",
                table: "Department",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "Provenance",
                table: "Client",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Provenance",
                table: "Client");

            migrationBuilder.AlterColumn<decimal>(
                name: "DeparmentPrice",
                table: "Department",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
