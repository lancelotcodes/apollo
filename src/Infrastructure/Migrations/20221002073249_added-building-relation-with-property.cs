using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class addedbuildingrelationwithproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Buildings_PropertyID",
                table: "Buildings");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_PropertyID",
                table: "Buildings",
                column: "PropertyID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Buildings_PropertyID",
                table: "Buildings");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_PropertyID",
                table: "Buildings",
                column: "PropertyID");
        }
    }
}
