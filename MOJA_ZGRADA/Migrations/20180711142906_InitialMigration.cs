using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOJA_ZGRADA.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    First_Name = table.Column<string>(nullable: true),
                    Last_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Admin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    First_Name = table.Column<string>(maxLength: 15, nullable: false),
                    Last_Name = table.Column<string>(maxLength: 15, nullable: false),
                    Date_Of_Birth = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    JMBG = table.Column<int>(maxLength: 13, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Building",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nickname = table.Column<string>(maxLength: 20, nullable: true),
                    Date_Of_Creation = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: false),
                    Number_Of_Apartments = table.Column<int>(maxLength: 3, nullable: false),
                    Number_Of_Tenants = table.Column<int>(maxLength: 3, nullable: false),
                    Number_Of_Parking_Places = table.Column<int>(maxLength: 3, nullable: false),
                    Number_Of_Basements = table.Column<int>(maxLength: 3, nullable: false),
                    Number_Of_Entrances = table.Column<int>(maxLength: 3, nullable: false),
                    Number_Of_Floors = table.Column<int>(maxLength: 3, nullable: false),
                    Special_Apartments_Annotation = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Building", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Cleaning_Plan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cleaning_Type = table.Column<string>(maxLength: 50, nullable: false),
                    Cleaning_Price = table.Column<float>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Cleaning_Plan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Created_Cleaning_Plan",
                columns: table => new
                {
                    Cleaning_Plan_Id = table.Column<int>(nullable: false),
                    Building_Id = table.Column<int>(nullable: false),
                    Admin_Id = table.Column<int>(nullable: false),
                    Cleaning_Issued_DateTime = table.Column<DateTime>(nullable: false),
                    Cleaning_DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Created_Cleaning_Plan", x => new { x.Cleaning_Plan_Id, x.Building_Id, x.Admin_Id });
                });

            migrationBuilder.CreateTable(
                name: "tbl_Handles",
                columns: table => new
                {
                    Admin_Id = table.Column<int>(nullable: false),
                    Building_Id = table.Column<int>(nullable: false),
                    Started_Working_DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Handles", x => new { x.Admin_Id, x.Building_Id });
                });

            migrationBuilder.CreateTable(
                name: "tbl_Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Invoice_Type = table.Column<string>(maxLength: 50, nullable: false),
                    Invoice_Amount = table.Column<float>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Issued_Invoice",
                columns: table => new
                {
                    Invoice_Id = table.Column<int>(nullable: false),
                    Tenant_Id = table.Column<int>(nullable: false),
                    Admin_Id = table.Column<int>(nullable: false),
                    Building_Id = table.Column<int>(nullable: false),
                    Invoice_Creation_DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Issued_Invoice", x => new { x.Invoice_Id, x.Tenant_Id, x.Admin_Id, x.Building_Id });
                });

            migrationBuilder.CreateTable(
                name: "tbl_Lives_In",
                columns: table => new
                {
                    Tenant_Id = table.Column<int>(nullable: false),
                    Building_Id = table.Column<int>(nullable: false),
                    Started_Living_DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Lives_In", x => new { x.Tenant_Id, x.Building_Id });
                });

            migrationBuilder.CreateTable(
                name: "tbl_Message",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Admin_Id = table.Column<int>(nullable: false),
                    Tenant_Id = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    File_URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Message", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Notification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Post",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Admin_Id = table.Column<int>(nullable: false),
                    Building_Id = table.Column<int>(nullable: false),
                    Post_Name = table.Column<string>(maxLength: 15, nullable: false),
                    Post_Priority = table.Column<string>(maxLength: 15, nullable: false),
                    Post_Creation_DateTime = table.Column<DateTime>(nullable: false),
                    Post_Update_DateTime = table.Column<DateTime>(nullable: false),
                    Post_LifeTime_DateTime = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    File_URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Post", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    First_Name = table.Column<string>(maxLength: 15, nullable: false),
                    Last_Name = table.Column<string>(maxLength: 15, nullable: false),
                    Date_Of_Birth = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    JMBG = table.Column<int>(maxLength: 13, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: false),
                    Apartment_Number = table.Column<string>(maxLength: 10, nullable: false),
                    Floor_Number = table.Column<int>(maxLength: 3, nullable: false),
                    Number_Of_Occupants = table.Column<int>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Admin_Email_JMBG",
                table: "tbl_Admin",
                columns: new[] { "Email", "JMBG" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Building_Address",
                table: "tbl_Building",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Cleaning_Plan_Cleaning_Type_Cleaning_Price",
                table: "tbl_Cleaning_Plan",
                columns: new[] { "Cleaning_Type", "Cleaning_Price" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Invoice_Invoice_Type_Invoice_Amount",
                table: "tbl_Invoice",
                columns: new[] { "Invoice_Type", "Invoice_Amount" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Tenant_Email_JMBG_Address",
                table: "tbl_Tenant",
                columns: new[] { "Email", "JMBG", "Address" },
                unique: true);
        }

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
                name: "tbl_Admin");

            migrationBuilder.DropTable(
                name: "tbl_Building");

            migrationBuilder.DropTable(
                name: "tbl_Cleaning_Plan");

            migrationBuilder.DropTable(
                name: "tbl_Created_Cleaning_Plan");

            migrationBuilder.DropTable(
                name: "tbl_Handles");

            migrationBuilder.DropTable(
                name: "tbl_Invoice");

            migrationBuilder.DropTable(
                name: "tbl_Issued_Invoice");

            migrationBuilder.DropTable(
                name: "tbl_Lives_In");

            migrationBuilder.DropTable(
                name: "tbl_Message");

            migrationBuilder.DropTable(
                name: "tbl_Notification");

            migrationBuilder.DropTable(
                name: "tbl_Post");

            migrationBuilder.DropTable(
                name: "tbl_Tenant");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
