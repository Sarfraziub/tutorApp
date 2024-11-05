using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TutorApp.Website.Migrations
{
    /// <inheritdoc />
    public partial class SeedClassData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassId", "ClassName" },
                values: new object[,]
                {
                    { 1, "Class 1" },
                    { 2, "Class 2" },
                    { 3, "Class 3" },
                    { 4, "Class 4" },
                    { 5, "Class 5" },
                    { 6, "Class 6" },
                    { 7, "Class 7" },
                    { 8, "Class 8" },
                    { 9, "Class 9" },
                    { 10, "Class 10" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 10);
        }
    }
}
