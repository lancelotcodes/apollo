using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class changedtheSEOIDfieldofaddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_SEO_SEOID",
                table: "Properties");

            migrationBuilder.AlterColumn<int>(
                name: "SEOID",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_SEO_SEOID",
                table: "Properties",
                column: "SEOID",
                principalTable: "SEO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_SEO_SEOID",
                table: "Properties");

            migrationBuilder.AlterColumn<int>(
                name: "SEOID",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_SEO_SEOID",
                table: "Properties",
                column: "SEOID",
                principalTable: "SEO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
