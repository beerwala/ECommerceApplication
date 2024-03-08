using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrasturcture.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "country",
                columns: new[] { "Id", "CountryName" },
                values: new object[,]
                {
                    { 1, "India" },
                    { 2, "USA" },
                    { 3, "China" }
                });

            migrationBuilder.InsertData(
                table: "states",
                columns: new[] { "Id", "CountryId", "StateName" },
                values: new object[,]
                {
                    { 1, 1, "Delhi" },
                    { 2, 1, "Maharashtra" },
                    { 3, 2, "California" },
                    { 4, 2, "New York" },
                    { 5, 3, "Beijing" },
                    { 6, 3, "Shanghai" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "states",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "states",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "states",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "states",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "states",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "states",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
