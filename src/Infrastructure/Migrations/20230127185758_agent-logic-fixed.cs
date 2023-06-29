using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class agentlogicfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyAgent_AspNetUsers_AgentID",
                table: "PropertyAgent");

            migrationBuilder.DropForeignKey(
                name: "FK_ResidentialListings_AspNetUsers_AgentID",
                table: "ResidentialListings");

            migrationBuilder.DropIndex(
                name: "IX_ResidentialListings_AgentID",
                table: "ResidentialListings");

            migrationBuilder.AlterColumn<int>(
                name: "AgentID",
                table: "ResidentialListings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgentID",
                table: "PropertyAgent",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialListings_AgentID",
                table: "ResidentialListings",
                column: "AgentID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyAgent_Contacts_AgentID",
                table: "PropertyAgent",
                column: "AgentID",
                principalTable: "Contacts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResidentialListings_Contacts_AgentID",
                table: "ResidentialListings",
                column: "AgentID",
                principalTable: "Contacts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyAgent_Contacts_AgentID",
                table: "PropertyAgent");

            migrationBuilder.DropForeignKey(
                name: "FK_ResidentialListings_Contacts_AgentID",
                table: "ResidentialListings");

            migrationBuilder.DropIndex(
                name: "IX_ResidentialListings_AgentID",
                table: "ResidentialListings");

            migrationBuilder.AlterColumn<string>(
                name: "AgentID",
                table: "ResidentialListings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AgentID",
                table: "PropertyAgent",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialListings_AgentID",
                table: "ResidentialListings",
                column: "AgentID",
                unique: true,
                filter: "[AgentID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyAgent_AspNetUsers_AgentID",
                table: "PropertyAgent",
                column: "AgentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResidentialListings_AspNetUsers_AgentID",
                table: "ResidentialListings",
                column: "AgentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
