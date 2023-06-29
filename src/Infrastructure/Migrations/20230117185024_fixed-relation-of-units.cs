using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class fixedrelationofunits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_HandOverConditions_HandOverConditionID1",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_HandOverConditionID1",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_PropertyID",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "HandOverConditionID1",
                table: "Units");

            migrationBuilder.AlterColumn<int>(
                name: "HandOverConditionID",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_PropertyID",
                table: "Units",
                column: "PropertyID",
                unique: true,
                filter: "[PropertyID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_HandOverConditions_HandOverConditionID",
                table: "Units",
                column: "HandOverConditionID",
                principalTable: "HandOverConditions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_HandOverConditions_HandOverConditionID",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_PropertyID",
                table: "Units");

            migrationBuilder.AlterColumn<string>(
                name: "HandOverConditionID",
                table: "Units",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "HandOverConditionID1",
                table: "Units",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_HandOverConditionID1",
                table: "Units",
                column: "HandOverConditionID1");

            migrationBuilder.CreateIndex(
                name: "IX_Units_PropertyID",
                table: "Units",
                column: "PropertyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_HandOverConditions_HandOverConditionID1",
                table: "Units",
                column: "HandOverConditionID1",
                principalTable: "HandOverConditions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
