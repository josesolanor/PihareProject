using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPihare.Migrations
{
    public partial class chattime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MessageTime",
                table: "Chat",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageTime",
                table: "Chat");
        }
    }
}
