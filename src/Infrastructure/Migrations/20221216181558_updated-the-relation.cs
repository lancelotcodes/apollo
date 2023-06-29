using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class updatedtherelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Mandates_PropertyID",
                table: "Mandates");

            migrationBuilder.CreateIndex(
                name: "IX_Mandates_PropertyID",
                table: "Mandates",
                column: "PropertyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Mandates_PropertyID",
                table: "Mandates");

            migrationBuilder.CreateIndex(
                name: "IX_Mandates_PropertyID",
                table: "Mandates",
                column: "PropertyID",
                unique: true);
        }
    }
}
