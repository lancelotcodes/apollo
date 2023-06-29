using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class addedoffergenerationpage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfferOptions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactID = table.Column<int>(type: "int", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyTypeID = table.Column<int>(type: "int", nullable: true),
                    ListingTypeID = table.Column<int>(type: "int", nullable: true),
                    PEZA = table.Column<int>(type: "int", nullable: true),
                    OperatingHours = table.Column<bool>(type: "bit", nullable: false),
                    HandOverConditionID = table.Column<int>(type: "int", nullable: true),
                    MinSize = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxSize = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Cities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provinces = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubMarkets = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegacyID = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OfferOptions_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferOptions_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfferOptions_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfferOptions_HandOverConditions_HandOverConditionID",
                        column: x => x.HandOverConditionID,
                        principalTable: "HandOverConditions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfferOptions_ListingTypes_ListingTypeID",
                        column: x => x.ListingTypeID,
                        principalTable: "ListingTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfferOptions_PropertyTypes_PropertyTypeID",
                        column: x => x.PropertyTypeID,
                        principalTable: "PropertyTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferOptions_CompanyID",
                table: "OfferOptions",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferOptions_ContactID",
                table: "OfferOptions",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferOptions_HandOverConditionID",
                table: "OfferOptions",
                column: "HandOverConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferOptions_ListingTypeID",
                table: "OfferOptions",
                column: "ListingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferOptions_PropertyTypeID",
                table: "OfferOptions",
                column: "PropertyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferOptions_UserID",
                table: "OfferOptions",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferOptions");
        }
    }
}
