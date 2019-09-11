using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.Data.Migrations
{
    public partial class SeedStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Naam" },
                values: new object[] { 1, "Toekomsting Project" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Naam" },
                values: new object[] { 2, "Huidig Project" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Naam" },
                values: new object[] { 3, "Afgewert Project" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
