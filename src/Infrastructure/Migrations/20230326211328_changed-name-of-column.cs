using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class changednameofcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferOptions_Contacts_AgentId",
                table: "OfferOptions");

            migrationBuilder.RenameColumn(
                name: "AgentId",
                table: "OfferOptions",
                newName: "AgentID");

            migrationBuilder.RenameIndex(
                name: "IX_OfferOptions_AgentId",
                table: "OfferOptions",
                newName: "IX_OfferOptions_AgentID");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferOptions_Contacts_AgentID",
                table: "OfferOptions",
                column: "AgentID",
                principalTable: "Contacts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferOptions_Contacts_AgentID",
                table: "OfferOptions");

            migrationBuilder.RenameColumn(
                name: "AgentID",
                table: "OfferOptions",
                newName: "AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferOptions_AgentID",
                table: "OfferOptions",
                newName: "IX_OfferOptions_AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferOptions_Contacts_AgentId",
                table: "OfferOptions",
                column: "AgentId",
                principalTable: "Contacts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
