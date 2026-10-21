using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameVault.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToLibrary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserGames",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_UserId_GameId",
                table: "UserGames",
                columns: new[] { "UserId", "GameId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGames_AspNetUsers_UserId",
                table: "UserGames",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_AspNetUsers_UserId",
                table: "UserGames");

            migrationBuilder.DropIndex(
                name: "IX_UserGames_UserId_GameId",
                table: "UserGames");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserGames");
        }
    }
}
