using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vm.Pm.Data.Migrations
{
    public partial class Contact_Collaborator_Optional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CollaboratorId",
                table: "Contacts",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CollaboratorId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
