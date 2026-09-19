using GameVault.Models;
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

        public DbSet<Game> Games { get; set; } = null!;

        public DbSet<Genre> Genres { get; set; } = null!;
    }
}