using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutorApp.Website.Migrations
{
    /// <inheritdoc />
    public partial class settingTblrestruct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Settings",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "LogoImageUrl",
                table: "Settings",
                newName: "Key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Settings",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Settings",
                newName: "LogoImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
