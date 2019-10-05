using Microsoft.EntityFrameworkCore.Migrations;

namespace Vm.Pm.Data.Migrations
{
    public partial class Address_TypeAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Adresses");

            migrationBuilder.AddColumn<int>(
                name: "TypeAddress",
                table: "Adresses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeAddress",
                table: "Adresses");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Adresses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
