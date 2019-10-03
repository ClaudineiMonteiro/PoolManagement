using Microsoft.EntityFrameworkCore.Migrations;

namespace Vm.Pm.Data.Migrations
{
    public partial class Company_Rename_FEIEIN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FEI_EIN",
                table: "Companies",
                newName: "FEIEIN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FEIEIN",
                table: "Companies",
                newName: "FEI_EIN");
        }
    }
}
