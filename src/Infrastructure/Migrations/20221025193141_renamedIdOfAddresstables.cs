using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class renamedIdOfAddresstables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AuditLogs",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Addresses",
                newName: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AuditLogs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Addresses",
                newName: "Id");
        }
    }
}
