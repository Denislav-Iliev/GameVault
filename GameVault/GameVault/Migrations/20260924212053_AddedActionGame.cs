using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameVault.Migrations
{
    /// <inheritdoc />
    public partial class AddedActionGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Developer", "GenreId", "ImageUrl", "ReleaseYear", "Title" },
                values: new object[] { 11, "Kratos and his son Atreus journey through the Norse realms while facing gods, monsters and their own past.", "Santa Monica Studio", 1, "/images/games/god-of-war.jpg", 2018, "God of War" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
