using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class ChangeToNullableFKBuildings2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ProjectStatuses_ProjectStatusID",
                table: "Buildings");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectStatusID",
                table: "Buildings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_ProjectStatuses_ProjectStatusID",
                table: "Buildings",
                column: "ProjectStatusID",
                principalTable: "ProjectStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ProjectStatuses_ProjectStatusID",
                table: "Buildings");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectStatusID",
                table: "Buildings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_ProjectStatuses_ProjectStatusID",
                table: "Buildings",
                column: "ProjectStatusID",
                principalTable: "ProjectStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
