using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initalize : Migration
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
                name: "BodyShapingPacketCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Packet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainCategoryId = table.Column<int>(type: "int", nullable: true),
                    SessionCount = table.Column<int>(type: "int", nullable: true),
                    SessionDuration = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyShapingPacketCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyShapingPacketCategories_BodyShapingPacketCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "BodyShapingPacketCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cosmetologs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cosmetologs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CosmetologyCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainCategoryId = table.Column<int>(type: "int", nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CosmetologyCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CosmetologyCategories_CosmetologyCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "CosmetologyCategories",
                        principalColumn: "Id");
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
                name: "LazerMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LazerMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LipuckaCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainCategoryId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LipuckaCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LipuckaCategories_LipuckaCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "LipuckaCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PirsinqCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainCategoryId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PirsinqCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PirsinqCategories_PirsinqCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "PirsinqCategories",
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
                    UsingPeriod = table.Column<int>(type: "int", nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
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
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyShapingMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyShapingMasters_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CosmetologFilials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CosmetologsId = table.Column<int>(type: "int", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CosmetologFilials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CosmetologFilials_Cosmetologs_CosmetologsId",
                        column: x => x.CosmetologsId,
                        principalTable: "Cosmetologs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CosmetologFilials_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Female = table.Column<bool>(type: "bit", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LazerCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainCategoryId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LazerCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LazerCategories_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LazerCategories_LazerCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "LazerCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpendCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpendCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpendCategories_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LazerMasterFilial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LazerMasterId = table.Column<int>(type: "int", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LazerMasterFilial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LazerMasterFilial_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LazerMasterFilial_LazerMasters_LazerMasterId",
                        column: x => x.LazerMasterId,
                        principalTable: "LazerMasters",
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
                name: "KassaActionLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastOutMoneyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OutMoneyQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                        name: "FK_KassaActionLists_Filials_FilialId",
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
                    TotalCount = table.Column<int>(type: "int", nullable: false),
                    SellingCount = table.Column<int>(type: "int", nullable: false),
                    RemainCount = table.Column<int>(type: "int", nullable: false),
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
                name: "BodyShapingAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false),
                    BodyshapingMasterId = table.Column<int>(type: "int", nullable: false),
                    RemaingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReturnMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyShapingAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyShapingAppointments_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodyShapingAppointments_BodyShapingMasters_BodyshapingMasterId",
                        column: x => x.BodyshapingMasterId,
                        principalTable: "BodyShapingMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BodyShapingAppointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodyShapingAppointments_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CosmetologyAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CosmetologId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CosmetologyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStart = table.Column<bool>(type: "bit", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CosmetologyAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CosmetologyAppointments_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CosmetologyAppointments_Cosmetologs_CosmetologId",
                        column: x => x.CosmetologId,
                        principalTable: "Cosmetologs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CosmetologyAppointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CosmetologyAppointments_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "LipuckaAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LazerMasterId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsStart = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LipuckaAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LipuckaAppointments_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LipuckaAppointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LipuckaAppointments_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LipuckaAppointments_LazerMasters_LazerMasterId",
                        column: x => x.LazerMasterId,
                        principalTable: "LazerMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PirsinqAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LazerMasterId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false),
                    IsStart = table.Column<bool>(type: "bit", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    FilialId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PirsinqAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PirsinqAppointments_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PirsinqAppointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PirsinqAppointments_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PirsinqAppointments_LazerMasters_LazerMasterId",
                        column: x => x.LazerMasterId,
                        principalTable: "LazerMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolariumAppointments",
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
                    FilialId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RemainingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsTimeOut = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ReturnMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolariumAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolariumAppointments_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolariumAppointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolariumAppointments_Filials_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolariumAppointments_SolariumCategories_SolariumCategoriesId",
                        column: x => x.SolariumCategoriesId,
                        principalTable: "SolariumCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutMoney",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SpendCategoryId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_OutMoney_SpendCategories_SpendCategoryId",
                        column: x => x.SpendCategoryId,
                        principalTable: "SpendCategories",
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
                    BuyingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                        name: "FK_BodyShapingPacketsReports_BodyShapingAppointments_BodyshapingAppointmentsId",
                        column: x => x.BodyshapingAppointmentsId,
                        principalTable: "BodyShapingAppointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodyShapingPacketsReports_BodyShapingPacketCategories_BodyShapingPacketCategoryId",
                        column: x => x.BodyShapingPacketCategoryId,
                        principalTable: "BodyShapingPacketCategories",
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
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    SessionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyShapingSessionList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyShapingSessionList_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BodyShapingSessionList_BodyShapingAppointments_BodyShapingAppointmentId",
                        column: x => x.BodyShapingAppointmentId,
                        principalTable: "BodyShapingAppointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CosmetologyReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CosmetologyAppointmentId = table.Column<int>(type: "int", nullable: false),
                    CosmetologyCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CosmetologyReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CosmetologyReport_CosmetologyAppointments_CosmetologyAppointmentId",
                        column: x => x.CosmetologyAppointmentId,
                        principalTable: "CosmetologyAppointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CosmetologyReport_CosmetologyCategories_CosmetologyCategoryId",
                        column: x => x.CosmetologyCategoryId,
                        principalTable: "CosmetologyCategories",
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

            migrationBuilder.CreateTable(
                name: "LipuckaReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LipuckaAppointmentId = table.Column<int>(type: "int", nullable: false),
                    LipuckaCategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LipuckaReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LipuckaReports_LipuckaAppointments_LipuckaAppointmentId",
                        column: x => x.LipuckaAppointmentId,
                        principalTable: "LipuckaAppointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LipuckaReports_LipuckaCategories_LipuckaCategoriesId",
                        column: x => x.LipuckaCategoriesId,
                        principalTable: "LipuckaCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PirsinqReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PirsinqAppointmentId = table.Column<int>(type: "int", nullable: false),
                    PirsinqCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PirsinqReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PirsinqReports_PirsinqAppointments_PirsinqAppointmentId",
                        column: x => x.PirsinqAppointmentId,
                        principalTable: "PirsinqAppointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PirsinqReports_PirsinqCategories_PirsinqCategoryId",
                        column: x => x.PirsinqCategoryId,
                        principalTable: "PirsinqCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolariumUsingLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolariumAppointmentId = table.Column<int>(type: "int", nullable: false),
                    UsingMinute = table.Column<int>(type: "int", nullable: false),
                    RemainingMinute = table.Column<int>(type: "int", nullable: false),
                    UsingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolariumUsingLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolariumUsingLists_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolariumUsingLists_SolariumAppointments_SolariumAppointmentId",
                        column: x => x.SolariumAppointmentId,
                        principalTable: "SolariumAppointments",
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
                    { "qwertyu1234", null, "Reservator", "RESERVATOR" }
                });

            migrationBuilder.InsertData(
                table: "Cosmetologs",
                columns: new[] { "Id", "FullName", "IsDeactive" },
                values: new object[] { 1, "Nuray", false });

            migrationBuilder.InsertData(
                table: "Filials",
                columns: new[] { "Id", "FilialName" },
                values: new object[,]
                {
                    { 1, "Arzum Mini Laser Studio" },
                    { 2, "Arzum Beauty Studio" },
                    { 3, "Arzum Estetik Studio" },
                    { 4, "İdarə Paneli" },
                    { 5, "Texniki Dəstək" }
                });

            migrationBuilder.InsertData(
                table: "LazerMasters",
                columns: new[] { "Id", "FullName", "IsDeactive" },
                values: new object[,]
                {
                    { 1, "Ellada", false },
                    { 2, "Aidə", false },
                    { 3, "Nuridə", false },
                    { 4, "Gülnar", false },
                    { 5, "Nəzrin", false },
                    { 6, "Əminə", false }
                });

            migrationBuilder.InsertData(
                table: "SolariumCategories",
                columns: new[] { "Id", "IsDeactive", "MainCategoryId", "Minute", "Name", "Price", "UsingPeriod" },
                values: new object[,]
                {
                    { 1, false, null, 0, "Günlük", 0m, null },
                    { 2, false, null, 0, "Aylıq", 0m, null }
                });

            migrationBuilder.InsertData(
                table: "LazerCategories",
                columns: new[] { "Id", "FilialId", "IsDeactive", "MainCategoryId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, false, null, "Qadın", 0m },
                    { 2, 1, false, null, " Kişi", 0m },
                    { 3, 2, false, null, "Qadın", 0m },
                    { 4, 2, false, null, "Kişi", 0m },
                    { 5, 3, false, null, "Qadın", 0m }
                });

            migrationBuilder.InsertData(
                table: "SolariumCategories",
                columns: new[] { "Id", "IsDeactive", "MainCategoryId", "Minute", "Name", "Price", "UsingPeriod" },
                values: new object[,]
                {
                    { 3, false, 1, 5, "MiniPacket", 4m, 1 },
                    { 4, false, 1, 10, "MediumPacket", 8m, 1 },
                    { 5, false, 1, 15, "LargePacket", 10m, 1 },
                    { 6, false, 1, 20, "ExtraLarge", 12m, 30 },
                    { 7, false, 2, 50, "MiniPacket", 19m, 30 },
                    { 8, false, 2, 100, "MediumPacket", 29m, 30 },
                    { 9, false, 2, 150, "LargePacket", 39m, 60 }
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
                name: "IX_BodyShapingAppointments_AppUserId",
                table: "BodyShapingAppointments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyShapingAppointments_BodyshapingMasterId",
                table: "BodyShapingAppointments",
                column: "BodyshapingMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyShapingAppointments_CustomerId",
                table: "BodyShapingAppointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyShapingAppointments_FilialId",
                table: "BodyShapingAppointments",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyShapingMasters_FilialId",
                table: "BodyShapingMasters",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyShapingPacketCategories_MainCategoryId",
                table: "BodyShapingPacketCategories",
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
                name: "IX_BodyShapingSessionList_AppUserId",
                table: "BodyShapingSessionList",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyShapingSessionList_BodyShapingAppointmentId",
                table: "BodyShapingSessionList",
                column: "BodyShapingAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CosmetologFilials_CosmetologsId",
                table: "CosmetologFilials",
                column: "CosmetologsId");

            migrationBuilder.CreateIndex(
                name: "IX_CosmetologFilials_FilialId",
                table: "CosmetologFilials",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_CosmetologyAppointments_AppUserId",
                table: "CosmetologyAppointments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CosmetologyAppointments_CosmetologId",
                table: "CosmetologyAppointments",
                column: "CosmetologId");

            migrationBuilder.CreateIndex(
                name: "IX_CosmetologyAppointments_CustomerId",
                table: "CosmetologyAppointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CosmetologyAppointments_FilialId",
                table: "CosmetologyAppointments",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_CosmetologyCategories_MainCategoryId",
                table: "CosmetologyCategories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CosmetologyReport_CosmetologyAppointmentId",
                table: "CosmetologyReport",
                column: "CosmetologyAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CosmetologyReport_CosmetologyCategoryId",
                table: "CosmetologyReport",
                column: "CosmetologyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_FilialId",
                table: "Customers",
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
                name: "IX_KassaActionLists_FilialId",
                table: "KassaActionLists",
                column: "FilialId");

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
                name: "IX_LazerCategories_FilialId",
                table: "LazerCategories",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_LazerCategories_MainCategoryId",
                table: "LazerCategories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LazerMasterFilial_FilialId",
                table: "LazerMasterFilial",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_LazerMasterFilial_LazerMasterId",
                table: "LazerMasterFilial",
                column: "LazerMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_LipuckaAppointments_AppUserId",
                table: "LipuckaAppointments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LipuckaAppointments_CustomerId",
                table: "LipuckaAppointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LipuckaAppointments_FilialId",
                table: "LipuckaAppointments",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_LipuckaAppointments_LazerMasterId",
                table: "LipuckaAppointments",
                column: "LazerMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_LipuckaCategories_MainCategoryId",
                table: "LipuckaCategories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LipuckaReports_LipuckaAppointmentId",
                table: "LipuckaReports",
                column: "LipuckaAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LipuckaReports_LipuckaCategoriesId",
                table: "LipuckaReports",
                column: "LipuckaCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_OutMoney_AppUserId",
                table: "OutMoney",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OutMoney_SpendCategoryId",
                table: "OutMoney",
                column: "SpendCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PirsinqAppointments_AppUserId",
                table: "PirsinqAppointments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PirsinqAppointments_CustomerId",
                table: "PirsinqAppointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PirsinqAppointments_FilialId",
                table: "PirsinqAppointments",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_PirsinqAppointments_LazerMasterId",
                table: "PirsinqAppointments",
                column: "LazerMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PirsinqCategories_MainCategoryId",
                table: "PirsinqCategories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PirsinqReports_PirsinqAppointmentId",
                table: "PirsinqReports",
                column: "PirsinqAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PirsinqReports_PirsinqCategoryId",
                table: "PirsinqReports",
                column: "PirsinqCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SolariumAppointments_AppUserId",
                table: "SolariumAppointments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolariumAppointments_CustomerId",
                table: "SolariumAppointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SolariumAppointments_FilialId",
                table: "SolariumAppointments",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_SolariumAppointments_SolariumCategoriesId",
                table: "SolariumAppointments",
                column: "SolariumCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_SolariumCategories_MainCategoryId",
                table: "SolariumCategories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SolariumUsingLists_AppUserId",
                table: "SolariumUsingLists",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolariumUsingLists_SolariumAppointmentId",
                table: "SolariumUsingLists",
                column: "SolariumAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SpendCategories_FilialId",
                table: "SpendCategories",
                column: "FilialId");

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
                name: "CosmetologFilials");

            migrationBuilder.DropTable(
                name: "CosmetologyReport");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "KassaActionLists");

            migrationBuilder.DropTable(
                name: "LazerAppointmentReports");

            migrationBuilder.DropTable(
                name: "LazerMasterFilial");

            migrationBuilder.DropTable(
                name: "LipuckaReports");

            migrationBuilder.DropTable(
                name: "OutMoney");

            migrationBuilder.DropTable(
                name: "PirsinqReports");

            migrationBuilder.DropTable(
                name: "SolariumUsingLists");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BodyShapingPacketCategories");

            migrationBuilder.DropTable(
                name: "BodyShapingAppointments");

            migrationBuilder.DropTable(
                name: "CosmetologyAppointments");

            migrationBuilder.DropTable(
                name: "CosmetologyCategories");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "LazerAppointments");

            migrationBuilder.DropTable(
                name: "LazerCategories");

            migrationBuilder.DropTable(
                name: "LipuckaAppointments");

            migrationBuilder.DropTable(
                name: "LipuckaCategories");

            migrationBuilder.DropTable(
                name: "SpendCategories");

            migrationBuilder.DropTable(
                name: "PirsinqAppointments");

            migrationBuilder.DropTable(
                name: "PirsinqCategories");

            migrationBuilder.DropTable(
                name: "SolariumAppointments");

            migrationBuilder.DropTable(
                name: "BodyShapingMasters");

            migrationBuilder.DropTable(
                name: "Cosmetologs");

            migrationBuilder.DropTable(
                name: "LazerMasters");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "SolariumCategories");

            migrationBuilder.DropTable(
                name: "Filials");
        }
    }
}
