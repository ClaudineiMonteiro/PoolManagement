using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vm.Pm.App.Data.Migrations
{
    public partial class InitialIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyViewModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DocumentNumber = table.Column<string>(maxLength: 20, nullable: false),
                    FEIEIN = table.Column<string>(maxLength: 20, nullable: false),
                    LegalName = table.Column<string>(maxLength: 200, nullable: false),
                    TradeName = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactViewModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactViewModel_CompanyViewModel_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "CompanyViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressViewModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PublicPlace = table.Column<string>(maxLength: 200, nullable: false),
                    Apt_Suite_Unit = table.Column<string>(nullable: true),
                    City = table.Column<string>(maxLength: 20, nullable: false),
                    State_Province = table.Column<string>(maxLength: 20, nullable: false),
                    ZipPostalCode = table.Column<string>(maxLength: 8, nullable: false),
                    TypeAddress = table.Column<int>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    ContactId = table.Column<Guid>(nullable: false),
                    CompanyViewModelId = table.Column<Guid>(nullable: true),
                    ContactViewModelId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressViewModel_CompanyViewModel_CompanyViewModelId",
                        column: x => x.CompanyViewModelId,
                        principalTable: "CompanyViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressViewModel_ContactViewModel_ContactViewModelId",
                        column: x => x.ContactViewModelId,
                        principalTable: "ContactViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneViewModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<string>(maxLength: 10, nullable: false),
                    TypePhoneId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    ContactId = table.Column<Guid>(nullable: true),
                    CompanyViewModelId = table.Column<Guid>(nullable: true),
                    ContactViewModelId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneViewModel_CompanyViewModel_CompanyViewModelId",
                        column: x => x.CompanyViewModelId,
                        principalTable: "CompanyViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhoneViewModel_ContactViewModel_ContactViewModelId",
                        column: x => x.ContactViewModelId,
                        principalTable: "ContactViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypePhone",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    PhoneViewModelId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypePhone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypePhone_PhoneViewModel_PhoneViewModelId",
                        column: x => x.PhoneViewModelId,
                        principalTable: "PhoneViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressViewModel_CompanyViewModelId",
                table: "AddressViewModel",
                column: "CompanyViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressViewModel_ContactViewModelId",
                table: "AddressViewModel",
                column: "ContactViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactViewModel_CompanyId",
                table: "ContactViewModel",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneViewModel_CompanyViewModelId",
                table: "PhoneViewModel",
                column: "CompanyViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneViewModel_ContactViewModelId",
                table: "PhoneViewModel",
                column: "ContactViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TypePhone_PhoneViewModelId",
                table: "TypePhone",
                column: "PhoneViewModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressViewModel");

            migrationBuilder.DropTable(
                name: "TypePhone");

            migrationBuilder.DropTable(
                name: "PhoneViewModel");

            migrationBuilder.DropTable(
                name: "ContactViewModel");

            migrationBuilder.DropTable(
                name: "CompanyViewModel");
        }
    }
}
