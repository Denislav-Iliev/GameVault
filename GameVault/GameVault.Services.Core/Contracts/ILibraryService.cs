using GameVault.Services.Core.Models.Library;

namespace GameVault.Services.Core.Contracts
{
    public interface ILibraryService
    {
        Task<IEnumerable<LibraryGameDto>> GetUserLibraryAsync(string userId);

        Task<bool> AddToLibraryAsync(int gameId, string userId);

        Task<bool> RemoveFromLibraryAsync(int userGameId, string userId);
    }
}