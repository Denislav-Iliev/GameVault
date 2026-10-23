using GameVault.Data;
using GameVault.Data.Models;
using GameVault.Services.Core.Contracts;
using GameVault.Services.Core.Models.Reviews;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Services.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly GameVaultDbContext dbContext;

        public ReviewService(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByGameAsync(int gameId)
        {
            return await dbContext.Reviews
                .AsNoTracking()
                .Where(r => r.GameId == gameId)
                .OrderByDescending(r => r.CreatedOn)
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    GameId = r.GameId,
                    Rating = r.Rating,
                    Content = r.Content,
                    CreatedOn = r.CreatedOn,
                    UserId = r.UserId,
                    UserName = r.User.UserName!
                })
                .ToArrayAsync();
        }

        public async Task<bool> AddReviewAsync(ReviewFormDto model, string userId)
        {
            bool gameExists = await dbContext.Games
                .AnyAsync(g => g.Id == model.GameId);

            if (!gameExists)
            {
                return false;
            }

            bool alreadyReviewed = await dbContext.Reviews
                .AnyAsync(r =>
                    r.GameId == model.GameId &&
                    r.UserId == userId);

            if (alreadyReviewed)
            {
                return false;
            }

            Review review = new Review
            {
                GameId = model.GameId,
                UserId = userId,
                Rating = model.Rating,
                Content = model.Content,
                CreatedOn = DateTime.UtcNow
            };

            await dbContext.Reviews.AddAsync(review);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<ReviewFormDto?> GetForEditAsync(int id, string userId)
        {
            return await dbContext.Reviews
                .AsNoTracking()
                .Where(r => r.Id == id && r.UserId == userId)
                .Select(r => new ReviewFormDto
                {
                    Id = r.Id,
                    GameId = r.GameId,
                    Rating = r.Rating,
                    Content = r.Content
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> EditReviewAsync(ReviewFormDto model, string userId)
        {
            Review? review = await dbContext.Reviews
                .FirstOrDefaultAsync(r =>
                    r.Id == model.Id &&
                    r.UserId == userId);

            if (review == null)
            {
                return false;
            }

            review.Rating = model.Rating;
            review.Content = model.Content;

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteReviewAsync(int id, string userId)
        {
            Review? review = await dbContext.Reviews
                .FirstOrDefaultAsync(r =>
                    r.Id == id &&
                    r.UserId == userId);

            if (review == null)
            {
                return false;
            }

            dbContext.Reviews.Remove(review);
            await dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(string userId)
        {
            return await dbContext.Reviews
                .AsNoTracking()
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedOn)
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    GameId = r.GameId,
                    GameTitle = r.Game.Title,
                    Rating = r.Rating,
                    Content = r.Content,
                    CreatedOn = r.CreatedOn,
                    UserId = r.UserId,
                    UserName = r.User.UserName!
                })
                .ToArrayAsync();
        }

    }
}
