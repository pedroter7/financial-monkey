using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PedroTer7.FinancialMonkey.IdentityServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(74)", maxLength: 74, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "UTC_TIMESTAMP(0)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClientCredentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Secret = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AllowedGrantTypes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AllowedScopes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "UTC_TIMESTAMP(0)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCredentials", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(74)", maxLength: 74, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "UTC_TIMESTAMP(0)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreationDateTime", "Email", "Password" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(5323), "adm1@financialmonkey.com", "U1HI2N4NY:71f200f2e9db3190d8d675356c9d7a802b606d9841d715c7fbc7a4a9bf216e6e" },
                    { 2, new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(5410), "adm2@financialmonkey.com", "9YR3WEX5F:69006a0d4fd00e5216ac69fe71588b7bc78a8f1144120b4bc46e3c448ccd2112" }
                });

            migrationBuilder.InsertData(
                table: "ClientCredentials",
                columns: new[] { "Id", "AllowedGrantTypes", "AllowedScopes", "CreationDateTime", "Description", "Key", "Name", "Secret" },
                values: new object[,]
                {
                    { 1, "password", "openid admin", new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(6466), "Client for admin users authentication", "6f65a068-e943-11ee-b79b-03fb35a7a73f", "Admin auth client", "9297320e-e943-11ee-882f-bf8013a44c6f" },
                    { 2, "password", "openid customer", new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(6503), "Client for customer users authentication", "ca32537e-e943-11ee-b968-2f83f8d52888", "Customer auth client", "d0dec608-e943-11ee-ad8c-532040233037" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreationDateTime", "Email", "Password" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(3541), "customer_1@gmail.com", "PVYH7CA0N:224efedf74992dfc29264369b1c2ee93cb36065e2c3b6efc34410756ee894792" },
                    { 2, new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(3839), "customer_2@somecorp.com", "7NYRKQYNR:f077f875d10879f19c6cd54a59ff0052f584b2dd9eb2b10d82407318e9c97f5a" },
                    { 3, new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(3904), "customer_3@thirdcopr.com", "LEYUOPYZ3:d01f90250d2ae834059ac01c61e88da5d2b79c36a156bef3a6b67a374497aa8e" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Email",
                table: "Admins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientCredentials_Key",
                table: "ClientCredentials",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ClientCredentials");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
