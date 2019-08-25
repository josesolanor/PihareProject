using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPihare.Migrations
{
    public partial class Chat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "Visitregistration",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    ChatId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    CommisionerId = table.Column<int>(nullable: false),
                    VisitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_Chat_Commisioner_CommisionerId",
                        column: x => x.CommisionerId,
                        principalTable: "Commisioner",
                        principalColumn: "CommisionerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visitregistration_ChatId",
                table: "Visitregistration",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_CommisionerId",
                table: "Chat",
                column: "CommisionerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitregistration_Chat_ChatId",
                table: "Visitregistration",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitregistration_Chat_ChatId",
                table: "Visitregistration");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Visitregistration_ChatId",
                table: "Visitregistration");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Visitregistration");
        }
    }
}
