using GameVault.Services.Core.Models.Genres;

namespace GameVault.Services.Core.Contracts
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllAsync();
    }
}
