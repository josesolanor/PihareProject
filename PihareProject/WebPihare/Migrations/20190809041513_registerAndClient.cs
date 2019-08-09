using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPihare.Migrations
{
    public partial class registerAndClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientRegister",
                table: "Visitregistration");

            migrationBuilder.DropColumn(
                name: "ReferencialPrice",
                table: "Visitregistration");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistredDate",
                table: "Client",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistredDate",
                table: "Client");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClientRegister",
                table: "Visitregistration",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "ReferencialPrice",
                table: "Visitregistration",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
