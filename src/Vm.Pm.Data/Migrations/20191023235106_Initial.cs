using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vm.Pm.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    DocumentNumber = table.Column<string>(type: "varchar(20)", nullable: true),
                    FEIEIN = table.Column<string>(type: "varchar(20)", nullable: true),
                    LegalName = table.Column<string>(type: "varchar(200)", nullable: false),
                    TradeName = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collaborators",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collaborators_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    CollaboratorId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contacts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    ContactId = table.Column<Guid>(nullable: false),
                    CollaboratorId = table.Column<Guid>(nullable: false),
                    PublicPlace = table.Column<string>(type: "varchar(200)", nullable: false),
                    Apt_Suite_Unit = table.Column<string>(type: "varchar(20)", nullable: true),
                    City = table.Column<string>(type: "varchar(20)", nullable: false),
                    State_Province = table.Column<string>(type: "varchar(20)", nullable: false),
                    ZipPostalCode = table.Column<string>(type: "varchar(8)", nullable: false),
                    TypeAddress = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adresses_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adresses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adresses_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    ContactId = table.Column<Guid>(nullable: true),
                    CollaboratorId = table.Column<Guid>(nullable: true),
                    Number = table.Column<string>(type: "varchar(11)", nullable: false),
                    TypePhone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Phones_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Phones_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_CollaboratorId",
                table: "Adresses",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_CompanyId",
                table: "Adresses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_ContactId",
                table: "Adresses",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_CompanyId",
                table: "Collaborators",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CollaboratorId",
                table: "Contacts",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CompanyId",
                table: "Contacts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_CollaboratorId",
                table: "Phones",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_CompanyId",
                table: "Phones",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_ContactId",
                table: "Phones",
                column: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Collaborators");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
