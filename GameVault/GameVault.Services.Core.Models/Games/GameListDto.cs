namespace GameVault.Services.Core.Models.Games
{
    public class GameListDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int ReleaseYear { get; set; }

        public string Developer { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string GenreName { get; set; } = null!;
    }
}
