using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class addedsubjectandmessagefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "OfferOptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "OfferOptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "OfferOptions");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "OfferOptions");
        }
    }
}
