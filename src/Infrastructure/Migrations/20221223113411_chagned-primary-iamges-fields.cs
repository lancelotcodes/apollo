using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class chagnedprimaryiamgesfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainFloorPlanImage",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "MainImage",
                table: "Properties");

            migrationBuilder.AddColumn<int>(
                name: "LegacyID",
                table: "Units",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegacyID",
                table: "ResidentialListings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "PropertyDocuments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LegacyID",
                table: "PropertyDocuments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegacyID",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegacyID",
                table: "Leads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegacyID",
                table: "Investments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegacyID",
                table: "Floors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegacyID",
                table: "Contracts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegacyID",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegacyID",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegacyID",
                table: "Buildings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegacyID",
                table: "Addresses",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LegacyID",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "LegacyID",
                table: "ResidentialListings");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "PropertyDocuments");

            migrationBuilder.DropColumn(
                name: "LegacyID",
                table: "PropertyDocuments");

            migrationBuilder.DropColumn(
                name: "LegacyID",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "LegacyID",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "LegacyID",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "LegacyID",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "LegacyID",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LegacyID",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LegacyID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LegacyID",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "LegacyID",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "MainFloorPlanImage",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainImage",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
