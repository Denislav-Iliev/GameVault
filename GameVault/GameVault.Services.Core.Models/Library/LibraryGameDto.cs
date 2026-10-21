namespace GameVault.Services.Core.Models.Library
{
    public class LibraryGameDto
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public string GameTitle { get; set; } = null!;

        public string Developer { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string Status { get; set; } = null!;

        public int HoursPlayed { get; set; }

        public string? Notes { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
