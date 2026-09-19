using GameVault.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Data
{
    public class GameVaultDbContext : DbContext
    {
        public GameVaultDbContext(
            DbContextOptions<GameVaultDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; } = null!;

        public virtual DbSet<Genre> Genres { get; set; } = null!;
    }
}