using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class databasechangeregardingthepropertytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyAgent_Employees_AgentID",
                table: "PropertyAgent");

            migrationBuilder.DropForeignKey(
                name: "FK_ResidentialListings_Employees_AgentID",
                table: "ResidentialListings");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Funds");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_TableName",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "StrSubsidiaries",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "StrSubsidiariesNames",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "TenantClassification",
                table: "Companies",
                newName: "TenantClassificationID");

            migrationBuilder.RenameColumn(
                name: "Subsidiary",
                table: "Companies",
                newName: "SubsidiaryID");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Companies",
                newName: "SectorID");

            migrationBuilder.RenameColumn(
                name: "Sector",
                table: "Companies",
                newName: "OriginID");

            migrationBuilder.RenameColumn(
                name: "Origin",
                table: "Companies",
                newName: "IndustryID");

            migrationBuilder.RenameColumn(
                name: "IndustryGroup",
                table: "Companies",
                newName: "IndustryGroupID");

            migrationBuilder.RenameColumn(
                name: "Industry",
                table: "Companies",
                newName: "CompanyStatusID");

            migrationBuilder.RenameColumn(
                name: "Business",
                table: "Companies",
                newName: "BusinessID");

            migrationBuilder.AddColumn<int>(
                name: "AvailabilityID",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Availability",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availability", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeadSourceID = table.Column<int>(type: "int", nullable: false),
                    LeadStatusID = table.Column<int>(type: "int", nullable: false),
                    DealID = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeadCategoryID = table.Column<int>(type: "int", nullable: false),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesiredLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerID = table.Column<int>(type: "int", nullable: false),
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    AccessLevelID = table.Column<int>(type: "int", nullable: false),
                    Roles = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ValuationApproach",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuationApproach", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ValuationType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuationType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Appraisals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    AppraisalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValuationTypeID = table.Column<int>(type: "int", nullable: false),
                    ValuationApproachID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appraisals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Appraisals_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appraisals_ValuationApproach_ValuationApproachID",
                        column: x => x.ValuationApproachID,
                        principalTable: "ValuationApproach",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appraisals_ValuationType_ValuationTypeID",
                        column: x => x.ValuationTypeID,
                        principalTable: "ValuationType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_AvailabilityID",
                table: "Units",
                column: "AvailabilityID");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_PropertyID",
                table: "Appraisals",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_ValuationApproachID",
                table: "Appraisals",
                column: "ValuationApproachID");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_ValuationTypeID",
                table: "Appraisals",
                column: "ValuationTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyAgent_Users_AgentID",
                table: "PropertyAgent",
                column: "AgentID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResidentialListings_Users_AgentID",
                table: "ResidentialListings",
                column: "AgentID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Availability_AvailabilityID",
                table: "Units",
                column: "AvailabilityID",
                principalTable: "Availability",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyAgent_Users_AgentID",
                table: "PropertyAgent");

            migrationBuilder.DropForeignKey(
                name: "FK_ResidentialListings_Users_AgentID",
                table: "ResidentialListings");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Availability_AvailabilityID",
                table: "Units");

            migrationBuilder.DropTable(
                name: "Appraisals");

            migrationBuilder.DropTable(
                name: "Availability");

            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ValuationApproach");

            migrationBuilder.DropTable(
                name: "ValuationType");

            migrationBuilder.DropIndex(
                name: "IX_Units_AvailabilityID",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "AvailabilityID",
                table: "Units");

            migrationBuilder.RenameColumn(
                name: "TenantClassificationID",
                table: "Companies",
                newName: "TenantClassification");

            migrationBuilder.RenameColumn(
                name: "SubsidiaryID",
                table: "Companies",
                newName: "Subsidiary");

            migrationBuilder.RenameColumn(
                name: "SectorID",
                table: "Companies",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "OriginID",
                table: "Companies",
                newName: "Sector");

            migrationBuilder.RenameColumn(
                name: "IndustryID",
                table: "Companies",
                newName: "Origin");

            migrationBuilder.RenameColumn(
                name: "IndustryGroupID",
                table: "Companies",
                newName: "IndustryGroup");

            migrationBuilder.RenameColumn(
                name: "CompanyStatusID",
                table: "Companies",
                newName: "Industry");

            migrationBuilder.RenameColumn(
                name: "BusinessID",
                table: "Companies",
                newName: "Business");

            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "Properties",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StrSubsidiaries",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrSubsidiariesNames",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "AuditLogs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DomainAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Funds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsUpcoming = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetRaise = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_TableName",
                table: "AuditLogs",
                column: "TableName");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyAgent_Employees_AgentID",
                table: "PropertyAgent",
                column: "AgentID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResidentialListings_Employees_AgentID",
                table: "ResidentialListings",
                column: "AgentID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
