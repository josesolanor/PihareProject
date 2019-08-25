using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPihare.Migrations
{
    public partial class chatonevisit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitregistration_Chat_ChatId",
                table: "Visitregistration");

            migrationBuilder.DropIndex(
                name: "IX_Visitregistration_ChatId",
                table: "Visitregistration");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Visitregistration");

            migrationBuilder.AddColumn<int>(
                name: "VisitRegistrationId",
                table: "Chat",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chat_VisitRegistrationId",
                table: "Chat",
                column: "VisitRegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Visitregistration_VisitRegistrationId",
                table: "Chat",
                column: "VisitRegistrationId",
                principalTable: "Visitregistration",
                principalColumn: "VisitRegistrationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Visitregistration_VisitRegistrationId",
                table: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Chat_VisitRegistrationId",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "VisitRegistrationId",
                table: "Chat");

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "Visitregistration",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitregistration_ChatId",
                table: "Visitregistration",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitregistration_Chat_ChatId",
                table: "Visitregistration",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
