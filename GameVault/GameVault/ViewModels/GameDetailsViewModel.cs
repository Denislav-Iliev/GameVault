using GameVault.Services.Core.Models.Games;
using GameVault.Services.Core.Models.Reviews;

namespace GameVault.ViewModels
{
    public class GameDetailsViewModel
    {
        public GameDetailsDto Game { get; set; } = null!;

        public IEnumerable<ReviewDto> Reviews { get; set; }
            = new List<ReviewDto>();

        public ReviewFormDto NewReview { get; set; } = new ReviewFormDto();

        public string? CurrentUserId { get; set; }
    }
}