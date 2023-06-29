using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class BuildingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PEZAStatusID",
                table: "Buildings");

            migrationBuilder.AlterColumn<int>(
                name: "PEZA",
                table: "Buildings",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "PEZA",
                table: "Buildings",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PEZAStatusID",
                table: "Buildings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
