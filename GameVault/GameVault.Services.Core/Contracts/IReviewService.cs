using GameVault.Services.Core.Models.Reviews;

namespace GameVault.Services.Core.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetReviewsByGameAsync(int gameId);

        Task<bool> AddReviewAsync(ReviewFormDto model, string userId);

        Task<ReviewFormDto?> GetForEditAsync(int id, string userId);

        Task<bool> EditReviewAsync(ReviewFormDto model, string userId);

        Task<bool> DeleteReviewAsync(int id, string userId);

        Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(string userId);
    }
}
