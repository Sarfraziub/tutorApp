using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TutorApp.Website.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "ClassId", "Email", "IsApproved", "Name", "PasswordHash" },
                values: new object[,]
                {
                    { 1, 1, "alice@example.com", false, "Alice", "hashedPassword1" },
                    { 2, 1, "bob@example.com", false, "Bob", "hashedPassword2" },
                    { 3, 2, "charlie@example.com", false, "Charlie", "hashedPassword3" },
                    { 4, 2, "david@example.com", false, "David", "hashedPassword4" },
                    { 5, 3, "eve@example.com", false, "Eve", "hashedPassword5" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "ClassId", "SubjectName" },
                values: new object[,]
                {
                    { 1, 1, "Mathematics" },
                    { 2, 1, "Science" },
                    { 3, 2, "History" },
                    { 4, 2, "Geography" },
                    { 5, 3, "Literature" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "TopicId", "SubjectId", "TopicName" },
                values: new object[,]
                {
                    { 1, 1, "Algebra" },
                    { 2, 1, "Geometry" },
                    { 3, 2, "Physics Basics" },
                    { 4, 3, "World War I" },
                    { 5, 4, "Continents and Oceans" }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "FileId", "FileName", "FilePath", "TopicId" },
                values: new object[,]
                {
                    { 1, "Math Homework", "/", 1 },
                    { 2, "Science Project", "/", 3 },
                    { 3, "History Essay", "/", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "FileId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "FileId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "FileId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 3);
        }
    }
}
