using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class changedforeignkeyrelationfordocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDocuments_Documents_DocumentID",
                table: "PropertyDocuments");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentID",
                table: "PropertyDocuments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentKey",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Documents_DocumentKey",
                table: "Documents",
                column: "DocumentKey");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentKey",
                table: "Documents",
                column: "DocumentKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDocuments_Documents_DocumentID",
                table: "PropertyDocuments",
                column: "DocumentID",
                principalTable: "Documents",
                principalColumn: "DocumentKey",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDocuments_Documents_DocumentID",
                table: "PropertyDocuments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Documents_DocumentKey",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_DocumentKey",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentID",
                table: "PropertyDocuments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentKey",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDocuments_Documents_DocumentID",
                table: "PropertyDocuments",
                column: "DocumentID",
                principalTable: "Documents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
