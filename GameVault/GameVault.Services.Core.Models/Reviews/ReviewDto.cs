
namespace GameVault.Services.Core.Models.Reviews
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int Rating { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string GameTitle { get; set; } = null!;
    }
}
