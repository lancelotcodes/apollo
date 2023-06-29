using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class addedagentidintofferoption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "OfferOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OfferOptions_AgentId",
                table: "OfferOptions",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferOptions_Contacts_AgentId",
                table: "OfferOptions",
                column: "AgentId",
                principalTable: "Contacts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferOptions_Contacts_AgentId",
                table: "OfferOptions");

            migrationBuilder.DropIndex(
                name: "IX_OfferOptions_AgentId",
                table: "OfferOptions");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "OfferOptions");
        }
    }
}
