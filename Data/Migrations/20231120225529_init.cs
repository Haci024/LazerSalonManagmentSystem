using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BodyShapingPacketCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Packet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainCategoryId = table.Column<int>(type: "int", nullable: true),
                    SessionCount = table.Column<int>(type: "int", nullable: true),
                    SessionDuration = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyShapingPacketCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyShapingPacketCategory_BodyShapingPacketCategory_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "BodyShapingPacketCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<double>(type: "float", nullable: false),
                    Female = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilialName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LazerCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainCategoryId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LazerCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LazerCategories_LazerCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "LazerCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SolariumCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainCategoryId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UsingPeriod = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolariumCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolariumCategories_SolariumCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "SolariumCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForgetPasswordCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BodyShapingMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false),
                    BodyShapingMasterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyShapingMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyShapingMasters_BodyShapingMasters_BodyShapingMasterId",
                        column: x => x.BodyShapingMasterId,
                        principalTable: "BodyShapingMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BodyShapingMasters_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budget_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cosmetologs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cosmetologs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cosmetologs_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LazerMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LazerMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LazerMasters_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolariumAppointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BuyingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingMinute = table.Column<int>(type: "int", nullable: false),
                    MinuteLimit = table.Column<int>(type: "int", nullable: false),
                    UsingMinute = table.Column<int>(type: "int", nullable: false),
                    SolariumCategoriesId = table.Column<int>(type: "int", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolariumAppointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolariumAppointment_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolariumAppointment_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolariumAppointment_SolariumCategories_SolariumCategoriesId",
                        column: x => x.SolariumCategoriesId,
                        principalTable: "SolariumCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutMoney",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutMoney", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutMoney_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutMoney_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCount = table.Column<int>(type: "int", nullable: false),
                    AddingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stock_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BodyshapingAppointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false),
                    BodyshapingMasterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyshapingAppointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyshapingAppointment_BodyShapingMasters_BodyshapingMasterId",
                        column: x => x.BodyshapingMasterId,
                        principalTable: "BodyShapingMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodyshapingAppointment_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodyshapingAppointment_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KassaActionLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastOutMoneyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KassaId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OutMoneyQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KassaActionLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KassaActionLists_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KassaActionLists_Budget_KassaId",
                        column: x => x.KassaId,
                        principalTable: "Budget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CosmetologyAppointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CosmetologId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CosmetologyAppointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CosmetologyAppointment_Cosmetologs_CosmetologId",
                        column: x => x.CosmetologId,
                        principalTable: "Cosmetologs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CosmetologyAppointment_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CosmetologyAppointment_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LazerAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InCompleteStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InCompleteEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LazerMasterId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImplusCount = table.Column<int>(type: "int", nullable: false),
                    IsStart = table.Column<bool>(type: "bit", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceUpdateDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsContiued = table.Column<bool>(type: "bit", nullable: false),
                    NextSessionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartForSecond = table.Column<bool>(type: "bit", nullable: false),
                    EndForSecond = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LazerAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LazerAppointments_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LazerAppointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LazerAppointments_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LazerAppointments_LazerMasters_LazerMasterId",
                        column: x => x.LazerMasterId,
                        principalTable: "LazerMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolariumUsingList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolariumAppointmentId = table.Column<int>(type: "int", nullable: false),
                    UsingMinute = table.Column<int>(type: "int", nullable: false),
                    RemainingMinute = table.Column<int>(type: "int", nullable: false),
                    UsingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolariumUsingList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolariumUsingList_SolariumAppointment_SolariumAppointmentId",
                        column: x => x.SolariumAppointmentId,
                        principalTable: "SolariumAppointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    IncomeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incomes_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incomes_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incomes_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BodyShapingPacketsReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyshapingAppointmentsId = table.Column<int>(type: "int", nullable: false),
                    BodyShapingPacketCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyShapingPacketsReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyShapingPacketsReports_BodyShapingPacketCategory_BodyShapingPacketCategoryId",
                        column: x => x.BodyShapingPacketCategoryId,
                        principalTable: "BodyShapingPacketCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodyShapingPacketsReports_BodyshapingAppointment_BodyshapingAppointmentsId",
                        column: x => x.BodyshapingAppointmentsId,
                        principalTable: "BodyshapingAppointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BodyShapingSessionList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyShapingAppointmentId = table.Column<int>(type: "int", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SessionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyShapingSessionList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyShapingSessionList_BodyshapingAppointment_BodyShapingAppointmentId",
                        column: x => x.BodyShapingAppointmentId,
                        principalTable: "BodyshapingAppointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LazerAppointmentReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LazerCategoryId = table.Column<int>(type: "int", nullable: false),
                    LazerAppointmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LazerAppointmentReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LazerAppointmentReports_LazerAppointments_LazerAppointmentId",
                        column: x => x.LazerAppointmentId,
                        principalTable: "LazerAppointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LazerAppointmentReports_LazerCategories_LazerCategoryId",
                        column: x => x.LazerCategoryId,
                        principalTable: "LazerCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "qwerty12345", null, "Admin", "ADMIN" },
                    { "qwertys123", null, "Supporter", "SUPPORTER" },
                    { "qwertys123567", null, "SuperSupporter", "SUPERSUPPORTER" },
                    { "qwertyu1234", null, "Qeydiyyatçı", "QEYDİYYATÇI" }
                });

            migrationBuilder.InsertData(
                table: "Filials",
                columns: new[] { "Id", "FilialName" },
                values: new object[,]
                {
                    { 1, "Arzum Mini Lazer Studio" },
                    { 2, "Arzum Beauty Studio" },
                    { 3, "Arzum Estetik Studio" },
                    { 4, "İdarə Paneli" }
                });

            migrationBuilder.InsertData(
                table: "LazerCategories",
                columns: new[] { "Id", "MainCategoryId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, null, "Lazer Mini Qadınlar üçün", 0m },
                    { 2, null, "Lazer Mini Kişilər üçün", 0m },
                    { 3, null, "Lazer Beauty Qadınlar üçün", 0m },
                    { 4, null, "Lazer Beauty Kişilər  üçün", 0m },
                    { 5, null, "Lazer Qadınlar üçün Estetik Salon", 0m }
                });

            migrationBuilder.InsertData(
                table: "SolariumCategories",
                columns: new[] { "Id", "MainCategoryId", "Minute", "Name", "Price", "UsingPeriod" },
                values: new object[,]
                {
                    { 1, null, 0, "Günlük", 0m, null },
                    { 2, null, 0, "Aylıq", 0m, null }
                });

            migrationBuilder.InsertData(
                table: "BodyShapingMasters",
                columns: new[] { "Id", "BodyShapingMasterId", "FilialId", "FullName", "IsDeactive" },
                values: new object[] { 1, null, 2, "Arifə", false });

            migrationBuilder.InsertData(
                table: "Budget",
                columns: new[] { "Id", "Budget", "FilialId" },
                values: new object[,]
                {
                    { 1, 0m, 1 },
                    { 2, 0m, 2 },
                    { 3, 0m, 3 }
                });

            migrationBuilder.InsertData(
                table: "Cosmetologs",
                columns: new[] { "Id", "FilialId", "FullName" },
                values: new object[] { 1, 2, "Nuray" });

            migrationBuilder.InsertData(
                table: "LazerCategories",
                columns: new[] { "Id", "MainCategoryId", "Name", "Price" },
                values: new object[,]
                {
                    { 6, 1, "Bütün bədən", 40m },
                    { 7, 1, "Sadə bədən", 35m },
                    { 8, 1, "Sinə & Qarın", 20m },
                    { 9, 1, "Kürək & Bel", 20m },
                    { 10, 1, "Bütün qol", 15m },
                    { 11, 1, "Qolaltı", 5m },
                    { 12, 1, "Üz", 5m },
                    { 13, 1, "Dodaqüstü", 2m },
                    { 14, 1, "Çənə", 3m },
                    { 15, 1, "Arxayan", 10m },
                    { 16, 1, "Bikini", 10m },
                    { 17, 1, "Bütün ayaq", 20m },
                    { 18, 2, "Yarı bədən", 50m },
                    { 19, 2, "Üz & Boyun & Boğaz", 10m },
                    { 20, 2, "Üz", 5m },
                    { 21, 2, "Boyun", 5m },
                    { 22, 2, "Boğaz", 5m },
                    { 23, 2, "Yanaq", 5m },
                    { 24, 2, "Qulaq", 3m },
                    { 25, 2, "Bütün qol", 25m },
                    { 26, 2, "Çiyin", 10m },
                    { 27, 2, "Qolaltı", 5m },
                    { 28, 2, "Kürək & Bel", 30m },
                    { 29, 2, "Sinə & Qarın", 30m },
                    { 30, 3, "Bütün bədən", 40m },
                    { 31, 3, "Sadə bədən", 35m },
                    { 32, 3, "Sinə & Qarın", 20m },
                    { 33, 3, "Kürək & Bel", 20m },
                    { 34, 4, "Bütün qol", 15m },
                    { 35, 4, "Qolaltı", 5m },
                    { 36, 4, "Üz", 5m },
                    { 37, 4, "Dodaqüstü", 2m },
                    { 38, 4, "Çənə", 3m },
                    { 39, 4, "Arxayan", 10m },
                    { 40, 4, "Bikini", 10m },
                    { 41, 4, "Bütün ayaq", 20m },
                    { 42, 4, "Yarı bədən", 50m },
                    { 43, 5, "Üz & Boyun & Boğaz", 10m },
                    { 44, 5, "Üz", 5m },
                    { 45, 5, "Boyun", 5m },
                    { 46, 5, "Boğaz", 5m },
                    { 47, 5, "Yanaq", 5m },
                    { 48, 5, "Qulaq", 3m },
                    { 49, 5, "Bütün qol", 25m },
                    { 50, 5, "Çiyin", 10m },
                    { 51, 5, "Qolaltı", 5m },
                    { 52, 5, "Kürək & Bel", 30m },
                    { 53, 5, "Sinə & Qarın", 30m }
                });

            migrationBuilder.InsertData(
                table: "LazerMasters",
                columns: new[] { "Id", "FilialId", "FullName" },
                values: new object[,]
                {
                    { 1, 1, "Ellada" },
                    { 2, 1, "Aidə" },
                    { 3, 2, "Ellada" },
                    { 4, 2, "Nuridə" },
                    { 5, 3, "Gülnar" },
                    { 6, 3, "Nəzrin" }
                });

            migrationBuilder.InsertData(
                table: "SolariumCategories",
                columns: new[] { "Id", "MainCategoryId", "Minute", "Name", "Price", "UsingPeriod" },
                values: new object[,]
                {
                    { 3, 1, 5, "MiniPacket", 4m, 1 },
                    { 4, 1, 10, "MediumPacket", 8m, 1 },
                    { 5, 1, 15, "LargePacket", 12m, 1 },
                    { 6, 1, 20, "ExtraLarge", 16m, 30 },
                    { 7, 2, 30, "MiniPacket", 19m, 30 },
                    { 8, 2, 40, "MediumPacket", 29m, 30 },
                    { 9, 2, 50, "LargePacket", 39m, 60 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FilialId",
                table: "AspNetUsers",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BodyshapingAppointment_BodyshapingMasterId",
                table: "BodyshapingAppointment",
                column: "BodyshapingMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyshapingAppointment_CustomerId",
                table: "BodyshapingAppointment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyshapingAppointment_FilialId",
                table: "BodyshapingAppointment",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyShapingMasters_BodyShapingMasterId",
                table: "BodyShapingMasters",
                column: "BodyShapingMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyShapingMasters_FilialId",
                table: "BodyShapingMasters",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyShapingPacketCategory_MainCategoryId",
                table: "BodyShapingPacketCategory",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyShapingPacketsReports_BodyshapingAppointmentsId",
                table: "BodyShapingPacketsReports",
                column: "BodyshapingAppointmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyShapingPacketsReports_BodyShapingPacketCategoryId",
                table: "BodyShapingPacketsReports",
                column: "BodyShapingPacketCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyShapingSessionList_BodyShapingAppointmentId",
                table: "BodyShapingSessionList",
                column: "BodyShapingAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Budget_FilialId",
                table: "Budget",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_Cosmetologs_FilialId",
                table: "Cosmetologs",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_CosmetologyAppointment_CosmetologId",
                table: "CosmetologyAppointment",
                column: "CosmetologId");

            migrationBuilder.CreateIndex(
                name: "IX_CosmetologyAppointment_CustomerId",
                table: "CosmetologyAppointment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CosmetologyAppointment_FilialId",
                table: "CosmetologyAppointment",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_AppUserId",
                table: "Incomes",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_FilialId",
                table: "Incomes",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_StockId",
                table: "Incomes",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_KassaActionLists_AppUserId",
                table: "KassaActionLists",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_KassaActionLists_KassaId",
                table: "KassaActionLists",
                column: "KassaId");

            migrationBuilder.CreateIndex(
                name: "IX_LazerAppointmentReports_LazerAppointmentId",
                table: "LazerAppointmentReports",
                column: "LazerAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LazerAppointmentReports_LazerCategoryId",
                table: "LazerAppointmentReports",
                column: "LazerCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LazerAppointments_AppUserId",
                table: "LazerAppointments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LazerAppointments_CustomerId",
                table: "LazerAppointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LazerAppointments_FilialId",
                table: "LazerAppointments",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_LazerAppointments_LazerMasterId",
                table: "LazerAppointments",
                column: "LazerMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_LazerCategories_MainCategoryId",
                table: "LazerCategories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LazerMasters_FilialId",
                table: "LazerMasters",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_OutMoney_AppUserId",
                table: "OutMoney",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OutMoney_FilialId",
                table: "OutMoney",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_SolariumAppointment_CustomerId",
                table: "SolariumAppointment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SolariumAppointment_FilialId",
                table: "SolariumAppointment",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_SolariumAppointment_SolariumCategoriesId",
                table: "SolariumAppointment",
                column: "SolariumCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_SolariumCategories_MainCategoryId",
                table: "SolariumCategories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SolariumUsingList_SolariumAppointmentId",
                table: "SolariumUsingList",
                column: "SolariumAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_AppUserId",
                table: "Stock",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_FilialId",
                table: "Stock",
                column: "FilialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BodyShapingPacketsReports");

            migrationBuilder.DropTable(
                name: "BodyShapingSessionList");

            migrationBuilder.DropTable(
                name: "CosmetologyAppointment");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "KassaActionLists");

            migrationBuilder.DropTable(
                name: "LazerAppointmentReports");

            migrationBuilder.DropTable(
                name: "OutMoney");

            migrationBuilder.DropTable(
                name: "SolariumUsingList");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BodyShapingPacketCategory");

            migrationBuilder.DropTable(
                name: "BodyshapingAppointment");

            migrationBuilder.DropTable(
                name: "Cosmetologs");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Budget");

            migrationBuilder.DropTable(
                name: "LazerAppointments");

            migrationBuilder.DropTable(
                name: "LazerCategories");

            migrationBuilder.DropTable(
                name: "SolariumAppointment");

            migrationBuilder.DropTable(
                name: "BodyShapingMasters");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LazerMasters");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "SolariumCategories");

            migrationBuilder.DropTable(
                name: "Filials");
        }
    }
}
