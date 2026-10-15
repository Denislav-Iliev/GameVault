namespace GameVault.Services.Core.Models.Reviews
{
    public class ReviewListDto
    {
        public string GameTitle { get; set; } = null!;

        public int Rating { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
    }
}
