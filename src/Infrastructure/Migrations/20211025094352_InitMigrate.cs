using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace apollo.Infrastructure.Migrations
{
    public partial class InitMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Sector = table.Column<int>(type: "int", nullable: false),
                    IndustryGroup = table.Column<int>(type: "int", nullable: false),
                    Industry = table.Column<int>(type: "int", nullable: false),
                    Business = table.Column<int>(type: "int", nullable: false),
                    Origin = table.Column<int>(type: "int", nullable: false),
                    TenantClassification = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HubSpotID = table.Column<int>(type: "int", nullable: false),
                    CompanyLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditRatings = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subsidiary = table.Column<int>(type: "int", nullable: false),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StrSubsidiaries = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StrSubsidiariesNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PolygonPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DomainAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUpcoming = table.Column<bool>(type: "bit", nullable: false),
                    TargetRaise = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HandOverConditions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandOverConditions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ListingTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OwnershipTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnershipTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SEO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    PublishType = table.Column<int>(type: "int", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    FeaturedWeight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SubTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TenantClassifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantClassifications", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UnitStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HubSpotID = table.Column<int>(type: "int", nullable: false),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contacts_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PolygonPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PolygonPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.ID);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PropertyTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Grades_PropertyTypes_PropertyTypeID",
                        column: x => x.PropertyTypeID,
                        principalTable: "PropertyTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PolygonPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Provinces_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PolygonPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MicroDistricts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PolygonPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicroDistricts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MicroDistricts_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubMarkets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PolygonPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMarkets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubMarkets_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    SubMarketID = table.Column<int>(type: "int", nullable: true),
                    MicroDistrictID = table.Column<int>(type: "int", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressTag = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    PolygonPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_MicroDistricts_MicroDistrictID",
                        column: x => x.MicroDistrictID,
                        principalTable: "MicroDistricts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_SubMarkets_SubMarketID",
                        column: x => x.SubMarketID,
                        principalTable: "SubMarkets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MasterProjectID = table.Column<int>(type: "int", nullable: true),
                    MasterPropertyID = table.Column<int>(type: "int", nullable: true),
                    ResidentialUnitID = table.Column<int>(type: "int", nullable: true),
                    ListingID = table.Column<int>(type: "int", nullable: true),
                    PropertyTypeID = table.Column<int>(type: "int", nullable: false),
                    SubTypeID = table.Column<int>(type: "int", nullable: true),
                    GradeID = table.Column<int>(type: "int", nullable: true),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    ContactID = table.Column<int>(type: "int", nullable: false),
                    OwnerID = table.Column<int>(type: "int", nullable: false),
                    OwnerCompanyID = table.Column<int>(type: "int", nullable: false),
                    MainImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainFloorPlanImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsExclusive = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    SEOID = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Properties_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Companies_OwnerCompanyID",
                        column: x => x.OwnerCompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Properties_Contacts_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Contacts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Properties_Grades_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grades",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_Properties_MasterProjectID",
                        column: x => x.MasterProjectID,
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_Properties_MasterPropertyID",
                        column: x => x.MasterPropertyID,
                        principalTable: "Properties",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Properties_Properties_ResidentialUnitID",
                        column: x => x.ResidentialUnitID,
                        principalTable: "Properties",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Properties_PropertyTypes_PropertyTypeID",
                        column: x => x.PropertyTypeID,
                        principalTable: "PropertyTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_SEO_SEOID",
                        column: x => x.SEOID,
                        principalTable: "SEO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_SubTypes_SubTypeID",
                        column: x => x.SubTypeID,
                        principalTable: "SubTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    DateBuilt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    YearBuilt = table.Column<int>(type: "int", nullable: false),
                    PEZAStatusID = table.Column<int>(type: "int", nullable: false),
                    PEZA = table.Column<bool>(type: "bit", nullable: false),
                    OperatingHours = table.Column<bool>(type: "bit", nullable: false),
                    LEED = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeasingContactID = table.Column<int>(type: "int", nullable: false),
                    DeveloperID = table.Column<int>(type: "int", nullable: true),
                    PropertyManagementID = table.Column<int>(type: "int", nullable: false),
                    OwnershipTypeID = table.Column<int>(type: "int", nullable: true),
                    ProjectStatusID = table.Column<int>(type: "int", nullable: false),
                    TurnOverDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantMix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossBuildingSize = table.Column<double>(type: "float", nullable: false),
                    GrossLeasableSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypicalFloorPlateSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalFloors = table.Column<int>(type: "int", nullable: false),
                    TotalUnits = table.Column<int>(type: "int", nullable: false),
                    EfficiencyRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CeilingHeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumLeaseTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Elevators = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Power = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ACSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telcos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amenities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebPage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DensityRatio = table.Column<double>(type: "float", nullable: false),
                    ParkingElevator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassengerElevator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceElevator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Buildings_Companies_DeveloperID",
                        column: x => x.DeveloperID,
                        principalTable: "Companies",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Buildings_Companies_LeasingContactID",
                        column: x => x.LeasingContactID,
                        principalTable: "Companies",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Buildings_Companies_PropertyManagementID",
                        column: x => x.PropertyManagementID,
                        principalTable: "Companies",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Buildings_OwnershipTypes_OwnershipTypeID",
                        column: x => x.OwnershipTypeID,
                        principalTable: "OwnershipTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buildings_ProjectStatuses_ProjectStatusID",
                        column: x => x.ProjectStatusID,
                        principalTable: "ProjectStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Buildings_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    ContactID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantClassificationID = table.Column<int>(type: "int", nullable: false),
                    EstimatedArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeaseTerm = table.Column<int>(type: "int", nullable: false),
                    BrokerID = table.Column<int>(type: "int", nullable: false),
                    BrokerCompanyID = table.Column<int>(type: "int", nullable: false),
                    IsHistorical = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contracts_Companies_BrokerCompanyID",
                        column: x => x.BrokerCompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Contracts_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Contracts_Contacts_BrokerID",
                        column: x => x.BrokerID,
                        principalTable: "Contacts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Contracts_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Contracts_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_TenantClassifications_TenantClassificationID",
                        column: x => x.TenantClassificationID,
                        principalTable: "TenantClassifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndustrialListings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    ProviderClass = table.Column<int>(type: "int", nullable: false),
                    ZoningClassification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccupancyClassification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalGrossLeasableSizeSQM = table.Column<double>(type: "float", nullable: false),
                    TGLSOpenArea = table.Column<double>(type: "float", nullable: false),
                    TGLSCoveredArea = table.Column<double>(type: "float", nullable: false),
                    Accommodate10W = table.Column<bool>(type: "bit", nullable: false),
                    Total10WSlots = table.Column<int>(type: "int", nullable: false),
                    StructureType = table.Column<int>(type: "int", nullable: false),
                    WithCargoLifts = table.Column<bool>(type: "bit", nullable: false),
                    WithOfficeComponents = table.Column<bool>(type: "bit", nullable: false),
                    TotalBaysDocks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeBayDock = table.Column<int>(type: "int", nullable: false),
                    PowerSupplyCapacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FloorLoadingCapacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaterSupply = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaterSewageTreatment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VentilationSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternetServiceProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustyTypeAllowed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnyRestriction = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustrialListings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IndustrialListings_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Investments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    LotArea = table.Column<double>(type: "float", nullable: false),
                    FloorArea = table.Column<double>(type: "float", nullable: false),
                    LeasableArea = table.Column<double>(type: "float", nullable: false),
                    TypicalFloorPlate = table.Column<double>(type: "float", nullable: false),
                    CommonArea = table.Column<double>(type: "float", nullable: false),
                    Frontage = table.Column<double>(type: "float", nullable: false),
                    CeilingHeight = table.Column<double>(type: "float", nullable: false),
                    FloorAreaRatio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalFloors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalRooms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalElevators = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalParkingSlots = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EfficiencyRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandoverCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxDeclarationClassification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearCompleted = table.Column<float>(type: "real", nullable: false),
                    BuildingClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PEZAStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackupPower = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirConditioningSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelecommunicationProviders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DensityRatio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amenities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Availability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossSellingPrice = table.Column<double>(type: "float", nullable: false),
                    NetSellingPrice = table.Column<double>(type: "float", nullable: false),
                    BaseGrossSellingPrice = table.Column<double>(type: "float", nullable: false),
                    BaseNetSellingPrice = table.Column<double>(type: "float", nullable: false),
                    BaseRent = table.Column<double>(type: "float", nullable: false),
                    AssociationDues = table.Column<double>(type: "float", nullable: false),
                    ParkingSlotLeaseRate = table.Column<double>(type: "float", nullable: false),
                    AirConditioningCharges = table.Column<double>(type: "float", nullable: false),
                    LeaseTermYears = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualEscalation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commission = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Investments_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mandates",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mandates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Mandates_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyAgent",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentID = table.Column<int>(type: "int", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    AgentType = table.Column<int>(type: "int", nullable: false),
                    IsVisibleOnWeb = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyAgent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyAgent_Employees_AgentID",
                        column: x => x.AgentID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyAgent_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsThumbNail = table.Column<bool>(type: "bit", nullable: false),
                    IsFloorPlan = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyImages_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResidentialListings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    ListingTypeID = table.Column<int>(type: "int", nullable: false),
                    FloorArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LotArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bathroom = table.Column<int>(type: "int", nullable: false),
                    Bedroom = table.Column<int>(type: "int", nullable: false),
                    ParkingSlot = table.Column<int>(type: "int", nullable: false),
                    HandOverCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandOverDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AgentID = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialListings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ResidentialListings_Employees_AgentID",
                        column: x => x.AgentID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResidentialListings_ListingTypes_ListingTypeID",
                        column: x => x.ListingTypeID,
                        principalTable: "ListingTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResidentialListings_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    FloorPlateSize = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Floors_Buildings_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Buildings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImageVersions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: false),
                    PropertyImageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImageVersions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyImageVersions_PropertyImages_PropertyImageID",
                        column: x => x.PropertyImageID,
                        principalTable: "PropertyImages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorID = table.Column<int>(type: "int", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: true),
                    UnitNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitStatusID = table.Column<int>(type: "int", nullable: false),
                    LeaseFloorArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ListingTypeID = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CUSA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HandOverConditionID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HandOverConditionID1 = table.Column<int>(type: "int", nullable: true),
                    ACCharges = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ACExtensionCharges = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EscalationRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumLeaseTerm = table.Column<int>(type: "int", nullable: false),
                    ParkingRent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HandOverDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Units_Floors_FloorID",
                        column: x => x.FloorID,
                        principalTable: "Floors",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Units_HandOverConditions_HandOverConditionID1",
                        column: x => x.HandOverConditionID1,
                        principalTable: "HandOverConditions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Units_ListingTypes_ListingTypeID",
                        column: x => x.ListingTypeID,
                        principalTable: "ListingTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Units_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Units_UnitStatuses_UnitStatusID",
                        column: x => x.UnitStatusID,
                        principalTable: "UnitStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityID",
                table: "Addresses",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_MicroDistrictID",
                table: "Addresses",
                column: "MicroDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_SubMarketID",
                table: "Addresses",
                column: "SubMarketID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_TableName",
                table: "AuditLogs",
                column: "TableName");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_DeveloperID",
                table: "Buildings",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_LeasingContactID",
                table: "Buildings",
                column: "LeasingContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_OwnershipTypeID",
                table: "Buildings",
                column: "OwnershipTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ProjectStatusID",
                table: "Buildings",
                column: "ProjectStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_PropertyID",
                table: "Buildings",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_PropertyManagementID",
                table: "Buildings",
                column: "PropertyManagementID");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceID",
                table: "Cities",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CompanyID",
                table: "Contacts",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_BrokerCompanyID",
                table: "Contracts",
                column: "BrokerCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_BrokerID",
                table: "Contracts",
                column: "BrokerID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CompanyID",
                table: "Contracts",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContactID",
                table: "Contracts",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PropertyID",
                table: "Contracts",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TenantClassificationID",
                table: "Contracts",
                column: "TenantClassificationID");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_BuildingID",
                table: "Floors",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_PropertyTypeID",
                table: "Grades",
                column: "PropertyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_IndustrialListings_PropertyID",
                table: "IndustrialListings",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Investments_PropertyID",
                table: "Investments",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Mandates_PropertyID",
                table: "Mandates",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_MicroDistricts_CityID",
                table: "MicroDistricts",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AddressID",
                table: "Properties",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ContactID",
                table: "Properties",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_GradeID",
                table: "Properties",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ListingID",
                table: "Properties",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_MasterProjectID",
                table: "Properties",
                column: "MasterProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_MasterPropertyID",
                table: "Properties",
                column: "MasterPropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerCompanyID",
                table: "Properties",
                column: "OwnerCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerID",
                table: "Properties",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeID",
                table: "Properties",
                column: "PropertyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ResidentialUnitID",
                table: "Properties",
                column: "ResidentialUnitID",
                unique: true,
                filter: "[ResidentialUnitID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_SEOID",
                table: "Properties",
                column: "SEOID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_SubTypeID",
                table: "Properties",
                column: "SubTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAgent_AgentID",
                table: "PropertyAgent",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAgent_PropertyID",
                table: "PropertyAgent",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_PropertyID",
                table: "PropertyImages",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImageVersions_PropertyImageID",
                table: "PropertyImageVersions",
                column: "PropertyImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_RegionID",
                table: "Provinces",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryID",
                table: "Regions",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialListings_AgentID",
                table: "ResidentialListings",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialListings_ListingTypeID",
                table: "ResidentialListings",
                column: "ListingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialListings_PropertyID",
                table: "ResidentialListings",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryID",
                table: "States",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_SubMarkets_CityID",
                table: "SubMarkets",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Units_FloorID",
                table: "Units",
                column: "FloorID");

            migrationBuilder.CreateIndex(
                name: "IX_Units_HandOverConditionID",
                table: "Units",
                column: "HandOverConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_Units_HandOverConditionID1",
                table: "Units",
                column: "HandOverConditionID1");

            migrationBuilder.CreateIndex(
                name: "IX_Units_ListingTypeID",
                table: "Units",
                column: "ListingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Units_PropertyID",
                table: "Units",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitStatusID",
                table: "Units",
                column: "UnitStatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Funds");

            migrationBuilder.DropTable(
                name: "IndustrialListings");

            migrationBuilder.DropTable(
                name: "Investments");

            migrationBuilder.DropTable(
                name: "Mandates");

            migrationBuilder.DropTable(
                name: "PropertyAgent");

            migrationBuilder.DropTable(
                name: "PropertyImageVersions");

            migrationBuilder.DropTable(
                name: "ResidentialListings");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "TenantClassifications");

            migrationBuilder.DropTable(
                name: "PropertyImages");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "HandOverConditions");

            migrationBuilder.DropTable(
                name: "ListingTypes");

            migrationBuilder.DropTable(
                name: "UnitStatuses");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "OwnershipTypes");

            migrationBuilder.DropTable(
                name: "ProjectStatuses");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "SEO");

            migrationBuilder.DropTable(
                name: "SubTypes");

            migrationBuilder.DropTable(
                name: "MicroDistricts");

            migrationBuilder.DropTable(
                name: "SubMarkets");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
