using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameVault.Migrations
{
    /// <inheritdoc />
    public partial class SeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" },
                    { 3, "RPG" },
                    { 4, "Strategy" },
                    { 5, "Shooter" },
                    { 6, "Simulation" },
                    { 7, "Sports" },
                    { 8, "Horror" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Developer", "GenreId", "ImageUrl", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 1, "An open-world RPG following Geralt of Rivia on his search for Ciri.", "CD Projekt Red", 3, null, 2015, "The Witcher 3: Wild Hunt" },
                    { 2, "A remastered action RPG focused on character progression, loot and challenging combat.", "Blizzard Entertainment", 3, null, 2021, "Diablo II: Resurrected" },
                    { 3, "An open-world action RPG set in the mysterious Lands Between.", "FromSoftware", 3, null, 2022, "Elden Ring" },
                    { 4, "A competitive tactical first-person shooter focused on team-based matches.", "Valve", 5, null, 2023, "Counter-Strike 2" },
                    { 5, "A fast-paced first-person shooter featuring intense combat against demonic forces.", "id Software", 5, null, 2020, "DOOM Eternal" },
                    { 6, "A turn-based strategy game about building and developing a civilization.", "Firaxis Games", 4, null, 2016, "Civilization VI" },
                    { 7, "A survival horror game following Leon Kennedy on a dangerous rescue mission.", "Capcom", 8, null, 2023, "Resident Evil 4" },
                    { 8, "A flight simulation game featuring detailed aircraft and locations around the world.", "Asobo Studio", 6, null, 2020, "Microsoft Flight Simulator" },
                    { 9, "A football simulation game featuring clubs, players and competitions from around the world.", "EA Sports", 7, null, 2024, "EA Sports FC 25" },
                    { 10, "An open-world adventure following outlaw Arthur Morgan and the Van der Linde gang.", "Rockstar Games", 2, null, 2018, "Red Dead Redemption 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
