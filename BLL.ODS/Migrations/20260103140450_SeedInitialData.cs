using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BLL.ODS.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "Description", "ImageUrl", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Electronic devices", null, true, "Electronics" },
                    { 2, "Computer accessories", null, true, "Accessories" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsActive", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 3, 18, 4, 50, 252, DateTimeKind.Local).AddTicks(240), "Mechanical keyboard", null, true, "Keyboard", 49.99m, 50 },
                    { 2, 2, new DateTime(2026, 1, 3, 18, 4, 50, 252, DateTimeKind.Local).AddTicks(242), "Wireless mouse", null, true, "Mouse", 29.99m, 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
