using GameVault.Data;
using GameVault.Data.Models;
using GameVault.Services.Core.Contracts;
using GameVault.Services.Core.Models.Library;
using Microsoft.EntityFrameworkCore;
using static GameVault.GCommon.PageValidation;

namespace GameVault.Services.Core.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly GameVaultDbContext dbContext;

        public LibraryService(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<LibraryGameDto>> GetUserLibraryAsync(string userId)
        {
            return await dbContext
                .UserGames
                .AsNoTracking()
                .Where(ug => ug.UserId == userId)
                .OrderByDescending(ug => ug.HoursPlayed)
                .Take(EntitiesPerPage)
                .Select(ug => new LibraryGameDto
                {
                    Id = ug.Id,
                    GameId = ug.GameId,
                    GameTitle = ug.Game.Title,
                    Developer = ug.Game.Developer,
                    ImageUrl = ug.Game.ImageUrl,
                    Status = ug.Status,
                    HoursPlayed = ug.HoursPlayed,
                    Notes = ug.Notes,
                    AddedOn = ug.AddedOn
                })
                .ToArrayAsync();
        }

        public async Task<bool> AddToLibraryAsync(int gameId, string userId)
        {
            bool gameExists = await dbContext.Games
                .AnyAsync(g => g.Id == gameId);

            if (!gameExists)
            {
                return false;
            }

            bool alreadyAdded = await dbContext.UserGames
                .AnyAsync(ug => ug.GameId == gameId && ug.UserId == userId);

            if (alreadyAdded)
            {
                return false;
            }

            UserGame userGame = new UserGame
            {
                GameId = gameId,
                UserId = userId,
                Status = "Playing",
                HoursPlayed = 0,
                Notes = null,
                AddedOn = DateTime.UtcNow
            };

            await dbContext.UserGames.AddAsync(userGame);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveFromLibraryAsync(int userGameId, string userId)
        {
            UserGame? userGame = await dbContext.UserGames
                .FirstOrDefaultAsync(ug =>
                    ug.Id == userGameId &&
                    ug.UserId == userId);

            if (userGame == null)
            {
                return false;
            }

            dbContext.UserGames.Remove(userGame);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<LibraryEditDto?> GetForEditAsync(int id, string userId)
        {
            return await dbContext.UserGames
                .AsNoTracking()
                .Where(ug => ug.Id == id && ug.UserId == userId)
                .Select(ug => new LibraryEditDto
                {
                    Id = ug.Id,
                    GameTitle = ug.Game.Title,
                    Status = ug.Status,
                    HoursPlayed = ug.HoursPlayed,
                    Notes = ug.Notes
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> EditAsync(LibraryEditDto model, string userId)
        {
            UserGame? userGame = await dbContext.UserGames
                .FirstOrDefaultAsync(ug =>
                    ug.Id == model.Id &&
                    ug.UserId == userId);

            if (userGame == null)
            {
                return false;
            }

            userGame.Status = model.Status;
            userGame.HoursPlayed = model.HoursPlayed;
            userGame.Notes = model.Notes;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
