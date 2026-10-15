
using GameVault.Services.Core.Models.Games;
using GameVault.Services.Core.Models.Genres;

namespace GameVault.Services.Core.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameListDto>> GetAllAsync(
            int? genreId = null,
            string? searchTerm = null);

        Task<GameDetailsDto?> GetDetailsAsync(int id);

        Task<IEnumerable<GenreDto>> GetGenresAsync();

        Task<GameFormDto?> GetForEditAsync(int id);

        Task<GameDeleteDto?> GetForDeleteAsync(int id);

        Task<bool> GenreExistsAsync(int genreId);

        Task CreateAsync(GameFormDto model);

        Task<bool> EditAsync(int id, GameFormDto model);

        Task<bool> DeleteAsync(int id);
    }
}
