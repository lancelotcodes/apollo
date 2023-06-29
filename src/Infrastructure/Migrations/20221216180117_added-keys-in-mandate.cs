using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class addedkeysinmandate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDocuments_Documents_DocumentID",
                table: "PropertyDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Mandates_PropertyID",
                table: "Mandates");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Documents_DocumentKey",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_DocumentKey",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Mandates");

            migrationBuilder.DropColumn(
                name: "ThumbNailUrl",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "Documents",
                newName: "ThumbNailId");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentID",
                table: "PropertyDocuments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Mandates",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttachmentId",
                table: "Mandates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentKey",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Mandates_AttachmentId",
                table: "Mandates",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Mandates_PropertyID",
                table: "Mandates",
                column: "PropertyID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentKey",
                table: "Documents",
                column: "DocumentKey",
                unique: true,
                filter: "[DocumentKey] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Mandates_Documents_AttachmentId",
                table: "Mandates",
                column: "AttachmentId",
                principalTable: "Documents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDocuments_Documents_DocumentID",
                table: "PropertyDocuments",
                column: "DocumentID",
                principalTable: "Documents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mandates_Documents_AttachmentId",
                table: "Mandates");

            migrationBuilder.DropForeignKey(
                name: "FK_Mandates_Properties_PropertyID1",
                table: "Mandates");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDocuments_Documents_DocumentID",
                table: "PropertyDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Mandates_AttachmentId",
                table: "Mandates");

            migrationBuilder.DropIndex(
                name: "IX_Mandates_PropertyID",
                table: "Mandates");

            migrationBuilder.DropIndex(
                name: "IX_Mandates_PropertyID1",
                table: "Mandates");

            migrationBuilder.DropIndex(
                name: "IX_Documents_DocumentKey",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Mandates");

            migrationBuilder.RenameColumn(
                name: "ThumbNailId",
                table: "Documents",
                newName: "DocumentType");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentID",
                table: "PropertyDocuments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Mandates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "Mandates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentKey",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbNailUrl",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Documents_DocumentKey",
                table: "Documents",
                column: "DocumentKey");

            migrationBuilder.CreateIndex(
                name: "IX_Mandates_PropertyID",
                table: "Mandates",
                column: "PropertyID");

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
    }
}
