using GameVault.Data;
using GameVault.Services.Core.Contracts;
using GameVault.Services.Core.Models.Genres;
using Microsoft.EntityFrameworkCore;
using static GameVault.GCommon.PageValidation;

namespace GameVault.Services.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly GameVaultDbContext dbContext;

        public GenreService(GameVaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<GenreDto>> GetAllAsync()
        {
            return await dbContext
                .Genres
                .AsNoTracking()
                .OrderBy(g => g.Id)
                .Take(EntitiesPerPage)
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToArrayAsync();
        }
    }
}
