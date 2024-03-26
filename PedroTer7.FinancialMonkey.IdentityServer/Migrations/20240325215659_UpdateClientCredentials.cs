using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PedroTer7.FinancialMonkey.IdentityServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDateTime", "Password" },
                values: new object[] { new DateTime(2024, 3, 25, 18, 56, 59, 580, DateTimeKind.Local).AddTicks(3492), "PHMO1FCN8:0c1028405133c1d8ca30b2efd783258b04d3f714b6bde765ad0ed5d94a19019b" });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDateTime", "Password" },
                values: new object[] { new DateTime(2024, 3, 25, 18, 56, 59, 580, DateTimeKind.Local).AddTicks(3573), "9M79NOXXQ:810a47b58436066a4d8c7c281357e251c01f162f466af7403ff55d34dafa69d3" });

            migrationBuilder.UpdateData(
                table: "ClientCredentials",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AllowedScopes", "CreationDateTime" },
                values: new object[] { "admin", new DateTime(2024, 3, 25, 18, 56, 59, 580, DateTimeKind.Local).AddTicks(4656) });

            migrationBuilder.UpdateData(
                table: "ClientCredentials",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AllowedScopes", "CreationDateTime" },
                values: new object[] { "customer", new DateTime(2024, 3, 25, 18, 56, 59, 580, DateTimeKind.Local).AddTicks(4664) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDateTime", "Password" },
                values: new object[] { new DateTime(2024, 3, 25, 18, 56, 59, 580, DateTimeKind.Local).AddTicks(1585), "VOEJGJAYN:2edd7175aea0009de3555b2a7945094ea30ca4637d2709b1f86cf30730e90e88" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDateTime", "Password" },
                values: new object[] { new DateTime(2024, 3, 25, 18, 56, 59, 580, DateTimeKind.Local).AddTicks(2130), "FO8QU3QAF:c8b4c93fd461e0dab4f39d3817eb9b89e9dea69efdc879467cbcb23684831b99" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDateTime", "Password" },
                values: new object[] { new DateTime(2024, 3, 25, 18, 56, 59, 580, DateTimeKind.Local).AddTicks(2172), "AGJVE5YKA:5d1735c9713aaee14cfa1572c147aeb864efca659b221fe62d10e5f6fda10216" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDateTime", "Password" },
                values: new object[] { new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(5323), "U1HI2N4NY:71f200f2e9db3190d8d675356c9d7a802b606d9841d715c7fbc7a4a9bf216e6e" });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDateTime", "Password" },
                values: new object[] { new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(5410), "9YR3WEX5F:69006a0d4fd00e5216ac69fe71588b7bc78a8f1144120b4bc46e3c448ccd2112" });

            migrationBuilder.UpdateData(
                table: "ClientCredentials",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AllowedScopes", "CreationDateTime" },
                values: new object[] { "openid admin", new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(6466) });

            migrationBuilder.UpdateData(
                table: "ClientCredentials",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AllowedScopes", "CreationDateTime" },
                values: new object[] { "openid customer", new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(6503) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDateTime", "Password" },
                values: new object[] { new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(3541), "PVYH7CA0N:224efedf74992dfc29264369b1c2ee93cb36065e2c3b6efc34410756ee894792" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDateTime", "Password" },
                values: new object[] { new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(3839), "7NYRKQYNR:f077f875d10879f19c6cd54a59ff0052f584b2dd9eb2b10d82407318e9c97f5a" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDateTime", "Password" },
                values: new object[] { new DateTime(2024, 3, 23, 16, 22, 3, 559, DateTimeKind.Local).AddTicks(3904), "LEYUOPYZ3:d01f90250d2ae834059ac01c61e88da5d2b79c36a156bef3a6b67a374497aa8e" });
        }
    }
}
