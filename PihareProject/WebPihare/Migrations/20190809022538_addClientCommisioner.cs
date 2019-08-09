using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPihare.Migrations
{
    public partial class addClientCommisioner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommisionerId",
                table: "Client",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_CommisionerId",
                table: "Client",
                column: "CommisionerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Commisioner_CommisionerId",
                table: "Client",
                column: "CommisionerId",
                principalTable: "Commisioner",
                principalColumn: "CommisionerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Commisioner_CommisionerId",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_CommisionerId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "CommisionerId",
                table: "Client");
        }
    }
}
