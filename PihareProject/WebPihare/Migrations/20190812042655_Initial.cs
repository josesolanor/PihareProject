using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPihare.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departmentstate",
                columns: table => new
                {
                    DepartmentStateId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DepartmentStateValue = table.Column<string>(nullable: true),
                    DepartmentStateDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departmentstate", x => x.DepartmentStateId);
                });

            migrationBuilder.CreateTable(
                name: "Departmenttype",
                columns: table => new
                {
                    DepartmentTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DepartmentTypeValue = table.Column<string>(nullable: true),
                    DepartmentTypeDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departmenttype", x => x.DepartmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleValue = table.Column<string>(nullable: true),
                    RoleDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "VisitState",
                columns: table => new
                {
                    VisitStateId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VisitStateValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitState", x => x.VisitStateId);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DepartmentCode = table.Column<int>(nullable: false),
                    NumberFloor = table.Column<int>(nullable: false),
                    NumberBedrooms = table.Column<int>(nullable: false),
                    DeparmentPrice = table.Column<decimal>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    DepartmentTypeId = table.Column<int>(nullable: false),
                    DepartmentStateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Department_Departmentstate_DepartmentStateId",
                        column: x => x.DepartmentStateId,
                        principalTable: "Departmentstate",
                        principalColumn: "DepartmentStateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Department_Departmenttype_DepartmentTypeId",
                        column: x => x.DepartmentTypeId,
                        principalTable: "Departmenttype",
                        principalColumn: "DepartmentTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Commisioner",
                columns: table => new
                {
                    CommisionerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    SecondLastName = table.Column<string>(nullable: true),
                    Nic = table.Column<string>(nullable: true),
                    ContractNumber = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Telefono = table.Column<int>(nullable: false),
                    CommisionerPassword = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commisioner", x => x.CommisionerId);
                    table.ForeignKey(
                        name: "FK_Commisioner_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    SecondLastName = table.Column<string>(nullable: true),
                    Observation = table.Column<string>(nullable: true),
                    CI = table.Column<string>(nullable: true),
                    CommisionerId = table.Column<int>(nullable: true),
                    RegistredDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Client_Commisioner_CommisionerId",
                        column: x => x.CommisionerId,
                        principalTable: "Commisioner",
                        principalColumn: "CommisionerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visitregistration",
                columns: table => new
                {
                    VisitRegistrationId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VisitDay = table.Column<DateTime>(nullable: false),
                    Observations = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    CommisionerId = table.Column<int>(nullable: false),
                    StateVisitStateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitregistration", x => x.VisitRegistrationId);
                    table.ForeignKey(
                        name: "FK_Visitregistration_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visitregistration_Commisioner_CommisionerId",
                        column: x => x.CommisionerId,
                        principalTable: "Commisioner",
                        principalColumn: "CommisionerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visitregistration_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visitregistration_VisitState_StateVisitStateId",
                        column: x => x.StateVisitStateId,
                        principalTable: "VisitState",
                        principalColumn: "VisitStateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_CommisionerId",
                table: "Client",
                column: "CommisionerId");

            migrationBuilder.CreateIndex(
                name: "IX_Commisioner_RoleId",
                table: "Commisioner",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_DepartmentStateId",
                table: "Department",
                column: "DepartmentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_DepartmentTypeId",
                table: "Department",
                column: "DepartmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitregistration_ClientId",
                table: "Visitregistration",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitregistration_CommisionerId",
                table: "Visitregistration",
                column: "CommisionerId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitregistration_DepartmentId",
                table: "Visitregistration",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitregistration_StateVisitStateId",
                table: "Visitregistration",
                column: "StateVisitStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visitregistration");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "VisitState");

            migrationBuilder.DropTable(
                name: "Commisioner");

            migrationBuilder.DropTable(
                name: "Departmentstate");

            migrationBuilder.DropTable(
                name: "Departmenttype");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
