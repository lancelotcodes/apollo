using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class addedthumbnailjoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ThumbNailId",
                table: "Documents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ThumbNailId",
                table: "Documents",
                column: "ThumbNailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Documents_ThumbNailId",
                table: "Documents",
                column: "ThumbNailId",
                principalTable: "Documents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Documents_ThumbNailId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ThumbNailId",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "ThumbNailId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
