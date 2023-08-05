using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NavigationModule.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("34440aea-c056-4917-acee-eb14cea3ec47"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bd44bb83-99fb-4625-9564-2a0ba54f0608"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b6b752d5-bb8e-4309-8387-84537fdf3de4"), "596962eb-0eb5-4dae-b54d-1c55aa7848e5", "User", "USER" },
                    { new Guid("f7d95636-dd64-4507-80bb-c8d07dadc436"), "01359364-b781-4dd4-bbe7-710ab23722a0", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b6b752d5-bb8e-4309-8387-84537fdf3de4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f7d95636-dd64-4507-80bb-c8d07dadc436"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("34440aea-c056-4917-acee-eb14cea3ec47"), null, "Admin", null },
                    { new Guid("bd44bb83-99fb-4625-9564-2a0ba54f0608"), null, "User", null }
                });
        }
    }
}
