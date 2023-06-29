using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class renameddocuemntcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageType",
                table: "PropertyDocuments",
                newName: "DocumentType");

            migrationBuilder.AddColumn<int>(
                name: "SourceType",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "PropertyDocuments",
                newName: "ImageType");
        }
    }
}
