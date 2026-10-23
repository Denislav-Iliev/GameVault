using GameVault.Data.Models;
using GameVault.Data;
using GameVault.Services.Core.Contracts;
using GameVault.Services.Core.Models.Games;
using GameVault.Services.Core.Models.Genres;
using Microsoft.EntityFrameworkCore;
using static GameVault.GCommon.PageValidation;

namespace GameVault.Services.Core.Services
{
    public class GameService : IGameService
    {
        private readonly GameVaultDbContext dbContext;

        public GameService(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<GameListDto>> GetAllAsync(
            int? genreId = null,
            string? searchTerm = null)
        {
            IQueryable<Game> gamesQuery = dbContext
                .Games
                .AsNoTracking();

            if (genreId != null)
            {
                gamesQuery = gamesQuery
                    .Where(g => g.GenreId == genreId);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                gamesQuery = gamesQuery
                    .Where(g => EF.Functions.Like(g.Title, $"%{searchTerm}%"));
            }

            return await gamesQuery
                .OrderByDescending(g => g.ReleaseYear)
                .Take(EntitiesPerPage)
                .Select(g => new GameListDto
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    ReleaseYear = g.ReleaseYear,
                    Developer = g.Developer,
                    ImageUrl = g.ImageUrl,
                    GenreName = g.Genre.Name
                })
                .ToArrayAsync();
        }

        public async Task<GameDetailsDto?> GetDetailsAsync(int id)
        {
            return await dbContext
                .Games
                .AsNoTracking()
                .Where(g => g.Id == id)
                .Select(g => new GameDetailsDto
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    ReleaseYear = g.ReleaseYear,
                    Developer = g.Developer,
                    ImageUrl = g.ImageUrl,
                    GenreName = g.Genre.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GenreDto>> GetGenresAsync()
        {
            return await dbContext
                .Genres
                .AsNoTracking()
                .OrderBy(g => g.Name)
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToArrayAsync();
        }

        public async Task<GameFormDto?> GetForEditAsync(int id)
        {
            return await dbContext
                .Games
                .AsNoTracking()
                .Where(g => g.Id == id)
                .Select(g => new GameFormDto
                {
                    Title = g.Title,
                    Description = g.Description,
                    ReleaseYear = g.ReleaseYear,
                    Developer = g.Developer,
                    ImageUrl = g.ImageUrl,
                    GenreId = g.GenreId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<GameDeleteDto?> GetForDeleteAsync(int id)
        {
            return await dbContext
                .Games
                .AsNoTracking()
                .Where(g => g.Id == id)
                .Select(g => new GameDeleteDto
                {
                    Id = g.Id,
                    Title = g.Title
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> GenreExistsAsync(int genreId)
        {
            return await dbContext
                .Genres
                .AnyAsync(g => g.Id == genreId);
        }

        public async Task CreateAsync(GameFormDto model)
        {
            Game newGame = new Game
            {
                Title = model.Title,
                Description = model.Description,
                ReleaseYear = model.ReleaseYear,
                Developer = model.Developer,
                ImageUrl = model.ImageUrl,
                GenreId = model.GenreId
            };

            await dbContext.Games.AddAsync(newGame);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> EditAsync(int id, GameFormDto model)
        {
            Game? gameToEdit = await dbContext
                .Games
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gameToEdit == null)
            {
                return false;
            }

            gameToEdit.Title = model.Title;
            gameToEdit.Description = model.Description;
            gameToEdit.ReleaseYear = model.ReleaseYear;
            gameToEdit.Developer = model.Developer;
            gameToEdit.ImageUrl = model.ImageUrl;
            gameToEdit.GenreId = model.GenreId;

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Game? gameToDelete = await dbContext
                .Games
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gameToDelete == null)
            {
                return false;
            }

            dbContext.Games.Remove(gameToDelete);

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
